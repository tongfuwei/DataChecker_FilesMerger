namespace DataChecker_FilesMerger.Dialog_Setting
{
    partial class MergeSetting
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
            this.cb1 = new System.Windows.Forms.ComboBox();
            this.cb2 = new System.Windows.Forms.ComboBox();
            this.cb3 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.cb4 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cb1
            // 
            this.cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb1.FormattingEnabled = true;
            this.cb1.Location = new System.Drawing.Point(12, 36);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(153, 23);
            this.cb1.TabIndex = 44;
            // 
            // cb2
            // 
            this.cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb2.FormattingEnabled = true;
            this.cb2.Location = new System.Drawing.Point(171, 36);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(153, 23);
            this.cb2.TabIndex = 45;
            // 
            // cb3
            // 
            this.cb3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb3.FormattingEnabled = true;
            this.cb3.Location = new System.Drawing.Point(330, 36);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(153, 23);
            this.cb3.TabIndex = 46;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 65);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 19);
            this.checkBox1.TabIndex = 47;
            this.checkBox1.Text = "规定长度";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(171, 65);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(89, 19);
            this.checkBox2.TabIndex = 48;
            this.checkBox2.Text = "规定长度";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(330, 65);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(89, 19);
            this.checkBox3.TabIndex = 49;
            this.checkBox3.Text = "规定长度";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(359, 213);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(82, 25);
            this.btnConfirm.TabIndex = 50;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(489, 65);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(89, 19);
            this.checkBox4.TabIndex = 52;
            this.checkBox4.Text = "规定长度";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // cb4
            // 
            this.cb4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb4.FormattingEnabled = true;
            this.cb4.Location = new System.Drawing.Point(489, 36);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(153, 23);
            this.cb4.TabIndex = 51;
            // 
            // OneToManyMergeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.cb4);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.cb2);
            this.Controls.Add(this.cb3);
            this.Name = "OneToManyMergeSetting";
            this.Text = "图片合并设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb1;
        private System.Windows.Forms.ComboBox cb2;
        private System.Windows.Forms.ComboBox cb3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.ComboBox cb4;
    }
}