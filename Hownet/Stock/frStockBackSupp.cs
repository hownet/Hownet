using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Threading;
using System.Drawing.Printing;
using BasicClass;
using Hownet.BaseForm;

namespace Hownet.Stock
{
    public partial class frStockBackSupp : DevExpress.XtraEditors.XtraForm
    {
        public frStockBackSupp()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frStockBackSupp(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _companyID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        string blMIOO = "Hownet.BLL.MoneyInOrOut";
        private string bllSB = "Hownet.BLL.StockBack";
        private string blSBI = "Hownet.BLL.StockBackInfo";
        private string blCL = "Hownet.BLL.CompanyLog";
       // private string blQP = "Hownet.BLL.QuotePrice";
        DataTable dtPS= new DataTable();
        DataTable dtPSO = new DataTable();
        DataTable dtBack = new DataTable();
        bool t = false;
        decimal price = 0;
        decimal money = 0;
        decimal amount = 0;
        decimal last = 0;
        decimal back = 0;
        decimal _depotAmount = 0;
        int _depotID = 0;
        string backDate = string.Empty;
        int _rowCount = 0;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            if (_MainID == 0)
            {
                InData();
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                bar1.Visible = false;
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barVerify.Enabled = false;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _coMaterielID.ColumnEdit = RepositoryItem._reAllMateriel;
            _coMeasureID.ColumnEdit = RepositoryItem._reMeasure;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = RepositoryItem._reColor;
            _coSizeID.ColumnEdit = RepositoryItem._reSize;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = RepositoryItem._re2Money;
            _leCompany.FormName =(int) BasicClass.Enums.TableType.Supplier;
            DataTable dtD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "仓库" }).Tables[0];
            _leDepot.Properties.DataSource = dtD;
            _leDepot.EditValue = 0;
           // _leDepot.FormName = (int)BasicClass.Enums.TableType.;
            _coPlanID.ColumnEdit = RepositoryItem._rePlanNum;
            //if (BasicClass.BasicFile.liST[0].Sell4Depot)
            //    _coAmount.ColumnEdit = _reBEAmount;
            _ltNum.IsCanEdit = false;
            _ltRemark.IsCanEdit = true;
            _leCompany.IsNotCanEdit = false;
         }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)Enums.TableType.StockBackSupp }).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;

        }
        /// <summary>
        /// 显示详细记录
        /// </summary>
        /// <param name="p"></param>
        void ShowView(int p)
        {
            #region 移动按钮
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;
            _brNext.Enabled = true;
            _brLast.Enabled = true;
            if (bs.Position == 0)
            {
                _brFrist.Enabled = false;
                _brPrv.Enabled = false;
            }
             if (bs.Position == dtMain.Rows.Count - 1)
            {
                _brNext.Enabled = false;
                _brLast.Enabled = false;
            }
            #endregion
             this._leCompany.EditValueChanged -= new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
             this.gridView1.RowCountChanged -= new System.EventHandler(this.gridView1_RowCountChanged);
             this.gridView1.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
             dtPSO.Rows.Clear();
             if (dtMain.DefaultView[p]["ID"].ToString() != "")
             {
                 _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                 dtPS = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                 last = decimal.Parse(dtPS.Rows[0]["LastMoney"].ToString());
                 back = decimal.Parse(dtPS.Rows[0]["BackMoney"].ToString());
                 money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
                 _leCompany.IsNotCanEdit = true;
             }
             else
             {
                 dtPS = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtPS.NewRow();
                 dr["CompanyID"] = dr["ID"] = dr["DepotID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                 dr["FillDate"] = dr["DataTime"] = DateTime.Today;
                 dr["FillMan"] = BasicClass.UserInfo.UserID;
                 dr["IsVerify"] = 1;
                  dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                 dr["Remark"] = "";
                 dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { DateTime.Today });
                 dr["LastMoney"] = last = 0;
                 dr["BackMoney"] = back = 0;
                 dr["BackDate"] = DateTime.Parse("1900-1-1");
                 money = 0;
                 dtPS.Rows.Add(dr);
                 _brAddNew.Enabled = false;
                 _leCompany.IsNotCanEdit = false;
             }
             _upData = int.Parse(dtPS.Rows[0]["UpData"].ToString());
             t =(int.Parse((dtPS.Rows[0]["IsVerify"]).ToString())==3);
             _ldDate.val = dtPS.Rows[0]["DataTime"];
             _leCompany.editVal = _companyID = int.Parse(dtPS.Rows[0]["CompanyID"].ToString());
             _leDepot.EditValue = _depotID = int.Parse(dtPS.Rows[0]["DepotID"].ToString());
             money = decimal.Parse(dtPS.Rows[0]["Money"].ToString());
             _ltRemark.val = dtPS.Rows[0]["Remark"].ToString();
             _ltNum.val = DateTime.Parse(dtPS.Rows[0]["DataTime"].ToString()).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');
             dtPSO = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
             gridControl1.DataSource = dtPSO;
             _barVerify.Enabled = _brSave.Enabled =  _barDel.Enabled =_coDepotAmount.Visible= !t;
             _brAddNew.Enabled = _barUnVierfy.Enabled = (t && p == dtMain.Rows.Count - 1);
             //_coDepotAmount.VisibleIndex = 3;
             //gridView1.OptionsBehavior.Editable = !t;
             if ((DateTime)(dtPS.Rows[0]["BackDate"]) == DateTime.Parse("1900-1-1"))
                 backDate = "      年    月    日";
             else
                 backDate = ((DateTime)(dtPS.Rows[0]["BackDate"])).ToString("yyyy年MM月dd日");
             SetColumnsReadOnly();
             ShowLoan();
             this._leCompany.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
             _coPrice.OptionsColumn.AllowEdit = _coDepotAmount.OptionsColumn.AllowEdit = false;
             _rowCount = gridView1.RowCount;
             this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
             this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
        }
        private void SetColumnsReadOnly()
        {
            //for (int i = 0; i < gridView1.Columns.Count; i++)
            //{
            //    if (gridView1.Columns[i].Visible && gridView1.Columns[i] != _coAmount)
            //    {
            //        gridView1.Columns[i].OptionsColumn.AllowEdit = !t;
            //    }
            //}
        }
        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }
        /// <summary>
        /// 下一条
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }
        /// <summary>
        /// 尾记录
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }
        /// <summary>
        /// 新单
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool f = _MainID == 0;

           if( Save())
           {
               //if (f)
               //{
                   int d = bs.Position;
                   InData();
                   bs.Position = d;
                   ShowView(d);
               //}
           }
        }
        private bool Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPSO.AcceptChanges();
            if (_companyID == 0)
            {
               if(DialogResult.No== XtraMessageBox.Show("未选择客户，是否为现金销售？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
                return false;
            }
            if (_depotID == 0)
            {
                XtraMessageBox.Show("请选择出货仓库！");
                return false;
            }
            bool isZore = false;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
                {
                    if (DialogResult.No == XtraMessageBox.Show("有记录没有产生金额，保存时将被删除，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        isZore = true;
                    }
                    break;
                }
            }
            if (isZore)
                return false;
            DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
            if (dttt.Rows.Count > 0)
                last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
            else
                last = 0;
            dtPS.Rows[0]["LastMoney"] = last;
            dtPS.Rows[0]["BackMoney"] = back;
            dtPS.Rows[0]["Remark"] = _ltRemark.val;
            dtPS.Rows[0]["Money"] = money;
            dtPS.Rows[0]["CompanyID"] = _companyID;
            dtPS.Rows[0]["DataTime"] = _ldDate.val;
            dtPS.Rows[0]["State"] = (int)Enums.TableType.StockBackSupp;
            dtPS.Rows[0]["A"] = 1;
            dtPS.Rows[0]["DepotID"] = _depotID;
            if (_MainID == 0)
            {
                dtPS.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllSB, "NewNum",  new object[] { BasicClass.GetDataSet.GetDateTime().Date, (int)Enums.TableType.StockBackSupp, _companyID });
                dtMain.Rows[bs.Position]["ID"] = dtPS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllSB, dtPS);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["UpData"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtPS.Rows[0]["UpData"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllSB, dtPS);
                }
            }
            DataTable dtt = dtPSO.Clone();
            for (int i = 0; i < dtPSO.Rows.Count; i++)
            {
                int a = int.Parse(dtPSO.Rows[i]["A"].ToString());
                if (a > 1)
                {
                    if (decimal.Parse(dtPSO.Rows[i]["Money"].ToString()) == 0)
                    {
                        int _id = int.Parse(dtPSO.Rows[i]["ID"].ToString());
                        if (_id > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(blSBI, "Delete", new object[] { });
                        }
                    }
                    else
                    {
                        dtPSO.Rows[i]["MainID"] = _MainID;
                        dtt.Clear();
                        dtt.Rows.Add(dtPSO.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtPSO.Rows[i]["ID"] = BasicClass.GetDataSet.Add(blSBI, dtt);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(blSBI, dtt);
                        }
                    }
                    dtPSO.Rows[i]["A"] = 1;

                }
            }
            ShowLoan();
            _leCompany.IsNotCanEdit = true;
            return true;
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.RowHandle > -1)
            {
                if (e.Column != _coA && gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            try
            {
                if (e.Column == _coPrice || e.Column == _coAmount)
                {
                    price = decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                    amount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString());
                    if (e.Column == _coAmount && amount > decimal.Parse(gridView1.GetFocusedRowCellValue(_coDepotAmount).ToString()))
                    {
                        XtraMessageBox.Show("退货数量超过现有库存数量！");
                        gridView1.SetFocusedRowCellValue(_coAmount, gridView1.GetFocusedRowCellValue(_coDepotAmount));
                    }
                    gridView1.SetFocusedRowCellValue(_coMoney, (price * amount).ToString("n2"));
                    SumMoney();
                    ShowLoan();
                }
            }
            catch { }
            try
            {
                if (e.Column == _coDepotAmount && decimal.Parse(gridView1.GetFocusedRowCellValue(_coAmount).ToString()) > decimal.Parse(e.Value.ToString()))
                {
                    XtraMessageBox.Show("退货数量超过现有库存数量！");
                    gridView1.SetFocusedRowCellValue(_coAmount, e.Value);
                }
            }
            catch { }
        }
     


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount > 0)
            {

            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
        }                  
        private void SumMoney()
        {
            money = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                try
                {
                    money += decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString());
                }
                catch { }
            }
        }
        private void _leCompany_EditValueChanged(object val, string text)
        {

            _companyID = int.Parse(val.ToString());
            if (!t && _companyID > 0)
            {
                DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
                if (dttt.Rows.Count > 0)
                    last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
                else
                    last = 0;
                ShowLoan();
            }
            if (_MainID == 0)
                GetBackList();
        }
        private void ShowLoan()
        {
            string ssss = "此前欠供应商： " + _leCompany.valStr + " 货款：" + last.ToString("C2");
            ssss = ssss + "扣减本单：" + money.ToString("N2") + "元，结欠货款：" + (last -  money).ToString("N2") + "元。";
            _barLoan.Caption = ssss;
        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("是否确认删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
            {
                if (DialogResult.Yes == MessageBox.Show("请再次确认是否删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    if (_MainID > 0)
                    {
                        object[] o = new object[] { _MainID };
                        BasicClass.GetDataSet.ExecSql(blSBI, "DeleteByMain", o);
                        BasicClass.GetDataSet.ExecSql(bllSB, "Delete", o);
                    }
                    InData();
                    bs.Position = dtMain.Rows.Count - 1;
                    if (bs.Position == 0)
                        ShowView(0);
                }
            }
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == MessageBox.Show("是否确认删除该条记录？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    int _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                    int _brandID = int.Parse(gridView1.GetFocusedRowCellValue(_coColorID).ToString());
                    int id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (id > 0)
                    {
                        BasicClass.GetDataSet.ExecSql(blSBI, "Delete", new object[] { id });
                    }
                    this.gridView1.RowCountChanged -= new System.EventHandler(this.gridView1_RowCountChanged);
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
                }
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i,_coAmount)!=string.Empty&& decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString()) == 0)
                {
                    XtraMessageBox.Show("有明细记录没有金额或有商品没有成本价！");
                    return;
                }
            }
            if (!Save())
                return;
            if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllStockBack, "CheckLinLiao", new object[] { _MainID })))
            {
                XtraMessageBox.Show("出库数量超过库存！");
                return;
            }
            dtPS.Rows[0]["IsVerify"] = 3;
            dtPS.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Today;
            BasicClass.GetDataSet.UpData(bllSB, dtPS);
            if (_companyID > 0)
            {
                DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
                DataRow dr = dtCL.NewRow();
                dr["ID"] = 0;
                dr["CompanyID"] = _companyID;
                dr["DateTime"] = dtPS.Rows[0]["DataTime"];
                dr["LastMoney"] = dtPS.Rows[0]["LastMoney"];
                dr["ChangMoney"] = money;
                dr["Money"] = last - money;
                dr["TypeID"] = (int)(BasicClass.Enums.MoneyTableType.StockBackSupp);
                dr["TableID"] = _MainID;
                dr["NowMoneyTypeID"] = 0;
                dr["NowMoney"] = 0;
                dr["NowReta"] = 1;
                dr["A"] = 1;
                dtCL.Rows.Add(dr);
                BasicClass.GetDataSet.Add(blCL, dtCL);

            }
                //if (BasicClass.BasicFile.liST[0].Sell4Depot)
                //  BasicClass.GetDataSet.ExecSql(bllSB, "Verify", new object[] { _MainID, false, _depotID });
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllRepertory, "VerifyBack", new object[] { _MainID, false });
            int d = bs.Position;
            InData();
            bs.Position = d;
            ShowView(d);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.StockBackSupp), _MainID }).ToString()) > 0)
            {
                XtraMessageBox.Show("本单之后已有后续业务发生，不能弃审！");
                return;
            }
            dtPS.Rows[0]["IsVerify"] = 0;
            dtPS.Rows[0]["VerifyMan"] = 0;
            dtPS.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            BasicClass.GetDataSet.UpData(bllSB, dtPS);
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.StockBackSupp), _MainID });
           // BasicClass.GetDataSet.ExecSql(bllSB, "Verify", new object[] { _MainID, true,_depotID });
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllRepertory, "VerifyBack", new object[] { _MainID, true });
            int d = bs.Position;
            InData();
            bs.Position = d;
            ShowView(d);
        }

        private void _reBEAmount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("请审核后再打印单据！");
                return;
            }
            int _id = int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllSysTem, "GetMaxId", null).ToString()) - 1;
            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSysTem, "GetList", new object[] { "(ID=" + _id + ")" });
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show("请完善公司信息！");
                //Form fr = new BaseForm.UserBaseSetForm();
                //fr.ShowDialog();
                return;
            }
            ds.Tables[0].TableName = "Main";
            ds.Tables["Main"].Columns.Add("LastMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("BackMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("NowMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("NewMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("DXMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("ComName", typeof(string));
            ds.Tables["Main"].Columns.Add("Date", typeof(string));
            ds.Tables["Main"].Columns.Add("Num", typeof(string));
            ds.Tables["Main"].Columns.Add("UserName", typeof(string));
            ds.Tables["Main"].Columns.Add("SumAmount", typeof(string));
            ds.Tables["Main"].Columns.Add("SumBoxAmount", typeof(string));
            ds.Tables["Main"].Columns.Add("SumMoney", typeof(string));
            //ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("BackDate", typeof(string));
            ds.Tables[0].Rows[0]["LastMoney"] = last.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["BackMoney"] = back.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NowMoney"] = money.ToString("C2") + "元，";
            ds.Tables[0].Rows[0]["NewMoney"] = (last - back + money).ToString("C2") + "元。";
            ds.Tables[0].Rows[0]["DXMoney"] = BasicClass.BasicFile.CmycurD(last - back + money);
            ds.Tables[0].Rows[0]["ComName"] = _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy年MM月dd日");
            ds.Tables[0].Rows[0]["Num"] = _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = _ltRemark.val;
            ds.Tables[0].Rows[0]["BackDate"] = backDate;
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterielName", typeof(string));
            dt.Columns.Add("BrandName", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Conversion", typeof(string));
            dt.Columns.Add("BoxMeasureAmount", typeof(string));
            dt.Columns.Add("Money", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("RowID", typeof(int));
            dt.Columns.Add("SizeName", typeof(string));
            int amount = 0;
            int boxAmount = 0;
            decimal moeny = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                dr[1] = gridView1.GetRowCellDisplayText(i, _coColorID);
                dr[2] = gridView1.GetRowCellDisplayText(i, _coAmount);
                dr[3] = decimal.Parse(gridView1.GetRowCellDisplayText(i, _coPrice)).ToString("C2");
                dr[4] = 3;
                dr[5] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                dr[6] = decimal.Parse(gridView1.GetRowCellDisplayText(i, _coMoney)).ToString("C2");
                dr[7] = gridView1.GetRowCellDisplayText(i, _coRemark);
                dr[8] = i + 1;
                dr[9] = gridView1.GetRowCellDisplayText(i, _coSizeID);
                dt.Rows.Add(dr);
                amount += int.Parse(gridView1.GetRowCellValue(i, _coAmount).ToString());
                boxAmount += int.Parse(gridView1.GetRowCellValue(i, _coMeasureID).ToString());
                moeny += decimal.Parse(gridView1.GetRowCellValue(i, _coMoney).ToString());
            }
            ds.Tables[0].Rows[0]["SumAmount"] = amount;
            ds.Tables[0].Rows[0]["SumBoxAmount"] = boxAmount;
            ds.Tables[0].Rows[0]["SumMoney"] = moeny.ToString("C2");
            for (int i = dt.Rows.Count; i < 8; i++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1]["RowID"] = dt.Rows.Count;
                //dt.Rows[i]["Amount"] = DBNull.Value;
                //dt.Rows[i]["Price"] = DBNull.Value;
                //dt.Rows[i]["Conversion"] = DBNull.Value;
                //dt.Rows[i]["Money"] = DBNull.Value;
                //dt.Rows[i]["BoxMeasureAmount"] = DBNull.Value;
            }
            dt.TableName = "Info";
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintSellTable(ds,0);

        }

        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }



        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }
        private void GetBackList()
        {
            dtPSO.Rows.Clear();
            if (_companyID == 0 || _depotID == 0)
                return;
            dtPSO = BasicClass.GetDataSet.GetDS(blSBI, "GetBackSupp", new object[] {_companyID,_depotID }).Tables[0];
            dtPSO.Columns.Add("DepotMeasureID", typeof(int));
            dtPSO.Columns.Add("Amount", typeof(decimal));
            dtPSO.Columns.Add("Conversion", typeof(int));
            dtPSO.Columns.Add("NotAmount", typeof(int));
            dtPSO.Columns.Add("PriceAmount", typeof(int));
            dtPSO.Columns.Add("NotPriceAmount", typeof(int));
            dtPSO.Columns.Add("BrandID", typeof(int));
            dtPSO.Columns.Add("IsEnd", typeof(int));
            dtPSO.Columns.Add("NeedAmount", typeof(int));
            dtPSO.Columns.Add("ExcessAmount", typeof(int));
            dtPSO.Columns.Add("StringTaskID", typeof(string));
            dtPSO.Columns.Add("LastTime", typeof(DateTime));
            dtPSO.Columns.Add("Money", typeof(decimal));
            for (int i = 0; i < dtPSO.Rows.Count; i++)
            {
                dtPSO.Rows[i]["NotAmount"] = dtPSO.Rows[i]["Money"] = dtPSO.Rows[i]["PriceAmount"] = dtPSO.Rows[i]["NotPriceAmount"] = dtPSO.Rows[i]["BrandID"] = dtPSO.Rows[i]["IsEnd"] =  dtPSO.Rows[i]["NeedAmount"] = dtPSO.Rows[i]["ExcessAmount"] = 0;
                dtPSO.Rows[i]["Conversion"] = 1;
                dtPSO.Rows[i]["DepotMeasureID"] = dtPSO.Rows[i]["CompanyMeasureID"];
                dtPSO.Rows[i]["Amount"] = DBNull.Value;
            }
            gridControl1.DataSource = dtPSO;
        }

        private void _leDepot_EditValueChanged(object sender, EventArgs e)
        {
            _depotID = Convert.ToInt32(_leDepot.EditValue);
            if (_MainID == 0)
                GetBackList();
        }
    }
}