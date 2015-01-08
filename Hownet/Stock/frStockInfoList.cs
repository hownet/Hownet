using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace Hownet.Stock
{
    public partial class frStockInfoList : DevExpress.XtraEditors.XtraForm
    {
        public frStockInfoList()
        {
            InitializeComponent();
        }
        DataTable dtColor = new DataTable();
        DataTable dtIsVerify = new DataTable();
        string per = BasicClass.BasicFile.GetPermissions("采购收货明细");
        private void frStockInfoList_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=2" }).Tables[0];
                DataRow drCom = dtCom.NewRow();
                drCom["ID"] = 0;
                drCom["Name"] = string.Empty;
                dtCom.Rows.Add(drCom);
                lookUpEdit1.Properties.DataSource = dtCom.DefaultView;
                dateEdit1.EditValue = Convert.ToDateTime(BasicClass.GetDataSet.GetDateTime().Year.ToString() + "-1-1");
                dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
                dtColor = BasicClass.BaseTableClass.dtColor;
                lookUpEdit1.EditValue = 0;
                repositoryItemLookUpEdit1.DataSource = dtColor;
                gridView1.OptionsBehavior.Editable = false;
                dtIsVerify.Columns.Add("Name", typeof(string));
                dtIsVerify.Columns.Add("ID", typeof(int));

                dtIsVerify.Rows.Add("未审核", "1");
                dtIsVerify.Rows.Add("审核中", "2");
                dtIsVerify.Rows.Add("已审核", "3");
                dtIsVerify.Rows.Add("开始生产", "4");
                dtIsVerify.Rows.Add("待确认", "5");
                dtIsVerify.Rows.Add("确认通过", "6");
                dtIsVerify.Rows.Add("合并生产", "7");
                dtIsVerify.Rows.Add("已完成", "9");
                dtIsVerify.Rows.Add("开始备料", "10");
                dtIsVerify.Rows.Add("客户取消", "21");
                dtIsVerify.Rows.Add("公司取消", "22");
                dtIsVerify.Rows.Add("已过帐", "31");
                repositoryItemLookUpEdit2.DataSource = dtIsVerify;
               _co供应商.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
               _co数量.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                _co金额.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                _co颜色.ColumnEdit = _co配色一.ColumnEdit = _co配色二.ColumnEdit = repositoryItemLookUpEdit1;
                _co状态.ColumnEdit = repositoryItemLookUpEdit2;
                _co尺码.ColumnEdit=BaseForm.RepositoryItem._reSize;
                _co单位.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
                _co物料名.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
               // ConditionsAdjustment();


            }
            catch (Exception ex)
            {
            }
        }
        private void ConditionsAdjustment()
        {
            StyleFormatCondition cn;
            cn = new StyleFormatCondition(FormatConditionEnum.Less, gridView1.Columns["订货单价"], null, gridView1.Columns["数量"]);
            cn.Appearance.BackColor = Color.Yellow;
            gridView1.FormatConditions.Add(cn);
            cn = new StyleFormatCondition(FormatConditionEnum.Greater, gridView1.Columns["订货单价"], null, gridView1.Columns["数量"]);
            cn.Appearance.BackColor = Color.Red;
            cn.Appearance.ForeColor = Color.White;
            gridView1.FormatConditions.Add(cn);
            //..
            gridView1.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int CompanyID = Convert.ToInt32(lookUpEdit1.EditValue);
            bool _IsSum = checkEdit1.Checked;
           // gridView1.Columns.Clear();
            gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBack, "GetInfoList", new object[] {dt1,dt2,CompanyID,_IsSum }).Tables[0];
            //if (gridView1.RowCount > 0)
            //{
            //    decimal _price = 0;
            //    decimal _oldPrice = 0;
            //    for (int i = 0; i < gridView1.RowCount; i++)
            //    {
            //        try
            //        {
            //            _price = Convert.ToDecimal(gridView1.GetRowCellValue(i, _co单价));
            //            _oldPrice = Convert.ToDecimal(gridView1.GetRowCellValue(i, _co订货单价));
            //            if(_oldPrice>_price)
            //                gridView1.set
            //        }
            //        catch
            //        { }
            //    }
            //}
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Posting).ToString()) == -1)
                return;
            if (gridView1.FocusedRowHandle > -1)
            {
                int _id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                string bllSB=BasicClass.Bllstr.bllStockBack;
                string blCL=BasicClass.Bllstr.bllCompanyLog;
                DataTable dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _id + ")" }).Tables[0];
                int _companyID = Convert.ToInt32(dtSB.Rows[0]["CompanyID"]);
                if (_companyID > 0)
                {
                    if (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) < 3)
                    {
                        XtraMessageBox.Show("请先审核！");
                        return;
                    }
                    if (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) == (int)BasicClass.Enums.IsVerify.已过帐)
                    {
                        XtraMessageBox.Show("本单已过帐！");
                        return;
                    }
                    if (BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "(TableID=" + _id + ") And (TypeID=" + (int)BasicClass.Enums.MoneyTableType.Back + ")" }).Tables[0].Rows.Count > 0)
                    {
                        XtraMessageBox.Show("本单已过帐！");
                        return;
                    }
                    if (DialogResult.No == XtraMessageBox.Show("是否确认过帐处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        return;
                    }
                    decimal last, back, money;
                    DataTable dtBack = new DataTable();
                    string backDate;
                    money = Convert.ToDecimal(dtSB.Rows[0]["Money"]);
                    last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                    dtBack.Clear();
                    dtBack = BasicClass.GetDataSet.GetDS(blCL, "GetBackMoney", new object[] { _companyID }).Tables[0];
                    back = 0;
                    backDate = "      年    月    日";
                    dtSB.Rows[0]["BackDate"] = DateTime.Parse("1900-1-1");
                    if (dtBack.Rows.Count > 0)
                    {
                        back = 0;
                        backDate = "      年    月    日";
                        if (dtBack.Rows.Count > 0)
                        {
                            if (dtBack.Rows[0]["Money"] != null && dtBack.Rows[0]["Money"].ToString().Trim() != string.Empty)
                            {
                                back = decimal.Parse(dtBack.Rows[0]["Money"].ToString());
                                dtSB.Rows[0]["BackDate"] = dtBack.Rows[0]["DateTime"];
                                backDate = ((DateTime)(dtBack.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                            }
                        }

                    }
                    dtSB.Rows[0]["LastMoney"] = last;
                    dtSB.Rows[0]["BackMoney"] = back;
                    dtSB.Rows[0]["IsVerify"] = (int)BasicClass.Enums.IsVerify.已过帐;



                    DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
                    DataRow dr = dtCL.NewRow();
                    dr["ID"] = 0;
                    dr["CompanyID"] = _companyID;
                    dr["DateTime"] = dtSB.Rows[0]["DataTime"];
                    dr["LastMoney"] = last + back;
                    dr["ChangMoney"] = money;
                    dr["Money"] = last + money - back;
                    dr["TypeID"] = (int)(BasicClass.Enums.MoneyTableType.Back);
                    dr["TableID"] = _id;
                    dr["NowMoneyTypeID"] = 0;
                    dr["NowMoney"] = 0;
                    dr["NowReta"] = 1;
                    dr["A"] = 1;
                    dtCL.Rows.Add(dr);
                    BasicClass.GetDataSet.Add(blCL, dtCL);
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllCompany, "UpLastMoney", new object[] { _companyID, (last + money - back) });
                    BasicClass.GetDataSet.UpData(bllSB, dtSB);
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                Form fr = new Stock.frSBack(int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString()),true);
                fr.ShowDialog();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string fileName = Hownet.BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }
    }
}