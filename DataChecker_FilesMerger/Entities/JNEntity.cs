using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    public class JNEntity
    {
        /// <summary>
        /// 该目录的完整数据
        /// </summary>
        public DataTable Value
        {
            get;
            set;
        } = new DataTable();

        /// <summary>
        /// 根据案卷-卷内对应关系拿到的关键值
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 当前卷内的页数
        /// </summary>
        public int Pages
        {
            get;
            set;
        }

        public string LoadProperty(string PageColumn,string key)
        {
            string errorInfo;
            try
            {
                var objPage = Value.Rows[0][PageColumn];
                if (objPage == null || string.IsNullOrWhiteSpace(objPage.ToString()))
                {
                    errorInfo = "指定的页数列不存在数据";
                    return errorInfo;
                }
                else if (!int.TryParse(objPage.ToString(), out int num))
                {
                    errorInfo = "指定的页数列的值不为数字";
                    return errorInfo;
                }
                else
                {
                    Pages = int.Parse(objPage.ToString());
                }
            }
            catch (Exception ex)
            {
                errorInfo = "[JN/LoadProperty/ReadPage]";
                return errorInfo + ex.Message;
            }
            return null;
        }

        public string GetDate(string PageColumn)
        {
            string errorInfo;
            try
            {
                var objPage = Value.Rows[0][PageColumn];
                if (objPage == null || string.IsNullOrWhiteSpace(objPage.ToString()))
                {
                    errorInfo = "指定的日期列不存在数据";
                    return errorInfo;
                }
                else if (!DateTime.TryParse(objPage.ToString(), out DateTime num))
                {
                    errorInfo = "指定的日期列格式有问题";
                    return errorInfo;
                }
                return objPage.ToString();
            }
            catch (Exception ex)
            {
                errorInfo = "[JN/LoadProperty/ReadPage]";
                return errorInfo + ex.Message;
            }
        }

        public string GetDH(string dh)
        {
            string errorInfo;
            try
            {
                var objPage = Value.Rows[0][dh];
                if (objPage == null || string.IsNullOrWhiteSpace(objPage.ToString()))
                {
                    errorInfo = "指定的日期列不存在数据";
                    return errorInfo;
                }
                return objPage.ToString();
            }
            catch (Exception ex)
            {
                errorInfo = "[JN/LoadProperty/ReadPage]";
                return errorInfo + ex.Message;
            }
        }
    }
}
