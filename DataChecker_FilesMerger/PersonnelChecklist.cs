using DataChecker_FilesMerger.Dialog_Setting;
using DataChecker_FilesMerger.Entities;
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
    public partial class PersonnelChecklist : Form
    {
        private static PersonnelChecklist personnelChecklist = null;
        public static PersonnelChecklist CreateInstrance()
        {
            if (personnelChecklist == null || personnelChecklist.IsDisposed)
            {
                personnelChecklist = new PersonnelChecklist();
            }
            return personnelChecklist;
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
        private ExcelReader DataReader
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
        private List<PersonalInfo> infoEntity_List = new List<PersonalInfo>();

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

        public PersonnelChecklist()
        {
            InitializeComponent();
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
            AdjustByRb.Add(tbModeFile.Name);
            AdjustByRb.Add(btnSelectJNExcel.Name);
            AdjustByRb.Add(cbJNSheets.Name);
        }

        //private void ResetState()
        //{
        //    if (DataLoaded)
        //    {
        //        if (MessageBox.Show("是否用原有设置重新加载数据?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            LoadData();
        //        }
        //        else
        //        {
        //            DataLoaded = false;
        //        }
        //    }
        //    DemergeSetted = false;
        //    MergeSetted = false;
        //}

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
                tbDataFile.Text = openFileDialog.FileName;
                DataReader.Load_Excel(tbDataFile.Text.Trim());
                cbAJSheets.DataSource = DataReader.SheetsName;
            }
        }

        private void btnSelectJNExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbModeFile.Text = openFileDialog.FileName;
            }
        }

        private void cbAJSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataReader.ResetSheet(cbAJSheets.SelectedItem.ToString());
        }

        private void cbJNSheets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region 设置扫描件目录

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (comboBox1.SelectedIndex == -1)
            //{
            //    MessageBox.Show("请先选择需要合并的文件格式!");
            //    return;
            //}
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择目录";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbFilesPath.Text = folderBrowserDialog.SelectedPath;
                rootDir = tbFilesPath.Text;
                //if (JumpScanFiles)
                //{
                //    MessageBox.Show("扫描件路径追加完成,将重新读取数据!");
                //    LoadData();
                //}
                //fileFormat = "*" + this.comboBox1.SelectedItem.ToString();
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
            {
                ColumnNameRow = int.Parse(tbColuNameRow.Text.Trim()) - 1;
            }
        }

        #endregion

        #region 组装案卷

        /// <summary>
        /// 初始化列名
        /// </summary>
        private void InitColumns()
        {
            DataReader.Caculate_Columns(ColumnNameRow);
            AJExcelColumns = DataReader.ExcelColumns;
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


        #endregion

        #region 合并图片



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

        private void button1_Click(object sender, EventArgs e)
        {
            bwgPrepareData.RunWorkerAsync();
        }

        private void btnTurn_Click(object sender, EventArgs e)
        {
            StartProgress();
            ThreadBaseControl<PersonalInfo> thfd = new ThreadBaseControl<PersonalInfo>(infoEntity_List, Check);
            thfd.OneCompleted += ShowOneDoneMsg;
            thfd.AllCompleted += ShowAllDoneMsg;
            thfd.Start();
        }

        private void Check(PersonalInfo info)
        {
            info.Turn2Check(tbModeFile.Text,rootDir);
        }

        private void ShowOneDoneMsg(PersonalInfo x, CompetedEventArgs args)
        {
            AdjustProgress(args.CompetedPrecent);
        }

        private void ShowAllDoneMsg(CompetedEventArgs args)
        {
            EndProgress();
            MessageBox.Show("完成!");
        }

        private void bwgPrepareData_DoWork(object sender, DoWorkEventArgs e)
        {
            int complete = 0;
            int DataCount = DataReader.Cells.MaxDataRow;
            int total = DataCount;

            InitColumns();
            for (int i = ColumnNameRow + 1; i < DataCount + 1; i++)
            {
                int location = i + 1;
                try
                {
                    var value = DataBuilder(i, DataReader);
                    PersonalInfo info = new PersonalInfo(location, value);
                    infoEntity_List.Add(info);
                    complete++;
                    int percent = complete * 100 / total;
                    bwgPrepareData.ReportProgress(percent);
                }
                catch (Exception ex)
                {
                    WriteErrorInfo("行号:" + location.ToString(), "[PrepareInfo]", ex.Message);
                    continue;
                }
            }
        }

        private void bwgPrepareData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EndProgress();
            MessageBox.Show("读取完成");
        }

        private void btnLicen_Click(object sender, EventArgs e)
        {
            StartProgress();
            ThreadBaseControl<PersonalInfo> thfd = new ThreadBaseControl<PersonalInfo>(infoEntity_List, Licen);
            thfd.OneCompleted += ShowOneDoneMsg;
            thfd.AllCompleted += ShowAllDoneMsg;
            thfd.Start();
        }

        private void Licen(PersonalInfo info)
        {
            info.Turn2Licen(tbModeFile.Text, rootDir,cb.Checked);
        }
    }
}
