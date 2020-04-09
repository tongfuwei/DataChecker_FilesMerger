using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
	public partial class OneToManyMatchSetting : Form
	{
		private Dictionary<string, int> AJColumn = new Dictionary<string, int>();
		private Dictionary<string, int> JNColumn = new Dictionary<string, int>();
		/// <summary>
		/// 目录构成,名称,规定长度
		/// </summary>
		public Dictionary<string, int> dirConstitute
		{
			get;
			set;
		}

		/// <summary>
		/// 页数所在列
		/// </summary>
		public string AJPageCount
		{
			get;
			set;
		}
		/// <summary>
		/// 页数所在列
		/// </summary>
		public string JNPageCount
		{
			get;
			set;
		}
		/// <summary>
		/// 件数
		/// </summary>
		public string JNCount
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

		/// <summary>
		/// 案卷卷内对应规则
		/// </summary>
		public Dictionary<string, string> AJ_JN
		{
			get;
			set;
		}

		private Dictionary<Control, Control> dir_check = new Dictionary<Control, Control>();
		private Dictionary<Control, Control> cbAJ_cbJN = new Dictionary<Control, Control>();

		public OneToManyMatchSetting(Dictionary<string, int> _AJColumns, Dictionary<string, int> _JNColumns, Dictionary<string, int> _dirConstitute = null, Dictionary<string, string> _AJ_JN = null, string _AJPageCount = null, string _JNPageCount = null,string _JNCount = null, bool _renameFolder = false)
		{
			InitializeComponent();
			AJColumn = _AJColumns;
			JNColumn = _JNColumns;
			dirConstitute = _dirConstitute;
			AJPageCount = _AJPageCount;
			JNPageCount = _JNPageCount;
			JNCount = _JNCount;
			renameFolder = _renameFolder;
			AJ_JN = _AJ_JN;
			dir_check.Add(cbFolder1, checkBox1);
			dir_check.Add(cbFolder2, checkBox2);
			dir_check.Add(cbFolder3, checkBox3);
			dir_check.Add(cbFolder4, checkBox4);
			cbAJ_cbJN.Add(cbAJ1, cbJN1);
			cbAJ_cbJN.Add(cbAJ2, cbJN2);
			cbAJ_cbJN.Add(cbAJ3, cbJN3);
			InitCombox();
			InitControls();
		}

		public void InitCombox()
		{
			foreach (Control control in tabPage1.Controls)
			{
				if (control is ComboBox)
				{
					ComboBox combo = control as ComboBox;
					foreach (string ColumnName in AJColumn.Keys)
					{
						combo.Items.Add(ColumnName);
					}
					combo.Items.Insert(0, "");
				}
			}

			foreach (Control control in tabPage2.Controls)
			{
				if (control is Panel)
				{
					if (control.Name == "AJColumns")
					{
						foreach (Control con in control.Controls)
						{
							if (con is ComboBox)
							{
								ComboBox combo = con as ComboBox;
								foreach (string ColumnName in AJColumn.Keys)
								{
									combo.Items.Add(ColumnName);
								}
								combo.Items.Insert(0, "");
							}
						}
					}
					if (control.Name == "JNColumns")
					{
						foreach (Control con in control.Controls)
						{
							if (con is ComboBox)
							{
								ComboBox combo = con as ComboBox;
								foreach (string ColumnName in JNColumn.Keys)
								{
									combo.Items.Add(ColumnName);
								}
								combo.Items.Insert(0, "");
							}
						}
					}
				}
			}
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
					foreach (Control control in (tabPage1.Controls.Find(comboName, false)))
					{
						ComboBox combo = control as ComboBox;
						combo.SelectedItem = Keys[i];
						if (Values[i] != 0)
						{
							foreach (Control chk in (tabPage1.Controls.Find(dir_check[control].Name, false)))
							{
								CheckBox checkBox = chk as CheckBox;
								checkBox.Checked = true;
								foreach (Control text in (tabPage1.Controls.Find(checkBox.Name + "_Num", false)))
								{
									TextBox textBox = text as TextBox;
									textBox.Text = Values[i].ToString();
								}
							}
						}

					}
				}
			}

			if(AJ_JN != null)
			{
				List<string> AJ = AJ_JN.Keys.ToList();
				for (int i = 0; i < AJ_JN.Count; i++)
				{
					int num = i + 1;
					string comboName = "cbAJ" + num;
					foreach (Control control in (AJColumns.Controls.Find(comboName, false)))
					{
						ComboBox combo = control as ComboBox;
						combo.SelectedItem = AJ[i];
					}
				}
				List<string> JN = AJ_JN.Values.ToList();
				for (int i = 0; i < AJ_JN.Count; i++)
				{
					int num = i + 1;
					string comboName = "cbJN" + num;
					foreach (Control control in (JNColumns.Controls.Find(comboName, false)))
					{
						ComboBox combo = control as ComboBox;
						combo.SelectedItem = JN[i];
					}
				}
			}

			if (renameFolder != false)
			{
				foreach (Control control in (tabPage1.Controls.Find("folderRename", false)))
				{
					CheckBox checkBox = control as CheckBox;
					checkBox.Checked = true;
				}
			}
			if (AJPageCount != null)
			{
				cbAJPageCount.SelectedItem = AJPageCount;
			}
			if (JNPageCount != null)
			{
				cbJNPageCount.SelectedItem = JNPageCount;
			}
			if (JNCount != null)
			{
				cbJNPageCount.SelectedItem = JNPageCount;
			}
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			dirConstitute = new Dictionary<string, int>();
			AJ_JN = new Dictionary<string, string>();

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
						foreach (Control control in (tabPage1.Controls.Find(ckb.Name + "_Num", false)))
						{
							if (!string.IsNullOrWhiteSpace(control.Text))
							{
								num = int.Parse(control.Text.Trim());
							}
						}
						dirConstitute.Add(folder.SelectedItem.ToString(), num);
					}
					else
					{
						dirConstitute.Add(folder.SelectedItem.ToString(), 0);
					}
				}
			}

			foreach (ComboBox cbAJ in cbAJ_cbJN.Keys)
			{
				if (cbAJ.SelectedItem != null && !string.IsNullOrWhiteSpace(cbAJ.SelectedItem.ToString()))
				{
					if (AJ_JN.ContainsKey(cbAJ.SelectedItem.ToString()))
					{
						MessageBox.Show("请勿重复填写!");
						return;
					}
					ComboBox cbJN = cbAJ_cbJN[cbAJ] as ComboBox;
					if(cbJN.SelectedItem != null && !string.IsNullOrWhiteSpace(cbJN.SelectedItem.ToString()))
					{
						AJ_JN.Add(cbAJ.SelectedItem.ToString(), cbJN.SelectedItem.ToString());
					}
					else
					{
						MessageBox.Show("请成对填写!");
					}
				}
			}

			foreach (Control control in (tabPage1.Controls.Find("folderRename", false)))
			{
				CheckBox checkBox = control as CheckBox;
				if (checkBox.Checked == true)
				{
					renameFolder = true;
				}
			}

			if (this.cbAJPageCount.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbAJPageCount.SelectedItem.ToString()))
				AJPageCount = this.cbAJPageCount.SelectedItem.ToString();
			if (this.cbJNPageCount.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbJNPageCount.SelectedItem.ToString()))
				JNPageCount = this.cbJNPageCount.SelectedItem.ToString();
			if (this.cbJNCount.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbJNCount.SelectedItem.ToString()))
				JNCount = this.cbJNCount.SelectedItem.ToString();
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
				tabPage1.Controls.Add(num);

				Label text = new Label();
				text.Name = ((System.Windows.Forms.CheckBox)sender).Name + "_Bit";
				text.Text = "位";
				text.Location = new Point(num.Location.X + num.Size.Width + 5, num.Location.Y + 5);
				tabPage1.Controls.Add(text);

				if(((System.Windows.Forms.CheckBox)sender).Name == checkBox1.Name)
				{
					CheckBox rename = new CheckBox();
					rename.Name = "folderRename";
					rename.Text = "重命名各级文件夹到指定长度\n!谨慎使用!小于指定长度将在左侧补0,大于指定长度将从右侧截取";
					rename.AutoSize = true;
					rename.Location = new Point(cbFolder4.Location.X, cbFolder4.Location.Y + cbFolder4.Size.Height + 5);
					tabPage1.Controls.Add(rename);
				}
			}
			if (((System.Windows.Forms.CheckBox)sender).Checked == false)
			{
				foreach (Control control in (tabPage1.Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Num", false)))
				{
					control.Dispose();
				}
				foreach (Control control in (tabPage1.Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Bit", false)))
				{
					control.Dispose();
				}
				if (((System.Windows.Forms.CheckBox)sender).Name == checkBox1.Name)
				{
					foreach (Control control in (tabPage1.Controls.Find("folderRename", false)))
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
