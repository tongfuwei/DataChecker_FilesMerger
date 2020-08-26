using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger
{
    class ExcelReader
    {
        private Workbook _workBook;

        private Worksheet _workSheet;

        /// <summary>
        /// <列名,索引号>
        /// </summary>
        public IndexDictionary<string> ExcelColumns
        {
            get; set;
        }

        /// <summary>
        /// sheets名称
        /// </summary>
        public List<string> SheetsName
        {
            get; set;
        }

        private bool Loaded = false;

        public Cells Cells
        {
            get
            {
                if(Loaded)
                {
                    return _workSheet.Cells;
                }
                else
                {                    
                    return null;
                }
            }
        }

        public void Load_Excel(string sFileName, int sheetIndex = 0)
        {
            try
            {
                SheetsName = new List<string>();
                _workBook = new Workbook(sFileName);
                foreach (var sheet in _workBook.Worksheets)
                {
                    SheetsName.Add(sheet.Name);
                }
                _workSheet = _workBook.Worksheets[sheetIndex];
                Loaded = true;
            }
            catch(Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("打开Excel", "[Load_Excel]", ex.Message);
            }
        }

        public void ResetSheet(string sheetName)
        {
            try
            {
                _workSheet = _workBook.Worksheets[sheetName];
                Loaded = true;
            }
            catch (Exception ex)
            {
                MainForm.CreateInstrance().WriteErrorInfo("打开Excel", "[Load_Excel]", ex.Message);
            }
        }

        /// <summary>
        /// 取出列名及列号
        /// </summary>
        /// <param name="columnNameRow"></param>      
        public void Caculate_Columns(int columnNameRow)
        {
            Cells cells = _workSheet.Cells;
            int num = cells.MaxDataColumn;
            this.ExcelColumns = new IndexDictionary<string>();

            for (int i = 0; i <= num; i++)
            {
                if (cells[columnNameRow, i].Value != null && (!string.IsNullOrWhiteSpace( cells[columnNameRow, i].Value.ToString())))
                {
                    string a = cells[columnNameRow, i].Value.ToString();
                    if (!ExcelColumns.Keys.Contains(a))
                        ExcelColumns.Add(a);
                }
            }
        }
    }
}

