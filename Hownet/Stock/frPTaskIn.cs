using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Threading;
using System.Drawing.Printing;
using BasicClass;

namespace Hownet.Stock
{
    public partial class frPTaskIn : DevExpress.XtraEditors.XtraForm
    {
        public frPTaskIn()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        bool _IsShowBar = false;
        DataTable dtMain = new DataTable();
        public frPTaskIn(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        public frPTaskIn(int MainID, bool IsShowBar)
            : this()
        {
            _MainID = MainID;
            _IsShowBar = IsShowBar;
        }
        int _companyID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        private string bllSB = "Hownet.BLL.StockBack";
        private string blSBI = "Hownet.BLL.StockBackInfo";
        private string blCL = "Hownet.BLL.CompanyLog";
        private string blQP = "Hownet.BLL.QuotePrice";
        DataTable dtSB = new DataTable();
        DataTable dtSBI = new DataTable();
        DataTable dtBack = new DataTable();
        DataTable dtMateriel = new DataTable();
        bool t = false;
        decimal price = 0;
        decimal money = 0;
        decimal amount = 0;
        decimal last = 0;
        decimal back = 0;
        decimal _depotAmount = 0;
        int _depotID = 0;
        string backDate = string.Empty;
        int _rowCount = 0;
        bool _IsChangCom = false;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            if (_MainID == 0)
            {
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                InData();
                bs.Position = dtMain.Rows.Count - 1;
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                bar1.Visible = _IsShowBar;
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barVerify.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Posting).ToString()) == -1)
                barButtonItem1.Enabled = false;

        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
            _leCompany.FormName = (int)BasicClass.Enums.TableType.Supplier;
            DataTable dtD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "仓库" }).Tables[0];
            _leDepot.Properties.DataSource = dtD;
            _leDepot.EditValue = 0;
            //if (BasicClass.BasicFile.liST[0].Sell4Depot)
            //    _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _leCompany.IsNotCanEdit = false;
            dtMateriel = ((DataView)(BaseForm.RepositoryItem._reAllMateriel.DataSource)).Table;
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)Enums.TableType._外协加工收货 }).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;

        }
        /// <summary>
        /// 显示详细记录
        /// </summary>
        /// <param name="p"></param>
        void ShowView(int p)
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
            this._leCompany.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            this.gridView1.RowCountChanged -= new System.EventHandler(this.gridView1_RowCountChanged);
            this.gridView1.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            dtSBI.Rows.Clear();
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                last = decimal.Parse(dtSB.Rows[0]["LastMoney"].ToString());
                back = decimal.Parse(dtSB.Rows[0]["BackMoney"].ToString());
                money = decimal.Parse(dtSB.Rows[0]["Money"].ToString());
                _leCompany.IsNotCanEdit = true;
                simpleButton2.Visible = false;
            }
            else
            {
                dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtSB.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["DepotID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                dr["FillDate"] = dr["DataTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.StockBack, 0 });
                dr["LastMoney"] = last = 0;
                dr["BackMoney"] = back = 0;
                dr["BackDate"] = DateTime.Parse("1900-1-1");
                money = 0;
                dtSB.Rows.Add(dr);
                _brAddNew.Enabled = false;
                _leCompany.IsNotCanEdit = false;
                simpleButton2.Visible = true;
            }
            _upData = int.Parse(dtSB.Rows[0]["UpData"].ToString());
            t = (int.Parse((dtSB.Rows[0]["IsVerify"]).ToString()) > 2);
            _ldDate.val = dtSB.Rows[0]["DataTime"];
            _leCompany.editVal = _companyID = int.Parse(dtSB.Rows[0]["CompanyID"].ToString());
            _leDepot.EditValue = _depotID = int.Parse(dtSB.Rows[0]["DepotID"].ToString());
            money = decimal.Parse(dtSB.Rows[0]["Money"].ToString());
            _ltRemark.val = dtSB.Rows[0]["Remark"].ToString();
            _ltNum.val = DateTime.Parse(dtSB.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtSB.Rows[0]["Num"].ToString().PadLeft(3, '0');
            dtSBI = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            dtSBI.DefaultView.RowFilter = "A<4";
            gridControl1.DataSource = dtSBI.DefaultView;
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = _coDepotAmount.Visible = !t;
            _barUnVierfy.Enabled = t;//&& p == dtMain.Rows.Count - 1);
            _brAddNew.Enabled = (_MainID > 0);
            //_coDepotAmount.VisibleIndex = 3;
            gridView1.OptionsBehavior.Editable = !t;
            if ((DateTime)(dtSB.Rows[0]["BackDate"]) == DateTime.Parse("1900-1-1"))
                backDate = "      年    月    日";
            else
                backDate = ((DateTime)(dtSB.Rows[0]["BackDate"])).ToString("yyyy年MM月dd日");
            SetColumnsReadOnly();
            if (t)
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            else
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            ShowLoan();
            this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            _coDepotAmount.OptionsColumn.AllowEdit = false;
            _rowCount = gridView1.RowCount;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
            if (!t)
            {
                last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                dtBack.Clear();
                dtBack = BasicClass.GetDataSet.GetDS(blCL, "GetBackMoney", new object[] { _companyID }).Tables[0];
                back = 0;
                backDate = "      年    月    日";
                if (dtBack.Rows.Count > 0)
                {
                    if (dtBack.Rows[0]["Money"] != null && dtBack.Rows[0]["Money"].ToString().Trim() != string.Empty)
                    {
                        back = decimal.Parse(dtBack.Rows[0]["Money"].ToString());
                        backDate = ((DateTime)(dtBack.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                    }
                }
                ShowLoan();
            }
            memoEdit1.Text = dtSB.Rows[0]["StockRemark"].ToString();
            if (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) > 10)
            {
                _barUnVierfy.Enabled = false;
            }
            // panel2.Visible = !t;
        }
        private void ShowTask()
        {
            memoEdit1.Text = string.Empty;
            DataTable dtTem = new DataTable();
            DataTable dtTask = new DataTable();
            dtTem.Columns.Add("ID", typeof(int));
            string st = string.Empty;
            string[] sts;
            for (int i = 0; i < dtSBI.DefaultView.Count; i++)
            {
                st = dtSBI.DefaultView[i]["StringTaskID"].ToString();
                if (st.Length > 0)
                {
                    sts = st.Split(',');
                    if (sts.Length > 0)
                    {
                        for (int m = 0; m < sts.Length; m++)
                        {
                            if (dtTem.Select("(ID=" + Convert.ToInt32(sts[m]) + ")").Length == 0)
                            {
                                dtTem.Rows.Add(Convert.ToInt32(sts[m]));
                            }
                        }
                    }
                }
            }
            if (dtTem.Rows.Count > 0)
            {
                for (int i = 0; i < dtTem.Rows.Count; i++)
                {
                    memoEdit1.Text += BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductionPlan", "GetNum", new object[] { Convert.ToInt32(dtTem.Rows[i]["ID"]) }).ToString() + "；";
                }
            }
        }
        private void SetColumnsReadOnly()
        {
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Visible && gridView1.Columns[i] != _coAmount)
                {
                    gridView1.Columns[i].OptionsColumn.AllowEdit = !t;
                }
            }
        }
        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }
        /// <summary>
        /// 下一条
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }
        /// <summary>
        /// 尾记录
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }
        /// <summary>
        /// 新单
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool f = _MainID == 0;
            if (Save())
            {
                //if (f)
                //{
                int d = bs.Position;
                InData();
                bs.Position = d;
                ShowView(d);
                //}
            }
        }
        private bool Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSBI.AcceptChanges();
            if (_companyID == 0)
            {
                if (DialogResult.No == XtraMessageBox.Show("未选择供应商，是否为现金采购？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    return false;
            }
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择收货仓库！");
                return false;
            }
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
                {
                    if (DialogResult.No == XtraMessageBox.Show("有记录没有产生金额，保存时将被删除，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        return false;
                }
            }
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
            dtSB.Rows[0]["Remark"] = _ltRemark.val;
            dtSB.Rows[0]["Money"] = money;
            dtSB.Rows[0]["CompanyID"] = _companyID;
            dtSB.Rows[0]["DataTime"] = _ldDate.val;
            dtSB.Rows[0]["State"] = (int)Enums.TableType._外协加工收货;
            dtSB.Rows[0]["A"] = 1;
            dtSB.Rows[0]["DepotID"] = _depotID;
            dtSB.Rows[0]["StockRemark"] = memoEdit1.Text;
            if (_MainID == 0)
            {
                dtSB.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType._外协加工收货, _companyID });
                dtMain.Rows[bs.Position]["ID"] = dtSB.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSB, dtSB);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtSB.Rows[0]["UpData"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllSB, dtSB);
                }
            }
            DataTable dtt = dtSBI.Clone();
            int _id = 0;
            this.gridView1.RowCountChanged -= new System.EventHandler(this.gridView1_RowCountChanged);

            //for (int i = 0; i < dtSBI.Rows.Count; i++)
            //{
            //    int a = int.Parse(dtSBI.Rows[i]["A"].ToString());
            //    if (a > 1)
            //    {
            //        dtSBI.Rows[i]["MainID"] = _MainID;
            //        dtt.Clear();
            //        dtt.Rows.Add(dtSBI.Rows[i].ItemArray);

            //        if (a == 3)
            //        {
            //            dtt.Rows[0]["DepotMeasureID"] = dtt.Rows[0]["CompanyMeasureID"];
            //            dtt.Rows[0]["Conversion"] = 1;

            //            dtSBI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blSBI, dtt);
            //        }
            //        else if (a == 4)
            //        {
            //            _id = Convert.ToInt32(dtSBI.Rows[i]["ID"]);
            //            if (_id > 0)
            //            {
            //                BasicClass.GetDataSet.ExecSql(blSBI, "Delete", new object[] { _id });
            //            }
            //        }
            //        else if (a == 2)
            //        {
            //            BasicClass.GetDataSet.UpData(blSBI, dtt);
            //        }
            //        dtSBI.Rows[i]["A"] = 1;
            //    }
            //}

            for (int i = 0; i < dtSBI.Rows.Count; i++)
            {
                dtSBI.Rows[i]["MainID"] = _MainID;
            }
            BasicClass.GetDataSet.UpOrAdd(blSBI, dtSBI);
            ////  ShowLoan();
            _leCompany.IsNotCanEdit = true;
            return true;
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if (e.Column == _coMaterielID)
            {
                object obj = BaseForm.RepositoryItem._reAllMateriel.GetDataSourceValue("MeasureID", BaseForm.RepositoryItem._reAllMateriel.GetDataSourceRowIndex("ID", e.Value));
                gridView1.SetFocusedRowCellValue(_coMeasureID, obj);
                if (Convert.ToInt32(e.Value) > 0)
                {
                    gridView1.SetFocusedRowCellValue(_coMaterielRemark, BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + e.Value + ")" }).Tables[0].Rows[0]["Remark"]);
                }
            }
            if (e.Column == _coPrice)
            {
                gridView1.SetFocusedRowCellValue(_coNowPrice, e.Value);
            }
            try
            {
                if (e.Column == _coColorID || e.Column == _coMaterielID || e.Column == _coSizeID || e.Column == _coMeasureID)
                {
                    SetPrice(e.RowHandle);
                }
            }
            catch { }
            //try
            //{
            //    if (BasicClass.BasicFile.liST[0].BoxOrPic)
            //    {
            //        if (e.Column == _coAmount || e.Column == _coConversion)
            //        {
            //            int amount = int.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
            //            int box = int.Parse(gridView1.GetFocusedRowCellValue(_coConversion).ToString());
            //            int b = (int)(amount / box);
            //            gridView1.SetFocusedRowCellValue(_coMeasureID, b);
            //        }
            //    }
            //    else
            //    {
            //        if (e.Column == _coMeasureID || e.Column == _coConversion)
            //        {
            //            int boxamount = int.Parse(gridView1.GetFocusedRowCellValue(_coMeasureID).ToString());
            //            int box = int.Parse(gridView1.GetFocusedRowCellValue(_coConversion).ToString());
            //            int b = (int)(boxamount * box);
            //            gridView1.SetFocusedRowCellValue(_coAmount, b);
            //        }
            //    }
            //}
            //catch { }
            try
            {
                if (e.Column == _coPrice || e.Column == _coAmount)
                {
                    price = decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                    amount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
                    //if (e.Column == _coAmount && amount > decimal.Parse(gridView1.GetFocusedRowCellValue(_coDepotAmount).ToString()))
                    //{
                    //    XtraMessageBox.Show("销售数量不能超过现有库存数量！");
                    //    gridView1.SetFocusedRowCellValue(_coAmount, gridView1.GetFocusedRowCellValue(_coDepotAmount));
                    //}
                    gridView1.SetFocusedRowCellValue(_coMoney, (price * amount).ToString("n2"));
                    SumMoney();
                    ShowLoan();
                }
            }
            catch { }
            //if (e.Column == _coDepotAmount &&  decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString())>decimal.Parse(e.Value.ToString()))
            //{
            //    XtraMessageBox.Show("销售数量不能超过现有库存数量！");
            //    gridView1.SetFocusedRowCellValue(_coAmount, e.Value);
            //}
            if ((e.Column == _coMaterielID || e.Column == _coColorID || e.Column == _coSizeID || e.Column == _coMeasureID) && e.Value.ToString() != "0")
                CheckMateriel(gridView1.GetFocusedRowCellValue(_coMaterielID), gridView1.GetFocusedRowCellValue(_coColorID), gridView1.GetFocusedRowCellValue(_coSizeID), gridView1.GetFocusedRowCellValue(_coMeasureID));
        }
        private void CheckMateriel(object mat, object brand, object size, object MeasureID)
        {
            //_depotAmount = decimal.Parse(BasicClass.GetDataSet.GetOne(BasicClass.strBll.bllRepertory, "GetAmount", new object[] { mat, brand, size, _depotID, MeasureID }).ToString());
            //gridView1.SetFocusedRowCellValue(_coDepotAmount, _depotAmount);
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (i != gridView1.FocusedRowHandle)
                {
                    if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && gridView1.GetRowCellValue(i, _coColorID).Equals(brand)
                        && gridView1.GetRowCellValue(i, _coSizeID).Equals(size) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(MeasureID))
                    {
                        XtraMessageBox.Show("同一入库单中，不能有多个相同记录！");
                        SendKeys.Send("{Esc}");
                        return;
                    }
                }
            }
        }
        private void SetPrice(int rowID)
        {
            int mater = int.Parse(gridView1.GetRowCellValue(rowID, _coMaterielID).ToString());
            int brandID = int.Parse(gridView1.GetRowCellValue(rowID, _coBrandID).ToString());
            int measureid = int.Parse(gridView1.GetRowCellValue(rowID, _coMeasureID).ToString());
            object[] o = new object[] { _companyID, mater, brandID, measureid, 0 };
            object p = BasicClass.GetDataSet.GetOne(blQP, "GetPrice", o);
            //DataSet sp = BasicClass.GetDataSet.GetDS(BasicClass.strBll.bllRepertory, "GetPrice", o);
            //if (sp.Tables[0].Rows.Count > 0)
            //    gridView1.SetRowCellValue(rowID, _coPrice, sp.Tables[0].Rows[0]["SellPrice"]);
            //else
            //    gridView1.SetRowCellValue(rowID, _coPrice, 0);
            //if (decimal.Parse(p.ToString()) != 0)
            //    gridView1.SetRowCellValue(rowID, _coNowPrice, p);
            //else
            //    gridView1.SetRowCellValue(rowID, _coNowPrice, sp.Tables[0].Rows[0]["SellPrice"]);
            gridView1.SetRowCellValue(rowID, _coPrice, p);
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                if (e.FocusedRowHandle < 0)
                    gridView1.AddNewRow();
            }
            if (!t && e.FocusedRowHandle > -1)
            {
                for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
                {
                    gridView1.VisibleColumns[i].OptionsColumn.AllowEdit = (int.Parse(gridView1.GetFocusedRowCellValue(_coStockInfoID).ToString()) == 0);
                }
                _coNeedIsEnd.OptionsColumn.AllowEdit = true;
                //gridView1.OptionsBehavior.Editable = (int.Parse(gridView1.GetFocusedRowCellValue(_coStockInfoID).ToString()) == 0);
            }
            else if (e.FocusedRowHandle < 0)
            {
                for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
                {
                    gridView1.VisibleColumns[i].OptionsColumn.AllowEdit = true;
                }
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int c = 0;
            if (gridView1.RowCount == 0)
                c = 1;
            for (int i = c; i < gridView1.Columns.Count; i++)
            {
                gridView1.SetFocusedRowCellValue(gridView1.Columns[i], 0);
            }
            gridView1.SetFocusedRowCellValue(_coRemark, "");
            gridView1.SetFocusedRowCellValue(_coMainID, _MainID);
            gridView1.SetFocusedRowCellValue(_coA, 3);
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0 && _rowCount < gridView1.RowCount)
            {
                int r = gridView1.RowCount - 1;
                object o = gridView1.GetRowCellValue(r, _coMaterielID);
                object b = gridView1.GetRowCellValue(r, _coColorID);
                object s = gridView1.GetRowCellValue(r, _coSizeID);
                object m = gridView1.GetRowCellValue(r, _coMeasureID);
                //object b = gridView1.GetRowCellValue(r, _coBrandID);|| b == null || b.ToString().Trim() == "" ||b.ToString().Trim() == "0"
                if (o == null || o.ToString().Trim() == "" || o.ToString().Trim() == "0")
                {
                    gridView1.DeleteRow(r);
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i != gridView1.FocusedRowHandle)
                        {
                            if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(o) && gridView1.GetRowCellValue(i, _coColorID).Equals(b)
                                && gridView1.GetRowCellValue(i, _coSizeID).Equals(s) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(m))
                            {
                                //gridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);
                                //SendKeys.Send("{Esc}");
                                XtraMessageBox.Show("同一入库单中，不能有多个相同记录！");
                                gridView1.DeleteRow(r);
                                return;
                            }
                        }
                    }
                }
                //if (gridView1.RowCount == 8)
                //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                //else
                //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            }
            _rowCount = gridView1.RowCount;
            SumMoney();
            ShowLoan();
        }
        private void SumMoney()
        {
            money = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                try
                {
                    money += decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString());
                }
                catch { }
            }
        }
        private void _leCompany_EditValueChanged(object val, string text)
        {
            _companyID = int.Parse(val.ToString());
            if (_MainID == 0)
            {
                last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                dtBack.Clear();
                dtBack = BasicClass.GetDataSet.GetDS(blCL, "GetBackMoney", new object[] { _companyID }).Tables[0];
                back = 0;
                backDate = "      年    月    日";
                if (dtBack.Rows.Count > 0)
                {
                    if (dtBack.Rows[0]["Money"] != null && dtBack.Rows[0]["Money"].ToString().Trim() != string.Empty)
                    {
                        back = decimal.Parse(dtBack.Rows[0]["Money"].ToString());
                        backDate = ((DateTime)(dtBack.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                    }
                }
                ShowLoan();

            }

        }

        void r_RowChanged(DataTable dt)
        {
            _IsChangCom = false;
            for (int i = 0; i < dtSBI.Rows.Count; i++)
            {
                if (int.Parse(dtSBI.Rows[i]["A"].ToString()) == 4)
                {
                    dtSBI.Rows[i].Delete();
                    if (i > -1)
                        i -= 1;
                }
            }
            dtSBI.AcceptChanges();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtSBI.NewRow();
                dr["ID"] = 0;
                dr["A"] = 3;
                dr["MaterielID"] = dt.Rows[i]["MaterielID"];
                dr["ColorID"] = dt.Rows[i]["ColorID"];
                dr["ColorOneID"] = dt.Rows[i]["ColorOneID"];
                dr["ColorTwoID"] = dt.Rows[i]["ColorTwoID"];
                dr["SizeID"] = dt.Rows[i]["SizeID"];
                dr["CompanyMeasureID"] = dr["DepotMeasureID"] = dt.Rows[i]["DepotMeasureID"];
                dr["Price"] = dt.Rows[i]["Price"];
                dr["Amount"] = dt.Rows[i]["NowAmount"];
                dr["Money"] = dt.Rows[i]["Money"];
                dr["Remark"] = "";
                dr["MainID"] = _MainID;
                dr["StockInfoID"] = dt.Rows[i]["StockInfoID"];
                dr["StringTaskID"] = dt.Rows[i]["StringTaskID"];
                dr["MListID"] = dt.Rows[i]["MListID"];
                dr["NeedIsEnd"] = dt.Rows[i]["NeedIsEnd"];
                if (dr["NeedIsEnd"] == null || dr["NeedIsEnd"] == DBNull.Value || dr["NeedIsEnd"].ToString() == string.Empty)
                {
                    dr["NeedIsEnd"] = 0;
                }
                dr["MaterielRemark"] = dt.Rows[i]["MaterielRemark"];
                dtSBI.Rows.Add(dr);
            }
            gridControl1.DataSource = dtSBI;
            ShowTask();
        }
        private void ShowLoan()
        {
            string ssss = "此前欠供应商：  " + _leCompany.valStr + "  货款： " + last.ToString("N2") + "元，";
            if (backDate == "      年    月    日")
                ssss = ssss + "期间未还款，";
            else
                ssss = ssss + backDate + "还款/退货： " + back.ToString("N2") + "元，";
            ssss = ssss + "加本单：" + money.ToString("N2") + "元，结欠货款：" + (last - back + money).ToString("N2") + "元。";
            _barLoan.Caption = ssss;
        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("是否确认删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
            {
                if (DialogResult.Yes == MessageBox.Show("请再次确认是否删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    if (_MainID > 0)
                    {
                        object[] o = new object[] { _MainID };
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSellInfo, "DeleteByMainID", o);
                        BasicClass.GetDataSet.ExecSql(blSBI, "DeleteByMain", o);
                        BasicClass.GetDataSet.ExecSql(bllSB, "Delete", o);
                    }
                    InData();
                    bs.Position = dtMain.Rows.Count - 1;
                    if (bs.Position == 0)
                        ShowView(0);
                }
            }
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == MessageBox.Show("是否确认删除该条记录？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    int _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                    int _brandID = int.Parse(gridView1.GetFocusedRowCellValue(_coColorID).ToString());
                    int id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (id > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blSBI, "Delete", new object[] { id });
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSBI.AcceptChanges();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
                {
                    XtraMessageBox.Show("有明细记录没有金额！");
                    return;
                }
            }
            if (!Save())
                return;
            if (_companyID > 0)
            {
                #region 移至过帐
                DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
                DataRow dr = dtCL.NewRow();
                dr["ID"] = 0;
                dr["CompanyID"] = _companyID;
                dr["DateTime"] = dtSB.Rows[0]["DataTime"];
                dr["LastMoney"] = last + back;
                dr["ChangMoney"] = money;
                dr["Money"] = last + money - back;
                dr["TypeID"] = (int)(BasicClass.Enums.MoneyTableType.Back);
                dr["TableID"] = _MainID;
                dr["NowMoneyTypeID"] = 0;
                dr["NowMoney"] = 0;
                dr["NowReta"] = 1;
                dr["A"] = 1;
                dtCL.Rows.Add(dr);
                BasicClass.GetDataSet.Add(blCL, dtCL);
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllCompany, "UpLastMoney", new object[] { _companyID, (last + money - back) });
                #endregion
                int MaterielID = 0;
                int BrandID = 0;
                int oneID = 0;
                decimal Price = 0;
                int measureid = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    MaterielID = int.Parse(gridView1.GetRowCellValue(i, _coMaterielID).ToString());
                    Price = decimal.Parse(gridView1.GetRowCellValue(i, _coPrice).ToString());
                    measureid = int.Parse(gridView1.GetRowCellValue(i, _coMeasureID).ToString());
                    BrandID = 0;// Convert.ToInt32(gridView1.GetRowCellValue(i, _coBrandID));
                    object[] o = new object[] { _companyID, MaterielID, BrandID, Price, measureid, 0 };
                    BasicClass.GetDataSet.ExecSql(blQP, "UpPrice", o);
                }
            }

            BasicClass.GetDataSet.ExecSql(bllSB, "VerifyWXIn", new object[] { _MainID, true, BasicClass.UserInfo.UserID });
            ShowView(bs.Position);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.Back), _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本单之后已有后续业务发生，不能弃审！");
                return;
            }
            if (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) > 10)
            {
                XtraMessageBox.Show("本单已过帐，不能弃审！");
                return;
            }
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.Back), _MainID });
            BasicClass.GetDataSet.ExecSql(bllSB, "VerifyWXIn", new object[] { _MainID, false, 0 });
            ShowView(bs.Position);
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        { }


        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("请审核后再打印单据！");
                return;
            }
            int _id = int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllSysTem, "GetMaxId", null).ToString()) - 1;
            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSysTem, "GetList", new object[] { "(ID=" + _id + ")" });
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show("请完善公司信息！");
                //Form fr = new BaseForm.UserBaseSetForm();
                //fr.ShowDialog();
                return;
            }
            ds.Tables[0].TableName = "Main";
            ds.Tables["Main"].Columns.Add("LastMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("BackMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("NowMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("NewMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("DXMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("ComName", typeof(string));
            ds.Tables["Main"].Columns.Add("Date", typeof(string));
            ds.Tables["Main"].Columns.Add("Num", typeof(string));
            ds.Tables["Main"].Columns.Add("UserName", typeof(string));
            ds.Tables["Main"].Columns.Add("SumAmount", typeof(string));
            ds.Tables["Main"].Columns.Add("SumBoxAmount", typeof(string));
            ds.Tables["Main"].Columns.Add("SumMoney", typeof(string));
            //  ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("BackDate", typeof(string));
            ds.Tables[0].Rows[0]["LastMoney"] = last.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["BackMoney"] = back.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NowMoney"] = money.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NewMoney"] = (last - back + money).ToString("C2") + "元。";
            ds.Tables[0].Rows[0]["DXMoney"] = BasicClass.BasicFile.CmycurD(last - back + money);
            ds.Tables[0].Rows[0]["ComName"] = _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;
            ds.Tables[0].Rows[0]["BackDate"] = backDate;
            ds.Tables["Main"].Columns.Add("审核", typeof(string));
            ds.Tables["Main"].Columns.Add("制单", typeof(string));
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("BrandName", typeof(string));
            dt.Columns.Add("ColorName", typeof(string));
            dt.Columns.Add("SizeName", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("Money", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("NeedAmount", typeof(string));
            decimal amount = 0;
            decimal boxAmount = 0;
            decimal moeny = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                dr[1] = gridView1.GetRowCellDisplayText(i, _coBrandID);
                dr[2] = gridView1.GetRowCellDisplayText(i, _coColorID);
                dr[3] = gridView1.GetRowCellDisplayText(i, _coSizeID);
                dr[4] = gridView1.GetRowCellDisplayText(i, _coAmount);
                dr[5] = gridView1.GetRowCellDisplayText(i, _coPrice);
                dr[6] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[7] = gridView1.GetRowCellDisplayText(i, _coMoney);
                dr[8] = gridView1.GetRowCellDisplayText(i, _coRemark);
                try
                {
                    if (Convert.ToInt32(gridView1.GetRowCellValue(i, "StockInfoID")) > 0)
                    {
                        dr[9] = Convert.ToDecimal(BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(ID=" + Convert.ToInt32(gridView1.GetRowCellValue(i, "StockInfoID")) + ")" }).Tables[0].Rows[0]["NotAmount"]) + Convert.ToDecimal(dr[4]);
                    }
                    else
                    {
                        dr[9] = dr[4];
                    }
                }
                catch (Exception ex)
                {
                    dr[9] = dr[4];
                }
                dt.Rows.Add(dr);
                amount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount));
                boxAmount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMeasureID));
                moeny += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
            }
            ds.Tables[0].Rows[0]["SumAmount"] = amount;
            ds.Tables[0].Rows[0]["SumBoxAmount"] = boxAmount;
            ds.Tables[0].Rows[0]["SumMoney"] = moeny.ToString("C2");
            for (int i = dt.Rows.Count; i < 8; i++)
            {
                dt.Rows.Add(dt.NewRow());
                //dt.Rows[i]["Amount"] = DBNull.Value;
                //dt.Rows[i]["Price"] = DBNull.Value;
                //dt.Rows[i]["Conversion"] = DBNull.Value;
                //dt.Rows[i]["Money"] = DBNull.Value;
                //dt.Rows[i]["BoxMeasureAmount"] = DBNull.Value;
            }
            try
            {
                ds.Tables["Main"].Rows[0]["审核"] = dtUser.Select("(ID=" + Convert.ToInt32(dtSB.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            try
            {
                ds.Tables["Main"].Rows[0]["制单"] = dtUser.Select("(ID=" + Convert.ToInt32(dtSB.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintStockToDep(ds);

        }

        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("请审核后再打印单据！");
                return;
            }
            DataTable dtMain = new DataTable();
            dtMain.TableName = "Main";
            dtMain.Columns.Add("Date", typeof(string));
            dtMain.Columns.Add("Num", typeof(string));
            dtMain.Columns.Add("ComName", typeof(string));
            dtMain.Rows.Add(dtMain.NewRow());
            dtMain.Rows[0]["ComName"] = _leCompany.valStr;
            dtMain.Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            dtMain.Rows[0]["Num"] = _ltNum.val;
            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetReport", new object[] { _MainID });
            ds.Tables[0].TableName = "Info";
            ds.Tables.Add(dtMain);
            BaseForm.PrintClass.PrintSellInfo(ds);
        }

        private void _leDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (_MainID == 0)
                _depotID = Convert.ToInt32(_leDepot.EditValue);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (_companyID > 0)
            {
                //if (BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(ID=" + _companyID + ")" }).Tables[0].Rows[0]["Name"].ToString() == text)
                //{
                dtSBI.Rows.Clear();
                dtSB.AcceptChanges();
                _IsChangCom = true;
                BasicClass.cResult r = new cResult();
                r.RowChanged += new RowChangedHandler(r_RowChanged);
                Form fr = new Stock.frStockBack(r, _companyID, (int)BasicClass.Enums.TableType.ProcessingTask);
                fr.ShowDialog();
                //}
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_companyID > 0)
            {
                if (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) < 3)
                {
                    XtraMessageBox.Show("请先审核！");
                    return;
                }
                else if (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) == (int)BasicClass.Enums.IsVerify.已过帐)
                {
                    XtraMessageBox.Show("本单已过帐！");
                    return;
                }
                if (BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] {"(TableID="+_MainID+") And (TypeID="+(int)BasicClass.Enums.MoneyTableType.Back+")" }).Tables[0].Rows.Count > 0)
                {
                    XtraMessageBox.Show("本单已过帐！");
                    return;
                }
                if (DialogResult.No == XtraMessageBox.Show("是否确认过帐处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
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
                dtSB.Rows[0]["Money"] = money;
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
                dr["TableID"] = _MainID;
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
}