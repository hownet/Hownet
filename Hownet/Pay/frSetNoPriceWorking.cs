using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Pay
{
    public partial class frSetNoPriceWorking : DevExpress.XtraEditors.XtraForm
    {
        public frSetNoPriceWorking()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void frSetNoPriceWorking_Load(object sender, EventArgs e)
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coWorkingID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingMain, "GetSetNoPrice", null).Tables[0];
            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            decimal price = 0;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                if(Convert.ToInt32(dt.Rows[i]["A"])==2)
                {
                    try
                    {
                        price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "UpWorkPrice", new object[] { Convert.ToInt32(dt.Rows[i]["ID"]), price });
                    }
                    catch
                    {

                    }
                }
            }
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingMain, "GetSetNoPrice", null).Tables[0];
            gridControl1.DataSource = dt;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != DBNull.Value && e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
        }
    }
}