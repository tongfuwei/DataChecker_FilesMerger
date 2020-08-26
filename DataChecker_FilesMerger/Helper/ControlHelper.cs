using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Helper
{
    public class ControlHelper
    {
		public List<Control> Controls
		{
			get;
			set;
		}

		internal static void AppendControl(object sender, Form form, Direction direction, List<Control> controls)
		{
			Control parent = (Control)sender;
			int X = parent.Location.X;
			int Y = parent.Location.Y;
			if (direction == Direction.Horizontal)
			{
				X += parent.Width + 5;
				foreach (Control control in controls)
				{
					control.Location = new System.Drawing.Point(X, Y);
					form.Controls.Add(control);
					X += control.Width + 5;
				}
			}
			else
			{
				Y += parent.Height + 5;
				foreach (Control control in controls)
				{
					form.Controls.Add(control);
				}
			}
		}

		/// <summary>
		/// 设置控件必须为数字
		/// </summary>
		/// <param name="sender">控件</param>
		/// <param name="positiveNumber">是否要求大于0</param>
		/// <returns></returns>
		internal static bool NumberCheck(object sender, bool positiveNumber)
		{
			if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
			{
				int num;
				if (!int.TryParse(((TextBox)sender).Text, out num))
				{
					((TextBox)sender).Clear();
					MessageBox.Show("必须输入数字");
					return false;
				}
				else if (positiveNumber && num <= 0)
				{
					((TextBox)sender).Clear();
					MessageBox.Show("必须大于0");
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}

		internal enum Direction
		{
			/// <summary>
			/// 垂直
			/// </summary>
			Vertical,
			/// <summary>
			/// 水平
			/// </summary>
			Horizontal
		}
	}
}
