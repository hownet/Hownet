using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Clothing
{
    public partial class frSelectEmp : DevExpress.XtraEditors.XtraForm
    {
        public frSelectEmp()
        {
            InitializeComponent();
        }

        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtEmp = new DataTable();
        DataTable dtWTI = new DataTable();
        int _WTIID = 0;
        int _TaskID = 0;
        int _BoxNum = 0;
        string _WorkingName = string.Empty;
        int _SizeID = 0;
        public frSelectEmp(BasicClass.cResult cr)
            : this()
        {
            r = cr;
        }
        public frSelectEmp(BasicClass.cResult cr, int WTID)
            : this()
        {
            r = cr;
            _WTIID = WTID;
        }
        public frSelectEmp(BasicClass.cResult cr,  int WTID,int TaskID,int BoxNum,string WorkingID,int SizeID)
            : this()
        {
            r = cr;
            _WTIID = WTID;
            _TaskID = TaskID;
            _BoxNum = BoxNum;
           _WorkingName  = WorkingID;
           _SizeID = SizeID;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
            {
                if (Convert.ToInt32(textEdit5.EditValue) < Convert.ToInt32(textEdit4.EditValue))
                {
                    XtraMessageBox.Show("箱号错误！");
                    return;
                }
                if (_WTIID>0&&Convert.ToInt32(textEdit5.EditValue) == _BoxNum && Convert.ToInt32(textEdit4.EditValue) == _BoxNum)
                {
                    if (Convert.ToInt32(dtWTI.Rows[0]["EmployeeID"]) == 0)
                    {
                        dtWTI.Rows[0]["EmployeeID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                        dtWTI.Rows[0]["DateTime"] = BasicClass.GetDataSet.GetDateTime();
                    }
                    dtWTI.Rows[0]["OutAmount"] = Convert.ToInt32(textEdit2.EditValue);
                    dtWTI.Rows[0]["Amount"] = Convert.ToInt32(textEdit3.EditValue);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllWorkTicketInfo, dtWTI);
                    if (!textEdit2.Enabled)
                    {
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllPayInfo, "DelByWTInfoID", new object[] { _WTIID });
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllWorkTicketInfo, "AddPayInfoByID", new object[] { _WTIID });
                    }
                }
                else if(_BoxNum>0)
                {
                    DateTime dtNow = BasicClass.GetDataSet.GetDateTime();
                    for (int i = Convert.ToInt32(textEdit4.EditValue); i < Convert.ToInt32(textEdit5.EditValue) + 1; i++)
                    {
                        _WTIID = Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllWorkTicketInfo, "GetID", new object[] { _TaskID, i, _WorkingName ,_SizeID}));
                        if (_WTIID > 0)
                        {
                            dtWTI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetList", new object[] { "(ID=" + _WTIID + ")" }).Tables[0];
                            dtWTI.Rows[0]["EmployeeID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                            dtWTI.Rows[0]["DateTime"] = dtNow;
                            if (Convert.ToInt32(textEdit2.EditValue) > 0)
                                dtWTI.Rows[0]["OutAmount"] = Convert.ToInt32(textEdit2.EditValue);
                            if (Convert.ToInt32(textEdit3.EditValue) > 0)
                                dtWTI.Rows[0]["Amount"] = Convert.ToInt32(textEdit3.EditValue);
                            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllWorkTicketInfo, dtWTI);
                            //if (!textEdit2.Enabled)
                            //{
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllPayInfo, "DelByWTInfoID", new object[] { _WTIID });
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllWorkTicketInfo, "AddPayInfoByID", new object[] { _WTIID });
                            //}
                        }
                    }
                }
            }
            r.ChangeText(lookUpEdit1.EditValue.ToString()+"+"+lookUpEdit1.Text);
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frSelectEmp_Load(object sender, EventArgs e)
        {
            dtEmp= BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetWorkList", null).Tables[0];
            lookUpEdit1.Properties.DataSource = dtEmp;
            if (_WTIID > 0)
            {
                dtWTI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetList", new object[] { "(ID=" + _WTIID + ")" }).Tables[0];
                if (dtWTI.Rows.Count > 0)
                {
                    lookUpEdit1.EditValue = Convert.ToInt32(dtWTI.Rows[0]["EmployeeID"]);
                    textEdit2.EditValue = dtWTI.Rows[0]["OutAmount"];
                    textEdit3.EditValue = dtWTI.Rows[0]["Amount"];
                    if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
                    {
                        textEdit2.Enabled= textEdit1.Enabled = lookUpEdit1.Enabled = false;
                    }
                }
            }
            if (_BoxNum > 0)
            {
                textEdit4.EditValue = textEdit5.EditValue = _BoxNum;
            }
            else
            {
                textEdit4.Enabled = textEdit5.Enabled = false;
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
             int k = (int)e.KeyChar;
            if (k == 13)
            {
                try
                {
                    Int64 CarID = Convert.ToInt64(textEdit1.EditValue);
                    DataRow[] drs = dtEmp.Select("(IDCardID=" + CarID + ")");
                    if (drs.Length > 0)
                    {
                        lookUpEdit1.EditValue = Convert.ToInt32(drs[0]["ID"]);
                    }
                    else
                    {
                        lookUpEdit1.EditValue = 0;
                    }
                }
                catch
                {
                    lookUpEdit1.EditValue = 0;
                }
                finally
                {
                    textEdit1.EditValue = string.Empty;
                }
            }
        }
    }
}