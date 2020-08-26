using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    public class JNEntity : DynamicObject
    {
        /// <summary>
        /// 该目录的数据[列名,值]
        /// </summary>
        public Dictionary<string, string> Value
        {
            get;
            set;
        }

        /// <summary>
        /// 该目录的行号
        /// </summary>
        public int Location
        {
            get;
            set;
        }

        /// <summary>
        /// 页数所在列名
        /// </summary>
        private string PageColumn = null;

        /// <summary>
        /// 当前卷内的页数
        /// </summary>
        public int Pages
        {
            get
            {
                try
                {
                    if (PageColumn != null)
                    {
                        int pages;
                        if (int.TryParse(Value[PageColumn].Trim(), out pages))
                            return pages;
                        else
                        {
                            StackTrace trace = new StackTrace();
                            MethodBase methodName = trace.GetFrame(1).GetMethod();
                            MainForm.CreateInstrance().WriteErrorInfo("JN行号:" + Location.ToString(), "[" + methodName.Name + "]", "页数调用的值[" + Value[PageColumn] + "]不是数字");
                            return -1;
                        }
                    }
                    else
                    {
                        StackTrace trace = new StackTrace();
                        MethodBase methodName = trace.GetFrame(1).GetMethod();
                        MainForm.CreateInstrance().WriteErrorInfo("", "", "页数所在列未设定");
                        return -2;
                    }
                }
                catch (Exception ex)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("JN行号:" + Location.ToString(), "读取件数出现问题", ex.Message);
                    return -3;
                }

            }
        }

        /// <summary>
        /// 扫描件
        /// </summary>
        public List<FileInfo> ScanFiles
        {
            get;
            set;
        } = new List<FileInfo>();

        #region 构造
        public JNEntity(Dictionary<string, string> _value, int _location, string _PageColumn = null)
        {
            this.Value = _value;
            this.Location = _location;
            this.PageColumn = _PageColumn;
        }
        #endregion

        Dictionary<string, object> Properties = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                //在此可以做一些小动作
                //if (binder.Name == "Col")
                //　　Properties.Add(binder.Name + (Properties.Count), value.ToString());
                //else
                //　　Properties.Add(binder.Name, value.ToString());


                Properties.Add(binder.Name, value.ToString());
            }
            return true;
        }
    }
}
