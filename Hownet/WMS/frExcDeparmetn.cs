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
    public partial class frExcDeparmetn : DevExpress.XtraEditors.XtraForm
    {
        public frExcDeparmetn()
        {
            InitializeComponent();
        }
        int _PlanID = 0;
        int DepTypeID = 0;
        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtDep = new DataTable();
        DataTable dtJGC = new DataTable();
        public frExcDeparmetn(BasicClass.cResult cr, int PlanID)
            : this()
        {
            r = cr;
            _PlanID = PlanID;
        }
        private void frExcDeparmetn_Load(object sender, EventArgs e)
        {
            dtDep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] {"缝制" }).Tables[0];
            dtJGC = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0];
            lookUpEdit1.Properties.DataSource = dtDep;
            labelControl1.Visible = textEdit1.Visible = false;
            textEdit1.EditValue = 0;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            textEdit1.EditValue = 0;
            if (checkEdit1.Checked)
            {
                lookUpEdit1.Properties.DataSource = dtJGC;
                DepTypeID = 3;
                labelControl1.Visible = textEdit1.Visible = true;
            }
            else
            {
                lookUpEdit1.Properties.DataSource = dtDep;
                DepTypeID = 0;
                labelControl1.Visible = textEdit1.Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("确认照此修改生产部门？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    if (DepTypeID == 3)
                    {
                        try
                        {
                            decimal ccc = Convert.ToDecimal(textEdit1.EditValue);
                        }
                        catch
                        {
                            XtraMessageBox.Show("加工费填写错误！");
                            return;
                        }
                    }
                    DataTable dtTM = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] {"(ID="+_PlanID+")"}).Tables[0];
                    if (dtTM.Rows.Count == 1)
                    {
                        
                        dtTM.Rows[0]["DeparmentID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                        dtTM.Rows[0]["DeparmentType"] = DepTypeID;
                        dtTM.Rows[0]["Price"] = textEdit1.EditValue;
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProductTaskMain, dtTM);
                        r.ChangeText("1");
                    }
                }
                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}