using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Stock
{
    public partial class frShowNeedInfo : DevExpress.XtraEditors.XtraForm
    {
        public frShowNeedInfo()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        /// <summary>
        /// 1为生产未领，2为采购未收货
        /// </summary>
        int _TypeID = 0;
        int _MListID = 0;
        int _NumTypeID = 0;
        int _depotID = 0;
        int RepertoryID = 0;
        string _StrWhere = string.Empty;
        string _Specif = string.Empty;
        string _strTaskID = string.Empty;
        string bllPR="Hownet.BLL.PlanUseRep";
        decimal _RepAmount = 0;
        decimal RepAmount = 0;
        decimal RepNotAmount = 0;
        DataTable dt = new DataTable();
        DataTable dtRep = new DataTable();
        DataTable dtUseRep = new DataTable();
        DataTable dtRList = new DataTable();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="TypeID">1为使用库存余量，2为使用采购余量</param>
        /// <param name="MListID"></param>
        public frShowNeedInfo(BasicClass.cResult cr, int TypeID, int MListID, string strWhere, string Specif, DataTable dtU, int NumTypeID,string strTaskID,int DepotID)
            : this()
        {
            r = cr;
            _TypeID = TypeID;
            _MListID = MListID;
            _StrWhere = strWhere;
            _Specif = Specif;
            dtUseRep = dtU;
            _NumTypeID = NumTypeID;
            _strTaskID = strTaskID;
            _depotID = DepotID;

        }
        private void frShowInfo_Load(object sender, EventArgs e)
        {

            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _barCancelStock.Enabled = (_TypeID == 2);
            _barIsEnd.Enabled = (_TypeID == 1);
            
            dtRep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(PlanID=0) And (MListID=" + _MListID + ") And (DepartmentID=" + _depotID + ")" }).Tables[0];
            if (dtRep.Rows.Count > 0)
            {
                RepAmount = Convert.ToDecimal(dtRep.Rows[0]["Amount"]);
                RepertoryID = Convert.ToInt32(dtRep.Rows[0]["ID"]);
            }
            InData();
            label1.Text = "仓库现有余量：" + RepAmount.ToString();
            dtRList = BasicClass.GetDataSet.GetDS("Hownet.BLL.RepertoryList", "GetList", new object[] { " (BatchNumber=" + _MListID + ")" }).Tables[0];
                gridControl2.DataSource = dtRList;
        }
        private void InData()
        {
            if (_TypeID == (int)BasicClass.Enums.PlanUseRep.使用原仓存)
            {
                //查找这些生产计划中该物料的备料情况
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetNeedAmountByMListID", new object[] { _MListID, _StrWhere, _NumTypeID, _strTaskID }).Tables[0];
                dt.Columns.Add("Specif", typeof(string));//规格
                dt.Columns.Add("NowUseRep", typeof(decimal));//本次使用库存
                dt.Columns.Add("NotAmount", typeof(decimal));
                dt.Columns.Add("TemUse", typeof(decimal));
                dt.Columns.Add("NowNeedAmount", typeof(decimal));//需申购数量
                decimal _amount = 0;
                dtUseRep.DefaultView.RowFilter = "(MListID=" + _MListID + ") And (TypeID=" + _TypeID + ") And (RelatedID=" + RepertoryID + ") And (A=1)";
                if (dtUseRep.DefaultView.Count > 0)
                {
                    _amount = 0;
                    for (int j = 0; j < dtUseRep.DefaultView.Count; j++)
                    {
                        _amount += Convert.ToDecimal(dtUseRep.DefaultView[j]["Amount"]);
                    }
                    RepNotAmount = RepAmount - _amount;
                }
                gridView1.UpdateCurrentRow();
                gridView1.CloseEditor();
                dt.AcceptChanges();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Specif"] = _Specif;
                    dtUseRep.DefaultView.RowFilter = "(MListID=" + _MListID + ") And (TypeID=" + _TypeID + ") And (DemandID=" + dt.Rows[i]["ID"] + ") And (A=1)";
                    if (dtUseRep.DefaultView.Count > 0)
                    {
                        dt.Rows[i]["NowUseRep"] = dt.Rows[i]["TemUse"] = dtUseRep.DefaultView[0]["Amount"];
                    }
                    dt.Rows[i]["NotAmount"] = Convert.ToDecimal(dt.Rows[i]["stockAmount"]) - Convert.ToDecimal(dt.Rows[i]["NeedAmount"]) - Convert.ToDecimal(dt.Rows[i]["HasStockAmount"]) - Convert.ToDecimal(dt.Rows[i]["OutAmount"]) - Convert.ToDecimal(dt.Rows[i]["RepertoryAmount"]);
                }
            }
            gridControl1.DataSource = dt;
        }
        private void _barCancelStock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("取消当前采购数量？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的取消当前采购数量？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBackInfo, "GetList", new object[] { "(ID=" + Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) + ")" }).Tables[0];
                    dttt.Rows[0]["IsEnd"] = 1;
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllStockBackInfo, dttt);
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    r.ChangeText("OK");
                }
            }
        }

        private void _barIsEnd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("取消当前将生产未领数量？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的取消当前将生产未领数量？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetList", new object[] { "(ID=" + Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) + ")" }).Tables[0];
                    dttt.Rows[0]["IsEnd"] = 1;
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllMaterielDemand, dttt);
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    r.ChangeText("OK");
                }
            }
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            //当修改本次占用数量后，如果占用总数超过库存总数，提示错误，并将当前行改为库存剩余量
            //如果未超过库存总数，则从第一条库存记录起，从库存剩余量中扣除本次数量
            if (e.Column == _coNowUseRep)
            {
                decimal _amount = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellValue(i, _coNowUseRep) != DBNull.Value)
                    {
                        _amount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coNowUseRep));
                    }
                }
                if (_amount > RepAmount)
                {
                    XtraMessageBox.Show("使用数量超过库存余量！");
                    SendKeys.Send("{Esc}");
                }
                else
                {
                    gridView1.SetFocusedRowCellValue(_coTemUse, e.Value);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //DataRow[] drs = dtUseRep.Select("(MListID=" + _MListID + ")");
            //for (int i = 0; i < drs.Length; i++)
            //{
            //    drs[i]["A"] = 4;
            //}
            //dtUseRep.AcceptChanges();
            //decimal _amount = 0;
            //decimal _lessAmount = 0;
            //decimal _hasUse = 0;
            //for (int i = 0; i < gridView2.RowCount; i++)
            //{
            //    _amount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coRepAmount));
            //    _hasUse = 0;
            //    for (int j = 0; j < gridView1.RowCount; j++)
            //    {
            //        _lessAmount = Convert.ToDecimal(gridView1.GetRowCellValue(j, _coNotAmount));
            //        if (gridView1.GetRowCellValue(j, _coNowUseRep) != DBNull.Value)
            //        {
            //            _lessAmount -= Convert.ToDecimal(gridView1.GetRowCellValue(j, _coNowUseRep));
            //        }
            //        if (_lessAmount > 0)
            //        {
            //            if (_lessAmount < _amount)
            //            {
            //                gridView1.SetRowCellValue(j, _coNowUseRep, _lessAmount);
            //                gridView1.SetRowCellValue(j, _coTemUse, _lessAmount);
            //                _amount -= _lessAmount;
            //                _hasUse += _lessAmount;
            //                DataRow dr = dtUseRep.NewRow();
            //                dr["ID"] = 0;
            //                dr["RelatedID"] = gridView2.GetRowCellValue(i, _coRepID);
            //                dr["NotAmount"] = dr["Amount"] = _lessAmount;
            //                dr["DemandID"] = gridView1.GetRowCellValue(j, _coID);
            //                dr["TypeID"] = _TypeID;
            //                dr["MListID"] = _MListID;
            //                dr["TaskID"] = gridView1.GetRowCellValue(j, _coProduceTaskID);
            //                dr["IsEnd"] = 0;
            //                dr["A"] = 1;
                            
            //                dtUseRep.Rows.Add(dr);
            //            }
            //            else
            //            {
            //                gridView1.SetRowCellValue(j, _coNowUseRep, _amount);
            //                gridView1.SetRowCellValue(j, _coTemUse, _amount);
            //                _hasUse += _amount;
            //                DataRow dr = dtUseRep.NewRow();
            //                dr["ID"] = 0;
            //                dr["RelatedID"] = gridView2.GetRowCellValue(i, _coRepID);
            //                dr["NotAmount"] = dr["Amount"] = _amount;
            //                dr["DemandID"] = gridView1.GetRowCellValue(j, _coID);
            //                dr["TypeID"] = _TypeID;
            //                dr["MListID"] = _MListID;
            //                dr["TaskID"] = gridView1.GetRowCellValue(j, _coProduceTaskID);
            //                dr["IsEnd"] = 0;
            //                dr["A"] = 1;
            //                dtUseRep.Rows.Add(dr);
            //                break;
            //            }
            //        }
            //    }
            //    gridView2.SetRowCellValue(i, _coHasUse, _hasUse);
            //}
            //simpleButton1.Enabled = false;
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            decimal _amount = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, _coNowUseRep) != DBNull.Value)
                {
                    _amount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coNowUseRep));
                }
            }
            if (_amount > RepAmount)
            {
                XtraMessageBox.Show("使用数量超过库存余量！");
                return;
            }
            DataRow[] drs = dtUseRep.Select("(MListID=" + _MListID + ") And (TypeID=" + _TypeID + ")");
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i].Delete();
                }
                dtUseRep.AcceptChanges();
            }
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, _coNowUseRep) != null && gridView1.GetRowCellValue(i, _coNowUseRep) != DBNull.Value && Convert.ToDecimal(gridView1.GetRowCellValue(i, _coNowUseRep)) > 0)
                {
                    DataRow dr = dtUseRep.NewRow();
                    dr["ID"] = 0;
                    dr["RelatedID"] = RepertoryID;
                    dr["NotAmount"] = dr["Amount"] = gridView1.GetRowCellValue(i, _coNowUseRep);
                    dr["DemandID"] = gridView1.GetRowCellValue(i, _coID);
                    dr["TypeID"] = _TypeID;
                    dr["MListID"] = _MListID;
                    dr["TaskID"] = gridView1.GetRowCellValue(i, _coProduceTaskID);
                    dr["IsEnd"] = 0;
                    dr["A"] = 1;
                    dtUseRep.Rows.Add(dr);
                }
            }
           // decimal _amount = 0;
            r.RowChang(dtUseRep);
            r.ChangeText(_amount.ToString());
            this.Close();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (gridView1.FocusedRowHandle < 0 || gridView1.FocusedColumn != _coNowUseRep)
                {
                    return;
                }
                int siID = 0;
                try
                {
                    siID = Convert.ToInt32(textEdit1.Text.Trim());
                    if(frMaterielStructure.dtRLUse.Select("(RID="+siID+")").Length>0)
                    {
                        XtraMessageBox.Show("已分配");
                        return;
                    }
                    DataRow[] drs;
                    if (siID > 0)
                        drs = dtRList.Select("(StockListID=" + siID + ")");
                    else
                        drs = dtRList.Select("(ID=" + siID * -1 + ")");
                    if (drs.Length > 0)
                    {
                        object o = gridView1.GetFocusedValue();
                        if (o == DBNull.Value)
                        {
                            gridView1.SetFocusedValue(drs[0]["NotAmount"]);
                        }
                        else
                        {
                            gridView1.SetFocusedValue(Convert.ToDecimal(drs[0]["NotAmount"]) + Convert.ToDecimal(o));
                        }
                        frMaterielStructure.dtRLUse.Rows.Add(Convert.ToInt32(drs[0]["ID"]), Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coProduceTaskID)));
                    }
                    else
                    {
                        XtraMessageBox.Show("条码号错误或所代表物料不一致");
                        return;
                    }
                }
               catch
                {
                    XtraMessageBox.Show("条码号错误");
                    return;
               }
            }
        }
    }
}