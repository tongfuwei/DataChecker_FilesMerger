using O2S.Components.PDF4NET;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DataChecker_FilesMerger.Helper
{
    public class MergeUtil
    {
        /// <summary>
        /// 合并成PDF文件。
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="saveFileName"></param>
        public static void MergeToPDF(List<string> fileNames, string saveFileName)
        {
            if (fileNames.Count != 0)
            {
                PDFDocument pdfDocument;
                if (File.Exists(saveFileName))
                    pdfDocument = new PDFDocument(saveFileName);
                else
                    pdfDocument = new PDFDocument();
                foreach (string item in fileNames)
                {
                    pdfDocument.AddPage();
                    Bitmap bitmap = new Bitmap(item);
                    pdfDocument.Pages[pdfDocument.Pages.Count - 1].Width = bitmap.Width;
                    pdfDocument.Pages[pdfDocument.Pages.Count - 1].Height = bitmap.Height;
                    pdfDocument.Pages[pdfDocument.Pages.Count - 1].Canvas.DrawImage(bitmap, 0.0, 0.0, bitmap.Width, bitmap.Height);
                    bitmap.Dispose();
                }
                pdfDocument.Save(saveFileName);
            }
        }
    }
}
