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
            labelControl1.Text = "款号：" + _materiel + "，商标：" + _brand + "，退货明细数量为：";
        }

        private void frSellBackInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (_isCanEdit)
            //{
            //    if (amountList1.CheckColor)
            //    {
            //        if (DialogResult.No == XtraMessageBox.Show("颜色重复，将不能更新所做修改，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
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
                XtraMessageBox.Show("颜色重复，将不能更新所做修改");
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