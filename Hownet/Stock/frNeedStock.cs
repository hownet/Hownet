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
    public partial class frNeedStock : DevExpress.XtraEditors.XtraForm
    {
        public frNeedStock()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frNeedStock(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _companyID = 0;
        int _depotID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        private string bllSB = "Hownet.BLL.StockBack";
        private string blSBI = "Hownet.BLL.StockBackInfo";
        private string blQP = "Hownet.BLL.QuotePrice";
        DataTable dtSB= new DataTable();
        DataTable dtSBI = new DataTable();
        DataTable dtBack = new DataTable();
        bool _IsVerify = false;
        bool _isNeedStock = false;
        decimal price = 0;
        decimal amount = 0;
        int _rowCount = 0;
        BasicClass.cResult r = new cResult();
        public frNeedStock(BasicClass.cResult cr, DataTable dt)
            : this()
        {
            r = cr;
            dtSBI = dt;
        }
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            if (dtSBI.Rows.Count > 0)
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(dtMain.NewRow());
                _isNeedStock = true;
                bs.DataSource = dtMain;
                ShowView(0);
                _brAddNew.Enabled = false;
            }
            else
            {
                if (_MainID == 0)
                {
                   
                    InData();
                    bs.PositionChanged += new EventHandler(bs_PositionChanged);
                    bs.Position = dtMain.Rows.Count - 1;
                    if (bs.Position == 0)
                        ShowView(0);
                }
                else
                {
                    dtMain.Columns.Add("ID", typeof(int));
                    dtMain.Rows.Add(_MainID);
                    bs.DataSource = dtMain;
                    ShowView(0);
                    //bar1.Visible = false;
                }
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barUnVierfy.Visibility = _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
            labAndLookup1.FormName = (int)BasicClass.Enums.TableType.Deparment;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
         }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)BasicClass.Enums.TableType.NeedStock }).Tables[0];
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
            // dtSBI.Rows.Clear();
            gridControl1.DataSource = null;
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            }
            else
            {
                dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtSB.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = dr["DepotID"] = _MainID = 0;
                dr["FillDate"] = dr["DataTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = dr["StockRemark"] = "";
                dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { DateTime.Today, BasicClass.BasicFile.liST[0].NumType, (int)BasicClass.Enums.TableType.NeedStock, 0 });
                dr["LastMoney"] = 0;
                dr["BackMoney"] = 0;
                dr["BackDate"] = BasicClass.GetDataSet.GetDateTime().Date.AddDays(5);
                dtSB.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            _upData = int.Parse(dtSB.Rows[0]["UpData"].ToString());
            _IsVerify = (int.Parse(dtSB.Rows[0]["IsVerify"].ToString()) > 2);
            _ldDate.val = dtSB.Rows[0]["DataTime"];
            _companyID = int.Parse(dtSB.Rows[0]["CompanyID"].ToString());
            _ltRemark.val = dtSB.Rows[0]["Remark"].ToString();
            _ltNum.val = DateTime.Parse(dtSB.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtSB.Rows[0]["Num"].ToString().PadLeft(3, '0');
            _leBackDate.val = Convert.ToDateTime(dtSB.Rows[0]["BackDate"]);
            labAndLookup1.editVal = _depotID = Convert.ToInt32(dtSB.Rows[0]["DepotID"]);
            if (!_isNeedStock)
                dtSBI = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = !_IsVerify;
            _barUnVierfy.Enabled = _IsVerify;// (_IsVerify && p == dtMain.Rows.Count - 1);
            _brAddNew.Enabled = (_MainID > 0);
            SetColumnsReadOnly();
            _rowCount = gridView1.RowCount;
            _barUnVierfy.Enabled = (Convert.ToInt32(dtSB.Rows[0]["IsVerify"]) == 3);
            if (!_IsVerify)
            {
                DataRow dr = dtSBI.NewRow();
                for (int i = 0; i < dtSBI.Columns.Count; i++)
                {
                    if (dtSBI.Columns[i].ColumnName != "LastTime")
                        dr[i] = 0;
                }
                dr["MainID"] = _MainID;
                dr["A"] = 3;
                dr["Remark"] = dr["StringTaskID"] = string.Empty;
                dr["LastTime"] = BasicClass.GetDataSet.GetDateTime().Date.AddDays(5);
                //dtSBI.Rows.Add(dr.ItemArray);
                dtSBI.Rows.Add(dr);
                dtSBI.Rows.Add(dr.ItemArray);
                dtSBI.AcceptChanges();
            }
            gridView1.OptionsBehavior.Editable = !_IsVerify;
            gridControl1.DataSource = dtSBI;
            // ShowTask();
            memoEdit1.Text = dtSB.Rows[0]["StockRemark"].ToString();
            textEdit1.Text = string.Empty;
            ShowMaterielName();
            _ltRemark.Focus();
            ShowGDI();
            if (gridView1.RowCount > 1)
            {
                gridView1.FocusedRowHandle = 1;
                gridView1.FocusedRowHandle = 0;
            }
        }
        private void ShowGDI()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i].Name == "broculosDrawing1")
                {
                    panel1.Controls.RemoveAt(i);
                    break;
                }
            }
            int vvv = Convert.ToInt32(dtSB.Rows[0]["IsVerify"]);
            if (vvv < 3)
                return;
            BaseContranl.BroculosDrawing broculosDrawing1 = new BaseContranl.BroculosDrawing();
            broculosDrawing1.Name = "broculosDrawing1";
            panel1.Controls.Add(broculosDrawing1);
            broculosDrawing1.BringToFront();
            broculosDrawing1.Location = label1.Location;// new System.Drawing.Point(502, 3);
            broculosDrawing1.Name = "broculosDrawing1";
            broculosDrawing1.Size = new System.Drawing.Size(120, 65);
            broculosDrawing1.TabIndex = 28;
            string strText="";
            if (vvv == 3 || vvv == 4)
                strText = "已审核";
            if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(blSBI, "NotIsEnd", new object[] { _MainID })) == 0)
                strText = "采购完成";
            broculosDrawing1.StrText = strText;
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
            memoEdit1.Text += textEdit1.Text + "/" + label3.Text;
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

           if( Save())
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
            //if (_companyID == 0)
            //{
            //   if(DialogResult.No== XtraMessageBox.Show("未选择供应商，是否为现金采购？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
            //    return false;
            //}
            dtSB.Rows[0]["LastMoney"] = 0;
            dtSB.Rows[0]["BackMoney"] = 0;
            dtSB.Rows[0]["Remark"] = _ltRemark.val;
            dtSB.Rows[0]["StockRemark"] = memoEdit1.Text;
            dtSB.Rows[0]["Money"] = 0;
            dtSB.Rows[0]["CompanyID"] = _companyID;
            dtSB.Rows[0]["DataTime"] = _ldDate.val;
            dtSB.Rows[0]["State"] = (int)Enums.TableType.NeedStock;
            dtSB.Rows[0]["A"] = 1;
            dtSB.Rows[0]["BackDate"] = _leBackDate.val;
            dtSB.Rows[0]["DataTime"] = _ldDate.val;
            dtSB.Rows[0]["DepotID"] = _depotID;
            //dtPS.Rows[0]["Depot"] = 0;
            if (_MainID == 0)
            {
                dtSB.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.NeedStock, 0 });
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
            for (int i = 0; i < dtSBI.Rows.Count; i++)
            {
                int a = int.Parse(dtSBI.Rows[i]["A"].ToString());
                if (Convert.ToInt32(dtSBI.Rows[i]["MaterielID"]) > 0 &&dtSBI.Rows[i]["Amount"].ToString()!=string.Empty&& Convert.ToDecimal(dtSBI.Rows[i]["Amount"]) > 0)
                {
                    if (a > 1)
                    {
                        dtSBI.Rows[i]["MainID"] = _MainID;
                      dtSBI.Rows[i]["NeedAmount"]=  dtSBI.Rows[i]["PriceAmount"]=dtSBI.Rows[i]["NotPriceAmount"]= dtSBI.Rows[i]["NotAmount"] = dtSBI.Rows[i]["Amount"];
                         dtSBI.Rows[i]["DepotMeasureID"]=dtSBI.Rows[i]["CompanyMeasureID"] ;
                        dtSBI.Rows[i]["Conversion"] = 1;
                        dtt.Clear();
                        dtt.Rows.Add(dtSBI.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtSBI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blSBI, dtt);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(blSBI, dtt);
                        }
                        dtSBI.Rows[i]["A"] = 1;
                    }
                }
                else
                {
                    if (Convert.ToInt32(dtSBI.Rows[i]["ID"]) > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blSBI, "Delete", new object[] { Convert.ToInt32(dtSBI.Rows[i]["ID"])});
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
            //if (e.RowHandle > -1)
            //{
            //    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            //}
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column == _coMaterielID)
            {
                if (e.RowHandle == gridView1.RowCount - 2)
                {
                    DataRow dr = dtSBI.NewRow();
                    for (int i = 0; i < dtSBI.Columns.Count; i++)
                    {
                        if (dtSBI.Columns[i].ColumnName != "LastTime")
                            dr[i] = 0;
                    }
                    dr["MainID"] = _MainID;
                    dr["A"] = 3;
                    dr["Remark"] = dr["StringTaskID"] = string.Empty;
                     dtSBI.Rows.Add(dr.ItemArray);
                    // dtSBI.Rows.Add(dr);
                    //   dtSBI.Rows.Add(dr.ItemArray);
                    dtSBI.AcceptChanges();
                }
                object obj = BaseForm.RepositoryItem._reMateriel.GetDataSourceValue("MeasureID", BaseForm.RepositoryItem._reMateriel.GetDataSourceRowIndex("ID", e.Value));
                gridView1.SetFocusedRowCellValue(_coMeasureID, obj);
                if (Convert.ToInt32( e.Value) > 0)
                {
                    gridView1.SetFocusedRowCellValue(_coMaterielRemark, BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + e.Value + ")" }).Tables[0].Rows[0]["Remark"]);
                }
            }
            if (e.RowHandle > -1)
            {
                if ((e.Column == _coMaterielID || e.Column == _coColorID || e.Column == _coSizeID || e.Column == _coMeasureID) && e.Value.ToString() != "0")
                    CheckMateriel(gridView1.GetFocusedRowCellValue(_coMaterielID), gridView1.GetFocusedRowCellValue(_coColorID), gridView1.GetFocusedRowCellValue(_coSizeID), gridView1.GetFocusedRowCellValue(_coMeasureID));
                if (e.Column == _coMaterielID || e.Column == _coAmount)
                {
                    GetNeed();
                }
            }
            if (e.RowHandle > -1)
            {
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }


        }
        private void CheckMateriel(object mat, object brand, object size,object MeasureID)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (i != gridView1.FocusedRowHandle)
                {
                    if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && gridView1.GetRowCellValue(i, _coColorID).Equals(brand)
                        && gridView1.GetRowCellValue(i, _coSizeID).Equals(size) && gridView1.GetRowCellValue(i, _coMeasureID).Equals(MeasureID))
                    {
                        XtraMessageBox.Show("同申购单中，不能有多个相同记录！");
                        SendKeys.Send("{Esc}");
                        return;
                    }
                }
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!_IsVerify)
            {
                gridView1.OptionsBehavior.Editable = e.FocusedRowHandle < gridView1.RowCount - 1;
                for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
                {
                    gridView1.VisibleColumns[i].OptionsColumn.AllowEdit = true;// (e.FocusedRowHandle == gridView1.RowCount - 2);
                  //  _coAmount.OptionsColumn.AllowEdit = true;
                }
                _coMeasureID.OptionsColumn.AllowEdit = false;
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
            //gridView1.SetFocusedRowCellValue(_coRemark, "");
            //gridView1.SetFocusedRowCellValue(_coMainID, _MainID);
            //gridView1.SetFocusedRowCellValue(_coA, 3);
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
            //                    XtraMessageBox.Show("同一申购单中，不能有多个相同记录！");
            //                    gridView1.DeleteRow(r);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    //if (gridView1.RowCount == 8)
            //    //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //    //else
            //    //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
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
            if (gridView1.RowCount == 2)
            {
                XtraMessageBox.Show("没有明细记录！");
                return;
            }

            if (!Save())
                return;
            dtSB.Rows[0]["IsVerify"] = 3;
            dtSB.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtSB.Rows[0]["VerifyDate"] = DateTime.Today;
            BasicClass.GetDataSet.UpData(bllSB, dtSB);
            BasicClass.GetDataSet.ExecSql(bllSB, "Verify", new object[] { _MainID, true, 0 });
            BasicClass.GetDataSet.ExecSql(blSBI, "VerifyOneAddNeedStock", new object[] { _MainID, true });
            //if (_companyID > 0)
            //{
            //     int MaterielID=0;
            //    int BrandID=0;
            //    decimal Price=0;
            //    int measureid = 0;
            //    for (int i = 0; i < gridView1.RowCount; i++)
            //    {
            //        MaterielID = int.Parse(gridView1.GetRowCellValue(i, _coMaterielID).ToString());
            //        BrandID = int.Parse(gridView1.GetRowCellValue(i, _coColorID).ToString());
            //        Price = decimal.Parse(gridView1.GetRowCellValue(i, _coPrice).ToString());
            //        measureid = int.Parse(gridView1.GetRowCellValue(i, _coMeasureID).ToString());
            //        object[] o = new object[] { _companyID,  MaterielID,  BrandID,   Price, measureid,0 };
            //        BasicClass.GetDataSet.ExecSql(blQP, "UpPrice", o);
            //    }
            //}
            r.ChangeText("OK");
            //if (BasicClass.BasicFile.liST[0].Sell4Depot)
            ShowView(bs.Position);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllSB, "NeedStockCheckCanUnVerify", new object[] { _MainID })))
            {
                dtSB.Rows[0]["IsVerify"] = 1;
                dtSB.Rows[0]["VerifyMan"] = 0;
                dtSB.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
                BasicClass.GetDataSet.UpData(bllSB, dtSB);
                BasicClass.GetDataSet.ExecSql(blSBI, "VerifyOneAddNeedStock", new object[] { _MainID, false });
                ShowView(bs.Position);
            }
            else
            {
                XtraMessageBox.Show("本单已在进行采购，不能弃审");
            }
        }

 
   
        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
         //   ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("BackDate", typeof(string));
            ds.Tables[0].Rows[0]["ComName"] = string.Empty;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val + " - " + memoEdit1.Text;
            ds.Tables[0].Rows[0]["BackDate"] = ((DateTime)(_leBackDate.val)).ToString("yyyy年MM月dd日");
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("GuiGe", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("Money", typeof(string));
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
                    _guiGe =_guiGe+ gridView1.GetRowCellDisplayText(i, _coColorOneID) + "/";
                if (gridView1.GetRowCellDisplayText(i, _coColorTwoID).Trim() != string.Empty)
                    _guiGe = _guiGe + gridView1.GetRowCellDisplayText(i, _coColorTwoID) + "/";
                if (gridView1.GetRowCellDisplayText(i, _coSizeID).Trim() != string.Empty)
                    _guiGe = _guiGe + gridView1.GetRowCellDisplayText(i, _coSizeID) + "/";
                if (_guiGe.Length > 0)
                    _guiGe = _guiGe.Remove(_guiGe.Length - 1);
                dr[1] = _guiGe;
                dr[2] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount)).ToString("n1");
                dr[3] =decimal.Parse( gridView1.GetRowCellValue(i, _coPrice).ToString()).ToString("C4");
                dr[4] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[5] = decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString()).ToString("C2");
                dr[6] = gridView1.GetRowCellDisplayText(i, _coRemark);
                dt.Rows.Add(dr);
            }
            for (int i = dt.Rows.Count; i < 8; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintNeedStock(ds);
        }

        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_IsVerify)
            {
                XtraMessageBox.Show("请审核后再打印单据！");
                return;
            }
        }
        private void GetNeed()
        {
            //gridView1.CloseEditor();
            //gridView1.UpdateCurrentRow();
            //dtSBI.AcceptChanges();
            //if(dtSBI.Rows.Count==0)
            //    return;
            //DataSet ddd=new DataSet();
            //ddd.DataSetName="dd";
            //DataTable dttttt=dtSBI.Copy();
            //dttttt.TableName="dtt";
            //ddd.Tables.Add(dttttt);
            //byte[] bb=ZipJpg.Ds2Byte(ddd);
            //DataSet ssss = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "GetSemiNeed", new object[] { bb, _MainID, (int)BasicClass.Enums.TableType.Stock });
            //_gcDemand.DataSource = ssss.Tables[0];
        }

        private void labAndLookup1_EditValueChanged(object val, string text)
        {
            _depotID = Convert.ToInt32(val);
        }

        private void ShowMaterielName()
        {
            label3.Text = string.Empty;
            if (_MainID == 0)
            {
                dtSBI.Rows.Clear();
                dtSBI.AcceptChanges();
                if (textEdit1.Text.Trim().Length > 9)
                {
                    DataTable dddt = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetListByNum", new object[] { textEdit1.Text.Trim() }).Tables[0];
                    if (dddt.Rows.Count > 0)
                    {
                        _companyID = Convert.ToInt32(dddt.Rows[0]["ID"]);
                        label3.Text = dddt.Rows[0]["MaterielName"].ToString();
                        if (_MainID == 0)
                        {
                            dtSBI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetListByPlanID", new object[] { _companyID, (int)BasicClass.Enums.TableType.ProductionPlan }).Tables[0];
                            dtSBI.Columns.Add("Amount", typeof(decimal));
                            memoEdit1.Text = textEdit1.Text + "-" + label3.Text;
                            try
                            {
                                dtSBI.Columns.Add("LastTime", typeof(DateTime));
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                if (!_IsVerify)
                {
                    DataRow dr = dtSBI.NewRow();
                    for (int i = 0; i < dtSBI.Columns.Count; i++)
                    {
                        if (dtSBI.Columns[i].ColumnName != "LastTime")
                            dr[i] = 0;
                    }
                    dr["MainID"] = _MainID;
                    dr["MaterielRemark"] = string.Empty;
                    dr["A"] = 3;
                    dr["Remark"] = string.Empty;
                    dr["StringTaskID"] = _companyID;
                    //dtSBI.Rows.Add(dr.ItemArray);
                    dtSBI.Rows.Add(dr);
                    dtSBI.Rows.Add(dr.ItemArray);
                    dtSBI.AcceptChanges();
                }
                gridControl1.DataSource = dtSBI;
            }
            else
            {
                if (_companyID > 0)
                {
                    DataTable dddt = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetNumList", new object[] { " (ProductionPlan.ID = " + _companyID + ")" }).Tables[0];
                    if (dddt.Rows.Count > 0)
                    {
                        textEdit1.Text = dddt.Rows[0]["Num"].ToString();
                    }
                }
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_MainID > 0)
                return;
            int k = (int)e.KeyChar;
            if (k == 13)
            {
                ShowMaterielName();
            }
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            if (_MainID > 0)
                return;
            ShowMaterielName();
        }
    }
}