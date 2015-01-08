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
    public partial class frSellBack : DevExpress.XtraEditors.XtraForm
    {
        public frSellBack()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frSellBack(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        string blMIOO = "Hownet.BLL.MoneyInOrOut";
        int _companyID = 0;
        int _depotID = 0;
        int _upData = 0;
        int _IsVerify = 0;
        BindingSource bs = new BindingSource();
        private string bllPS = BasicClass.Bllstr.bllProduceSell;
        private string blPSO = BasicClass.Bllstr.bllProduceSellOne;
        private string blCL = BasicClass.Bllstr.bllCompanyLog;
        private string blQP = BasicClass.Bllstr.bllQuotePrice;
        DataTable dtPS= new DataTable();
        DataTable dtPSO = new DataTable();
        DataTable dtInfo = new DataTable();
        bool t = false;
        decimal price = 0;
        decimal money = 0;
        decimal amount = 0;
        decimal last = 0;
        decimal back = 0;
        object _oldMat = null;
        object _oldBrand = null;
         int _materielID =0;
         int _brandID = 0;
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
                bar1.Visible = false;
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
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVierfy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _leDepotID.FormName = (int)BasicClass.Enums.TableType.Deparment;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
            _leCompany.FormName =(int) BasicClass.Enums.TableType.Company;
            if (BasicClass.BasicFile.liST[0].Sell4Depot)
                _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
         }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllPS, "GetIDList", new object[] { (int)Enums.TableType.SellBack }).Tables[0];
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
             if (dtMain.DefaultView[p]["ID"].ToString() != "")
             {
                 _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                 dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                 last = decimal.Parse(dtPS.Rows[0]["LastMoney"].ToString());
                 back = decimal.Parse(dtPS.Rows[0]["BackMoney"].ToString());
                 money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
                 _leCompany.IsNotCanEdit = true;
             }
             else
             {
                 dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtPS.NewRow();
                 dr["CompanyID"] = dr["ID"] = dr["Depot"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                 dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                 dr["FillMan"] = BasicClass.UserInfo.UserID;
                 dr["IsVerify"] =1;
                  dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                 dr["Remark"] = "";
                 dr["Num"] =  BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(),0 });
                 dr["LastMoney"] = last = 0;
                 dr["BackMoney"] = back = 0;
                 money = 0;
                 dtPS.Rows.Add(dr);
                 _brAddNew.Enabled = false;
                 _leCompany.IsNotCanEdit = false;
             }
             _upData = int.Parse(dtPS.Rows[0]["UpData"].ToString());
             _IsVerify = Convert.ToInt32(dtPS.Rows[0]["IsVerify"]);
             t = _IsVerify == 3;
             _ldDate.val = dtPS.Rows[0]["DateTime"];
             _leCompany.editVal = _companyID = int.Parse(dtPS.Rows[0]["CompanyID"].ToString());
             _leDepotID.editVal = _depotID = int.Parse(dtPS.Rows[0]["Depot"].ToString());
             money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
             _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
             _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
             dtPSO = BasicClass.GetDataSet.GetDS(blPSO, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
             dtInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
             gridControl1.DataSource = dtPSO;
             //_leCompany.IsCanEdit = !t;
             _barVerify.Enabled = _brSave.Enabled =  _barDel.Enabled = !t;
             _brAddNew.Enabled = _barUnVierfy.Enabled = (t && p == dtMain.Rows.Count - 1);
             SetColumnsReadOnly();
             if (t)
                 bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            else
                 bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
             ShowLoan();
             this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
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
           if( Save())
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
                XtraMessageBox.Show("请选择仓库！");
                return false;
            }
            DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
            if (dttt.Rows.Count > 0)
                last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
            else
                last = 0;
            dtPS.Rows[0]["LastMoney"] = last;
            dtPS.Rows[0]["BackMoney"] = back;
            dtPS.Rows[0]["Depot"] = _depotID;
            dtPS.Rows[0]["Remark"] = _ltRemark.val;
            dtPS.Rows[0]["Money"] = money;
            dtPS.Rows[0]["CompanyID"] = _companyID;
            dtPS.Rows[0]["DateTime"] = _ldDate.val;
            dtPS.Rows[0]["State"] = (int)Enums.TableType.SellBack;
            dtPS.Rows[0]["LastDate"] = Convert.ToDateTime("1900-1-1");
            dtPS.Rows[0]["A"] = 1;
            if (_MainID == 0)
            {
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(),_companyID });
                dtMain.Rows[bs.Position]["ID"] = dtPS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllPS, dtPS);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID="+_MainID+")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
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
            int a=0;
            for (int i = 0; i < dtPSO.Rows.Count; i++)
            {
                 a= int.Parse(dtPSO.Rows[i]["A"].ToString());
                 if (a > 1)
                 {
                     if (Convert.ToInt32(dtPSO.Rows[i]["Amount"]) > 0)
                     {
                         dtPSO.Rows[i]["MainID"] = _MainID;
                         dtt.Clear();
                         dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                         if (a == 3)
                         {
                             dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blPSO, dtt);
                             dtPSO.Rows[i]["A"] = 1;
                         }
                         else if (a == 2)
                         {
                             BasicClass.GetDataSet.UpData(blPSO, dtt);
                             dtPSO.Rows[i]["A"] = 1;
                         }
                         else if (a == 4)
                         {
                             if (Convert.ToInt32(dtPSO.Rows[i]["ID"]) > 0)
                             {
                                 BasicClass.GetDataSet.ExecSql(blPSO, "Delete", new object[] { Convert.ToInt32(dtInfo.Rows[i]["ID"]) });
                                 int _brandID = Convert.ToInt32(dtPSO.Rows[i]["BrandID"]);
                                 int _materielID = Convert.ToInt32(dtPSO.Rows[i]["MaterielID"]);
                                 DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
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
                         DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
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
                dtInfo.Rows[i]["MainID"] = _MainID;
                a = Convert.ToInt32(dtInfo.Rows[i]["A"]);
                if (a > 1)
                {
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
                if (e.Column != _coA && bandedGridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    bandedGridView1.SetFocusedRowCellValue(_coA, 2);
            }
            try
            {
                if (e.Column == _coBrandID || e.Column == _coMaterielID)
                {
                    if (e.Column == _coMaterielID)
                    {
                        object o = BaseForm.RepositoryItem._reProduce.GetDataSourceValue("MeasureID", BaseForm.RepositoryItem._reProduce.GetDataSourceRowIndex("ID", e.Value));
                        bandedGridView1.SetRowCellValue(e.RowHandle, _coMeasureID, o);
                    }
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
            if ((e.Column == _coMaterielID||e.Column==_coBrandID) && e.Value.ToString() != "0")
                CheckMateriel(bandedGridView1.GetFocusedRowCellValue(_coMaterielID),bandedGridView1.GetFocusedRowCellValue(_coBrandID));
        }
        private void CheckMateriel(object mat,object brand)
        {
            for (int i = 0; i < bandedGridView1.RowCount; i++)
            {
                if (i != bandedGridView1.FocusedRowHandle)
                {
                    if (bandedGridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && bandedGridView1.GetRowCellValue(i, _coBrandID).Equals(brand))
                    {
                        bandedGridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);
                        XtraMessageBox.Show("同一销售单中，同种商标的同一款号只能有一条记录！");
                        return;
                    }
                }
            }
        }
        private void SetPrice(int rowID)
        {
            int brand = int.Parse(bandedGridView1.GetRowCellValue(rowID,_coBrandID).ToString());
            int mater = int.Parse(bandedGridView1.GetRowCellValue(rowID, _coMaterielID).ToString());
            int measureid = Convert.ToInt32(bandedGridView1.GetRowCellValue(rowID, _coMaterielID));
            object[] o = new object[] { _companyID, mater, brand ,measureid,0};
            bandedGridView1.SetRowCellValue(rowID, _coPrice, BasicClass.GetDataSet.GetOne(blQP, "GetPrice", o));
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (bandedGridView1.RowCount > 0)
            {
                if (e.FocusedRowHandle < 0)
                    bandedGridView1.AddNewRow();
                _oldMat = bandedGridView1.GetFocusedRowCellValue(_coMaterielID);
                _oldBrand = bandedGridView1.GetFocusedRowCellValue(_coBrandID);
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int c = 0;
            if (bandedGridView1.RowCount == 0)
                c = 1;
            for (int i = c; i < bandedGridView1.Columns.Count; i++)
            {
                bandedGridView1.SetFocusedRowCellValue(bandedGridView1.Columns[i], 0);
            }
            bandedGridView1.SetFocusedRowCellValue(_coRemark, "");
            bandedGridView1.SetFocusedRowCellValue(_coMainID, _MainID);
            bandedGridView1.SetFocusedRowCellValue(_coA, 3);
            bandedGridView1.SetFocusedRowCellValue(_coConversion, 3);
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (bandedGridView1.RowCount > 0)
            {
                int r = bandedGridView1.RowCount - 1;
                object o = bandedGridView1.GetRowCellValue(r, _coMaterielID);
                //object b = bandedGridView1.GetRowCellValue(r, _coBrandID);|| b == null || b.ToString().Trim() == "" ||b.ToString().Trim() == "0"
                if (o == null || o.ToString().Trim() == "" || o.ToString().Trim() == "0" )
                {
                    bandedGridView1.DeleteRow(r);
                }
                else
                {
                    SumMoney();
                    ShowLoan();
                }
                if (bandedGridView1.RowCount == 8)
                    bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                else
                    bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
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
            if (_MainID == 0)
            {
                //last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetSellLastMoney", new object[] { _companyID }).ToString());
                // back = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetBackSellMoney", new object[] { _companyID }).ToString());
                 DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
                 if (dttt.Rows.Count > 0)
                     last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
                 else
                     last = 0;
            }
            SumMoney();
            ShowLoan();
        }
        private void ShowLoan()
        {
            _barLoan.Caption = "客户：  " + _leCompany.valStr + "  此前欠货款： " + last.ToString("N2") + "元，扣减本单： " + money.ToString("N2") + "元，结欠货款：" + (last  - money).ToString("N2") + "元。";
        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("是否确认删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
            {
                if (DialogResult.Yes == MessageBox.Show("请再次确认是否删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    if (_MainID > 0)
                    {
                        object[] o = new object[] {_MainID };
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
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSellInfo, "DeleteInfo", new object[] { _MainID,_materielID,_brandID});
                    }
                    bandedGridView1.DeleteRow(bandedGridView1.FocusedRowHandle);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < bandedGridView1.RowCount; i++)
            {
                if (decimal.Parse(bandedGridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
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
            dr["Money"] = last - money - back;
            dr["TypeID"] = (int)(BasicClass.Enums.MoneyTableType.SellBack);
            dr["TableID"] = _MainID;
            dr["NowMoneyTypeID"] = 0;
            dr["NowMoney"] = 0;
            dr["NowReta"] = 1;
            dr["A"] = 1;
            dtCL.Rows.Add(dr);
            BasicClass.GetDataSet.Add(blCL, dtCL);
            if (BasicClass.BasicFile.liST[0].Sell4Depot)
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, false,_depotID });
            bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled =false;
            _brAddNew.Enabled = _barUnVierfy.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            t = true;
            SetColumnsReadOnly();
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.SellBack), _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本单之后已有后续业务发生，不能弃审！");
                return;
            }
            dtPS.Rows[0]["IsVerify"] = 1;
            dtPS.Rows[0]["VerifyMan"] = 0;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            BasicClass.GetDataSet.UpData(bllPS, dtPS);
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.SellBack), _MainID });
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduceSell, "Verify", new object[] { _MainID, true,_depotID });
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = true;
            _brAddNew.Enabled = _barUnVierfy.Enabled = false;
            bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            t = false;
            SetColumnsReadOnly();
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object o = bandedGridView1.GetFocusedRowCellValue(_coMaterielID);
            if (o!=null&& o.ToString()!="0")
            {
                 _materielID = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                 _brandID = int.Parse(bandedGridView1.GetFocusedRowCellValue(_coBrandID).ToString());
                dtInfo.DefaultView.RowFilter = "(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")";
                DataTable dtt = dtInfo.Clone();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    object br = dtInfo.Rows[i]["BrandID"];
                    object ma = dtInfo.Rows[i]["MaterielID"];
                    if (Convert.ToInt32(dtInfo.Rows[i]["BrandID"]) == Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coBrandID)) && Convert.ToInt32(dtInfo.Rows[i]["A"]) < 4 &&
                        Convert.ToInt32(dtInfo.Rows[i]["MaterielID"]) == Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coMaterielID)))
                    {
                        dtt.Rows.Add(dtInfo.Rows[i].ItemArray);
                    }
                }
                cResult r = new cResult();
                r.RowChanged += new RowChangedHandler(r_RowChanged);
                //Form fr = new Sell.SellTemForm(r, dtTem, _materielID, _brandID);
                Form fr = new Sell.frSellBackInfo(r, !t, dtt, bandedGridView1.GetFocusedRowCellDisplayText(_coMaterielID), bandedGridView1.GetFocusedRowCellDisplayText(_coBrandID));
                fr.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("请选择款号和商标！");
                return;
            }
        }
        void r_RowChanged(DataTable dt)
        {
            int _brandID = Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coBrandID));
            int _materielID = Convert.ToInt32(bandedGridView1.GetFocusedRowCellValue(_coMaterielID));
            DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
            foreach (DataRow drr in drs)
            {
                drr["A"] = 4;
            }
            dtInfo.AcceptChanges();
            int aaaa = 0;
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtInfo.NewRow();
                    dr["ID"]   = dr["MListID"] = 0;//= dr["DeparmentID"]= dr["TaskID"]
                    dr["MainID"] = _MainID;
                    dr["MaterielID"] = _materielID;
                    dr["BrandID"] = _brandID;
                    dr["ColorID"] = dt.Rows[i]["ColorID"];
                    dr["ColorOneID"] = dt.Rows[i]["ColorOneID"];
                    dr["ColorTwoID"] = dt.Rows[i]["ColorTwoID"];
                    dr["SizeID"] = dt.Rows[i]["SizeID"];
                    dr["Amount"] = dt.Rows[i]["Amount"];
                    dr["A"] = 3;
                    dr["MeasureID"] = bandedGridView1.GetFocusedRowCellValue(_coMeasureID);
                    dtInfo.Rows.Add(dr);
                    aaaa += Convert.ToInt32(dt.Rows[i]["Amount"]);
                }
            }
            bandedGridView1.SetFocusedRowCellValue(_coAmount, aaaa);
        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("请审核后再打印单据！");
                return;
            }
            int _id = int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllSysTem, "GetMaxId", null).ToString())-1;
            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSysTem, "GetList", new object[] { "(ID=" + _id + ")" });
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show("请完善公司信息！");
                //Form fr = new BaseForm.UserBaseSetForm();
                //fr.ShowDialog();
                return;
            }
            ds.Tables[0].TableName="Main";
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
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables[0].Rows[0]["LastMoney"] = "此前欠货款： " + last.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["BackMoney"] = "期间共还款： " + back.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NowMoney"] = "扣减本单： " + money.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NewMoney"] = "结欠货款：" + (last - back - money).ToString("C2") + "元。";
            ds.Tables[0].Rows[0]["DXMoney"] = BasicClass.BasicFile.CmycurD(last - back + money);
            ds.Tables[0].Rows[0]["ComName"] = _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("BrandName", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Conversion", typeof(string));
            dt.Columns.Add("BoxMeasureAmount", typeof(string));
            dt.Columns.Add("Money", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            int amount = 0;
            int boxAmount = 0;
            decimal moeny = 0;
            for (int i = 0; i < bandedGridView1.RowCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = bandedGridView1.GetRowCellDisplayText(i,_coMaterielID);
                dr[1] = bandedGridView1.GetRowCellDisplayText(i,_coBrandID);
                dr[2] =Convert.ToInt32( bandedGridView1.GetRowCellValue(i, _coAmount))*-1;
                dr[3] =Convert.ToDecimal(bandedGridView1.GetRowCellValue(i, _coPrice)).ToString("C2");
                dr[4] = bandedGridView1.GetRowCellDisplayText(i, _coConversion);
                dr[5] = bandedGridView1.GetRowCellDisplayText(i, _coBoxMeasureAmount);
                dr[6] ="－"+ Convert.ToDecimal(bandedGridView1.GetRowCellValue(i, _coMoney)).ToString("C2");
                dr[7] = bandedGridView1.GetRowCellDisplayText(i, _coRemark);
                dt.Rows.Add(dr);
                amount += int.Parse(bandedGridView1.GetRowCellValue(i, _coAmount).ToString());
                boxAmount += int.Parse(bandedGridView1.GetRowCellValue(i, _coBoxMeasureAmount).ToString());
                moeny += decimal.Parse(bandedGridView1.GetRowCellValue(i, _coMoney).ToString());
            }
            ds.Tables[0].Rows[0]["SumAmount"] = amount;
            ds.Tables[0].Rows[0]["SumBoxAmount"] = boxAmount;
            ds.Tables[0].Rows[0]["SumMoney"] ="－"+ moeny.ToString("C2");
            for (int i = dt.Rows.Count; i < 9; i++)
            {
                dt.Rows.Add(dt.NewRow());
                //dt.Rows[i]["Amount"] = DBNull.Value;
                //dt.Rows[i]["Price"] = DBNull.Value;
                //dt.Rows[i]["Conversion"] = DBNull.Value;
                //dt.Rows[i]["Money"] = DBNull.Value;
                //dt.Rows[i]["BoxMeasureAmount"] = DBNull.Value;
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintSellBack(ds);

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

        private void _leDepotID_EditValueChanged(object val, string text)
        {
            _depotID = int.Parse(val.ToString());
        }

    }
}