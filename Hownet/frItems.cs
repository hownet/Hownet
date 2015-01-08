using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet
{
    public partial class frItems : Form
    {
        public frItems()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void frItems_Load(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.Items", "GetList", new object[] { "(1=1) Order by ParentID,Orders" }).Tables[0];
            DataRow dr = dt.NewRow();
            dr["A"] = 3;
            dr["ID"] = 0;
            dr["ParentID"] = 0;
            dr["IsModule"] = 0;
            dr["Level"] = 0;
            dr["Orders"] = 0;
            dr["IsBar"] = 0;
            dr["Text"] = string.Empty;
            dr["FormName"] = string.Empty;
            dr["Parameter"] = string.Empty;
            dt.Rows.Add(dr.ItemArray);
            dt.Rows.Add(dr.ItemArray);
            gridControl1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 1;
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            DataTable dtTem=dt.Clone();
            for(int i=0;i<dt.Rows.Count-2;i++)
            {
               a = Convert.ToInt32(dt.Rows[i]["A"]);
                if(a>1)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dt.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData("Hownet.BLL.Items", dtTem);
                    else if (a == 3)
                        dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add("Hownet.BLL.Items", dtTem);
                    dt.Rows[i]["A"] = 1;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1 && gridView1.FocusedRowHandle < gridView1.RowCount - 2)
            {
                if (DialogResult.Yes == MessageBox.Show("真的删除", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    int _id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                    if(_id>0)
                    {
                        BasicClass.GetDataSet.ExecSql("Hownet.BLL.Items", "Delete", new object[] { _id });
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    dt.AcceptChanges();
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == DBNull.Value)
                return;
            if (e.Column.FieldName != "A" && Convert.ToInt32(gridView1.GetFocusedRowCellValue("A")) == 1)
                gridView1.SetFocusedRowCellValue("A", 2);
            if(e.RowHandle==gridView1.RowCount-2)
            {
                DataRow dr = dt.NewRow();
                dr["A"] = 3;
                dr["ID"] = 0;
                dr["ParentID"] = 0;
                dr["IsModule"] = 0;
                dr["Level"] = 0;
                dr["Orders"] = 0;
                dr["IsBar"] = 0;
                dr["Text"] = string.Empty;
                dr["FormName"] = string.Empty;
                dr["Parameter"] = string.Empty;
                dt.Rows.Add(dr.ItemArray);
            }
        }
    }
}

