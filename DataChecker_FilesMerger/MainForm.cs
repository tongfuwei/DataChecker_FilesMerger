using Aspose.Cells;
using DataChecker_FilesMerger.Dialog_Setting;
using DataChecker_FilesMerger.Helper;
using FileHelper;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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


        /// <summary>
        /// excel读取类
        /// </summary>
        private ExcelReader AJReader = new ExcelReader();

        /// <summary>
        /// excel读取类
        /// </summary>
        private ExcelReader JNReader = new ExcelReader();

        /// <summary>
        /// 列名所在行
        /// </summary>
        int ColumnNameRow = 0;

        /// <summary>
        /// 根目录
        /// </summary>
        private string rootDir;

        /// <summary>
        /// 筛选文件类型
        /// </summary>
        private string fileFormat;

        /// <summary>
        /// 案卷列名及其所在列号
        /// </summary>
        private IndexDictionary<string> AJExcelColumns = new IndexDictionary<string>();

        /// <summary>
        /// 卷内列名及其所在列号
        /// </summary>
        private IndexDictionary<string> JNExcelColumns = new IndexDictionary<string>();

        /// <summary>
        /// 案卷页数所在列
        /// </summary>
        private string AJPageColumn;

        /// <summary>
        /// 卷内页数所在列
        /// </summary>
        private string JNPageColumn;

        /// <summary>
        /// 件数所在列
        /// </summary>
        private string JNCountColumn;

        /// <summary>
        /// 案卷筛选列
        /// </summary>
        private List<string> AJFilter = new List<string>();

        /// <summary>
        /// 卷内筛选列
        /// </summary>
        public List<string> JNFilter = new List<string>();

        /// <summary>
        /// 目录构成[名称,规定长度]
        /// </summary>
        private Dictionary<string, int> dirConstitute = new Dictionary<string, int>();

        /// <summary>
        /// pdf文件名规则
        /// </summary>
        private Dictionary<string, int> PdfNameRule;

        /// <summary>
        /// pdf保存文件夹规则
        /// </summary>
        private Dictionary<string, int> FolderNameRule;


        /// <summary>
        /// 集合存放案卷实体
        /// </summary>
        private List<AJEntity> ajEntities_List = new List<AJEntity>();

        /// <summary>
        /// 集合存放卷内实体
        /// </summary>
        public volatile List<JNEntity> jnEntities_List = new List<JNEntity>();

        /// <summary>
        /// 锁
        /// </summary>
        readonly static object locker = new object();

        /// <summary>
        /// 案卷拆为卷内时保留的列
        /// </summary>
        List<string> saveColumn = new List<string>();
        /// <summary>
        /// 案卷拆为卷内时转换为行的列
        /// </summary>
        List<string> turnRow = new List<string>();

        /// <summary>
        /// 是否对文件夹名称应用规定长度
        /// </summary>
        public bool shouldRename;
        /// <summary>
        /// pdf保存路径
        /// </summary>
        string fileSaveFolder;

        /// <summary>
        /// 是否完成匹配设置
        /// </summary>
        private bool MatchSetted = false;
        /// <summary>
        /// 是否完成拆分设置
        /// </summary>
        private bool DemergeSetted = false;
        /// <summary>
        /// 是否完成合并设置
        /// </summary>
        private bool MergeSetted = false;
        /// <summary>
        /// 案卷组装完毕
        /// </summary>
        private bool prepareAJComplete = false;
        /// <summary>
        /// 是否合并封面卷内和封底
        /// </summary>
        public bool IsMergeFM;

        public string Addition;


        private bool IsOneToMany
        {
            get;
            set;
        } = true;

        public MainForm()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            UnAdjustList_ADD();
            AdjustByRb_ADD();
        }

        #region 控件状态调整

        /// <summary>
        /// 不被调整的控件
        /// </summary>
        private List<string> UnAdjustByRb = new List<string>();
        /// <summary>
        /// 由单选设置状态的控件
        /// </summary>
        private List<string> AdjustByRb = new List<string>();

        /// <summary>
        /// 调整控件可用性
        /// </summary>
        /// <param name="enable">是否可用</param>
        private void AdjustControlEnable(bool enable)
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

        Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 调整进度状态
        /// </summary>
        /// <param name="i"></param>
        private void AdjustProgress(int i)
        {
            this.progressBar1.Value = i;
            this.lblPercent.Text = @"已完成：" + i.ToString() + @"%";
            this.lblTimer.Text = "当前耗时:" + stopwatch.Elapsed.TotalMinutes.ToString();
            if(i == 100)
            {
                stopwatch.Stop();
            }
        }

        private void StartProgress()
        {
            stopwatch.Restart();
            AdjustProgress(0);
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
            }
        }

        private void btnSelectJNExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbJNFile.Text = openFileDialog.FileName;
                JNReader.Load_Excel(tbJNFile.Text.Trim());
                cbJNSheets.DataSource = JNReader.SheetsName;
            }
        }

        private void cbAJSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            AJReader.ResetSheet(cbAJSheets.SelectedItem.ToString());
        }

        private void cbJNSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            JNReader.ResetSheet(cbJNSheets.SelectedItem.ToString());
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
                fileFormat = "*" + this.comboBox1.SelectedItem.ToString();
            }
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
                ColumnNameRow = int.Parse(tbColuNameRow.Text.Trim()) - 1;
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

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            ajEntities_List.Clear();
            jnEntities_List.Clear();
            this.listView_Error.Items.Clear();
            AdjustControlEnable(false);
            StartProgress();
            bgwPrepareAJ.RunWorkerAsync();
        }

        /// <summary>
        /// 预先读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPrepareAJ_DoWork(object sender, DoWorkEventArgs e)
        {
            int complete = 0;
            prepareAJComplete = false;

            int AJCount = AJReader.Cells.MaxDataRow;
            int JNCount = JNReader.Cells.MaxDataRow;

            int total = AJCount;

            if (IsOneToMany)
            {
                total += JNCount;
                for (int i = ColumnNameRow + 1; i < JNCount + 1; i++)
                {
                    try
                    {
                        int percent = complete * 100 / total;
                        bgwPrepareAJ.ReportProgress(percent);
                        var value = DataBuilder(i, JNReader);
                        JNEntity jn = new JNEntity(value, i, JNPageColumn);
                        jnEntities_List.Add(jn);
                        complete++;
                    }
                    catch (Exception ex)
                    {
                        WriteErrorInfo("JN行号:" + i.ToString(), "[PrepareAJ/JN]", ex.Message);
                        continue;
                    }
                }
            }
            for (int i = ColumnNameRow + 1; i < AJCount + 1; i++)
            {
                try
                {
                    int percent = complete * 100 / total;
                    bgwPrepareAJ.ReportProgress(percent);
                    var value = DataBuilder(i, AJReader);
                    AJEntity aj = new AJEntity(value, i, rootDir, AJPageColumn, dirConstitute);
                    if (IsOneToMany)
                        aj.OneToManyAppend(JNCountColumn, AJFilter);
                    complete++;
                }
                catch (Exception ex)
                {
                    WriteErrorInfo("AJ行号:" + i.ToString(), "[PrepareAJ/AJ]", ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 生成[列名][值]的字典
        /// </summary>
        /// <param name="i"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Dictionary<string, string> DataBuilder(int i,ExcelReader reader)
        {
            Dictionary<string,string> archive = new Dictionary<string, string>();
            for (int j = 0; j <= reader.Cells.Rows[i].LastCell.Column; j++)
            {
                var currentCellValue = reader.Cells[i, j].Value;
                //将不为空的单元格取出
                if (currentCellValue != null && !string.IsNullOrWhiteSpace(currentCellValue.ToString()))
                {
                    archive.Add(reader.ExcelColumns[j], currentCellValue.ToString());
                }
            }
            return archive;
        }

        #endregion

        #region 检测

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
            if (string.IsNullOrWhiteSpace(rootDir))
            {
                MessageBox.Show("请先选择扫描件目录"); return;
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

                List<Form> forms = new List<Form> { columnsetting, foldersetting, oneTomany };
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
                    MatchSetted = true;
                }
            }
            else
            {
                InitColumns();
                List<Form> forms = new List<Form> { columnsetting, foldersetting };
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
                    MatchSetted = true;
                }
            }
        }        

        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (MatchSetted)
            {
                this.listView_Error.Items.Clear();
                StartProgress();
                AdjustControlEnable(false);

                QueueThreadBase<AJEntity> thfd = new QueueThreadBase<AJEntity>(ajEntities_List, Match);
                thfd.OneCompleted += ShowOneDoneMsg;
                thfd.AllCompleted += ShowAllDoneMsg;
                thfd.Start();
            }
            else
            {
                MessageBox.Show("请先完成检测设置");
            }
        }

        private void Match(AJEntity aj)
        {
            aj.MatchInfo();
        }

        private void ShowOneDoneMsg(AJEntity x, CompetedEventArgs args)
        {
            AdjustProgress(args.CompetedPrecent);
        }

        private void ShowAllDoneMsg(CompetedEventArgs args)
        {
            AdjustProgress(args.CompetedPrecent);
        }       

        #endregion

        #region 合并图片

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (!prepareAJComplete)
            {
                MessageBox.Show("请先完成检测"); return;
            }
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择目录";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                fileSaveFolder = folderBrowserDialog.SelectedPath + "\\";
            }
            else
            {
                return;
            }
            if (MergeSetted)
            {
                if (IsOneToMany)
                {
                    this.listView_Error.Items.Clear();
                    AdjustProgress(0);
                    AdjustControlEnable(false);
                    this.mergeFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MergeFile_DoWork_OneToMany);
                    this.mergeFile.RunWorkerAsync();
                }
                else
                {
                    this.listView_Error.Items.Clear();
                    AdjustProgress(0);
                    AdjustControlEnable(false);
                    this.mergeFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MergeFile_DoWork_OneToOne);
                    this.mergeFile.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("请先完成合并设置!");
            }
        }

        Operater op = new Operater();
        //private void MergeFile_DoWork_OneToMany(object sender, DoWorkEventArgs e)
        //{
        //    string SavePath = fileSaveFolder;
        //    string AJSavePath = SavePath;
        //    int nowJNrow = 1;
        //    foreach (var dic in Entities_Dic)
        //    {
        //        //当前卷内起始页
        //        int nowPage = 0;
        //        #region 是否新建文件夹保存
        //        if (FolderNameRule != null)
        //        {
        //            List<string> folderName_List = new List<string>();
        //            foreach (string key in FolderNameRule.Keys)
        //            {
        //                int bit = FolderNameRule[key];
        //                string partName = dic.Key.Value.Rows[0][key].ToString();
        //                if (bit != 0)
        //                {
        //                    if (partName.Length < bit)
        //                    {
        //                        partName = partName.PadLeft(bit, '0');
        //                    }
        //                    else if (partName.Length > bit)
        //                    {
        //                        partName = partName.Substring(partName.Length - bit);
        //                    }
        //                }
        //                folderName_List.Add(partName);
        //            }
        //            string folderName = string.Join("-", folderName_List);
        //            AJSavePath = SavePath + folderName + "\\";
        //        }
        //        if (!Directory.Exists(AJSavePath))
        //        {
        //            Directory.CreateDirectory(AJSavePath);
        //        }
        //        #endregion
        //        List<string> adittionalName = new List<string>();
        //        foreach (JNEntity jn in dic.Value)
        //        {
                    
        //            //对每个卷内行执行合并操作
        //            for (int i = 0; i < jn.Value.Rows.Count; i++)
        //            {
        //                try
        //                {
        //                    List<string> inputFiles = new List<string>();
        //                    //当前卷内页数
        //                    int page = int.Parse(jn.Value.Rows[i][JNPageColumn].ToString());
        //                    //下一个卷内的起始页
        //                    int endPage = nowPage + page;
        //                    for (int j = nowPage; j < endPage; j++)
        //                    {
        //                        if (!(j < dic.Key.ScanFiles.Count))
        //                        {
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            inputFiles.Add(dic.Key.ScanFiles[j].FullName);
        //                        }
        //                    }
        //                    nowPage = endPage;
        //                    #region 拼接pdf文件名
        //                    List<string> fileName_List = new List<string>();
        //                    foreach (string Key in PdfNameRule.Keys)
        //                    {
        //                        int bit = PdfNameRule[Key];
        //                        string partName = jn.Value.Rows[i][Key].ToString();
        //                        if (bit != 0)
        //                        {
        //                            if (partName.Length < bit)
        //                            {
        //                                partName = partName.PadLeft(bit, '0');
        //                            }
        //                            else if (partName.Length > bit)
        //                            {
        //                                partName = partName.Substring(partName.Length - bit);
        //                            }
        //                        }
        //                        fileName_List.Add(partName);
        //                    }
        //                    adittionalName.AddRange(fileName_List.ToArray());
        //                    string fileName = string.Join("-", fileName_List);
        //                    #endregion
        //                    #region 合并
        //                    if (inputFiles.Count != 0)
        //                    {                                
        //                        if (File.Exists(AJSavePath + "\\" + fileName + ".pdf"))
        //                        {
        //                            WriteErrorInfo(dic.Key.Location.ToString(), fileName, "该pdf已存在,请检查");
        //                            continue;
        //                        }
        //                        //op.MergerFile(AJSavePath + "\\" + fileName + ".pdf", "pdf", inputFiles.ToArray(), null);
        //                        MergeUtil.MergeToPDF(inputFiles, AJSavePath + "\\" + fileName + ".pdf");
        //                        float temp = ((float)(nowJNrow) / (float)(JNCells.MaxDataRow - ColumnNameRow)) * 100;
        //                        int percent = (int)temp;
        //                        mergeFile.ReportProgress(percent);
        //                        nowJNrow++;
        //                    }
        //                    else
        //                    {
        //                        WriteErrorInfo("-", jn.Key + "-" + nowPage, "没有对应的文件");
        //                    }
        //                    #endregion
        //                }
        //                catch (Exception ex)
        //                {
        //                    WriteErrorInfo(dic.Key.Location.ToString(), "[MergeJN/"+ i +"]", ex.Message);
        //                }
        //            }
        //        }

        //        #region
        //        if (IsMergeFM)
        //        {
        //            adittionalName.Remove(adittionalName[adittionalName.Count - 1]);
        //            string aditionalName = string.Join("-", adittionalName);
        //            string FmJnFdPath = AJSavePath + "\\" + aditionalName + "-" + Addition + ".pdf";
        //            MergeAdditional(dic.Key.Additional, FmJnFdPath);
        //        }
        //        #endregion
        //    }

        //}
        //private void MergeFile_DoWork_OneToOne(object sender, DoWorkEventArgs e)
        //{
        //    string SavePath = fileSaveFolder;
        //    int nowRow = 1;
        //    foreach (AJEntity aj in ajEntities_List)
        //    {
        //        try
        //        {
        //            #region 是否新建文件夹保存
        //            if (FolderNameRule != null)
        //            {
        //                List<string> folderName_List = new List<string>();
        //                foreach (string key in FolderNameRule.Keys)
        //                {
        //                    int bit = FolderNameRule[key];
        //                    string partName = aj.Value.Rows[0][key].ToString();
        //                    if (bit != 0)
        //                    {
        //                        if (partName.Length < bit)
        //                        {
        //                            partName = partName.PadLeft(bit, '0');
        //                        }
        //                        else if (partName.Length > bit)
        //                        {
        //                            partName = partName.Substring(partName.Length - bit);
        //                        }
        //                    }
        //                    folderName_List.Add(partName);
        //                }
        //                string folderName = string.Join("-", folderName_List);
        //                SavePath += folderName + "\\";
        //            }
        //            #endregion
        //            #region 把文件路径取出
        //            List<string> inputFiles = new List<string>();
        //            foreach (FileInfo file in aj.ScanFiles)
        //            {
        //                inputFiles.Add(file.FullName);
        //            }
        //            #endregion
        //            #region 拼接pdf文件名
        //            List<string> fileName_List = new List<string>();
        //            foreach (string Key in PdfNameRule.Keys)
        //            {
        //                int bit = PdfNameRule[Key];
        //                string partName = aj.Value.Rows[0][Key].ToString();
        //                if (bit != 0)
        //                {
        //                    if (partName.Length < bit)
        //                    {
        //                        partName = partName.PadLeft(bit, '0');
        //                    }
        //                    else if (partName.Length > bit)
        //                    {
        //                        partName = partName.Substring(partName.Length - bit);
        //                    }
        //                }
        //                fileName_List.Add(partName);
        //            }
        //            string fileName = string.Join("-", fileName_List);
        //            #endregion
        //            #region 合并
        //            if (inputFiles.Count != 0)
        //            {
        //                if (!Directory.Exists(SavePath))
        //                {
        //                    Directory.CreateDirectory(SavePath);
        //                }
        //                if (File.Exists(SavePath + "\\" + fileName + ".pdf"))
        //                {
        //                    WriteErrorInfo(aj.Location.ToString(), fileName, "该pdf已存在,请检查");
        //                    continue;
        //                }
        //                //op.MergerFile(SavePath + "\\" + fileName + ".pdf", "pdf", inputFiles, null);
        //                MergeUtil.MergeToPDF(inputFiles, SavePath + "\\" + fileName + ".pdf");
        //                float temp = nowRow / (float)(ajEntities_List.Count) * 100;
        //                int percent = (int)temp;
        //                mergeFile.ReportProgress(percent);
        //                nowRow++;
        //            }
        //            else
        //            {
        //                WriteErrorInfo(aj.Location.ToString(), "", "该案卷没有文件");
        //            }
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            WriteErrorInfo(aj.Location.ToString(), "[MergeFile_DoWork_OneToOne]", ex.Message);
        //            continue;
        //        }
        //    }
        //}

        /// <summary>
        /// 合并FMJNFD
        /// </summary>
        /// <param name="list">附件集合</param>
        /// <param name="fileSavePath">文件fullName</param>
        private void MergeAdditional(List<FileInfo> list,string fileSavePath)
        {
            List<string> inputFiles = new List<string>();
            foreach(FileInfo fileInfo in list)
            {
                inputFiles.Add(fileInfo.FullName);
            }
            //op.MergerFile(fileSavePath, "pdf", inputFiles.ToArray(), null);
            MergeUtil.MergeToPDF(inputFiles, fileSavePath);
        }

        private List<FileInfo> GetFileInfo(string path)
        {
            List<FileInfo> fileInfos = new List<FileInfo>();
            //指定目录
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //指定类型的文件
            FileInfo[] files = directoryInfo.GetFiles(fileFormat);
            IOrderedEnumerable<FileInfo> orderedEnumerable = files.OrderBy((FileInfo c) => c.Name);
            foreach (FileInfo item in orderedEnumerable)
            {
                if (item.Name.Contains("FM")) //封面
                {

                }
                else if (item.Name.Contains("ML") || item.Name.Contains("JN")) //卷内目录。
                {

                }
                else if (item.Name.Contains("BK") || item.Name.Contains("FD")) //备考表，封面。
                {

                }
                else //扫描的卷内目录图片文件。
                {
                    fileInfos.Add(item);
                }
            }
            return fileInfos;
        }

        private void btnMergeSetting_Click(object sender, EventArgs e)
        {
            if (AJExcelColumns == null || AJExcelColumns.Count == 0)
            {
                MessageBox.Show("请先选择案卷文件"); return;
            }
            if (rootDir == null)
            {
                MessageBox.Show("请先选择扫描件目录"); return;
            }
            if (IsOneToMany)
            {
                if (JNExcelColumns == null || JNExcelColumns.Count == 0)
                {
                    MessageBox.Show("请先选择卷内文件"); return;
                }
                else
                {
                    MergeSetting mergeSetting = new MergeSetting(JNExcelColumns, PdfNameRule, FolderNameRule, true, AJExcelColumns);
                    if (mergeSetting.ShowDialog() == DialogResult.OK)
                    {
                        PdfNameRule = mergeSetting.PdfNameRule;
                        FolderNameRule = mergeSetting.FolderNameRule;
                        MergeSetted = true;
                        IsMergeFM = mergeSetting.IsMergeFM;
                        Addition = mergeSetting.Addition;
                    }
                }
            }
            else
            {
                MergeSetting mergeSetting = new MergeSetting(AJExcelColumns, PdfNameRule, FolderNameRule, false);
                if (mergeSetting.ShowDialog() == DialogResult.OK)
                {
                    PdfNameRule = mergeSetting.PdfNameRule;
                    FolderNameRule = mergeSetting.FolderNameRule;
                    MergeSetted = true;
                    IsMergeFM = mergeSetting.IsMergeFM;
                }
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
            float Count = (float)GetFolderCount(rootDir, 0,bits.Count);
            GetDirectory(rootDir, 0, bits, Count);
        }

        float nowNum = 0;
        private void GetDirectory(DirectoryInfo parent,int layer,List<int> bits,float count)
        {
            if (layer < bits.Count)
            {
                foreach (DirectoryInfo item in parent.GetDirectories())
                {
                    string newFullName = RewriteFolderName(item, bits[layer],count);
                    if (newFullName != null)
                    {
                        int nextLayer = layer + 1;
                        GetDirectory(new DirectoryInfo(newFullName), nextLayer, bits, count);
                    }
                }
            }
        }

        public static int GetFolderCount(DirectoryInfo dirInfo,int layer,int maxLayer)
        {
            int totalFolder = 0;
            if (layer < maxLayer)
            {
                totalFolder += dirInfo.GetDirectories().Length;
                foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
                {
                    int nextLayer = layer + 1;
                    totalFolder += GetFolderCount(subdir,nextLayer,maxLayer);
                }
            }
            return totalFolder;
        }

        private string RewriteFolderName(DirectoryInfo target,int bit,float count)
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
                else if (target.Name.Length>bit)
                {
                    string newName = target.Name.Substring(target.Name.Length - bit);
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameDirectory(target.FullName, newName);
                    return target.Parent.FullName + "\\" + newName;
                }
                else return null;
            }
            catch(Exception ex)
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
            DemergeSetting demergeSetting = new DemergeSetting(AJExcelColumns,saveColumn,turnRow);
            if (demergeSetting.ShowDialog() == DialogResult.OK)
            {
                saveColumn = demergeSetting.saveColumn;
                DemergeSetted = true;
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if(!prepareAJComplete)
            {
                MessageBox.Show("请先完成检测");return;
            }
            if(DemergeSetted)
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


        DataTable JNs = new DataTable();
        private void demergeExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            JNs = new DataTable();
            foreach (string column in saveColumn)
            {
                JNs.Columns.Add(column);
            }
            JNs.Columns.Add("序号");
            JNs.Columns.Add("题名");
            JNs.Columns.Add("页数");
            object[] obj = new object[JNs.Columns.Count];
            foreach (AJEntity aj in ajEntities_List)
            {
                DataTable JN = DataTableRecombine(aj);
                for (int i = 0; i < JN.Rows.Count; i++)
                {
                    JN.Rows[i].ItemArray.CopyTo(obj, 0);
                    JNs.Rows.Add(obj);
                }
            }
        }

        private void demergeExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ExcelSaver.SaveToExcel(JNs);
            AdjustControlEnable(true);
        }

        private DataTable DataTableRecombine(AJEntity AJEntity)
        {
            DataTable AJ = AJEntity.Value;
            DataTable JN = new DataTable();
            foreach (string column in saveColumn)
            {
                JN.Columns.Add(column);
            }
            JN.Columns.Add("序号");
            JN.Columns.Add("题名");
            JN.Columns.Add("页数");
            try
            {
                int orderNum = 1;
                for (int i = 0; i < turnRow.Count; i++)
                {
                    if (AJ.Columns.Contains(turnRow[i]))
                    {
                        DataRow row = JN.NewRow();
                        for (int j = 0; j < saveColumn.Count; j++)
                        {
                            row[saveColumn[j]] = AJ.Rows[0][saveColumn[j]];
                        }
                        row["序号"] = orderNum;
                        row["题名"] = turnRow[i].Trim();
                        //纯数字,则页数为1
                        if (int.TryParse(AJ.Rows[0][turnRow[i]].ToString(),out int num1))
                        {
                            row["页数"] = 1;
                        }
                        else
                        {
                            //用分隔符进行拆分
                            string[] pageNum = AJ.Rows[0][turnRow[i]].ToString().Split('-');
                            //正常情况,拆分为两段
                            if (pageNum.Length == 2)
                            {
                                //首位不为数字
                                if (!int.TryParse(pageNum[0], out int num2))
                                {
                                    //第二位不为数字
                                    if (!int.TryParse(pageNum[1], out int num3))
                                    {
                                        //放弃
                                        continue;
                                    }
                                }
                                //第二位不为数字
                                if (!int.TryParse(pageNum[1], out int num4))
                                {
                                    WriteErrorInfo(AJEntity.Location.ToString(), turnRow[i], "该位置页号存在问题");
                                    row["页数"] = "-";
                                }
                                else
                                {
                                    row["页数"] = int.Parse(pageNum[1].Trim()) - int.Parse(pageNum[0].Trim()) + 1;
                                }
                            }
                            //不正常情况
                            else
                            {
                                //首位不为数字,直接放弃
                                if (!int.TryParse(pageNum[0], out int num2))
                                {
                                    continue;
                                }
                                //首位为数字,但整体为不标准情况,需要检查
                                else
                                {
                                    WriteErrorInfo(AJEntity.Location.ToString(), turnRow[i], "该位置页号存在问题");
                                    row["页数"] = "-";
                                }
                            }
                        }

                        JN.Rows.Add(row);
                        orderNum++;
                    }
                }
            }
            catch(Exception ex)
            {
                WriteErrorInfo(AJEntity.Location.ToString(), "DataTableRecombine", ex.Message);
            }
            return JN;
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

        private void bgwPrepareAJ_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prepareAJComplete = true;
        }

        private void ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            AdjustProgress(e.ProgressPercentage);
        }

        
    }
}
