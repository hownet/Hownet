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
    public partial class frWorkingPrice : DevExpress.XtraEditors.XtraForm
    {
        public frWorkingPrice()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        public frWorkingPrice(BasicClass.cResult cr)
            : this()
        {
            r = cr;
        }
        DataTable dtMateriel = new DataTable();
        DataTable dtWork = new DataTable();
        
        private void frWorkingPrice_Load(object sender, EventArgs e)
        {
            dtWork = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorking, "GetAllList", null).Tables[0];
          //  dtWork.Columns.Add("Price", typeof(decimal));
            dtWork.Columns.Add("OK", typeof(string));
            gridControl2.DataSource = dtWork;
            _coWorkingID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coPrice.ColumnEdit = repositoryItemTextEdit1;
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                dtMateriel = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingMain, "GetSetPrice", new object[] { Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coWorkingID)) }).Tables[0];
                gridControl1.DataSource = dtMateriel;
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coIsSelect && e.Value != null)
            {
                gridView1.SetFocusedRowCellValue(_coIsSelect, e.Value);
                gridView1.UpdateCurrentRow();
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            decimal price = 0;
            try
            {
                price = Convert.ToDecimal(gridView2.GetFocusedRowCellValue(_coNewPrice));
            }
            catch 
            {
                XtraMessageBox.Show("请正确填写工价！");
                return;
            }
            if (price == 0 || price < 0)
            {
                XtraMessageBox.Show("请正确填写工价！");
                return;
            }
            if (gridView1.RowCount == 0)
                return;
            if (DialogResult.Yes == XtraMessageBox.Show("确认将所选款号的工序  " + gridView2.GetFocusedRowCellDisplayText(_coWorkingID) + "工价，修改为  " + price.ToString("C3"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsSelect)))
                    {
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "UpWorkPrice", new object[] { Convert.ToInt32(gridView1.GetRowCellValue(i, _coID)), price });
                        gridView1.SetRowCellValue(i, _coPrice, price);
                    }
                }
            }
        }
    }
}