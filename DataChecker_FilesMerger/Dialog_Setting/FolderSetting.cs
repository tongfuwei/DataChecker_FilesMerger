using DataChecker_FilesMerger.Helper;
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
	public partial class FolderSetting : Form
	{
		private static FolderSetting mainForm = null;
		public static FolderSetting CreateInstrance()
		{
			if (mainForm == null || mainForm.IsDisposed)
			{
				mainForm = new FolderSetting();
			}
			return mainForm;
		}

		private Dictionary<string, int> _Column = new Dictionary<string, int>();
		public Dictionary<string, int> Column
		{
			get
			{
				return _Column;
			}
			set
			{
				_Column = value;
				InitCombox();
			}
		}

		/// <summary>
		/// 目录构成,名称,规定长度
		/// </summary>
		public Dictionary<string,int> dirConstitute
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

		public FolderSetting()
		{
			InitializeComponent();
		}

		public void Upload(IndexDictionary<string> AJColumns)
		{
			if (!Column.EqualDictionary(AJColumns.strIndex))
			{
				dir_check.Clear();
				Column = AJColumns.strIndex;
			}
		}

		public void InitCombox()
		{
			foreach (Control control in this.Controls)
			{
				if (control is ComboBox)
				{
					ComboBox combo = control as ComboBox;
					Commons.ComboAdd(combo, Column.Keys.ToList());
				}
			}
			dir_check.Add(cbFolder1, checkBox1);
			dir_check.Add(cbFolder2, checkBox2);
			dir_check.Add(cbFolder3, checkBox3);
			dir_check.Add(cbFolder4, checkBox4);
		}

		public DialogResult CheckSave()
		{
			dirConstitute = new Dictionary<string, int>();
			foreach (ComboBox folder in dir_check.Keys)
			{
				if (folder.SelectedItem != null && !string.IsNullOrWhiteSpace(folder.SelectedItem.ToString()))
				{
					if (dirConstitute.ContainsKey(folder.SelectedItem.ToString()))
					{
						MessageBox.Show("请勿重复填写!");
						return DialogResult.Cancel;
					}
					else
					{
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
							dirConstitute.Add(folder.SelectedItem.ToString(), num);
						}
						else
						{
							dirConstitute.Add(folder.SelectedItem.ToString(), 0);
						}
					}
				}
			}

			if (cbRename.Checked == true)
				renameFolder = true;
			else
				renameFolder = false;

			if (dirConstitute.Count == 0)
			{
				MessageBox.Show("请填写扫描件目录结构!");
				return DialogResult.Cancel;
			}
			else
			{
			 	return DialogResult.OK;
			}
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (((CheckBox)sender).Checked == true)
			{
				cbRename.Enabled = true;
				List<Control> controls = new List<Control>();

				TextBox num = new TextBox();
				num.Name = ((CheckBox)sender).Name + "_Num";
				num.TextChanged += new EventHandler(this.Text_TextChanged);
				controls.Add(num);

				Label text = new Label();
				text.Name = ((CheckBox)sender).Name + "_Bit";
				text.Text = "位";
				controls.Add(text);

				ControlHelper.AppendControl(sender, this, ControlHelper.Direction.Horizontal, controls);
			}
			else
			{
				if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
				{
					cbRename.Enabled = true;
				}
				else
				{
					cbRename.Enabled = false;
				}

				foreach (Control control in (this.Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Num", false)))
				{
					control.Dispose();
				}
				foreach (Control control in (this.Controls.Find(((System.Windows.Forms.CheckBox)sender).Name + "_Bit", false)))
				{
					control.Dispose();
				}
			}
		}

		private void Text_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
			{
				int num;
				if (!int.TryParse(((TextBox)sender).Text, out num))
				{
					((TextBox)sender).Clear();
					MessageBox.Show("必须输入数字");
				}
				else if (num <= 0)
				{
					((TextBox)sender).Clear();
					MessageBox.Show("必须大于0");
				}
			}
		}
	}
}
