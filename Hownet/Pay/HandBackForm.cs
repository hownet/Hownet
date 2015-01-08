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
    public partial class HandBackForm : DevExpress.XtraEditors.XtraForm
    {
        public HandBackForm()
        {
            InitializeComponent();
        }
        private void splitContainerControl2_SplitterMoved(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = 67;
        }
        string blHBM = "Hownet.BLL.HandBackMain";
        string blPM = "Hownet.BLL.PayMain";
        string blHBI = "Hownet.BLL.HandBackInfo";
        string blPI = "Hownet.BLL.PayInfo";
        DataTable dtMain = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtWork = new DataTable();
        DataTable dtHBM = new DataTable();
        int _mainID = 0;
        bool t = false;
        BindingSource bs = new BindingSource();
        private void Demo_Load(object sender, EventArgs e)
        {
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            _leMiniEmp.FormName = _leMiniEmp.FormName = (int)BasicClass.Enums.TableType.MiniEmp;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coEmployeeID.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
            dtWork = BasicClass.GetDataSet.GetDS(blHBM, "GetWorkByPW", null).Tables[0];
            DataRow dr = dtWork.NewRow();
            dr["WorkingID"] = dr["Price"] = dr["PWID"] = dr["MaterielID"] = 0;
            dr["WorkingName"] = string.Empty;
            dtWork.Rows.Add(dr);
            _reWorkID.DataSource = dtWork;
            InData();
            bs.Position = dtMain.Rows.Count - 1;
            ShowView(bs.Position);
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _brVerify.Enabled = _brUnVerify.Enabled = _brAddNew.Enabled = _brDel.Enabled = _brSave.Enabled = _brEdit.Enabled = false;
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //}
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        private void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(blHBM, "GetIDList", null).Tables[0];
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
            t = false;
            dtWork.DefaultView.RowFilter = "";
            if (dtMain.DefaultView[p]["MainID"].ToString() != "")
            {
                _mainID = int.Parse(dtMain.DefaultView[p]["MainID"].ToString());
                dtHBM = BasicClass.GetDataSet.GetDS(blHBM, "GetList", new object[] { "(MainID=" + _mainID + ")" }).Tables[0];
                if (dtHBM.Rows.Count > 0)
                {
                    _leMiniEmp.editVal = dtHBM.Rows[0]["EmployeeID"];
                    _ldDate.val = dtHBM.Rows[0]["DateTime"];
                    _ltRemark.val = dtHBM.Rows[0]["Remark"].ToString();
                }
            }
            else
            {
                _mainID = 0;
                dtHBM = BasicClass.GetDataSet.GetDS(blHBM, "GetList", new object[] { "(MainID=" + _mainID + ")" }).Tables[0];
                DataRow dr = dtHBM.NewRow();
                dr["MainID"] = 0;
                dr["DateTime"] = DateTime.Today;
                dr["Num"] = BasicClass.GetDataSet.GetOne(blHBM, "NewNum", new object[] { DateTime.Today });
                dr["EmployeeID"] = _leMiniEmp.editVal = 0;
                dr["Remark"] = _ltRemark.val = "";
                dr["IsVerify"] = 1;
                dr["VerifyManID"] = 0;
                dr["VerifyDateTime"] = DateTime.Parse("1900-1-1");
                dr["IsEnd"] = 0;
                dtHBM.Rows.Add(dr);
                DataTable dtPM = BasicClass.GetDataSet.GetDS(blPM, "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];
                if (dtPM.Rows.Count > 0)
                    _ldDate.val = _ldDate.MinDate = ((DateTime)(dtPM.Rows[0]["EndDate"])).AddDays(1);
                else
                    _ldDate.val = DateTime.Today;

            }
            _ltNum.val = DateTime.Parse(dtHBM.Rows[0]["DateTime"].ToString()).ToString("yyyyMMdd") + dtHBM.Rows[0]["Num"].ToString().PadLeft(3, '0');

            t = (int.Parse(dtHBM.Rows[0]["IsVerify"].ToString()) == 3);

            if (t || int.Parse(dtHBM.Rows[0]["IsEnd"].ToString()) == 1)
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            else
                this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            gridView1.OptionsBehavior.Editable = !t;
            _brEdit.Enabled = _brSave.Enabled = _brVerify.Enabled = !t;
            _brDel.Enabled = !t;
            dtInfo = BasicClass.GetDataSet.GetDS(blHBI, "GetList", new object[] { "(MainID=" + _mainID + ")" }).Tables[0];
            gridControl1.DataSource = dtInfo;
            _brAddNew.Enabled = t;
            _brUnVerify.Enabled = (t && (Convert.ToInt32(dtHBM.Rows[0]["IsEnd"]) == 0));
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
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(_coInfoID, 0);
            gridView1.SetFocusedRowCellValue(_coMainID, _mainID);
            gridView1.SetFocusedRowCellValue(_coA, 3);
            gridView1.SetFocusedRowCellValue(_coPrice, 0);
            gridView1.SetFocusedRowCellValue(_coPayInfoID, 0);
            gridView1.SetFocusedRowCellValue(_coWorkingID, 0);
            gridView1.SetFocusedRowCellValue(_coAmount, 0);
            gridView1.SetFocusedRowCellValue(_coMoney, 0);
           // gridView1.SetFocusedRowCellValue(_coEmployeeID, 0);
            gridView1.SetFocusedRowCellValue(_coDateTime, _ldDate.val);
            //gridView1.SetFocusedRowCellValue(_coWorkID, 0);
        }

        private void _reWorkID_QueryPopUp(object sender, CancelEventArgs e)
        {
            int materielID = 0;
            if (gridView1.GetFocusedRowCellValue(_coMaterielID) != null)
            {
                materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
            }
            string sql = "MaterielID =" + materielID;
            dtWork.DefaultView.RowFilter = sql;
        }

        private void _reWorkID_QueryCloseUp(object sender, CancelEventArgs e)
        {
            dtWork.DefaultView.RowFilter = "";
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal money = 0;
            if (e.Column == _coMaterielID)
            {
                gridView1.SetFocusedRowCellValue(_coPriceID, 0);
            }
            if (e.Column == _coWorkID && e.Value != null)
            {
                object obj = _reWorkID.GetDataSourceValue("Price", _reWorkID.GetDataSourceRowIndex("PWID", e.Value));
                gridView1.SetFocusedRowCellValue(_coPrice, obj);
                obj = _reWorkID.GetDataSourceValue("WorkingID", _reWorkID.GetDataSourceRowIndex("PWID", e.Value));
                gridView1.SetFocusedRowCellValue(_coWorkingID, obj);
            }
            if (e.Column == _coAmount && e.Value != null)
            {
                money = decimal.Parse(e.Value.ToString()) * decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                gridView1.SetFocusedRowCellValue(_coMoney, money);
                if (gridView1.GetFocusedRowCellValue(_coEmployeeID)==null)
                {
                    gridView1.SetFocusedRowCellValue(_coEmployeeID, 0);
                }
            }
            if (e.Column == _coPrice && gridView1.GetFocusedRowCellValue(_coAmount).ToString() != "")
            {
                money = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString()) * decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                gridView1.SetFocusedRowCellValue(_coMoney, money);
            }
            try
            {
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                {
                    gridView1.SetFocusedRowCellValue(_coA, 2);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                gridView1.OptionsBehavior.Editable = true;
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            }
        }

        private void barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                if (!t && int.Parse(dtHBM.Rows[0]["IsEnd"].ToString()) == 0)
                {
                    BasicClass.GetDataSet.ExecSql(blHBM, "Delete", new object[] { _mainID });
                    BasicClass.GetDataSet.ExecSql(blHBI, "DeleteByMainID", new object[] { _mainID });
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
            dtHBM.Rows[0]["IsVerify"] = 3;
            dtHBM.Rows[0]["VerifyManID"] = BasicClass.UserInfo.UserID;
            dtHBM.Rows[0]["VerifyDateTime"] = DateTime.Now;
            BasicClass.GetDataSet.UpData(blHBM,  dtHBM);
            gridView1.CloseEditor();
            DataTable dtP = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayInfo", "GetList", new object[] {"(ID=0)" }).Tables[0];
            dtP.Rows.Add(dtP.NewRow());
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                dtP.Rows[0]["ID"] = 0;
                object o = dtInfo.Rows[i]["EmployeeID"];
                if (dtInfo.Rows[i]["EmployeeID"] == null)
                    dtP.Rows[0]["EmployeeID"] = _leMiniEmp.editVal;
                else if (dtInfo.Rows[i]["EmployeeID"] == DBNull.Value)
                    dtP.Rows[0]["EmployeeID"] = _leMiniEmp.editVal;
                else
                    dtP.Rows[0]["EmployeeID"] = dtInfo.Rows[i]["EmployeeID"];
                dtP.Rows[0]["DateTime"] = dtInfo.Rows[i]["DateTime"];
                dtP.Rows[0]["MaterielID"] = dtInfo.Rows[i]["MaterielID"];
                dtP.Rows[0]["WorkingID"] = dtInfo.Rows[i]["WorkingID"];
                dtP.Rows[0]["Amount"] = dtInfo.Rows[i]["Amount"];
                dtP.Rows[0]["Price"] = dtInfo.Rows[i]["Price"];
                dtP.Rows[0]["ProductWorkingID"] = dtInfo.Rows[i]["PriceID"];
                dtP.Rows[0]["WorkticketInfoID"] = 0;
                dtP.Rows[0]["IsSum"] = false;
                dtP.Rows[0]["BreakID"] = int.Parse(dtInfo.Rows[i]["InfoID"].ToString()) * -1;
                dtP.Rows[0]["ColorID"] = _mainID * -1;
                dtP.Rows[0]["SizeID"] = 0;
                dtP.Rows[0]["BoxNum"] = 0;
                dtP.Rows[0]["OderNum"] = "";
                BasicClass.GetDataSet.Add("Hownet.BLL.PayInfo", dtP);
            }
            //this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            ShowView(bs.Position);
        }

        private void _brUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(dtHBM.Rows[0]["IsEnd"].ToString()) == 1)
            {
                XtraMessageBox.Show("本单已作汇总处理，不能反审核！");
                return;
            }
            dtHBM.Rows[0]["IsVerify"] = 1;
            dtHBM.Rows[0]["VerifyManID"] = 0;
            dtHBM.Rows[0]["VerifyDateTime"] = DateTime.Parse("1900-1-1");

            BasicClass.GetDataSet.UpData(blHBM, dtHBM);
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.PayInfo", "DeleteHand", new object[] { _mainID * -1 });
            //this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            ShowView(bs.Position);
        }

        private void _brPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("本单未审核，报表中将没有本单记录！");
            }
            DateTime dte = DateTime.Parse(dtHBM.Rows[0]["DateTime"].ToString());
          DataSet ds = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayInfo", "GetDayReport", new object[] { dte });
          ds.Tables[0].TableName = "DayReport";
          ds.Tables[0].Columns.Add("Date", typeof(string));
          if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["Date"] = dte.ToString("yyyy年MM月dd日");
                }
            }
            BaseForm.PrintClass.PrintDay(ds);
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                if (gridView1.FocusedRowHandle > -1)
                {
                    if (gridView1.GetFocusedRowCellValue(_coInfoID).ToString() != "")
                    {
                        BasicClass.GetDataSet.ExecSql(blHBI, "Delete", new object[] { int.Parse(gridView1.GetFocusedRowCellValue(_coInfoID).ToString()) });
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
            dtHBM.Rows[0]["DateTime"] = _ldDate.val;
            dtHBM.Rows[0]["EmployeeID"] = _leMiniEmp.editVal;
            dtHBM.Rows[0]["Remark"] = _ltRemark.val;
            if (_mainID == 0)
            {
                dtHBM.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(blHBM, "NewNum", new object[] { DateTime.Parse(_ldDate.val.ToString()) });
                dtMain.Rows[bs.Position]["MainID"] = dtHBM.Rows[0]["MainID"] = _mainID = BasicClass.GetDataSet.Add(blHBM, dtHBM);
            }
            else
            {
                BasicClass.GetDataSet.UpData(blHBM, dtHBM);
            }
            DataTable dtt = dtInfo.Clone();
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                int a = int.Parse(dtInfo.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    if (Convert.ToInt32(dtInfo.Rows[i]["PriceID"]) > 0 && Convert.ToInt32(dtInfo.Rows[i]["Amount"]) != 0)
                    {
                        dtt.Clear();
                        dtInfo.Rows[i]["MainID"] = _mainID;
                        dtt.Rows.Add(dtInfo.Rows[i].ItemArray);
                        if (a == 3)
                            dtInfo.Rows[i]["InfoID"] = BasicClass.GetDataSet.Add(blHBI, dtt);
                        else if (a == 2)
                            BasicClass.GetDataSet.UpData(blHBI, dtt);
                        dtInfo.Rows[i]["A"] = 1;
                    }
                    else
                    {
                        if (Convert.ToInt32(dtInfo.Rows[i]["InfoID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(blHBI, "Delete", new object[] { Convert.ToInt32(dtInfo.Rows[i]["InfoID"]) });
                        }
                    }
                }
            }
            return true;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0 && gridView1.FocusedColumn == _coEmployeeID)
            {
                object o = gridView1.GetFocusedValue();
                if (o == null)
                {
                    gridView1.SetFocusedValue(_leMiniEmp.editVal);
                }
            }
        }
    }
}