using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    public class AJEntity
    {
        /// <summary>
        /// 该目录的数据
        /// </summary>
        public DataTable Value
        {
            get;
            set;
        }

        /// <summary>
        /// 根据案卷-卷内对应关系拿到的关键值
        /// </summary>
        public List<string> Key
        {
            get;
            set;
        }

        /// <summary>
        /// 该目录的片段路径
        /// </summary>
        public string Dir
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
        /// 该目录的页数
        /// </summary>
        public int Pages
        {
            get;
            set;
        }

        /// <summary>
        /// 件数
        /// </summary>
        public int JNCount
        {
            get;
            set;
        }

        /// <summary>
        /// 案卷页数是否与扫面件数相等
        /// </summary>
        public bool AJPageFileCount_CheckOK
        {
            get;
            set;
        }
    }
}

