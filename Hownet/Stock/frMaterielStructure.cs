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
    public partial class frMaterielStructure : DevExpress.XtraEditors.XtraForm
    {
        public frMaterielStructure()
        {
            InitializeComponent();
        }

        DataTable dtPP = new DataTable();
        DataTable dtDemand = new DataTable();
        DataTable dtUseRep = new DataTable();
        int _TableTypeID = (int)BasicClass.Enums.TableType.ProductionPlan;
        int _NumTypeID = BasicClass.BasicFile.liST[0].NumType;
        int _DepotID=0;
        string strWhere = string.Empty;
        string _strTask = string.Empty;
        string PP = string.Empty;
      public static  DataTable dtRLUse = new DataTable();
        private void frMaterielStructure_Load(object sender, EventArgs e)
        {
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coMateriel.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coPMateriel.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coCompanyID.ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
            labAndLookupEdit1.FormName = (int)BasicClass.Enums.TableType.Deparment;
            labAndLookupEdit1.editVal = BasicClass.BasicFile.liST[0].DefaultRawDepot;
            labAndLookupEdit1.IsNotCanEdit = false;
            //dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetTaskList", new object[] { _TableTypeID, _NumTypeID, true }).Tables[0];
            //gridControl1.DataSource = dtPP;
            dtUseRep = BasicClass.GetDataSet.GetDS("Hownet.BLL.PlanUseRep", "GetList", new object[] { "ID=0" }).Tables[0];
            DataTable dtIsVerify = new DataTable();
            dtIsVerify.Columns.Add("Name", typeof(string));
            dtIsVerify.Columns.Add("ID", typeof(int));
            dtIsVerify.Rows.Add("未审核", 1);
            dtIsVerify.Rows.Add("审核中", 2);
            dtIsVerify.Rows.Add("已审核", 3);
            dtIsVerify.Rows.Add("开始生产", 4);
            dtIsVerify.Rows.Add("待确认", 5);
            dtIsVerify.Rows.Add("确认通过", 6);
            dtIsVerify.Rows.Add("合并生产", 7);
            dtIsVerify.Rows.Add("已完成", 9);
            dtIsVerify.Rows.Add("开始备料", 10);
            dtIsVerify.Rows.Add("客户取消", 21);
            dtIsVerify.Rows.Add("公司取消", 22);
            _reIsVerify.DataSource = dtIsVerify;
            DataTable dtTypeID = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetNumList", new object[] { "(ProductionPlan.TypeID=-1)" }).Tables[0];
            dtTypeID.Rows.Add("合并后的计划", -1);
            repositoryItemLookUpEdit1.DataSource = dtTypeID;
            dtRLUse.Columns.Add("RID", typeof(int));
            dtRLUse.Columns.Add("PID", typeof(int));
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coIsSelect)
            {
                gridView1.SetFocusedRowCellValue(_coIsSelect, e.Value);
                gridView1.UpdateCurrentRow();
                gridView1.CloseEditor();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(labAndLookupEdit1.editVal) < 1)
            {
                XtraMessageBox.Show("请先选择仓库");
                return;
            }
            ShowInfo();
        }
        private void ShowInfo()
        {
            dtUseRep.Rows.Clear();
            strWhere = "(MaterielDemand.IsEnd=0) And (MaterielDemand.TableTypeID=" + (int)BasicClass.Enums.TableType.ProductionPlan + ") And ( ProduceTaskID IN(";
            _strTask = string.Empty;
            int _count = 0;
            PP = string.Empty;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsSelect)))
                {
                    _strTask += gridView1.GetRowCellValue(i, _coPID) + ",";
                    _count += 1;
                    PP += gridView1.GetRowCellDisplayText(i, _coNum) + "/" + gridView1.GetRowCellDisplayText(i, _coPMateriel)+"；";
                }
            }
            if (_count == 0)
            {
                gridControl2.DataSource = null;
                return;
            }
            if (_strTask.Length > 0)
            {
                _strTask = _strTask.Remove(_strTask.Length - 1);
            }
            else
            {
                gridControl2.DataSource = null;
                return;
            }
            dtDemand = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "GetNeedStock", new object[] { strWhere, Convert.ToInt32(labAndLookupEdit1.editVal),_strTask }).Tables[0];
             gridControl2.DataSource = dtDemand;
            decimal _amount = 0;
            decimal _repAmount = 0;
            decimal _useRepAmount = 0;
            decimal _needNotAmount= 0;
            decimal _stockAmount = 0;
            decimal _hasStock = 0;
            decimal _outAmount = 0;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                _amount = 0;
                _repAmount = 0;
                _useRepAmount = 0;
                _needNotAmount = 0;
                _stockAmount = 0;
                _hasStock = 0;
                if (gridView2.GetRowCellValue(i, _coRepertoryAmount) != DBNull.Value)
                    _repAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coRepertoryAmount));
                if (gridView2.GetRowCellValue(i, _coUseRepertAmount) != DBNull.Value)
                    _useRepAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coUseRepertAmount));
                if (gridView2.GetRowCellValue(i, _coNeedAmount) != DBNull.Value)
                    _needNotAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coNeedAmount));
                if (gridView2.GetRowCellValue(i, _coHasStockAmount) != DBNull.Value)
                    _hasStock = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coHasStockAmount));
                _stockAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _costockAmount));
                _outAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coOutAmount));
                _amount = _stockAmount - _repAmount - _needNotAmount - _hasStock - _useRepAmount - _outAmount;
                gridView2.SetRowCellValue(i, _coNAmount, _amount);
            }
        }
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0||gridView2.FocusedColumn==null)
                return;
            if (gridView2.FocusedColumn == _coUseRepertAmount)
            {
                BasicClass.cResult r=new BasicClass.cResult();
                r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                r.RowChanged += new BasicClass.RowChangedHandler(r_RowChanged);
                Form fr = new frShowNeedInfo(r, (int)BasicClass.Enums.PlanUseRep.使用原仓存, Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coMListID)), strWhere, gridView2.GetFocusedRowCellDisplayText(_coSpecif), dtUseRep,_NumTypeID,_strTask,_DepotID);
                fr.ShowDialog();
            }
        }

        void r_RowChanged(DataTable dt)
        {
            dtUseRep = dt;
        }

        void r_TextChanged(string s)
        {
            gridView2.SetFocusedRowCellValue(_coUseRepertAmount, Convert.ToDecimal(s));
            decimal _amount = 0;
            decimal _repAmount = 0;
            decimal _useRepAmount = 0;
            decimal _useStockLass = 0;
            decimal _stockAmount = 0;
            decimal _hasStock = 0;
            decimal _outAmount = 0;
            int i = gridView2.FocusedRowHandle;
            if (gridView2.GetRowCellValue(i, _coRepertoryAmount) != DBNull.Value)
                _repAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coRepertoryAmount));
            if (gridView2.GetRowCellValue(i, _coUseRepertAmount) != DBNull.Value)
                _useRepAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coUseRepertAmount));
            if (gridView2.GetRowCellValue(i, _coNeedAmount) != DBNull.Value)
                _useStockLass = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coNeedAmount));
            if (gridView2.GetRowCellValue(i, _coHasStockAmount) != DBNull.Value)
                _hasStock = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coHasStockAmount));
            _stockAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _costockAmount));
            _outAmount = Convert.ToDecimal(gridView2.GetRowCellValue(i, _coOutAmount));
            _amount = _stockAmount - _repAmount - _useStockLass - _hasStock - _useRepAmount - _outAmount;
            gridView2.SetRowCellValue(i, _coNAmount, _amount);
        }
        private void labAndLookupEdit1_EditValueChanged(object val, string text)
        {
            _DepotID = Convert.ToInt32(val);
            if (_DepotID != 0 && _DepotID != BasicClass.BasicFile.liST[0].DefaultRawDepot)
            {
                if (DialogResult.No == XtraMessageBox.Show("所选择仓库不是默认原料仓库，计算数量可能出现误差，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    labAndLookupEdit1.editVal = _DepotID = 0;
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(_DepotID==0)
            {
                XtraMessageBox.Show("请选择物料存放仓库！");
                return;
            }
            if (DialogResult.Yes == XtraMessageBox.Show("请确认是否将所有已选择的物料的全部申购？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                decimal Amount = 0;
                bool t = false;
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    if (Convert.ToBoolean(gridView2.GetRowCellValue(i,_coIsS))&& Convert.ToDecimal(gridView2.GetRowCellValue(i, _coNAmount)) > 0)
                    {
                        t = true;
                        break;
                    }
                }
                //if (!t)
                //{
                //    XtraMessageBox.Show("没有需采购的数量！");
                //    return;
                //}
                int _sbID = 0;
                int _sbiID = 0;
                if (t)
                {
                    int _tabTypeID = (int)BasicClass.Enums.TableType.NeedStock;
                    DataTable dtSB = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBack, "GetList", new object[] { "ID=0" }).Tables[0];
                    DataRow dr = dtSB.NewRow();//请购单主表
                    dr["ID"] = 0;
                    dr["DataTime"] = BasicClass.GetDataSet.GetDateTime();
                    dr["Num"] = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllStockBack, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, _tabTypeID, 0 });
                    dr["CompanyID"] = 0;
                    dr["Remark"] = string.Empty;
                    dr["StockRemark"] = PP;
                    dr["Money"] = 0;
                    dr["IsVerify"] = 4;
                    dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                    dr["DepotID"] = labAndLookupEdit1.editVal;
                    dr["LastMoney"] = dr["BackMoney"] = 0;
                    dr["FillDate"] = BasicClass.GetDataSet.GetDateTime();
                    dr["VerifyMan"] = dr["FillMan"] = BasicClass.UserInfo.UserID;
                    dr["State"] = _tabTypeID;
                    dr["BackDate"] = DateTime.Parse("1900-1-1");
                    dr["TaskID"] = 0;
                    dr["A"] = 1;
                    dtSB.Rows.Add(dr);
                     _sbID = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllStockBack, dtSB);
                }
                 Amount=0;
                int RelatedID,DemandID,TypeID,MListID,TaskID,StockInfoID;
                RelatedID=DemandID=TypeID=MListID=TaskID=StockInfoID=0;

                DataRow[] drs;
                DataTable dtSBI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBackInfo, "GetList", new object[] {"ID=0" }).Tables[0];
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    _sbiID = 0;
                    if (Convert.ToBoolean(gridView2.GetRowCellValue(i,_coIsS))&& gridView2.GetRowCellValue(i, _coNAmount) != DBNull.Value && Convert.ToDecimal(gridView2.GetRowCellValue(i, _coNAmount)) > 0)
                    {
                        dtSBI.Rows.Clear();
                        DataRow drr = dtSBI.NewRow();
                        drr["ID"] = 0;
                        drr["MainID"] = _sbID;
                        drr["StockInfoID"] = 0;
                        drr["MaterielID"] = gridView2.GetRowCellValue(i, _coMateriel);
                        drr["ColorID"] = gridView2.GetRowCellValue(i, _coColorID);
                        drr["ColorOneID"] = gridView2.GetRowCellValue(i, _coColorOneID);
                        drr["ColorTwoID"] = gridView2.GetRowCellValue(i, _coColorTwoID);
                        drr["SizeID"] = gridView2.GetRowCellValue(i, _coSizeID);
                        drr["Price"] = drr["Money"] = 0;
                        drr["CompanyMeasureID"] = drr["DepotMeasureID"] = gridView2.GetRowCellValue(i, _coMeasureID);
                        drr["Conversion"] = 1;
                        drr["Remark"] = string.Empty;
                        drr["Amount"] = drr["PriceAmount"] = drr["NotAmount"] = drr["NotPriceAmount"] = drr["NeedAmount"] = gridView2.GetRowCellValue(i, _coNAmount);
                        drr["MListID"] = gridView2.GetRowCellValue(i, _coMListID);
                        drr["CompanyID"] = 0;
                        drr["BrandID"] = 0;
                        drr["IsEnd"] = 0;
                        drr["DemandID"] = 0;
                        drr["ExcessAmount"] = 0;
                        drr["A"] = 1;
                        drr["StringTaskID"] = _strTask;
                        dtSBI.Rows.Add(drr);
                        _sbiID = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllStockBackInfo, dtSBI);
                    }
                    drs = dtUseRep.Select("(A=1) And (MListID=" + gridView2.GetRowCellValue(i, _coMListID) + ")");
                    if (drs.Length > 0)
                    {
                        for (int j = 0; j < drs.Length; j++)
                        {
                            drs[j]["StockInfoID"] = _sbiID;
                            MListID = Convert.ToInt32(drs[j]["MListID"]);
                            DemandID = Convert.ToInt32(drs[j]["DemandID"]);
                            Amount = Convert.ToDecimal(drs[j]["Amount"]);
                            StockInfoID = _sbID;
                            TypeID = (int)BasicClass.Enums.PlanUseRep.使用原仓存;
                            TaskID = Convert.ToInt32(drs[j]["TaskID"]);
                            RelatedID = Convert.ToInt32(drs[j]["RelatedID"]);
                            //将使用空闲仓存数量转入相应生产计划的库存备料中，并增加到相应物料拆分表的已备料数量中
                            BasicClass.GetDataSet.ExecSql("Hownet.BLL.PlanUseRep", "UpAmount", new object[] { MListID, DemandID, StockInfoID, Amount, TypeID, TaskID, RelatedID, true, _DepotID });
                            //BasicClass.GetDataSet.ExecSql("Hownet.BLL.PlanUseRep", "UpAmount", new object[] { MListID, DemandID, StockInfoID, Amount, (int)BasicClass.Enums.PlanUseRep.已申购数量, TaskID, RelatedID });
                        }
                    }
                    dtUseRep.AcceptChanges();
                }
                //if (dtUseRep.Rows.Count > 0)//更新库存情况，将空闲物料转入计划单
                //{
                //    DataSet dss = new DataSet();
                //    dss.DataSetName = "dss";
                //    dss.Tables.Add(dtUseRep.Copy());
                //    byte[] bb = BasicClass.ZipJpg.Ds2Byte(dss);
                //    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllRepertory, "UpTaskUseRepertoryAmount", new object[] { bb, _DepotID });
                //}
                //增加明细的申购数量
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllStockBackInfo, "StockToDemand", new object[] { _sbID,(int)BasicClass.Enums.PlanUseRep.已申购数量,true });
                string[] ss = _strTask.Split(',');
                if (ss.Length > 0)
                {
                    DataTable dttt = new DataTable();
                    string bllPP = "Hownet.BLL.ProductionPlan";
                    for (int i = 0; i < ss.Length; i++)
                    {
                        dttt = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=" + Convert.ToInt32(ss[i]) + ")" }).Tables[0];
                        if (dttt.Rows.Count > 0)
                        {
                            dttt.Rows[0]["PWorkingID"] = (int)BasicClass.Enums.IsVerify.开始备料;
                            BasicClass.GetDataSet.UpData(bllPP, dttt);
                        }
                    }
                }
                for (int i = 0; i < dtRLUse.Rows.Count; i++)
                {
                    BasicClass.GetDataSet.ExecSql("Hownet.BLL.RepertoryList", "UpPlanID", new object[] { Convert.ToInt32(dtRLUse.Rows[i]["RID"]), Convert.ToInt32(dtRLUse.Rows[i]["PID"]) });
                }
                ShowInfo();
                dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetTaskList", new object[] { _TableTypeID, _NumTypeID, true }).Tables[0];
                gridControl1.DataSource = dtPP;
                if (_sbID > 0)
                {
                    Form fr = new frNeedStock(_sbID);
                    fr.ShowDialog();
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
                return;
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add("PPID", typeof(string));
            dtMain.Rows.Add(PP);
            dtMain.TableName = "Main";
            DataTable dtInfo = new DataTable();
            for (int i = 0; i < gridView2.VisibleColumns.Count; i++)
            {
                dtInfo.Columns.Add(gridView2.VisibleColumns[i].FieldName, typeof(string));
            }
            dtInfo.Rows.Add(dtInfo.NewRow());
            for (int i = 0; i < gridView2.VisibleColumns.Count; i++)
            {
              dtInfo.Rows[0][i] = gridView2.VisibleColumns[i].Caption;
               
            }
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow dr = dtInfo.NewRow();
                for (int j = 0; j < gridView2.VisibleColumns.Count; j++)
                {
                    dr[j] = gridView2.GetRowCellDisplayText(i, gridView2.VisibleColumns[j]);
                }
                dtInfo.Rows.Add(dr);
            }
            dtInfo.TableName = "Info";
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            ds.Tables.Add(dtMain);
            ds.Tables.Add(dtInfo);
            BaseForm.PrintClass.MaterielStructure(ds);
        }

        private void gridView2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView2.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
                SelectMateriel(Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coMateriel)), true);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
                SelectMateriel(Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coMateriel)), false);
        }
        private void SelectMateriel(int MaterielID, bool IsSelect)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                if (MaterielID > 0)
                {
                    if (Convert.ToInt32(gridView2.GetRowCellValue(i, _coMateriel)) == MaterielID)
                    {
                        gridView2.SetRowCellValue(i, _coIsS, IsSelect);
                    }
                }
                else
                {
                    gridView2.SetRowCellValue(i, _coIsS, IsSelect);
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            SelectMateriel(0, checkEdit1.Checked);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetTaskList", new object[] { _TableTypeID, _NumTypeID, checkEdit2.Checked }).Tables[0];
            gridControl1.DataSource = dtPP;
        }
    }
}