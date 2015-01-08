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
    public partial class frEditCard : DevExpress.XtraEditors.XtraForm
    {
        public frEditCard()
        {
            InitializeComponent();
        }
        private int _ticketID, _IDCardNo, _GroupBy, _TaskID;
        private BasicClass.cResult r = new BasicClass.cResult();
        private DataTable dt;
        private string blWTDC = "Hownet.BLL.WorkTicketIDCard";
        public frEditCard(BasicClass.cResult cr, int TicketID, int IDCardNo, int GroupBy, int TaskID,DataTable dtt)
            : this()
        {
            r = cr;
            _ticketID = TicketID;
            _IDCardNo = IDCardNo;
            _GroupBy = GroupBy;
            _TaskID = TaskID;
            dt = dtt;
        }
        private void frEditCard_Load(object sender, EventArgs e)
        {
            labelControl1.Text = "将现在卡号：" + _IDCardNo.ToString() + "  更改为：";
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = (int)e.KeyChar;
            if (k == 13)
            {
                try
                {
                    int aa = Convert.ToInt32(textEdit1.Text);
                    DataTable dttt = BasicClass.GetDataSet.GetDS(blWTDC, "CheckCard", new object[] { aa, _TaskID }).Tables[0];
                    if (dttt.Rows.Count == 0)
                    {
                        DataRow[] drs = dt.Select("(IDCardNo=" + aa + ")");
                        if (drs.Length > 0)
                        {
                            XtraMessageBox.Show("该卡正在本生产单中使用！");
                            textEdit1.Text = DBNull.Value.ToString();
                            return;
                        }
                        UpCard(aa);
                    }
                    else
                    {
                        int c = int.Parse(dttt.Rows[0]["ID"].ToString());
                        if (dttt.Rows.Count > 1)
                        {
                            for (int i = 1; i < dttt.Rows.Count; i++)
                            {
                                BasicClass.GetDataSet.ExecSql(blWTDC, "Delete", new object[] { Convert.ToInt32(dttt.Rows[i]["ID"]) });
                            }
                            dttt = BasicClass.GetDataSet.GetDS(blWTDC, "CheckCard", new object[] { aa, _TaskID }).Tables[0];
                        }
                        if ((bool)(dttt.Rows[0]["FishWork"])||(bool)(dttt.Rows[0]["IsEnd"]))
                        {
                            BasicClass.GetDataSet.ExecSql(blWTDC, "UpdateByID", new object[] { c });
                            UpCard(aa);
                        }
                        else
                        {
                            if (DialogResult.Yes == XtraMessageBox.Show("该卡号在使用中,有工序未完成，是否回收并使用该卡号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                            {
                                if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否回收并使用该卡号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                                {
                                    BasicClass.GetDataSet.ExecSql(blWTDC, "UpdateByID", new object[] { c });
                                    UpCard(aa);
                                    return;
                                }
                            }
                            textEdit1.Text = DBNull.Value.ToString();
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("更新失败!");
                }
            }
        }
        private void UpCard(int aa)
        {
            object o = BasicClass.GetDataSet.GetOne(blWTDC, "UpIDCardNo", new object[] { _ticketID, _GroupBy, aa, _TaskID });
            if (o != null && Convert.ToInt32(o) == 1)
            {
                XtraMessageBox.Show("更新成功!");
                r.ChangeText(textEdit1.Text);
                this.Close();
            }
        }
    }
}