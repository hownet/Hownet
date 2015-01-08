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
    public partial class frMeasureOne : DevExpress.XtraEditors.XtraForm
    {
        public frMeasureOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtMeasure = new DataTable();
        DataTable dtOld = new DataTable();
        string bll = BasicClass.Bllstr.bllMeasure;
        public frMeasureOne(BasicClass.cResult cr, int ID,DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
             dtMeasure = dtOld.Clone();
             DataTable dtType = new DataTable();
             dtType.Columns.Add("ID", typeof(int));
             dtType.Columns.Add("Name", typeof(string));
             dtType.Rows.Add(1, "数值");
             dtType.Rows.Add(2, "长度");
             dtType.Rows.Add(3, "重量");
             lookUpEdit1.Properties.DataSource = dtType;
             lookUpEdit1.EditValue = 0;
             panel1.Visible = false;
            if (_ID == 0)
            {
                this.Text = "添加计量单位";
                _teEnName.EditValue = _teName.EditValue = string.Empty;
                lookUpEdit1.EditValue = 1;
               _teEnName.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
               textEdit1.EditValue = 0;
            }
            else
            {
                this.Text = "编辑计量单位";
                dtMeasure.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teEnName.EditValue = dtMeasure.Rows[0]["Sn"];
                _teName.EditValue = dtMeasure.Rows[0]["Name"];
                lookUpEdit1.EditValue = Convert.ToInt32(dtMeasure.Rows[0]["MeasureTypeID"]);
                textEdit1.EditValue = dtMeasure.Rows[0]["Conversion"];
            }
        }
        private bool Save()
        {
            if (_teName.Text.Trim().Length == 0 || _teEnName.Text.Trim().Length == 0 )
            {
                XtraMessageBox.Show("编号、名称不能为空！");
                return false;
            }
            dtMeasure.Rows.Clear();
            string sqlWhere = " (ID <> " + _ID + ")  And ((Name = '" + _teName.Text.Trim() + "') OR (Sn = '" + _teEnName.Text.Trim() + "')) ";
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString())>0 && drs[0]["Name"].Equals(_teName.Text.Trim()) && drs[0]["Sn"].Equals(_teEnName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtMeasure.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(bll, dtMeasure);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("编号或名称重复！");
                    return false;
                }
            }
            DataRow dr = dtMeasure.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["Sn"] = _teEnName.Text.Trim();
            dr["IsEnd"] = 0;
            dr["MeasureTypeID"] = Convert.ToInt32(lookUpEdit1.EditValue);
            dr["Conversion"] = Convert.ToDecimal(textEdit1.EditValue);
            dtMeasure.Rows.Add(dr);
            if (_ID == 0)
            {
               dr["ID"]= BasicClass.GetDataSet.Add(bll, dtMeasure);
               dtOld.Rows.Add(dr.ItemArray);
            }
            else
            {
                BasicClass.GetDataSet.UpData(bll, dtMeasure);
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
                _teEnName.EditValue = _teName.EditValue =  string.Empty;
                lookUpEdit1.EditValue = 1;
                dtMeasure.Rows.Clear();
              _teEnName.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
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

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            int _MT=Convert.ToInt32(lookUpEdit1.EditValue);
            if(_MT>1)
            {
                panel1.Visible = true;
                if (_MT == 2)
                    labelControl5.Text = "米";
                else if (_MT == 3)
                    labelControl5.Text = "公斤";
            }
            else
            {
                textEdit1.EditValue = 1;
            }
        }

    }
}