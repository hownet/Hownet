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
    public partial class frDeparmentOne : DevExpress.XtraEditors.XtraForm
    {
        public frDeparmentOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        DataTable dtCaiPian = new DataTable();
        DataTable dtOld = new DataTable();
        DataTable dtInfo = new DataTable();
        string bll = BasicClass.Bllstr.bllDeparment;
        public frDeparmentOne(BasicClass.cResult cr, int ID, DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
             dtCaiPian = dtOld.Clone();
             lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "部门类型" }).Tables[0];
            if (_ID == 0)
            {
                this.Text = "增加部門名稱";
                _teMeRemark.EditValue = _teSn.EditValue = _teName.EditValue = string.Empty;
                _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
                lookUpEdit1.EditValue = 0;
                dtInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "(ID=0)" }).Tables[0];

            }
            else
            {
                this.Text = "編輯部門名稱";
                dtCaiPian.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teSn.EditValue = dtCaiPian.Rows[0]["Sn"];
                _teName.EditValue = dtCaiPian.Rows[0]["Name"];
                _teMeRemark.EditValue = dtCaiPian.Rows[0]["Remark"];
                lookUpEdit1.EditValue = Convert.ToInt32(dtCaiPian.Rows[0]["TypeID"]);
                dtInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "(ParentID=" + _ID + ")" }).Tables[0];

            }
            DataRow dr = dtInfo.NewRow();
            dr["ID"] = dr["IsEnd"] =dr["CountEmployee"]= 0;
            dr["Name"] = dr["Sn"] = dr["Remark"] = string.Empty;
            dr["ParentID"] = _ID;
            dr["A"] = 3;
            dtInfo.Rows.Add(dr.ItemArray);
            dtInfo.Rows.Add(dr.ItemArray);
            gridControl1.DataSource = dtInfo;
        }
        private bool Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtInfo.AcceptChanges();
            if (_teName.Text.Trim().Length == 0 )
            {
                XtraMessageBox.Show("部门名称不能为空！");
                return false;
            }
            dtCaiPian.Rows.Clear();
            string sqlWhere = " (ID <> " + _ID + ")  And ((Name = '" + _teName.Text.Trim() + "')  OR (Sn='" + _teSn.Text.Trim() + "')) And (ParentID=0 ) ";//OR (EnName = '" + _teEnName.Text.Trim() + "')
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString())>0 && drs[0]["Name"].Equals(_teName.Text.Trim()) && drs[0]["Sn"].Equals(_teSn.Text.Trim()))
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
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToInt32(gridView1.GetRowCellValue(i, _coID)) > 0 && Convert.ToInt32(gridView1.GetRowCellValue(i, _coA)) == 2)
                {
                    DataTable dtttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetIsUse", new object[] { Convert.ToInt32(gridView1.GetRowCellValue(i, _coID)) }).Tables[0];
                    if (dtttt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtttt.Rows[0]["CountUse"]) > Convert.ToInt32(gridView1.GetRowCellValue(i, _coCountEmployee)))
                        {
                            XtraMessageBox.Show(gridView1.GetRowCellDisplayText(i,_coName)+ "现已安排员工人数，大于现设定的容纳人数！");
                            return false;
                        }
                    }
                }
            }
            DataRow dr = dtCaiPian.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Name"] = _teName.Text.Trim();
            dr["Sn"] = _teSn.Text.Trim();
            dr["Remark"] = _teMeRemark.Text.Trim();
            dr["IsEnd"] = 0;
            dr["TypeID"] = Convert.ToInt32(lookUpEdit1.EditValue);
            dtCaiPian.Rows.Add(dr);
            DataTable dtTem = dtInfo.Clone();
            int a = 1;
            if (_ID == 0)
            {
               dr["ID"]=_ID= BasicClass.GetDataSet.Add(bll, dtCaiPian);
               dtOld.Rows.Add(dr.ItemArray);
               
            }
            else
            {
                BasicClass.GetDataSet.UpData(bll, dtCaiPian);
                drs = dtOld.Select("(ID=" + _ID + ")");
                drs[0].ItemArray = dr.ItemArray;
            }
            for (int i = 0; i <dtInfo.Rows.Count; i++)
            {
                if (dtInfo.Rows[i]["Name"].ToString() != string.Empty)
                {
                    a = Convert.ToInt32(dtInfo.Rows[i]["A"]);
                    if (a > 1)
                    {
                        dtTem = dtInfo.Clone();
                        if (a == 2)
                        {
                            dtTem.Rows.Add(dtInfo.Rows[i].ItemArray);
                            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllDeparment, dtTem);
                        }
                        else if (a == 3)
                        {
                            dtInfo.Rows[i]["ParentID"] = _ID;
                            dtTem.Rows.Add(dtInfo.Rows[i].ItemArray);
                            dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllDeparment, dtTem);
                        }
                        dtInfo.Rows[i]["A"] = 1;
                    }
                }
            }
            dtOld.AcceptChanges();
            return true;
        }
        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                lookUpEdit1.EditValue = _ID = 0;
                _teMeRemark.EditValue = _teSn.EditValue = _teName.EditValue = string.Empty;
                dtCaiPian.Rows.Clear();
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
            if (e.Column == _coName && gridView1.GetFocusedRowCellDisplayText(_coSn) == string.Empty)
                gridView1.SetFocusedRowCellValue(_coSn, BasicClass.GetChinese.GetChineseSpell(_teName.Text) + "-" + (e.RowHandle + 1).ToString());
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtInfo.NewRow();
                dr["ID"] = dr["IsEnd"] =dr["CountEmployee"]= 0;
                dr["Name"] = dr["Sn"] = dr["Remark"] = string.Empty;
                dr["ParentID"] = _ID;
                dr["A"] = 3;
                dtInfo.Rows.Add(dr.ItemArray);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < gridView1.RowCount - 1);
        }

    }
}