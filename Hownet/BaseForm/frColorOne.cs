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
    public partial class frColorOne : DevExpress.XtraEditors.XtraForm
    {
        public frColorOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtColor = new DataTable();
        DataTable dtOld = new DataTable();
        public frColorOne(BasicClass.cResult cr, int ID, DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
            dtColor = dtOld.Clone();
            DataTable dtType = new DataTable();
            dtType.Columns.Add("ID", typeof(int));
            dtType.Columns.Add("Name", typeof(string));
            dtType.Rows.Add(1, "浅色");
            dtType.Rows.Add(2, "中色");
            dtType.Rows.Add(3, "深色");
            lookUpEdit1.Properties.DataSource=dtType;
            if (_ID == 0)
            {
                this.Text = "添加颜色";
                _teSn.EditValue = _teName.EditValue =_teMeRemark.EditValue=  string.Empty;
                colorEdit1.EditValue = lookUpEdit1.EditValue = 0;
                _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                this.Text = "编辑颜色";
                dtColor.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teName.EditValue = dtColor.Rows[0]["Name"];
                _teSn.EditValue = dtColor.Rows[0]["Sn"];
                colorEdit1.EditValue = dtColor.Rows[0]["Value"];
                lookUpEdit1.EditValue = Convert.ToInt32(dtColor.Rows[0]["ColorTypeID"]);
                _teMeRemark.EditValue = dtColor.Rows[0]["Remark"];
            }

        }
        private bool Save()
        {

            if (_teName.Text.Trim().Length == 0 || _teSn.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("颜色名称、色号不能为空！");
                return false;
            }
            dtColor.Rows.Clear();
            string sqlWhere = " ((ID <> " + _ID + ")) And ((Name = '" + _teName.Text.Trim() + "') OR (Sn = '" + _teSn.Text.Trim() + "') ) ";
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString()) > 0 && drs[0]["Sn"].Equals(_teSn.Text.Trim()) && drs[0]["Name"].Equals(_teName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtColor.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllColor, dtColor);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("色号或颜色名称重复！");
                    return false;
                }
            }
            DataRow dr = dtColor.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["Value"] = colorEdit1.EditValue;
            dr["Sn"] = _teSn.Text.Trim();
            dr["IsEnd"] = 0;
            dr["ColorTypeID"] = lookUpEdit1.EditValue;
            dr["Remark"] = _teMeRemark.EditValue;
            dtColor.Rows.Add(dr);
            if (_ID == 0)
            {
                dr["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllColor, dtColor);
                dtOld.Rows.Add(dr.ItemArray);
            }
            else
            {
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllColor, dtColor);
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
                _teSn.EditValue = _teName.EditValue =string.Empty;
                colorEdit1.EditValue = lookUpEdit1.EditValue = 0;
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