using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataChecker_FilesMerger
{
	public partial class OneToOneMatchSetting : Form
	{
		private Dictionary<string, int> Column = new Dictionary<string, int>();

		/// <summary>
		/// 目录构成,名称,规定长度
		/// </summary>
		public Dictionary<string,int> dirConstitute
		{
			get;
			set;
		}

		/// <summary>
		/// 页数所在列
		/// </summary>
		public string pageColumn
		{
			get; 
			set;
		}

		/// <summary>
		/// 是否对文件夹名称应用规定长度
		/// </summary>
		public bool renameFolder
		{
			get;
			set;
		} = false;

		private Dictionary<Control, Control> dir_check = new Dictionary<Control, Control>();

		public OneToOneMatchSetting(Dictionary<string, int> _columns,Dictionary<string,int> _dirConstitute = null,string _pageColumn = null,bool _renameFolder = false)
		{
			InitializeComponent();
			Column = _columns;
			dirConstitute = _dirConstitute;
			pageColumn = _pageColumn;
			renameFolder = _renameFolder;
			InitCombox();
			InitControls();
		}

		public void InitCombox()
		{
			foreach (Control control in this.Controls)
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
			dir_check.Add(cbFolder1, checkBox1);
			dir_check.Add(cbFolder2, checkBox2);
			dir_check.Add(cbFolder3, checkBox3);
			dir_check.Add(cbFolder4, checkBox4);
		}

		/// <summary>
		/// 读取前一次的状态
		/// </summary>
		private void InitControls()
		{
			if (dirConstitute != null)
			{
				List<string> Keys = dirConstitute.Keys.ToList();
				List<int> Values = dirConstitute.Values.ToList();
				for (int i = 0; i < dirConstitute.Count; i++)
				{
					int num = i + 1;
					string comboName = "cbFolder" + num;
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
			if(pageColumn != null)
			{
				this.cbPageColumn.SelectedItem = pageColumn;
			}
			if(renameFolder != false)
			{
				foreach (Control control in (this.Controls.Find("folderRename", false)))
				{
					CheckBox checkBox = control as CheckBox;
					checkBox.Checked = true;
				}
			}
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			dirConstitute = new Dictionary<string, int>();

			foreach(ComboBox folder in dir_check.Keys)
			{
				if (folder.SelectedItem != null && !string.IsNullOrWhiteSpace(folder.SelectedItem.ToString()))
				{
					if (dirConstitute.ContainsKey(folder.SelectedItem.ToString()))
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
						dirConstitute.Add(folder.SelectedItem.ToString(), num);
					}
					else
					{
						dirConstitute.Add(folder.SelectedItem.ToString(), 0);
					}
				}
			}

			foreach (Control control in (this.Controls.Find("folderRename", false)))
			{
				CheckBox checkBox = control as CheckBox;
				if (checkBox.Checked == true)
				{
					renameFolder = true;
				}
			}

			if (this.cbPageColumn.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbPageColumn.SelectedItem.ToString()))
				pageColumn = this.cbPageColumn.SelectedItem.ToString();

			if (dirConstitute.Count == 0)
			{
				MessageBox.Show("请填写扫描件目录结构!");
				return;
			}
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if(((System.Windows.Forms.CheckBox)sender).Checked == true)
			{
				TextBox num = new TextBox();
				num.Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Num";
				num.Location = new Point(((System.Windows.Forms.CheckBox)sender).Location.X + 75, ((System.Windows.Forms.CheckBox)sender).Location.Y - 1);
				num.TextChanged += new System.EventHandler(this.Text_TextChanged);
				this.Controls.Add(num);

				Label text = new Label();
				text.Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Bit";
				text.Text = "位";
				text.Location = new Point(num.Location.X + num.Size.Width + 5, num.Location.Y + 5);
				this.Controls.Add(text);

				if(((System.Windows.Forms.CheckBox)sender).Name == checkBox1.Name)
				{
					CheckBox rename = new CheckBox();
					rename.Name = "folderRename";
					rename.Text = "重命名各级文件夹到指定长度\n!谨慎使用!小于指定长度将在左侧补0,大于指定长度将从右侧截取";
					rename.AutoSize = true;
					rename.Location = new Point(this.cbPageColumn.Location.X, this.cbPageColumn.Location.Y + this.cbPageColumn.Size.Height + 5);
					this.Controls.Add(rename);
				}
			}
			if (((System.Windows.Forms.CheckBox)sender).Checked == false)
			{
				foreach (Control control in (this.Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Num", false)))
				{
					control.Dispose();
				}
				foreach (Control control in (this.Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Bit", false)))
				{
					control.Dispose();
				}
				if (((System.Windows.Forms.CheckBox)sender).Name == checkBox1.Name)
				{
					foreach (Control control in (this.Controls.Find("folderRename", false)))
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
	}
}
