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
using System.Reflection;

namespace Hownet.Task
{
    public partial class frTaskForm : DevExpress.XtraEditors.XtraForm
    {

        public frTaskForm()
        {

            InitializeComponent();
        }
        int _MainID = 0;
        int _ParentID = -1;
        DataTable dtMain = new DataTable();
        public frTaskForm(DataTable dt)
            : this()
        {
            dtMain = dt;
        }
        BasicClass.cResult cRR = new BasicClass.cResult();
        public frTaskForm(BasicClass.cResult rr, int ID)
            : this()
        {
            cRR = rr;
            _ParentID = ID;
        }
        public frTaskForm(BasicClass.cResult rr, int MainID, int a)
            : this()
        {
            cRR = rr;
            _MainID = MainID;
        }
        BindingSource bs = new BindingSource();
        private string bllTM = "Hownet.BLL.ProductTaskMain";
        private string bllPW = "Hownet.BLL.ProductWorkingMain";
        private string bllAR = "Hownet.BLL.AllRemark";
        private string bllLR = "Hownet.BLL.AllLineRemark";
        bool _isTick = false;
        bool _isBom = false;
        bool _isVerify = false;
        string per = string.Empty;
        string fileName = string.Empty;
        int KaiDan = 0;
        int _TableTypeID = (int)BasicClass.Enums.TableType.Task;
        int _PWID = 0;
        int _numType = 0;
        int _bomID = 0;
        int _materielID, _brandID;
        DataTable dtTM = new DataTable();
        DataTable dtTick = new DataTable();
        DataTable dtWTI = new DataTable();
        DataTable dtPP = new DataTable();
        DataTable dtDemand = new DataTable();
        DataTable dtJGC = new DataTable();
        DataTable dtOTSet = new DataTable();
        DataTable dtAR = new DataTable();
        int _LR = 0;
        DataTable dtLR = new DataTable();
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            _numType = BasicClass.BasicFile.liST[0].NumType;
            per = BasicClass.BasicFile.GetPermissions(this.Text);
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
                bs.DataSource = dtMain;
                if (dtMain.Rows.Count > 0)
                    bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
                _brFrist.Enabled = _brPrv.Enabled = _brNext.Enabled = _brLast.Enabled = _brAddNew.Enabled = false;
            }
            if (_ParentID > 0)
            {
                _brSave.Enabled = false;
            }
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _barVerify.Enabled = _barUnVerify.Enabled = _brAddNew.Enabled = _brDel.Enabled = _brSave.Enabled = false;
            //}

            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _brDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barUnVerify.Visibility = _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            _lePackID.FormName = (int)BasicClass.Enums.TableType.PackingMethod;
            _trUsing.ColumnEdit = BaseForm.RepositoryItem._reUse;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _deDepID.ColumnEdit = BaseForm.RepositoryItem._reDepartmentType;
            amountList1.IsCanEditCS = false;
            dtJGC = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0];

            dtOTSet = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Value='生产单设置')" }).Tables[0];

            DataTable dtLRTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetList", new object[] { "(Value='列表记录条数')" }).Tables[0];
            if (dtLRTem.Rows.Count == 0)
            {
                DataRow drL = dtLRTem.NewRow();
                drL["A"] = 1;
                drL["ID"] = 0;
                drL["TypeID"] = -99;
                drL["Value"] = "列表记录条数";
                drL["Name"] = 4;
                dtLRTem.Rows.Add(drL);
                dtLRTem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dtLRTem);
            }
            _LR = Convert.ToInt32(dtLRTem.Rows[0]["Name"]);
            dtLR = BasicClass.GetDataSet.GetDS(bllLR, "GetList", new object[] { "(MainID=0) And (TableTypeID=0)" }).Tables[0];
            gridControl2.DataSource = dtLR;
            for (int i = _LR; i < 8; i++)
            {
                gridView2.Columns[i + 4].Visible = false;
            }
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {

            if (_ParentID > 0)
            {
                dtMain.Columns.Add("ID", typeof(int));
                DataTable dtt = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ParentID=" + _ParentID + ")" }).Tables[0];
                if (dtt.Rows.Count > 0)
                {
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        dtMain.Rows.Add(dtt.Rows[i]["ID"]);
                    }
                }
                bs.DataSource = dtMain;
                dtMain.Rows.Add(dtMain.NewRow());
                bs.Position = dtMain.Rows.Count - 1;
                ShowView(bs.Position);
            }
            else
            {
                dtMain = BasicClass.GetDataSet.GetDS(bllTM, "GetIDList", null).Tables[0];
                if (dtMain.Rows.Count == 0)
                    dtMain.Rows.Add(dtMain.NewRow());
                bs.DataSource = dtMain;
            }
        }
        /// <summary>
        /// 显示详细记录
        /// </summary>
        /// <param name="p"></param>
        void ShowView(int p)
        {
            #region 移动按钮
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _brDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barVerify.Enabled = false;
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
            dtPP.Rows.Clear();
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];

            }
            else
            {
                _MainID = 0;
                if (_ParentID > 0)
                    dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetList", new object[] { "(ID=" + _ParentID + ")" }).Tables[0];
                dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtTM.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["SalesOrderInfoID"] = dr["MaterielID"] = dr["BrandID"] = dr["PWorkingID"] = dr["BomID"] = dr["DeparmentID"] = dr["UpData"] = dr["VerifyMan"] = _MainID = 0;
                dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
                dr["LastDate"] = DateTime.Today.AddDays(15);
                dr["FilMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["IsBom"] = dr["IsTicket"] = false;
                dr["TicketDate"] = dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["SewingRemark"] = string.Empty;
                dr["ParentID"] = _ParentID;
                dr["DeparmentType"] = 0;
                dr["Price"] = 0;
                dr["BedNO"] = BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductionPlan", "GetMaxTask", new object[] { _ParentID });
                if (dtPP.Rows.Count > 0)
                {
                    dr["DateTime"] = dtPP.Rows[0]["DateTime"];
                    dr["Num"] = dtPP.Rows[0]["Num"];
                }
                else
                {
                    //dr["Num"] = BasicClass.GetDataSet.GetOne(bllTM, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, _numType });
                    dr["Num"] = 0;
                }
                dtTM.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            dtTick = new DataTable();
            KaiDan = 0;
            linkLabel1.Visible = (_MainID == 0);
            _leCompany.editVal = dtTM.Rows[0]["CompanyID"];
            _leMateriel.editVal = _materielID = Convert.ToInt32(dtTM.Rows[0]["MaterielID"]);
            _leBrand.editVal = _brandID = Convert.ToInt32(dtTM.Rows[0]["BrandID"]);
            _ldLastDate.val = dtTM.Rows[0]["LastDate"];
            _ltBedNO.val = dtTM.Rows[0]["BedNO"].ToString();
            checkEdit1.Checked = labelControl1.Visible = textEdit1.Visible = Convert.ToInt32(dtTM.Rows[0]["DeparmentType"]) == 3;
            _leDeparment.editVal = dtTM.Rows[0]["DeparmentID"];
            if (_ParentID < 1)
            {
                amountList1.Open(_MainID, _TableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
            }
            else
            {
                amountList1.Open(_ParentID, (int)BasicClass.Enums.TableType.ProductionPlan, true, (int)BasicClass.Enums.AmountType.未完成数量);
                if (_MainID == 0)
                    amountList1.IsEdit = true;
            }
            _ParentID = Convert.ToInt32(dtTM.Rows[0]["ParentID"]);
            amountList2.Open(_MainID, _TableTypeID, true, radioGroup1.SelectedIndex + 2);
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _isBom = (bool)(dtTM.Rows[0]["IsBom"]);
            _isVerify = (int.Parse(dtTM.Rows[0]["IsVerify"].ToString()) > 2);
            _ldLastDate.t = _isVerify;
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _barUnVerify.Enabled = _isVerify;
            _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = !_isVerify;
            _ltBedNO.IsCanEdit = amountList1.IsShowPopupMenu = amountList1.IsCanEdit = !_isVerify;
            _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = amountList1.IsShowPopupMenu = !_isVerify;
            if (_isVerify && (p == dtMain.Rows.Count - 1))
                _brAddNew.Enabled = true;
            _PWID = int.Parse(dtTM.Rows[0]["PWorkingID"].ToString());
            xtraTabControl1.SelectedTabPage = _xtpAmount;
            userNum1.IsCanEdit = (_MainID == 0);
            userNum1.ClearData();
            userNum1.NumStr = "SCZD";
            userNum1.Num = Convert.ToInt32(dtTM.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtTM.Rows[0]["DateTime"]);
            if (_MainID > 0)
                userNum1.LastEdit = dtTM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            if (_isVerify)
                userNum1.VerifyUser = dtTM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");
            if (_isTick)
                userNum1.JinDu = "已出工票";
            if (_isBom)
                userNum1.JinDu += " 已计算物料";
            dtPP.Rows.Clear();
            dtDemand.Rows.Clear();
            _leCompany.IsNotCanEdit = _leDeparment.IsNotCanEdit = _leBrand.IsNotCanEdit = _leMateriel.IsNotCanEdit = _isVerify;
            _leBom.editVal = _bomID = Convert.ToInt32(dtTM.Rows[0]["BomID"]);
            _meSewRemark.EditValue = dtTM.Rows[0]["SewingRemark"];
            if (_ParentID > 0)
            {
                dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetList", new object[] { "(ID=" + _ParentID + ")" }).Tables[0];
                //   _lePackID.t = true;
                _leMateriel.IsNotCanEdit = _leBrand.IsNotCanEdit = true;
                _leMateriel.editVal = _materielID = Convert.ToInt32(dtPP.Rows[0]["MaterielID"]);
                _leCompany.editVal = Convert.ToInt32(dtPP.Rows[0]["CompanyID"]);
                _leBrand.editVal = Convert.ToInt32(dtPP.Rows[0]["BrandID"]);
                _leBom.Par = new object[] { "(MaterielID=" + Convert.ToInt32(_leMateriel.editVal) * -1 + ")" };
                _leBom.FormName = (int)BasicClass.Enums.TableType.BOM;
                _leBom.editVal = _bomID = Convert.ToInt32(dtPP.Rows[0]["BomID"]);
                _ldLastDate.val = dtTM.Rows[0]["LastDate"];
                dtTM.Rows[0]["LastDate"] = dtPP.Rows[0]["LastDate"];
                if (!_isVerify)
                {
                    GetDemand();
                    //dtDemand = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielDemand", "GetList", new object[] { "(ProduceTaskID=" + _ParentID + ") And (TableTypeID=" + (int)BasicClass.Enums.TableType.ProductionPlan + ")" }).Tables[0];
                }
                else
                {
                    dtDemand = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielDemand", "GetList", new object[] { "(ProduceTaskID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
                }
                _gcDemand.DataSource = dtDemand;
                //DataTable dtSTT = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.OrderTask", "ShowSizeInfo", new object[] { _ParentID, true, (int)BasicClass.Enums.TableType.ProductionPlan, _materielID }).Tables[0];
                //_gcSizeTable.DataSource = dtSTT;
                //_gvSizeTable.Columns[0].ColumnEdit = BaseForm.RepositoryItem._reSizePart;
              
                ucSizeBow1.Open(_ParentID, (int)BasicClass.Enums.TableType.ProductionPlan);
                ucSizeList1.Open(Convert.ToInt32(dtTM.Rows[0]["MaterielID"]), _ParentID, !_isVerify);
                ucSizeList1.IsCanEdit = ucSizeList1.IsCanEditCS = ucSizeList1.IsShowPopupMenu = false;
                if (_MainID == 0)
                    _meSewRemark.EditValue = dtPP.Rows[0]["SewingRemark"];
            }
            else
            {
                amountList1.IsCanEditCS = !_isVerify;
                ucSizeBow1.Open(_MainID, (int)BasicClass.Enums.TableType.Task);
                //DataTable dtSTT = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.OrderTask", "ShowSizeInfo", new object[] { _materielID, true, (int)BasicClass.Enums.TableType.Task, _materielID }).Tables[0];
                //_gcSizeTable.DataSource = dtSTT;
                //_gvSizeTable.Columns[0].ColumnEdit = BaseForm.RepositoryItem._reSizePart;
                ucSizeList1.Open(Convert.ToInt32(dtTM.Rows[0]["MaterielID"]) * -1, 0, !_isVerify);
                ucSizeList1.IsCanEdit = ucSizeList1.IsCanEditCS = ucSizeList1.IsShowPopupMenu = true;
                
            }
            _leBom.IsNotCanEdit = _isVerify;
            //xtraTabControl1.SelectedTabPage = _xtpNeed;
            //// xtraTabControl1.SelectedTabPage = _xtraSize;
            //xtraTabControl1.SelectedTabPage = _xtpBOM;
            //xtraTabControl1.SelectedTabPage = xtraTabPage8;
            xtraTabControl1.SelectedTabPage = _xtpAmount;
            amountList1.IsCanEdit = !_isVerify;
            checkEdit1.Visible = !_isVerify;
            textEdit1.EditValue = dtTM.Rows[0]["Price"];

            dtAR = BasicClass.GetDataSet.GetDS(bllAR, "GetList", new object[] { "(MainID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
            if (dtAR.Rows.Count == 0)
            {

                dtAR = BasicClass.GetDataSet.GetDS(bllAR, "GetList", new object[] { "(MainID=" + _ParentID + ") And (TableTypeID=" + (int)BasicClass.Enums.TableType.ProductionPlan + ")" }).Tables[0];
                if (dtAR.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAR.Rows.Count; i++)
                    {
                        dtAR.Rows[i]["A"] = 3;
                        dtAR.Rows[i]["ID"] = 0;
                        dtAR.Rows[i]["MainID"] = _MainID;
                        dtAR.Rows[i]["TableTypeID"] = _TableTypeID;
                    }
                }
                else
                {
                    for (int i = 0; i < dtOTSet.Rows.Count; i++)
                    {
                        DataRow dr = dtAR.NewRow();
                        dr["A"] = 3;
                        dr["ID"] = 0;
                        dr["MainID"] = _MainID;
                        dr["TableTypeID"] = _TableTypeID;
                        dr["OTID"] = dtOTSet.Rows[i]["ID"];
                        dr["OTName"] = dtOTSet.Rows[i]["Name"];
                        dr["Remark"] = string.Empty;
                        dtAR.Rows.Add(dr);
                    }
                }
            }
            gridControl1.DataSource = dtAR;
            dtLR = BasicClass.GetDataSet.GetDS(bllLR, "GetList", new object[] { "(MainID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
           if(dtLR.Rows.Count==0)
           {
               dtLR = BasicClass.GetDataSet.GetDS(bllLR, "GetList", new object[] { "(MainID=" + _ParentID + ") And (TableTypeID=" + (int)BasicClass.Enums.TableType.ProductionPlan + ")" }).Tables[0];
               if (dtLR.Rows.Count > 0)
               {
                   for (int i = 0; i < dtLR.Rows.Count; i++)
                   {
                       dtLR.Rows[i]["A"] = 3;
                       dtLR.Rows[i]["ID"] = 0;
                       dtLR.Rows[i]["MainID"] = _MainID;
                       dtLR.Rows[i]["TableTypeID"] = _TableTypeID;
                   }
               }
           }
            
            
            if (!_isVerify)
            {
                DataRow drLR = dtLR.NewRow();
                drLR["A"] = 3;
                drLR["ID"] = 0;
                drLR["MainID"] = _MainID;
                drLR["TableTypeID"] = _TableTypeID;
                drLR["Remark1"] = drLR["Remark2"] = drLR["Remark3"] = drLR["Remark4"] = drLR["Remark5"] = drLR["Remark6"] = drLR["Remark7"] = drLR["Remark8"] = string.Empty;
                dtLR.Rows.Add(drLR);
            }
            gridControl2.DataSource = dtLR;
        }
        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MoveFirst();
        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MovePrevious();
        }
        /// <summary>
        /// 下一条
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MoveNext();
        }
        /// <summary>
        /// 尾记录
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MoveLast();
        }
        /// <summary>
        /// 新单
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Save())
            {
                _barUnVerify.Enabled = _isVerify;
                _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = amountList1.IsCanEdit = !_isVerify;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = amountList1.IsShowPopupMenu = !_isVerify;
                if (_isVerify && (bs.Position == dtMain.Rows.Count - 1))
                    _brAddNew.Enabled = true;
                XtraMessageBox.Show("保存成功！");
            }
        }
        private bool Save()
        {
            if (int.Parse(_leMateriel.editVal.ToString()) == 0)
            {
                XtraMessageBox.Show("请选择款号！");
                return false;
            }
            if (int.Parse(_leBrand.editVal.ToString()) == 0)
            {
                XtraMessageBox.Show("请选择商标！");
                return false;
            }
            if (amountList1.SumAmount == 0)
            {
                XtraMessageBox.Show("请输入详细数量！");
                return false;
            }
            if (amountList1.CheckColor)
            {
                XtraMessageBox.Show("颜色重复！");
                return false;
            }
            if (KaiDan == 2)
            {
                if (DialogResult.No == XtraMessageBox.Show("请检查数量是否正确，将直接生成工票！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return false;
                }
            }
            dtTM.Rows[0]["CompanyID"] = _leCompany.editVal;
            dtTM.Rows[0]["MaterielID"] = _leMateriel.editVal;
            dtTM.Rows[0]["BrandID"] = _leBrand.editVal;
            dtTM.Rows[0]["DateTime"] = userNum1.NumDate;
            dtTM.Rows[0]["LastDate"] = _ldLastDate.val;
            dtTM.Rows[0]["PWorkingID"] = 0;
            dtTM.Rows[0]["BedNO"] = _ltBedNO.val;
            dtTM.Rows[0]["DeparmentID"] = _leDeparment.editVal;
            dtTM.Rows[0]["A"] = 1;
            dtTM.Rows[0]["Price"] = textEdit1.EditValue;
            dtTM.Rows[0]["SewingRemark"] = _meSewRemark.EditValue;
            if (KaiDan == 2)
            {
                BasicClass.cResult r = new BasicClass.cResult();
                r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                Form fr = new SelectWorkingForm(r, int.Parse(_leMateriel.editVal.ToString()));
                fr.ShowDialog();
                if (_PWID == 0)
                {
                    XtraMessageBox.Show("请先选择工艺单");
                    return false;
                }
                if (Convert.ToInt32(_leDeparment.editVal) == 0)
                {
                    XtraMessageBox.Show("请选择生产班组！");
                    return false;
                }
                dtTM.Rows[0]["IsVerify"] = 3;
                dtTM.Rows[0]["IsTicket"] = _isVerify = true;
                dtTM.Rows[0]["PWorkingID"] = _PWID;
                if (!bool.Parse(BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductWorkingInfo", "CheckSpecial", new object[] { _PWID }).ToString()))
                {
                    XtraMessageBox.Show("有特殊工序未指定通用工序名！");
                    return false;
                }
            }
            if (_MainID == 0)
            {
                if (userNum1.IsEdit)
                {
                    if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllTM, "CheckNum", new object[] { userNum1.NumDate, userNum1.Num })))
                    {
                        XtraMessageBox.Show("生产编号重复！");
                        return false;
                    }
                    dtTM.Rows[0]["Num"] = userNum1.Num;
                }
                else if (_ParentID == 0)
                {
                    dtTM.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllTM, "NewNum", new object[] { userNum1.NumDate, _numType });
                }
                dtTM.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllTM, dtTM);
                _brAddNew.Enabled = true;
                dtMain.Rows[bs.Position]["ID"] = _MainID;
            }
            else
            {
                BasicClass.GetDataSet.UpData(bllTM, dtTM);
            }
            amountList1.Save(_MainID, int.Parse(dtTM.Rows[0]["MaterielID"].ToString()), int.Parse(dtTM.Rows[0]["BrandID"].ToString()), _TableTypeID);
            //_ltNum.val = ((DateTime)(dtTM.Rows[0]["DateTime"])).ToString("yyyyMMdd") + dtTM.Rows[0]["Num"].ToString().PadLeft(3, '0');
            if (KaiDan == 2)
            {
                //DataTable dtTem = new DataTable();
                //dtTem.Columns.Add("ColorID", typeof(string));
                //dtTem.Columns.Add("SizeID", typeof(string));
                //dtTem.Columns.Add("Amount", typeof(string));
                //dtTem.Columns.Add("BoxNum", typeof(string));
                //dtTem.Columns.Add("MListID",typeof(string));
                //for (int r = 0; r < dtTick.Rows.Count; r++)
                //{
                //    dtTem.Rows.Add(dtTem.NewRow());
                //    int RowID = dtTem.Rows.Count - 1;
                //    dtTem.Rows[RowID]["ColorID"] = dtTick.Rows[r]["ColorID"];
                //    dtTem.Rows[RowID]["SizeID"] = dtTick.Rows[r]["SizeID"];
                //    dtTem.Rows[RowID]["BoxNum"] = dtTick.Rows[r]["BoxNum"];
                //    dtTem.Rows[RowID]["Amount"] = dtTick.Rows[r]["Amount"];
                //}
                DataSet ds = new DataSet();
                ds.Tables.Add(dtTick.Copy());
                byte[] bb = ZipJpg.Ds2Byte(ds);
                BasicClass.GetDataSet.ExecSql("Hownet.BLL.WorkTicket", "Save", new object[] { bb, int.Parse(dtTM.Rows[0]["PWorkingID"].ToString()), _MainID });
                //if (_ParentID > 0)
                //{
                //    BasicClass.GetDataSet.ExecSql("Hownet.BLL.ProductionPlan", "UpPlanAmount", new object[] { _ParentID, _MainID, (int)BasicClass.Enums.TableType.ProductionPlan, _TableTypeID, true });
                //}

                _isVerify = true;
                if (_ParentID > 0)
                    BasicClass.GetDataSet.ExecSql("Hownet.BLL.ProductionPlan", "UpPlanAmount", new object[] { _ParentID, _MainID, (int)BasicClass.Enums.TableType.ProductionPlan, _TableTypeID, true });
                //DataTable dtttt = new DataTable();
                //if (ucSizeBow1.dtDataSource != null)
                //    dtttt = ucSizeBow1.dtDataSource.Copy();
                //dtttt.TableName = "dtSizeBow";
                //DataSet dsss = new DataSet();
                //dsss.DataSetName = "Dsss";
                //dsss.Tables.Add(dtttt);
                //byte[] bb = ZipJpg.Ds2Byte(dsss);
                //DataSet ssss = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "accountDemand", new object[] { _MainID, Convert.ToInt32(_leBom.editVal), _TableTypeID, bb,new byte[0] });
                //if (ssss.Tables.Count > 0)
                //{
                //    DataTable dtDemand = ssss.Tables[0];

                if (dtDemand.Rows.Count > 0)
                {
                    DataTable dtt = dtDemand.Clone();
                    for (int i = 0; i < dtDemand.Rows.Count; i++)
                    {
                        dtDemand.Rows[i]["ProduceTaskID"] = _MainID;
                        dtDemand.Rows[i]["TableTypeID"] = _TableTypeID;
                        dtt.Rows.Clear();
                        dtt.Rows.Add(dtDemand.Rows[i].ItemArray);
                        dtDemand.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllMaterielDemand, dtt);
                    }
                }
            }
            DataTable dtTem = dtAR.Clone();
            int a = 0;
            for (int i = 0; i < dtAR.Rows.Count; i++)
            {
                a = Convert.ToInt32(dtAR.Rows[i]["A"]);
                if (a > 1)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dtAR.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(bllAR, dtTem);
                    else if (a == 3)
                    {
                        dtAR.Rows[i]["MainID"] = dtTem.Rows[0]["MainID"] = _MainID;
                        dtAR.Rows[i]["TableTypeID"] = dtTem.Rows[0]["TableTypeID"] = _TableTypeID;
                        dtAR.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllAR, dtTem);
                    }
                    dtAR.Rows[i]["A"] = 1;
                }
            }
            dtTem = dtLR.Clone();
            for (int i = 0; i < dtLR.Rows.Count - 1; i++)
            {
                a = Convert.ToInt32(dtLR.Rows[i]["A"]);
                if (a > 1)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dtLR.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(bllLR, dtTem);
                    else if (a == 3)
                    {
                        dtLR.Rows[i]["MainID"] = dtTem.Rows[0]["MainID"] = _MainID;
                        dtLR.Rows[i]["TableTypeID"] = dtTem.Rows[0]["TableTypeID"] = _TableTypeID;
                        dtLR.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllLR, dtTem);
                    }
                    dtLR.Rows[i]["A"] = 1;
                }
            }
            KaiDan = 1;
            linkLabel1.Visible = false;
            dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            userNum1.ClearData();
            userNum1.NumStr = "SCZD";
            userNum1.Num = Convert.ToInt32(dtTM.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtTM.Rows[0]["DateTime"]);
            if (_MainID > 0)
                userNum1.LastEdit = dtTM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            if (_isVerify)
                userNum1.VerifyUser = dtTM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");

            return true;
        }
        private void UpPlan()
        {

        }
        void r_TextChanged(string s)
        {
            _PWID = int.Parse(s);
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _leMateriel_EditValueChanged(object val, string text)
        {
            dtTM.Rows[0]["MaterielID"] = val;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KaiDan = 2;
            BasicClass.cResult r = new BasicClass.cResult();
            r.TableChanged += new BasicClass.TableNumChangedHandler(r_TableChanged);
            Form fr = new Ticket2Task(r);
            fr.ShowDialog();
        }
        void r_TableChanged(DataSet ds)
        {
            amountList1.Open(_MainID, _TableTypeID, true, ds.Tables[0]);
            amountList1.IsCanEdit = false;
            dtTick = ds.Tables[1];
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.No == XtraMessageBox.Show("确认审核本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            //if (Convert.ToInt32(_leDeparment.editVal) == 0)
            //{
            //    XtraMessageBox.Show("请选择生产班组！");
            //    return;
            //}
            if (Convert.ToInt32(dtTM.Rows[0]["DeparmentType"]) == 3)
            {
                dtTM.Rows[0]["Price"] = textEdit1.EditValue;
                if (Convert.ToDecimal(dtTM.Rows[0]["Price"]) <= 0)
                {
                    XtraMessageBox.Show("外发加工请填写加工费");
                    return;
                }
            }
            dtTM.Rows[0]["IsVerify"] = 3;
            dtTM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtTM.Rows[0]["VerifyDate"] = DateTime.Today;
            if (Save())
            {
                _isVerify = true;
                if (_ParentID > 0)
                    BasicClass.GetDataSet.ExecSql("Hownet.BLL.ProductionPlan", "UpPlanAmount", new object[] { _ParentID, _MainID, (int)BasicClass.Enums.TableType.ProductionPlan, _TableTypeID, true });
                //DataTable dtttt = new DataTable();
                //if (ucSizeBow1.dtDataSource != null)
                //    dtttt = ucSizeBow1.dtDataSource.Copy();
                //dtttt.TableName = "dtSizeBow";
                //DataSet dsss = new DataSet();
                //dsss.DataSetName = "Dsss";
                //dsss.Tables.Add(dtttt);
                //byte[] bb = ZipJpg.Ds2Byte(dsss);
                //DataSet ssss = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "accountDemand", new object[] { _MainID, Convert.ToInt32(_leBom.editVal), _TableTypeID, bb,new byte[0] });
                //if (ssss.Tables.Count > 0)
                //{
                //    DataTable dtDemand = ssss.Tables[0];

                if (dtDemand.Rows.Count > 0)
                {
                    DataTable dtt = dtDemand.Clone();
                    for (int i = 0; i < dtDemand.Rows.Count; i++)
                    {
                        dtDemand.Rows[i]["ProduceTaskID"] = _MainID;
                        dtDemand.Rows[i]["TableTypeID"] = _TableTypeID;
                        dtt.Rows.Clear();
                        dtt.Rows.Add(dtDemand.Rows[i].ItemArray);
                        dtDemand.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllMaterielDemand, dtt);
                    }
                }
                //}
                _barUnVerify.Enabled = true;
                _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = amountList1.IsCanEdit = false;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = amountList1.IsShowPopupMenu = false;
                if (_isVerify && (bs.Position == dtMain.Rows.Count - 1))
                    _brAddNew.Enabled = true;
                cRR.ChangeText(_MainID.ToString() + "," + userNum1.NumStr + "/" + _leDeparment.valStr);
                checkEdit1.Visible = false;
                if (_ParentID == 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否转至出工票？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        Form fr = new Hownet.Pay.frToTicket(_MainID);
                        fr.ShowDialog();
                    }
                }
            }
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            if ((bool)(dtTM.Rows[0]["IsTicket"]))
            {
                XtraMessageBox.Show("已出工票，不能弃审！");
                return;
            }
            if (Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllTM, "CheckHasToDepot", new object[] { _MainID })))
            {
                XtraMessageBox.Show("本单已有入库记录，不能弃审！");
                return;
            }
            if (_ParentID > 0)
            {
                if (DialogResult.No == XtraMessageBox.Show("弃审将删除本单，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    return;
                BasicClass.GetDataSet.ExecSql("Hownet.BLL.ProductionPlan", "UpPlanAmount", new object[] { _ParentID, _MainID, (int)BasicClass.Enums.TableType.ProductionPlan, _TableTypeID, false });
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMaterielDemand, "DelNeed", new object[] { _MainID, _TableTypeID });
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, _TableTypeID });
                BasicClass.GetDataSet.ExecSql(bllTM, "Delete", new object[] { _MainID });
                cRR.ChangeText("-1");
                this.Close();
            }
            else
            {
                dtTM.Rows[0]["IsVerify"] = 0;
                dtTM.Rows[0]["VerifyMan"] = 0;
                dtTM.Rows[0]["VerifyDate"] = DateTime.Today;
                if (Save())
                {
                    ShowView(bs.Position);
                }
            }
            checkEdit1.Visible = true;
        }

        private void _brDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (Convert.ToBoolean(dtTM.Rows[0]["IsTicket"]))
            //{
            //    XtraMessageBox.Show("本单已出工票，请先删除工票记录！");
            //    return;
            //}
            //if (Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllTM, "CheckHasToDepot", new object[] { _MainID})))
            //{
            //    XtraMessageBox.Show("本单已有入库记录，不能弃审！");
            //    return;
            //}
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, _TableTypeID });
                BasicClass.GetDataSet.ExecSql(bllTM, "Delete", new object[] { _MainID });
                cRR.ChangeText((_MainID * -1).ToString() + "," + userNum1.NumStr + "/" + _leDeparment.valStr);
                if (dtMain.Rows.Count == 1)
                {
                    this.Close();
                }
                else
                {
                    dtMain.Rows.RemoveAt(bs.Position);
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amountList2.Open(_MainID, _TableTypeID, true, radioGroup1.SelectedIndex + 2);

        }
        /// <summary>
        /// 根据物料结构主表ID显示单件用量树形
        /// </summary>
        /// <param name="ID"></param>
        void ShowBomRoot(int BomID)
        {
            _trBom.Nodes.Clear();
            if (BomID == 0)
                return;
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
            _trBom.AppendNode(new object[] { _materielID, _leMateriel.valStr }, null);
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

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == _xtpSizeBow)
            {
                if (ucSizeBow1.TaskID != _ParentID)
                {
                    ucSizeBow1.Open(_ParentID, (int)BasicClass.Enums.TableType.ProductionPlan);
                }
            }
            else if (e.Page == _xtpBOM)
            {
                _leBom.Par = new object[] { "(MaterielID=" + Convert.ToInt32(_leMateriel.editVal) * -1 + ")" };
                _leBom.FormName = (int)BasicClass.Enums.TableType.BOM;
                if (dtPP.Rows.Count > 0)
                    _leBom.editVal = Convert.ToInt32(dtPP.Rows[0]["BomID"]);
                ShowBomRoot(Convert.ToInt32(_leBom.editVal));
            }
            else if (e.Page == _xtpNeed)
            {
                if (!_isVerify)
                {
                    GetDemand();
                    //dtDemand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetTask", new object[] { _ParentID,(int)BasicClass.Enums.TableType.ProductionPlan}).Tables[0];
                }
                else
                {

                    dtDemand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetTask", new object[] { _MainID, _TableTypeID }).Tables[0];
                    _gcDemand.DataSource = dtDemand;
                }

            }
            else if (e.Page == xtraTabPage8)
            {
                if (dtPP.Rows.Count > 0)
                {
                    _lePackID.editVal = dtPP.Rows[0]["PackingMethodID"];
                    _meSewRemark.EditValue = dtPP.Rows[0]["SewingRemark"];
                }
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DataSet ds = new DataSet();
            //ds.DataSetName = "ds";
            //DataTable dtBomMain = new DataTable();
            //dtBomMain.TableName = "Main";
            //dtBomMain.Columns.Add("Num", typeof(string));
            //dtBomMain.Columns.Add("Materiel", typeof(string));
            //dtBomMain.Columns.Add("Date", typeof(DateTime));
            //dtBomMain.Columns.Add("MaterielJpg", typeof(byte[]));
            //dtBomMain.Columns.Add("Pack", typeof(string));
            //dtBomMain.Columns.Add("Remark", typeof(string));
            //dtBomMain.Columns.Add("Brand", typeof(string));
            //dtBomMain.Columns.Add("CompanyName", typeof(string));
            //dtBomMain.Columns.Add("DeparmentName", typeof(string));
            //string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            //byte[] bbb = new byte[0];
            //if (picName.Trim() != string.Empty)
            //    bbb = BasicClass.FileUpDown.getServerPic(picName);
            //dtBomMain.Rows.Add(userNum1.NumStr, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr,_leCompany.valStr, _leDeparment.valStr);
            //DataTable dtAmount = amountList1.dtDataSource.Copy();
            //dtAmount.TableName = "AmountInfo";
            //ds.Tables.Add(dtBomMain);
            //ds.Tables.Add(dtAmount);
            //DataTable dttt = new DataTable();
            //dttt.TableName = "dtDemand";
            //dttt.Columns.Add("MaterielName", typeof(string));
            //dttt.Columns.Add("OneAmount", typeof(string));
            //dttt.Columns.Add("MeasureName", typeof(string));
            //dttt.Columns.Add("ColorName", typeof(string));
            //dttt.Columns.Add("Amount", typeof(double));
            //DataTable dtSizeDemand = dttt.Clone();
            //dtSizeDemand.TableName = "SizeDemand";
            //DataTable dtCSDemand = dttt.Clone();
            //dtCSDemand.Columns.Add("ColorOneName", typeof(string));
            //dtCSDemand.Columns.Add("ColorTwoName", typeof(string));
            //dtCSDemand.Columns.Add("SizeName", typeof(string));
            //dtCSDemand.TableName = "CSDemand";
            //int _colorID, _colorOneID, _colorTwoID, _sizeID;
            //bool t = true;
            //for (int i = 0; i < _gvDemand.RowCount; i++)
            //{
            //    _colorID = _colorOneID = _colorTwoID = _sizeID = 0;
            //    _colorID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coColorID));
            //    _colorOneID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coColorOneID));
            //    _colorTwoID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coColorTwoID));
            //    _sizeID = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coSizeID));
            //    DataRow dr = dttt.NewRow();
            //    dr[0] = _gvDemand.GetRowCellDisplayText(i, _coMaterielID);
            //    t = true;
            //    _trBom.MoveLast();
            //    int _trCount = _trBom.FocusedNode.Id;
            //    int a = 0;
            //    _trBom.MoveFirst();
            //    while (t)
            //    {
            //        if (_trBom.FocusedNode.GetValue(_trMaterielID).Equals(_gvDemand.GetRowCellValue(i, _coMaterielID)))
            //        {
            //            dr[1] = Convert.ToDouble(_trBom.FocusedNode.GetValue(_trAmount));
            //            break;
            //        }
            //        if (a == _trCount)
            //            break;
            //        _trBom.MoveNext();
            //        a++;
            //    }
            //    if (dr[1].ToString() == string.Empty)
            //        dr[1] = 1;
            //    dr[2] = _gvDemand.GetRowCellDisplayText(i, _coMeasureID);
            //    dr[4] = Convert.ToDouble(_gvDemand.GetRowCellDisplayText(i, _coAmount));
            //    if ((_colorID > 0 && _colorOneID == 0 && _colorTwoID == 0 && _sizeID == 0) || (_colorID == 0 && _colorOneID > 0 && _colorTwoID == 0 && _sizeID == 0)
            //      || (_colorID == 0 && _colorOneID == 0 && _colorTwoID > 0 && _sizeID == 0))
            //    {
            //        if (_colorID > 0)
            //            dr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorID);
            //        else if (_colorOneID > 0)
            //            dr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorOneID);
            //        else if (_colorTwoID > 0)
            //            dr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorTwoID);
            //        dttt.Rows.Add(dr);
            //    }
            //    else if (_colorID == 0 && _colorOneID == 0 && _colorTwoID == 0 && _sizeID > 0)
            //    {
            //        dr[3] = _gvDemand.GetRowCellDisplayText(i, _coSizeID);
            //        dtSizeDemand.Rows.Add(dr.ItemArray);
            //    }
            //    else if ((_colorID > 0 || _colorOneID > 0 || _colorTwoID > 0) && _sizeID > 0)
            //    {
            //        DataRow drr = dtCSDemand.NewRow();
            //        drr[0] = dr[0];
            //        drr[1] = dr[1];
            //        drr[2] = dr[2];
            //        drr[4] = dr[4];
            //        drr[3] = _gvDemand.GetRowCellDisplayText(i, _coColorID);
            //        drr[5] = _gvDemand.GetRowCellDisplayText(i, _coColorOneID);
            //        drr[6] = _gvDemand.GetRowCellDisplayText(i, _coColorTwoID);
            //        drr[7] = _gvDemand.GetRowCellDisplayText(i, _coSizeID);
            //        dtCSDemand.Rows.Add(drr);
            //    }
            //}
            //ds.Tables.Add(dttt);
            //ds.Tables.Add(dtSizeDemand);
            //ds.Tables.Add(dtCSDemand);
            //DataTable dtSizePart = BasicClass.GetDataSet.GetDS("Hownet.BLL.SizeTableTask", "GetReport", new object[] { _ParentID }).Tables[0];
            //dtSizePart.TableName = "SizePart";
            //ds.Tables.Add(dtSizePart.Copy());
            //BaseForm.PrintClass.TaskBom(ds);
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
            dtBomMain.Columns.Add("BedNO", typeof(string));
            dtBomMain.Columns.Add("LastDate", typeof(DateTime));
            dtBomMain.Columns.Add("FillDate", typeof(DateTime));
            string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            byte[] bbb = new byte[0];
            //if (picName.Trim() != string.Empty)
            //    bbb = BasicClass.FileUpDown.getServerPic(picName);
            dtBomMain.Rows.Add(userNum1.NumStr, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, _leDeparment.valStr, _ltBedNO.EditVal, Convert.ToDateTime(_ldLastDate.val) ,Convert.ToDateTime(dtTM.Rows[0]["FillDate"]));
            DataTable dtAmount = amountList1.dtDataSource.Copy();
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
            //DataTable dtSizePart = new DataTable();// BasicClass.GetDataSet.GetDS("Hownet.BLL.SizeTableTask", "GetReport", new object[] { _MainID }).Tables[0];
            //dtSizePart.TableName = "SizePart";
            //dtSizePart.Columns.Add("SizePartName", typeof(string));
            //dtSizePart.Columns.Add("Columns1", typeof(string));
            //dtSizePart.Columns.Add("Columns2", typeof(string));
            //dtSizePart.Columns.Add("Columns3", typeof(string));
            //dtSizePart.Columns.Add("Columns4", typeof(string));
            //dtSizePart.Columns.Add("Columns5", typeof(string));
            //dtSizePart.Columns.Add("Columns6", typeof(string));
            //dtSizePart.Columns.Add("Columns7", typeof(string));
            //dtSizePart.Columns.Add("Columns8", typeof(string));
            //dtSizePart.Columns.Add("Columns9", typeof(string));
            //dtSizePart.Columns.Add("Columns10", typeof(string));
            //dtSizePart.Columns.Add("Columns11", typeof(string));
            //dtSizePart.Columns.Add("Columns12", typeof(string));
            //for (int i = 0; i < _gvSizeTable.RowCount; i++)
            //{
            //    DataRow dr = dtSizePart.NewRow();
            //    for (int j = 0; j < _gvSizeTable.VisibleColumns.Count; j++)
            //    {
            //        dr[j] = _gvSizeTable.GetRowCellDisplayText(i, _gvSizeTable.VisibleColumns[j]);
            //    }
            //    dtSizePart.Rows.Add(dr);
            //}
            //ds.Tables.Add(dtSizePart);
            ds.Tables.Add(ucSizeList1.dtPrint);
            ds.Tables.Add(dtNoCS);
            BaseForm.PrintClass.TaskReport(ds);
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue != null)
            {
                Form fr = new BaseForm.ShowPic(fileName);
                fr.ShowDialog();
            }
        }
        private void ShowBom()
        {
            _leBom.Par = new object[] { "(MaterielID=" + _materielID * -1 + ") And (IsVerify=3)" };
            _leBom.FormName = (int)BasicClass.Enums.TableType.BOM;
            _leBom.editVal = 0;
        }
        private void _leMateriel_EditValueChanged_1(object val, string text)
        {
            dtTM.Rows[0]["MaterielID"] = _materielID = Convert.ToInt32(val);
            ShowBom();
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
            #region
            if (_MainID==0)
            {
                _ltBedNO.EditVal = BasicClass.GetDataSet.GetOne(bllTM, "GetMaxBedNO", new object[] { _materielID }).ToString();
                amountList1.Open(true, BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderMain, "GetCS2RepByMatID", new object[] {_materielID }).Tables[0]);
            }
            #endregion
        }

        private void amountList1_EditValueChanged(object val, string text)
        {
            if (xtraTabControl1.SelectedTabPage != _xtpNeed)
                xtraTabControl1.SelectedTabPage = _xtpNeed;
            else
                GetDemand();
        }
        private void GetDemand()
        {
            if (_isVerify)
                return;
            if (_bomID == 0)
                return;
            DataTable dtttt = new DataTable();
            if (ucSizeBow1.dtDataSource != null)
                dtttt = ucSizeBow1.dtDataSource.Copy();
            dtttt.TableName = "dtSizeBow";
            DataSet dsss = new DataSet();
            dsss.DataSetName = "Dsss";
            dsss.Tables.Add(dtttt);
            byte[] bb = ZipJpg.Ds2Byte(dsss);
            dsss.Tables.Remove(dtttt);
            dsss.Tables.Add(amountList1.GetDTList());
            byte[] bbai = ZipJpg.Ds2Byte(dsss);
            DataSet ssss = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "accountDemand", new object[] { _MainID, _bomID, _TableTypeID, bb, bbai, Convert.ToInt32(_leCompany.editVal), Convert.ToInt32(_leBrand.editVal) });
            if (ssss.Tables.Count > 0)
                dtDemand = ssss.Tables[0];
            _gcDemand.DataSource = dtDemand;
        }

        private void _leBom_EditValueChanged(object val, string text)
        {
            _bomID = int.Parse(val.ToString());
            ShowBomRoot(_bomID);
           // GetDemand();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                _leDeparment.FormName = (int)BasicClass.Enums.TableType.JGC;
                dtTM.Rows[0]["DeparmentType"] = 3;
                labelControl1.Visible = textEdit1.Visible = true;
            }
            else
            {
                _leDeparment.FormName = (int)BasicClass.Enums.TableType.Deparment;
                dtTM.Rows[0]["DeparmentType"] = 0;
                labelControl1.Visible = textEdit1.Visible = false;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            dtBomMain.Columns.Add("Amount", typeof(int));
            dtBomMain.Columns.Add("MaterielRemark");
            string DD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=" + Convert.ToInt32(dtTM.Rows[0]["FilMan"]) + ")" }).Tables[0].Rows[0]["TrueName"].ToString();
            string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            byte[] bbb = new byte[0];
            if (picName.Trim() != string.Empty)
                bbb = BasicClass.FileUpDown.getServerPic(picName);
            string MR = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _materielID + ")" }).Tables[0].Rows[0]["Remark"].ToString();
            dtBomMain.Rows.Add(userNum1.NumStr, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, _leDeparment.valStr, _ldLastDate.strLab, DD, amountList1.SumAmount, MR);
            DataTable dtAmount = amountList1.dtDataSource.Copy();
            dtAmount.TableName = "AmountInfo";
            if (dtAmount.Rows.Count < 11)
            {
                for (int i = dtAmount.Rows.Count; i < 11; i++)
                {
                    dtAmount.Rows.Add(dtAmount.NewRow());
                }
            }
            DataTable dtARR = dtAR.Copy();
            dtARR.TableName = "dtAR";
            ds.Tables.Add(dtARR);
            ds.Tables.Add(dtBomMain);
            ds.Tables.Add(dtAmount);
            BaseForm.PrintClass.PrintTaskAmount(ds);
        }
        //竖式物料
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            dtBomMain.Columns.Add("BedNO", typeof(string));
            dtBomMain.Columns.Add("LastDate", typeof(DateTime));
            dtBomMain.Columns.Add("FillDate", typeof(DateTime));
            string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            byte[] bbb = new byte[0];
            //if (picName.Trim() != string.Empty)
            //    bbb = BasicClass.FileUpDown.getServerPic(picName);
            dtBomMain.Rows.Add(userNum1.NumStr, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, _leDeparment.valStr, _ltBedNO.EditVal, Convert.ToDateTime(_ldLastDate.val), Convert.ToDateTime(dtTM.Rows[0]["FillDate"]));
            DataTable dtAmount = amountList1.dtDataSource.Copy();
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
            DataTable dtD = new DataTable();
            dtD.Columns.Add("物料名");
            dtD.Columns.Add("单位");
            dtD.Columns.Add("单耗");
            dtD.Columns.Add("配料总量");
            dtD.Columns.Add("颜色");
             dtD.Columns.Add("尺码");
             dtD.Columns.Add("配色一");
             dtD.Columns.Add("配色二");
             dtD.Columns.Add("工艺部位");
             dtD.Columns.Add("使用方法");
             dtD.Columns.Add("备注");
            dtD.TableName = "Demand";
            DataTable dtBom = ((DataTable)_trBom.DataSource).Copy();
            dtBom.TableName = "bom";
            //int _mm = 0;
            //int _dd = 0;
            bool t = true;
            for (int i = 0; i < _gvDemand.RowCount; i++)
            {
                DataRow dr = dtD.NewRow();
                dr[0] = _gvDemand.GetRowCellDisplayText(i, _coMaterielID);
                dr[1] = _gvDemand.GetRowCellDisplayText(i, _coMeasureID);
                dr[3] = _gvDemand.GetRowCellDisplayText(i, _coAmount);
                dr[4] = _gvDemand.GetRowCellDisplayText(i, _coColorID);
                dr[5] = _gvDemand.GetRowCellDisplayText(i, _coSizeID);
                dr[6] = _gvDemand.GetRowCellDisplayText(i, _coColorOneID);
                dr[7] = _gvDemand.GetRowCellDisplayText(i, _coColorTwoID);
                //_mm = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _coMaterielID));
                //_dd = Convert.ToInt32(_gvDemand.GetRowCellValue(i, _deDepID));
                //DataRow[] drs = dtBom.Select("(MaterielID=" + _mm + ") And ");



                t = true;
                _trBom.MoveLast();
                int _trCount = _trBom.FocusedNode.Id;
                int a = 0;
                _trBom.MoveFirst();
                while (t)
                {
                    if (_trBom.FocusedNode.GetValue(_trMaterielID).Equals(_gvDemand.GetRowCellValue(i, _coMaterielID)))
                    {
                        dr[2] = Convert.ToDouble(_trBom.FocusedNode.GetValue(_trAmount));
                        dr[8] = _trBom.FocusedNode.GetDisplayText(_trPartName);
                        dr[9] = _trBom.FocusedNode.GetDisplayText(_trUsing);
                        break;
                    }
                    if (a == _trCount)
                        break;
                    _trBom.MoveNext();
                    a++;
                }
                if (dr[2].ToString() == string.Empty)
                    dr[2] = 1;
                dtD.Rows.Add(dr);
            }
            ds.Tables.Add(dtD);
            Hownet.BaseForm.PrintClass.TaskReportLine(ds);
        }

    }
}