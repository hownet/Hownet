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
    public partial class frPrintCard : DevExpress.XtraEditors.XtraForm
    {
        public frPrintCard()
        {
            InitializeComponent();
        }
        private int _colorID, _sizeID, _boxID, _groupID,_taskID,_ticketID,_oneAmount;
        private string _num = string.Empty;
        public frPrintCard(int ColorID, int SizeID, int BoxID, int GroupID,int TaskID,int TicketID,string Num,int OneAmount)
            : this()
        {
            _colorID = ColorID;
            _sizeID = SizeID;
            _boxID = BoxID;
            _groupID = GroupID;
            _taskID = TaskID;
            _ticketID = TicketID;
            _num = Num;
            _oneAmount = OneAmount;
        }
        private void frPrintCard_Load(object sender, EventArgs e)
        {
           // checkEdit2.Checked = true;
            radioGroup1.Enabled = radioGroup2.Enabled = radioGroup3.Enabled = true;
            radioGroup1.SelectedIndex = radioGroup2.SelectedIndex = radioGroup3.SelectedIndex = 1;
            textEdit1.EditValue = 0;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
          checkEdit2.Enabled=  radioGroup1.Enabled = radioGroup2.Enabled = radioGroup3.Enabled = !checkEdit1.Checked;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //int TaskID, int TicketID, int GroupBy, int ColorID, int SizeID)
            int a = 0;
            try
            {
                a = Convert.ToInt32(textEdit1.Text);
            }
            catch { }
            int _tiID = _ticketID;
            int _coID = _colorID;
            int _siID = _sizeID;
            int _grID = _groupID;
            if (!checkEdit1.Checked && !checkEdit2.Checked)
            {
                _tiID = 0;
                if (radioGroup1.SelectedIndex == 1)
                    _coID = 0;
                if (radioGroup2.SelectedIndex == 1)
                    _siID = 0;
                if (radioGroup3.SelectedIndex == 1)
                    _grID = 0;
            }
            if (checkEdit2.Checked)
                _grID = 0;
            DataTable dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.WorkTicketIDCard", "PrintLable", new object[] { _taskID, _tiID, _grID, _coID, _siID,_oneAmount }).Tables[0];
            DataTable dtTem = BasicClass.GetDataSet.GetBySql("Select Value From  OtherType Where  (Name='分组显示为部位')");

            string bll = "Hownet.BLL.CaiPian";
            DataTable dtCaiPian = new DataTable();
            bool _IsUserCaiPian = false;
            if (dtTem.Rows.Count > 0)
            {
                _IsUserCaiPian = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
                dtCaiPian = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            }
            
            
            dt.Columns.Add("Num", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Num"] = _num+"-"+dt.Rows[i]["BN"];
            }
            if(_IsUserCaiPian)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        dt.Rows[i]["GroupBy"] = dtCaiPian.Select("(ID=" + dt.Rows[i]["CaiPian"] + ")")[0]["Name"].ToString();
                    }
                    catch
                    {

                    }
                }
            }
            dt.TableName = "Labs";
            if (a > 1)
            {
                while (a > 44)
                    a -= 45;
                for (int i = 0; i < a; i++)
                {
                    //dtt.Rows.Add(dtt.NewRow());
                    DataRow dr = dt.NewRow();
                    //for (int j = 0; j < dt.Columns.Count; j++)
                    //{
                    //    dr[j] = DBNull.Value;
                    //}
                    dt.Rows.InsertAt(dr, 0);
                }
            }
            BaseForm.PrintClass.PrintLabes(dt);
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            radioGroup1.Enabled = radioGroup2.Enabled = radioGroup3.Enabled = !checkEdit2.Checked;
            if (checkEdit2.Checked)
                checkEdit1.Checked = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //int TaskID, int TicketID, int GroupBy, int ColorID, int SizeID)
            int a = 0;
            try
            {
                a = Convert.ToInt32(textEdit1.Text);
            }
            catch { }
            int _tiID = _ticketID;
            int _coID = _colorID;
            int _siID = _sizeID;
            int _grID = _groupID;
            if (!checkEdit1.Checked && !checkEdit2.Checked)
            {
                _tiID = 0;
                if (radioGroup1.SelectedIndex == 1)
                    _coID = 0;
                if (radioGroup2.SelectedIndex == 1)
                    _siID = 0;
                if (radioGroup3.SelectedIndex == 1)
                    _grID = 0;
            }
            if (checkEdit2.Checked)
                _grID = 0;
            DataTable dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.WorkTicketIDCard", "PrintLable", new object[] { _taskID, _tiID, _grID, _coID, _siID, _oneAmount }).Tables[0];
            dt.Columns.Add("Num", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Num"] = _num + "-" + dt.Rows[i]["BN"];
            }
            dt.TableName = "Labs";
            if (a > 1)
            {
                while (a > 44)
                    a -= 45;
                for (int i = 0; i < a; i++)
                {
                    //dtt.Rows.Add(dtt.NewRow());
                    DataRow dr = dt.NewRow();
                    //for (int j = 0; j < dt.Columns.Count; j++)
                    //{
                    //    dr[j] = DBNull.Value;
                    //}
                    dt.Rows.InsertAt(dr, 0);
                }
            }
            BaseForm.PrintClass.PrintBigLabes(dt);
        }
    }
}