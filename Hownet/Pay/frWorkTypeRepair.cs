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
    public partial class frWorkTypeRepair : DevExpress.XtraEditors.XtraForm
    {
        public frWorkTypeRepair()
        {
            InitializeComponent();
        }
        int _PayMainID = 0;
        public frWorkTypeRepair(int PayMainID)
            : this()
        {
            _PayMainID = PayMainID;
        }
        private void frWorkTypeRepair_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetBuTieList", new object[] {_PayMainID }).Tables[0];
        }
    }
}