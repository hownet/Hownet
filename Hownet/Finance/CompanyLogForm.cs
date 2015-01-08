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
    public partial class CompanyLogForm : DevExpress.XtraEditors.XtraForm
    {
        public CompanyLogForm()
        {
            InitializeComponent();
        }
        string blCL = "Hownet.BLL.CompanyLog";
        DateTime dtOne = Convert.ToDateTime("1900-1-1");
        DateTime dtTwo = Convert.ToDateTime("1900-1-1");
        int typeID = 0;
        private void CompanyLogForm_Load(object sender, EventArgs e)
        {
            radioGroup1.SelectedIndex = -1;
            radioGroup1.SelectedIndex = 0;
            DateTime dt=BasicClass.GetDataSet.GetDateTime().Date;
            dateEdit1.EditValue = dt.AddDays(dt.Day * -1 + 1);
            dateEdit2.EditValue = dt;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
           //// bool t = radioGroup1.SelectedIndex == 0 ? true : false;
           // int typeID = radioGroup1.SelectedIndex + 1;
           // if (typeID > 0)
           // {
           //     gridControl1.DataSource = BasicClass.GetDataSet.GetDS(blCL, "GetFinanceList", new object[] { typeID }).Tables[0];
           //     gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(gridControl1_ViewRegistered);
           // }
           // if (radioGroup1.SelectedIndex > -1)
           // {
           //     _leCompany.labText = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Description + "：";
           //     if (typeID == 1)
           //         _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
           //     else if (typeID == 2)
           //         _leCompany.FormName = (int)BasicClass.Enums.TableType.Supplier;
           //     else if (typeID == 3)
           //         _leCompany.FormName = (int)BasicClass.Enums.TableType.Processing;
           //     _leCompany.editVal = 0;
           // }
        }
        /// <summary>
        /// 显示子表
        /// </summary>
        void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            //try
            //{
            if (radioGroup1.SelectedIndex == 0)
            {
                DevExpress.XtraGrid.Views.Grid.GridView TemView = (DevExpress.XtraGrid.Views.Grid.GridView)(e.View);
                TemView.Columns.ColumnByFieldName("ID").Visible = false;
                TemView.Columns.ColumnByFieldName("TypeID").Visible = false;
                TemView.Columns.ColumnByFieldName("MaterielName").Caption = "款号";
                TemView.Columns.ColumnByFieldName("BrandName").Caption = "商标";
                TemView.Columns.ColumnByFieldName("Amount").Caption = "件数";
                TemView.Columns.ColumnByFieldName("BoxMeasureAmount").Caption = "包数";
                TemView.Columns.ColumnByFieldName("Conversion").Caption = "每包数量";
                TemView.Columns.ColumnByFieldName("Price").Caption = "单价";
                TemView.Columns.ColumnByFieldName("Money").Caption = "金额";
                TemView.Columns.ColumnByFieldName("MaterielName").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                TemView.Columns.ColumnByFieldName("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.Columns.ColumnByFieldName("Money").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.Columns.ColumnByFieldName("BoxMeasureAmount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.OptionsBehavior.Editable = false;
            }
            else if (radioGroup1.SelectedIndex == 2||radioGroup1.SelectedIndex ==1)
            {
                DevExpress.XtraGrid.Views.Grid.GridView TemView = (DevExpress.XtraGrid.Views.Grid.GridView)(e.View);
                TemView.Columns.ColumnByFieldName("ID").Visible = false;
                TemView.Columns.ColumnByFieldName("TypeID").Visible = false;
                TemView.Columns.ColumnByFieldName("TypeName").Caption = "商品类型";
                TemView.Columns.ColumnByFieldName("MaterielName").Caption = "商品名称";
                TemView.Columns.ColumnByFieldName("Amount").Caption = "数量";
                TemView.Columns.ColumnByFieldName("MeasureName").Caption = "单位";
                TemView.Columns.ColumnByFieldName("Price").Caption = "单价";
                TemView.Columns.ColumnByFieldName("Money").Caption = "金额";
                TemView.Columns.ColumnByFieldName("MaterielName").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                TemView.Columns.ColumnByFieldName("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.Columns.ColumnByFieldName("Money").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.OptionsBehavior.Editable = false;
            }
            //}
            //catch { }
        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BasicClass.cResult r = new BasicClass.cResult();
            int o = int.Parse(gridView1.GetFocusedRowCellValue(_coWhys).ToString());
            if (typeID == 1)
            {
                if (o==3)
                {
                    if (gridView1.GetFocusedRowCellValue(_coIndexs).ToString() != "")
                    {
                        Form fr = new Sell.frSell(int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                        fr.ShowDialog();
                    }
                }
                else if(o==4)
                {
                    Form fr = new Finance.BsInMoneyForm(int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                    fr.ShowDialog();
                }
                else if (o == 5)
                {
                    Form fr = new Sell.frSellBack(int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                    fr.ShowDialog();
                }
            }
            else if (typeID == 2)
            {
                if (o==1)
                {
                    if (gridView1.GetFocusedRowCellValue(_coIndexs).ToString() != "")
                    {
                        Form fr = new Stock.frSBack(int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                       fr.ShowDialog();
                    }
                }
                else if(o==2)
                {
                    Form fr = new Finance.BsOutMoneyForm(int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                    fr.ShowDialog();
                }
                else if (o == 6)
                {
                }
            }
            else if (typeID == 3)
            {
                if (o == 3)
                {
                    if (gridView1.GetFocusedRowCellValue(_coIndexs).ToString() != "")
                    {
                        //Form fr = new Sell.frSellProcess(int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                        //fr.ShowDialog();
                    }
                }
                else if(o==8)
                {
                    if (gridView1.GetFocusedRowCellValue(_coIndexs).ToString() != "")
                    {
                        //Form fr = new Task.frP2Pack (int.Parse(gridView1.GetFocusedRowCellValue(_coIndexs).ToString()));
                        //fr.ShowDialog();
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int _comID = int.Parse(_leCompany.editVal.ToString());
                DataTable dtCompanyMoneyList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompanyLog, "GetCompanyMoneyList", new object[] { (radioGroup1.SelectedIndex == 0), _comID,dtOne,dtTwo }).Tables[0];
                dtCompanyMoneyList.TableName = "CompanyMoneyList";
                dtCompanyMoneyList.Columns.Add("LastDate", typeof(DateTime));
                dtCompanyMoneyList.Columns.Add("LastMoney", typeof(decimal));
                if (dtCompanyMoneyList.Rows.Count > 0)
                {
                    DataTable dtTem = BasicClass.GetDataSet.GetBySql("SELECT     TOP (1) ID, CompanyID, Money, DateTime FROM  CompanyLog WHERE     (CompanyID = "+_comID+") ORDER BY ID DESC");
                    if (dtTem.Rows.Count > 0)
                    {
                        for(int i=0;i<dtCompanyMoneyList.Rows.Count;i++)
                        {
                            dtCompanyMoneyList.Rows[i]["LastDate"] = dtTem.Rows[0]["DateTime"];
                            dtCompanyMoneyList.Rows[i]["LastMoney"] = dtTem.Rows[0]["Money"];
                        }
                    }
                }
                BaseForm.PrintClass.PrintCompanyMoneyList((radioGroup1.SelectedIndex+1), dtCompanyMoneyList);
            }
            catch { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //DataTable dtCompanyMoney = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompanyLog, "GetCompanyMoney", new object[] { radioGroup1.SelectedIndex + 1 }).Tables[0];
            //dtCompanyMoney.TableName = "CompanyMoney";
            //BaseForm.PrintClass.PrintCompanyMoney((radioGroup1.SelectedIndex +1), dtCompanyMoney);

            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompanyLog, "GetInOutList", new object[] { radioGroup1.SelectedIndex + 1, dtOne, dtTwo });
           ds.Tables[0].TableName = "CompanyMoney";
            decimal SumMoney = 0;
            decimal Money = 0;
            decimal AddMoney = 0;
            decimal LassMoney = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["余额"].ToString() != string.Empty)
                    SumMoney += Convert.ToDecimal(ds.Tables[0].Rows[i]["余额"]);

                if (ds.Tables[0].Rows[i]["期初"].ToString() != string.Empty)
                    Money += Convert.ToDecimal(ds.Tables[0].Rows[i]["期初"]);

                if (ds.Tables[0].Rows[i]["增加"].ToString() != string.Empty)
                    AddMoney += Convert.ToDecimal(ds.Tables[0].Rows[i]["增加"]);

                if (ds.Tables[0].Rows[i]["减少"].ToString() != string.Empty)
                    LassMoney += Convert.ToDecimal(ds.Tables[0].Rows[i]["减少"]);
            }
            DataTable dtTem = new DataTable();
            dtTem.Columns.Add("总余额", typeof(decimal));
            dtTem.Columns.Add("总期初", typeof(decimal));
            dtTem.Columns.Add("总增加", typeof(decimal));
            dtTem.Columns.Add("总减少", typeof(decimal));
            dtTem.Columns.Add("dt1", typeof(string));
            dtTem.Columns.Add("dt2", typeof(string));
            dtTem.TableName = "Tem";

            DataRow dr = dtTem.NewRow();
            dr[0] = SumMoney;
            dr[1] = Money;
            dr[2] = AddMoney;
            dr[3] = LassMoney;
            dr[4] = dtOne.ToLongDateString();
            dr[5] = dtTwo.AddDays(-1).ToLongDateString();
            dtTem.Rows.Add(dr);

            ds.Tables.Add(dtTem);
            BaseForm.PrintClass.PrintCompanyMoney((radioGroup1.SelectedIndex + 1), ds);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
             dtOne = Convert.ToDateTime(dateEdit1.EditValue).Date;
             dtTwo = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
             typeID = radioGroup1.SelectedIndex + 1;
            if (typeID > 0)
            {
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(blCL, "GetFinanceList", new object[] { typeID,dtOne,dtTwo }).Tables[0];
                gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(gridControl1_ViewRegistered);
            }
            if (radioGroup1.SelectedIndex > -1)
            {
                _leCompany.labText = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Description + "：";
                if (typeID == 1)
                    _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
                else if (typeID == 2)
                    _leCompany.FormName = (int)BasicClass.Enums.TableType.Supplier;
                else if (typeID == 3)
                    _leCompany.FormName = (int)BasicClass.Enums.TableType.Processing;
                _leCompany.editVal = 0;
            }
        }
    }
}