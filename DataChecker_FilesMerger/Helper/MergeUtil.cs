using O2S.Components.PDF4NET;
using System.Collections.Generic;
using System.Drawing;

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
            if (fileNames.Count != 0)
            {
                PDFDocument pDFDocument = new PDFDocument();
                foreach (string item in fileNames)
                {
                    pDFDocument.AddPage();
                    Bitmap bitmap = new Bitmap(item);
                    pDFDocument.Pages[pDFDocument.Pages.Count - 1].Width = bitmap.Width;
                    pDFDocument.Pages[pDFDocument.Pages.Count - 1].Height = bitmap.Height;
                    pDFDocument.Pages[pDFDocument.Pages.Count - 1].Canvas.DrawImage(bitmap, 0.0, 0.0, bitmap.Width, bitmap.Height);
                    bitmap.Dispose();
                }
                pDFDocument.Save(sSaveFileName);
            }
        }

        public static void MergeToPDF(List<Bitmap> Files,string SaveFileName)
        {
            if (Files.Count != 0)
            {
                PDFDocument PDF = new PDFDocument();
                foreach (Bitmap item in Files)
                {
                    PDF.AddPage();
                    PDF.Pages[PDF.Pages.Count - 1].Width = item.Width;
                    PDF.Pages[PDF.Pages.Count - 1].Height = item.Height;
                    PDF.Pages[PDF.Pages.Count - 1].Canvas.DrawImage(item, 0.0, 0.0, item.Width, item.Height);
                }
                PDF.Save(SaveFileName);
            }
        }
    }
}
