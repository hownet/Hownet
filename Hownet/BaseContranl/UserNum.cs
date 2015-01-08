using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet.BaseContranl
{
    public partial class UserNum : UserControl
    {
        public UserNum()
        {
            InitializeComponent();
        }
        private string _NumStr = string.Empty;
        private int _Num = 1;
        private string _edit = string.Empty;
        private string _verify = string.Empty;
        private string _jindu = string.Empty;
        private string _num = string.Empty;
        private string _strText = "已生产";
        private DateTime _dt = DateTime.Today;
        private DateTime _dtEdit = DateTime.Today;
        private DateTime _dtVerify = DateTime.Today;
        private bool _IsCanEdit = false;
        private bool _IsEdit = false;

        public bool IsEdit
        {
            get
            {
                return _IsEdit;
            }
        }
        public void ClearData()
        {
            _leNum.Text = _leJinDu.Text = _leVerify.Text = _leEdit.Text = string.Empty;
        }
        public string StrNum
        {
            get
            {
                return _num;
            }
        }
        public string StrText
        {
            set
            {
                _strText = value;
               // broculosDrawing1.BringToFront();
               // broculosDrawing1.StrText = _strText;
            }
        }
        private void SetNum()
        {
            //if (_numType == 0 || _numType == 1)
            //    _num= _dt.Year.ToString() +"-"+ _Num.ToString();//.PadLeft(3, '0');
            //else if (_numType == 2)
            //   _num= _dt.Year.ToString() + _dt.Month.ToString() + "-" + _Num.ToString();//.PadLeft(3, '0');
            //else if (_numType == 3)
                _num= _dt.ToString("yyyyMMdd") + "-" + _Num.ToString();//.PadLeft(3, '0');

        }
        public bool IsCanEdit
        {
            set
            {
                _IsCanEdit = value;
            }
            get
            {
                return _IsCanEdit;
            }
        }
        public string NumStr
        {
            set
            {
                _NumStr = value;
                SetNum();
                _leNum.Text = _NumStr + "\r\n" + _num;
            }
            get
            {
                return _NumStr + _num;
            }
        }
        public int Num
        {
            set
            {
                _Num = value;
                textBox1.Text = _Num.ToString();
                SetNum();
                _leNum.Text = _NumStr + "\r\n" + _num;
            }
            get
            {
                return _Num;
            }
        }
        public DateTime NumDate
        {
            set
            {
                dateTimePicker1.Value = _dt = value;
                SetNum();
                _leNum.Text = _NumStr + "\r\n" + _num;
            }
            get
            {
                return _dt;
            }
        }
        public string LastEdit
        {
            set
            {
                _edit = value;
                _leEdit.Text = _edit;
            }
            get
            {
                return _edit;
            }
        }
        public string VerifyUser
        {
            set
            {
                _verify = value;
                _leVerify.Text = _verify;
            }
            get
            {
                return _verify;
            }
        }
        public string JinDu
        {
            set
            {
                _jindu = value;
                _leJinDu.Text = _jindu;
            }
            get
            {
                return _jindu;
            }
        }

        private void _leNum_Click(object sender, EventArgs e)
        {
            if (_IsCanEdit)
                _leNum.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (_IsCanEdit)
            {
                if (_dt != dateTimePicker1.Value)
                {
                    _IsEdit = true;
                    _dt = dateTimePicker1.Value;
                    SetNum();
                    _leNum.Text = _NumStr + "\r\n" + _num;
                    _leNum.Visible = true;
                }
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            if (_IsCanEdit)
            {
                if (_dt != dateTimePicker1.Value)
                {
                    _IsEdit = true;
                    _dt = dateTimePicker1.Value;
                    SetNum();
                    _leNum.Text = _NumStr + "\r\n" + _num;
                    _leNum.Visible = true;
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_Num != Convert.ToInt32(textBox1.Text))
                {
                    _Num = Convert.ToInt32(textBox1.Text);
                    _IsEdit = true;
                    SetNum();
                    _leNum.Text = _NumStr + "\r\n" + _num;
                }
            }
            catch { }
            finally
            {
                _leNum.Visible = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    _Num = Convert.ToInt32(textBox1.Text);
            //    SetNum();
            //    _leNum.Text = _NumStr + "\r\n" + _num;
            //    _IsEdit = true;
            //}
            //catch { }
            //finally
            //{
            //    _leNum.Visible = true;
            //}
        }
    }
}
