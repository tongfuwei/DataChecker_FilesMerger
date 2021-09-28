namespace DataChecker_FilesMerger
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbDataFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectAJExcel = new System.Windows.Forms.Button();
            this.cbAJSheets = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbFilesPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listView_Error = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnExportError = new System.Windows.Forms.Button();
            this.lblPercent = new System.Windows.Forms.Label();
            this.UpdatePercent = new System.Windows.Forms.Timer(this.components);
            this.btnLoad = new System.Windows.Forms.Button();
            this.tbColuNameRow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bwgPrepareData = new System.ComponentModel.BackgroundWorker();
            this.tbThread = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnWork = new System.Windows.Forms.Button();
            this.tbConfig = new System.Windows.Forms.TextBox();
            this.btnConfig = new System.Windows.Forms.Button();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbDataFile
            // 
            this.tbDataFile.Location = new System.Drawing.Point(94, 11);
            this.tbDataFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbDataFile.Name = "tbDataFile";
            this.tbDataFile.Size = new System.Drawing.Size(489, 25);
            this.tbDataFile.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "选择数据:";
            // 
            // btnSelectAJExcel
            // 
            this.btnSelectAJExcel.Location = new System.Drawing.Point(589, 11);
            this.btnSelectAJExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectAJExcel.Name = "btnSelectAJExcel";
            this.btnSelectAJExcel.Size = new System.Drawing.Size(82, 25);
            this.btnSelectAJExcel.TabIndex = 19;
            this.btnSelectAJExcel.Text = "查看";
            this.btnSelectAJExcel.UseVisualStyleBackColor = true;
            this.btnSelectAJExcel.Click += new System.EventHandler(this.btnSelectAJExcel_Click);
            // 
            // cbAJSheets
            // 
            this.cbAJSheets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAJSheets.FormattingEnabled = true;
            this.cbAJSheets.Location = new System.Drawing.Point(743, 11);
            this.cbAJSheets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAJSheets.Name = "cbAJSheets";
            this.cbAJSheets.Size = new System.Drawing.Size(114, 23);
            this.cbAJSheets.TabIndex = 24;
            this.cbAJSheets.SelectedIndexChanged += new System.EventHandler(this.cbAJSheets_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(677, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "工作簿:";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(589, 70);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(82, 25);
            this.btnSelectFolder.TabIndex = 28;
            this.btnSelectFolder.Text = "查看";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbFilesPath
            // 
            this.tbFilesPath.Location = new System.Drawing.Point(94, 70);
            this.tbFilesPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbFilesPath.Name = "tbFilesPath";
            this.tbFilesPath.Size = new System.Drawing.Size(489, 25);
            this.tbFilesPath.TabIndex = 27;
            this.tbFilesPath.TextChanged += new System.EventHandler(this.tbFilesPath_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "文件路径:";
            // 
            // listView_Error
            // 
            this.listView_Error.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.column1,
            this.column2,
            this.column3});
            this.listView_Error.FullRowSelect = true;
            this.listView_Error.GridLines = true;
            this.listView_Error.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_Error.HideSelection = false;
            this.listView_Error.Location = new System.Drawing.Point(11, 173);
            this.listView_Error.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView_Error.Name = "listView_Error";
            this.listView_Error.Size = new System.Drawing.Size(846, 246);
            this.listView_Error.TabIndex = 30;
            this.listView_Error.UseCompatibleStateImageBehavior = false;
            this.listView_Error.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // column1
            // 
            this.column1.Text = "案卷Excel行号";
            this.column1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column1.Width = 120;
            // 
            // column2
            // 
            this.column2.Text = "索引值";
            this.column2.Width = 100;
            // 
            // column3
            // 
            this.column3.Text = "错误信息";
            this.column3.Width = 622;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 457);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(845, 23);
            this.progressBar1.TabIndex = 39;
            // 
            // btnExportError
            // 
            this.btnExportError.Location = new System.Drawing.Point(774, 426);
            this.btnExportError.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExportError.Name = "btnExportError";
            this.btnExportError.Size = new System.Drawing.Size(82, 25);
            this.btnExportError.TabIndex = 40;
            this.btnExportError.Text = "错误导出";
            this.btnExportError.UseVisualStyleBackColor = true;
            this.btnExportError.Click += new System.EventHandler(this.btnExportError_Click);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(12, 431);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(67, 15);
            this.lblPercent.TabIndex = 41;
            this.lblPercent.Text = "工作进度";
            // 
            // UpdatePercent
            // 
            this.UpdatePercent.Enabled = true;
            this.UpdatePercent.Interval = 50;
            this.UpdatePercent.Tick += new System.EventHandler(this.UpdatePercent_work);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(134, 101);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(82, 25);
            this.btnLoad.TabIndex = 50;
            this.btnLoad.Text = "加载";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tbColuNameRow
            // 
            this.tbColuNameRow.Location = new System.Drawing.Point(94, 101);
            this.tbColuNameRow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbColuNameRow.Name = "tbColuNameRow";
            this.tbColuNameRow.Size = new System.Drawing.Size(33, 25);
            this.tbColuNameRow.TabIndex = 51;
            this.tbColuNameRow.TextChanged += new System.EventHandler(this.tbColuNameRow_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 52;
            this.label4.Text = "列名行号:";
            // 
            // bwgPrepareData
            // 
            this.bwgPrepareData.WorkerReportsProgress = true;
            this.bwgPrepareData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwgPrepareData_DoWork);
            this.bwgPrepareData.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwgPrepareData_ProgressChanged);
            this.bwgPrepareData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwgPrepareData_RunWorkerCompleted);
            // 
            // tbThread
            // 
            this.tbThread.Location = new System.Drawing.Point(94, 132);
            this.tbThread.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbThread.Name = "tbThread";
            this.tbThread.Size = new System.Drawing.Size(33, 25);
            this.tbThread.TabIndex = 62;
            this.tbThread.Text = "4";
            this.tbThread.TextChanged += new System.EventHandler(this.tbThread_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 63;
            this.label2.Text = "线程数:";
            // 
            // btnWork
            // 
            this.btnWork.Location = new System.Drawing.Point(222, 101);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(82, 25);
            this.btnWork.TabIndex = 64;
            this.btnWork.Text = "执行";
            this.btnWork.UseVisualStyleBackColor = true;
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // tbConfig
            // 
            this.tbConfig.Location = new System.Drawing.Point(94, 40);
            this.tbConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.Size = new System.Drawing.Size(489, 25);
            this.tbConfig.TabIndex = 65;
            this.tbConfig.TextChanged += new System.EventHandler(this.tbConfig_TextChanged);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(589, 40);
            this.btnConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(82, 25);
            this.btnConfig.TabIndex = 66;
            this.btnConfig.Text = "查看";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // tbSavePath
            // 
            this.tbSavePath.Location = new System.Drawing.Point(310, 103);
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.Size = new System.Drawing.Size(361, 25);
            this.tbSavePath.TabIndex = 67;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(222, 132);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(82, 25);
            this.textBox1.TabIndex = 68;
            this.textBox1.Text = "1000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 69;
            this.label5.Text = "线程等待:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 495);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tbSavePath);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.tbConfig);
            this.Controls.Add(this.btnWork);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbThread);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbColuNameRow);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.btnExportError);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listView_Error);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.tbFilesPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbAJSheets);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSelectAJExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDataFile);
            this.Name = "MainForm";
            this.Text = "PDF批量转图片";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDataFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectAJExcel;
        private System.Windows.Forms.ComboBox cbAJSheets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbFilesPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView_Error;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader column1;
        private System.Windows.Forms.ColumnHeader column2;
        private System.Windows.Forms.ColumnHeader column3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnExportError;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer UpdatePercent;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox tbColuNameRow;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker bwgPrepareData;
        private System.Windows.Forms.TextBox tbThread;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnWork;
        private System.Windows.Forms.TextBox tbConfig;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.TextBox tbSavePath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}

