namespace DataChecker_FilesMerger.Dialog_Setting
{
    partial class DemergeSetting
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cbColumn1 = new System.Windows.Forms.ComboBox();
            this.cbColumn3 = new System.Windows.Forms.ComboBox();
            this.cbColumn2 = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRow11 = new System.Windows.Forms.ComboBox();
            this.cbRow10 = new System.Windows.Forms.ComboBox();
            this.cbRow12 = new System.Windows.Forms.ComboBox();
            this.cbRow8 = new System.Windows.Forms.ComboBox();
            this.cbRow7 = new System.Windows.Forms.ComboBox();
            this.cbRow9 = new System.Windows.Forms.ComboBox();
            this.cbRow5 = new System.Windows.Forms.ComboBox();
            this.cbRow4 = new System.Windows.Forms.ComboBox();
            this.cbRow6 = new System.Windows.Forms.ComboBox();
            this.cbRow2 = new System.Windows.Forms.ComboBox();
            this.cbRow1 = new System.Windows.Forms.ComboBox();
            this.cbRow3 = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(304, 399);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(82, 25);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(399, 393);
            this.tabControl1.TabIndex = 106;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cbColumn1);
            this.tabPage1.Controls.Add(this.cbColumn3);
            this.tabPage1.Controls.Add(this.cbColumn2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(391, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "拆分后保存为列";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "可以为空,为空时仅添加基本列\n序号|题名|页数|页号";
            // 
            // cbColumn1
            // 
            this.cbColumn1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn1.FormattingEnabled = true;
            this.cbColumn1.Location = new System.Drawing.Point(8, 6);
            this.cbColumn1.Name = "cbColumn1";
            this.cbColumn1.Size = new System.Drawing.Size(121, 23);
            this.cbColumn1.TabIndex = 0;
            // 
            // cbColumn3
            // 
            this.cbColumn3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn3.FormattingEnabled = true;
            this.cbColumn3.Location = new System.Drawing.Point(262, 6);
            this.cbColumn3.Name = "cbColumn3";
            this.cbColumn3.Size = new System.Drawing.Size(121, 23);
            this.cbColumn3.TabIndex = 2;
            // 
            // cbColumn2
            // 
            this.cbColumn2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn2.FormattingEnabled = true;
            this.cbColumn2.Location = new System.Drawing.Point(135, 6);
            this.cbColumn2.Name = "cbColumn2";
            this.cbColumn2.Size = new System.Drawing.Size(121, 23);
            this.cbColumn2.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbRow11);
            this.tabPage2.Controls.Add(this.cbRow10);
            this.tabPage2.Controls.Add(this.cbRow12);
            this.tabPage2.Controls.Add(this.cbRow8);
            this.tabPage2.Controls.Add(this.cbRow7);
            this.tabPage2.Controls.Add(this.cbRow9);
            this.tabPage2.Controls.Add(this.cbRow5);
            this.tabPage2.Controls.Add(this.cbRow4);
            this.tabPage2.Controls.Add(this.cbRow6);
            this.tabPage2.Controls.Add(this.cbRow2);
            this.tabPage2.Controls.Add(this.cbRow1);
            this.tabPage2.Controls.Add(this.cbRow3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(391, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "拆分后保存为行";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(246, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 40);
            this.button1.TabIndex = 114;
            this.button1.Text = "根据选择的第一项\n自动填充";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 30);
            this.label2.TabIndex = 113;
            this.label2.Text = "至少选择一项\n选中的项将作为题名";
            // 
            // cbRow11
            // 
            this.cbRow11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow11.FormattingEnabled = true;
            this.cbRow11.Location = new System.Drawing.Point(8, 296);
            this.cbRow11.Name = "cbRow11";
            this.cbRow11.Size = new System.Drawing.Size(121, 23);
            this.cbRow11.TabIndex = 111;
            // 
            // cbRow10
            // 
            this.cbRow10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow10.FormattingEnabled = true;
            this.cbRow10.Location = new System.Drawing.Point(8, 267);
            this.cbRow10.Name = "cbRow10";
            this.cbRow10.Size = new System.Drawing.Size(121, 23);
            this.cbRow10.TabIndex = 110;
            // 
            // cbRow12
            // 
            this.cbRow12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow12.FormattingEnabled = true;
            this.cbRow12.Location = new System.Drawing.Point(8, 325);
            this.cbRow12.Name = "cbRow12";
            this.cbRow12.Size = new System.Drawing.Size(121, 23);
            this.cbRow12.TabIndex = 112;
            // 
            // cbRow8
            // 
            this.cbRow8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow8.FormattingEnabled = true;
            this.cbRow8.Location = new System.Drawing.Point(8, 209);
            this.cbRow8.Name = "cbRow8";
            this.cbRow8.Size = new System.Drawing.Size(121, 23);
            this.cbRow8.TabIndex = 108;
            // 
            // cbRow7
            // 
            this.cbRow7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow7.FormattingEnabled = true;
            this.cbRow7.Location = new System.Drawing.Point(8, 180);
            this.cbRow7.Name = "cbRow7";
            this.cbRow7.Size = new System.Drawing.Size(121, 23);
            this.cbRow7.TabIndex = 107;
            // 
            // cbRow9
            // 
            this.cbRow9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow9.FormattingEnabled = true;
            this.cbRow9.Location = new System.Drawing.Point(8, 238);
            this.cbRow9.Name = "cbRow9";
            this.cbRow9.Size = new System.Drawing.Size(121, 23);
            this.cbRow9.TabIndex = 109;
            // 
            // cbRow5
            // 
            this.cbRow5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow5.FormattingEnabled = true;
            this.cbRow5.Location = new System.Drawing.Point(8, 122);
            this.cbRow5.Name = "cbRow5";
            this.cbRow5.Size = new System.Drawing.Size(121, 23);
            this.cbRow5.TabIndex = 105;
            // 
            // cbRow4
            // 
            this.cbRow4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow4.FormattingEnabled = true;
            this.cbRow4.Location = new System.Drawing.Point(8, 93);
            this.cbRow4.Name = "cbRow4";
            this.cbRow4.Size = new System.Drawing.Size(121, 23);
            this.cbRow4.TabIndex = 104;
            // 
            // cbRow6
            // 
            this.cbRow6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow6.FormattingEnabled = true;
            this.cbRow6.Location = new System.Drawing.Point(8, 151);
            this.cbRow6.Name = "cbRow6";
            this.cbRow6.Size = new System.Drawing.Size(121, 23);
            this.cbRow6.TabIndex = 106;
            // 
            // cbRow2
            // 
            this.cbRow2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow2.FormattingEnabled = true;
            this.cbRow2.Location = new System.Drawing.Point(8, 35);
            this.cbRow2.Name = "cbRow2";
            this.cbRow2.Size = new System.Drawing.Size(121, 23);
            this.cbRow2.TabIndex = 102;
            // 
            // cbRow1
            // 
            this.cbRow1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow1.FormattingEnabled = true;
            this.cbRow1.Location = new System.Drawing.Point(8, 6);
            this.cbRow1.Name = "cbRow1";
            this.cbRow1.Size = new System.Drawing.Size(121, 23);
            this.cbRow1.TabIndex = 101;
            // 
            // cbRow3
            // 
            this.cbRow3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRow3.FormattingEnabled = true;
            this.cbRow3.Location = new System.Drawing.Point(8, 64);
            this.cbRow3.Name = "cbRow3";
            this.cbRow3.Size = new System.Drawing.Size(121, 23);
            this.cbRow3.TabIndex = 103;
            // 
            // DemergeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 436);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnConfirm);
            this.Name = "DemergeSetting";
            this.Text = "传统案卷拆分设置";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbColumn1;
        private System.Windows.Forms.ComboBox cbColumn3;
        private System.Windows.Forms.ComboBox cbColumn2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbRow2;
        private System.Windows.Forms.ComboBox cbRow1;
        private System.Windows.Forms.ComboBox cbRow3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRow11;
        private System.Windows.Forms.ComboBox cbRow10;
        private System.Windows.Forms.ComboBox cbRow12;
        private System.Windows.Forms.ComboBox cbRow8;
        private System.Windows.Forms.ComboBox cbRow7;
        private System.Windows.Forms.ComboBox cbRow9;
        private System.Windows.Forms.ComboBox cbRow5;
        private System.Windows.Forms.ComboBox cbRow4;
        private System.Windows.Forms.ComboBox cbRow6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}