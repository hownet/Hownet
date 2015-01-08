using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Hownet.Materiel
{
    public partial class MaterielSize : DevExpress.XtraEditors.XtraForm
    {
        public MaterielSize()
        {
            InitializeComponent();
        }

        string blMat = "Hownet.BLL.Materiel";
        string blSM = "Hownet.BLL.MaterielStructMain";
        string blSI = "Hownet.BLL.MaterielStructInfo";
        int _MainID = 0;
        int MaterielID = 0;
        bool t = false;
        bool _IsDefault = false;
        int isEnd = 0;
        DataTable dtMain = new DataTable();
        DataTable dtMSM = new DataTable();
        DataTable dtMat = new DataTable();
        DataTable dtMSI = new DataTable();
        ArrayList liDel = new ArrayList();
        BindingSource bs = new BindingSource();
        string per;
        private void MaterielBom_Load(object sender, EventArgs e)
        {
            ShowData();
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            //addNode(0);
            per = BasicClass.BasicFile.GetPermissions(this.Text);
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
            //    _barAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
            //    gridControl1.EmbeddedNavigator.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barUnVerify.Visibility = _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ucSizeList1.IsCanEdit = true;
            ucSizeList1.IsCanEditCS = true;
            ucSizeList1.IsShowPopupMenu = true;
        }
        private void ShowData()
        {
            //_coChildMaterielID.ColumnEdit =BaseForm. RepositoryItem._reMateriel;
            dtMat = BasicClass.GetDataSet.GetDS(blMat, "GetListAndMeasure", null).Tables[0];
            dtMat.DefaultView.RowFilter = "AttributeID<>4";
            //_reMateriel.DataSource = dtMat.DefaultView;
            //_coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            //_coUsePartID.ColumnEdit = BaseForm.RepositoryItem._reCaiPian;
            //_coUsingTypeID.ColumnEdit = BaseForm.RepositoryItem._reUse;
            //_coDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDepartmentType;
            treeList1.DataSource = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielType", "GetFinishedTree", null).Tables[0];
            treeList1.ExpandAll();
        }
        void bs_PositionChanged(object sender, EventArgs e)
        {
            //ShowInfo(bs.Position);
        }
        void InData(int materielID)
        {
            dtMain = BasicClass.GetDataSet.GetDS(blSM, "GetIDList", new object[] { materielID }).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;
            ShowInfo(0);
        }
        void ShowDefault(int MTID)
        {
            dtMSM = BasicClass.GetDataSet.GetDS(blSM, "GetList", new object[] { "(TaskID=" + MTID * -1 + ")" }).Tables[0];
            if (dtMSM.Rows.Count == 0)
            {
                DataRow dr = dtMSM.NewRow();
                dr["MainID"] = dr["MaterielID"] = dr["CompanyID"] = dr["Executant"] = dr["VerifyManID"] = dr["A"] = 0;
                dr["Ver"] = dr["Remark"] = string.Empty;
                dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
                dr["VerifyDateTime"] = Convert.ToDateTime("1900-1-1");
                dr["IsDefault"] = true;
                dr["TaskID"] = MTID * -1;
                dr["IsVerify"] = 1;
                dtMSM.Rows.Add(dr);
                dtMSM.Rows[0]["MainID"] = BasicClass.GetDataSet.Add(blSM, dtMSM);
            }
            _MainID = Convert.ToInt32(dtMSM.Rows[0]["MainID"]);
            t = (int.Parse(dtMSM.Rows[0]["IsVerify"].ToString()) == 3);
            dtMSI = BasicClass.GetDataSet.GetDS(blSI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            DataRow drr = dtMSI.NewRow();
            for (int i = 0; i < dtMSI.Columns.Count; i++)
            {
                drr[i] = 0;
            }
            drr["IsTogethers"] = false;
            drr["IsCaic"] = true;
            drr["UsingTypeID"] = 1;
            drr["MaterielID"] = MaterielID;
            drr["A"] = 3;
            dtMSI.Rows.Add(drr.ItemArray);
            dtMSI.Rows.Add(drr.ItemArray);
           // gridControl1.DataSource = dtMSI;
            t = (Convert.ToInt32(dtMSM.Rows[0]["IsVerify"]) > 2);
           // gridView1.OptionsBehavior.Editable = _barEdit.Enabled = _barSave.Enabled = _barVerify.Enabled = !t;
            _barNew.Enabled = _barUnVerify.Enabled = t;
        }
        private void ShowInfo(int p)
        {
            t = false;
            _barFrist.Enabled = _barPve.Enabled = _barNext.Enabled = _barLast.Enabled = true;
            if (p == 0)
                _barFrist.Enabled = _barPve.Enabled = false;
            if (p == dtMain.Rows.Count - 1)
                _barNext.Enabled = _barLast.Enabled = false;
            if (dtMain.DefaultView[p]["MainID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["MainID"].ToString());
                dtMSM = BasicClass.GetDataSet.GetDS(blSM, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                //modMSM = bllMSM.GetModel(int.Parse(dtMain.DefaultView[p]["MainID"].ToString()));
                //textEdit1.Text = modMSM.Ver;
                //_loCompany.EditValue = modMSM.CompanyID;
                //_loTaskNum.EditValue = modMSM.TaskID;
                //checkEdit1.Checked = modMSM.IsDefault;
                //_barFill.Caption = "本单由：" + bllUI.GetModel(modMSM.Executant).UserInfoName + " 于：" + modMSM.DateTime.ToString("yyyy年MM月dd日") + "填写";
            }
            else
            {
                _MainID = 0;
                dtMSM = BasicClass.GetDataSet.GetDS(blSM, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                DataRow dr = dtMSM.NewRow();
                dr["MainID"] = 0;
                dr["Ver"] = treeList1.FocusedNode.GetValue(_trName).ToString() + "的默认单用量";
                dr["DateTime"] = DateTime.Today;
                dr["CompanyID"] = 0;
                dr["TaskID"] = 0;
                dr["Remark"] = 0;
                dr["IsDefault"] = true;
                dr["IsVerify"] = 1;
                dr["VerifyManID"] = 0;
                dr["VerifyDateTime"] = DateTime.Parse("1900-1-1");
                dr["Executant"] = BasicClass.UserInfo.UserID;
                dr["A"] = 3;
                dr["MaterielID"] = MaterielID;
                dtMSM.Rows.Add(dr);
                //_loCompany.EditValue = modMSM.CompanyID = 0;
                //_barFill.Caption = "";
                //modMSM.DateTime = DateTime.Today;
                //modMSM.Executant = Hownet.BLL.BaseFile.UserInfo.UserID;
                
                //modMSM.IsVerify = 1;
                //modMSM.MainID = 0;
                //modMSM.MaterielID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                //modMSM.Remark = "";
                //_loTaskNum.EditValue = modMSM.TaskID = 0;
                
                //modMSM.VerifyDateTime = DateTime.Parse("1900-1-1");
                //modMSM.VerifyManID = 0;
                //if (p == 0)
                //{
                //    checkEdit1.Checked = modMSM.IsDefault = true;
                //    textEdit1.Text = modMSM.Ver = treeList1.FocusedNode.GetValue(_trMaterielName).ToString() + "的默认单用量";
                //}
                //else
                //{
                //    checkEdit1.Checked = modMSM.IsDefault = false;
                //    textEdit1.Text = modMSM.Ver = treeList1.FocusedNode.GetValue(_trMaterielName).ToString() + "的第" + (p + 1).ToString() + "份单用量";
                //}
            }
            t = (int.Parse(dtMSM.Rows[0]["IsVerify"].ToString()) == 3);
            _MainID = Convert.ToInt32(dtMSM.Rows[0]["MainID"]);
            dtMSI = BasicClass.GetDataSet.GetDS(blSI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            barStaticItem1.Caption = string.Empty;
            if (dtMSI.Rows.Count == 0)
            {
                if (treeList1.FocusedNode.Level > 1)
                {
                    int a = Convert.ToInt32(treeList1.FocusedNode.ParentNode.GetValue(_trID));
                    DataTable mdt = BasicClass.GetDataSet.GetDS(blSM, "GetList", new object[] { "(TaskID=" + a * -1 + ")" }).Tables[0];
                    if (mdt.Rows.Count > 0)
                    {
                        dtMSI = BasicClass.GetDataSet.GetDS(blSI, "GetList", new object[] { "(MainID=" + Convert.ToInt32(mdt.Rows[0]["MainID"]) + ")" }).Tables[0];
                        if (dtMSI.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtMSI.Rows.Count; i++)
                            {
                                dtMSI.Rows[i]["MainID"] = _MainID;
                                dtMSI.Rows[i]["InfoID"] = 0;
                                dtMSI.Rows[i]["MaterielID"] = MaterielID;
                                dtMSI.Rows[i]["A"] = 3;
                            }
                        }
                        barStaticItem1.Caption = "此为引用上级默认用量";
                    }
                }
            }
            DataRow drr = dtMSI.NewRow();
            for (int i = 0; i < dtMSI.Columns.Count; i++)
            {
                drr[i] = 0;
            }
            drr["IsTogethers"] = false;
            drr["IsCaic"] = true;
            drr["UsingTypeID"] = 1;
            drr["MaterielID"] = MaterielID;
            drr["A"] = 3;
            dtMSI.Rows.Add(drr.ItemArray);
            dtMSI.Rows.Add(drr.ItemArray);
            //gridControl1.DataSource = dtMSI;
            t = (Convert.ToInt32(dtMSM.Rows[0]["IsVerify"]) > 2);
          //  gridView1.OptionsBehavior.Editable = _barEdit.Enabled = _barSave.Enabled = _barVerify.Enabled = !t;
            _barNew.Enabled = _barUnVerify.Enabled = t;
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _IsDefault = false;
            if (treeList1.FocusedNode != null)
            {
                if (!treeList1.FocusedNode.HasChildren)
                {
                   // gridControl1.DataSource = null;
                    MaterielID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                    //InData(MaterielID);
                    ucSizeList1.Open(MaterielID,-1,  true);
                   // gridView1.OptionsBehavior.Editable = true;
                }
                else
                {
                  //  gridView1.OptionsBehavior.Editable = false;
                }
                if (treeList1.FocusedNode.Level == 1)
                {

                        MaterielID = 0;//int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                        ucSizeList1.Open(MaterielID, -1, true);
                       // gridView1.OptionsBehavior.Editable = true;
                   // }
                    //modMSM.MaterielID = MaterielID = 0;
                    //dtMain = bllMSM.GetIDList(0).Tables[0];
                    //dtMain.Rows.Add(dtMain.NewRow());
                    //ShowInfo(0);
                    //splitContainerControl1.Panel2.Enabled = false;
                    //if (!treeList1.FocusedNode.HasChildren)
                    //{
                    //    int i = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                    //    DataTable dt = bllMat.GetLaftTree(i, isEnd).Tables[0];
                    //    for (int j = 0; j < dt.Rows.Count; j++)
                    //    {
                    //        treeList1.AppendNode(new object[] { dt.DefaultView[j]["MaterielID"], dt.DefaultView[j]["MaterielName"], i }, treeList1.Nodes[i - 1]);
                    //    }
                    //    treeList1.FocusedNode.ExpandAll();
                    //}
                }
              //  string bb=treeList1.FocusedNode.GetDisplayText(tree
                //if (treeList1.FocusedNode.Level == 1)
                //{
                //    modMSM.MaterielID = MaterielID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                //    //_reMateriel.DataSource = bllMat.GetList("(AttributeID>1) and (MaterielID<>" + modMSM.MaterielID + ")").Tables[0];
                //    string attribName = treeList1.FocusedNode.ParentNode.GetValue(_trMaterielName).ToString();
                //    splitContainerControl1.Panel2.Text = attribName + "：" + treeList1.FocusedNode.GetValue(_trMaterielName).ToString();
                //    InData(MaterielID);
                //    splitContainerControl1.Panel2.Enabled = true;
                //    bool zzz = (treeList1.FocusedNode.ParentNode.GetValue(_trAttributeID).ToString() == "1");
                //    _coIsTogethers.OptionsColumn.AllowEdit = zzz;
                //}
                //_laNum.Visible = _laTaskNum.Visible = _laCompany.Visible = true;
            }
        }
        //加新行
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }
        //打印
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XtraGridPrint xgp = new XtraGridPrint();
            //xgp.IsPrintDate = false;
            //xgp.IsPrintPage = false;
            //xgp.EnableEditPage = true;
            //xgp.PageHeaderName = splitContainerControl1.Panel1.Text;
            //xgp.ShowDevPreview(gridControl1);
        }
        //View导航条按钮点击

        //行数变动

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
            bs.AddNew();
        }
        #endregion

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(false);
            if (!_IsDefault)
                InData(MaterielID);// ShowInfo(bs.Position);
            else
                ShowDefault(MaterielID);
        }
        private void Save(bool IsVerify)
        {
            ucSizeList1.Save(MaterielID, -1);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //if (modMSM.MainID == 0)
            //{
            //    int MainID = bllMSM.GetIsDefaultID(modMSM.MaterielID);
            //    BindingList<Hownet.Model.MaterielStructInfo> bingList = new BindingList<Hownet.Model.MaterielStructInfo>(bllMSI.DataTableToList(bllMSI.GetList("(MainID='" + modMSM.MainID + "')").Tables[0]));
            //    for (int i = 0; i < bingList.Count; i++)
            //    {
            //        bingList[i].A = 3;
            //    }
            //    gridControl1.DataSource = bingList;
            //}
            //else
            //    XtraMessageBox.Show("本单已保存，不能继续此操作！");
        }
        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            //dtMain = bllMSM.GetIDList(0).Tables[0];
            //dtMain.Rows.Add(dtMain.NewRow());
            ////ShowInfo(0);
            //if (barEditItem1.EditValue.ToString() == "全部")
            //    isEnd = -1;
            //else if (barEditItem1.EditValue.ToString() == "使用中")
            //    isEnd = 0;
            //else if (barEditItem1.EditValue.ToString() == "停用")
            //    isEnd = 1;
            //addNode(isEnd);
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(true);
            //modMSM.IsVerify = 3;
            //modMSM.VerifyDateTime = DateTime.Today;
            //bllMSM.Update(modMSM);
            if (!_IsDefault)
              InData(MaterielID);//  ShowInfo(bs.Position);
            else
                ShowDefault(MaterielID);
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (Convert.ToInt32( BasicClass.GetDataSet.GetOne(blSM,"CountUse",new object[]{_MainID}))==0)
            //{
            Save(false);
            if (!_IsDefault)
               InData(MaterielID);// ShowInfo(bs.Position);
            else
                ShowDefault(MaterielID);
            //}
            //else
            //{
            //    XtraMessageBox.Show("本物料结构表已被使用，不能反审修改！");
            //    return;
            //}
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowHitInfo(treeList1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        private void ShowHitInfo(DevExpress.XtraTreeList.TreeListHitInfo hi)
        {
            //lbHitTest.Text = hi.HitInfoType.ToString();
            //lbColumn.Text = hi.Column == null ? "No column" : hi.Column.GetCaption();
            //lbNode.Text = hi.Node == null ? "-1" : hi.Node.Id.ToString();
            //if (hi.Column == null || hi.Node == null)
            //    lbCellValue.Text = " ";
            //else
            //    lbCellValue.Text = hi.Node.GetDisplayText(hi.Column.AbsoluteIndex);
            if (hi.Node != null)
                XtraMessageBox.Show(hi.Node.GetValue(_trName).ToString());
        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtt = new DataTable();
            dtt.TableName = "BOM";
            dtt.Columns.Add("Materiel", typeof(string));
            dtt.Columns.Add("ChildMateriel", typeof(string));
            dtt.Columns.Add("UsePart", typeof(string));
            dtt.Columns.Add("Department", typeof(string));
            dtt.Columns.Add("Amount", typeof(decimal));
            dtt.Columns.Add("Measure", typeof(string));
            dtt.Columns.Add("Wastage", typeof(decimal));
            dtt.Columns.Add("IsTogeth", typeof(bool));
            dtt.Columns.Add("Price", typeof(decimal));
            dtt.Columns.Add("Money", typeof(decimal));
            dtt.Columns.Add("Use", typeof(string));
            dtt.Columns.Add("IsCaic", typeof(bool));
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    dtt.Rows.Add(treeList1.FocusedNode.GetValue(_trName).ToString(), gridView1.GetRowCellDisplayText(i, _coChildMaterielID),
            //        gridView1.GetRowCellDisplayText(i, _coUsePartID), gridView1.GetRowCellDisplayText(i, _coDepartmentID),
            //        Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount)), gridView1.GetRowCellDisplayText(i, _coMeasureID),
            //        Convert.ToDecimal(gridView1.GetRowCellValue(i, _coWastage)), Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsTogethers)),
            //        Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice)), Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney)),
            //        gridView1.GetRowCellDisplayText(i, _coUsingTypeID), Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsCaiC)));
            //}
            BaseForm.PrintClass.BOM(dtt);
        }

        private void _barExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucSizeList1.ToExcel();
        }
    }
}