using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.WMS
{
    public partial class frP2DAmount : DevExpress.XtraEditors.XtraForm
    {
        public frP2DAmount()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        string _strLab = string.Empty;
        bool _IsCanEdit = false;
        DataTable dt = new DataTable();
        int _materielID, _brandID;
        public frP2DAmount(BasicClass.cResult cr, string ss,bool IsCanEdit,DataTable dtt,int MaterielID,int BrandID)
            : this()
        {
            r = cr;
            _strLab = ss;
            _IsCanEdit = IsCanEdit;
            dt = dtt;
            _materielID = MaterielID;
            _brandID = BrandID;
        }
        private void frP2DAmount_Load(object sender, EventArgs e)
        {
            labelControl1.Text = _strLab;
            if (dt.Rows.Count > 0)
            {
                amountList1.Open(_IsCanEdit, dt);
            }
            else
            {
                amountList1.Open(true, BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderMain, "GetCS2RepByMatID", new object[] { _materielID }).Tables[0]);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            DataTable dtttt = amountList1.GetDTList();
            if (dtttt.Rows.Count > 0)
            {

            }
            r.RowChang(dtttt);
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}