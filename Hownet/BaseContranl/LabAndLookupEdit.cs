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
    public partial class LabAndLookupEdit : DevExpress.XtraEditors.XtraUserControl
    {
        public LabAndLookupEdit()
        {
            InitializeComponent();
        }
       DataSoure bllDS = new DataSoure();
        public delegate void EditValueChangedHandler(object val,string text);
        public event EditValueChangedHandler EditValueChanged;
        public delegate void ClickButtonHandler(BasicClass.cResult cr, int formName);
        public event ClickButtonHandler ClickButtonChanged;
        private bool _isMust=false;
        private bool _isCanEdit=false;
      //  private bool _isShowLab2 = true;
        private BasicClass.cResult r;
        //private DataView _dv = new DataView();
        private int  _fr;
        private object[] _par=null;
        DataView dv = new DataView();
        /// <summary>
        /// 数据源
        /// </summary>
        public DataView DV
        {
            set
            {
                dv = value;
                lookUpEdit1.Properties.DataSource = dv;
            }
            get
            {
                return dv;
            }
        }
        /// <summary>
        /// 下拉框數據源的篩選條件
        /// </summary>
        public string Filter
        {
            set
            {
                dv.RowFilter = value;
                lookUpEdit1.Properties.DataSource = dv;
            }
        }
        public bool IsShowNewButton
        {
            set
            {
                if (value == false)
                    lookUpEdit1.Properties.Buttons[1].Visible = false;
            }
        }
        public BasicClass.cResult cr
        {
            get
            {
                return r;
            }
        }
        /// <summary>
        /// 参数
        /// </summary>
        public object[] Par
        {
            set
            {
                _par = value;
                if(_par!=null)
                    dv = bllDS.getParDS(_fr, _par);
            }
            get
            {
                return _par;
            }
        }
        /// <summary>
        /// 是否可以修改 
        /// </summary>
        public bool IsNotCanEdit
        {
            set
            {
                _isCanEdit = value;
                lookUpEdit1.Properties.ReadOnly = _isCanEdit;
            }
            get
            {
                return _isCanEdit;
            }
        }
        /// <summary>
        /// 设置数据源，
        /// </summary>
        public int FormName
        {
            set
            {
                _fr = value;
                if (_fr != 0)
                {
                    if (_par == null)
                        dv = bllDS.getDS(_fr);
                    else
                        dv = bllDS.getParDS(_fr, _par);
                    lookUpEdit1.Properties.DataSource = dv;
                    lookUpEdit1.Properties.DisplayMember = bllDS.DMember(_fr);
                    lookUpEdit1.Properties.ValueMember = bllDS.VMember(_fr);
                    SetCol(bllDS.ColName(_fr));
                    r = new BasicClass.cResult();
                    r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                }
               
            }
            get
            {
                return _fr;
            }
        }
        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="aaa"></param>
        private void SetCol(List<DataSoure.strColumns> aaa)
        {
            if (aaa.Count > 0)
            {
                this.lookUpEdit1.Properties.Columns.Clear();
                for (int i = 0; i < aaa.Count; i++)
                {
                       this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(aaa[i].ColumnName,aaa[i].Caption)});
                }
            }
        }
        /// <summary>
        /// 前面红色的*是否显示
        /// </summary>
        public bool IsMust
        {
            set
            {
                _isMust = value;
                labelControl2.Visible = _isMust;
            }
            get
            {
                return _isMust;
            }
        }
        private void ChangeVal(object s,string text)
        {
            if (EditValueChanged != null)
                EditValueChanged(s,text);
        }
        private void ClickButton(BasicClass.cResult cr, int formName)
        {
            if (ClickButtonChanged != null)
                ClickButtonChanged(cr, formName);
        }
        /// <summary>
        /// 前面标签的显示文本
        /// </summary>
        public string labText
        {
            set { labelControl1.Text = value; }
            get { return labelControl1.Text; }
        }
        /// <summary>
        /// 获取lookup的文本
        /// </summary>
        public string valStr
        {
            get { return lookUpEdit1.Text; }
        }
        /// <summary>
        /// 设置lookup的值
        /// </summary>
        public object editVal
        {
            set
            {
                lookUpEdit1.EditValue = value;
            }
            get 
            {
                if (lookUpEdit1.EditValue != null)
                    return lookUpEdit1.EditValue;
                else
                    return 0;
            }
        }

          /// <summary>
        /// 长度设置。
        /// </summary>
        public int[] lenth
        {
            set
            {
                int[] aaa = value;
                this.Size = new System.Drawing.Size((aaa[0] + aaa[1]+10), 22);
                labelControl1.Width = aaa[0];
                lookUpEdit1.Width = aaa[1];
                this.lookUpEdit1.Location =  new System.Drawing.Point(aaa[0], 0);
                labelControl2.Location = new System.Drawing.Point(aaa[0] + aaa[1], 0);
            }
            get
            {
                int[] aa = { labelControl1.Width, lookUpEdit1.Width };
                return aa;
            }
        }

        private void lookUpEdit1_Leave(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEdit1.EditValue != null)
            {
                ChangeVal(lookUpEdit1.EditValue, lookUpEdit1.Text);
            }
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                ClickButton(r, _fr);
                if (_fr == (int)BasicClass.Enums.TableType.Product)
                {
                    Form fr = new BaseForm.frFinished(r, -1);
                    fr.ShowDialog();
                }
                else if (_fr == (int)BasicClass.Enums.TableType.Brand)
                {
                    Form fr = new BaseForm.frBrandList(r, -1);
                    fr.ShowDialog();
                }
                else if (_fr == (int)BasicClass.Enums.TableType.Company)
                {
                    Form fr = new BaseForm.frCompany(r, -1);
                    fr.ShowDialog();
                }

            }
        }

        void r_TextChanged(string s)
        {
            lookUpEdit1.Properties.DataSource = dv = bllDS.getDS(_fr);
            if (int.Parse(s) > -1)
            {
                editVal = lookUpEdit1.EditValue = int.Parse(s);
                object obj = lookUpEdit1.Text;
                if (obj == null || obj.ToString() == "")
                {
                    editVal = lookUpEdit1.EditValue = 0;
                    XtraMessageBox.Show("選擇類型不對！");
                }
            }
            else
            {
                editVal = lookUpEdit1.EditValue = 0;
            }
        }

        private void lookUpEdit1_QueryCloseUp(object sender, CancelEventArgs e)
        {

        }
        public object GetColumnValue(string ColumnsName)
        {
            return lookUpEdit1.GetColumnValue(ColumnsName);
        }
    }
}
