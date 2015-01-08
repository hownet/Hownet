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
    public partial class frSell : DevExpress.XtraEditors.XtraForm
    {
        public frSell()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frSell(int MainID)
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
        DataTable dtSales = new DataTable();
        DataTable dtSizeList = new DataTable();
        DataTable dtSalesList = new DataTable();
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
        ArrayList ColorList = new ArrayList();
        ArrayList SizeList = new ArrayList();
        ArrayList ColorOneList = new ArrayList();
        ArrayList ColorTwoList = new ArrayList();
        ArrayList SizeNameList = new ArrayList();
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            //  this.Text = "销售开单";
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            ShowData();
            if (_MainID == 0)
            {

                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                InData();
                bs.Position = dtMain.Rows.Count - 1;
                simpleButton1.Visible = false;
                simpleButton5.Visible = false;
                if (bs.Position == 0)
                    ShowView(0);

            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                bar1.Visible = false;
                simpleButton1.Visible = true;
            }
            dtSizeList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSize, "GetAllList", null).Tables[0];
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
            {
                _brSave.Enabled =  false;
                _barExAmount.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            if (bs.Position > -1)
                ShowView(bs.Position);
        }
        void ShowData()
        {
            dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
            _reMateriel.DataSource = dtMat.DefaultView;
            _cosBrandID.ColumnEdit = _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _cosMaterielID.ColumnEdit = _reMateriel;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
         //   _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _leDepotID.Par = new object[] { "TypeID=42" };
            _coMeasureID.ColumnEdit = _coBoxMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;

            _leDepotID.FormName = (int)BasicClass.Enums.TableType.Deparment;

            if (BasicClass.BasicFile.liST[0].Sell4Depot)
                _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _coMTID.ColumnEdit = BaseForm.RepositoryItem._reMTID(4);
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
           // this._leCompany.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                last = decimal.Parse(dtPS.Rows[0]["LastMoney"].ToString());
                back = decimal.Parse(dtPS.Rows[0]["BackMoney"].ToString());
                money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
                lastDate = Convert.ToDateTime(dtPS.Rows[0]["LastDate"]).ToString("yyyy年MM月dd日");
               // _leCompany.IsNotCanEdit = true;
                _leDepotID.IsNotCanEdit = true;
            }
            else
            {
                dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtPS.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["Depot"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, 0 });
                dr["LastMoney"] = last = 0;
                dr["BackMoney"] = back = 0;
                dr["BackDate"] = DateTime.Parse("1900-1-1");
                money = 0;
                dtPS.Rows.Add(dr);
                _brAddNew.Enabled = false;
              //  _leCompany.IsNotCanEdit = false;
                _leDepotID.IsNotCanEdit = false;
            }
            _upData = int.Parse(dtPS.Rows[0]["UpData"].ToString());

            _IsVerify = Convert.ToInt32(dtPS.Rows[0]["IsVerify"]);
            t = _IsVerify == 3;
            _ldDate.val = dtPS.Rows[0]["DateTime"];
             _companyID = int.Parse(dtPS.Rows[0]["CompanyID"].ToString());//_leCompany.editVal =
             ucGridLookup1.Values = _companyID;
            _leDepotID.editVal = _depotID = int.Parse(dtPS.Rows[0]["Depot"].ToString());
            money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
            _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
            _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
            dtPSO = BasicClass.GetDataSet.GetDS(blPSO, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            dtInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            if (!BasicClass.BasicFile.liST[0].Sell4Depot && !t)
            {
                DataRow dr = dtPSO.NewRow();
                for (int i = 0; i < dtPSO.Columns.Count; i++)
                {
                    dr[i] = 0;
                }
                dr["Remark"] = string.Empty;
                dr["ID"] = 0;
                dr["MainID"] = _MainID;
                dr["A"] = dr["Conversion"] = 3;
                dtPSO.Rows.Add(dr.ItemArray);
                dtPSO.Rows.Add(dr.ItemArray);
            }
            dtPSO.DefaultView.RowFilter = "(A<4)";
            gridControl1.DataSource = dtPSO.DefaultView;
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = !t;
            _brAddNew.Enabled = (t && p == dtMain.Rows.Count - 1);
            _barUnVierfy.Enabled = t;
            if ((DateTime)(dtPS.Rows[0]["BackDate"]) == DateTime.Parse("1900-1-1"))
                backDate = "      年    月    日";
            else
                backDate = ((DateTime)(dtPS.Rows[0]["BackDate"])).ToString("yyyy年MM月dd日");
            SetColumnsReadOnly();
            ShowLoan();
           // this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            ShowSalesList();
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
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
            if (_IsVerify == 2)
            {
                _barExAmount.Caption = "退仓";
            }
            else
            {
                _barExAmount.Caption = "出仓";
            }
            _barExAmount.Enabled = _IsVerify < 3;
         
            ucGridLookup1.IsReadOnly = t;
        }
        private void SetColumnsReadOnly()
        {
            for (int i = 0; i < bandedGridView1.Columns.Count; i++)
            {
                if (bandedGridView1.Columns[i].Visible && bandedGridView1.Columns[i] != _coAmount)
                {
                    bandedGridView1.Columns[i].OptionsColumn.AllowEdit = !t;
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
            bandedGridView1.CloseEditor();
            bandedGridView1.UpdateCurrentRow();
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
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(),_companyID });
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
                int a = int.Parse(dtPSO.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    if (Convert.ToInt32(dtPSO.Rows[i]["Amount"]) > 0)
                    {
                        dtPSO.Rows[i]["MainID"] = _MainID;
                        dtt.Clear();
                        dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                        if (a == 3)//新增
                        {
                            dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blPSO, dtt);
                            dtPSO.Rows[i]["A"] = 1;
                        }
                        else if (a == 2)//修改
                        {
                            BasicClass.GetDataSet.UpData(blPSO, dtt);
                            dtPSO.Rows[i]["A"] = 1;
                        }
                        else if (a == 4)//删除
                        {
                            if (Convert.ToInt32(dtPSO.Rows[i]["ID"]) > 0)
                            {
                                BasicClass.GetDataSet.GetDS(blPSO, "Delete", new object[] { Convert.ToInt32(dtPSO.Rows[i]["ID"]) });
                                int _brandID = Convert.ToInt32(dtPSO.Rows[i]["BrandID"]);
                                int _materielID = Convert.ToInt32(dtPSO.Rows[i]["MaterielID"]);
                                int _salesID = Convert.ToInt32(dtPSO.Rows[i]["SalesOrderInfoID"]);
                                DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ") And (SalesInfoID=" + _salesID + ")");
                                foreach (DataRow drr in drs)
                                {
                                    drr["A"] = 4;
                                }
                            }
                            dtPSO.Rows[i]["A"] = 5;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dtPSO.Rows[i]["ID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(blPSO, "Delete", new object[] { Convert.ToInt32(dtPSO.Rows[i]["ID"]) });
                            dtPSO.Rows[i]["A"] = 5;
                        }
                        int _brandID = Convert.ToInt32(dtPSO.Rows[i]["BrandID"]);
                        int _materielID = Convert.ToInt32(dtPSO.Rows[i]["MaterielID"]);
                        int _salesID = Convert.ToInt32(dtPSO.Rows[i]["SalesOrderInfoID"]);
                        DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ") And (SalesInfoID=" + _salesID + ")");
                        foreach (DataRow drr in drs)
                        {
                            drr["A"] = 4;
                        }
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
                    ttt.Clear();
                    ttt.Rows.Add(dtInfo.Rows[i].ItemArray);
                    if (a == 3)
                    {
                        dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllProduceSellInfo, ttt);
                        dtInfo.Rows[i]["A"] = 1;
                    }
                    else if (a == 2)
                    {
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProduceSellInfo, ttt);
                        dtInfo.Rows[i]["A"] = 1;
                    }
                    else if (a == 4)
                    {
                        if (Convert.ToInt32(dtInfo.Rows[i]["ID"]) > 0)
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSellInfo, "Delete", new object[] { Convert.ToInt32(dtInfo.Rows[i]["ID"]) });
                        dtInfo.Rows[i]["A"] = 5;
                    }
                }
            }
            ShowLoan();
           // _leCompany.IsNotCanEdit = true;
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
            if (e.Value == null || e.Value == DBNull.Value)
                return;
            if ((e.Column == _coMaterielID || e.Column == _coBrandID) && !BasicClass.BasicFile.liST[0].Sell4Depot)
            {
                if (e.Column == _coMaterielID)
                {
                    object obj = BaseForm.RepositoryItem._reProduce.GetDataSourceValue("MeasureID", BaseForm.RepositoryItem._reProduce.GetDataSourceRowIndex("ID", e.Value));
                    bandedGridView1.SetFocusedRowCellValue(_coMeasureID, obj);
                    obj = BaseForm.RepositoryItem._reProduce.GetDataSourceValue("TypeID", BaseForm.RepositoryItem._reProduce.GetDataSourceRowIndex("ID", e.Value));
                    bandedGridView1.SetFocusedRowCellValue(_coMTID, obj);

                }
                SetPrice(e.RowHandle);
            }
            if (e.RowHandle > -1)
            {
                if (e.Column != _coA && bandedGridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    bandedGridView1.SetFocusedRowCellValue(_coA, 2);
            }

            try
            {
                if (BasicClass.BasicFile.liST[0].BoxOrPic)
                {
                    if (e.Column == _coAmount || e.Column == _coConversion)
                    {
                        int amount = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coAmount).ToString());
                        int box = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coConversion).ToString());
                        int b = (int)(amount / box);
                        bandedGridView1.SetFocusedRowCellValue(_coBoxMeasureAmount, b);
                    }
                }
                else
                {
                    if (e.Column == _coBoxMeasureAmount || e.Column == _coConversion)
                    {
                        int boxamount = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coBoxMeasureAmount).ToString());
                        int box = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coConversion).ToString());
                        int b = (int)(boxamount * box);
                        bandedGridView1.SetFocusedRowCellValue(_coAmount, b);
                    }
                }
            }
            catch { }
            try
            {
                if (e.Column == _coPrice || e.Column == _coAmount)
                {
                    price = decimal.Parse(bandedGridView1.GetFocusedRowCellValue(_coPrice).ToString());
                    amount = decimal.Parse(bandedGridView1.GetFocusedRowCellValue(_coAmount).ToString());
                    bandedGridView1.SetFocusedRowCellValue(_coMoney, (price * amount).ToString("n3"));
                    SumMoney();
                    ShowLoan();
                }
            }
            catch { }
            if (e.RowHandle == bandedGridView1.RowCount - 2 && !BasicClass.BasicFile.liST[0].Sell4Depot && !t)
            {
                DataRow dr = dtPSO.NewRow();
                for (int i = 0; i < dtPSO.Columns.Count; i++)
                {
                    dr[i] = 0;
                }
                dr["Remark"] = string.Empty;
                dr["ID"] = 0;
                dr["MainID"] = _MainID;
                dr["A"] = dr["Conversion"] = 3;
                dtPSO.Rows.Add(dr);
            }

        }
        private void SetPrice(int rowID)
        {
            try
            {
                int brand = int.Parse(bandedGridView1.GetRowCellValue(rowID, _coBrandID).ToString());
                int mater = int.Parse(bandedGridView1.GetRowCellValue(rowID, _coMaterielID).ToString());
                int measureid = Convert.ToInt32(bandedGridView1.GetRowCellValue(rowID, _coMeasureID));
                int mtid = Convert.ToInt32(bandedGridView1.GetRowCellValue(rowID, _coMTID));
                object[] o = new object[] { _companyID, mater, brand, measureid, mtid };
                bandedGridView1.SetRowCellValue(rowID, _coPrice, BasicClass.GetDataSet.GetOne(blQP, "GetPrice", o));
            }
            catch (Exception ex)
            {
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (t)
                return;
            if (BasicClass.BasicFile.liST[0].Sell4Depot)
            {
                if (bandedGridView1.RowCount > 0)
                {
                    if (e.FocusedRowHandle < 0)
                        bandedGridView1.AddNewRow();
                    _oldMat = bandedGridView1.GetFocusedRowCellValue(_coMaterielID);
                    _oldBrand = bandedGridView1.GetFocusedRowCellValue(_coBrandID);
                }
                _coMaterielID.OptionsColumn.AllowEdit = _coBrandID.OptionsColumn.AllowEdit = e.FocusedRowHandle < 0;
            }
            else
            {
                bandedGridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < (bandedGridView1.RowCount - 1));
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (bandedGridView1.RowCount > 0)
            {
                //int r = bandedGridView1.RowCount - 1;
                //object o = bandedGridView1.GetRowCellValue(r, _coMaterielID);
                //if (o == null || o.ToString().Trim() == "" || o.ToString().Trim() == "0")
                //{
                //    bandedGridView1.DeleteRow(r);
                //}
                //else
                //{
                SumMoney();
                ShowLoan();
                //}
                //if (bandedGridView1.RowCount == 8)
                //    bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                //else
                //    bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            }
        }
        private void SumMoney()
        {
            money = 0;
            for (int i = 0; i < bandedGridView1.RowCount; i++)
            {
                try
                {
                    money += decimal.Parse(bandedGridView1.GetRowCellValue(i, _coMoney).ToString());
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
            ShowSalesList();
        }
        private void ShowSalesList()
        {
            if (!t)
            {
                dtSalesList.Rows.Clear();
                dtSalesList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetSales", new object[] { _companyID, 0, 0, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                gridControl2.DataSource = dtSalesList;
                if (gridView1.FocusedRowHandle > -1)
                    SetSellInfo();
            }
        }
        private void ShowLoan()
        {
            string ssss = "客户：  " +ucGridLookup1.StringValues  + " 于" + lastDate + "欠货款： " + last.ToString("N2") + "元，";//_leCompany.valStr

            // string ssss = "客户：  " + _leCompany.valStr + "  此前欠货款： " + last.ToString("N2") + "元，";
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
                    if (dtMain.Rows.Count > 1)
                    {
                        dtMain.Rows.RemoveAt(bs.Position);
                        _brAddNew.Enabled = (t && bs.Position == dtMain.Rows.Count - 1);
                    }
                    else
                    {
                        InData();
                        ShowView(0);
                    }
                    //if (dtMain.Rows.Count > 0)
                    //    bs.Position = dtMain.Rows.Count - 1;
                    //else
                    //    ShowView(0);
                }
            }
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bandedGridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == MessageBox.Show("是否确认删除该条记录？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    int _materielID = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                    int _brandID = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coBrandID).ToString());
                    DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i].Delete();
                        }
                    }
                    dtInfo.AcceptChanges();
                    int id = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (id > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blPSO, "Delete", new object[] { id });
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSellInfo, "DeleteInfo", new object[] { _MainID, _materielID, _brandID });
                    }
                    bandedGridView1.DeleteRow(bandedGridView1.FocusedRowHandle);
                    dtPSO.AcceptChanges();
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _count = bandedGridView1.RowCount;
            if (!BasicClass.BasicFile.liST[0].Sell4Depot)
            {
                _count = bandedGridView1.RowCount - 2;
            }
            if (_count == 0)
                return;
            for (int i = 0; i < _count; i++)
            {
                if (decimal.Parse(bandedGridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
                {
                    XtraMessageBox.Show("有明细记录没有金额！");
                    return;
                }
            }
            if (!Save())
                return;
            if (_IsVerify==1&& !BasicClass.BasicFile.liST[0].DepotAllowNegative)
            {
                if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduceSellInfo, "CheckSellAmount", new object[] { _MainID, _depotID })) > 0)
                {
                    XtraMessageBox.Show("有出库数量超过库存数量，请检查后再审核!");
                    return;
                }
            }
            
            dtPS.Rows[0]["IsVerify"] = 3;
            dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtPS.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
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
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllCompany, "UpLastMoney", new object[] { _companyID, (last + money - back) });
            dtPSO.DefaultView.RowFilter = "A<4";
            for (int i = 0; i < _count; i++)
            {
                object[] o = new object[] { _companyID, Convert.ToInt32(dtPSO.DefaultView[i]["MaterielID"]), Convert.ToInt32(dtPSO.DefaultView[i]["BrandID"]), Convert.ToDecimal(dtPSO.DefaultView[i]["Price"]), Convert.ToInt32(dtPSO.DefaultView[i]["MeasureID"]), Convert.ToInt32(dtPSO.DefaultView[i]["MTID"]) };
                BasicClass.GetDataSet.ExecSql(blQP, "UpPrice", o);
            }
            if (BasicClass.BasicFile.liST[0].Sell4Depot&&_IsVerify<3)
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, true, _depotID });
            ShowView(bs.Position);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.Sell), _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本单之后已有后续业务发生，不能弃审！");
                return;
            }
            dtPS.Rows[0]["IsVerify"] = 2;
            dtPS.Rows[0]["VerifyMan"] = 0;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            BasicClass.GetDataSet.UpData(bllPS, dtPS);
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.Sell), _MainID });
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, false, _depotID });
                        
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllCompany, "UpLastMoney", new object[] { _companyID, (last - back) });
            ShowView(bs.Position);
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object o = bandedGridView1.GetFocusedRowCellValue(_coMaterielID);
            if (o != null && o.ToString() != "0")
            {
                int _materielID = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                int _brandID = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coBrandID).ToString());
                DataTable ddt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetSumAmount", new object[] { _MainID }).Tables[0];

                DataTable dtt = ddt.Clone();
                for (int i = 0; i < ddt.Rows.Count; i++)
                {
                    object br = ddt.Rows[i]["BrandID"];
                    object ma = ddt.Rows[i]["MaterielID"];
                    if (Convert.ToInt32(ddt.Rows[i]["BrandID"]) == Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coBrandID)) &&
                         //Convert.ToInt32(dtInfo.Rows[i]["SelesID"]) == Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coSalesOrderInfoID)) &&
                        Convert.ToInt32(ddt.Rows[i]["MaterielID"]) == Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coMaterielID)))
                    {
                        dtt.Rows.Add(ddt.Rows[i].ItemArray);
                    }
                }
                //cResult r = new cResult();
                int _salesID = Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coSalesOrderInfoID));

                //r.RowChanged += new RowChangedHandler(r_RowChanged);
                object oo = bandedGridView1.GetFocusedRowCellValue(_coSalesOrderInfoID);
                if (t)
                {
                    Form fr = new Form1(dtt);
                    fr.ShowDialog();
                }
                //else
                //{
                //    if (!BasicClass.BasicFile.liST[0].IsChangedSales)
                //    {
                //        Form fr = new Sell.SellTemForm(r, !t, dtt, _materielID, _brandID, _depotID);
                //        fr.ShowDialog();
                //    }
                //    else
                //    {
                //        Form fr = new Sell.SellTemForm2(r, !t, dtt, _materielID, _brandID, _depotID, _companyID, _salesID);
                //        fr.ShowDialog();
                //    }
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
            if (dt.Rows.Count > 1)
            {
                int _SalesID = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]);
                int _brandID = Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coBrandID));
                int _materielID = Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coMaterielID));
                if (_SalesID > 0)
                {
                    for (int i = 0; i < bandedGridView1.RowCount; i++)
                    {
                        if (i != bandedGridView1.FocusedRowHandle)
                        {
                            if (Convert.ToInt32(bandedGridView1.GetRowCellValue(i, _coMaterielID)) == _materielID &&
                               Convert.ToInt32(bandedGridView1.GetRowCellValue(i, _coBrandID)).Equals(_brandID) &&
                                Convert.ToInt32(bandedGridView1.GetRowCellValue(i, _coSalesOrderInfoID)).Equals(_SalesID))
                            {
                                bandedGridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);
                                XtraMessageBox.Show("同一销售单中，同种商标的同一款号或同一客户订单只能有一条记录！");
                                return;
                            }
                        }
                    }
                }
                DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ") And (SelesID=" + _SalesID + ")");
                foreach (DataRow drr in drs)
                {
                    drr["A"] = 4;
                }
                dtInfo.AcceptChanges();
                int aaaa = 0;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    dt.Rows[i]["MeasureID"] = bandedGridView1.GetFocusedRowCellValue(_coMeasureID);
                    dtInfo.Rows.Add(dt.Rows[i].ItemArray);
                    if (dtInfo.Rows[i]["SelesID"].ToString() == "")
                        dtInfo.Rows[i]["SelesID"] = 0;
                    aaaa += Convert.ToInt32(dt.Rows[i]["Amount"]);
                }
                object ooo = bandedGridView1.GetFocusedRowCellValue(_coMeasureID);
                bandedGridView1.SetFocusedRowCellValue(_coAmount, aaaa);
                bandedGridView1.SetFocusedRowCellValue(_coSalesOrderInfoID, dt.Rows[dt.Rows.Count - 1][0]);
            }

        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTable(0);
        }
        private void PrintTable(int  A4A5)
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
            ds.Tables["Main"].Columns.Add("审核", typeof(string));
            ds.Tables["Main"].Columns.Add("制单", typeof(string));
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
                ds.Tables["Main"].Rows[0]["审核"] = dtUser.Select("(ID=" + Convert.ToInt32(dtPS.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            try
            {
                ds.Tables["Main"].Rows[0]["制单"] = dtUser.Select("(ID=" + Convert.ToInt32(dtPS.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
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
            dt.Columns.Add("MTName", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("LingShouJia", typeof(string));
            int amount = 0;
            int boxAmount = 0;
            decimal moeny = 0;
            for (int i = 0; i < bandedGridView1.RowCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = bandedGridView1.GetRowCellDisplayText(i, _coMaterielID);
                dr[1] = bandedGridView1.GetRowCellDisplayText(i, _coBrandID);
                dr[2] = bandedGridView1.GetRowCellDisplayText(i, _coAmount);
                dr[3] = Convert.ToDecimal(bandedGridView1.GetRowCellValue(i, _coPrice));//.ToString("c2")
                dr[4] = bandedGridView1.GetRowCellDisplayText(i, _coConversion);
                dr[5] = bandedGridView1.GetRowCellDisplayText(i, _coBoxMeasureAmount);
                dr[6] = Convert.ToDecimal(bandedGridView1.GetRowCellValue(i, _coMoney));//.ToString("c2")
                dr[7] = bandedGridView1.GetRowCellDisplayText(i, _coRemark);
                dr[8] = bandedGridView1.GetRowCellDisplayText(i, _coMTID);
                dr[9] = bandedGridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[10] = bandedGridView1.GetRowCellValue(i, _coSellRemark);
                dt.Rows.Add(dr);
                amount += int.Parse(bandedGridView1.GetRowCellValue(i, _coAmount).ToString());
                boxAmount += int.Parse(bandedGridView1.GetRowCellValue(i, _coBoxMeasureAmount).ToString());
                moeny += decimal.Parse(bandedGridView1.GetRowCellValue(i, _coMoney).ToString());
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
                } if (A4A5>0)
                {
                    for (int i = dt.Rows.Count; i < 8; i++)
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
            BaseForm.PrintClass.PrintSellTable(ds,A4A5);

        }
        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("请审核后再打印单据！");
                return;
            }
            PrintInfo();
        }
        private void PrintInfo()
        {
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
            PrintTable(2);
        }

        private void _leDepotID_EditValueChanged(object val, string text)
        {
            if (_MainID == 0)
            {
                _depotID = int.Parse(val.ToString());
                ShowSalesList();
            }
        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                advBandedGridView2.Columns.Clear();
                advBandedGridView2.Bands.Clear();
                return;
            }
            SetSellInfo();
        }
        private void SetSellInfo()
        {
            int _materielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosMaterielID));
            int _brandID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosBrandID));
            int SalesID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosID));
            gridControl3.DataSource = null;
            advBandedGridView2.Columns.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("Color", typeof(string));
            DataTable dtRep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(DepartmentID=" + _depotID + ") And (MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ") And (Amount>-1) " }).Tables[0];
            dtSales = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetList", new object[] { "(MainID=" + SalesID + ") And (TableTypeID=" + (int)BasicClass.Enums.TableType.SalesOne + ") And (NotAmount>-1) " }).Tables[0];

            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["Color"] = "颜色\\尺码";
            DataTable dtSize = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetSize", new object[] { _materielID, _brandID }).Tables[0];

            int i = 1;

            ColorList.Clear();
            ColorOneList.Clear();
            ColorTwoList.Clear();
            SizeNameList.Clear();
            SizeList.Clear();
            ColorList.Add(0);
            SizeList.Add(0);
            ColorOneList.Add(0);
            ColorTwoList.Add(0);
            advBandedGridView2.Columns.Clear();
            advBandedGridView2.Bands.Clear();
            advBandedGridView2.OptionsView.ShowColumnHeaders = false;
            for (int s = 0; s < dtSize.Rows.Count; s++)
            {
                int m = 0;
                if (i == 1)
                    m = 1;
                else
                    m = i * 3 - 1;
                dt.Columns.Add("KuCun" + i);
                dt.Columns.Add("Sales" + i);
                dt.Columns.Add("FaHuo" + i);
                dt.Rows[0][m] = dtSize.Rows[s][0].ToString();
                SizeList.Add(int.Parse(dtSize.Rows[s][1].ToString()));
                SizeNameList.Add(dtSize.Rows[s][0].ToString());
                i++;
            }

            dt.Columns.Add("SumNum", typeof(string));
            dt.Rows[0]["SumNum"] = "库存总数";
            dt.Columns.Add("SumSales", typeof(string));
            dt.Rows[0]["SumSales"] = "订单总数";
            dt.Columns.Add("GoSum", typeof(string));
            dt.Rows[0]["GoSum"] = "出库总数";
            dt.Columns.Add("ColorOne", typeof(string));
            dt.Columns.Add("ColorTwo", typeof(string));
            dt.Rows[0]["ColorOne"] = "插色一";
            dt.Rows[0]["ColorTwo"] = "插色二";
            for (int j = 0; j < dt.Columns.Count - 5; j++)
            {
                advBandedGridView2.Bands.Add();
                advBandedGridView2.Columns.Add();
                advBandedGridView2.Columns[j].FieldName = dt.Columns[j].ColumnName;
                advBandedGridView2.Columns[j].Caption = dt.Columns[j].ColumnName;
                advBandedGridView2.Bands[j].Columns.Add((advBandedGridView2.Columns[j]) as BandedGridColumn);
                if (j > 0)
                {
                    int zzzzzzzz = j % 3;
                    if ((j % 3) == 0)
                    {
                        advBandedGridView2.Bands[j].Caption = "发货量";
                    }
                    else if ((j % 3) == 1)
                    {
                        advBandedGridView2.Bands[j].Caption = "库存数";
                        advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                    }
                    else if ((j % 3) == 2)
                    {
                        advBandedGridView2.Bands[j].Caption = "订单占用";
                        advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                    }
                }
                else
                {
                    advBandedGridView2.Bands[j].Caption = "颜色";
                    advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                }

                advBandedGridView2.Bands[j].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                advBandedGridView2.Columns[j].Visible = true;
                advBandedGridView2.Columns[j].Width = 60;
            }
            int bCount = advBandedGridView2.Bands.Count;
            for (int j = 0; j < SizeNameList.Count; j++)
            {
                advBandedGridView2.Bands.Add();
                advBandedGridView2.Bands[bCount + j].Caption = SizeNameList[j].ToString();

                advBandedGridView2.Bands[bCount + j].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            for (int j = 0; j < SizeNameList.Count; j++)
            {
                try
                {
                    advBandedGridView2.Bands[bCount - j].Children.AddRange(new GridBand[] { advBandedGridView2.Bands[1], advBandedGridView2.Bands[2], advBandedGridView2.Bands[3] });
                    bCount -= 1;
                }
                catch { }
            }
            bCount = advBandedGridView2.Bands.Count;
            i = 0;
            for (int j = dt.Columns.Count - 5; j < dt.Columns.Count; j++)
            {
                advBandedGridView2.Bands.Add();
                advBandedGridView2.Columns.Add();
                advBandedGridView2.Columns[j].FieldName = dt.Columns[j].ColumnName;
                advBandedGridView2.Bands[bCount + i].Caption = dt.DefaultView[0][j].ToString();
                advBandedGridView2.Bands[bCount + i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                advBandedGridView2.Bands[bCount + i].Columns.Add((advBandedGridView2.Columns[j]) as BandedGridColumn);
                advBandedGridView2.Columns[j].Visible = true;
                advBandedGridView2.Columns[j].Width = 60;
                advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                i++;
            }
            advBandedGridView2.Bands.Add();
            advBandedGridView2.Bands[advBandedGridView2.Bands.Count - 1].Caption = "合计";
            advBandedGridView2.Bands[advBandedGridView2.Bands.Count - 1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            advBandedGridView2.Bands[advBandedGridView2.Bands.Count - 1].Children.AddRange(new GridBand[] { advBandedGridView2.Bands[bCount], advBandedGridView2.Bands[bCount + 1], advBandedGridView2.Bands[bCount + 2] });
            DataTable dtcolor = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetColor", new object[] { _materielID, _brandID, _depotID }).Tables[0];
            i = 1;
            for (int c = 0; c < dtcolor.Rows.Count; c++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = dtcolor.Rows[c][0].ToString();
                dt.Rows[i][dt.Columns.Count - 5] = dtcolor.Rows[c][2].ToString();
                dt.Rows[i][dt.Columns.Count - 2] = dtcolor.Rows[c][3].ToString();
                dt.Rows[i][dt.Columns.Count - 1] = dtcolor.Rows[c][5].ToString();
                ColorList.Add(int.Parse(dtcolor.Rows[c][1].ToString()));
                ColorOneList.Add(int.Parse(dtcolor.Rows[c][4].ToString()));
                ColorTwoList.Add(int.Parse(dtcolor.Rows[c][6].ToString()));
                i++;
            }
            for (int r = 1; r < SizeList.Count; r++)
            {
                for (int c = 1; c < ColorList.Count; c++)
                {
                    try
                    {
                        int m = 0;
                        if (r == 1)
                            m = 1;
                        else
                            m = r * 3 - 2;
                        string sql = "(SizeID=" + SizeList[r] + ") and (ColorID=" + ColorList[c] + ") and (ColorOneID=" + ColorOneList[c] + ") and (ColorTwoID=" + ColorTwoList[c] + ") And (A<4)";
                        DataRow[] drs = dtRep.Select(sql);
                        if (drs.Length > 0)
                            dt.Rows[c][m] = Convert.ToInt32(drs[0]["Amount"]);
                        drs = dtInfo.Select("(SizeID=" + SizeList[r] + ") and (ColorID=" + ColorList[c] + ") and (ColorOneID=" + ColorOneList[c] + ") and (ColorTwoID=" + ColorTwoList[c] + ") And (A<4) And (SelesID=" + Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosID)) + ")");
                        if (drs.Length > 0)
                        {
                            dt.Rows[c][m + 2] =Convert.ToInt32( drs[0]["Amount"]);
                        }
                        drs = dtSales.Select(sql);
                        if (drs.Length > 0)
                            dt.Rows[c][m + 1] = Convert.ToInt32(drs[0]["NotAmount"]);
                    }
                    catch { }
                }
            }
            dt.Rows.RemoveAt(0);
            advBandedGridView2.Bands[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridControl3.DataSource = dt;
            for (int z = 0; z < advBandedGridView2.RowCount; z++)
            {
                if (advBandedGridView2.GetRowCellValue(z, advBandedGridView2.Columns[0]).ToString() != string.Empty)
                {
                    SumRow(i);
                }
            }
            SumAmount();
            for (int j = 0; j < advBandedGridView2.RowCount; j++)
            {
                int amount = 0;
                for (int k = 2; k < advBandedGridView2.Columns.Count - 5; k++)
                {
                    if ((k % 3) == 2)
                    {
                        if (advBandedGridView2.GetRowCellValue(j, advBandedGridView2.Columns[k]).ToString() != string.Empty)
                        {
                            amount = amount + int.Parse(advBandedGridView2.GetRowCellValue(j, advBandedGridView2.Columns[k]).ToString());
                        }
                    }
                }
                advBandedGridView2.SetRowCellValue(j, advBandedGridView2.Columns[advBandedGridView2.Columns.Count - 4], amount);
            }
        }
        private void SumRow(int rowHandle)
        {
            try
            {
                int amount = 0;
                for (int i = 2; i < advBandedGridView2.Columns.Count - 5; i++)
                {
                    if ((i % 3) == 0)
                    {
                        if (advBandedGridView2.GetRowCellValue(rowHandle, advBandedGridView2.Columns[i]).ToString() != string.Empty)
                        {
                            amount = amount + int.Parse(advBandedGridView2.GetRowCellValue(rowHandle, advBandedGridView2.Columns[i]).ToString());
                        }
                    }
                }
                advBandedGridView2.SetRowCellValue(rowHandle, advBandedGridView2.Columns[advBandedGridView2.Columns.Count - 3], amount);
            }
            catch { }
        }
        private void SumAmount()
        {
            try
            {
                int amount = 0;
                for (int i = 0; i < advBandedGridView2.RowCount; i++)
                {
                    if (advBandedGridView2.GetRowCellValue(i, "GoSum").ToString() != string.Empty)
                    {
                        amount = amount + int.Parse(advBandedGridView2.GetRowCellValue(i, "GoSum").ToString());
                    }
                }
            }
            catch { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)(gridControl3.DataSource);
            int colorID = 0;
            int colorOneID = 0;
            int colorTwoID = 0;
            int amount = 0;
            int SalesID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosID));
            DataRow[] drs = dtInfo.Select("(SelesID=" + SalesID + ")");
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i]["A"] = 4;
                }
                dtInfo.AcceptChanges();
            }
            drs = dtPSO.Select("(SalesOrderInfoID=" + SalesID + ")");
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i]["Amount"] = 0;
                }
                dtInfo.AcceptChanges();
            }
            for (int i = 1; i < ColorList.Count; i++)
            {
                colorID = int.Parse(ColorList[i].ToString());
                if (ColorOneList[i].ToString() != string.Empty)
                    colorOneID = int.Parse(ColorOneList[i].ToString());
                if (ColorTwoList[i].ToString() != string.Empty)
                    colorTwoID = int.Parse(ColorTwoList[i].ToString());
                for (int m = 1; m < SizeList.Count; m++)
                {
                    if (dt.DefaultView[i - 1][m * 3].ToString() != string.Empty)
                    {
                        string strAmount = dt.DefaultView[i - 1][m * 3].ToString();
                        amount = int.Parse(strAmount);
                        if (amount > 0)
                        {
                            DataRow dr = dtInfo.NewRow();
                            dr["ID"] = 0;
                            dr["MainID"] = 0;
                            dr["MaterielID"] = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosMaterielID));
                            dr["ColorID"] = colorID;
                            dr["ColorOneID"] = colorOneID;
                            dr["ColorTwoID"] = colorTwoID;
                            dr["SizeID"] = SizeList[m];
                            dr["Amount"] = amount;
                            dr["BrandID"] = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosBrandID));
                            dr["MListID"] = 0;
                            dr["SelesID"] = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosID));
                            dr["MeasureID"] = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosMeasureID));
                            drs = dtSales.Select("(SizeID=" + SizeList[m] + ") and (ColorID=" + colorID + ") and (ColorOneID=" + colorOneID + ") and (ColorTwoID=" + colorTwoID + ")");
                            if (drs.Length > 0)
                                dr["SalesInfoID"] = drs[0]["ID"];
                            else
                                dr["SalesInfoID"] = 0;
                            dr["A"] = 3;
                            dtInfo.Rows.Add(dr);
                        }
                    }
                }
            }
            drs = dtInfo.Select("(SelesID=" + Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosID)) + ") And (A<4)");
            amount = 0;
            bool f = false;
            int MTID = 0;
            int _MaterielID = 0;
            DataTable dtSalePrice = BasicClass.GetDataSet.GetDS("Hownet.BLL.SalesPrice", "GetList", new object[] { "(SalesID=" + SalesID + ")" }).Tables[0];
            for (int i = 0; i < drs.Length; i++)
            {
                f = false;
                MTID = Convert.ToInt32(dtSizeList.Select("(ID=" + Convert.ToInt32(drs[i]["SizeID"]) + ")")[0]["SizeTypeID"]);
                for (int j = 0; j < dtPSO.Rows.Count; j++)
                {
                    if (Convert.ToInt32(dtPSO.Rows[j]["MTID"]) == MTID && Convert.ToInt32(dtPSO.Rows[j]["SalesOrderInfoID"]) == SalesID)
                    {
                        f = true;
                        dtPSO.Rows[j]["Amount"] = Convert.ToInt32(dtPSO.Rows[j]["Amount"]) + Convert.ToInt32(drs[i]["Amount"]);
                        break;
                    }
                }
                if (!f)
                {
                    DataRow dr = dtPSO.NewRow();
                    dr["BoxMeasureID"] = dr["Money"] = dr["ID"] = 0;
                    dr["MaterielID"] = _MaterielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_cosMaterielID));
                    decimal ppp = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_cosPrice));
                    if (ppp > 0)
                    {
                        dr["Price"] = ppp;
                    }
                    else
                    {
                        try
                        {
                            dr["Price"] = dtSalePrice.Select("(MTID=" + MTID + ")")[0]["Price"];
                        }
                        catch
                        {
                            dr["Price"] = ppp;
                        }
                    }
                    dr["Amount"] = amount = Convert.ToInt32(drs[i]["Amount"]);
                    dr["Remark"] = gridView1.GetFocusedRowCellDisplayText(_cosRemark);
                    dr["MainID"] = _MainID;
                    dr["MeasureID"] = gridView1.GetFocusedRowCellValue(_cosMeasureID);
                    dr["Conversion"] = 3;
                    dr["BoxMeasureAmount"] = (int)(amount / 3);
                    dr["BrandID"] = gridView1.GetFocusedRowCellValue(_cosBrandID);
                    dr["SalesOrderInfoID"] = SalesID;
                    dr["RemarkID"] = 0;
                    dr["SellRemark"] = dtMat.Select("(ID=" + _MaterielID + ")")[0]["LingShouJia"];
                    dr["MTID"] = MTID;
                    dr["A"] = 3;
                    dtPSO.Rows.Add(dr);
                }
            }
            drs = dtPSO.Select("(SalesOrderInfoID=" + SalesID + ")");
            for (int i = 0; i < drs.Length; i++)
            {
                drs[i]["BoxMeasureAmount"] = (int)(Convert.ToInt32(drs[i]["Amount"]) / Convert.ToInt32(drs[i]["Conversion"]));
                drs[i]["Money"] = (Convert.ToDecimal(drs[i]["Amount"]) * Convert.ToDecimal(drs[i]["Price"])).ToString("n3");
                if (Convert.ToInt32(drs[i]["A"]) == 1)
                    drs[i]["A"] = 2;
            }
            SumMoney();
            ShowLoan();
        }

        private void advBandedGridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.ColumnHandle < advBandedGridView2.Columns.Count - 4)
            {
                int amount = 0;
                if (e.Value.ToString() != string.Empty)
                {
                    try
                    {
                        amount = int.Parse(e.Value.ToString());
                        int SaleAmount = 0;
                        int DepAmount = 0;
                        if (advBandedGridView2.GetRowCellValue(e.RowHandle, advBandedGridView2.Columns[e.Column.ColumnHandle - 2]) != DBNull.Value)
                            DepAmount = Convert.ToInt32(advBandedGridView2.GetRowCellValue(e.RowHandle, advBandedGridView2.Columns[e.Column.ColumnHandle - 2]));
                        if (advBandedGridView2.GetFocusedRowCellValue(advBandedGridView2.Columns[e.Column.ColumnHandle - 1]) != DBNull.Value)
                            SaleAmount = Convert.ToInt32(advBandedGridView2.GetFocusedRowCellValue(advBandedGridView2.Columns[e.Column.ColumnHandle - 1]));
                        if (amount > DepAmount)
                        {
                            if (!BasicClass.BasicFile.liST[0].DepotAllowNegative)
                            {
                                XtraMessageBox.Show("出库数量超过库存总数！");
                                advBandedGridView2.SetRowCellValue(e.RowHandle, e.Column, DepAmount);
                                return;
                            }
                        }
                        if (amount > SaleAmount)
                        {
                            if (DialogResult.Yes == XtraMessageBox.Show("超过客户订单数量，是否取消？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                            {
                                advBandedGridView2.SetFocusedRowCellValue(e.Column, DBNull.Value);
                                return;
                            }
                            else if (amount > DepAmount)
                            {
                                if (!BasicClass.BasicFile.liST[0].DepotAllowNegative)
                                {
                                    XtraMessageBox.Show("出库数量超过库存总数！");
                                    advBandedGridView2.SetRowCellValue(e.RowHandle, e.Column, DepAmount);
                                    return;
                                }
                            }
                        }
                        SumRow(e.RowHandle);
                        SumAmount();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("只能填整数！");
                        advBandedGridView2.SetRowCellValue(e.RowHandle, e.Column, string.Empty);
                    }
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void bandedGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!t && e.Button == MouseButtons.Right && BasicClass.BasicFile.liST[0].Sell4Depot)
                DoShowMenu(bandedGridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void gridView1_RowCountChanged_1(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0 && gridView1.FocusedRowHandle == 0)
            {
                SetSellInfo();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
            object o;
            object h;

            for (int i = 0; i < advBandedGridView2.VisibleColumns.Count; i++)
            {
                if (advBandedGridView2.VisibleColumns[i].FieldName.IndexOf("FaHuo")>-1)
                {
                    for (int j = 0; j < advBandedGridView2.RowCount; j++)
                    {
                        o=advBandedGridView2.GetRowCellValue(j, advBandedGridView2.VisibleColumns[i - 1]);
                        
                        if (o.ToString() != string.Empty)
                        {
                            h = advBandedGridView2.GetRowCellValue(j, advBandedGridView2.VisibleColumns[i - 2]);
                            if (h.ToString() != string.Empty)
                            {
                                if (Convert.ToInt32(h) < Convert.ToInt32(o))
                                {
                                    advBandedGridView2.SetRowCellValue(j, advBandedGridView2.VisibleColumns[i], Convert.ToInt32(h));
                                }
                                else
                                {
                                    advBandedGridView2.SetRowCellValue(j, advBandedGridView2.VisibleColumns[i], Convert.ToInt32(o));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTable(1);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            PrintInfo();
        }

        private void _barExAmount_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_IsVerify == 1)
            {
                if (DialogResult.No == XtraMessageBox.Show("是否确认做出仓处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
                if (!Save())
                    return;
                if (!BasicClass.BasicFile.liST[0].DepotAllowNegative)
                {
                    if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduceSellInfo, "CheckSellAmount", new object[] { _MainID, _depotID })) > 0)
                    {
                        XtraMessageBox.Show("有出库数量超过库存数量，请检查后再审核!");
                        return;
                    }
                }
                dtPS.Rows[0]["IsVerify"] = 2;
                dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
                dtPS.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
                BasicClass.GetDataSet.UpData(bllPS, dtPS);
                if (BasicClass.BasicFile.liST[0].Sell4Depot)
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, true, _depotID });
                ShowView(bs.Position);
            }
            else if (_IsVerify == 2)
            {
                if (DialogResult.No == XtraMessageBox.Show("是否确认做退仓处理？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
                //if (!Save())
                //    return;
                if (!BasicClass.BasicFile.liST[0].DepotAllowNegative)
                {
                    if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduceSellInfo, "CheckSellAmount", new object[] { _MainID, _depotID })) > 0)
                    {
                        XtraMessageBox.Show("有出库数量超过库存数量，请检查后再审核!");
                        return;
                    }
                }
                dtPS.Rows[0]["IsVerify"] = 1;
                dtPS.Rows[0]["VerifyMan"] = 0;
                dtPS.Rows[0]["VerifyDate"] = Convert.ToDateTime("1900-1-1");
                BasicClass.GetDataSet.UpData(bllPS, dtPS);
                if (BasicClass.BasicFile.liST[0].Sell4Depot)
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, false, _depotID });
                ShowView(bs.Position);
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
            ShowSalesList();
        }


    }
}