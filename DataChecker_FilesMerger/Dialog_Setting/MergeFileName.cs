using DataChecker_FilesMerger.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
	public partial class MergeFileName : Form
	{
		private static MergeFileName mainForm = null;
		public static MergeFileName CreateInstrance()
		{
			if (mainForm == null || mainForm.IsDisposed)
			{
				mainForm = new MergeFileName();
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

		public Dictionary<string, string> pdfPartName
		{
			get;
			set;
		} = new Dictionary<string, string>();

		private string DefaultSeparator = "-";

		public MergeFileName()
		{
			InitializeComponent();
		}

		public void Upload(IndexDictionary<string> AJColumns)
		{
			DialogResult = DialogResult.None;
			if (!AJColumn.EqualDictionary(AJColumns.strIndex))
			{
				AJColumn = AJColumns.strIndex;
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
				TextBox num = new TextBox();
				num.Name = ((CheckBox)sender).Name + "_Num";
				num.MaxLength = 1;
				num.Size = new Size(28, 25);
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
			pdfPartName.Clear();
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
								pdfPartName.Add(com.SelectedItem.ToString(), txt.Text);
							}
							else
							{
								pdfPartName.Add(com.SelectedItem.ToString(), DefaultSeparator);
							}
						}
					}
					else
					{
						pdfPartName.Add(com.SelectedItem.ToString(), DefaultSeparator);
					}
				}
				else
				{
					continue;
				}
			}

			if (pdfPartName.Count == 0)
			{
				MessageBox.Show("请先设置PDF命名规则!");
				return DialogResult.Cancel;
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
	}
}
