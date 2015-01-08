using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BasicClass;

namespace Hownet.BaseContranl
{
    public partial class LabAndText : DevExpress.XtraEditors.XtraUserControl
    {
        public LabAndText()
        {
            InitializeComponent();
        }
        public delegate void EditValueChangedHandler(object val);
        public event EditValueChangedHandler EditValueChanged;
        private bool _isMust;
        private string _mask = "";
        private string _val;
        private bool _isHand = false;
        private int _lenght1 = 0;
        private int _lenght2 = 0;
        private bool _isCanEdit = true;
        /// <summary>
        /// 文本框的值
        /// </summary>
        public object EditVal
        {
            set
            {
                textEdit1.EditValue = value;
            }
            get
            {
                return textEdit1.EditValue;
            }
        }
        public bool IsHand
        {
            set
            {
                _isHand = value;
            }
            get
            {
                return _isHand;
            }
        }
        public bool IsMust
        {
            set 
            {
                _isMust = value;
                labelControl2.Visible = _isMust;
            }
            get { return _isMust; }
        }
        private void ChangeVal(object s)
        {
            if (EditValueChanged != null)
                EditValueChanged(s);
        }
        public bool IsCanEdit
        {
            set
            {
                _isCanEdit = value;
                textEdit1.Properties.ReadOnly = !_isCanEdit;
            }
            get
        {
            return _isCanEdit;
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
                _lenght1 = aaa[0];
                _lenght2 = aaa[1];
                this.Size = new System.Drawing.Size((aaa[0] + aaa[1]+10), 22);
                labelControl1.Width = aaa[0];
                 textEdit1.Width = aaa[1];
                this.textEdit1.Location =  new System.Drawing.Point(aaa[0], 0);
                labelControl2.Location = new System.Drawing.Point(aaa[0] + aaa[1], 0);
            }
            get
            {
                int[] aa = { labelControl1.Width, textEdit1.Width };
                return aa;
            }
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
        /// 文本框的值
        /// </summary>
        public string val
        {
            set 
            {
                _val = value;
                if (_mask != "" && _val != "")
                {
                    if (_mask == Enums.Mask.整數.ToString())
                    {
                        _val = int.Parse(_val).ToString();
                    }
                    else if (_mask == Enums.Mask.金額.ToString())
                    {
                        _val = decimal.Parse(_val).ToString("n2");
                    }
                    else if (_mask == Enums.Mask.匯率.ToString())
                    {
                        _val = decimal.Parse(_val).ToString("n4");
                    }
                }
                textEdit1.Text = _val;
            }
            get
            {
                return textEdit1.Text;
            }
        }
        /// <summary>
        /// 掩碼，主要爲數字類型
        /// </summary>
        public string Mask
        {
            set
            {
                _mask = value;
                if (_mask == "%")
                {
                    this.textEdit1.Properties.DisplayFormat.FormatString = "p";
                    this.textEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    this.textEdit1.Properties.EditFormat.FormatString = "p";
                    this.textEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    this.textEdit1.Properties.Mask.EditMask = "p";
                    this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                }
                else if (_mask == BasicClass.Enums.Mask.金額.ToString())
                {
                    this.textEdit1.Properties.EditFormat.FormatString = "c";
                    this.textEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    this.textEdit1.Properties.Mask.EditMask = "c";
                    this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    this.textEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
                    this.textEdit1.Properties.DisplayFormat.FormatString = "c";
                    this.textEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                }
            }
            get { return _mask; }
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim().Length > 0 && _mask != "")
            {
                try
                {
                if (_mask == Enums.Mask.整數.ToString())
                {
                    if (textEdit1.Text != "")
                    {
                        int a =Convert.ToInt32(textEdit1.EditValue);
                        textEdit1.Text = a.ToString();
                    }
                }
                else if (_mask == Enums.Mask.金額.ToString())
                {
                    if (textEdit1.EditValue.ToString() != "")
                    {
                        double a = Convert.ToDouble(textEdit1.EditValue);
                       // textEdit1.Text = a.ToString("n2");
                    }
                }
                else if (_mask == Enums.Mask.匯率.ToString())
                {
                    if (textEdit1.Text != "")
                    {
                        decimal a =Convert.ToDecimal(textEdit1.EditValue);
                     //   textEdit1.Text = a.ToString("n4");
                    }
                }
                else if(_mask=="%")
                {
                    //object a = textEdit1.EditValue;
                    if (textEdit1.Text != "")
                    {
                        decimal a = decimal.Parse(textEdit1.EditValue.ToString());
                        //textEdit1.Text = textEdit1.Text;
                    }
                }
                }
                catch
                {
                    XtraMessageBox.Show("輸入類型錯誤！");
                    textEdit1.Text = "";
                }
            }

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit1.EditValue != null && textEdit1.Text != "")
            {
                //if (_mask != "")
                //{
                //    try
                //    {
                //        if (_mask == Hownet.Enums.Mask.整數.ToString())
                //        {
                //            int a = int.Parse(textEdit1.Text);
                //        }
                //        else if (_mask == Hownet.Enums.Mask.金額.ToString())
                //        {
                //            decimal a = decimal.Parse(textEdit1.Text);
                //        }
                //        else if (_mask == Hownet.Enums.Mask.匯率.ToString())
                //        {
                //            decimal a = decimal.Parse(textEdit1.Text);
                //        }
                //    }
                //    catch
                //    {
                //        textEdit1.Text = "0";
                //    }
                //    textEdit1.SelectionStart = textEdit1.Text.Length;
                //}
                ChangeVal(textEdit1.Text);
            }
        }

        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_mask != "" && _isHand)
            {
                try
                {
                if (_mask == Enums.Mask.整數.ToString())
                {
                    int a = int.Parse(textEdit1.Text);
                }
                else if (_mask == Enums.Mask.金額.ToString())
                {
                    decimal a = decimal.Parse(textEdit1.Text);
                }
                else if (_mask == Enums.Mask.匯率.ToString())
                {
                    decimal a = decimal.Parse(textEdit1.Text);
                }
                }
                catch
                {
                    e.Cancel = true;
                }
                //textEdit1.SelectionStart = textEdit1.Text.Length;
            }
        }

        private void LabAndText_Resize(object sender, EventArgs e)
        {
            if(this.Width>(_lenght1+_lenght2))
            {
                textEdit1.Width = this.Width - _lenght1;
               labelControl2.Location = new System.Drawing.Point(this.Width-10, 0);
            }
        }

    }
}
