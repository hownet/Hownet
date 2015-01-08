using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet.Pay
{
    public partial class frDepositSet : Form
    {
        public frDepositSet()
        {
            InitializeComponent();
        }
        string bllSF = "Hownet.BLL.SysFormula";
        //List<Hownet.Model.SysFormula> li = new List<Hownet.Model.SysFormula>();
        DataTable dtSF = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            if (dtSF.Rows.Count > 0)
            {
                dtSF.Rows[0]["Value1"] = textEdit1.EditValue;
                dtSF.Rows[0]["Value2"] = textEdit2.EditValue;
                dtSF.Rows[0]["Value3"] = Convert.ToInt32(checkEdit1.Checked);
                BasicClass.GetDataSet.UpData(bllSF, dtSF);
            }
            else
            {
                DataRow dr = dtSF.NewRow();
                dr["Value1"] = textEdit1.EditValue;
                dr["Value2"] = textEdit2.EditValue;
                dr["Value3"] = Convert.ToInt32(checkEdit1.Checked);
                dr["TypeID"] = (int)BasicClass.Enums.Formula.押金;
                dr["A"] = 3;
                dr["ID"] = 0;
                 dr["Value4"] = dr["Value5"] = dr["Value6"] = dr["Value7"] = dr["Value8"] = dr["Operator1"] = dr["Operator2"] = dr["Operator3"] = dr["Operator4"] = dr["Operator5"] = dr["Operator6"] = dr["Operator7"] = dr["Operator8"] = string.Empty;
                BasicClass.GetDataSet.Add(bllSF, dtSF);
                this.Close();
            }
        }

        private void frDepositSet_Load(object sender, EventArgs e)
        {
            dtSF = BasicClass.GetDataSet.GetDS(bllSF, "GetList", new object[] { "(TypeID=" + (int)BasicClass.Enums.Formula.押金 + ")" }).Tables[0];
            checkEdit1.Enabled = false;
            if (dtSF.Rows.Count > 0)
            {
                textEdit1.EditValue = dtSF.Rows[0]["Value1"];
                textEdit2.EditValue = dtSF.Rows[0]["Value2"];
                checkEdit1.Checked = (Convert.ToInt32(dtSF.Rows[0]["Value3"]) == 1);
                if (Convert.ToDecimal(textEdit1.EditValue) == 0 && Convert.ToDecimal(textEdit2.EditValue) == 0)
                {
                    checkEdit1.Enabled = true;
                }
            }
            else
            {
                textEdit1.EditValue = textEdit2.EditValue = 0;
                checkEdit1.Checked = false;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            SetViable();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            SetViable();
        }
        private void SetViable()
        {
            try
            {
                if (Convert.ToDecimal(textEdit1.EditValue) == 0 && Convert.ToDecimal(textEdit2.EditValue) == 0)
                {
                    checkEdit1.Enabled = true;
                    checkEdit1.Checked = true;
                }
                else
                {
                    checkEdit1.Enabled = false;
                    checkEdit1.Checked = false;
                }
            }
            catch
            {
                checkEdit1.Enabled = false;
                checkEdit1.Checked = false;
            }
        }
    }
}
