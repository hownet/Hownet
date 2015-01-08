using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Sell
{
    public partial class frSellAnalysis : DevExpress.XtraEditors.XtraForm
    {
        public frSellAnalysis()
        {
            InitializeComponent();
        }
        DataTable dtMat = new DataTable();
        DataTable dtBrand = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtUser = new DataTable();
        DataTable dt = new DataTable();
        string strWhere = string.Empty;
        string strFiled = string.Empty;
        string strGroup = string.Empty;
        private void frSellAnalysis_Load(object sender, EventArgs e)
        {
            dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
            dtBrand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=5)" }).Tables[0];
            dtCompany = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=1)" }).Tables[0];
            dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            dtMat.Rows.Add(1, 0, "");
            dtBrand.Rows.Add(1, 0, "");
            dtCompany.Rows.Add(1, 0, "");
            dtUser.Rows.Add(1, 0, "");
            dtMat.DefaultView.Sort = dtBrand.DefaultView.Sort = dtCompany.DefaultView.Sort = dtUser.DefaultView.Sort = "ID";
            _reMat.DataSource = _leMat.Properties.DataSource = dtMat.DefaultView;
            _reBrand.DataSource = _leBrand.Properties.DataSource = dtBrand.DefaultView;
            _reCompany.DataSource = _leCompany.Properties.DataSource = dtCompany.DefaultView;
            _reFillMan.DataSource = _leUser.Properties.DataSource = dtUser.DefaultView;
            dateEdit1.EditValue = BasicClass.GetDataSet.GetDateTime().Date.AddYears(-1);
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;

            _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            if (!BasicClass.BasicFile.liST[0].Sell4Depot )
            {
                checkedListBoxControl1.Items[3].Enabled = checkedListBoxControl1.Items[4].Enabled = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            dt.Columns.Clear();
          //  gridView1.Columns.Clear();
           strFiled = strGroup = strWhere = string.Empty;
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
             strWhere = " And (DateTime>'" + dt1 + "') And (DateTime<'" + dt2 + "')";
            int Mat = Convert.ToInt32(_leMat.EditValue);
            int Brand = Convert.ToInt32(_leBrand.EditValue);
            int CompanyID = Convert.ToInt32(_leCompany.EditValue);
            int UserID = Convert.ToInt32(_leUser.EditValue);
            #region  //客户订货
            if (radioGroup1.SelectedIndex == 0)
            {
                strFiled += " ,SalesOrderInfoList.MeasureID ";
                strGroup += " ,SalesOrderInfoList.MeasureID ";
                if (Mat > 0)
                    strWhere += " And (SalesOrderInfoList.MaterielID=" + Mat + ")";
                if (Brand > 0)
                    strWhere += "And (SalesOrderInfoList.BrandID=" + Brand + ")";
                if (CompanyID > 0)
                    strWhere += "And (SalesOrderInfoList.CompanyID=" + CompanyID + ")";
                if (UserID > 0)
                    strWhere += " And (SalesOrderInfoList.FillMan=" + UserID + ")";
                //客户
                if (checkedListBoxControl1.Items[0].CheckState == CheckState.Checked)
                {
                    strFiled += ",SalesOrderInfoList.CompanyID";
                    strGroup += ",SalesOrderInfoList.CompanyID";
                }
                //款号
                if (checkedListBoxControl1.Items[1].CheckState == CheckState.Checked)
                {
                    strFiled += ", SalesOrderInfoList.MaterielID";
                    strGroup += ", SalesOrderInfoList.MaterielID";
                }
                //商标
                if (checkedListBoxControl1.Items[2].CheckState == CheckState.Checked)
                {
                    strFiled += ", SalesOrderInfoList.BrandID";
                    strGroup += ", SalesOrderInfoList.BrandID";
                }
                //颜色
                if (checkedListBoxControl1.Items[3].CheckState == CheckState.Checked)
                {
                    strFiled += ", AmountInfo.ColorID";
                    strGroup += ", AmountInfo.ColorID";
                }
                //尺码
                if (checkedListBoxControl1.Items[4].CheckState == CheckState.Checked)
                {
                    strFiled += ", AmountInfo.SizeID";
                    strGroup += ", AmountInfo.SizeID";
                }
                //单价
                if (checkedListBoxControl1.Items[5].CheckState == CheckState.Checked)
                {
                    strFiled += ",cast( SalesOrderInfoList.Price as Real) as Price";
                    strGroup += ", SalesOrderInfoList.Price";
                }

                //业务员
                if (checkedListBoxControl1.Items[7].CheckState == CheckState.Checked)
                {
                    strFiled += ", SalesOrderInfoList.FillMan";
                    strGroup += ", SalesOrderInfoList.FillMan";
                }

                //年
                if (radioGroup2.SelectedIndex == 0)
                {
                    strFiled += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' as Date ";
                    strGroup += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年'  ";
                }
                //季度
                else if (radioGroup2.SelectedIndex == 1)
                {
                    strFiled += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' + '第' + CAST(DATEPART(qq, SalesOrderInfoList.DateTime) AS varchar) + '季度' as Date ";
                    strGroup += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' + '第' + CAST(DATEPART(qq, SalesOrderInfoList.DateTime) AS varchar) + '季度' ";
                }
                //月
                else if (radioGroup2.SelectedIndex == 2)
                {
                    strFiled += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' + CAST(DATEPART(mm, SalesOrderInfoList.DateTime) AS varchar) + '月' as Date";
                    strGroup += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' + CAST(DATEPART(mm, SalesOrderInfoList.DateTime) AS varchar) + '月' ";
                }
                //周
                else if (radioGroup2.SelectedIndex == 3)
                {
                    strFiled += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' +'第'+ CAST(DATEPART(wk, SalesOrderInfoList.DateTime) AS varchar) + '周'  as Date ";
                    strGroup += ", CAST(DATEPART(yy, SalesOrderInfoList.DateTime) AS varchar) + '年' +'第'+ CAST(DATEPART(wk, SalesOrderInfoList.DateTime) AS varchar) + '周'  ";
                }
                //天
                else if (radioGroup2.SelectedIndex == 4)
                {
                    strFiled += ", CONVERT(varchar(10), SalesOrderInfoList.DateTime, 120)  as Date";
                    strGroup += ", CONVERT(varchar(10), SalesOrderInfoList.DateTime, 120)  ";
                }
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAnalysisList", new object[] { strFiled, strGroup, strWhere }).Tables[0];
                if (checkedListBoxControl1.Items[6].CheckState == CheckState.Checked)
                {
                    dt.Columns.Add("Money", typeof(decimal));
                    if (checkedListBoxControl1.Items[5].CheckState == CheckState.Checked)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["Money"] = Convert.ToDecimal(dt.Rows[i]["SumAmount"]) * Convert.ToDecimal(dt.Rows[i]["Price"]);
                        }
                    }
                }
                dt.DefaultView.Sort = "Date";
                gridControl1.DataSource = dt.DefaultView;
            }
            #endregion
            //销售发货
            else
            {
                strFiled += " ProduceSellOne.MeasureID ";
                strGroup += " ,ProduceSellOne.MeasureID ";
                if (Mat > 0)
                    strWhere += " And (ProduceSellOne.MaterielID=" + Mat + ")";
                if (Brand > 0)
                    strWhere += "And (ProduceSellOne.BrandID=" + Brand + ")";
                if (CompanyID > 0)
                    strWhere += "And (ProduceSell.CompanyID=" + CompanyID + ")";
                //客户
                if (checkedListBoxControl1.Items[0].CheckState == CheckState.Checked)
                {
                    strFiled += ",ProduceSell.CompanyID";
                    strGroup += ",ProduceSell.CompanyID";
                }
                //款号
                if (checkedListBoxControl1.Items[1].CheckState == CheckState.Checked)
                {
                    strFiled += ", ProduceSellOne.MaterielID";
                    strGroup += ", ProduceSellOne.MaterielID";
                }
                //商标
                if (checkedListBoxControl1.Items[2].CheckState == CheckState.Checked)
                {
                    strFiled += ", ProduceSellOne.BrandID";
                    strGroup += ", ProduceSellOne.BrandID";
                }
                //颜色
                if (checkedListBoxControl1.Items[3].CheckState == CheckState.Checked)
                {
                    strFiled += ", ProduceSellInfo.ColorID";
                    strGroup += ", ProduceSellInfo.ColorID";
                }
                //尺码
                if (checkedListBoxControl1.Items[4].CheckState == CheckState.Checked)
                {
                    strFiled += ", ProduceSellInfo.SizeID";
                    strGroup += ", ProduceSellInfo.SizeID";
                }
                //单价
                if (checkedListBoxControl1.Items[5].CheckState == CheckState.Checked)
                {
                    strFiled += ",cast( ProduceSellOne.Price as Real) as Price";
                    strGroup += ", ProduceSellOne.Price";
                }

                //年
                if (radioGroup2.SelectedIndex == 0)
                {
                    strFiled += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' as Date ";
                    strGroup += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年'  ";
                }
                //季度
                else if (radioGroup2.SelectedIndex == 1)
                {
                    strFiled += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' + '第' + CAST(DATEPART(qq, ProduceSell.DateTime) AS varchar) + '季度' as Date ";
                    strGroup += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' + '第' + CAST(DATEPART(qq, ProduceSell.DateTime) AS varchar) + '季度' ";
                }
                //月
                else if (radioGroup2.SelectedIndex == 2)
                {
                    strFiled += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' + CAST(DATEPART(mm, ProduceSell.DateTime) AS varchar) + '月' as Date";
                    strGroup += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' + CAST(DATEPART(mm, ProduceSell.DateTime) AS varchar) + '月' ";
                }
                //周
                else if (radioGroup2.SelectedIndex == 3)
                {
                    strFiled += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' +'第'+ CAST(DATEPART(wk, ProduceSell.DateTime) AS varchar) + '周'  as Date ";
                    strGroup += ", CAST(DATEPART(yy, ProduceSell.DateTime) AS varchar) + '年' +'第'+ CAST(DATEPART(wk, ProduceSell.DateTime) AS varchar) + '周'  ";
                }
                //天
                else if (radioGroup2.SelectedIndex == 4)
                {
                    strFiled += ", CONVERT(varchar(10), ProduceSell.DateTime, 120)  as Date";
                    strGroup += ", CONVERT(varchar(10), ProduceSell.DateTime, 120)  ";
                }
                if (checkedListBoxControl1.Items[3].CheckState == CheckState.Checked || checkedListBoxControl1.Items[4].CheckState == CheckState.Checked)
                {
                    strFiled += ", SUM(ProduceSellInfo.Amount) AS SumAmount  ";
                    dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSell, "GetPSAnListByCS", new object[] { strFiled, strGroup, strWhere, checkedListBoxControl1.Items[4].Enabled }).Tables[0];
                }
                else
                {
                    strFiled += ", SUM(ProduceSellOne.Amount) AS SumAmount  ";
                    dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSell, "GetPSAnalysisList", new object[] { strFiled, strGroup, strWhere, checkedListBoxControl1.Items[4].Enabled }).Tables[0];
                }
                if (checkedListBoxControl1.Items[6].CheckState == CheckState.Checked)
                {
                    dt.Columns.Add("Money", typeof(decimal));
                    if (checkedListBoxControl1.Items[5].CheckState == CheckState.Checked)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["Money"] = Convert.ToDecimal(dt.Rows[i]["SumAmount"]) * Convert.ToDecimal(dt.Rows[i]["Price"]);
                        }
                    }
                }
                if (checkedListBoxControl1.Items[1].CheckState == CheckState.Checked && checkedListBoxControl1.Items[5].CheckState == CheckState.Checked)
                {
                    dt.Columns.Add("CBJ", typeof(decimal));
                    dt.Columns.Add("LR", typeof(decimal));
                    DataRow[] drs;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        drs = dtMat.Select("(ID=" + dt.Rows[i]["MaterielID"] + ")");
                        if (drs.Length > 0)
                        {
                            dt.Rows[i]["CBJ"] = drs[0]["ChengBengJ"];
                            dt.Rows[i]["LR"] = (Convert.ToDecimal(dt.Rows[i]["Price"]) - Convert.ToDecimal(drs[0]["ChengBengJ"])) * Convert.ToDecimal(dt.Rows[i]["SumAmount"]);
                        }
                    }
                }
                dt.DefaultView.Sort = "Date";
                
                gridControl1.DataSource = dt.DefaultView;
            }
            ShowChart(radioGroup3.SelectedIndex);
        }
        private void ShowChart(int TypeID)
        {
            chartControl1.Series.Clear();
            if (checkedListBoxControl1.CheckedItems.Count > 1 || checkedListBoxControl1.CheckedItems.Count == 0)
            {
                splitContainer1.Panel2Collapsed = true;
                return;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
            }
            DataTable dtOne = new DataTable();
            DataTable dtTem = dt.Copy();
            string strFiled = string.Empty;
            for(int i=0;i<checkedListBoxControl1.Items.Count;i++)
            {
                 if (checkedListBoxControl1.GetItemChecked(i))
                 {
                     strFiled=checkedListBoxControl1.Items[i].Description;
                     break;
                 }
            }
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Caption == strFiled)
                {
                    strFiled = gridView1.Columns[i].FieldName;
                    break;
                }
            }
            dtOne.Columns.Add("ss", typeof(string));
            dtOne.Columns.Add("nn", typeof(int));
            bool t = false;
            for (int i = 0; i < gridView1.RowCount;i++)
            {
                t = false;
                for (int j = 0; j < dtOne.Rows.Count; j++)
                {
                    if (gridView1.GetRowCellDisplayText(i, strFiled) == dtOne.Rows[j][0].ToString())
                    {
                        t = true;
                        break;
                    }
                }
                if (!t)
                {
                    dtOne.Rows.Add(gridView1.GetRowCellDisplayText(i, strFiled), gridView1.GetRowCellValue(i, strFiled));
                }
            }
            for (int r = 0; r < dtOne.Rows.Count; r++)
            {
                if(TypeID==0)
                    chartControl1.Series.Add(dtOne.Rows[r][0].ToString(), DevExpress.XtraCharts.ViewType.Line);
                else if(TypeID==1)
                    chartControl1.Series.Add(dtOne.Rows[r][0].ToString(), DevExpress.XtraCharts.ViewType.Bar);
                if (dtOne.Rows[r][1].ToString() != string.Empty)
                {
                  dtTem.DefaultView.RowFilter = strFiled + "=" + dtOne.Rows[r][1].ToString();

                }
                for (int c = 0; c < dtTem.DefaultView.Count; c++)
                {
                    chartControl1.Series[r].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dtTem.DefaultView[c]["Date"].ToString(), new object[] { (dtTem.DefaultView[c]["SumAmount"].ToString()) }));
                }

                chartControl1.Series[r].Visible = true;
            }
        }

        private void radioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChart(radioGroup3.SelectedIndex);
        }


    }
}