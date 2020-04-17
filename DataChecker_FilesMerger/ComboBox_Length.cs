using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ComboBox;

namespace DataChecker_FilesMerger
{
    public partial class ComboBox_Length : UserControl
    {
		private bool _Level = false;
		public bool Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
			}
		}

		public ObjectCollection Items
		{
			get
			{
				return this.comboBox1.Items;
			}
			set
			{
				this.comboBox1.Items.Clear();
				foreach(var item in value)
				{
					this.comboBox1.Items.Add(item);
				}
			}
		}

		public bool Checked
		{
			get
			{
				bool result = false;
				foreach(CheckBox checkBox in this.Controls.Find(this.Name + "_Check", false))
				{
					result = checkBox.Checked;
				}
				return result;
			}
			set
			{
				foreach (CheckBox checkBox in this.Controls.Find(this.Name + "_Check", false))
				{
					checkBox.Checked = value;
				}
			}
		}

		public int? Bit
		{
			get
			{
				int? bit = null;
				foreach (TextBox textBox in this.Controls.Find(this.Name + "_Num", false))
				{
					if (!string.IsNullOrWhiteSpace(textBox.Text))
					{
						bit = int.Parse(textBox.Text.Trim());
					}
				}
				return bit;
			}
			set
			{
				foreach (TextBox textBox in this.Controls.Find(this.Name + "_Num", false))
				{
					textBox.Text = value.ToString(); 
				}
			}
		}

		public ComboBox_Length(bool isLevel = false)
        {
            InitializeComponent();
			Level = isLevel;
			InitControl();
        }

		private void InitControl()
		{
			if(Level)
			{
				CheckBox checkBox = new CheckBox
				{
					Name = this.Name + "_Check",
					AutoSize = true,
					Location = new System.Drawing.Point(this.comboBox1.Location.X + this.comboBox1.Width + 6, this.comboBox1.Location.Y + 2),
					TabIndex = 48,
					Text = "规定长度",
					UseVisualStyleBackColor = true
				};
				checkBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_level);
				this.Size = new Size(283, 31);
			}
			else
			{
				CheckBox checkBox = new CheckBox
				{
					Name = this.Name + "_Check",
					AutoSize = true,
					Location = new System.Drawing.Point(this.comboBox1.Location.X ,this.comboBox1.Location.Y +this.comboBox1.Height + 6),
					TabIndex = 48,
					Text = "规定长度",
					UseVisualStyleBackColor = true
				};
				checkBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_vertical);
				this.Size = new Size(127, 88);
			}
		}

		private void checkBox1_CheckedChanged_level(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.CheckBox)sender).Checked == true)
			{
				TextBox num = new TextBox
				{
					Name = this.Name + "_Num",
					Location = new Point(((System.Windows.Forms.CheckBox)sender).Location.X + ((System.Windows.Forms.CheckBox)sender).Size.Width + 6, ((System.Windows.Forms.CheckBox)sender).Location.Y - 2),
					Size = new Size(33, 25)
				};
				num.TextChanged += new System.EventHandler(Text_TextChanged);
				this.Controls.Add(num);

				Label text = new Label
				{
					Name = this.Name + "_Bit",
					Text = "位",
					Location = new Point(num.Location.X + num.Size.Width + 5, num.Location.Y + 5),
					AutoSize = true
				};
				this.Controls.Add(text);
			}
			if (((System.Windows.Forms.CheckBox)sender).Checked == false)
			{
				foreach (Control control in Controls.Find(this.Name + "_Num", false))
				{
					control.Dispose();
				}
				foreach (Control control in Controls.Find(this.Name + "_Bit", false))
				{
					control.Dispose();
				}
			}
		}

		private void checkBox1_CheckedChanged_vertical(object sender, EventArgs e)
        {
			if (((System.Windows.Forms.CheckBox)sender).Checked == true)
			{
				TextBox num = new TextBox
				{
					Name = this.Name + "_Num",
					Location = new Point(((System.Windows.Forms.CheckBox)sender).Location.X, ((System.Windows.Forms.CheckBox)sender).Location.Y + ((System.Windows.Forms.CheckBox)sender).Size.Height + 6),
					Size = new Size(33, 25)
				};
				num.TextChanged += new System.EventHandler(Text_TextChanged);
				this.Controls.Add(num);

				Label text = new Label
				{
					Name = this.Name + "_Bit",
					Text = "位",
					Location = new Point(num.Location.X + num.Size.Width + 5, num.Location.Y + 5),
					AutoSize = true
				};
				this.Controls.Add(text);
			}
			if (((System.Windows.Forms.CheckBox)sender).Checked == false)
			{
				foreach (Control control in Controls.Find(this.Name + "_Num", false))
				{
					control.Dispose();
				}
				foreach (Control control in Controls.Find(this.Name + "_Bit", false))
				{
					control.Dispose();
				}
			}
		}

		private void Text_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(((System.Windows.Forms.TextBox)sender).Text))
			{
				int num = 0;
				if (!int.TryParse(((System.Windows.Forms.TextBox)sender).Text, out num))
				{
					((System.Windows.Forms.TextBox)sender).Clear();
					MessageBox.Show("必须输入数字");
				}
				else if (int.Parse(((System.Windows.Forms.TextBox)sender).Text.Trim()) <= 0)
				{
					((System.Windows.Forms.TextBox)sender).Clear();
					MessageBox.Show("必须大于0");
				}
			}
		}
	}
}
