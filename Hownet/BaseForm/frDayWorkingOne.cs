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
    public partial class frDayWorkingOne : DevExpress.XtraEditors.XtraForm
    {
        public frDayWorkingOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtWorking = new DataTable();
        DataTable dtOld = new DataTable();
        public frDayWorkingOne(BasicClass.cResult cr, int ID, DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
            dtWorking = dtOld.Clone();
            if (_ID == 0)
            {
                this.Text = "添加工序";
                _teMeRemark.EditValue = _teSn.EditValue = _teName.EditValue = string.Empty;
               _teSn.Text = "D" + (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
               textEdit1.EditValue = 0;
            }
            else
            {
                this.Text = "编辑工序";
                dtWorking.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teName.EditValue = dtWorking.Rows[0]["Name"];
                _teSn.EditValue = dtWorking.Rows[0]["Sn"];
                _teMeRemark.EditValue =   dtWorking.Rows[0]["Remark"];
                textEdit1.EditValue = Convert.ToDecimal(dtWorking.Rows[0]["Price"]);
            }

        }
        private bool Save()
        {

            if (_teName.Text.Trim().Length == 0 || _teSn.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("工序名称、编号不能为空！");
                return false;
            }
            dtWorking.Rows.Clear();
            string sqlWhere = " ((ID <> " + _ID + ")) And ((Name = '" + _teName.Text.Trim() + "') OR (Sn = '" + _teSn.Text.Trim() + "') ) ";
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString()) > 0 && drs[0]["Sn"].Equals(_teSn.Text.Trim()) && drs[0]["Name"].Equals(_teName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtWorking.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllColor, dtWorking);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("编号或工序名称重复！");
                    return false;
                }
            }
            DataRow dr = dtWorking.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["IsSpecial"] = false;
            dr["Sn"] = _teSn.Text.Trim();
            dr["IsEnd"] = 0;
            dr["WorkTypeID"] = -1;
            dr["Remark"] = _teMeRemark.EditValue;
            dr["Price"] = Convert.ToDecimal(textEdit1.EditValue);
            dtWorking.Rows.Add(dr);
            if (_ID == 0)
            {
                dr["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllWorking, dtWorking);
                dtOld.Rows.Add(dr.ItemArray);
            }
            else
            {
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllWorking, dtWorking);
                drs = dtOld.Select("(ID=" + _ID + ")");
                drs[0].ItemArray = dr.ItemArray;
            }
            return true;
        }
        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                _ID = 0;
                _teMeRemark.EditValue = _teSn.EditValue = _teName.EditValue = string.Empty;
             textEdit1.EditValue = 0;
               _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
            }
        }

        private void _sbSaveAndExit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
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

        private void _teName_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}