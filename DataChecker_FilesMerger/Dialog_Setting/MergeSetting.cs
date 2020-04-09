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
		private Dictionary<string, int> AJColumn = new Dictionary<string, int>();
		private Dictionary<Control, Control> dir_check = new Dictionary<Control, Control>();
		public bool IsOneToMany
		{
			get;
			set;
		} = true;

		public Dictionary<string, int> PdfNameRule
		{
			get;
			set;
		}
		public Dictionary<string, int> FolderNameRule
		{
			get;
			set;
		}

		public MergeSetting(Dictionary<string, int> _Columns, Dictionary<string, int> _PdfNameRule = null,Dictionary<string,int> _FolderNameRule = null, bool _isOneToMany = true, Dictionary<string, int> _AJColumns = null)
		{
			Column = _Columns;
			PdfNameRule = _PdfNameRule;
			IsOneToMany = _isOneToMany;
			if(IsOneToMany)
			{
				AJColumn = _AJColumns;
			}
			FolderNameRule = _FolderNameRule;
			InitializeComponent();
			dir_check.Add(cb1, checkBox1);
			dir_check.Add(cb2, checkBox2);
			dir_check.Add(cb3, checkBox3);
			dir_check.Add(cb4, checkBox4);
			dir_check.Add(cb5, checkBox5);
			dir_check.Add(cb6, checkBox6);
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
			if(FolderNameRule != null)
			{
				checkBox.Checked = true;
				List<string> Keys = FolderNameRule.Keys.ToList();
				List<int> Values = FolderNameRule.Values.ToList();
				IntFolderControl(checkBox.Name, Keys, Values);
			}
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.CheckBox)sender).Checked == true)
			{
				TextBox num = new TextBox
				{
					Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Num",
					Location = new Point(((System.Windows.Forms.CheckBox)sender).Location.X, ((System.Windows.Forms.CheckBox)sender).Location.Y + 25),
					Size = new Size(33, 25)
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
							if (!string.IsNullOrWhiteSpace(control.Text))
							{
								num = int.Parse(control.Text.Trim());
							}
						}
						PdfNameRule.Add(folder.SelectedItem.ToString(), num);
					}
					else
					{
						PdfNameRule.Add(folder.SelectedItem.ToString(), 0);
					}
				}
			}
			if(checkBox.Checked)
			{
				FolderNameRule = new Dictionary<string, int>();
				GetFolderControlValue(checkBox.Name);
			}
		}

		private void IntFolderControl(string controlName, List<string> Keys, List<int> Values, int i = 0)
		{
			if (i < Keys.Count)
			{
				foreach (Control control in this.Controls.Find(controlName + "_FolderName", false))
				{
					ComboBox combo = control as ComboBox;
					combo.SelectedItem = Keys[i];
					if (Values[i] != 0)
					{
						foreach (Control control1 in this.Controls.Find(controlName + "_Length", false))
						{
							CheckBox check = control1 as CheckBox;
							check.Checked = true;
							foreach (Control text in (this.Controls.Find(control1.Name + "_Num", false)))
							{
								TextBox textBox = text as TextBox;
								textBox.Text = Values[i].ToString();
							}
						}
					}
					i++;
					IntFolderControl(control.Name,Keys,Values, i);
				}
			}
		}

		private void GetFolderControlValue(string controlName)
		{
			foreach (Control control in this.Controls.Find(controlName + "_FolderName", false))
			{
				ComboBox combo = control as ComboBox;
				if (combo.SelectedItem != null && !string.IsNullOrWhiteSpace(combo.SelectedItem.ToString()))
				{
					if (FolderNameRule.ContainsKey(combo.SelectedItem.ToString()))
					{
						MessageBox.Show("请勿重复填写!");
						return;
					}
					foreach (Control control1 in this.Controls.Find(controlName + "_Length", false))
					{
						CheckBox check = control1 as CheckBox;
						int num = 0;
						if (check.Checked)
						{
							foreach (Control control2 in (this.Controls.Find(control1.Name + "_Num", false)))
							{
								if (!string.IsNullOrWhiteSpace(control2.Text))
								{
									num = int.Parse(control2.Text.Trim());
								}
							}
						}
						FolderNameRule.Add(combo.SelectedItem.ToString(), num);
					}
				}
				GetFolderControlValue(control.Name);
			}
		}

		private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.CheckBox)sender).Checked == true)
			{
				ComboBox comboBox = new ComboBox
				{
					Name = ((System.Windows.Forms.CheckBox)sender).Name + "_FolderName",
					Location = new Point(((System.Windows.Forms.CheckBox)sender).Location.X, ((System.Windows.Forms.CheckBox)sender).Location.Y + 20),
					FormattingEnabled = true,
					Width = 80
				};
				if (IsOneToMany)
				{
					foreach (string ColumnName in AJColumn.Keys)
					{
						comboBox.Items.Add(ColumnName);
					}
				}
				else
				{
					foreach (string ColumnName in Column.Keys)
					{
						comboBox.Items.Add(ColumnName);
					}
				}
				comboBox.Items.Insert(0, "");
				comboBox.SelectedIndexChanged += new System.EventHandler(AddComboBox);
				this.Controls.Add(comboBox);

				CheckBox checkBox = new CheckBox
				{
					Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Length",
					Location = new Point(comboBox.Location.X, comboBox.Location.Y + 20),
					Text = "规定长度"
				};
				checkBox.CheckedChanged += new System.EventHandler(checkBox_CheckedChanged);
				this.Controls.Add(checkBox);
			}
			else
			{
				foreach (Control control in (Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_FolderName", false)))
				{
					control.Dispose();
				}
				foreach (Control control in (Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Length", false)))
				{
					control.Dispose();
				}
			}
		}

		private void AddComboBox(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.ComboBox)sender).SelectedIndex != 0)
			{
				ComboBox comboBox = new ComboBox
				{
					Name = ((System.Windows.Forms.ComboBox)sender).Name + "_FolderName",
					Location = new Point(((System.Windows.Forms.ComboBox)sender).Location.X + ((System.Windows.Forms.ComboBox)sender).Size.Width + 5, ((System.Windows.Forms.ComboBox)sender).Location.Y),
					FormattingEnabled = true,
					Width = 80
				};
				if (IsOneToMany)
				{
					foreach (string ColumnName in AJColumn.Keys)
					{
						comboBox.Items.Add(ColumnName);
					}
				}
				else
				{
					foreach (string ColumnName in Column.Keys)
					{
						comboBox.Items.Add(ColumnName);
					}
				}
				comboBox.Items.Insert(0, "");
				comboBox.SelectedIndexChanged += new System.EventHandler(AddComboBox);
				this.Controls.Add(comboBox);

				CheckBox checkBox = new CheckBox
				{
					Name = ((System.Windows.Forms.ComboBox)sender).Name + "_Length",
					Location = new Point(comboBox.Location.X, comboBox.Location.Y + 20),
					Text = "规定长度"
				};
				checkBox.CheckedChanged += new System.EventHandler(checkBox_CheckedChanged);
				this.Controls.Add(checkBox);
			}
			else
			{
				foreach (Control control in (Controls.Find(((System.Windows.Forms.ComboBox)sender).Name + "_FolderName", false)))
				{
					control.Dispose();
				}
				foreach (Control control in (Controls.Find(((System.Windows.Forms.ComboBox)sender).Name + "_Length", false)))
				{
					control.Dispose();
				}
			}
		}
	}
}
