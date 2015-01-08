using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace Hownet.Sell
{
    public partial class frSalesFormList : DevExpress.XtraEditors.XtraForm
    {
        public frSalesFormList()
        {
            InitializeComponent();
        }
        private int _tableTypeID = (int)BasicClass.Enums.TableType.SalesOne;
        private int _taskID, _colorID, _sizeID;
        DataTable dtTaskList = new DataTable();
        private void frTaskList_Load(object sender, EventArgs e)
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coCompanyID.ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
            _coFillMan.ColumnEdit = BaseForm.RepositoryItem._reUser;
            DataTable dtMat = BasicClass.BaseTableClass.dtFinished;// BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];
            DataTable dtBrand = BasicClass.BaseTableClass.dtBrand;// BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=5" }).Tables[0];
            DataTable dtCom = BasicClass.BaseTableClass.dtCompany;// BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=1" }).Tables[0];
            DataRow drmat = dtMat.NewRow();
            drmat["ID"] = 0;
            drmat["Name"] = string.Empty;
            dtMat.Rows.Add(drmat);
            dtBrand.Rows.Add(drmat.ItemArray);
            DataRow drCom = dtCom.NewRow();
            drCom["ID"] = 0;
            drCom["Name"] = string.Empty;
            dtCom.Rows.Add(drCom);
            dtMat.DefaultView.Sort = dtBrand.DefaultView.Sort = dtCom.DefaultView.Sort = "ID";
            lookUpEdit1.Properties.DataSource = dtMat.DefaultView;
            lookUpEdit2.Properties.DataSource = dtBrand.DefaultView;
            lookUpEdit3.Properties.DataSource = dtCom.DefaultView;
            dateEdit1.EditValue = BasicClass.GetDataSet.GetDateTime().Date.AddYears(-1);
           dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            lookUpEdit1.EditValue = lookUpEdit2.EditValue = lookUpEdit3.EditValue = 0;
        }
        private void GetList()
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int Mat = Convert.ToInt32(lookUpEdit1.EditValue);
            int Brand = Convert.ToInt32(lookUpEdit2.EditValue);
            int CompanyID = Convert.ToInt32(lookUpEdit3.EditValue);
            string strWhere = " (DateTime>'" + dt1 + "') And (DateTime<'" + dt2 + "')";
            if (Mat > 0)
                strWhere += " And (MaterielID=" + Mat + ")";
            if (Brand > 0)
                strWhere += "And (BrandID=" + Brand + ")";
            if(CompanyID>0)
                strWhere += "And (CompanyID=" + CompanyID + ")";
            dtTaskList = BasicClass.GetDataSet.GetDS("Hownet.BLL.SalesOrderInfoList", "GetSalesViewList", new object[] { _tableTypeID, strWhere }).Tables[0];
            gridControl1.DataSource = dtTaskList;
            if (gridView1.RowCount > 0)
            {
                gridView1.FocusedRowHandle = dtTaskList.Rows.Count - 1;
                if (gridView1.RowCount == 1)
                {
                    _taskID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                    amountList1.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _taskID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            amountList1.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
        }


        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }


        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName =BaseContranl .BaseFormClass. ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                {
                    DataTable dtTem = new DataTable();
                    dtTem.Columns.Add("ID", typeof(int));
                    dtTem.Rows.Add(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)));
                    Form fr = new SalesForm(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)));
                    fr.ShowDialog();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataTable dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetList", new object[] { "(SalesOrderInfoID=" + gridView1.GetFocusedRowCellValue(_coID) + ")" }).Tables[0];
                if (dtPP.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtPP.Rows[0]["ID"]) > 0)
                    {
                        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                       // string formName = "WMS.frTaskBOM";
                        if (!BasicClass.BasicFile.IsHavePermissions((int)BasicClass.Enums.Operation.View, "生产计划"))
                        {
                            XtraMessageBox.Show("沒有权限！");
                            return;
                        }
                        //System.Reflection.Assembly assembly = Assembly.Load(formName.Split('.')[0]);// System.Reflection.Assembly.GetExecutingAssembly();
                        //Form f = (Form)assembly.CreateInstance(formName);
                        //Type t = assembly.GetType(formName);
                        //t.InvokeMember("_MainID", BindingFlags.SetField, null, f, new object[] { Convert.ToInt32(dtPP.Rows[0]["ID"]) });
                        //f.ShowDialog();
                        Form fr = new WMS.frTaskBOM(Convert.ToInt32(dtPP.Rows[0]["ID"]));
                        fr.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
               
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的已发货？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    DataTable dtI = BasicClass.GetDataSet.GetDS("Hownet.BLL.SalesOrderInfoList", "GetList", new object[] {"(ID="+gridView1.GetFocusedRowCellValue(_coID)+")" }).Tables[0];
                    dtI.Rows[0]["IsVerify"] = 9;
                    string re = dtI.Rows[0]["Remark"].ToString() + BasicClass.GetDataSet.GetDateTime().ToString() + "发货";
                    dtI.Rows[0]["Remark"] = re;
                    BasicClass.GetDataSet.UpData("Hownet.BLL.SalesOrderInfoList", dtI);
                    gridView1.SetFocusedRowCellValue(_coRemark, re);
                    
                }
            }
        }

    }
}