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
                InitCombox("AJColumns",AJColumn.Keys.ToList());
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
                InitCombox("JNColumns", JNColumn.Keys.ToList());
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
                AJColumn = AJColumns.strIndex;
            }
            if(!JNColumn.EqualDictionary(JNColumns.strIndex))
            {
                JNColumn = JNColumns.strIndex;
            }
        }

        private void InitCombox(string panelName,List<string> columns)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    //初始化案卷部分
                    if (control.Name == panelName)
                    {
                        if (columns == null || columns.Count == 0)
                        {
                            control.Visible = false;
                            return;
                        }
                        foreach (Control con in control.Controls)
                        {
                            if (con is ComboBox)
                            {
                                ComboBox combo = con as ComboBox;
                                Commons.ComboAdd(combo, columns);
                            }
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
                    AJKeyColumn.Add(ajcb.SelectedItem.ToString());
                }
                if ((AJ_JN[ajcb].SelectedItem != null) && !string.IsNullOrWhiteSpace(AJ_JN[ajcb].SelectedItem.ToString()))
                {
                    JNKeyColumn.Add(AJ_JN[ajcb].SelectedItem.ToString());
                }
            }
            if(AJKeyColumn.Count != JNKeyColumn.Count)
            {
                MessageBox.Show("案卷与卷内必须一一对应!");
                AJKeyColumn.Clear();
                JNKeyColumn.Clear();
                return DialogResult.Cancel;
            }
            return DialogResult.OK;
        }
    }
}
