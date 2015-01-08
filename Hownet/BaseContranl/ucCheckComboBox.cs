using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
    public partial class ucCheckComboBox : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCheckComboBox()
        {
            InitializeComponent();
            //comboBox1.DataSource;
        }
        public delegate void EditValueChangedHandler(object val, string text);
        public event EditValueChangedHandler EditValueChanged;
        private DataTable dt;
        private string _DisplayMember;
        private string _ValueMember;
        private string _value;
        public DataTable DataDt
        {
            set
            {
                dt = value;
                checkedListBox1.DataSource = dt;
            }
        }
        public string DisplayMember
        {
            set
            {
                _DisplayMember = value;
                checkedListBox1.DisplayMember = _DisplayMember;
            }
        }
        public string ValueMember
        {
            set
            {
                _ValueMember = value;
                checkedListBox1.ValueMember = _ValueMember;
            }
        }
        public string Values
        {
            set
            {
                _value = value;
                string ssss = string.Empty;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i,false);
                }
                if (_value.IndexOf(',') > -1)
                {
                    string[] vvv = _value.Split(',');
                    //checkedListBox1.SelectedValue = _value;
                    checkedListBox1.SelectedValue = 0;
                    for (int i = 0; i < vvv.Length; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (vvv[i] == dt.Rows[j][_ValueMember].ToString())
                            {
                                checkedListBox1.SetItemChecked(j, true);
                                ssss = ssss + dt.Rows[j][_DisplayMember].ToString() + ",";
                                break;
                            }
                        }
                    }
                }
                else if(_value.Trim().Length>0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (_value == dt.Rows[j][_ValueMember].ToString())
                        {
                            checkedListBox1.SetItemChecked(j, true);
                            ssss = ssss + dt.Rows[j][_DisplayMember].ToString() + ",";
                            break;
                        }
                    }
                }
                if(ssss.Length>0)
                    ssss=ssss.Remove(ssss.Length-1,1);
                popupContainerEdit1.EditValue = ssss;
            }
            get
            {
                string ss=string.Empty;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                        ss = ss + dt.Rows[i][_ValueMember].ToString() + ",";
                }
                if (ss.Length > 0)
                    ss = ss.Remove(ss.Length - 1, 1);
                return ss;
            }
        }

        private void popupContainerEdit1_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            string ss = string.Empty;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    ss = ss + dt.Rows[i][_DisplayMember].ToString() + ",";
            }
            if (ss.Length > 0)
                ss = ss.Remove(ss.Length - 1, 1);
            //popupContainerEdit1.EditValue = ss;
            e.Value = ss;
        }

        private void ucCheckComboBox_SizeChanged(object sender, EventArgs e)
        {
            popupContainerEdit1.Size = this.Size;
        }
        private void ChangeVal(object s, string text)
        {
            if (EditValueChanged != null)
                EditValueChanged(s, text);
        }

        private void popupContainerEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (popupContainerEdit1.EditValue != null)
            {
                ChangeVal(Values, popupContainerEdit1.Text);
            }
        }
    }
}
