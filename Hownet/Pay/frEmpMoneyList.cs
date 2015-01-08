using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Pay
{
    public partial class frEmpMoneyList : DevExpress.XtraEditors.XtraForm
    {
        public frEmpMoneyList()
        {
            InitializeComponent();
        }
        DateTime dtOne;
        DateTime dtTwo;
        private void frEmpMoneyList_Load(object sender, EventArgs e)
        {
            _ldDtOne.val = DateTime.Today.AddYears(-1);
            _ldDtTwo.val = DateTime.Today;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dtOne =( (DateTime)_ldDtOne.val).AddSeconds(-1);
            dtTwo =( (DateTime)_ldDtTwo.val).AddSeconds(1);
            DataTable dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPayMain, "GetMoneyList", new object[] { dtOne,dtTwo}).Tables[0];
            dt.Columns.Add("Date", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Date"] = "´Ó" + ((DateTime)(dt.Rows[i]["BegingDate"])).ToLongDateString() + "µ½" + ((DateTime)(dt.Rows[i]["EndDate"])).ToLongDateString();
                }
            }
            pivotGridControl1.DataSource = dt;
        }
    }
}