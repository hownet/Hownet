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
    public partial class frEditTime : DevExpress.XtraEditors.XtraForm
    {
        public frEditTime()
        {
            InitializeComponent();
        }
        //1上午上班，2上午下班，3下午上班，4下午下班，5晚上上班，6晚上下班，7通宵下班
       // int TypeID = 0;
        DateTime dt = Convert.ToDateTime("1900-1-1");
        BasicClass.cResult r = new BasicClass.cResult();
        public frEditTime(BasicClass.cResult cr,  DateTime dtt)
            : this()
        {
            r = cr;
           // TypeID = _TypeID;
            dt = dtt;
        }
        private void frEditTime_Load(object sender, EventArgs e)
        {
              dateEdit1 .EditValue = dt;
              dateEdit1.Properties.MinValue = dt.Date;
              dateEdit1.Properties.MaxValue = dt.Date.AddDays(1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            r.ChangeText(Convert.ToDateTime(dateEdit1.EditValue).ToString());
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的清除此次签到？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                r.ChangeText(string.Empty);
                this.Close();
            }
        }
    }
}