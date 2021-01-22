using Aspose.Cells;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    if (Value.Keys.Contains("身份证号码"))
                    {
                        try
                        {
                            _ID = Value["身份证号码"].Trim();
                            return _ID;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取身份证号出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "身份证号为空");
                        _ID = "";
                        return "";
                    }
                }
                else
                {
                    return _ID;
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
        public string Birth { get; set; }

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
                    if (Value.Keys.Contains("退休手续办理时间"))
                    {
                        try
                        {
                            _Retire = Value["退休手续办理时间"].Trim();
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
                            return _Dead;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取死亡日期出现问题", ex.Message);
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
        public string ArchID
        {
            get
            {
                if (_ArchID == null)
                {
                    if (Value.Keys.Contains("档案局档号"))
                    {
                        try
                        {
                            _ArchID = Value["档案局档号"].Trim();
                            return _ArchID;
                        }
                        catch (Exception ex)
                        {
                            PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "读取档号出现问题", ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        PersonnelChecklist.CreateInstrance().WriteErrorInfo("行号:" + Location.ToString(), "", "档号为空");
                        _ArchID = "";
                        return "";
                    }
                }
                else
                {
                    return _ArchID;
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
        #endregion

        public PersonalInfo(int _location,Dictionary<string,string> _value)
        {
            this.Location = _location;
            this.Value = _value;
        }

        public void Turn2Check(string modePath,string savePath)
        {
            Workbook mode = new Workbook(modePath);
            Worksheet sheet = mode.Worksheets[0];
            //姓名B2
            sheet.Cells[1, 1].PutValue(Name);
            //身份证号E2
            sheet.Cells[1, 4].PutValue(ID);
            //公司B3
            sheet.Cells[2, 1].PutValue(Company);
            //档案编号E3
            sheet.Cells[2, 4].PutValue(ArchID);
            string RetireYear = "﹍﹍﹍";
            if(Retire.Length >=4)
            {
                RetireYear = Retire.Substring(0, 4);
            }
            string RetireMonth = "﹍﹍";
            if (Retire.Length >= 6)
            {
                RetireMonth = Retire.Substring(4, 2);
            }
            string RetireDay  = "﹍﹍";
            if (Retire.Length>=8)
            {
                RetireDay = Retire.Substring(6, 2);
            }
            //退休日期C5
            sheet.Cells[4, 2].PutValue(string.Format("1、退休日期：{0}年{1}月{2}日（社保账号﹍﹍﹍﹍﹍﹍）", RetireYear, RetireMonth, RetireDay));

            string DeadYear = "﹍﹍﹍";
            if (Dead.Length >= 4)
            {
                DeadYear = Dead.Substring(0, 4);
            }
            string DeadMonth = "﹍﹍";
            if (Dead.Length >= 6)
            {
                DeadMonth = Dead.Substring(4, 2);
            }
            string DeadDay = "﹍﹍";
            if (Dead.Length >= 8)
            {
                DeadDay = Dead.Substring(6, 2);
            }

            //死亡日期C6
            sheet.Cells[5, 2].PutValue(string.Format("2、死亡日期：{0}年{1}月{2}日 3、无去向 □ 4、无 □",DeadYear,DeadMonth,DeadDay));
            sheet.Cells[26, 4].PutValue(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            try
            {
                mode.Save(savePath + "//" + Value["序号"] + "-" + Name + ".xls");
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "Turn2Check", e.Message);
            }
        }

        public void Turn2Licen(string modePath,string savePath)
        {
            Document doc = new Document(modePath);
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.MoveToBookmark("name");
            builder.Underline = Underline.Single;
            builder.Write(Name);
            builder.MoveToBookmark("ID");
            builder.Underline = Underline.Single;
            builder.Write(ID);
            builder.MoveToBookmark("company");
            builder.Write(Company);
            builder.MoveToBookmark("date");
            builder.Write(string.Format("{0}年{1}月{2}日", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString()));
            try
            {
                doc.Save(savePath + "//" + Value["序号"] + "-" + Name + ".doc");
            }
            catch (Exception e)
            {
                PersonnelChecklist.CreateInstrance().WriteErrorInfo(Location.ToString(), "Turn2Licen", e.Message);
            }
        }
    }
}
