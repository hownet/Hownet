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
    public partial class frSizeOne : DevExpress.XtraEditors.XtraForm
    {
        public frSizeOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtCaiPian = new DataTable();
        DataTable dtOld = new DataTable();
        DataTable _dtMT = new DataTable();
        string bll = BasicClass.Bllstr.bllSize;
        public frSizeOne(BasicClass.cResult cr, int ID, DataTable dt,DataTable dtMT)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
            _dtMT = dtMT;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
             dtCaiPian = dtOld.Clone();
             _leTypeID.Properties.DataSource = _dtMT;
            if (_ID == 0)
            {
                this.Text = "增加尺碼名稱";
                _teName.EditValue = string.Empty;
                _leTypeID.EditValue = 0;
                _teOrders.EditValue = 0;
            }
            else
            {
                this.Text = "編輯尺碼名稱";
                dtCaiPian.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teName.EditValue = dtCaiPian.Rows[0]["Name"];
                _teOrders.EditValue = dtCaiPian.Rows[0]["Orders"];
                _leTypeID.EditValue = Convert.ToInt32(dtCaiPian.Rows[0]["SizeTypeID"]);
            }
        }
        private bool Save()
        {
            if (_teName.Text.Trim().Length == 0 )
            {
                XtraMessageBox.Show("尺碼名稱不能為空！");
                return false;
            }
            dtCaiPian.Rows.Clear();
            string sqlWhere = " (ID <> " + _ID + ")  And ((Name = '" + _teName.Text.Trim() + "')) ";//OR (EnName = '" + _teEnName.Text.Trim() + "')
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString())>0 && drs[0]["Name"].Equals(_teName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtCaiPian.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(bll, dtCaiPian);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("名稱重複！");
                    return false;
                }
            }
            DataRow dr = dtCaiPian.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["IsEnd"] = 0;
            dr["SizeTypeID"] = _leTypeID.EditValue;
            try
            {
                dr["Orders"] = _teOrders.EditValue;
            }
            catch
            {
                dr["Orders"] = 0;
            }
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
                _teOrders.EditValue = 0;
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