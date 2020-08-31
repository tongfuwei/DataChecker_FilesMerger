using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Helper
{
    public static class ControlHelper
    {
        /// <summary>
        /// 批量追加控件
        /// </summary>
        /// <param name="_parent">基准控件</param>
        /// <param name="_form">目标窗体</param>
        /// <param name="Direction">List<object>中的First的追加方向</param>
        /// <param name="son_Direction">其余的基于First追加方向</param>
        /// <param name="controls">存放控件的二维表</param>
        public static void AppendControls(object _parent, Form _form, Direction Direction, Direction son_Direction, List<List<object>> controls)
        {
            try
            {
                object parent = _parent;
                foreach (List<object> objs in controls)
                {
                    Control obj = (Control)objs[0];
                    AppendControl(parent, _form, Direction, obj);
                    object Parent = objs[0];
                    for (int i = 1; i < objs.Count(); i++)
                    {
                        AppendControl(Parent, _form, son_Direction, objs[i]);
                        Parent = objs[i];
                    }
                    //把当前控件作为下个控件的基准
                    parent = obj;
                }
            }
            catch (Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("", "", ex.Message);
            }
        }

        /// <summary>
        /// 简单追加控件
        /// </summary>
        /// <param name="_parent">基准控件</param>
        /// <param name="form">目标窗体</param>
        /// <param name="direction">追加方向</param>
        /// <param name="son">追加的控件</param>
        /// <param name="vlue">偏移值</param>
        public static void AppendControl(object _parent, Form form, Direction direction, object son, int vlue = 5)
        {
            try
            {
                Control parent = (Control)_parent;
                Control control = (Control)son;
                int X = parent.Location.X;
                int Y = parent.Location.Y;
                if (direction == Direction.Horizontal)
                {
                    X += parent.Width + vlue;
                    control.Location = new Point(X, Y);
                    form.Controls.Add(control);
                }
                else if (direction == Direction.Vertical)
                {
                    Y += parent.Height + vlue;
                    control.Location = new Point(X, Y);
                    form.Controls.Add(control);
                }
                else
                {
                    X += parent.Width + vlue;
                    Y += parent.Height + vlue;
                    control.Location = new Point(X, Y);
                    form.Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("", "", ex.Message);
            }
        }

        /// <summary>
        /// 给comboBox添加项
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="value"></param>
        public static void ComboAdd(ComboBox combo, List<string> value)
        {
            //Combo中没有时添加
            foreach (string ColumnName in value)
            {
                if (combo.Items.Contains(ColumnName))
                {
                    continue;
                }
                else
                {
                    combo.Items.Add(ColumnName);
                }
            }
            //将list中没有的项从combo移除
            for (int i = 0; i < combo.Items.Count; i++)
            {
                object item = combo.Items[i];
                if (value.Contains(item))
                {
                    continue;
                }
                else
                {
                    combo.Items.Remove(item);
                }
            }
            //在头部添加空白项
            if (combo.Items.Count != 0)
            {
                if (!string.IsNullOrWhiteSpace(combo.Items[0].ToString()))
                {
                    combo.Items.Insert(0, "");
                    combo.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 设置控件必须为数字
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="positiveNumber">是否要求大于0</param>
        /// <returns></returns>
        public static bool NumberCheck(object sender, bool positiveNumber)
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

        public static void TextIsPositiveNumber(object sender, EventArgs e)
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

        public enum Direction
        {
            /// <summary>
            /// 垂直
            /// </summary>
            Vertical,
            /// <summary>
            /// 水平
            /// </summary>
            Horizontal,
            /// <summary>
            /// 斜向
            /// </summary>
            Bevel
        }
    }
}
