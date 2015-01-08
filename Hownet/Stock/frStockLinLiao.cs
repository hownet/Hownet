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
    public partial class frStockLinLiao : DevExpress.XtraEditors.XtraForm
    {
        public frStockLinLiao()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new cResult();
        int _MainID = 0;
        int _StockID = 0;
        string _StockNum = string.Empty;
        DataTable dtMain = new DataTable();
        public frStockLinLiao(BasicClass.cResult cr, int MainID,int StockID)
            : this()
        {
            r = cr;
            _MainID = MainID;
            _StockID = StockID;
        }
        int _companyID = 0;
        BindingSource bs = new BindingSource();
        private string bllSB = "Hownet.BLL.StockBack";
        private string blSBI = "Hownet.BLL.StockBackInfo";
        DataTable dtPS= new DataTable();
        DataTable dtPSO = new DataTable();
        bool _IsVerify = false;
        //decimal price = 0;
        //decimal money = 0;
        decimal amount = 0;
        decimal _depotAmount = 0;
        int _depotID = 0;
        int _taskID = 0;
        int _rowCount = 0;
        private void XtraForm1_Load(object sender, EventArgs e)
        {

            ShowData();
            
           if(_MainID>0)
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                bar1.Visible = false;
            }
           else if (_StockID > 0)
           {
               dtMain.Columns.Add("ID", typeof(int));
               dtMain.Rows.Add(dtMain.NewRow());
               bs.DataSource = dtMain;
               ShowView(0);
               _brFrist.Enabled = _brPrv.Enabled = _brNext.Enabled = _brLast.Enabled = false;
           }
           else
           {
               InData();
               bs.PositionChanged += new EventHandler(bs_PositionChanged);
               bs.Position = dtMain.Rows.Count - 1;
               if (bs.Position == 0)
                   ShowView(0);
           }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
            //    _brAddNew.Enabled = false;
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
            //    _brSave.Enabled = false;
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
            //    _barDel.Enabled = false;
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            //    _barVerify.Enabled = false;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _leCompany.FormName = (int)BasicClass.Enums.TableType.Supplier;
            _leDepot.FormName = (int)BasicClass.Enums.TableType.Deparment;
            //if (BasicClass.BasicFile.liST[0].Sell4Depot)
            //    _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _leCompany.IsNotCanEdit = false;
            _leCompany.IsNotCanEdit = true;
         }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetStockLinLiaoIDList", new object[] { _StockID, (int)Enums.TableType.StockLinLiao }).Tables[0];
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
             dtPSO.Rows.Clear();
             if (dtMain.DefaultView[p]["ID"].ToString() != "")
             {
                 _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                 dtPS = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                // money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
             }
             else
             {
                 dtPS = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtPS.NewRow();
                 dr["CompanyID"] = dr["ID"] = dr["DepotID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                 dr["FillDate"] = dr["DataTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                 dr["FillMan"] = BasicClass.UserInfo.UserID;
                 dr["IsVerify"] = 1;
                  dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                 dr["Remark"] = "";
                 dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { DateTime.Today });
                 dr["LastMoney"] =  0;
                 dr["BackMoney"] =   0;
                 dr["BackDate"] = DateTime.Parse("1900-1-1");
                 dr["TaskID"] = _taskID = 0;
               //  money = 0;
                 dtPS.Rows.Add(dr);
                 _brAddNew.Enabled = false;
             }
             _IsVerify =(int.Parse((dtPS.Rows[0]["IsVerify"]).ToString())==3);
             _ldDate.val = dtPS.Rows[0]["DataTime"];
             _leCompany.editVal = _companyID = int.Parse(dtPS.Rows[0]["CompanyID"].ToString());
             _leDepot.editVal = _depotID = int.Parse(dtPS.Rows[0]["DepotID"].ToString());
           //  money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
             _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
             _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
             dtPSO = BasicClass.GetDataSet.GetDS(blSBI, "GetLinLiaoInfo", new object[] {  _MainID  }).Tables[0];
             gridControl1.DataSource = dtPSO;
            
             _barVerify.Enabled = _brSave.Enabled =  _barDel.Enabled =_coDepotAmount.Visible= !_IsVerify;
             _brAddNew.Enabled = _barUnVierfy.Enabled = (_IsVerify && p == dtMain.Rows.Count - 1);
             SetColumnsReadOnly();
            // if (_IsVerify)
            //     gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //else
            //     gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
             this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
              _coDepotAmount.OptionsColumn.AllowEdit = false;
             _rowCount = gridView1.RowCount;
             this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
             this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
              _taskID = Convert.ToInt32(dtPS.Rows[0]["TaskID"]);
              _leCompany.IsNotCanEdit = _leDepot.IsNotCanEdit = (_MainID > 0);// 
             _ltRemark.Focus();
             DataTable dtParent = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _StockID + ")" }).Tables[0];
             if (dtParent.Rows.Count > 0)
             {
                 _leCompany.IsNotCanEdit = true;
                 _leCompany.editVal = Convert.ToInt32(dtParent.Rows[0]["CompanyID"]);
                 _laStockNum.val = DateTime.Parse(dtParent.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtParent.Rows[0]["Num"].ToString().PadLeft(3, '0');
             }
        }
        private void SetColumnsReadOnly()
        {
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Visible && gridView1.Columns[i] != _coAmount)
                {
                    gridView1.Columns[i].OptionsColumn.AllowEdit = !_IsVerify;
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
                r.ChangeText(_MainID.ToString());
                //int d = bs.Position;
                //InData();
                //bs.Position = d;
                ShowView(bs.Position);
                //}
            }
        }
        private bool Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPSO.AcceptChanges();
            if (_companyID == 0)
            {
                XtraMessageBox.Show("请选择领料部门！");
                return false;
            }
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择出货仓库！");
                return false;
            }
            bool t = false;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToInt32(gridView1.GetRowCellValue(i, _coMListID)) == 0)
                {
                    t = true;
                    break;
                }
            }
            if (t)
            {
                XtraMessageBox.Show("有物料信息不明确，请修改后再保存！");
                return false;
            }
            dtPS.Rows[0]["LastMoney"] = 0;
            dtPS.Rows[0]["BackMoney"] = 0;
            dtPS.Rows[0]["Remark"] = _ltRemark.val;
            dtPS.Rows[0]["Money"] = 0;// money;
            dtPS.Rows[0]["CompanyID"] = _companyID;
            dtPS.Rows[0]["DataTime"] = _ldDate.val;
            dtPS.Rows[0]["State"] = (int)Enums.TableType.StockLinLiao;
            dtPS.Rows[0]["A"] = 1;
            dtPS.Rows[0]["DepotID"] = _depotID;
            dtPS.Rows[0]["TaskID"] = _StockID;
            if (_MainID == 0)
            {
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.TaskLinLiao, 0 });
                dtMain.Rows[bs.Position]["ID"] = dtPS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSB, dtPS);
            }
            else
            {
                BasicClass.GetDataSet.UpData(bllSB, dtPS);
            }
            DataTable dtt = dtPSO.Clone();
            int a = 1;
            for (int i = 0; i < dtPSO.Rows.Count; i++)
            {
                if (dtPSO.Rows[i]["Amount"].ToString() != string.Empty && Convert.ToDecimal(dtPSO.Rows[i]["Amount"]) > 0)
                {
                    a = int.Parse(dtPSO.Rows[i]["A"].ToString());
                    if (a > 1)
                    {

                        dtPSO.Rows[i]["MainID"] = _MainID;
                        dtt.Clear();
                        dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blSBI, dtt);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(blSBI, dtt);
                        }
                        dtPSO.Rows[i]["A"] = 1;
                    }
                }
            }
             _leDepot.IsNotCanEdit = _leCompany.IsNotCanEdit = true;
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
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "0")
                    gridView1.SetFocusedRowCellValue(_coA, 3);
            }
            if (e.Column == _coMaterielID)
            {
                object obj = BaseForm.RepositoryItem._reMateriel.GetDataSourceValue("MeasureID", BaseForm.RepositoryItem._reMateriel.GetDataSourceRowIndex("ID", e.Value));
                gridView1.SetFocusedRowCellValue(_coMeasureID, obj);
            }
            try
            {
                if (e.Column == _coAmount)
                {
                    amount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
                    if (gridView1.GetFocusedRowCellValue(_coDepotAmount).ToString().Trim() == string.Empty || amount > decimal.Parse(gridView1.GetFocusedRowCellValue(_coDepotAmount).ToString()))
                    {
                        XtraMessageBox.Show("领料数量超过现有库存数量！");
                        gridView1.SetFocusedRowCellValue(_coAmount, gridView1.GetFocusedRowCellValue(_coDepotAmount));
                    }
                }
            }
            catch (Exception ex)
            { 
            }
            try
            {
                if (e.Column == _coDepotAmount && decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString()) > decimal.Parse(e.Value.ToString()))
                {
                    XtraMessageBox.Show("领料数量超过现有库存数量！");
                    gridView1.SetFocusedRowCellValue(_coAmount, e.Value);
                }
            }
            catch { }
            if ((e.Column == _coMaterielID || e.Column == _coColorID || e.Column == _coSizeID || e.Column == _coMeasureID||e.Column==_coColorOneID||
                e.Column==_coColorTwoID) && e.Value.ToString() != "0")
                CheckMateriel(gridView1.GetFocusedRowCellValue(_coMaterielID), gridView1.GetFocusedRowCellValue(_coColorID), 
                    gridView1.GetFocusedRowCellValue(_coSizeID), gridView1.GetFocusedRowCellValue(_coMeasureID),
                    gridView1.GetFocusedRowCellValue(_coColorOneID),gridView1.GetFocusedRowCellValue(_coColorTwoID));
        }
     
        private void CheckMateriel(object mat, object color, object size, object MeasureID,object ColorOneID,object ColorTwoID)
        {
            //if (mat == null || mat == DBNull.Value)
            //    mat = 0;
            //if (color == null || color == DBNull.Value)
            //    color = 0;
            //if (size == null || size == DBNull.Value)
            //    size = 0;
            //if (MeasureID == null || MeasureID == DBNull.Value)
            //    MeasureID = 0;
            //if (ColorOneID == null || ColorOneID == DBNull.Value)
            //    ColorOneID = 0;
            //if (ColorTwoID == null || ColorTwoID == DBNull.Value)
            //    ColorTwoID = 0;
            //if (gridView1.FocusedRowHandle > -1)
            //{
            //    for (int i = 0; i < gridView1.RowCount; i++)
            //    {
            //        if (i != gridView1.FocusedRowHandle)
            //        {
            //            if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && gridView1.GetRowCellValue(i, _coColorID).Equals(color)
            //                && gridView1.GetRowCellValue(i, _coSizeID).Equals(size) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(MeasureID)
            //                &&gridView1.GetRowCellValue(i,_coColorOneID).Equals(ColorOneID)&&gridView1.GetRowCellValue(i,_coColorTwoID).Equals(ColorTwoID))
            //            {
            //                //gridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);

            //                XtraMessageBox.Show("同一领料单中，不能有多个相同记录！");
            //                SendKeys.Send("{Esc}");
            //                return;
            //            }
            //        }
            //    }
            //}
            //DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, MeasureID, 0, ColorOneID, ColorTwoID, 0 }).Tables[0];
            //if (dttt.Rows.Count > 0)
            //{
            //    _depotAmount = Convert.ToDecimal(dttt.Rows[0]["Amount"]);
            //    gridView1.SetFocusedRowCellValue(_coDepotAmount, _depotAmount);
            //    gridView1.SetFocusedRowCellValue(_coStockInfoID, dttt.Rows[0]["ID"]);
            //    gridView1.SetFocusedRowCellValue(_coDemandID, 0);
            //    gridView1.SetFocusedRowCellValue(_coMListID, dttt.Rows[0]["MListID"]);
            //}
            //else
            //{
            //    gridView1.SetFocusedRowCellValue(_coDepotAmount, 0);
            //    gridView1.SetFocusedRowCellValue(_coStockInfoID, 0);
            //    gridView1.SetFocusedRowCellValue(_coDemandID, 0);
            //    gridView1.SetFocusedRowCellValue(_coMListID, 0);
            //}
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (gridView1.RowCount > 0)
            //{
            //    if (e.FocusedRowHandle < 0)
            //        gridView1.AddNewRow();
            //}
            //_coMaterielID.OptionsColumn.AllowEdit = _coSizeID.OptionsColumn.AllowEdit = _coColorID.OptionsColumn.AllowEdit =
            //   _coColorOneID.OptionsColumn.AllowEdit = _coColorTwoID.OptionsColumn.AllowEdit = (e.FocusedRowHandle < 0);
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
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            //if (gridView1.RowCount > 0&&_rowCount<gridView1.RowCount)
            //{
            //    int r = gridView1.RowCount - 1;
            //    object o = gridView1.GetRowCellValue(r, _coMaterielID);
            //    object b = gridView1.GetRowCellValue(r, _coColorID);
            //    object s = gridView1.GetRowCellValue(r, _coSizeID);
            //    object m = gridView1.GetRowCellValue(r, _coMeasureID);
            //    //object b = gridView1.GetRowCellValue(r, _coBrandID);|| b == null || b.ToString().Trim() == "" ||b.ToString().Trim() == "0"
            //    if (o == null || o.ToString().Trim() == "" || o.ToString().Trim() == "0")
            //    {
            //        gridView1.DeleteRow(r);
            //    }
            //    else
            //    {
            //        for (int i = 0; i < gridView1.RowCount - 1; i++)
            //        {
            //            if (i != gridView1.FocusedRowHandle)
            //            {
            //                if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(o) && gridView1.GetRowCellValue(i, _coColorID).Equals(b)
            //                    && gridView1.GetRowCellValue(i, _coSizeID).Equals(s) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(m))
            //                {
            //                    //gridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);
            //                    //SendKeys.Send("{Esc}");
            //                    XtraMessageBox.Show("同一领料单中，不能有多个相同记录！");
            //                    gridView1.DeleteRow(r);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    if (gridView1.RowCount == 8)
            //        gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //    else
            //        gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            //}
            _rowCount = gridView1.RowCount;
        }                  
        private void _leCompany_EditValueChanged(object val, string text)
        {
            _companyID = int.Parse(val.ToString());
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
                    this.gridView1.RowCountChanged -= new System.EventHandler(this.gridView1_RowCountChanged);
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!Save())
                return;
            if(!Convert.ToBoolean( BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllStockBack,"CheckLinLiao",new object[]{_MainID})))
            {
                XtraMessageBox.Show("出库数量超过库存！");
                return;
            }
            dtPS.Rows[0]["IsVerify"] = 3;
            dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Today;
            BasicClass.GetDataSet.UpData(bllSB, dtPS);
         //   BasicClass.GetDataSet.ExecSql(bllSB, "Verify", new object[] { _MainID, false, _depotID });
            BasicClass.GetDataSet.ExecSql(blSBI, "VerifyStockLinLiao", new object[] {_MainID,false });
         //   BasicClass.GetDataSet.ExecSql(bllSB, "UpDemand", new object[] { _MainID, true });
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = false;
            _brAddNew.Enabled = _barUnVierfy.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            _IsVerify = true;
            SetColumnsReadOnly();
            r.ChangeText(_MainID.ToString());
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtPS.Rows[0]["IsVerify"] = 0;
            dtPS.Rows[0]["VerifyMan"] = 0;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            BasicClass.GetDataSet.UpData(bllSB, dtPS);
            BasicClass.GetDataSet.ExecSql(blSBI, "VerifyLinLiao", new object[] { _MainID, true });
           // BasicClass.GetDataSet.ExecSql(bllSB, "UpDemand", new object[] { _MainID, false });
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = true;
            _brAddNew.Enabled = _barUnVierfy.Enabled = false;
           // gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            _IsVerify = false;
            SetColumnsReadOnly();
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_IsVerify)
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
            ds.Tables["Main"].Columns.Add("ComName", typeof(string));
            ds.Tables["Main"].Columns.Add("Date", typeof(string));
            ds.Tables["Main"].Columns.Add("Num", typeof(string));
            ds.Tables["Main"].Columns.Add("UserName", typeof(string));
            //ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("TaskNum", typeof(string));
            ds.Tables["Main"].Columns.Add("DepotName", typeof(string));
            ds.Tables["Main"].Columns.Add("TrueName", typeof(string));
            ds.Tables[0].Rows[0]["ComName"] = _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;
            ds.Tables[0].Rows[0]["TaskNum"] = _laStockNum.val;
            ds.Tables[0].Rows[0]["DepotName"] = _leDepot.valStr;
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            ds.Tables[0].Rows[0]["TrueName"] = dtUser.Select("(ID=" + dtPS.Rows[0]["FillMan"] + ")")[0]["TrueName"];
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("GuiGe", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            string _guiGe = string.Empty;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                _guiGe = string.Empty;
                DataRow dr = dt.NewRow();
                dr[0] = gridView1.GetRowCellDisplayText(i,_coMaterielID);
                if (gridView1.GetRowCellDisplayText(i, _coColorID).Trim() != string.Empty)
                    _guiGe = gridView1.GetRowCellDisplayText(i, _coColorID) + "/";
                if (gridView1.GetRowCellDisplayText(i, _coColorOneID).Trim() != string.Empty)
                    _guiGe = _guiGe + gridView1.GetRowCellDisplayText(i, _coColorOneID) + "/";
                if (gridView1.GetRowCellDisplayText(i, _coColorTwoID).Trim() != string.Empty)
                    _guiGe = _guiGe + gridView1.GetRowCellDisplayText(i, _coColorTwoID) + "/";
                if (gridView1.GetRowCellDisplayText(i, _coSizeID).Trim() != string.Empty)
                    _guiGe = _guiGe + gridView1.GetRowCellDisplayText(i, _coSizeID) + "/";
                if (_guiGe.Length > 0)
                    _guiGe = _guiGe.Remove(_guiGe.Length - 1);
                dr[1] = _guiGe;
                dr[2] = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, _coAmount));
                dr[3] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[4] = gridView1.GetRowCellDisplayText(i, _coRemark);
                dt.Rows.Add(dr);
            }
            for (int i = dt.Rows.Count; i < 10; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintLinLiao(ds,false);

        }

        private void _leDepot_EditValueChanged(object val, string text)
        {
          //  if (_MainID == 0)
                _depotID = int.Parse(val.ToString());
            GetNeedAmount();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        private void _leNum_EditValueChanged(object val, string text)
        {
            if (_MainID == 0)
            {
                _taskID = Convert.ToInt32(val);
                
                GetNeedAmount();
            }
        }
        private void GetNeedAmount()
        {
            if (_StockID == 0 || _depotID == 0)
                return;
            dtPSO.Rows.Clear();
            dtPSO = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBackInfo, "GetStockLinLiaoInfo", new object[] { _StockID, _depotID }).Tables[0];
            gridControl1.DataSource = dtPSO;
        }

    }
}