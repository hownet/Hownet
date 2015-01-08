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
    public partial class frPackingOne : DevExpress.XtraEditors.XtraForm
    {
        public frPackingOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtBrand = new DataTable();
        string bll = "Hownet.BLL.PackingMethod";
        public frPackingOne(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _ID = ID;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
            if (_ID == 0)
                this.Text = "增加包装方法";
            else
                this.Text = "编辑包装方法";
            dtBrand = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ID=" + _ID + ")" }).Tables[0];
            if (dtBrand.Rows.Count > 0)
            {
                _meName.EditValue = dtBrand.Rows[0]["Name"];
                _teMeRemark.EditValue = dtBrand.Rows[0]["Remark"];
            }
            else
            {
                _meName.EditValue = _teMeRemark.EditValue = string.Empty;
            }
        }
        private bool Save()
        {


            DataTable dtt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ID=0)" }).Tables[0];
            DataRow dr = dtt.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _meName.EditValue.ToString();
            dr["Remark"] = _teMeRemark.EditValue;
            dr["IsEnd"] = 0;
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
                 _meName.EditValue= _teMeRemark.EditValue = string.Empty;
                 _meName.Focus();
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