using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Finance
{
    public partial class frMoneyList : DevExpress.XtraEditors.XtraForm
    {
        public frMoneyList()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int CompanyID = Convert.ToInt32(lookUpEdit1.EditValue);
            bool _IsSum = checkEdit1.Checked;
            gridView1.Columns.Clear();
            gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMoneyInOrOut, "GetInfoList", new object[] { dt1, dt2, CompanyID, _IsSum }).Tables[0];
            gridView1.Columns["日期"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["ID"].Visible = false;
            if(_IsSum)
            gridView1.Columns[2].ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
            else
                gridView1.Columns[2].ColumnEdit = BaseForm.RepositoryItem._reSupplier;
            gridView1.Columns[0].Width = 120;
            gridView1.Columns[1].Width = 300;
            gridView1.Columns[2].Width = 120;
            gridView1.Columns[3].Width = 300;
        }

        private void frMoneyList_Load(object sender, EventArgs e)
        {
            DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetAllList",null).Tables[0];
            DataRow drCom = dtCom.NewRow();
            drCom["ID"] = 0;
            drCom["Name"] = string.Empty;
            dtCom.Rows.Add(drCom);
            lookUpEdit1.Properties.DataSource = dtCom.DefaultView;
            dateEdit1.EditValue = Convert.ToDateTime(BasicClass.GetDataSet.GetDateTime().Year.ToString() + "-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;

            lookUpEdit1.EditValue = 0;
        }


        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
        {
            checkEdit2.Checked = !checkEdit1.Checked;
        }

        private void checkEdit2_EditValueChanged(object sender, EventArgs e)
        {
            checkEdit1.Checked = !checkEdit2.Checked;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string fileName = BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }


    }
}