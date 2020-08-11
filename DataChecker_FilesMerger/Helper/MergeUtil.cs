using O2S.Components.PDF4NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger.Helper
{
    public class MergeUtil
    {
        /// <summary>
        /// 合并成PDF文件。
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="sSaveFileName"></param>
        public static void MergeToPDF(List<string> fileNames, string sSaveFileName)
        {
            PDFDocument pDFDocument = new PDFDocument();
            foreach (string item4 in fileNames)
            {
                pDFDocument.AddPage();
                Bitmap bitmap = new Bitmap(item4);
                pDFDocument.Pages[pDFDocument.Pages.Count - 1].Width = bitmap.Width;
                pDFDocument.Pages[pDFDocument.Pages.Count - 1].Height = bitmap.Height;
                pDFDocument.Pages[pDFDocument.Pages.Count - 1].Canvas.DrawImage(bitmap, 0.0, 0.0, bitmap.Width, bitmap.Height);
                bitmap.Dispose();
            }
            pDFDocument.Save(sSaveFileName);
        }
    }
}
