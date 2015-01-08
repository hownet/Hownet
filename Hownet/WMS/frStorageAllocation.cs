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
    public partial class frStorageAllocation : DevExpress.XtraEditors.XtraForm
    {
        public frStorageAllocation()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frStorageAllocation(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _companyID = 0;
        int _depotID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        private string bllSB = "Hownet.BLL.StockBack";
        private string bllSBI = "Hownet.BLL.StockBackInfo";
        DataTable dtSB= new DataTable();
        DataTable dtSBI = new DataTable();
        DataTable dt = new DataTable();
        string backDate = string.Empty;
        bool _isVerify = false;
        object _oldMat = null;
        object _oldBrand = null;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
          //  this.Text = "销售开单";
            ShowData();
            if (_MainID == 0)
            {
                InData();
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
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
            _leCompanyID.Properties.DataSource = _leDepotID.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "仓库" }).Tables[0];
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _leDepotID.EditValue = _leCompanyID.EditValue = 0;
            _coPlanID.ColumnEdit = BaseForm.RepositoryItem._rePlanNum;
        }

        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)Enums.TableType.StorageAllocation }).Tables[0];
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
                 dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                
             }
             else
             {
                 dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtSB.NewRow();
                 dr["CompanyID"] = dr["ID"] = dr["DepotID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                 dr["FillDate"] = dr["DataTime"] = BasicClass.GetDataSet.GetDateTime();
                 dr["FillMan"] =0;
                 dr["IsVerify"] = 1;
                 dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                 dr["Remark"] = "";
                 dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { DateTime.Today });
                 dr["LastMoney"] = 0;
                 dr["BackMoney"] = 0;
                 dr["BackDate"] = DateTime.Parse("1900-1-1");
                 dtSB.Rows.Add(dr);
                 _brAddNew.Enabled = false;
                
             }
             _upData = int.Parse(dtSB.Rows[0]["UpData"].ToString());
             _isVerify = (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) > 2);
             _ldDate.val = dtSB.Rows[0]["DataTime"];
             _leDepotID.EditValue = _depotID = int.Parse(dtSB.Rows[0]["DepotID"].ToString());
             _leCompanyID.EditValue = _companyID = Convert.ToInt32(dtSB.Rows[0]["CompanyID"]);
             _ltRemark.val = dtSB.Rows[0]["Remark"].ToString();
             _ltNum.val = DateTime.Parse(dtSB.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtSB.Rows[0]["Num"].ToString().PadLeft(3, '0');
             dtSBI = BasicClass.GetDataSet.GetDS(bllSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
             dtSBI.DefaultView.RowFilter = "(A<4)";
             gridControl1.DataSource = dtSBI.DefaultView;
             _barVerify.Enabled = _brSave.Enabled =  _barDel.Enabled = !_isVerify;
             _brAddNew.Enabled = (_MainID>0);
             _leDepotID.Enabled = (_MainID == 0);
             _barUnVierfy.Enabled = _isVerify;
             gridView1.OptionsBehavior.Editable = !_isVerify;

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
            if (Save(false))
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
        private bool Save(bool IsVerify)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSBI.AcceptChanges();
            if (_depotID == 0||_companyID==0)
            {
                XtraMessageBox.Show("请选择调出、调入仓库！");
                return false;
            }

            dtSB.Rows[0]["DepotID"] = _depotID;
            dtSB.Rows[0]["LastMoney"] = 0;
            dtSB.Rows[0]["BackMoney"] = 0;
            dtSB.Rows[0]["Remark"] = _ltRemark.val;
            dtSB.Rows[0]["Money"] = 0;
            dtSB.Rows[0]["CompanyID"] = _companyID;
            dtSB.Rows[0]["DataTime"] = _ldDate.val;
            dtSB.Rows[0]["State"] = (int)Enums.TableType.StorageAllocation;
            dtSB.Rows[0]["A"] = 1;
            if (IsVerify)
            {
                dtSB.Rows[0]["IsVerify"] = 3;
                dtSB.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
                dtSB.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
            }
            if (_MainID == 0)
            {
                dtSB.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.StorageAllocation, 0 });
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
            DateTime dtNow = BasicClass.GetDataSet.GetDateTime();
            for (int i = 0; i < dtSBI.Rows.Count; i++)
            {
                int a = int.Parse(dtSBI.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    if (Convert.ToInt32(dtSBI.Rows[i]["Amount"]) > 0)
                    {
                        dtSBI.Rows[i]["MainID"] = _MainID;
                        dtSBI.Rows[i]["LastTime"] = dtNow;
                        dtt.Clear();
                        dtt.Rows.Add(dtSBI.Rows[i].ItemArray);
                        if (a == 3)//新增
                        {
                            dtSBI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllSBI, dtt);
                            dtSBI.Rows[i]["A"] = 1;
                        }
                        else if (a == 2)//修改
                        {
                            BasicClass.GetDataSet.UpData(bllSBI, dtt);
                            dtSBI.Rows[i]["A"] = 1;
                        }
                        else if (a == 4)//删除
                        {
                            if (Convert.ToInt32(dtSBI.Rows[i]["ID"]) > 0)
                            {
                                BasicClass.GetDataSet.GetDS(bllSBI, "Delete", new object[] { Convert.ToInt32(dtSBI.Rows[i]["ID"]) });

                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dtSBI.Rows[i]["ID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(bllSBI, "Delete", new object[] { Convert.ToInt32(dtSBI.Rows[i]["ID"]) });
                            dtSBI.Rows[i]["A"] = 5;
                        }

                    }
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
            if (e.Column == _coAmount)
            {
                try
                {
                    decimal am = Convert.ToDecimal(e.Value);
                    if(am>Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coNeedAmount)))
                    {
                        XtraMessageBox.Show("调拨数量超过库存数量！");
                        gridView1.SetFocusedValue(gridView1.GetFocusedRowCellValue(_coNeedAmount));
                    }
                }
                catch
                {
                }
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
                    DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, measure, 0, colorOne, colorTwo, brand }).Tables[0];
                    if(dttt.Rows.Count>0)
                    {
                        gridView1.SetFocusedRowCellValue(_coNeedAmount, dttt.Rows[0]["Amount"]);
                        gridView1.SetFocusedRowCellValue(_coStockInfoID, dttt.Rows[0]["ID"]);
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
            //_coMaterielID.OptionsColumn.AllowEdit = _coBrandID.OptionsColumn.AllowEdit = _coColorID.OptionsColumn.AllowEdit =
            //    _coColorOneID.OptionsColumn.AllowEdit = _coColorTwoID.OptionsColumn.AllowEdit = _coSizeID.OptionsColumn.AllowEdit = e.FocusedRowHandle < 0;
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {

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
                        BasicClass.GetDataSet.ExecSql(bllSBI, "DeleteByMain", o);
                        BasicClass.GetDataSet.ExecSql(bllSB, "Delete", o);
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
                        BasicClass.GetDataSet.ExecSql(bllSBI, "Delete", new object[] { id });
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("审核后将做进、出仓处理，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                bool f = _MainID == 0;
                if (Save(true))
                {
                    if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllSB, "CheckLinLiao", new object[] { _MainID })))
                    {
                        XtraMessageBox.Show("有出仓数量超过调出仓库存！");
                        return;
                    }

                    BasicClass.GetDataSet.ExecSql(bllSBI, "VerifyStorageAllocation", new object[] { _MainID, true,_companyID });
                }
                else
                {
                    return;
                }

                ShowView(bs.Position);
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
            _depotID = Convert.ToInt32(_leDepotID.EditValue);
            if (_MainID == 0&&_depotID>0)
            {
                dtSBI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetStorageList", new object[] {_depotID }).Tables[0];
                dtSBI.Columns.Add("LastTime", typeof(DateTime));
            

                gridControl1.DataSource = dtSBI;
            }
        }

        private void _leCompanyID_EditValueChanged(object sender, EventArgs e)
        {
            _companyID = Convert.ToInt32(_leCompanyID.EditValue);
        }

    }
}