using Aspose.Cells;
using Aspose.Words;
using Aspose.Words.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using ImageMagick;

namespace DataChecker_FilesMerger.Entities
{
    public class PersonalInfo
    {
        public int Location { get; set; }

        public Dictionary<string, string> Value { get; set; }

        #region 信息内容
        private string _Name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                if (_Name == null)
                {
                    if (Value.Keys.Contains("姓名"))
                    {
                        try
                        {
                            _Name = Value["姓名"].Trim();
                            return _Name;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取姓名出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "姓名为空");
                        _Name = "";
                        return "";
                    }
                }
                else
                {
                    return _Name;
                }
            }
        }

        private string _ID;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string ID
        {
            get
            {
                if (_ID == null)
                {
                    if (Value.Keys.Contains("身份证号"))
                    {
                        try
                        {
                            _ID = Value["身份证号"].Trim();
                            if (!(_ID.Length == 18 || _ID.Length == 15))
                            {
                                _Birth = _ID;
                                RemakeBirth();
                                _ID = _Birth;
                            }

                            if (string.IsNullOrWhiteSpace(_ID))
                                _ID = Birth;
                            return _ID;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取身份证号出现问题", ex.Message);
                            return null;
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(Birth))
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "不存在列名身份证号");
                        _ID = Birth;
                        return _ID;
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "身份证号为空");
                        _ID = "                  ";
                        return _ID;
                    }
                }
                else
                {
                    return _ID;
                }
            }
        }

        private void RemakeBirth()
        {

            if (_Birth.Contains('-'))
            {
                string[] str = _Birth.Split('-');
                if (str.Length == 3)
                {
                    BirthDay = str[2].PadLeft(2, '0');
                }
                if (str.Length >= 2)
                {
                    BirthMonth = str[1].PadLeft(2, '0');
                }
                BirthYear = str[0].PadLeft(4, '0');
            }
            else if (_Birth.Contains("年"))
            {
                string[] str = _Birth.Split('年');
                BirthYear = str[0].PadLeft(4, '0');
                if (_Birth.Contains("月"))
                {
                    string[] str1 = str[1].Split('月');
                    BirthMonth = str1[0].PadLeft(2, '0');
                    if (_Birth.Contains("日"))
                    {
                        string[] str2 = str[1].Split('日');
                        BirthMonth = str2[0].PadLeft(2, '0');
                    }
                }
            }
            else if (_Birth.Contains('/'))
            {
                string[] str = _Birth.Split('/');
                if (str.Length == 3)
                {
                    BirthDay = str[2].PadLeft(2, '0');
                    if (BirthDay.Length != 2)
                    {
                        string[] s = BirthDay.Split(' ');
                        BirthDay = s[0].PadLeft(2, '0');
                    }
                }
                if (str.Length >= 2)
                {
                    BirthMonth = str[1].PadLeft(2, '0');
                }
                BirthYear = str[0].PadLeft(4, '0');
            }
            else if (_Birth.Contains('.'))
            {
                string[] str = _Birth.Split('.');
                if (str.Length == 3)
                {
                    BirthDay = str[2].PadLeft(2, '0');
                }
                if (str.Length >= 2)
                {
                    BirthMonth = str[1].PadLeft(2, '0');
                }
                BirthYear = str[0].PadLeft(4, '0');
            }
            else
            {
                if (Birth.Length >= 4)
                {
                    BirthYear = Birth.Substring(0, 4);
                }
                if (Birth.Length >= 6)
                {
                    BirthMonth = Birth.Substring(4, 2);
                }
                if (Birth.Length >= 8)
                {
                    BirthDay = Birth.Substring(6, 2);
                }
            }

            _Birth = _BirthYear + _BirthMonth + _BirthDay;
            if (_Birth.Length != 8)
                _Birth = _Birth.PadLeft(8, '0');
        }

        private void MakePartDate()
        {
            if (_Retire == null)
            {
                if (Value.Keys.Contains("退休日期"))
                {
                    try
                    {
                        _Retire = Value["退休日期"].Trim();

                        if (_Retire.Contains('-'))
                        {
                            string[] str = _Retire.Split('-');
                            if (str.Length == 3)
                            {
                                RetireDay = str[2].PadLeft(2, '0');
                            }
                            if (str.Length >= 2)
                            {
                                RetireMonth = str[1].PadLeft(2, '0');
                            }
                            RetireYear = str[0].PadLeft(4, '0');
                        }
                        else if (_Retire.Contains('/'))
                        {
                            string[] str = _Retire.Split('/');
                            if (str.Length == 3)
                            {
                                RetireDay = str[2].PadLeft(2, '0');
                            }
                            if (str.Length >= 2)
                            {
                                RetireMonth = str[1].PadLeft(2, '0');
                            }
                            RetireYear = str[0].PadLeft(4, '0');
                        }
                        else if (_Retire.Contains('.'))
                        {
                            string[] str = _Retire.Split('.');
                            if (str.Length == 3)
                            {
                                RetireDay = str[2].PadLeft(2, '0');
                            }
                            if (str.Length >= 2)
                            {
                                RetireMonth = str[1].PadLeft(2, '0');
                            }
                            RetireYear = str[0].PadLeft(4, '0');
                        }
                        else
                        {
                            if (Retire.Length >= 4)
                            {
                                RetireYear = Retire.Substring(0, 4);
                            }
                            if (Retire.Length >= 6)
                            {
                                RetireMonth = Retire.Substring(4, 2);
                            }
                            if (Retire.Length >= 8)
                            {
                                RetireDay = Retire.Substring(6, 2);
                            }
                        }

                        _Retire = RetireYear + RetireMonth + RetireDay;
                    }
                    catch (Exception ex)
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取退休时间出现问题", ex.Message);
                    }
                }
                else
                {
                    PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "退休日期为空");
                    _Retire = "";
                }
            }


            if (_Dead == null)
            {
                if (Value.Keys.Contains("死亡日期"))
                {
                    try
                    {
                        _Dead = Value["死亡日期"].Trim();

                        if (_Dead.Contains('-'))
                        {
                            string[] str = _Dead.Split('-');
                            if (str.Length == 3)
                            {
                                DeadDay = str[2].PadLeft(2, '0');
                            }
                            if (str.Length >= 2)
                            {
                                DeadMonth = str[1].PadLeft(2, '0');
                            }
                            DeadYear = str[0].PadLeft(4, '0');
                        }
                        else if (_Dead.Contains('/'))
                        {
                            string[] str = _Dead.Split('/');
                            if (str.Length == 3)
                            {
                                DeadDay = str[2].PadLeft(2, '0');
                            }
                            if (str.Length >= 2)
                            {
                                DeadMonth = str[1].PadLeft(2, '0');
                            }
                            DeadYear = str[0].PadLeft(4, '0');
                        }
                        else if (_Dead.Contains('.'))
                        {
                            string[] str = _Dead.Split('.');
                            if (str.Length == 3)
                            {
                                DeadDay = str[2].PadLeft(2, '0');
                            }
                            if (str.Length >= 2)
                            {
                                DeadMonth = str[1].PadLeft(2, '0');
                            }
                            DeadYear = str[0].PadLeft(4, '0');
                        }
                        else
                        {
                            if (Dead.Length >= 4)
                            {
                                DeadYear = Dead.Substring(0, 4);
                            }
                            if (Dead.Length >= 6)
                            {
                                DeadMonth = Dead.Substring(4, 2);
                            }
                            if (Dead.Length >= 8)
                            {
                                DeadDay = Dead.Substring(6, 2);
                            }
                        }

                        _Dead = DeadYear + DeadMonth + DeadDay;
                    }
                    catch (Exception ex)
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取死亡时间出现问题", ex.Message);
                    }
                }
                else
                {
                    PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "死亡日期为空");
                    _Dead = "";
                }
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birth
        {
            get
            {
                if (_Birth == null)
                {
                    if (Value.Keys.Contains("出生日期"))
                    {
                        try
                        {
                            _Birth = Value["出生日期"].Trim();
                            RemakeBirth();

                            return _Birth;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取出生时间出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "出生日期为空");
                        _Birth = "";
                        return "";
                    }
                }
                else
                {
                    return _Birth;
                }
            }
        }

        private string _Retire;
        /// <summary>
        /// 退休时间
        /// </summary>
        public string Retire
        {
            get
            {
                if (_Retire == null)
                {
                    if (Value.Keys.Contains("退休日期"))
                    {
                        try
                        {
                            _Retire = Value["退休日期"].Trim();

                            if (_Retire.Contains('-'))
                            {
                                string[] str = _Retire.Split('-');
                                if (str.Length == 3)
                                {
                                    RetireDay = str[2].PadLeft(2, '0');
                                }
                                if (str.Length >= 2)
                                {
                                    RetireMonth = str[1].PadLeft(2, '0');
                                }
                                RetireYear = str[0].PadLeft(4, '0');
                            }
                            else if (_Retire.Contains('/'))
                            {
                                string[] str = _Retire.Split('/');
                                if (str.Length == 3)
                                {
                                    RetireDay = str[2].PadLeft(2, '0');
                                }
                                if (str.Length >= 2)
                                {
                                    RetireMonth = str[1].PadLeft(2, '0');
                                }
                                RetireYear = str[0].PadLeft(4, '0');
                            }
                            else if (_Retire.Contains('.'))
                            {
                                string[] str = _Retire.Split('.');
                                if (str.Length == 3)
                                {
                                    RetireDay = str[2].PadLeft(2, '0');
                                }
                                if (str.Length >= 2)
                                {
                                    RetireMonth = str[1].PadLeft(2, '0');
                                }
                                RetireYear = str[0].PadLeft(4, '0');
                            }
                            else
                            {
                                if (Retire.Length >= 4)
                                {
                                    RetireYear = Retire.Substring(0, 4);
                                }
                                if (Retire.Length >= 6)
                                {
                                    RetireMonth = Retire.Substring(4, 2);
                                }
                                if (Retire.Length >= 8)
                                {
                                    RetireDay = Retire.Substring(6, 2);
                                }
                            }

                            _Retire = _RetireYear + _RetireMonth + _RetireDay;

                            return _Retire;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取退休时间出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "退休日期为空");
                        _Retire = "";
                        return "";
                    }
                }
                else
                {
                    return _Retire;
                }
            }
        }

        private string _Dead;
        /// <summary>
        /// 死亡日期
        /// </summary>
        public string Dead
        {
            get
            {
                if (_Dead == null)
                {
                    if (Value.Keys.Contains("死亡日期"))
                    {
                        try
                        {
                            _Dead = Value["死亡日期"].Trim();

                            if (_Dead.Contains('-'))
                            {
                                string[] str = _Dead.Split('-');
                                if (str.Length == 3)
                                {
                                    DeadDay = str[2].PadLeft(2, '0');
                                }
                                if (str.Length >= 2)
                                {
                                    DeadMonth = str[1].PadLeft(2, '0');
                                }
                                DeadYear = str[0].PadLeft(4, '0');
                            }
                            else if (_Dead.Contains('/'))
                            {
                                string[] str = _Dead.Split('/');
                                if (str.Length == 3)
                                {
                                    DeadDay = str[2].PadLeft(2, '0');
                                }
                                if (str.Length >= 2)
                                {
                                    DeadMonth = str[1].PadLeft(2, '0');
                                }
                                DeadYear = str[0].PadLeft(4, '0');
                            }
                            else if (_Dead.Contains('.'))
                            {
                                string[] str = _Dead.Split('.');
                                if (str.Length == 3)
                                {
                                    DeadDay = str[2].PadLeft(2, '0');
                                }
                                if (str.Length >= 2)
                                {
                                    DeadMonth = str[1].PadLeft(2, '0');
                                }
                                DeadYear = str[0].PadLeft(4, '0');
                            }
                            else
                            {
                                if (Dead.Length >= 4)
                                {
                                    DeadYear = Dead.Substring(0, 4);
                                }
                                if (Dead.Length >= 6)
                                {
                                    DeadMonth = Dead.Substring(4, 2);
                                }
                                if (Dead.Length >= 8)
                                {
                                    DeadDay = Dead.Substring(6, 2);
                                }
                            }

                            _Dead = _DeadYear + _DeadMonth + _DeadDay;

                            return _Dead;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取死亡时间出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "死亡日期为空");
                        _Dead = "";
                        return "";
                    }
                }
                else
                {
                    return _Dead;
                }
            }
        }

        private string _Company;
        /// <summary>
        /// 单位
        /// </summary>
        public string Company
        {
            get
            {
                if (_Company == null)
                {
                    if (Value.Keys.Contains("所属单位"))
                    {
                        try
                        {
                            _Company = Value["所属单位"].Trim();
                            return _Company;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取单位出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "单位为空");
                        _Company = "";
                        return "";
                    }
                }
                else
                {
                    return _Company;
                }
            }
        }

        private string _ArchID;
        private string _Birth;

        public string ArchID
        {
            get
            {
                if (_ArchID == null)
                {
                    if (Value.Keys.Contains("档案编号"))
                    {
                        try
                        {
                            _ArchID = Value["档案编号"].Trim();
                            return _ArchID;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取档案编号出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "档案编号为空");
                        _ArchID = "    ";
                        return _ArchID;
                    }
                }
                else
                {
                    return _ArchID;
                }
            }
        }

        public string WorkID
        {
            get
            {
                if (_WorkID == null)
                {
                    if (Value.Keys.Contains("工号"))
                    {
                        try
                        {
                            _WorkID = Value["工号"].Trim();
                            return _WorkID;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取工号出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "工号为空");
                        _WorkID = "      ";
                        return _WorkID;
                    }
                }
                else
                {
                    return _WorkID;
                }
            }
        }

        private string _SoID = "﹍﹍﹍﹍﹍﹍";
        public string SoID
        {
            get
            {
                if (_SoID == "﹍﹍﹍﹍﹍﹍")
                {
                    if (Value.Keys.Contains("社保账号"))
                    {
                        try
                        {
                            _SoID = Value["社保账号"].Trim();
                            return _SoID;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取社保账号出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "社保账号为空");
                        return _SoID;
                    }
                }
                else
                {
                    return _SoID;
                }
            }
        }

        /// <summary>
        /// 户口
        /// </summary>
        public string Household { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        private string _Live;
        public string Live
        {
            get
            {
                if (Value.Keys.Contains("生存状态"))
                {
                    try
                    {
                        _Live = Value["生存状态"].Trim();
                        return _Live;
                    }
                    catch (Exception ex)
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取生存状态出现问题", ex.Message);
                        return "";
                    }
                }
                else
                {
                    PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "生存状态为空");
                    _Live = "";
                    return _Live;
                }
            }
            set
            {
                Live = value;
            }
        }
        #endregion

        public PersonalInfo(int _location, Dictionary<string, string> _value)
        {
            this.Location = _location;
            this.Value = _value;
        }

        #region 日期分字段

        private string _RetireYear = "0000";
        private string RetireYear
        {
            get
            {
                if (_RetireYear == "0000")
                {
                    var temp = Retire;
                }
                return _RetireYear;
            }
            set
            {
                _RetireYear = value;
            }
        }

        private string _RetireMonth = "00";
        string RetireMonth
        {
            get
            {
                if (_RetireMonth == "00")
                {
                    var temp = Retire;
                }
                return _RetireMonth;
            }
            set
            {
                _RetireMonth = value;
            }
        }

        private string _RetireDay = "00";
        string RetireDay
        {
            get
            {
                if (_RetireDay == "00")
                {
                    var temp = Retire;
                }
                return _RetireDay;
            }
            set
            {
                _RetireDay = value;
            }
        }

        private string _DeadYear = "0000";
        private string DeadYear
        {
            get
            {
                if (_DeadYear == "0000")
                {
                    var temp = Dead;
                }
                return _DeadYear;
            }
            set
            {
                _DeadYear = value;
            }
        }

        private string _DeadMonth = "00";
        string DeadMonth
        {
            get
            {
                if (_DeadMonth == "00")
                {
                    var temp = Dead;
                }
                return _DeadMonth;
            }
            set
            {
                _DeadMonth = value;
            }
        }

        private string _DeadDay = "00";
        string DeadDay
        {
            get
            {
                if (_DeadDay == "00")
                {
                    var temp = Dead;
                }
                return _DeadDay;
            }
            set
            {
                _DeadDay = value;
            }
        }


        private string _BirthYear = "0000";
        private string BirthYear
        {
            get
            {
                if (_BirthYear == "0000")
                {
                    var temp = Birth;
                }
                return _BirthYear;
            }
            set
            {
                _BirthYear = value;
            }
        }

        private string _BirthMonth = "00";
        string BirthMonth
        {
            get
            {
                if (_BirthMonth == "00")
                {
                    var temp = Birth;
                }
                return _BirthMonth;
            }
            set
            {
                _BirthMonth = value;
            }
        }

        private string _BirthDay = "00";
        private string _WorkID;

        string BirthDay
        {
            get
            {
                if (_BirthDay == "00")
                {
                    var temp = Birth;
                }
                return _BirthDay;
            }
            set
            {
                _BirthDay = value;
            }
        }

        #endregion


        public string Turn2Check(string modePath, string savePath, Aspose.Cells.SaveFormat saveFormat = Aspose.Cells.SaveFormat.Xlsx)
        {
            FontSettings.DefaultInstance.SetFontsFolder(@"C:\WINDOWS\Fonts", true);
            Workbook mode = new Workbook(modePath);
            Worksheet sheet = mode.Worksheets[0];
            //姓名B2
            sheet.Cells[1, 1].PutValue(Name);
            //身份证号E2
            sheet.Cells[1, 4].PutValue(ID);
            //公司B3
            sheet.Cells[2, 1].PutValue(Company);
            //档案编号E3
            if (string.IsNullOrEmpty(ArchID))
                sheet.Cells[2, 4].PutValue(Value["序号"]);
            else
                sheet.Cells[2, 4].PutValue(ArchID);


            if (RetireYear == "0000")
                RetireYear = "﹍﹍﹍";
            if (RetireMonth == "00")
                RetireMonth = "﹍﹍﹍";
            if (RetireDay == "00")
                RetireDay = "﹍﹍﹍";

            //退休日期C5
            sheet.Cells[4, 2].PutValue(string.Format("1、退休日期：{0}年{1}月{2}日（社保账号{3}）", RetireYear, RetireMonth, RetireDay, SoID));

            if (DeadYear == "0000")
                DeadYear = "﹍﹍﹍";
            if (DeadMonth == "00")
                DeadMonth = "﹍﹍﹍";
            if (DeadDay == "00")
                DeadDay = "﹍﹍﹍";

            //死亡日期C6
            sheet.Cells[5, 2].PutValue(string.Format("2、死亡日期：{0}年{1}月{2}日 3、无去向 □ 4、无 □", DeadYear, DeadMonth, DeadDay));
            sheet.Cells[26, 4].PutValue(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            string forMat = ".xlsx";
            if (saveFormat == Aspose.Cells.SaveFormat.Pdf)
                forMat = ".pdf";
            string fileName = savePath + "\\"+"核查" + ArchID + "-" + Name + forMat;
            try
            {
                mode.Save(fileName,saveFormat);
                return fileName;
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "Turn2Check", e.Message);
                return string.Empty;
            }
        }

        public string Turn2Licen(string modePath, string savePath, bool jianjie, bool retire, bool workID,Aspose.Words.SaveFormat saveFormat=Aspose.Words.SaveFormat.Doc)
        {
            Aspose.Words.Document doc = new Aspose.Words.Document(modePath);
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.MoveToBookmark("name");
            builder.Underline = Underline.Single;
            builder.Write(Name);
            builder.MoveToBookmark("ID");
            builder.Underline = Underline.Single;
            builder.Write(ID);
            builder.MoveToBookmark("ArID");
            builder.Underline = Underline.Single;
            builder.Write(ArchID);
            builder.MoveToBookmark("company1");
            builder.Write(Company);
            builder.MoveToBookmark("date");
            builder.Write(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            if (retire)
            {
                builder.MoveToBookmark("retire");
                if (RetireDay == "﹍﹍﹍" || RetireDay == "00")
                    builder.Write(string.Format("{0}年{1}月", RetireYear, RetireMonth));
                else
                    builder.Write(string.Format("{0}年{1}月{2}日", RetireYear, RetireMonth, RetireDay));
            }
            if (jianjie)
            {
                builder.MoveToBookmark("birth");
                if (BirthDay == "﹍﹍﹍" || BirthDay == "00")
                    builder.Write(string.Format("{0}年{1}月", BirthYear, BirthMonth));
                else
                    builder.Write(string.Format("{0}年{1}月{2}日", BirthYear, BirthMonth, BirthDay));
                builder.MoveToBookmark("retire");
                if (RetireDay == "﹍﹍﹍" || RetireDay == "00")
                    builder.Write(string.Format("{0}年{1}月", RetireYear, RetireMonth));
                else
                    builder.Write(string.Format("{0}年{1}月{2}日", RetireYear, RetireMonth, RetireDay));


                builder.MoveToBookmark("company2");
                builder.Write(Company);
            }
            if (workID)
            {
                builder.MoveToBookmark("workID");
                builder.Underline = Underline.Single;
                builder.Write(WorkID);
            }
            string forMat = ".doc";
            if (saveFormat == Aspose.Words.SaveFormat.Pdf)
                forMat = ".pdf";
            string fileName = savePath + "\\"+"情况-" + ArchID + "-" + Name + forMat;
            try
            {
                doc.Save(fileName,saveFormat);
                return fileName;
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "Turn2Licen", e.Message);
                return string.Empty;
            }
        }

        public string UnArchiveLicences(string modePath, string savePath,Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc)
        {
            FontSettings.DefaultInstance.SetFontsFolder(@"C:\WINDOWS\Fonts", true);
            Aspose.Words.Document doc = new Aspose.Words.Document(modePath);
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.MoveToBookmark("name");
            builder.Underline = Underline.Single;
            builder.Write(Name);
            builder.MoveToBookmark("ID");
            builder.Underline = Underline.Single;
            builder.Write(ID);
            builder.MoveToBookmark("ArID");
            builder.Underline = Underline.Single;
            builder.Write(ArchID);
            builder.MoveToBookmark("company1");
            builder.Write(Company);
            builder.MoveToBookmark("date");
            builder.Write(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            string forMat = ".doc";
            if (saveFormat == Aspose.Words.SaveFormat.Pdf)
                forMat = ".pdf";
            string fileName = savePath + "\\"+"无档" + ArchID + "-" + Name + forMat;
            try
            {                
                doc.Save(fileName,saveFormat);
                return fileName;
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "Turn2Licen", e.Message);
                return string.Empty;
            }
        }

        public string DeadInfo(string modePath, string savePath, bool workID,Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc)
        {
            //if (Live != "死亡")
            //    return ;
            FontSettings.DefaultInstance.SetFontsFolder(@"C:\WINDOWS\Fonts", true);
            Aspose.Words.Document doc = new Aspose.Words.Document(modePath);
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.MoveToBookmark("name");
            builder.Underline = Underline.Single;
            builder.Write(Name);
            builder.MoveToBookmark("ID");
            builder.Underline = Underline.Single;
            builder.Write(ID);
            builder.MoveToBookmark("ArID");
            builder.Underline = Underline.Single;
            builder.Write(ArchID);
            builder.MoveToBookmark("company1");
            builder.Write(Company);
            builder.MoveToBookmark("company2");
            builder.Write(Company);
            builder.MoveToBookmark("dead");
            if (workID)
            {
                builder.MoveToBookmark("workID");
                builder.Underline = Underline.Single;
                builder.Write(WorkID);
            }
            if (Dead == "00000000")
            { builder.Write("        "); }
            else if (DeadDay == "00")
                builder.Write(string.Format("{0}年{1}月", DeadYear, DeadMonth));
            else
                builder.Write(string.Format("{0}年{1}月{2}日", DeadYear, DeadMonth, DeadDay));
            builder.MoveToBookmark("date");
            builder.Write(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            string forMat = ".doc";
            if (saveFormat == Aspose.Words.SaveFormat.Pdf)
                forMat = ".pdf";
            string fileName = savePath + "//" + ArchID + "-" + Name+"死亡" + forMat;
            try
            {
                doc.Save(fileName,saveFormat);
                return fileName;
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "", e.Message);
                return null;
            }
        }

        public string RetireInfo(string modePath, string savePath, bool workID, Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc)
        {
            FontSettings.DefaultInstance.SetFontsFolder(@"C:\WINDOWS\Fonts", true);
            Aspose.Words.Document doc = new Aspose.Words.Document(modePath);
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            builder.MoveToBookmark("name");
            builder.Underline = Underline.Single;
            builder.Write(Name);
            builder.MoveToBookmark("ID");
            builder.Underline = Underline.Single;
            builder.Write(ID);
            builder.MoveToBookmark("ArID");
            builder.Underline = Underline.Single;
            builder.Write(ArchID);
            builder.MoveToBookmark("company1");
            builder.Write(Company);
            builder.MoveToBookmark("company2");
            builder.Write(Company);
            builder.MoveToBookmark("retire");
            if (Retire == "00000000")
            { builder.Write("        "); }
            else if (RetireDay == "00")
                builder.Write(string.Format("{0}年{1}月", RetireYear, RetireMonth));
            else
                builder.Write(string.Format("{0}年{1}月{2}日", RetireYear, RetireMonth, RetireDay));
            builder.MoveToBookmark("date");
            builder.Write(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            if (workID)
            {
                builder.MoveToBookmark("workID");
                builder.Underline = Underline.Single;
                builder.Write(WorkID);
            }
            string forMat = ".doc";
            if (saveFormat == Aspose.Words.SaveFormat.Pdf)
                forMat = ".pdf";
            string fileName = savePath + "//" + ArchID + "-" + Name +"退休"+ forMat;
            try
            {
                doc.Save(fileName,saveFormat);
                return fileName;
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "", e.Message);
                return null;
            }
        }

        public void Convert2PDF(string excelPath,string UnarchiveMode,string LicenceMode, string savePath)
        {
            string excelPdf = Turn2Check(excelPath, savePath,Aspose.Cells.SaveFormat.Pdf);
            string path = savePath+"\\"+"图片" + "\\" + Value["案卷号"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            ConvertPDF2Image(excelPdf,path  + "\\" + "JN-1.jpg");
            string UnArchivePdf = UnArchiveLicences(UnarchiveMode, savePath, Aspose.Words.SaveFormat.Pdf);
            ConvertPDF2Image(UnArchivePdf, path + "\\" + "001.jpg");

            string LicencePdf = UnArchiveLicences(LicenceMode, savePath, Aspose.Words.SaveFormat.Pdf);
            ConvertPDF2Image(excelPdf, path + "\\" + "002.jpg");
        }

        public static void ConvertPDF2Image(string pdfInputPath, string saveName)
        {
            MagickNET.SetGhostscriptDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            MagickReadSettings settings = new MagickReadSettings();
            settings.Density = new Density(300, 300); //设置格式
            MagickImageCollection images = new MagickImageCollection();
            images.Read(pdfInputPath, settings);
            if (images.Count == 0)
                return;
            MagickImage image = (MagickImage)images[0];
            if (image.HasAlpha)
                image.Alpha(AlphaOption.Background);
            image.Format = MagickFormat.Jpg;
            image.Write(saveName);
            images.Dispose();
        }

        public void InsertPDF( string UnarchiveModel,string RetireModel, string DeadModel, string savePath,string pdfModel)
        {
            string UnArchivePdf = UnArchiveLicences(UnarchiveModel, savePath, Aspose.Words.SaveFormat.Pdf);
            string RetirePdf = RetireInfo(RetireModel, savePath, false, Aspose.Words.SaveFormat.Pdf);
            string DeadPdf= DeadInfo(DeadModel,savePath,false, Aspose.Words.SaveFormat.Pdf);

            Aspose.Pdf.Document pdfdoc1 = new Aspose.Pdf.Document(UnArchivePdf);
            Aspose.Pdf.Document pdfdoc2 = new Aspose.Pdf.Document(RetirePdf);
            Aspose.Pdf.Document pdfdoc3 = new Aspose.Pdf.Document(DeadPdf);
            if (File.Exists(pdfModel))
            {
                Aspose.Pdf.Document result;
                result = new Aspose.Pdf.Document(pdfModel);
                result.Pages.Add(pdfdoc1.Pages);
                result.Pages.Add(pdfdoc2.Pages);
                result.Pages.Add(pdfdoc3.Pages);
                result.Save(pdfModel);
            }
            else
            {
                pdfdoc1.Pages.Add(pdfdoc2.Pages);
                pdfdoc1.Pages.Add(pdfdoc3.Pages);
                pdfdoc1.Save(pdfModel);
            }

            //pdfEditor.Append(pdfModel,strings,1,3, pdfModel);
        }

        public void BuildPDF(string UnarchiveModel, string CheckTable, string LicenceModel, string savePath, string pdfModel)
        {

            string LicenPdf = Turn2Licen(LicenceModel, savePath, false, false, false, Aspose.Words.SaveFormat.Pdf);
            string CheckPdf = Turn2Check(CheckTable, savePath, Aspose.Cells.SaveFormat.Pdf);
            Aspose.Pdf.Document pdfdoc1 = new Aspose.Pdf.Document(CheckPdf);
            Aspose.Pdf.Document pdfdoc2 = new Aspose.Pdf.Document(LicenPdf);
            Aspose.Pdf.Document pdfdoc3 = new Aspose.Pdf.Document();
            bool unarchive = Value["备注"].ToString() == "无档";
            if (unarchive)
            {
                string UnArchivePdf = UnArchiveLicences(UnarchiveModel, savePath, Aspose.Words.SaveFormat.Pdf);
                pdfdoc3 = new Aspose.Pdf.Document(UnArchivePdf);
            }

            if (File.Exists(pdfModel))
            {
                Aspose.Pdf.Document result;
                result = new Aspose.Pdf.Document(pdfModel);
                result.Pages.Add(pdfdoc1.Pages);
                result.Pages.Add(pdfdoc2.Pages);
                if (unarchive)
                    result.Pages.Add(pdfdoc3.Pages);
                result.Save(pdfModel);
            }
            else
            {
                pdfdoc1.Pages.Add(pdfdoc2.Pages);
                if (unarchive)
                    pdfdoc1.Pages.Add(pdfdoc3.Pages);
                pdfdoc1.Save(pdfModel);
            }

            //pdfEditor.Append(pdfModel,strings,1,3, pdfModel);
        }
    }
}
