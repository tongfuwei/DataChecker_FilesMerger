using DataChecker_FilesMerger.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
	public partial class MergeFolder : Form
	{
		private static MergeFolder mainForm = null;
		public static MergeFolder CreateInstrance()
		{
			if (mainForm == null || mainForm.IsDisposed)
			{
				mainForm = new MergeFolder();
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

		public string PDFSavePath
		{
			get;
			set;
		} = null;

		public Dictionary<string, string> savePartFolder
		{
			get;
			set;
		} = new Dictionary<string, string>();

		private string DefaultSeparator = "-";

		public MergeFolder()
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

		private void cbSaveAlone_CheckedChanged(object sender, EventArgs e)
		{
			if (((CheckBox)sender).Checked == true)
			{
				Button add = new Button();
				add.Name = ((CheckBox)sender).Name + "_Add";
				add.Text = "增加片段";
				add.Click += new EventHandler(btn_AddControl);

				ControlHelper.AppendControl(sender, this, ControlHelper.Direction.Horizontal, add);
			}
			else
			{
				Part_Separator.Clear();
				foreach (Control control in Controls.Find(((CheckBox)sender).Name + "_Add", false))
				{
					control.Dispose();
				}
				foreach (Control control in AddedControls)
				{
					control.Dispose();
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "选择目录";
			folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				tbPDFSavePath.Text = folderBrowserDialog.SelectedPath;
				PDFSavePath = tbPDFSavePath.Text;
			}
		}

		private void tbDefault_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
			{
				if (((TextBox)sender).Text == @"\")
				{
					((TextBox)sender).Text = "";
				}
				else
				{
					DefaultSeparator = ((TextBox)sender).Text;
				}
			}
		}

		private void btn_AddControl(object sender, EventArgs e)
		{
			Control pointControl;
			if (Part_Separator.Count == 0)
			{
				pointControl = cbSaveAlone;
			}
			else
			{
				pointControl = Part_Separator.Keys.Last();
			}
			List<List<object>> controls = new List<List<object>>();
			List<object> list = new List<object>();

			ComboBox combo = new ComboBox();
			combo.Name = "ComboBox_" + AddedControls.Count.ToString();
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
				num.TextChanged += Num_TextChanged;
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

		private void Num_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
			{
				if (((TextBox)sender).Text == @"\")
				{
					((TextBox)sender).Text = "";
				}
			}
		}

		public DialogResult CheckSave()
		{
			if (string.IsNullOrWhiteSpace(PDFSavePath))
			{
				MessageBox.Show("请设置保存路径!");
				return DialogResult.Cancel;
			}

			if (cbSaveAlone.Checked)
			{
				savePartFolder.Clear();
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
									savePartFolder.Add(com.SelectedItem.ToString(), txt.Text);
								}
								else
								{
									savePartFolder.Add(com.SelectedItem.ToString(), DefaultSeparator);
								}
							}
						}
						else
						{
							savePartFolder.Add(com.SelectedItem.ToString(), DefaultSeparator);
						}
					}
					else
					{
						continue;
					}
				}

				if (savePartFolder.Count == 0)
				{
					MessageBox.Show("若要单独保存案卷,请先输入路径设置!");
					return DialogResult.Cancel;
				}

				savePartFolder[savePartFolder.Last().Key] = "";
			}

			return DialogResult.OK;
		}


	}
}
