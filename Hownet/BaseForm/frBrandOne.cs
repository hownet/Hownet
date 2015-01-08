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
    public partial class frBrandOne : DevExpress.XtraEditors.XtraForm
    {
        public frBrandOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtBrand = new DataTable();
        string bll = "Hownet.BLL.Materiel";
        public frBrandOne(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _ID = ID;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
            if (_ID == 0)
                this.Text = "添加商标";
            else
                this.Text = "编辑商标";
            dtBrand = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ID=" + _ID + ")" }).Tables[0];
            if (dtBrand.Rows.Count > 0)
            {
                _teName.EditValue = dtBrand.Rows[0]["Name"];
                _teMeRemark.EditValue = dtBrand.Rows[0]["Remark"];
            }
            else
            {
                 _teName.EditValue = _teMeRemark.EditValue = string.Empty;
            }
        }
        private bool Save()
        {
            int _companyID = 0;
            if (_teName.Text.Trim().Length == 0  )
            {
                XtraMessageBox.Show("商标名称不能為空！");
                return false;
            }
            string sqlWhere = " (ID <> " + _ID + ") And (Name = '" + _teName.Text.Trim() + "') ";
            DataTable dtt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { sqlWhere }).Tables[0];
            if (dtt.Rows.Count > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(dtt.Rows[0]["IsEnd"].ToString())>0 && dtt.Rows[0]["Name"].Equals(_teName.Text.Trim()) )
                {
                    dtt.Rows[0]["IsEnd"] = 0;
                    BasicClass.GetDataSet.UpData(bll, dtt);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("名称名英文名称重复！");
                    return false;
                }
            }
            DataRow dr = dtt.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["MeasureID"] = 1;
            dr["Sn"] = string.Empty;
            dr["AttributeID"] = 5;
            dr["SecondMeasureID"] = dr["Conversion"] = 1;
            dr["Remark"] = _teMeRemark.EditValue;
            dr["IsEnd"] = 0;
            dr["Designers"] = 0;
            dtt.Rows.Add(dr);
            if (_ID == 0)
               dtt.Rows[0]["ID"]= BasicClass.GetDataSet.Add(bll, dtt);
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
                 _teName.EditValue = _teMeRemark.EditValue = string.Empty;
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