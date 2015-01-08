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
    public partial class frWorkPriceList : DevExpress.XtraEditors.XtraForm
    {
        public frWorkPriceList()
        {
            InitializeComponent();
        }
        public DataTable dtt = new DataTable();
        public frWorkPriceList(DataTable dtTem)
            : this()
        {
            dtt = dtTem;
        }
        private void frWorkPriceList_Load(object sender, EventArgs e)
        {
            if (dtt.Rows.Count > 0)
            {
                this.Text = dtt.Rows[0][0].ToString() + "-" + dtt.Rows[0][1].ToString() + "工价记录";
            }
            int _MaterielID = Convert.ToInt32(dtt.Rows[0][2]);
            int _WorkingID = Convert.ToInt32(dtt.Rows[0][3]);
            DataTable dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingMain, "GetWorkPriceList", new object[] {_MaterielID,_WorkingID }).Tables[0];
           
            gridControl1.DataSource = dt;
        }
    }
}