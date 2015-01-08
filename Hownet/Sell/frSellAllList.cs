using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Sell
{
    public partial class frSellAllList : DevExpress.XtraEditors.XtraForm
    {
        public frSellAllList()
        {
            InitializeComponent();
        }
        DataTable dtlist = new DataTable();
        DataTable dtOT = new DataTable();
        bool IsShowBoxMeasureID = false;
        private void frSellAllList_Load(object sender, EventArgs e)
        {
            DateTime dtNow = BasicClass.GetDataSet.GetDateTime();
            dateEdit1.EditValue = dtNow.Date.AddDays((dtNow.Day - 1) * -1);
            dateEdit2.EditValue = dtNow.Date;
            ucGridLookup1.DisplayMember = "Name";
            ucGridLookup1.ValueMember = "ID";
            ucGridLookup1.TypeID = (int)BasicClass.Enums.TableType.Company;
            lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtAllMateriel;
            dtOT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='仓储单位')" }).Tables[0];
            if (dtOT.Rows.Count > 0)
            {
                int measureID = Convert.ToInt32(dtOT.Rows[0]["Value"]);
                dtOT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='默认单位')" }).Tables[0];
                if(dtOT.Rows.Count>0)
                {
                    IsShowBoxMeasureID = (measureID != Convert.ToInt32(dtOT.Rows[0]["Value"]));
                }
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        //订单
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            gridView1.Columns.Clear();
            gridControl1.DataSource = dtlist;
        }
        //销售
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            gridView1.Columns.Clear();
            gridControl1.DataSource = dtlist;
        }
        //来款
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            gridView1.Columns.Clear();
            gridControl1.DataSource = dtlist;
        }
        //退货
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            gridView1.Columns.Clear();
            gridControl1.DataSource = dtlist;
        }
        //累欠
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            gridView1.Columns.Clear();
            gridControl1.DataSource = dtlist;
        }
        //查询
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int _companyID = ucGridLookup1.Values;
            int _materielID = Convert.ToInt32(lookUpEdit1.EditValue);
            DateTime dt1 = Convert.ToDateTime("1900-1-1");
            DateTime dt2 = DateTime.Now.Date.AddYears(1);
            if(checkEdit1.Checked)
            {
                dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMilliseconds(-1);
                dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            }
            int _top = 0;
            if (checkEdit2.Checked)
                _top = 200;
            if (radioButton1.Checked)//订单
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetListForOne", new object[] { _top, _companyID, _materielID, dt1, dt2 }).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["客户"].ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
                gridView1.Columns["款号"].ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
                gridView1.Columns["商标"].ColumnEdit = BaseForm.RepositoryItem._reBrand;
                gridView1.Columns["单位"].ColumnEdit = BaseForm.RepositoryItem._reMeasure;
                gridView1.Columns["状态"].ColumnEdit = BaseForm.RepositoryItem._reIsVerify;
                gridView1.Columns["客户"].Width = gridView1.Columns["款号"].Width = gridView1.Columns["商标"].Width = gridView1.Columns["备注"].Width = 200;
                gridView1.Columns["数量"].Summary.Clear();
                gridView1.Columns["数量"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            //    gridView1.Columns["金额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            //new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            }
            else if(radioButton2.Checked)//销售
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetListForOne", new object[] { _top, _companyID, _materielID, dt1, dt2 }).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["客户"].ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
                gridView1.Columns["款号"].ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
                gridView1.Columns["商标"].ColumnEdit = BaseForm.RepositoryItem._reBrand;
                gridView1.Columns["单位"].ColumnEdit =gridView1.Columns["销售单位"].ColumnEdit= BaseForm.RepositoryItem._reMeasure;
                gridView1.Columns["状态"].ColumnEdit = BaseForm.RepositoryItem._reIsVerify;
                gridView1.Columns["销售单位"].Visible = gridView1.Columns["单位"].Visible = false;
                string per = BasicClass.BasicFile.GetPermissions(this.Text);
                if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                {
                    gridView1.Columns["金额"].Visible  = false;
                    gridView1.Columns["单价"].Visible  = false;
                }
                
                if (IsShowBoxMeasureID)
                {
                    gridView1.Columns["销售单位"].Visible = true;
                }
                else
                {
                    gridView1.Columns["单位"].Visible = true;
                }
                gridView1.Columns["客户"].Width = gridView1.Columns["款号"].Width = gridView1.Columns["商标"].Width = gridView1.Columns["备注"].Width = 200;
                gridView1.Columns["数量"].Summary.Clear();
                gridView1.Columns["金额"].Summary.Clear();
                gridView1.Columns["数量"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
                gridView1.Columns["金额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            }
            else if(radioButton3.Checked)//来款
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMoneyInOrOut, "GetInfoList", new object[] { dt1, dt2, _companyID, true }).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["客户"].ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
                gridView1.Columns[0].Width = gridView1.Columns[1].Width = gridView1.Columns[2].Width = gridView1.Columns[3].Width = 200;
                gridView1.Columns["收款金额"].Summary.Clear();
                gridView1.Columns["收款金额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            }
            else if(radioButton5.Checked)//累欠
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompanyLog, "GetInOutList", new object[] { 1, dt1, dt2 }).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["帐号"].Visible = false;
                gridView1.Columns["编号"].VisibleIndex = 0;
                gridView1.Columns["公司名"].VisibleIndex = 1;
                gridView1.Columns["手机"].VisibleIndex = 2;
                gridView1.Columns["期初"].Summary.Clear();
                gridView1.Columns["增加"].Summary.Clear();
                gridView1.Columns["减少"].Summary.Clear();
                gridView1.Columns["余额"].Summary.Clear();
                gridView1.Columns["结余"].Summary.Clear();
                gridView1.Columns["期初"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum) });
                gridView1.Columns["增加"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum) });
                gridView1.Columns["减少"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum) });
                gridView1.Columns["余额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum) });
                gridView1.Columns["结余"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum) });


            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if(gridView1.FocusedRowHandle>-1)
            {
                int _ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                if(radioButton1.Checked)
                {
                    Form fr = new Hownet.Sell.SalesForm(_ID);
                    fr.ShowDialog();
                }
                else if(radioButton2.Checked)
                {
                    Form fr = new Hownet.Sell.frSell1(_ID);
                    fr.ShowDialog();
                }
                else if(radioButton3.Checked)
                {
                    Form fr = new Hownet.Finance.BsInMoneyForm(_ID);
                    fr.ShowDialog();
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = checkEdit1.Checked;
        }
    }
}