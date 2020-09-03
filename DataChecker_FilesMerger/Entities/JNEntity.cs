using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DataChecker_FilesMerger
{
    [Serializable]
    public class JNEntity
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
        /// 扫描件
        /// </summary>
        public List<FileInfo> ScanFiles
        {
            get;
            set;
        } = new List<FileInfo>();

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
                        {
                            return pages;
                        }
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
                        MainForm.CreateInstrance().WriteErrorInfo("JN行号:" + Location.ToString(), "读取页数出现问题", "页数列未设置!");
                        return -2;
                    }
                }
                catch (Exception ex)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("JN行号:" + Location.ToString(), "读取页数出现问题", ex.Message);
                    return -3;
                }
            }
        }


        #region 构造
        public JNEntity(Dictionary<string, string> _value, int _location, string _PageColumn = null)
        {
            this.Value = _value;
            this.Location = _location;
            this.PageColumn = _PageColumn;
        }
        #endregion

    }
}
