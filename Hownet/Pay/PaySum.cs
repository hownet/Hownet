using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace Hownet.Pay
{
    public partial class PaySum : DevExpress.XtraEditors.XtraForm
    {
        public PaySum()
        {
            InitializeComponent();
        }
        string blPM = "Hownet.BLL.PayMain";
        string blP = "Hownet.BLL.Pay";
        string blPS = "Hownet.BLL.PaySum";
        BindingSource bs = new BindingSource();
        DataTable dtMain = new DataTable();
        DataTable dtPM = new DataTable();
        DataSet dsTem = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtSet = new DataTable();
        DataTable dtEmp = new DataTable();
        bool t = false;
        int val = 0;
        bool f = false;
        int _mainID = 0;
        int _lassDeposit, _poorDeposit;
        bool IsCaicDeposit = false;
        bool IsEditFac = false;
        decimal Fac = 0;

        private void PaySum_Load(object sender, EventArgs e)
        {
            if (!(bool)(BasicClass.GetDataSet.GetOne(blPM, "NoEnd", null)))
            {
                _brAddNew.Enabled = false;
            }
             dtSet = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPayColumnsSet, "GetAllList", null).Tables[0];
            int j = 5;
            for (int i = 0; i < dtSet.Rows.Count; i++)
            {
                if (dtSet.Rows[i]["Name"].ToString().Length > 0)
                {
                    gridView1.Columns[dtSet.Rows[i]["ColumnsName"].ToString()].Caption = dtSet.Rows[i]["Name"].ToString();
                  //  gridView1.Columns[dtSet.Rows[i]["ColumnsName"].ToString()].VisibleIndex = j;
                    j += 1;
                }
                else
                {
                    gridView1.Columns[dtSet.Rows[i]["ColumnsName"].ToString()].Visible = false;
                }
            }
            dt.TableName = "EmpPay";
            dtEmp = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetViewList", null).Tables[0];
            DataRow dr = dtEmp.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtEmp.Rows.Add(dr);
            dtEmp.DefaultView.Sort = "ID";
            lookUpEdit1.Properties.DataSource = dtEmp.DefaultView;
            lookUpEdit1.EditValue = 0;
            _rePayID.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "计薪方式" }).Tables[0];
            bs.PositionChanged += new EventHandler(_dataNavigator1_PositionChanged);
            showData();
            bs.Position = dtMain.Rows.Count - 1;
            showValue(bs.Position);
            _coKuanHao.ColumnEdit =BaseForm. RepositoryItem._reProduce;
            dt.Columns.Add("EmployeeID", typeof(int));
            dt.Columns.Add("Sn", typeof(string));
            dt.Columns.Add("LastRemain", typeof(decimal));
            dt.Columns.Add("Repair", typeof(decimal));
            dt.Columns.Add("BoardWages", typeof(decimal));
            dt.Columns.Add("Payment", typeof(decimal));
            dt.Columns.Add("Fact", typeof(decimal));
            dt.Columns.Add("Remain", typeof(decimal));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Month", typeof(decimal));
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("WorkingName", typeof(string));
            dt.Columns.Add("Amount", typeof(int));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("OneDate", typeof(string));
            dt.Columns.Add("TwoDate", typeof(string));
            dt.Columns.Add("EmployID", typeof(int));
            dt.Columns.Add("Money", typeof(decimal));
            dt.Columns.Add("NowMoney", typeof(decimal));
            dt.Columns.Add("SumAmount", typeof(decimal));
            dt.Columns.Add("FullAttendance", typeof(decimal));
            dt.Columns.Add("Fine", typeof(decimal));
            dt.Columns.Add("Deposit", typeof(decimal));
            dt.Columns.Add("AllDeposit", typeof(decimal));
            dt.Columns.Add("DeparmentName", typeof(string));
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    simpleButton1.Enabled = _brVerify.Enabled = _brUnVerify.Enabled = _brAddNew.Enabled = _brDel.Enabled = _brSave.Enabled = _brEdit.Enabled = false;
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //}
            DataTable dtttt = BasicClass.GetDataSet.GetDS("Hownet.BLL.SysFormula", "GetList", new object[] { "(TypeID=" + (int)BasicClass.Enums.Formula.押金 + ")" }).Tables[0];
            if (dtttt.Rows.Count > 0)
            {
                _lassDeposit = Convert.ToInt32(dtttt.Rows[0]["Value1"]);
                _poorDeposit = Convert.ToInt32(dtttt.Rows[0]["Value2"]);
                try
                {
                    IsCaicDeposit = (Convert.ToInt32(dtttt.Rows[0]["Value3"]) == 1);
                }
                catch
                {
                }
            }
            _coDeposit.OptionsColumn.AllowEdit = !IsCaicDeposit;
            _coDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            _coEmployeeID.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
        }

        void _dataNavigator1_PositionChanged(object sender, EventArgs e)
        {
            if (bs.Position > -1)
            {
                showValue(bs.Position);
            }
        }
        private void showData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(blPM, "GetIDList", null).Tables[0];
            if (dtMain.Rows.Count == 0)
            {
                dtMain.Rows.Add(dtMain.NewRow());
            }
            bs.DataSource = dtMain.DefaultView;

        }
        private void showValue(int p)
        {
            #region 移动按钮
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;
            _brNext.Enabled = true;
            _brLast.Enabled = true;
            if (bs.Position == 0)
            {
                _brFrist.Enabled = false;
                _brPrv.Enabled = false;
            }
            if (bs.Position == dtMain.Rows.Count - 1)
            {
                _brNext.Enabled = false;
                _brLast.Enabled = false;
            }
            #endregion
            t = false;
            f = false;
            simpleButton1.Visible = true;
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _mainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtPM = BasicClass.GetDataSet.GetDS(blPM, "GetList", new object[] { "(ID=" + _mainID + ")" }).Tables[0];
                if (dtPM.Rows.Count > 0)
                {
                    _ldBegingDate.val = dtPM.Rows[0]["BegingDate"];
                    _ldEndDate.val = dtPM.Rows[0]["EndDate"];
                    _ldFillDate.val = dtPM.Rows[0]["DateTime"];
                }
                simpleButton1.Visible = false;
                labelControl3.Visible = lookUpEdit1.Visible = false;
            }
            else
            {
                _mainID = 0;
                dtPM = BasicClass.GetDataSet.GetDS(blPM, "GetList", new object[] { "(ID=" + _mainID + ")" }).Tables[0];
                DataRow dr = dtPM.NewRow();
                dr["VerifyMan"] = 0;
                dr["VerifyDateTime"] = DateTime.Parse("1900-1-1");
                dr["IsVerify"] = 1;
                dr["ID"] = 0;
                dr["Indexs"] = 0;
                dr["VerifyDateTime"] = DateTime.Parse("1900-1-1");
                dr["CaicType"] = 100;
                _brVerify.Enabled = false;
                _brUnVerify.Enabled = false;
                if (dtMain.Rows.Count > 1)
                {
                    DateTime ddt = (DateTime)(BasicClass.GetDataSet.GetDS(blPM, "GetList", new object[] { "(ID=" + int.Parse(dtMain.Rows[dtMain.Rows.Count - 2]["ID"].ToString()) + ")" }).Tables[0].Rows[0]["EndDate"]);
                    _ldBegingDate.MinDate = ddt.AddDays(1);
                     _ldEndDate.MinDate =ddt.AddDays(1);
                    _ldBegingDate.val = dr["BegingDate"] = ddt.AddDays(1); 
                    _ldEndDate.val=  dr["EndDate"] = ddt.AddMonths(1).AddDays(-1);
                }
                else
                {
                    _ldBegingDate.val = dr["BegingDate"] = DateTime.Parse(DateTime.Today.Year.ToString() + "-1-1");
                    _ldEndDate.val = dr["EndDate"] = DateTime.Parse(DateTime.Today.Year.ToString() + "-1-31");
                }
                dtPM.Rows.Add(dr);
                labelControl3.Visible = lookUpEdit1.Visible = true;

            }
            val = Convert.ToInt32(dtPM.Rows[0]["CaicType"]);
            t = (int.Parse(dtPM.Rows[0]["IsVerify"].ToString()) == 3);
            _brSave.Enabled = !t;
            _brEdit.Enabled = !t;
            _brDel.Enabled = !t;
            _brSum.Enabled = !t;
            _brVerify.Enabled = (!t && _mainID > 0);
            if (t && bs.Position == dtMain.Rows.Count - 1)
                _brAddNew.Enabled = _brUnVerify.Enabled = t;
            else
                _brAddNew.Enabled = _brUnVerify.Enabled = false;
            if (!t && bs.Position == dtMain.Rows.Count - 1)
                _brDel.Enabled = true;
            else
                _brDel.Enabled = false;
            _ldBegingDate.t = _ldEndDate.t = false;
            if (_mainID > 0)
            {
                _ldBegingDate.t = _ldEndDate.t = true;
            }
            else
            {
                if (dtMain.Rows.Count > 1)
                {
                    _ldBegingDate.t = true;
                }
            }
            gridView1.OptionsBehavior.Editable = false;
            gridView2.OptionsBehavior.Editable = false;
            dsTem =BasicClass.GetDataSet.GetDS(blP,"GetEmpPay", new object[]{_mainID});
            gridControl2.DataSource = dsTem.Tables[0];
            gridControl2.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(gridControl2_ViewRegistered);
            barButtonItem14.Down = barButtonItem15.Down = barButtonItem16.Down = barButtonItem17.Down = barButtonItem18.Down = false;
            IsEditFac = false;
            if (val == 100)
                barButtonItem14.Down = true;
            else if (val == 50)
                barButtonItem15.Down = true;
            else if (val == 10)
                barButtonItem16.Down = true;
            else if (val == 1)
                barButtonItem17.Down = true;
            else if (val == 0)
                barButtonItem18.Down = true;
        }
        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position = 0;
            _brFrist.Enabled = false;
            _brPrv.Enabled = false;
            _brNext.Enabled = true;
            _brLast.Enabled = true;
        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position -= 1;
            if (bs.Position == 0)
            {
                _brFrist.Enabled = false;
                _brPrv.Enabled = false;
            }
            _brNext.Enabled = true;
            _brLast.Enabled = true;
        }
        /// <summary>
        /// 下一条
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position += 1;
            if (bs.Position == dtMain.Rows.Count - 1)
            {
                _brNext.Enabled = false;
                _brLast.Enabled = false;
            }
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;
        }
        /// <summary>
        /// 尾记录
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position = dtMain.Rows.Count - 1;
            _brNext.Enabled = false;
            _brLast.Enabled = false;
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;
        }
        /// <summary>
        /// 新单
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _brNext.Enabled = false;
            _brLast.Enabled = false;
            _brAddNew.Enabled = false;
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }
        #endregion
        /// <summary>
        /// 显示子表
        /// </summary>
        void gridControl2_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView TemView = (DevExpress.XtraGrid.Views.Grid.GridView)(e.View);
                TemView.Columns.ColumnByFieldName("EmployeeID").ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
                TemView.Columns.ColumnByFieldName("MaterielID").ColumnEdit = BaseForm.RepositoryItem._reProduce;
                TemView.Columns.ColumnByFieldName("WorkingID").ColumnEdit = BaseForm.RepositoryItem._reWorking;
                TemView.Columns.ColumnByFieldName("MaterielID").Caption = "款号";
                TemView.Columns.ColumnByFieldName("WorkingID").Caption = "工序名";
                TemView.Columns.ColumnByFieldName("EmployeeID").Caption = "姓名";
                try
                {
                    TemView.Columns.ColumnByFieldName("MaterielName").Visible=false;
                    TemView.Columns.ColumnByFieldName("WorkingName").Visible = false; 
                }
                catch (Exception ex)
                {
                }
                //TemView.Columns.ColumnByFieldName("EmployeeName").Caption = "姓名";
                //TemView.Columns.ColumnByFieldName("MaterielName").Caption = "款号";
                //TemView.Columns.ColumnByFieldName("WorkingName").Caption = "工序名";
                TemView.Columns.ColumnByFieldName("Amount").Caption = "数量";
                TemView.Columns.ColumnByFieldName("Price").Caption = "单价";
                TemView.Columns.ColumnByFieldName("Money").Caption = "金额";
              //  TemView.Columns.ColumnByFieldName("EmployeeName").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                TemView.Columns.ColumnByFieldName("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.Columns.ColumnByFieldName("Money").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                TemView.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dtPM.Rows[0]["BegingDate"] = _ldBegingDate.val;
            dtPM.Rows[0]["EndDate"] = _ldEndDate.val;
            if (BasicClass.BasicFile.liST[0].AutoCaicBoardWages)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否需要计算本期伙食费？\r\n 将占用比较长时间。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (BasicClass.BasicFile.liST[0].OrderNeedEat)
                    {
                        BasicClass.GetDataSet.ExecSql("Hownet.BLL.OrderingList", "CaicMoney", new object[] { ((DateTime)(_ldEndDate.val)).Date.AddDays(1) });
                    }
                    else
                    {

                    }
                }
                
            }
            dsTem =BasicClass.GetDataSet.GetDS(blPM,"GetTemPayByPW",new object[]{(DateTime)(_ldBegingDate.val),(DateTime)(_ldEndDate.val),Convert.ToInt32(lookUpEdit1.EditValue)});
            gridControl2.DataSource = dsTem.Tables[0];
            if (Convert.ToInt32(lookUpEdit1.EditValue) == 0)
                simpleButton1.Visible = false;
            _ldBegingDate.t = _ldEndDate.t = true;
        }
        void Save(bool IsVerify)
        {
            StringBuilder strNoPrice = new StringBuilder();
            DataTable dtMat = ((DataView)(BaseForm.RepositoryItem._reProduce.DataSource)).Table;
            DataTable dtWork = ((DataView)(BaseForm.RepositoryItem._reWorking.DataSource)).Table;
            for (int i = 0; i < dsTem.Tables["Info"].Rows.Count; i++)
            {
                if (decimal.Parse(dsTem.Tables["Info"].DefaultView[i]["Price"].ToString()) == 0)
                {
                    try
                    {
                        strNoPrice.Append("款号：" + dtMat.Select("(ID=" + dsTem.Tables["Info"].DefaultView[i]["MaterielID"] + ")")[0]["Name"].ToString() + "  工序：" + dtWork.Select("(ID=" + dsTem.Tables["Info"].DefaultView[i]["WorkingID"] + ")")[0]["Name"].ToString());
                        strNoPrice.Append("\r\n");
                        //XtraMessageBox.Show("款号：" + mat + "  的工序： " + work + "  工价未定义，不能保存！");
                        //return;
                    }
                    catch
                    {
                    }
                }
            }
            if (strNoPrice.Length > 0)
            {
                if (!BasicClass.BasicFile.liST[0].IsTicketNotNeedCaic)
                {
                    XtraMessageBox.Show(strNoPrice.ToString() + "   工价未定义，不能保存！");
                    return;
                }
                else
                {
                    if (DialogResult.No == XtraMessageBox.Show(strNoPrice.ToString() + "   工价未定义，是否保存！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        return;
                    }
                }
            }
            dtPM.Rows[0]["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
            if (!IsVerify)
            {
                dtPM.Rows[0]["IsVerify"] = 1;
                dtPM.Rows[0]["VerifyDateTime"] = DateTime.Parse("1900-1-1");
                dtPM.Rows[0]["VerifyMan"] = 0;
            }
            else
            {
                dtPM.Rows[0]["IsVerify"] = 3;
                dtPM.Rows[0]["VerifyDateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dtPM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            }
            t = IsVerify;
            dtPM.Rows[0]["CaicType"] = val;
            dtMain.Rows[bs.Position]["ID"] = dtPM.Rows[0]["ID"] = _mainID = BasicClass.GetDataSet.Add(blPM, dtPM);
            BasicClass.GetDataSet.ExecSql(blPM, "PaySums", new object[] { _mainID });
            //dss.Tables.Clear();
            //dss.Clear();
            //dss.Tables.Add(dsTem.Tables[0].Copy());
           // bb = Hownet.ZipJpg.Ds2Byte(dss);
            DataTable ddddt = dsTem.Tables[0].Clone();
            int a = 0;
            for (int i = 0; i < dsTem.Tables[0].Rows.Count; i++)
            {
                a = Convert.ToInt32(dsTem.Tables[0].Rows[i]["A"]);
                if (a > 1)
                {
                    ddddt.Rows.Clear();
                    dsTem.Tables[0].Rows[i]["MainID"] = _mainID;
                    ddddt.Rows.Add(dsTem.Tables[0].Rows[i].ItemArray);
                    if (a == 2)
                    {
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllPay, ddddt);
                    }
                    else if (a == 3)
                    {
                        dsTem.Tables[0].Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllPay, ddddt);
                    }
                }
                if (IsVerify)
                {
                    if (dsTem.Tables[0].Rows[i]["Deposit"] != null && dsTem.Tables[0].Rows[i]["Deposit"].ToString() != string.Empty && Convert.ToDecimal(dsTem.Tables[0].Rows[i]["Deposit"]) != 0)
                    {
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMiniEmp, "UpDeposit", new object[] { Convert.ToInt32(dsTem.Tables[0].Rows[i]["EmployeeID"]), Convert.ToDecimal(dsTem.Tables[0].Rows[i]["Deposit"]), true });
                    }
                }
            }
           // BasicClass.GetDataSet.ExecSql(blPM, "PayMoney", new object[] { bb, _mainID });

            BasicClass.GetDataSet.ExecSql(blPM, "UpHandIsEnd", new object[] { DateTime.Parse(dtPM.Rows[0]["BegingDate"].ToString()), DateTime.Parse(dtPM.Rows[0]["EndDate"].ToString()), 1 });//暂时不用
          //  showData();
          //  bs.Position = dtMain.Rows.Count - 1;
          
        }

        private void CaiCPay(int va)
        {
            f = true;
            val = va;
            decimal fac = 0;
            decimal Remain = 0;
            decimal huoSi = 0;
            decimal yuZi = 0;
            decimal buTe = 0;
            decimal benYue = 0;
            decimal shangYue = 0;
            decimal FullAttendance = 0;
            decimal Fine = 0;
            decimal lass = 0;
            decimal roya =0;
            decimal lw = 0;//超额或提成部份
            decimal deposit = 0;
            decimal needDeposit = 0;
            decimal allDeposit = 0;
            decimal add1 = 0;
            decimal add2 = 0;
            decimal add3 = 0;
            decimal add4 = 0;
            decimal add5 = 0;
            decimal Jian1 = 0;
            decimal Jian2 = 0;
            decimal Jian3 = 0;
            decimal Jian4 = 0;
            decimal Jian5 = 0;
            bool _notFact = false;
          //  decimal ActualMonth = 0;
            int payID = 0;
            for (int m = 0; m < gridView1.RowCount; m++)
            {
                 fac = 0;
                 Remain = 0;
                 huoSi = 0;
                 yuZi = 0;
                 buTe = 0;
                 benYue = 0;
                 shangYue = 0;
                 FullAttendance = 0;
                 Fine = 0;
                 lass = decimal.Parse(gridView1.GetRowCellValue(m, _coLassMoney).ToString());
                 roya = decimal.Parse(gridView1.GetRowCellValue(m, _coRoyalty).ToString());
                 lw = 0;//超额或提成部份
                 payID = int.Parse(gridView1.GetRowCellValue(m, _coPayName).ToString());
                 deposit = 0;
                 needDeposit = 0;
                 allDeposit = 0;
                 add1 = 0;
                 add2 = 0;
                 add3 = 0;
                 add4 = 0;
                 add5 = 0;
                 Jian1 = 0;
                 Jian2 = 0;
                 Jian3 = 0;
                 Jian4 = 0;
                 Jian5 = 0;
               // ActualMonth = 0;
                if (gridView1.GetRowCellValue(m,"Month").ToString() != string.Empty)
                {
                    benYue = decimal.Parse(gridView1.GetRowCellValue(m, "Month").ToString());
                }
                if (gridView1.GetRowCellValue(m,"LastRemain").ToString() != string.Empty)
                {
                    shangYue = decimal.Parse(gridView1.GetRowCellValue(m, "LastRemain").ToString());
                }
                if (gridView1.GetRowCellValue(m,"Repair").ToString() != string.Empty)
                {
                    buTe = decimal.Parse(gridView1.GetRowCellValue(m, "Repair").ToString());
                }
                if (gridView1.GetRowCellValue(m,"BoardWages").ToString() != string.Empty)
                {
                    huoSi = decimal.Parse(gridView1.GetRowCellValue(m, "BoardWages").ToString());
                }
                if (gridView1.GetRowCellValue(m, "Payment").ToString() != string.Empty)
                {
                    yuZi = decimal.Parse(gridView1.GetRowCellValue(m, "Payment").ToString());
                }
                if (gridView1.GetRowCellValue(m, _coFullAttendance).ToString().Trim() != string.Empty)
                {
                    FullAttendance = decimal.Parse(gridView1.GetRowCellValue(m, _coFullAttendance).ToString());
                }
                if (gridView1.GetRowCellValue(m, _coFine).ToString().Trim() != string.Empty)
                {
                    Fine = decimal.Parse(gridView1.GetRowCellValue(m, _coFine).ToString());
                }
                if (gridView1.GetRowCellValue(m, _coAllDeposit).ToString().Trim() != string.Empty)
                {
                    allDeposit = Convert.ToDecimal(gridView1.GetRowCellValue(m, _coTemDeposit));
                }
                if (gridView1.GetRowCellDisplayText(m, _coDeposit).Trim() != string.Empty)
                {
                    deposit = Convert.ToDecimal(gridView1.GetRowCellValue(m, _coDeposit));
                }
                if (gridView1.GetRowCellDisplayText(m, "Add1").Trim() != string.Empty)
                {
                    add1 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add1"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Add2").Trim() != string.Empty)
                {
                    add2 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add2"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Add3").Trim() != string.Empty)
                {
                    add3 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add3"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Add4").Trim() != string.Empty)
                {
                    add4 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add4"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Add5").Trim() != string.Empty)
                {
                    add5 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add5"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Jian1").Trim() != string.Empty)
                {
                    Jian1 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian1"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Jian2").Trim() != string.Empty)
                {
                    Jian2 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian2"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Jian3").Trim() != string.Empty)
                {
                    Jian3 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian3"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Jian4").Trim() != string.Empty)
                {
                    Jian4 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian4"));
                }
                if (gridView1.GetRowCellDisplayText(m, "Jian5").Trim() != string.Empty)
                {
                    Jian5 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian5"));
                }

                //if (gridView1.GetRowCellValue(m, _coDeposit).ToString().Trim() != string.Empty)
                //{
                //    deposit = Convert.ToDecimal(gridView1.GetRowCellValue(m, _coDeposit));
                //}
                if (payID == 52)//保底+超额
                {
                    if (benYue > lass)
                    {
                        lw = decimal.Parse(((benYue - lass) * roya).ToString("n2"));
                    }
                    benYue = lass;
                    gridView1.SetRowCellValue(m, Month,lass+lw);
                }
                else if (payID == 63)//保底+提成
                {
                    lw = CaicCut(Convert.ToInt32(gridView1.GetRowCellValue(m, coEmployee)));
                    benYue = lass;
                    gridView1.SetRowCellValue(m, Month, lass + lw);
                }
                huoSi = huoSi * Convert.ToInt32(dtSet.Select("(ColumnsName='BoardWages')")[0]["Caic"]);
                yuZi = yuZi * Convert.ToInt32(dtSet.Select("(ColumnsName='Payment')")[0]["Caic"]);
                FullAttendance = FullAttendance * Convert.ToInt32(dtSet.Select("(ColumnsName='FullAttendance')")[0]["Caic"]);
                Fine = Fine * Convert.ToInt32(dtSet.Select("(ColumnsName='Fine')")[0]["Caic"]);
                deposit = deposit * Convert.ToInt32(dtSet.Select("(ColumnsName='Deposit')")[0]["Caic"]);
                buTe = buTe * Convert.ToInt32(dtSet.Select("(ColumnsName='Repair')")[0]["Caic"]);
                add1 = add1 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add1')")[0]["Caic"]);
                add2 = add2 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add2')")[0]["Caic"]);
                add3 = add3 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add3')")[0]["Caic"]);
                add4 = add4 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add4')")[0]["Caic"]);
                add5 = add5 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add5')")[0]["Caic"]);
                Jian1 = Jian1 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian1')")[0]["Caic"]);
                Jian2 = Jian2 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian2')")[0]["Caic"]);
                Jian3 = Jian3 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian3')")[0]["Caic"]);
                Jian4 = Jian4 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian4')")[0]["Caic"]);
                Jian5 = Jian5 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian5')")[0]["Caic"]);
                _notFact = Convert.ToBoolean(gridView1.GetRowCellValue(m, _coNotFact));
                Remain = benYue + shangYue + buTe + lw + huoSi + yuZi + FullAttendance + Fine + deposit + add1 + add2 + add3 + add4 + add5 + Jian1 + Jian2 + Jian3 + Jian4 + Jian5;

                if (val != 0)
                {
                    fac = (int)Remain / val;
                    fac = fac * val;
                }
                else
                {
                    fac = Remain;
                }
                if (_lassDeposit > 0 && _poorDeposit > 0)//有设置扣押金方式
                {
                    needDeposit = Convert.ToDecimal(gridView1.GetRowCellValue(m, _coNeedDeposit));
                    if ((needDeposit - allDeposit) > 0)//押金未扣足
                    {
                        if (fac > _lassDeposit)//本月可发工资大于扣押金设置中的最低工资
                        {
                            if ((fac - _poorDeposit) > (needDeposit - allDeposit))//当月可扣押金大于需扣押金
                            {
                                fac = fac - (needDeposit - allDeposit);//当月实发等于扣减扣押金后的数额
                                deposit = (needDeposit - allDeposit);//当月所扣押金为所需扣的押金
                            }
                            else
                            {
                                deposit = fac - _poorDeposit;
                                fac = _poorDeposit;
                            }
                        }
                        else//本月可发工资大于扣押金设置中的最低工资，需全部扣除
                        {
                            if (fac > (needDeposit - allDeposit))//当月可扣押金大于需扣押金
                            {
                                fac = fac - (needDeposit - allDeposit);//当月实发等于扣减扣押金后的数额
                                deposit = (needDeposit - allDeposit);//当月所扣押金为所需扣的押金
                            }
                            else
                            {
                                deposit = fac;
                                fac = 0;
                            }
                        }
                    }
                    allDeposit += deposit;
                }
                else if (IsCaicDeposit)
                {
                    deposit =needDeposit- allDeposit  ;
                    if (deposit < 0)
                        deposit = 0;
                }
                //deposit = deposit * Convert.ToInt32(dtSet.Select("(ColumnsName='Deposit')")[0]["Caic"]);
                if (fac < 0)
                {
                    Remain = benYue + shangYue + buTe + lw + huoSi + yuZi + FullAttendance + Fine + deposit + add1 + add2 + add3 + add4 + add5 + Jian1 + Jian2 + Jian3 + Jian4 + Jian5;
                    fac = 0;
                }
                if (_notFact)
                {
                    fac = 0;
                }
                //else
                //{
                    Remain = Remain - (decimal)fac;
                //}
                gridView1.SetRowCellValue(m, "Fact", fac);
                gridView1.SetRowCellValue(m, "Remain", Remain);
                if (IsCaicDeposit)
                {
                    deposit = deposit * Convert.ToInt32(dtSet.Select("(ColumnsName='Deposit')")[0]["Caic"]);
                      gridView1.SetRowCellValue(m, _coDeposit, deposit);
                }
               // gridView1.SetRowCellValue(m, _coAllDeposit, allDeposit);
            }
            f = false;
        }
        void UpFact(bool IsVerify)
        {
            dtPM.Rows[0]["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
            t = IsVerify;
            if (!IsVerify)
            {
                dtPM.Rows[0]["IsVerify"] = 1;
                dtPM.Rows[0]["VerifyDateTime"] = DateTime.Parse("1900-1-1");
                dtPM.Rows[0]["VerifyMan"] = 0;
            }
            else
            {
                dtPM.Rows[0]["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dtPM.Rows[0]["IsVerify"] = 3;
                dtPM.Rows[0]["VerifyDateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dtPM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            }
            dtPM.Rows[0]["CaicType"] = val;
            BasicClass.GetDataSet.UpData(blPM, dtPM);
            gridView1.CloseEditor();
            DataTable dttt = dsTem.Tables[0].Clone();
            for (int i = 0; i < dsTem.Tables[0].Rows.Count; i++)
            {
                dttt.Clear();
                dttt.Rows.Add(dsTem.Tables[0].Rows[i].ItemArray);
                BasicClass.GetDataSet.UpData(blP, dttt);
                if (IsVerify)
                {
                    if (dsTem.Tables[0].Rows[i]["Deposit"] != null && dsTem.Tables[0].Rows[i]["Deposit"].ToString() != string.Empty && Convert.ToDecimal(dsTem.Tables[0].Rows[i]["Deposit"]) != 0)
                    {
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMiniEmp, "UpDeposit", new object[] { Convert.ToInt32(dsTem.Tables[0].Rows[i]["EmployeeID"]), Convert.ToDecimal(dsTem.Tables[0].Rows[i]["Deposit"]), true });
                    }
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

                if (f)
                {
                    if (e.Column.FieldName != "Fact" && e.Column.FieldName != "Remain" && e.Column != Month&&e.Column!=_coDeposit)
                    {
                        int m = e.RowHandle;
                        decimal fac = 0;
                        decimal Remain = 0;
                        decimal huoSi = 0;
                        decimal yuZi = 0;
                        decimal buTe = 0;
                        decimal benYue = 0;
                        decimal shangYue = 0;
                        decimal FullAttendance = 0;
                        decimal Fine = 0;
                        decimal lass = decimal.Parse(gridView1.GetRowCellValue(m, _coLassMoney).ToString());
                        decimal roya = decimal.Parse(gridView1.GetRowCellValue(m, _coRoyalty).ToString());
                        decimal deposit = 0;
                        decimal lw = 0;//超额或提成部份
                        decimal add1 = 0;
                        decimal add2 = 0;
                        decimal add3 = 0;
                        decimal add4 = 0;
                        decimal add5 = 0;
                        decimal Jian1 = 0;
                        decimal Jian2 = 0;
                        decimal Jian3 = 0;
                        decimal Jian4 = 0;
                        decimal Jian5 = 0;
                        int payID = int.Parse(gridView1.GetRowCellValue(m, _coPayName).ToString());
                        if (gridView1.GetRowCellValue(m, "Month").ToString() != string.Empty)
                        {
                            benYue = decimal.Parse(gridView1.GetRowCellValue(m, "Month").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "LastRemain").ToString() != string.Empty)
                        {
                            shangYue = decimal.Parse(gridView1.GetRowCellValue(m, "LastRemain").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "Repair").ToString() != string.Empty)
                        {
                            buTe = decimal.Parse(gridView1.GetRowCellValue(m, "Repair").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "BoardWages").ToString() != string.Empty)
                        {
                            huoSi = decimal.Parse(gridView1.GetRowCellValue(m, "BoardWages").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "Payment").ToString() != string.Empty)
                        {
                            yuZi = decimal.Parse(gridView1.GetRowCellValue(m, "Payment").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, _coFullAttendance).ToString().Trim() != string.Empty)
                        {
                            FullAttendance = decimal.Parse(gridView1.GetRowCellValue(m, _coFullAttendance).ToString());
                        }
                        if (gridView1.GetRowCellValue(m, _coFine).ToString().Trim() != string.Empty)
                        {
                            Fine = decimal.Parse(gridView1.GetRowCellValue(m, _coFine).ToString());
                        }
                        if (gridView1.GetRowCellValue(m, _coDeposit).ToString().Trim() != string.Empty)
                        {
                            deposit = decimal.Parse(gridView1.GetRowCellValue(m, _coDeposit).ToString());
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add1").Trim() != string.Empty)
                        {
                            add1 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add1"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add2").Trim() != string.Empty)
                        {
                            add2 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add2"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add3").Trim() != string.Empty)
                        {
                            add3 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add3"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add4").Trim() != string.Empty)
                        {
                            add4 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add4"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add5").Trim() != string.Empty)
                        {
                            add5 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add5"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian1").Trim() != string.Empty)
                        {
                            Jian1 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian1"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian2").Trim() != string.Empty)
                        {
                            Jian2 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian2"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian3").Trim() != string.Empty)
                        {
                            Jian3 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian3"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian4").Trim() != string.Empty)
                        {
                            Jian4 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian4"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian5").Trim() != string.Empty)
                        {
                            Jian5 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian5"));
                        }
                        if (payID == 52)//保底+超额
                        {
                            if (benYue > lass)
                            {
                                lw = decimal.Parse(((benYue - lass) * roya).ToString("n2"));
                            }
                            benYue = lass;
                        }
                        else if (payID == 63)
                        {
                            // lw = decimal.Parse((benYue * roya).ToString("n2"));
                            lw = CaicCut(Convert.ToInt32(gridView1.GetFocusedRowCellValue(coEmployee)));
                            benYue = lass;
                        }

                        huoSi = huoSi * Convert.ToInt32(dtSet.Select("(ColumnsName='BoardWages')")[0]["Caic"]);
                        yuZi = yuZi * Convert.ToInt32(dtSet.Select("(ColumnsName='Payment')")[0]["Caic"]);
                        FullAttendance = FullAttendance * Convert.ToInt32(dtSet.Select("(ColumnsName='FullAttendance')")[0]["Caic"]);
                        Fine = Fine * Convert.ToInt32(dtSet.Select("(ColumnsName='Fine')")[0]["Caic"]);
                        deposit = deposit * Convert.ToInt32(dtSet.Select("(ColumnsName='Deposit')")[0]["Caic"]);
                        buTe = buTe * Convert.ToInt32(dtSet.Select("(ColumnsName='Repair')")[0]["Caic"]);
                        add1 = add1 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add1')")[0]["Caic"]);
                        add2 = add2 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add2')")[0]["Caic"]);
                        add3 = add3 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add3')")[0]["Caic"]);
                        add4 = add4 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add4')")[0]["Caic"]);
                        add5 = add5 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add5')")[0]["Caic"]);
                        Jian1 = Jian1 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian1')")[0]["Caic"]);
                        Jian2 = Jian2 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian2')")[0]["Caic"]);
                        Jian3 = Jian3 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian3')")[0]["Caic"]);
                        Jian4 = Jian4 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian4')")[0]["Caic"]);
                        Jian5 = Jian5 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian5')")[0]["Caic"]);
                        bool _notFact = Convert.ToBoolean(gridView1.GetFocusedRowCellValue(_coNotFact));

                        Remain = benYue + shangYue + buTe + lw + huoSi + yuZi + FullAttendance + Fine + deposit + add1 + add2 + add3 + add4 + add5 + Jian1 + Jian2 + Jian3 + Jian4 + Jian5;
                        if (val != 0)
                        {
                            fac = (int)Remain / val;
                            fac = fac * val;
                        }
                        else
                        {
                            fac = Remain;
                        }
                        if (_notFact)
                        {
                            fac = 0;
                        }
                        Remain = Remain - (decimal)fac;
                        if (fac < 0)
                        {
                            Remain += fac;
                            fac = 0;
                        }
                        gridView1.SetRowCellValue(m, "Fact", fac);
                        gridView1.SetRowCellValue(m, "Remain", Remain);
                    }
                }
                else
                {
                    if (e.Column.FieldName != "Fact" && e.Column.FieldName != "Remain")
                    {
                        int m = e.RowHandle;
                        decimal fac = 0;
                        decimal Remain = 0;
                        decimal huoSi = 0;
                        decimal yuZi = 0;
                        decimal buTe = 0;
                        decimal benYue = 0;
                        decimal shangYue = 0;
                        decimal FullAttendance = 0;
                        decimal Fine = 0;
                        decimal lass = decimal.Parse(gridView1.GetRowCellValue(m, _coLassMoney).ToString());
                        decimal roya = decimal.Parse(gridView1.GetRowCellValue(m, _coRoyalty).ToString());
                        decimal deposit = 0;
                        decimal lw = 0;//超额或提成部份
                        decimal add1 = 0;
                        decimal add2 = 0;
                        decimal add3 = 0;
                        decimal add4 = 0;
                        decimal add5 = 0;
                        decimal Jian1 = 0;
                        decimal Jian2 = 0;
                        decimal Jian3 = 0;
                        decimal Jian4 = 0;
                        decimal Jian5 = 0;
                        int payID = int.Parse(gridView1.GetRowCellValue(m, _coPayName).ToString());
                        if (gridView1.GetRowCellValue(m, "Month").ToString() != string.Empty)
                        {
                            benYue = decimal.Parse(gridView1.GetRowCellValue(m, "Month").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "LastRemain").ToString() != string.Empty)
                        {
                            shangYue = decimal.Parse(gridView1.GetRowCellValue(m, "LastRemain").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "Repair").ToString() != string.Empty)
                        {
                            buTe = decimal.Parse(gridView1.GetRowCellValue(m, "Repair").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "BoardWages").ToString() != string.Empty)
                        {
                            huoSi = decimal.Parse(gridView1.GetRowCellValue(m, "BoardWages").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, "Payment").ToString() != string.Empty)
                        {
                            yuZi = decimal.Parse(gridView1.GetRowCellValue(m, "Payment").ToString());
                        }
                        if (gridView1.GetRowCellValue(m, Fact).ToString() != string.Empty)
                        {
                            fac = decimal.Parse(gridView1.GetRowCellValue(m, Fact).ToString());
                        }
                        if (gridView1.GetRowCellValue(m, _coFullAttendance).ToString().Trim() != string.Empty)
                        {
                            FullAttendance = decimal.Parse(gridView1.GetRowCellValue(m, _coFullAttendance).ToString());
                        }
                        if (gridView1.GetRowCellValue(m, _coFine).ToString().Trim() != string.Empty)
                        {
                            Fine = decimal.Parse(gridView1.GetRowCellValue(m, _coFine).ToString());
                        }
                        if (gridView1.GetRowCellValue(m, _coDeposit).ToString().Trim() != string.Empty)
                        {
                            deposit = decimal.Parse(gridView1.GetRowCellValue(m, _coDeposit).ToString());
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add1").Trim() != string.Empty)
                        {
                            add1 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add1"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add2").Trim() != string.Empty)
                        {
                            add2 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add2"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add3").Trim() != string.Empty)
                        {
                            add3 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add3"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add4").Trim() != string.Empty)
                        {
                            add4 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add4"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Add5").Trim() != string.Empty)
                        {
                            add5 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Add5"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian1").Trim() != string.Empty)
                        {
                            Jian1 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian1"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian2").Trim() != string.Empty)
                        {
                            Jian2 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian2"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian3").Trim() != string.Empty)
                        {
                            Jian3 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian3"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian4").Trim() != string.Empty)
                        {
                            Jian4 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian4"));
                        }
                        if (gridView1.GetRowCellDisplayText(m, "Jian5").Trim() != string.Empty)
                        {
                            Jian5 = Convert.ToDecimal(gridView1.GetRowCellValue(m, "Jian5"));
                        }
                        if (payID == 52)//保底+超额
                        {
                            if (benYue > lass)
                            {
                                lw = decimal.Parse(((benYue - lass) * roya).ToString("n2"));
                            }
                            benYue = lass;
                        }
                        else if (payID == 63)
                        {
                            // lw = decimal.Parse((benYue * roya).ToString("n2"));
                            lw = CaicCut(Convert.ToInt32(gridView1.GetFocusedRowCellValue(coEmployee)));
                            benYue = lass;
                        }

                        //   Remain = benYue + shangYue + buTe + lw - huoSi - yuZi - fac + FullAttendance - Fine-deposit;

                        huoSi = huoSi * Convert.ToInt32(dtSet.Select("(ColumnsName='BoardWages')")[0]["Caic"]);
                        yuZi = yuZi * Convert.ToInt32(dtSet.Select("(ColumnsName='Payment')")[0]["Caic"]);
                        FullAttendance = FullAttendance * Convert.ToInt32(dtSet.Select("(ColumnsName='FullAttendance')")[0]["Caic"]);
                        Fine = Fine * Convert.ToInt32(dtSet.Select("(ColumnsName='Fine')")[0]["Caic"]);
                        deposit = deposit * Convert.ToInt32(dtSet.Select("(ColumnsName='Deposit')")[0]["Caic"]);
                        buTe = buTe * Convert.ToInt32(dtSet.Select("(ColumnsName='Repair')")[0]["Caic"]);
                        add1 = add1 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add1')")[0]["Caic"]);
                        add2 = add2 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add2')")[0]["Caic"]);
                        add3 = add3 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add3')")[0]["Caic"]);
                        add4 = add4 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add4')")[0]["Caic"]);
                        add5 = add5 * Convert.ToInt32(dtSet.Select("(ColumnsName='Add5')")[0]["Caic"]);
                        Jian1 = Jian1 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian1')")[0]["Caic"]);
                        Jian2 = Jian2 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian2')")[0]["Caic"]);
                        Jian3 = Jian3 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian3')")[0]["Caic"]);
                        Jian4 = Jian4 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian4')")[0]["Caic"]);
                        Jian5 = Jian5 * Convert.ToInt32(dtSet.Select("(ColumnsName='Jian5')")[0]["Caic"]);
                        bool _notFact = Convert.ToBoolean(gridView1.GetFocusedRowCellValue(_coNotFact));
                        Remain = benYue + shangYue + buTe + lw + huoSi + yuZi + FullAttendance + Fine + deposit + add1 + add2 + add3 + add4 + add5 + Jian1 + Jian2 + Jian3 + Jian4 + Jian5;
                        if (val != 0)
                        {
                            fac = (int)Remain / val;
                            fac = fac * val;
                        }
                        else
                        {
                            fac = Remain;
                        }
                        if (_notFact)
                        {
                            fac = 0;
                        }
                            Remain = Remain - (decimal)fac;
                        if (fac < 0)
                        {
                            Remain += fac;
                            fac = 0;
                        }
                        gridView1.SetRowCellValue(m, "Fact", fac);
                        gridView1.SetRowCellValue(m, "Remain", Remain);
                    }
                }
            if(e.Column==Fact)
            {
                if (IsEditFac)
                {
                    decimal re = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(Remain));
                    gridView1.SetFocusedRowCellValue(Remain, Fac + re - Convert.ToDecimal(e.Value));
                }
            }
        }
         private decimal CaicDeposit(int EmployeeID)
         {
             decimal depo = 0;

             return depo;
         }
         private decimal CaicCut(int EmployeeID)
         {
             return Convert.ToDecimal(BasicClass.GetDataSet.GetOne(blPM, "GetCut", new object[] { EmployeeID,(DateTime)(_ldBegingDate.val), (DateTime)(_ldEndDate.val) }));
         }
        private void _brEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
        }

        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = false;
            Fact.OptionsColumn.AllowEdit = LastRemain.OptionsColumn.AllowEdit = false;
        }

        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_mainID == 0)
            {
                Save(false);
                showValue(bs.Position);
            }
            else
            {
                UpFact(false);
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barClick(barButtonItem14);
            CaiCPay(100);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barClick(barButtonItem15);
            CaiCPay(50);
           
        }
        private void barClick(DevExpress.XtraBars.BarBaseButtonItem item)
        {
            barButtonItem14.Down = false;
            barButtonItem15.Down = false;
            barButtonItem16.Down = false;
            barButtonItem17.Down = false;
            barButtonItem18.Down = false;
            item.Down = true;
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barClick(barButtonItem16);
            CaiCPay(10);
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barClick(barButtonItem17);
            CaiCPay(1);
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barClick(barButtonItem18);
            CaiCPay(0);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dsTem.Tables[0].TableName = "PayLine";
            try
            {
                dsTem.Tables[0].Columns.Add("WorkTypeName", typeof(string));
                dsTem.Tables[0].Columns.Add("Name", typeof(string));             
                dsTem.Tables[0].Columns.Add("DepartmentName", typeof(string));
                dsTem.Tables[0].Columns.Add("IntSn", typeof(int));

            }
            catch (Exception ex)
            {
            }
               // DataTable dtEM = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetAllList", null).Tables[0];
                DataRow[] drs;
                for (int i = 0; i < dsTem.Tables[0].Rows.Count; i++)
                {
                    drs = dtEmp.Select("(ID=" + dsTem.Tables[0].Rows[i]["EmployeeID"] + ")");
                    dsTem.Tables[0].Rows[i]["Name"] = drs[0]["Name"];
                    dsTem.Tables[0].Rows[i]["DepartmentName"] = drs[0]["DepartmentName"];
                    if (drs[0]["WorkTypeName"].ToString().Length < 11)
                        dsTem.Tables[0].Rows[i]["WorkTypeName"] = drs[0]["WorkTypeName"];
                    else
                        dsTem.Tables[0].Rows[i]["WorkTypeName"] = drs[0]["WorkTypeName"].ToString().Substring(0, 10);
                    try
                    {
                        dsTem.Tables[0].Rows[i]["IntSn"] = Convert.ToInt32(dsTem.Tables[0].Rows[i]["Sn"]);
                    }
                    catch
                    {
                        dsTem.Tables[0].Rows[i]["IntSn"] = -1;
                    }
                }
           
            BaseForm.PrintClass.PrintPayLine(dsTem);
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print();           
            BaseForm.PrintClass.PrintEmpPay(dsTem,false);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _brVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_mainID == 0)
            {
                Save(true);
            }
            else
            {
                UpFact(true);
            }
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.OrderingList", "UpIsSum", new object[] { 1, Convert.ToDateTime(_ldBegingDate.val).AddMinutes(-1), Convert.ToDateTime(_ldEndDate.val).AddDays(1) });
            showValue(bs.Position);
            _brSave.Enabled = _brEdit.Enabled = _brDel.Enabled = _brSum.Enabled = false;
            _brVerify.Enabled =false;
            if (t && bs.Position == dtMain.Rows.Count - 1)
                _brAddNew.Enabled = _brUnVerify.Enabled = t;
            else
                _brAddNew.Enabled = _brUnVerify.Enabled = false;
            if (!t && bs.Position == dtMain.Rows.Count - 1)
                _brDel.Enabled = true;
            else
                _brDel.Enabled = false;
            _ldBegingDate.t = _ldEndDate.t = false;
            gridView1.OptionsBehavior.Editable =false;
        }

        private void _brUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtPM.Rows[0]["IsVerify"] = 1;
            dtPM.Rows[0]["VerifyDateTime"] = DateTime.Parse("1900-1-1");
            dtPM.Rows[0]["VerifyMan"] = 0;
            BasicClass.GetDataSet.UpData(blPM,  dtPM);
            for (int i = 0; i < dsTem.Tables[0].Rows.Count; i++)
            {
                if (dsTem.Tables[0].Rows[i]["Deposit"] != null && dsTem.Tables[0].Rows[i]["Deposit"].ToString() != string.Empty && Convert.ToDecimal(dsTem.Tables[0].Rows[i]["Deposit"]) != 0)
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMiniEmp, "UpDeposit", new object[] { Convert.ToInt32(dsTem.Tables[0].Rows[i]["EmployeeID"]), Convert.ToDecimal(dsTem.Tables[0].Rows[i]["Deposit"]), false });
                }
            }
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.OrderingList", "UpIsSum", new object[] { 0, Convert.ToDateTime(_ldBegingDate.val).AddMinutes(-1), Convert.ToDateTime(_ldEndDate.val).AddDays(1) });
            t = false;
            _brSave.Enabled = !t;
            _brEdit.Enabled = !t;
            _brDel.Enabled = !t;
            _brSum.Enabled = !t;
            _brVerify.Enabled = (!t && _mainID > 0);
                _brAddNew.Enabled = _brUnVerify.Enabled = false;
            if (!t && bs.Position == dtMain.Rows.Count - 1)
                _brDel.Enabled = true;
            else
                _brDel.Enabled = false;
            _ldBegingDate.t = _ldEndDate.t = false;
            if (_mainID > 0)
            {
                _ldBegingDate.t = _ldEndDate.t = true;
            }
            else
            {
                if (dtMain.Rows.Count > 1)
                {
                    _ldBegingDate.t = true;
                }
            }
            gridView1.OptionsBehavior.Editable = false;
        }

        private void _brDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_mainID == 0)
            {
                dtMain.Rows.Remove(dtMain.Rows[dtMain.Rows.Count - 1]);
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("是否确认删除此次工资汇总？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    if (DialogResult.Yes == MessageBox.Show("请再次确认是否删除此次工资汇总？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                    {
                        BasicClass.GetDataSet.ExecSql(blPM, "UpHandIsEnd", new object[] { DateTime.Parse(dtPM.Rows[0]["BegingDate"].ToString()), DateTime.Parse(dtPM.Rows[0]["EndDate"].ToString()), 0 });
                        BasicClass.GetDataSet.ExecSql(blP, "DelPay", new object[] { _mainID });
                        BasicClass.GetDataSet.ExecSql(blPS, "DelPay", new object[] { _mainID });
                        BasicClass.GetDataSet.ExecSql(blPM, "Delete", new object[] { _mainID });
                    }
                }
            }
            showData();
            bs.Position = dtMain.Rows.Count - 1;
            showValue(bs.Position);
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (_mainID==0)
                _brSave.Enabled = Convert.ToInt32(lookUpEdit1.EditValue) == 0;
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form fr = new frWorkTypeRepair(_mainID);
            fr.ShowDialog();
        }
        //修改实发工资
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsEditFac = true;
            Fact.OptionsColumn.AllowEdit = true;
            gridView1.OptionsBehavior.Editable = true;
            Fac = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(Fact));
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!t&& e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn != Fact)
            {
                IsEditFac = false;
                Fact.OptionsColumn.AllowEdit = false;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName =BaseContranl  .BaseFormClass. ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1. ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LastRemain.OptionsColumn.AllowEdit = true;
            gridView1.OptionsBehavior.Editable = true;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Print();
            BaseForm.PrintClass.PrintEmpPay(dsTem,true);
        }
        private void Print()
        {
            dt.Clear();
            dsTem.Tables[0].TableName = "PayLine";
            DataTable dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];
            DataTable dtWork = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorking, "GetAllList", null).Tables[0];
            decimal Deposit = 0;
            //  DataRow[] drs;
            DataRow[] ddrs;
            try
            {
                //  dsTem.Tables[0].Columns.Add("AllDeposit", typeof(decimal));
                dsTem.Tables[0].Columns.Add("Name", typeof(string));
                dsTem.Tables[0].Columns.Add("DepartmentName", typeof(string));
                dsTem.Tables[0].Columns.Add("IntSn", typeof(int));
                DataTable dtEM = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetAllList", null).Tables[0];
                DataRow[] drss;
                for (int i = 0; i < dsTem.Tables[0].Rows.Count; i++)
                {
                    drss = dtEM.Select("(ID=" + dsTem.Tables[0].Rows[i]["EmployeeID"] + ")");
                    dsTem.Tables[0].Rows[i]["Name"] = drss[0]["Name"];
                    dsTem.Tables[0].Rows[i]["DepartmentName"] = drss[0]["DepartmentName"];
                    try
                    {
                        dsTem.Tables[0].Rows[i]["IntSn"] = Convert.ToInt32(dsTem.Tables[0].Rows[i]["Sn"]);
                    }
                    catch
                    {
                        dsTem.Tables[0].Rows[i]["IntSn"] = -1;
                    }
                }
            }
            catch
            {
            }

            for (int i = 0; i < dsTem.Tables["PayLine"].Rows.Count; i++)
            {
                Deposit = 0;
                if (t)
                {
                    Deposit = Convert.ToDecimal(dsTem.Tables["PayLine"].Rows[i]["AllDeposit"]); // Convert.ToDecimal(BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(ID=" + dsTem.Tables["PayLine"].Rows[i]["EmployeeID"] + ")" }).Tables[0].Rows[0]["Deposit"]);
                }
                else
                {
                    Deposit = Convert.ToDecimal(dsTem.Tables["PayLine"].Rows[i]["TemDeposit"]);
                    if (dsTem.Tables["PayLine"].Rows[i]["Deposit"] != null && dsTem.Tables["PayLine"].Rows[i]["Deposit"].ToString().Trim() != string.Empty)
                    {
                        Deposit += Convert.ToDecimal(dsTem.Tables["PayLine"].Rows[i]["Deposit"]);
                    }
                }
                dsTem.Tables["PayLine"].Rows[i]["AllDeposit"] = Deposit;
            }
            try
            {
                dsTem.Tables["Info"].Columns.Add("MaterielName", typeof(string));
                dsTem.Tables["Info"].Columns.Add("WorkingName", typeof(string));
                for (int i = 0; i < dsTem.Tables["Info"].Rows.Count; i++)
                {
                    ddrs = dtMat.Select("(ID=" + dsTem.Tables["Info"].Rows[i]["MaterielID"] + ")");
                    if (ddrs.Length > 0)
                        dsTem.Tables["Info"].Rows[i]["MaterielName"] = ddrs[0]["Name"];
                    else
                        dsTem.Tables["Info"].Rows[i]["MaterielName"] = "";
                    ddrs = dtWork.Select("(ID=" + dsTem.Tables["Info"].Rows[i]["WorkingID"] + ")");
                    if (ddrs.Length > 0)
                        dsTem.Tables["Info"].Rows[i]["WorkingName"] = ddrs[0]["Name"];
                    else
                        dsTem.Tables["Info"].Rows[i]["WorkingName"] = "";
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                dsTem.Tables["NoDefault"].Columns.Add("MaterielName", typeof(string));
                dsTem.Tables["NoDefault"].Columns.Add("WorkingName", typeof(string));
                for (int i = 0; i < dsTem.Tables["NoDefault"].Rows.Count; i++)
                {
                    ddrs = dtMat.Select("(ID=" + dsTem.Tables["NoDefault"].Rows[i]["MaterielID"] + ")");
                    if (ddrs.Length > 0)
                        dsTem.Tables["NoDefault"].Rows[i]["MaterielName"] = ddrs[0]["Name"];
                    else
                        dsTem.Tables["NoDefault"].Rows[i]["MaterielName"] = "";
                    ddrs = dtWork.Select("(ID=" + dsTem.Tables["NoDefault"].Rows[i]["WorkingID"] + ")");
                    if (ddrs.Length > 0)
                        dsTem.Tables["NoDefault"].Rows[i]["WorkingName"] = ddrs[0]["Name"];
                    else
                        dsTem.Tables["NoDefault"].Rows[i]["WorkingName"] = "";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coNotFact)
            {
                gridView1.SetFocusedValue(e.Value);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}