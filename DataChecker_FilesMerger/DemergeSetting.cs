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
	public partial class DemergeSetting : Form
	{
		private Dictionary<string, int> Column = new Dictionary<string, int>();

		public List<string> saveColumn = new List<string>();

		public List<string> turnRow = new List<string>();

		public DemergeSetting(Dictionary<string, int> _Columns, List<string> _saveColumn = null, List<string> _turnRow = null)
		{
			InitializeComponent();
			Column = _Columns;
			saveColumn = _saveColumn;
			turnRow = _turnRow;
			InitCombox();
			InitControls();
		}

		public void InitCombox()
		{
			foreach (Control control in this.tabPage1.Controls)
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
			foreach (Control control in this.tabPage2.Controls)
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

		/// <summary>
		/// 读取前一次的状态
		/// </summary>
		private void InitControls()
		{
			if (saveColumn != null)
			{
				for (int i = 0; i < saveColumn.Count; i++)
				{
					int num = i + 1;
					string comboName = "cbColumn" + num;
					foreach (Control control in (this.tabPage1.Controls.Find(comboName, false)))
					{
						ComboBox combo = control as ComboBox;
						combo.SelectedItem = saveColumn[i];
					}
				}
			}
			if (turnRow != null)
			{
				for (int i = 0; i < turnRow.Count; i++)
				{
					int num = i + 1;
					string comboName = "cbRow" + num;
					foreach (Control control in (this.tabPage2.Controls.Find(comboName, false)))
					{
						ComboBox combo = control as ComboBox;
						combo.SelectedItem = turnRow[i];
					}
				}
			}
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			saveColumn.Clear();
			turnRow.Clear();
			for (int i = 1; i < 4; i++)
			{
				string comboName = "cbColumn" + i;
				foreach (Control control in this.tabPage1.Controls.Find(comboName, false))
				{
					ComboBox combo = control as ComboBox;
					if (combo.SelectedItem != null && !string.IsNullOrWhiteSpace(combo.SelectedItem.ToString()))
					{
						saveColumn.Add(combo.SelectedItem.ToString());
					}
				}
			}
			for (int i = 1; i < 13; i++)
			{
				string comboName = "cbRow" + i;
				foreach (Control control in this.tabPage2.Controls.Find(comboName, false))
				{
					ComboBox combo = control as ComboBox;
					if (combo.SelectedItem != null && !string.IsNullOrWhiteSpace(combo.SelectedItem.ToString()))
					{
						turnRow.Add(combo.SelectedItem.ToString());
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (cbRow1.SelectedItem == null || string.IsNullOrWhiteSpace(cbRow1.SelectedItem.ToString()))
			{
				MessageBox.Show("请先给第一项赋值");
			}
			else
			{
				int startItem = cbRow1.SelectedIndex;
				for (int i = 2; i < 13; i++)
				{
					startItem++;
					if (startItem < cbRow1.Items.Count)
					{
						string comboName = "cbRow" + i;
						foreach (Control control in (this.tabPage2.Controls.Find(comboName, false)))
						{
							ComboBox combo = control as ComboBox;
							combo.SelectedIndex = startItem;
						}
					}
				}
			}
		}
	}
}