using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hownet.BaseContranl;

namespace Hownet.Task
{
    public partial class frP2PackList : DevExpress.XtraEditors.XtraForm
    {
        public frP2PackList()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dtJGC = new DataTable();
        DataTable dtDep = new DataTable();
        int DTID = 0;
        string ss = string.Empty;
        private void frP2PackList_Load(object sender, EventArgs e)
        {
            dateEdit2.EditValue = dateEdit1.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            _loMateriel.Properties.DataSource = BasicClass.BaseTableClass.dtFinished;
            _coMaterieID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            //_coDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coTaskID.ColumnEdit = BaseForm.RepositoryItem._reTaskNum;
            dtDep=BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "(ParentID=0)" }).Tables[0];
            DataRow dr = dtDep.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtDep.Rows.InsertAt(dr, 0);
            lookUpEdit1.Properties.DataSource = dtDep;

            dtJGC = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0];
            DataRow drj = dtJGC.NewRow();
            drj["ID"] = 0;
            drj["Name"] = string.Empty;
            dtJGC.Rows.InsertAt(drj, 0);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMilliseconds(1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int _Mat = Convert.ToInt32(_loMateriel.EditValue);
            int _DepID = Convert.ToInt32(lookUpEdit1.EditValue);
            if (radioButton1.Checked)
            {
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPackAmount, "GetP2PackList", new object[] { dt1, dt2, _Mat, _DepID,DTID }).Tables[0];
                ss = radioButton1.Text;
            }
            else
            {
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPackAmount, "GetP2DeoptList", new object[] { dt1, dt2, _Mat, _DepID,DTID }).Tables[0];
                ss = radioButton2.Text;
            }
            gridControl2.DataSource = dt;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string filesNmae = dateEdit1.Text + "到" + dateEdit2.Text;
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
                filesNmae += " 部门：" + lookUpEdit1.Text;
            if (Convert.ToInt32(_loMateriel.EditValue) > 0)
                filesNmae += " 款号：" + _loMateriel.Text;
            filesNmae += ss;
            filesNmae += "明细记录";
            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", filesNmae);
            if (fileName != "")
            {
                gridView2.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                DTID = 3;
                lookUpEdit1.Properties.DataSource = dtJGC;
            }
            else
            {
                lookUpEdit1.Properties.DataSource = dtDep;
                DTID = 0;
            }
        }
    }
}