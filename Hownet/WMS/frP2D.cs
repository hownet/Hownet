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
using Hownet.BaseContranl;
using BasicClass;

namespace Hownet.WMS
{
    public partial class frP2D : DevExpress.XtraEditors.XtraForm
    {

        public frP2D()
        {

            InitializeComponent();
        }

        DataTable dtMain = new DataTable();
        public frP2D(DataTable dt)
            : this()
        {
            dtMain = dt;
        }
        BindingSource bs = new BindingSource();
        int _MainID = 0;
        int _depotID = 0;
        int _TableTypeID = (int)BasicClass.Enums.TableType.P2D;
        bool t = false;
        DataTable dtP2D = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtDeoptInfo = new DataTable();
        bool _UseDepotInfo = false;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            InData();
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            bs.Position = -1;
            if (bs.Count > 1)
                bs.Position = dtMain.Rows.Count - 1;
            else
                ShowView(0);
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    simpleButton3.Enabled = _barVerify.Enabled = _barUnVerify.Enabled = _brAddNew.Enabled = _barDel.Enabled = _brSave.Enabled = false;
            //    gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
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
                _barUnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='成品需按货架存放')" }).Tables[0];
            if (dtTem.Rows.Count > 0)
            {
                _UseDepotInfo = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
            }
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            if (bs.Position > -1)
                ShowView(bs.Position);
        }
        void ShowData()
        {
            _leDepotID.Par = new object[] { "TypeID=42" };
            _leDepotID.FormName = (int)BasicClass.Enums.TableType.Deparment;
            _coMaterieID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coTaskID.ColumnEdit = BaseForm.RepositoryItem._reTaskNum;
            _leBrandID.FormName = (int)BasicClass.Enums.TableType.Brand;
            _leMaterielID.FormName = (int)BasicClass.Enums.TableType.Product;
            _leBrandID.IsNotCanEdit = _leMaterielID.IsNotCanEdit = false;
            _leDeparment.Par = new object[] { "TypeID=38" };
            _leDeparment.FormName = (int)BasicClass.Enums.TableType.Deparment;
           // lookUpEdit3.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduct2Depot, "GetIDList", new object[] { (int)BasicClass.Enums.TableType.P2D }).Tables[0];
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
                dtP2D = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduct2Depot, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            }
            else
            {
                _MainID = 0;
                dtP2D = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduct2Depot, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtP2D.NewRow();
                dr["ID"] = dr["UpData"] = dr["VerifyMan"] = dr["DepotID"] = _MainID = 0;
                dr["DateTime"] = DateTime.Today;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduct2Depot, "NewNum", new object[] { DateTime.Today });
                dr["A"] = 1;
                dr["DeparmentID"] = dr["DeparmentTypeID"] = 0;
                dtP2D.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            checkEdit1.Checked = _leDeparment.IsNotCanEdit = Convert.ToInt32(dtP2D.Rows[0]["DeparmentTypeID"]) == 3;
            _leDeparment.editVal = dtP2D.Rows[0]["DeparmentID"];
            t = (Convert.ToInt32(dtP2D.Rows[0]["IsVerify"]) > 2);
            checkEdit1.Visible = !t;
            _leDepotID.IsNotCanEdit = t;
            _leDepotID.editVal = _depotID = int.Parse(dtP2D.Rows[0]["DepotID"].ToString());
            _ltNum.val = ((DateTime)(dtP2D.Rows[0]["DateTime"])).ToString("yyyyMMdd") + dtP2D.Rows[0]["Num"].ToString().PadLeft(3, '0');
            _ldDate.val = dtP2D.Rows[0]["DateTime"];
            _ltRemark.val = dtP2D.Rows[0]["Remark"].ToString();
            dtInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduct2DepotInfo, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            dtInfo.DefaultView.RowFilter = "(A<4)";
            gridControl2.DataSource = dtInfo.DefaultView;

            panel2.Visible = _barDel.Enabled = _barEdit.Enabled = _brSave.Enabled = _barVerify.Enabled = !t;
            if (t)
                gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            else
                gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            if (t && p == dtMain.Rows.Count - 1)
                _barUnVerify.Enabled = _brAddNew.Enabled = t;
            else
                _barUnVerify.Enabled = _brAddNew.Enabled = false;
            if (Convert.ToInt32(dtP2D.Rows[0]["IsVerify"]) > 3)
            {
                _barUnVerify.Enabled = false;
            }
            lookUpEdit1.EditValue = 0;
            gridView2.OptionsBehavior.Editable = !t;
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

            if (Save())
                ShowView(bs.Position);
        }
        private bool Save()
        {
            gridView2.CloseEditor();
            gridView2.UpdateCurrentRow();
            dtInfo.AcceptChanges();
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择仓库！");
                return false;
            }
            if (_UseDepotInfo)
            {
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dtInfo.Rows[i]["Amount"]) > 0 && Convert.ToInt32(dtInfo.Rows[i]["DepotInfoID"]) == 0)
                    {
                        XtraMessageBox.Show("请选择存放货位！");
                        return false;
                    }
                }
            }
            dtP2D.Rows[0]["DepotID"] = _depotID;
            dtP2D.Rows[0]["DateTime"] = _ldDate.val;
            dtP2D.Rows[0]["Remark"] = _ltRemark.val;
            dtP2D.Rows[0]["DeparmentID"] = _leDeparment.editVal;
            dtP2D.Rows[0]["TypeID"] = (int)BasicClass.Enums.TableType.P2D;
            if (_MainID == 0)
            {
                dtP2D.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduct2Depot, "NewNum", new object[] { (DateTime)(_ldDate.val) });
                dtP2D.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllProduct2Depot, dtP2D);
                dtMain.Rows[bs.Position]["ID"] = _MainID;
            }
            else
            {
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProduct2Depot, dtP2D);
            }
            DataTable ttt = dtInfo.Clone();
            int a = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                a = int.Parse(dtInfo.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    ttt.Clear();
                    dtInfo.Rows[i]["MainID"] = _MainID;
                    ttt.Rows.Add(dtInfo.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProduct2DepotInfo, ttt);
                    else if (a == 3)
                        dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllProduct2DepotInfo, ttt);
                    else if (a == 4 && Convert.ToInt32(dtInfo.Rows[i]["ID"]) > 0)
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduct2DepotInfo, "Delete", new object[] { Convert.ToInt32(dtInfo.Rows[i]["ID"]) });
                }
                dtInfo.Rows[i]["A"] = 1;
            }
            return true;
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 在入库明细中填写入库数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int _taskID = 0;
            DataTable dtTem = dtInfo.Clone();
            _taskID = Convert.ToInt32(lookUpEdit1.EditValue);
            if (dtInfo.Rows.Count > 0)
            {
                // _taskID = int.Parse(_leNum.editVal.ToString());
                DataRow[] foundRows = dtInfo.Select("(TaskID='" + _taskID + "')");
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
            cResult r = new cResult();
            r.RowChanged += new RowChangedHandler(r_RowChanged);
            Form fr = new P2dTemForm(r, dtTem, _taskID, _MainID);
            fr.ShowDialog();
        }
        /// <summary>
        /// 从入库明细中传回的数量列表
        /// </summary>
        /// <param name="dt"></param>
        void r_RowChanged(DataTable dt)
        {
            bool f = false;
            string _taskID = dt.DefaultView[dt.Rows.Count - 1]["TaskID"].ToString();
            decimal _price = 0;
            if (Convert.ToInt32(_taskID) > 0)
            {
                DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] { "(ID=" + _taskID + ")" }).Tables[0];
                _price = Convert.ToDecimal(dtTem.Rows[0]["Price"]);
            }
            string colorID = "";
            string sizeID = "";
            string colorOneID = string.Empty;
            string colorTwoID = string.Empty; 
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    f = false;
                    colorID = dt.Rows[j]["ColorID"].ToString();
                    sizeID = dt.Rows[j]["SizeID"].ToString();
                    _taskID = dt.Rows[j]["TaskID"].ToString();
                    colorOneID = dt.Rows[j]["ColorOneID"].ToString();
                    colorTwoID = dt.Rows[j]["ColorTwoID"].ToString();
                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            if (dtInfo.DefaultView[i]["ColorID"].ToString() == colorID && dtInfo.DefaultView[i]["ColorOneID"].ToString() == colorOneID &&
                                dtInfo.DefaultView[i]["ColorTwoID"].ToString() == colorTwoID &&
                                dtInfo.DefaultView[i]["SizeID"].ToString() == sizeID && dtInfo.DefaultView[i]["TaskID"].ToString() == _taskID)
                            {
                                dtInfo.Rows[i]["Amount"] = dt.Rows[j]["Amount"];
                                dtInfo.Rows[i]["Price"] = _price;
                                dtInfo.Rows[i]["Money"] = Convert.ToDecimal(dt.Rows[i]["Amount"]) * _price;
                                if (dtInfo.Rows[i]["A"].ToString() == "1")
                                    dtInfo.Rows[i]["A"] = 2;
                                f = true;
                                break;
                            }
                        }
                    }
                    if (!f)
                    {
                        DataRow dr = dtInfo.NewRow();
                        dr.ItemArray = dt.Rows[j].ItemArray;
                        dr["Price"] = _price;
                        dr["DepotInfoID"] = 0;
                        dr["Money"] = Convert.ToDecimal(dr["Amount"]) * _price;
                        dtInfo.Rows.Add(dr);
                    }
                }
            }
            lookUpEdit1.EditValue = 0;
        }

        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView2.OptionsBehavior.Editable = true;
        }

        private void _brDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除本张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (_MainID > 0)
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduct2DepotInfo, "DeleteByMainID", new object[] { _MainID });
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduct2Depot, "Delete", new object[] { _MainID });
                }
                InData();
                ShowView(dtMain.Rows.Count - 1);
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Save())
            {

                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduct2Depot, "Verify", new object[] { _MainID, true, BasicClass.UserInfo.UserID, _depotID, (int)BasicClass.Enums.TableType.P2D });
                string blCL = "Hownet.BLL.CompanyLog";
                int _companyID = Convert.ToInt32(_leDeparment.editVal);

                if (Convert.ToInt32(dtP2D.Rows[0]["DeparmentTypeID"]) == 3)
                {
                    decimal last = decimal.Parse(BasicClass.GetDataSet.GetOne(blCL, "GetProcessingBackLastMoney", new object[] { _companyID }).ToString());
                    DataTable dtBack = BasicClass.GetDataSet.GetDS(blCL, "GetProcessingBackMoney", new object[] { _companyID }).Tables[0];
                    decimal back = 0;
                    if (dtBack.Rows.Count > 0)
                    {
                        back = 0;
                        if (dtBack.Rows.Count > 0)
                        {
                            if (dtBack.Rows[0]["Money"] != null && dtBack.Rows[0]["Money"].ToString().Trim() != string.Empty)
                            {
                                back = decimal.Parse(dtBack.Rows[0]["Money"].ToString());
                            }
                        }

                    }

                    DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
                    DataRow dr = dtCL.NewRow();
                    decimal money = 0;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        money += Convert.ToDecimal(gridView2.GetRowCellValue(i, _coMoney));
                    }
                    dr["ID"] = 0;
                    dr["CompanyID"] = _companyID;
                    dr["DateTime"] = dtP2D.Rows[0]["DateTime"];
                    dr["LastMoney"] = last + back;
                    dr["ChangMoney"] = money;
                    dr["Money"] = last + money - back;
                    dr["TypeID"] = (int)(BasicClass.Enums.MoneyTableType.Processing2Depot);
                    dr["TableID"] = _MainID;
                    dr["NowMoneyTypeID"] = 0;
                    dr["NowMoney"] = 0;
                    dr["NowReta"] = 1;
                    dr["A"] = 1;
                    dtCL.Rows.Add(dr);
                    BasicClass.GetDataSet.Add(blCL, dtCL);
                }
                ShowView(bs.Position);
            }
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string blCL = "Hownet.BLL.CompanyLog";
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { Convert.ToInt32(_leDeparment.editVal), (int)(BasicClass.Enums.MoneyTableType.Processing2Depot), _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本单之后已有后续业务发生，不能弃审！");
                return;
            }
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { Convert.ToInt32(_leDeparment.editVal), (int)(BasicClass.Enums.MoneyTableType.Processing2Depot), _MainID });
 
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduct2Depot, "Verify", new object[] { _MainID, false, 0, _depotID, (int)BasicClass.Enums.TableType.P2D });
            ShowView(bs.Position);
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!t)
            {
                gridView2.OptionsBehavior.Editable = (e.FocusedRowHandle < 0);
                if (e.FocusedRowHandle < 0 && gridView2.RowCount > 0)
                    gridView2.AddNewRow();
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle > -1 && e.Column != _coA && gridView2.GetFocusedRowCellValue(_coA).Equals(1))
                gridView2.SetFocusedRowCellValue(_coA, 2);
            if (e.Column == _coPrice)
            {
                int _taskID = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coTaskID));
                decimal _price = Convert.ToDecimal(e.Value);
                if (_taskID > 0)
                {
                    DataRow[] drs = dtInfo.Select("(TaskID=" + _taskID + ")");
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i]["Price"] = _price;
                            drs[i]["Money"] = Convert.ToDecimal(drs[i]["Amount"]) * _price;
                        }
                    }
                }
            }

        }

        private void gridView2_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView2.RowCount > 0)
            {
                try
                {
                    int r = gridView2.RowCount - 1;
                    if (gridView2.GetRowCellValue(r, _coMaterieID).Equals(0))
                    {
                        gridView2.DeleteRow(r);
                        return;
                    }
                    if (gridView2.GetRowCellValue(r, _coColorID).Equals(0))
                    {
                        gridView2.DeleteRow(r);
                        return;
                    }
                    if (gridView2.GetRowCellValue(r, _coSizeID).Equals(0))
                    {
                        gridView2.DeleteRow(r);
                        return;
                    }
                    if (gridView2.GetRowCellValue(r, _coAmount).Equals(0))
                    {
                        gridView2.DeleteRow(r);
                        return;
                    }
                }
                catch { }
            }
        }

        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            for (int i = 0; i < gridView2.Columns.Count; i++)
            {
                gridView2.SetFocusedRowCellValue(gridView2.Columns[i], 0);
            }
            gridView2.SetFocusedRowCellValue(_coRemark, "");
            gridView2.SetFocusedRowCellValue(_coA, 3);
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除本条明细？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (!gridView2.GetFocusedRowCellValue(_coID).Equals(0))
                    {
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProduct2DepotInfo, "Delete", new object[] { int.Parse(gridView2.GetFocusedRowCellValue(_coID).ToString()) });
                        gridView2.DeleteRow(gridView2.FocusedRowHandle);
                        dtInfo.AcceptChanges();
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
            dtDeoptInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "(ParentID=" + _depotID + ")" }).Tables[0];
            _reDeoptInfo.DataSource = dtDeoptInfo;
            if(!t)
            {
                for(int i=0;i<gridView2.RowCount;i++)
                {
                    if (gridView2.GetRowCellDisplayText(i, _coDepotInfoID) == string.Empty)
                        gridView2.SetRowCellValue(i, _coDepotInfoID, 0);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(_leBrandID.editVal) > 0 && Convert.ToInt32(_leMaterielID.editVal) > 0)
            {
                int _brandID = Convert.ToInt32(_leBrandID.editVal);
                int _materielID = Convert.ToInt32(_leMaterielID.editVal);
                BasicClass.cResult cr = new cResult();
                cr.RowChanged += new RowChangedHandler(cr_RowChanged);
                DataTable dtt = dtInfo.Clone();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    object br = dtInfo.Rows[i]["BrandID"];
                    object ma = dtInfo.Rows[i]["MaterielID"];
                    if (Convert.ToInt32(dtInfo.Rows[i]["BrandID"]) == Convert.ToInt32(_leBrandID.editVal) && Convert.ToInt32(dtInfo.Rows[i]["A"]) < 4 &&
                        Convert.ToInt32(dtInfo.Rows[i]["MaterielID"]) == Convert.ToInt32(_leMaterielID.editVal))
                    {
                        dtt.Rows.Add(dtInfo.Rows[i].ItemArray);
                    }
                }
                Form fr = new frP2DAmount(cr, "款号：" + _leMaterielID.valStr + "，商标：" + _leBrandID.valStr, !t, dtt, _materielID, _brandID);
                fr.ShowDialog();
            }
        }

        void cr_RowChanged(DataTable dt)
        {
            int _brandID = Convert.ToInt32(_leBrandID.editVal);
            int _materielID = Convert.ToInt32(_leMaterielID.editVal);
            DataRow[] drs = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
            foreach (DataRow drr in drs)
            {
                drr["A"] = 4;
            }
            dtInfo.AcceptChanges();
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtInfo.NewRow();
                    dr["ID"] = dr["TaskID"] = dr["DeparmentID"] = dr["MListID"] = 0;
                    dr["MainID"] = _MainID;
                    dr["MaterielID"] = _materielID;
                    dr["BrandID"] = _brandID;
                    dr["ColorID"] = dt.Rows[i]["ColorID"];
                    dr["ColorOneID"] = dt.Rows[i]["ColorOneID"];
                    dr["ColorTwoID"] = dt.Rows[i]["ColorTwoID"];
                    dr["SizeID"] = dt.Rows[i]["SizeID"];
                    dr["Amount"] = dt.Rows[i]["Amount"];
                    dr["A"] = 3;
                    dr["DepotInfoID"] = 0;
                    dtInfo.Rows.Add(dr);
                }
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            BaseForm.PrintClass.PrintP2DTable(GetPrintDS(), false);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseForm.PrintClass.PrintP2DTable(GetPrintDS(), true);

        }
        private DataSet GetPrintDS()
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            DataTable dtOne = new DataTable();
            dtOne.TableName = "Main";
            dtOne.Columns.Add("表头", typeof(string));
            dtOne.Columns.Add("编号", typeof(string));
            dtOne.Columns.Add("收货仓库", typeof(string));
            dtOne.Columns.Add("日期", typeof(string));
            dtOne.Columns.Add("备注", typeof(string));
            dtOne.Columns.Add("审核", typeof(string));
            dtOne.Columns.Add("制单", typeof(string));
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            DataTable dtDep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetAllList", null).Tables[0];
            DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetAllList", null).Tables[0];
            dtOne.Rows.Add("成品入库", _ltNum.EditVal, _leDepotID.valStr, _ldDate.strLab, _ltRemark.EditVal);
            try
            {
                dtOne.Rows[0]["审核"] = dtUser.Select("(ID=" + Convert.ToInt32(dtP2D.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            try
            {
                dtOne.Rows[0]["制单"] = dtUser.Select("(ID=" + Convert.ToInt32(dtP2D.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            ds.Tables.Add(dtOne);

            DataTable dtTwo = new DataTable();
            dtTwo.TableName = "Info";
            dtTwo.Columns.Add("任务单编号", typeof(string));
            dtTwo.Columns.Add("款号", typeof(string));
            dtTwo.Columns.Add("商标", typeof(string));
            dtTwo.Columns.Add("颜色", typeof(string));
            dtTwo.Columns.Add("尺码", typeof(string));
            dtTwo.Columns.Add("配色一", typeof(string));
            dtTwo.Columns.Add("配色二", typeof(string));
            dtTwo.Columns.Add("数量", typeof(decimal));
            dtTwo.Columns.Add("未完成数量", typeof(decimal));
            dtTwo.Columns.Add("说明", typeof(string));
            dtTwo.Columns.Add("班组", typeof(string));
            int _TaskID = 0;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow dr = dtTwo.NewRow();
                dr[0] = gridView2.GetRowCellDisplayText(i, _coTaskID);
                dr[1] = gridView2.GetRowCellDisplayText(i, _coMaterieID);
                dr[2] = gridView2.GetRowCellDisplayText(i, _coBrandID);
                dr[3] = gridView2.GetRowCellDisplayText(i, _coColorID);
                dr[4] = gridView2.GetRowCellDisplayText(i, _coSizeID);
                dr[5] = gridView2.GetRowCellDisplayText(i, _coColorOneID);
                dr[6] = gridView2.GetRowCellDisplayText(i, _coColorTwoID);
                dr[7] = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coAmount));
                _TaskID = Convert.ToInt32(gridView2.GetRowCellValue(i, _coTaskID));
                if (_TaskID > 0)
                {
                    DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetList", new object[] { "(MainID=" + _TaskID + ") And (TableTypeID=" + (int)BasicClass.Enums.TableType.Task + ") And (MListID=" + Convert.ToInt32(gridView2.GetRowCellValue(i, _coMListID)) + ")" }).Tables[0];
                    if (dtTem.Rows.Count > 0)
                    {
                        dr[8] = dtTem.Rows[0]["NotAmount"];
                    }
                    dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] { "(ID=" + _TaskID + ")" }).Tables[0];
                    if (dtTem.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtTem.Rows[0]["DeparmentType"]) == 3)
                        {
                            dr[10] = dtCom.Select("(ID=" + Convert.ToInt32(dtTem.Rows[0]["DeparmentID"]) + ")")[0]["Name"];
                        }
                        else
                        {
                            dr[10] = dtDep.Select("(ID=" + Convert.ToInt32(dtTem.Rows[0]["DeparmentID"]) + ")")[0]["Name"];
                        }
                    }
                }
                dr[9] = gridView2.GetRowCellDisplayText(i, _coRemark);
                dtTwo.Rows.Add(dr);
            }
            ds.Tables.Add(dtTwo);
            return ds;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                _leDeparment.Par = null;
                _leDeparment.FormName = (int)BasicClass.Enums.TableType.JGC;
                dtP2D.Rows[0]["DeparmentTypeID"] = 3;
            }
            else
            {
                _leDeparment.Par = new object[] { "TypeID=38" };
                _leDeparment.FormName = (int)BasicClass.Enums.TableType.Deparment;
                dtP2D.Rows[0]["DeparmentTypeID"] = 0;
            }
        }

        private void _leDeparment_EditValueChanged(object val, string text)
        {
            if (!t)
            {
                int _companyID = Convert.ToInt32(_leDeparment.editVal);
                if (_companyID > 0)
                    lookUpEdit3.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetMaterielByDeparmentID", new object[] { _companyID, Convert.ToInt32(dtP2D.Rows[0]["DeparmentTypeID"]) }).Tables[0];
            }
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            int _matID = Convert.ToInt32(lookUpEdit3.EditValue);
            if (_matID > 0)
            {
                int _companyID = Convert.ToInt32(_leDeparment.editVal);
                if (_companyID > 0)
                {
                    if (!checkEdit1.Checked)
                    {
                        lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "(DeparmentID=" + _companyID + ") And (DeparmentType=0) And ( ProductTaskMain.MaterielID=" + _matID + ")" }).Tables[0]; ;
                        lookUpEdit1.EditValue = 0;
                    }

                    else
                    {

                        lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", new object[] { "(DeparmentID=" + _companyID + ") And (DeparmentType=3) And ( ProductTaskMain.MaterielID=" + _matID + ")" }).Tables[0];
                        lookUpEdit1.EditValue = 0;
                    }
                }
            }
        }
    }
}