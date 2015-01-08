using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet
{
    public partial class frEditCardAmount : DevExpress.XtraEditors.XtraForm
    {
        public frEditCardAmount()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.ProduceTaskPhoto";
        string bllSF = "Hownet.BLL.SysFormula";
        string a = string.Empty;
        private void frEditCardAmount_Load(object sender, EventArgs e)
        {
            a = DateTime.Now.Hour.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() == "289232" + a)
            {
                if (Convert.ToInt32(textEdit2.EditValue) > 0)
                {
                    int amount=Convert.ToInt32(textEdit2.EditValue);
                    DataTable dtPTP = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] {"1=0" }).Tables[0];
                    DataRow dr = dtPTP.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["MainID"] = amount * -1;
                    dr["Imagess"] = new byte[0];
                    dr["Remark"] = BasicClass.GetDataSet.GetDateTime();
                    dtPTP.Rows.Add(dr);
                    BasicClass.GetDataSet.Add(bll, dtPTP);

                    DataTable dtTem = BasicClass.GetDataSet.GetDS(bllSF, "GetList", new object[] { "(TypeID=-1)" }).Tables[0];
                    if (dtTem.Rows.Count == 0)
                    {
                        DataRow drr = dtTem.NewRow();
                        drr["A"] = 3;
                        drr["ID"] = 0;
                       
                        drr["TypeID"] = -1;
                        for (int i = 0; i < dtTem.Columns.Count; i++)
                        {
                            if (dtTem.Columns[i].DataType == System.Type.GetType("System.String"))
                            {
                                drr[i] = string.Empty;
                            }
                        }
                        drr["Operator1"] = amount;
                        dtTem.Rows.Add(drr);
                        BasicClass.GetDataSet.Add(bllSF, dtTem);
                    }
                    else
                    {
                        dtTem.Rows[0]["Operator1"] = Convert.ToInt32(dtTem.Rows[0]["Operator1"]) + amount;
                        BasicClass.GetDataSet.UpData(bllSF, dtTem);
                    }
                    this.Close();
                }
            }
            else
            {
                XtraMessageBox.Show("密码错误");
                textEdit1.Text = string.Empty;
            }
        }
    }
}