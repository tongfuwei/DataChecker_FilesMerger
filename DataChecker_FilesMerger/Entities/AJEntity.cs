using DataChecker_FilesMerger.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataChecker_FilesMerger
{
    public class AJEntity
    {
        #region 案卷的基本属性
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
            private set;
        }

        /// <summary>
        /// 是否为传统案卷
        /// </summary>
        public bool IsOneToMany
        {
            get;
            private set;
        } = false;

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
        private bool JNLoaded
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 片段路径是否读取完成
        /// </summary>
        private bool PathLoaded
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
        private int Pages
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
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[" + methodName.Name + "]", "页数调用的值[" + Value[PageColumn] + "]不是数字");
                            return -1;
                        }
                    }
                    else
                    {
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
        private int JNCount
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

        private List<string> JNFilter = null;

        /// <summary>
        /// 用于查找卷内的关键列
        /// </summary>
        private List<string> AJFilter = null;
        /// <summary>
        /// 用于查找卷内的值
        /// </summary>
        public List<string> Key
        {
            get
            {
                if (AJFilter != null && AJFilter.Count != 0)
                {
                    try
                    {
                        List<string> key = new List<string>();
                        foreach (string str in AJFilter)
                        {
                            key.Add(Value[str].Trim());
                        }
                        return key;
                    }
                    catch (Exception ex)
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

        /// <summary>
        /// 扫描件根目录
        /// </summary>
        private string RootDir
        {
            get;
            set;
        }

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
            private set
            {
                if (!Directory.Exists(value))
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[路径查询]", "不存在给定的路径:" + value);
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
            private set;
        } = new List<FileInfo>();

        /// <summary>
        /// 封面文件
        /// </summary>
        public List<FileInfo> FMFiles
        {
            get;
            private set;
        } = new List<FileInfo>();

        /// <summary>
        /// 目录文件
        /// </summary>
        public List<FileInfo> MLFiles
        {
            get;
            private set;
        } = new List<FileInfo>();

        /// <summary>
        /// 封底文件
        /// </summary>
        public List<FileInfo> FDFiles
        {
            get;
            private set;
        } = new List<FileInfo>();

        private string _PDFSavePath;
        /// <summary>
        /// PDF保存路径
        /// </summary>
        private string PDFSavePath
        {
            get
            {
                return _PDFSavePath;
            }
            set
            {
                if (!Directory.Exists(value))
                {
                    Directory.CreateDirectory(value);
                }
                _PDFSavePath = value;
            }
        }

        private int _SettingDPI;
        public int SettingDPI
        {
            get
            {
                return _SettingDPI;
            }
            set
            {
                _SettingDPI = value;
            }
        }

        #endregion

        #region 构造
        public AJEntity(Dictionary<string, string> _value, int _location, string _rootDir = null,
                                string _PageColumn = null, Dictionary<string, int> _DirRule = null)
        {
            this.Value = _value;
            this.Location = _location;
            this.PageColumn = _PageColumn;
            this.RootDir = _rootDir;
            this.DirRule = _DirRule;
        }
        #endregion

        #region 装载方法

        public void OneToManyAppend(string _JNCountColumn, List<string> _AJfilter, List<string> _JNFilter)
        {
            this.JNCountColumn = _JNCountColumn;
            this.AJFilter = _AJfilter;
            this.JNFilter = _JNFilter;
            this.IsOneToMany = true;
        }

        /// <summary>
        /// 装载卷内
        /// </summary>
        private void LoadJN()
        {
            List<JNEntity> entities = new List<JNEntity>(MainForm.CreateInstrance().jnEntities_List);
            List<string> JNColumnForFilter = JNFilter;
            if (JNColumnForFilter != null && JNColumnForFilter.Count != 0)
            {
                JNEntities = Commons.Clone<JNEntity>(entities.Where(jn => ListFind(jn, JNColumnForFilter, Key)).ToList());
                foreach (JNEntity jn in JNEntities)
                {
                    MainForm.CreateInstrance().jnEntities_List.Remove(jn);
                }
            }
        }
        public static bool ListFind(JNEntity jn, List<string> filter, List<string> value)
        {
            for (int i = 0; i < filter.Count; i++)
            {
                if (jn.Value[filter[i]] == value[i])
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 装载扫描件
        /// </summary>
        private void LoadFiles()
        {
            if (!PathLoaded)
            {
                LoadPath();
            }
            if (Dir != null)
            {
                try
                {
                    //指定目录
                    DirectoryInfo directoryInfo = new DirectoryInfo(Dir);
                    //指定类型的文件
                    List<FileInfo> files = directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly).
                        Where(s => s.Name.EndsWith(".jpg") || s.Name.EndsWith(".tif") || s.Name.EndsWith(".png")).ToList();
                    //根据文件名排序
                    IOrderedEnumerable<FileInfo> orderedEnumerable = files.OrderBy((FileInfo c) => c.Name);
                    List<FileInfo> fmFile = new List<FileInfo>();
                    List<FileInfo> mlFile = new List<FileInfo>();
                    List<FileInfo> fdFile = new List<FileInfo>();
                    List<FileInfo> scanFile = new List<FileInfo>();
                    foreach (FileInfo item in orderedEnumerable)
                    {
                        Bitmap bitmap = new Bitmap(item.FullName);
                        if(bitmap.VerticalResolution != SettingDPI || bitmap.HorizontalResolution != SettingDPI)
                        {
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), item.Name, "垂直DPI:" + bitmap.VerticalResolution + "||水平DPI:" + bitmap.HorizontalResolution);
                        }
                        bitmap.Dispose();
                        if (item.Name.Contains("FM") || item.Name.Contains("fm")) //封面
                        {
                            fmFile.Add(item);
                        }
                        else if (item.Name.Contains("ML") || item.Name.Contains("JN") || item.Name.Contains("ml") || item.Name.Contains("jn")) //卷内目录。
                        {
                            mlFile.Add(item);
                        }
                        else if (item.Name.Contains("BK") || item.Name.Contains("FD") || item.Name.Contains("fd") || item.Name.Contains("bk")) //备考表，封面。
                        {
                            fdFile.Add(item);
                        }
                        else //扫描的卷内目录图片文件。
                        {
                            scanFile.Add(item);
                        }
                    }
                    FMFiles = fmFile;
                    MLFiles = mlFile;
                    FDFiles = fdFile;
                    ScanFiles = scanFile;
                }
                catch (Exception ex)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "[LoadFiles]", ex.Message);
                }
            }
        }

        /// <summary>
        /// 装载路径
        /// </summary>
        private void LoadPath()
        {
            try
            {
                if (DirRule == null || DirRule.Count == 0)
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
                    Dir = RootDir + @"\" + string.Join(@"\", partPath.ToArray());
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

        #endregion

        #region 工作方法

        /// <summary>
        /// 检测页数
        /// </summary>
        public void MatchPages()
        {
            if (IsOneToMany)
            {
                if (!JNLoaded)
                {
                    LoadJN();
                }
                int jnpages = 0;
                foreach (JNEntity jn in JNEntities)
                {
                    if (jn.Pages > 0)
                    {
                        jnpages += jn.Pages;
                    }
                    else if (jn.Pages == -2)
                    {
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (jnpages != Pages)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "页数检测", "卷内总页数[" + jnpages + "]与案卷页数[" + Pages + "]不符");
                }
            }
        }

        /// <summary>
        /// 检测件数
        /// </summary>
        public void MatchCount()
        {
            if (IsOneToMany)
            {
                if (!JNLoaded)
                {
                    LoadJN();
                }
                if (JNEntities.Count != JNCount)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "件数检测", "卷内条目[" + JNEntities.Count + "]与案卷[" + JNCount + "]不符");
                }
            }
        }

        /// <summary>
        /// 检测扫描件
        /// </summary>
        public void MatchScanfile()
        {
            if (!FilesLoaded)
            {
                LoadFiles();
            }
            if (ScanFiles.Count != Pages)
            {
                MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location.ToString(), "扫描件检测", "扫描件数[" + ScanFiles.Count + "]与案卷页数[" + Pages + "]不符");
            }
        }

        /// <summary>
        /// 拼接保存路径
        /// </summary>
        /// <param name="SavePath">保存路径</param>
        /// <param name="partFolder">拼接文件夹名称</param>
        public void SplitPath(string SavePath, Dictionary<string, string> partFolder = null)
        {
            PDFSavePath = SavePath;
            if (partFolder != null)
            {
                List<string> partNames = new List<string>();
                foreach (string name in partFolder.Keys)
                {
                    partNames.Add(Value[name]);
                    if (!string.IsNullOrWhiteSpace(partFolder[name]))
                    {
                        partNames.Add(partFolder[name]);
                    }
                }
                string Name = string.Join("", partNames.ToArray());
                PDFSavePath = PDFSavePath + "\\" + Name;
            }
        }

        public void Merge(Dictionary<string, string> scanPartName, bool mergeAdditions, List<string> additionSort,
            bool appendToHead, Dictionary<string, string> additionPartName)
        {
            if (!FilesLoaded)
            {
                LoadFiles();
            }
            if (IsOneToMany)
            {
                if (!JNLoaded)
                {
                    LoadJN();
                }

                #region 合并附件
                if (mergeAdditions)
                {
                    try
                    {
                        //获取附件扫描件
                        List<string> fileNames = GetAdditionFileNames(additionSort);
                        //解析文件名
                        string Name = AnalysisNameRule(additionPartName, Value);

                        MergeUtil.MergeToPDF(fileNames, PDFSavePath + "\\" + Name + ".pdf");
                    }
                    catch (Exception ex)
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "附件合并", ex.Message);
                    }
                }
                #endregion

                #region 合并扫描件

                List<FileInfo> files = Commons.Clone<FileInfo>(ScanFiles);

                foreach (JNEntity jn in JNEntities)
                {
                    try
                    {
                        IEnumerable<FileInfo> list = new List<FileInfo>();
                        if (jn.Pages == -2)
                        {
                            continue;
                        }
                        //取出扫描件
                        if (files.Count < jn.Pages)
                        {
                            MainForm.CreateInstrance().WriteErrorInfo("JN行号:" + jn.Location.ToString(), "卷内扫描件合并", "剩余扫描件数量[" + files.Count + "]小于卷内页数[" + jn.Pages + "],将合并剩余页数");
                            list = files.Take(files.Count);
                        }
                        else
                        {
                            list = files.Take(jn.Pages);
                        }

                        // ScanFiles.RemoveRange(0, jn.Pages - 1);
                        List<string> fileNames = new List<string>();
                        foreach (FileInfo info in list)
                        {
                            fileNames.Add(info.FullName);
                        }

                        files.RemoveRange(0, fileNames.Count);
                        //解析文件名
                        string Name = AnalysisNameRule(scanPartName, jn.Value);

                        MergeUtil.MergeToPDF(fileNames, PDFSavePath + "\\"+Name + ".pdf");
                    }
                    catch (Exception ex)
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("JN行号:" + jn.Location.ToString(), "卷内扫描件合并", ex.Message);
                    }
                }
                #endregion
            }
            else
            {
                //需要合并附件
                if (mergeAdditions)
                {
                    List<string> additionFileNames = GetAdditionFileNames(additionSort);
                    //不追加到头部
                    if (!appendToHead)
                    {

                        #region 合并附件
                        try
                        {
                            //构建文件名称
                            string Name = AnalysisNameRule(additionPartName, Value);
                            MergeUtil.MergeToPDF(additionFileNames, PDFSavePath + "\\" + Name + ".pdf");
                        }
                        catch (Exception ex)
                        {
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "附件合并", ex.Message);
                        }
                        #endregion

                        #region 合并扫描件
                        try
                        {
                            //构建文件名称
                            string Name1 = AnalysisNameRule(scanPartName, Value);
                            //拉取扫描件路径
                            List<string> fileNames = new List<string>();
                            foreach (FileInfo info in ScanFiles)
                            {
                                fileNames.Add(info.FullName);
                            }
                            MergeUtil.MergeToPDF(fileNames, PDFSavePath + "\\" + Name1 + ".pdf");
                        }
                        catch (Exception ex)
                        {
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "扫描件合并", ex.Message);
                        }
                        #endregion
                    }
                    //追加到头部
                    else
                    {
                        #region 合并
                        try
                        {
                            //直接向附件List中追加正文路径
                            foreach (FileInfo info in ScanFiles)
                            {
                                additionFileNames.Add(info.FullName);
                            }
                            //构建文件名称
                            string Name = AnalysisNameRule(scanPartName, Value);
                            //合并
                            MergeUtil.MergeToPDF(additionFileNames, PDFSavePath + "\\" + Name + ".pdf");
                        }
                        catch (Exception ex)
                        {
                            MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "附件追加扫描件合并", ex.Message);
                        }
                        #endregion
                    }
                }
                else
                {
                    try
                    {
                        string Name = AnalysisNameRule(scanPartName, Value);
                        //拉取扫描件路径
                        List<string> fileNames = new List<string>();
                        foreach (FileInfo info in ScanFiles)
                        {
                            fileNames.Add(info.FullName);
                        }
                        MergeUtil.MergeToPDF(fileNames, PDFSavePath + "\\" + Name + ".pdf");

                    }
                    catch (Exception ex)
                    {
                        MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "仅扫描件合并", ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 根据附件排序方式拉取文件路径
        /// </summary>
        /// <param name="additionSort"></param>
        /// <returns></returns>
        public List<string> GetAdditionFileNames(List<string> additionSort)
        {
            try
            {
                List<string> additions = new List<string>();
                foreach (string str in additionSort)
                {
                    if (str == "封面")
                    {
                        foreach (FileInfo info in FMFiles)
                        {
                            additions.Add(info.FullName);
                        }
                    }
                    else if (str == "卷内")
                    {
                        foreach (FileInfo info in MLFiles)
                        {
                            additions.Add(info.FullName);
                        }
                    }
                    else if (str == "封底")
                    {
                        foreach (FileInfo info in FDFiles)
                        {
                            additions.Add(info.FullName);
                        }
                    }
                }
                return additions;
            }
            catch (Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "获取附件[GetAdditionFileNames]", ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 解析文件名规则
        /// </summary>
        /// <param name="NameRule"></param>
        /// <param name="ValuePool"></param>
        /// <returns></returns>
        public string AnalysisNameRule(Dictionary<string, string> NameRule, Dictionary<string, string> ValuePool)
        {
            try
            {
                List<string> nameParts = new List<string>();
                foreach (string name in NameRule.Keys)
                {
                    nameParts.Add(ValuePool[name]);
                    if (!string.IsNullOrWhiteSpace(NameRule[name]))
                    {
                        nameParts.Add(NameRule[name]);
                    }
                }
                if(nameParts.Last() == "-")
                {
                    nameParts.RemoveAt(nameParts.Count - 1);
                }
                string Name = string.Join("", nameParts.ToArray());
                return Name;
            }
            catch (Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("AJ行号:" + Location, "解析文件名规则[AnalysisNameRule]", ex.Message);
                return null;
            }
        }

        #endregion
    }
}

