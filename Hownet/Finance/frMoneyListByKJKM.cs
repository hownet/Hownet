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
    public partial class frMoneyListByKJKM : DevExpress.XtraEditors.XtraForm
    {
        public frMoneyListByKJKM()
        {
            InitializeComponent();
        }
        string bllMoney = "Hownet.BLL.MoneyList";
        DataTable dtKJKM = new DataTable();
        private void frMoneyListByKJKM_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = Convert.ToDateTime(BasicClass.GetDataSet.GetDateTime().Year.ToString() + "-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            dtKJKM= BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetMoneyKJKM", null).Tables[0];
            lookUpEdit1.Properties.DataSource = dtKJKM;
            lookUpEdit1.EditValue = 130;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            DataTable dt = BasicClass.GetDataSet.GetDS(bllMoney, "GetList", new object[] {"(DateTime>'"+dt1+"') And (DateTime<'"+dt2+"') And (KJKMID="+Convert.ToInt32(lookUpEdit1.EditValue)+")" }).Tables[0];
            dt.DefaultView.Sort = "ID";
            gridControl1.DataSource = dt.DefaultView;
            gridView1.Columns["ID"].Visible = gridView1.Columns["A"].Visible = gridView1.Columns["TableID"].Visible = gridView1.Columns["TypeID"].Visible = gridView1.Columns["KJKMID"].Visible = false;
            gridView1.Columns["InMoney"].Caption = "收入";
            gridView1.Columns["OutMoney"].Caption = "支出";
            gridView1.Columns["Money"].Caption = "结余";
            gridView1.Columns["Remark"].Caption = "摘要";
            gridView1.Columns["DateTime"].Caption = "日期";
            gridView1.Columns["Remark"].VisibleIndex = 1;
            gridView1.Columns["InMoney"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["OutMoney"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                int _TypeID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("TypeID"));
                int _MainID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("TableID"));
                Form fr = new Form();
                if (_TypeID == 3||_TypeID==(int)BasicClass.Enums.MoneyTableType.Vouchers)
                {
                    fr = new frVouchers(_MainID);
                }
                else if (_TypeID == (int)BasicClass.Enums.MoneyTableType.OutMoney)
                {
                    fr = new BsOutMoneyForm(_MainID);
                }
                else if (_TypeID == (int)BasicClass.Enums.MoneyTableType.BackMoney)
                {
                    fr = new BsInMoneyForm(_MainID);
                }
                else if (_TypeID == (int)BasicClass.Enums.MoneyTableType.KJFL)
                {
                    fr = new frKJFL(_MainID);
                }
                else if (_TypeID == (int)BasicClass.Enums.MoneyTableType.KJFLInMoney)
                {
                    fr = new frKJFLInMoney(_MainID);
                }
                if (_TypeID > 0)
                {
                    fr.ShowDialog();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            BasicClass.GetDataSet.ExecSql(bllMoney, "Balance", null);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string fileName =BaseContranl. BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
               BaseContranl. BaseFormClass.OpenFile(fileName);
            }
        }
    }
}