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
    public partial class MainForm : Form
    {
        private static MainForm personnelChecklist = null;
        public static MainForm CreateInstrance()
        {
            if (personnelChecklist == null || personnelChecklist.IsDisposed)
            {
                personnelChecklist = new MainForm();
            }
            return personnelChecklist;
        }

        #region 规则

        /// <summary>
        /// 列名所在行
        /// </summary>
        private int ColumnNameRow
        {
            get;
            set;
        } = 0;

        private string Path;

        private string Config;
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
        private List<Data> infoEntity_List = new List<Data>();

        #endregion

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object locker = new object();

        public MainForm()
        {
            InitializeComponent();
            UnAdjustList_ADD();
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
                            control.Enabled = true;
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
                if (infoEntity_List.Count != 0)
                    infoEntity_List = new List<Data>();
            }
        }

        private void cbAJSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataReader.ResetSheet(cbAJSheets.SelectedItem.ToString());
            if (infoEntity_List.Count != 0)
                infoEntity_List = new List<Data>();
        }

        #endregion

        #region 设置扫描件目录

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择目录";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbFilesPath.Text = folderBrowserDialog.SelectedPath;
                Path = tbFilesPath.Text;
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

        private void tbThread_TextChanged(object sender, EventArgs e)
        {
            if (ControlHelper.NumberCheck(sender, true))
            {
                ThreadHelper.ThreadCount = int.Parse(((TextBox)sender).Text.Trim());
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ControlHelper.NumberCheck(sender, true))
            {
                ThreadHelper.Wait = int.Parse(((TextBox)sender).Text.Trim());
            }
        }
        #endregion

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
            for (int j = 0; j < reader.Cells.Rows[i].LastCell.Column + 1; j++)
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


        private void bwgPrepareData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AdjustProgress(e.ProgressPercentage);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            bwgPrepareData.RunWorkerAsync();
        }

        private void ShowOneDoneMsg(Data x, CompetedEventArgs args)
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
            if (DataReader.HasValue)
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
                        Data info = new Data(Path,location, value);
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
            else
            {
                DirectoryInfo dir = new DirectoryInfo(Path);
                DirectoryInfo[] dis = dir.GetDirectories();//目录下的子目录
                foreach(DirectoryInfo d in dis)
                {
                    Data info = new Data(d.FullName);
                    infoEntity_List.Add(info);
                }
            }
        }

        private void bwgPrepareData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EndProgress();
            MessageBox.Show("读取完成");
        }

        private void Work(Data info)
        {
            info.Work(Config);
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            Data.saveParent = tbSavePath.Text;
            StartProgress();
            ThreadBaseControl<Data> thfd = new ThreadBaseControl<Data>(infoEntity_List, Work);
            thfd.OneCompleted += ShowOneDoneMsg;
            thfd.AllCompleted += ShowAllDoneMsg;
            thfd.Start();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbConfig.Text = openFileDialog.FileName;
                Config = tbConfig.Text;
            }
        }

        private void tbFilesPath_TextChanged(object sender, EventArgs e)
        {
            Path = tbFilesPath.Text;
        }

        private void tbConfig_TextChanged(object sender, EventArgs e)
        {
            Config = tbConfig.Text;
        }

        
    }
}
