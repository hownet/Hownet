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
    public partial class frPayTiChengSet : DevExpress.XtraEditors.XtraForm
    {
        public frPayTiChengSet()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.OtherType";
        DataTable dt = new DataTable();
        DataTable dtOT = new DataTable();
        int _ID = 0;
        int _PID = 117;//默认为117
        private void frPayTiChengSet_Load(object sender, EventArgs e)
        {
            try
            {
                dt = BasicClass.GetDataSet.GetBySql("SELECT TOP 1 ID FROM OtherType AS OtherType_1  WHERE (Name = '工资提成') AND (TypeID = 0)");
                if(dt.Rows.Count==1)
                {
                    _PID = Convert.ToInt32(dt.Rows[0]["ID"]);
                }
                else
                {
                    dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] {"1=2" }).Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["A"] = 1;
                    dr["ID"] = 0;
                    dr["TypeID"] = 0;
                    dr["Name"] = "工资提成";
                    dr["Value"] = string.Empty;
                    dt.Rows.Add(dr);
                    _PID = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dt);
                }
                dt = BasicClass.GetDataSet.GetDS(bll, "GetTypeList", new object[] { "工资提成" }).Tables[0];

                dtOT = dt.Copy();
                dtOT.Columns.Add("NValue", typeof(decimal));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtOT.Rows[i]["NValue"] = Convert.ToDecimal(dt.Rows[i]["Value"]);
                }
                dtOT.DefaultView.Sort = "NValue";
                gridControl1.DataSource = dtOT.DefaultView;
            }
            catch (Exception ex)
            {
            }
        }

        private void _buAdd_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue = string.Empty;
            textEdit2.EditValue = string.Empty;
            _ID = 0;
        }

        private void _buSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ID == 0)
                {
                    DataTable dtTem = dtOT.Clone();
                    DataRow dr = dtTem.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["Name"] = Convert.ToDecimal(textEdit1.EditValue);
                    dr["TypeID"] = _PID;
                    dr["Value"] = Convert.ToDecimal(textEdit2.EditValue);
                    dr["NValue"] = Convert.ToDecimal(textEdit2.EditValue);
                    dtTem.Rows.Add(dr);
                    dtTem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bll, dtTem);
                    dtOT.Rows.Add(dtTem.Rows[0].ItemArray);
                }
                else
                {
                    DataTable dtTem = dtOT.Clone();
                    DataRow dr = dtTem.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = _ID;
                    dr["Name"] = Convert.ToDecimal(textEdit1.EditValue);
                    dr["TypeID"] = _PID;
                    dr["Value"] = Convert.ToDecimal(textEdit2.EditValue);
                    dr["NValue"] = Convert.ToDecimal(textEdit2.EditValue);
                    dtTem.Rows.Add(dr);
                    BasicClass.GetDataSet.UpData(bll, dtTem);
                    DataRow[] drs = dtOT.Select("(ID=" + _ID + ")");
                    drs[0]["Name"] = Convert.ToDecimal(textEdit1.EditValue);
                    drs[0]["Value"] = Convert.ToDecimal(textEdit2.EditValue);
                    drs[0]["NValue"] = Convert.ToDecimal(textEdit2.EditValue);
                    dtOT.AcceptChanges();
                }
                dtOT.DefaultView.Sort = "NValue";
                gridControl1.DataSource = dtOT.DefaultView;
            }
            catch (Exception ex)
            {
            }
            textEdit1.EditValue = string.Empty;
            textEdit2.EditValue = string.Empty;
            _ID = 0;
        }

        private void _buExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                textEdit1.EditValue = gridView1.GetFocusedRowCellValue(_coName);
                textEdit2.EditValue = gridView1.GetFocusedRowCellValue(_coNValue);
                _ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            //{
            //    _ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            //    BasicClass.GetDataSet.ExecSql(bll,"Excel
            //}
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }
    }
}