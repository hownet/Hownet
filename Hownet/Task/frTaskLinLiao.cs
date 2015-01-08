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

namespace Hownet.Task
{
    public partial class frTaskLinLiao : DevExpress.XtraEditors.XtraForm
    {
        public frTaskLinLiao()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frTaskLinLiao(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        public frTaskLinLiao(DataTable dt)
            : this()
        {
            dtMain = dt;
        }
        int _companyID = 0;
        BindingSource bs = new BindingSource();
        private string bllSB = "Hownet.BLL.StockBack";
        private string bllSBI = "Hownet.BLL.StockBackInfo";
        string bllRL = "Hownet.BLL.RepertoryList";
        string bllSBIL = "Hownet.BLL.StockBackInfoList";
        string bllDep = "Hownet.BLL.Deparment";
        DataTable dtPS = new DataTable();
        DataTable dtPSO = new DataTable();
        DataTable dtDeparment = new DataTable();
        DataTable dtTaskList = new DataTable();
        DataTable dtJGC = new DataTable();


        DataTable dtSBIL = new DataTable();
        DataTable dtRL = new DataTable();
        DataTable dtDepInfo = new DataTable();

        bool _IsVerify = false;
        //decimal price = 0;
        //decimal money = 0;
        decimal amount = 0;
        decimal _depotAmount = 0;
        int _depotID = 0;
        int _taskID = 0;
        int _rowCount = 0;
        int _deparmentTypeID = 1;
        bool _IsUseQR = false;
        int _OtherTypeID = 0;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            if (dtMain.Rows.Count > 0)
            {
                bs.DataSource = dtMain;
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
            }
            else if (_MainID > 0)
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                bar1.Visible = false;
            }
            else
            {
                InData();
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
            }
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _barVerify.Enabled = _barUnVierfy.Enabled = _brAddNew.Enabled = _barDel.Enabled = _brSave.Enabled = false;
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
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVierfy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            lookUpEdit3.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];

            DataTable dtD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "仓库" }).Tables[0];
          _leDepot  .Properties.DataSource = dtD;
          _leDepot.EditValue = 0;

            dtDeparment = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeLists", new object[] { "缝制" }).Tables[0];
            lookUpEdit2.Properties.DataSource = dtDeparment;
            lookUpEdit2.EditValue = 0;
            //if (BasicClass.BasicFile.liST[0].Sell4Depot)
            //    _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            dtJGC = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0];

            dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "" }).Tables[0];
            dtTaskList.DefaultView.RowFilter = "";
            lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
            DataTable dtTem = BasicClass.GetDataSet.GetBySql("Select Value From  OtherType Where  (Name='物料使用条码出入仓')");
            if (dtTem.Rows.Count > 0)
            {
                _IsUseQR = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
            }
            if (_IsUseQR)
            {
               // _coAmount.ColumnEdit = _reBEAmount;
            }
            textEdit1.Visible = _IsUseQR;
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)Enums.TableType.TaskLinLiao }).Tables[0];
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
                dr["LastMoney"] = 0;
                dr["BackMoney"] = 0;
                dr["BackDate"] = DateTime.Parse("1900-1-1");
                dr["TaskID"] = _taskID = 0;
                dr["DeparmentType"] = 0;
                dr["OtherTypeID"]=0;
                //  money = 0;
                dtPS.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            checkEdit1.Checked = Convert.ToInt32(dtPS.Rows[0]["DeparmentType"]) == 3;

            _IsVerify = (int.Parse((dtPS.Rows[0]["IsVerify"]).ToString()) == 3);
            _ldDate.val = dtPS.Rows[0]["DataTime"];
            lookUpEdit2.EditValue = _companyID = int.Parse(dtPS.Rows[0]["CompanyID"].ToString());
            _leDepot.EditValue = _depotID = int.Parse(dtPS.Rows[0]["DepotID"].ToString());
            //  money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
            _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
            _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
            dtPSO = BasicClass.GetDataSet.GetDS(bllSBI, "GetLinLiaoInfo", new object[] { _MainID }).Tables[0];
            dtPSO.Columns.Add("LastTime", typeof(DateTime));
            gridControl1.DataSource = null;
            gridControl1.DataSource = dtPSO;
            lookUpEdit1.EditValue = _taskID = Convert.ToInt32(dtPS.Rows[0]["TaskID"]);
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = _coDepotAmount.Visible = !_IsVerify;
            _brAddNew.Enabled = (_MainID > 0);
            _barUnVierfy.Enabled = _IsVerify;
            SetColumnsReadOnly();
            _coExcessAmount.Visible = !_IsVerify;
            dtSBIL = BasicClass.GetDataSet.GetDS(bllSBIL, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            if(!_IsVerify)
            {

                DataRow dr = dtPSO.NewRow();
                for (int i = 0; i < dtPSO.Columns.Count; i++)
                {
                    if (dtPSO.Columns[i].DataType == System.Type.GetType("System.Int32"))
                        dr[i] = 0;
                    else if (dtPSO.Columns[i].DataType == System.Type.GetType("System.String"))
                        dr[i] = string.Empty;
                    dr["LastTime"] = Convert.ToDateTime("1900-1-1");
                    dr["A"] = 3;
                }
                dtPSO.Rows.Add(dr);
                dtPSO.Rows.Add(dr.ItemArray);
            }
            _rowCount = gridView1.RowCount;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
            lookUpEdit1.Enabled = lookUpEdit2.Enabled = (_MainID == 0);
            _leDepot.Enabled = (_MainID == 0);
            SetMaterielName();
            _ltRemark.Focus();
            checkEdit1.Visible = _MainID == 0;
          //  lookUpEdit3.EditValue = 0;
            textEdit1.Visible = !_IsVerify;
            _coSBILDepotInfoID.ColumnEdit = BaseForm.RepositoryItem._reDepotInfo(_depotID);
            if (gridView1.FocusedRowHandle > -1)
            {
                int _infoID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                dtSBIL.DefaultView.RowFilter = "(InfoID=" + _infoID + ")";
                gridControl2.DataSource = dtSBIL.DefaultView;
            }
           lookUpEdit1.Enabled= lookUpEdit3.Enabled = _MainID == 0;
           _OtherTypeID = Convert.ToInt32(dtPS.Rows[0]["OtherTypeID"]);
           if (_OtherTypeID < 2)
               radioButton1.Checked = true;
           else if (_OtherTypeID == 2)
               radioButton2.Checked = true;
           else if (_OtherTypeID == 3)
               radioButton3.Checked = true;
        }
        private void SetColumnsReadOnly()
        {
            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if ( gridView1.VisibleColumns[i] != _coAmount)
                {
                    gridView1.VisibleColumns[i].OptionsColumn.AllowEdit = !_IsVerify;
                }
            }
            _coExcessAmount.OptionsColumn.AllowEdit = false;
            _coNeedAmount.OptionsColumn.AllowEdit = false;
            _coDepotAmount.OptionsColumn.AllowEdit = false;
            _coNotAllotAmount.OptionsColumn.AllowEdit = false;
            _coOutAmount.OptionsColumn.AllowEdit = false;
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
            if (_taskID == 0)
            {
                if (DialogResult.No == XtraMessageBox.Show("没有选择生产单，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return false;
                }
            }
            bool t = false;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToInt32(gridView1.GetRowCellValue(i, _coMaterielID)) > 0 && Convert.ToInt32(gridView1.GetRowCellValue(i, _coMListID)) == 0)
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
            if (radioButton1.Checked)
                _OtherTypeID = 1;
            else if (radioButton2.Checked)
                _OtherTypeID = 2;
            else if (radioButton3.Checked)
                _OtherTypeID = 3;
            dtPS.Rows[0]["LastMoney"] = 0;
            dtPS.Rows[0]["BackMoney"] = 0;
            dtPS.Rows[0]["Remark"] = _ltRemark.val;
            dtPS.Rows[0]["Money"] = 0;// money;
            dtPS.Rows[0]["CompanyID"] = _companyID;
            dtPS.Rows[0]["DataTime"] = _ldDate.val;
            dtPS.Rows[0]["State"] = (int)Enums.TableType.TaskLinLiao;
            dtPS.Rows[0]["A"] = 1;
            dtPS.Rows[0]["DepotID"] = _depotID;
            dtPS.Rows[0]["TaskID"] = _taskID;
            dtPS.Rows[0]["OtherTypeID"] = _OtherTypeID;
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
                if (dtPSO.Rows[i]["Amount"].ToString() != string.Empty && Convert.ToDecimal(dtPSO.Rows[i]["Amount"]) != 0)
                {
                    a = int.Parse(dtPSO.Rows[i]["A"].ToString());
                    if (a > 1)
                    {

                        dtPSO.Rows[i]["MainID"] = _MainID;
                        dtt.Clear();
                        dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllSBI, dtt);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(bllSBI, dtt);
                        }
                        dtPSO.Rows[i]["A"] = 1;
                    }
                }
            }
            dtt = dtSBIL.Clone();
            for (int i = 0; i < dtSBIL.Rows.Count;i++ )
            {
                a = Convert.ToInt32(dtSBIL.Rows[i]["A"]);
                if(a==2)
                {
                    dtt.Rows.Clear();
                    dtt.Rows.Add(dtSBIL.Rows[i].ItemArray);
                    BasicClass.GetDataSet.UpData(bllSBIL, dtt);
                    dtSBIL.Rows[i]["A"] = 1;
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
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "0")
                    gridView1.SetFocusedRowCellValue(_coA, 3);
            }
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtPSO.NewRow();
                for (int i = 0; i < dtPSO.Columns.Count; i++)
                {
                    if (dtPSO.Columns[i].DataType == System.Type.GetType("System.Int32"))
                        dr[i] = 0;
                    else if (dtPSO.Columns[i].DataType == System.Type.GetType("System.String"))
                        dr[i] = string.Empty;
                    dr["LastTime"] = Convert.ToDateTime("1900-1-1");
                    dr["A"] = 3;
                }
                dtPSO.Rows.Add(dr);
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
                    if (gridView1.GetFocusedRowCellDisplayText(_coDepotAmount).Trim() != string.Empty)
                    {
                        if (gridView1.GetFocusedRowCellValue(_coDepotAmount).ToString().Trim() == string.Empty || amount > (decimal.Parse(gridView1.GetFocusedRowCellValue(_coDepotAmount).ToString())) + Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coExcessAmount)))
                        {
                            XtraMessageBox.Show("领料数量超过现有库存数量！");
                            gridView1.SetFocusedRowCellValue(_coAmount, gridView1.GetFocusedRowCellValue(_coDepotAmount));
                        }
                    }
                    else
                    {
                        if (amount > Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coExcessAmount)))
                        {
                            XtraMessageBox.Show("领料数量超过现有库存数量！");
                            gridView1.SetFocusedRowCellValue(_coAmount, gridView1.GetFocusedRowCellValue(_coExcessAmount));
                        }
                    }
                    CaicMoney();
                }
            }
            catch (Exception ex)
            {
            }
            if (e.Column == _coPrice)
            {
                CaicMoney();
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
            if ((e.Column == _coMaterielID || e.Column == _coColorID || e.Column == _coSizeID || e.Column == _coMeasureID || e.Column == _coColorOneID ||
                e.Column == _coColorTwoID) && e.Value.ToString() != "0")
                CheckMateriel(gridView1.GetFocusedRowCellValue(_coMaterielID), gridView1.GetFocusedRowCellValue(_coColorID),
                    gridView1.GetFocusedRowCellValue(_coSizeID), gridView1.GetFocusedRowCellValue(_coMeasureID),
                    gridView1.GetFocusedRowCellValue(_coColorOneID), gridView1.GetFocusedRowCellValue(_coColorTwoID));
        }
        private void CaicMoney()
        {
            decimal _amount = 0;
            decimal _price = 0;
            try
            {
                _amount = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coAmount));
                _price = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coPrice));
                gridView1.SetFocusedRowCellValue(_coMoney, _amount * _price);
            }
            catch
            {
                gridView1.SetFocusedRowCellValue(_coMoney, 0);
            }
        }
        private void CheckMateriel(object mat, object color, object size, object MeasureID, object ColorOneID, object ColorTwoID)
        {
            if (mat == null || mat == DBNull.Value)
                mat = 0;
            if (color == null || color == DBNull.Value)
                color = 0;
            if (size == null || size == DBNull.Value)
                size = 0;
            if (MeasureID == null || MeasureID == DBNull.Value)
                MeasureID = 0;
            if (ColorOneID == null || ColorOneID == DBNull.Value)
                ColorOneID = 0;
            if (ColorTwoID == null || ColorTwoID == DBNull.Value)
                ColorTwoID = 0;
            if (gridView1.FocusedRowHandle > -1)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i != gridView1.FocusedRowHandle)
                    {
                        if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && gridView1.GetRowCellValue(i, _coColorID).Equals(color)
                            && gridView1.GetRowCellValue(i, _coSizeID).Equals(size) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(MeasureID)
                            && gridView1.GetRowCellValue(i, _coColorOneID).Equals(ColorOneID) && gridView1.GetRowCellValue(i, _coColorTwoID).Equals(ColorTwoID))
                        {
                            //gridView1.SetFocusedRowCellValue(_coMaterielID, _oldMat);

                            XtraMessageBox.Show("同一领料单中，不能有多个相同记录！");
                            SendKeys.Send("{Esc}");
                            return;
                        }
                    }
                }
            }
            DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, MeasureID, 0, ColorOneID, ColorTwoID, 0,0 }).Tables[0];
            if (dttt.Rows.Count > 0)
            {
                _depotAmount = Convert.ToDecimal(dttt.Rows[0]["Amount"]);
                gridView1.SetFocusedRowCellValue(_coDepotAmount, _depotAmount);
                gridView1.SetFocusedRowCellValue(_coStockInfoID, dttt.Rows[0]["ID"]);
                gridView1.SetFocusedRowCellValue(_coDemandID, 0);
                gridView1.SetFocusedRowCellValue(_coMListID, dttt.Rows[0]["MListID"]);
            }
            else
            {
                gridView1.SetFocusedRowCellValue(_coDepotAmount, 0);
                gridView1.SetFocusedRowCellValue(_coStockInfoID, 0);
                gridView1.SetFocusedRowCellValue(_coDemandID, 0);
                gridView1.SetFocusedRowCellValue(_coMListID, 0);
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;
            if (_IsVerify)
            {
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                if (e.FocusedRowHandle == gridView1.RowCount - 1)
                {
                    gridView1.OptionsBehavior.Editable = false;
                }
                else
                {
                    gridView1.OptionsBehavior.Editable = true;
                    for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
                    {
                        if (gridView1.GetFocusedRowCellValue(_coStockInfoID) != DBNull.Value)
                        gridView1.VisibleColumns[i].OptionsColumn.AllowEdit = (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coDemandID).ToString()) == 0);
                    }

                    _coMeasureID.OptionsColumn.AllowEdit = false;
                    _coMoney.OptionsColumn.AllowEdit = false;
                    _coAmount.OptionsColumn.AllowEdit = true;
                    _coIsSelect.OptionsColumn.AllowEdit = true;
                }
            }
            int _infoID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            dtSBIL.DefaultView.RowFilter = "(InfoID=" + _infoID + ")";
            gridControl2.DataSource = dtSBIL.DefaultView;
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            //if (gridView1.RowCount > 0 && _rowCount < gridView1.RowCount)
            //{
            //    int r = gridView1.RowCount - 1;
            //    object o = gridView1.GetRowCellValue(r, _coMaterielID);
            //    object b = gridView1.GetRowCellValue(r, _coColorID);
            //    object s = gridView1.GetRowCellValue(r, _coSizeID);
            //    object m = gridView1.GetRowCellValue(r, _coMeasureID);
            //    object t = gridView1.GetRowCellValue(r, _coStringTaskID);
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
            //                    && gridView1.GetRowCellValue(i, _coSizeID).Equals(s) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(m)
            //                    &&gridView1.GetRowCellValue(i,_coStringTaskID).Equals(t))
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

            //}
            _rowCount = gridView1.RowCount;
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
                        BasicClass.GetDataSet.ExecSql(bllSBI, "DeleteByMain", o);
                        BasicClass.GetDataSet.ExecSql(bllSB, "Delete", o);
                        BasicClass.GetDataSet.ExecSql(bllSBIL, "DeleteByMainID", o);
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
                        BasicClass.GetDataSet.ExecSql(bllSBI, "Delete", new object[] { id });
                        BasicClass.GetDataSet.ExecSql(bllSBIL, "DeleteByInfoID", new object[] { id });
                    }
                    this.gridView1.RowCountChanged -= new System.EventHandler(this.gridView1_RowCountChanged);
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
                    int _infoID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                    dtSBIL.DefaultView.RowFilter = "(InfoID=" + _infoID + ")";
                    gridControl2.DataSource = dtSBIL.DefaultView;
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!Save())
                return;
            //if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllStockBack, "CheckLinLiao", new object[] { _MainID })))
            //{
            //    XtraMessageBox.Show("出库数量超过库存！");
            //    return;
            //}
            dtPS.Rows[0]["IsVerify"] = 3;
            dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Today;
            BasicClass.GetDataSet.UpData(bllSB, dtPS);
            //   BasicClass.GetDataSet.ExecSql(bllSB, "Verify", new object[] { _MainID, false, _depotID });
            BasicClass.GetDataSet.ExecSql(bllSBI, "VerifyLinLiao", new object[] { _MainID, false,_deparmentTypeID });
            ////   BasicClass.GetDataSet.ExecSql(bllSB, "UpDemand", new object[] { _MainID, true });
            //gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //_barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = false;
            //_brAddNew.Enabled = _barUnVierfy.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            //_coExcessAmount.Visible = false;
            //dtPSO = BasicClass.GetDataSet.GetDS(blSBI, "GetLinLiaoInfo", new object[] { _MainID }).Tables[0];
            //dtPSO.Columns.Add("LastTime", typeof(DateTime));
            //gridControl1.DataSource = dtPSO;
            //_IsVerify = true;
            //SetColumnsReadOnly();
            if (Convert.ToInt32(dtPS.Rows[0]["DeparmentType"]) == 3)
            {
                decimal chang = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                  //  chang += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
                    object o = gridView1.GetRowCellValue(i, _coMoney);
                    if (o != DBNull.Value)
                        chang += Convert.ToDecimal(o);
                }
                if (chang > 0)
                {
                    string blCL = "Hownet.BLL.CompanyLog";
                   // string blMIOO = "Hownet.BLL.MoneyInOrOut";
                    //DataTable dttt = BasicClass.GetDataSet.GetDS(blCL, "GetProcessingBackLastMoney", new object[] { _companyID }).Tables[0];
                    //decimal last = 0;
                    //if (dttt.Rows.Count > 0)
                    //    last = decimal.Parse(dttt.Rows[0]["Money"].ToString());

                    decimal last = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(blCL, "GetProcessingBackLastMoney", new object[] { _companyID }));
                    DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
                    DataRow dr = dtCL.NewRow();
                    dr["ID"] = 0;
                    dr["CompanyID"] = _companyID;
                    dr["DateTime"] = dtPS.Rows[0]["DataTime"];
                    dr["LastMoney"] = last;
                    dr["ChangMoney"] = chang;
                    dr["Money"] = last - chang;
                    dr["TypeID"] = (int)BasicClass.Enums.MoneyTableType.ProcessingLinLiao;
                    dr["TableID"] = _MainID;
                    dr["NowMoneyTypeID"] = 0;
                    dr["NowMoney"] = 0;
                    dr["NowReta"] = 1;
                    dr["A"] = 1;
                    dtCL.Rows.Add(dr);
                    BasicClass.GetDataSet.Add(blCL, dtCL);
                }
            }
            ShowView(bs.Position);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtPS.Rows[0]["IsVerify"] = 0;
            dtPS.Rows[0]["VerifyMan"] = 0;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            BasicClass.GetDataSet.UpData(bllSB, dtPS);
            BasicClass.GetDataSet.ExecSql(bllSBI, "VerifyLinLiao", new object[] { _MainID, true, _deparmentTypeID });
            //// BasicClass.GetDataSet.ExecSql(bllSB, "UpDemand", new object[] { _MainID, false });
            //_barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = true;
            //_brAddNew.Enabled = _barUnVierfy.Enabled = false;
            //gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            //_coExcessAmount.Visible = true;
            //_IsVerify = false;
            //SetColumnsReadOnly();
            ShowView(bs.Position);
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
         //   GetMLID(int MaterielID, int ColorID, int ColorOneID, int ColorTwoID, int SizeID, int BrandID, int MeasureID,int SpecID)
            int _MaterielID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID));
            int _ColorID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coColorID));
            int _ColorOneID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coColorOneID));
            int _ColorTwoID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coColorTwoID));
            int _SizeID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coSizeID));
            int _BrandID=0;
            int _MeasureID=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMeasureID));
            int _SpecID=0;
            int _MLID = Convert.ToInt32(BasicClass.GetDataSet.GetOne("Hownet.BLL.MaterielList", "GetMLID", new object[] { _MaterielID, _ColorID, _ColorOneID, _ColorTwoID, _SizeID, _BrandID, _MeasureID, _SpecID }));

            if (_depotID == 0)
            {
                XtraMessageBox.Show("请先选择收货仓库");
                return;
            }
            if(_companyID==0)
            {
                XtraMessageBox.Show("请先选择领料部门");
                return;
            }
            if (_MainID == 0)
            {
                dtPS.Rows[0]["LastMoney"] = 0;
                dtPS.Rows[0]["BackMoney"] = 0;
                dtPS.Rows[0]["Remark"] = _ltRemark.val;
                dtPS.Rows[0]["Money"] = 0;// money;
                dtPS.Rows[0]["CompanyID"] = _companyID;
                dtPS.Rows[0]["DataTime"] = _ldDate.val;
                dtPS.Rows[0]["State"] = (int)Enums.TableType.TaskLinLiao;
                dtPS.Rows[0]["A"] = 1;
                dtPS.Rows[0]["DepotID"] = _depotID;
                dtPS.Rows[0]["TaskID"] = _taskID;
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.TaskLinLiao, 0 });
                dtMain.Rows[bs.Position]["ID"] = dtPS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSB, dtPS);
                _leDepot.Enabled = false;
                lookUpEdit1.Enabled = false;
            }
            int _rows = gridView1.FocusedRowHandle;
            int _infoID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            if (_infoID == 0)
            {
                DataTable dtTem = dtPSO.Clone();
                dtPSO.Rows[_rows]["MainID"] = _MainID;
                dtTem.Rows.Add(dtPSO.Rows[_rows].ItemArray);

                dtPSO.Rows[_rows]["ID"] = _infoID = BasicClass.GetDataSet.Add(bllSBI, dtTem);
                dtPSO.Rows[_rows]["A"] = 2;
            }

            BasicClass.cResult crAI = new cResult();
            crAI.TextChanged += crAI_TextChanged;
            Form fr = new frLinLiaoInfoList(_MLID, _depotID, _infoID, _MainID, _IsVerify, crAI);
            fr.ShowDialog();
        }

        void crAI_TextChanged(string s)
        {
            int _rows = gridView1.FocusedRowHandle;
            int _infoID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            decimal _amount = 0;
            try
            {
                _amount = Convert.ToDecimal(dtPSO.Rows[_rows]["Amount"]);
            }
            catch
            {

            }
            dtPSO.Rows[_rows]["Amount"] = Convert.ToDecimal(s) + _amount;
            dtPSO.Rows[_rows]["A"] = 1;
            DataTable dtTem = dtPSO.Clone();
            dtTem.Rows.Add(dtPSO.Rows[_rows].ItemArray);
            BasicClass.GetDataSet.UpData(bllSBI, dtTem);
            gridView1.SetFocusedValue(s);
        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!_IsVerify)
            //{
            //    XtraMessageBox.Show("请审核后再打印单据！");
            //    return;
            //}

            Print(false);
        }
        private void Print(bool IsA4)
        {
           // int _id = int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllSysTem, "GetMaxId", null).ToString()) - 1;
            DataSet ds = new DataSet();// BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSysTem, "GetList", new object[] { "(ID=" + _id + ")" });
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    XtraMessageBox.Show("请完善公司信息！");
            //    //Form fr = new BaseForm.UserBaseSetForm();
            //    //fr.ShowDialog();
            //    return;
            //}
            ds.Tables.Add();
            ds.Tables[0].TableName = "Main";
            ds.Tables["Main"].Columns.Add("ComName", typeof(string));
            ds.Tables["Main"].Columns.Add("Date", typeof(string));
            ds.Tables["Main"].Columns.Add("Num", typeof(string));
            ds.Tables["Main"].Columns.Add("UserName", typeof(string));
            //ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("TaskNum", typeof(string));
            ds.Tables["Main"].Columns.Add("DepotName", typeof(string));
            ds.Tables["Main"].Columns.Add("TrueName", typeof(string));
            ds.Tables["Main"].Columns.Add("MaterielName", typeof(string));
            ds.Tables["Main"].Columns.Add("审核", typeof(string));
            ds.Tables["Main"].Columns.Add("制单", typeof(string));
            ds.Tables["Main"].Columns.Add("TypeID", typeof(int));
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["ComName"] = lookUpEdit2.Text;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;
            ds.Tables[0].Rows[0]["TaskNum"] = lookUpEdit1.Text;
            ds.Tables[0].Rows[0]["DepotName"] = _leDepot.Text;
            ds.Tables[0].Rows[0]["TypeID"] = _OtherTypeID;
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            ds.Tables[0].Rows[0]["TrueName"] = dtUser.Select("(ID=" + dtPS.Rows[0]["FillMan"] + ")")[0]["TrueName"];
            ds.Tables[0].Rows[0]["MaterielName"] = lookUpEdit3.Text;
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
            dt.Columns.Add("GuiGe", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("NeedAmount", typeof(string));
            dt.Columns.Add("OutAmount", typeof(string));
            dt.Columns.Add("NotAmount", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Money", typeof(decimal));
            dt.Columns.Add("QR", typeof(string));
            DataRow[] drs;
            string _guiGe = string.Empty;
            string _QR = string.Empty;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                try
                {
                    if (_IsVerify)
                    {
                        _guiGe = string.Empty;
                        DataRow dr = dt.NewRow();
                        dr[0] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
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
                        dr[2] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount));
                        dr[3] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                        dr[4] = gridView1.GetRowCellDisplayText(i, _coRemark);
                        dr[5] = gridView1.GetRowCellDisplayText(i, _coNeedAmount);
                        dr[6] = gridView1.GetRowCellDisplayText(i, _coOutAmount);
                        dr[7] = gridView1.GetRowCellDisplayText(i, _coNotAllotAmount);
                        dr[8] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice));
                        dr[9] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
                        
                        drs = dtSBIL.Select("(InfoID=" + Convert.ToInt32(gridView1.GetRowCellValue(i,_coID)) + ")");
                        if(drs.Length>0)
                        {
                            _QR = string.Empty;
                            for(int j=0;j<drs.Length;j++)
                            {
                                _QR += drs[j]["QRID"].ToString() + ":" +Convert.ToDouble( drs[j]["NowAmount"]).ToString() + "；";
                            }
                            dr[10] = _QR;
                        }
                        dt.Rows.Add(dr);
                    }
                    else if (Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsSelect)))
                    {
                        try
                        {
                            _guiGe = string.Empty;
                            DataRow dr = dt.NewRow();
                            dr[0] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
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
                            dr[2] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount));
                            dr[3] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                            dr[4] = gridView1.GetRowCellDisplayText(i, _coRemark);
                            dr[5] = gridView1.GetRowCellDisplayText(i, _coNeedAmount);
                            dr[6] = gridView1.GetRowCellDisplayText(i, _coOutAmount);
                            dr[7] = gridView1.GetRowCellDisplayText(i, _coNotAllotAmount);
                            dr[8] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice));
                            dr[9] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
                            drs = dtSBIL.Select("(InfoID=" + Convert.ToInt32(gridView1.GetRowCellValue(i,_coID)) + ")");
                            if (drs.Length > 0)
                            {
                                _QR = string.Empty;
                                for (int j = 0; j < drs.Length; j++)
                                {
                                    _QR += drs[j]["QRID"].ToString() + ":" +Convert.ToDouble( drs[j]["NowAmount"]).ToString() + "；";
                                }
                                dr[10] = _QR;
                            }
                            dt.Rows.Add(dr);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch
                {
                }
            }
            for (int i = dt.Rows.Count; i < 10; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintLinLiao(ds,IsA4);
        }
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coIsSelect)
                gridView1.SetFocusedValue(e.Value);
        }

        private void _leNum_EditValueChanged(object val, string text)
        {
            if (_MainID == 0)
            {
                _taskID = Convert.ToInt32(val);
                GetNeedAmount();
                SetMaterielName();
            }
        }
        private void SetMaterielName()
        {
            if (_taskID > 0)
            {
                DataRow[] drs = dtTaskList.Select("(ID=" + _taskID + ")");
                if (drs.Length > 0)
                {
               //     label2.Text = drs[0]["MaterielName"].ToString();
                    try
                    {
                        if (_MainID > 0)
                            lookUpEdit3.EditValue = Convert.ToInt32(drs[0]["MaterielID"]);
                           
                    }
                    catch
                    {

                    }
                }
            }
            if(_MainID==0)
                 lookUpEdit3.EditValue = 0;
        }
        private void GetNeedAmount()
        {
            if (_MainID > 0)
                return;
       
            if (_taskID == 0 || _depotID == 0 || _deparmentTypeID == 0)
            {
                try
                {
                    if (dtPSO.Columns.Count == 0)
                        return;
                    if (dtPSO.Rows.Count>0&& Convert.ToInt32(dtPSO.Rows[dtPSO.Rows.Count - 1]["MaterielID"]) == 0)
                        return;
                    DataRow dr = dtPSO.NewRow();
                    for (int i = 0; i < dtPSO.Columns.Count; i++)
                    {
                        if (dtPSO.Columns[i].DataType == System.Type.GetType("System.Int32"))
                            dr[i] = 0;
                        else if (dtPSO.Columns[i].DataType == System.Type.GetType("System.String"))
                            dr[i] = string.Empty;
                        dr["LastTime"] = Convert.ToDateTime("1900-1-1");
                        dr["A"] = 3;
                    }
                    dtPSO.Rows.Add(dr.ItemArray);
                    dtPSO.Rows.Add(dr.ItemArray);
                }
                catch (Exception ex)
                {

                }
                return;
            }

            dtPSO = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBackInfo, "GetTemLinLiaoInfo", new object[] { _taskID, _depotID, _deparmentTypeID }).Tables[0];
            dtPSO.Columns.Add("SpecID",typeof(int));
            dtPSO.Columns.Add("MaterielName");
            dtPSO.Columns.Add("ColorName");
            dtPSO.Columns.Add("ColorOneName");
            dtPSO.Columns.Add("ColorTwoName");
            dtPSO.Columns.Add("SizeName");
            dtPSO.Columns.Add("BrandName");
            dtPSO.Columns.Add("SupplierID",typeof(int));
            dtPSO.Columns.Add("SupplierName");
            dtPSO.Columns.Add("SupplierSN");
            dtPSO.Columns.Add("SpecName");
            
            dtPSO.Columns.Add("LastTime", typeof(DateTime));
           
            
           
            DataRow drr = dtPSO.NewRow();
            for (int i = 0; i < dtPSO.Columns.Count; i++)
            {
                if (dtPSO.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    drr[i] = 0;
                else if (dtPSO.Columns[i].DataType == System.Type.GetType("System.String"))
                    drr[i] = string.Empty;
                drr["LastTime"] = Convert.ToDateTime("1900-1-1");
                drr["A"] = 3;
            }
            dtPSO.Rows.Add(drr.ItemArray);
            dtPSO.Rows.Add(drr.ItemArray);

            dtPSO.AcceptChanges();
            gridControl1.DataSource = dtPSO;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            _taskID = Convert.ToInt32(lookUpEdit1.EditValue);
            if (_MainID == 0)
            {
                GetNeedAmount();
              //  SetMaterielName();
            }
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            _companyID = Convert.ToInt32(lookUpEdit2.EditValue);
            if (!checkEdit1.Checked)
            {
                if (_companyID > 0)
                {
                    DataRow[] drs=dtDeparment.Select("(ID=" + _companyID + ")");
                    if (drs.Length>0&&drs [0]["TypeName"].ToString() == "缝制")
                    {
                        dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "(DeparmentID=" + _companyID + ") And (DeparmentType=0)" }).Tables[0];
                        // dtTaskList.DefaultView.RowFilter = "(DeparmentID=" + _companyID + ")";
                        lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                        lookUpEdit1.EditValue = _taskID = 0;
                      //  label2.Text = string.Empty;
                        dtPSO.Rows.Clear();
                        dtPSO.AcceptChanges();
                        _deparmentTypeID = 1;
                    }
                    else
                    {
                        dtTaskList = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetNumListForLinLiao", new object[] { "" }).Tables[0];
                        lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                        lookUpEdit1.EditValue = _taskID = 0;
                        //label2.Text = string.Empty;
                        dtPSO.Rows.Clear();
                        dtPSO.AcceptChanges();
                        _deparmentTypeID = 2;
                    }
                }
                else
                {
                    dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "" }).Tables[0];
                    // dtTaskList.DefaultView.RowFilter = "(DeparmentID=" + _companyID + ")";
                    lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                    lookUpEdit1.EditValue = _taskID = 0;
                   // label2.Text = string.Empty;
                    dtPSO.Rows.Clear();
                    dtPSO.AcceptChanges();
                    _deparmentTypeID = 1;
                }
            }
            else
            {
                dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "(DeparmentID=" + _companyID + ") And (DeparmentType=3)" }).Tables[0];
                // dtTaskList.DefaultView.RowFilter = "(DeparmentID=" + _companyID + ")";
                lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                lookUpEdit1.EditValue = _taskID = 0;
               // label2.Text = string.Empty;
                dtPSO.Rows.Clear();
                dtPSO.AcceptChanges();
                _deparmentTypeID = 3;
            }

        }

        private void _leDepot_EditValueChanged(object sender, EventArgs e)
        {
            _depotID = Convert.ToInt32(_leDepot.EditValue);
            _coSBILDepotInfoID.ColumnEdit = BaseForm.RepositoryItem._reDepotInfo(_depotID);
            if (lookUpEdit1.Text.Trim() == string.Empty)
                _taskID = 0;
            GetNeedAmount();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                lookUpEdit2.Properties.DataSource = dtJGC;
                dtPS.Rows[0]["DeparmentType"] = _deparmentTypeID = 3;
                
            }
            else
            {
                lookUpEdit2.Properties.DataSource = dtDeparment;
                dtPS.Rows[0]["DeparmentType"] = _deparmentTypeID = 1;
            }
            lookUpEdit2.EditValue = 0;
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            int _matID = Convert.ToInt32(lookUpEdit3.EditValue);
            if (_MainID > 0)
                return;
            if (_matID > 0)
            {
                _companyID = Convert.ToInt32(lookUpEdit2.EditValue);
                if (!checkEdit1.Checked)
                {
                    if (_companyID > 0)
                    {
                        DataRow[] drs = dtDeparment.Select("(ID=" + _companyID + ")");
                        if (drs.Length > 0 && drs[0]["TypeName"].ToString() == "缝制")
                        {
                            dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "(DeparmentID=" + _companyID + ") And (DeparmentType=0) And ( ProductTaskMain.MaterielID=" + _matID + ")" }).Tables[0];
                            // dtTaskList.DefaultView.RowFilter = "(DeparmentID=" + _companyID + ")";
                            lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                            lookUpEdit1.EditValue = _taskID = 0;
                          //  label2.Text = string.Empty;
                            dtPSO.Rows.Clear();
                            dtPSO.AcceptChanges();
                            _deparmentTypeID = 1;
                        }
                        else
                        {
                            dtTaskList = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetNumListForLinLiao", new object[] { "(ProductionPlan.MaterielID=" + _matID + ")" }).Tables[0];
                            lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                            lookUpEdit1.EditValue = _taskID = 0;
                           // label2.Text = string.Empty;
                            dtPSO.Rows.Clear();
                            dtPSO.AcceptChanges();
                            _deparmentTypeID = 2;
                        }
                    }
                    else
                    {
                        dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "( ProductTaskMain.MaterielID=" + _matID + ")" }).Tables[0];
                        // dtTaskList.DefaultView.RowFilter = "(DeparmentID=" + _companyID + ")";
                        lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                        lookUpEdit1.EditValue = _taskID = 0;
                       // label2.Text = string.Empty;
                        dtPSO.Rows.Clear();
                        dtPSO.AcceptChanges();
                        _deparmentTypeID = 1;
                    }
                }
                else
                {
                    dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "(DeparmentID=" + _companyID + ") And (DeparmentType=3) And ( ProductTaskMain.MaterielID=" + _matID + ")" }).Tables[0];
                    // dtTaskList.DefaultView.RowFilter = "(DeparmentID=" + _companyID + ")";
                    lookUpEdit1.Properties.DataSource = dtTaskList.DefaultView;
                    lookUpEdit1.EditValue = _taskID = 0;
                   // label2.Text = string.Empty;
                    dtPSO.Rows.Clear();
                    dtPSO.AcceptChanges();
                    _deparmentTypeID = 3;
                }

            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsVerify)
                return;
            for (int i = 0; i < gridView1.RowCount-2; i++)
            {
                
                gridView1.SetRowCellValue(i, _coIsSelect, checkEdit2.Checked);
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print(true);
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (_depotID == 0)
                {
                    XtraMessageBox.Show("请先选择收货仓库");
                    return;
                }
                if (_companyID == 0)
                {
                    XtraMessageBox.Show("请先选择领料部门");
                    return;
                }
                if (_MainID == 0)
                {
                    dtPS.Rows[0]["LastMoney"] = 0;
                    dtPS.Rows[0]["BackMoney"] = 0;
                    dtPS.Rows[0]["Remark"] = _ltRemark.val;
                    dtPS.Rows[0]["Money"] = 0;// money;
                    dtPS.Rows[0]["CompanyID"] = _companyID;
                    dtPS.Rows[0]["DataTime"] = _ldDate.val;
                    dtPS.Rows[0]["State"] = (int)Enums.TableType.TaskLinLiao;
                    dtPS.Rows[0]["A"] = 1;
                    dtPS.Rows[0]["DepotID"] = _depotID;
                    dtPS.Rows[0]["TaskID"] = _taskID;
                    dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.TaskLinLiao, 0 });
                    dtMain.Rows[bs.Position]["ID"] = dtPS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSB, dtPS);
                    _leDepot.Enabled = false;
                    lookUpEdit1.Enabled = false;
                    checkEdit1.Visible = false;
                }
                int _ID = 0;
                try
                {

                    _ID = Convert.ToInt32(textEdit1.Text.Trim());
                    textEdit1.Text = string.Empty;
                    DataTable dtTem = new DataTable();
                    decimal price = 0;
                    decimal money = 0;
                    //if (_ID > 0)
                    //    dtTem = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(StockListID=" + _ID + ")" }).Tables[0];
                    //else
                        dtTem = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(QRID='" + _ID  + "')" }).Tables[0];

                    if (dtTem.Rows.Count == 1)
                    {

                        if (dtSBIL.Select("(NotAmount=" + dtTem.Rows[0]["ID"] + ")").Length > 0)
                        {
                            XtraMessageBox.Show("该物料已被领走");
                            return;
                        }
                        if (Convert.ToDecimal(dtTem.Rows[0]["NotAmount"]) == 0)
                        {
                            XtraMessageBox.Show("该物料已被领走");
                            return;
                        }
                        dtDepInfo = BasicClass.GetDataSet.GetDS(bllDep, "GetList", new object[] { "(ParentID=" + _depotID + ")" }).Tables[0];
                        if (dtDepInfo.Select("(ID=" + dtTem.Rows[0]["DepotInfoID"] + ")").Length > 0)
                        {
                            if (DialogResult.Yes == XtraMessageBox.Show("该物料确认出仓？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                            {
                                int _InfoID = 0;
                                dtSBIL = BasicClass.GetDataSet.GetDS(bllSBIL, "GetList", new object[] { "(BatchNumber=" + dtTem.Rows[0]["BatchNumber"] + ") And (MainID=" + _MainID + ")" }).Tables[0];
                                if (dtSBIL.Rows.Count > 0)//是否已经有明细数据
                                {
                                    _InfoID = Convert.ToInt32(dtSBIL.Rows[0]["InfoID"]);
                                }
                                else
                                {
                                    int _MListID = Convert.ToInt32(dtTem.Rows[0]["BatchNumber"]);
                                    DataRow[] drPSO = dtPSO.Select("(MListID=" + _MListID + ")");
                                    if (drPSO.Length > 0)
                                    {

                                        if (Convert.ToInt32(drPSO[0]["ID"]) == 0)
                                        {
                                            drPSO[0]["MainID"] = _MainID;
                                          
                                            DataTable dtSBITem = dtPSO.Clone();
                                            dtSBITem.Rows.Add(drPSO[0].ItemArray);
                                            drPSO[0]["ID"] = _InfoID = BasicClass.GetDataSet.Add(bllSBI, dtSBITem);
                                            drPSO[0]["A"] = 2;
                                            dtPSO.AcceptChanges();
                                        }

                                    }
                                    else
                                    {
                                        DataTable dtSBITem = dtPSO.Clone();
                                        DataRow drr = dtSBITem.NewRow();
                                        for (int i = 0; i < dtSBITem.Columns.Count; i++)
                                        {
                                            if (dtSBITem.Columns[i].DataType == System.Type.GetType("System.Int32"))
                                                drr[i] = 0;
                                            else if (dtSBITem.Columns[i].DataType == System.Type.GetType("System.String"))
                                                drr[i] = string.Empty;
                                            drr["LastTime"] = Convert.ToDateTime("1900-1-1");
                                            drr["A"] = 3;
                                        }
                                        drr["MainID"] = _MainID;
                                        drr["StockInfoID"] = 0;
                                        int mat, color, size, MeasureID, ColorOneID, ColorTwoID;
                                        DataTable dtML = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielList, "GetList", new object[] { "(ID=" + Convert.ToInt32(dtTem.Rows[0]["BatchNumber"]) + ")" }).Tables[0];
                                        drr["MaterielID"] = mat = Convert.ToInt32(dtML.Rows[0]["MaterielID"]);
                                        drr["ColorID"] = color = Convert.ToInt32(dtML.Rows[0]["ColorID"]);
                                        drr["ColorOneID"] = ColorOneID = Convert.ToInt32(dtML.Rows[0]["ColorOneID"]);
                                        drr["ColorTwoID"] = ColorTwoID = Convert.ToInt32(dtML.Rows[0]["ColorTwoID"]);
                                        drr["SizeID"] = size = Convert.ToInt32(dtML.Rows[0]["SizeID"]);
                                        drr["BrandID"] = dtML.Rows[0]["BrandID"];
                                        drr["DepotMeasureID"] = drr["CompanyMeasureID"] = MeasureID = Convert.ToInt32(dtML.Rows[0]["MeasureID"]);
                                        drr["MListID"] = dtML.Rows[0]["ID"];

                                        //DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, MeasureID, 0, ColorOneID, ColorTwoID, 0 ,0}).Tables[0];
                                        //if (dttt.Rows.Count > 0)
                                        //{
                                        //    drr["PriceAmount"] = Convert.ToDecimal(dttt.Rows[0]["Amount"]);
                                        //    drr["StockInfoID"] = dttt.Rows[0]["ID"];
                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("仓库没有这种空闲材料");
                                        //    return;
                                        //}
                                        //dtSBITem.Rows.Add(drr);
                                        //_InfoID = BasicClass.GetDataSet.Add(bllSBI, dtSBITem);

                                        DataRow[] drs = dtTaskList.Select("(ID=" + _taskID + ")");
                                        int _PlanID = 0;
                                        if (drs.Length > 0)
                                        {
                                            _PlanID = Convert.ToInt32(drs[0]["ParentID"]);
                                        }


                                        DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, MeasureID, 0, ColorOneID, ColorTwoID, 0, _PlanID }).Tables[0];
                                        if (dttt.Rows.Count > 0)
                                        {
                                            drr["ExcessAmount"] = Convert.ToDecimal(dttt.Rows[0]["Amount"]);
                                            drr["CompanyID"] = dttt.Rows[0]["ID"];
                                        }
                                        else
                                        {
                                            // MessageBox.Show("仓库没有这种空闲材料");
                                            //return;


                                            if (_taskID > 0)
                                            {
                                                dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetAmount", new object[] { mat, color, size, _depotID, MeasureID, 0, ColorOneID, ColorTwoID, 0, 0 }).Tables[0];
                                                if (dttt.Rows.Count > 0)
                                                {
                                                    drr["PriceAmount"] = Convert.ToDecimal(dttt.Rows[0]["Amount"]);
                                                    drr["StockInfoID"] = dttt.Rows[0]["ID"];
                                                }
                                                else
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                        dtSBITem.Rows.Add(drr);
                                        _InfoID = BasicClass.GetDataSet.Add(bllSBI, dtSBITem);
                                    }

                                }
                                decimal _amount = 0;
                                DataRow dr = dtSBIL.NewRow();
                                dr["ID"] = 0;
                                dr["A"] = 1;
                                dr["InfoID"] = _InfoID;
                                _amount = Convert.ToDecimal(dtTem.Rows[0]["NotAmount"]);
                                dr["Amount"] = dtTem.Rows[0]["Amount"];
                                dr["Remark"] = string.Empty;
                                dr["IsOk"] = true;
                                dr["BatchNumBer"] = dtTem.Rows[0]["BatchNumBer"];
                                dr["NotAmount"] = dtTem.Rows[0]["NotAmount"];
                                dr["SpecID"] = dtTem.Rows[0]["SpecID"];
                                dr["DepotInfoID"] = dtTem.Rows[0]["DepotInfoID"];
                                dr["MainID"] = _MainID;
                                dr["QRID"] = dtTem.Rows[0]["QRID"];
                                dr["NowAmount"] = dtTem.Rows[0]["NotAmount"];
                                dr["RListID"] = dtTem.Rows[0]["ID"];
                                DataTable dtt = dtSBIL.Clone();
                                dtt.Rows.Add(dr.ItemArray);
                                dtt.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllSBIL, dtt);
                                dtSBIL.Rows.Add(dtt.Rows[0].ItemArray);
                                DataTable dtPSOTem = BasicClass.GetDataSet.GetDS(bllSBI, "GetList", new object[] { "(ID=" + _InfoID + ")" }).Tables[0];
                                dtPSOTem.Rows[0]["NotPriceAmount"]=dtPSOTem.Rows[0]["Amount"] = Convert.ToDecimal(dtPSOTem.Rows[0]["Amount"]) + _amount;
                                
                                try
                                {
                                    if (Convert.ToInt32(dtTem.Rows[0]["StockListID"]) > 0)//是从采购单中直接过来的，就取采购单的单价
                                    {
                                        DataTable dttt = BasicClass.GetDataSet.GetDS(bllSBI, "GetList", new object[] { "(MainID=" + dtTem.Rows[0]["MainID"] + ") And (MListID=" + dtTem.Rows[0]["BatchNumber"] + ")" }).Tables[0];
                                        if (dttt.Rows.Count > 0)
                                        {
                                            object pp, mm;
                                            dtPSOTem.Rows[0]["Price"] = pp = dttt.Rows[0]["Price"];
                                            dtPSOTem.Rows[0]["Money"] = mm = Convert.ToDecimal(dtPSOTem.Rows[0]["Price"]) * Convert.ToDecimal(dtPSOTem.Rows[0]["Amount"]);
                                        }
                                    }
                                    else
                                    {
                                        DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(ID=" + dtTem.Rows[0]["MainID"] + ")" }).Tables[0];
                                        if (dttt.Rows.Count > 0)
                                        {
                                            dtPSOTem.Rows[0]["Price"] = dttt.Rows[0]["Price"];
                                            dtPSOTem.Rows[0]["Money"] = Convert.ToDecimal(dtPSOTem.Rows[0]["Price"]) * Convert.ToDecimal(dtPSOTem.Rows[0]["Amount"]);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                }


                                BasicClass.GetDataSet.UpData(bllSBI, dtPSOTem);
                                DataRow[] drsSBI = dtPSO.Select("(ID=" + _InfoID + ")");


                                if (drsSBI.Length > 0)
                                {
                                    drsSBI[0]["NotPriceAmount"] = drsSBI[0]["Amount"] = dtPSOTem.Rows[0]["Amount"];
                                    drsSBI[0]["Price"] = dtPSOTem.Rows[0]["Price"];
                                    drsSBI[0]["Money"] = dtPSOTem.Rows[0]["Money"];
                                }
                                else
                                {
                                    DataRow drPSO = dtPSO.NewRow();
                                    for (int i = 0; i < dtPSOTem.Columns.Count; i++)
                                    {
                                        try
                                        {
                                            drPSO[dtPSOTem.Columns[i].ColumnName] = dtPSOTem.Rows[0][i];
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }



                                    //  drPSO.ItemArray = dtPSOTem.Rows[0].ItemArray;
                                    dtPSO.Rows.InsertAt(drPSO, 0);
                                }
                                //dtPSO = BasicClass.GetDataSet.GetDS(bllSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                                //DataRow ddr = dtPSO.NewRow();
                                //for (int i = 0; i < dtPSO.Columns.Count; i++)
                                //{
                                //    if (dtPSO.Columns[i].DataType == System.Type.GetType("System.Int32"))
                                //        ddr[i] = 0;
                                //    else if (dtPSO.Columns[i].DataType == System.Type.GetType("System.String"))
                                //        ddr[i] = string.Empty;
                                //    ddr["LastTime"] = Convert.ToDateTime("1900-1-1");
                                //    ddr["A"] = 3;
                                //}
                                //dtPSO.Rows.Add(ddr.ItemArray);
                                //dtPSO.Rows.Add(ddr.ItemArray);
                                gridControl1.DataSource = dtPSO;
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("非所选择仓库的物料！");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("条码号不正确");
                    return;
                }
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coSBILA && Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coSBILA)) == 1)
                gridView2.SetFocusedRowCellValue(_coSBILA, 2);
            if(e.Column==_coSBILNowAmount)
            {
                decimal _amount = 0;
                for(int i=0;i<gridView2.RowCount;i++)
                {
                    _amount += Convert.ToDecimal(gridView2.GetRowCellValue(i, _coSBILNowAmount));
                }
                gridView1.SetFocusedRowCellValue(_coAmount, _amount);
            }
        }

    }
}