using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Hownet.BaseForm
{
    public partial class BarSpecialWork : DevExpress.XtraEditors.XtraForm
    {
        public BarSpecialWork()
        {
            InitializeComponent();
        }
        //bool t = false;
        string bllMa = "Hownet.BLL.Materiel";
        string bllPWI = "Hownet.BLL.ProductWorkingInfo";
      //  string bllW = "Hownet.BLL.Working";
      //  int _mainID = 0;
        int _materielID = 0;
        ArrayList liDel = new ArrayList();
        DataTable dtInfo = new DataTable();
        DataTable dtM = new DataTable();
        DataTable dtWork = new DataTable();
        private void StockBackForm_Load(object sender, EventArgs e)
        {
            InData();
            _coWorkingID.ColumnEdit = RepositoryItem._reNotSpecialWorking;
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void InData()
        {
            dtM = BasicClass.GetDataSet.GetDS(bllMa, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
            gridControl2.DataSource = dtM;
        }
        #region 记录移动

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount > 0 && ((DataTable)(gridControl1.DataSource)).GetChanges() != null)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("修改后未保存，是否保存后再退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    Save();
                }
            }
            this.Close();
        }
        #endregion

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int c = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (int.Parse(gridView1.GetRowCellValue(i, _coColorID).ToString()) == 0)
                    c += 1;
            }
            if (c == 0)
            {
                XtraMessageBox.Show("请指明一个颜色为空的对应工序！");
                return;
            }
            Save();
          //  gridControl1.DataSource = bllPWI.GetList("(MaterielID=" + MaterielID + ")").Tables[0];
        }
        private void Save()
        {
            
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                int a = int.Parse(gridView1.GetRowCellValue(i, _coA).ToString());
                if (a != 1) 
                {
                    //modPWI.MainID = -1;
                    //modPWI.ProductWorkingID = int.Parse(gridView1.GetRowCellValue(i, _coID).ToString());
                    //modPWI.MaterielID = MaterielID;
                    //modPWI.ColorID = int.Parse(gridView1.GetRowCellValue(i, _coColorID).ToString());
                    //modPWI.SpecialWork = int.Parse(gridView1.GetRowCellValue(i, _coSpecialWork).ToString());
                    //modPWI.WorkingID = int.Parse(gridView1.GetRowCellValue(i, _coWorkingID).ToString());
                    //modPWI.Price = decimal.Parse(gridView1.GetRowCellValue(i, _coPrice).ToString());
                    //modPWI.GroupBy = int.Parse(gridView1.GetRowCellValue(i, _coGroupBy).ToString());
                    //modPWI.Orders = int.Parse(gridView1.GetRowCellValue(i, _coOrders).ToString());
                    //modPWI.StitchWidth = "";
                    //modPWI.Remark = "";
                    //modPWI.IsTicket = true;
                    //if (modPWI.SpecialWork != 0 && modPWI.WorkingID != 0)
                    //{
                    //    if (a == 2)
                    //        bllPWI.Update(modPWI);
                    //    else if (a == 3)
                    //        bllPWI.Add(modPWI);
                    //}
                    //else
                    //{
                    //    bllPWI.Delete(modPWI.ProductWorkingID);
                    //}
                }
            }
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column != _coA)
            {
                if (gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if (e.Column == _coSpecialWork)
            {
                object or = _reWork.GetDataSourceValue("Orders", _reWork.GetDataSourceRowIndex("WorkingID", e.Value));
                object gr = _reWork.GetDataSourceValue("GroupBy", _reWork.GetDataSourceRowIndex("WorkingID", e.Value));
                gridView1.SetFocusedRowCellValue(_coOrders, or);
                gridView1.SetFocusedRowCellValue(_coGroupBy, gr);
            }
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                _materielID = Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coMID));
                dtInfo = BasicClass.GetDataSet.GetDS(bllPWI, "GetList", new object[] { "(MaterielID=" + _materielID + ")" }).Tables[0];
                DataRow dr = dtInfo.NewRow();
                dr["A"] = 0;
                dtInfo.Rows.Add(dr.ItemArray);
                dtInfo.Rows.Add(dr.ItemArray);
                gridControl1.DataSource = dtInfo;
            }
        }
    }
}