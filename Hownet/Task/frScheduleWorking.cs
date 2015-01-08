using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Task
{
    public partial class frScheduleWorking : DevExpress.XtraEditors.XtraForm
    {
        public frScheduleWorking()
        {
            InitializeComponent();
        }
        string bllW = "Hownet.BLL.Working";
        string bllOT = "Hownet.BLL.OtherType";
        DataTable dtOT = new DataTable();
        DataTable dtW = new DataTable();
        DataTable dtOTI = new DataTable();
        int _OTID = 0;
        private void frScheduleWorking_Load(object sender, EventArgs e)
        {
            dtW = BasicClass.GetDataSet.GetDS(bllW, "GetAllList", null).Tables[0];
            _loWorking.Properties.DataSource = dtW;
            dtOT = BasicClass.GetDataSet.GetDS(bllOT, "GetList", new object[] {"(Name='进度工序') And (TypeID=0)" }).Tables[0];

            if (dtOT.Rows.Count == 0)
            {
                DataRow dr = dtOT.NewRow();
                dr["ID"] = 0;
                dr["A"] = 0;
                dr["Name"] = "进度工序";
                dr["TypeID"] = 0;
                dr["Value"] = string.Empty;
                dtOT.Rows.Add(dr);
                dtOT.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllOT, dtOT);
            }
            dtOTI = BasicClass.GetDataSet.GetDS(bllOT, "GetTypeList", new object[] { "进度工序" }).Tables[0];
           // dtOTI.Columns["Value"].DataType = System.Type.GetType("System.Int32");
            gridControl1.DataSource = dtOTI;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int _workingID = Convert.ToInt32(_loWorking.EditValue);
            if (_workingID > 0)
            {
                if (dtOTI.Select("(Value=" + _workingID + ")").Length == 0)
                {
                    DataTable dtTem = dtOTI.Clone();
                    DataRow dr = dtTem.NewRow();
                    dr["ID"] = 0;
                    dr["A"] = 0;
                    dr["Name"] = _loWorking.Text;
                    dr["TypeID"] = dtOT.Rows[0]["ID"];
                    dr["Value"] = _workingID;
                    dtTem.Rows.Add(dr);
                    dtTem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllOT, dtTem);
                    dtOTI.Rows.Add(dtTem.Rows[0].ItemArray);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    _OTID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                    BasicClass.GetDataSet.ExecSql(bllOT, "Delete", new object[] {_OTID });
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    dtOTI.AcceptChanges();
                    gridView1.CloseEditor();
                    gridView1.UpdateCurrentRow();
                }
            }
        }
    }
}