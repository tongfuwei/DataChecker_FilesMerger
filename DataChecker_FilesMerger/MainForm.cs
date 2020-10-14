using DataChecker_FilesMerger.Dialog_Setting;
using DataChecker_FilesMerger.Helper;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static DataChecker_FilesMerger.Helper.ThreadHelper;

namespace DataChecker_FilesMerger
{
    public partial class MainForm : Form
    {
        private static MainForm mainForm = null;
        public static MainForm CreateInstrance()
        {
            if (mainForm == null || mainForm.IsDisposed)
            {
                mainForm = new MainForm();
            }
            return mainForm;
        }

        #region 规则

        /// <summary>
        /// 传统案卷
        /// </summary>
        private bool IsOneToMany
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 列名所在行
        /// </summary>
        private int ColumnNameRow
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 根目录
        /// </summary>
        private string rootDir
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 筛选文件类型
        /// </summary>
        private string fileFormat
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 案卷页数所在列
        /// </summary>
        private string AJPageColumn
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 卷内页数所在列
        /// </summary>
        private string JNPageColumn
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 件数所在列
        /// </summary>
        private string JNCountColumn
        {
            get;
            set;
        } = null;

        /// <summary>
        /// 案卷筛选列
        /// </summary>
        private List<string> AJFilter
        {
            get;
            set;
        } = new List<string>();

        /// <summary>
        /// 卷内筛选列
        /// </summary>
        public List<string> JNFilter
        {
            get;
            private set;
        } = new List<string>();

        /// <summary>
        /// 目录构成[名称,规定长度]
        /// </summary>
        private Dictionary<string, int> dirConstitute
        {
            get;
            set;
        } = new Dictionary<string, int>();

        #endregion

        #region 数据

        /// <summary>
        /// excel读取类
        /// </summary>
        private ExcelReader AJReader
        {
            get;
            set;
        } = new ExcelReader();

        /// <summary>
        /// excel读取类
        /// </summary>
        private ExcelReader JNReader
        {
            get;
            set;
        } = new ExcelReader();

        /// <summary>
        /// 案卷列名及其所在列号
        /// </summary>
        private IndexDictionary<string> AJExcelColumns = new IndexDictionary<string>();

        /// <summary>
        /// 卷内列名及其所在列号
        /// </summary>
        private IndexDictionary<string> JNExcelColumns = new IndexDictionary<string>();

        /// <summary>
        /// 集合存放案卷实体
        /// </summary>
        private List<AJEntity> ajEntities_List = new List<AJEntity>();

        /// <summary>
        /// 集合存放卷内实体
        /// </summary>
        public volatile List<JNEntity> jnEntities_List = new List<JNEntity>();

        #endregion

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 案卷拆为卷内时保留的列
        /// </summary>
        private List<string> saveColumn = new List<string>();

        /// <summary>
        /// 案卷拆为卷内时转换为行的列
        /// </summary>
        private List<string> turnRow = new List<string>();

        #region PDF合并规则

        /// <summary>
        /// PDF的总保存路径
        /// </summary>
        public string PDFSavePath
        {
            get;
            private set;
        } = null;

        /// <summary>
        /// 案卷单独保存时的文件夹命名规则
        /// </summary>
        public Dictionary<string, string> PDFPartFolder
        {
            get;
            private set;
        } = new Dictionary<string, string>();

        /// <summary>
        /// PDF命名规则
        /// </summary>
        public Dictionary<string, string> PDFPartName
        {
            get;
            private set;
        } = new Dictionary<string, string>();

        /// <summary>
        /// 是否合并附件
        /// </summary>
        public bool MergeAdditions
        {
            get;
            private set;
        } = false;

        /// <summary>
        /// 附件是否追加到案卷头部
        /// </summary>
        public bool AppendToHead
        {
            get;
            private set;
        } = false;

        /// <summary>
        /// 附件的排序方式
        /// </summary>
        public List<string> AdditionSort
        {
            get;
            private set;
        } = new List<string>();

        /// <summary>
        /// 附件单独保存时的命名方式
        /// </summary>
        public Dictionary<string, string> AdditionPartName
        {
            get;
            private set;
        } = new Dictionary<string, string>();

        #endregion

        #region 状态标志

        /// <summary>
        /// 是否读取数据
        /// </summary>
        private bool DataLoaded = false;

        /// <summary>
        /// 是否完成合并设置
        /// </summary>
        private bool MergeSetted = false;

        /// <summary>
        /// 是否完成拆分设置
        /// </summary>
        private bool DemergeSetted = false;

        #endregion

        #region 跳过标志

        /// <summary>
        /// 是否跳过件数检查
        /// </summary>
        public bool JumpJNCount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否跳过页数检查
        /// </summary>
        public bool JumpPageCount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否跳过扫描件
        /// </summary>
        public bool JumpScanFiles
        {
            get;
            set;
        }

        /// <summary>
        /// 是否跳过卷内部分
        /// </summary>
        public bool JumpJN
        {
            get;
            set;
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            UnAdjustList_ADD();
            AdjustByRb_ADD();
            UpdatePercent.Stop();
        }

        #region 状态调整

        /// <summary>
        /// 不被调整的控件
        /// </summary>
        private List<string> UnAdjustByRb = new List<string>();
        /// <summary>
        /// 由单选设置状态的控件
        /// </summary>
        private List<string> AdjustByRb = new List<string>();

        private delegate void Enable_FlushClient(bool enable);
        /// <summary>
        /// 调整控件可用性
        /// </summary>
        /// <param name="enable">是否可用</param>
        private void AdjustControlEnable(bool enable)
        {
            if (progressBar1.InvokeRequired)
            {
                Enable_FlushClient fc = new Enable_FlushClient(AdjustControlEnable);
                BeginInvoke(fc, new object[] { enable });
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    if (!UnAdjustByRb.Contains(control.Name))
                    {
                        if (AdjustByRb.Contains(control.Name) && enable)
                        {
                            control.Enabled = IsOneToMany;
                        }
                        else
                        {
                            control.Enabled = enable;
                        }
                    }
                }
            }
        }
        private void UnAdjustList_ADD()
        {
            UnAdjustByRb.Add(listView_Error.Name);
            UnAdjustByRb.Add(lblPercent.Name);
            UnAdjustByRb.Add(progressBar1.Name);
        }
        private void AdjustByRb_ADD()
        {
            AdjustByRb.Add(tbJNFile.Name);
            AdjustByRb.Add(btnSelectJNExcel.Name);
            AdjustByRb.Add(cbJNSheets.Name);
        }

        private void ResetState()
        {
            if (DataLoaded)
            {
                if (MessageBox.Show("是否用原有设置重新加载数据?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LoadData();
                }
                else
                {
                    DataLoaded = false;
                }
            }
            DemergeSetted = false;
            MergeSetted = false;
        }

        #endregion

        #region 调整进度

        private int nowPercent;

        private delegate void Progress_FlushClient(int i);
        /// <summary>
        /// 调整进度状态
        /// </summary>
        /// <param name="i"></param>
        private void AdjustProgress(int i)
        {
            if (progressBar1.InvokeRequired)
            {
                Progress_FlushClient fc = new Progress_FlushClient(AdjustProgress);
                BeginInvoke(fc, new object[] { i });
            }
            else
            {
                this.progressBar1.Value = i;
                if (i == 100)
                {
                    this.lblPercent.Text = i.ToString() + "%";
                }
                else
                {
                    nowPercent = i;
                }
            }
        }

        private void UpdatePercent_work(object sender, EventArgs e)
        {
            this.lblPercent.Text = nowPercent.ToString() + "%";
        }

        /// <summary>
        /// 开始工作,调整控件状态和进度状态
        /// </summary>
        private void StartProgress()
        {
            UpdatePercent.Start();
            AdjustProgress(0);
            AdjustControlEnable(false);
        }

        private delegate void EndProgress_FlushClient();
        /// <summary>
        /// 结束工作,调整控件状态和进度状态
        /// </summary>
        private void EndProgress()
        {
            if (progressBar1.InvokeRequired)
            {
                EndProgress_FlushClient fc = new EndProgress_FlushClient(EndProgress);
                BeginInvoke(fc);
            }
            else
            {
                UpdatePercent.Stop();
                AdjustProgress(100);
                AdjustControlEnable(true);
            }
        }

        #endregion

        #region 打开案卷文件

        private void btnSelectAJExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbAJFile.Text = openFileDialog.FileName;
                AJReader.Load_Excel(tbAJFile.Text.Trim());
                cbAJSheets.DataSource = AJReader.SheetsName;
                ResetState();
            }
        }

        private void btnSelectJNExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbJNFile.Text = openFileDialog.FileName;
                JNReader.Load_Excel(tbJNFile.Text.Trim());
                cbJNSheets.DataSource = JNReader.SheetsName;
                ResetState();
            }
        }

        private void cbAJSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            AJReader.ResetSheet(cbAJSheets.SelectedItem.ToString());
            ResetState();
        }

        private void cbJNSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            JNReader.ResetSheet(cbJNSheets.SelectedItem.ToString());
            ResetState();
        }

        #endregion

        #region 设置扫描件目录

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择需要合并的文件格式!");
                return;
            }
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择目录";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbFilesPath.Text = folderBrowserDialog.SelectedPath;
                rootDir = tbFilesPath.Text;
                if (JumpScanFiles)
                {
                    MessageBox.Show("扫描件路径追加完成,将重新读取数据!");
                    LoadData();
                }
                fileFormat = "*" + this.comboBox1.SelectedItem.ToString();
            }
        }

        private void selectedChanged(object sender, EventArgs e)
        {
            fileFormat = "*" + this.comboBox1.SelectedItem.ToString();
        }

        #endregion

        #region 输出异常

        /// <summary>
        /// 异常信息使用的代理
        /// </summary>
        /// <param name="AJLocatoin"></param>
        /// <param name="Findable"></param>
        /// <param name="ErrorInfo"></param>
        private delegate void ErrorInfo_FlushClient(string Locatoin, string FncName, string ErrorInfo);
        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="Locatoin"></param>
        /// <param name="FncName"></param>
        /// <param name="ErrorInfo"></param>
        public void WriteErrorInfo(string Locatoin, string FncName, string ErrorInfo)
        {
            if (listView_Error.InvokeRequired)
            {
                ErrorInfo_FlushClient fc = new ErrorInfo_FlushClient(WriteErrorInfo);
                BeginInvoke(fc, new object[] { Locatoin, FncName, ErrorInfo });
            }
            else
            {
                listView_Error.Items.Add(new ListViewItem(new string[4] { "", Locatoin, FncName, ErrorInfo }));
            }
        }

        private void btnExportError_Click(object sender, EventArgs e)
        {
            if (listView_Error.Items.Count == 0)
            {
                MessageBox.Show("无错误信息!");
                return;
            }
            else
            {
                ExcelSaver.SaveToExcel(listView_Error);
            }
        }

        #endregion

        #region 主界面设置

        private void tbColuNameRow_TextChanged(object sender, EventArgs e)
        {
            if (ControlHelper.NumberCheck(sender, true))
            {
                ColumnNameRow = int.Parse(tbColuNameRow.Text.Trim()) - 1;
            }
        }

        private void rbOneToMany_CheckedChanged(object sender, EventArgs e)
        {
            IsOneToMany = rbOneToMany.Checked;
            foreach (string name in AdjustByRb)
            {
                foreach (Control control in this.Controls.Find(name, false))
                {
                    control.Enabled = IsOneToMany;
                }
            }
        }

        #endregion

        #region 组装案卷

        /// <summary>
        /// 初始化列名
        /// </summary>
        private void InitColumns()
        {
            AJReader.Caculate_Columns(ColumnNameRow);
            AJExcelColumns = AJReader.ExcelColumns;
            if (IsOneToMany)
            {
                JNReader.Caculate_Columns(ColumnNameRow);
                JNExcelColumns = JNReader.ExcelColumns;
            }
        }

        private void btnRelationSetting_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAJFile.Text))
            {
                MessageBox.Show("请先选择案卷文件"); return;
            }
            var columnsetting = ColumnSetting.CreateInstrance();
            var foldersetting = FolderSetting.CreateInstrance();
            var oneTomany = OneToManySetting.CreateInstrance();
            if (IsOneToMany)
            {
                if (string.IsNullOrWhiteSpace(tbJNFile.Text))
                {
                    MessageBox.Show("请先选择卷内文件"); return;
                }
                InitColumns();

                List<Form> forms = new List<Form> { foldersetting, columnsetting, oneTomany };
                columnsetting.Upload(AJReader.ExcelColumns, JNReader.ExcelColumns);
                foldersetting.Upload(AJReader.ExcelColumns);
                oneTomany.Upload(AJReader.ExcelColumns, JNReader.ExcelColumns);

                Setting setting = new Setting(forms);
                if (setting.ShowDialog() == DialogResult.OK)
                {
                    dirConstitute = foldersetting.dirConstitute;
                    AJFilter = oneTomany.AJKeyColumn;
                    JNFilter = oneTomany.JNKeyColumn;
                    AJPageColumn = columnsetting.AJPageColumn;
                    JNCountColumn = columnsetting.JNCountColumn;
                    JNPageColumn = columnsetting.JNPageColumn;
                    if (foldersetting.renameFolder == true)
                    {
                        //Rename();
                    }
                    MessageBox.Show("设置完成,将开始读取数据!");
                    LoadData();
                }
            }
            else
            {
                InitColumns();
                List<Form> forms = new List<Form> { foldersetting, columnsetting };
                columnsetting.Upload(AJReader.ExcelColumns);
                foldersetting.Upload(AJReader.ExcelColumns);

                Setting setting = new Setting(forms);
                if (setting.ShowDialog() == DialogResult.OK)
                {
                    dirConstitute = foldersetting.dirConstitute;
                    AJPageColumn = columnsetting.AJPageColumn;
                    if (foldersetting.renameFolder == true)
                    {
                        //Rename();
                    }
                    MessageBox.Show("设置完成,将开始读取数据!");
                    LoadData();
                }
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        private void LoadData()
        {
            ajEntities_List.Clear();
            jnEntities_List.Clear();
            this.listView_Error.Items.Clear();
            StartProgress();
            ReSetJump();
            bgwPrepareAJ.RunWorkerAsync();
        }

        /// <summary>
        /// 重置跳过标记
        /// </summary>
        private void ReSetJump()
        {
            JumpPageCount = false;
            JumpJNCount = false;
            JumpScanFiles = false;
            JumpJN = false;
        }

        /// <summary>
        /// 预先读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPrepareAJ_DoWork(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AJPageColumn))
            {
                WriteErrorInfo("", "", "案卷页数未设置,将跳过相关工作");
                JumpScanFiles = true;
                JumpPageCount = true;
            }
            if (string.IsNullOrWhiteSpace(JNCountColumn))
            {
                WriteErrorInfo("", "", "案卷件数未设置,将跳过相关工作");
                JumpJNCount = true;
            }
            if (string.IsNullOrWhiteSpace(rootDir))
            {
                WriteErrorInfo("", "", "文件路径未设置,将跳过相关工作");
                JumpScanFiles = true;
            }
            if (IsOneToMany)
            {
                if (string.IsNullOrWhiteSpace(JNPageColumn))
                {
                    WriteErrorInfo("", "", "卷内页数未设置,将跳过相关工作");
                    JumpPageCount = true;
                }
                if (AJFilter == null || AJFilter.Count == 0)
                {
                    WriteErrorInfo("", "", "案卷与卷内关系未设置,将跳过相关工作");
                    JumpJN = true;
                }
            }

            int complete = 0;
            int AJCount = AJReader.Cells.MaxDataRow;
            int total = AJCount;

            InitColumns();
            if (IsOneToMany)
            {
                int JNCount = JNReader.Cells.MaxDataRow;
                total += JNCount;
                for (int i = ColumnNameRow + 1; i < JNCount + 1; i++)
                {
                    int location = i + 1;
                    try
                    {
                        var value = DataBuilder(i, JNReader);
                        JNEntity jn = new JNEntity(value, location, JNPageColumn);
                        jnEntities_List.Add(jn);
                        complete++;
                        int percent = complete * 100 / total;
                        bgwPrepareAJ.ReportProgress(percent);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorInfo("JN行号:" + location.ToString(), "[PrepareAJ/JN]", ex.Message);
                        continue;
                    }
                }
            }
            for (int i = ColumnNameRow + 1; i < AJCount + 1; i++)
            {
                int location = i + 1;
                try
                {
                    var value = DataBuilder(i, AJReader);
                    AJEntity aj = new AJEntity(value, location, rootDir, AJPageColumn, dirConstitute);
                    if (IsOneToMany)
                    {
                        aj.OneToManyAppend(JNCountColumn, AJFilter, JNFilter);
                    }

                    ajEntities_List.Add(aj);
                    complete++;
                    int percent = complete * 100 / total;
                    bgwPrepareAJ.ReportProgress(percent);
                }
                catch (Exception ex)
                {
                    WriteErrorInfo("AJ行号:" + location.ToString(), "[PrepareAJ/AJ]", ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPrepareAJ_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataLoaded = true;
            EndProgress();
            MessageBox.Show("读取完成");
        }

        /// <summary>
        /// 生成[列名][值]的字典
        /// </summary>
        /// <param name="i"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Dictionary<string, string> DataBuilder(int i, ExcelReader reader)
        {
            Dictionary<string, string> archive = new Dictionary<string, string>();
            for (int j = 0; j < reader.Cells.Rows[i].LastCell.Column+1; j++)
            {
                try
                {
                    var currentCellValue = reader.Cells[i, j].Value;
                    //将不为空的单元格取出
                    if (currentCellValue != null && !string.IsNullOrWhiteSpace(currentCellValue.ToString()))
                    {
                        if (archive.Count != 0 && archive.Keys.Contains(reader.ExcelColumns[j]))
                        {
                            WriteErrorInfo("AJ行号:" + i.ToString() + "|列号:" + j.ToString(), "[DataBuilder]", "列名[" + reader.ExcelColumns[j] + "]已重复,将放弃");
                            continue;
                        }
                        archive.Add(reader.ExcelColumns[j], currentCellValue.ToString());
                    }
                }
                catch (Exception ex)
                {
                    this.WriteErrorInfo("AJ行号:" + i, "[DataBuilder]", ex.Message);
                }
            }
            return archive;
        }

        #endregion

        #region 检测

        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (!DataLoaded)
            {
                MessageBox.Show("请先完成数据读取!");
            }
            else
            {
                StartProgress();
                ThreadBaseControl<AJEntity> thfd = new ThreadBaseControl<AJEntity>(ajEntities_List, Match);
                thfd.OneCompleted += ShowOneDoneMsg;
                thfd.AllCompleted += ShowAllDoneMsg;
                thfd.Start();
            }
        }

        private void Match(AJEntity aj)
        {
            if (!JumpJNCount && !JumpJN)
            {
                aj.MatchCount();
            }

            if (!JumpPageCount && !JumpJN)
            {
                aj.MatchPages();
            }

            if (!JumpScanFiles)
            {
                aj.MatchScanfile();
            }
        }

        private void ShowOneDoneMsg(AJEntity x, CompetedEventArgs args)
        {
            AdjustProgress(args.CompetedPrecent);
        }

        private void ShowAllDoneMsg(CompetedEventArgs args)
        {
            EndProgress();
            MessageBox.Show("完成!");
        }

        #endregion

        #region 合并图片

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (!DataLoaded)
            {
                MessageBox.Show("请先完成数据读取!");
            }
            else if (!MergeSetted)
            {
                MessageBox.Show("请先完成合并设置!");
            }
            else
            {
                StartProgress();
                ThreadBaseControl<AJEntity> thfd = new ThreadBaseControl<AJEntity>(ajEntities_List, Merge);
                thfd.OneCompleted += ShowOneDoneMsg;
                thfd.AllCompleted += ShowAllDoneMsg;
                thfd.Start();
            }
        }

        private void Merge(AJEntity aj)
        {
            aj.SplitPath(PDFSavePath, PDFPartFolder);
            aj.Merge(PDFPartName, MergeAdditions, AdditionSort, AppendToHead, AdditionPartName);
        }

        private void btnMergeSetting_Click(object sender, EventArgs e)
        {
            MergeFolder mergeFolder = MergeFolder.CreateInstrance();
            MergeFileName mergeFileName = MergeFileName.CreateInstrance();
            MergeAddition mergeAddition = MergeAddition.CreateInstrance();
            InitColumns();
            if (IsOneToMany)
            {
                if (string.IsNullOrWhiteSpace(tbJNFile.Text))
                {
                    MessageBox.Show("请先选择卷内文件"); return;
                }
                mergeFileName.Upload(JNExcelColumns);
            }
            else
            {
                mergeFileName.Upload(AJExcelColumns);
            }
            List<Form> forms = new List<Form> { mergeFolder, mergeFileName, mergeAddition };
            mergeFolder.Upload(AJExcelColumns);
            mergeAddition.Upload(AJExcelColumns, IsOneToMany);
            Setting setting = new Setting(forms);
            if (setting.ShowDialog() == DialogResult.OK)
            {
                PDFSavePath = mergeFolder.PDFSavePath;
                PDFPartFolder = mergeFolder.savePartFolder;
                PDFPartName = mergeFileName.pdfPartName;
                MergeAdditions = mergeAddition.MergeAdditions;
                AppendToHead = mergeAddition.AppendToHead;
                AdditionSort = mergeAddition.additionSort;
                AdditionPartName = mergeAddition.additionPartName;
                MergeSetted = true;
            }
        }

        #endregion

        #region 重命名文件夹

        public void Rename()
        {
            this.listView_Error.Items.Clear();
            this.progressBar1.Value = 0;
            AdjustControlEnable(false);
            nowNum = 0;
            renameFolder.RunWorkerAsync();
        }

        private void renameFolder_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo rootDir = new DirectoryInfo(this.rootDir);
            List<int> bits = dirConstitute.Values.ToList();
            float Count = (float)GetFolderCount(rootDir, 0, bits.Count);
            GetDirectory(rootDir, 0, bits, Count);
        }

        private float nowNum = 0;
        private void GetDirectory(DirectoryInfo parent, int layer, List<int> bits, float count)
        {
            if (layer < bits.Count)
            {
                foreach (DirectoryInfo item in parent.GetDirectories())
                {
                    string newFullName = RewriteFolderName(item, bits[layer], count);
                    if (newFullName != null)
                    {
                        int nextLayer = layer + 1;
                        GetDirectory(new DirectoryInfo(newFullName), nextLayer, bits, count);
                    }
                }
            }
        }

        public static int GetFolderCount(DirectoryInfo dirInfo, int layer, int maxLayer)
        {
            int totalFolder = 0;
            if (layer < maxLayer)
            {
                totalFolder += dirInfo.GetDirectories().Length;
                foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
                {
                    int nextLayer = layer + 1;
                    totalFolder += GetFolderCount(subdir, nextLayer, maxLayer);
                }
            }
            return totalFolder;
        }

        private string RewriteFolderName(DirectoryInfo target, int bit, float count)
        {
            try
            {
                nowNum++;
                float a = nowNum / count * 100;
                int percent = (int)a;
                renameFolder.ReportProgress(percent);
                if (target.Name.Length < bit)
                {
                    string newName = target.Name.PadLeft(bit, '0');
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameDirectory(target.FullName, newName);
                    return target.Parent.FullName + "\\" + newName;
                }
                else if (target.Name.Length > bit)
                {
                    string newName = target.Name.Substring(target.Name.Length - bit);
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameDirectory(target.FullName, newName);
                    return target.Parent.FullName + "\\" + newName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void renameFolder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("重命名完成");
            AdjustControlEnable(true);
        }

        #endregion

        #region 拆分案卷Excel

        private void btnSplitSetting_Click(object sender, EventArgs e)
        {
            if (AJExcelColumns.Count == 0)
            {
                MessageBox.Show("请先选择案卷文件"); return;
            }
            DemergeSetting demergeSetting = new DemergeSetting(AJReader.ExcelColumns.strIndex, saveColumn, turnRow);
            if (demergeSetting.ShowDialog() == DialogResult.OK)
            {
                saveColumn = demergeSetting.saveColumn;
                DemergeSetted = true;
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (!DataLoaded)
            {
                MessageBox.Show("请先完成检测"); return;
            }
            if (DemergeSetted)
            {
                this.listView_Error.Items.Clear();
                this.progressBar1.Value = 0;
                AdjustControlEnable(false);
                this.demergeExcel.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("请先完成拆分设置!");
            }
        }

        private DataTable JNs = new DataTable();
        private void demergeExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            //JNs = new DataTable();
            //foreach (string column in saveColumn)
            //{
            //    JNs.Columns.Add(column);
            //}
            //JNs.Columns.Add("序号");
            //JNs.Columns.Add("题名");
            //JNs.Columns.Add("页数");
            //object[] obj = new object[JNs.Columns.Count];
            //foreach (AJEntity aj in ajEntities_List)
            //{
            //    DataTable JN = DataTableRecombine(aj);
            //    for (int i = 0; i < JN.Rows.Count; i++)
            //    {
            //        JN.Rows[i].ItemArray.CopyTo(obj, 0);
            //        JNs.Rows.Add(obj);
            //    }
            //}
        }

        private void demergeExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ExcelSaver.SaveToExcel(JNs);
            AdjustControlEnable(true);
        }

        #endregion




        private void mergeFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("合并完成");
            AdjustControlEnable(true);
        }

        private void bgwMatch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("检测完成");
            AdjustControlEnable(true);
        }



        private void ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            AdjustProgress(e.ProgressPercentage);
        }

    }
}
