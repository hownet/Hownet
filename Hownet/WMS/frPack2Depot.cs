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
    public partial class frPack2Depot : DevExpress.XtraEditors.XtraForm
    {
        public frPack2Depot()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frPack2Depot(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _packID = 0;
        int _depotID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        private string bllPDM = "Hownet.BLL.Pack2DepotMain";
        private string bllPDI = "Hownet.BLL.Pack2DepotInfo";
        DataTable dtPDM= new DataTable();
        DataTable dtPDI = new DataTable();
        DataTable dt = new DataTable();
        string backDate = string.Empty;
        bool _isVerify = false;
        object _oldMat = null;
        object _oldBrand = null;
        DataTable dtDeoptInfo = new DataTable();
        bool _UseDepotInfo = false;
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
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVierfy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='成品需按货架存放')" }).Tables[0];
            if(dtTem.Rows.Count>0)
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
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
          _coDepotBrandID.ColumnEdit=  _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _leDepot.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] {"仓库" }).Tables[0];
            _lePack.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "包装" }).Tables[0];
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _lePack.EditValue = _leDepot.EditValue = 0;
            _coTaskID.ColumnEdit = BaseForm.RepositoryItem._reTaskNum;
         }

        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllPDM, "GetIDList", null).Tables[0];
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
             dtPDI.Rows.Clear();
             object o = dtMain.DefaultView[p]["ID"];
             if (dtMain.DefaultView[p]["ID"].ToString() != ""&&o.ToString().Length>0)
             {
                 _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                 dtPDM = BasicClass.GetDataSet.GetDS(bllPDM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                 dtPDI = BasicClass.GetDataSet.GetDS(bllPDI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
             }
             else
             {
                 dtPDM = BasicClass.GetDataSet.GetDS(bllPDM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtPDM.NewRow();
                 dr["PackID"] = dr["ID"] = dr["DepotID"] = dr["UpData"] = dr["VerifyMan"] =  _MainID = 0;
                 dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                 dr["FillMan"] = BasicClass.UserInfo.UserID;
                 dr["IsVerify"] = false;
                 dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                 dr["Remark"] = "";
                 dr["Num"] = 0;
                 dtPDM.Rows.Add(dr);
                 _brAddNew.Enabled = false;
             }
             _upData = int.Parse(dtPDM.Rows[0]["UpData"].ToString());
             _isVerify = Convert.ToInt32 (dtPDM.Rows[0]["IsVerify"])>2;
             _ldDate.val = dtPDM.Rows[0]["DateTime"];
             _leDepot.EditValue =_depotID= int.Parse(dtPDM.Rows[0]["DepotID"].ToString());
             _lePack.EditValue =_packID=  int.Parse(dtPDM.Rows[0]["PackID"].ToString());
             _ltRemark.val = dtPDM.Rows[0]["Remark"].ToString();
             _ltNum.val = DateTime.Parse(dtPDM.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtPDM.Rows[0]["Num"].ToString().PadLeft(3, '0');
             gridControl1.DataSource = dtPDI;
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
           if( Save(false))
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
        private bool Save(bool isverify)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPDI.AcceptChanges();
            _depotID = Convert.ToInt32(_leDepot.EditValue);
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择收货仓库！");
                return false;
            }
            if(_UseDepotInfo)
            {
                for(int i=0;i<dtPDI.Rows.Count;i++)
                {
                    object aa = dtPDI.Rows[0]["NowAmount"];
                    object bbb = dtPDI.Rows[i]["DepotInfoID"];
                    if (Convert.ToDecimal(dtPDI.Rows[i]["NowAmount"]) > 0 && Convert.ToInt32(dtPDI.Rows[i]["DepotInfoID"]) == 0)
                    {
                        XtraMessageBox.Show("请选择存放货位！");
                        return false;
                    }
                }
            }
            dtPDM.Rows[0]["DepotID"] = _depotID;
            dtPDM.Rows[0]["Remark"] = _ltRemark.val;
            dtPDM.Rows[0]["PackID"] = _packID;
            dtPDM.Rows[0]["DateTime"] = _ldDate.val;
            dtPDM.Rows[0]["State"] = 0;
            dtPDM.Rows[0]["A"] = 1;
            if (isverify)
            {
                dtPDM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
                dtPDM.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
                dtPDM.Rows[0]["IsVerify"] = 3;
            }
            else
            {
                dtPDM.Rows[0]["VerifyMan"] = 0;
                dtPDM.Rows[0]["VerifyDate"] = Convert.ToDateTime("1900-1-1");
                dtPDM.Rows[0]["IsVerify"] = 1;
            }
            if (_MainID == 0)
            {
                dtPDM.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllPDM, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime() });
                dtMain.Rows[bs.Position]["ID"] = dtPDM.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllPDM, dtPDM);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllPDM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtPDM.Rows[0]["UpData"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllPDM, dtPDM);
                }
            }
            DataTable dtt = dtPDI.Clone();
            int a = 0;
            for (int i = 0; i < dtPDI.Rows.Count; i++)
            {
                 a = int.Parse(dtPDI.Rows[i]["A"].ToString());
                if (a > 1)
                {

                        dtPDI.Rows[i]["MainID"] = _MainID;
                        dtt.Clear();
                        dtt.Rows.Add(dtPDI.Rows[i].ItemArray);
                        if (a == 3 &&Convert.ToDecimal(dtPDI.Rows[i]["NowAmount"])!=0)//新增
                        {
                            dtPDI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllPDI, dtt);
                            dtPDI.Rows[i]["A"] = 1;
                        }
                        else if (a == 2)//修改
                        {
                            BasicClass.GetDataSet.UpData(bllPDI, dtt);
                            dtPDI.Rows[i]["A"] = 1;
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
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMainID)) == 0)
                    gridView1.SetFocusedRowCellValue(_coA, 3);
                else
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if(e.Column==_coNowAmount)
            {
                decimal aaa = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coPackAmount));
                if(aaa<Convert.ToDecimal(e.Value))
                {
                    XtraMessageBox.Show("超过包装部现有数量！");
                    gridView1.SetFocusedRowCellValue(_coNowAmount, gridView1.GetFocusedRowCellValue(_coPackAmount));

                }
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
                        object[] o = new object[] {_MainID };
                        BasicClass.GetDataSet.ExecSql(bllPDI, "DeleteByMainID", o);
                        BasicClass.GetDataSet.ExecSql(bllPDM, "Delete", o);
                    }

                    if (dtMain.Rows.Count > 1)
                    {
                        dtMain.Rows.RemoveAt(bs.Position);
                        _brAddNew.Enabled = (_isVerify && bs.Position == dtMain.Rows.Count - 1);
                    }
                    else
                    {
                        InData();
                        ShowView(0);
                    }
                }
            }
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridView1.FocusedRowHandle > -1)
            //{
            //    if (DialogResult.Yes == MessageBox.Show("是否确认删除该条记录？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
            //    {
            //        int id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
            //        if (id > 0)
            //        {
            //            BasicClass.GetDataSet.ExecSql(bllPDI, "Delete", new object[] { id });
            //        }
            //        gridView1.DeleteRow(gridView1.FocusedRowHandle);
            //    }
            //}
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool f = _MainID == 0;
            if (Save(true))
            {
                BasicClass.GetDataSet.ExecSql(bllPDI, "Verify", new object[] {_MainID,true });
            }
            else
            {
                return;
            }

            ShowView(bs.Position);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                BasicClass.GetDataSet.ExecSql(bllPDI, "Verify", new object[] { _MainID, false });

            ShowView(bs.Position);
        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseForm.PrintClass.PrintPack2DTable(GetPrintDS(), false);
        }
        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseForm.PrintClass.PrintPack2DTable(GetPrintDS(), true);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BaseForm.PrintClass.PrintPack2DTable(GetPrintDS(), false);
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
            
            dtOne.Rows.Add("包装部入库", _ltNum.EditVal, _leDepot.Text, _ldDate.strLab, _ltRemark.EditVal);
            try
            {
                dtOne.Rows[0]["审核"] = dtUser.Select("(ID=" + Convert.ToInt32(dtPDM.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            }
            catch
            {
            }
            try
            {
                dtOne.Rows[0]["制单"] = dtUser.Select("(ID=" + Convert.ToInt32(dtPDM.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
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
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow dr = dtTwo.NewRow();
                dr[0] = gridView1.GetRowCellDisplayText(i, _coTaskID);
                dr[1] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                dr[2] = gridView1.GetRowCellDisplayText(i, _coBrandID);
                dr[3] = gridView1.GetRowCellDisplayText(i, _coColorID);
                dr[4] = gridView1.GetRowCellDisplayText(i, _coSizeID);
                dr[5] = gridView1.GetRowCellDisplayText(i, _coColorOneID);
                dr[6] = gridView1.GetRowCellDisplayText(i, _coColorTwoID);
                dr[7] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coNowAmount));
                _TaskID=Convert.ToInt32(gridView1.GetRowCellValue(i, _coTaskID));
                if (_TaskID > 0)
                {
                    DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPackAmount, "GetList", new object[] { "(PlanID=" + _TaskID + ")  And (MListID=" + Convert.ToInt32(gridView1.GetRowCellValue(i, _coMListID)) + ")" }).Tables[0];
                    if (dtTem.Rows.Count > 0)
                    {
                        dr[8] = dtTem.Rows[0]["Amount"];
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
                dr[9] = gridView1.GetRowCellDisplayText(i, _coRemark); dtTwo.Rows.Add(dr);

            }
            ds.Tables.Add(dtTwo);
            return ds;
        }
        private void _lePack_EditValueChanged(object sender, EventArgs e)
        {
            if (_MainID == 0)
            {
                _packID = Convert.ToInt32(_lePack.EditValue);
                dtPDI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPackAmount, "GetPackList", new object[] {_packID }).Tables[0];
                gridControl1.DataSource = dtPDI;
            }
        }

        private void _leDepot_EditValueChanged(object sender, EventArgs e)
        {
            _depotID = Convert.ToInt32(_leDepot.EditValue);
            dtDeoptInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "(ParentID=" + _depotID + ")" }).Tables[0];
            _reDepotInfoID.DataSource = dtDeoptInfo;
            if (!_isVerify)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellDisplayText(i, _coDepotInfoID) == string.Empty)
                        gridView1.SetRowCellValue(i, _coDepotInfoID, 0);
                }
            }
        }





    }
}