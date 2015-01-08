using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frDefaultSP : DevExpress.XtraEditors.XtraForm
    {
        public frDefaultSP()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        string bllSTK = "Hownet.BLL.SizeTableTask";
        int MTID=0;
        private void frDefaultSP_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] { "(ParentID>0) And (AttributeID=4)" }).Tables[0];
            lookUpEdit1.EditValue = 0;
            _coSizePartID.ColumnEdit = RepositoryItem._reSizePart;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
            {
                MTID=Convert.ToInt32(lookUpEdit1.EditValue);
                dt = BasicClass.GetDataSet.GetDS(bllSTK, "GetList", new object[] { "(TaskID=" + MTID * -1 + ")" }).Tables[0];
                DataRow dr = dt.NewRow();
                dr["ID"] = dr["SizeID"] = dr["SizePartID"] = dr["Length"] = 0;
                dr["A"] = 3;
                dr["TaskID"] = MTID * -1;
                dt.Rows.Add(dr.ItemArray);
                dt.Rows.Add(dr.ItemArray);
                gridControl1.DataSource = dt;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < gridView1.RowCount - 1);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
                return;
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = dr["SizeID"] = dr["SizePartID"] = dr["Length"] = 0;
                dr["A"] = 3;
                dr["TaskID"] = MTID * -1;
                dt.Rows.Add(dr.ItemArray);
            }
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dtt = new DataTable();
            int a = 0;
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {
                a = Convert.ToInt32(dt.Rows[i]["A"]);
                if (a > 0)
                {
                    dtt = dt.Clone();
                    dtt.Rows.Add(dt.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(bllSTK, dtt);
                    else if (a == 3)
                        dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllSTK, dtt);
                    dt.Rows[i]["A"] = 1;
                }
            }
        }
    }
}