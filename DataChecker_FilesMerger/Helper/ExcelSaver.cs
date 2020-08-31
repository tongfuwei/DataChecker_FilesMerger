using Aspose.Cells;
using System;
using System.Data;
using System.Windows.Forms;

namespace DataChecker_FilesMerger
{
    public class ExcelSaver
    {
        public static void SaveToExcel(ListView listview)
        {
            string path;
            SaveFileDialog filedialog = new SaveFileDialog();
            filedialog.Filter = "Excel file(*,xls)|*.xls";
            if (filedialog.ShowDialog() == DialogResult.OK)
            {
                path = filedialog.FileName;
                //新建excel
                Workbook wb = new Workbook();
                Worksheet ws = wb.Worksheets[0];
                Cells cell = ws.Cells;
                //储存数据到数组  
                string[,] _dataReport = new string[listview.Items.Count, listview.Columns.Count - 1];
                for (int i = 0; i < listview.Items.Count; i++)
                {
                    for (int j = 0; j < listview.Columns.Count - 1; j++)
                    {
                        _dataReport[i, j] = listview.Items[i].SubItems[j + 1].Text.ToString();
                    }
                }
                //标题style  
                cell.SetRowHeight(0, 40);
                Style style1 = wb.Styles[wb.Styles.Add()];
                style1.HorizontalAlignment = TextAlignmentType.Center;
                style1.Font.Name = "宋体";
                style1.Font.IsBold = true;
                style1.Font.Size = 14;
                //内容style  
                Style style2 = wb.Styles[wb.Styles.Add()];
                style2.HorizontalAlignment = TextAlignmentType.Center;
                style2.Font.Size = 12;
                //第一行内容
                for (int i = 0; i < listview.Columns.Count - 1; i++)
                {
                    cell[0, i].PutValue(listview.Columns[i + 1].Text);
                    cell[0, i].SetStyle(style1);
                }
                //第二行以后内容  
                int posStart = 1;
                for (int i = 0; i < listview.Items.Count; i++)
                {
                    for (int j = 0; j < listview.Columns.Count - 1; j++)
                    {
                        cell[i + posStart, j].PutValue(_dataReport[i, j].ToString());
                        cell[i + posStart, j].SetStyle(style2);
                    }
                }
                for (int i = 0; i < listview.Columns.Count - 1; i++)
                {
                    cell.SetColumnWidth(i, (i + 1) * 30);
                }
                //储存  
                try
                {
                    wb.Save(path);
                    MessageBox.Show("保存成功!");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public static void SaveToExcel(DataTable dataTable)
        {
            string path;
            SaveFileDialog filedialog = new SaveFileDialog();
            filedialog.Filter = "Excel file(*,xls)|*.xls";
            if (filedialog.ShowDialog() == DialogResult.OK)
            {
                path = filedialog.FileName;
                Workbook book = new Workbook();
                Worksheet sheet = book.Worksheets[0];
                Cells cells = sheet.Cells;
                int Colnum = dataTable.Columns.Count;//表格列数 
                int Rownum = dataTable.Rows.Count;//表格行数 
                for (int i = 0; i < Colnum; i++)//生成行 列名行 
                {
                    cells[0, i].PutValue(dataTable.Columns[i].ColumnName);
                }
                //生成数据行 
                for (int i = 0; i < Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        cells[1 + i, k].PutValue(dataTable.Rows[i][k].ToString());
                    }
                }
                try
                {
                    book.Save(path);
                    MessageBox.Show("保存成功!");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
