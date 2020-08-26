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
    public partial class OneToManySetting : Form
    {
        private static OneToManySetting mainForm = null;
        public static OneToManySetting CreateInstrance()
        {
            if (mainForm == null || mainForm.IsDisposed)
            {
                mainForm = new OneToManySetting();
            }
            return mainForm;
        }

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
            }
        }

        public List<string> AJKeyColumn
        {
            get;
            set;
        } = new List<string>();

        public List<string> JNKeyColumn
        {
            get;
            set;
        } = new List<string>();

        Dictionary<ComboBox, ComboBox> AJ_JN = new Dictionary<ComboBox, ComboBox>();

        private OneToManySetting()
        {
            InitializeComponent();
            AJ_JN.Add(cbAJ1, cbJN1);
            AJ_JN.Add(cbAJ2, cbJN2);
            AJ_JN.Add(cbAJ3, cbJN3);
        }

        public void Upload(IndexDictionary<string> AJColumns, IndexDictionary<string> JNColumns)
        {
            DialogResult = DialogResult.None;
            if (!AJColumn.EqualDictionary(AJColumns.strIndex))
            {
                JNColumn = JNColumns.strIndex;
                AJColumn = AJColumns.strIndex;
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
                                Commons.ComboAdd(combo, AJColumn.Keys.ToList());
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
                                    ComboBox combo = con as ComboBox;
                                    Commons.ComboAdd(combo, JNColumn.Keys.ToList());
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

        public DialogResult CheckSave()
        {
            foreach (ComboBox ajcb in AJ_JN.Keys)
            {
                if ((ajcb.SelectedItem!=null) && (!string.IsNullOrWhiteSpace(ajcb.SelectedItem.ToString())))
                {
                    if ((AJ_JN[ajcb].SelectedItem!= null)&& !string.IsNullOrWhiteSpace (AJ_JN[ajcb].SelectedItem.ToString()))
                    {
                        AJKeyColumn.Add(ajcb.SelectedItem.ToString());
                        JNKeyColumn.Add(AJ_JN[ajcb].SelectedItem.ToString());
                    }
                    else
                    {
                        MessageBox.Show("两侧均要选择列!");
                        return DialogResult.Cancel;
                    }
                }
            }
            if(AJKeyColumn.Count != JNKeyColumn.Count || AJKeyColumn.Count == 0)
            {
                MessageBox.Show("案卷与卷内的对应关系不能为空!");
                return DialogResult.Cancel;
            }
            return DialogResult.OK;
        }
    }
}
