namespace DataChecker_FilesMerger.Dialog_Setting
{
    partial class FolderSetting
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
            this.cbFolder1 = new System.Windows.Forms.ComboBox();
            this.cbFolder2 = new System.Windows.Forms.ComboBox();
            this.cbFolder3 = new System.Windows.Forms.ComboBox();
            this.cbFolder4 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.cbRename = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbFolder1
            // 
            this.cbFolder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFolder1.FormattingEnabled = true;
            this.cbFolder1.Location = new System.Drawing.Point(12, 27);
            this.cbFolder1.Name = "cbFolder1";
            this.cbFolder1.Size = new System.Drawing.Size(121, 23);
            this.cbFolder1.TabIndex = 0;
            // 
            // cbFolder2
            // 
            this.cbFolder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFolder2.FormattingEnabled = true;
            this.cbFolder2.Location = new System.Drawing.Point(12, 56);
            this.cbFolder2.Name = "cbFolder2";
            this.cbFolder2.Size = new System.Drawing.Size(121, 23);
            this.cbFolder2.TabIndex = 1;
            // 
            // cbFolder3
            // 
            this.cbFolder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFolder3.FormattingEnabled = true;
            this.cbFolder3.Location = new System.Drawing.Point(12, 85);
            this.cbFolder3.Name = "cbFolder3";
            this.cbFolder3.Size = new System.Drawing.Size(121, 23);
            this.cbFolder3.TabIndex = 2;
            // 
            // cbFolder4
            // 
            this.cbFolder4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFolder4.FormattingEnabled = true;
            this.cbFolder4.Location = new System.Drawing.Point(12, 114);
            this.cbFolder4.Name = "cbFolder4";
            this.cbFolder4.Size = new System.Drawing.Size(121, 23);
            this.cbFolder4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 37;
            this.label1.Text = "文件夹结构";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(139, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 19);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.Text = "规定长度";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(139, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(89, 19);
            this.checkBox2.TabIndex = 40;
            this.checkBox2.Text = "规定长度";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(139, 87);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(89, 19);
            this.checkBox3.TabIndex = 41;
            this.checkBox3.Text = "规定长度";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(139, 116);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(89, 19);
            this.checkBox4.TabIndex = 42;
            this.checkBox4.Text = "规定长度";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // cbRename
            // 
            this.cbRename.AutoSize = true;
            this.cbRename.Enabled = false;
            this.cbRename.Location = new System.Drawing.Point(12, 163);
            this.cbRename.Name = "cbRename";
            this.cbRename.Size = new System.Drawing.Size(466, 34);
            this.cbRename.TabIndex = 43;
            this.cbRename.Text = "重命名各级文件夹到指定长度\n!谨慎使用!小于指定长度将在左侧补0,大于指定长度将从右侧截取";
            this.cbRename.UseVisualStyleBackColor = true;
            // 
            // FolderSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 293);
            this.Controls.Add(this.cbRename);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFolder4);
            this.Controls.Add(this.cbFolder3);
            this.Controls.Add(this.cbFolder2);
            this.Controls.Add(this.cbFolder1);
            this.Name = "FolderSetting";
            this.Text = "文件夹设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFolder1;
        private System.Windows.Forms.ComboBox cbFolder2;
        private System.Windows.Forms.ComboBox cbFolder3;
        private System.Windows.Forms.ComboBox cbFolder4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox cbRename;
    }
}