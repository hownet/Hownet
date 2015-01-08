using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frRemarkOne : DevExpress.XtraEditors.XtraForm
    {
        public frRemarkOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtBrand = new DataTable();
        string bll = "Hownet.BLL.Remark";
        public frRemarkOne(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _ID = ID;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
            if (_ID == 0)
                this.Text = "增加商標";
            else
                this.Text = "編輯商標";
            _leCompanyID.Properties.DataSource = BasicClass.BaseTableClass.dtTableType;
            dtBrand = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ID=" + _ID + ")" }).Tables[0];
            if (dtBrand.Rows.Count > 0)
            {
                _leCompanyID.EditValue = int.Parse(dtBrand.Rows[0]["TableTypeID"].ToString());
                _teMeRemark.EditValue = dtBrand.Rows[0]["Remarks"];
            }
            else
            {
                 _teMeRemark.EditValue = string.Empty;
            }
        }
        private bool Save()
        {
            int _companyID = 0;
            if (_leCompanyID.EditValue == null)
            {
                _companyID = 0;
            }
            else
            {
                _companyID = int.Parse(_leCompanyID.EditValue.ToString());
            }

            DataTable dtt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ID=0)" }).Tables[0];
            DataRow dr = dtt.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["TableTypeID"] = _companyID;
            dr["Remarks"] = _teMeRemark.EditValue;
            dtt.Rows.Add(dr);
            if (_ID == 0)
                BasicClass.GetDataSet.Add(bll, dtt);
            else
                BasicClass.GetDataSet.UpData(bll, dtt);
            return true;
        }
        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.ChangeText("1");
                _ID = 0;
                  _teMeRemark.EditValue = string.Empty;
                _leCompanyID.EditValue = 0;
            }
        }

        private void _sbSaveAndExit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.ChangeText("1");
                this.Close();
            }
        }

        private void _sbCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否不保存當前處理直接退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                this.Close();
            }
        }

    }
}