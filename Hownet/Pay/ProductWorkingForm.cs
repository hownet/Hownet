using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Hownet.Pay
{
    public partial class ProductWorkingForm : DevExpress.XtraEditors.XtraForm
    {
        public ProductWorkingForm()
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
        string bllOT = "Hownet.BLL.OtherType";
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
        int _Orders = 0;
        string per = string.Empty;
        bool t = false;
        bool IsRe = true;
        bool _IsUserCaiPian = false;
        private void PrlductWorkingForm_Load(object sender, EventArgs e)
        {
            this.barManager1.Form = this;
            //Hownet.BLL.Company bllCom = new Hownet.BLL.Company();
            checkBox1.Checked = false;
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            ShowData();
            if (!BasicClass.BasicFile.liST[0].CustOder)
                _coCustOder.Visible = false;
            //if (BasicClass.BasicFile.liST[0].IsTicketNotNeedCaic)
            //    _coIsCaiC.Visible = false;
            per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _coPrice.Visible = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Print).ToString()) == -1)
                _coIsCaiC.Visible = false;
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //   simpleButton1.Enabled= _barNew.Enabled = barSubItem1.Enabled = _barSave.Enabled = _barEdit.Enabled = false;
            //}
        }
        private void ShowData()
        {
            if (checkBox1.Checked)
            {
                lookUpEdit1.Properties.DisplayMember = _reWork.DisplayMember = checkedListBoxControl1.DisplayMember = "SnName";
                lookUpEdit1.Properties.Columns.Clear();
                this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SnName", 40, "工序名")});
            }
            else
            {
                lookUpEdit1.Properties.DisplayMember = _reWork.DisplayMember = checkedListBoxControl1.DisplayMember = "Name";
                lookUpEdit1.Properties.Columns.Clear();
                this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 40, "工序名")});
            }
            dtWork = BasicClass.GetDataSet.GetDS(bllW, "GetPWList", new object[] { checkBox1.Checked }).Tables[0];
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
            DataTable dtTem = BasicClass.GetDataSet.GetBySql("Select Value From  OtherType Where  (Name='分组显示为部位')");
            if (dtTem.Rows.Count > 0)
            {
                _IsUserCaiPian = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
            }
            checkEdit2.Checked = _IsUserCaiPian;
        }
        void bs_PositionChanged(object sender, EventArgs e)
        {
            showInfo(bs.Position);
        }
        void InMain(int MaterielID)
        {
            dtID = BasicClass.GetDataSet.GetDS(bllPWM, "GetIDList", new object[] { MaterielID,0 }).Tables[0];
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
                 dr["TaskID"] = 0;
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
          //  checkEdit2.Checked = Convert.ToInt32(dtMain.Rows[0]["CompanyID"]) != 0;
            simpleButton2.Visible = !checkEdit1.Checked;
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
            checkBox2.Checked = false;
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
            if (e.RowHandle>-1&&e.Column != _coA && gridView2.GetFocusedRowCellValue(_coA).ToString() == "1")
                gridView2.SetFocusedRowCellValue(_coA, 2);
            if (e.Column == _coOrders)
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
                        gridView2.SetRowCellValue(i, _coIsTicket, true);
                        gridView2.SetRowCellValue(i, _coA, 2);
                        return;
                    }
                } 
                int o = 1;
                int g = 1;
                if (gridView2.RowCount > 0)
                {
                    o = int.Parse(gridView2.GetRowCellValue(gridView2.RowCount - 1, _coOrders).ToString()) + 1;
                    g = int.Parse(gridView2.GetRowCellValue(gridView2.RowCount - 1, _coGroupBy).ToString());
                }

                if (gridView2.FocusedRowHandle > -1)
                {
                    o = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coOrders)) + 1;
                    g = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coGroupBy));
                }
                //    if (gridView2.FocusedRowHandle < gridView2.RowCount - 1)
                //    {
                //        for (int i = gridView2.FocusedRowHandle + 1; i < gridView2.RowCount; i++)
                //        {
                //            gridView2.SetRowCellValue(i, _coOrders, Convert.ToInt32(gridView2.GetRowCellValue(i, _coOrders)) + 1);
                //            if (Convert.ToInt32(gridView2.GetRowCellValue(i, _coA)) == 1)
                //                gridView2.SetRowCellValue(i, _coA, 2);
                //        }
                //    }
                //}
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
                for (int i = 0; i <gridView2.RowCount; i++)// dtInfo.Rows.Count;
                {
                    string gv = gridView2.GetRowCellValue(i, _coWorkingID).ToString();// dtInfo.Rows[i]["WorkingID"].ToString();
                    if (gv == ch)
                    {
                        if (Convert.ToInt32( gridView2.GetRowCellValue(i, _coID)) >0)
                        {
                            t = false;
                            gridView2.SetRowCellValue(i, _coIsTicket, false);
                            gridView2.SetRowCellValue(i, _coA, 2);
                        }
                        else
                        {
                            gridView2.DeleteRow(i); //dtInfo.Rows.RemoveAt(i);
                            dtInfo.AcceptChanges();
                        }
                        break;
                    }
                }
            }
            t = true;
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


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (int.Parse(dtMain.Rows[0]["ID"].ToString()) < 1)
            {
                int MainID = int.Parse(BasicClass.GetDataSet.GetOne(bllPWM, "GetIsDefaultID", new object[] { _materielID }).ToString());
                if (MainID > 0)
                {
                    dtInfo = BasicClass.GetDataSet.GetDS(bllPWI, "GetList", new object[] { "(MainID=" + MainID + ")" }).Tables[0];
                    gridControl2.DataSource = dtInfo;
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
                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        dtInfo.Rows[i]["A"] = 3;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("本单已保存，不能继续此操作！");
            }
        }

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridView2.RowCount == 0)
            //{
            //    XtraMessageBox.Show("还没有明细工序，不能保存!");
            //    return;
            //}
            gridView2.CloseEditor();
            gridView2.UpdateCurrentRow();
            gridView3.CloseEditor();
            gridView3.UpdateCurrentRow();
                dtMain.Rows[0]["CompanyID"] = Convert.ToInt32(checkEdit2.Checked);
            if (checkEdit1.Checked)
                BasicClass.GetDataSet.ExecSql(bllPWM, "UpDefault", new object[] { _materielID });
            dtMain.Rows[0]["Remark"] = _ltRemark.EditVal;
            if (_mainID > 0)
            {
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
                    if (Convert.ToBoolean(dtTem.Rows[0]["IsSpecial"]))
                    {
                        dtTemInfo.Rows.Clear();
                        dtTemInfo = BasicClass.GetDataSet.GetDS(bllPWI, "GetList", new object[] { "(PWIID=" + dtInfo.Rows[i]["ID"] + ")" }).Tables[0];
                        if (dtTemInfo.Rows.Count > 0)
                        {
                            for (int z = 0; z < dtTemInfo.Rows.Count; z++)
                            {
                                dtTem.Rows.Clear();
                                dtTem.Rows.Add(dtTemInfo.Rows[z].ItemArray);
                                dtTem.Rows[0]["IsTicket"] = dtInfo.Rows[i]["IsTicket"];
                                dtTem.Rows[0]["IsCaiC"] = dtInfo.Rows[i]["IsCaiC"];
                                dtTem.Rows[0]["IsCut"] = dtInfo.Rows[i]["IsCut"];
                                dtTem.Rows[0]["IsCanMove"] = dtInfo.Rows[i]["IsCanMove"];
                                BasicClass.GetDataSet.UpData(bllPWI, dtTem);
                            }
                        }
                    }
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
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView2.ExportToXls(fileName);
                OpenFile(fileName);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            simpleButton2.Visible = !checkEdit1.Checked;
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
            if (gridView2.FocusedRowHandle > -1)
            {
                if(DialogResult.Yes==XtraMessageBox.Show("删除后，已打印工票将不能正常录入，是否继续？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("删除后，已打印工票将不能正常录入，请再次确认是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        if (int.Parse(gridView2.GetFocusedRowCellValue(_coID).ToString()) > 0)
                        {
                            if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProductWorkingInfo, "GetPWIDUseCount", new object[] { Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coID)) })) > 0)
                            {
                                XtraMessageBox.Show("该工序已有生成工票，不能被删除！");
                                return;
                            }
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "Delete", new object[] { int.Parse(gridView2.GetFocusedRowCellValue(_coID).ToString())});
                        }
                        gridView2.DeleteRow(gridView2.FocusedRowHandle);
                        dtInfo.AcceptChanges();
                    }
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
            if (lookUpEdit1.EditValue != null && lookUpEdit1.EditValue.ToString().Trim() != string.Empty)
            {
                t = false;
                int a = int.Parse(lookUpEdit1.EditValue.ToString());
                //for (int i = 0; i < dtInfo.Rows.Count; i++)
                //{

                //}
                string work = lookUpEdit1.Text;
                for (int i = 0; i < dtWork.Rows.Count; i++)
                {
                    if (checkedListBoxControl1.GetItemValue(i).Equals(a))
                    {
                        if (!checkedListBoxControl1.GetItemChecked(i))
                        {
                            for (int j = 0; j < gridView2.RowCount; j++)
                            {
                                if (gridView2.GetRowCellValue(j, _coWorkingID).Equals(a))
                                {
                                    if (!(bool)(gridView2.GetRowCellValue(j, _coIsTicket)))
                                    {
                                        gridView2.SetRowCellValue(j, _coIsTicket, true);
                                        checkedListBoxControl1.SetItemChecked(i, true);
                                        return;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("已添加该道工序!");
                                        return;
                                    }
                                }

                            }
                            int o = 1;
                            int g = 1;
                            if (gridView2.RowCount > 0)
                            {
                                o = int.Parse(gridView2.GetRowCellValue(gridView2.RowCount - 1, _coOrders).ToString()) + 1;
                                g = int.Parse(gridView2.GetRowCellValue(gridView2.RowCount - 1, _coGroupBy).ToString());
                            }

                            if (gridView2.FocusedRowHandle > -1)
                            {
                                o = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coOrders)) + 1;
                                g = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coGroupBy));
                            }
                            //    if (gridView2.FocusedRowHandle < gridView2.RowCount - 1)
                            //    {
                            //        for (int z = gridView2.FocusedRowHandle + 1; z < gridView2.RowCount; z++)
                            //        {
                            //            gridView2.SetRowCellValue(z, _coOrders, Convert.ToInt32(gridView2.GetRowCellValue(z, _coOrders)) + 1);
                            //            if (Convert.ToInt32(gridView2.GetRowCellValue(i, _coA)) == 1)
                            //                gridView2.SetRowCellValue(i, _coA, 2);
                            //        }
                            //    }
                            //}
                            DataRow[] drs;
                            drs = dtInfo.Select("(Orders>" + _Orders + ") ");//And (Orders>=" + _o + ")");
                            if (drs.Length > 0)
                            {
                                for (int c = 0; c < drs.Length; c++)
                                {
                                    drs[c]["Orders"] = Convert.ToInt32(drs[c]["Orders"]) + 1;
                                    if (Convert.ToInt32(drs[c]["A"]) == 1)
                                    drs[c]["A"] = 2;
                                }
                            }
                            dtInfo.AcceptChanges();

                            dtInfo.Rows.Add(dtInfo.NewRow());
                            int r = dtInfo.Rows.Count - 1;
                            dtInfo.Rows[r]["Orders"] = o;
                            dtInfo.Rows[r]["WorkingID"] =a;
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
                            dtInfo.Rows[r]["IsSpecial"] = dtWork.Select("(ID=" + a + ")")[0]["IsSpecial"];
                            checkedListBoxControl1.SetItemChecked(i, true);
                        }
                        else
                        {
                            XtraMessageBox.Show("已添加该道工序！");
                        }
                       
                        //checkedListBoxControl1.SetItemChecked(i, (bool)(e.Value));
                        break;
                    }
                }
            }
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coIsTicket && e.Value != null)
            {
                string work = gridView2.GetRowCellValue(e.RowHandle, _coWorkingID).ToString();
                for (int i = 0; i < dtWork.Rows.Count; i++)
                {
                    if (checkedListBoxControl1.GetItemValue(i).ToString() == work)
                    {
                        t = false;
                        checkedListBoxControl1.SetItemChecked(i, (bool)(e.Value));
                        break;
                    }
                }
                if ((!(bool)(e.Value)) && gridView2.GetFocusedRowCellValue(_coID).Equals(0))
                {
                    gridView2.DeleteRow(e.RowHandle);
                    dtInfo.AcceptChanges();
                }
            }
            if (e.Value != null)
            {
                if (e.Column == _coIsCaiC || e.Column == _coIsCut)
                    gridView2.SetFocusedRowCellValue(e.Column, e.Value);
            }
            t = true;
        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtt = new DataTable();
            dtt.TableName = "Work";
            dtt.Columns.Add("Materiel", typeof(string));
            dtt.Columns.Add("Work", typeof(string));
            dtt.Columns.Add("Orders", typeof(string));
            dtt.Columns.Add("CustOrders", typeof(string));
            dtt.Columns.Add("GroupBy", typeof(string));
            dtt.Columns.Add("Price", typeof(decimal));
            dtt.Columns.Add("Remark", typeof(string));
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                if (Convert.ToBoolean(gridView2.GetRowCellValue(i, _coIsTicket)))
                {
                    dtt.Rows.Add(_ltRemark.val, gridView2.GetRowCellDisplayText(i, _coWorkingID),
                        gridView2.GetRowCellDisplayText(i, _coOrders), gridView2.GetRowCellDisplayText(i, _coCustOder),
                        gridView2.GetRowCellDisplayText(i, _coGroupBy), Convert.ToDecimal(gridView2.GetRowCellValue(i, _coPrice)),
                        gridView2.GetRowCellDisplayText(i, _coRemark));
                }
            }
            BaseForm.PrintClass.ProductWorking(dtt);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount > 0)
            {
                if (DialogResult.No == XtraMessageBox.Show("本款式已有设置工序，是否删除现有的工序？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    return;
                }
            }
            if (Convert.ToInt32( dtMain.Rows[0]["ID"])>0)
            {
                if (BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] { "(PWorkingID=" + dtMain.Rows[0]["ID"] +")"}).Tables[0].Rows.Count > 0)
                {
                    XtraMessageBox.Show("本工艺单已被使用！");
                    return;
                }
            }
            IsRe = false;
            BasicClass.cResult r = new BasicClass.cResult();
            r.RowChanged += new BasicClass.RowChangedHandler(r_RowChanged);
            Form fr = new frWorkingForm(r);
            fr.ShowDialog();
            IsRe = true;
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.TextChanged += new BasicClass.TextChangedHandler(cr_TextChanged);
            Form fr = new frWorkingPrice(cr);
            fr.ShowDialog();
        }

        void cr_TextChanged(string s)
        {
            
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                if (Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coID)) > 0&&Convert.ToBoolean(gridView2.GetFocusedRowCellValue(_coIsSpecial)))
                {
                    if (DialogResult.Cancel == XtraMessageBox.Show("请在保存之后继续以下操作，否则请返回", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        return;
                    }
                    IsRe = false;
                    BasicClass.cResult r=new BasicClass.cResult();
                    r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                    Form fr = new frSpecial(r, Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coID)),
                        gridView2.GetFocusedRowCellDisplayText(_coWorkingID));
                    fr.ShowDialog();
                    IsRe = true;
                }
            }
        }

        void r_TextChanged(string s)
        {

            showInfo(bs.Position);
        }

        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _coPrice.OptionsColumn.AllowEdit = true;
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //_coPrice.OptionsColumn.AllowEdit = false;
            try
            {
                if (gridView2.FocusedRowHandle > -1)
                    _Orders = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coOrders));
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
            _coPrice.OptionsColumn.AllowEdit = false;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                int _id = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coWorkingID));
                DataTable dttt = new DataTable();
                dttt.Columns.Add("MName", typeof(string));
                dttt.Columns.Add("WName", typeof(string));
                dttt.Columns.Add("MID", typeof(int));
                dttt.Columns.Add("WID", typeof(int));
                dttt.Rows.Add(gridView1.GetFocusedRowCellDisplayText(_coName), gridView2.GetFocusedRowCellDisplayText(_coWorkingID), _materielID, _id);
                Form fr = new frWorkPriceList(dttt);
                fr.ShowDialog();
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
                _coGroupBy.ColumnEdit = BaseForm.RepositoryItem._reCaiPian;
            else
                _coGroupBy.ColumnEdit = null;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                gridControl2.MainView = gridView3;
            }
            else
            {
                gridControl2.MainView = gridView2;
            }
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && Convert.ToInt32(gridView3.GetFocusedRowCellValue(_coIA)) == 1)
                gridView3.SetFocusedRowCellValue(_coIA, 2);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Form fr = new frSetNoPriceWorking();
            fr.ShowDialog();
        }
    }
}