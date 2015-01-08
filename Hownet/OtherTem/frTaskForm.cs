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

namespace Hownet.OtherTem
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
        //int KaiDan = 0;
        int _TableTypeID = (int)BasicClass.Enums.TableType._制单入库;
        //int _PWID = 0;
        int _numType = 0;
        //int _bomID = 0;
        int _materielID, _brandID;
        DataTable dtTM = new DataTable();
        //DataTable dtTick = new DataTable();
        //DataTable dtWTI = new DataTable();
        //DataTable dtPP = new DataTable();
        //DataTable dtDemand = new DataTable();
        //DataTable dtJGC = new DataTable();
        //DataTable dtOTSet = new DataTable();
        //DataTable dtAR = new DataTable();
        //int _LR = 0;
        //DataTable dtLR = new DataTable();
        DataTable dtDep = new DataTable();
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
            dtDep = BasicClass.GetDataSet.GetBySql("Select ID,Name From Deparment Where TypeID=42");
            lookUpEdit1.Properties.DataSource = dtDep;
            if (dtDep.Rows.Count > 0)
                lookUpEdit1.EditValue = Convert.ToInt32(dtDep.Rows[0]["ID"]);
            else
                lookUpEdit1.EditValue = 0;
            amountList1.IsCanEditCS = false;
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
               dr["Num"] = 0;
                dtTM.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }

            _leCompany.editVal = dtTM.Rows[0]["CompanyID"];
            _leMateriel.editVal = _materielID = Convert.ToInt32(dtTM.Rows[0]["MaterielID"]);
            _leBrand.editVal = _brandID = Convert.ToInt32(dtTM.Rows[0]["BrandID"]);
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
            lookUpEdit1.EditValue = Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]);
            _ParentID = Convert.ToInt32(dtTM.Rows[0]["ParentID"]);
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _isBom = (bool)(dtTM.Rows[0]["IsBom"]);
            _isVerify = (int.Parse(dtTM.Rows[0]["IsVerify"].ToString()) > 2);
            _isTick = (bool)(dtTM.Rows[0]["IsTicket"]);
            _barUnVerify.Enabled = _isVerify;
            _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = !_isVerify;
            amountList1.IsShowPopupMenu = amountList1.IsCanEdit = !_isVerify;
            _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = amountList1.IsShowPopupMenu = !_isVerify;
            if (_isVerify && (p == dtMain.Rows.Count - 1))
                _brAddNew.Enabled = true;
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

            _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = _leMateriel.IsNotCanEdit = _isVerify;

            lookUpEdit1.Enabled = amountList1.IsCanEditCS = !_isVerify;
            
            amountList1.IsCanEdit = !_isVerify;


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
            if(Convert.ToInt32(lookUpEdit1.EditValue)==0)
            {
                XtraMessageBox.Show("请选择存放仓库！");
                return false;
            }
            dtTM.Rows[0]["CompanyID"] = _leCompany.editVal;
            dtTM.Rows[0]["MaterielID"] = _leMateriel.editVal;
            dtTM.Rows[0]["BrandID"] = _leBrand.editVal;
            dtTM.Rows[0]["DateTime"] = userNum1.NumDate;
            dtTM.Rows[0]["PWorkingID"] = 0;
            dtTM.Rows[0]["A"] = 1;
            dtTM.Rows[0]["DeparmentID"] = lookUpEdit1.EditValue;
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
           

            dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            userNum1.ClearData();
            userNum1.NumStr = "SHRK";
            userNum1.Num = Convert.ToInt32(dtTM.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtTM.Rows[0]["DateTime"]);
            if (_MainID > 0)
                userNum1.LastEdit = dtTM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            if (_isVerify)
                userNum1.VerifyUser = dtTM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");

            return true;
        }

        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _leMateriel_EditValueChanged(object val, string text)
        {
            dtTM.Rows[0]["MaterielID"] = val;
        }


        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.No == XtraMessageBox.Show("确认审核本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;

            dtTM.Rows[0]["IsVerify"] = 3;
            dtTM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtTM.Rows[0]["VerifyDate"] = DateTime.Today;

            
            if (Save())
            {
                BasicClass.GetDataSet.ExecSql(bllTM, "VerifyInDepot", new object[] { _MainID, true, _TableTypeID }); ;
                _isVerify = true;
                _barUnVerify.Enabled = true;
                _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = amountList1.IsCanEdit = false;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = amountList1.IsShowPopupMenu = false;
                if (_isVerify && (bs.Position == dtMain.Rows.Count - 1))
                    _brAddNew.Enabled = true;
            }
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtTM = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];

                if (DialogResult.No == XtraMessageBox.Show("是否确认弃审本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    return;

                dtTM.Rows[0]["IsVerify"] = 0;
                dtTM.Rows[0]["VerifyMan"] = 0;
                dtTM.Rows[0]["VerifyDate"] = DateTime.Today;
                if (Save())
                {
                    ShowView(bs.Position);
                }
         
        }

        private void _brDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, _TableTypeID });
                BasicClass.GetDataSet.ExecSql(bllTM, "Delete", new object[] { _MainID });
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
        //打印标签
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintLab(true);
        }

        private void _leMateriel_EditValueChanged_1(object val, string text)
        {
            dtTM.Rows[0]["MaterielID"] = _materielID = Convert.ToInt32(val);
        }

        private void amountList1_EditValueChanged(object val, string text)
        {

        }
        //设计标签

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintLab(false);
        }
        private void PrintLab(bool IsPrint)
        {
            DataTable dtAI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetList", new object[] { "(MainID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
            DataTable dtPrint = new DataTable();
            dtPrint.TableName = "Labs";
            DataTable dtSize = BasicClass.BaseTableClass.dtSize;
            DataTable dtColor = BasicClass.BaseTableClass.dtColor;
            dtPrint.Columns.Add("款号", typeof(string));
            dtPrint.Columns.Add("款号ID", typeof(int));
            dtPrint.Columns.Add("商标", typeof(string));
            dtPrint.Columns.Add("商标ID", typeof(int));
            dtPrint.Columns.Add("颜色", typeof(string));
            dtPrint.Columns.Add("颜色ID", typeof(int));
            dtPrint.Columns.Add("尺码", typeof(string));
            dtPrint.Columns.Add("尺码ID", typeof(int));
            dtPrint.Columns.Add("条码", typeof(string));
            dtPrint.Columns.Add("MListID", typeof(string));
            DataRow dr = dtPrint.NewRow();
            dr[0] = _leMateriel.valStr;
            dr[1] = Convert.ToInt32(_leMateriel.editVal);
            dr[2] = _leBrand.valStr;
            dr[3] = Convert.ToInt32(_leBrand.editVal);
            for (int i = 0; i < dtAI.Rows.Count; i++)
            {
                dr[4] = dtColor.Select("(ID=" + dtAI.Rows[i]["ColorID"] + ")")[0]["Name"];
                dr[5] = dtAI.Rows[i]["ColorID"];
                dr[6] = dtSize.Select("(ID=" + dtAI.Rows[i]["SizeID"] + ")")[0]["Name"];
                dr[7] = dtAI.Rows[i]["SizeID"];
                dr[8] = _leMateriel.valStr.PadLeft(6, '0') + _leBrand.editVal.ToString().PadLeft(2, '0') + dr[5].ToString().PadLeft(3, '0') + dr[7].ToString().PadLeft(2, '0');
                dr[9] = dtAI.Rows[i]["MListID"].ToString().PadLeft(8, '0');
                //for(int z=0;z<Convert.ToInt32(dtAI.Rows[i]["Amount"]);z++)
                //{
                    dtPrint.Rows.Add(dr.ItemArray);
                //}
            }
            BaseForm.PrintClass.PrintInLabes(dtPrint,IsPrint);
        }
        //竖式物料
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Tag.ToString()=="New")
            {
                BasicClass.cResult r = new BasicClass.cResult();
                    r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                    Form fr = new Hownet.BaseForm.frDeparment(r, -1);
                    fr.ShowDialog();
            }
        }
        void r_TextChanged(string s)
        {
        
            if (int.Parse(s) > -1)
            {
                dtDep = BasicClass.GetDataSet.GetBySql("Select ID,Name From Deparment Where TypeID=42");
                lookUpEdit1.Properties.DataSource = dtDep;
            }

        }
    }
}