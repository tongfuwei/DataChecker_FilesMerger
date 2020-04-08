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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tbAJFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectAJExcel = new System.Windows.Forms.Button();
            this.cbAJSheets = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbFilesPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView_Error = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMatchSetting = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.btnSplitSetting = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnMergeSetting = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbColuNameRow = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnExportError = new System.Windows.Forms.Button();
            this.lblPercent = new System.Windows.Forms.Label();
            this.prepareAJ = new System.ComponentModel.BackgroundWorker();
            this.match = new System.ComponentModel.BackgroundWorker();
            this.renameFolder = new System.ComponentModel.BackgroundWorker();
            this.demergeExcel = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.tbJNFile = new System.Windows.Forms.TextBox();
            this.btnSelectJNExcel = new System.Windows.Forms.Button();
            this.rbOneToMany = new System.Windows.Forms.RadioButton();
            this.rbOneToOne = new System.Windows.Forms.RadioButton();
            this.cbJNSheets = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mergeFile = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // tbAJFile
            // 
            this.tbAJFile.Location = new System.Drawing.Point(94, 11);
            this.tbAJFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbAJFile.Name = "tbAJFile";
            this.tbAJFile.Size = new System.Drawing.Size(489, 25);
            this.tbAJFile.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "选择案卷:";
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
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "*.jpg",
            "*.tif",
            "*.png"});
            this.comboBox1.Location = new System.Drawing.Point(760, 72);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 23);
            this.comboBox1.TabIndex = 29;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(677, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "图片格式:";
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
            this.listView_Error.Location = new System.Drawing.Point(11, 131);
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
            // btnMatchSetting
            // 
            this.btnMatchSetting.Location = new System.Drawing.Point(324, 99);
            this.btnMatchSetting.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMatchSetting.Name = "btnMatchSetting";
            this.btnMatchSetting.Size = new System.Drawing.Size(82, 25);
            this.btnMatchSetting.TabIndex = 31;
            this.btnMatchSetting.Text = "检测设置";
            this.btnMatchSetting.UseVisualStyleBackColor = true;
            this.btnMatchSetting.Click += new System.EventHandler(this.btnMatchSetting_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(414, 99);
            this.btnMatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(82, 25);
            this.btnMatch.TabIndex = 32;
            this.btnMatch.Text = "检测";
            this.btnMatch.UseVisualStyleBackColor = true;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // btnSplitSetting
            // 
            this.btnSplitSetting.Location = new System.Drawing.Point(504, 99);
            this.btnSplitSetting.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSplitSetting.Name = "btnSplitSetting";
            this.btnSplitSetting.Size = new System.Drawing.Size(82, 25);
            this.btnSplitSetting.TabIndex = 33;
            this.btnSplitSetting.Text = "拆分设置";
            this.btnSplitSetting.UseVisualStyleBackColor = true;
            this.btnSplitSetting.Click += new System.EventHandler(this.btnSplitSetting_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(594, 99);
            this.btnSplit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(82, 25);
            this.btnSplit.TabIndex = 34;
            this.btnSplit.Text = "拆分";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnMergeSetting
            // 
            this.btnMergeSetting.Location = new System.Drawing.Point(684, 99);
            this.btnMergeSetting.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMergeSetting.Name = "btnMergeSetting";
            this.btnMergeSetting.Size = new System.Drawing.Size(82, 25);
            this.btnMergeSetting.TabIndex = 35;
            this.btnMergeSetting.Text = "合并设置";
            this.btnMergeSetting.UseVisualStyleBackColor = true;
            this.btnMergeSetting.Click += new System.EventHandler(this.btnMergeSetting_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(774, 99);
            this.btnMerge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(82, 25);
            this.btnMerge.TabIndex = 36;
            this.btnMerge.Text = "合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 37;
            this.label4.Text = "列名行号:";
            // 
            // tbColuNameRow
            // 
            this.tbColuNameRow.Location = new System.Drawing.Point(283, 99);
            this.tbColuNameRow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbColuNameRow.Name = "tbColuNameRow";
            this.tbColuNameRow.Size = new System.Drawing.Size(33, 25);
            this.tbColuNameRow.TabIndex = 38;
            this.tbColuNameRow.TextChanged += new System.EventHandler(this.tbColuNameRow_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 415);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(845, 23);
            this.progressBar1.TabIndex = 39;
            // 
            // btnExportError
            // 
            this.btnExportError.Location = new System.Drawing.Point(774, 384);
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
            this.lblPercent.Location = new System.Drawing.Point(12, 397);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(67, 15);
            this.lblPercent.TabIndex = 41;
            this.lblPercent.Text = "工作进度";
            // 
            // prepareAJ
            // 
            this.prepareAJ.WorkerReportsProgress = true;
            this.prepareAJ.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PrepareAJ_DoWork);
            this.prepareAJ.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Prepare_ProgressChanged);
            this.prepareAJ.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Prepare_RunWorkerCompleted);
            // 
            // match
            // 
            this.match.WorkerReportsProgress = true;
            this.match.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Match_RunWorkerCompleted);
            // 
            // renameFolder
            // 
            this.renameFolder.WorkerReportsProgress = true;
            this.renameFolder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.renameFolder_DoWork);
            this.renameFolder.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Prepare_ProgressChanged);
            this.renameFolder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.renameFolder_RunWorkerCompleted);
            // 
            // demergeExcel
            // 
            this.demergeExcel.WorkerReportsProgress = true;
            this.demergeExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.demergeExcel_DoWork);
            this.demergeExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Prepare_ProgressChanged);
            this.demergeExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.demergeExcel_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "选择卷内:";
            // 
            // tbJNFile
            // 
            this.tbJNFile.Location = new System.Drawing.Point(94, 40);
            this.tbJNFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbJNFile.Name = "tbJNFile";
            this.tbJNFile.Size = new System.Drawing.Size(489, 25);
            this.tbJNFile.TabIndex = 44;
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
            // rbOneToMany
            // 
            this.rbOneToMany.AutoSize = true;
            this.rbOneToMany.Checked = true;
            this.rbOneToMany.Location = new System.Drawing.Point(11, 102);
            this.rbOneToMany.Name = "rbOneToMany";
            this.rbOneToMany.Size = new System.Drawing.Size(88, 19);
            this.rbOneToMany.TabIndex = 46;
            this.rbOneToMany.TabStop = true;
            this.rbOneToMany.Text = "传统案卷";
            this.rbOneToMany.UseVisualStyleBackColor = true;
            this.rbOneToMany.CheckedChanged += new System.EventHandler(this.rbOneToMany_CheckedChanged);
            // 
            // rbOneToOne
            // 
            this.rbOneToOne.AutoSize = true;
            this.rbOneToOne.Location = new System.Drawing.Point(105, 102);
            this.rbOneToOne.Name = "rbOneToOne";
            this.rbOneToOne.Size = new System.Drawing.Size(88, 19);
            this.rbOneToOne.TabIndex = 47;
            this.rbOneToOne.TabStop = true;
            this.rbOneToOne.Text = "一文一件";
            this.rbOneToOne.UseVisualStyleBackColor = true;
            // 
            // cbJNSheets
            // 
            this.cbJNSheets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.label7.Location = new System.Drawing.Point(677, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "工作簿:";
            // 
            // mergeFile
            // 
            this.mergeFile.WorkerReportsProgress = true;
            this.mergeFile.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mergeFile_ProgressChanged);
            this.mergeFile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mergeFile_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 448);
            this.Controls.Add(this.cbJNSheets);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rbOneToOne);
            this.Controls.Add(this.rbOneToMany);
            this.Controls.Add(this.btnSelectJNExcel);
            this.Controls.Add(this.tbJNFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.btnExportError);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tbColuNameRow);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnMergeSetting);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.btnSplitSetting);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.btnMatchSetting);
            this.Controls.Add(this.listView_Error);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.tbFilesPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbAJSheets);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSelectAJExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAJFile);
            this.Name = "MainForm";
            this.Text = "检测+案卷拆分+合并pdf";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAJFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectAJExcel;
        private System.Windows.Forms.ComboBox cbAJSheets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbFilesPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView_Error;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader column1;
        private System.Windows.Forms.ColumnHeader column2;
        private System.Windows.Forms.ColumnHeader column3;
        private System.Windows.Forms.Button btnMatchSetting;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.Button btnSplitSetting;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Button btnMergeSetting;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbColuNameRow;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnExportError;
        private System.Windows.Forms.Label lblPercent;
        private System.ComponentModel.BackgroundWorker prepareAJ;
        private System.ComponentModel.BackgroundWorker match;
        private System.ComponentModel.BackgroundWorker renameFolder;
        private System.ComponentModel.BackgroundWorker demergeExcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbJNFile;
        private System.Windows.Forms.Button btnSelectJNExcel;
        private System.Windows.Forms.RadioButton rbOneToMany;
        private System.Windows.Forms.RadioButton rbOneToOne;
        private System.Windows.Forms.ComboBox cbJNSheets;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker mergeFile;
    }
}

