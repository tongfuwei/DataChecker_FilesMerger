namespace DataChecker_FilesMerger.Dialog_Setting
{
    partial class MergeFolder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSaveAlone = new System.Windows.Forms.CheckBox();
            this.tbPDFSavePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDefault = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbSaveAlone
            // 
            this.cbSaveAlone.AutoSize = true;
            this.cbSaveAlone.Location = new System.Drawing.Point(12, 50);
            this.cbSaveAlone.Name = "cbSaveAlone";
            this.cbSaveAlone.Size = new System.Drawing.Size(149, 19);
            this.cbSaveAlone.TabIndex = 0;
            this.cbSaveAlone.Text = "独立保存每个案卷";
            this.cbSaveAlone.UseVisualStyleBackColor = true;
            this.cbSaveAlone.CheckedChanged += new System.EventHandler(this.cbSaveAlone_CheckedChanged);
            // 
            // tbPDFSavePath
            // 
            this.tbPDFSavePath.Location = new System.Drawing.Point(85, 12);
            this.tbPDFSavePath.Name = "tbPDFSavePath";
            this.tbPDFSavePath.Size = new System.Drawing.Size(334, 25);
            this.tbPDFSavePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存路径";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(425, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 3;
            this.button1.Text = "查看";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "默认符号";
            // 
            // tbDefault
            // 
            this.tbDefault.Location = new System.Drawing.Point(454, 44);
            this.tbDefault.MaxLength = 1;
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.Size = new System.Drawing.Size(28, 25);
            this.tbDefault.TabIndex = 5;
            this.tbDefault.Text = "-";
            this.tbDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDefault.TextChanged += new System.EventHandler(this.tbDefault_TextChanged);
            // 
            // MergeFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(512, 293);
            this.Controls.Add(this.tbDefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPDFSavePath);
            this.Controls.Add(this.cbSaveAlone);
            this.Name = "MergeFolder";
            this.Text = "文件夹设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSaveAlone;
        private System.Windows.Forms.TextBox tbPDFSavePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDefault;
    }
}