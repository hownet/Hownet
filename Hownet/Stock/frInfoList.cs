using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Stock
{
    public partial class frInfoList : DevExpress.XtraEditors.XtraForm
    {
        public frInfoList()
        {
            InitializeComponent();
        }
        int _InfoID = 0;
        string _MaterielName = string.Empty;
        string _CS = string.Empty;
        string _MeasureName = string.Empty;
        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtSBIL = new DataTable();
        DataTable dtDepInfo = new DataTable();
        string bll = "Hownet.BLL.StockBackInfoList";
        string bllDep = "Hownet.BLL.Deparment";

        decimal _Amount = 0;
        int _DepotID = 0;
        int _MainID = 0;
        int _ID = 0;
        bool _IsVerify = false;
        public frInfoList(int InfoID, string MaterielName, string CS,string MeasureName,int DepotID,int MainID,bool IsVerify, BasicClass.cResult cr)
            : this()
        {
            _InfoID = InfoID;
            _MaterielName = MaterielName;
            _CS = CS;
            _MeasureName = MeasureName;
            _DepotID = DepotID;
            _MainID = MainID;
            _IsVerify = IsVerify;
            r = cr;
        }
        private void frInfoList_Load(object sender, EventArgs e)
        {
            this.Text = _MaterielName + _CS + "   明细数量";
            _lbMeasure.Text = _MeasureName;
            _coSpecID.ColumnEdit = BaseForm.RepositoryItem._reSpec;
            lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtSpec;
            dtDepInfo= BasicClass.GetDataSet.GetDS(bllDep, "GetList", new object[] { "(ParentID=" + _DepotID+ ")" }).Tables[0];
            lookUpEdit2.Properties.DataSource = dtDepInfo;
            repositoryItemLookUpEdit1.DataSource = dtDepInfo;
            dtSBIL = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(InfoID=" + _InfoID + ")" }).Tables[0];
            gridControl1.DataSource = dtSBIL;
            lookUpEdit1.EditValue = 0;
            lookUpEdit2.EditValue = 0;
            _teReamrk.EditValue = string.Empty;
            _teAmount.EditValue = DBNull.Value;
            panel1.Visible = !_IsVerify;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_teAmount.Text.Trim().Length == 0 || Convert.ToDecimal(_teAmount.EditValue) == 0)
                return;
            if (simpleButton1.Text == "确定")
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否确认添加？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    _Amount = 0;
                    DataRow dr = dtSBIL.NewRow();
                    dr["A"] = 1;
                    dr["ID"] = 0;
                    dr["InfoID"] = _InfoID;
                    dr["Amount"] = Convert.ToDecimal(_teAmount.EditValue);
                    dr["Remark"] = _teReamrk.EditValue;
                    dr["SpecID"] = lookUpEdit1.EditValue;
                    dr["DepotInfoID"] = lookUpEdit2.EditValue;
                    dr["MainID"] = _MainID;
                    DataTable dtTem = dtSBIL.Clone();
                    dtTem.Rows.Add(dr.ItemArray);
                    dtTem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bll, dtTem);
                    dtSBIL.Rows.Add(dtTem.Rows[0].ItemArray);
                    Print(Convert.ToInt32(dtTem.Rows[0]["ID"]));
                    lookUpEdit1.EditValue = 0;
                   // lookUpEdit2.EditValue = 0;
                    _teAmount.EditValue = DBNull.Value;
                    _teReamrk.EditValue = string.Empty;
                    //for (int i = 0; i < gridView1.RowCount; i++)
                    //{
                    //    _Amount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount));
                    //}
                    //r.ChangeText(_Amount.ToString());
                }
            }
            else
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否确认修改？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    _Amount = 0;
                    DataTable dtTem = dtSBIL.Clone();
                    DataRow[] drs = dtSBIL.Select("(ID=" + _ID + ")");

                    drs[0]["Amount"] = Convert.ToDecimal(_teAmount.EditValue);
                    drs[0]["Remark"] = _teReamrk.EditValue;
                    drs[0]["SpecID"] = lookUpEdit1.EditValue;
                    drs[0]["DepotInfoID"] = lookUpEdit2.EditValue;
                   
                    
                    dtTem.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(bll, dtTem);
                    dtSBIL.AcceptChanges();
                    Print(Convert.ToInt32(dtTem.Rows[0]["ID"]));
                    lookUpEdit1.EditValue = 0;
                    //lookUpEdit2.EditValue = 0;
                    _teAmount.EditValue = DBNull.Value;
                    _teReamrk.EditValue = string.Empty;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        _Amount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount));
                    }
                    r.ChangeText(_Amount.ToString());
                    simpleButton1.Text = "确定";
                }
            }

        }
        private void Print(int SBILID)
        {
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

            DataRow dr = dtPrint.NewRow();
            dr[0] = _MaterielName;
            dr[1] = _CS;
            dr[2] = string.Empty;
            dr[3] = string.Empty;
            dr[4] = string.Empty;
            dr[5] = lookUpEdit1.Text;
            dr[6] = _teReamrk.EditValue;
            dr[7] = lookUpEdit2.Text;
            dr[8] = _teAmount.EditValue;
            dr[9] = BasicClass.GetDataSet.GetDateTime();
            dr[10] = SBILID;
            dr[11] = string.Empty;
            dr[12] = string.Empty;
            dtPrint.Rows.Add(dr);

            BaseForm.PrintClass.PrintMaterielQR(dtPrint);
        }

        private void lookUpEdit2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                Form fr = new BaseForm.frDeparment();
                fr.ShowDialog();
                dtDepInfo = BasicClass.GetDataSet.GetDS(bllDep, "GetList", new object[] { "(ParentID=" + _DepotID + ")" }).Tables[0];
                lookUpEdit2.Properties.DataSource = dtDepInfo;
                repositoryItemLookUpEdit1.DataSource = dtDepInfo;
            }
        }
        //修改
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            _teAmount.EditValue = gridView1.GetFocusedRowCellValue(_coAmount);
            _teReamrk.EditValue = gridView1.GetFocusedRowCellValue(_coRemark);
            lookUpEdit1.EditValue = gridView1.GetFocusedRowCellValue(_coSpecID);
            lookUpEdit2.EditValue = gridView1.GetFocusedRowCellValue(_coDepotInfoID);
            simpleButton1.Text = "修改";
        }
        //打印
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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

            DataRow dr = dtPrint.NewRow();
            dr[0] = _MaterielName;
            dr[1] = _CS;
            dr[2] = string.Empty;
            dr[3] = string.Empty;
            dr[4] = string.Empty;
            dr[5] = gridView1.GetFocusedRowCellDisplayText(_coSpecID);
            dr[6] = gridView1.GetFocusedRowCellDisplayText(_coRemark);
            dr[7] = gridView1.GetFocusedRowCellDisplayText(_coDepotInfoID);
            dr[8] = gridView1.GetFocusedRowCellDisplayText(_coAmount);
            dr[9] = BasicClass.GetDataSet.GetDateTime();
            dr[10] = gridView1.GetFocusedRowCellValue(_coID); ;
            dr[11] = string.Empty;
            dr[12] = string.Empty;
            dtPrint.Rows.Add(dr);

            BaseForm.PrintClass.PrintMaterielQR(dtPrint);
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

        private void frInfoList_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Amount = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                _Amount += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount));
            }
            r.ChangeText(_Amount.ToString());
        }
    }
}