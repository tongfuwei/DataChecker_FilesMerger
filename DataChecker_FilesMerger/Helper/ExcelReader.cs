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
        /// 列名及其所在行号
        /// </summary>
        public Dictionary<string, int> ExcelColumns
        {
            get; set;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public List<string> SheetsName
        {
            get;set;
        }


        /// <summary>
        /// 将excel转换为datatable
        /// </summary>
        public DataTable Table
        {
            get
            {
                Cells cells = _workSheet.Cells;
                DataTable dataTable = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxColumn, true);
                return dataTable;
            }
        }

        public Cells Load_Excel(string sFileName, int sheetIndex = 0)
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

                return _workSheet.Cells;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Cells Load_Excel(string sFileName, string sheetName)
        {
            try
            {
                _workBook = new Workbook(sFileName);
                _workSheet = _workBook.Worksheets[sheetName];

                return _workSheet.Cells;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 取出列名及列号
        /// </summary>
        /// <param name="iStartRow"></param>      
        public void Caculate_Columns(int iStartRow)
        {
            try
            {
                Cells cells = _workSheet.Cells;
                int row = 0;
                int row2 = cells.MaxDataRow;
                int column = 0;
                int num = cells.MaxDataColumn;
                if (num < 4)
                {
                    num = 4;
                }

                this.ExcelColumns = new Dictionary<string, int>();
                for (int i = column; i <= num; i++)
                {
                    try
                    {
                        string a = (cells[iStartRow, i].Value == null) ? "" : cells[iStartRow, i].Value.ToString();
                        ExcelColumns.Add(a, i);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                //Commons.ShowMessage_Info(DialogType.Warring, ex.Message);
            }
        }
    }
}

