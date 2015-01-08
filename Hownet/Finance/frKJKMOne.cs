using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Finance
{
    public partial class frKJKMOne : DevExpress.XtraEditors.XtraForm
    {
        public frKJKMOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtKM = new DataTable();
        int PID = 0;
        int KMID = 0;
        int SXH = 0;
        public frKJKMOne(BasicClass.cResult cr, DataTable dt, int ID,int kmID)
            : this()
        {
            r = cr;
            dtKM = dt;
            PID = ID;
            KMID = kmID;
        }
        private void frKJKMOne_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            DataRow[] drs = dtKM.Select("(Num=" + PID + ")");
            if (drs.Length > 0)
            {
                int _MT = Convert.ToInt32(drs[0]["MoneyType"]);
                if (_MT == 0)
                {
                    _MT = Convert.ToInt32(drs[0]["Num"]);
                }
                if (_MT == 1)
                    checkBox1.Checked = true;
                else if (_MT == 2)
                    checkBox1.Checked = false;
                if (_MT < 3)
                    checkBox1.Enabled = false;
                if (_MT == 0)
                {
                    _MT = Convert.ToInt32(drs[0]["Num"]);
                }
            }
            try
            {
                if (KMID == 0)
                {
                    this.Text = "增加会计科目";
                    dtKM.DefaultView.RowFilter = "(ParentID=" + PID + ")";
                    if (dtKM.DefaultView.Count == 0)
                    {
                        _te科目编号.Text = PID.ToString() + "001";
                    }
                    else
                    {
                        dtKM.DefaultView.Sort = "Num DESC";
                        _te科目编号.EditValue = Convert.ToInt64(dtKM.DefaultView[0]["Num"]) + 1;
                    }
                    dtKM.DefaultView.RowFilter = "Orders<10000";
                    dtKM.DefaultView.Sort = "Orders DESC";
                    SXH = Convert.ToInt32(dtKM.DefaultView[0]["Orders"]) + 1;
                    dtKM.DefaultView.RowFilter = "";
                    dtKM.DefaultView.Sort = "Orders";
                    textEdit1.Enabled = true;
                }
                else
                {
                    this.Text = "修改会计科目";
                    DataRow[] drss = dtKM.Select("(Num='" + KMID + "')");
                    if (drss.Length > 0)
                    {
                        _te科目编号.Text = KMID.ToString();
                        _te科目名称.Text = drss[0]["Name"].ToString();
                        SXH = Convert.ToInt32(drss[0]["Orders"]);
                        textEdit1.EditValue = drss[0]["Money"];
                        if (Convert.ToInt32(drss[0]["CompanyID"]) > 0)
                            _te科目名称.Enabled = false;
                        _te科目编号.Enabled = false;
                    }
                    textEdit1.Enabled = false;
                    if(Convert.ToInt32(drss[0]["MoneyType"]) >0)
                    checkBox1.Checked = Convert.ToInt32(drss[0]["MoneyType"]) == 1;
                }
                _te上级科目.Text = drs[0]["Name"].ToString();
                
            }
            catch (Exception ex)
            {
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTem = dtKM.Clone();
                if (KMID == 0)
                {
                    DataRow dr = dtTem.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["Orders"] = SXH;
                    dr["Num"] = Convert.ToInt32(_te科目编号.Text.Trim());
                    dr["Nums"] = _te科目编号.Text.Trim();
                    dr["Name"] = _te科目名称.Text.Trim();
                    dr["ParentID"] = PID;
                    dr["Money"] = textEdit1.EditValue;
                    if (checkBox1.Checked)
                        dr["MoneyType"] = 1;
                    else
                        dr["MoneyType"] = 2;
                    dtTem.Rows.Add(dr);
                    dtKM.AcceptChanges();
                    dtTem.Rows[0]["ID"] =KMID= BasicClass.GetDataSet.Add("Hownet.BLL.Bas_KJKM", dtTem);
                    dtKM.Rows.Add(dtTem.Rows[0].ItemArray);
                    SXH += 1;//继续添加，顺序号加1

                    if (PID == 1002)
                    {
                        string bllMI = "Hownet.BLL.MoneyList";
                        DataTable dtMI = BasicClass.GetDataSet.GetDS(bllMI, "GetList", new object[] { "1=2" }).Tables[0];
                        DataRow ddr = dtMI.NewRow();
                        ddr["KJKMID"] =KMID;
                        ddr["DateTime"] =BasicClass.GetDataSet.GetDateTime().Date;
                        ddr["InMoney"] = textEdit1.EditValue;
                        ddr["OutMoney"] = 0;
                        ddr["Money"] = textEdit1.EditValue;
                        ddr["TableID"] = 0;
                        ddr["TypeID"] = 0;
                        ddr["Remark"] ="期初余额";
                        dtMI.Rows.Add(ddr);
                        BasicClass.GetDataSet.Add(bllMI, dtMI);
                        BasicClass.GetDataSet.ExecSql("Hownet.BLL.Bas_KJKM", "UpMoney", new object[] { 1002});
                    }
                }
                else
                {
                    dtTem.Rows.Add(dtKM.Select("(Num=" + KMID + ")")[0].ItemArray);
                    dtTem.Rows[0]["Num"] = Convert.ToInt32(_te科目编号.Text.Trim());
                    dtTem.Rows[0]["Nums"] = _te科目编号.Text.Trim();
                    dtTem.Rows[0]["Name"] = _te科目名称.Text.Trim();
                    dtTem.Rows[0]["Money"] = textEdit1.EditValue;
                    if (checkBox1.Checked)
                        dtTem.Rows[0]["MoneyType"] = 1;
                    else
                        dtTem.Rows[0]["MoneyType"] = 2;
                    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtTem);
                    DataRow[] drss = dtKM.Select("(Num=" + KMID + ")");
                    drss[0].ItemArray = dtTem.Rows[0].ItemArray;
                    dtKM.AcceptChanges();

                    dtKM.DefaultView.RowFilter = "Orders<10000";
                    dtKM.DefaultView.Sort = "Orders DESC";
                    SXH = Convert.ToInt32(dtKM.DefaultView[0]["Orders"]) + 1;//继续添加，顺序号查询最大的加1
                    dtKM.DefaultView.RowFilter = "";
                    dtKM.DefaultView.Sort = "Orders";
                }
                _te科目编号.Text = string.Empty;
                _te科目名称.Text = string.Empty;
                textEdit1.EditValue = 0;
                //this.Text = "增加会计科目";
                simpleButton1.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}