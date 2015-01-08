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
    public partial class SalesForm : DevExpress.XtraEditors.XtraForm
    {
        public SalesForm()
        {
            InitializeComponent();
        }
        BindingSource bs = new BindingSource();
        DataTable dtMain = new DataTable();
        DataTable dtSOMI = new DataTable();
        DataTable dtMT = new DataTable();
        DataTable dtSize = new DataTable();
        DataTable dtSP = new DataTable();
        string bllSOMI = "";
        string bllQP = "";
        string per = string.Empty;
        int _MainID = 0;
        int _companyID = 0;
        int _upData = 0;
        int _measureID = 0;
        bool t = false;
        public SalesForm(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        private void StockBackForm_Load(object sender, EventArgs e)
        {

            per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            {
                _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barSubItem1.Enabled = false;
            }
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVerify .Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

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
                ShowInfo(0);

        }
        private void ShowBase()
        {
            //_leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _leMateriel.FormName = (int)BasicClass.Enums.TableType.Product;
            _leBrand.FormName = (int)BasicClass.Enums.TableType.Brand;
            _lePackID.FormName = (int)BasicClass.Enums.TableType.PackingMethod;
            dtSOMI = BasicClass.GetDataSet.GetDS(bllSOMI, "GetList", new object[] { "(ID=0)" }).Tables[0];
            dtMT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] { "ParentID=4" }).Tables[0];
            dtSize = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSize, "GetAllList", null).Tables[0];
            repositoryItemLookUpEdit1.DataSource = dtMT;
            ucGridLookup1.DisplayMember = "Name";
            ucGridLookup1.ValueMember = "ID";
            ucGridLookup1.TypeID = (int)BasicClass.Enums.TableType.Company;
        }
        private void ShowInfo(int p)
        {
            _barFrist.Enabled = _barPve.Enabled = _barNext.Enabled = _barLast.Enabled = true;
            if (p == 0)
                _barFrist.Enabled = _barPve.Enabled = false;
            if (p == dtMain.Rows.Count - 1)
                _barNext.Enabled = _barLast.Enabled = false;
            t = false;
        //    this._leCompany.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            this._leMateriel.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leMateriel_EditValueChanged);
            this._leBrand.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leBrand_EditValueChanged);
            dtSOMI.Rows.Clear();
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtSOMI = BasicClass.GetDataSet.GetDS(bllSOMI, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            }
            else
            {
                DataRow dr = dtSOMI.NewRow();
                dr["ID"] = dr["A"] =  dr["MaterielID"] = dr["BrandID"] = dr["MeasureID"] = dr["Price"] = dr["Progress"] = _MainID = 0;
                dr["MainID"] = -1;
                dr["PackingMethodID"] = dr["CompanyID"] = 0;
                dr["DateTime"] = dr["FillDate"] = DateTime.Today;
                dr["SewingRemark"] = dr["Remark"] = "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllSOMI, "NewNum", new object[] { DateTime.Today });
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["LastDate"] = DateTime.Today.AddDays(30);
                dr["IsVerify"] = 1;
                dr["VerifyMan"] = 0;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["UpData"] = 1;
                dr["IsToPlan"] = false;
                dtSOMI.Rows.Add(dr);
            }
            t = (int.Parse(dtSOMI.Rows[0]["IsVerify"].ToString()) > 2);
            _upData = int.Parse(dtSOMI.Rows[0]["UpData"].ToString());
            _ltRemark.val = dtSOMI.Rows[0]["Remark"].ToString();
            _ltRemark.IsCanEdit = !t;
            _ltAmount.IsCanEdit = false;
            _ltMoney.IsCanEdit = false;
            _companyID = int.Parse(dtSOMI.Rows[0]["CompanyID"].ToString()); //_leCompany.editVal =
            ucGridLookup1.Values = _companyID;
            _leMateriel.editVal = int.Parse(dtSOMI.Rows[0]["MaterielID"].ToString());
            _leBrand.editVal = int.Parse(dtSOMI.Rows[0]["BrandID"].ToString());
            _ldLastDate.val = (DateTime)(dtSOMI.Rows[0]["LastDate"]);
            _lePackID.editVal = Convert.ToInt32(dtSOMI.Rows[0]["PackingMethodID"]);
            try
            {
                _measureID = int.Parse(BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["MeasureID"].ToString());
            }
            catch
            {
                _measureID = 1;
            }
            _meSewRemark.EditValue = dtSOMI.Rows[0]["SewingRemark"];
            _barSave.Enabled = _barDel.Enabled = _barVerify.Enabled = !t;
            _barUnVerify.Enabled = t;// && p == dtMain.Rows.Count - 1);
            amountList1.Open(_MainID, (int)BasicClass.Enums.TableType.SalesOne, true, (int)BasicClass.Enums.AmountType.原始数量);
            amountList1.IsCanEdit = !t;
            _ltAmount.val = amountList1.SumAmount.ToString();
            radioGroup1.Visible = _barAddNew.Enabled = (_MainID > 0);
            amountList1.IsShowPopupMenu = !t;

            dtSP = BasicClass.GetDataSet.GetDS("Hownet.BLL.SalesPrice", "GetList", new object[] { "(SalesID=" + _MainID + ")" }).Tables[0];
            gridControl1.DataSource = dtSP;
            gridView1.OptionsBehavior.Editable = !t;
            userNum1.IsCanEdit = (_MainID == 0);
            userNum1.ClearData();
            userNum1.NumStr = "SCZD";
            userNum1.Num = Convert.ToInt32(dtSOMI.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtSOMI.Rows[0]["DateTime"]);
            if (_MainID > 0)
                userNum1.LastEdit = dtSOMI.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtSOMI.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            if (t)
                userNum1.VerifyUser = dtSOMI.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtSOMI.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");
            barSubItem1.Enabled = (Convert.ToInt32(dtSOMI.Rows[0]["IsVerify"]) > 2 && Convert.ToInt32(dtSOMI.Rows[0]["IsVerify"]) < 9);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            {
                barSubItem1.Enabled = false;
            }
          //  this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
            this._leMateriel.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leMateriel_EditValueChanged);
            this._leBrand.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leBrand_EditValueChanged);
            SumMoney();
            checkEdit1.Enabled = !t;
            checkEdit1.Checked = true;
            _ltMoney.Mask = BasicClass.Enums.Mask.金額.ToString();
            _ltRemark.Focus();
 
            ucGridLookup1.IsReadOnly = t;
            _ltPrice.EditVal = dtSOMI.Rows[0]["Price"];
        }
        private void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowInfo(bs.Position);
        }

        private void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSOMI, "GetIDList", null).Tables[0];
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
                    ShowInfo(d);
                }
            }
        }
        private bool Save(bool IsVerify)
        {
            if (_companyID == 0)
            {
                XtraMessageBox.Show("请选择客户！");
                return false;
            }
            if (_leMateriel.editVal.Equals(0))
            {
                XtraMessageBox.Show("请填写款号！");
                return false;
            }
            if (_leBrand.editVal.Equals(0))
            {
                XtraMessageBox.Show("请填写商标！");
                return false;
            }
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSP.AcceptChanges();
            dtSOMI.Rows[0]["CompanyID"] = _companyID;
            dtSOMI.Rows[0]["MaterielID"] = _leMateriel.editVal;
            dtSOMI.Rows[0]["BrandID"] = _leBrand.editVal;
            dtSOMI.Rows[0]["DateTime"] = userNum1.NumDate;
            dtSOMI.Rows[0]["LastDate"] = _ldLastDate.val;
            dtSOMI.Rows[0]["Remark"] = _ltRemark.val;
            dtSOMI.Rows[0]["Price"] = 0;
            dtSOMI.Rows[0]["SunAmount"] = amountList1.SumAmount;
            _measureID = int.Parse(BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["MeasureID"].ToString());
            dtSOMI.Rows[0]["MeasureID"] = _measureID;
            dtSOMI.Rows[0]["PackingMethodID"] = _lePackID.editVal;
            dtSOMI.Rows[0]["SewingRemark"] = _meSewRemark.EditValue;
            if(gridView1.RowCount>0)
            {
                dtSOMI.Rows[0]["Price"] = gridView1.GetRowCellValue(0, _coPrice);
            }
            if (IsVerify)
            {
                dtSOMI.Rows[0]["IsVerify"] = 3;
                dtSOMI.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
                dtSOMI.Rows[0]["VerifyDate"] = BasicClass.GetDataSet.GetDateTime();
            }
            if (_MainID == 0)
            {
                dtSOMI.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSOMI, "NewNum", new object[] { userNum1.NumDate });
                dtMain.Rows[bs.Position]["ID"] = dtSOMI.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSOMI, dtSOMI);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllSOMI, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtSOMI.Rows[0]["UpData"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllSOMI, dtSOMI);
                }
            }
            amountList1.Save(_MainID, int.Parse(_leMateriel.editVal.ToString()), int.Parse(_leBrand.editVal.ToString()));
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.SalesPrice", "DeleteByMainID", new object[] { _MainID });
            DataTable dttt = dtSP.Clone();
            for (int i = 0; i < dtSP.Rows.Count; i++)
            {
                dttt.Rows.Clear();
                dtSP.Rows[i]["SalesID"] = _MainID;
                dttt.Rows.Add(dtSP.Rows[i].ItemArray);
                dtSP.Rows[i]["ID"] = BasicClass.GetDataSet.Add("Hownet.BLL.SalesPrice", dttt);
            }
            return true;
        }
        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToDecimal(_ltMoney.EditVal) < 1)
            {
                XtraMessageBox.Show("金额不正确，请核对后再审核！");
                return;
            }
            if (gridView1.RowCount > 1)
            {
                if (DialogResult.No == XtraMessageBox.Show("本单有一种以上的款式，审核后将不会转入生产计划！是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    return;
                }
            }
            //if (DialogResult.Yes == XtraMessageBox.Show("审核后将自动转入生产计划单", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            //{
            if (Save(true))
            {

                if (gridView1.RowCount == 1)
                {
                    if (checkEdit1.Checked)
                    {
                        if (DialogResult.Yes == XtraMessageBox.Show("请确认是否在审核后将本单转入生产计划？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            int PlanID = Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllSalesOrderInfoList, "ToPlan", new object[] { _MainID, BasicClass.UserInfo.UserID }));
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "SaleToPlan", new object[] { _MainID, PlanID, (int)BasicClass.Enums.TableType.SalesOne, (int)BasicClass.Enums.TableType.ProductionPlan });

                        }
                    }
                    else
                    {
                        if (DialogResult.No == XtraMessageBox.Show("请确认不转入生产计划？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            return;
                        }
                    }
                }
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    BasicClass.GetDataSet.ExecSql(bllQP, "UpPrice", new object[] { _companyID, Convert.ToInt32(_leMateriel.editVal), Convert.ToInt32(_leBrand.editVal), Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice)), _measureID, Convert.ToInt32(gridView1.GetRowCellValue(i, _coMTID)) });
                }
                ShowInfo(bs.Position);
            }
            //}
        }
        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProduceSellOne, "CountSellSales", new object[] { _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本订单已有发货记录，不能弃审！");
                return;
            }
            if (Convert.ToBoolean(dtSOMI.Rows[0]["IsToPlan"]))
            {
                XtraMessageBox.Show("本订单已转入计划单，不能弃审！");
                return;
            }
            if (Convert.ToInt32(dtSOMI.Rows[0]["IsVerify"]) > 3)
            {
                XtraMessageBox.Show("当前状态不能弃审！");
                return;
            }
            BasicClass.GetDataSet.ExecSql(bllSOMI, "DelPlan", new object[] { _MainID, (int)BasicClass.Enums.TableType.ProductionPlan });
            dtSOMI.Rows[0]["IsVerify"] = 1;
            dtSOMI.Rows[0]["VerifyMan"] = 0;
            dtSOMI.Rows[0]["VerifyDate"] = DateTime.Today;
            BasicClass.GetDataSet.UpData(bllSOMI, dtSOMI);
            ShowInfo(bs.Position);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amountList1.IsCanEdit = !t;
            amountList1.Open(_MainID, (int)BasicClass.Enums.TableType.SalesOne, true, radioGroup1.SelectedIndex + 1);
            if (radioGroup1.SelectedIndex > 0)
            {
                amountList1.IsCanEdit = false;
            }
        }

        private void _leCompany_EditValueChanged(object val, string text)
        {
            if (!t)
            {
                _companyID = int.Parse(val.ToString());
                SetPrice();
            }
        }

        private void amountList1_EditValueChanged(object val, string text)
        {
            _ltAmount.val = amountList1.SumAmount.ToString();
            SetPrice();
        }

        private void _leMateriel_EditValueChanged(object val, string text)
        {
            if (!t)
            {
                if (!val.Equals(0))
                    _measureID = int.Parse(BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + val + ")" }).Tables[0].Rows[0]["MeasureID"].ToString());
                SetPrice();
                amountList1.Open(true, BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderMain, "GetCS2RepByMatID", new object[] { Convert.ToInt32(val) }).Tables[0]);
            }
        }

        private void _leBrand_EditValueChanged(object val, string text)
        {
            if (!t)
                SetPrice();
        }

        private void _leMeasureID_EditValueChanged(object val, string text)
        {
        }
        private void SetPrice()
        {
            DataTable dttt = amountList1.GetDTList();
            bool f = false;
            dtSP.Rows.Clear();
            int MTID = 0;
            int Amount = 0;
            decimal Price = 0;
            decimal moeney = 0;
            if (dttt.Rows.Count > 0)
            {
                for (int i = 0; i < dttt.Rows.Count; i++)
                {
                    f = false;
                    MTID = Convert.ToInt32(dtSize.Select("(ID=" + dttt.Rows[i]["SizeID"] + ")")[0]["SizeTypeID"]);
                    for (int j = 0; j < dtSP.Rows.Count; j++)
                    {
                        if (MTID == Convert.ToInt32(dtSP.Rows[j]["MTID"]))
                        {
                            f = true;
                            break;
                        }
                    }
                    if (!f)
                    {//1 as A,ID,MTID,Price,SalesID,Amount,Money,Remark
                        dtSP.Rows.Add(0, 0, MTID, 0, _MainID, 0, 0, "");
                    }
                }
                for (int i = 0; i < dtSP.Rows.Count; i++)
                {
                    MTID = Convert.ToInt32(dtSP.Rows[i]["MTID"]);
                    Amount = 0;
                    for (int j = 0; j < dttt.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(dtSize.Select("(ID=" + dttt.Rows[j]["SizeID"] + ")")[0]["SizeTypeID"]) == MTID)
                        {
                            Amount += Convert.ToInt32(dttt.Rows[j]["Amount"]);
                        }
                    }
                    dtSP.Rows[i]["Amount"] = Amount;
                    if (_companyID > 0 && Convert.ToInt32(_leMateriel.editVal) > 0 && Convert.ToInt32(_leBrand.editVal) > 0)
                    {
                        dtSP.Rows[i]["Price"] = Price = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(bllQP, "GetPrice", new object[] { _companyID, Convert.ToInt32(_leMateriel.editVal), Convert.ToInt32(_leBrand.editVal), _measureID, MTID }));
                        dtSP.Rows[i]["Money"] = Price * Amount;
                        moeney += Convert.ToDecimal(dtSP.Rows[i]["Money"]);
                    }
                }
                gridControl1.DataSource = dtSP;
                _ltMoney.EditVal = moeney;
            }
        }
        private void _barAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            //try
            //{
            if (e.Column == _coPrice)
            {
                gridView1.SetFocusedRowCellValue(_coMoney, Convert.ToDecimal(e.Value) * Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coAmount)));
            }
            SumMoney();
            //}
            //catch { }
        }
        private void SumMoney()
        {
            decimal moeney = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                moeney += Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
            }
            _ltMoney.EditVal = moeney;
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
                if (Convert.ToBoolean(dtSOMI.Rows[0]["IsToPlan"]))
                {
                    XtraMessageBox.Show("本单已转入生产计划，请先删除生产计划！");
                    return;
                }
                if (DialogResult.Yes == XtraMessageBox.Show("请确认是否真的删除本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, (int)BasicClass.Enums.TableType.SalesOne });
                    BasicClass.GetDataSet.ExecSql("Hownet.BLL.SalesPrice", "DeleteByMainID", new object[] { _MainID });
                    BasicClass.GetDataSet.ExecSql(bllSOMI, "Delete", new object[] { _MainID });
                    if (dtMain.Rows.Count > 1)
                    {
                        dtMain.Rows.RemoveAt(bs.Position);
                    }
                    else
                    {
                        InData();
                        ShowInfo(0);
                    }
                }
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(9, "已完成");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(21, "客户取消");
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(22, "公司取消");
        }
        /// <summary>
        /// 设置生产任务进度标记，9为已完成，21为 客户取消 ，22为公司取消
        /// </summary>
        /// <param name="TypeID"></param>
        private void SetMark(int TypeID, string strType)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的将本单标记为： " + strType + " ？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否真的将本单标记为： " + strType + " ？\r\n不提供撤消操作！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    dtSOMI.Rows[0]["IsVerify"] = TypeID;
                    BasicClass.GetDataSet.UpData(bllSOMI, dtSOMI);
                   // BasicClass.GetDataSet.ExecSql(bllSOMI, "UpPlanMD", new object[] { _MainID });
                    //ShowGDI();
                    barSubItem1.Enabled = false;
                }
            }
        }

        private void ucGridLookup1_EditValueChanged(object val, string text)
        {
            if (!t)
            {
                _companyID = int.Parse(val.ToString());
                SetPrice();
            }
        }
    }
}