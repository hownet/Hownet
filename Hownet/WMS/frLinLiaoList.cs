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

namespace Hownet.WMS
{
    public partial class frLinLiaoList : DevExpress.XtraEditors.XtraForm
    {
        public frLinLiaoList()
        {
            InitializeComponent();
        }
        private string bllSB = "Hownet.BLL.StockBack";
        private string bllSBI = "Hownet.BLL.StockBackInfo";
        string bllRL = "Hownet.BLL.RepertoryList";
        string bllSBIL = "Hownet.BLL.StockBackInfoList";
        string bllDep = "Hownet.BLL.Deparment";
        
        int _deparmentTypeID = 0;
        DataTable dtJGC = new DataTable();
        DataTable dtDeparment = new DataTable();
        DataTable dtInList = new DataTable();
        DataTable dtSBIL = new DataTable();
        DataTable dt = new DataTable();
        private void frLinLiaoList_Load(object sender, EventArgs e)
        {
            dateEdit2.EditValue = dateEdit1.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            dtDeparment = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "(ParentID=0)" }).Tables[0];
            dtJGC = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0];
            lookUpEdit1.Properties.DataSource = dtDeparment;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            DataTable dtMat = BasicClass.BaseTableClass.dtMateriel.Copy();
            DataRow dr = dtMat.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtMat.Rows.InsertAt(dr, 0);
            lookUpEdit2.Properties.DataSource = dtMat;
            DataTable dtFinished = BasicClass.BaseTableClass.dtFinished.Copy();
            DataRow drF = dtFinished.NewRow();
            drF["ID"] = 0;
            drF["Name"] = string.Empty;
            dtFinished.Rows.InsertAt(drF, 0);
            lookUpEdit4.Properties.DataSource = dtFinished;

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                lookUpEdit1.Properties.DataSource = dtJGC;
                _deparmentTypeID = 3;
            }
            else
            {
                lookUpEdit1.Properties.DataSource = dtDeparment;
              _deparmentTypeID = 1;
            }
            lookUpEdit1.EditValue = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMilliseconds(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int _Mat = Convert.ToInt32(lookUpEdit2.EditValue);
            int _DepID = Convert.ToInt32(lookUpEdit1.EditValue);
            //if(_DepID==0)
            //{
            //    XtraMessageBox.Show("请选择部门");
            //    return;
            //}
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPackAmount, "GetLinLiaoList", new object[] { dt1, dt2, _DepID, _Mat, _deparmentTypeID,Convert.ToInt32(lookUpEdit3.EditValue) }).Tables[0];
            gridControl2.DataSource = dt;
            if(gridView2.RowCount>0)
            {
                ShowSBIL(Convert.ToInt32(gridView2.GetRowCellValue(0, _coID)));
            }
        }
        private void ShowSBIL(int StockInfoID)
        {
            dtSBIL = BasicClass.GetDataSet.GetDS(bllSBIL, "GetList", new object[] { "(InfoID=" + StockInfoID + ")" }).Tables[0];
            dtSBIL.Columns.Add("BackAmount", typeof(decimal));
            gridControl1.DataSource = dtSBIL;
        }

        /// <summary>
        /// 保存退料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(DialogResult.No==XtraMessageBox.Show("退回物料将转入空闲状态，并且将不能撤消此操作，是否继续？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
            {
                return;
            }
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSBIL.AcceptChanges();
            object o;
            DataTable dtRL = new DataTable();//库存明细
            decimal Amount = 0;//合计退回数量，用于转入空闲库存
            DateTime dtNow = BasicClass.GetDataSet.GetDateTime();
             int _depotID = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coDepotID));
            int _MListID=Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coMListID));
            for(int i=0;i<gridView1.RowCount;i++)
            {
                o = gridView1.GetRowCellValue(i, _coIBackAmount);
                if(o!=DBNull.Value)
                {
                   
                    dtRL = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(ID=" + Convert.ToInt32(gridView1.GetRowCellValue(i, _coIRListID)) + ")" }).Tables[0];
                    if (dtRL.Rows.Count == 1)
                    {
                        dtRL.Rows[0]["NotAmount"] = Convert.ToDecimal(dtRL.Rows[0]["NotAmount"]) + Convert.ToDecimal(gridView1.GetRowCellValue(i, _coIBackAmount));
                        BasicClass.GetDataSet.UpData(bllRL, dtRL);
                        DataTable dtSIL = BasicClass.GetDataSet.GetDS(bllSBIL, "GetList", new object[] { "(ID=" + Convert.ToInt32(gridView1.GetRowCellValue(i, _coIID))+")" }).Tables[0];
                        if (dtSIL.Rows.Count == 1)
                        {
                            dtSIL.Rows[0]["Remark"] = dtSIL.Rows[0]["Remark"].ToString() + dtNow.ToShortDateString() + dtNow.ToShortTimeString() + "退料：" + o;
                            dtSIL.Rows[0]["NowAmount"] = Convert.ToDecimal(dtSIL.Rows[0]["NowAmount"]) - Convert.ToDecimal(o);
                            BasicClass.GetDataSet.UpData(bllSBIL, dtSIL);
                        }
                    }
                    Amount += Convert.ToDecimal(o);
                }
            }
            DataTable dtSI = BasicClass.GetDataSet.GetDS(bllSBI, "GetList", new object[] { "(ID=" + Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coID))+")" }).Tables[0];
            if (dtSI.Rows.Count == 1)
            {
                dtSI.Rows[0]["NotAmount"] = Amount;
                dtSI.Rows[0]["Amount"] = Convert.ToDecimal(dtSI.Rows[0]["Amount"]) - Amount;
                dtSI.Rows[0]["Money"] = Convert.ToDecimal(dtSI.Rows[0]["Amount"]) * Convert.ToDecimal(dtSI.Rows[0]["Price"]);
                dtSI.Rows[0]["Remark"] = dtSI.Rows[0]["Remark"].ToString() + dtNow.ToShortDateString() + dtNow.ToShortTimeString() + "退料：" + Amount;
                BasicClass.GetDataSet.UpData(bllSBI, dtSI);
            }

            DataTable dtRep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { "(MListID=" + _MListID + ") And (PlanID=0) And (DepartmentID=" + _depotID + ")" }).Tables[0];
            if (dtRep.Rows.Count == 1)
            {
                dtRep.Rows[0]["Amount"] = Convert.ToDecimal(dtRep.Rows[0]["Amount"]) + Amount;
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtRep);
            }
            else if(dtRep.Rows.Count==0)
            {
                DataRow dr = dtRep.NewRow();
                dr["A"] = 1;
                dr["ID"] = 0;
                dr["MListID"] = _MListID;
                dr["Amount"] = Amount;
                dr["MeasureID"] = gridView2.GetFocusedRowCellValue(_coMeasureID);
                dr["MaterielID"] = gridView2.GetFocusedRowCellValue(_coMaterielID);
                dr["DepartmentID"] = _depotID;
                dr["SizeID"] = gridView2.GetFocusedRowCellValue(_coSizeID);
                dr["ColorID"] = gridView2.GetFocusedRowCellValue(_coColorID);
                dr["ColorOneID"] = gridView2.GetFocusedRowCellValue(_coColorOneID);
                dr["ColorTwoID"] = gridView2.GetFocusedRowCellValue(_coColorTwoID);
                dr["BrandID"] = 0;
                dr["CompanyID"] = 0;
                dr["PlanID"] = 0;
                dr["Remark"] = string.Empty;
                dr["SupplierID"] = 0;
                dr["SupplierName"] = string.Empty;
                dr["SupplierSN"] = string.Empty;
                dr["SpecID"] = 0;
                dr["SpecName"] = string.Empty;
                dr["MaterielName"] = string.Empty;
                dr["ColorName"] = string.Empty;
                dr["SizeName"] = string.Empty;
                dr["ColorOneName"] = string.Empty;
                dr["ColorTwoName"] = string.Empty;
                dr["BrandName"] = string.Empty;
                dr["ComanyName"] = string.Empty;
                dr["MeasureName"] = string.Empty;
                dr["DeparmentName"] = string.Empty;
                dr["DepotInfoID"] = 0;
                dr["DepotInfoName"] = string.Empty;
                dtRep.Rows.Add(dr);
                BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllRepertory, dtRep);
                
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowSBIL(Convert.ToInt32(gridView2.GetRowCellValue(e.FocusedRowHandle, _coID)));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ShowNum();
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ShowNum();
        }
        private void ShowNum()
        {
            //int _TypeID = 0;

            //int _DeparmentID = Convert.ToInt32(lookUpEdit1.EditValue);
           
            //DataTable dtNum = new DataTable();
            //if(_DeparmentID>0)
            //{
            //    if(radioButton1.Checked)
            //    {
            //        StringBuilder strSql = new StringBuilder();
            //        strSql.Append("SELECT     CONVERT(varchar(100), ProductionPlan.DateTime, 112) + '-' + CAST(ProductionPlan.Num AS varchar) AS Num, ProductionPlan.ID, ");
            //        strSql.Append(" Materiel.Name AS MaterielName FROM         ProductionPlan INNER JOIN Materiel ON ProductionPlan.MaterielID = Materiel.ID INNER JOIN ");
            //        strSql.Append(" StockBack ON ProductionPlan.ID = StockBack.TaskID WHERE     (StockBack.State = 60) ");
            //        if (_deparmentTypeID == 3)
            //            strSql.Append(" AND (StockBack.DeparmentType = " + _deparmentTypeID + ")  ");
            //        else
            //            strSql.Append(" AND (StockBack.DeparmentType <3  )");
            //        strSql.Append(" AND (StockBack.CompanyID = "+_DeparmentID+") GROUP BY CONVERT(varchar(100), ProductionPlan.DateTime, 112) + '-' + CAST(ProductionPlan.Num AS varchar), ");
            //        strSql.Append(" ProductionPlan.ID, Materiel.Name");
            //        dtNum = BasicClass.GetDataSet.GetBySql(strSql.ToString());
            //    }
            //    else
            //    {
            //        StringBuilder strSql = new StringBuilder();
            //        strSql.Append("SELECT     CONVERT(varchar(100), ProductTaskMain.DateTime, 112) + '-' + CAST(ProductTaskMain.Num AS varchar) AS Num, Materiel.Name AS ");
            //        strSql.Append(" MaterielName, ProductTaskMain.ID FROM         Materiel INNER JOIN ProductTaskMain ON Materiel.ID = ProductTaskMain.MaterielID ");
            //        strSql.Append(" INNER JOIN StockBack ON ProductTaskMain.ID = StockBack.TaskID WHERE     (StockBack.State = 26)  ");
            //        if (_deparmentTypeID == 3)
            //        strSql.Append(" AND (StockBack.DeparmentType = " + _deparmentTypeID + ") ");
            //        else
            //            strSql.Append(" AND (StockBack.DeparmentType <3  )");
            //        strSql.Append(" AND (StockBack.CompanyID =  " + _DeparmentID + ") GROUP BY ");
            //        strSql.Append(" CONVERT(varchar(100), ProductTaskMain.DateTime, 112) + '-' + CAST(ProductTaskMain.Num AS varchar), Materiel.Name, ProductTaskMain.ID");
            //        dtNum = BasicClass.GetDataSet.GetBySql(strSql.ToString());
            //    }
            //}
            //lookUpEdit3.Properties.DataSource = dtNum;
            //lookUpEdit3.EditValue = 0;
        }

        private void lookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {
             int _MaterielID = Convert.ToInt32(lookUpEdit4.EditValue);
             if (_MaterielID > 0)
             {
                 StringBuilder strSql = new StringBuilder();
                 strSql.Append("SELECT     CONVERT(varchar(100), ProductionPlan.DateTime, 112) + '-' + CAST(ProductionPlan.Num AS varchar) AS Num, ProductionPlan.ID, ");
                 strSql.Append(" Materiel.Name AS MaterielName FROM         ProductionPlan INNER JOIN Materiel ON ProductionPlan.MaterielID = Materiel.ID where (ProductionPlan.MaterielID=" + _MaterielID + ")");
                DataTable dtNum = BasicClass.GetDataSet.GetBySql(strSql.ToString());
                 lookUpEdit3.Properties.DataSource = dtNum;
                 lookUpEdit3.EditValue = 0;
             }
            else
             {
                 ShowNum();
             }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string filesNmae = dateEdit1.Text + "到" + dateEdit2.Text;
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
                filesNmae += " 部门：" + lookUpEdit1.Text;
            if (Convert.ToInt32( lookUpEdit4 .EditValue) > 0)
                filesNmae += " 款号：" + lookUpEdit4.Text;
            if (Convert.ToInt32(lookUpEdit3.EditValue) > 0)
                filesNmae += " 制单：" + lookUpEdit3.Text;
            filesNmae += "明细记录";
            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", filesNmae);
            if (fileName != "")
            {
                gridView2.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if(gridView2.FocusedRowHandle>-1)
            {
                int State = Convert.ToInt32(gridView2.GetFocusedRowCellValue( _coState));
                if(State==60)
                {
                    Form fr = new Task.frLinLiao(Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coMainID)));
                    fr.ShowDialog();
                }
                else
                {
                    Form fr = new Task.frTaskLinLiao(Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coMainID)));
                    fr.ShowDialog();
                }
            }
        }


    }
}