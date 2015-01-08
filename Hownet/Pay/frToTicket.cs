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
using Hownet.BaseContranl;

namespace Hownet.Pay
{
    public partial class frToTicket : DevExpress.XtraEditors.XtraForm
    {

        public frToTicket()
        {

            InitializeComponent();
        }
        public int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frToTicket(DataTable dt)
            : this()
        {
            dtMain = dt;
        }
        public frToTicket(int ID)
            : this()
        {
            _MainID = ID;
        }
        BindingSource bs = new BindingSource();
        private string bllTM = "Hownet.BLL.ProductTaskMain";
        private string bllPW = "Hownet.BLL.ProductWorkingMain";
        //Hownet.BLL.Materiel bllMat = new Hownet.BLL.Materiel();
        //Hownet.BLL.Company bllCom = new Hownet.BLL.Company();
        //Hownet.BLL.WorkTicket bllWT = new Hownet.BLL.WorkTicket();

        //Hownet.BLL.ProductWorkingMain bllPWM = new Hownet.BLL.ProductWorkingMain();
        //Hownet.BLL.ProductWorkingInfo bllPWI = new Hownet.BLL.ProductWorkingInfo();
        bool _isTick = false;
        bool _isVerify = false;
        string per = string.Empty;
        int KaiDan = 0;
        int _TableTypeID = (int)BasicClass.Enums.TableType.Task;
        int _PWID = 0;
        int _numType;
        int _DPWID = 0;
        int _materielID = 0;
        string fileName = string.Empty;
        DataTable dtTM = new DataTable();
        DataTable dtTick = new DataTable();
        DataTable dtWTI = new DataTable();
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (!BasicClass.BasicFile.liST[0].CustOder)
                _coCustOder.Visible = false;
            _numType = BasicClass.BasicFile.liST[0].NumType;
            ShowData();
            if (_MainID == 0)
            {
                InData();
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                if (dtMain.Rows.Count > 0)
                    bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                ShowView(0);
                _brFrist.Enabled = _brPrv.Enabled = _brNext.Enabled = _brLast.Enabled = _brAddNew.Enabled = false;
            }
            per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                simpleButton1.Enabled = _barEditAmount.Enabled = _barWTISave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                simpleButton8.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                barSubItem2.Enabled = false;
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _leMateriel.FormName = (int)BasicClass.Enums.TableType.Product;
            _leBrand.FormName = (int)BasicClass.Enums.TableType.Brand;
            _leDeparment.FormName = (int)BasicClass.Enums.TableType.Deparment;
            _lePWI.IsNotCanEdit = _lePW.IsNotCanEdit = false;
            _gvWorkTickInfo.Columns["班组"].ColumnEdit = _coDepID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            _gvWorkTickInfo.Columns["颜色"].ColumnEdit = _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _gvWorkTickInfo.Columns["尺码"].ColumnEdit = _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coWorkingID.ColumnEdit = _coInfoWorkID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
            _coInfoEmployee.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;

        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllTM, "GetIDList", null).Tables[0];
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
            GC.Collect();

            #region 移动按钮
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                simpleButton1.Enabled = simpleButton3.Enabled = _barVerify.Enabled = _barUnVerify.Enabled = _brAddNew.Enabled = _brDel.Enabled = _brSave.Enabled = false;
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
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];

            }
            else
            {
                _MainID = 0;
                dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtTM.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["SalesOrderInfoID"] = dr["MaterielID"] = dr["BrandID"] = dr["PWorkingID"] = dr["BomID"] = dr["DeparmentID"] = dr["UpData"] = dr["VerifyMan"] = _MainID = 0;
                dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                dr["LastDate"] = DateTime.Today.AddDays(15);
                dr["FilMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["IsBom"] = dr["IsTicket"] = false;
                dr["TicketDate"] = dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = dr["BedNO"] = "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllTM, "NewNum", new object[] { DateTime.Today, _numType });
                dtTM.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            KaiDan = 0;
            //if (_numType == 0 || _numType == 1)
            //    _ltNum.val = ((DateTime)(dtTM.Rows[0]["DateTime"])).Year.ToString() + dtTM.Rows[0]["Num"].ToString().PadLeft(3, '0');
            //else if (_numType == 2)
            //    _ltNum.val = ((DateTime)(dtTM.Rows[0]["DateTime"])).Year.ToString() + ((DateTime)(dtTM.Rows[0]["DateTime"])).Month.ToString() + dtTM.Rows[0]["Num"].ToString().PadLeft(3, '0');
            //else if (_numType == 3)
                _ltNum.val = ((DateTime)(dtTM.Rows[0]["DateTime"])).ToString("yyyyMMdd") + dtTM.Rows[0]["Num"].ToString().PadLeft(3, '0');
            _leCompany.editVal = dtTM.Rows[0]["CompanyID"];
            _leMateriel.editVal = _materielID = Convert.ToInt32(dtTM.Rows[0]["MaterielID"]);
            _leBrand.editVal = dtTM.Rows[0]["BrandID"];
            _ldDate.val = dtTM.Rows[0]["DateTime"];
            _ldLastDate.val = dtTM.Rows[0]["LastDate"];
            _ltBedNO.val = dtTM.Rows[0]["BedNO"].ToString();
            _leDeparment.editVal = dtTM.Rows[0]["DeparmentID"];
            amountList1.Open(_MainID, _TableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _isVerify = (int.Parse(dtTM.Rows[0]["IsVerify"].ToString()) > 2);
            if (_isTick)
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
            else
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
            _lePW.IsNotCanEdit = _isTick;
            _ldDate.t = _ldLastDate.t = _isVerify;
            _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = _isVerify;
            amountList1.IsCanEdit = !_isVerify;
            amountList1.IsShowPopupMenu = !_isVerify;
            _leDeparment.IsNotCanEdit = _isTick;
            ShowPW();
            _lePW.editVal = _PWID = int.Parse(dtTM.Rows[0]["PWorkingID"].ToString());
            _gcTictMain.DataSource = BasicClass.GetDataSet.GetDS(bllTM, "GetTicketMain", new object[] { _MainID, 0 }).Tables[0];
            ShowPWI();
            _gvWorkTickInfo.Columns.Clear();
            _lePW.IsNotCanEdit = false;
            xtraTabControl1.Enabled = true;
            if (Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]) == -1)
            {
                _lePW.IsNotCanEdit = true;
                xtraTabControl1.Enabled = false;
            }

            //DataTable dtTo = BasicClass.GetDataSet.GetDS(bllTM, "GetToDepList", new object[] { _MainID }).Tables[0];
            //if (dtTo.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtTo.Rows.Count; i++)
            //    {
            //        DevExpress.XtraEditors.Controls.RadioGroupItem ddi = new DevExpress.XtraEditors.Controls.RadioGroupItem(Convert.ToInt32(dtTo.Rows[i]["ID"]), dtTo.Rows[i]["Name"].ToString());
            //        radioGroup2.Properties.Items.Add(ddi);
            //    }
            //    radioGroup2.SelectedIndex = 0;
            //}
            //else
            //{
            //    amountList2.Open(0, 0, true, 1);
            //}
            DataTable dtColor = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetSumColor", new object[] { _MainID, _TableTypeID }).Tables[0];
            DataRow ddr = dtColor.NewRow();
            ddr["ColorName"] = "";
            ddr["ColorID"] = 0;
            dtColor.Rows.Add(ddr);
            dtColor.DefaultView.Sort = "ColorID";
            lookUpEdit2.Properties.DataSource = dtColor.DefaultView;
            xtraTabControl2.SelectedTabPage = _tabGYD;
            _gcWorkTickInfo.DataSource = null;
            barSubItem2.Enabled = (Convert.ToInt32(dtTM.Rows[0]["IsVerify"]) > 2 && Convert.ToInt32(dtTM.Rows[0]["IsVerify"]) < 9);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            {
                barSubItem2.Enabled = false;
            }
            ShowGDI();
            _lePWI.editVal = 0;
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
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (textEdit1.EditValue.ToString().Trim() != "" && int.Parse(textEdit1.EditValue.ToString()) > 1)
                {
                    decimal a = 1.5M;
                    decimal dAm = decimal.Parse(textEdit1.EditValue.ToString()) * a;
                    int am = int.Parse(Math.Truncate(dAm).ToString());
                    textEdit2.Text = am.ToString();
                }
                else
                {
                    textEdit1.Text = "2";
                }
            }
            catch
            {
                textEdit1.EditValue = textEdit2.EditValue = "";
            }
        }

        private void _leMateriel_EditValueChanged(object val, string text)
        {
            dtTM.Rows[0]["MaterielID"] = val;
            ShowPW();
            if (_materielID > 0)
            {
                fileName = _leMateriel.DV.Table.Select("(ID=" + _materielID + ")")[0]["Image"].ToString().Trim();
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
            else
            {
                pictureEdit1.EditValue = null;
                fileName = string.Empty;
            }
        }
        private void ShowPW()
        {
            _lePW.Par = new object[] { "(MaterielID=" + dtTM.Rows[0]["MaterielID"] + ")" };
            _lePW.FormName = (int)BasicClass.Enums.TableType.PW;
        }

        private void _lePW_EditValueChanged(object val, string text)
        {
            _PWID = int.Parse(val.ToString());
            DataSet dds = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductWorkingInfo", "GetInfoList", new object[] { Convert.ToInt32(val) });
            //gridControl2.DataSource = dtInfo = dds.Tables[0];
            _gcGYD.DataSource = dds.Tables[0];// BasicClass.GetDataSet.GetDS(bllPW, "GetInfoList", new object[] { int.Parse(val.ToString()) }).Tables[0];
            _gcGYD.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(_gcGYD_ViewRegistered);
        }
        void _gcGYD_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            //try
            //{
            DevExpress.XtraGrid.Views.Grid.GridView TemView = (DevExpress.XtraGrid.Views.Grid.GridView)(e.View);
            TemView.Columns.ColumnByFieldName("PWIID").Visible = false;
            // TemView.Columns.ColumnByFieldName("ID").Visible = false;
            //  TemView.Columns.ColumnByFieldName("MaterielID").Visible = false;
            //  TemView.Columns.ColumnByFieldName("WorkingID").Visible = false;
            //   TemView.Columns.ColumnByFieldName("ColorID").Visible = false;
            //   TemView.Columns.ColumnByFieldName("CompanyID").Visible = false;
            TemView.Columns.ColumnByFieldName("ColorName").Caption = "颜色";
            TemView.Columns.ColumnByFieldName("CompanyName").Caption = "客户";
            TemView.Columns.ColumnByFieldName("WorkingName").Caption = "工序名";
            TemView.Columns.ColumnByFieldName("Price").Caption = "工价";
            TemView.Columns.ColumnByFieldName("IsCaiC").Caption = "参与统计";
            TemView.Columns.ColumnByFieldName("IsCut").Caption = "参与折扣";
            TemView.OptionsBehavior.Editable = false;
            //}
            //catch { }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]) == 0)
            {
                if (Convert.ToInt32(_leDeparment.editVal) == 0)
                {
                    XtraMessageBox.Show("未选择生产班组，不能出工票");
                    return;
                }
                dtTM.Rows[0]["DeparmentID"] = Convert.ToInt32(_leDeparment.editVal);
            }
            _PWID = int.Parse(_lePW.editVal.ToString());
            if (!_isVerify)
            {
                XtraMessageBox.Show("本单还未审核，不能出工票！");
                return;
            }
            DataTable dttt = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            if (Convert.ToInt32(dttt.Rows[0]["DeparmentID"]) == -1)
            {
                XtraMessageBox.Show("本单已有分配给班组，请为每一班组单独出工票!");

            }
            else
            {
                int PjAmount = 0;
                int WAmount = 0;
                try
                {
                    PjAmount = int.Parse(textEdit1.EditValue.ToString());
                    WAmount = int.Parse(textEdit2.EditValue.ToString());
                }
                catch
                {
                    XtraMessageBox.Show("分箱数量错误！");
                    return;
                }
                if (_PWID == 0)
                {
                    XtraMessageBox.Show("未选择工艺单！");
                    return;
                }
                if (!bool.Parse(BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductWorkingInfo", "CheckSpecial", new object[] { int.Parse(dtTM.Rows[0]["PWorkingID"].ToString()) }).ToString()))
                {
                    XtraMessageBox.Show("有特殊工序未指定通用工序名！");
                    return;
                }
                int a = amountList1.SumAmount / PjAmount;
                //if ((amountList1.SumAmount / PjAmount) > 999)
                //{
                //    XtraMessageBox.Show("分箱后数量超过999箱，请核对分箱数量或将本单分拆!");
                //    return;
                //}
                if (WAmount > PjAmount && WAmount < (PjAmount * 2))
                {
                    dtTM.Rows[0]["IsTicket"] = _isTick = true;
                    dtTM.Rows[0]["TicketDate"] = DateTime.Today;
                    dtTM.Rows[0]["PWorkingID"] = _lePW.editVal;
                    BasicClass.GetDataSet.UpData(bllTM, dtTM);
                    int MaterielID = int.Parse(dtTM.Rows[0]["MaterielID"].ToString());
                    int BrandID = int.Parse(dtTM.Rows[0]["BrandID"].ToString());
                    object[] o = new object[] { _PWID, _MainID, PjAmount, WAmount, MaterielID, BrandID, _TableTypeID };
                    string bllMK = "Hownet.BLL.BaseFile.MakBox";
                    BasicClass.GetDataSet.ExecSql(bllMK, "MakBoxThreeID", o);
                    xtraTabControl1.SelectedTabPage = xtraTabPage2;
                    _lePW.IsNotCanEdit = _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = _isTick;
                    amountList1.IsCanEdit = !_isTick;
                    _gcTictMain.DataSource = BasicClass.GetDataSet.GetDS(bllTM, "GetTicketMain", new object[] { _MainID, 0 }).Tables[0];
                    ShowPWI();
                }
                else
                {
                    XtraMessageBox.Show("尾箱数量填写错误！");
                    return;
                }
            }
        }

        private void _gwTictMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle > -1)
                {
                    dtWTI = BasicClass.GetDataSet.GetDS(bllTM, "GetTicketInfo", new object[] { int.Parse(_gwTictMain.GetFocusedRowCellValue(_coWTID).ToString()) }).Tables[0];
                    _gcTictInfo.DataSource = dtWTI;
                    _gwTictInfo.OptionsBehavior.Editable = false;
                }
                else
                {
                    _gcTictInfo.DataSource = null;
                }
            }
            catch { }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            PrintTicket(false);
        }

        private void PrintTicket(bool IsDesign)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (_isTick)
            {
                ds = BasicClass.GetDataSet.GetDS(bllTM, "GetReport", new object[] { _MainID, 0 });
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicket, "GetReport", new object[] { _MainID, 0 }).Tables[0].Copy();
            }
            //else if (Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]) == -1)
            //{
            //    try
            //    {
            //        int _dtmID = Convert.ToInt32(radioGroup2.Properties.Items[radioGroup2.SelectedIndex].Value);
            //        ds = BasicClass.GetDataSet.GetDS(bllTM, "GetReport", new object[] { _MainID, _dtmID });
            //        dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicket, "GetReport", new object[] { _MainID, _dtmID }).Tables[0].Copy();
            //    }
            //    catch
            //    {
            //    }
            //}
            if (ds.Tables[0].Rows.Count == 0)
                return;
            ds.Tables[0].Columns.Add("MaterielName", typeof(string));
            ds.Tables[0].Columns.Add("TaskNum", typeof(string));
            ds.Tables[0].Columns.Add("BarcodeNum", typeof(string));
            ds.Tables[0].Columns.Add("BrandName", typeof(string));
            ds.Tables[0].Columns.Add("BedNO", typeof(string));
            string bedNO = string.Empty;
            if (_ltBedNO.val.Trim().Length > 0)
                bedNO = _ltBedNO.val;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["MaterielName"] = _leMateriel.valStr;
                ds.Tables[0].Rows[i]["TaskNum"] = _ltNum.val;
                ds.Tables[0].Rows[i]["BrandName"] = _leBrand.valStr;
                ds.Tables[0].Rows[i]["BarcodeNum"] = _MainID.ToString().PadLeft(5, '0') + ds.Tables[0].Rows[i]["BoxNum"].ToString().PadLeft(4, '0') + ds.Tables[0].Rows[i]["WorkingID"].ToString().PadLeft(3, '0');
                ds.Tables[0].Rows[i]["BedNO"] = bedNO;
            }
            ds.Tables[0].TableName = "Ticket";

            dt.TableName = "MainDt";
            dt.Columns.Add("BoxBarcod", typeof(string));
            dt.Columns.Add("MaterielN", typeof(string));
            dt.Columns.Add("BrandN", typeof(string));
            dt.Columns.Add("Num", typeof(string));
            dt.Columns.Add("BedNO", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["BoxBarcod"] = _MainID.ToString().PadLeft(5, '0') + dt.Rows[i]["BoxNum"].ToString().PadLeft(4, '0');
                dt.Rows[i]["MaterielN"] = _leMateriel.valStr;
                dt.Rows[i]["BrandN"] = _leBrand.valStr;
                dt.Rows[i]["Num"] = _ltNum.val;
                dt.Rows[i]["BedNO"] = bedNO;
            }
            ds.Tables.Add(dt);
            BaseForm.PrintClass.PrintTaskTicket(ds,IsDesign);
            GC.Collect();
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (_isTick)
                {
                    ds = BasicClass.GetDataSet.GetDS(bllTM, "GetTickInfo", new object[] { _MainID, 0 });
                }
                //else if (Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]) == -1)
                //{
                //    int _dtmID = Convert.ToInt32(radioGroup2.Properties.Items[radioGroup2.SelectedIndex].Value);
                //    ds = BasicClass.GetDataSet.GetDS(bllTM, "GetTickInfo", new object[] { _MainID, _dtmID });
                //}
                if (ds.Tables[0].Rows.Count == 0)
                    return;
                ds.Tables[0].TableName = "Ticket";
                ds.Tables[0].Columns.Add("MaterielName", typeof(string));
                ds.Tables[0].Columns.Add("TaskNum", typeof(string));
                ds.Tables[0].Columns.Add("BrandName", typeof(string));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["MaterielName"] = _leMateriel.valStr;
                    ds.Tables[0].Rows[i]["TaskNum"] = _ltNum.val;
                    ds.Tables[0].Rows[i]["BrandName"] = _leBrand.valStr;
                }
                BaseForm.PrintClass.PrintTaskInfo(ds);
                GC.Collect();
            }
            catch
            {
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (_gwTictMain.RowCount > 0)
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
            else
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }
        private void ShowPWI()
        {
            _lePWI.Par = new object[] { int.Parse(dtTM.Rows[0]["PWorkingID"].ToString()) };
            _lePWI.FormName = (int)BasicClass.Enums.TableType.PWI;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (_lePWI.editVal != null && _lePWI.editVal.ToString() != "" && _lePWI.editVal.ToString() != "0")
            {
                //Hownet.FormOpen.CloseRepor();
                //if (!(bool)BasicClass.GetDataSet.GetOne(BasicClass.strBll.bllProductWorkingInfo, "CheckWorkSpecial", new object[] {int.Parse( _lePWI.editVal.ToString()) }))
                //{
                //    //XtraMessageBox.Show("有特殊工序未指定通用工序名！");
                //    //return;
                //}
                if (!(bool)(BasicClass.GetDataSet.GetOne("Hownet.BLL.WorkTicketInfo", "CheckWorking", new object[] { _MainID, int.Parse(_lePWI.editVal.ToString()),  Convert.ToInt32(_leCompany.editVal) })))
                {
                    XtraMessageBox.Show("有特殊工序未指定通用工序名！");
                    return;
                }
                DataSet ds = BasicClass.GetDataSet.GetDS("Hownet.BLL.WorkTicketInfo", "GetTicketLine", new object[] { _MainID, int.Parse(_lePWI.editVal.ToString()), 0, 0 });
                ds.Tables[0].TableName = "Ticket";
                ds.Tables[0].Columns.Add("MaterielName", typeof(string));
                ds.Tables[0].Columns.Add("TaskNum", typeof(string));
                ds.Tables[0].Columns.Add("BarcodeNum", typeof(string));
                ds.Tables[0].Columns.Add("BrandName", typeof(string));
                BasicClass.GetDataSet.SetDataTable();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["MaterielName"] = _leMateriel.valStr;
                    ds.Tables[0].Rows[i]["TaskNum"] = _ltNum.val;
                    ds.Tables[0].Rows[i]["BrandName"] = _leBrand.valStr;
                    ds.Tables[0].Rows[i]["BarcodeNum"] = _MainID.ToString().PadLeft(5, '0') + ds.Tables[0].Rows[i]["BoxNum"].ToString().PadLeft(4, '0') + ds.Tables[0].Rows[i]["WorkingID"].ToString().PadLeft(3, '0');
                }
                BaseForm.PrintClass.PrintTicketLine(ds);
            }
        }

        private void _barEditAmount_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_gwTictInfo.GetFocusedRowCellDisplayText(_coInfoEmployee).Trim() == "")
                _gwTictInfo.OptionsBehavior.Editable = true;
            else
                XtraMessageBox.Show("该工序已回收，不能更改数量");
        }

        private void _barWTISave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dttt = dtWTI.Clone();
            for (int i = 0; i < dtWTI.Rows.Count; i++)
            {
                dttt.Clear();
                int a = int.Parse(dtWTI.Rows[i]["A"].ToString());
                if (a == 2)
                {
                    dttt.Rows.Add(dtWTI.Rows[i].ItemArray);
                    BasicClass.GetDataSet.UpData("Hownet.BLL.WorkTicketInfo", dttt);
                    dtWTI.Rows[i]["A"] = 1;
                    BasicClass.GetDataSet.SetDataTable();
                }
            }
        }

        private void _gwTictInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _gwTictInfo.OptionsBehavior.Editable = false;
        }

        private void _gwTictInfo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                int.Parse(e.Value.ToString());
                if (_gwTictInfo.GetFocusedRowCellValue(_coA).ToString() == "1")
                    _gwTictInfo.SetFocusedRowCellValue(_coA, 2);
            }
            catch
            {
            }
        }

        private void _gwTictInfo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(_gwTictInfo.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Form fr = new BarTaskCar(_MainID, int.Parse(dtTM.Rows[0]["PWorkingID"].ToString()), _leMateriel.valStr, _leBrand.valStr, _ltNum.val,_materielID);
            fr.ShowDialog();
        }
        private void ShowWorkList()
        {
            _gvWorkTickInfo.Columns.Clear();
            if (_isTick)
            {
                _gcWorkTickInfo.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetWorkListByPW", new object[] { _MainID, _PWID, int.Parse(dtTM.Rows[0]["MaterielID"].ToString()), 0 }).Tables[0];
                for (int i = 0; i < 9; i++)
                {
                    _gvWorkTickInfo.Columns[i].Width = 70;
                    _gvWorkTickInfo.Columns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                _gvWorkTickInfo.Columns["班组"].ColumnEdit = BaseForm.RepositoryItem._reDeparment;
                _gvWorkTickInfo.Columns["颜色"].ColumnEdit = _gvWorkTickInfo.Columns["插色一"].ColumnEdit = _gvWorkTickInfo.Columns["插色二"].ColumnEdit = BaseForm.RepositoryItem._reColor;
                _gvWorkTickInfo.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
                _gvWorkTickInfo.Columns["ID"].Visible = false;
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Hownet.BLL.WorkticketInfo bllWTI = new Hownet.BLL.WorkticketInfo();
            DataTable dtGroup = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingInfo, "GetGroupTicket", new object[] { _PWID,0 }).Tables[0];
            if (dtGroup.Rows.Count > 0)
            {
                int a = 0;
                for (int j = 0; j < dtGroup.Rows.Count; j++)
                {
                    a = int.Parse(dtGroup.Rows[j]["GroupBy"].ToString());
                    DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetFristReport", new object[] { _MainID, a });

                    ds.Tables[0].TableName = "Ticket";
                    ds.Tables[0].Columns.Add("MaterielName", typeof(string));
                    ds.Tables[0].Columns.Add("TaskNum", typeof(string));
                    ds.Tables[0].Columns.Add("BrandName", typeof(string));
                    ds.Tables[0].Columns.Add("BarcodeNum", typeof(string));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["MaterielName"] = _leMateriel.valStr;
                        ds.Tables[0].Rows[i]["TaskNum"] = _ltNum.val;
                        ds.Tables[0].Rows[i]["BrandName"] = _leBrand.valStr;
                        ds.Tables[0].Rows[i]["BarcodeNum"] = _MainID.ToString().PadLeft(5, '0') + ds.Tables[0].Rows[i]["BoxNum"].ToString().PadLeft(4, '0') + ds.Tables[0].Rows[i]["WorkingID"].ToString().PadLeft(3, '0');
                    }
                    DataTable dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicket, "GetGroupReport", new object[] { a, _MainID }).Tables[0].Copy();
                    dt.TableName = "MainDt";
                    dt.Columns.Add("BoxBarcod", typeof(string));
                    dt.Columns.Add("MaterielN", typeof(string));
                    dt.Columns.Add("BrandN", typeof(string));
                    dt.Columns.Add("Num", typeof(string));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["BoxBarcod"] = _MainID.ToString().PadLeft(5, '0') + dt.Rows[i]["BoxNum"].ToString().PadLeft(4, '0');
                        dt.Rows[i]["MaterielN"] = _leMateriel.valStr;
                        dt.Rows[i]["BrandN"] = _leBrand.valStr;
                        dt.Rows[i]["Num"] = _ltNum.val;
                    }
                    ds.Tables.Add(dt);
                    BaseForm.PrintClass.PrintTaskGroup(ds);
                    GC.Collect();
                }
            }

        }


        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllWorkTicketInfo, "CheckNotWork", new object[] { _MainID, 0 })))
            {
                XtraMessageBox.Show("此前所生成工票，已有工人记数，不能重新生成！");
                return;
            }
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的重新出工票？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllWorkTicketInfo, "DeleteTicket", new object[] { _MainID, Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]) });
                dtTM.Rows[0]["IsTicket"] = _isTick = false;
                dtTM.Rows[0]["TicketDate"] = DateTime.Today;
                dtTM.Rows[0]["PWorkingID"] = 0;
                BasicClass.GetDataSet.UpData(bllTM, dtTM);
                // ShowView(bs.Position);
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                _lePW.IsNotCanEdit = _isTick;
                _lePW.editVal = 0;
                dtWTI.Rows.Clear();
                _gcTictMain.DataSource = BasicClass.GetDataSet.GetDS(bllTM, "GetTicketMain", new object[] { 0, 0 }).Tables[0];
            }
        }



        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue != null)
            {
                Form fr = new BaseForm.ShowPic(fileName);
                fr.ShowDialog();
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                _gvWorkTickInfo.ExportToXls(fileName);
                OpenFile(fileName);
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

        private void _gvWorkTickInfo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu2(_gvWorkTickInfo.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu2(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowWorkList();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int a = Convert.ToInt32(_gwTictInfo.GetFocusedRowCellValue(_coAmount));
            DataTable dtt = new DataTable();
            int _rowid = _gwTictInfo.FocusedRowHandle;
            for (int i = _rowid + 1; i < _gwTictInfo.RowCount; i++)
            {
                if (Convert.ToInt32(_gwTictInfo.GetRowCellValue(i, _coAmount)) != a)
                {
                    dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetList", new object[] { "(ID=" + _gwTictInfo.GetRowCellDisplayText(i, _coInfoID) + ")" }).Tables[0];
                    if (Convert.ToInt32(dtt.Rows[0]["EmployeeID"]) == 0)
                    {
                        dtt.Rows[0]["Amount"] = a;
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllWorkTicketInfo, dtt);
                        _gwTictInfo.SetRowCellValue(i, _coAmount, a);
                    }
                }
            }
            BasicClass.GetDataSet.SetDataTable();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(9, "已完成");
        }
        /// <summary>
        /// 设置生产任务进度标记，9为已完成，21为 客户取消 ，22为公司取消
        /// </summary>
        /// <param name="TypeID"></param>
        private void SetMark(int TypeID, string strType)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的将本单标记为： " + strType + " ？\r\n将清除本单所有在线数量！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否真的将本单标记为： " + strType + " ？\r\n不提供撤消操作！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    dtTM.Rows[0]["IsVerify"] = TypeID;
                    BasicClass.GetDataSet.UpData(bllTM, dtTM);
                   // BasicClass.GetDataSet.ExecSql(bllTM, "UpAmount", new object[] { _MainID, (int)BasicClass.Enums.TableType.Task });
                    ShowGDI();
                    barSubItem2.Enabled = false;
                }
            }
        }
        private void ShowGDI()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i].Name == "broculosDrawing1")
                {
                    panel1.Controls.RemoveAt(i);
                    break;
                }
            }
            int vvv = Convert.ToInt32(dtTM.Rows[0]["IsVerify"]);
            if (vvv < 3)
                return;
            BroculosDrawing broculosDrawing1 = new BroculosDrawing();
            broculosDrawing1.Name = "broculosDrawing1";
            panel1.Controls.Add(broculosDrawing1);
            broculosDrawing1.BringToFront();
            broculosDrawing1.Location = label1.Location;// new System.Drawing.Point(502, 3);
            broculosDrawing1.Name = "broculosDrawing1";
            broculosDrawing1.Size = new System.Drawing.Size(120, 65);
            broculosDrawing1.TabIndex = 28;
                broculosDrawing1.StrText = Enum.GetName(typeof(BasicClass.Enums.IsVerify), vvv);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (_isTick)
                {
                    ds = BasicClass.GetDataSet.GetDS(bllTM, "GetTicketInfoCard", new object[] { _MainID });
                }
                //else if (Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]) == -1)
                //{
                //    int _dtmID = Convert.ToInt32(radioGroup2.Properties.Items[radioGroup2.SelectedIndex].Value);
                //    ds = BasicClass.GetDataSet.GetDS(bllTM, "GetTickInfo", new object[] { _MainID, _dtmID });
                //}
                if (ds.Tables[0].Rows.Count == 0)
                    return;
                ds.Tables[0].TableName = "Ticket";
                ds.Tables[0].Columns.Add("MaterielName", typeof(string));
                ds.Tables[0].Columns.Add("TaskNum", typeof(string));
                ds.Tables[0].Columns.Add("BrandName", typeof(string));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["MaterielName"] = _leMateriel.valStr;
                    ds.Tables[0].Rows[i]["TaskNum"] = _ltNum.val;
                    ds.Tables[0].Rows[i]["BrandName"] = _leBrand.valStr;
                }
                BaseForm.PrintClass.PrintTaskInfoCard(ds);
                GC.Collect();
            }
            catch
            {
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (!_isTick)
                return;
            DataTable dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.WorkTicketIDCard", "PrintQR", new object[] { _MainID }).Tables[0];
            dt.Columns.Add("Num", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Num"] = _ltNum.EditVal + "-" + dt.Rows[i]["BN"];
            }
            dt.TableName = "Labs";

            BaseForm.PrintClass.PrintQR(dt);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTicket(true);
        }
    }
}