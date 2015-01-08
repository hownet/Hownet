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
    public partial class LabAndData : DevExpress.XtraEditors.XtraUserControl
    {
        public LabAndData()
        {
            InitializeComponent();
        }
        private bool _isMust;
        private bool _isShowClear = false;
        private DateTime _minDate;
        private DateTime _maxDate;
        private bool _isReadOnly = false;
        /// <summary>
        /// 是否显示清除按钮
        /// </summary>
        public bool IsShowClear
        {
            set
            {
                _isShowClear = value;
                dateEdit1.Properties.ShowClear = _isShowClear;
            }
            get
            {
                return _isShowClear;
            }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public DateTime MinDate
        {
            set
            {
                _minDate = value;
                dateEdit1.Properties.MinValue = _minDate;
            }
            get
            {
                return _minDate;
            }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public DateTime MaxDate
        {
            set
            {
                _maxDate = value;
                dateEdit1.Properties.MaxValue = _maxDate;
            }
            get
            {
                return _maxDate;
            }
        }
        /// <summary>
        /// 是否显示前面红色的*
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
        /// <summary>
        /// 点击后是否显示下面的日期控件,为假时显示，为真不显示
        /// </summary>
        public bool t 
        {
            set
            {
                _isReadOnly = value;
                   dateEdit1.Properties.ReadOnly = _isReadOnly;
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
                labelControl12.Width =dateEdit1.Width = aaa[1];
                this.dateEdit1.Location = this.labelControl12.Location = new System.Drawing.Point(aaa[0]+10, 0);
            }
            get
            {
                int[] aa = { labelControl1.Width, dateEdit1.Width };
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
        /// 标签上显示的文字
        /// </summary>
        public string strLab
        {
            set { labelControl12.Text = value; }
            get { return labelControl12.Text; }
        }
        /// <summary>
        /// 設置日期
        /// </summary>
        public object val
        {
            set 
            {
                dateEdit1.EditValue = value;
                if (value != null)
                    labelControl12.Text = ((DateTime)(dateEdit1.EditValue)).ToString("yyyy年MM月dd日");
            }
            get { return dateEdit1.EditValue; }
        }
        private void labelControl12_Click(object sender, EventArgs e)
        {
            if (!_isReadOnly)
            {
                labelControl12.Visible = false;
                dateEdit1.Focus();
            }
        }

        private void dateEdit1_Leave(object sender, EventArgs e)
        {
            if (dateEdit1.EditValue != null)
            {
                labelControl12.Text = ((DateTime)(dateEdit1.EditValue)).ToString("yyyy年MM月dd日");
            }
            else
            {
                labelControl12.Text = string.Empty;
            }
          //  labelControl12.Visible = true;
        }
    }
}
