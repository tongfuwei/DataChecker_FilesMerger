using System;
using System.Windows.Forms;

namespace DataChecker_FilesMerger
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(MainForm.CreateInstrance());
            Application.Run(MainForm.CreateInstrance());
            //Application.Run(new Form1());
        }
    }
}
