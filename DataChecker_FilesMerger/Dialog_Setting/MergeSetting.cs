using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
	public partial class MergeSetting : Form
	{
		private Dictionary<string, int> Column = new Dictionary<string, int>(); 
		private Dictionary<Control, Control> dir_check = new Dictionary<Control, Control>();
		public bool IsOneToMany
		{
			get;
			set;
		} = true;

		public Dictionary<string,int> PdfNameRule
		{
			get;
			set;
		}

		public MergeSetting(Dictionary<string, int> _Columns,Dictionary<string,int> _PdfNameRule = null,bool _isOneToMany = true)
		{
			Column = _Columns;
			PdfNameRule = _PdfNameRule;
			IsOneToMany = _isOneToMany;
			InitializeComponent();
			dir_check.Add(cb1, checkBox1);
			dir_check.Add(cb2, checkBox2);
			dir_check.Add(cb3, checkBox3);
			dir_check.Add(cb4, checkBox4);
			InitCombox();
			InitControls();
		}

		public void InitCombox()
		{
			foreach (Control control in Controls)
			{
				if (control is ComboBox)
				{
					ComboBox combo = control as ComboBox;
					foreach (string ColumnName in Column.Keys)
					{
						combo.Items.Add(ColumnName);
					}
					combo.Items.Insert(0, "");
				}
			}
		}

		private void InitControls()
		{
			if (PdfNameRule != null)
			{
				for (int i = 0; i < PdfNameRule.Count; i++)
				{
					List<string> Keys = PdfNameRule.Keys.ToList();
					List<int> Values = PdfNameRule.Values.ToList();
					int num = i + 1;
					string comboName = "cb" + num;
					foreach (Control control in (this.Controls.Find(comboName, false)))
					{
						ComboBox combo = control as ComboBox;
						combo.SelectedItem = Keys[i];
						if (Values[i] != 0)
						{
							foreach (Control chk in (this.Controls.Find(dir_check[control].Name, false)))
							{
								CheckBox checkBox = chk as CheckBox;
								checkBox.Checked = true;
								foreach (Control text in (this.Controls.Find(checkBox.Name + "_Num", false)))
								{
									TextBox textBox = text as TextBox;
									textBox.Text = Values[i].ToString();
								}
							}
						}

					}
				}
			}
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.CheckBox)sender).Checked == true)
			{
				TextBox num = new TextBox
				{
					Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Num",
					Location = new Point(((System.Windows.Forms.CheckBox)sender).Location.X, ((System.Windows.Forms.CheckBox)sender).Location.Y + 20),
					Size = new Size(33,25)
				};
				num.TextChanged += new System.EventHandler(Text_TextChanged);
				this.Controls.Add(num);

				Label text = new Label
				{
					Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Bit",
					Text = "位",
					Location = new Point(num.Location.X + num.Size.Width + 5, num.Location.Y + 5),
					AutoSize = true
				};
				this.Controls.Add(text);
			}
			if (((System.Windows.Forms.CheckBox)sender).Checked == false)
			{
				foreach (Control control in (Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Num", false)))
				{
					control.Dispose();
				}
				foreach (Control control in (Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Bit", false)))
				{
					control.Dispose();
				}
				if (((System.Windows.Forms.CheckBox)sender).Name == checkBox1.Name)
				{
					foreach (Control control in (Controls.Find("folderRename", false)))
					{
						control.Dispose();
					}
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

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			PdfNameRule = new Dictionary<string, int>();

			foreach (ComboBox folder in dir_check.Keys)
			{
				if (folder.SelectedItem != null && !string.IsNullOrWhiteSpace(folder.SelectedItem.ToString()))
				{
					if (PdfNameRule.ContainsKey(folder.SelectedItem.ToString()))
					{
						MessageBox.Show("请勿重复填写!");
						return;
					}
					CheckBox ckb = dir_check[folder] as CheckBox;
					if (ckb.Checked == true)
					{
						int num = 0;
						foreach (Control control in (this.Controls.Find(ckb.Name + "_Num", false)))
						{
							num = int.Parse(control.Text.Trim());
						}
						PdfNameRule.Add(folder.SelectedItem.ToString(), num);
					}
					else
					{
						PdfNameRule.Add(folder.SelectedItem.ToString(), 0);
					}
				}
			}
		}
	}
}
