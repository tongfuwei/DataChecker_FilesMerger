namespace DataChecker_FilesMerger.Dialog_Setting
{
    partial class OneToManySetting
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
            this.JNColumns = new System.Windows.Forms.Panel();
            this.cbJN1 = new System.Windows.Forms.ComboBox();
            this.cbJN2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbJN3 = new System.Windows.Forms.ComboBox();
            this.cbAJ1 = new System.Windows.Forms.ComboBox();
            this.cbAJ2 = new System.Windows.Forms.ComboBox();
            this.cbAJ3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AJColumns.SuspendLayout();
            this.JNColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // AJColumns
            // 
            this.AJColumns.AutoSize = true;
            this.AJColumns.Controls.Add(this.cbAJ1);
            this.AJColumns.Controls.Add(this.cbAJ2);
            this.AJColumns.Controls.Add(this.cbAJ3);
            this.AJColumns.Controls.Add(this.label3);
            this.AJColumns.Dock = System.Windows.Forms.DockStyle.Left;
            this.AJColumns.Location = new System.Drawing.Point(0, 0);
            this.AJColumns.Name = "AJColumns";
            this.AJColumns.Size = new System.Drawing.Size(266, 293);
            this.AJColumns.TabIndex = 46;
            // 
            // JNColumns
            // 
            this.JNColumns.AutoSize = true;
            this.JNColumns.Controls.Add(this.cbJN1);
            this.JNColumns.Controls.Add(this.cbJN2);
            this.JNColumns.Controls.Add(this.label4);
            this.JNColumns.Controls.Add(this.cbJN3);
            this.JNColumns.Dock = System.Windows.Forms.DockStyle.Right;
            this.JNColumns.Location = new System.Drawing.Point(246, 0);
            this.JNColumns.Name = "JNColumns";
            this.JNColumns.Size = new System.Drawing.Size(266, 293);
            this.JNColumns.TabIndex = 47;
            // 
            // cbJN1
            // 
            this.cbJN1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJN1.FormattingEnabled = true;
            this.cbJN1.Location = new System.Drawing.Point(40, 37);
            this.cbJN1.Name = "cbJN1";
            this.cbJN1.Size = new System.Drawing.Size(175, 23);
            this.cbJN1.TabIndex = 40;
            // 
            // cbJN2
            // 
            this.cbJN2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJN2.FormattingEnabled = true;
            this.cbJN2.Location = new System.Drawing.Point(40, 80);
            this.cbJN2.Name = "cbJN2";
            this.cbJN2.Size = new System.Drawing.Size(175, 23);
            this.cbJN2.TabIndex = 42;
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
            // cbJN3
            // 
            this.cbJN3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJN3.FormattingEnabled = true;
            this.cbJN3.Location = new System.Drawing.Point(40, 123);
            this.cbJN3.Name = "cbJN3";
            this.cbJN3.Size = new System.Drawing.Size(175, 23);
            this.cbJN3.TabIndex = 43;
            // 
            // cbAJ1
            // 
            this.cbAJ1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAJ1.FormattingEnabled = true;
            this.cbAJ1.Location = new System.Drawing.Point(35, 37);
            this.cbAJ1.Name = "cbAJ1";
            this.cbAJ1.Size = new System.Drawing.Size(175, 23);
            this.cbAJ1.TabIndex = 37;
            // 
            // cbAJ2
            // 
            this.cbAJ2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAJ2.FormattingEnabled = true;
            this.cbAJ2.Location = new System.Drawing.Point(35, 80);
            this.cbAJ2.Name = "cbAJ2";
            this.cbAJ2.Size = new System.Drawing.Size(175, 23);
            this.cbAJ2.TabIndex = 38;
            // 
            // cbAJ3
            // 
            this.cbAJ3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAJ3.FormattingEnabled = true;
            this.cbAJ3.Location = new System.Drawing.Point(35, 123);
            this.cbAJ3.Name = "cbAJ3";
            this.cbAJ3.Size = new System.Drawing.Size(175, 23);
            this.cbAJ3.TabIndex = 39;
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
            // OneToManySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 293);
            this.Controls.Add(this.JNColumns);
            this.Controls.Add(this.AJColumns);
            this.Name = "OneToManySetting";
            this.Text = "案卷-卷内匹配";
            this.AJColumns.ResumeLayout(false);
            this.JNColumns.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel AJColumns;
        private System.Windows.Forms.ComboBox cbAJ1;
        private System.Windows.Forms.ComboBox cbAJ2;
        private System.Windows.Forms.ComboBox cbAJ3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel JNColumns;
        private System.Windows.Forms.ComboBox cbJN1;
        private System.Windows.Forms.ComboBox cbJN2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbJN3;
    }
}