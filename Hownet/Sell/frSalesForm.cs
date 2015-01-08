using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Hownet.Sell
{
    public partial class frSalesForm : DevExpress.XtraEditors.XtraForm
    {
        public frSalesForm()
        {
            InitializeComponent();
        }
        BindingSource bs = new BindingSource();
        DataTable dtMain = new DataTable();
        DataTable dtSOM = new DataTable();
        DataTable dtSOMI = new DataTable();
        DataTable dtAmount = new DataTable();
        DataTable dtMat = new DataTable();
        //DataTable dtMT = new DataTable();
        //DataTable dtSize = new DataTable();
        //DataTable dtSP = new DataTable();
        string bllSOMI = "";
        string bllSOM = BasicClass.Bllstr.bllSalesOrderMain;
        string bllQP = "";
        int _MainID = 0;
        int _companyID = 0;
        int _upData = 0;
        int _InfoID = 0;
        int _MaxRowID = 0;
        bool _IsVerify = false;
        public frSalesForm(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        private void StockBackForm_Load(object sender, EventArgs e)
        {
            bllSOMI = BasicClass.Bllstr.bllSalesOrderInfoList;
            bllQP = BasicClass.Bllstr.bllQuotePrice;
            ShowBase();
            if (_MainID == 0)
            {
                InData();
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                bs.DataSource = dtMain;
            }
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            bs.MoveLast();
            if (bs.Position == 0)
                ShowView(0);
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
              _barUnVerify .Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        private void ShowBase()
        {
            //_leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _lePackID.FormName = (int)BasicClass.Enums.TableType.PackingMethod;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            //_coIsVerify.ColumnEdit = BaseForm.RepositoryItem._reIsExc;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            DataTable dtIsVerify = new DataTable();
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
           repositoryItemLookUpEdit1.DataSource = dtIsVerify;
            dtMat = ((DataView)(BaseForm.RepositoryItem._reProduce.DataSource)).Table;
            ucGridLookup1.DisplayMember = "Name";
            ucGridLookup1.ValueMember = "ID";
            ucGridLookup1.TypeID = (int)BasicClass.Enums.TableType.Company;
        }
        private void ShowView(int p)
        {
            _barFrist.Enabled = _barPve.Enabled = _barNext.Enabled = _barLast.Enabled = true;
            if (p == 0)
                _barFrist.Enabled = _barPve.Enabled = false;
            if (p == dtMain.Rows.Count - 1)
                _barNext.Enabled = _barLast.Enabled = false;
            _IsVerify = false;
           // this._leCompany.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            dtSOM.Rows.Clear();
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtSOM = BasicClass.GetDataSet.GetDS(bllSOM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            }
            else
            {
                dtSOM = BasicClass.GetDataSet.GetDS(bllSOM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtSOM.NewRow();
                dr["ID"] = dr["A"] = _MainID = 0;
                dr["CompanyID"] = 0;
                dr["DateTime"] = dr["FillDate"] = BasicClass.GetDataSet.GetDateTime();
                dr["Remark"] = "";
                dr["Num"] = 0;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyMan"] = 0;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["UpData"] = 1;
                dtSOM.Rows.Add(dr);
            }
            _IsVerify = (int.Parse(dtSOM.Rows[0]["IsVerify"].ToString()) > 2);
            _upData = int.Parse(dtSOM.Rows[0]["UpData"].ToString());
            memoEdit1.EditValue = dtSOM.Rows[0]["Remark"];
            dateEdit1.EditValue = dtSOM.Rows[0]["DateTime"];
            _companyID = int.Parse(dtSOM.Rows[0]["CompanyID"].ToString()); //_leCompany.editVal =
            ucGridLookup1.Values = _companyID;
            _barSave.Enabled = _barDel.Enabled = _barVerify.Enabled = !_IsVerify;
            radioGroup1.Visible = _barAddNew.Enabled = (_MainID > 0);
            amountList1.IsShowPopupMenu = !_IsVerify;
            userNum1.IsCanEdit = (_MainID == 0);
            userNum1.ClearData();
            userNum1.NumStr = "DHHT";
            userNum1.Num = Convert.ToInt32(dtSOM.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtSOM.Rows[0]["DateTime"]);
            //_leCompany.IsNotCanEdit = _IsVerify;
            dateEdit1.Properties.ReadOnly = _IsVerify;
            //if (_MainID > 0)
            //    userNum1.LastEdit = dtSOM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtSOM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            //if (_IsVerify)
            //    userNum1.VerifyUser = dtSOM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtSOM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");
           
            checkEdit1.Enabled = !_IsVerify;
            checkEdit1.Checked = true;
            memoEdit1.Focus();
            dtSOMI = BasicClass.GetDataSet.GetDS(bllSOMI, "GetList", new object[] { "(MainID=" + _MainID + ") order by ID" }).Tables[0];
            dtAmount = BasicClass.GetDataSet.GetDS(bllSOM, "GetAmount", new object[] { _MainID, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
            if (!_IsVerify)
            {
                DataRow dr = dtSOMI.NewRow();
                for (int i = 0; i < dtSOMI.Columns.Count; i++)
                {
                    if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }
                   else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = "";
                    }
                   else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.DateTime"))
                    {
                        dr[i] = BasicClass.GetDataSet.GetDateTime();
                    }
                    else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.Boolean"))
                    {
                        dr[i] = false;
                    }
                    else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                    {
                        dr[i] = 0;
                    }
                }
                dr["A"] = 3;
                dr["RowID"] = (dtSOMI.Rows.Count + 1) * -1;
                dtSOMI.Rows.Add(dr.ItemArray);
                dr["RowID"] = (dtSOMI.Rows.Count + 1) * -1;
                dtSOMI.Rows.Add(dr.ItemArray);
            }
            gridControl1.DataSource = dtSOMI;
            _MaxRowID = (dtSOMI.Rows.Count + 1) * -1;
            amountList1.ClearData();
            if (advBandedGridView1.RowCount > 0)
            {
                ShowInfo(0,-1);
            }
            advBandedGridView1.OptionsBehavior.Editable = !_IsVerify;
            amountList1.IsCanEdit = !_IsVerify;
           // this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            userNum1.IsCanEdit = (_MainID == 0);
            userNum1.ClearData();
            userNum1.NumStr = "SCZD";
            userNum1.Num = Convert.ToInt32(dtSOM.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtSOM.Rows[0]["DateTime"]);
            if (_MainID > 0)
                userNum1.LastEdit = dtSOM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtSOM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            if (_IsVerify)
                userNum1.VerifyUser = dtSOM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtSOM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");
      
            ucGridLookup1.IsReadOnly = _IsVerify;
        }
        private void ShowInfo(int RowID,int PrveRowID)
        {
            if (advBandedGridView1.RowCount > 0)
            {
                _InfoID = Convert.ToInt32(advBandedGridView1.GetRowCellValue(RowID, _coID));
                int _RowID = Convert.ToInt32(advBandedGridView1.GetRowCellValue(RowID, _coRowID));
                DataRow[] drs;
                int _PrveRowID = Convert.ToInt32(advBandedGridView1.GetRowCellValue(PrveRowID, _coRowID)); 
                if (PrveRowID > -1 && Convert.ToInt32(advBandedGridView1.GetRowCellValue(PrveRowID, _coA)) > 1)
                {
                    if (Convert.ToInt32(advBandedGridView1.GetRowCellValue(PrveRowID, _coSumAmount)) != amountList1.SumAmount)
                    {
                        advBandedGridView1.SetRowCellValue(PrveRowID, _coSumAmount, amountList1.SumAmount);
                        advBandedGridView1.SetRowCellValue(PrveRowID, _coMoney, (Convert.ToDecimal(advBandedGridView1.GetRowCellValue(PrveRowID, _coPrice)) * amountList1.SumAmount));
                    }
                    drs = dtAmount.Select("(RowID=" + advBandedGridView1.GetRowCellValue(PrveRowID, _coRowID) + ")");
                    for (int i = 0; i < drs.Length; i++)
                    {
                        drs[i].Delete();
                    }
                    dtAmount.AcceptChanges();
                    DataTable dtt = amountList1.GetDTList();
                    int _ID = Convert.ToInt32(advBandedGridView1.GetRowCellValue(PrveRowID, _coID));
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        DataRow dr = dtAmount.NewRow();
                        dr["ColorID"] = dtt.Rows[i]["ColorID"];
                        dr["ColorOneID"] = dtt.Rows[i]["ColorOneID"];
                        dr["ColorTwoID"] = dtt.Rows[i]["ColorTwoID"];
                        dr["SizeID"] = dtt.Rows[i]["SizeID"];
                        dr["Amount"] = dtt.Rows[i]["Amount"];
                        dr["MListID"] = 0;
                        dr["NotAmount"] = dtt.Rows[i]["Amount"]; ;
                        dr["NotDepAmount"] = dtt.Rows[i]["Amount"]; ;
                        dr["Remark"] = "";
                        dr["RowID"] = _PrveRowID;
                        dr["MainID"] = _ID;
                        dr["ID"] = 0;
                        dr["TableTypeID"] = (int)BasicClass.Enums.TableType.SalesOne;
                        dr["A"] = 1;
                        dtAmount.Rows.Add(dr);
                    }

                }
                drs = dtSOMI.Select("(RowID=" + _PrveRowID + ")");
                if (drs.Length > 0)
                {
                    if (!drs[0]["PackingMethodID"].Equals(_lePackID.editVal) || !drs[0]["Remark"].Equals(labAndText1.EditVal) || !drs[0]["SewingRemark"].Equals(_meSewRemark.EditValue))
                    {
                        drs[0]["PackingMethodID"] = _lePackID.editVal;
                        drs[0]["Remark"] = labAndText1.EditVal;
                        drs[0]["SewingRemark"] = _meSewRemark.EditValue;
                        dtSOMI.AcceptChanges();
                        if (Convert.ToInt32(advBandedGridView1.GetRowCellValue(RowID, _coA)) == 1)
                            advBandedGridView1.SetRowCellValue(RowID,_coA, 2);
                    }
                }
                DataTable dt = dtAmount.Clone();
                amountList1.ClearData();
                drs = dtAmount.Select("(RowID=" +_RowID + ")");// advBandedGridView1.GetRowCellValue(RowID, _coRowID)
                if (drs.Length > 0)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        dt.Rows.Add(drs[i].ItemArray);
                    }
                }
                else if(_InfoID>0)
                {
                    drs = dtAmount.Select("(MainID=" + _InfoID + ")");
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i]["RowID"] = _RowID;
                            dt.Rows.Add(drs[i].ItemArray);
                        }
                    }
                }
                amountList1.Open(!_IsVerify, dt);
                drs = dtSOMI.Select("(RowID=" + _RowID + ")");
                if (drs.Length > 0)
                {
                    _lePackID.editVal = Convert.ToInt32(drs[0]["PackingMethodID"]);
                    labAndText1.EditVal = drs[0]["Remark"];
                    _meSewRemark.EditValue = drs[0]["SewingRemark"];
                }
            }
        }
        private void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }

        private void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSOM, "GetIDList", null).Tables[0];
            if (dtMain.Rows.Count == 0)
            {
                dtMain.Rows.Add(dtMain.NewRow());
            }
            bs.DataSource = dtMain;
        }
        #region 记录移动
        private void _barPve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }

        private void _barNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }

        private void _barLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool f = (_MainID == 0);
            if (Save(false))
            {
                if (f)
                {
                    int d = bs.Position;
                    InData();
                    bs.Position = d;
                    ShowView(d);
                }
            }
        }
        private bool Save(bool IsVerify)
        {
            amountList1.EndEdit();
            advBandedGridView1.FocusedRowHandle = (advBandedGridView1.RowCount - 1);
            if (_companyID == 0)
            {
                XtraMessageBox.Show("请选择客户！");
                return false;
            }
             dtSOM.Rows[0]["Remark"]=memoEdit1.EditValue ;
             dtSOM.Rows[0]["CompanyID"] = _companyID;
            if (IsVerify)
            {
                dtSOM.Rows[0]["IsVerify"] = 3;
                dtSOM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
                dtSOM.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
            }
            if (_MainID == 0)
            {
                dtSOM.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSOM, "NewNum", new object[] { userNum1.NumDate });
                dtMain.Rows[bs.Position]["ID"] = dtSOM.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSOM, dtSOM);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllSOM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtSOM.Rows[0]["UpData"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllSOM, dtSOM);
                }
            }
            int a = 1;
            DataTable dttt;
            DataTable dttA;
            DataRow[] drs;
            int _materielID, _brandID, _colorID, _colorOneID, _colorTwoID, _measureID, _sizeID;
            for (int i = 0; i < dtSOMI.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtSOMI.Rows[i]["MaterielID"]) > 0)
                {
                    a = Convert.ToInt32(dtSOMI.Rows[i]["A"]);
                    if (IsVerify)
                    {
                        dtSOMI.Rows[i]["IsVerify"] = 3;
                        dtSOMI.Rows[i]["VerifyMan"] = BasicClass.UserInfo.UserID;
                        dtSOMI.Rows[i]["VerifyDate"] = dtSOM.Rows[0]["VerifyDate"];
                    }
                    if (a > 1)
                    {
                        dtSOMI.Rows[i]["CompanyID"] = ucGridLookup1.Values;// _leCompany.editVal;
                        dttt = dtSOMI.Clone();
                        if (a == 2)
                        {
                            dttt.Rows.Add(dtSOMI.Rows[i].ItemArray);
                            BasicClass.GetDataSet.UpData(bllSOMI, dttt);
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { Convert.ToInt32(dtSOMI.Rows[0]["ID"]), (int)BasicClass.Enums.TableType.SalesOne });
                            drs = dtAmount.Select("(RowID=" + dtSOMI.Rows[i]["RowID"] + ")");
                            if (drs.Length > 0)
                            {
                                for (int j = 0; j < drs.Length; j++)
                                {
                                    if (Convert.ToInt32(drs[j]["MListID"]) == 0)
                                    {
                                        _materielID = Convert.ToInt32(dtSOMI.Rows[i]["MaterielID"]);
                                        _colorID = Convert.ToInt32(drs[j]["ColorID"]);
                                        _colorOneID = Convert.ToInt32(drs[j]["ColorOneID"]);
                                        _colorTwoID = Convert.ToInt32(drs[j]["ColorTwoID"]);
                                        _brandID = Convert.ToInt32(dtSOMI.Rows[i]["BrandID"]);
                                        _sizeID = Convert.ToInt32(drs[j]["SizeID"]);
                                        _measureID = Convert.ToInt32(dtSOMI.Rows[i]["MeasureID"]);
                                        drs[j]["MListID"] = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllMaterielList, "GetMLID", new object[] { _materielID, _colorID, _colorOneID, _colorTwoID, _sizeID, _brandID, _measureID,0 });
                                    }
                                    drs[j]["MainID"] = dtSOMI.Rows[i]["ID"];
                                    dttA = dtAmount.Clone();
                                    dttA.Rows.Add(drs[j].ItemArray);
                                    BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllAmountInfo, dttA);
                                }
                            }
                        }
                        else if (a == 3)
                        {
                            dtSOMI.Rows[i]["DateTime"] = dtSOM.Rows[0]["DateTime"];
                            dtSOMI.Rows[i]["Num"] = BasicClass.GetDataSet.GetOne(bllSOMI, "NewNum", new object[] { dateEdit1.DateTime });
                            dtSOMI.Rows[i]["MainID"] = _MainID;
                            dttt.Rows.Add(dtSOMI.Rows[i].ItemArray);
                            dtSOMI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllSOMI, dttt);
                            drs = dtAmount.Select("(RowID=" + dtSOMI.Rows[i]["RowID"] + ")");
                            if (drs.Length > 0)
                            {
                                for (int j = 0; j < drs.Length; j++)
                                {
                                    if (Convert.ToInt32(drs[j]["MListID"]) == 0)
                                    {
                                        _materielID = Convert.ToInt32(dtSOMI.Rows[i]["MaterielID"]);
                                        _colorID = Convert.ToInt32(drs[j]["ColorID"]);
                                        _colorOneID = Convert.ToInt32(drs[j]["ColorOneID"]);
                                        _colorTwoID = Convert.ToInt32(drs[j]["ColorTwoID"]);
                                        _brandID = Convert.ToInt32(dtSOMI.Rows[i]["BrandID"]);
                                        _sizeID = Convert.ToInt32(drs[j]["SizeID"]);
                                        _measureID = Convert.ToInt32(dtSOMI.Rows[i]["MeasureID"]);
                                        drs[j]["MListID"] = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllMaterielList, "GetMLID", new object[] { _materielID, _colorID, _colorOneID, _colorTwoID, _sizeID, _brandID, _measureID,0 });
                                    }
                                    drs[j]["MainID"] = dtSOMI.Rows[i]["ID"];
                                    dttA = dtAmount.Clone();
                                    dttA.Rows.Add(drs[j].ItemArray);
                                    BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllAmountInfo, dttA);
                                }
                            }
                        }
                        dtSOMI.Rows[i]["A"] = 1;
                    }
                    else
                    {
                        dttt = dtSOMI.Clone();
                        dttt.Rows.Add(dtSOMI.Rows[i].ItemArray);
                        BasicClass.GetDataSet.UpData(bllSOMI, dttt);
                    }
                }
            }
            return true;
        }
        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("确认审核？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                Save(true);
                ShowView(bs.Position);
            }
        }
        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtt = dtSOMI.Clone();
            for (int i = 0; i < dtSOMI.Rows.Count; i++)
            {
                if (int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduceSellOne, "CountSellSales", new object[] { Convert.ToInt32(dtSOMI.Rows[i]["ID"]) }).ToString()) > 0)
                {
                    XtraMessageBox.Show("本订单已有发货记录，不能弃审！");
                    return;
                }
                if (Convert.ToBoolean(dtSOMI.Rows[i]["IsToPlan"]))
                {
                    XtraMessageBox.Show("本订单已转入计划单，不能弃审！");
                    return;
                }
                if (Convert.ToInt32(dtSOMI.Rows[i]["IsVerify"]) > 3)
                {
                    XtraMessageBox.Show("当前状态不能弃审！");
                    return;
                }
            }
            for (int i = 0; i < dtSOMI.Rows.Count; i++)
            {
                BasicClass.GetDataSet.ExecSql(bllSOMI, "DelPlan", new object[] { Convert.ToInt32(dtSOMI.Rows[i]["ID"]), (int)BasicClass.Enums.TableType.ProductionPlan });
                dtSOMI.Rows[i]["IsVerify"] = 1;
                dtSOMI.Rows[i]["VerifyMan"] = 0;
                dtSOMI.Rows[i]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
                dtt = dtSOMI.Clone();
                dtt.Rows.Add(dtSOMI.Rows[i].ItemArray);
                BasicClass.GetDataSet.UpData(bllSOMI, dtt);
            }
            dtSOM.Rows[0]["IsVerify"] = 1;
            dtSOM.Rows[0]["VerifyMan"] = 0;
            dtSOM.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
            BasicClass.GetDataSet.UpData(bllSOM, dtSOM);
            ShowView(bs.Position);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amountList1.IsCanEdit = !_IsVerify;
            amountList1.Open(Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coID)) , (int)BasicClass.Enums.TableType.SalesOne, true, radioGroup1.SelectedIndex + 1);
            if (radioGroup1.SelectedIndex > 0)
            {
                amountList1.IsCanEdit = false;
            }
        }

        private void _leCompany_EditValueChanged(object val, string text)
        {
            if (!_IsVerify)
            {
                _companyID = int.Parse(val.ToString());
                SetPrice();
            }
        }

        private void amountList1_EditValueChanged(object val, string text)
        {
           // _ltAmount.val = amountList1.SumAmount.ToString();
            advBandedGridView1.SetFocusedRowCellValue(_coSumAmount,val);
            advBandedGridView1.SetFocusedRowCellValue(_coMoney, (Convert.ToDecimal(advBandedGridView1.GetFocusedRowCellValue(_coPrice)) * Convert.ToDecimal(val)).ToString("n2"));
            //SetPrice();
        }

        private void SetPrice()
        {
            int _materielID = 0;// Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coMaterielID));
            int _brandID = 0;// Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coBrandID));
            int _measureID = 0;// Convert.ToInt32(drs[0]["MeasureID"]);
            int _MTID = 0;// Convert.ToInt32(drs[0]["TypeID"]);
            decimal _price = 0;
            //advBandedGridView1.SetFocusedRowCellValue(_coPrice, BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllQuotePrice, "GetPrice", new object[] { _companyID, _materielID, _brandID, _measureID, _MTID }));
            DataRow[] drs;
            for (int i = 0; i < advBandedGridView1.RowCount; i++)
            {
                _materielID = Convert.ToInt32(advBandedGridView1.GetRowCellValue(i, _coMaterielID));
                _brandID = Convert.ToInt32(advBandedGridView1.GetRowCellValue(i, _coBrandID));
                if (_materielID > 0 && _brandID > 0)
                {
                    drs = dtMat.Select("(ID=" + _materielID + ")");
                    if (drs.Length > 0)
                    {
                        _measureID = Convert.ToInt32(drs[0]["MeasureID"]);
                        _MTID = Convert.ToInt32(drs[0]["TypeID"]);
                        _price = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllQuotePrice, "GetPrice", new object[] { _companyID, _materielID, _brandID, _measureID, _MTID }));
                    }
                    else
                    {
                        _price = 0;
                    }
                }
                else
                {
                    _price = 0;
                }
                advBandedGridView1.SetRowCellValue(i, _coPrice, _price);
            }
        }
        private void _barAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }


        private void _barToPlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_MainID == 0)
            {
                dtMain.Rows.RemoveAt(dtMain.Rows.Count - 1);
                // ShowInfo(dtMain.Rows.Count - 1);
            }
            else
            {

                if (XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelSalesBySaleID", new object[] {_MainID,(int)BasicClass.Enums.TableType.SalesOne });
                    BasicClass.GetDataSet.ExecSql(bllSOMI, "DeleteByMainID", new object[] { _MainID });
                    BasicClass.GetDataSet.ExecSql(bllSOM, "Delete", new object[] {_MainID });
                    if (dtMain.Rows.Count > 0)
                    {
                        dtMain.Rows.RemoveAt(bs.Position);
                    }
                    else
                    {
                        InData();
                        ShowView(0);
                    }
                }
            }
        }

        private void advBandedGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
                ShowInfo(e.FocusedRowHandle,e.PrevFocusedRowHandle);
            if (!_IsVerify)
            {
                amountList1.IsCanEdit = advBandedGridView1.OptionsBehavior.Editable = e.FocusedRowHandle < advBandedGridView1.RowCount - 1;
            }
        }

        private void advBandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(advBandedGridView1.GetRowCellValue(e.RowHandle, _coA)) == 1)
                advBandedGridView1.SetRowCellValue(e.RowHandle, _coA, 2);
            if (e.Column==_coMaterielID&& e.RowHandle == advBandedGridView1.RowCount - 2)
            {
                DataRow dr = dtSOMI.NewRow();
                for (int i = 0; i < dtSOMI.Columns.Count; i++)
                {
                    if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }
                    else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = "";
                    }
                    else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.DateTime"))
                    {
                        dr[i] = BasicClass.GetDataSet.GetDateTime();
                    }
                    else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.Boolean"))
                    {
                        dr[i] = false;
                    }
                    else if (dtSOMI.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                    {
                        dr[i] = 0;
                    }
                }
                dr["A"] = 3;
                dr["RowID"] = _MaxRowID;
                _MaxRowID -= 1;
                dtSOMI.Rows.Add(dr.ItemArray);
            }
            if (e.Column == _coMaterielID || e.Column == _coBrandID)
            {
                DataRow[] drs = dtMat.Select("(ID=" + advBandedGridView1.GetFocusedRowCellValue(_coMaterielID) + ")");
                if (drs.Length > 0)
                {
                    int _materielID=Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coMaterielID));
                    int _brandID=Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coBrandID));
                    int _measureID = Convert.ToInt32(drs[0]["MeasureID"]);
                    advBandedGridView1.SetFocusedRowCellValue(_coMeasureID, _measureID);
                    int _MTID = Convert.ToInt32(drs[0]["TypeID"]);
                    advBandedGridView1.SetFocusedRowCellValue(_coPrice, BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllQuotePrice, "GetPrice", new object[] {_companyID,_materielID,_brandID,_measureID,_MTID }));
                }
                if (e.Column == _coMaterielID&&Convert.ToInt32(e.Value)>0) 
                {
                    amountList1.Open(true, BasicClass.GetDataSet.GetDS(bllSOM, "GetCS2RepByMatID", new object[] {Convert.ToInt32(e.Value) }).Tables[0]);
                }
            }
            if (e.Column == _coPrice || e.Column == _coSumAmount)
            {
                advBandedGridView1.SetFocusedRowCellValue(_coMoney, Convert.ToDecimal(advBandedGridView1.GetFocusedRowCellValue(_coPrice)) * Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coSumAmount)));
            }
        }
        //删除明细
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (advBandedGridView1.FocusedRowHandle > -1 && advBandedGridView1.FocusedRowHandle < advBandedGridView1.RowCount - 2)
            {
                if (XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    _InfoID = Convert.ToInt32(advBandedGridView1.GetFocusedRowCellValue(_coID));
                    if (_InfoID > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _InfoID, (int)BasicClass.Enums.TableType.SalesOne });
                        DataRow[] drs = dtAmount.Select("(RowID=" + advBandedGridView1.GetFocusedRowCellValue(_coRowID) + ")");
                        for (int i = 0; i < drs.Length; i++)
                        {
                            drs[i].Delete();
                        }
                        dtAmount.AcceptChanges();
                        BasicClass.GetDataSet.ExecSql(bllSOMI, "Delete", new object[] { _InfoID });
                    }
                    //  amountList1.ClearData();
                    DataTable dt = dtAmount.Clone();
                    amountList1.Open(true, dt);
                    advBandedGridView1.DeleteRow(advBandedGridView1.FocusedRowHandle);
                    dtSOMI.AcceptChanges();
                }
                advBandedGridView1.FocusedRowHandle = (advBandedGridView1.RowCount - 1);
            }
        }

        private void advBandedGridView1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print(false);
        }
        private void Print(bool IsDesign)
        {
            if (!_IsVerify)
            {
                XtraMessageBox.Show("未审核！");
                return;
            }
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";

            DataTable dtOne = new DataTable();
            dtOne.TableName = "Main";
            dtOne.Columns.Add("编号", typeof(string));
            dtOne.Columns.Add("客户", typeof(string));
            dtOne.Columns.Add("说明", typeof(string));
            dtOne.Columns.Add("制单", typeof(string));
            dtOne.Columns.Add("审核", typeof(string));
            dtOne.Columns.Add("日期", typeof(string));
            DataRow drOne = dtOne.NewRow();
            drOne[0] =userNum1.NumStr;
            drOne[1] = ucGridLookup1.StringValues;// _leCompany.valStr;
            drOne[2] = memoEdit1.Text;
            if (Convert.ToInt32(dtSOM.Rows[0]["FillMan"]) > 0)
                drOne[3] = dtUser.Select("(ID=" + Convert.ToInt32(dtSOM.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
            if (Convert.ToInt32(dtSOM.Rows[0]["VerifyMan"]) > 0)
                drOne[4] = dtUser.Select("(ID=" + Convert.ToInt32(dtSOM.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            drOne[5] = userNum1.NumDate.ToString("yyyy年MM月dd日");
            dtOne.Rows.Add(drOne);
            ds.Tables.Add(dtOne);

            DataTable dtTwo = new DataTable();
            dtTwo.TableName = "List";
            dtTwo.Columns.Add("编号", typeof(string));
            dtTwo.Columns.Add("款号", typeof(string));
            dtTwo.Columns.Add("商标", typeof(string));
            dtTwo.Columns.Add("数量", typeof(decimal));
            dtTwo.Columns.Add("单价", typeof(decimal));
            dtTwo.Columns.Add("金额", typeof(decimal));
            dtTwo.Columns.Add("货期", typeof(string));
            dtTwo.Columns.Add("单位", typeof(string));
            dtTwo.Columns.Add("ID", typeof(int));
            for (int i = 0; i < advBandedGridView1.RowCount; i++)
            {
                DataRow drTwo = dtTwo.NewRow();
                drTwo[0] = advBandedGridView1.GetRowCellDisplayText(i,_coNum);
                drTwo[1] = advBandedGridView1.GetRowCellDisplayText(i, _coMaterielID);
                drTwo[2] = advBandedGridView1.GetRowCellDisplayText(i, _coBrandID);
                drTwo[3] = advBandedGridView1.GetRowCellValue(i, _coSumAmount);
                drTwo[4] = advBandedGridView1.GetRowCellValue(i, _coPrice);
                drTwo[5] = advBandedGridView1.GetRowCellValue(i, _coMoney);
                drTwo[6] = advBandedGridView1.GetRowCellDisplayText(i, _coLastDate);
                drTwo[7] = advBandedGridView1.GetRowCellDisplayText(i, _coMeasureID);
                drTwo[8] = advBandedGridView1.GetRowCellValue(i, _coID);
                dtTwo.Rows.Add(drTwo);
            }
            ds.Tables.Add(dtTwo);
            
            DataTable dtThree = new DataTable();
            if (dtTwo.Rows.Count > 0)
                dtThree = BasicClass.OrderTask.ShowTemInfo(Convert.ToInt32(dtTwo.Rows[0]["ID"]), (int)BasicClass.Enums.TableType.SalesOne);
            if (dtTwo.Rows.Count > 1)
            {
                for (int i = 1; i < dtTwo.Rows.Count; i++)
                {
                    DataTable dtTem = BasicClass.OrderTask.ShowTemInfo(Convert.ToInt32(dtTwo.Rows[i]["ID"]), (int)BasicClass.Enums.TableType.SalesOne);
                    for (int j = 0; j < dtTem.Rows.Count; j++)
                    {
                        try
                        {
                            dtThree.Rows.Add(dtTem.Rows[j].ItemArray);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            dtThree.TableName = "Info";
            ds.Tables.Add(dtThree);
            BaseForm.PrintClass.PrintSaleslist(ds, IsDesign);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print(true);
        }

        private void ucGridLookup1_EditValueChanged(object val, string text)
        {
            if (!_IsVerify)
            {
                _companyID = int.Parse(val.ToString());
                SetPrice();
            }
        }

    }
}