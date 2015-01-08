using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hownet.Model;

namespace Hownet.Pay
{
    public partial class AttendanceRulesForm : DevExpress.XtraEditors.XtraForm
    {
        public AttendanceRulesForm()
        {
            InitializeComponent();
        }
        List<modOtherType> list;
        OtherTypeOTT bllOT = new OtherTypeOTT();
        DataTable dt = new DataTable();
        private void AttendanceRulesForm_Load(object sender, EventArgs e)
        {
            list =bllOT.DataTableToList( BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType,"GetTypeList",new object[]{"¿¼ÇÚ¹æÔò"}).Tables[0]);
            for (int i = 0; i < list.Count; i++)
            {
                string ot = list[i].Name;
                string val = list[i].Value;
                if (ot == "OutTime")
                    _tOutTime.EditValue = val;
                else if (ot == "OutCount")
                    _tOutCount.EditValue = val;
                else if (ot == "HoursS")
                    _tHoursS.EditValue = val;
                else if (ot == "HalfAnHour")
                    _tHalfAnHour.EditValue = val;
                else if (ot == "LateS")
                    _tLateS.EditValue = val;
                else if (ot == "Absenteeism")
                    _tAbsenteeism.EditValue = val;
                else if (ot == "BeLate")
                    _seBeLate.EditValue = val;
                else if (ot == "BeLateMiners")
                    _seBeLateMiners.EditValue = val;
                else if (ot == "LeaveEarly")
                    _seLeaveEarly.EditValue = val;
                else if (ot == "LeaveEarlyMiners")
                    _seLeaveEarlyMiners.EditValue = val;
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                simpleButton1.Enabled = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string ot = list[i].Name;
                if (ot == "OutTime")
                    list[i].Value = _tOutTime.EditValue.ToString();
                else if (ot == "OutCount")
                    list[i].Value = _tOutCount.EditValue.ToString();
                else if (ot == "HoursS")
                    list[i].Value = _tHoursS.EditValue.ToString();
                else if (ot == "HalfAnHour")
                    list[i].Value = _tHalfAnHour.EditValue.ToString();
                else if (ot == "LateS")
                    list[i].Value = _tLateS.EditValue.ToString();
                else if (ot == "Absenteeism")
                    list[i].Value = _tAbsenteeism.EditValue.ToString();
                else if (ot == "BeLate")
                    list[i].Value = _seBeLate.EditValue.ToString();
                else if (ot == "BeLateMiners")
                    list[i].Value = _seBeLateMiners.EditValue.ToString();
                else if (ot == "LeaveEarly")
                    list[i].Value = _seLeaveEarly.EditValue.ToString();
                else if (ot == "LeaveEarlyMiners")
                    list[i].Value = _seLeaveEarlyMiners.EditValue.ToString();
            }
            DataTable dtt = BasicClass.ToDataTable.ListToDataTable<modOtherType>(list);
            dtt.TableName = "dt";
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                DataTable dtttt = dtt.Clone();
                dtttt.Rows.Add(dtt.Rows[i].ItemArray);
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtttt);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}