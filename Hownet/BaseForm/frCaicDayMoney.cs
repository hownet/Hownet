using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BaseForm
{
    public partial class frCaicDayMoney : DevExpress.XtraEditors.XtraForm
    {
        public frCaicDayMoney()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.CaicDayMoney";
        DataTable dt = new DataTable();
        private void frCaicDayMoney_Load(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            DataRow dr = dt.NewRow();
            dr["A"] = 3;
            dr["ID"] = 0;
            dr["Remark"] = string.Empty;
            dt.Rows.Add(dr.ItemArray);
            dt.Rows.Add(dr.ItemArray);
            gridControl1.DataSource = dt;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
                return;
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dt.NewRow();
                dr["A"] = 3;
                dr["ID"] = 0;
                dr["Remark"] = string.Empty;
                dt.Rows.Add(dr.ItemArray);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            DataTable dtt = dt.Clone();
            int a = 0;
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {
                a = Convert.ToInt32(dt.Rows[i]["A"]);
                if (a > 1)
                {
                    dtt = dt.Clone();
                    dtt.Rows.Add(dt.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(bll, dtt);
                    else if (a == 3)
                        dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bll, dtt);
                    dt.Rows[i]["A"] = 1;
                }
            }
        }
    }
}