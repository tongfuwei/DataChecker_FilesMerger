using DataChecker_FilesMerger.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
	public partial class MergeAddition : Form
	{
		private static MergeAddition mainForm = null;
		public static MergeAddition CreateInstrance()
		{
			if (mainForm == null || mainForm.IsDisposed)
			{
				mainForm = new MergeAddition();
			}
			return mainForm;
		}

		private List<Control> AddedControls
		{
			get;
			set;
		} = new List<Control>();

		private Dictionary<string, int> _AJColumn = new Dictionary<string, int>();
		public Dictionary<string, int> AJColumn
		{
			get
			{
				return _AJColumn;
			}
			set
			{
				_AJColumn = value;
			}
		}

		private Dictionary<ComboBox, CheckBox> Part_Separator = new Dictionary<ComboBox, CheckBox>();

		private string DefaultSeparator = "-";

		public bool MergeAdditions
		{
			get;
			set;
		} = false;

		public Dictionary<string, string> additionPartName
		{
			get;
			set;
		} = new Dictionary<string, string>();

		public List<string> additionSort
		{
			get;
			set;
		} = new List<string>();

		public bool AppendToHead
		{
			get;
			set;
		} = false;

		public MergeAddition()
		{
			InitializeComponent();
		}

		public void Upload(IndexDictionary<string> AJColumns, bool OneToMany = true)
		{
			DialogResult = DialogResult.None;
			if (!AJColumn.EqualDictionary(AJColumns.strIndex))
			{
				AJColumn = AJColumns.strIndex;
			}
			if (!OneToMany)
			{
				CheckBox check = new CheckBox
				{
					Name = "cbAppend",
					Text = "追加到案卷头部"
				};
				check.CheckedChanged += Check_CheckedChanged; ;
				ControlHelper.AppendControl(checkBox1, this, ControlHelper.Direction.Horizontal, check);
			}
		}

		private void Check_CheckedChanged(object sender, EventArgs e)
		{
			AppendToHead = ((CheckBox)sender).Checked;
			if (AppendToHead)
			{
				foreach (Control control in AddedControls)
				{
					control.Dispose();
				}
				button1.Enabled = false;
				button2.Enabled = false;
				tbDefault.Enabled = false;
				tbLast.Enabled = false;
			}
			else
			{
				button1.Enabled = true;
				button2.Enabled = true;
				tbDefault.Enabled = true;
				tbLast.Enabled = true;
			}
		}

		private void tbDefault_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
			{
				DefaultSeparator = ((TextBox)sender).Text;
			}
		}

		private void btn_AddControl(object sender, EventArgs e)
		{
			Control pointControl;
			if (Part_Separator.Count == 0)
			{
				pointControl = (Control)sender;
			}
			else
			{
				pointControl = Part_Separator.Keys.Last();
			}
			List<List<object>> controls = new List<List<object>>();
			List<object> list = new List<object>();

			ComboBox combo = new ComboBox
			{
				Name = "ComboBox_" + AddedControls.Count.ToString()
			};
			ControlHelper.ComboAdd(combo, AJColumn.Keys.ToList());
			list.Add(combo);

			CheckBox check = new CheckBox();
			check.Name = "CheckBox" + AddedControls.Count.ToString();
			check.Text = "特殊分隔符";
			check.CheckedChanged += checkBox_CheckedChanged;
			list.Add(check);

			controls.Add(list);

			ControlHelper.AppendControls(pointControl, this, ControlHelper.Direction.Vertical, ControlHelper.Direction.Horizontal, controls);

			AddedControls.Add(combo);
			AddedControls.Add(check);
			Part_Separator.Add(combo, check);
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (((CheckBox)sender).Checked == true)
			{
				TextBox num = new TextBox
				{
					Name = ((CheckBox)sender).Name + "_Num",
					MaxLength = 1,
					Size = new Size(28, 25)
				};
				ControlHelper.AppendControl(sender, this, ControlHelper.Direction.Horizontal, num);

				AddedControls.Add(num);
			}
			else
			{
				foreach (Control control in Controls.Find(((CheckBox)sender).Name + "_Num", false))
				{
					control.Dispose();
					AddedControls.Remove(control);
				}
			}
		}

		public DialogResult CheckSave()
		{
			if (MergeAdditions)
			{
				additionSort.Clear();

				if (!string.IsNullOrWhiteSpace(comboBox1.SelectedItem.ToString()))
				{
					additionSort.Add(comboBox1.SelectedItem.ToString());
				}

				if (!string.IsNullOrWhiteSpace(comboBox2.SelectedItem.ToString()))
				{
					additionSort.Add(comboBox2.SelectedItem.ToString());
				}

				if (!string.IsNullOrWhiteSpace(comboBox3.SelectedItem.ToString()))
				{
					additionSort.Add(comboBox3.SelectedItem.ToString());
				}

				if (additionSort.Count == 0)
				{
					MessageBox.Show("若要合并附件,请先设置附件排序!");
					return DialogResult.Cancel;
				}

				if (!AppendToHead)
				{
					additionPartName.Clear();
					foreach (ComboBox com in Part_Separator.Keys)
					{
						if (!string.IsNullOrWhiteSpace(com.SelectedItem.ToString()))
						{
							CheckBox chk = Part_Separator[com];
							if (chk.Checked)
							{
								foreach (Control con in Controls.Find(chk.Name + "_Num", false))
								{
									TextBox txt = (TextBox)con;
									if (!string.IsNullOrWhiteSpace(txt.Text))
									{
										additionPartName.Add(com.SelectedItem.ToString(), txt.Text);
									}
									else
									{
										additionPartName.Add(com.SelectedItem.ToString(), DefaultSeparator);
									}
								}
							}
							else
							{
								additionPartName.Add(com.SelectedItem.ToString(), DefaultSeparator);
							}
						}
						else
						{
							continue;
						}
					}

					if (additionPartName.Count == 0)
					{
						MessageBox.Show("若要合并附件,请先设置命名规则!");
						return DialogResult.Cancel;
					}

					else if (string.IsNullOrWhiteSpace(tbLast.Text))
					{
						MessageBox.Show("为了避免重名,必须设置附件后缀!");
						return DialogResult.Cancel;
					}

					else
					{
						additionPartName[additionPartName.Last().Key] = tbLast.Text;
					}
				}
			}

			return DialogResult.OK;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			foreach (Control control in AddedControls)
			{
				control.Dispose();
			}
			Part_Separator.Clear();
			AddedControls.Clear();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			MergeAdditions = ((CheckBox)sender).Checked;
		}
	}
}
