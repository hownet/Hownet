using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
    public partial class ucSampleMC : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSampleMC()
        {
            InitializeComponent();
        }
        DataTable dtSMC = new DataTable();
        private string bllCA = "Hownet.BLL.SampleColorAmount";
        int _ID = 0, _MainID = 0;
        public void Open(int ID,int MainID)
        {
            _ID = ID;
            _MainID = MainID;
            dtSMC = BasicClass.GetDataSet.GetDS(bllCA, "GetList", new object[] { "(ID=" + _ID + ")" }).Tables[0];
            if(dtSMC.Rows.Count==0)
            {
                DataRow dr = dtSMC.NewRow();
                for(int i=0;i<dtSMC.Columns.Count;i++)
                {
                    if (dtSMC.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = string.Empty;
                    }
                    else if (dtSMC.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }

                }
                dr["ID"] = _ID;
                dr["MainID"] = _MainID;
                dtSMC.Rows.Add(dr);
                
            }
            textEdit11.EditValue = dtSMC.Rows[0]["ColorName"];
            textEdit13.EditValue = dtSMC.Rows[0]["PantoneName"];
            textEdit12.EditValue = dtSMC.Rows[0]["CupName"];
            textEdit14.EditValue = dtSMC.Rows[0]["Amount"];
            textEdit1.EditValue = dtSMC.Rows[0]["Size1Name"];
            textEdit2.EditValue = dtSMC.Rows[0]["Size2Name"];
            textEdit3.EditValue = dtSMC.Rows[0]["Size3Name"];
            textEdit4.EditValue = dtSMC.Rows[0]["Size4Name"];
            textEdit5.EditValue = dtSMC.Rows[0]["Size5Name"];
            if (Convert.ToInt32(dtSMC.Rows[0]["Size1Amount"]) != 0)
                textEdit6.EditValue = dtSMC.Rows[0]["Size1Amount"];
            else
                textEdit6.EditValue = DBNull.Value;

            if (Convert.ToInt32(dtSMC.Rows[0]["Size2Amount"]) != 0)
                textEdit8.EditValue = dtSMC.Rows[0]["Size2Amount"];
            else
                textEdit8.EditValue = DBNull.Value;

            if (Convert.ToInt32(dtSMC.Rows[0]["Size3Amount"]) != 0)
                textEdit7.EditValue = dtSMC.Rows[0]["Size3Amount"];
            else
                textEdit7.EditValue = DBNull.Value;

            if (Convert.ToInt32(dtSMC.Rows[0]["Size4Amount"]) != 0)
                textEdit9.EditValue = dtSMC.Rows[0]["Size4Amount"];
            else
                textEdit9.EditValue = DBNull.Value;

            if (Convert.ToInt32(dtSMC.Rows[0]["Size5Amount"]) != 0)
                textEdit10.EditValue = dtSMC.Rows[0]["Size5Amount"];
            else
                textEdit10.EditValue = DBNull.Value;
        }
        public int GetID()
        {
            return _ID;
        }
        public void Save(int MainID)
        {

            _MainID = MainID;

            dtSMC.Rows[0]["ColorName"] = textEdit11.EditValue;
            dtSMC.Rows[0]["PantoneName"] = textEdit13.EditValue;
            dtSMC.Rows[0]["CupName"] = textEdit12.EditValue;
            dtSMC.Rows[0]["Amount"] = textEdit14.EditValue;
            dtSMC.Rows[0]["Size1Name"] = textEdit1.EditValue;
            dtSMC.Rows[0]["Size2Name"] = textEdit2.EditValue;
            dtSMC.Rows[0]["Size3Name"] = textEdit3.EditValue;
            dtSMC.Rows[0]["Size4Name"] = textEdit4.EditValue;
            dtSMC.Rows[0]["Size5Name"] = textEdit5.EditValue;

            if (textEdit6.EditValue != DBNull.Value)
                dtSMC.Rows[0]["Size1Amount"] = textEdit6.EditValue;
            if (textEdit8.EditValue != DBNull.Value)
                dtSMC.Rows[0]["Size2Amount"] = textEdit8.EditValue;
            if (textEdit7.EditValue != DBNull.Value)
                dtSMC.Rows[0]["Size3Amount"] = textEdit7.EditValue;
            if (textEdit9.EditValue != DBNull.Value)
                dtSMC.Rows[0]["Size4Amount"] = textEdit9.EditValue;
            if (textEdit10.EditValue != DBNull.Value)
                dtSMC.Rows[0]["Size5Amount"] = textEdit10.EditValue;
            dtSMC.Rows[0]["MainID"] = _MainID;
            if (Convert.ToInt32(dtSMC.Rows[0]["ID"]) == 0)
            {
                dtSMC.Rows[0]["ID"] = _ID = BasicClass.GetDataSet.Add(bllCA, dtSMC);
            }
            else
            {
                BasicClass.GetDataSet.UpData(bllCA, dtSMC);
            }

        }
        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            Caic();
        }
        private void Caic()
        {
            int am1 = 0, am2 = 0, am3 = 0, am4 = 0, am5 = 0;
            try{am1 = Convert.ToInt32(textEdit6.EditValue);}
            catch { }

            try { am2 = Convert.ToInt32(textEdit8.EditValue); }
            catch { }

            try { am3 = Convert.ToInt32(textEdit7.EditValue); }
            catch { }

            try { am4 = Convert.ToInt32(textEdit9.EditValue); }
            catch { }

            try { am5 = Convert.ToInt32(textEdit10.EditValue); }
            catch { }
            textEdit14.EditValue = am1 + am2 + am3 + am4 + am5;
        }
    }
}
