using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Hownet.BaseContranl;

namespace Hownet.Pay
{
    public partial class FinishedWorkingForm : DevExpress.XtraEditors.XtraForm
    {
        public FinishedWorkingForm()
        {
            InitializeComponent();
        }
        string Permissions = "";
        BindingSource bs = new BindingSource();
        //Hownet.BLL.ProductWorkingInfo bllPWI = new Hownet.BLL.ProductWorkingInfo();
        //Hownet.BLL.ProductWorkingMain bllPWM = new Hownet.BLL.ProductWorkingMain();
        //Hownet.BLL.UserInfo bllUI = new Hownet.BLL.UserInfo();
        //Hownet.BLL.Working bllWork = new Hownet.BLL.Working();
        //Hownet.Model.ProductWorkingMain modPWM = new Hownet.Model.ProductWorkingMain();
        //Hownet.Model.ProductWorkingInfo modPWI = new Hownet.Model.ProductWorkingInfo();
        string bllMa = "Hownet.BLL.Materiel";
        string bllPWI = "Hownet.BLL.ProductWorkingInfo";
        string bllPWM = "Hownet.BLL.ProductWorkingMain";
        string bllW = "Hownet.BLL.Working";
        string bllRemark = "Hownet.BLL.Remark";
        int _mainID = 0;
        int _materielID = 0;
        ArrayList liDel = new ArrayList();
        DataTable dtID = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtWork = new DataTable();
        DataTable dtM = new DataTable();
        private DataTable dtLineOne = new DataTable();
        private DataTable dtLineTwo = new DataTable();
        private DataTable dtPin = new DataTable();
        private DataTable dtMachine = new DataTable();
        private DataTable dtMaPart = new DataTable();
        private DataTable dtZhenBu = new DataTable();
        private DataTable dtZhenJu = new DataTable();
        private DataTable dtZhenKuan = new DataTable();
        int _Orders = 0;
        string per = string.Empty;
        bool t = false;
        bool IsRe = true;
        private void PrlductWorkingForm_Load(object sender, EventArgs e)
        {
            this.barManager1.Form = this;
            //Hownet.BLL.Company bllCom = new Hownet.BLL.Company();
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            ShowData();

            //if (BasicClass.BasicFile.liST[0].IsTicketNotNeedCaic)
            //    _coIsCaiC.Visible = false;
            per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //   simpleButton1.Enabled= _barNew.Enabled = barSubItem1.Enabled = _barSave.Enabled = _barEdit.Enabled = false;
            //}
            gridControl2.MainView = gridView3;
        }
        private void ShowData()
        {
          
                lookUpEdit1.Properties.DisplayMember = _reWork.DisplayMember = checkedListBoxControl1.DisplayMember = "Name";
                lookUpEdit1.Properties.Columns.Clear();
                this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 40, "工序名")});
            
            dtWork = BasicClass.GetDataSet.GetDS(bllW, "GetPWList", new object[] { false }).Tables[0];
            dtM = BasicClass.GetDataSet.GetDS(bllMa, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
            lookUpEdit1.Properties.DataSource = checkedListBoxControl1.DataSource = _reWork.DataSource = dtWork;
            //dtWork.DefaultView.RowFilter = "(IsEnd=0)";
            //lookUpEdit1.Properties.DataSource = checkedListBoxControl1.DataSource = dtWork.DefaultView;
            gridControl1.DataSource = dtM;

            dtLineOne = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetByTypeName", new object[] { "针线" }).Tables[0];
            dtLineTwo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetByTypeName", new object[] { "缝线" }).Tables[0];
            dtMachine = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetByTypeName", new object[] { "车台" }).Tables[0];
            dtPin = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetByTypeName", new object[] { "车针" }).Tables[0];
            dtMaPart = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetByTypeName", new object[] { "针车配件" }).Tables[0];
            _reLineOne.DataSource = dtLineOne;
            _reLineTwo.DataSource = dtLineTwo;
            _reMachine.DataSource = dtMachine;
            _rePin.DataSource = dtPin;
            _reMachinePart.DataSource = dtMaPart;
            _reMateriel.DataSource = BasicClass.BaseTableClass.dtMateriel;
            dtZhenBu = BasicClass.GetDataSet.GetDS(bllRemark, "GetList", new object[] {"(TableTypeID=" + (int)BasicClass.Enums.TableType._针步 + ")" }).Tables[0];
            dtZhenJu = BasicClass.GetDataSet.GetDS(bllRemark, "GetList", new object[] { "(TableTypeID=" + (int)BasicClass.Enums.TableType._针距 + ")" }).Tables[0];
            dtZhenKuan = BasicClass.GetDataSet.GetDS(bllRemark, "GetList", new object[] { "(TableTypeID=" + (int)BasicClass.Enums.TableType._针宽 + ")" }).Tables[0];
            _reZhenBu.DataSource = dtZhenBu;
            _reZhenJu.DataSource = dtZhenJu;
            _reZhenKuan.DataSource = dtZhenKuan;
        }
        void bs_PositionChanged(object sender, EventArgs e)
        {
            showInfo(bs.Position);
        }
        void InMain(int MaterielID)
        {
            dtID = BasicClass.GetDataSet.GetDS(bllPWM, "GetIDList", new object[] { MaterielID,1 }).Tables[0];
            if (dtID.Rows.Count == 0)
                dtID.Rows.Add(dtID.NewRow());
            bs.DataSource = dtID;
            showInfo(0);
        }
        void showInfo(int p)
        {
            t = false;
            _barFrist.Enabled = _barPve.Enabled = _barNext.Enabled = _barLast.Enabled = true;
            if (p == 0)
                _barFrist.Enabled = _barPve.Enabled = false;
            if (p == dtID.Rows.Count - 1)
                _barNext.Enabled = _barLast.Enabled = false;
            if (dtID.DefaultView[p]["ID"].ToString() != "")
            {
                _mainID = int.Parse(dtID.DefaultView[p]["ID"].ToString());
                dtMain = BasicClass.GetDataSet.GetDS(bllPWM, "GetList", new object[] { "(ID=" +_mainID+ ")" }).Tables[0];
                _ltRemark.val = dtMain.Rows[0]["Remark"].ToString();
                checkEdit1.Checked = (bool)(dtMain.Rows[0]["IsDefault"]);
                _barNew.Enabled = true;
            }
            else
            {
                dtMain = BasicClass.GetDataSet.GetDS(bllPWM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtMain.NewRow();
                if (p == 0)
                {
                    dr["IsDefault"] = checkEdit1.Checked = true;
                    dr["Remark"] = _ltRemark.val = gridView1.GetFocusedRowCellValue(_coName).ToString() + "的默认工艺单";
                }
                else
                {
                    dr["IsDefault"] = checkEdit1.Checked = false;
                    dr["Remark"] = _ltRemark.val = gridView1.GetFocusedRowCellValue(_coName).ToString() + "第" + (p + 1).ToString() + "份工艺单";
                }
                dr["MaterielID"] = _materielID;
                //dr["IsVerify"] = 1;
                 dr["TaskID"] = 1;
                 dr["ID"] = _mainID = 0;
                dr["CompanyID"] = 0;
                dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["VerifyMan"] = 0;
                dr["Ver"] = "";
                dr["A"] = 3;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dtMain.Rows.Add(dr);
                _barNew.Enabled = false;
            }
            gridControl2.DataSource = null;
          //  dtInfo =BasicClass.GetDataSet.GetDS( bllPWI,"GetList",new object[]{"(MainID="+dtMain.Rows[0]["ID"]+")"}).Tables[0];
            //DataSet dds = BasicClass.GetDataSet.GetDS(bllPWI, "GetList", new object[] { "(MainID=" + dtMain.Rows[0]["ID"] + ")" });
            DataSet dds = BasicClass.GetDataSet.GetDS(bllPWI, "GetInfoList", new object[] { Convert.ToInt32(dtMain.Rows[0]["ID"])});
            gridControl2.DataSource =dtInfo= dds.Tables[0];
            
            liDel.Clear();
            for (int j = 0; j < dtWork.Rows.Count; j++)
            {
                checkedListBoxControl1.SetItemChecked(j, false);
            }
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                for (int j = 0; j < dtWork.Rows.Count; j++)
                {
                    string gv = dtInfo.Rows[i]["WorkingID"].ToString();
                    string ch = dtWork.Rows[j]["ID"].ToString();
                    bool isTick = (bool)(dtInfo.Rows[i]["IsTicket"]);
                    if (gv == ch && isTick)
                    {
                        checkedListBoxControl1.SetItemChecked(j, true);
                        break;
                    }
                }
            }
            t = true;
            gridControl2.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(gridControl2_ViewRegistered);
        }
        void gridControl2_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            try
            {
            DevExpress.XtraGrid.Views.Grid.GridView TemView = (DevExpress.XtraGrid.Views.Grid.GridView)(e.View);
            TemView.Columns.ColumnByFieldName("PWIID").Visible = false;
           // TemView.Columns.ColumnByFieldName("ID").Visible = false;
          //  TemView.Columns.ColumnByFieldName("MaterielID").Visible = false;
          //  TemView.Columns.ColumnByFieldName("WorkingID").Visible = false;
         //   TemView.Columns.ColumnByFieldName("ColorID").Visible = false;
         //   TemView.Columns.ColumnByFieldName("CompanyID").Visible = false;
            TemView.Columns.ColumnByFieldName("ColorName").Caption = "颜色";
            TemView.Columns.ColumnByFieldName("SizeName").Caption = "尺码";
            TemView.Columns.ColumnByFieldName("CompanyName").Caption = "客户";
            TemView.Columns.ColumnByFieldName("WorkingName").Caption = "工序名";
            TemView.Columns.ColumnByFieldName("Price").Caption = "工价";
            TemView.Columns.ColumnByFieldName("IsCaiC").Caption = "参与统计";
            TemView.Columns.ColumnByFieldName("IsCut").Caption = "参与折扣";
            TemView.Columns.ColumnByFieldName("IsTicket").Caption = "出工票";
            TemView.OptionsBehavior.Editable = false;
            }
            catch { }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coIA)) == 1)
                gridView3.SetFocusedRowCellValue(_coIA, 2);
            if (e.Column == _coIOrders)
            {
                int _o = Convert.ToInt32(e.Value);
                DataRow[] drs;
                if (_o > _Orders)
                {
                    drs = dtInfo.Select("(Orders>" + _Orders + ") And (Orders<=" + _o + ")");
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i]["Orders"] = Convert.ToInt32(drs[i]["Orders"]) - 1;
                            if (Convert.ToInt32(drs[i]["A"]) == 1)
                            drs[i]["A"] = 2;
                        }
                    }
                }
                else if (_o < _Orders)
                {
                    drs = dtInfo.Select("(Orders<" + _Orders + ") And (Orders>=" + _o + ")");
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i]["Orders"] = Convert.ToInt32(drs[i]["Orders"]) + 1;
                            if (Convert.ToInt32(drs[i]["A"]) == 1)
                            drs[i]["A"] = 2;
                        }
                    }
                }

                dtInfo.AcceptChanges();
            }

        }
        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "导出" + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("是否打开刚才导出的文档？", "导出Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "未找到导出的文档", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (!t)
                return;
            string ch = checkedListBoxControl1.GetItemValue(e.Index).ToString();
            if (ch == "-1")
                return;
            if (e.State.ToString() == "Checked")
            {
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    string gv = dtInfo.Rows[i]["WorkingID"].ToString();
                    if (gv == ch)
                    {
                        gridView3.SetRowCellValue(i, _coIA, 2);
                        return;
                    }
                } 
                int o = 1;
                int g = 1;
                if (gridView3.RowCount > 0)
                {
                    o = int.Parse(gridView3.GetRowCellValue(gridView3.RowCount - 1, _coIOrders).ToString()) + 1;
                    //g = int.Parse(gridView3.GetRowCellValue(gridView3.RowCount - 1, _coGroupBy).ToString());
                }

                if (gridView3.FocusedRowHandle > -1)
                {
                    o = Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coIOrders)) + 1;
                    //g = Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coGroupBy));
                }

                DataRow[] drs;
                drs = dtInfo.Select("(Orders>" + _Orders + ") ");//And (Orders>=" + _o + ")");
                if (drs.Length > 0)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        drs[i]["Orders"] = Convert.ToInt32(drs[i]["Orders"]) + 1;
                        if(Convert.ToInt32(drs[i]["A"])==1)
                        drs[i]["A"] = 2;
                    }
                }
                dtInfo.AcceptChanges();


                dtInfo.Rows.Add(dtInfo.NewRow());
                int r = dtInfo.Rows.Count - 1;
                dtInfo.Rows[r]["Orders"] = o;
                dtInfo.Rows[r]["WorkingID"] = checkedListBoxControl1.GetItemValue(e.Index);
                dtInfo.Rows[r]["Price"] = 0;
                dtInfo.Rows[r]["GroupBy"] = g;
                dtInfo.Rows[r]["IsTicket"] = true;
                if (r > 0)
                {
                    int aaa = Convert.ToInt32(dtInfo.Rows[r - 1]["ID"]);
                    if (aaa > 0)
                        dtInfo.Rows[r]["ID"] = -1;
                    else
                        dtInfo.Rows[r]["ID"] = Convert.ToInt32(dtInfo.Rows[r - 1]["ID"]) - 1;
                }
                else
                {
                    dtInfo.Rows[r]["ID"] = -1;
                }
                dtInfo.Rows[r]["A"] = 3;
                dtInfo.Rows[r]["Remark"] = string.Empty;
                dtInfo.Rows[r]["FastTime"] = 0;
                dtInfo.Rows[r]["MainID"] = _mainID;
                dtInfo.Rows[r]["CustOder"] = "";
                dtInfo.Rows[r]["IsCaiC"] = true;
                dtInfo.Rows[r]["IsCut"] = true;
                dtInfo.Rows[r]["CompanyID"] = 0;
                dtInfo.Rows[r]["OneAmount"] = 0;
                dtInfo.Rows[r]["IsCanMove"] = 1;
                dtInfo.Rows[r]["IsSpecial"] = dtWork.Select("(ID=" + checkedListBoxControl1.GetItemValue(e.Index) + ")")[0]["IsSpecial"];
            }
            else
            {
                for (int i = 0; i <gridView3.RowCount; i++)// dtInfo.Rows.Count;
                {
                    string gv = gridView3.GetRowCellValue(i, _coIWorkingID).ToString();// dtInfo.Rows[i]["WorkingID"].ToString();
                    if (gv == ch)
                    {
                        if (Convert.ToInt32( gridView3.GetRowCellValue(i, _coIID)) >0)
                        {
                            t = false;
                            gridView3.SetRowCellValue(i, _coIA, 2);
                        }
                        else
                        {
                            gridView3.DeleteRow(i); //dtInfo.Rows.RemoveAt(i);
                            dtInfo.AcceptChanges();
                        }
                        break;
                    }
                }
            }
            t = true;
            if(gridView3.RowCount>0)
            gridView3.FocusedRowHandle = gridView3.RowCount - 1;
        }
        private void checkedListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                splitContainerControl1.Panel1.Text = "款号：" + gridView1.GetFocusedRowCellValue(_coName).ToString();
                InMain(_materielID);
            }
        }
        #region 记录移动
        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }

        private void _barPve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _barNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }

        private void _barLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }

        private void _barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XtraMessageBox.Show(dtMain.GetChanges().Rows.Count.ToString());
            //XtraMessageBox.Show(dtInfo.GetChanges().Rows.Count.ToString());
            bs.AddNew();
        }
        #endregion




        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gridView3.CloseEditor();
            gridView3.UpdateCurrentRow();
                dtMain.Rows[0]["CompanyID"] =0;
            if (checkEdit1.Checked)
                BasicClass.GetDataSet.ExecSql(bllPWM, "UpDefault", new object[] { _materielID });
            dtMain.Rows[0]["Remark"] = _ltRemark.EditVal;
            if (_mainID > 0)
            {
                dtMain.Rows[0]["TaskID"] = 1;
                BasicClass.GetDataSet.UpData(bllPWM,  dtMain);
            }
            else
                dtMain.Rows[0]["ID"] = _mainID = BasicClass.GetDataSet.Add(bllPWM, dtMain);
            int aa = 0;
            DataTable dtTem=dtInfo.Clone();
            DataTable dtTemInfo = dtInfo.Clone();
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                aa = int.Parse(dtInfo.Rows[i]["A"].ToString());
                if (aa > 1)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dtInfo.Rows[i].ItemArray);
                    dtInfo.Rows[i]["MainID"] = dtTem.Rows[0]["MainID"] = _mainID;
                    if (aa == 3)
                        dtInfo.Rows[i]["ID"] = dtTem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllPWI, dtTem);
                    else if (aa == 2)
                        BasicClass.GetDataSet.UpData(bllPWI, dtTem);
                    dtInfo.Rows[i]["A"] = 1;
                }
            }
            int p = bs.Position;
            InMain(_materielID);
            bs.Position = p;
            if (bs.Position == dtID.Rows.Count - 1)
                showInfo(p);
        }

        private void _barToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", gridView1.GetFocusedRowCellDisplayText(_coName) + "的缝制说明");
            if (fileName != "")
            {
                gridView3.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                dtMain.Rows[0]["IsDefault"] = checkEdit1.Checked;
            }
            catch { }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.GetDataSet.SetDataTable();
            this.Close();
        }

        private void _barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowData();
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.FocusedRowHandle > -1)
            {
                if(DialogResult.Yes==XtraMessageBox.Show("是否真的删除？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
                {
    
                        gridView3.DeleteRow(gridView3.FocusedRowHandle);
                        dtInfo.AcceptChanges();
                    
                }
            }
        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("删除后，已打印工票将不能正常录入，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("删除后，已打印工票将不能正常录入，请再次确认是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (_mainID>0)
                    {
                        if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProductWorkingInfo, "GetPWUseCount", new object[] {_mainID })) > 0)
                        {
                            XtraMessageBox.Show("已有使用该工艺单生成工票，不能删除！");
                            return;
                        }
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingMain, "Delete", new object[] { _mainID});
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "DeleteByMainID", new object[] {_mainID });
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "DeleteByMainID", new object[] { _mainID * -1 });
                    }
                    InMain(_materielID);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtt = new DataTable();
            for (int i = 0; i < gridView3.VisibleColumns.Count; i++)
            {
                dtt.Columns.Add(gridView3.VisibleColumns[i].Caption);
            }
            for (int i = 0; i < gridView3.RowCount; i++)
            {
                DataRow dr = dtt.NewRow();
                for (int j = 0; j < gridView3.VisibleColumns.Count; j++)
                {
                    dr[j] = gridView3.GetRowCellDisplayText(i, gridView3.VisibleColumns[j]);
                }
                dtt.Rows.Add(dr);
            }
            dtt.TableName = "Info";
            DataTable dttt = new DataTable();
            dttt.TableName = "Main";
            dttt.Columns.Add("品名");
            dttt.Columns.Add("款号");
            dttt.Columns.Add("日期");
            dttt.Columns.Add("制表");
            dttt.Columns.Add("审核");
            dttt.Columns.Add("批准");
            int typeid = Convert.ToInt32(dtM.Select("(ID=" + _materielID + ")")[0]["TypeID"]);
            DataTable dtatt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] {"(ID="+typeid+")" }).Tables[0];
            dttt.Rows.Add(dtatt.Rows[0]["Name"].ToString(), gridView1.GetFocusedRowCellDisplayText(_coName), dtMain.Rows[0]["FillDate"]);
            DataSet dsPrint = new DataSet();
            dsPrint.DataSetName = "ds";
            dsPrint.Tables.Add(dtt);
            dsPrint.Tables.Add(dttt);
            BaseForm.PrintClass.PrintFinishedWorking(dsPrint);
        }



        void r_RowChanged(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (_mainID > 0)
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "DeleteByMainID", new object[] { _mainID });
                }
                dtInfo = dt;
                gridControl2.DataSource = dtInfo;
                t = false;
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    dtInfo.Rows[i]["ID"] = 0;
                    dtInfo.Rows[i]["MainID"] = _mainID;
                    dtInfo.Rows[i]["A"] = 3;
                    for (int j = 0; j < dtWork.Rows.Count; j++)
                    {
                        string gv = dtInfo.Rows[i]["WorkingID"].ToString();
                        string ch = dtWork.Rows[j]["ID"].ToString();
                        bool isTick = (bool)(dtInfo.Rows[i]["IsTicket"]);
                        if (gv == ch && isTick)
                        {
                            checkedListBoxControl1.SetItemChecked(j, true);
                            break;
                        }
                    }
                }
                t = true;
            }

        }




        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

        }

        void r_TextChanged(string s)
        {

            showInfo(bs.Position);
        }

        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //_coPrice.OptionsColumn.AllowEdit = false;
            try
            {
                if (gridView3.FocusedRowHandle > -1)
                    _Orders = Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coIOrders));
            }
            catch
            {
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void ProductWorkingForm_Activated(object sender, EventArgs e)
        {
            //if (IsRe)
            //    ShowData();
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                splitContainerControl1.Panel1.Text = "款号：" + gridView1.GetFocusedRowCellValue(_coName).ToString();
                InMain(_materielID);
            }
        }

        private void gridView2_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }


        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coIA)) == 1)
                gridView3.SetFocusedRowCellValue(_coIA, 2);
        }

        private void repositoryItemMemoEdit1_DoubleClick(object sender, EventArgs e)
        {
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.TextChanged+=cr_TextChanged;
            Form fr = new Hownet.BaseForm.frRemark(cr, (int)BasicClass.Enums.TableType._缝制要求);
            fr.ShowDialog();
        }

        private void cr_TextChanged(string s)
        {
            gridView3.SetFocusedRowCellValue(_coIRemark, s);
        }


        private void _reZhenBu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Tag.ToString()=="New")
            {
                BasicClass.cResult crr = new BasicClass.cResult();
                crr.TextChanged += crr_TextChanged;
                Form fr = new Hownet.BaseForm.frRemark(crr, (int)BasicClass.Enums.TableType._针步);
                fr.ShowDialog();
            }
        }
        private void crr_TextChanged(string s)
        {
            try
            {
                dtZhenBu = BasicClass.GetDataSet.GetDS(bllRemark, "GetList", new object[] { "(TableTypeID=" + (int)BasicClass.Enums.TableType._针步 + ")" }).Tables[0];
                _reZhenBu.DataSource = dtZhenBu;
                gridView3.SetFocusedValue(Convert.ToInt32(s));
            }
            catch
            {

            }
        }
        private void crj_TextChanged(string s)
        {
            try
            {
                dtZhenJu = BasicClass.GetDataSet.GetDS(bllRemark, "GetList", new object[] { "(TableTypeID=" + (int)BasicClass.Enums.TableType._针距 + ")" }).Tables[0];
                _reZhenJu.DataSource = dtZhenJu;
                gridView3.SetFocusedValue( Convert.ToInt32(s));
            }
            catch
            {

            }
        }
        private void crk_TextChanged(string s)
        {
            try
            {
                dtZhenKuan = BasicClass.GetDataSet.GetDS(bllRemark, "GetList", new object[] { "(TableTypeID=" + (int)BasicClass.Enums.TableType._针宽 + ")" }).Tables[0];
                _reZhenKuan.DataSource = dtZhenKuan;
                gridView3.SetFocusedValue(Convert.ToInt32(s));
            }
            catch
            {

            }
        }
        private void _reZhenJu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                BasicClass.cResult crj = new BasicClass.cResult();
                crj.TextChanged += crj_TextChanged;
                Form fr = new Hownet.BaseForm.frRemark(crj, (int)BasicClass.Enums.TableType._针距);
                fr.ShowDialog();
            }
        }

        private void _reZhenKuan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                BasicClass.cResult crk = new BasicClass.cResult();
                crk.TextChanged += crk_TextChanged;
                Form fr = new Hownet.BaseForm.frRemark(crk, (int)BasicClass.Enums.TableType._针宽);
                fr.ShowDialog();
            }
        }
    }
}