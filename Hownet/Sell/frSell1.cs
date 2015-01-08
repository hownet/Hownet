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

namespace Hownet.Sell
{
    public partial class frSell1 : DevExpress.XtraEditors.XtraForm
    {
        public frSell1()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frSell1(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _companyID = 0;
        int _depotID = 0;
        int _upData = 0;
        int _IsVerify = 0;

        BindingSource bs = new BindingSource();
        private string bllPS = "Hownet.BLL.ProduceSell";
        private string blPSO = "Hownet.BLL.ProduceSellOne";
        private string blCL = "Hownet.BLL.CompanyLog";
        private string blQP = "Hownet.BLL.QuotePrice";
        DataTable dtPS = new DataTable();
        DataTable dtPSO = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtMat = new DataTable();
        string backDate = string.Empty;
        string lastDate = string.Empty;
        bool t = false;
        decimal price = 0;
        decimal money = 0;
        decimal amount = 0;
        decimal last = 0;
        decimal back = 0;
        object _oldMat = null;
        object _oldBrand = null;
        int _BrandID = 0;
        int _RowID = 0;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            if (_MainID == 0)
            {

                InData();
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                bs.Position = dtMain.Rows.Count - 1;
                simpleButton1.Visible = false;
                if (bs.Position == 0)
                    ShowView(0);
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                bs.DataSource = dtMain;
                bs.Position = 0;
                ShowView(0);
                //bar1.Visible = false;
                //simpleButton1.Visible = true;
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
            {
                _brSave.Enabled = false;
            }
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            {
                _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                _coPrice.Visible = _coPrice.OptionsColumn.AllowEdit = false;
                _coMoney.Visible = _coMoney.OptionsColumn.AllowEdit = false;
            }
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVierfy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(4=4)" }).Tables[0];
            _reMateriel.DataSource = dtMat.DefaultView;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
          //  _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _leDepotID.Par = new object[] { "TypeID=42" };
            _leDepotID.FormName = (int)BasicClass.Enums.TableType.Deparment;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            if (BasicClass.BasicFile.liST[0].Sell4Depot)
                _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _BrandID = Convert.ToInt32(BasicClass.GetDataSet.GetBySql("Select ID ,Name From Materiel Where (AttributeID=5)").Rows[0]["ID"]);
            ucGridLookup1.DisplayMember = "Name";
            ucGridLookup1.ValueMember = "ID";
            ucGridLookup1.TypeID = (int)BasicClass.Enums.TableType.Company;
        }

        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllPS, "GetIDList", new object[] { (int)Enums.TableType.Sell }).Tables[0];
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
          //  this._leCompany.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                last = decimal.Parse(dtPS.Rows[0]["LastMoney"].ToString());
                back = decimal.Parse(dtPS.Rows[0]["BackMoney"].ToString());
                money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
                lastDate = Convert.ToDateTime(dtPS.Rows[0]["LastDate"]).ToString("yyyy年MM月dd日");
               // _leCompany.IsNotCanEdit = true;
            }
            else
            {
                dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtPS.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["Depot"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(), 0 });
                dr["LastMoney"] = last = 0;
                dr["BackMoney"] = back = 0;
                dr["BackDate"] = DateTime.Parse("1900-1-1");
                money = 0;
                dtPS.Rows.Add(dr);
                _brAddNew.Enabled = false;
              //  _leCompany.IsNotCanEdit = false;
            }
            _MainID = Convert.ToInt32(dtPS.Rows[0]["ID"]);
            _upData = int.Parse(dtPS.Rows[0]["UpData"].ToString());
            _IsVerify = Convert.ToInt32(dtPS.Rows[0]["IsVerify"]);
            t = _IsVerify == 3;
            _ldDate.val = dtPS.Rows[0]["DateTime"];
            _companyID = int.Parse(dtPS.Rows[0]["CompanyID"].ToString());// _leCompany.editVal =
            ucGridLookup1.Values = _companyID;
            _leDepotID.editVal = _depotID = int.Parse(dtPS.Rows[0]["Depot"].ToString());
            money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
            _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
            _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
            dtPSO = BasicClass.GetDataSet.GetDS(blPSO, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            dtInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            //_leCompany.IsCanEdit = !t;
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = !t;
            _brAddNew.Enabled = (_MainID>0 && p == dtMain.Rows.Count - 1);
            _barUnVierfy.Enabled = t;
            if ((DateTime)(dtPS.Rows[0]["BackDate"]) == DateTime.Parse("1900-1-1"))
                backDate = "      年    月    日";
            else
                backDate = ((DateTime)(dtPS.Rows[0]["BackDate"])).ToString("yyyy年MM月dd日");
            if (!t)
            {
                dt = BasicClass.GetDataSet.GetDS(blCL, "GetSellLastMoney", new object[] { _companyID }).Tables[0];
                last = 0;
                lastDate = "      年    月    日";
                if (dt.Rows.Count > 0)
                {
                    last = Convert.ToDecimal(dt.Rows[0]["Money"]);// 0;// decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                    lastDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                }
                dt.Clear();
                dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];
                back = 0;
                backDate = "      年    月    日";
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
                    {
                        back = decimal.Parse(dt.Rows[0]["Money"].ToString());
                        backDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                    }
                }
                SumMoney();
            
                //  ShowLoan();
            }



            //gridView1.OptionsBehavior.Editable = !t;
            SetColumnsReadOnly();
            //if (t)
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //else
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            ShowLoan();
           // this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            _leDepotID.IsNotCanEdit = t;// _leCompany.IsNotCanEdit =
            if(!t)
            {
               
                DataRow dr = dtPSO.NewRow();
                for(int i=0;i<dtPSO.Columns.Count;i++)
                {
                    dr[i] = 0;
                }
                dr["A"] = 3;
                dr["Conversion"] = 3;
                dr["Remark"] = dr["SellRemark"] = string.Empty;
                dr["RowID"] = dtPSO.Rows.Count + 1;
                dtPSO.Rows.Add(dr.ItemArray);
                dr["RowID"] = dtPSO.Rows.Count + 1;
                dtPSO.Rows.Add(dr.ItemArray);
            }
            gridControl1.DataSource = dtPSO;

            ucGridLookup1.IsReadOnly = _MainID > 0;
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
                if (f)
                {
                    int d = bs.Position;
                    InData();
                    bs.Position = d;
                    ShowView(d);
                }
            }
        }
        private bool Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPSO.AcceptChanges();
            dtInfo.AcceptChanges();
            if (_companyID == 0)
            {
                XtraMessageBox.Show("请选择客户！");
                return false;
            }
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择出货仓库！");
                return false;
            }
            //last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetSellLastMoney", new object[] { _companyID }).ToString());
            //dt.Clear();
            //dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];
            //back = 0;
            //backDate = "      年    月    日";
            //dtPS.Rows[0]["BackDate"] = DateTime.Parse("1900-1-1");
            //if (dt.Rows.Count > 0)
            //{
            //    back = 0;
            //    backDate = "      年    月    日";
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
            //        {
            //            back = decimal.Parse(dt.Rows[0]["Money"].ToString());
            //            dtPS.Rows[0]["BackDate"] = dt.Rows[0]["DateTime"];
            //            backDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
            //        }
            //    }

            //}

            dt = BasicClass.GetDataSet.GetDS(blCL, "GetSellLastMoney", new object[] { _companyID }).Tables[0];
            last = 0;
            lastDate = "      年    月    日";
            dtPS.Rows[0]["LastDate"] = DateTime.Parse("1900-1-1");
            if (dt.Rows.Count > 0)
            {
                last = Convert.ToDecimal(dt.Rows[0]["Money"]);// 0;// decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                lastDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                dtPS.Rows[0]["LastDate"] = dt.Rows[0]["DateTime"];
            }
            dt.Clear();
            dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];
            back = 0;
            backDate = "      年    月    日";
            dtPS.Rows[0]["BackDate"] = DateTime.Parse("1900-1-1");
            if (dt.Rows.Count > 0)
            {
                back = 0;
                backDate = "      年    月    日";
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
                    {
                        back = decimal.Parse(dt.Rows[0]["Money"].ToString());
                        dtPS.Rows[0]["BackDate"] = dt.Rows[0]["DateTime"];
                        backDate = Convert.ToDateTime((dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                    }
                }

            }
            dtPS.Rows[0]["Depot"] = _depotID;
            dtPS.Rows[0]["LastMoney"] = last;
            dtPS.Rows[0]["BackMoney"] = back;
            dtPS.Rows[0]["Remark"] = _ltRemark.val;
            dtPS.Rows[0]["Money"] = money;
            dtPS.Rows[0]["CompanyID"] = _companyID;
            dtPS.Rows[0]["DateTime"] = _ldDate.val;
            dtPS.Rows[0]["State"] = (int)Enums.TableType.Sell;
            dtPS.Rows[0]["A"] = 1;
            if (_MainID == 0)
            {
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(), _companyID });
                dtMain.Rows[bs.Position]["ID"] = dtPS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllPS, dtPS);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtPS.Rows[0]["UpData"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllPS, dtPS);
                }
            }
            DataTable dtt = dtPSO.Clone();
            for (int i = 0; i < dtPSO.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtPSO.Rows[i]["MaterielID"]) > 0)
                {
                    int a = int.Parse(dtPSO.Rows[i]["A"].ToString());
                    if (a > 1)
                    {
                        dtPSO.Rows[i]["MainID"] = _MainID;
                        dtt.Rows.Clear();
                        dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blPSO, dtt);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(blPSO, dtt);
                        }
                        dtPSO.Rows[i]["A"] = 1;
                    }
                }
            }
            DataTable ttt = dtInfo.Clone();
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                int a = int.Parse(dtInfo.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    dtInfo.Rows[i]["MainID"] = _MainID;
                    if (Convert.ToInt32(dtInfo.Rows[i]["PSOID"]) == 0)
                        dtInfo.Rows[i]["PSOID"] = dtPSO.Select("(RowID=" + dtInfo.Rows[i]["RowID"] + ")")[0]["ID"];
                    ttt.Rows.Clear();
                    ttt.Rows.Add(dtInfo.Rows[i].ItemArray);
                    if (a == 3)
                    {
                        dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllProduceSellInfo, ttt);
                    }
                    else if (a == 2)
                    {
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProduceSellInfo, ttt);
                    }
                    dtInfo.Rows[i]["A"] = 1;
                }
            }
            ShowLoan();
            //_leCompany.IsNotCanEdit = true;
            ucGridLookup1.IsReadOnly = true;
            _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
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
            if (e.Value == DBNull.Value)
                return;
            if (e.RowHandle > -1)
            {
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            //if (e.Column == _coMaterielID && Convert.ToInt32(e.Value) > 0)
            //{
            //    gridView1.SetFocusedRowCellValue(_coBrandID, _BrandID);
            //}
            try
            {
                if (e.Column == _coBrandID || e.Column == _coMaterielID)
                {
                    SetPrice(e.RowHandle);
                }
            }
            catch { }

            try
            {
                if (BasicClass.BasicFile.liST[0].BoxOrPic)
                {
                    if (e.Column == _coAmount || e.Column == _coConversion)
                    {
                        int amount = int.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
                        int box = int.Parse(gridView1.GetFocusedRowCellValue(_coConversion).ToString());
                        int b = (int)(amount / box);
                        gridView1.SetFocusedRowCellValue(_coBoxMeasureAmount, b);
                    }
                }
                else
                {
                    if (e.Column == _coBoxMeasureAmount || e.Column == _coConversion)
                    {
                        int boxamount = int.Parse(gridView1.GetFocusedRowCellValue(_coBoxMeasureAmount).ToString());
                        int box = int.Parse(gridView1.GetFocusedRowCellValue(_coConversion).ToString());
                        int b = (int)(boxamount * box);
                        gridView1.SetFocusedRowCellValue(_coAmount, b);
                    }
                }
            }
            catch { }
            try
            {
                if (e.Column == _coPrice || e.Column == _coAmount)
                {
                    price = decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                    amount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
                    gridView1.SetFocusedRowCellValue(_coMoney, (price * amount).ToString("n3"));
                    SumMoney();
                    ShowLoan();
                }
            }
            catch { }
            //if ((e.Column == _coMaterielID || e.Column == _coBrandID) && e.Value.ToString() != "0")
            //    CheckMateriel(gridView1.GetFocusedRowCellValue(_coMaterielID), gridView1.GetFocusedRowCellValue(_coBrandID));
            if (e.Column == _coMaterielID)
            {
                try
                {
                    gridView1.SetFocusedRowCellValue(_coCompanyMaterielName, gridView1.GetFocusedDisplayText());
                    gridView1.SetFocusedRowCellValue(_coMeasureID, dtMat.Select("(ID=" + e.Value + ")")[0]["MeasureID"]);
                }
                catch (Exception ex)
                {
                }
            }
            if(e.RowHandle==gridView1.RowCount-2)
            {
                DataRow dr = dtPSO.NewRow();
                for (int i = 0; i < dtPSO.Columns.Count; i++)
                {
                    dr[i] = 0;
                }
                dr["A"] = 3;
                dr["Conversion"] = 3;
                dr["Remark"] = dr["SellRemark"] = string.Empty;
                dr["RowID"] = Convert.ToInt32(dtPSO.Rows[dtPSO.Rows.Count - 1]["RowID"]) + 1;
                dtPSO.Rows.Add(dr.ItemArray);
            }
        }
        private void CheckMateriel(object mat, object brand)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (i != gridView1.FocusedRowHandle)
                {
                    if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && gridView1.GetRowCellValue(i, _coBrandID).Equals(brand))
                    {
                        gridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);
                        XtraMessageBox.Show("同一销售单中，同种商标的同一款号只能有一条记录！");
                        return;
                    }
                }
            }
        }
        private void SetPrice(int rowID)
        {
            int brand = int.Parse(gridView1.GetRowCellValue(rowID, _coBrandID).ToString());
            int mater = int.Parse(gridView1.GetRowCellValue(rowID, _coMaterielID).ToString());
            object[] o = new object[] { _companyID, mater, brand, 1, 0 };
            gridView1.SetRowCellValue(rowID, _coPrice, BasicClass.GetDataSet.GetOne(blQP, "GetPrice", o));
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (gridView1.RowCount > 0)
            //{
            //    if (e.FocusedRowHandle < 0)
            //        gridView1.AddNewRow();
            //    _oldMat = gridView1.GetFocusedRowCellValue(_coMaterielID);
            //    _oldBrand = gridView1.GetFocusedRowCellValue(_coBrandID);
            //}
            _coMaterielID.OptionsColumn.AllowEdit = _coBrandID.OptionsColumn.AllowEdit = e.FocusedRowHandle < gridView1.RowCount-1;
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //int c = 0;
            //if (gridView1.RowCount == 0)
            //    c = 1;
            //for (int i = c; i < gridView1.Columns.Count; i++)
            //{
            //    gridView1.SetFocusedRowCellValue(gridView1.Columns[i], 0);
            //}
            //gridView1.SetFocusedRowCellValue(_coRemark, "");
            //gridView1.SetFocusedRowCellValue(_coMainID, _MainID);
            //gridView1.SetFocusedRowCellValue(_coA, 3);
            //gridView1.SetFocusedRowCellValue(_coConversion, 3);
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                int r = gridView1.RowCount - 1;
                object o = gridView1.GetRowCellValue(r, _coMaterielID);
                //object b = gridView1.GetRowCellValue(r, _coBrandID);|| b == null || b.ToString().Trim() == "" ||b.ToString().Trim() == "0"
                //if (o == null || o.ToString().Trim() == "" || o.ToString().Trim() == "0")
                //{
                //    gridView1.DeleteRow(r);
                //}
                //else
                //{
                    SumMoney();
                    ShowLoan();
                //}
                //if (gridView1.RowCount > 14)
                //{
                //    //if (DialogResult.Yes == XtraMessageBox.Show("明细记录太多，是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                //    //{
                //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                //    //}
                //}
                //else
                //{
                //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                //}
               
            }
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
            if (BasicClass.BasicFile.liST[0].SellMoney == 1)
            {
                money = (int)(money + 0.5M);
            }

            else if (BasicClass.BasicFile.liST[0].SellMoney == 2)
            {
                money = (int)(money + 0.4M);
            }

        }
        private void _leCompany_EditValueChanged(object val, string text)
        {
            _companyID = int.Parse(val.ToString());
            //if (_MainID == 0)
            //{
            //    last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetSellLastMoney", new object[] { _companyID }).ToString());
            //    dt.Clear();
            //    dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];
            //    back = 0;
            //    backDate = "      年    月    日";
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
            //        {
            //            back = decimal.Parse(dt.Rows[0]["Money"].ToString());
            //            backDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
            //        }
            //    }
            //    SumMoney();
            //    ShowLoan();
            //}

            if (_MainID == 0)
            {
                last = 0;
                lastDate = "      年    月    日";
                dt = BasicClass.GetDataSet.GetDS(blCL, "GetSellLastMoney", new object[] { _companyID }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    last = Convert.ToDecimal(dt.Rows[0]["Money"]);// 0;// decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                    lastDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                }
                dt.Clear();
                dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];// dalCL.GetBackSellMoney(Convert.ToInt32(_companyID)).Tables[0];
                back = 0;
                backDate = "      年    月    日";
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
                    {
                        back = decimal.Parse(dt.Rows[0]["Money"].ToString());
                        backDate = Convert.ToDateTime(dt.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
                    }
                }
                SumMoney();
                ShowLoan();
            }
            //  ShowSalesList();
        }
        private void ShowLoan()
        {
            string ssss = "客户：  " + ucGridLookup1.StringValues + " 于" + lastDate + "欠货款： " + last.ToString("N2") + "元，";//_leCompany.valStr

            //  string ssss="客户：  " + _leCompany.valStr + "  此前欠货款： " + last.ToString("N2") + "元，";
            if (backDate == "      年    月    日")
                ssss = ssss + "期间未还款，";
            else
                ssss = ssss + backDate + "还款： " + back.ToString("N2") + "元，";
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
                        BasicClass.GetDataSet.ExecSql(blPSO, "DeleteByMain", o);
                        BasicClass.GetDataSet.ExecSql(bllPS, "Delete", o);
                    }
                    InData();
                    if (dtMain.Rows.Count > 0)
                        bs.Position = dtMain.Rows.Count - 1;
                    else
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
                    int _brandID = int.Parse(gridView1.GetFocusedRowCellValue(_coBrandID).ToString());
                    DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i].Delete();
                        }
                    }
                    dtInfo.AcceptChanges();
                    int id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (id > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blPSO, "Delete", new object[] { id });
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSellInfo, "DeleteInfo", new object[] {id });
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount-2; i++)
            {
                if (decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
                {
                    XtraMessageBox.Show("有明细记录没有金额！");
                    return;
                }
            }
            if (!Save())
                return;
            dtPS.Rows[0]["IsVerify"] = 3;
            dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Today;
            BasicClass.GetDataSet.UpData(bllPS, dtPS);
            DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
            DataRow dr = dtCL.NewRow();
            dr["ID"] = 0;
            dr["CompanyID"] = _companyID;
            dr["DateTime"] = dtPS.Rows[0]["DateTime"];
            dr["LastMoney"] = dtPS.Rows[0]["LastMoney"];
            dr["ChangMoney"] = money;
            dr["Money"] = last + money - back;
            dr["TypeID"] = (int)(BasicClass.Enums.MoneyTableType.Sell);
            dr["TableID"] = _MainID;
            dr["NowMoneyTypeID"] = 0;
            dr["NowMoney"] = 0;
            dr["NowReta"] = 1;
            dr["A"] = 1;
            dtCL.Rows.Add(dr);
            BasicClass.GetDataSet.Add(blCL, dtCL);
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                object[] o = new object[] { _companyID, int.Parse(gridView1.GetRowCellValue(i, _coMaterielID).ToString()), int.Parse(gridView1.GetRowCellValue(i, _coBrandID).ToString()), decimal.Parse(gridView1.GetRowCellValue(i, _coPrice).ToString()), 1, 0 };
                BasicClass.GetDataSet.ExecSql(blQP, "UpPrice", o);
            }
            if (BasicClass.BasicFile.liST[0].Sell4Depot)
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, true, _depotID });
            //gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //_barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = false;
            //_brAddNew.Enabled = _barUnVierfy.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            //t = true;
            //SetColumnsReadOnly();
            int d = bs.Position;
            InData();
            bs.Position = d;
            ShowView(d);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.Sell), _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本单之后已有后续业务发生，不能弃审！");
                return;
            }
            dtPS.Rows[0]["IsVerify"] = 1;
            dtPS.Rows[0]["VerifyMan"] = 0;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            BasicClass.GetDataSet.UpData(bllPS, dtPS);
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.Sell), _MainID });
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, false, _depotID });
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = true;
            _brAddNew.Enabled = _barUnVierfy.Enabled = false;
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            t = false;
            SetColumnsReadOnly();
            int d = bs.Position;
            //InData();
            bs.Position = d;
            ShowView(d);
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object o = gridView1.GetFocusedRowCellValue(_coMaterielID);
            if (o != null && o.ToString() != "0")
            {
                int _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                int _brandID = int.Parse(gridView1.GetFocusedRowCellValue(_coBrandID).ToString());
                DataTable ddt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetSumAmount", new object[] { _MainID }).Tables[0];

                DataTable dtTem = ddt.Clone();
                if (gridView1.FocusedRowHandle > -1)
                {
                    DataRow[] foundRows = ddt.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
                    if (foundRows.Length > 0)
                    {
                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            DataRow dr = dtTem.NewRow();
                            dr.ItemArray = foundRows[i].ItemArray;
                            dtTem.Rows.Add(dr);
                        }
                    }
                }
                //if (!t)
                //{
                    cResult r = new cResult();
                    r.RowChanged += new RowChangedHandler(r_RowChanged);
                   // Form fr = new SellTemForm(r, !t, dtTem, _materielID, _brandID, _depotID);
                    _RowID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coRowID));
                    Form fr = new frSellReportey(r, dtInfo, _materielID, _brandID, _depotID, t,_RowID);
                    fr.ShowDialog();
                //}
                //else
                //{
                //    Form fr = new Form1(dtTem);
                //    fr.ShowDialog();
                //}
            }
            else
            {
                XtraMessageBox.Show("请选择款号和商标！");
                return;
            }
        }
        void r_RowChanged(DataTable dt)
        {
            DataRow[] drs = dt.Select("(RowID=" + _RowID + ")");
            decimal amount=0;
            if(drs.Length>0)
            {
                for(int i=0;i<drs.Length;i++)
                {
                    amount += Convert.ToDecimal(drs[i]["Amount"]);
                }
            }
            gridView1.SetFocusedRowCellValue(_coAmount, amount);
            //bool f = false;
            //int amount = 0;
            //if (dt.Rows.Count > 0)
            //{

            //    for (int j = 0; j < dt.Rows.Count; j++)
            //    {
            //        f = false;
            //        try
            //        {
            //            string _materielID = dt.DefaultView[j]["MaterielID"].ToString();
            //            string _brandID = dt.DefaultView[j]["BrandID"].ToString();
            //            string _sizeID = dt.DefaultView[j]["SizeID"].ToString();
            //            string _colorID = dt.DefaultView[j]["ColorID"].ToString();
            //            amount += int.Parse(dt.DefaultView[j]["Amount"].ToString());
            //            if (dtInfo.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dtInfo.Rows.Count; i++)
            //                {
            //                    if (dtInfo.DefaultView[i]["MaterielID"].ToString() == _materielID && dtInfo.DefaultView[i]["BrandID"].ToString() == _brandID && dtInfo.DefaultView[i]["ColorID"].ToString() == _colorID && dtInfo.DefaultView[i]["SizeID"].ToString() == _sizeID)
            //                    {
            //                        dtInfo.Rows[i]["Amount"] = dt.Rows[j]["Amount"];
            //                        if (dtInfo.Rows[i]["A"].ToString() == "1")
            //                            dtInfo.Rows[i]["A"] = 2;
            //                        f = true;
            //                        break;
            //                    }
            //                }
            //            }
            //            if (!f)
            //            {
            //                DataRow dr = dtInfo.NewRow();
            //                dr.ItemArray = dt.Rows[j].ItemArray;
            //                dtInfo.Rows.Add(dr);
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    gridView1.SetFocusedRowCellValue(_coAmount, amount);
            //}
        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTable(0);
        }
        private void PrintTable(int A4A5)
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
            //ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("BackDate", typeof(string));
            ds.Tables["Main"].Columns.Add("LastDate", typeof(string));
            ds.Tables["Main"].Columns.Add("制单", typeof(string));
            ds.Tables["Main"].Columns.Add("审核", typeof(string));
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];

            ds.Tables[0].Rows[0]["LastMoney"] = last.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["BackMoney"] = back.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NowMoney"] = money.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NewMoney"] = (last - back + money).ToString("C2") + "元。";
            ds.Tables[0].Rows[0]["DXMoney"] = BasicClass.BasicFile.CmycurD(last - back + money);
            ds.Tables[0].Rows[0]["ComName"] = ucGridLookup1.StringValues;// _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');// _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;
            ds.Tables[0].Rows[0]["BackDate"] = backDate;
            ds.Tables[0].Rows[0]["LastDate"] = lastDate;

            try
            {
                ds.Tables[0].Rows[0]["审核"] = dtUser.Select("(ID=" + Convert.ToInt32(dtPS.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            try
            {
                ds.Tables[0].Rows[0]["制单"] = dtUser.Select("(ID=" + Convert.ToInt32(dtPS.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("BrandName", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Conversion", typeof(string));
            dt.Columns.Add("BoxMeasureAmount", typeof(string));
            dt.Columns.Add("Money", typeof(decimal));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("LingShouJia", typeof(string));
            dt.Columns.Add("CompanyMaterielName", typeof(string));
            int amount = 0;
            int boxAmount = 0;
            decimal moeny = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                dr[1] = gridView1.GetRowCellDisplayText(i, _coBrandID);
                dr[2] = gridView1.GetRowCellDisplayText(i, _coAmount);
                dr[3] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice));//.ToString("c2")
                dr[4] = gridView1.GetRowCellDisplayText(i, _coConversion);
                dr[5] = gridView1.GetRowCellDisplayText(i, _coBoxMeasureAmount);
                dr[6] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));//.ToString("c2")
                dr[7] = gridView1.GetRowCellDisplayText(i, _coRemark);
                dr[8] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[10] = gridView1.GetRowCellDisplayText(i, _coCompanyMaterielName);
                dt.Rows.Add(dr);
                amount += int.Parse(gridView1.GetRowCellValue(i, _coAmount).ToString());
                boxAmount += int.Parse(gridView1.GetRowCellValue(i, _coBoxMeasureAmount).ToString());
                moeny += decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString());
            }
            ds.Tables[0].Rows[0]["SumAmount"] = amount;
            ds.Tables[0].Rows[0]["SumBoxAmount"] = boxAmount;
            ds.Tables[0].Rows[0]["SumMoney"] = moeny.ToString("C2");
            for (int i = dt.Rows.Count; i < 6; i++)
            {
                dt.Rows.Add(dt.NewRow());
                //dt.Rows[i]["Amount"] = DBNull.Value;
                //dt.Rows[i]["Price"] = DBNull.Value;
                //dt.Rows[i]["Conversion"] = DBNull.Value;
                //dt.Rows[i]["Money"] = DBNull.Value;
                //dt.Rows[i]["BoxMeasureAmount"] = DBNull.Value;
            }
            if (A4A5 > 0)
            {
                for (int i = dt.Rows.Count; i < 15; i++)
                {
                    dt.Rows.Add(dt.NewRow());
                    //dt.Rows[i]["Amount"] = DBNull.Value;
                    //dt.Rows[i]["Price"] = DBNull.Value;
                    //dt.Rows[i]["Conversion"] = DBNull.Value;
                    //dt.Rows[i]["Money"] = DBNull.Value;
                    //dt.Rows[i]["BoxMeasureAmount"] = DBNull.Value;
                }
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintSellTable(ds, A4A5);

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
            dtMain.Rows[0]["ComName"] = ucGridLookup1.StringValues;// _leCompany.valStr;
            dtMain.Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            dtMain.Rows[0]["Num"] = _ltNum.val;
            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetReport", new object[] { _MainID });
            ds.Tables[0].TableName = "Info";
            ds.Tables.Add(dtMain);
            BaseForm.PrintClass.PrintSellInfo(ds);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PrintTable(0);
        }

        private void _reMateriel_Leave(object sender, EventArgs e)
        {

        }

        private void _reMateriel_QueryCloseUp(object sender, CancelEventArgs e)
        {
            LookUpEdit li = sender as LookUpEdit;
            object ss = li.Text.Trim();
            bool f = false;
            int id = 0;
            DataView dv = (DataView)(li.Properties.DataSource);
            if (ss != null && ss.ToString().Trim() != "")
            {
                BaseContranl.DataSoure bllDS = new BaseContranl.DataSoure();
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv[i][li.Properties.DisplayMember].ToString() == ss.ToString().Trim())
                    {
                        f = true;
                        break;
                    }
                }
                if (!f)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("所填內容不在已有列表中，是否新增？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        id = bllDS.AddNew((int)BasicClass.Enums.TableType.Product, ss.ToString().Trim());
                        dv = bllDS.getDS((int)BasicClass.Enums.TableType.Product);
                        _reMateriel.DataSource = dv;
                        gridView1.SetFocusedValue(id);
                    }
                    else
                    {
                        li.EditValue = 0;
                    }
                }
            }
        }

        private void _leDepotID_EditValueChanged(object val, string text)
        {
            if (_MainID == 0)
            {
                _depotID = int.Parse(val.ToString());
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTable(2);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (_companyID > 0)
            {
                BasicClass.cResult r = new cResult();
                r.TextChanged += r_TextChanged;
                Form fr = new Finance.BsInMoneyForm(_companyID, 0, r);
                fr.ShowDialog();

            }
        }

        void r_TextChanged(string s)
        {
            if(s!="0")
            {
                if (_MainID == 0)
                {
                    last = 0;
                    lastDate = "      年    月    日";
                    dt = BasicClass.GetDataSet.GetDS(blCL, "GetSellLastMoney", new object[] { _companyID }).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        last = Convert.ToDecimal(dt.Rows[0]["Money"]);// 0;// decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                        lastDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                    }
                    dt.Clear();
                    dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];// dalCL.GetBackSellMoney(Convert.ToInt32(_companyID)).Tables[0];
                    back = 0;
                    backDate = "      年    月    日";
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
                        {
                            back = decimal.Parse(dt.Rows[0]["Money"].ToString());
                            backDate = Convert.ToDateTime(dt.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
                        }
                    }
                    SumMoney();
                    ShowLoan();
                }
            }
        }

        private void ucGridLookup1_EditValueChanged(object val, string text)
        {
            _companyID = int.Parse(val.ToString());
            if (_MainID == 0)
            {
                last = 0;
                lastDate = "      年    月    日";
                dt = BasicClass.GetDataSet.GetDS(blCL, "GetSellLastMoney", new object[] { _companyID }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    last = Convert.ToDecimal(dt.Rows[0]["Money"]);// 0;// decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackLastMoney", new object[] { _companyID }).ToString());
                    lastDate = ((DateTime)(dt.Rows[0]["DateTime"])).ToString("yyyy年MM月dd日");
                }
                dt.Clear();
                dt = BasicClass.GetDataSet.GetDS(blCL, "GetBackSellMoney", new object[] { _companyID }).Tables[0];// dalCL.GetBackSellMoney(Convert.ToInt32(_companyID)).Tables[0];
                back = 0;
                backDate = "      年    月    日";
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Money"] != null && dt.Rows[0]["Money"].ToString().Trim() != string.Empty)
                    {
                        back = decimal.Parse(dt.Rows[0]["Money"].ToString());
                        backDate = Convert.ToDateTime(dt.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
                    }
                }
                SumMoney();
                ShowLoan();
            }
        }

        private void barPrintInfoList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintInfoList(false);
        }
        private void PrintInfoList(bool IsDesign)
        {
            if (!t)
            {
                XtraMessageBox.Show("未审核！");
                return;
            }
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";

            DataTable dtOne = new DataTable();
            dtOne.TableName = "Main";
            dtOne.Columns.Add("编号", typeof(string));
            dtOne.Columns.Add("客户", typeof(string));
            dtOne.Columns.Add("说明", typeof(string));
            dtOne.Columns.Add("制单", typeof(string));
            dtOne.Columns.Add("审核", typeof(string));
            dtOne.Columns.Add("日期", typeof(string));
            DataRow drOne = dtOne.NewRow();

            drOne[0] = _ltNum.EditVal;
            drOne[1] = ucGridLookup1.StringValues;// _leCompany.valStr;
            drOne[2] = _ltRemark.EditVal;
            if (Convert.ToInt32(dtPS.Rows[0]["FillMan"]) > 0)
                drOne[3] = dtUser.Select("(ID=" + Convert.ToInt32(dtPS.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
            if (Convert.ToInt32(dtPS.Rows[0]["VerifyMan"]) > 0)
                drOne[4] = dtUser.Select("(ID=" + Convert.ToInt32(dtPS.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            drOne[5] = _ldDate.strLab;
            dtOne.Rows.Add(drOne);
            ds.Tables.Add(dtOne);

            DataTable dtTwo = new DataTable();
            dtTwo.TableName = "List";
            dtTwo.Columns.Add("编号", typeof(string));
            dtTwo.Columns.Add("款号", typeof(string));
            dtTwo.Columns.Add("商标", typeof(string));
            dtTwo.Columns.Add("数量", typeof(decimal));
            dtTwo.Columns.Add("单价", typeof(decimal));
            dtTwo.Columns.Add("金额", typeof(decimal));
            dtTwo.Columns.Add("货期", typeof(string));
            dtTwo.Columns.Add("单位", typeof(string));
            dtTwo.Columns.Add("ID", typeof(int));
            for (int i = 0; i <gridView1 .RowCount; i++)
            {
                DataRow drTwo = dtTwo.NewRow();
              //  drTwo[0] = gridView1.GetRowCellDisplayText(i, _coNum);
                drTwo[1] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                drTwo[2] = gridView1.GetRowCellDisplayText(i, _coBrandID);
                drTwo[3] = gridView1.GetRowCellValue(i, _coAmount);
                drTwo[4] = gridView1.GetRowCellValue(i, _coPrice);
                drTwo[5] = gridView1.GetRowCellValue(i, _coMoney);
               // drTwo[6] = gridView1.GetRowCellDisplayText(i, _coLastDate);
                drTwo[7] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                drTwo[8] = gridView1.GetRowCellValue(i, _coID);
                dtTwo.Rows.Add(drTwo);
            }
            ds.Tables.Add(dtTwo);

            DataTable dtThree = new DataTable();
            if (dtTwo.Rows.Count > 0)
                dtThree = BasicClass.OrderTask.ShowPSInfo(Convert.ToInt32(dtTwo.Rows[0]["ID"]), (int)BasicClass.Enums.TableType.SalesOne);
            if (dtTwo.Rows.Count > 1)
            {
                for (int i = 1; i < dtTwo.Rows.Count; i++)
                {
                    DataTable dtTem = BasicClass.OrderTask.ShowPSInfo(Convert.ToInt32(dtTwo.Rows[i]["ID"]), (int)BasicClass.Enums.TableType.SalesOne);
                    for (int j = 0; j < dtTem.Rows.Count; j++)
                    {
                        try
                        {
                            dtThree.Rows.Add(dtTem.Rows[j].ItemArray);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            dtThree.TableName = "Info";
            ds.Tables.Add(dtThree);
            BaseForm.PrintClass.PrintSelllist(ds, IsDesign);
        }
    }
}