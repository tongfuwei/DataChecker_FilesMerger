namespace DataChecker_FilesMerger
{
    partial class PersonnelChecklist
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
            this.label5 = new System.Windows.Forms.Label();
            this.tbModeFile = new System.Windows.Forms.TextBox();
            this.btnSelectJNExcel = new System.Windows.Forms.Button();
            this.cbJNSheets = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.UpdatePercent = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tbColuNameRow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bwgPrepareData = new System.ComponentModel.BackgroundWorker();
            this.btnTurn = new System.Windows.Forms.Button();
            this.btnLicen = new System.Windows.Forms.Button();
            this.cb = new System.Windows.Forms.CheckBox();
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "选择模板:";
            // 
            // tbModeFile
            // 
            this.tbModeFile.Location = new System.Drawing.Point(94, 40);
            this.tbModeFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbModeFile.Name = "tbModeFile";
            this.tbModeFile.Size = new System.Drawing.Size(489, 25);
            this.tbModeFile.TabIndex = 44;
            // 
            // btnSelectJNExcel
            // 
            this.btnSelectJNExcel.Location = new System.Drawing.Point(589, 40);
            this.btnSelectJNExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectJNExcel.Name = "btnSelectJNExcel";
            this.btnSelectJNExcel.Size = new System.Drawing.Size(82, 25);
            this.btnSelectJNExcel.TabIndex = 45;
            this.btnSelectJNExcel.Text = "查看";
            this.btnSelectJNExcel.UseVisualStyleBackColor = true;
            this.btnSelectJNExcel.Click += new System.EventHandler(this.btnSelectJNExcel_Click);
            // 
            // cbJNSheets
            // 
            this.cbJNSheets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJNSheets.Enabled = false;
            this.cbJNSheets.FormattingEnabled = true;
            this.cbJNSheets.Location = new System.Drawing.Point(743, 42);
            this.cbJNSheets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbJNSheets.Name = "cbJNSheets";
            this.cbJNSheets.Size = new System.Drawing.Size(114, 23);
            this.cbJNSheets.TabIndex = 49;
            this.cbJNSheets.SelectedIndexChanged += new System.EventHandler(this.cbJNSheets_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(677, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "工作簿:";
            // 
            // UpdatePercent
            // 
            this.UpdatePercent.Enabled = true;
            this.UpdatePercent.Interval = 50;
            this.UpdatePercent.Tick += new System.EventHandler(this.UpdatePercent_work);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 25);
            this.button1.TabIndex = 50;
            this.button1.Text = "加载";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.bwgPrepareData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwgPrepareData_RunWorkerCompleted);
            // 
            // btnTurn
            // 
            this.btnTurn.Location = new System.Drawing.Point(222, 101);
            this.btnTurn.Name = "btnTurn";
            this.btnTurn.Size = new System.Drawing.Size(82, 25);
            this.btnTurn.TabIndex = 53;
            this.btnTurn.Text = "生成核查表";
            this.btnTurn.UseVisualStyleBackColor = true;
            this.btnTurn.Click += new System.EventHandler(this.btnTurn_Click);
            // 
            // btnLicen
            // 
            this.btnLicen.Location = new System.Drawing.Point(310, 101);
            this.btnLicen.Name = "btnLicen";
            this.btnLicen.Size = new System.Drawing.Size(82, 25);
            this.btnLicen.TabIndex = 54;
            this.btnLicen.Text = "生成说明";
            this.btnLicen.UseVisualStyleBackColor = true;
            this.btnLicen.Click += new System.EventHandler(this.btnLicen_Click);
            // 
            // cb
            // 
            this.cb.AutoSize = true;
            this.cb.Location = new System.Drawing.Point(310, 132);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(89, 19);
            this.cb.TabIndex = 55;
            this.cb.Text = "间接说明";
            this.cb.UseVisualStyleBackColor = true;
            // 
            // PersonnelChecklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 495);
            this.Controls.Add(this.cb);
            this.Controls.Add(this.btnLicen);
            this.Controls.Add(this.btnTurn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbColuNameRow);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbJNSheets);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSelectJNExcel);
            this.Controls.Add(this.tbModeFile);
            this.Controls.Add(this.label5);
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
            this.Name = "PersonnelChecklist";
            this.Text = "检测+案卷拆分+合并pdf";
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbModeFile;
        private System.Windows.Forms.Button btnSelectJNExcel;
        private System.Windows.Forms.ComboBox cbJNSheets;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer UpdatePercent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbColuNameRow;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker bwgPrepareData;
        private System.Windows.Forms.Button btnTurn;
        private System.Windows.Forms.Button btnLicen;
        private System.Windows.Forms.CheckBox cb;
    }
}

