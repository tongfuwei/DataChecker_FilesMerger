using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    /// <summary>
    /// 以案卷为单位存储卷内数据
    /// </summary>
    public class JNEntity
    {
        /// <summary>
        /// 该目录的完整数据
        /// </summary>
        public DataTable Value
        {
            get;
            set;
        }

        /// <summary>
        /// 根据案卷-卷内对应关系拿到的关键值
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 卷内总页数
        /// </summary>
        public int TotalPage
        {
            get;
            set;
        }

        /// <summary>
        /// 卷内条目总数
        /// </summary>
        public int JNCount
        {
            get;
            set;
        }
    }
}
