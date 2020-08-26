using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
    public partial class Setting : Form
    {
        private List<Form> _forms = new List<Form>();
        public List<Form> forms
        {
            get
            {
                return _forms;
            }
            set
            {
                _forms = value;
            }
        }

        public Setting(List<Form> forms)
        {
            InitializeComponent();
            this.forms = forms;
            foreach(Form form in this.forms)
            {
                Add_TabPage(form);
            }
        }

        public void Add_TabPage(Form myForm) //将标题添加进tabpage中
        {
            if (!this.tabControlCheckHave(this.tabControl1, myForm.Text))
            {
                this.tabControl1.TabPages.Add(myForm.Text);
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.TopLevel = false;
                myForm.Parent = this.tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                myForm.Show();
            }
        }

        public bool tabControlCheckHave(TabControl tab, string tabName) //看tabpage中是否已有窗体
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        private delegate void CheckSave_FlushClient();
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            foreach(Form form in this.forms)
            {
                MethodInfo method = form.GetType().GetMethod("CheckSave");
                object result = method.Invoke(form,null);                
                if ((DialogResult)result != DialogResult.OK)
                    return;
                else
                    continue;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
