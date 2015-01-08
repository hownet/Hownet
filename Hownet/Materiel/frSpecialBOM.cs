using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Materiel
{
    public partial class frSpecialBOM : DevExpress.XtraEditors.XtraForm
    {
        public frSpecialBOM()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _MSIID =0;
        int _MainID = 0;
        int _MaterielID = 0;
        bool _IsCaiC = false;
        bool _IsTogethers = false;
        bool _IsVerify = false;
        string _MaterielName = string.Empty;
        DataTable dtColor = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtMSI = new DataTable();
        DataTable dtMat = new DataTable();
        public frSpecialBOM(BasicClass.cResult cr, int pwiID,string WorkingName,bool IsVerify)
            : this()
        {
            r = cr;
            _MSIID = pwiID;
            _MaterielName = WorkingName;
            _IsVerify = IsVerify;
        }
        private void frSpecial_Load(object sender, EventArgs e)
        {
            DataTable ddddt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructInfo, "GetList", new object[] { "(InfoID=" + _MSIID + ")" }).Tables[0];
            _MainID = Convert.ToInt32(ddddt.Rows[0]["MainID"]);
            _IsCaiC = Convert.ToBoolean(ddddt.Rows[0]["IsCaiC"]);
            _MaterielID = Convert.ToInt32(ddddt.Rows[0]["MaterielID"]);
            _IsTogethers = Convert.ToBoolean(ddddt.Rows[0]["IsTogethers"]);
            labelControl1.Text = _MaterielName + " 所使用的普通物料：";
            _coChildMaterielID.ColumnEdit = BaseForm.RepositoryItem._reNotSpecialWorking;
            dtColor = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllColor, "GetAllList", null).Tables[0];
            DataRow dr = dtColor.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtColor.Rows.Add(dr);
            dtColor.DefaultView.Sort = "ID";
            _reColor.DataSource = dtColor.DefaultView;

            dtCompany = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=1)" }).Tables[0];
            DataRow drr = dtCompany.NewRow();
            drr["ID"] = 0;
            drr["Name"] = string.Empty;
            dtCompany.Rows.Add(drr);
            dtCompany.DefaultView.Sort = "ID";
            _reCompany.DataSource = dtCompany.DefaultView;

            dtMSI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructInfo, "GetList", new object[] { "(MSIID=" + _MSIID + ")" }).Tables[0];
            DataRow ddr = dtMSI.NewRow();
            ddr["MainID"] = -1;
            ddr["MSIID"] = _MSIID;
            ddr["A"] = 3;
            ddr["InfoID"] = ddr["CompanyID"] = ddr["ColorID"] =ddr["Price"] = ddr["Money"] =ddr["ToColorID"]= 0;
            ddr["MainID"] = _MainID * -1;
            ddr["IsCaiC"] = true;
            ddr["ChildMaterielID"] = ddr["UsePartID"] = ddr["DepartmentID"] = ddr["Amount"] = ddr["UsingTypeID"] = ddr["MeasureID"] = 0;
            ddr["MaterielID"] = _MaterielID;
            ddr["IsTogethers"] = _IsTogethers;
            dtMSI.Rows.Add(ddr.ItemArray);
            dtMSI.Rows.Add(ddr.ItemArray);
            gridControl1.DataSource = dtMSI;

            _coChildMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            dtMat = ((DataView)(BaseForm.RepositoryItem._reMateriel.DataSource)).Table;
            gridView1.OptionsBehavior.Editable = !_IsVerify;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(!_IsVerify)
            gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < gridView1.RowCount - 1);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          if (e.Value == null||e.Value==DBNull.Value)
                return;
            if (e.Column == _coChildMaterielID && e.Value != null&&Convert.ToInt32(e.Value)>0)
            {
                for (int i = 0; i < gridView1.RowCount - 2; i++)
                {
                    if (i != e.RowHandle && Convert.ToInt32(gridView1.GetRowCellValue(i, _coChildMaterielID)) == Convert.ToInt32(e.Value))
                    {
                        XtraMessageBox.Show("物料名重复！");
                        gridView1.SetFocusedRowCellValue(_coChildMaterielID, 0);
                        return;
                    }
                }
                object o = dtMat.Select("(ID=" + e.Value + ")")[0]["MeasureID"];
                gridView1.SetRowCellValue(e.RowHandle, _coMeasureID, o);

            }
            if (e.Column == _coChildMaterielID || e.Column == _coColorID || e.Column == _coToColorID||e.Column==_coCompanyID)
            {
                if (e.RowHandle == gridView1.RowCount - 2)
                {
                    DataRow drr = dtMSI.NewRow();
                    for (int i = 0; i < dtMSI.Columns.Count; i++)
                    {
                        drr[i] = 0;
                    }
                    drr["IsTogethers"] = false;
                    drr["IsCaic"] = true;
                    drr["UsingTypeID"] = 1;
                    drr["MaterielID"] = _MaterielID;
                    drr["A"] = 3;
                    dtMSI.Rows.Add(drr.ItemArray);
                }
            }
            if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                gridView1.SetFocusedRowCellValue(_coA, 2);
            if (e.Column != _coMoney)
                CaicMoney();
        }
        private void CaicMoney()
        {
            try
            {
                decimal _amount = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coAmount)) * (1 + Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coWastage)));
                decimal _price = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coPrice));
                gridView1.SetFocusedRowCellValue(_coMoney, (_amount * _price).ToString("n3"));
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtMSI.AcceptChanges();
            if (CheckRepeat())
                return;
            DataTable dtt = dtMSI.Clone();
            int a = 0;
            for (int i = 0; i < dtMSI.Rows.Count - 2; i++)
            {
                a = Convert.ToInt32(dtMSI.Rows[i]["A"]);
                if (a > 1)
                {
                    dtt.Rows.Clear();
                    dtt.Rows.Add(dtMSI.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllMaterielStructInfo, dtt);
                    else if (a == 3)
                        dtMSI.Rows[i]["InfoID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllMaterielStructInfo, dtt);
                    dtMSI.Rows[i]["A"] = 1;
                }
            }
            r.ChangeText("OK");
        }
        private bool CheckRepeat()
        {
            bool t = false;
            for (int i = 0; i < gridView1.RowCount - 3; i++)
            {
                for (int j = i + 1; j < gridView1.RowCount - 2; j++)
                {
                    string aaa = gridView1.GetRowCellValue(i, _coColorID).ToString() + "-" + gridView1.GetRowCellValue(i, _coCompanyID).ToString() + "-" + gridView1.GetRowCellValue(i, _coChildMaterielID).ToString()+"-"+gridView1.GetRowCellValue(i,_coToColorID).ToString();
                    string bb = gridView1.GetRowCellValue(j, _coColorID).ToString() + "-" + gridView1.GetRowCellValue(j, _coCompanyID).ToString() + "-" + gridView1.GetRowCellValue(j, _coChildMaterielID).ToString() + "-" + gridView1.GetRowCellValue(j, _coToColorID).ToString();
                    if (aaa==bb)
                    {
                        XtraMessageBox.Show("不能有重复的记录！");
                        return true;
                    }
                }
            }
            return t;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1 && gridView1.FocusedRowHandle < gridView1.RowCount - 2)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {

                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMaterielStructInfo, "Delete", new object[] { int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()) });
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                        dtMSI.AcceptChanges();
                    }
                }

            }
        }

    }
}