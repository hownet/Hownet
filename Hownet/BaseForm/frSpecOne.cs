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
    public partial class frSpecOne : DevExpress.XtraEditors.XtraForm
    {
        public frSpecOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtCaiPian = new DataTable();
        DataTable dtOld = new DataTable();
        string bll = BasicClass.Bllstr.bllSize;
        public frSpecOne(BasicClass.cResult cr, int ID, DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
             dtCaiPian = dtOld.Clone();         

            if (_ID == 0)
            {
                this.Text = "增加规格";
                _teName.EditValue = string.Empty;
                _teOrders.EditValue = 0;
            }
            else
            {
                this.Text = "编辑规格";
                dtCaiPian.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teName.EditValue = dtCaiPian.Rows[0]["Name"];
                _teOrders.EditValue = dtCaiPian.Rows[0]["Remark"];
            }
        }
        private bool Save()
        {
            if (_teName.Text.Trim().Length == 0 )
            {
                XtraMessageBox.Show("规格名称不能为空！");
                return false;
            }
            dtCaiPian.Rows.Clear();
            string sqlWhere = " (ID <> " + _ID + ")  And ((Name = '" + _teName.Text.Trim() + "')) ";//OR (EnName = '" + _teEnName.Text.Trim() + "')
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                if (int.Parse(drs[0]["IsEnd"].ToString()) > 0 && drs[0]["Name"].Equals(_teName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtCaiPian.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(bll, dtCaiPian);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("名称重复！");
                    return false;
                }
            }
            DataRow dr = dtCaiPian.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["IsEnd"] = 0;
                dr["Renark"] = _teOrders.EditValue;

            dtCaiPian.Rows.Add(dr);
            if (_ID == 0)
            {
               dr["ID"]= BasicClass.GetDataSet.Add(bll, dtCaiPian);
               dtOld.Rows.Add(dr.ItemArray);
            }
            else
            {
                BasicClass.GetDataSet.UpData(bll, dtCaiPian);
                drs = dtOld.Select("(ID=" + _ID + ")");
                drs[0].ItemArray = dr.ItemArray;
            }
            dtOld.AcceptChanges();
            return true;
        }
        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                _ID = 0;
                _teName.EditValue = string.Empty;
                _teOrders.EditValue = string.Empty;
                dtCaiPian.Rows.Clear();
                _teName.Focus();
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

    }
}