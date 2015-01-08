using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;


namespace Hownet.Pay
{
    public partial class frAddDayWorking : DevExpress.XtraEditors.XtraForm
    {
        public frAddDayWorking()
        {
            InitializeComponent();
        }
        private void splitContainerControl2_SplitterMoved(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = 67;
        }
        string bllDW = "Hownet.BLL.DayWorking";
        string blPM = "Hownet.BLL.PayMain";
        string bllDWI = "Hownet.BLL.DayWorkingInfo";
        string blPI = "Hownet.BLL.PayInfo";
        DataTable dtMain = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtWork = new DataTable();
        DataTable dtDW = new DataTable();
        int _mainID = 0;
        bool _isVerify = false;
        BindingSource bs = new BindingSource();
        private void Demo_Load(object sender, EventArgs e)
        {
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            _leMiniEmp.FormName = _leMiniEmp.FormName = (int)BasicClass.Enums.TableType.MiniEmp;
           // dtWork = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorking, "GetList", new object[] {"(WorkTypeID=-1)" }).Tables[0];
            dtWork = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorking,  "GetList", new object[] { "(WorkTypeID=-1)"}).Tables[0];
            _coEmployeeID.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
            _reWorkID.DataSource = dtWork;
            InData();
            bs.Position = dtMain.Rows.Count - 1;
            //if (bs.Position == 0)
            //    ShowView(0);
          //  ShowView(bs.Position);

            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Enabled = _brEdit.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _brDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _brVerify.Enabled = false;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        private void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllDW, "GetIDList", null).Tables[0];
            if (dtMain.Rows.Count == 0)
            {
                dtMain.Rows.Add(dtMain.NewRow());
            }
            bs.DataSource = dtMain;
        }
        private void ShowView(int p)
        {
            _brFrist.Enabled = _brPrv.Enabled = _brNext.Enabled = _brLast.Enabled = true;
            if (p == 0)
                _brFrist.Enabled = _brPrv.Enabled = false;
            if (p == dtMain.Rows.Count - 1)
                _brNext.Enabled = _brLast.Enabled = false;
            _isVerify = false;
            dtWork.DefaultView.RowFilter = "";
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _mainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtDW = BasicClass.GetDataSet.GetDS(bllDW, "GetList", new object[] { "(ID=" + _mainID + ")" }).Tables[0];
                if (dtDW.Rows.Count > 0)
                {
                    _leMiniEmp.editVal = dtDW.Rows[0]["EmployeeID"];
                    _ldDate.val = dtDW.Rows[0]["DateTime"];
                    _ltRemark.val = dtDW.Rows[0]["Remark"].ToString();
                }
            }
            else
            {
                _mainID = 0;
                dtDW = BasicClass.GetDataSet.GetDS(bllDW, "GetList", new object[] { "(ID=" + _mainID + ")" }).Tables[0];
                DataRow dr = dtDW.NewRow();
                dr["ID"] = 0;
                dr["DateTime"] = DateTime.Today;
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllDW, "NewNum", new object[] { DateTime.Today });
                dr["EmployeeID"] = _leMiniEmp.editVal = 0;
                dr["Remark"] = _ltRemark.val = "";
                dr["IsVerify"] = 1;
                dr["VerifyMan"] = 0;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["IsEnd"] = 0;
                dr["A"] = 3;
                dtDW.Rows.Add(dr);
                DataTable dtPM = BasicClass.GetDataSet.GetDS(blPM, "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];
                if (dtPM.Rows.Count > 0)
                    _ldDate.val = _ldDate.MinDate = ((DateTime)(dtPM.Rows[0]["EndDate"])).AddDays(1);
                else
                    _ldDate.val = DateTime.Today;

            }
            _ltNum.val = DateTime.Parse(dtDW.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtDW.Rows[0]["Num"].ToString().PadLeft(3, '0');
            _isVerify = (int.Parse(dtDW.Rows[0]["IsVerify"].ToString()) == 3);
            gridView1.OptionsBehavior.Editable = !_isVerify;
            _brEdit.Enabled = _brSave.Enabled = _brVerify.Enabled = !_isVerify;
            _brDel.Enabled = !_isVerify;
            dtInfo = BasicClass.GetDataSet.GetDS(bllDWI, "GetList", new object[] { "(MainID=" + _mainID + ")" }).Tables[0];
            if (!_isVerify)
            {
                DataRow dr = dtInfo.NewRow();
                dr["ID"] = dr["WorkingID"] = dr["Amount"] = dr["Price"] = dr["Money"] = dr["EmployeeID"] = 0;
                dr["A"] = 3;
                dr["Remark"] = "";
                dr["MainID"] = _mainID;
                dtInfo.Rows.Add(dr.ItemArray);
                dtInfo.Rows.Add(dr.ItemArray);
            }
            gridControl1.DataSource = dtInfo;
            _brAddNew.Enabled = _isVerify;
            _brUnVerify.Enabled = (_isVerify && (Convert.ToInt32(dtDW.Rows[0]["IsEnd"]) == 0));
            //if (bs.Position == dtMain.Rows.Count - 1 && _mainID != 0 && _isVerify)
            //    _brUnVerify.Enabled = _brAddNew.Enabled = true;
            //else
            //    _brUnVerify.Enabled = _brAddNew.Enabled = false;
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal money = 0;
            if (e.Column == _coWorkID && e.Value != null)
            {
                object obj = _reWorkID.GetDataSourceValue("Price", _reWorkID.GetDataSourceRowIndex("ID", e.Value));
                gridView1.SetFocusedRowCellValue(_coPrice, obj);
            }
            if (e.Column == _coAmount && e.Value != null)
            {
                money = decimal.Parse(e.Value.ToString()) * decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                gridView1.SetFocusedRowCellValue(_coMoney, money);
            }
            if (e.Column == _coPrice && gridView1.GetFocusedRowCellValue(_coAmount).ToString() != "")
            {
                money = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString()) * decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                gridView1.SetFocusedRowCellValue(_coMoney, money);
            }
            if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
            {
                gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            //if(e.Column==_coEmployeeID&&e.Value!=null)
            //{
            //    DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(ID=" + e.Value + ")" }).Tables[0];
            //    if(dtTem.Rows.Count>0)
            //    {
            //        gridView1.SetFocusedRowCellValue(_coWorkID, Convert.ToInt32(dtTem.Rows[0]["LassMoney"]));
            //    }
            //}
            if (!_isVerify && e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtInfo.NewRow();
                dr["ID"] = dr["WorkingID"] = dr["Amount"] = dr["Price"] = dr["Money"] = 0;
                dr["A"] = 3;
                dr["Remark"] = "";
                dr["MainID"] = _mainID;
                dtInfo.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_isVerify)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
        }

        private void barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                if (!_isVerify && int.Parse(dtDW.Rows[0]["IsEnd"].ToString()) == 0)
                {
                    BasicClass.GetDataSet.ExecSql(bllDW, "Delete", new object[] { _mainID });
                    BasicClass.GetDataSet.ExecSql(bllDWI, "DeleteByMainID", new object[] { _mainID });
                    InData();
                    bs.Position = dtMain.Rows.Count - 1;
                    if (bs.Position == 0)
                        ShowView(bs.Position);
                }
            }
        }

        private void _brVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!Save())
                return;
            if (_mainID== 0)
            {
                XtraMessageBox.Show("本单还未保存，不能审核！");
                return;
            }
            dtDW.Rows[0]["IsVerify"] = 3;
            dtDW.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtDW.Rows[0]["VerifyDate"] = DateTime.Now;
            BasicClass.GetDataSet.UpData(bllDW, dtDW);
            gridView1.CloseEditor();
            DataTable dtP = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayInfo", "GetList", new object[] {"(ID=0)" }).Tables[0];
            dtP.Rows.Add(dtP.NewRow());
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtInfo.Rows[i]["WorkingID"]) != 0)
                {
                    dtP.Rows[0]["ID"] = 0;
                    dtP.Rows[0]["EmployeeID"] =dtInfo.Rows[i]["EmployeeID"];
                    dtP.Rows[0]["DateTime"] = dtDW.Rows[0]["DateTime"];
                    dtP.Rows[0]["MaterielID"] = 0;
                    dtP.Rows[0]["WorkingID"] = dtInfo.Rows[i]["WorkingID"];
                    dtP.Rows[0]["Amount"] = dtInfo.Rows[i]["Amount"];
                    dtP.Rows[0]["Price"] = dtInfo.Rows[i]["Price"];
                    dtP.Rows[0]["ProductWorkingID"] = 0;
                    dtP.Rows[0]["WorkticketInfoID"] = 0;
                    dtP.Rows[0]["IsSum"] = false;
                    dtP.Rows[0]["BreakID"] = 0;
                    dtP.Rows[0]["ColorID"] = 0;
                    dtP.Rows[0]["SizeID"] = 0;
                    dtP.Rows[0]["BoxNum"] = _mainID * -1;
                    dtP.Rows[0]["OderNum"] = "";
                    dtP.Rows[0]["IsDay"] = 1;
                    dtP.Rows[0]["A"] = 1;
                    BasicClass.GetDataSet.Add("Hownet.BLL.PayInfo", dtP);
                }
            }
            //this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            ShowView(bs.Position);
        }

        private void _brUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(dtDW.Rows[0]["IsEnd"].ToString()) == 1)
            {
                XtraMessageBox.Show("本单已作汇总处理，不能反审核！");
                return;
            }
            dtDW.Rows[0]["IsVerify"] = 1;
            dtDW.Rows[0]["VerifyMan"] = 0;
            dtDW.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");

            BasicClass.GetDataSet.UpData(bllDW, dtDW);
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.PayInfo", "DelDayWorking", new object[] { _mainID * -1 });
            ShowView(bs.Position);
        }

        private void _brPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName =BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
            //if (!t)
            //{
            //    XtraMessageBox.Show("本单未审核，报表中将没有本单记录！");
            //}
            //DateTime dte = DateTime.Parse(dtHBM.Rows[0]["DateTime"].ToString());
            //DataSet ds = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayInfo", "GetDayReport", new object[] { dte });
            //ds.Tables[0].TableName = "DayReport";
            //ds.Tables[0].Columns.Add("Date", typeof(string));
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        ds.Tables[0].Rows[i]["Date"] = dte.ToString("yyyy年MM月dd日");
            //    }
            //}
            //BaseForm.PrintClass.PrintDay(ds);
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                if (gridView1.FocusedRowHandle > -1)
                {
                    if (gridView1.GetFocusedRowCellValue(_coInfoID).ToString() != "")
                    {
                        BasicClass.GetDataSet.ExecSql(bllDWI, "Delete", new object[] { int.Parse(gridView1.GetFocusedRowCellValue(_coInfoID).ToString()) });
                        BasicClass.GetDataSet.ExecSql(blPI, "Delete", new object[] { int.Parse(gridView1.GetFocusedRowCellValue(_coPayInfoID).ToString()) });
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    }
                }
            }
        }

        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            if (Save())
            {
                int d = bs.Position;
                InData();
                bs.Position = d;
                if (d == dtMain.Rows.Count - 1)
                    ShowView(d);
            }
        }
        private bool Save()
        {
            //if (_leMiniEmp.editVal == null || int.Parse(_leMiniEmp.editVal.ToString()) == 0)
            //{
            //    XtraMessageBox.Show("请选择员工！");
            //    return false;
            //}
            DataTable dtPM = BasicClass.GetDataSet.GetDS(blPM, "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];
            if (dtPM.Rows.Count > 0)
            {
                if (!(Convert.ToDateTime(_ldDate.val) > Convert.ToDateTime(dtPM.Rows[0]["EndDate"])))
                {
                    XtraMessageBox.Show("当前日期的工资已做结算，不能添加在当期！");
                    return false;
                }
            }
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtInfo.AcceptChanges();
            dtDW.Rows[0]["DateTime"] = _ldDate.val;
            dtDW.Rows[0]["EmployeeID"] = _leMiniEmp.editVal;
            dtDW.Rows[0]["Remark"] = _ltRemark.val;
            if (_mainID == 0)
            {
                dtDW.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllDW, "NewNum", new object[] { DateTime.Parse(_ldDate.val.ToString()) });
                dtMain.Rows[bs.Position]["ID"] = dtDW.Rows[0]["ID"] = _mainID = BasicClass.GetDataSet.Add(bllDW, dtDW);
            }
            else
            {
                BasicClass.GetDataSet.UpData(bllDW, dtDW);
            }
            DataTable dtt = dtInfo.Clone();
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                int a = int.Parse(dtInfo.Rows[i]["A"].ToString());
                if (a > 1)
                {

                    if (Convert.ToInt32(dtInfo.Rows[i]["WorkingID"]) != 0 && Convert.ToInt32(dtInfo.Rows[i]["EmployeeID"]) > 0 && Convert.ToDecimal(dtInfo.Rows[i]["Money"]) != 0)
                    {
                        dtt.Clear();
                        dtInfo.Rows[i]["MainID"] = _mainID;
                        dtt.Rows.Add(dtInfo.Rows[i].ItemArray);
                        if (a == 3)
                            dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllDWI, dtt);
                        else if (a == 2)
                            BasicClass.GetDataSet.UpData(bllDWI, dtt);
                        dtInfo.Rows[i]["A"] = 1;
                    }
                    else
                    {
                        if (Convert.ToInt32(dtInfo.Rows[i]["ID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(bllDWI, "Delete", new object[] { Convert.ToInt32(dtInfo.Rows[i]["ID"]) });
                        }
                    }
                }
            }
            return true;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!_isVerify)
            {
                gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < gridView1.RowCount - 1);
            }
        }
    }
}