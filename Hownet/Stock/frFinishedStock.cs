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
    public partial class frFinishedStock : DevExpress.XtraEditors.XtraForm
    {
        public frFinishedStock()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frFinishedStock(int MainID)
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
        private string strTaskID = "";
        DataTable dtSB= new DataTable();
        DataTable dtSBI = new DataTable();
        DataTable dtBack = new DataTable();
        DataTable dtMat = new DataTable();
        bool _IsVerify = false;
        bool _isNeedStock = false;
        decimal price = 0;
        decimal amount = 0;
        int _rowCount = 0;
        BasicClass.cResult r = new cResult();
        public frFinishedStock(BasicClass.cResult cr, DataTable dt)
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
                    ShowView(0);
                    bar1.Visible = false;
                }
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
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
            _leCompany.FormName = (int)BasicClass.Enums.TableType.Supplier;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _leCompany.IsNotCanEdit = false;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            string bll = "Hownet.BLL.Materiel";
              dtMat = BasicClass.GetDataSet.GetDS(bll, "GetLookupList", new object[] { "(AttributeID<5)" }).Tables[0];
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)BasicClass.Enums.TableType.FinishedStock }).Tables[0];
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
            // dtSBI.Rows.Clear();
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
               // _leCompany.t = true;
            }
            else
            {
                dtSB = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtSB.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] =dr["DepotID"]= _MainID = 0;
                dr["FillDate"] = dr["DataTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, BasicClass.BasicFile.liST[0].NumType, (int)Enums.TableType.Stock, 0 });
                dr["LastMoney"] = 0;
                dr["BackMoney"] = 0;
                dr["BackDate"] = BasicClass.GetDataSet.GetDateTime().Date.AddDays(5);
                dtSB.Rows.Add(dr);
                _brAddNew.Enabled = false;
               // _leCompany.t = false;
            }
            _upData = int.Parse(dtSB.Rows[0]["UpData"].ToString());
            _IsVerify = (int.Parse(dtSB.Rows[0]["IsVerify"].ToString()) >2);
            _ldDate.val = dtSB.Rows[0]["DataTime"];
            _leCompany.editVal = _companyID = int.Parse(dtSB.Rows[0]["CompanyID"].ToString());
            _ltRemark.val = dtSB.Rows[0]["Remark"].ToString();
            _ltNum.val = DateTime.Parse(dtSB.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtSB.Rows[0]["Num"].ToString().PadLeft(3, '0');
            _leBackDate.val = Convert.ToDateTime(dtSB.Rows[0]["BackDate"]);
            strTaskID = "";
            memoEdit1.Text = dtSB.Rows[0]["StockRemark"].ToString();
            if (!_isNeedStock)
                dtSBI = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            //if (!_IsVerify)
            //{
            //    DataRow dr = dtSBI.NewRow();
            //    for (int i = 0; i < dtSBI.Columns.Count; i++)
            //    {
            //        if (dtSBI.Columns[i].ColumnName != "LastTime")
            //            dr[i] = 0;
            //    }
            //    dr["A"] = 3;
            //    dr["StringTaskID"] = dr["Remark"] = string.Empty;
            //    dr["LastTime"] = BasicClass.GetDataSet.GetDateTime().Date.AddDays(5);
            //    //dtSBI.Rows.Add(dr.ItemArray);
            //    //dtSBI.Rows.Add(dr.ItemArray);
            //}
            simpleButton1.Visible = !_IsVerify;
            gridControl1.DataSource = dtSBI;
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = !_IsVerify;
          _leCompany.IsNotCanEdit=  _barUnVierfy.Enabled=_IsVerify;
            _brAddNew.Enabled=(_MainID>0);
            //_coDepotAmount.VisibleIndex = 3;
            //gridView1.OptionsBehavior.Editable = !t;
            SetColumnsReadOnly();
            this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            _rowCount = gridView1.RowCount;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
           // ShowTask();
            xtraTabControl1.SelectedTabPage = _xtraInfo;
            ShowLinLiao();
            ShowGDI();
            if(_IsVerify)
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            else
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

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
            string strText = "";
            if (vvv == 3 || vvv == 4)
                strText = "已审核";
            if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(blSBI, "NotIsEnd", new object[] { _MainID })) == 0)
                strText = "全部到料";
            broculosDrawing1.StrText = strText;
        }
        private void ShowTask()
        {
            strTaskID = "";
            memoEdit1.Text = string.Empty;
            DataTable dtTem = new DataTable();
            DataTable dtTask=new DataTable();
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
                   memoEdit1.Text += BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductionPlan", "GetNum", new object[] { Convert.ToInt32(dtTem.Rows[i]["ID"]) }).ToString()+"；";
                }
            }
            GetNeed();
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
            _coMoney.OptionsColumn.AllowEdit = false;
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
            if (_companyID == 0)
            {
               if(DialogResult.No== XtraMessageBox.Show("未选择供应商，是否为现金采购？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
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
            dtSB.Rows[0]["LastMoney"] = 0;
            dtSB.Rows[0]["BackMoney"] = 0;
            dtSB.Rows[0]["Remark"] = _ltRemark.val;
            dtSB.Rows[0]["Money"] = 0;
            dtSB.Rows[0]["CompanyID"] = _companyID;
            dtSB.Rows[0]["DataTime"] = _ldDate.val;
            dtSB.Rows[0]["State"] = (int)Enums.TableType.FinishedStock;
            dtSB.Rows[0]["A"] = 1;
            dtSB.Rows[0]["BackDate"] = _leBackDate.val;
            dtSB.Rows[0]["DataTime"] = _ldDate.val;
            dtSB.Rows[0]["StockRemark"] = memoEdit1.Text;
            //dtPS.Rows[0]["Depot"] = 0;
            if (_MainID == 0)
            {
                dtSB.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.FinishedStock, 0 });
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
                if (a > 1)
                {
                    dtSBI.Rows[i]["MainID"] = _MainID;
                    dtSBI.Rows[i]["NotAmount"] = dtSBI.Rows[i]["Amount"];
                    dtSBI.Rows[i]["DepotMeasureID"] = dtSBI.Rows[i]["CompanyMeasureID"];
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
           // _leCompany.t = true;
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
            if (e.Column == _coMaterielID&&Convert.ToInt32(e.Value)>0)
            {
                object obj = dtMat.Select("(ID=" + e.Value + ")")[0]["MeasureID"];
                gridView1.SetFocusedRowCellValue(_coMeasureID, obj);
            }
            try
            {
                if (e.Column == _coColorID || e.Column == _coMaterielID || e.Column == _coSizeID || e.Column == _coMeasureID)
                {
                    SetPrice(e.RowHandle);
                }
            }
            catch { }
            try
            {
                if (e.Column == _coPrice || e.Column == _coAmount)
                {
                    price = decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                    amount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
                    gridView1.SetFocusedRowCellValue(_coMoney, (price * amount).ToString("n2"));
                }
            }
            catch { }
            if (e.RowHandle > -1)
            {
                if ((e.Column == _coMaterielID || e.Column == _coColorID || e.Column == _coSizeID || e.Column == _coMeasureID) && e.Value.ToString() != "0")
                    CheckMateriel(gridView1.GetFocusedRowCellValue(_coMaterielID), gridView1.GetFocusedRowCellValue(_coColorID), gridView1.GetFocusedRowCellValue(_coSizeID), gridView1.GetFocusedRowCellValue(_coMeasureID));
                if (e.Column == _coMaterielID || e.Column == _coAmount)
                {
                    GetNeed();
                }
            }
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtSBI.NewRow();
                for (int i = 0; i < dtSBI.Columns.Count; i++)
                {
                    if (dtSBI.Columns[i].ColumnName != "LastTime")
                    {
                        dr[i] = 0;
                    }
                }
                dr["A"] = 3;
                dr["StringTaskID"] = dr["Remark"] = string.Empty;
                dr["LastTime"] = _leBackDate.val;
                //dtSBI.Rows.Add(dr.ItemArray);
            }
        }
        private void CheckMateriel(object mat, object brand, object size,object MeasureID)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (i != gridView1.FocusedRowHandle)
                {
                    if (gridView1.GetRowCellValue(i, _coMaterielID).Equals(mat) && gridView1.GetRowCellValue(i, _coColorID).Equals(brand) 
                        && gridView1.GetRowCellValue(i, _coSizeID).Equals(size)&&gridView1.GetRowCellValue(i,_coMeasureID).Equals(MeasureID))
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
            //int brand = int.Parse(gridView1.GetRowCellValue(rowID, _coColorID).ToString());
            //int mater = int.Parse(gridView1.GetRowCellValue(rowID, _coMaterielID).ToString());
            //int sizeID = int.Parse(gridView1.GetRowCellValue(rowID, _coSizeID).ToString());
            //int measureid = int.Parse(gridView1.GetRowCellValue(rowID, _coMeasureID).ToString());
            //object[] o = new object[] { _companyID, mater, brand,sizeID,measureid };
            //object p = BasicClass.GetDataSet.GetOne(blQP, "GetPrice", o);
            //gridView1.SetRowCellValue(rowID, _coPrice, p);
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                if (e.FocusedRowHandle < 0)
                    gridView1.AddNewRow();
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int c = 0;
            if (gridView1.RowCount == 0)
                c = 1;
            for (int i = c; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i] != _coLastTime)
                    gridView1.SetFocusedRowCellValue(gridView1.Columns[i], 0);
            }
            gridView1.SetFocusedRowCellValue(_coRemark, "");
            gridView1.SetFocusedRowCellValue(_coMainID, _MainID);
            gridView1.SetFocusedRowCellValue(_coA, 3);
            gridView1.SetFocusedRowCellValue(_coLastTime, _leBackDate.val);
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
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                dtSBI.AcceptChanges();
                //if (gridView1.RowCount == 8)
                //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                //else
                //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            }
            _rowCount = gridView1.RowCount;
        }
        private void _leCompany_EditValueChanged(object val, string text)
        {
            _companyID = int.Parse(val.ToString());
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                SetPrice(i);
            }
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
            dtSB.Rows[0]["IsVerify"] = 3;
            dtSB.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtSB.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
            BasicClass.GetDataSet.UpData(bllSB, dtSB);
            if (_companyID > 0)
            {
                 int MaterielID=0;
                int BrandID=0;
                decimal Price=0;
                int measureid = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    MaterielID = int.Parse(gridView1.GetRowCellValue(i, _coMaterielID).ToString());
                    BrandID = int.Parse(gridView1.GetRowCellValue(i, _coColorID).ToString());
                    Price = decimal.Parse(gridView1.GetRowCellValue(i, _coPrice).ToString());
                    measureid = int.Parse(gridView1.GetRowCellValue(i, _coMeasureID).ToString());
                    object[] o = new object[] { _companyID,  MaterielID,  BrandID,   Price, measureid,0 };
                    BasicClass.GetDataSet.ExecSql(blQP, "UpPrice", o);
                }
            }
            DataTable dtt = new DataTable();
            //for (int i = 0; i < dtSBI.Rows.Count; i++)
            //{
            //    dtt = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(ID=" + dtSBI.Rows[i]["StockInfoID"] + " )" }).Tables[0];
            //    dtt.Rows[0]["IsEnd"] = dtSBI.Rows[i]["NeedIsEnd"];
            //    BasicClass.GetDataSet.UpData(blSBI, dtt);
            //}
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllStockBackInfo, "StockToDemand", new object[] { _MainID, (int)BasicClass.Enums.PlanUseRep.已采购数量,true });
            r.ChangeText("OK");
            ShowView(bs.Position);
            SetColumnsReadOnly();
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllSB, "NeedStockCheckCanUnVerify", new object[] { _MainID })))
            {
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllStockBackInfo, "StockToDemand", new object[] { _MainID, (int)BasicClass.Enums.PlanUseRep.已采购数量, false });
                dtSB.Rows[0]["IsVerify"] = 1;
                dtSB.Rows[0]["VerifyMan"] = 0;
                dtSB.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
                BasicClass.GetDataSet.UpData(bllSB, dtSB);
                ShowView(bs.Position);
                SetColumnsReadOnly();
            }
            else
            {
                XtraMessageBox.Show("本单已有收货入库，不能弃审");
            }
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //object o = gridView1.GetFocusedRowCellValue(_coMaterielID);
            //if (o!=null&& o.ToString()!="0")
            //{
            //    int _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
            //    int _brandID = int.Parse(gridView1.GetFocusedRowCellValue(_coBrandID).ToString());
            //    DataTable dtTem = dtInfo.Clone();
            //    if (gridView1.FocusedRowHandle > -1)
            //    {
            //        DataRow[] foundRows = dtInfo.Select("(MaterielID=" + _materielID + ") And (BrandID=" + _brandID + ")");
            //        if (foundRows.Length > 0)
            //        {
            //            for (int i = 0; i < foundRows.Length; i++)
            //            {
            //                DataRow dr = dtTem.NewRow();
            //                dr.ItemArray = foundRows[i].ItemArray;
            //                dtTem.Rows.Add(dr);
            //            }
            //        }
            //    }
            //    cResult r = new cResult();
            //    r.RowChanged += new RowChangedHandler(r_RowChanged);
            //    Form fr = new Sell.SellTemForm(r, !t,dtTem, _materielID, _brandID);
            //    fr.ShowDialog();
            //}
            //else
            //{
            //    XtraMessageBox.Show("请选择款号和商标！");
            //    return;
            //}
        }
   
        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!_IsVerify)
            //{
            //    XtraMessageBox.Show("请审核后再打印单据！");
            //    return;
            //}
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
            ds.Tables["Main"].Columns.Add("审核", typeof(string));
            ds.Tables["Main"].Columns.Add("制单", typeof(string));
            ds.Tables[0].Rows[0]["ComName"] = _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;//memoEdit1.Text + "\r\n" + 
            ds.Tables[0].Rows[0]["BackDate"] = ((DateTime)(_leBackDate.val)).ToString("yyyy年MM月dd日");
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];

            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("GuiGe", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("MeasureName", typeof(string));
            dt.Columns.Add("Money", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("MaterielRemark", typeof(string));
            dt.Columns.Add("LastTime", typeof(string));
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
                dr[2] = Convert.ToDouble(gridView1.GetRowCellValue(i, _coAmount));
                dr[3] =Convert.ToDecimal( gridView1.GetRowCellValue(i, _coPrice)).ToString("C4");
                dr[4] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[5] =Convert.ToDecimal( gridView1.GetRowCellValue(i, _coMoney)).ToString("C2");
                dr[6] = gridView1.GetRowCellDisplayText(i, _coRemark);
                dr[7] = gridView1.GetRowCellDisplayText(i, _coMaterielRemark);
                if (gridView1.GetRowCellDisplayText(i, _coLastTime).Trim() != string.Empty)
                    dr[8] = Convert.ToDateTime(gridView1.GetRowCellValue(i, _coLastTime)).ToShortDateString();
                dt.Rows.Add(dr);
            }
            for (int i = dt.Rows.Count; i < 8; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);

            strTaskID = memoEdit1.Text;
            DataTable dtTem = new DataTable();
            dtTem.TableName = "dtTem";
            dtTem.Columns.Add("Materiel", typeof(string));
            dtTem.Columns.Add("PlanID", typeof(string));
            string[] ss = strTaskID.Split('；');
            bool t = false;
            for (int i = 0; i < ss.Length; i++)
            {
                if (ss[i].Length > 0)
                {
                    string[] pp = ss[i].Split('/');
                    t = false;
                    for (int j = 0; j < dtTem.Rows.Count; j++)
                    {
                        if (dtTem.Rows[j][0].Equals(pp[1]))
                        {
                            t = true;
                            dtTem.Rows[j][1] = dtTem.Rows[j][1].ToString() + pp[0] + "；";
                        }
                    }
                    if(!t)
                        dtTem.Rows.Add(pp[1], pp[0]+"；");
                }
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
            ds.Tables.Add(dtTem);
            BaseForm.PrintClass.PrintStockTable(ds);
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
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSBI.AcceptChanges();
            if(dtSBI.Rows.Count==0)
                return;

            //DataSet ddd=new DataSet();
            //ddd.DataSetName="dd";
            //DataTable dttttt = new DataTable();
            //if (!_IsVerify)
            //{
            //    dttttt = dtSBI.Copy();
            //    dttttt.Columns.Add("PlanID", typeof(int));
            //    for (int i = 0; i < dttttt.Rows.Count; i++)
            //    {
            //        dttttt.Rows[i]["PlanID"] = 0;
            //    }
            //}
            //else
            //{
            //    dttttt = BasicClass.GetDataSet.GetDS("Hownet.BLL.StockMaterielDemand", "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            //    if (dttttt.Rows.Count > 0)
            //    {
            //        _gcDemand.DataSource = dttttt;
            //        return;
            //    }
            //    dttttt = BasicClass.GetDataSet.GetDS(blSBI, "GetSemi", new object[] { (int)BasicClass.Enums.PlanUseRep.已采购数量, _MainID }).Tables[0].Copy();
            //}
            //dttttt.TableName = "dtt";
            //ddd.Tables.Add(dttttt);
            //byte[] bb = ZipJpg.Ds2Byte(ddd);
            //DataSet ssss = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "GetSemiNeed", new object[] { bb, _MainID });
            //_gcDemand.DataSource = ssss.Tables[0];
            //DataTable dTem;
            //for (int i = 0; i < ssss.Tables[0].Rows.Count; i++)
            //{
            //    dTem = ssss.Tables[0].Clone();
            //    dTem.Rows.Add(ssss.Tables[0].Rows[i].ItemArray);
            //    BasicClass.GetDataSet.Add("Hownet.BLL.StockMaterielDemand", dTem);
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BasicClass.cResult r = new cResult();
            r.RowChanged += new RowChangedHandler(r_RowChanged);
            Form fr = new frNeedInfoList(r,dtSBI);
            fr.ShowDialog();
        }

        void r_RowChanged(DataTable dt)
        {
            ShowTask();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult cr = new cResult();
            cr.TextChanged += new TextChangedHandler(cr_TextChanged);
            Form fr = new frStockLinLiao(cr, 0, _MainID);
            fr.ShowDialog();
        }

        void cr_TextChanged(string s)
        {
            ShowLinLiao();
        }
        private void ShowLinLiao()
        {

            //DataTable ddTem = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(TaskID=" + _MainID + ") And (State=" + (int)BasicClass.Enums.TableType.StockLinLiao + ")" }).Tables[0];
            //listBoxControl1.DataSource = ddTem;
            //GetNeed();
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(listBoxControl1.SelectedIndex>-1)
            //{
            //    gridControl2.DataSource = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + listBoxControl1.SelectedValue + ")" }).Tables[0];
            //}
        }
    }
}