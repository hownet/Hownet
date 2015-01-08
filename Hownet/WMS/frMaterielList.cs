using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.WMS
{
    public partial class frMaterielList : DevExpress.XtraEditors.XtraForm
    {
        public frMaterielList()
        {
            InitializeComponent();
        }
        string bllRL = "Hownet.BLL.RepertoryList";
        DataTable dtRL = new DataTable();
        DataTable dt = new DataTable();
        decimal Amount = 0;
        string bllDep = "Hownet.BLL.Deparment";
        int _MainID = 0;
        bool _IsUseQRID = false;
        private void frProductDepotList_Load(object sender, EventArgs e)
        {
            string per = BasicClass.BasicFile.GetPermissions(this.Text);

            DataTable dtTem = BasicClass.GetDataSet.GetBySql("Select Value  From OtherType Where (Name='物料使用条码出入仓')");
            if(dtTem.Rows.Count>0)
            {
                _IsUseQRID = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
            }
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _coChengBengJ.Visible = _coChengBengJMoney.Visible = _coYiJiDaiLiJia.Visible = _coYiJiDaiLiJiaMoney.Visible = false;
            DataTable dtM = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] {"(AttributeID<>5)" }).Tables[0];
            DataRow drM = dtM.NewRow();
            drM["A"] = drM["ID"] = drM["ParentID"] = drM["IsEnd"] = drM["IsUse"] = drM["AttributeID"] = 0;
            drM["Name"] = drM["Sn"] = drM["Remark"] = "";
            dtM.Rows.Add(drM);
            dtM.DefaultView.Sort = "ID";
            _leMM.Properties.DataSource = dtM.DefaultView;

            DataTable dtD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[]{"仓库"}).Tables[0];
            DataRow drD = dtD.NewRow();
            drD["ID"] = drD["A"] = 0;
            drD["Name"] = ""; //= drD["Remark"]= drD["Sn"] 
            dtD.Rows.Add(drD);
            dtD.DefaultView.Sort = "ID";
            _leDepot.Properties.DataSource = dtD.DefaultView;
            InData();
            _leDepot.EditValue = _leMM.EditValue = 0;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
           _coColorOneID.ColumnEdit=_coColorTwoID.ColumnEdit= _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coRPlanID.ColumnEdit = _coPlanID.ColumnEdit = BaseForm.RepositoryItem._rePlanNum;
            _coTypeID.ColumnEdit = BaseForm.RepositoryItem._reMTID(0);
            _coAttributeID.ColumnEdit = BaseForm.RepositoryItem._reAttribute;
             _coDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            _coSpecID.ColumnEdit = BaseForm.RepositoryItem._reSpec;
            gridControl2.Visible = _IsUseQRID;
            lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtAllMateriel;
            lookUpEdit1.EditValue = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            InData();
            lookUpEdit1.EditValue = 0;
        }
        private void InData()
        {
            int _depot = 0;
            int _mm = 0;
            int _materielID = 0;
            if (_leDepot.EditValue != null && _leDepot.EditValue.ToString().Trim() != string.Empty)
                _depot = int.Parse(_leDepot.EditValue.ToString());
            if (_leMM.EditValue != null && _leMM.EditValue.ToString().Trim() != string.Empty)
                _mm = int.Parse(_leMM.EditValue.ToString());
            _materielID = Convert.ToInt32(lookUpEdit1.EditValue);
             dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetMaterielList", new object[] { _depot, _mm ,_materielID}).Tables[0];
           gridControl1.DataSource = dt;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && int.Parse(gridView1.GetFocusedRowCellValue(_coA).ToString()) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);

        }

        private void _sbSave_Click(object sender, EventArgs e)
        {
            DataTable dtTem = new DataTable();
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            object o;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (int.Parse(gridView1.GetRowCellValue(i, _coA).ToString()) == 2)
                {
                    dtTem.Clear();
                    dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(ID=" + int.Parse(gridView1.GetRowCellValue(i, _coID).ToString()) + ")" }).Tables[0];
                    if (dtTem.Rows.Count > 0 && dtTem.Rows[0]["ID"].ToString().Trim() != string.Empty)
                    {
                        //o = gridView1.GetRowCellValue(i, "SellPrice");
                        //if (o != null && o.ToString().Trim() != string.Empty)
                        //    dt.Rows[0]["SellPrice"] = o;
                        //else
                        //    dt.Rows[0]["SellPrice"] = 0;
                        //o = gridView1.GetRowCellValue(i, "MinAmount");
                        //if (o != null && o.ToString().Trim() != string.Empty)
                        //    dt.Rows[0]["MinAmount"] = o;
                        //else
                        //    dt.Rows[0]["MinAmount"] = 0;
                        //o = gridView1.GetRowCellValue(i, "MaxAmount");
                        //if (o != null && o.ToString().Trim() != string.Empty)
                        //    dt.Rows[0]["MaxAmount"] = o;
                        //else
                        //    dt.Rows[0]["MaxAmount"] = 0;
                        //o = gridView1.GetRowCellValue(i, "StockPrice");
                        //if (o != null && o.ToString().Trim() != string.Empty)
                        //    dt.Rows[0]["StockPrice"] = o;
                        //else
                        //    dt.Rows[0]["StockPrice"] = 0;
                        dtTem.Rows[0]["Remark"] = gridView1.GetRowCellDisplayText(i, _coRemark);
                    }
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtTem);
                    gridView1.SetRowCellValue(i, _coA, 1);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //Form fr = new frMinList();
            //fr.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Form fr = new frMaxList();
            //fr.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string fileName =BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                int _id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                int _planID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coPlanID));
                if (_planID > 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否真的转入空闲库存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(ID=" + _id + ")" }).Tables[0];
                        DataTable dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(MListID=" + dtTem.Rows[0]["MListID"] + ") And (DepartmentID=" + dtTem.Rows[0]["DepartmentID"] + ") And (PlanID=0)" }).Tables[0];
                        if (dtt.Rows.Count > 0)
                        {
                            dtt.Rows[0]["Amount"] = Convert.ToDecimal(dtt.Rows[0]["Amount"]) + Convert.ToDecimal(dtTem.Rows[0]["Amount"]);
                            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtt);
                            dtTem.Rows[0]["Amount"] = 0;
                            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtTem);
                        }
                        else
                        {
                            dtt.Rows.Add(dtTem.Rows[0].ItemArray);
                            dtt.Rows[0]["ID"] = 0;
                            dtt.Rows[0]["PlanID"] = 0;
                            BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllRepertory, dtt);
                            dtTem.Rows[0]["Amount"] = 0;
                            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtTem);

                        }
                        InData();
                    }
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!_IsUseQRID)
                return;
            if (e.FocusedRowHandle > -1)
            {
                if (dtRL.Rows.Count > 2)
                {
                    bool t = false;
                    for (int i = 0; i < dtRL.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(dtRL.Rows[i]["Amount"]) > 0 && Convert.ToInt32(dtRL.Rows[i]["A"]) > 1)
                        {
                            t = true;
                            break;
                        }
                    }
                    if (t)
                    {
                        if (DialogResult.Yes == XtraMessageBox.Show("前次更改是否保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                        {
                            SaveRL();
                        }

                    }
                }
            }
            if (gridView1.FocusedRowHandle > -1)
            {
                Amount = 0;
                _MainID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                object o = gridView1.GetFocusedRowCellValue(_coMListID);
                if (o == null)
                    return;
                string strWhere = string.Empty;
                if (checkEdit1.Checked)
                    strWhere = " And (NotAmount>0)";
                dtRL = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(BatchNumber=" + gridView1.GetFocusedRowCellValue(_coMListID) + ")"+strWhere }).Tables[0];
                if (dtRL.Rows.Count == 0)
                    Amount = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("Amount"));
                DataRow dr = dtRL.NewRow();
                dr["A"] = 3;
                dr["ID"] = 0;
                dr["MainID"] = _MainID;
                if (Amount > 0)
                    dr["NotAmount"] = dr["Amount"] = Amount;
                else
                    dr["NotAmount"] = dr["Amount"] = 0;
                dr["Remark"] = string.Empty;
                dr["StockListID"] = 0;
                dr["BatchNumBer"] = gridView1.GetFocusedRowCellValue(_coMListID);
                dr["IsEnd"] = 0;
                dr["SpecID"] = 0;
                dr["SpecName"] = string.Empty;
                dr["DepotInfoID"] = 0;
                dr["DepotInfoName"] = string.Empty;
                dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
                dr["PlanID"] = gridView1.GetFocusedRowCellValue(_coPlanID);
                dr["QRID"] = 0;
                dtRL.Rows.Add(dr.ItemArray);
                dr["NotAmount"] = dr["Amount"] = 0;
                dtRL.Rows.Add(dr.ItemArray);
                gridControl2.DataSource = dtRL;
                _reDepotInfo.DataSource = BasicClass.GetDataSet.GetDS(bllDep, "GetList", new object[] { "(ParentID=" + gridView1.GetFocusedRowCellValue(_coDepartmentID) + ")" }).Tables[0];
                gridView2.OptionsBehavior.Editable = false;
            }
            else
            {
                dtRL.Rows.Clear();
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle == gridView2.RowCount - 2)
            {
                DataRow dr = dtRL.NewRow();
                dr["A"] = 3;
                dr["ID"] = 0;
                dr["MainID"] = _MainID;
                dr["NotAmount"] = dr["Amount"] = 0;
                dr["Remark"] = string.Empty;
                dr["StockListID"] = 0;
                dr["BatchNumBer"] = 0;
                dr["IsEnd"] = 0;
                dr["SpecID"] = 0;
                dr["SpecName"] = string.Empty;
                dr["DepotInfoID"] = 0;
                dr["DepotInfoName"] = string.Empty;
                dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
                dr["PlanID"] = gridView1.GetFocusedRowCellValue(_coPlanID);
                dr["QRID"] = 0;
                dtRL.Rows.Add(dr.ItemArray);
            }
            if (e.Column != _coIA && Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coIA)) == 1)
            {
                gridView2.SetFocusedRowCellValue(_coIA, 2);
            }
            if(e.Column==_coIAmount)
            {
                Amount = 0;
                gridView2.SetFocusedRowCellValue(_coNotAmount, e.Value);
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    Amount += Convert.ToDecimal(gridView2.GetRowCellValue(i, _coIAmount));
                }
                if (Amount > Convert.ToDecimal(gridView1.GetFocusedRowCellValue("Amount")))
                {
                    XtraMessageBox.Show("明细数量超过帐面库存总数！");
                    Amount -= Convert.ToDecimal(gridView1.GetFocusedRowCellValue("Amount"));
                    gridView2.SetFocusedRowCellValue(_coIAmount, Convert.ToDecimal(gridView2.GetFocusedRowCellValue(_coIAmount)) - Amount);
                }
            }
            else if(e.Column==_coSpecID)
            {
                gridView2.SetFocusedRowCellValue(_coSpecName, gridView2.GetFocusedRowCellDisplayText(_coSpecID));
            }
            else if (e.Column == _coDepotInfoID)
            {
                gridView2.SetFocusedRowCellValue(_coDepotInfoName, gridView2.GetFocusedRowCellDisplayText(_coDepotInfoID));
            }
           
        }
        void SaveRL()
        {
            gridView2.CloseEditor();
            gridView2.UpdateCurrentRow();
            dtRL.AcceptChanges();
            DataTable dtTem = dtRL.Clone();
            int a = 0;
            for (int i = 0; i < dtRL.Rows.Count; i++)
            {
                a = Convert.ToInt32(dtRL.Rows[i]["A"]);
                if (a > 1)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dtRL.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(bllRL, dtTem);
                    else if (a == 3)
                    {
                        if (Convert.ToDecimal(dtRL.Rows[i]["Amount"]) > 0)
                            dtRL.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllRL, dtTem);
                    }
                    dtRL.Rows[i]["A"] = 1;
                }
            }
        }

        private void gridView2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu3(gridView2.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu3(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }
        //编辑
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView2.OptionsBehavior.Editable = true;
        }
        //保存
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveRL();
            dtRL = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            DataRow dr = dtRL.NewRow();
            dr["A"] = 3;
            dr["ID"] = 0;
            dr["MainID"] = _MainID;
            dr["NotAmount"] = dr["Amount"] = 0;
            dr["Remark"] = string.Empty;
            dr["StockListID"] = 0;
            dr["BatchNumBer"] = 0;
            dr["IsEnd"] = 0;
            dr["SpecID"] = 0;
            dr["SpecName"] = string.Empty;
            dr["DepotInfoID"] = 0;
            dr["DepotInfoName"] = string.Empty;
            dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
            dtRL.Rows.Add(dr.ItemArray);
            dtRL.Rows.Add(dr.ItemArray);
            gridControl2.DataSource = dtRL;
            gridView2.OptionsBehavior.Editable = false;
        }
        //删除
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1 && gridView2.FocusedRowHandle < gridView2.RowCount - 2)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("前次更改是否保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    int _id = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coIID));
                    if (_id > 0)
                        BasicClass.GetDataSet.ExecSql(bllRL, "Delete", new object[] { _id });
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);
                    gridView2.CloseEditor();
                    gridView2.UpdateCurrentRow();
                    dtRL.AcceptChanges();
                }
            }
        }
        //打印
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtRL.Rows.Count > 2)
            {
                bool t = false;
                for (int i = 0; i < dtRL.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dtRL.Rows[i]["Amount"]) > 0 && Convert.ToInt32(dtRL.Rows[i]["A"]) > 1)
                    {
                        t = true;
                        break;
                    }
                }
                if (t)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("所做更改是否保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        SaveRL();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            DataTable dtPrint = new DataTable();
            dtPrint.TableName = "dt";
            dtPrint.Columns.Add("物料名", typeof(string));
            dtPrint.Columns.Add("颜色", typeof(string));
            dtPrint.Columns.Add("尺码", typeof(string));
            dtPrint.Columns.Add("插色一", typeof(string));
            dtPrint.Columns.Add("插色二", typeof(string));
            dtPrint.Columns.Add("规格", typeof(string));
            dtPrint.Columns.Add("说明", typeof(string));
            dtPrint.Columns.Add("货位", typeof(string));
            dtPrint.Columns.Add("初始数量", typeof(decimal));
            dtPrint.Columns.Add("时间", typeof(DateTime));
            dtPrint.Columns.Add("二维码", typeof(string));
            dtPrint.Columns.Add("所用款号", typeof(string));
            dtPrint.Columns.Add("所用计划单", typeof(string));
            int _planID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coPlanID));
            string MatName = string.Empty;
            string PlanName = string.Empty;
            if(_planID>0)
            {
                DataTable dtTem = ((DataView)BaseForm.RepositoryItem._rePlanNum.DataSource).Table;
                MatName = dtTem.Select("(ID=" + _planID + ")")[0]["Num"].ToString();
                string[] ss = MatName.Split('-');
                PlanName = ss[0] + "-" + ss[1];
                MatName = ss[2];
            }
            int a = 0;
            for (int i = 0; i < dtRL.Rows.Count; i++)
            {
                a = Convert.ToInt32(dtRL.Rows[i]["StockListID"]);

                DataRow dr = dtPrint.NewRow();
                dr[0] = gridView1.GetFocusedRowCellDisplayText(_coMaterielID);
                dr[1] = gridView1.GetFocusedRowCellDisplayText(_coColorID);
                dr[2] = gridView1.GetFocusedRowCellDisplayText(_coSizeID);
                dr[3] = gridView1.GetFocusedRowCellDisplayText(_coColorOneID);
                dr[4] = gridView1.GetFocusedRowCellDisplayText(_coColorTwoID);
                dr[5] = dtRL.Rows[i]["SpecName"];
                dr[6] = dtRL.Rows[i]["Remark"];
                dr[7] = dtRL.Rows[i]["DepotInfoName"];
                dr[8] = dtRL.Rows[i]["Amount"];
                dr[9] = dtRL.Rows[i]["DateTime"];
                if (a > 0)
                {
                    dr[10] = a;
                }
                else
                {
                    dr[10] = Convert.ToInt32(dtRL.Rows[i]["ID"]) * -1;
                }
                dr[11] = MatName;
                dr[12] = PlanName;
                dtPrint.Rows.Add(dr);

            }
            BaseForm.PrintClass.PrintMaterielQR(dtPrint);
        }
    }
}