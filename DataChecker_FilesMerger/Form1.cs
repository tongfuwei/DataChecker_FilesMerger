using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DataChecker_FilesMerger.Helper.ThreadHelper;

namespace DataChecker_FilesMerger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class DownLoadFile
        {
            public string FileName { get; set; }

            public string a { get; set; }

            public void Append()
            {

                this.a = this.FileName + "a";
            }
        }
        public delegate void InvokeMsg0(DownLoadFile x);
        public void ShowOneStartMsg(DownLoadFile x)
        {
            if (this.tb5.InvokeRequired)
            {
                InvokeMsg0 msgCallback = new InvokeMsg0(ShowOneStartMsg);
                tb5.Invoke(msgCallback, new object[] { x });
            }
            else
            {
                tb5.Text += x.FileName + " begin!" + Environment.NewLine;
            }
        }



        private delegate void InvokeMsg2(CompetedEventArgs args);
        private void ShowAllDoneMsg(CompetedEventArgs args)
        {
            if (this.tb5.InvokeRequired)
            {
                InvokeMsg2 msgCallback = new InvokeMsg2(ShowAllDoneMsg);
                tb5.Invoke(msgCallback, new object[] { args });
            }
            else
            {
                tb5.Text += "完成率：" + Convert.ToString(args.CompetedPrecent) + "%  All Job finished!" + Environment.NewLine;
            }
        }



        private delegate void InvokeMsg1(DownLoadFile x, CompetedEventArgs args);
        private void ShowOneDoneMsg(DownLoadFile x, CompetedEventArgs args)
        {
            if (this.tb5.InvokeRequired)
            {
                InvokeMsg1 msgCallback = new InvokeMsg1(ShowOneDoneMsg);
                tb5.Invoke(msgCallback, new object[] { x, args });
            }
            else
            {
                tb5.Text += x.FileName + " finished!" + "  完成率：" + Convert.ToString(args.CompetedPrecent) + "%  " + Environment.NewLine;
            }
        }

        List<DownLoadFile> Quefd = new List<DownLoadFile>();

        private void button1_Click(object sender, EventArgs e)
        {
            DownLoadFile fd1 = new DownLoadFile();
            fd1.FileName = "myfile1.txt";
            DownLoadFile fd2 = new DownLoadFile();
            fd2.FileName = "myfile2.txt";
            DownLoadFile fd3 = new DownLoadFile();
            fd3.FileName = "myfile3.txt";

            DownLoadFile fd4 = new DownLoadFile();
            fd4.FileName = "myfile4.txt";
            DownLoadFile fd5 = new DownLoadFile();
            fd5.FileName = "myfile5.txt";
            DownLoadFile fd6 = new DownLoadFile();
            fd6.FileName = "myfile6.txt";


            //for (int i = 0;i<100;i++)
            //{
            //    DownLoadFile a = new DownLoadFile();
            //    a.FileName = "myfile" + i.ToString() + ".txt";
            //    Quefd.Add(a);
            //}

            Quefd.Add(fd1);
            Quefd.Add(fd2);
            Quefd.Add(fd3);
            Quefd.Add(fd4);
            Quefd.Add(fd5);
            Quefd.Add(fd6);
            ThreadBaseControl<DownLoadFile> thfd = new ThreadBaseControl<DownLoadFile>(Quefd,Rename);
            thfd.OneStart += ShowOneStartMsg;
            thfd.OneCompleted += ShowOneDoneMsg;
            thfd.AllCompleted += ShowAllDoneMsg;
            thfd.Start();
        }

        private void Rename(DownLoadFile file)
        {
            file.Append();
        }
    }    
}
