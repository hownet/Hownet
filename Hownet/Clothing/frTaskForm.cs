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

namespace Hownet.Clothing
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
        public frTaskForm(BasicClass.cResult rr,int MainID,int a)
            : this()
        {
            cRR = rr;
            _MainID = MainID;
        }
        BindingSource bs = new BindingSource();
        private string bllTM = "Hownet.BLL.ProductTaskMain";
        private string bllPW = "Hownet.BLL.ProductWorkingMain";
        private string bllCA = "Hownet.BLL.ClothAmount";
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
        DataTable dtJGC = new DataTable();
        DataTable dtCA = new DataTable();
        DataTable dtBrand = new DataTable();
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
            _coColorID.ColumnEdit =  BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coWorkingID.ColumnEdit = _coInfoWorkID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
            _coInfoEmployee.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
            dtJGC = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0];
             dtBrand = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetLookupList", new object[] { "(AttributeID=5)" }).Tables[0];
            DataRow drB = dtBrand.NewRow();
            drB["ID"] = 0;
            drB["Name"] = string.Empty;
            dtBrand.Rows.InsertAt(drB, 0);
            for (int i = 0; i < dtBrand.Rows.Count; i++)
            {
                _reBrand.Items.Add(dtBrand.Rows[i]["Name"].ToString());
            }
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
                       
            if (_ParentID > 0)
            {
                dtMain.Columns.Add("ID",typeof(int));
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
                if(_ParentID>0)
                    dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetList", new object[] { "(ID=" + _ParentID + ")" }).Tables[0];
                dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtTM.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["SalesOrderInfoID"] = dr["MaterielID"] = dr["BrandID"] = dr["PWorkingID"] = dr["BomID"] = dr["DeparmentID"] = dr["UpData"] = dr["VerifyMan"] = _MainID = 0;
                dr["FillDate"] = dr["DateTime"] =BasicClass.GetDataSet.GetDateTime();
                dr["LastDate"] =  DateTime.Today.AddDays(15);
                dr["FilMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["IsBom"] = dr["IsTicket"] = false;
                dr["TicketDate"] = dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] =  "";
                dr["SewingRemark"] = string.Empty;
                dr["ParentID"] = _ParentID;
                dr["DeparmentType"] = 0;
                dr["BedNO"] = BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductionPlan", "GetMaxTask", new object[] {_ParentID });
                if (dtPP.Rows.Count > 0)
                {
                    dr["DateTime"] = dtPP.Rows[0]["DateTime"];
                    //dr["Num"] = dtPP.Rows[0]["Num"];
                }
                else
                {
                    //dr["Num"] = BasicClass.GetDataSet.GetOne(bllTM, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, _numType });
                }
                dr["Num"] = 0;
                dtTM.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            dtTick = new DataTable();
            KaiDan = 0;
            
            _leCompany.editVal = dtTM.Rows[0]["CompanyID"];
            _leMateriel.editVal = _materielID = Convert.ToInt32(dtTM.Rows[0]["MaterielID"]);
            _leBrand.editVal = _brandID = Convert.ToInt32(dtTM.Rows[0]["BrandID"]);
            _ldLastDate.val = dtTM.Rows[0]["LastDate"];
            _ltBedNO.val = dtTM.Rows[0]["BedNO"].ToString();
            checkEdit1.Checked = Convert.ToInt32(dtTM.Rows[0]["DeparmentType"]) == 3;
            _leDeparment.editVal = dtTM.Rows[0]["DeparmentID"];
            dtCA = BasicClass.GetDataSet.GetDS(bllCA, "GetList", new object[] {"(MainID="+_MainID+")" }).Tables[0];
            gridControl1.DataSource = dtCA;
            _ParentID = Convert.ToInt32(dtTM.Rows[0]["ParentID"]);
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _isBom = (bool)(dtTM.Rows[0]["IsBom"]);
            _isVerify = (int.Parse(dtTM.Rows[0]["IsVerify"].ToString()) > 2);
            _ldLastDate.t = _isVerify;
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _barUnVerify.Enabled = _isVerify;
            _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = !_isVerify;
            _ltBedNO.IsCanEdit =  !_isVerify;
            _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled =  !_isVerify;
            if (_isVerify && (p == dtMain.Rows.Count - 1))
                _brAddNew.Enabled = true;
            _PWID = int.Parse(dtTM.Rows[0]["PWorkingID"].ToString());
            _lePW.editVal = _PWID = int.Parse(dtTM.Rows[0]["PWorkingID"].ToString());
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
            _leCompany.IsNotCanEdit = _leDeparment.IsNotCanEdit = _leBrand.IsNotCanEdit = _leMateriel.IsNotCanEdit = _isVerify;
            if (_ParentID > 0)
            {
                dtPP = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetList", new object[] { "(ID=" + _ParentID + ")" }).Tables[0];
                //   _lePackID.t = true;
                _leMateriel.IsNotCanEdit = _leBrand.IsNotCanEdit = true;
                _leMateriel.editVal = _materielID = Convert.ToInt32(dtPP.Rows[0]["MaterielID"]);
                _leCompany.editVal = Convert.ToInt32(dtPP.Rows[0]["CompanyID"]);
                _leBrand.editVal = Convert.ToInt32(dtPP.Rows[0]["BrandID"]);
                _ldLastDate.val = dtTM.Rows[0]["LastDate"];
                dtTM.Rows[0]["LastDate"] = dtPP.Rows[0]["LastDate"];
                
            }
            else
            {
            }
            checkEdit1.Visible = !_isVerify;
            dtTick = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicket, "GetList", new object[] { "(TaskID="+_MainID+")" }).Tables[0];
            _gcTictMain.DataSource = dtTick;
            gridView1.Columns["A"].Visible = false;
            gridView1.Columns["ID"].Visible = false;
            gridView1.Columns["MainID"].Visible = false;
            gridView1.Columns["ColorID"].Visible = false;
            gridView1.Columns["BrandID"].Visible = false;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].FieldName != "BrandName")
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            gridView1.OptionsBehavior.Editable = linkLabel1.Visible = !_isVerify;
            SumAmount();
            ShowPW();
            ShowPWI();
            _gcWorkTickInfo.DataSource = null;
            _meSewRemark.EditValue = dtTM.Rows[0]["SewingRemark"];
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
                _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = !_isVerify;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = !_isVerify;
                if (_isVerify && (bs.Position == dtMain.Rows.Count - 1))
                    _brAddNew.Enabled = true;
                XtraMessageBox.Show("保存成功！");
            }
        }
        private void ShowPW()
        {
            _lePW.Par = new object[] { "(MaterielID=" + dtTM.Rows[0]["MaterielID"] + ")" };
            _lePW.FormName = (int)BasicClass.Enums.TableType.PW;
        }
        private bool Save()
        {
            if(int.Parse( _leMateriel.editVal.ToString())==0)
            {
                XtraMessageBox.Show("请选择款号！");
                return false;
            }
            if(int.Parse(_leBrand.editVal.ToString())==0)
            {
                XtraMessageBox.Show("请选择商标！");
                return false;
            }


            dtTM.Rows[0]["CompanyID"] = _leCompany.editVal;
            dtTM.Rows[0]["MaterielID"] = _leMateriel.editVal;
            dtTM.Rows[0]["BrandID"] = _leBrand.editVal;
            dtTM.Rows[0]["DateTime"] = userNum1.NumDate;
            dtTM.Rows[0]["LastDate"] = _ldLastDate.val;
            dtTM.Rows[0]["BedNO"] = _ltBedNO.val;
            dtTM.Rows[0]["DeparmentID"] = _leDeparment.editVal;
            dtTM.Rows[0]["A"] = 1;
            dtTM.Rows[0]["SewingRemark"]= _meSewRemark.EditValue ;
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
                else
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
            DataTable dtTem = dtCA.Clone();
            int a = 0;
            for (int i = 0; i < dtCA.Rows.Count; i++)
            {
                dtTem.Rows.Clear();
                dtCA.Rows[i]["MainID"] = _MainID;
                dtTem.Rows.Add(dtCA.Rows[i].ItemArray);
                a = Convert.ToInt32(dtCA.Rows[i]["A"]);
                if (a > 1)
                {
                    if (a == 2)
                        BasicClass.GetDataSet.UpData("Hownet.BLL.ClothAmount", dtTem);
                    else if (a == 3)
                        dtCA.Rows[i]["ID"] = BasicClass.GetDataSet.Add("Hownet.BLL.ClothAmount", dtTem);
                    dtCA.Rows[i]["A"] = 1;
                }
            }
            dtTem = dtTick.Clone();
            for (int i = 0; i < dtTick.Rows.Count; i++)
            {
                dtTem.Rows.Clear();
                dtTick.Rows[i]["TaskID"] = _MainID;
                dtTem.Rows.Add(dtTick.Rows[i].ItemArray);
                a = Convert.ToInt32(dtTick.Rows[i]["A"]);
                if (a > 1)
                {
                    if (a == 2)
                        BasicClass.GetDataSet.UpData("Hownet.BLL.WorkTicket", dtTem);
                    else if (a == 3)
                        dtTick.Rows[i]["ID"] = BasicClass.GetDataSet.Add("Hownet.BLL.WorkTicket", dtTem);
                    dtTick.Rows[i]["A"] = 1;
                }
            }
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
        private void SumAmount()
        {
            DataRow dr = dtCA.NewRow();
            dr["A"] = 4;
            dr["ID"] = 0;
            dr["BoxNum"] = "合计";
            int amount = 0;
            for (int i = 1; i < 16; i++)
            {
                amount = 0;
                for (int j = 1; j < dtCA.Rows.Count; j++)
                {
                    try
                    {
                        amount += Convert.ToInt32(dtCA.Rows[j]["Size" + i.ToString()]);
                    }
                    catch
                    {
                    }
                }
                if (amount > 0)
                    dr["Size" + i.ToString()] = amount;
            }
            for (int j = 1; j < dtCA.Rows.Count; j++)
            {
                try
                {
                    amount += Convert.ToInt32(dtCA.Rows[j]["SumAmount"]);
                }
                catch
                {
                }
            }
            if (amount > 0)
                dr["SumAmount"] = amount;
            dtCA.Rows.Add(dr);
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
            ShowPW();
        }
        private void ShowPWI()
        {
            _lePWI.Par = new object[] { int.Parse(dtTM.Rows[0]["PWorkingID"].ToString()) };
            _lePWI.FormName = (int)BasicClass.Enums.TableType.PWI;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KaiDan = 2;
            BasicClass.cResult r = new BasicClass.cResult();
            r.TableChanged += new BasicClass.TableNumChangedHandler(r_TableChanged);
            Form fr = new  Ticket2Task(r,_MainID);
            fr.ShowDialog();
        }
        void r_TableChanged(DataSet ds)
        {
            dtCA = ds.Tables[0];
            gridControl1.DataSource = dtCA;
            dtTick = ds.Tables[1];
            _gcTictMain.DataSource = dtTick;
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_MainID == 0)
            {
                XtraMessageBox.Show("请先保存！");
                return;
            }
            if (DialogResult.No == XtraMessageBox.Show("确认审核本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            if (Convert.ToInt32(_lePW.editVal) == 0)
            {
                XtraMessageBox.Show("请选择工艺单");
                return;
            }
            dtTM.Rows[0]["PWorkingID"] = _lePW.editVal;
            dtTM.Rows[0]["IsVerify"] = 3;
            dtTM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtTM.Rows[0]["VerifyDate"] = DateTime.Today;
            dtTM.Rows[0]["IsTicket"] = true;
            if (Save())
            {
                _isVerify = true;
                BasicClass.GetDataSet.ExecSql("Hownet.BLL.BaseFile.MakBox", "MakClothing", new object[] {_MainID });
                if (_ParentID > 0)
                    BasicClass.GetDataSet.ExecSql("Hownet.BLL.ProductionPlan", "UpPlanAmount", new object[] { _ParentID, _MainID, (int)BasicClass.Enums.TableType.ProductionPlan, _TableTypeID, true });
                _barUnVerify.Enabled = true;
                _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit  = false;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled =  false;
                if (_isVerify && (bs.Position == dtMain.Rows.Count - 1))
                    _brAddNew.Enabled = true;
                cRR.ChangeText(_MainID.ToString() + "," + userNum1.NumStr + "/" + _leDeparment.valStr);
                checkEdit1.Visible = false;
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "InNoList", new object[] { 0 });
            }
            ShowPWI();
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

                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, _TableTypeID });
                    ShowView(bs.Position);
                }
            }
            checkEdit1.Visible = true;
        }

        private void _brDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, _TableTypeID });
                BasicClass.GetDataSet.ExecSql(bllTM, "Delete", new object[] { _MainID });
                cRR.ChangeText((_MainID * -1).ToString() + "," + userNum1.NumStr + "/" + _leDeparment.valStr);
                if (dtMain.Rows.Count ==1)
                {
                    this.Close();
                }
                else
                {
                    dtMain.Rows.RemoveAt(bs.Position);
                }
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
           
        }

        private void _leMateriel_EditValueChanged_1(object val, string text)
        {
            dtTM.Rows[0]["MaterielID"] = _materielID = Convert.ToInt32(val);
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



        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                _leDeparment.FormName = (int)BasicClass.Enums.TableType.JGC;
                dtTM.Rows[0]["DeparmentType"] = 3;
            }
            else
            {
                _leDeparment.FormName = (int)BasicClass.Enums.TableType.Deparment;
                dtTM.Rows[0]["DeparmentType"] = 0;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BoxNum"));
                gridView1.Columns["BrandName"].ColumnEdit = _reBrand;
                gridView1.Columns["BrandName"].OptionsColumn.AllowEdit = true;
            }
            catch
            {
                gridView1.Columns["BrandName"].ColumnEdit = null;
                gridView1.Columns["BrandName"].OptionsColumn.AllowEdit = false;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "BrandName")
            {
                if (_reBrand.Items.IndexOf(e.Value.ToString().Trim()) == -1)
                {
                    gridView1.SetFocusedValue(string.Empty);
                }
                DataRow[] drs = dtTick.Select("(BoxNum=" + gridView1.GetFocusedRowCellValue("BoxNum") + ")");
                DataRow[] drsb = dtBrand.Select("(Name='" + e.Value + "')");
                int brandID = 0;
                if (drsb.Length > 0)
                    brandID = Convert.ToInt32(drsb[0]["ID"]);
                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i]["BrandName"] = e.Value;
                    drs[i]["BrandID"] = brandID;
                }
                dtTick.AcceptChanges();
            }
        }

        private void _lePW_EditValueChanged(object val, string text)
        {

            _PWID = int.Parse(val.ToString());
            DataSet dds = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductWorkingInfo", "GetInfoList", new object[] { Convert.ToInt32(val) });
            //gridControl2.DataSource = dtInfo = dds.Tables[0];
            _gcGYD.DataSource = dds.Tables[0];// BasicClass.GetDataSet.GetDS(bllPW, "GetInfoList", new object[] { int.Parse(val.ToString()) }).Tables[0];
            //_gcGYD.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(_gcGYD_ViewRegistered);
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _gvWorkTickInfo.Columns.Clear();
            if (_isVerify)
            {
                _gcWorkTickInfo.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetWorkListByPW", new object[] { _MainID, _PWID, int.Parse(dtTM.Rows[0]["MaterielID"].ToString()), 0 }).Tables[0];
                for (int i = 0; i < 7; i++)
                {
                    _gvWorkTickInfo.Columns[i].Width = 70;
                    _gvWorkTickInfo.Columns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                _gvWorkTickInfo.Columns["班组"].ColumnEdit = BaseForm.RepositoryItem._reDeparment;
                _gvWorkTickInfo.Columns["颜色"].ColumnEdit = BaseForm.RepositoryItem._reColor;
                _gvWorkTickInfo.Columns["尺码"].ColumnEdit = BaseForm.RepositoryItem._reSize;
                _gvWorkTickInfo.Columns["商标"].ColumnEdit = BaseForm.RepositoryItem._reBrand ;
                _gvWorkTickInfo.Columns["ID"].Visible = false;

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

        private void _gvWorkTickInfo_DoubleClick(object sender, EventArgs e)
        {
            if (_gvWorkTickInfo.FocusedRowHandle > -1&&_gvWorkTickInfo.FocusedColumn.ColumnHandle>6)
            {
                if (_gvWorkTickInfo.GetFocusedValue().ToString().Trim().Length == 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否选择生产员工", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        BasicClass.cResult rEM = new BasicClass.cResult();
                        rEM.TextChanged += new BasicClass.TextChangedHandler(rEM_TextChanged);
                        int _BoxNum = Convert.ToInt32(_gvWorkTickInfo.GetFocusedRowCellValue("箱号"));
                        int _SizeID = Convert.ToInt32(_gvWorkTickInfo.GetFocusedRowCellValue(gridColumn2));
                        string _WorkingName = _gvWorkTickInfo.FocusedColumn.FieldName;

                        //Form fr = new frSelectEmp(rEM);
                        Form fr = new frSelectEmp(new BasicClass.cResult(), -1, _MainID, _BoxNum, _WorkingName, _SizeID);
                        fr.ShowDialog();
                        _gcWorkTickInfo.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetWorkListByPW", new object[] { _MainID, _PWID, int.Parse(dtTM.Rows[0]["MaterielID"].ToString()), 0 }).Tables[0];

                    }
                }
            }
        }

        void rEM_TextChanged(string s)
        {
            if (s.Length > 0)
            {
                string[] ss = s.Split('+');
                if (Convert.ToInt32(ss[0]) != 0)
                {
                    int _TicketID = Convert.ToInt32(_gvWorkTickInfo.GetFocusedRowCellValue("ID"));
                    int _EmpID = Convert.ToInt32(ss[0]);
                    string aaa = _gvWorkTickInfo.FocusedColumn.FieldName;
                    aaa = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllWorkTicketInfo, "UpWorkEmp", new object[] { _TicketID, aaa, _EmpID }).ToString();
                    _gvWorkTickInfo.SetFocusedValue(ss[1]+"/"+ aaa);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Form fr = new Hownet.Pay.BarTaskCar(_MainID, int.Parse(dtTM.Rows[0]["PWorkingID"].ToString()), _leMateriel.valStr, _leBrand.valStr,userNum1.NumStr, _materielID);
            fr.ShowDialog();
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
                if (!(bool)(BasicClass.GetDataSet.GetOne("Hownet.BLL.WorkTicketInfo", "CheckWorking", new object[] { _MainID, int.Parse(_lePWI.editVal.ToString()), Convert.ToInt32(_leCompany.editVal) })))
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
                    ds.Tables[0].Rows[i]["TaskNum"] = userNum1.NumStr;
                    ds.Tables[0].Rows[i]["BrandName"] = _leBrand.valStr;
                    ds.Tables[0].Rows[i]["BarcodeNum"] = _MainID.ToString().PadLeft(5, '0') + ds.Tables[0].Rows[i]["BoxNum"].ToString().PadLeft(4, '0') + ds.Tables[0].Rows[i]["WorkingID"].ToString().PadLeft(3, '0');
                }
                BaseForm.PrintClass.PrintTicketLine(ds);
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

        private void _gwTictInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _gwTictInfo.OptionsBehavior.Editable = false;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_gwTictInfo.GetFocusedRowCellDisplayText(_coInfoEmployee).Trim() == "")
                _gwTictInfo.OptionsBehavior.Editable = true;
            else
                XtraMessageBox.Show("该工序已回收，不能更改数量");
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void _gwTictInfo_DoubleClick(object sender, EventArgs e)
        {
            int _rowid = _gwTictInfo.FocusedRowHandle;
            if (_rowid > -1)
            {
                BasicClass.cResult rWTI = new BasicClass.cResult();
                rWTI.TextChanged += new BasicClass.TextChangedHandler(rWTI_TextChanged);
                Form fr = new frSelectEmp(rWTI,Convert.ToInt32(_gwTictInfo.GetFocusedRowCellValue(_coInfoID)));
                fr.ShowDialog();
            }
        }

        void rWTI_TextChanged(string s)
        {
            dtWTI = BasicClass.GetDataSet.GetDS(bllTM, "GetTicketInfo", new object[] { int.Parse(_gwTictMain.GetFocusedRowCellValue(_coWTID).ToString()) }).Tables[0];
            _gcTictInfo.DataSource = dtWTI;
            _gwTictInfo.OptionsBehavior.Editable = false;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "InNoList", new object[] { 0});
        }

    }
}