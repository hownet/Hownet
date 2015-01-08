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

namespace Hownet.WMS
{
    public partial class frTaskBOM : DevExpress.XtraEditors.XtraForm
    {
     
       public frTaskBOM()
        {

            InitializeComponent();
        }
           
        DataTable dtMain = new DataTable();
        public frTaskBOM(DataTable dt)
            : this()
        {
            dtMain = dt;
        }
        BindingSource bs = new BindingSource();
        private string bllPP = "Hownet.BLL.ProductionPlan";
        private string bllTM = "Hownet.BLL.ProductTaskMain";
        private string bllAR = "Hownet.BLL.AllRemark";
        private string bllLR = "Hownet.BLL.AllLineRemark";
        //Hownet.BLL.Materiel bllMat = new Hownet.BLL.Materiel();
        //Hownet.BLL.Company bllCom = new Hownet.BLL.Company();
        //Hownet.BLL.WorkTicket bllWT = new Hownet.BLL.WorkTicket();

        //Hownet.BLL.ProductWorkingMain bllPWM = new Hownet.BLL.ProductWorkingMain();
        //Hownet.BLL.ProductWorkingInfo bllPWI = new Hownet.BLL.ProductWorkingInfo();
        bool _isBom = false;
        bool _isVerify = false;
        bool _isShow = true;
       public int _MainID = 0;
        int _TableTypeID = (int)BasicClass.Enums.TableType.ProductionPlan;
        int _bomID = 0;
        int _numType = 0;
        int _materielID, _brandID;
        string per = string.Empty;
        string fileName = string.Empty;
        DataTable dtTM = new DataTable();
        DataTable dtDemand = new DataTable();
        DataTable dtOTSet = new DataTable();
        DataTable dtAR = new DataTable();
        int _LR = 0;
        DataTable dtLR = new DataTable();
      // DataTable dtSTT = new DataTable();
        public frTaskBOM(int ID)
            : this()
        {
            _MainID = ID;
        }
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            try
            {
               // _numType = BasicClass.BasicFile.liST[0].NumType;
                ShowData();
                if (_MainID == 0 && dtMain.Rows.Count == 0)
                {
                    InData();
                }
                else if (dtMain.Rows.Count == 0)
                {
                    dtMain.Columns.Add("ID", typeof(int));
                    dtMain.Rows.Add(_MainID);
                    bs.DataSource = dtMain;
                }
                else
                {
                    bs.DataSource = dtMain;
                }
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                if (dtMain.Rows.Count > 0)
                    bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
                //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
                //{
                //    _barVerify.Enabled = _barUnVerify.Enabled = _brAddNew.Enabled = _brDel.Enabled = _brSave.Enabled = false;
                //}
                per = BasicClass.BasicFile.GetPermissions(this.Text);
                if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                    _brAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                    _brSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                    _brDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                {
                    _barUnVerify.Visibility = _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barSubItem2.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _leMateriel.FormName = (int)BasicClass.Enums.TableType.Product;
            _leAssociatedMatID.FormName = (int)BasicClass.Enums.TableType.Product;
            _leBrand.FormName = (int)BasicClass.Enums.TableType.Brand;
            _trUsing.ColumnEdit = BaseForm.RepositoryItem._reUse;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coMeasureID.ColumnEdit =_trTaskMeasureID.ColumnEdit= BaseForm.RepositoryItem._reMeasure;
            _deDepID.ColumnEdit = BaseForm.RepositoryItem._reDepartmentType;
            _lePackID.FormName = (int)BasicClass.Enums.TableType.PackingMethod;
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
           for(int i=_LR;i<8;i++)
           {
               gridView2.Columns[i + 4].Visible = false;
           }
           _numType = BasicClass.BasicFile.liST[0].NumType;
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            if (dtMain.Rows.Count == 0)
                dtMain = BasicClass.GetDataSet.GetDS(bllPP, "GetIDList", null).Tables[0];
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
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtTM = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];

            }
            else
            {
                _MainID = 0;
                dtTM = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtTM.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["SalesOrderInfoID"] = dr["MaterielID"] = dr["BrandID"] = dr["PWorkingID"] =dr["AssociatedMatID"]= dr["BomID"] = dr["DeparmentID"] = dr["UpData"] = dr["VerifyMan"] = dr["PackingMethodID"] = dr["ParentID"] =dr["TypeID"]= _MainID = _bomID = 0;
                dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                dr["LastDate"] = DateTime.Today.AddDays(15);
                dr["FilMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["IsBom"] = dr["IsTicket"] = false;
                dr["TicketDate"] = dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = dr["BedNO"] = dr["SewingRemark"] =dr["AssociatedID"]= "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllPP, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime().Date, _numType });
                dtTM.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            _isShow = true;
            _leCompany.editVal = dtTM.Rows[0]["CompanyID"];
            _leMateriel.editVal = _materielID = int.Parse(dtTM.Rows[0]["MaterielID"].ToString());
            _leBrand.editVal = dtTM.Rows[0]["BrandID"];
            if (Convert.ToDateTime(dtTM.Rows[0]["LastDate"]) > Convert.ToDateTime("2000-1-1"))
                _ldLastDate.val = dtTM.Rows[0]["LastDate"];
            else
                _ldLastDate.val = null;
            _lePackID.editVal = dtTM.Rows[0]["PackingMethodID"];
            _meSewRemark.EditValue = dtTM.Rows[0]["SewingRemark"].ToString();
            amountList1.Open(_MainID, _TableTypeID, true, (int)BasicClass.Enums.AmountType.原始数量);
            _isBom = (bool)(dtTM.Rows[0]["IsBom"]);
            _isVerify = (int.Parse(dtTM.Rows[0]["IsVerify"].ToString()) > 2);
            _ldLastDate.t = _isVerify;
            _barUnVerify.Enabled = _leMateriel.IsNotCanEdit = _leCompany.IsNotCanEdit = _leBrand.IsNotCanEdit = _isVerify;
            _brSave.Enabled = amountList1.IsShowPopupMenu = amountList1.IsCanEdit = _barVerify.Enabled = !_isVerify;
            _brDel.Enabled = amountList1.IsShowPopupMenu = !_isVerify;
            _leAssociatedMatID.editVal = Convert.ToInt32(dtTM.Rows[0]["AssociatedMatID"]);
            textEdit1.EditValue = dtTM.Rows[0]["AssociatedID"].ToString();
            // _brSave.Enabled =!_isBom;
            _brAddNew.Enabled = (_MainID > 0);
            _bomID = int.Parse(dtTM.Rows[0]["BomID"].ToString());
            dtDemand = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielDemand", "GetList", new object[] { "(ProduceTaskID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
            _gcDemand.DataSource = dtDemand;
            //  ShowBom();
            _leBom.editVal = int.Parse(dtTM.Rows[0]["BomID"].ToString());
            //dtSTT = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.OrderTask", "ShowSizeInfo", new object[] { _MainID, true, _TableTypeID,_materielID }).Tables[0];
            //_gvSizeTable.Columns.Clear();
            //_gcSizeTable.DataSource = dtSTT;
            //_gvSizeTable.Columns[0].ColumnEdit = BaseForm.RepositoryItem._reSizePart;
            radioGroup1.Properties.Items.Clear();
            _isShow = false;
            userNum1.IsCanEdit = (_MainID == 0);
            userNum1.ClearData();
            userNum1.NumStr = "SCJH";
            userNum1.Num = Convert.ToInt32(dtTM.Rows[0]["Num"]);
            userNum1.NumDate = Convert.ToDateTime(dtTM.Rows[0]["DateTime"]);
            if (_MainID > 0)
                userNum1.LastEdit = dtTM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
            if (_isVerify)
                userNum1.VerifyUser = dtTM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");
            xtraTabControl1.SelectedTabPage = _xtpNeed;
            xtraTabControl1.SelectedTabPage = _xtraSize;
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
            xtraTabControl1.SelectedTabPage = _xtpBOM;
            simpleButton1.Visible = (int.Parse(dtTM.Rows[0]["IsVerify"].ToString()) == 3);
            barSubItem2.Enabled = (Convert.ToInt32(dtTM.Rows[0]["IsVerify"]) > 2 && Convert.ToInt32(dtTM.Rows[0]["IsVerify"]) < 9);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            {
                barSubItem2.Enabled = false;
            }
            ucSizeList1.Open(Convert.ToInt32(dtTM.Rows[0]["MaterielID"]), _MainID, !_isVerify);
            simpleButton2.Enabled = !_isVerify;
            _leBom.IsNotCanEdit = _isVerify;
            ShowGDI();
            DataTable dtTask = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ParentID=" + _MainID + ")" }).Tables[0];
            ucSizeList1.IsCanEdit = ucSizeList1.IsCanEditCS = ucSizeList1.IsShowPopupMenu = dtTask.Rows.Count == 0;
            if (_MainID == 0)
                ucSizeList1.IsShowPopupMenu = false;
            if (Convert.ToInt32(dtTM.Rows[0]["TypeID"]) == -1)
            {
                simpleButton1.Visible = false;
                simpleButton3.Visible = true;
                DataTable dtTemPP = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(DeparmentID=" + _MainID + ")" }).Tables[0];
                if (dtTemPP.Rows.Count > 0)
                {
                    _meSewRemark.EditValue += "\r\n";
                    _meSewRemark.EditValue += "参与合并的生产计划单：";
                    _meSewRemark.EditValue += "\r\n";
                    for (int i = 0; i < dtTemPP.Rows.Count; i++)
                    {
                        _meSewRemark.EditValue += Convert.ToDateTime(dtTemPP.Rows[i]["DateTime"]).ToString("yyyyMMdd") + "-" + dtTemPP.Rows[i]["Num"].ToString() + "；";
                    }
                }
                _leMateriel.IsNotCanEdit = _leBrand.IsNotCanEdit = true;
                amountList1.IsShowPopupMenu = amountList1.IsCanEdit = false;
            }
            ucSizeBow1.Open(_MainID, _TableTypeID);
            dtAR = BasicClass.GetDataSet.GetDS(bllAR, "GetList", new object[] { "(MainID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
            if (dtAR.Rows.Count == 0)
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
            gridControl1.DataSource = dtAR;
            dtLR = BasicClass.GetDataSet.GetDS(bllLR, "GetList", new object[] { "(MainID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
            if(!_isVerify)
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
           // ucSizeBow1.
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
            try
            {
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
                simpleButton3.Visible = false;
                if (Convert.ToInt32(dtTM.Rows[0]["TypeID"]) == -1)
                {
                    broculosDrawing1.StrText = "合并的计划";

                }
                else
                {
                    broculosDrawing1.StrText = Enum.GetName(typeof(BasicClass.Enums.IsVerify), vvv);
                }
            }
            catch
            {
            }
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
            if (Save())
            {
                ShowView(bs.Position);
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
            dtTM.Rows[0]["CompanyID"] = _leCompany.editVal;
            dtTM.Rows[0]["MaterielID"] = _leMateriel.editVal;
            dtTM.Rows[0]["BrandID"] = _leBrand.editVal;
            dtTM.Rows[0]["DateTime"] = userNum1.NumDate;
            object o = _ldLastDate.val;
            if (o != null)
                dtTM.Rows[0]["LastDate"] = _ldLastDate.val;
            else
                dtTM.Rows[0]["LastDate"] = Convert.ToDateTime("1900-1-1");
            dtTM.Rows[0]["PWorkingID"] = 0;
            dtTM.Rows[0]["BedNO"] = string.Empty;
            dtTM.Rows[0]["PackingMethodID"] = _lePackID.editVal;
            dtTM.Rows[0]["SewingRemark"] = _meSewRemark.EditValue;
            dtTM.Rows[0]["BomID"] = _bomID;
            dtTM.Rows[0]["AssociatedMatID"] = _leAssociatedMatID.editVal;
            dtTM.Rows[0]["AssociatedID"] = textEdit1.Text.Trim();
            dtTM.Rows[0]["A"] = 1;
            if (_MainID == 0)
            {
                if (userNum1.IsEdit)
                {
                    if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllPP, "CheckNum", new object[] { userNum1.NumDate, userNum1.Num })))
                    {
                        XtraMessageBox.Show("生产编号重复！");
                        return false;
                    }
                    dtTM.Rows[0]["Num"] = userNum1.Num;
                }
                else
                {
                    dtTM.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(bllPP, "NewNum", new object[] { userNum1.NumDate, _numType });
                }
                dtMain.Rows[bs.Position]["ID"] = dtTM.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllPP, dtTM);
                _brAddNew.Enabled = true;
            }
            else
            {
                BasicClass.GetDataSet.UpData(bllPP, dtTM);
            }
            amountList1.Save(_MainID, int.Parse(dtTM.Rows[0]["MaterielID"].ToString()), int.Parse(dtTM.Rows[0]["BrandID"].ToString()));
            ucSizeBow1.TaskID = _MainID;
            ucSizeBow1.Save();
            BasicClass.GetDataSet.UpData(bllPP, dtTM);
            ucSizeList1.Save(_materielID, _MainID);
            DataTable dtTem = dtAR.Clone();
            int a = 0;
            for (int i = 0; i < dtAR.Rows.Count;i++ )
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
            for (int i = 0; i < dtLR.Rows.Count-1;i++)
            {
                a = Convert.ToInt32(dtLR.Rows[i]["A"]);
                if(a>1)
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
                //_gvSizeTable.CloseEditor();
                //_gvSizeTable.UpdateCurrentRow();
                //dtSTT.AcceptChanges();
                //SaveSizePart(dtSTT);
                //dtTM = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                //userNum1.ClearData();
                //userNum1.NumStr = "SCJH";
                //userNum1.Num = Convert.ToInt32(dtTM.Rows[0]["Num"]);
                //userNum1.NumDate = Convert.ToDateTime(dtTM.Rows[0]["DateTime"]);
                //if (_MainID > 0)
                //    userNum1.LastEdit = dtTM.Rows[0]["Fill"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["DateTime"]).ToString("yyyy年MM月dd日");
                //if (_isVerify)
                //    userNum1.VerifyUser = dtTM.Rows[0]["Verify"].ToString() + "\r\n" + Convert.ToDateTime(dtTM.Rows[0]["VerifyDate"]).ToString("yyyy年MM月dd日");
                return true;
        }

        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _leMateriel_EditValueChanged(object val, string text)
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
            if (_MainID == 0)
            {
                GetAR();
                amountList1.Open(true, BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderMain, "GetCS2RepByMatID", new object[] { _materielID }).Tables[0]);
            }
            //if (dtSTT.Rows.Count == 2)
            //{

            //}
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtDemand.Rows.Count == 0)
            {
                if (DialogResult.No == XtraMessageBox.Show("没有计算所需物料，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
            }
            //if (_gvSizeTable.RowCount < 4)
            //{
            //    if (DialogResult.No == XtraMessageBox.Show("没有尺寸记录，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
            //    {
            //        return;
            //    }
            //}
            if (DialogResult.No == XtraMessageBox.Show("审核后将不能修改，请再次确认！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                return;
            }
          //  dtTM.Rows[0]["IsBom"] = true;
            dtTM.Rows[0]["BomID"] = _bomID;
            dtTM.Rows[0]["IsVerify"] = 3;
            dtTM.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtTM.Rows[0]["VerifyDate"] = DateTime.Today;
            Save();
            if (dtDemand.Rows.Count > 0)
            {
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMaterielDemand, "DelNeed", new object[] { _MainID, _TableTypeID });
                DataTable dtt = dtDemand.Clone();
                int a = 0;
                for (int i = 0; i < dtDemand.Rows.Count; i++)
                {
                    a = int.Parse(dtDemand.Rows[i]["A"].ToString());
                    if (a > 1)
                    {
                        dtt.Rows.Clear();
                        dtDemand.Rows[i]["ProduceTaskID"] = _MainID;
                        dtt.Rows.Add(dtDemand.Rows[i].ItemArray);
                        if (a == 3)
                            dtDemand.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllMaterielDemand, dtt);
                        else if (a == 2)
                            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllMaterielDemand, dtt);
                        dtDemand.Rows[i]["A"] = 1;
                    }
                }
            }
            ShowView(bs.Position);
            //_isVerify = _barUnVerify.Enabled = _leMateriel.t = _leCompany.t = _leBrand.t = true;
            //amountList1.IsCanEdit = false;
            //_brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = amountList1.IsShowPopupMenu = false;
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtTM = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bllPP, "CheckCanDelete", new object[] { _MainID })))
            {
                XtraMessageBox.Show("本单已有产生裁剪单，不能弃审！");
                return;
            }
            if (Convert.ToInt32(dtTM.Rows[0]["PWorkingID"]) == 10)
            {
                XtraMessageBox.Show("本单已开始物料采购，不能弃审！");
                return;
            }
            dtTM.Rows[0]["IsVerify"] = 1;
            dtTM.Rows[0]["VerifyMan"] = 0;
            dtTM.Rows[0]["VerifyDate"] = DateTime.Today;
            dtTM.Rows[0]["PWorkingID"] = 0;
            BasicClass.GetDataSet.UpData(bllPP, dtTM);
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMaterielDemand, "DelNeed", new object[] { _MainID, _TableTypeID });
            BasicClass.GetDataSet.ExecSql(bllPP, "UpPlanMD", new object[] { _MainID });
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllRepertory, "DelByPlanID", new object[] { _MainID });
            ShowView(bs.Position);
        }

        private void _brDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (Convert.ToInt32(dtTM.Rows[0]["SalesOrderInfoID"]) > 0)
                {
                    DataTable dtSOI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetList", new object[] { "(ID=" + Convert.ToInt32(dtTM.Rows[0]["SalesOrderInfoID"]) + ")" }).Tables[0];
                    if (dtSOI.Rows.Count > 0)
                    {
                        dtSOI.Rows[0]["IsToPlan"] = false;
                        dtSOI.Rows[0]["IsVerify"] = 3;
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllSalesOrderInfoList, dtSOI);
                    }
                }
                BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAmountInfo, "DelInfo", new object[] { _MainID, _TableTypeID });
                BasicClass.GetDataSet.ExecSql(bllPP, "Delete", new object[] { _MainID });

                if (dtMain.Rows.Count > 1)
                {
                    dtMain.Rows.RemoveAt(bs.Position);
                    _brAddNew.Enabled = (_isVerify && bs.Position == dtMain.Rows.Count - 1);
                }
                else
                {
                    InData();
                    ShowView(0);
                }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == _xtpSizeBow)
            {
                if (ucSizeBow1.TaskID != _MainID)
                {
                    ucSizeBow1.Open(_MainID,_TableTypeID);
                }
            }
            else if (e.Page == _xtpNeed)
            {
                if (_gvDemand.RowCount == 0)
                {
                    dtDemand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielDemand, "GetTask", new object[] { _MainID, _TableTypeID }).Tables[0];
                    if (dtDemand.Rows.Count == 0 && _bomID > 0)
                        GetDemand();
                    else
                        _gcDemand.DataSource = dtDemand;
                }
            }
            else if (e.Page == _xtraTask)
            {
                if (radioGroup1.Properties.Items.Count < 3)//选择项少于3个，进行查询
                {
                    AddRadioGroup();
                }
            }
            else if (e.Page == xtraTabPage2)
            {
                _lePackID.editVal = dtTM.Rows[0]["PackingMethodID"];
            }
        }
        private void GetDemand()
        {
            if (_isVerify||_isShow)
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
            DataSet ssss = BasicClass.GetDataSet.GetDS("Hownet.BLL.BaseFile.MaterielDemandClass", "accountDemand", new object[] { _MainID, _bomID, _TableTypeID, bb, bbai, Convert.ToInt32(_leCompany.editVal),Convert.ToInt32(_leBrand.editVal) });
            if (ssss.Tables.Count > 0)
                dtDemand = ssss.Tables[0];
            xtraTabControl1.SelectedTabPage = _xtpNeed;
            _gcDemand.DataSource = dtDemand;
        }
        private void AddRadioGroup()
        {
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "已裁数量"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "未裁数量"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "已入库数量")});
            DataTable dtTask = BasicClass.GetDataSet.GetDS(bllTM, "GetList", new object[] { "(ParentID=" + _MainID + ")" }).Tables[0];
            if (dtTask.Rows.Count > 0)
            {
                string _DeparmentName = string.Empty;
                string _Num = string.Empty;
                string _BedNo = string.Empty;
                for (int i = 0; i < dtTask.Rows.Count; i++)
                {
                    _DeparmentName = dtTask.Rows[i]["DeparmentName"].ToString();
                    _Num = Convert.ToDateTime(dtTask.Rows[i]["DateTime"]).ToString("yyyyMMdd") + "-" + dtTask.Rows[i]["Num"].ToString();
                    _BedNo = dtTask.Rows[i]["BedNO"].ToString();
                    this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                    new DevExpress.XtraEditors.Controls.RadioGroupItem(Convert.ToInt32(dtTask.Rows[i]["ID"]), _Num+"-"+_BedNo+"/"+_DeparmentName)});
                }
            }
        }
        /// <summary>
        /// 根据物料结构主表ID显示单件用量树形
        /// </summary>
        /// <param name="ID"></param>
        void ShowBomRoot(int BomID)
        {
            _trBom.Nodes.Clear();
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
            dtStruct.Columns.Add("TMAmount", typeof(decimal));
            dtStruct.Columns.Add("TaskMeasureID", typeof(int));

            _trBom.DataSource = null;
            _trBom.DataSource = dtStruct;
            _trBom.AppendNode(new object[] { _materielID,_leMateriel.valStr }, null);
            DataTable dtBom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructMain, "GetBomListByMainID", new object[] { BomID }).Tables[0];
            for (int i = 0; i < dtBom.Rows.Count; i++)
            {
                _trBom.AppendNode(new object[] {int.Parse( dtBom.Rows[i]["MaterielID"].ToString()),dtBom.Rows[i]["MaterielName"].ToString(),
                    dtBom.Rows[i]["MeasureName"].ToString(),decimal.Parse( dtBom.Rows[i]["Amount"].ToString()),
                    decimal.Parse( dtBom.Rows[i]["Wastage"].ToString()),dtBom.Rows[i]["DepartmentName"].ToString(),
                    dtBom.Rows[i]["PartName"].ToString(),dtBom.Rows[i]["UsingTypeID"],dtBom.Rows[i]["IsTogethers"].ToString(),Convert.ToDecimal( dtBom.Rows[i]["TMAmount"]),Convert.ToInt32(dtBom.Rows[i]["TaskMeasureID"])}, _trBom.Nodes[0]);
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

        private void ShowBom()
        {
            _leBom.Par = new object[] { "(MaterielID=" + _materielID * -1 + ") And (IsVerify=3)" };
            _leBom.FormName = (int)BasicClass.Enums.TableType.BOM;
        }

        private void _leBom_EditValueChanged(object val, string text)
        {
            _bomID = int.Parse(val.ToString());
            ShowBomRoot(_bomID);
            GetDemand();
        }

        private void _gvDemand_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column != _coA && int.Parse(_gvDemand.GetFocusedRowCellValue(_coA).ToString()) == 1)
                _gvDemand.SetFocusedRowCellValue(_coA, 2);
            if (e.Column == _costockAmount)
                _gvDemand.SetFocusedRowCellValue(_costockNotAmount, e.Value);
            if (e.Column == _coAmount)
                _gvDemand.SetFocusedRowCellValue(_coNotAllotAmount, e.Value);
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            string DD = BasicClass.GetDataSet.GetOne(bllPP, "GetDesigners", new object[] { Convert.ToInt32(_leMateriel.editVal) }).ToString();
            string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            byte[] bbb =new byte[0];
            if (picName.Trim() != string.Empty)
                bbb = BasicClass.FileUpDown.getServerPic(picName);
            dtBomMain.Rows.Add(userNum1.NumStr, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, "", _ldLastDate.strLab,DD);
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
                    || (_colorID == 0 && _colorOneID == 0 && _colorTwoID > 0 && _sizeID == 0) )
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
            //dtSizePart.Columns.Add("Tolerance", typeof(string));
            
            //for (int i = 0; i < _gvSizeTable.RowCount; i++)
            //{
            //    DataRow dr = dtSizePart.NewRow();
            //    for (int j = 0; j < _gvSizeTable.VisibleColumns.Count; j++)
            //    {
            //        dr[j] = _gvSizeTable.GetRowCellDisplayText(i, _gvSizeTable.VisibleColumns[j]);
            //    }
            //    if (_gvSizeTable.GetRowCellDisplayText(i, _gvSizeTable.VisibleColumns[0]).Trim() != string.Empty)
            //    {
            //        try
            //        {
            //            dr["Tolerance"] = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSizePart, "GetList", new object[] { "(Name='" + _gvSizeTable.GetRowCellDisplayText(i, _gvSizeTable.VisibleColumns[0]) + "')" }).Tables[0].Rows[0]["Tolerance"];
            //        }
            //        catch
            //        {
            //            dr["Tolerance"] = string.Empty;
            //        }
            //    }
            //    dtSizePart.Rows.Add(dr);
            //}
            //if (dtSizePart.Rows.Count > 0)
            //{
            //    dtSizePart.Rows[0]["Tolerance"] = "允许误差";
            //}
            ds.Tables.Add(ucSizeList1.dtPrint);
            ds.Tables.Add(dtNoCS);
            BaseForm.PrintClass.TaskBom(ds);
        }

        private void _gvSizeTable_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (!_isVerify)
            //{
            //    _gvSizeTable.OptionsBehavior.Editable = (e.FocusedRowHandle > 0 && e.FocusedRowHandle < _gvSizeTable.RowCount - 1);
            //}
        }

        private void _gvSizeTable_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //decimal len = 0;
            //try
            //{
            //    len = decimal.Parse(e.Value.ToString());
            //}
            //catch
            //{
            //    XtraMessageBox.Show("请输入数字!");
            //    _gvSizeTable.SetFocusedValue(0);
            //}
            //if (e.RowHandle == _gvSizeTable.RowCount - 2)
            //{
            //    dtSTT.Rows.Add(dtSTT.NewRow());
            //}
        }

        private void _gvSizeTable_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.ColumnHandle == 0)
            //{
            //    if (e.RowHandle > 0)
            //    {
            //        for (int i = 1; i < _gvSizeTable.RowCount - 1; i++)
            //        {
            //            if (i != e.RowHandle && _gvSizeTable.GetRowCellValue(i, _gvSizeTable.Columns[0]).ToString() == e.Value.ToString())
            //            {
            //                XtraMessageBox.Show("尺寸部位重复！");
            //                SendKeys.Send("{Esc}");
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 1; i < _gvSizeTable.RowCount - 1; i++)
            //        {
            //            if (i != e.RowHandle && _gvSizeTable.GetRowCellValue(i, _gvSizeTable.Columns[0]).ToString() == e.Value.ToString())
            //            {
            //                XtraMessageBox.Show("尺寸部位重复！");
            //                SendKeys.Send("{Esc}");
            //                break;
            //            }
            //        }
            //    }
            //}
        }
        private void SaveSizePart(DataTable dt)
        {
            
            DataTable dtSP =BasicClass.GetDataSet.GetDS("Hownet.BLL.SizePart","GetAllList",null).Tables[0];
            DataTable dtS = BasicClass.GetDataSet.GetDS("Hownet.BLL.Size", "GetAllList", null).Tables[0];
            ArrayList PartList = new ArrayList();
            ArrayList SizeList = new ArrayList();
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.SizeTableTask", "DelInfo", new object[] { _MainID });
            DataTable ddStt = BasicClass.GetDataSet.GetDS("Hownet.BLL.SizeTableTask", "GetList", new object[] {"(ID=0)" }).Tables[0];
            PartList.Clear();
            SizeList.Clear();
            PartList.Add(0);
            SizeList.Add(0);
            for (int r = 1; r < dt.Rows.Count - 2; r++)
            {
                int partID = 0;
                if (Convert.ToInt32( dt.DefaultView[r]["SizePart"]) !=0)
                {
                    partID = Convert.ToInt32(dt.DefaultView[r]["SizePart"]);
                }
                PartList.Add(partID);
            }
            for (int c = 1; c < dt.Columns.Count; c++)
            {
                int sizeID = 0;
                if (dt.Rows[0][c].ToString() != string.Empty)
                {
                    DataRow[] drs = dtS.Select("(Name='" + dt.Rows[0][c].ToString() + "')");
                    if (drs.Length > 0)
                        sizeID = int.Parse(drs[0]["ID"].ToString());
                }
                SizeList.Add(sizeID);
            }
            for (int r = 0; r < PartList.Count; r++)
            {
                if (PartList[r].ToString() != "0")
                {
                    for (int c = 0; c < SizeList.Count; c++)
                    {
                        if (SizeList[c].ToString() != "0")
                        {
                            if (dt.DefaultView[r][c].ToString() != string.Empty && dt.DefaultView[r][c].ToString() != "0")
                            {
                                ddStt.Rows.Clear();
                                ddStt.Rows.Add(ddStt.NewRow());
                                ddStt.Rows[ddStt.Rows.Count - 1]["TaskID"] = _MainID;
                                ddStt.Rows[ddStt.Rows.Count - 1]["SizeID"] = int.Parse(SizeList[c].ToString());
                                ddStt.Rows[ddStt.Rows.Count - 1]["SizePartID"] = int.Parse(PartList[r].ToString());
                                ddStt.Rows[ddStt.Rows.Count - 1]["Length"] = decimal.Parse(dt.DefaultView[r][c].ToString());
                                ddStt.Rows[ddStt.Rows.Count - 1]["A"] = 1;
                                BasicClass.GetDataSet.Add("Hownet.BLL.SizeTableTask", ddStt);
                            }
                        }
                    }
                }
            }
        }

        private void _barAddCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_isVerify)
            {
                XtraMessageBox.Show("未审核，不能转入裁剪单！");
                return;
            }
            dtTM = BasicClass.GetDataSet.GetDS(bllPP, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            if (Convert.ToInt32(dtTM.Rows[0]["IsVerify"]) == (int)BasicClass.Enums.IsVerify.合并生产)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("本生产计划已被合并,是否打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    DataTable dt=new DataTable();
                    dt.Columns.Add("ID",typeof(int));
                    dt.Rows.Add(Convert.ToInt32(dtTM.Rows[0]["DeparmentID"]));
                    Form frr = new WMS.frTaskBOM(dt);
                    frr.ShowDialog();
                }
                return;
            }
            BasicClass.cResult r = new BasicClass.cResult();
            r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
            Form fr = new Hownet.Task.frTaskForm(r, _MainID);
            fr.ShowDialog();
        }

        void r_TextChanged(string s)
        {
            if (s.Length > 2)
            {
                string[] ss = s.Split(',');
                if (Convert.ToInt32(ss[0]) > 0)
                {
                    this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(Convert.ToInt32(ss[0]), ss[1])});
                }
                else if (Convert.ToInt32(ss[0]) < 0)
                {
                    DevExpress.XtraEditors.Controls.RadioGroupItem ri = radioGroup1.Properties.Items[radioGroup1.SelectedIndex];
                    this.radioGroup1.Properties.Items.Remove(ri);
                }
            }
            else
            {
                radioGroup1.Properties.Items.Clear();
                AddRadioGroup();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                amountList2.Open(_MainID, _TableTypeID, true, (int)BasicClass.Enums.AmountType.完成数量);
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                amountList2.Open(_MainID, _TableTypeID, true, (int)BasicClass.Enums.AmountType.未完成数量);
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                try
                {
                    amountList2.Open(true, BasicClass.GetDataSet.GetDS(bllPP, "Get2Depot", new object[] { _MainID, (int)BasicClass.Enums.TableType.Task }).Tables[0]);
                }
                catch
                {
                    amountList2.ClearData();
                }
            }
            else
            {
                amountList2.Open(Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value), (int)BasicClass.Enums.TableType.Task, true, (int)BasicClass.Enums.AmountType.原始数量);
            }
        }

        private void ucSizeBow1_EditValueChanged()
        {
            GetDemand();
        }

        private void frTaskBOM_SizeChanged(object sender, EventArgs e)
        {
            ShowGDI();
        }

        private void _barReCaicNeed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("将删除当前已计算出的所需物料！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllMaterielDemand, "DelNeed", new object[] { _MainID, _TableTypeID });
                    dtDemand.Rows.Clear();
                    GetDemand();
                }
            }

        }

        private void _gvDemand_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isVerify)
            {
                if (e.Button == MouseButtons.Right)
                    DoShowMenu(_gvDemand.CalcHitInfo(new Point(e.X, e.Y)));
            }
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }

        private void amountList1_EditValueChanged(object val, string text)
        {
            GetDemand();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroup1.SelectedIndex < 3)
                return;
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                int _TID = Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
                BasicClass.cResult r = new BasicClass.cResult();
                r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                Form fr = new Hownet.Task.frTaskForm(r, _TID,1);
                fr.ShowDialog();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroup1.SelectedIndex < 2)
                return;
            try
            {

                int _TID = Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (!BasicClass.BasicFile.IsHavePermissions((int)BasicClass.Enums.Operation.View, "出工票"))
                {
                    XtraMessageBox.Show("沒有权限！");
                    return;
                }

                Form fr = new Pay.frToTicket(_TID);
                fr.ShowDialog();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(bllPP, "GetTMCount", new object[] {_MainID })) > 0)
            {
                XtraMessageBox.Show("本生产计划已有生成裁剪单，不能合并！");
                return;
            }
            BasicClass.cResult r=new BasicClass.cResult();
            Form fr = new frMergePP(r, _materielID);
            fr.ShowDialog();
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue != null)
            {
                Form fr = new  BaseForm.ShowPic(fileName);
                fr.ShowDialog();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(9, "已完成");
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(21, "客户取消");
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMark(22, "公司取消");
        }
        /// <summary>
        /// 设置生产任务进度标记，9为已完成，21为 客户取消 ，22为公司取消
        /// </summary>
        /// <param name="TypeID"></param>
        private void SetMark(int TypeID,string strType)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的将本单标记为： "+strType+" ？\r\n本单所有在库物料，将转入空闲物料中！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否真的将本单标记为： " + strType + " ？\r\n不提供撤消操作！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    dtTM.Rows[0]["IsVerify"] = TypeID;
                    BasicClass.GetDataSet.UpData(bllPP, dtTM);
                    BasicClass.GetDataSet.ExecSql(bllPP, "UpPlanMD", new object[] { _MainID });
                    ShowGDI();
                    barSubItem2.Enabled = false;
                }
            }
        }

        private void _leBrand_EditValueChanged(object val, string text)
        {
            GetDemand();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form fr = new Hownet.Materiel.MaterielBom(_materielID);
            fr.ShowDialog();
            _leBom.editVal = 0;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroup1.SelectedIndex < 2)
                return;
            try
            {

                int _TID = Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                BasicClass.cResult re = new BasicClass.cResult();
                re.TextChanged += new BasicClass.TextChangedHandler(re_TextChanged);
                Form fr = new frExcDeparmetn(re, _TID);
                fr.ShowDialog();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        void re_TextChanged(string s)
        {
            radioGroup1.Properties.Items.Clear();
            AddRadioGroup();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (_MainID > 0)
            {
                int a = Convert.ToInt32(BasicClass.GetDataSet.GetOne(bllPP, "MDPP", new object[] { _MainID }));
                if (a == 1)
                {
                    dtDemand = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielDemand", "GetList", new object[] { "(ProduceTaskID=" + _MainID + ") And (TableTypeID=" + _TableTypeID + ")" }).Tables[0];
                    _gcDemand.DataSource = dtDemand;
                    XtraMessageBox.Show("合并物料成功");
                }
                else
                {
                    XtraMessageBox.Show("合并物料失败");
                }
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            dtBomMain.Columns.Add("CompanySN", typeof(string));
            string DD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=" + Convert.ToInt32(dtTM.Rows[0]["FilMan"]) + ")" }).Tables[0].Rows[0]["TrueName"].ToString();
            string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            byte[] bbb = new byte[0];
            if (picName.Trim() != string.Empty)
                bbb = BasicClass.FileUpDown.getServerPic(picName);
            string MR = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _materielID + ")" }).Tables[0].Rows[0]["Remark"].ToString();
            string csn = string.Empty;
            if(Convert.ToInt32( _leCompany.editVal)>0 )
            {
                try
                {
                    csn = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(ID=" + Convert.ToInt32(_leCompany.editVal) + ")" }).Tables[0].Rows[0]["SN"].ToString();
                }
                catch
                {

                }
            }
            dtBomMain.Rows.Add(userNum1.Num, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, "", _ldLastDate.strLab, DD, amountList1.SumAmount, MR,csn);
            DataTable dtAmount = amountList1.dtDataSource.Copy();
            dtAmount.TableName = "AmountInfo";
            if(dtAmount.Rows.Count<10)
            {
                for(int i=dtAmount.Rows.Count;i<11;i++)
                {
                    dtAmount.Rows.Add(dtAmount.NewRow());
                }
            }
            DataTable dtLRTem = dtLR.Copy();
            dtLRTem.TableName = "dtLR";
            if(dtLRTem.Rows.Count<8)
            {
                for(int i=dtLRTem.Rows.Count;i<8;i++)
                {
                    dtLRTem.Rows.Add(dtLRTem.NewRow());
                }
            }
            ds.Tables.Add(dtBomMain);
            ds.Tables.Add(dtAmount);
            ds.Tables.Add(dtLRTem);
            BaseForm.PrintClass.PrintTaskAmount(ds);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column!=_coA&&Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA))==1)
            {
                gridView1.SetFocusedRowCellValue(_coA, 2);
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            dtBomMain.Columns.Add("AssociatedMat");
            dtBomMain.Columns.Add("AssociatedID");
            string DD = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=" + Convert.ToInt32(dtTM.Rows[0]["FilMan"]) + ")" }).Tables[0].Rows[0]["TrueName"].ToString();
            string picName = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _leMateriel.editVal + ")" }).Tables[0].Rows[0]["Image"].ToString();
            byte[] bbb = new byte[0];
            if (picName.Trim() != string.Empty)
                bbb = BasicClass.FileUpDown.getServerPic(picName);
            string last = _ldLastDate.strLab;
            if (last == string.Empty)
                last = "    年   月   日";
            string MR = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(ID=" + _materielID + ")" }).Tables[0].Rows[0]["Remark"].ToString();
            dtBomMain.Rows.Add(userNum1.StrNum, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, "", last, DD, amountList1.SumAmount, MR, _leAssociatedMatID.valStr, textEdit1.Text.Trim());
            DataTable dtAmount = amountList1.dtDataSource.Copy();
            dtAmount.TableName = "AmountInfo";
            int rows = dtAmount.Rows.Count;
            if (rows < 10)
            {
                for (int i = dtAmount.Rows.Count; i < 11; i++)
                {
                    dtAmount.Rows.Add(dtAmount.NewRow());
                }
            }
            else if (rows < 18)
            {
                for (int i = dtAmount.Rows.Count; i < 19; i++)
                {
                    dtAmount.Rows.Add(dtAmount.NewRow());
                }
            }

            DataTable dtBOMTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructMain, "GetPrintList", new object[] { _bomID }).Tables[0].Copy();
            dtBOMTem.Columns.Add("使用方法", typeof(string));
            dtBOMTem.TableName = "BOM";
            DataTable dtARR = dtAR.Copy();
            dtARR.TableName = "dtAR";
            ds.Tables.Add(dtBomMain);
            ds.Tables.Add(dtAmount);
            ds.Tables.Add(dtARR);

            DataTable dtUT = new DataTable();
            dtUT.Columns.Add("ID", typeof(int));
            dtUT.Columns.Add("Name", typeof(string));
            dtUT.Rows.Add(1, "按主色");
            dtUT.Rows.Add(2, "按插色一");
            dtUT.Rows.Add(3, "按插色二");
            dtUT.Rows.Add(4, "按尺码");
            dtUT.Rows.Add(5, "按主色+尺码");
            dtUT.Rows.Add(6, "按插色一+尺码");
            dtUT.Rows.Add(7, "按插色二+尺码");
            dtUT.Rows.Add(8, "按主色+插色一");
            dtUT.Rows.Add(9, "按主色+插色二");
            dtUT.Rows.Add(10, "按插色一+插色二");
            dtUT.Rows.Add(11, "按主色+插色一+插色二");
            dtUT.Rows.Add(12, "都不用");
            dtUT.Rows.Add(13, "特殊配色");
            for (int i = 0; i < dtBOMTem.Rows.Count; i++)
            {
                if(dtBOMTem.Rows[i]["车间单位"]==null||dtBOMTem.Rows[i]["车间单位"].ToString()==string.Empty)
                {
                    dtBOMTem.Rows[i]["车间单位"] = dtBOMTem.Rows[i]["默认单位"];
                    dtBOMTem.Rows[i]["用量"] = dtBOMTem.Rows[i]["Amount"];
                }
                try
                {
                    dtBOMTem.Rows[i]["使用方法"] = dtUT.Select("(ID=" + dtBOMTem.Rows[i]["UsingTypeID"] + ")")[0]["Name"];
                }
                catch
                {

                }
            }

            ds.Tables.Add(dtBOMTem);

            BaseForm.PrintClass.PrintTaskt(ds, dtAmount.Rows.Count);
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coLA && Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coLA)) == 1)
                gridView2.SetFocusedRowCellValue(_coLA, 2);
            if(e.RowHandle==gridView2.RowCount-1&&gridView2.RowCount<7)
            {
                DataRow drLR = dtLR.NewRow();
                drLR["A"] = 3;
                drLR["ID"] = 0;
                drLR["MainID"] = _MainID;
                drLR["TableTypeID"] = _TableTypeID;
                drLR["Remark1"] = drLR["Remark2"] = drLR["Remark3"] = drLR["Remark4"] = drLR["Remark5"] = drLR["Remark6"] = drLR["Remark7"] = drLR["Remark8"] = string.Empty;
                dtLR.Rows.Add(drLR);
            }
        }
        private void GetAR()
        {
            dtAR = BasicClass.GetDataSet.GetDS(bllAR, "GetLastList", new object[] { Convert.ToInt32(_leCompany.editVal), _materielID, _TableTypeID }).Tables[0];
            if (dtAR.Rows.Count == 0)
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
            gridControl1.DataSource = dtAR;
        }

        private void _leCompany_EditValueChanged(object val, string text)
        {
            if (Convert.ToInt32(_leCompany.editVal)>0&& _MainID == 0)
                GetAR();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            dtBomMain.Rows.Add(userNum1.NumStr, _leMateriel.valStr, userNum1.NumDate, bbb, _lePackID.valStr, _meSewRemark.EditValue, _leBrand.valStr, _leCompany.valStr, string.Empty, 0, Convert.ToDateTime(_ldLastDate.val), Convert.ToDateTime(dtTM.Rows[0]["FillDate"]));
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