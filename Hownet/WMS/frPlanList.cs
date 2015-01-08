using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace Hownet.WMS
{
    public partial class frPlanList : DevExpress.XtraEditors.XtraForm
    {
        public frPlanList()
        {
            InitializeComponent();
        }
        private int _tableTypeID = (int)BasicClass.Enums.TableType.ProductionPlan;
        private int _taskID, _colorID, _sizeID;
        private int _bom = 0;
        private int _demand = 0;
        private int _size = 0;
        private int _remark = 0;
        private int _bow = 0;
        private int _materielID = 0;
        DataTable dtTaskList = new DataTable();
        DataTable dtIsVerify = new DataTable();
        DataTable dtPP = new DataTable();
        DataTable dtMat = new DataTable();
        private string bllPP = "Hownet.BLL.ProductionPlan";
        private string bllTM = "Hownet.BLL.ProductTaskMain";
        private string fileName = string.Empty;
        private void frTaskList_Load(object sender, EventArgs e)
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coCompanyID.ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
            dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];
            DataTable dtBrand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=5" }).Tables[0];
            DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=1" }).Tables[0];
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
            dateEdit1.EditValue = Convert.ToDateTime("2010-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            lookUpEdit1.EditValue = lookUpEdit2.EditValue = lookUpEdit3.EditValue = lookUpEdit4.EditValue = 0;
            DataTable dtTypeID = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetNumList", new object[] { "(ProductionPlan.TypeID=-1)" }).Tables[0];
            dtTypeID.Rows.Add("合并后的计划", -1);
            repositoryItemLookUpEdit2.DataSource = dtTypeID;
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                 barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Thread oThread = new Thread(new ThreadStart(SetData));
            oThread.Start();
        }
        private void SetData()
        {
            _trUsing.ColumnEdit = BaseForm.RepositoryItem._reUse;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coDMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _deDepID.ColumnEdit = BaseForm.RepositoryItem._reDepartmentType;
            _lePackID.FormName = (int)BasicClass.Enums.TableType.PackingMethod;
        }
        private void GetList()
        {
            DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            int Mat = Convert.ToInt32(lookUpEdit1.EditValue);
            int Brand = Convert.ToInt32(lookUpEdit2.EditValue);
            int CompanyID = Convert.ToInt32(lookUpEdit3.EditValue);
            int IsVerify = Convert.ToInt32(lookUpEdit4.EditValue);
            string strWhere = " (DateTime>'" + dt1 + "') And (DateTime<'" + dt2 + "')";
            if (Mat > 0)
                strWhere += " And (MaterielID=" + Mat + ")";
            if (Brand > 0)
                strWhere += "And (BrandID=" + Brand + ")";
            if(CompanyID>0)
                strWhere += "And (CompanyID=" + CompanyID + ")";
            if (IsVerify > 0)
                strWhere += " And (IsVerify=" + IsVerify + ")";
            dtTaskList = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetPlanList", new object[] { _tableTypeID, strWhere }).Tables[0];
            gridControl1.DataSource = dtTaskList;
            if(_coFillMan.ColumnEdit==null)
            _coFillMan.ColumnEdit = BaseForm.RepositoryItem._reUser;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (radioGroup1.Properties.Items.Count > 4)
            {
                for (int i = 4; i < radioGroup1.Properties.Items.Count; i++)
                {
                    radioGroup1.Properties.Items.RemoveAt(i);
                    i--;
                }
            }
            _taskID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            dtPP = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=" + _taskID + ")" }).Tables[0];
            if (_materielID != Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID)))
            {
                _materielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID));
                pictureEdit1.EditValue = null;
                fileName = string.Empty;
               if (_materielID > 0)
                {
                    DataRow[] drs = dtMat.Select("(ID=" + _materielID + ")");
                    if (drs.Length > 0)
                    {
                        fileName = drs[0]["Image"].ToString().Trim();
                    }
                    if (fileName.Trim() != "")
                    {
                        if (!BasicClass.BasicFile.FileExists("Mini" + fileName))
                            BasicClass.FileUpDown.DownLoad("Mini" + fileName, "Mini" + fileName);
                        pictureEdit1.EditValue = BasicClass.FileUpDown.getPicEditValue("Mini" + fileName);
                    }
                    else
                    {
                        pictureEdit1.EditValue = null;
                    }
                }

            }

            xtraTabControl1.SelectedTabPage = _xtraTask;
            AddRadioGroup();
            if (radioGroup1.SelectedIndex > 0)
            {
                radioGroup1.SelectedIndex = 0;
            }
            else
            {
                amountList2.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
            }
            _bom = _bow = _size = _demand = _remark = 0;
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
            string fileName =Hownet.BaseContranl.BaseFormClass. ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
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
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                {
                    DataTable dtTem = new DataTable();
                    dtTem.Columns.Add("ID", typeof(int));
                    dtTem.Rows.Add(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)));
                    Form fr = new WMS.frTaskBOM(dtTem);
                    fr.ShowDialog();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }
        //标记为已完成
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否确认将本生产计划标识为已完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        DataTable dtTem = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetList", new object[] { "(ID=" + gridView1.GetFocusedRowCellValue(_coID) + ")" }).Tables[0];
                        dtTem.Rows[0]["IsVerify"] = 9;
                        BasicClass.GetDataSet.UpData("Hownet.BLL.ProductionPlan", dtTem);
                        gridView1.SetFocusedRowCellValue(_coIsVerify, 9);
                    }
                }
            }
        }
        //打开合并后的生产计划
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMerge)) > 0)
                {
                    DataTable dtTem = new DataTable();
                    dtTem.Columns.Add("ID", typeof(int));
                    dtTem.Rows.Add(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMerge)));
                    //Form fr = new frTaskBOM(dtTem);
                    //fr.ShowDialog();
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                amountList2.ClearData();
                return;
            }
            if (radioGroup1.SelectedIndex == 0)
            {
                amountList2.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                amountList2.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.完成数量);
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                amountList2.Open(_taskID, _tableTypeID, true, (int)BasicClass.Enums.AmountType.未完成数量);
            }
            else if (radioGroup1.SelectedIndex == 3)
            {
                try
                {
                    amountList2.Open(true, BasicClass.GetDataSet.GetDS(bllPP, "Get2Depot", new object[] { _taskID, (int)BasicClass.Enums.TableType.Task }).Tables[0]);
                }
                catch
                {
                    amountList2.ClearData();
                }
            }
            else
            {
                amountList2.Open(Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value), (int)BasicClass.Enums.TableType.Task, true, (int)BasicClass.Enums.AmountType.原始数量);
            }
        }
        private void AddRadioGroup()
        {
            DataTable dtTask = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ParentID=" + _taskID + ")" }).Tables[0];
            if (dtTask.Rows.Count > 0)
            {
                string _DeparmentName = string.Empty;
                string _Num = string.Empty;
                string _BedNo = string.Empty;
                for (int i = 0; i < dtTask.Rows.Count; i++)
                {
                    _DeparmentName = dtTask.Rows[i]["DeparmentName"].ToString();
                    _Num = Convert.ToDateTime(dtTask.Rows[i]["DateTime"]).ToString("yyyyMMdd") + "-" + dtTask.Rows[i]["Num"].ToString();
                    _BedNo = dtTask.Rows[i]["BedNO"].ToString();
                    this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                    new DevExpress.XtraEditors.Controls.RadioGroupItem(Convert.ToInt32(dtTask.Rows[i]["ID"]), _Num+"-"+_BedNo+"/"+_DeparmentName)});
                }
            }
        }
        //添加裁剪单
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        //查看裁剪单
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroup1.SelectedIndex < 4)
                return;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                int _TID = Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
                BasicClass.cResult r = new BasicClass.cResult();
              //  r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                Form fr = new Task.frTaskForm(r, _TID, 1);
                fr.ShowDialog();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        //查看工序完成情况
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void frPlanList_SizeChanged(object sender, EventArgs e)
        {
            this._lePackID.lenth = new int[] { 65, this.Width - 70 };
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == _xtpBOM && _bom == 0)
            {
                _bom = Convert.ToInt32(dtPP.Rows[0]["BomID"]);
                if (_bom > 0)
                    ShowBomRoot(_bom);
                else
                    _bom = -1;
            }
            else if (e.Page == _xtpNeed && _demand == 0)
            {
                _gcDemand.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetTask", new object[] { _taskID, _tableTypeID }).Tables[0];
                _demand = -1;
            }
            else if(e.Page==_xtpSizeBow&&_bow==0)
            {
                ucSizeBow1.Open(_taskID, _tableTypeID);
                _bow = -1;
            }
            else if (e.Page == _xtraSize && _size == 0)
            {
                ucSizeList1.Open(_materielID, _taskID, true);
                _size = -1;
            }
            else if (e.Page == xtraTabPage2 && _remark == 0)
            {
                _lePackID.editVal = dtPP.Rows[0]["PackingMethodID"];
                _meSewRemark.EditValue = dtPP.Rows[0]["SewingRemark"].ToString();
                _remark = -1;
            }

        }
        /// <summary>
        /// 根据物料结构主表ID显示单件用量树形
        /// </summary>
        /// <param name="ID"></param>
        void ShowBomRoot(int BomID)
        {
            _trBom.Nodes.Clear();
            DataTable dtStruct = new DataTable();
            dtStruct.Columns.Add("MaterielID", typeof(int));
            dtStruct.Columns.Add("MaterielName", typeof(string));
            dtStruct.Columns.Add("MeasureName", typeof(string));
            dtStruct.Columns.Add("Amount", typeof(decimal));
            dtStruct.Columns.Add("Wastage", typeof(decimal));
            dtStruct.Columns.Add("DepartmentName", typeof(string));
            dtStruct.Columns.Add("PartName", typeof(string));
            dtStruct.Columns.Add("UsingTypeID", typeof(string));
            dtStruct.Columns.Add("IsTogethers", typeof(string));
            _trBom.DataSource = null;
            _trBom.DataSource = dtStruct;
            _trBom.AppendNode(new object[] { _materielID, gridView1.GetFocusedRowCellDisplayText(_coMaterielID) }, null);
            DataTable dtBom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructMain, "GetBomListByMainID", new object[] { BomID }).Tables[0];
            for (int i = 0; i < dtBom.Rows.Count; i++)
            {
                _trBom.AppendNode(new object[] {int.Parse( dtBom.Rows[i]["MaterielID"].ToString()),dtBom.Rows[i]["MaterielName"].ToString(),
                    dtBom.Rows[i]["MeasureName"].ToString(),decimal.Parse( dtBom.Rows[i]["Amount"].ToString()),
                    decimal.Parse( dtBom.Rows[i]["Wastage"].ToString()),dtBom.Rows[i]["DepartmentName"].ToString(),
                    dtBom.Rows[i]["PartName"].ToString(),dtBom.Rows[i]["UsingTypeID"],dtBom.Rows[i]["IsTogethers"].ToString()}, _trBom.Nodes[0]);
            }
            for (int j = 0; j < _trBom.Nodes[0].Nodes.Count; j++)
            {
                int temID = int.Parse(_trBom.Nodes[0].Nodes[j].GetValue(_trMaterielID).ToString());
                dtBom.Clear();
                dtBom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructMain, "GetBomListByMateriel", new object[] { temID }).Tables[0];
                for (int i = 0; i < dtBom.Rows.Count; i++)
                {
                    _trBom.AppendNode(new object[] {int.Parse( dtBom.Rows[i]["MaterielID"].ToString()),dtBom.Rows[i]["MaterielName"].ToString(),
                    dtBom.Rows[i]["MeasureName"].ToString(),decimal.Parse( dtBom.Rows[i]["Amount"].ToString()),
                    decimal.Parse( dtBom.Rows[i]["Wastage"].ToString()),dtBom.Rows[i]["DepartmentName"].ToString(),
                    dtBom.Rows[i]["PartName"].ToString(),dtBom.Rows[i]["UsingTypeID"],dtBom.Rows[i]["IsTogethers"].ToString()}, _trBom.Nodes[0].Nodes[j]);
                }
            }
            _trBom.ExpandAll();
            _trBom.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(treeList1_FocusedNodeChanged);
        }
        /// <summary>
        /// 当树形节点获取焦点时，加载下一级，从第二层开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            if (_trBom.FocusedNode != null && _trBom.FocusedNode.Level > 1)
            {
                if (_trBom.FocusedNode.Expanded == false)
                {
                    _trBom.FocusedNode.Nodes.Clear();
                    int id = int.Parse(_trBom.FocusedNode.GetValue("MaterielID").ToString());
                    DataTable dtBom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructMain, "GetBomListByMateriel", new object[] { id }).Tables[0];
                    for (int i = 0; i < dtBom.Rows.Count; i++)
                    {
                        _trBom.AppendNode(new object[] {int.Parse( dtBom.Rows[i]["MaterielID"].ToString()),dtBom.Rows[i]["MaterielName"].ToString(),
                            dtBom.Rows[i]["MeasureName"].ToString(),decimal.Parse( dtBom.Rows[i]["Amount"].ToString()),
                            decimal.Parse( dtBom.Rows[i]["Wastage"].ToString()),dtBom.Rows[i]["DepartmentName"].ToString(),
                            dtBom.Rows[i]["PartName"].ToString(),dtBom.Rows[i]["UsingTypeID"],dtBom.Rows[i]["IsTogethers"].ToString()}, _trBom.FocusedNode);
                    }
                }
                _trBom.FocusedNode.ExpandAll();
            }
        }
        //打印生产计划
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            DataTable dtBomMain = new DataTable();
            dtBomMain.TableName = "Main";
            dtBomMain.Columns.Add("Num", typeof(string));
            dtBomMain.Columns.Add("Materiel", typeof(string));
            dtBomMain.Columns.Add("Date", typeof(DateTime));
            dtBomMain.Columns.Add("MaterielJpg", typeof(byte[]));
            dtBomMain.Columns.Add("Pack", typeof(string));
            dtBomMain.Columns.Add("Remark", typeof(string));
            dtBomMain.Columns.Add("Brand", typeof(string));
            dtBomMain.Columns.Add("CompanyName", typeof(string));
            dtBomMain.Columns.Add("DeparmentName", typeof(string));
            dtBomMain.Columns.Add("LastDate", typeof(string));
            dtBomMain.Columns.Add("Employee", typeof(string));
            string DD = BasicClass.GetDataSet.GetOne(bllPP, "GetDesigners", new object[] { _materielID }).ToString();
            byte[] bbb = new byte[0];
            if (pictureEdit1.EditValue!=null)
                bbb = (byte[])pictureEdit1.EditValue;

            dtBomMain.Rows.Add(gridView1.GetFocusedRowCellDisplayText(_coNum), gridView1.GetFocusedRowCellDisplayText(_coMaterielID), gridView1.GetFocusedRowCellDisplayText(_coDateTime), bbb, _lePackID.valStr, _meSewRemark.EditValue, gridView1.GetFocusedRowCellDisplayText(_coBrandID), gridView1.GetFocusedRowCellDisplayText(_coCompanyID), "", gridView1.GetFocusedRowCellDisplayText(_coLastDate), DD);
            DataTable dtAmount = amountList2.dtDataSource.Copy();
            dtAmount.TableName = "AmountInfo";
            try
            {
                dtAmount.Columns.Add("Columns12", typeof(string));
                dtAmount.Columns.Add("Columns11", typeof(string));
                dtAmount.Columns.Add("Columns10", typeof(string));
                dtAmount.Columns.Add("Columns9", typeof(string));
                dtAmount.Columns.Add("Columns8", typeof(string));
            }
            catch
            { }
            ds.Tables.Add(dtBomMain);
            ds.Tables.Add(dtAmount);
            DataTable dttt = new DataTable();
            dttt.TableName = "dtDemand";
            dttt.Columns.Add("MaterielName", typeof(string));
            dttt.Columns.Add("OneAmount", typeof(string));
            dttt.Columns.Add("MeasureName", typeof(string));
            dttt.Columns.Add("ColorName", typeof(string));
            dttt.Columns.Add("Amount", typeof(double));
            DataTable dtSizeDemand = dttt.Clone();
            dtSizeDemand.TableName = "SizeDemand";
            DataTable dtCSDemand = dttt.Clone();
            dtCSDemand.Columns.Add("ColorOneName", typeof(string));
            dtCSDemand.Columns.Add("ColorTwoName", typeof(string));
            dtCSDemand.Columns.Add("SizeName", typeof(string));
            dtCSDemand.TableName = "CSDemand";
            DataTable dtNoCS = dttt.Clone();
            dtNoCS.TableName = "dtNoCS";
            int _colorID, _colorOneID, _colorTwoID, _sizeID;
            bool t = true;
            for (int i = 0; i < _gvDemand.RowCount; i++)
            {
                _colorID = _colorOneID = _colorTwoID = _sizeID = 0;
                _colorID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coColorID));
                _colorOneID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coColorOneID));
                _colorTwoID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coColorTwoID));
                _sizeID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coSizeID));
                DataRow dr = dttt.NewRow();
                dr[0] = _gvDemand.GetRowCellDisplayText(i, _coMaterielID);
                t = true;
                _trBom.MoveLast();
                int _trCount = _trBom.FocusedNode.Id;
                int a = 0;
                _trBom.MoveFirst();
                while (t)
                {
                    if (_trBom.FocusedNode.GetValue(_trMaterielID).Equals(_gvDemand.GetRowCellValue(i, _coMaterielID)))
                    {
                        dr[1] = Convert.ToDouble(_trBom.FocusedNode.GetValue(_trAmount));
                        break;
                    }
                    if (a == _trCount)
                        break;
                    _trBom.MoveNext();
                    a++;
                }
                if (dr[1].ToString() == string.Empty)
                    dr[1] = 1;
                dr[2] = _gvDemand.GetRowCellDisplayText(i, _coMeasureID);
                dr[4] = Convert.ToDouble(_gvDemand.GetRowCellDisplayText(i, _coAmount));
                if ((_colorID > 0 && _colorOneID == 0 && _colorTwoID == 0 && _sizeID == 0) || (_colorID == 0 && _colorOneID > 0 && _colorTwoID == 0 && _sizeID == 0)
                    || (_colorID == 0 && _colorOneID == 0 && _colorTwoID > 0 && _sizeID == 0))
                {
                    if (_colorID > 0)
                        dr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorID);
                    else if (_colorOneID > 0)
                        dr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorOneID);
                    else if (_colorTwoID > 0)
                        dr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorTwoID);
                    dttt.Rows.Add(dr);
                }
                else if (_colorID == 0 && _colorOneID == 0 && _colorTwoID == 0 && _sizeID > 0)
                {
                    dr[3] = _gvDemand.GetRowCellDisplayText(i, _coSizeID);
                    dtSizeDemand.Rows.Add(dr.ItemArray);
                }
                else if ((_colorID > 0 || _colorOneID > 0 || _colorTwoID > 0) && _sizeID > 0)
                {
                    DataRow drr = dtCSDemand.NewRow();
                    drr[0] = dr[0];
                    drr[1] = dr[1];
                    drr[2] = dr[2];
                    drr[4] = dr[4];
                    drr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorID);
                    drr[5] = _gvDemand.GetRowCellDisplayText(i, _coColorOneID);
                    drr[6] = _gvDemand.GetRowCellDisplayText(i, _coColorTwoID);
                    drr[7] = _gvDemand.GetRowCellDisplayText(i, _coSizeID);
                    dtCSDemand.Rows.Add(drr);
                }
                else if (_colorID == 0 && _colorOneID == 0 && _colorTwoID == 0 && _sizeID == 0)
                {
                    dtNoCS.Rows.Add(dr.ItemArray);
                }
            }
            ds.Tables.Add(dttt);
            ds.Tables.Add(dtSizeDemand);
            ds.Tables.Add(dtCSDemand);
            ds.Tables.Add(ucSizeList1.dtPrint);
            ds.Tables.Add(dtNoCS);
            BaseForm.PrintClass.TaskBom(ds);
        }
        //打印数量表
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}