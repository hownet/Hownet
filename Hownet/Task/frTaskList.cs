using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Task
{
    public partial class frTaskList : DevExpress.XtraEditors.XtraForm
    {
        public frTaskList()
        {
            InitializeComponent();
        }
        private int _tableTypeID = (int)BasicClass.Enums.TableType.Task;
        private int _taskID, _colorID, _sizeID,_colorOneID,_colorTwoID;
        private int _isAll = 0;
        private int _MaterielID = 0;
        private int _PWID = 0;
        DataTable dtTaskList = new DataTable();
        DataTable dtIsVerify = new DataTable();
        DateTime dt1 = DateTime.Today;
        DateTime dt2 = DateTime.Today;

        private void frTaskList_Load(object sender, EventArgs e)
        {
            _coColorID.ColumnEdit=_coColorOneID.ColumnEdit=_coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coWorkingID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coCompanyID.ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
            _coTypeID.ColumnEdit = BaseForm.RepositoryItem._reMTID(4);
            _coFillMan.ColumnEdit = BaseForm.RepositoryItem._reUser;
            // _coDeparmentID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            DataTable dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];
            DataTable dtBrand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=5" }).Tables[0];
            DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=1" }).Tables[0];
            DataTable dtDep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "缝制" }).Tables[0];
            dtIsVerify.Columns.Add("Name", typeof(string));
            dtIsVerify.Columns.Add("ID", typeof(int));
            dtIsVerify.Rows.Add("", 0);
            dtIsVerify.Rows.Add("未审核", 1);
            dtIsVerify.Rows.Add("审核中", 2);
            dtIsVerify.Rows.Add("已审核", 3);
            dtIsVerify.Rows.Add("开始生产", 4);
            dtIsVerify.Rows.Add("待确认", 5);
            dtIsVerify.Rows.Add("确认通过", 6);
            dtIsVerify.Rows.Add("合并生产", 7);
            dtIsVerify.Rows.Add("已完成", 9);
            dtIsVerify.Rows.Add("客户取消", 21);
            dtIsVerify.Rows.Add("公司取消", 22);
            lookUpEdit4.Properties.DataSource = repositoryItemLookUpEdit1.DataSource = dtIsVerify;
            _coIsVerify.ColumnEdit = repositoryItemLookUpEdit1;
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
            dateEdit1.EditValue = Convert.ToDateTime(BasicClass.GetDataSet.GetDateTime().Year.ToString() + "-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            lookUpEdit1.EditValue = lookUpEdit2.EditValue = lookUpEdit3.EditValue = 0;
            DataRow drD = dtDep.NewRow();
            drD["ID"] = 0;
            drD["Name"] = string.Empty;
            dtDep.Rows.InsertAt(drD, 0);
            lookUpEdit5.Properties.DataSource = dtDep;
            _coDColorID.ColumnEdit = _coDColorOneID.ColumnEdit = _coDColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coDSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coDMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coDMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
        }
        private void GetList()
        {
            dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int Mat = Convert.ToInt32(lookUpEdit1.EditValue);
            int Brand = Convert.ToInt32(lookUpEdit2.EditValue);
            int CompanyID = Convert.ToInt32(lookUpEdit3.EditValue);
            int IsVerify = Convert.ToInt32(lookUpEdit4.EditValue);
            int DepID = Convert.ToInt32(lookUpEdit5.EditValue);
            string strWhere = " (DateTime>'" + dt1 + "') And (DateTime<'" + dt2 + "')";
            if (Mat > 0)
                strWhere += " And (MaterielID=" + Mat + ")";
            if (Brand > 0)
                strWhere += "And (BrandID=" + Brand + ")";
            if (CompanyID > 0)
                strWhere += "And (CompanyID=" + CompanyID + ")";
            if (IsVerify > 0)
                strWhere += " And (IsVerify=" + IsVerify + ")";
            if (DepID > 0)
                strWhere += " And (DeparmentType<3) And (DeparmentID=" + DepID + ")";
            dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetTaskList", new object[] { _tableTypeID, strWhere }).Tables[0];
            gridControl1.DataSource = dtTaskList;
            if (gridView1.RowCount > 0)
            {
                gridView1.FocusedRowHandle = dtTaskList.Rows.Count - 1;
                _taskID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                if (gridView1.RowCount == 1)
                {
                    ShowGroupColorSize();
                    if (radioGroup1.SelectedIndex == 0)
                        amountList1.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
                    else
                        radioGroup1.SelectedIndex = 0;
                }
            }
        }
        private void ShowGroupColorSize()
        {
            gridControl2.DataSource = null;
            _taskID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());

            if (_taskID > 0)
            {
                DataTable dtCS = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetGroupColorSize", new object[] { _taskID, _tableTypeID }).Tables[0];
                gridControl2.DataSource = dtCS;
                if (gridView2.FocusedRowHandle > -1)
                {
                    ShowEndWorking();
                }
            }

        }
        private void ShowEndWorking()
        {
            try
            {
                _colorID = int.Parse(gridView2.GetFocusedRowCellValue(_coColorID).ToString());
                _sizeID = int.Parse(gridView2.GetFocusedRowCellValue(_coSizeID).ToString());
                _colorOneID = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coColorOneID));
                _colorTwoID = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coColorTwoID));
                DataTable dtW = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetGroupColorSize", new object[] { _taskID, _colorID, _sizeID, _colorOneID, _colorTwoID }).Tables[0];
                gridControl3.DataSource = dtW;
            }
            catch { }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {


                if (_taskID > 0)
                {
                    ShowGroupColorSize();
                    if (radioGroup1.SelectedIndex == 0)
                        amountList1.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
                    else
                        radioGroup1.SelectedIndex = 0;
                    _isAll = 0;
                    DataTable dtDemand = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielDemand", "GetList", new object[] { "(ProduceTaskID=" + _taskID + ") And (TableTypeID=1)" }).Tables[0];
                    gridControl5.DataSource = dtDemand;
                }
                else
                {
                    ShowCS();

                }
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (_taskID > 0)
            {
                ShowEndWorking();
                _isAll = 0;
            }
            else
            {
                ShowSUMEndWorking();
                _isAll = 0;
            }
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                BasicClass.cResult r = new BasicClass.cResult();
                Form fr = new Task.frTaskForm(r, Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)), 1);
                fr.ShowDialog();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                Form fr = new Pay.frToTicket(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)));
                fr.ShowDialog();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                {
                    gridControl3.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetWorkFishByTaskID", new object[] { Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) }).Tables[0];
                    _isAll = 1;
                }
                else
                {
                    _MaterielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID));
                    _PWID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coPWorkingID));
                    gridControl3.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetSUMNotCS", new object[] { dt1, dt2, _MaterielID, _PWID }).Tables[0];
                    _isAll = 1;
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = Hownet.BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coParentID)) > 0)
                {
                    DataTable dtTem = new DataTable();
                    dtTem.Columns.Add("ID", typeof(int));
                    dtTem.Rows.Add(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coParentID)));
                    Form fr = new WMS.frTaskBOM(dtTem);
                    fr.ShowDialog();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!checkEdit1.Checked)
            {
                GetList();
            }
            else
            {
                _taskID = 0;
                GetSumList();
            }
        }
        private void GetSumList()
        {
            dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int MatID = Convert.ToInt32(lookUpEdit1.EditValue);
            dtTaskList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetSumTaskList", new object[] { dt1, dt2, MatID, Convert.ToInt32(lookUpEdit5.EditValue),lookUpEdit5.Text }).Tables[0];
            gridControl1.DataSource = dtTaskList;
            if (dtTaskList.Rows.Count > 0)
            {
                gridView1.FocusedRowHandle = dtTaskList.Rows.Count - 1;
                if (gridView1.RowCount == 1)
                {
                    ShowCS();
                }
            }
        }
        private void ShowCS()
        {
            _MaterielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID));
            _PWID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coPWorkingID));
            DataTable dtCS = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetSumAmount", new object[] { dt1, dt2, _MaterielID, _PWID }).Tables[0];
            gridControl2.DataSource = dtCS;
            amountList1.Open(true, dtCS);
            if (gridView2.FocusedRowHandle > -1)
            {
                ShowSUMEndWorking();
            }
        }
        private void ShowSUMEndWorking()
        {
            try
            {
                _colorID = int.Parse(gridView2.GetFocusedRowCellValue(_coColorID).ToString());
                _sizeID = int.Parse(gridView2.GetFocusedRowCellValue(_coSizeID).ToString());
                _colorOneID = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coColorOneID));
                _colorTwoID = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coColorTwoID));
                DataTable dtW = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetSUMCS", new object[] { dt1, dt2, _MaterielID, _PWID, _colorID, _sizeID,_colorOneID,_colorTwoID }).Tables[0];
                gridControl3.DataSource = dtW;
            }
            catch { }
        }
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                //object o = gridView1.GetFocusedRowCellValue("DeparmentType");
                //DataTable dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetLinLiaoList", new object[] { _taskID, (int)BasicClass.Enums.TableType.TaskLinLiao }).Tables[0];
                //if (dt.Rows.Count == 0)
                //{
                //    if (DialogResult.Yes == XtraMessageBox.Show("本单没有领料记录，是否打开领料单以便新增？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                //    {
                //        if (!BasicClass.BasicFile.IsHavePermissions((int)BasicClass.Enums.Operation.View, "生产领料"))
                //        {
                //            XtraMessageBox.Show("沒有权限！");
                //            return;
                //        }

                //    }
                //}
                //dt.TableName = "dtMain";
                //Form fr = new Task.frLinLiao(dt, Convert.ToInt32(o));
                //fr.ShowDialog();
                //Form fr = new Task.frLinLiaoInfoList(_taskID);
                //fr.ShowDialog();

            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtMain = new DataTable();
            dtMain.TableName = "Main";
            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                dtMain.Columns.Add(gridView1.VisibleColumns[i].FieldName, typeof(string));
            }
            dtMain.Rows.Add(dtMain.NewRow());
            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                dtMain.Rows[0][i] = gridView1.GetFocusedRowCellDisplayText(dtMain.Columns[i].ColumnName);
            }
            dtMain.Columns.Add("Re", typeof(string));
            if (_isAll == 0)
            {
                dtMain.Rows[0]["Re"] = "颜色：" + gridView2.GetFocusedRowCellDisplayText(_coColorID) + "，尺码：" + gridView2.GetFocusedRowCellDisplayText(_coSizeID) + "，数量：" + gridView2.GetFocusedRowCellDisplayText(_coAmount) + "件";
            }
            else
            {
                dtMain.Rows[0]["Re"] = "整单完成情况";
            }
            DataTable dtInfo = new DataTable();
            dtInfo.TableName = "Info";
            for (int i = 0; i < gridView3.VisibleColumns.Count; i++)
            {
                dtInfo.Columns.Add(gridView3.VisibleColumns[i].FieldName, typeof(string));
            }
            for (int i = 0; i < gridView3.RowCount; i++)
            {
                dtInfo.Rows.Add(gridView3.GetRowCellDisplayText(i, _coOrders), gridView3.GetRowCellDisplayText(i, _coWorkingID), gridView3.GetRowCellDisplayText(i, _coEndAmount), gridView3.GetRowCellDisplayText(i, _coNotAmount));
            }
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            ds.Tables.Add(dtMain);
            ds.Tables.Add(dtInfo);
            BaseForm.PrintClass.WorkEnd(ds);
        }

        private void gridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu2(gridView3.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu2(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }
        //已完成
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否确认将本生产计划标识为已完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] { "(ID=" + gridView1.GetFocusedRowCellValue(_coID) + ")" }).Tables[0];
                        dtTem.Rows[0]["IsVerify"] = 9;
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProductTaskMain, dtTem);
                        gridView1.SetFocusedRowCellValue(_coIsVerify, 9);
                    }
                }
            }
        }
        //取消
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否确认将本生产计划标识为取消？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] { "(ID=" + gridView1.GetFocusedRowCellValue(_coID) + ")" }).Tables[0];
                        dtTem.Rows[0]["IsVerify"] = 22;
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProductTaskMain, dtTem);
                        gridView1.SetFocusedRowCellValue(_coIsVerify, 22);
                    }
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string fileName = Hownet.BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amountList1.Open(_taskID, _tableTypeID, true, radioGroup1.SelectedIndex + 1);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                BasicClass.cResult r = new BasicClass.cResult();
                Form fr = new Clothing.frTaskForm(r, Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)), 1);
                fr.ShowDialog();
            }
        }
        //设置进度工序
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form fr = new Task.frScheduleWorking();
            fr.ShowDialog();
        }

        private void gridView4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu3(gridView4.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu3(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell||hi.HitTest==DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow)
            {
                popupMenu3.ShowPopup(Control.MousePosition);
            }
        }
        //刷新
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (_taskID > 0)
            //{
            //    DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetSchedule", new object[] { _taskID });
            //    DataTable dt = ds.Tables[0];
            //    gridView4.Columns.Clear();
            //    gridControl4.DataSource = dt;
            //    gridView4.Columns["款号"].ColumnEdit = BaseForm.RepositoryItem._reProduce;
            //    gridView4.Columns["颜色"].ColumnEdit = BaseForm.RepositoryItem._reColor;
            //    gridView4.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
            //    gridView4.Columns["款号"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //    gridView4.Columns["颜色"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True; ;
            //}
            if (_taskID == 0)
            {
                if (dtTaskList.Rows.Count > 10)
                {
                    if (DialogResult.No == XtraMessageBox.Show("制单数量超过10个，耗时较长，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        return;
                    }
                }
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    DataTable dt = new DataTable();
                    dt.TableName = "dt";
                    dt.Columns.Add("款号", typeof(string));
                    dt.Columns.Add("颜色", typeof(int));
                    dt.Columns.Add("尺码", typeof(int));
                    dt.Columns.Add("未领", typeof(int));
                    DataTable dtOT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "进度工序" }).Tables[0];
                    for (int i = 0; i < dtOT.Rows.Count; i++)
                    {
                        dt.Columns.Add(dtOT.Rows[i]["Name"].ToString(), typeof(int));
                    }
                    dt.Columns.Add("合计", typeof(int));
                    DataSet ds = new DataSet();
                    for (int i = 0; i < dtTaskList.Rows.Count; i++)
                    {
                        ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetSchedule", new object[] { 0, Convert.ToInt32(dtTaskList.Rows[i]["MaterielID"]), dt1, dt2, Convert.ToInt32(dtTaskList.Rows[i]["PWorkingID"]) });
                        if (ds.Tables.Count > 0)
                        {
                            dt.Merge(ds.Tables[0]);
                        }
                    }


                    gridView4.Columns.Clear();
                    gridControl4.DataSource = dt;
                    gridView4.Columns["颜色"].ColumnEdit = BaseForm.RepositoryItem._reColor;
                    gridView4.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
                    for (int i = 2; i < gridView4.Columns.Count; i++)
                    {
                        gridView4.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                finally
                {
                    //fw.Close();
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
            else
            {
                DataRow[] drs = dtTaskList.Select("(IsTicket=1)");
                if (drs.Length > 10)
                {
                    if (DialogResult.No == XtraMessageBox.Show("制单数量超过10个，耗时较长，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        return;
                    }
                }
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    DataTable dt = new DataTable();
                    dt.TableName = "dt";
                    dt.Columns.Add("款号", typeof(string));
                    dt.Columns.Add("颜色", typeof(int));
                    dt.Columns.Add("尺码", typeof(int));
                    dt.Columns.Add("未领", typeof(int));
                    DataTable dtOT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "进度工序" }).Tables[0];
                    for (int i = 0; i < dtOT.Rows.Count; i++)
                    {
                        dt.Columns.Add(dtOT.Rows[i]["Name"].ToString(), typeof(int));
                    }
                    dt.Columns.Add("合计", typeof(int));
                    DataSet ds = new DataSet();
                    for (int i = 0; i < drs.Length; i++)
                    {
                        ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetSchedule", new object[] { Convert.ToInt32(drs[i]["ID"]), 0, dt1, dt2, Convert.ToInt32(drs[i]["PWorkingID"]) });
                        if (ds.Tables.Count > 0)
                        {
                            dt.Merge(ds.Tables[0]);
                        }
                    }


                    gridView4.Columns.Clear();
                    gridControl4.DataSource = dt;
                    gridView4.Columns["颜色"].ColumnEdit = BaseForm.RepositoryItem._reColor;
                    gridView4.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
                    for (int i = 2; i < gridView4.Columns.Count; i++)
                    {
                        gridView4.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                finally
                {
                    //fw.Close();
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
        }
        //导出
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = Hownet.BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", lookUpEdit5.Text + "生产进度表");
            if (fileName != "")
            {
                gridView4.ExportToXls(fileName);
                Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

    }
}