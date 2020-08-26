﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    public class AJEntity
    {
        #region 案卷的基本属性
        /// <summary>
        /// 该目录的数据[列名,值]
        /// </summary>
        public Dictionary<string,string> Value
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
        /// 是否为传统案卷
        /// </summary>
        public bool IsOneToMany
        {
            get;
            set;
        } = true;

        #endregion

        #region 案卷状态标志

        /// <summary>
        /// 扫描件是否装载完成
        /// </summary>
        private bool FilesLoaded
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 卷内数据是否装载完成
        /// </summary>
        public bool JNLoaded
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 片段路径是否读取完成
        /// </summary>
        public bool PathLoaded
        {
            get;
            set;
        } = false;

        #endregion

        #region 案卷的赋值属性
        /// <summary>
        /// 页数所在列名
        /// </summary>
        private string PageColumn = null;
        /// <summary>
        /// 该目录的总页数
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
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[" + methodName.Name + "]", "页数调用的值[" + Value[PageColumn] + "]不是数字");
                            return -1;
                        }
                    }
                    else
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("", "", "页数所在列未设定");
                        JumpPageCount = true;
                        return -2;
                    }
                }
                catch (Exception ex)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "读取页数出现问题", ex.Message);
                    return -3;
                }
            }
        }

        /// <summary>
        /// 件数所在列名
        /// </summary>
        private string JNCountColumn = null;
        /// <summary>
        /// 件数
        /// </summary>
        public int JNCount
        {
            get
            {
                try
                {
                    if (JNCountColumn != null)
                    {
                        int jncount;
                        if (int.TryParse(Value[JNCountColumn].Trim(), out jncount))
                        {
                            return jncount;
                        }
                        else
                        {
                            StackTrace trace = new StackTrace();
                            MethodBase methodName = trace.GetFrame(1).GetMethod();
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[" + methodName.Name + "]", "件数调用的值[" + Value[JNCountColumn] + "]不是数字");
                            return -1;
                        }
                    }
                    else
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("", "", "件数所在列未设定");
                        JumpJNCount = true;
                        return -2;
                    }
                }
                catch (Exception ex)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "读取件数出现问题", ex.Message);
                    return -3;
                }
            }
        }

        /// <summary>
        /// 用于查找卷内的关键列
        /// </summary>
        private List<string> Filter = null;
        /// <summary>
        /// 用于查找卷内的值
        /// </summary>
        public List<string> Key
        {
            get
            {
                if (Filter != null && Filter.Count != 0)
                {
                    try
                    {
                        List<string> key = new List<string>();
                        foreach (string str in Filter)
                        {
                            key.Add(Value[str].Trim());
                        }
                        return key;
                    }
                    catch(Exception ex)
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[AJ.Key]", ex.Message);
                        return null;
                    }
                }
                else
                {
                    StackTrace trace = new StackTrace();
                    MethodBase methodName = trace.GetFrame(1).GetMethod();
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[" + methodName.Name + "]", "案卷的索引列未给定");
                    return null;
                }
            }
        }

        /// <summary>
        /// 片段路径规则[名称,规定长度]
        /// </summary>
        private Dictionary<string, int> DirRule = new Dictionary<string, int>();

        private string RootDir;

        private string _Dir = null;

        /// <summary>
        /// 扫描件存放的路径
        /// </summary>
        public string Dir
        {
            get
            {
                return _Dir;
            }
            set
            {
                if (!Directory.Exists(value))
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[Dir]", "不存在给定的路径:" + value);
                    _Dir = null;
                }
                else
                {
                    _Dir = value;
                }
            }
        }

        /// <summary>
        /// 卷内数据
        /// </summary>
        private List<JNEntity> JNEntities
        {
            get;
            set;
        } = new List<JNEntity>();        

        /// <summary>
        /// 该案卷的正文扫描件
        /// </summary>
        public List<FileInfo> ScanFiles
        {
            get;
            set;
        } = new List<FileInfo>();

        /// <summary>
        /// 封面文件
        /// </summary>
        public List<FileInfo> FMFiles
        {
            get;
            set;
        } = new List<FileInfo>();

        /// <summary>
        /// 目录文件
        /// </summary>
        public List<FileInfo> MLFiles
        {
            get;
            set;
        } = new List<FileInfo>();

        /// <summary>
        /// 封底文件
        /// </summary>
        public List<FileInfo> FDFiles
        {
            get;
            set;
        } = new List<FileInfo>();

        private bool JumpJNCount = false;
        private bool JumpPageCount = false;

        #endregion

        #region 构造
        public AJEntity(Dictionary<string, string> _value, int _location,string _rootDir = null,
            string _PageColumn = null, 
            Dictionary<string, int> _DirRule = null)
        {
            this.Value = _value;
            this.Location = _location;
            this.PageColumn = _PageColumn;
            this.RootDir = _rootDir;
            this.DirRule = _DirRule;
        }
        #endregion

        #region 装载方法

        public void OneToManyAppend(string _JNCountColumn, List<string> _filter)
        {
            this.JNCountColumn = _JNCountColumn;
            this.Filter = _filter;
        }

        /// <summary>
        /// 装载卷内
        /// </summary>
        private void  LoadJN()
        {
            List<JNEntity> entities = new List<JNEntity>(MainForm.CreateInstrance().jnEntities_List);
            List<string> JNColumnForFilter = MainForm.CreateInstrance().JNFilter;
            JNEntities = entities.Where(jn => ListFind(jn, JNColumnForFilter, Key)).ToList();            
            foreach (JNEntity jn in JNEntities)
            {
                    MainForm.CreateInstrance().jnEntities_List.Remove(jn);                
            }
        }
        public static bool ListFind(JNEntity jn, List<string> filter, List<string> value)
        {
            for (int i = 0; i < filter.Count; i++)
            {
                if (jn.Value[filter[i]] == value[i])
                    continue;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 装载扫描件
        /// </summary>
        private void LoadFiles()
        {           
            if(!PathLoaded)
            {
                LoadPath();
            }
            try
            {
                //指定目录
                DirectoryInfo directoryInfo = new DirectoryInfo(Dir);
                //指定类型的文件
                FileInfo[] files = directoryInfo.GetFiles();
                //根据文件名排序
                IOrderedEnumerable<FileInfo> orderedEnumerable = files.OrderBy((FileInfo c) => c.Name);
                List<FileInfo> fmFile = new List<FileInfo>();
                List<FileInfo> mlFile = new List<FileInfo>();
                List<FileInfo> fdFile = new List<FileInfo>();
                foreach (FileInfo item in orderedEnumerable)
                {
                    if (item.Name.Contains("FM") || item.Name.Contains("fm")) //封面
                    {
                        FMFiles.Add(item);
                    }
                    else if (item.Name.Contains("ML") || item.Name.Contains("JN") || item.Name.Contains("ml") || item.Name.Contains("jn")) //卷内目录。
                    {
                        MLFiles.Add(item);
                    }
                    else if (item.Name.Contains("BK") || item.Name.Contains("FD") || item.Name.Contains("fd") || item.Name.Contains("bk")) //备考表，封面。
                    {
                        FDFiles.Add(item);
                    }
                    else //扫描的卷内目录图片文件。
                    {
                        ScanFiles.Add(item);
                    }
                }
            }
            catch(Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[LoadFiles]", ex.Message);
            }
        }

        /// <summary>
        /// 装载路径
        /// </summary>
        private void LoadPath()
        {
            try
            {
                if(DirRule == null || DirRule.Count == 0)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[LoadPath]", "没有有效的路径规则");
                    return;
                }
                List<string> partPath = new List<string>();
                foreach (string s in DirRule.Keys)
                {
                    string part = Value[s].ToString().Trim();
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
                        MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString() + "列名:" + s, "扫描件路径拼接", "在指定的位置没有数据");
                    }
                }
                if (partPath.Count != 0)
                {
                    Dir = string.Join(@"\", partPath.ToArray());
                    PathLoaded = true;
                }
                else
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "扫描件路径拼接", "在该行无法根据给定列名" + string.Join("/", DirRule.Keys.ToList()) + "得到任何值");
                }
            }
            catch (Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[LoadPath]", ex.Message);
            }
        }

        public void LoadProperty(string PageColumn, Dictionary<string, int> DirRule, string JNCountColumn = null, Dictionary<string, string> AJ_JN = null)
        {
        //    string errorInfo;
        //    #region 装载页数
        //    try
        //    {
        //        var objPage = Value.Rows[0][PageColumn];
        //        if (objPage == null || string.IsNullOrWhiteSpace(objPage.ToString()))
        //        {
        //            errorInfo = "指定的页数列不存在数据";
        //            return errorInfo;
        //        }
        //        else if (!int.TryParse(objPage.ToString(), out int num))
        //        {
        //            errorInfo = "指定的页数列的值不为数字";
        //            return errorInfo;
        //        }
        //        else
        //        {
        //            Pages = int.Parse(objPage.ToString());
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        errorInfo = "[AJ/LoadProperty/ReadPage]";
        //        return errorInfo + ex.Message;
        //    }
        //    #endregion
        //    #region 装载路径
        //    try
        //    {
        //        List<string> partPath = new List<string>();
        //        foreach (string s in DirRule.Keys)
        //        {
        //            string part = Value.Rows[0][s].ToString().Trim();
        //            if (!string.IsNullOrWhiteSpace(part))
        //            {
        //                int length = DirRule[s];
        //                if (length != 0)
        //                {
        //                    if (part.Length < length)
        //                    {
        //                        part = part.PadLeft(length, '0');
        //                    }
        //                    else if (part.Length > length)
        //                    {
        //                        part = part.Substring(part.Length - length);
        //                    }
        //                    partPath.Add(part);
        //                }
        //                else
        //                {
        //                    partPath.Add(part);
        //                }
        //            }
        //            else
        //            {
        //                errorInfo = s + "列不存在数据,拼接失败";
        //                return errorInfo;
        //            }
        //        }
        //        if (partPath.Count != 0)
        //        {
        //            Dir = string.Join(@"\", partPath.ToArray());
        //        }
        //        else
        //        {
        //            errorInfo = "指定的规则下不存在数据";
        //            return errorInfo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorInfo = "[AJ/LoadProperty/SpellPath]";
        //        return errorInfo + ex.Message;
        //    }
        //    #endregion
        //    if(IsOneToMany)
        //    {
        //        #region 装载件数
        //        if (!string.IsNullOrWhiteSpace(JNCountColumn))
        //        {
        //            try
        //            {
        //                var objJNCount = Value.Rows[0][JNCountColumn];
        //                if (objJNCount == null || string.IsNullOrWhiteSpace(objJNCount.ToString()))
        //                {
        //                    errorInfo = "指定的件数列不存在数据";
        //                    return errorInfo;
        //                }
        //                else if (!int.TryParse(objJNCount.ToString(), out int num))
        //                {
        //                    errorInfo = "指定的件数列的值不为数字";
        //                    return errorInfo;
        //                }
        //                else
        //                {
        //                    JNCount = int.Parse(objJNCount.ToString());
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                errorInfo = "[AJ/LoadProperty/ReadJNCount]";
        //                return errorInfo + ex.Message;
        //            }
        //        }
        //        #endregion
        //        #region 装载关键值
        //        if (AJ_JN != null)
        //        {
        //            try
        //            {
        //                List<string> keyList = new List<string>();
        //                foreach (string key in AJ_JN.Keys)
        //                {
        //                    var currentCellValue = Value.Rows[0][key];
        //                    if (currentCellValue == null || string.IsNullOrWhiteSpace(currentCellValue.ToString()))
        //                    {
        //                        errorInfo = "指定的关联列" + key + "不存在数据";
        //                        return errorInfo;
        //                    }
        //                    else
        //                    {
        //                        keyList.Add(currentCellValue.ToString());
        //                    }
        //                }
        //                Key = keyList;
        //            }
        //            catch(Exception ex)
        //            {
        //                errorInfo = "[AJ/LoadProperty/SpellKey]";
        //                return errorInfo + ex.Message;
        //            }
        //        }
        //        #endregion
        //    }
        //    return null;
        }

        #endregion

        #region 工作方法

        /// <summary>
        /// 信息匹配检测
        /// </summary>
        public void MatchInfo()
        {
            if (!FilesLoaded)
            {
                LoadFiles();
            }
            MatchPages();
            MatchCount();
        }

        private void MatchPages()
        {
            if (!JumpPageCount)
            {
                if (IsOneToMany)
                {
                    if (!JNLoaded)
                    {
                        LoadJN();
                    }
                    int jnpages = 0;
                    foreach(JNEntity jn in JNEntities)
                    {
                        if (jn.Pages > 0)
                            jnpages += jn.Pages;
                        else if (jn.Pages == -2)
                            JumpPageCount = true;
                        else
                            continue;
                    }
                    if(jnpages != Pages)
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "页数检测", "卷内总页数[" + jnpages + "]与案卷页数[" + Pages + "]不符");
                    }
                }
                if (ScanFiles.Count != Pages)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "页数检测", "扫描件数[" + ScanFiles.Count + "]与案卷页数[" + Pages + "]不符");
                }
            }
        }

        private void MatchCount()
        {
            if (IsOneToMany)
            {
                if (!JNLoaded)
                {
                    LoadJN();
                }
                if (!JumpJNCount)
                {
                    if (JNEntities.Count != JNCount)
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "传统案卷件数检测", "卷内条目[" + JNEntities.Count + "]与案卷[" + JNCount + "]不符");
                    }
                }
            }
        }

        #endregion
    }
}

