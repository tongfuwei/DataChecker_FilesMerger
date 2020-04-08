using Aspose.Cells;
using DataChecker_FilesMerger.Dialog_Setting;
using FileHelper;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataChecker_FilesMerger
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 打开文件窗体
        /// </summary>
        private OpenFileDialog openFileDialog1;

        /// <summary>
        /// excel读取类
        /// </summary>
        private ExcelReader AJExcelHelper = new ExcelReader();
        /// <summary>
        /// excel读取类
        /// </summary>
        private ExcelReader JNExcelHelper = new ExcelReader();

        /// <summary>
        /// 保存案卷数据
        /// </summary>
        private Cells AJCells;
        /// <summary>
        /// 保存卷内数据
        /// </summary>
        private Cells JNCells;

        /// <summary>
        /// 是否正在选择案卷
        /// </summary>
        bool isSelectingArchiveFile = false;

        /// <summary>
        /// 列名所在行
        /// </summary>
        int ColumnNameRow = 0;

        /// <summary>
        /// 案卷列名及其所在列号
        /// </summary>
        private Dictionary<string, int> AJExcelColumns = new Dictionary<string, int>();
        /// <summary>
        /// 卷内列名及其所在列号
        /// </summary>
        private Dictionary<string, int> JNExcelColumns = new Dictionary<string, int>();

        /// <summary>
        /// 案卷页数所在列
        /// </summary>
        private string AJPageCount;
        /// <summary>
        /// 卷内页数所在列
        /// </summary>
        private string JNPageCount;
        /// <summary>
        /// 件数所在列
        /// </summary>
        private string JNCount;
        /// <summary>
        /// 全体卷内的DataView
        /// </summary>
        private DataView JNTotalView;
        /// <summary>
        /// 案卷卷内对应规则
        /// </summary>
        Dictionary<string, string> AJ_JN = new Dictionary<string, string>();
        /// <summary>
        /// 目录构成,名称,规定长度
        /// </summary>
        private Dictionary<string, int> dirConstitute = new Dictionary<string, int>();
        /// <summary>
        /// pdf文件名规则
        /// </summary>
        public Dictionary<string, int> PdfNameRule = new Dictionary<string, int>();

        /// <summary>
        /// 根目录
        /// </summary>
        private string rootDir;

        /// <summary>
        /// 最大文件夹深度
        /// </summary>
        private int maxFolderTreeDeepth = 0;

        /// <summary>
        /// 筛选文件类型
        /// </summary>
        private string fileFormat;

        /// <summary>
        /// 队列存放案卷实体
        /// </summary>
        Queue<AJEntity> ajEntities_Queue = new Queue<AJEntity>();
        /// <summary>
        /// 集合存放案卷实体
        /// </summary>
        List<AJEntity> ajEntities_List = new List<AJEntity>();
        /// <summary>
        /// 队列存放卷内实体
        /// </summary>
        Queue<JNEntity> jnEntities_Queue = new Queue<JNEntity>();
        /// <summary>
        /// 集合存放卷内实体
        /// </summary>
        List<JNEntity> jnEntities_List = new List<JNEntity>();

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
        private bool MatchSetted
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 是否完成拆分设置
        /// </summary>
        private bool DemergeSetted
        {
            get;
            set;
        } = false;
        private bool MergeSetted
        {
            get;
            set;
        } = false;

        private bool prepareAJComplete
        {
            get;
            set;
        } = false;

        public MainForm()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
        }

        private void btnSelectAJExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                isSelectingArchiveFile = true;
                tbAJFile.Text = openFileDialog1.FileName;
                AJCells = AJExcelHelper.Load_Excel(tbAJFile.Text.Trim());
                cbAJSheets.DataSource = AJExcelHelper.SheetsName;
                InitColumns(true);
                isSelectingArchiveFile = false;
            }
        }

        private void btnSelectJNExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                isSelectingArchiveFile = true;
                tbJNFile.Text = openFileDialog1.FileName;
                JNCells = JNExcelHelper.Load_Excel(tbJNFile.Text.Trim());
                cbJNSheets.DataSource = JNExcelHelper.SheetsName;
                InitColumns(false);
                isSelectingArchiveFile = false;
            }
        }

        /// <summary>
        /// 初始化列名
        /// </summary>
        /// <param name="isAJ">需要初始化的表类型</param>
        private void InitColumns(bool isAJ)
        {
            if (isAJ)
            {
                AJExcelHelper.Caculate_Columns(ColumnNameRow);
                AJExcelColumns = AJExcelHelper.ExcelColumns;
            }
            else
            {
                JNExcelHelper.Caculate_Columns(ColumnNameRow);
                JNExcelColumns = JNExcelHelper.ExcelColumns;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileFormat = "*" + this.comboBox1.SelectedItem.ToString();
        }

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
            }
        }



        private delegate void FlushClient(string AJLocatoin,string Findable, string ErrorInfo);//代理
        /// <summary>
        /// 保存异常信息
        /// </summary>
        /// <param name="AJLocatoin"></param>
        /// <param name="ErrorInfo"></param>
        public void WriteErrorInfo(string AJLocatoin,string Findable, string ErrorInfo)
        {
            if (listView_Error.InvokeRequired)
            {
                FlushClient fc = new FlushClient(WriteErrorInfo);
                BeginInvoke(fc, new object[] { AJLocatoin,Findable, ErrorInfo });
            }
            else
            {
                listView_Error.Items.Add(new ListViewItem(new string[4] { "", AJLocatoin, Findable, ErrorInfo }));
            }
        }

        private void cbAJSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAJSheets.SelectedIndex == -1 || isSelectingArchiveFile) return;
            AJCells = AJExcelHelper.Load_Excel(this.tbAJFile.Text.Trim(), cbAJSheets.SelectedItem.ToString());
            InitColumns(true);
        }

        private void cbJNSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbJNSheets.SelectedIndex == -1 || isSelectingArchiveFile) return;
            JNCells = JNExcelHelper.Load_Excel(this.tbJNFile.Text.Trim(), cbJNSheets.SelectedItem.ToString());
            InitColumns(false);
        }

        private void btnMatchSetting_Click(object sender, EventArgs e)
        {
            if (AJExcelColumns == null || AJExcelColumns.Count == 0)
            {
                MessageBox.Show("请先选择案卷文件"); return;
            }
            if(rootDir == null)
            {
                MessageBox.Show("请先选择扫描件目录");return;
            }
            if (rbOneToMany.Checked)
            {
                if (JNExcelColumns == null || JNExcelColumns.Count == 0)
                {
                    MessageBox.Show("请先选择卷内文件"); return;
                }
                else
                {
                    OneToManyMatchSetting matchSetting = new OneToManyMatchSetting(AJExcelColumns, JNExcelColumns, dirConstitute, AJPageCount, JNPageCount, JNCount, shouldRename);
                    if (matchSetting.ShowDialog() == DialogResult.OK)
                    {
                        dirConstitute = matchSetting.dirConstitute;
                        AJ_JN = matchSetting.AJ_JN;
                        AJPageCount = matchSetting.AJPageCount;
                        JNPageCount = matchSetting.JNPageCount;
                        JNCount = matchSetting.JNCount;
                        shouldRename = matchSetting.renameFolder;
                        if (matchSetting.renameFolder == true)
                        {
                            Rename();
                        }
                        MatchSetted = true;
                    }
                }
            }
            else
            {
                OneToOneMatchSetting matchSetting = new OneToOneMatchSetting(AJExcelColumns, dirConstitute, AJPageCount, shouldRename);
                if (matchSetting.ShowDialog() == DialogResult.OK)
                {
                    dirConstitute = matchSetting.dirConstitute;
                    AJPageCount = matchSetting.pageColumn;
                    shouldRename = matchSetting.renameFolder;
                    if (matchSetting.renameFolder == true)
                    {
                        Rename();
                    }
                    MatchSetted = true;
                }
            }
        }

        /// <summary>
        /// 数值检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbColuNameRow_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbColuNameRow.Text))
            {
                int num = 0;
                if (!int.TryParse(tbColuNameRow.Text, out num))
                {
                    ((System.Windows.Forms.TextBox)sender).Clear();
                    MessageBox.Show("必须输入数字");
                }
                else if(int.Parse(tbColuNameRow.Text.Trim()) <= 0)
                {
                    ((System.Windows.Forms.TextBox)sender).Clear();
                    MessageBox.Show("必须大于0");
                }
                else
                {
                    ColumnNameRow = int.Parse(tbColuNameRow.Text.Trim()) - 1;
                    InitColumns(true);
                    InitColumns(false);
                }
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (MatchSetted)
            {
                if (rbOneToMany.Checked)
                {
                    ajEntities_List.Clear();
                    this.listView_Error.Items.Clear();
                    this.UsefulAJ = 0;
                    this.progressBar1.Value = 0;
                    AdjustControlEnable(false);
                    this.prepareAJ.RunWorkerAsync();
                    this.match.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Match_DoWork_OneToMany);
                    this.match.RunWorkerAsync();
                }
                else
                {
                    ajEntities_List.Clear();
                    this.listView_Error.Items.Clear();
                    this.UsefulAJ = 0;
                    this.progressBar1.Value = 0;
                    AdjustControlEnable(false);
                    this.prepareAJ.RunWorkerAsync();
                    this.match.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Match_DoWork_OneToOne);
                    this.match.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("请先完成检测设置");
            }
        }

        private void Match_DoWork_OneToMany(object sender, DoWorkEventArgs e)
        {
            DataTable JNTotalTable = DataTableBuilder(JNCells);
            JNTotalView = JNTotalTable.AsDataView();
            while (true)
            {
                lock (locker)
                {
                    if (ajEntities_Queue.Count > 0)
                    {
                        AJEntity aj = ajEntities_Queue.Dequeue();
                        string Findable = string.Join("-",aj.Key);
                        string path = rootDir + "\\" + aj.Dir;
                        var readResult = ReadDir(aj.Pages, path);
                        if (readResult != null)
                        {
                            aj.AJPageFileCount_CheckOK = false;
                            WriteErrorInfo(aj.Location.ToString(), Findable, readResult);
                        }
                        else aj.AJPageFileCount_CheckOK = true;
                        ajEntities_List.Add(aj);
                        JNEntity jn = BuildJNEntity(aj);
                        if(aj.Pages != jn.TotalPage)
                        {
                            WriteErrorInfo(aj.Location.ToString(), Findable, "案卷页数与卷内总页数不匹配");
                        }
                        if(JNCount != null)
                        {
                            if(aj.JNCount != jn.JNCount)
                            {
                                WriteErrorInfo(aj.Location.ToString(), Findable, "案卷件数与卷内条目数不匹配");
                            }
                        }
                        jnEntities_List.Add(jn);
                    }
                    if (prepareAJComplete == true && ajEntities_List.Count == UsefulAJ)
                    {
                        break;
                    }
                }
            }
        }
        private JNEntity BuildJNEntity(AJEntity aj)
        {
            try
            {
                string Findable = string.Join("-", aj.Key);
                JNEntity jn = new JNEntity();
                string filter = string.Empty;
                List<string> columnName = AJ_JN.Values.ToList();
                for (int i = 0; i < AJ_JN.Count; i++)
                {
                    filter += columnName[i] + "=" + "'" + aj.Key[i] + "'";
                    if (i != AJ_JN.Count - 1)
                    {
                        filter += " and ";
                    }
                }
                JNTotalView.RowFilter = filter;
                DataTable jnTable = JNTotalView.ToTable();
                int JNCount = jnTable.Rows.Count;
                if (JNCount != 0)
                {
                    jn.Value = jnTable;
                    List<int> pages = new List<int>();
                    for (int i = 0; i < JNCount; i++)
                    {
                        int page = int.Parse(jnTable.Rows[i][JNExcelColumns[JNPageCount]].ToString());
                        pages.Add(page);
                    }
                    jn.TotalPage = pages.Sum();
                    jn.JNCount = JNCount;
                    jn.Key = Findable;
                    return jn;
                }
                else
                {
                    WriteErrorInfo(aj.Location.ToString(),Findable, "未能找到对应的卷内:" + filter);
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteErrorInfo(aj.Location.ToString(), "", ex.Message);
                return null;
            }
        }

        private void Prepare_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.lblPercent.Text = @"已完成：" + e.ProgressPercentage.ToString() + @"%";
        }

        private void Match_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("检测完成");
            AdjustControlEnable(true);
        }

        private void Prepare_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prepareAJComplete = true;
        }

        /// <summary>
        /// 正常案卷
        /// </summary>
        private int UsefulAJ = 0;
        /// <summary>
        /// 组装案卷信息,不保留[页数为空],[路径无法拼接],[某单元格无列名]情况的案卷
        /// </summary>
        private void PrepareAJ_DoWork(object sender, DoWorkEventArgs e)
        {
            int location = 0;
            try
            {
                prepareAJComplete = false;
                for (int i = ColumnNameRow + 1; i < AJCells.MaxDataRow +1; i++)
                {
                    location = i + 1;
                    float a = ((float)(i - ColumnNameRow) / (float)(AJCells.MaxDataRow - ColumnNameRow) * 100 );
                    int percent = (int)a;
                    prepareAJ.ReportProgress(percent);

                    AJEntity aj = new AJEntity();

                    aj.Location = location;

                    string page = GetCellValue(i, AJExcelColumns[AJPageCount]);
                    if (!string.IsNullOrWhiteSpace(page))
                    {
                        aj.Pages = int.Parse(page);
                    }
                    else
                    {
                        continue;
                    }

                    if (JNCount != null)
                    {
                        string Count = GetCellValue(i, AJExcelColumns[JNCount]);
                        if (!string.IsNullOrWhiteSpace(Count))
                        {
                            aj.JNCount = int.Parse(Count);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    var path = SpellPath(dirConstitute, i);
                    if(path != null)
                    {
                        aj.Dir = path;
                    }
                    else
                    {
                        continue;
                    }
                    DataTable AJ = DataTableBuilder(i);
                    if (AJ != null)
                    {
                        aj.Value = AJ;
                    }
                    else
                    {
                        continue;
                    }

                    List<string> keyList = ListBuilder(i);
                    aj.Key = keyList;

                    InsertQueue(aj);
                    UsefulAJ++;
                }
            }
            catch(Exception ex)
            {
                WriteErrorInfo(location.ToString(),"-", ex.Message);
            }
        }

        private List<string> ListBuilder(int i)
        {
            try
            {
                List<string> AJ = new List<string>();
                foreach (string key in AJ_JN.Keys)
                {
                    var currentCellValue = AJCells[i, AJExcelColumns[key]].Value;
                    if (currentCellValue != null && !string.IsNullOrWhiteSpace(currentCellValue.ToString()))
                    {
                        AJ.Add(currentCellValue.ToString());
                    }
                }
                return AJ;
            }
            catch(Exception ex)
            {
                WriteErrorInfo("-", "ListBuilder" + i, ex.Message);
                return null;
            }
        }

        private void InsertQueue(AJEntity aj)
        {
            lock (locker)
            {
                ajEntities_Queue.Enqueue(aj);
            }
        }
        private void InsertQueue(JNEntity jn)
        {
            lock(locker)
            {
                jnEntities_Queue.Enqueue(jn);
            }
        }

        private void Match_DoWork_OneToOne(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                AJEntity entity = new AJEntity();
                lock (locker)
                {
                    if (ajEntities_Queue.Count > 0)
                    {
                        entity = ajEntities_Queue.Dequeue();
                        string path = rootDir + "\\" + entity.Dir;
                        var readResult = ReadDir(entity.Pages, path);
                        if (readResult != null)
                        {
                            entity.AJPageFileCount_CheckOK = false;
                            WriteErrorInfo(entity.Location.ToString(),"-", readResult);
                        }
                        else entity.AJPageFileCount_CheckOK = true;
                        ajEntities_List.Add(entity);
                    }
                    if (prepareAJComplete == true && ajEntities_List.Count == UsefulAJ)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 读取指定坐标的值,如果为空就直接抛出异常
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private string GetCellValue(int x,int y)
        {
            try
            {
                string value;
                int a = x + 1;
                var currentCellValue = AJCells[x, y].Value;
                if (currentCellValue == null || string.IsNullOrWhiteSpace(currentCellValue.ToString()))
                {
                    WriteErrorInfo(a.ToString(), GetCellValue(ColumnNameRow, y), "单元格为空");
                    return null;
                }
                else
                {
                    value = currentCellValue.ToString();
                    return value;
                }
            }
            catch(Exception ex)
            {
                WriteErrorInfo("", "GetCellValue" + x + "," + y, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 拼接路径
        /// </summary>
        /// <param name="dirConstitute">拼接条件</param>
        /// <param name="i">当前行号</param>
        /// <returns></returns>
        private string SpellPath(Dictionary<string,int> dirConstitute,int i)
        {
            try
            {
                string path;
                int x = i + 1;
                List<string> partPath = new List<string>();
                foreach (string s in dirConstitute.Keys)
                {
                    int y = AJExcelColumns[s] + 1;
                    string part = GetCellValue(i, AJExcelColumns[s]);
                    if (!string.IsNullOrWhiteSpace(part))
                    {
                        int length = dirConstitute[s];
                        if (length != 0)
                        {
                            if (part.Length < length)
                            {
                                //WriteErrorInfo("[" + x.ToString() + "," + y.ToString() + "]", "长度小于规定值" + length + ",将在左侧补0后执行操作");
                                part = part.PadLeft(length, '0');
                                partPath.Add(part);
                            }
                            else if (part.Length > length)
                            {
                                //WriteErrorInfo("[" + x.ToString() + "," + y.ToString() + "]", "长度大于规定值" + length + ",将从右侧截取后执行操作");
                                part = part.Substring(part.Length - length);
                                partPath.Add(part);
                            }
                        }
                        else
                        {
                            partPath.Add(part);
                        }
                    }
                    else
                    {
                        WriteErrorInfo("[" + x.ToString() + "," + y.ToString() + "]", "-", "该单元格为空,拼接路径失败");
                        break;
                    }
                }
                if (partPath.Count != 0)
                {
                    path = string.Join(@"\", partPath.ToArray());
                    return path;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                WriteErrorInfo("", "SpellPath" + i, ex.Message);
                return null;
            }
        }

        private DataTable DataTableBuilder(int i)
        {
            DataTable AJ = new DataTable();
            AJ.Rows.Add();
            for (int j = 0; j <= AJCells.Rows[i].LastCell.Column; j++)
            {
                var currentCellValue = AJCells[i, j].Value;
                if (currentCellValue != null && !string.IsNullOrWhiteSpace(currentCellValue.ToString()))
                {
                    var columnNameValue = AJCells[ColumnNameRow, j].Value;
                    if (columnNameValue != null && !string.IsNullOrWhiteSpace(columnNameValue.ToString()))
                    {
                        string columnName = columnNameValue.ToString();
                        AJ.Columns.Add(columnName);
                        AJ.Rows[0][columnName] = currentCellValue.ToString();
                    }
                    else
                    {
                        int x = i + 1;
                        int y = j + 1;
                        WriteErrorInfo("[" + x + "," + y + "]", "","未找到该单元格对应的列名");
                        return null;
                    }
                }
            }
            return AJ;
        }
        private DataTable DataTableBuilder(Cells cells)
        {
            DataTable dataTable = cells.ExportDataTableAsString(ColumnNameRow, 0, cells.MaxDataRow +1, cells.MaxDataColumn + 1, true);
            return dataTable;
        }

        /// <summary>
		/// 读取扫描件情况
		/// </summary>
		/// <param name="targetPage">目标页数</param>
		/// <param name="path">指定路径</param>
		/// <returns>信息</returns>
		private string ReadDir(int targetPage, string path)
        {
            string result;
            if (Directory.Exists(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                int fileCount = GetFilesCount(dirInfo,fileFormat);
                if (fileCount == targetPage)
                {
                    return null;
                }
                else
                {
                    result = "页数与扫描件数量不匹配";
                    return result;
                }
            }
            else
            {
                result = "不存在给定的路径:" + path;
                return result;
            }
        }
        /// <summary>
		/// 递归读取所有文件
		/// </summary>
		/// <param name="dirInfo"></param>
		/// <returns></returns>
		public static int GetFilesCount(DirectoryInfo dirInfo,string fileFormat)
        {

            int totalFile = 0;
            totalFile += dirInfo.GetFiles(fileFormat).Length;//获取指定文件
            foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                totalFile += GetFilesCount(subdir,fileFormat);
            }
            return totalFile;
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
            foreach (string column in saveColumn)
            {
                JNs.Columns.Add(column);
            }
            JNs.Columns.Add("序号");
            JNs.Columns.Add("题名");
            JNs.Columns.Add("页数");
            JNs.Columns.Add("起始页号");
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
            if (listView_Error.Items.Count != 0)
            {
                MessageBox.Show("存在问题,请检查");
            }
            else
            {
                ExcelSaver.SaveToExcel(JNs);
            }
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
            JN.Columns.Add("起始页号");
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
                        row["题名"] = turnRow[i];
                        string[] pageNum = AJ.Rows[0][turnRow[i]].ToString().Split('-');
                        if (pageNum.Length > 1)
                        {
                            row["页数"] = int.Parse(pageNum[1].Trim()) - int.Parse(pageNum[0].Trim()) + 1;
                        }
                        else if (pageNum.Length == 1)
                        {
                            row["页数"] = 1;
                        }
                        row["起始页号"] = pageNum[0].Trim();
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

        private void rbOneToMany_CheckedChanged(object sender, EventArgs e)
        {
            tbJNFile.Enabled = rbOneToMany.Checked;
            btnSelectJNExcel.Enabled = rbOneToMany.Checked;
            cbJNSheets.Enabled = rbOneToMany.Checked;
        }
        
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
                if (rbOneToMany.Checked)
                {
                    this.listView_Error.Items.Clear();
                    this.progressBar1.Value = 0;
                    AdjustControlEnable(false);
                    this.mergeFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MergeFile_DoWork_OneToMany);
                    this.mergeFile.RunWorkerAsync();
                }
                else
                {
                    this.listView_Error.Items.Clear();
                    this.progressBar1.Value = 0;
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
        private void MergeFile_DoWork_OneToMany(object sender, DoWorkEventArgs e)
        {
            int nowJNrow = 1;
            try
            {
                foreach (AJEntity aj in ajEntities_List)
                {
                    string Findable = string.Join("-", aj.Key);
                    //根据案卷片段路径读取路径下符合条件的文件,并根据文件名排序
                    string path = rootDir + "\\" + aj.Dir;
                    List<FileInfo> AJfileInfos = GetFileInfo(path);
                    //根据关键值筛选卷内list
                    var JNEntityData = jnEntities_List.AsEnumerable();
                    var query = from a in JNEntityData
                                where a.Key == Findable
                                select a;

                    foreach (JNEntity jn in query)
                    {
                        //当前卷内起始页
                        int nowPage = 0;
                        //对每个卷内行执行合并操作
                        for (int i = 0; i < jn.Value.Rows.Count; i++)
                        {
                            List<string> inputFiles = new List<string>();
                            //当前卷内页数
                            int page = int.Parse(jn.Value.Rows[i][JNPageCount].ToString());
                            //下一个卷内的起始页
                            int endPage = nowPage + page;
                            for (int j = nowPage; j < endPage; j++)
                            {
                                inputFiles.Add(AJfileInfos[j].FullName);
                            }
                            nowPage = endPage;
                            #region 拼接pdf文件名
                            List<string> fileName_List = new List<string>();
                            foreach (string Key in PdfNameRule.Keys)
                            {
                                int bit = PdfNameRule[Key];
                                string partName = jn.Value.Rows[i][Key].ToString();
                                if (bit != 0)
                                {
                                    if (partName.Length < bit)
                                    {
                                        partName = partName.PadLeft(bit, '0');
                                    }
                                    else if (partName.Length > bit)
                                    {
                                        partName = partName.Substring(partName.Length - bit);
                                    }
                                }
                                fileName_List.Add(partName);
                            }
                            string fileName = string.Join("-", fileName_List);
                            #endregion
                            op.MergerFile(fileSaveFolder + "\\" + fileName + ".pdf", "pdf", inputFiles.ToArray(), null);
                            float temp = ((float)(nowJNrow) / (float)(JNCells.MaxDataRow - ColumnNameRow) * 100);
                            int percent = (int)temp;
                            mergeFile.ReportProgress(percent);
                            nowJNrow++;
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                WriteErrorInfo("-", "MergeFile_DoWork_OneToMany", ex.Message);
            }
        }
        private void MergeFile_DoWork_OneToOne(object sender, DoWorkEventArgs e)
        {
            int nowRow = 1;
            try
            {
                foreach (AJEntity aj in ajEntities_List)
                {
                    string Findable = string.Join("-", aj.Key);
                    //根据案卷片段路径读取路径下符合条件的文件,并根据文件名排序
                    string path = rootDir + "\\" + aj.Dir;
                    List<FileInfo> AJfileInfos = GetFileInfo(path);
                    List<string> inputFiles = new List<string>();
                    foreach(FileInfo file in AJfileInfos)
                    {
                        inputFiles.Add(file.FullName);
                    }
                    //根据关键值筛选卷内list
                    #region 拼接pdf文件名
                    List<string> fileName_List = new List<string>();
                    foreach (string Key in PdfNameRule.Keys)
                    {
                        int bit = PdfNameRule[Key];
                        string partName = aj.Value.Rows[0][Key].ToString();
                        if (bit != 0)
                        {
                            if (partName.Length < bit)
                            {
                                partName = partName.PadLeft(bit, '0');
                            }
                            else if (partName.Length > bit)
                            {
                                partName = partName.Substring(partName.Length - bit);
                            }
                        }
                        fileName_List.Add(partName);
                    }
                    string fileName = string.Join("-", fileName_List);
                    #endregion
                    op.MergerFile(fileSaveFolder + "\\" + fileName + ".pdf", "pdf", inputFiles, null);

                    float temp = ((float)(nowRow) / (float)(JNCells.MaxDataRow - ColumnNameRow) * 100);
                    int percent = (int)temp;
                    mergeFile.ReportProgress(percent);
                    nowRow++;
                }
            }
            catch (Exception ex)
            {
                WriteErrorInfo("-", "MergeFile_DoWork_OneToMany", ex.Message);
            }
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
            if (rbOneToMany.Checked)
            {
                if (JNExcelColumns == null || JNExcelColumns.Count == 0)
                {
                    MessageBox.Show("请先选择卷内文件"); return;
                }
                else
                {
                    MergeSetting mergeSetting = new MergeSetting( JNExcelColumns, PdfNameRule);
                    if (mergeSetting.ShowDialog() == DialogResult.OK)
                    {
                        PdfNameRule = mergeSetting.PdfNameRule;
                        MergeSetted = true;
                    }
                }
            }
            else
            {
                MergeSetting mergeSetting = new MergeSetting(AJExcelColumns, PdfNameRule,false);
                if (mergeSetting.ShowDialog() == DialogResult.OK)
                {
                    PdfNameRule = mergeSetting.PdfNameRule;
                    MergeSetted = true;
                }
            }
        }

        private void mergeFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("合并完成");
            AdjustControlEnable(true);
        }

        private void mergeFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.lblPercent.Text = @"已完成：" + e.ProgressPercentage.ToString() + @"%";
        }

        /// <summary>
        /// 调整控件状态
        /// </summary>
        /// <param name="enable">是否可用</param>
        private void AdjustControlEnable(bool enable)
        {
            foreach(Control control in this.Controls)
            {
                control.Enabled = enable;
            }
        }
    }
}
