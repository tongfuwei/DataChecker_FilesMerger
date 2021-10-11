using DataChecker_FilesMerger.Helper;
using OpenCvSharp;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
                                                       //DirectoryInfo s = new DirectoryInfo(SavePath);
            int jn = 1;
            int attach = 1;
            int main = 1;
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                if (Commons.IsMatch(fi.Name, @"[a-zA-z0-9-]{0,}-0\([0-9]{1}(.pdf){1}"))
                {
                    if (OcrHelper.OCR(fi.FullName, SavePath + "\\temp.jpg", configPath, attach))
                    {
                        if (attach == 1 || attach == 2)
                        {
                            if (!File.Exists(SavePath + "\\" + "FM-" + attach + ".jpg"))
                            {
                                File.Move(SavePath + "\\temp.jpg", SavePath + "\\" + "FM-" + attach + ".jpg");
                            }
                        }
                        else if (attach == 3)
                        {
                            if (!File.Exists(SavePath + "\\" + "FD.jpg"))
                            {
                                File.Move(SavePath + "\\temp.jpg", SavePath + "\\" + "FD.jpg");
                            }
                        }
                        attach++;
                    }
                    else
                    {
                        if (!File.Exists(SavePath + "\\" + "JN-" + jn + ".jpg"))
                        {
                            File.Move(SavePath + "\\temp.jpg", SavePath + "\\" + "JN-" + jn + ".jpg");
                        }
                        jn++;
                    }
                }
                else
                {
                    if (!File.Exists(SavePath + "\\" + main.ToString().PadLeft(3, '0') + ".jpg"))
                    {
                        OcrHelper.RenderPage(fi.FullName, SavePath + "\\" + main.ToString().PadLeft(3, '0') + ".jpg");
                    }
                    main++;
                }
            }
            GC.Collect();
        }

       

    }
}
