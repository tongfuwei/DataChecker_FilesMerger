using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    public class AJEntity
    {
        /// <summary>
        /// 该目录的行号
        /// </summary>
        public int Location
        {
            get;
            set;
        }

        /// <summary>
        /// 该目录的数据
        /// </summary>
        public DataTable Value
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
        /// 该目录的总页数
        /// </summary>
        public int Pages
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为传统案卷
        /// </summary>
        public bool IsOneToMany
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 件数
        /// </summary>
        public int JNCount
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
        /// 该案卷对应的所有扫描件
        /// </summary>
        public List<FileInfo> ScanFiles
        {
            get;
            set;
        } = new List<FileInfo>();

        public List<FileInfo> Additional
        {
            get;
            set;
        } = new List<FileInfo>();

        public string LoadProperty(string PageColumn, Dictionary<string, int> DirRule, string JNCountColumn = null, Dictionary<string, string> AJ_JN = null)
        {
            string errorInfo;
            #region 装载页数
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
            catch(Exception ex)
            {
                errorInfo = "[AJ/LoadProperty/ReadPage]";
                return errorInfo + ex.Message;
            }
            #endregion
            #region 装载路径
            try
            {
                List<string> partPath = new List<string>();
                foreach (string s in DirRule.Keys)
                {
                    string part = Value.Rows[0][s].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(part))
                    {
                        int length = DirRule[s];
                        if (length != 0)
                        {
                            if (part.Length < length)
                            {
                                part = part.PadLeft(length, '0');
                            }
                            else if (part.Length > length)
                            {
                                part = part.Substring(part.Length - length);
                            }
                            partPath.Add(part);
                        }
                        else
                        {
                            partPath.Add(part);
                        }
                    }
                    else
                    {
                        errorInfo = s + "列不存在数据,拼接失败";
                        return errorInfo;
                    }
                }
                if (partPath.Count != 0)
                {
                    Dir = string.Join(@"\", partPath.ToArray());
                }
                else
                {
                    errorInfo = "指定的规则下不存在数据";
                    return errorInfo;
                }
            }
            catch (Exception ex)
            {
                errorInfo = "[AJ/LoadProperty/SpellPath]";
                return errorInfo + ex.Message;
            }
            #endregion
            if(IsOneToMany)
            {
                #region 装载件数
                if (!string.IsNullOrWhiteSpace(JNCountColumn))
                {
                    try
                    {
                        var objJNCount = Value.Rows[0][JNCountColumn];
                        if (objJNCount == null || string.IsNullOrWhiteSpace(objJNCount.ToString()))
                        {
                            errorInfo = "指定的件数列不存在数据";
                            return errorInfo;
                        }
                        else if (!int.TryParse(objJNCount.ToString(), out int num))
                        {
                            errorInfo = "指定的件数列的值不为数字";
                            return errorInfo;
                        }
                        else
                        {
                            JNCount = int.Parse(objJNCount.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        errorInfo = "[AJ/LoadProperty/ReadJNCount]";
                        return errorInfo + ex.Message;
                    }
                }
                #endregion
                #region 装载关键值
                if (AJ_JN != null)
                {
                    try
                    {
                        List<string> keyList = new List<string>();
                        foreach (string key in AJ_JN.Keys)
                        {
                            var currentCellValue = Value.Rows[0][key];
                            if (currentCellValue == null || string.IsNullOrWhiteSpace(currentCellValue.ToString()))
                            {
                                errorInfo = "指定的关联列" + key + "不存在数据";
                                return errorInfo;
                            }
                            else
                            {
                                keyList.Add(currentCellValue.ToString());
                            }
                        }
                        Key = keyList;
                    }
                    catch(Exception ex)
                    {
                        errorInfo = "[AJ/LoadProperty/SpellKey]";
                        return errorInfo + ex.Message;
                    }
                }
                #endregion
            }
            return null;
        }

        public string LoadFiles(string rootDir, string fileFormat)
        {
            string errorInfo;
            string path = rootDir + "\\" + Dir;
            if (!Directory.Exists(path))
            {
                errorInfo = "不存在给定的路径:" + path;
                return errorInfo;
            }
            else
            {
                try
                {
                    //指定目录
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    //指定类型的文件
                    FileInfo[] files = directoryInfo.GetFiles(fileFormat);
                    IOrderedEnumerable<FileInfo> orderedEnumerable = files.OrderBy((FileInfo c) => c.Name);
                    List<FileInfo> fmFile = new List<FileInfo>();
                    List<FileInfo> mlFile = new List<FileInfo>();
                    List<FileInfo> fdFile = new List<FileInfo>();
                    foreach (FileInfo item in orderedEnumerable)
                    {
                        if (item.Name.Contains("FM")) //封面
                        {
                            fmFile.Add(item);
                        }
                        else if (item.Name.Contains("ML") || item.Name.Contains("JN")) //卷内目录。
                        {
                            mlFile.Add(item);
                        }
                        else if (item.Name.Contains("BK") || item.Name.Contains("FD")) //备考表，封面。
                        {
                            fdFile.Add(item);
                        }
                        else //扫描的卷内目录图片文件。
                        {
                            ScanFiles.Add(item);
                        }
                    }
                    Additional.AddRange(fmFile);
                    Additional.AddRange(mlFile);
                    Additional.AddRange(fdFile);
                    if(ScanFiles.Count == 0)
                    {
                        errorInfo = "该案卷未找到扫描件";
                        return errorInfo;
                    }
                    return null;
                }
                catch(Exception ex)
                {
                    errorInfo = "[AJ/LoadFiles]";
                    return errorInfo + ex.Message;
                }
            }
        }
    }
}

