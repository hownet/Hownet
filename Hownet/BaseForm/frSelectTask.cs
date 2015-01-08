using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frSelectTask : DevExpress.XtraEditors.XtraForm
    {
        public frSelectTask()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        public frSelectTask(BasicClass.cResult cr)
            : this()
        {
            r = cr;
        }
        DataTable dtMat = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtSales = new DataTable();
        private void frSelectTask_Load(object sender, EventArgs e)
        {
            dtMat = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetList", new object[] { "AttributeID=4" }).Tables[0];
            dtCompany = BasicClass.GetDataSet.GetDS("Hownet.BLL.Company", "GetList", new object[] {"(TypeID=1)" }).Tables[0];
            lookUpEdit1.Properties.DataSource = dtMat;
            lookUpEdit3.Properties.DataSource = dtCompany;
            lookUpEdit3.EditValue =  lookUpEdit1.EditValue = 0;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
            {
                dtSales = BasicClass.GetDataSet.GetDS("Hownet.BLL.SalesOrderInfoList", "GetNumList", new object[] { "(SalesOrderInfoList.MaterielID=" + lookUpEdit1.EditValue + ")" }).Tables[0];
                lookUpEdit2.Properties.DataSource = dtSales;
                lookUpEdit2.EditValue = 0;
            }
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit2.EditValue) > 0)
            {
                DataRow[] drs = dtSales.Select("(ID=" + lookUpEdit2.EditValue + ")");
                lookUpEdit3.EditValue = Convert.ToInt32(drs[0]["CompanyID"]);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("1", typeof(int));
            dt.Columns.Add("11", typeof(string));
            dt.Columns.Add("2", typeof(int));
            dt.Columns.Add("22", typeof(string));
            dt.Columns.Add("3", typeof(int));
            dt.Columns.Add("33", typeof(string));
            DataRow dr = dt.NewRow();
            dr[0] = Convert.ToInt32(lookUpEdit1.EditValue);
            dr[1] = lookUpEdit1.Text;
            dr[2] = Convert.ToInt32(lookUpEdit2.EditValue);
            dr[3] = lookUpEdit2.Text;
            dr[4] = Convert.ToInt32(lookUpEdit3.EditValue);
            dr[5] = lookUpEdit3.Text;
            dt.Rows.Add(dr);
            r.RowChang(dt);
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}