using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraTreeList;

namespace Hownet.Materiel
{
    public partial class MaterielBom : DevExpress.XtraEditors.XtraForm
    {
        public MaterielBom()
        {
            InitializeComponent();
        }
        
        string blMat = "Hownet.BLL.Materiel";
        string blSM = "Hownet.BLL.MaterielStructMain";
        string blSI = "Hownet.BLL.MaterielStructInfo";
        int _MainID = 0;
        int MaterielID = 0;
        public MaterielBom(int MatID)
            : this()
        {
            MaterielID = MatID;
        }
        bool t = false;
        bool _isEdit = false;
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

            per = BasicClass.BasicFile.GetPermissions(this.Text);
            //if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
            //    _barAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                gridControl1.EmbeddedNavigator.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barUnVerify.Visibility = _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (MaterielID > 0)
            {
                InData(MaterielID * -1);
                lookUpEdit1.EditValue = MaterielID;
            }
            _loCompany.Properties.DataSource = BasicClass.BaseTableClass.dtCompany;
         

        }

        private void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowInfo(bs.Position);
        }
        private void ShowData()
        {
            BasicClass.BaseTableClass.dtMateriel.DefaultView.RowFilter = "";
            dtMat = BasicClass.BaseTableClass.dtMateriel;
            _reMateriel.DataSource = dtMat;
            _coMeasureID.ColumnEdit =_coTaskMeasureID.ColumnEdit= BaseForm.RepositoryItem._reMeasure;
            _coUsePartID.ColumnEdit = BaseForm.RepositoryItem._reCaiPian;
            _coUsingTypeID.ColumnEdit = BaseForm.RepositoryItem._reUse;
            _coDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDepartmentType;
            treeList1.DataSource =BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielType", "GetTree", null).Tables[0];
            treeList1.ExpandAll();
            lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtFinished;
            _reSpec.DataSource = BasicClass.BaseTableClass.dtSpec;
        }
        void InData(int materielID)
        {
            dtMain = BasicClass.GetDataSet.GetBySql("SELECT MainID FROM MaterielStructMain WHERE (MaterielID = " + materielID + ") order by MainID ");
            listView1.Items.Clear();
            if (dtMain.Rows.Count == 0)//,Ver, CONVERT(varchar(100), DateTime, 23) as DateTime,Money
            {
                dtMain.Rows.Add(0);//, string.Empty, string.Empty, 0
            }
            bs.DataSource = dtMain;
           // listView1.Items[0].Selected = true;
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
                dr["Executant"] = BasicClass.UserInfo.UserID; ;
                dr["Money"] = 0;
                dr["WorkingMoney"] = 0;
                dr["OutPrice"] = 0;
                dr["MaterielPro"] = 0;
                dr["GrossProfit"] = 0;
                dr["GrossPro"] = 0;
                dr["CMT"] = 0;
                dr["BySizeName"] = dr["ExSize"] = dr["MaterielLoss"] = dr["PackLoss"] = dr["FillManName"] = dr["VerifyManName"] = string.Empty;
                dtMSM.Rows.Add(dr);
                dtMSM.Rows[0]["MainID"] = BasicClass.GetDataSet.Add(blSM, dtMSM);
            }
            _MainID = Convert.ToInt32(dtMSM.Rows[0]["MainID"]);

            SetBing();

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
            drr["SupplierName"] = string.Empty;
            drr["SpecName"] = string.Empty;
            drr["SupplierSN"] = string.Empty;
            dtMSI.Rows.Add(drr.ItemArray);
            dtMSI.Rows.Add(drr.ItemArray);
            gridControl1.DataSource = dtMSI;
            t = (Convert.ToInt32(dtMSM.Rows[0]["IsVerify"]) > 2);
            gridView1.OptionsBehavior.Editable = _barEdit.Enabled = _barSave.Enabled = _barVerify.Enabled = !t;
            _barNew.Enabled = _barUnVerify.Enabled = t;
            _isEdit = false;
        }
        private void ShowInfo(int p)
        {
            t = false;
            _barPrv.Enabled = _barNext.Enabled =  true;
            if (p == 0)
                _barPrv.Enabled = false;
            if (p == dtMain.Rows.Count - 1)
                _barNext.Enabled =  false;

            object o = dtMain.DefaultView[p]["MainID"];
            if (o!=DBNull.Value&& dtMain.DefaultView[p]["MainID"].ToString() != "0")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["MainID"].ToString());
                dtMSM = BasicClass.GetDataSet.GetDS(blSM, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            }
            else
            {
                _MainID = 0;
                dtMSM = BasicClass.GetDataSet.GetDS(blSM, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                DataRow dr = dtMSM.NewRow();
                dr["MainID"] = 0;
                dr["Ver"] =lookUpEdit1.Text +BasicClass.GetDataSet.GetDateTime().Date+ "的单用量";
                dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
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
                dr["Money"] = 0;
                dr["WorkingMoney"] = BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductWorkingMain", "GetWorkingMoney", new object[] { MaterielID });
                dr["OutPrice"] = 0;
                dr["MaterielPro"] = 0;
                dr["GrossProfit"] = 0;
                dr["GrossPro"] = 0;
                dr["CMT"] = 0;
                dr["BySizeName"] = dr["ExSize"] = dr["MaterielLoss"] = dr["PackLoss"] =  dr["VerifyManName"] = string.Empty;
                dr["FillManName"] = BasicClass.UserInfo.TrueName;
                dtMSM.Rows.Add(dr);

            }
            SetBing();
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
            drr["Remark"] = string.Empty;
            drr["SupplierName"] = string.Empty;
            drr["SpecName"] = string.Empty;
            drr["SupplierSN"] = string.Empty;
            dtMSI.Rows.Add(drr.ItemArray);
            dtMSI.Rows.Add(drr.ItemArray);
            gridControl1.DataSource = dtMSI;
            t = (Convert.ToInt32(dtMSM.Rows[0]["IsVerify"]) > 2);
            gridView1.OptionsBehavior.Editable = _barEdit.Enabled = _barSave.Enabled = _barVerify.Enabled = !t;
            _barNew.Enabled = _barUnVerify.Enabled = t;
            _isEdit = false;
            tabControl1.SelectedTab = tabPage1;
            ShowPic();
           // dtMSM.AcceptChanges();
        }
        private void SetBing()
        {
            dtMSM.ColumnChanged += dtMSM_ColumnChanged;
            //  bs.DataSource = dtMSM;
            _teWorkingMoney.DataBindings.Clear();
            _teWorkingMoney.DataBindings.Add("EditValue", dtMSM, "WorkingMoney", true, DataSourceUpdateMode.OnPropertyChanged);

            _teVer.DataBindings.Clear();
            _teVer.DataBindings.Add("EditValue", dtMSM, "Ver", true, DataSourceUpdateMode.OnPropertyChanged);

            _teBySizeName.DataBindings.Clear();
            _teBySizeName.DataBindings.Add("EditValue", dtMSM, "BySizeName", true, DataSourceUpdateMode.OnPropertyChanged);

            _teCMT.DataBindings.Clear();
            _teCMT.DataBindings.Add("EditValue", dtMSM, "CMT", true, DataSourceUpdateMode.OnPropertyChanged);

            _teExSize.DataBindings.Clear();
            _teExSize.DataBindings.Add("EditValue", dtMSM, "ExSize", true, DataSourceUpdateMode.OnPropertyChanged);

            _teGrossPro.DataBindings.Clear();
            _teGrossPro.DataBindings.Add("EditValue", dtMSM, "GrossPro", true, DataSourceUpdateMode.OnPropertyChanged);

            _teGrossProfit.DataBindings.Clear();
            _teGrossProfit.DataBindings.Add("EditValue", dtMSM, "GrossProfit", true, DataSourceUpdateMode.OnPropertyChanged);

            _teMaterielPro.DataBindings.Clear();
            _teMaterielPro.DataBindings.Add("EditValue", dtMSM, "MaterielPro", true, DataSourceUpdateMode.OnPropertyChanged);

            _teMoney.DataBindings.Clear();
            _teMoney.DataBindings.Add("EditValue", dtMSM, "Money", true, DataSourceUpdateMode.OnPropertyChanged);

            _teOutPrice.DataBindings.Clear();
            _teOutPrice.DataBindings.Add("EditValue", dtMSM, "OutPrice", true, DataSourceUpdateMode.OnPropertyChanged);

            _loCompany.DataBindings.Clear();
            //_loCompany.EditValue = Convert.ToInt32(dtMSM.Rows[0]["CompanyID"]);
            _loCompany.DataBindings.Add("EditValue", dtMSM, "CompanyID", true, DataSourceUpdateMode.OnPropertyChanged);

            _lbDateTime.Text = Convert.ToDateTime(dtMSM.Rows[0]["DateTime"]).ToString("yyyy-MM-dd");
            _lbFillMan.Text = dtMSM.Rows[0]["FillManName"].ToString();
            _lbVerify.Text = dtMSM.Rows[0]["VerifyManName"].ToString();
            if (_lbVerify.Text != string.Empty)
                _lbVerifyDateTime.Text = dtMSM.Rows[0]["VerifyDateTime"].ToString();
            else
                _lbVerifyDateTime.Text = string.Empty;
        }
        private void ShowPic()
        {
            DataRow[] drs = BasicClass.BaseTableClass.dtAllMateriel.Select("(ID=" + MaterielID * -1 + ")");
            string picName = string.Empty;
            if (drs.Length > 0)
            {
               picName=drs[0]["Image"].ToString();
            }
            if (picName.Trim() != "")
            {
                if (!BasicClass.BasicFile.FileExists( picName))
                    BasicClass.FileUpDown.DownLoad( picName,   picName);
                pictureEdit1.EditValue = BasicClass.FileUpDown.getPicEditValue( picName);
            }
            else
            {
                pictureEdit1.EditValue = null;
            }
        }
        void dtMSM_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            _isEdit = true;
            CaicTextMoney();
        }
        private void CaicTextMoney()
        {
            try
            {
               _teMaterielPro.EditValue = Convert.ToDecimal(_teMoney.EditValue) / Convert.ToDecimal(_teOutPrice.EditValue);
                _teGrossProfit.EditValue = Convert.ToDecimal(_teOutPrice.EditValue) - Convert.ToDecimal(_teMoney.EditValue);
                _teGrossPro.EditValue = Convert.ToDecimal(_teGrossProfit.EditValue) / Convert.ToDecimal(_teOutPrice.EditValue);
                 _teCMT.EditValue = Convert.ToDecimal(_teGrossProfit.EditValue) / Convert.ToDecimal(_teWorkingMoney.EditValue);
            }
            catch
            {

            }
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _IsDefault = false;
            if (treeList1.FocusedNode != null)
            {
                if (!treeList1.FocusedNode.HasChildren)
                {
                    gridControl1.DataSource = null;
                    MaterielID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                    lookUpEdit1.EditValue = MaterielID * -1;
                  //  if(lookUpEdit1.Text==string.Empty)
                    InData(MaterielID);
                   // gridView1.OptionsBehavior.Editable = true;
                }
                else
                {
                    gridView1.OptionsBehavior.Editable = false;
                }
                if (treeList1.FocusedNode.Level == 1)
                {
                    if (Convert.ToInt32(treeList1.FocusedNode.GetValue(_trID)) > 0 && Convert.ToInt32(treeList1.FocusedNode.GetValue(_trAttributeID)) == 4)
                    {
                        _IsDefault = true;
                        gridControl1.DataSource = null;
                        MaterielID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                        ShowDefault(MaterielID);
                       // gridView1.OptionsBehavior.Editable = true;
                    }
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
        //单元格更改
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null||e.Value==DBNull.Value)
                return;
            _isEdit = true;
            if (e.Column == _coChildMaterielID && e.Value != null&&Convert.ToInt32(e.Value)>0)
            {
                DataTable dtMatTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID="+e.Value+")"}).Tables[0]; //object o = _reMateriel.GetDataSourceValue("MeasureID", _reMateriel.GetDataSourceRowIndex("ID", e.Value));
                if (dtMatTem.Rows.Count == 1)
                {
                    gridView1.SetRowCellValue(e.RowHandle, _coMeasureID, dtMatTem.Rows[0]["MeasureID"]);
                    gridView1.SetRowCellValue(e.RowHandle, _coTaskMeasureID, dtMatTem.Rows[0]["MeasureID"]);
                    gridView1.SetRowCellValue(e.RowHandle, _coPrice, dtMatTem.Rows[0]["ChengBengJ"]);
                }
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
                    drr["MaterielID"] = MaterielID;
                    drr["A"] = 3;
                    drr["Remark"] = string.Empty;
                    drr["SupplierName"] = string.Empty;
                    drr["SpecName"] = string.Empty;
                    drr["SupplierSN"] = string.Empty;
                    dtMSI.Rows.Add(drr.ItemArray);
                }
            }
            if(e.Column==_coTaskMeasureID||e.Column==_coAmount)
            {
                int _mea = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMeasureID));
                int _tm = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coTaskMeasureID));
                if(_mea>0&&_tm>0&& _mea!=_tm)
                {
                    DataRow[] drs = BasicClass.BaseTableClass.dtMeasure.Select("(ID=" + _mea + ")");
                    DataRow[] drsT = BasicClass.BaseTableClass.dtMeasure.Select("(ID=" + _tm + ")");

                    if (Convert.ToInt32(drs[0]["MeasureTypeID"]) == Convert.ToInt32(drsT[0]["MeasureTypeID"]))
                    {
                        try
                        {
                            decimal aaa = (Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coAmount)) * Convert.ToDecimal(drs[0]["Conversion"])) / Convert.ToDecimal(drsT[0]["Conversion"]);
                            if (aaa != Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coTMAmount)))
                                gridView1.SetFocusedRowCellValue(_coTMAmount, aaa);
                        }
                        catch
                        {
                            gridView1.SetFocusedRowCellValue(_coTMAmount, 0);
                        }
                    }
                }
            }
            if(e.Column==_coTMAmount)
            {
                int _mea = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMeasureID));
                int _tm = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coTaskMeasureID));
                if (_mea > 0 && _tm > 0 && _mea != _tm)
                {
                    DataRow[] drs = BasicClass.BaseTableClass.dtMeasure.Select("(ID=" + _mea + ")");
                    DataRow[] drsT = BasicClass.BaseTableClass.dtMeasure.Select("(ID=" + _tm + ")");
                    if (Convert.ToInt32(drs[0]["MeasureTypeID"]) == Convert.ToInt32(drsT[0]["MeasureTypeID"]))
                    {
                        try
                        {
                            decimal aaa = (Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coTMAmount)) * Convert.ToDecimal(drsT[0]["Conversion"])) / Convert.ToDecimal(drs[0]["Conversion"]);
                            if (aaa != Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coAmount)))
                                gridView1.SetFocusedRowCellValue(_coAmount, aaa);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            if ((e.Column == _coChildMaterielID || e.Column == _coUsingTypeID)&&( e.Value != null && Convert.ToInt32(e.Value) > 0) )
            {
                int _U=Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coUsingTypeID));
                for (int i = 0; i < gridView1.RowCount - 2; i++)
                {
                    //int aaaaa = Convert.ToInt32(gridView1.GetRowCellValue(i, _coUsingTypeID));
                    if (i != e.RowHandle)
                    {
                        if (Convert.ToInt32(gridView1.GetRowCellValue(i, _coChildMaterielID)) ==
                           Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coChildMaterielID)) &&
                           Convert.ToInt32(gridView1.GetRowCellValue(i, _coUsingTypeID)) ==_U)
                        {
                            XtraMessageBox.Show("物料名重复！");
                            gridView1.SetFocusedRowCellValue(_coChildMaterielID, 0);
                            return;
                        }
                    }
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
                decimal _money = 0;
                for(int i=0;i<gridView1.RowCount-2;i++)
                {
                    _money += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
                }
                _teMoney.EditValue = _money;
            }
            catch { }
        }
        //View导航条按钮点击
        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.Tag.ToString() == "Del" && gridView1.FocusedRowHandle > -1&&!t )
            {
                if (Convert.ToInt32( gridView1.GetFocusedRowCellValue(_coID))>0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除该记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                       BasicClass.GetDataSet.ExecSql(blSI,"Delete",new object[]{Convert.ToInt32( gridView1.GetFocusedRowCellValue(_coID))});

                    }
                }
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                dtMSI.AcceptChanges();
            }
            if (e.Button.Tag.ToString() == "Add" && !t)
            {
                if (gridView1.FocusedRowHandle < 0)
                    return;
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coUsingTypeID)) == 13 && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0&&Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA))==1)
                {
                   // XtraMessageBox.Show("特殊方法的单用量请稍候~！");
                    BasicClass.cResult r = new BasicClass.cResult();
                    r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                    Form fr = new frSpecialBOM(r, Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)),
                        gridView1.GetFocusedRowCellDisplayText(_coChildMaterielID),t);
                    fr.ShowDialog();
                }
            }

        }

        void r_TextChanged(string s)
        {
            
        }
        //行数变动
        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {

        }

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
            dtMSM.Rows[0]["MaterielPro"] = _teMaterielPro.EditValue;
            dtMSM.Rows[0]["GrossProfit"] = _teGrossProfit.EditValue;
            dtMSM.Rows[0]["GrossPro"] = _teGrossPro.EditValue;
            dtMSM.Rows[0]["CMT"] = _teCMT.EditValue;

            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtMSI.AcceptChanges();
            if (IsVerify)
            {
                dtMSM.Rows[0]["IsVerify"] = 3;
                dtMSM.Rows[0]["VerifyDateTime"] = BasicClass.GetDataSet.GetDateTime();
                dtMSM.Rows[0]["VerifyManID"] = BasicClass.UserInfo.UserID;
                dtMSM.Rows[0]["VerifyManName"] = BasicClass.UserInfo.TrueName;
            }
            else
            {
                dtMSM.Rows[0]["IsVerify"] = 1;
                dtMSM.Rows[0]["VerifyDateTime"] = Convert.ToDateTime("1900-1-1");
                dtMSM.Rows[0]["VerifyManID"] = 1;
                dtMSM.Rows[0]["VerifyManName"] =string.Empty;
            }
            if (_MainID == 0)
            {
                dtMain.Rows[0]["MainID"] = _MainID = BasicClass.GetDataSet.Add(blSM, dtMSM);
            }
            else
            {
                BasicClass.GetDataSet.UpData(blSM, dtMSM);
            }
            DataTable dtt = dtMSI.Clone();
            int a = 0;
            for (int i = 0; i < dtMSI.Rows.Count ; i++)
            {
                dtMSI.Rows[i]["MainID"] = _MainID;
                dtMSI.Rows[i]["MaterielID"] = MaterielID * -1;
                a = int.Parse(dtMSI.Rows[i]["A"].ToString());
                if (Convert.ToInt32(dtMSI.Rows[i]["ChildMaterielID"])>0&& Convert.ToDecimal(dtMSI.Rows[i]["Amount"]) > 0)
                {
                    if (a > 1)
                    {
                        dtt.Rows.Clear();
                        dtt.Rows.Add(dtMSI.Rows[i].ItemArray);
                        if (a == 2)
                            BasicClass.GetDataSet.UpData(blSI, dtt);
                        else if (a == 3)
                            dtMSI.Rows[i]["InfoID"] = BasicClass.GetDataSet.Add(blSI, dtt);
                        dtMSI.Rows[i]["A"] = 1;
                    }
                }
                else
                {
                    if (Convert.ToInt32(dtMSI.Rows[i]["InfoID"]) > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blSI, "Delete", new object[] { Convert.ToInt32(dtMSI.Rows[i]["InfoID"]) });
                    }
                }
            }
            int p = bs.Position;
            InData(MaterielID);
            bs.Position = p;
            if (bs.Position == dtMain.Rows.Count - 1)
              ShowInfo(p);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(_isEdit)
            {
                if (_MainID > 0)
                {
                    dtMSM.Rows[0]["MaterielPro"] = _teMaterielPro.EditValue;
                    dtMSM.Rows[0]["GrossProfit"] = _teGrossProfit.EditValue;
                    dtMSM.Rows[0]["GrossPro"] = _teGrossPro.EditValue;
                    dtMSM.Rows[0]["CMT"] = _teCMT.EditValue;
                    BasicClass.GetDataSet.UpData(blSM, dtMSM);
                    dtMSM.AcceptChanges();
                    _isEdit = false;
                }
            }
            MessageBox.Show(_isEdit.ToString());
        }
        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(true);
            ShowInfo(0);
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(false);
            ShowInfo(0);
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowHitInfo(treeList1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        private void ShowHitInfo(DevExpress.XtraTreeList.TreeListHitInfo hi)
        {
            if (hi.Node != null)
                XtraMessageBox.Show(hi.Node.GetValue(_trName).ToString());
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!t)
            {
                gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < gridView1.RowCount - 1);
            }
        }

        private void _barExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName =Hownet.BaseContranl. BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
              Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
           
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
            string _MaterielName = lookUpEdit1.Text;
            if (_MaterielName.Trim() == string.Empty)
                _MaterielName = treeList1.FocusedNode.GetValue(_trName).ToString();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                dtt.Rows.Add(_MaterielName , gridView1.GetRowCellDisplayText(i, _coChildMaterielID),
                    gridView1.GetRowCellDisplayText(i, _coUsePartID), gridView1.GetRowCellDisplayText(i, _coDepartmentID),
                    Convert.ToDecimal(gridView1.GetRowCellValue(i, _coAmount)), gridView1.GetRowCellDisplayText(i, _coMeasureID),
                    Convert.ToDecimal(gridView1.GetRowCellValue(i, _coWastage)), Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsTogethers)),
                    Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice)), Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney)),
                    gridView1.GetRowCellDisplayText(i, _coUsingTypeID), Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsCaiC)));
            }
          //  BaseForm.PrintClass.BOM(dtt);

            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            ds.Tables.Add(dtMSM.Copy());
            ds.Tables[0].TableName = "Main";
            ds.Tables[0].Columns.Add("CompanyName");
            ds.Tables[0].Columns.Add("MaterielName");
            ds.Tables[0].Columns.Add("VerifyTime");
            ds.Tables[0].Columns.Add("Image", typeof(byte[]));
            ds.Tables[0].Rows[0]["CompanyName"] = _loCompany.Text;
            ds.Tables[0].Rows[0]["MaterielName"] = _MaterielName;
            ds.Tables[0].Rows[0]["VerifyTime"] = _lbVerifyDateTime.Text;
            if (pictureEdit1.EditValue != null)
                ds.Tables[0].Rows[0]["Image"] =(byte[])pictureEdit1.EditValue;
            DataTable dtI = new DataTable();
            dtI.TableName="Info";
            dtI.Columns.Add("Materiel");
            dtI.Columns.Add("SupplierName");
            dtI.Columns.Add("SupplierSN");
            dtI.Columns.Add("SpecName");
            dtI.Columns.Add("UsePart");
            dtI.Columns.Add("Amount");
            dtI.Columns.Add("Wastage");
            dtI.Columns.Add("Measure");
            dtI.Columns.Add("Price");
            dtI.Columns.Add("Money");
            dtI.Columns.Add("Department");
            dtI.Columns.Add("Use");
          
            for(int i=0;i<gridView1.RowCount;i++)
            {
                DataRow dr = dtI.NewRow();
                dr["Materiel"] = gridView1.GetRowCellDisplayText(i, _coChildMaterielID); dr[1] = gridView1.GetRowCellDisplayText(i, _coSupplierName); dr[2] = gridView1.GetRowCellDisplayText(i, _coSupplierSN); dr[3] = gridView1.GetRowCellDisplayText(i, _coSpecID);
                dr[4] = gridView1.GetRowCellDisplayText(i, _coUsePartID); dr[5] = gridView1.GetRowCellDisplayText(i, _coAmount); dr[6] = gridView1.GetRowCellDisplayText(i, _coWastage); dr[7] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[8] = gridView1.GetRowCellDisplayText(i, _coPrice); dr[9] = gridView1.GetRowCellDisplayText(i, _coMoney); dr[10] = gridView1.GetRowCellDisplayText(i, _coDepartmentID); dr[11] = gridView1.GetRowCellDisplayText(i, _coUsingTypeID);
                dtI.Rows.Add(dr);
            }
            if(dtI.Rows.Count<40)
            {
                for(int i=dtI.Rows.Count;i<40;i++)
                {
                    dtI.Rows.Add(dtI.NewRow());
                }
            }
            ds.Tables.Add(dtI);
            BaseForm.PrintClass.PrintBOMPrice(ds);
        }

        private void _reMateriel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Tag.ToString()=="New")
            {
                BasicClass.cResult cr=new BasicClass.cResult();
                cr.TextChanged+=new BasicClass.TextChangedHandler(cr_TextChanged);
                Form fr = new BaseForm.frMateriel(cr, -1);
                fr.ShowDialog();
            }
        }

        void cr_TextChanged(string s)
        {
            try
            {
                int a = Convert.ToInt32(s);
                dtMat = BasicClass.GetDataSet.GetDS(blMat, "GetListAndMeasure", null).Tables[0];
                dtMat.DefaultView.RowFilter = "AttributeID<>4";
                _reMateriel.DataSource = dtMat.DefaultView;
                gridView1.SetFocusedValue(s);
            }
            catch
            {
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
            {
                if (MaterielID != Convert.ToInt32(lookUpEdit1.EditValue) * -1)
                {
                    MaterielID = Convert.ToInt32(lookUpEdit1.EditValue) * -1;
                    for (int i = 0; i < treeList1.Nodes.Count; i++)
                    {
                        if (treeList1.Nodes[i].HasChildren)
                        {
                            for (int j = 0; j < treeList1.Nodes[i].Nodes.Count; j++)
                            {
                                if (treeList1.Nodes[i].Nodes[j].HasChildren)
                                {
                                    for (int m = 0; m < treeList1.Nodes[i].Nodes[j].Nodes.Count; m++)
                                    {
                                        if (treeList1.Nodes[i].Nodes[j].Nodes[m][_trName].ToString() == lookUpEdit1.Text)
                                        {
                                            treeList1.Nodes[i].Nodes[j].Nodes[m].Selected = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (treeList1.Nodes[i].Nodes[j][_trName].ToString() == lookUpEdit1.Text)
                                    {
                                        treeList1.Nodes[i].Nodes[j].Selected = true;
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (treeList1.Nodes[i][_trName].ToString() == lookUpEdit1.Text)
                            {
                                treeList1.Nodes[i].Selected = true;
                                return;
                            }
                        }

                    }
                }
            }
        }
        private void SetNode(DevExpress.XtraTreeList.Nodes.TreeListNode tn,string selectstr)
        {
            
                for(int i=0;i<tn.Nodes.Count;i++)
                {
                    object o = tn.Nodes[i][_trName];
                    if(tn.Nodes[i].HasChildren)
                    {
                        SetNode(tn.Nodes[i],selectstr);
                    }
                    else
                    {
                        if(tn.Nodes[i][_trName].ToString()==selectstr)
                        {
                            //treeList1.Nodes[i].Selected;
                            tn.Selected = true;
                            break;
                        }
                    }
                }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count>0)
            {
                ShowInfo(listView1.SelectedItems[0].Index);
            }
        }

        private void _teGrossPro_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellDisplayText(_coChildMaterielID) != string.Empty && gridView1.FocusedRowHandle < gridView1.RowCount - 1 && gridView1.FocusedColumn == _coSupplierName)
            {
                int _m = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coChildMaterielID));
                DataRow[] drs = BasicClass.BaseTableClass.dtMateriel.Select("(ID=" + _m + ")");
                if(drs.Length==1)
                {
                    BasicClass.cResult rM=new BasicClass.cResult();
                    rM.TextChanged += rM_TextChanged;
                    Form fr = new BaseForm.frMaterielOne(rM, Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coChildMaterielID)), BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetList", new object[] { "(ID=" + _m + ")" }).Tables[0]);
                    fr.ShowDialog();
                }
            }
        }

        void rM_TextChanged(string s)
        {
            if (s.IndexOf(',') > -1)
            {
                string[] ss = s.Split(',');
                DataTable dtMC = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielCompany", "GetList", new object[] { "(ID=" + ss[0] + ")" }).Tables[0];
                if (dtMC.Rows.Count > 0)
                {
                    gridView1.SetFocusedRowCellValue(_coSupplierID, dtMC.Rows[0]["CompanyID"]);
                    gridView1.SetFocusedRowCellValue(_coSupplierName, ss[1]);
                    gridView1.SetFocusedRowCellValue(_coSupplierSN, dtMC.Rows[0]["CompanySN"]);
                    gridView1.SetFocusedRowCellValue(_coPrice, dtMC.Rows[0]["Price"]);
                }
            }
        }

        private void MaterielBom_Activated(object sender, EventArgs e)
        {
            //BasicClass.BaseTableClass.ReAllMateriel();
            //BasicClass.BaseTableClass.ReMateriel();
            //BasicClass.BaseTableClass.ReFinished();
            //BasicClass.BaseTableClass.ReCompany();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.BaseTableClass.ReAllMateriel();
            ShowPic();
        }

        private void _teWorkingMoney_DoubleClick(object sender, EventArgs e)
        {
            if(!t)
            {
                _teWorkingMoney.EditValue = BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductWorkingMain", "GetWorkingMoney", new object[] { MaterielID*-1 });
            }
        }

        private void _barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.AddNew();
        }

        private void _barPrv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _barNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }
    }
}