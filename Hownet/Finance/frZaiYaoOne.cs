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
    public partial class frZaiYaoOne : DevExpress.XtraEditors.XtraForm
    {
        public frZaiYaoOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtKM = new DataTable();
        int PID = 0;
        int KMID = 0;
        int SXH = 0;
        public frZaiYaoOne(BasicClass.cResult cr, DataTable dt, int ID, int kmID)
            : this()
        {
            r = cr;
            dtKM = dt;
            PID = ID;
            KMID = kmID;
        }
        private void frKJKMOne_Load(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drs = dtKM.Select("(ID=" + PID + ")");
                if (KMID == 0)
                {
                    this.Text = "增加项目名称";
                }
                else
                {
                    this.Text = "修改项目名称";
                    
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
                    dr["Name"] = _te科目名称.Text.Trim();
                    dr["ParentID"] = PID;
                    dtTem.Rows.Add(dr);
                    dtKM.AcceptChanges();
                    dtTem.Rows[0]["ID"] =dtTem.Rows[0]["IDS"] = BasicClass.GetDataSet.Add("Hownet.BLL.Bas_LBXM", dtTem);
                    dtKM.Rows.Add(dtTem.Rows[0].ItemArray);
                }
                else
                {
                    dtTem.Rows.Add(dtKM.Select("(ID=" + KMID + ")")[0].ItemArray);
                    dtTem.Rows[0]["Name"] = _te科目名称.Text.Trim();
                    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_LBXM", dtTem);
                    DataRow[] drss = dtKM.Select("(ID=" + KMID + ")");
                    drss[0].ItemArray = dtTem.Rows[0].ItemArray;
                    dtKM.AcceptChanges();

                }
                _te科目名称.Text = string.Empty;
                this.Text = "增加项目名称";
            }
            catch (Exception ex)
            {
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}