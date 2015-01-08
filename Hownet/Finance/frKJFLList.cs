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
    public partial class frKJFLList : DevExpress.XtraEditors.XtraForm
    {
        public frKJFLList()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.CW_KJFL";
        private void frKJFLList_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = Convert.ToDateTime(BasicClass.GetDataSet.GetDateTime().Year.ToString() + "-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            DataTable dt = BasicClass.GetDataSet.GetDS(bll, "GetInfoList", new object[] {  dt1, dt2, Convert.ToInt32(checkEdit1.Checked)}).Tables[0];
            gridControl1.DataSource = dt;
            //gridView1.Columns["A"].Visible = false;
            gridView1.Columns["ID"].Visible = false;
            //gridView1.Columns["年"].Visible = false;
            //gridView1.Columns["月"].Visible = false;
            //gridView1.Columns["日"].Visible = false;
            //gridView1.Columns["财务主管"].Visible = false;
            //gridView1.Columns["制单人ID"].Visible = false;
            //gridView1.Columns["审核ID"].Visible = false;
            //gridView1.Columns["财务主管ID"].Visible = false;
            //gridView1.Columns["帐户ID"].Visible = false;
            gridView1.Columns["TypeID"].Visible = false;
            //gridView1.Columns["TableID"].Visible = false;
            //gridView1.Columns["记帐ID"].Visible = false;
            //gridView1.Columns["附件"].Visible = false;
            //gridView1.Columns["主管审核日期"].Visible = false;
            gridView1.Columns["金额"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["手续费"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > 0)
            {
                Form fr = new Form();
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue("TypeID")) == 0)
                {
                    fr = new frKJFL(Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID")));

                }
                else
                {
                    fr = new frKJFLInMoney(Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID")));
                }
                fr.ShowDialog();
            }
        }
    }
}