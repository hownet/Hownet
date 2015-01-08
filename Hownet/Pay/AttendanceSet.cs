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
    public partial class AttendanceSet : DevExpress.XtraEditors.XtraForm
    {
        public AttendanceSet()
        {
            InitializeComponent();
        }
        List<modOtherType> list;
        OtherTypeOTT bllOT = new OtherTypeOTT();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        private void AttendanceSet_Load(object sender, EventArgs e)
        {
            _tThree.ToolTip = _tOne.ToolTip = _tTwo.ToolTip = "正点上班时间";
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "考勤设置" }).Tables[0];
            list = bllOT.DataTableToList(dt);
            for (int i = 0; i < list.Count; i++)
            {
                string ot=list[i].Name;
                string val = list[i].Value;
                if (ot == "One")
                    _tOne.EditValue = val;
                else if (ot == "Two")
                    _tTwo.EditValue = val;
                else if (ot == "Three")
                    _tThree.EditValue = val;
                else if (ot == "lOne")
                    _lOne.EditValue = val;
                else if (ot == "lTwo")
                    _lTwo.EditValue = val;
                else if (ot == "lThree")
                    _lThree.EditValue = val;
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                simpleButton1.Enabled = false;
            dt2 = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='按实际刷卡计时')" }).Tables[0];
            checkEdit1.Checked = (Convert.ToInt32(dt2.Rows[0]["Value"])==1);
            dt3 = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Name='刷上班卡后才能计件')" }).Tables[0];
            if (dt3.Rows.Count == 0)
            {
                DataRow dr = dt3.NewRow();
                dr["A"] = 1;
                dr["ID"] = 0;
                dr["Name"] = "刷上班卡后才能计件";
                dr["TypeID"] = -1;
                dr["Value"] = 0;
                dt3.Rows.Add(dr);
                dt3.Rows[0]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dt3);
            }
            checkEdit2.Checked = Convert.ToInt32(dt3.Rows[0]["Value"]) == 1;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < list.Count; i++)
            {
                string ot = list[i].Name;
                if (ot == "One")
                {
                    if (_tOne.EditValue.ToString().Length > 6)
                        list[i].Value = ((DateTime)(_tOne.EditValue)).Hour.ToString().PadLeft(2, '0') + ":" + ((DateTime)(_tOne.EditValue)).Minute.ToString().PadLeft(2, '0');
                    else
                        list[i].Value = _tOne.EditValue.ToString();
                }
                else if (ot == "Two")
                {
                    if (_tTwo.EditValue.ToString().Length > 6)
                        list[i].Value = ((DateTime)(_tTwo.EditValue)).Hour.ToString().PadLeft(2, '0') + ":" + ((DateTime)(_tTwo.EditValue)).Minute.ToString().PadLeft(2, '0');
                    else
                        list[i].Value = _tTwo.EditValue.ToString();
                }
                else if (ot == "Three")
                {
                    if (_tThree.EditValue.ToString().Length > 6)
                        list[i].Value = ((DateTime)(_tThree.EditValue)).Hour.ToString().PadLeft(2, '0') + ":" + ((DateTime)(_tThree.EditValue)).Minute.ToString().PadLeft(2, '0');
                    else
                        list[i].Value = _tThree.EditValue.ToString();
                }
                else if (ot == "lOne")
                {
                    if (_lOne.EditValue.ToString().Length > 6)
                        list[i].Value = ((DateTime)(_lOne.EditValue)).Hour.ToString().PadLeft(2, '0') + ":" + ((DateTime)(_lOne.EditValue)).Minute.ToString().PadLeft(2, '0');
                    else
                        list[i].Value = _lOne.EditValue.ToString();
                }
                else if (ot == "lTwo")
                {
                    if (_lTwo.EditValue.ToString().Length > 6)
                        list[i].Value = ((DateTime)(_lTwo.EditValue)).Hour.ToString().PadLeft(2, '0') + ":" + ((DateTime)(_lTwo.EditValue)).Minute.ToString().PadLeft(2, '0');
                    else
                        list[i].Value = _lTwo.EditValue.ToString();
                }
                else if (ot == "lThree")
                {
                    if (_lThree.EditValue.ToString().Length > 6)
                        list[i].Value = ((DateTime)(_lThree.EditValue)).Hour.ToString().PadLeft(2, '0') + ":" + ((DateTime)(_lThree.EditValue)).Minute.ToString().PadLeft(2, '0');
                    else
                        list[i].Value = _lThree.EditValue.ToString();
                }
            }
            DataTable dtt = BasicClass.ToDataTable.ListToDataTable<modOtherType>(list);
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                DataTable dtttt = dtt.Clone();
                dtttt.TableName = "dt";
                dtttt.Rows.Add(dtt.Rows[i].ItemArray);
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtttt);
            }
            dt2.Rows[0]["Value"] = Convert.ToInt32(checkEdit1.Checked);
            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dt2);
            dt3.Rows[0]["Value"] = Convert.ToInt32(checkEdit2.Checked);
            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dt3);
            BasicClass.GetDataSet.SetDataTable();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private string ddd(int h)
        {
            if (h.ToString().Length == 1)
                return "0" + h.ToString();
            else
                return h.ToString();
        }

    }
}