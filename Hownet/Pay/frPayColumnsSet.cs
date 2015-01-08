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
    public partial class frPayColumnsSet : DevExpress.XtraEditors.XtraForm
    {
        public frPayColumnsSet()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dtCaic = new DataTable();
        string bll = "Hownet.BLL.PayColumnsSet";
        private void frPayColumnsSet_Load(object sender, EventArgs e)
        {
            dtCaic.Columns.Add("ID", typeof(int));
            dtCaic.Columns.Add("Name", typeof(string));
            dtCaic.Rows.Add(1, "增加");
            dtCaic.Rows.Add(-1, "减少");
            repositoryItemLookUpEdit1.DataSource = dtCaic;
            dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            DataTable dtTem = dt.Clone();
            int a = 1;
            for (int i = 0; i <dt.Rows.Count; i++)
            {
                a = Convert.ToInt32(dt.Rows[i]["A"]);
                if (a == 2&&dt.Rows[i]["Name"].ToString().Trim().Length>0)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dt.Rows[i].ItemArray);
                    dtTem.Rows[0]["Name"] = dt.Rows[i]["Name"].ToString().Trim();
                    BasicClass.GetDataSet.UpData(bll, dtTem);
                    dt.Rows[i]["A"] = 1;
                }
            }
            dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            gridControl1.DataSource = dt;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = !(gridView1.GetFocusedRowCellDisplayText(_coColumnsName) == "BoardWages" || gridView1.GetFocusedRowCellDisplayText(_coColumnsName) == "Deposit");
        }
    }
}