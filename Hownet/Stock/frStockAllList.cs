using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Stock
{
    public partial class frStockAllList : DevExpress.XtraEditors.XtraForm
    {
        public frStockAllList()
        {
            InitializeComponent();
        }
        DataTable dtlist = new DataTable();
        private void frSellAllList_Load(object sender, EventArgs e)
        {
            DateTime dtNow = BasicClass.GetDataSet.GetDateTime();
            dateEdit1.EditValue = dtNow.Date.AddDays((dtNow.Day - 1) * -1);
            dateEdit2.EditValue = dtNow.Date;
            ucGridLookup1.DisplayMember = "Name";
            ucGridLookup1.ValueMember = "ID";
            ucGridLookup1.TypeID = (int)BasicClass.Enums.TableType.Supplier;
            lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtAllMateriel;
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
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBackInfo, "GetListForOne", new object[] { _top, _companyID, _materielID, dt1, dt2, 23 }).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["供应商"].ColumnEdit = BaseForm.RepositoryItem._reSupplier;
                gridView1.Columns["物料"].ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
                gridView1.Columns["颜色"].ColumnEdit = BaseForm.RepositoryItem._reColor;
                gridView1.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
                gridView1.Columns["单位"].ColumnEdit = BaseForm.RepositoryItem._reMeasure;
                gridView1.Columns["供应商"].Width = gridView1.Columns["物料"].Width = gridView1.Columns["颜色"].Width = gridView1.Columns["尺码"].Width = 200;
                gridView1.Columns["数量"].Summary.Clear();
                gridView1.Columns["金额"].Summary.Clear();
                gridView1.Columns["数量"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
                gridView1.Columns["金额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            }
            else if(radioButton2.Checked)//采购
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBackInfo, "GetListForOne", new object[] { _top, _companyID, _materielID, dt1, dt2 ,24}).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["供应商"].ColumnEdit = BaseForm.RepositoryItem._reSupplier;
                gridView1.Columns["物料"].ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
                gridView1.Columns["颜色"].ColumnEdit = BaseForm.RepositoryItem._reColor;
                gridView1.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
                gridView1.Columns["单位"].ColumnEdit = BaseForm.RepositoryItem._reMeasure;
                gridView1.Columns["供应商"].Width = gridView1.Columns["物料"].Width = gridView1.Columns["颜色"].Width = gridView1.Columns["尺码"].Width = 200;
                gridView1.Columns["数量"].Summary.Clear();
                gridView1.Columns["金额"].Summary.Clear();
                gridView1.Columns["数量"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
                gridView1.Columns["金额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            }
            else if(radioButton3.Checked)//付款
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMoneyInOrOut, "GetInfoList", new object[] { dt1, dt2, _companyID, false }).Tables[0];
                gridControl1.DataSource = dtlist;
                gridView1.Columns["ID"].Visible = false;
                gridView1.Columns["供应商"].ColumnEdit = BaseForm.RepositoryItem._reSupplier;
                gridView1.Columns[0].Width = gridView1.Columns[1].Width = gridView1.Columns[2].Width = gridView1.Columns[3].Width = 200;
                gridView1.Columns["付款金额"].Summary.Clear();
                gridView1.Columns["付款金额"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            }
            else if(radioButton5.Checked)//累欠
            {
                dtlist = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompanyLog, "GetInOutList", new object[] { 2, dt1, dt2 }).Tables[0];
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
                    Form fr = new Hownet.Stock.frStock(_ID);
                    fr.ShowDialog();
                }
                else if(radioButton2.Checked)
                {
                    Form fr = new Hownet.Stock.frSBack(_ID);
                    fr.ShowDialog();
                }
                else if(radioButton3.Checked)
                {
                    Form fr = new Hownet.Finance.BsOutMoneyForm(_ID);
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