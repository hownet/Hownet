using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BasicClass;

namespace Hownet.Sell
{
    public partial class frSellInfoTem : DevExpress.XtraEditors.XtraForm
    {
        public frSellInfoTem()
        {
            InitializeComponent();
        }
        cResult r = new cResult();
        bool _isCanEdit = false;
        DataTable dt;
        string _materiel = string.Empty;
        string _brand = string.Empty;
        public frSellInfoTem(cResult cr, bool t, DataTable Dt, string Materiel, string Brand)
            : this()
        {
            r = cr;
            _isCanEdit = t;
            dt = Dt;
            _materiel = Materiel;
            _brand = Brand;
        }
        private void frSellBackInfo_Load(object sender, EventArgs e)
        {
            amountList1.IsCanEdit = _isCanEdit;
           // amountList1.ShowInfo(_materielID, _brandID, _isCanEdit, dt);
            amountList1.Open(_isCanEdit, dt);
            labelControl1.Text = "��ţ�" + _materiel + "���̱꣺" + _brand + "���˻���ϸ����Ϊ��";
        }

        private void frSellBackInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (_isCanEdit)
            //{
            //    if (amountList1.CheckColor)
            //    {
            //        if (DialogResult.No == XtraMessageBox.Show("��ɫ�ظ��������ܸ��������޸ģ��Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            //        {
            //            e.Cancel = true;
            //        }
            //    }
            //    else
            //    {
            //       // r.RowChang(dt);
            //    }
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!_isCanEdit)
            {
                this.Close();
                return;
            }
            if (amountList1.CheckColor)
            {
                XtraMessageBox.Show("��ɫ�ظ��������ܸ��������޸�");
                return;
            }
            else
            {
                dt.Rows.Clear();
                DataTable dtttt = amountList1.GetDTList();
                if (dtttt.Rows.Count > 0)
                {

                }
                r.RowChang(dtttt);
                this.Close();
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}