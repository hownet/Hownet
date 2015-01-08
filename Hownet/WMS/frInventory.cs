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

namespace Hownet.WMS
{
    public partial class frInventory : DevExpress.XtraEditors.XtraForm
    {
        public frInventory()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frInventory(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _companyID = 0;
        int _depotID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        private string bllPS = "Hownet.BLL.ProduceSell";
        private string bllII = "Hownet.BLL.WMSInventoryInfo";
        DataTable dtPS= new DataTable();
        DataTable dtPSO = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtRList = new DataTable();
        DataTable dtM = new DataTable();
        string backDate = string.Empty;
        bool _isVerify = false;
        object _oldMat = null;
        object _oldBrand = null;
        bool _IsShowHW = false;
        int _AttID = 0;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
          //  this.Text = "销售开单";
            ShowData();
            if (_MainID == 0)
            {
              
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                InData();
                bs.Position = dtMain.Rows.Count - 1;
                simpleButton1.Visible = false;
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                bar1.Visible = false;
                simpleButton1.Visible = true;
            }
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _barVerify.Enabled = _barUnVierfy.Enabled = _brAddNew.Enabled = _barDel.Enabled = _brSave.Enabled =  false;
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //}
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barVerify.Enabled = false;
            DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='成品需按货架存放')" }).Tables[0];
            if (dtTem.Rows.Count > 0)
                _IsShowHW = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
            gridControl2.Visible = _IsShowHW;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            if (bs.Position > -1)
                ShowView(bs.Position);
        }
        void ShowData()
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _leDepotID.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] {"仓库" }).Tables[0];
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            dtM = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] { "(AttributeID<>5)" }).Tables[0];
            DataRow drM = dtM.NewRow();
            drM["A"] = drM["ID"] = drM["ParentID"] = drM["IsEnd"] = drM["IsUse"] = drM["AttributeID"] = 0;
            drM["Name"] = drM["Sn"] = drM["Remark"] = "";
            dtM.Rows.Add(drM);
            dtM.DefaultView.Sort = "ID";
            _leMM.Properties.DataSource = dtM.DefaultView;
            _leDepotID.EditValue = _leMM.EditValue = 0;
         }

        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllPS, "GetIDList", new object[] { (int)Enums.TableType.WMSInventoryInfo }).Tables[0];
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
             if (dtMain.DefaultView[p]["ID"].ToString() != "")
             {
                 _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                 dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                 simpleButton2.Visible = _leMM.Visible = false;
             }
             else
             {
                 dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtPS.NewRow();
                 dr["CompanyID"] = dr["ID"] = dr["Depot"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                 dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                 dr["FillMan"] = BasicClass.UserInfo.UserID;
                 dr["IsVerify"] = false;
                 dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                 dr["Remark"] = "";
                 dr["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(),((int)BasicClass.Enums.TableType.WMSInventoryInfo)*-1 });
                 dr["LastMoney"] = 0;
                 dr["BackMoney"] = 0;
                 dr["BackDate"] = DateTime.Parse("1900-1-1");
                 dr["LastDate"] = DateTime.Parse("1900-1-1");
                 dtPS.Rows.Add(dr);
                 _brAddNew.Enabled = false;
                 simpleButton2.Visible = _leMM.Visible = true;
             }
             _upData = int.Parse(dtPS.Rows[0]["UpData"].ToString());
             _isVerify = Convert.ToInt32(dtPS.Rows[0]["IsVerify"]) == 3;
             _ldDate.val = dtPS.Rows[0]["DateTime"];
             _leDepotID.EditValue = _depotID = int.Parse(dtPS.Rows[0]["Depot"].ToString());
             _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
             _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
             dtPSO = BasicClass.GetDataSet.GetDS(bllII, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
             if (!_isVerify)
             {
                 DataRow dr = dtPSO.NewRow();
                 dr["MainID"] = _MainID;
                 dr["ID"]= dr["MaterielID"] = dr["BrandID"] = dr["ColorID"] = dr["ColorOneID"] = dr["ColorTwoID"] = dr["SizeID"] = dr["MeasureID"] = dr["PreviousNumber"] = dr["NowAmount"] = dr["ChangeAmount"] = dr["CompanyID"] = dr["MListID"] = dr["RepertoryID"] = 0;
                 dr["Remark"] = string.Empty;
                 dr["A"] = 3;
                 dtPSO.Rows.Add(dr.ItemArray);
                 dtPSO.Rows.Add(dr.ItemArray);
             }
             dtPSO.DefaultView.RowFilter = "(A<4)";
             gridControl1.DataSource = dtPSO.DefaultView;
             //_leCompany.IsCanEdit = !t;
             _barVerify.Enabled = _brSave.Enabled =  _barDel.Enabled = !_isVerify;
             _brAddNew.Enabled = (_isVerify && p == dtMain.Rows.Count - 1);
             _barUnVierfy.Enabled = _isVerify;
            // if ((DateTime)(dtPS.Rows[0]["BackDate"]) == DateTime.Parse("1900-1-1"))
            //     backDate = "      年    月    日";
            //else
            //     backDate=((DateTime)(dtPS.Rows[0]["BackDate"])).ToString("yyyy年MM月dd日");

             //gridView1.OptionsBehavior.Editable = !t;
            // if (_isVerify)
            //     gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //else
            //     gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
           // _coMaterielID.OptionsColumn.AllowEdit = _coBrandID.OptionsColumn.AllowEdit =gridView1.FocusedRowHandle< 0;
              gridView1.OptionsBehavior.Editable = !_isVerify;
              gridControl2.Visible = (!_isVerify && _IsShowHW);
             _coSBILNotAmount.ColumnEdit = _coSBILDepotInfoID.ColumnEdit = BaseForm.RepositoryItem._reDepotInfo(_depotID);
            if(!_isVerify)
            {
                gridControl2.Visible = _IsShowHW&&_AttID==4;
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
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPSO.AcceptChanges();
            bool _checkCS = false;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToInt32(gridView1.GetRowCellValue(i,_coColorID)) == 0 || Convert.ToInt32(gridView1.GetRowCellValue(i,_coSizeID)) == 0)
                {
                    _checkCS = true;
                    break;
                }
            }
            if (_checkCS)
            {
                if (DialogResult.No == XtraMessageBox.Show("所盘点物料有记录没有颜色或尺码，请确认是否正确", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    return false;
                }
            }
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择盘点仓库！");
                return false;
            }

            dtPS.Rows[0]["Depot"] = _depotID;
            dtPS.Rows[0]["LastMoney"] = 0;
            dtPS.Rows[0]["BackMoney"] = 0;
            dtPS.Rows[0]["Remark"] = _ltRemark.val;
            dtPS.Rows[0]["Money"] = 0;
            dtPS.Rows[0]["CompanyID"] = _companyID;
            dtPS.Rows[0]["DateTime"] = _ldDate.val;
            dtPS.Rows[0]["State"] = (int)Enums.TableType.WMSInventoryInfo;
            dtPS.Rows[0]["A"] = 1;
            dtPS.Rows[0]["IsVerify"] = 3;
            dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Today;
            if (_MainID == 0)
            {
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(),((int)BasicClass.Enums.TableType.WMSInventoryInfo)*-1 });
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
            int a = 0;
            for (int i = 0; i < dtPSO.Rows.Count-2; i++)
            {
                 a = int.Parse(dtPSO.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    if (Convert.ToInt32(dtPSO.Rows[i]["ChangeAmount"]) != 0)
                    {
                        dtPSO.Rows[i]["MainID"] = _MainID;
                        dtt.Clear();
                        dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                        if (a == 3)//新增
                        {
                            dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllII, dtt);
                            dtPSO.Rows[i]["A"] = 1;
                        }
                        else if (a == 2)//修改
                        {
                            BasicClass.GetDataSet.UpData(bllII, dtt);
                            dtPSO.Rows[i]["A"] = 1;
                        }
                        else if (a == 4)//删除
                        {
                            if (Convert.ToInt32(dtPSO.Rows[i]["ID"]) > 0)
                            {
                                BasicClass.GetDataSet.GetDS(bllII, "Delete", new object[] { Convert.ToInt32(dtPSO.Rows[i]["ID"]) });

                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dtPSO.Rows[i]["ID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(bllII, "Delete", new object[] { Convert.ToInt32(dtPSO.Rows[i]["ID"]) });
                            dtPSO.Rows[i]["A"] = 5;
                        }

                    }
                }
            }
            DataTable dtTem = dtRList.Clone();
             a = 0;
            for (int i = 0; i < dtRList.Rows.Count; i++)
            {
                a = Convert.ToInt32(dtRList.Rows[i]["A"]);
                if (a == 2)
                {
                    dtTem.Rows.Clear();
                    dtRList.Rows[i]["Amount"] = dtRList.Rows[i]["NotAmount"] = dtRList.Rows[i]["NowAmount"];
                    dtRList.Rows[i]["DepotInfoID"] = dtRList.Rows[i]["NewDepotInfoID"];
                    dtTem.Rows.Add(dtRList.Rows[i].ItemArray);
                    BasicClass.GetDataSet.UpData("Hownet.BLL.RepertoryList", dtTem);
                    dtRList.Rows[i]["A"] = 1;
                }
            }

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
            if (e.Value == null)
                return;
            if (e.RowHandle > -1)
            {
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            try
            {
                if (e.Column == _coBrandID || e.Column == _coMaterielID)
                {
                    if (e.Column == _coMaterielID)
                    {
                        object o = BaseForm.RepositoryItem._reAllMateriel.GetDataSourceValue("MeasureID", BaseForm.RepositoryItem._reAllMateriel.GetDataSourceRowIndex("ID", e.Value));
                        gridView1.SetRowCellValue(e.RowHandle, _coMeasureID, o);
                    }
                }
            }
            catch { }
            if (e.Column == _coMaterielID||e.Column==_coBrandID||e.Column==_coColorID||e.Column==_coColorOneID||e.Column==_coColorTwoID||e.Column==_coSizeID)
            {
                CheckMateriel(e.RowHandle);
            }
            if (e.Column == _coPreviousNumber || e.Column == _coNowAmount)
            {
                try
                {
                    decimal pn = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coPreviousNumber));
                    decimal na = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coNowAmount));
                    gridView1.SetFocusedRowCellValue(_coChangeAmount, (pn - na));
                }
                catch { }
            }
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtPSO.NewRow();
                dr["MainID"] = _MainID;
                dr["ID"] = dr["MaterielID"] = dr["BrandID"] = dr["ColorID"] = dr["ColorOneID"] = dr["ColorTwoID"] = dr["SizeID"] = dr["MeasureID"] = dr["PreviousNumber"] = dr["NowAmount"] = dr["ChangeAmount"] = dr["CompanyID"] = dr["MListID"] = dr["RepertoryID"] = 0;
                dr["Remark"] = string.Empty;
                dr["A"] = 3;
                dtPSO.Rows.Add(dr.ItemArray);
            }
        }
        private void CheckMateriel(int RowID)
        {
            object mat = gridView1.GetRowCellValue(RowID, _coMaterielID);
            object brand = gridView1.GetRowCellValue(RowID, _coBrandID);
            object color = gridView1.GetRowCellValue(RowID, _coColorID);
            object colorOne = gridView1.GetRowCellValue(RowID, _coColorOneID);
            object colorTwo = gridView1.GetRowCellValue(RowID, _coColorTwoID);
            object size = gridView1.GetRowCellValue(RowID, _coSizeID);
            object measure = gridView1.GetRowCellValue(RowID, _coMeasureID);
            if (mat.ToString() != string.Empty && brand.ToString() != string.Empty)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i != gridView1.FocusedRowHandle)
                    {
                        if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) &&
                            gridView1.GetRowCellValue(i, _coBrandID).Equals(brand) &&
                            gridView1.GetRowCellValue(i, _coColorID).Equals(color) &&
                            gridView1.GetRowCellValue(i, _coColorOneID).Equals(colorOne) &&
                            gridView1.GetRowCellValue(i, _coColorTwoID).Equals(colorTwo) &&
                            gridView1.GetRowCellValue(i, _coSizeID).Equals(size))
                        {
                            //   gridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);
                            gridView1.DeleteRow(RowID);
                            XtraMessageBox.Show("同一盘点单中，相同物料只能有一条记录！");
                            return;
                        }
                    }
                }
                try  
                {
                    DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, measure, 0, colorOne, colorTwo, brand,0 }).Tables[0];
                    if(dttt.Rows.Count>0)
                    {
                        gridView1.SetFocusedRowCellValue(_coPreviousNumber, dttt.Rows[0]["Amount"]);
                        gridView1.SetFocusedRowCellValue(_coRepertoryID, dttt.Rows[0]["ID"]);
                        gridView1.SetFocusedRowCellValue(_coMListID, dttt.Rows[0]["MListID"]);
                    }
                }
                catch { }
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            if (gridView1.RowCount > 0)
            {
                _oldMat = gridView1.GetFocusedRowCellValue(_coMaterielID);
                _oldBrand = gridView1.GetFocusedRowCellValue(_coBrandID);
            }
            _coMaterielID.OptionsColumn.AllowEdit = _coBrandID.OptionsColumn.AllowEdit = _coColorID.OptionsColumn.AllowEdit =
                _coColorOneID.OptionsColumn.AllowEdit = _coColorTwoID.OptionsColumn.AllowEdit = _coSizeID.OptionsColumn.AllowEdit = (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coRepertoryID))) == 0;
            _coNowAmount.OptionsColumn.AllowEdit = true;
         if(_AttID==4)
         {
             _coNowAmount.OptionsColumn.AllowEdit = !_IsShowHW;
         }
           
            if ( _IsShowHW && !_isVerify)//_AttID == 4 &&
            {
                if (_AttID == 4)
                {
                    int _RID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coRepertoryID));
                    if (dtRList.Rows.Count > 0)
                    {
                        dtRList.DefaultView.RowFilter = "(MainID=" + _RID + ")";
                        if (dtRList.DefaultView.Count == 0)
                        {
                            DataTable dtTem = BasicClass.GetDataSet.GetBySql(" SELECT  1 as A,   ID, Amount, MainID, Remark, NotAmount, StockListID, BatchNumber, IsEnd, SpecID, SpecName, DepotInfoID, DepotInfoName, QRID, DateTime, PlanID,NotAmount as NowAmount,DepotInfoID as NewDepotInfoID FROM         RepertoryList Where (MainID=" + _RID + ")");
                            for (int i = 0; i < dtTem.Rows.Count; i++)
                            {
                                dtRList.Rows.Add(dtTem.Rows[i].ItemArray);
                            }
                        }
                    }
                    else
                    {
                        dtRList = BasicClass.GetDataSet.GetBySql(" SELECT  1 as A,   ID, Amount, MainID, Remark, NotAmount, StockListID, BatchNumber, IsEnd, SpecID, SpecName, DepotInfoID, DepotInfoName, QRID, DateTime, PlanID,NotAmount as NowAmount,DepotInfoID as NewDepotInfoID FROM         RepertoryList Where (MainID=" + _RID + ")");
                    }

                    dtRList.DefaultView.RowFilter = "(MainID=" + _RID + ")";
                    gridControl2.DataSource = dtRList.DefaultView;
                }
                else
                {

                }
            }
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
            //gridView1.SetFocusedRowCellValue(_coRemark, " ");
            //gridView1.SetFocusedRowCellValue(_coMainID, _MainID);
            //gridView1.SetFocusedRowCellValue(_coA, 3);
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            //if (gridView1.RowCount > 0)
            //{
            //    int r = gridView1.RowCount - 1;
            //    object o = gridView1.GetRowCellValue(r, _coMaterielID);
            //    //object b = gridView1.GetRowCellValue(r, _coBrandID);|| b == null || b.ToString().Trim() == "" ||b.ToString().Trim() == "0"
            //    if (o == null || o.ToString().Trim() == "" || o.ToString().Trim() == "0" )
            //    {
            //        gridView1.DeleteRow(r);
            //    }
            //    else
            //    {

            //    }
            //    //if (gridView1.RowCount == 8)
            //    //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //    //else
            //    //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            //}
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
                        BasicClass.GetDataSet.ExecSql(bllII, "DeleteByMain", o);
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
                    int id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (id > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(bllII, "Delete", new object[] { id });
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool f = _MainID == 0;
            if (Save())
            {
                BasicClass.GetDataSet.ExecSql(bllII, "Verify", new object[] {_MainID,true });
                _barVerify.Enabled = false;

            }
            else
            {
                return;
            }
            

            //_barVerify.Enabled = _brSave.Enabled = _barDel.Enabled =false;
            //_brAddNew.Enabled = _barUnVierfy.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            //_isVerify = true;
            if (f)
            {
                int d = bs.Position;
                InData();
                bs.Position = d;
                ShowView(d);
            }
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTable();
        }
        private void PrintTable()
        {
          

        }
        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PrintTable();
        }

        private void _leDepotID_EditValueChanged(object val, string text)
        {

        }

        private void _leDepotID_EditValueChanged(object sender, EventArgs e)
        {
            if (_MainID == 0)
            {
                _depotID = Convert.ToInt32(_leDepotID.EditValue);
               _coSBILNotAmount.ColumnEdit= _coSBILDepotInfoID.ColumnEdit = BaseForm.RepositoryItem._reDepotInfo(_depotID);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(_leDepotID.EditValue) == 0 || Convert.ToInt32(_leMM.EditValue) == 0)
            {
                return;
            }
            _AttID = Convert.ToInt32(dtM.Select("(ID=" + _leMM.EditValue + ")")[0]["AttributeID"]);
            dtPSO=BasicClass.GetDataSet.GetDS(Bllstr.bllRepertory,"GetInventory",new object[]{Convert.ToInt32(_leDepotID.EditValue),Convert.ToInt32(_leMM.EditValue)}).Tables[0];
            DataRow dr = dtPSO.NewRow();
            dr["MainID"] = _MainID;
            dr["ID"] = dr["MaterielID"] = dr["BrandID"] = dr["ColorID"] = dr["ColorOneID"] = dr["ColorTwoID"] = dr["SizeID"] = dr["MeasureID"] = dr["PreviousNumber"] = dr["NowAmount"] = dr["ChangeAmount"] = dr["CompanyID"] = dr["MListID"] = dr["RepertoryID"] = 0;
            dr["Remark"] = string.Empty;
            dr["A"] = 3;
            dtPSO.Rows.Add(dr.ItemArray);
            dtPSO.Rows.Add(dr.ItemArray);
            gridControl1.DataSource = dtPSO;
            dtRList.Rows.Clear();
            gridControl2.Visible = _IsShowHW && _AttID == 4;
            gridControl2.DataSource = dtRList.DefaultView;
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coSBILA && Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coSBILA)) == 1)
                gridView2.SetFocusedRowCellValue(_coSBILA, 2);
            if(e.Column==_coSBILNowAmount)
            {
                int _amount = 0;
                for(int i=0;i<gridView2.RowCount;i++)
                {
                    _amount += Convert.ToInt32(gridView2.GetRowCellValue(i, _coSBILNowAmount));
                }
                gridView1.SetFocusedRowCellValue(_coNowAmount, _amount);
            }
        }
    }
}