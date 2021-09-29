using DataChecker_FilesMerger.Helper;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger.Entities
{
    class Data
    {
        public int Location { get; set; }

        public Dictionary<string, string> Value { get; set; }

        public Data(string parent, int _location, Dictionary<string, string> _value)
        {
            ParentPath = parent;
            this.Location = _location;
            this.Value = _value;
        }
        public Data(string filepath)
        {
            FilePath = filepath;
        }

        public string ParentPath { get; set; }

        private string _FilePath;
        private string FilePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_FilePath))
                {
                    return _FilePath;
                }
                else
                {
                    _FilePath = ParentPath + "\\" + Value["身份证号"].Trim();
                    return _FilePath;
                }
            }
            set
            {
                _FilePath = value;
            }
        }

        public static string saveParent;

        private string _SavePath;
        private string SavePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_SavePath))
                {
                    return _SavePath;
                }
                else
                {
                    if (!string.IsNullOrEmpty(saveParent))
                        _SavePath = saveParent + "\\" + Value["身份证号"].Trim();
                    else
                        _SavePath = ParentPath + "\\" + Value["身份证号"].Trim();
                    if (!Directory.Exists(_SavePath))
                        Directory.CreateDirectory(_SavePath);
                    return _SavePath;
                }
            }
            set
            {
                _SavePath = value;
            }
        }

        public void Work(string configPath)
        {
            if (!Directory.Exists(FilePath))
            {
                MainForm.CreateInstrance().WriteErrorInfo(this.Location.ToString(), "寻找路径", "路径不存在" + FilePath);
                return;
            }
            DirectoryInfo di = new DirectoryInfo(FilePath);
            FileInfo[] fis = di.GetFiles($"*.{"pdf"}");//文件类型
            int jn = 1;
            int attach = 1;
            int main = 1;
            DirectoryInfo s = new DirectoryInfo(SavePath);
            if (s.GetFiles($"*.{"jpg"}").Length == fis.Length)
                return;
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                if (IsMatch(fi.Name, @"[a-zA-z0-9-]{0,}-0\([0-9]{1}(.pdf){1}"))
                {
                    if (OcrHelper.OCR(fi.FullName, configPath, out Mat mat))
                    {
                        if (!File.Exists(SavePath + "\\" + "JN-" + attach + ".jpg"))
                        {
                            mat.ImWrite(SavePath + "\\" + "JN-" + jn + ".jpg");
                        }
                        mat.Dispose();
                        jn++;
                    }
                    else
                    {
                        if (attach == 1)
                        {
                            if(!File.Exists(SavePath + "\\" + "FM-" + attach + ".jpg"))
                            {
                                mat.ImWrite(SavePath + "\\" + "FM-" + attach + ".jpg");
                            }
                            mat.Dispose();
                            attach++;
                        }
                        else if (attach == 2)
                        {
                            if (!File.Exists(SavePath + "\\" + "FM-" + attach + ".jpg"))
                            {
                                mat.ImWrite(SavePath + "\\" + "FM-" + attach + ".jpg");
                            }
                            mat.Dispose();
                            attach++;
                        }
                        else if (attach == 3)
                        {
                            if (!File.Exists(SavePath + "\\" + "FD" + attach + ".jpg"))
                            {
                                mat.ImWrite(SavePath + "\\" + "FD" + attach + ".jpg");
                            }
                            mat.Dispose();
                        }
                    }
                }
                else
                {
                    if (!File.Exists(SavePath + "\\" + main.ToString().PadLeft(3, '0') + ".jpg"))
                    {
                        Stream stream = OcrHelper.ConvertPDF2Image(fi.FullName);
                        Mat mat = Mat.FromStream(stream, ImreadModes.AnyColor);
                        mat.ImWrite(SavePath + "\\" + main.ToString().PadLeft(3, '0') + ".jpg");
                        mat.Dispose();
                        stream.Dispose();
                    }
                    main++;
                }
            }
            GC.Collect();
        }

        public static bool IsMatch(string inputStr, string patternStr)
        {
            if (string.IsNullOrWhiteSpace(inputStr))//.NET 4.0 新增IsNullOrWhiteSpace 方法，便于对用户做处理
                return false;//如果不要求验证空白字符串而此时传入的待验证字符串为空白字符串，则不匹配  
            Regex regex = new Regex(patternStr);
            return regex.IsMatch(inputStr);
        }
    }
}
