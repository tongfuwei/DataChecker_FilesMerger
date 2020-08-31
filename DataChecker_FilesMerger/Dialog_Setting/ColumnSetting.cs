using DataChecker_FilesMerger.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataChecker_FilesMerger.Dialog_Setting
{
    public partial class ColumnSetting : Form
    {
        private static ColumnSetting mainForm = null;
        public static ColumnSetting CreateInstrance()
        {
            if (mainForm == null || mainForm.IsDisposed)
            {
                mainForm = new ColumnSetting();
            }
            return mainForm;
        }

        public string AJPageColumn { get; private set; }
        public string JNPageColumn { get; private set; }
        public string JNCountColumn { get; private set; }

        private Dictionary<string, int> _AJColumn = new Dictionary<string, int>();
        public Dictionary<string, int> AJColumn
        {
            get
            {
                return _AJColumn;
            }
            set
            {
                _AJColumn = value;
                InitCombox();
            }
        }

        private Dictionary<string, int> _JNColumn = new Dictionary<string, int>();
        public Dictionary<string, int> JNColumn
        {
            get
            {
                return _JNColumn;
            }
            set
            {
                _JNColumn = value;
                InitCombox();
            }
        }

        private void InitCombox()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    //初始化案卷部分
                    if (control.Name == "AJColumns")
                    {
                        foreach (Control con in control.Controls)
                        {
                            if (con is ComboBox)
                            {
                                ComboBox combo = con as ComboBox;
                                ControlHelper.ComboAdd(combo, AJColumn.Keys.ToList());
                            }
                        }
                    }
                    //初始化卷内部分
                    if (control.Name == "JNColumns")
                    {
                        if (JNColumn != null)
                        {
                            foreach (Control con in control.Controls)
                            {
                                if (con is ComboBox)
                                {
                                    if (con.Name == "cbJNCount")
                                    {
                                        ComboBox combo = con as ComboBox;
                                        ControlHelper.ComboAdd(combo, AJColumn.Keys.ToList());
                                    }
                                    else
                                    {
                                        ComboBox combo = con as ComboBox;
                                        ControlHelper.ComboAdd(combo, JNColumn.Keys.ToList());
                                    }
                                }
                            }
                        }
                        //卷内未传入值时设为不可用
                        else
                        {
                            control.Visible = false;
                        }
                    }
                }
            }
        }        

        private ColumnSetting()
        {
            InitializeComponent();
        }

        public void Upload(IndexDictionary<string> AJColumns, IndexDictionary<string> JNColumns = null)
        {
            DialogResult = DialogResult.None;
            if (!AJColumn.EqualDictionary(AJColumns.strIndex))
            {
                if (JNColumns != null)
                    JNColumn = JNColumns.strIndex;
                else
                    JNColumn = null;
                AJColumn = AJColumns.strIndex;
            }
        }

        public DialogResult CheckSave()
        {
            if (this.cbAJPageCount.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbAJPageCount.SelectedItem.ToString()))
                AJPageColumn = this.cbAJPageCount.SelectedItem.ToString();
            else
                AJPageColumn = null;
            if (this.cbJNPageCount.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbJNPageCount.SelectedItem.ToString()))
                JNPageColumn = this.cbJNPageCount.SelectedItem.ToString();
            else
                JNPageColumn = null;
            if (this.cbJNCount.SelectedItem != null && !string.IsNullOrWhiteSpace(this.cbJNCount.SelectedItem.ToString()))
                JNCountColumn = this.cbJNCount.SelectedItem.ToString();
            else
                JNCountColumn = null;
            return DialogResult.OK;
        }
    }
}
