using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Task
{
    public partial class frTaskSet : DevExpress.XtraEditors.XtraForm
    {
        public frTaskSet()
        {
            InitializeComponent();
        }
        DataTable dtOT = new DataTable();
        DataTable dtLR = new DataTable();
        private void frTaskSet_Load(object sender, EventArgs e)
        {
            dtOT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Value='生产单设置')" }).Tables[0];
            DataRow dr = dtOT.NewRow();
            dr["A"] = 3;
            dr["ID"] = 0;
            dr["TypeID"] = -99;
            dr["Value"] = "生产单设置";
            dr["Name"] = string.Empty;
            dtOT.Rows.Add(dr.ItemArray);
            dtOT.Rows.Add(dr.ItemArray);
            gridControl1.DataSource = dtOT;
            dtLR = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Value='列表记录条数')" }).Tables[0];
            if(dtLR.Rows.Count==0)
            {
                DataRow drL = dtLR.NewRow();
                drL["A"] = 1;
                drL["ID"] = 0;
                drL["TypeID"] = -99;
                drL["Value"] = "列表记录条数";
                drL["Name"] = 4;
                dtLR.Rows.Add(drL);
                dtLR.Rows[0]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dtLR);
            }
            textEdit1.EditValue = Convert.ToInt32(dtLR.Rows[0]["Name"]);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == DBNull.Value)
                return;
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
            if (e.Column == _coName)
            {
                string name = e.Value.ToString();
                if (name != string.Empty)
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if(i!=e.RowHandle)
                        {
                            if(gridView1.GetRowCellDisplayText(i,_coName).Trim()==name)
                            {
                                MessageBox.Show("有重复！");
                                break;
                            }
                        }
                    }
                }
            }
            if(e.RowHandle==gridView1.RowCount-2)
            {
                DataRow dr = dtOT.NewRow();
                dr["A"] = 3;
                dr["ID"] = 0;
                dr["TypeID"] = -99;
                dr["Value"] = "生产单设置";
                dr["Name"] = string.Empty;
                dtOT.Rows.Add(dr.ItemArray);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = e.FocusedRowHandle < gridView1.RowCount - 1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(textEdit1.EditValue)>8)
            {
                XtraMessageBox.Show("使用表格形式记录说明项的列数不能超过8列");
                return;
            }
            DataTable dtTem = dtOT.Clone();
            int a = 0;
            for (int i = 0; i <dtOT.Rows.Count - 2; i++)
            {
                a = Convert.ToInt32(dtOT.Rows[i]["A"]);
                if(a>1)
                {
                    dtTem.Rows.Clear();
                    if(a==2)
                    {
                        dtTem.Rows.Add(dtOT.Rows[i].ItemArray);
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtTem);
                    }
                    else if (a==3)
                    {
                        if(dtOT.Rows[i]["Name"].ToString().Trim()!=string.Empty)
                        {
                            dtTem.Rows.Add(dtOT.Rows[i].ItemArray);
                           dtOT.Rows[i]["ID"]= BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dtTem);
                        }
                    }
                    dtOT.Rows[i]["A"] = 1;
                }
            }
            dtLR.Rows[0]["Name"] = textEdit1.EditValue;
            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtLR);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}