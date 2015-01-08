using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Pay
{
    public partial class BarPayCosts : DevExpress.XtraEditors.XtraForm
    {
        public BarPayCosts()
        {
            InitializeComponent();
        }
        BindingSource bs = new BindingSource();
        DataTable dtMain = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtPC = new DataTable();
        DataTable dtPM = new DataTable();
        string blPC = "Hownet.BLL.PayCosts";
        string blPCI = "Hownet.BLL.PayCostsInfo";
        string blOT = "Hownet.BLL.OtherType";
        string blEmp = "Hownet.BLL.MiniEmp";
        string blPM = "Hownet.BLL.PayMain";
        int _mainID = 0;
        bool t = false;
        private void StockBackForm_Load(object sender, EventArgs e)
        {
            _leType.FormName = (int)BasicClass.Enums.TableType.Costs;
            _coMoney.ColumnEdit =BaseForm. RepositoryItem._re2Money;
            _coEmployeeID.ColumnEdit =BaseForm. RepositoryItem._reMiniEmp;
            InData();
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            bs.MoveLast();
            if (bs.Position == 0)
                ShowInfo(0);
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //   _barVerify.Enabled=_barUnVerify.Enabled= _barNew.Enabled = _barDel.Enabled = _barSave.Enabled = _barEdit.Enabled = false;
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //}
       }
        private void ShowInfo(int p)
        {
            _barFrist.Enabled = _barPve.Enabled = _barNext.Enabled = _barLast.Enabled = true;
            if (p == 0)
                _barFrist.Enabled = _barPve.Enabled = false;
            if (p == dtMain.Rows.Count - 1)
                _barNext.Enabled = _barLast.Enabled = false;
            panelControl1.Visible = t = false;
            dtPM = BasicClass.GetDataSet.GetDS(blPM, "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];

            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _mainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtPC = BasicClass.GetDataSet.GetDS(blPC, "GetList", new object[] { "(ID=" + _mainID + ")" }).Tables[0];
                if (dtPC.Rows.Count > 0)
                {
                    _ldDate.val = dtPC.Rows[0]["DateTime"];
                    _leType.editVal = dtPC.Rows[0]["TypeID"];
                    _ltNum.val = "FY-" + ((DateTime)(dtPC.Rows[0]["DateTime"])).ToString("yyyyMMdd") + dtPC.Rows[0]["Num"].ToString().PadLeft(3, '0');
                    _ltRemark.val = dtPC.Rows[0]["Remark"].ToString();
                }
                _leType.IsNotCanEdit = true;
            }
            else
            {
                _mainID = 0;
                dtPC = BasicClass.GetDataSet.GetDS(blPC, "GetList", new object[] { "(ID=" + _mainID + ")" }).Tables[0];
                DataRow dr = dtPC.NewRow();
                dr["VerifyMan"] = 0;
                dr["ID"] = 0;
                dr["TypeID"] = 0;
                if (dtPM.Rows.Count > 0)
                    _ldDate.val = _ldDate.MinDate = ((DateTime)(dtPM.Rows[0]["EndDate"])).AddDays(1);
                else
                    _ldDate.val = DateTime.Today;
                dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["Remark"] = _ltRemark.val = "";
                dr["Num"] = 0;
                _ltNum.val = "FY-";
                _leType.IsNotCanEdit = false;
                dr["TypeID"] = 0;
                dtPC.Rows.Add(dr);
                panelControl1.Visible = true;
                _leType.editVal = 0;
            }
            t = (Convert.ToInt32(dtPC.Rows[0]["IsVerify"]) > 2);
            dtInfo = BasicClass.GetDataSet.GetDS(blPCI, "GetList", new object[] { "(MainID=" + _mainID + ")" }).Tables[0];
            if (!t)
            {
                DataRow dr = dtInfo.NewRow();
                dr["A"] = 3;
                dr["ID"] = dr["MainID"] = dr["EmployeeID"] = dr["Money"] = dr["IsDeposit"] = 0;
                dr["Remark"] = string.Empty;
                dtInfo.Rows.Add(dr.ItemArray);
                dtInfo.Rows.Add(dr.ItemArray);
            }
            gridControl1.DataSource = dtInfo;
            _barEdit.Enabled = _barAddInfo.Enabled = _barSave.Enabled = _barDel.Enabled = _barVerify.Enabled = gridView1.OptionsBehavior.Editable = !t;
            _barAddTable.Enabled = _barUnVerify.Enabled = t;
            _ltNum.IsCanEdit = false;
            if (t)
            {

                if (dtPM.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(_ldDate.val) > Convert.ToDateTime(dtPM.Rows[0]["EndDate"]))
                    {
                        _barUnVerify.Enabled = true;
                    }
                }
                if (_leType.valStr == "预支款")
                {
                    //  _barUnVerify.Enabled = false;
                }
            }
            else
            {
                _barUnVerify.Enabled = false;
            }

            //if (t)
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //else
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }
        private void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowInfo(bs.Position);
        }

        private void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(blPC, "GetIDList", null).Tables[0];
            if (dtMain.Rows.Count == 0)
            {
                dtMain.Rows.Add(dtMain.NewRow());
            }
            bs.DataSource = dtMain;
        }
        #region 记录移动
        private void _barPve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }

        private void _barNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }

        private void _barLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            decimal mo = 0;
            try
            {
                mo = decimal.Parse(textEdit1.EditValue.ToString());
            }
            catch { }
            gridControl1.DataSource = BasicClass.GetDataSet.GetDS(blEmp, "GetCosts", new object[] { mo }).Tables[0];
        }

        private void _barAddTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.AddNew();
        }

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
            int d = bs.Position;
            InData();
            bs.Position = d;
            if (d == dtMain.Rows.Count - 1)
                ShowInfo(d);
        }
        private void Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPC.Rows[0]["DateTime"] = _ldDate.val;
            dtPC.Rows[0]["TypeID"] = _leType.editVal;
            dtPC.Rows[0]["Remark"] = _ltRemark.EditVal.ToString();
            int _typeID = Convert.ToInt32(dtPC.Rows[0]["TypeID"]);
            if (dtPC.Rows[0]["TypeID"].Equals(0))
            {
                XtraMessageBox.Show("请选择费用类型");
                return;
            }

                decimal mm = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    mm += decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString());
                }
                if (mm == 0)
                {
                    XtraMessageBox.Show("必须至少有一个不为0的金额");
                    return;
                }
            if (_mainID == 0)
            {

                dtPC.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(blPC, "NewNum", new object[] { (DateTime)(_ldDate.val) }).ToString();
                dtPC.Rows[0]["ID"] = _mainID = BasicClass.GetDataSet.Add(blPC, dtPC);
            }
            else
            {
                BasicClass.GetDataSet.UpData(blPC, dtPC);
            }
            dtInfo = (DataTable)(gridControl1.DataSource);
            DataTable dtTem = dtInfo.Clone();
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                int a = int.Parse(dtInfo.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    if (Convert.ToInt32(dtInfo.Rows[i]["EmployeeID"]) > 0 && Convert.ToDecimal(dtInfo.Rows[i]["Money"]) != 0)
                    {
                        dtTem.Rows.Clear();
                        dtInfo.Rows[i]["MainID"] = _mainID;
                        dtInfo.Rows[i]["IsDeposit"] = _typeID==109;
                        dtTem.Rows.Add(dtInfo.Rows[i].ItemArray);
                        if (a == 3)
                            dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blPCI, dtTem);
                        else if (a == 2)
                            BasicClass.GetDataSet.UpData(blPCI, dtTem);
                        dtInfo.Rows[i]["A"] = 1;
                    }
                    else
                    {
                        if (Convert.ToInt32(dtInfo.Rows[i]["ID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(blPCI, "Delete", new object[] { Convert.ToInt32(dtInfo.Rows[i]["ID"]) });
                        }
                    }
                }
            }
        }
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
                return;
            if (e.Column != _coA &&Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA))==1)
            {
                   gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtInfo.NewRow();
                dr["A"] = 3;
                dr["ID"] = dr["MainID"] = dr["EmployeeID"] = dr["Money"] = dr["IsDeposit"] = 0;
                dr["Remark"] = string.Empty;
                dtInfo.Rows.Add(dr.ItemArray);
            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                int r = gridView1.RowCount - 1;
                object obj = gridView1.GetRowCellValue(r, _coEmployeeID);
                if (obj.ToString() == "")
                {
                    XtraMessageBox.Show("员工姓名不能为空！");
                    gridView1.DeleteRow(r);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
            dtPC.Rows[0]["IsVerify"] = 3;
            dtPC.Rows[0]["VerifyDate"] = DateTime.Today;
            dtPC.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            BasicClass.GetDataSet.UpData(blPC,  dtPC);
            int d = bs.Position;
            InData();
            bs.Position = d;
            if (d == dtMain.Rows.Count - 1)
                ShowInfo(d);
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtPM.Rows.Count==0|| Convert.ToDateTime(dtPC.Rows[0]["DateTime"]) > Convert.ToDateTime(dtPM.Rows[0]["EndDate"]))
            {
                dtPC.Rows[0]["IsVerify"] = 1;
                dtPC.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
                dtPC.Rows[0]["VerifyMan"] = 0;
                BasicClass.GetDataSet.UpData(blPC, dtPC);
                ShowInfo(bs.Position);
            }
            else
            {
                XtraMessageBox.Show("已汇总，不能弃审！");
            }
        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除整份单据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(blPCI, "DeleteByMainID", new object[] { _mainID });
                BasicClass.GetDataSet.ExecSql(blPC, "Delete", new object[] { _mainID });
               InData();
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowInfo(0);
            }
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除该明细？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    int infoID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (infoID > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blPCI, "Delete", new object[] { infoID });
                    }
                    gridView1.DeleteSelectedRows();
                }
            }
        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtM = new DataTable();
            dtM.Columns.Add("Num", typeof(string));
            dtM.Columns.Add("Remark", typeof(string));
            dtM.Columns.Add("Date", typeof(string));
            dtM.Columns.Add("Type", typeof(string));
            dtM.Rows.Add(_ltNum.val, _ltRemark.val, ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日"), _leType.valStr);
            dtM.TableName = "Main";
            DataTable dtI = new DataTable();
            dtI.Columns.Add("Name", typeof(string));
            dtI.Columns.Add("Money", typeof(decimal));
            dtI.Columns.Add("Remark", typeof(string));
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                dtI.Rows.Add(gridView1.GetRowCellDisplayText(i, _coEmployeeID), gridView1.GetRowCellValue(i, _coMoney), gridView1.GetRowCellValue(i, _coRemark));
            }
            dtI.TableName = "Info";
            DataSet ds = new DataSet();
            ds.DataSetName = "dds";
            ds.Tables.Add(dtM);
            ds.Tables.Add(dtI);
            BaseForm.PrintClass.PrintPayCosts(ds);
        }
    }
}