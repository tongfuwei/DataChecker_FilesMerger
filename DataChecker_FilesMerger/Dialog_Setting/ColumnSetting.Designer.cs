namespace DataChecker_FilesMerger.Dialog_Setting
{
    partial class ColumnSetting
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
            this.AJColumns = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbJNCount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAJPageCount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.JNColumns = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbJNPageCount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AJColumns.SuspendLayout();
            this.JNColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // AJColumns
            // 
            this.AJColumns.AutoSize = true;
            this.AJColumns.Controls.Add(this.label2);
            this.AJColumns.Controls.Add(this.cbAJPageCount);
            this.AJColumns.Controls.Add(this.label3);
            this.AJColumns.Dock = System.Windows.Forms.DockStyle.Left;
            this.AJColumns.Location = new System.Drawing.Point(0, 0);
            this.AJColumns.Name = "AJColumns";
            this.AJColumns.Size = new System.Drawing.Size(266, 293);
            this.AJColumns.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(51, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 15);
            this.label5.TabIndex = 51;
            this.label5.Text = "件数                ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbJNCount
            // 
            this.cbJNCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJNCount.FormattingEnabled = true;
            this.cbJNCount.Location = new System.Drawing.Point(51, 112);
            this.cbJNCount.Name = "cbJNCount";
            this.cbJNCount.Size = new System.Drawing.Size(115, 23);
            this.cbJNCount.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(66, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "页数               ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbAJPageCount
            // 
            this.cbAJPageCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAJPageCount.FormattingEnabled = true;
            this.cbAJPageCount.Location = new System.Drawing.Point(68, 56);
            this.cbAJPageCount.Name = "cbAJPageCount";
            this.cbAJPageCount.Size = new System.Drawing.Size(115, 23);
            this.cbAJPageCount.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 15);
            this.label3.TabIndex = 41;
            this.label3.Text = "案卷";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JNColumns
            // 
            this.JNColumns.AutoSize = true;
            this.JNColumns.Controls.Add(this.label5);
            this.JNColumns.Controls.Add(this.label1);
            this.JNColumns.Controls.Add(this.cbJNCount);
            this.JNColumns.Controls.Add(this.cbJNPageCount);
            this.JNColumns.Controls.Add(this.label4);
            this.JNColumns.Dock = System.Windows.Forms.DockStyle.Right;
            this.JNColumns.Location = new System.Drawing.Point(246, 0);
            this.JNColumns.Name = "JNColumns";
            this.JNColumns.Size = new System.Drawing.Size(266, 293);
            this.JNColumns.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(51, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "页数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbJNPageCount
            // 
            this.cbJNPageCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJNPageCount.FormattingEnabled = true;
            this.cbJNPageCount.Location = new System.Drawing.Point(51, 56);
            this.cbJNPageCount.Name = "cbJNPageCount";
            this.cbJNPageCount.Size = new System.Drawing.Size(120, 23);
            this.cbJNPageCount.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "卷内";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ColumnSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 293);
            this.Controls.Add(this.JNColumns);
            this.Controls.Add(this.AJColumns);
            this.Name = "ColumnSetting";
            this.Text = "列名设置";
            this.AJColumns.ResumeLayout(false);
            this.JNColumns.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel AJColumns;
        private System.Windows.Forms.Panel JNColumns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbJNCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbAJPageCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbJNPageCount;
    }
}