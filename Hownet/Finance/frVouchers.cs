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

namespace Hownet.Finance
{
    public partial class frVouchers : DevExpress.XtraEditors.XtraForm
    {
        public frVouchers()
        {
            InitializeComponent();
        }
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        public frVouchers(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        int _companyID = 0;
        int _depotID = 0;
        int _upData = 0;
        BindingSource bs = new BindingSource();
        private string bllVM = "Hownet.BLL.VouchersMain";
        private string bllVI = "Hownet.BLL.VouchersInfo";
        private string bllAc = "Hownet.BLL.Bas_KJKM";
        DataTable dtVM= new DataTable();
        DataTable dtVI = new DataTable();
        DataTable dtAc = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtMat = new DataTable();
        string backDate = string.Empty;
        bool _isVerivy = false;
        decimal price = 0;
        decimal money = 0;
        decimal amount = 0;
        decimal last = 0;
        decimal back = 0;
        object _oldMat = null;
        object _oldBrand = null;
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
    
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            dtMat=BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
            dtAc = BasicClass.GetDataSet.GetDS(bllAc, "GetAllList", null).Tables[0];
         }

        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllVM, "GetIDList",null).Tables[0];
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
                 dtVM = BasicClass.GetDataSet.GetDS(bllVM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
                 _teSumNum.EditValue = dtVM.Rows[0]["SumNum"];
                 _teNum.EditValue = dtVM.Rows[0]["Num"];
                 _teAN.EditValue = dtVM.Rows[0]["AttachmentSum"];
                 dtVI = BasicClass.GetDataSet.GetDS(bllVI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                 DataRow drr = dtVI.NewRow();
                 drr["A"] = 3;
                 drr["IsPosting"] = 0;
                 drr["ID"] = 0;
                 for (int i = dtVI.Rows.Count; i < 7; i++)
                 {
                     dtVI.Rows.Add(drr.ItemArray);
                 }
             }
             else
             {
                 _MainID = 0;
                 dtVM = BasicClass.GetDataSet.GetDS(bllVM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                 DataRow dr = dtVM.NewRow();
                 dr["CaiKuai"] = dr["JiZhang"] = dr["FuHe"] = dr["ZhiZheng"] = dr["SumNum"] = dr["Num"] = dr["AttachmentSum"] = dr["ID"] = dr["MainID"] = 0;
                 dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
                 dr["A"] = 3;
                 dr["CKDate"] = dr["JZDate"] = dr["FHDate"] = dr["ZZDate"] = Convert.ToDateTime("1900-1-1");
                 dr["FillMan"] = BasicClass.UserInfo.UserID;
                 dtVM.Rows.Add(dr);
                 _teAN.EditValue = _teNum.EditValue = _teSumNum.EditValue = string.Empty;
                 dtVI = BasicClass.GetDataSet.GetDS(bllVI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                 DataRow drr = dtVI.NewRow();
                 drr["A"] = 3;
                 drr["IsPosting"] = 0;
                 drr["ID"] = 0;
                 dtVI.Rows.Add(drr.ItemArray);
                 dtVI.Rows.Add(drr.ItemArray);
                 dtVI.Rows.Add(drr.ItemArray);
                 dtVI.Rows.Add(drr.ItemArray);
                 dtVI.Rows.Add(drr.ItemArray);
                 dtVI.Rows.Add(drr.ItemArray);
             }
             _upData = int.Parse(dtVM.Rows[0]["MainID"].ToString());
             gridControl1.DataSource = dtVI;
             dateEdit1.EditValue = dtVM.Rows[0]["DateTime"];

         _barDelTable.Enabled=  _brSave.Enabled = _barGZ.Enabled = _barVerify.Enabled = Convert.ToInt32(dtVM.Rows[0]["CaiKuai"].ToString()) == 0;
             _barUnVierfy.Enabled = !_barVerify.Enabled;
             _barGZ.Enabled = Convert.ToInt32(dtVM.Rows[0]["FuHe"]) == 0;
        //     _barTuiZhang.Enabled = !_barGZ.Enabled;

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
               if (f)
               {
                   int d = bs.Position;
                   InData();
                   bs.Position = d;
                   ShowView(d);
               }
           }
        }
        private bool Save()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtVI.AcceptChanges();
            if (DialogResult.No == XtraMessageBox.Show("没有金额产生的明细，将被删除，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                return false;
            }
            if (_teSumNum.EditValue.ToString().Trim() != string.Empty)
                dtVM.Rows[0]["SumNum"] = _teSumNum.EditValue;
            else
                dtVM.Rows[0]["SumNum"] = 0;

            if (_teNum.EditValue.ToString().Trim() != string.Empty)
                dtVM.Rows[0]["Num"] = _teNum.EditValue;
            else
                dtVM.Rows[0]["Num"] = 0;

            if (_teAN.EditValue.ToString().Trim() != string.Empty)
                dtVM.Rows[0]["AttachmentSum"] = _teAN.EditValue;
            else
                dtVM.Rows[0]["AttachmentSum"] = 0;
            dtVM.Rows[0]["DateTime"] = dateEdit1.EditValue;
            if (_MainID == 0)
            {
                dtMain.Rows[bs.Position]["ID"] = dtVM.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllVM, dtVM);
            }
            else
            {
                if (int.Parse(BasicClass.GetDataSet.GetDS(bllVM, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0].Rows[0]["MainID"].ToString()) != _upData)
                {
                    XtraMessageBox.Show("本单已被其他用户修改！");
                    return false;
                }
                else
                {
                    dtVM.Rows[0]["MainID"] = _upData = _upData + 1;
                    BasicClass.GetDataSet.UpData(bllVM, dtVM);
                }
            }
            DataTable dtTem = dtVI.Clone();
            int a = 0;
            decimal debit=0;
            decimal credit=0;
            for (int i = 0; i < dtVI.Rows.Count; i++)
            {
                a=Convert.ToInt32(dtVI.Rows[i]["A"]);
                if (a > 1)
                {
                    debit = credit = 0;
                    if (dtVI.Rows[i]["DebitMoney"].ToString().Trim() != string.Empty)
                    {
                        debit = Convert.ToDecimal(dtVI.Rows[i]["DebitMoney"]);
                    }
                    if (dtVI.Rows[i]["CreditMoney"].ToString().Trim() != string.Empty)
                    {
                        credit = Convert.ToDecimal(dtVI.Rows[i]["CreditMoney"]);
                    }
                    if (debit == 0 && credit == 0)
                    {
                        if (Convert.ToInt32(dtVI.Rows[i]["ID"]) > 0)
                        {
                            BasicClass.GetDataSet.ExecSql(bllVI, "Delete", new object[] { Convert.ToInt32(dtVI.Rows[i]["ID"]) });
                        }
                    }
                    else
                    {
                        dtTem.Rows.Clear();
                        dtVI.Rows[i]["MainID"] = _MainID;
                        dtTem.Rows.Add(dtVI.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtVI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllVI, dtTem);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(bllVI, dtTem);
                        }
                        dtVI.Rows[i]["A"] = 1;
                    }
                }
            }
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

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("是否确认删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
            {
                if (DialogResult.Yes == MessageBox.Show("请再次确认是否删除整张单据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1))
                {
                    if (_MainID > 0)
                    {
                        object[] o = new object[] {_MainID };
                        BasicClass.GetDataSet.ExecSql(bllVM, "Delete", o);
                    }
                    InData();
                    if (dtMain.Rows.Count > 0)
                        bs.Position = dtMain.Rows.Count - 1;
                    else
                        ShowView(0);
                }
            }
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Save())
            {
                if (DialogResult.No == XtraMessageBox.Show("是否确认审核？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
                dtVM.Rows[0]["CaiKuai"] = BasicClass.UserInfo.UserID;
                dtVM.Rows[0]["CKDate"] = BasicClass.GetDataSet.GetDateTime().Date;
                BasicClass.GetDataSet.UpData(bllVM, dtVM);
                ShowView(bs.Position);
            }
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_barGZ.Enabled)
            {
                if (DialogResult.No == XtraMessageBox.Show("是否确认弃审？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
                dtVM.Rows[0]["CaiKuai"] =0;
                dtVM.Rows[0]["CKDate"] = Convert .ToDateTime("1900-1-1");
                BasicClass.GetDataSet.UpData(bllVM, dtVM);
                ShowView(bs.Position);
            }
        }


        private void _barPrintTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintTable();
        }
        private void PrintTable()
        {
           

        }
        private void _barPrintInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _beAccountsOne_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
                BasicClass.cResult crKM = new BasicClass.cResult();
                crKM.RowChanged += new BasicClass.RowChangedHandler(crKM_RowChanged);
                Form fr = new frKJKM(crKM, -1);
                fr.ShowDialog();
        }
        void crKM_RowChanged(DataTable dtKM)
        {
            if (dtKM.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtKM.Rows[0][0]) > 0)
                {
                    gridView1.SetFocusedValue(dtKM.Rows[0][1]);
                    gridView1.SetFocusedRowCellValue(_coAccountsTwoOneID , dtKM.Rows[0][0]);
                    gridView1.SetFocusedRowCellValue(_coATwo, dtKM.Rows[1][1]);
                    gridView1.SetFocusedRowCellValue(_coAccountsTwoID, dtKM.Rows[1][0]);
                }
                else
                {
                    gridView1.SetFocusedValue(dtKM.Rows[1][1]);
                    gridView1.SetFocusedRowCellValue(_coAccountsTwoOneID, dtKM.Rows[1][0]);
                    gridView1.SetFocusedRowCellValue(_coATwo, string.Empty);
                    gridView1.SetFocusedRowCellValue(_coAccountsTwoID, 0);
                }
            }
        }
        private void _beAccountsTwo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
            {
                gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if (e.Column == _coAOne || e.Column == _coATwo)
            {
                if (e.Value.ToString() != string.Empty)
                {
                    DataRow[] drs = dtAc.Select("(Name='" + e.Value + "')");
                    if (drs.Length == 0)
                    {
                        drs = dtAc.Select("(Name='" + e.Value + "')");
                        if (drs.Length == 0)
                        {
                            XtraMessageBox.Show("请输入正确的会计科目。");
                            gridView1.SetFocusedValue(string.Empty);
                            gridView1.SetFocusedRowCellValue(e.Column.FieldName.Substring(0, e.Column.FieldName.Length - 4), 0);
                        }
                        else
                        {
                            gridView1.SetFocusedValue(drs[0]["Name"]);
                            gridView1.SetFocusedRowCellValue(e.Column.FieldName.Substring(0, e.Column.FieldName.Length - 4), drs[0]["ID"]);
                        }
                    }
                }
            }

        }

        private void repositoryItemTextEdit3_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle != 0)
            {
                if (gridView1.GetFocusedValue().ToString().Trim() == string.Empty)
                {
                    gridView1.SetFocusedValue(gridView1.GetRowCellValue(gridView1.FocusedRowHandle - 1, _coSummary));
                }
            }
        }

        private void _barGZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (dtVM.Rows[0]["CaiKuai"].ToString() == string.Empty)
                {
                    MessageBox.Show("请先审核~");
                    return;
                }
                gridView1.UpdateCurrentRow();
                gridView1.CloseEditor();
                dtVI.AcceptChanges();
                //借
                decimal _DM = 0;
                //贷
                decimal _CM = 0;
                int _MT = 0;
                DataTable dtKM = new DataTable();
                for (int i = 0; i < dtVI.Rows.Count; i++)
                {
                    if (dtVI.Rows[i]["CreditMoney"].ToString() != string.Empty)
                        _CM += Convert.ToDecimal(dtVI.Rows[i]["CreditMoney"]);
                    if (dtVI.Rows[i]["DebitMoney"].ToString() != string.Empty)
                        _DM += Convert.ToDecimal(dtVI.Rows[i]["DebitMoney"]);
                    if (dtVI.Rows[i]["CreditMoney"].ToString() != string.Empty && dtVI.Rows[i]["DebitMoney"].ToString() != string.Empty )
                    {
                        if (Convert.ToDecimal(dtVI.Rows[i]["DebitMoney"]) != 0 && Convert.ToDecimal(dtVI.Rows[i]["CreditMoney"]) != 0)
                        {
                            XtraMessageBox.Show("不能借贷双方都有金额！");
                            return;
                        }
                        dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtVI.Rows[i]["AccountsOne"] + ")" }).Tables[0];
                        if (dtKM.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtKM.Rows[0]["MoneyType"]) == 0)
                            {
                                XtraMessageBox.Show("科目"+dtKM.Rows[0]["Name"].ToString()+"没有指定余额方向 ！");
                                return;
                            }
                        }
                    }

                }
                if (_CM != _DM)
                {
                    XtraMessageBox.Show("借贷双方不相等，不能过帐！");
                    return;
                }
                if (DialogResult.No == MessageBox.Show("是否确认过帐", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    return;
                //dtVM.Rows[0]["FuHe"] = BasicClass.UserInfo.UserID;
                //dtVM.Rows[0]["FHDate"] = BasicClass.GetDataSet.GetDateTime().Date;
                //BasicClass.GetDataSet.UpData(bllVM, dtVM);
                string bllMI = "Hownet.BLL.MoneyList";
               
                
                for (int i = 0; i < dtVI.Rows.Count; i++)
                {
                    try
                    {
                        if (dtVI.Rows[i]["AccountsOne"]!=null&&dtVI.Rows[i]["AccountsOne"].ToString()!=string.Empty&& Convert.ToInt32(dtVI.Rows[i]["AccountsOne"]) > 0)//有一级科目的，才更新科目余额
                        {
                            _CM = Convert.ToDecimal(dtVI.Rows[i]["CreditMoney"]);
                            _DM = Convert.ToDecimal(dtVI.Rows[i]["DebitMoney"]);
                            if (Convert.ToInt32(dtVI.Rows[i]["AccountsTwo"]) > 0)//有二级级科目的，更新科目余额
                            {
                                
                                dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtVI.Rows[i]["AccountsTwo"] + ")" }).Tables[0];
                                if (dtKM.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(dtKM.Rows[0]["MoneyType"]) == 1)//科目余额需在借方的
                                    {
                                        if (_CM != 0)//有贷方金额
                                        {
                                            dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - _CM;//原余额减少
                                        }
                                        if (_DM != 0)//有贷方金额
                                        {
                                            dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + _DM;//原余额增加
                                        }
                                        if (Convert.ToInt32(dtKM.Rows[0]["ParentID"]) == 1002)//属于银行存款的
                                        {
                                            decimal lastMoney = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(bllMI, "GetLastMoney", new object[] { Convert.ToInt32(dtVI.Rows[i]["AccountsTwo"]) }));
                                            DataTable dtMI = BasicClass.GetDataSet.GetDS(bllMI, "GetList", new object[] { "1=2" }).Tables[0];
                                            DataRow dr = dtMI.NewRow();
                                            dr["KJKMID"] = Convert.ToInt32(dtVI.Rows[i]["AccountsTwo"]);
                                            dr["DateTime"] = dtVM.Rows[0]["DateTime"];
                                            if (_DM != 0)
                                            {
                                                dr["InMoney"] = _DM;
                                                dr["Money"] = lastMoney + _DM;
                                            }
                                            if (_CM != 0)
                                            {
                                                dr["OutMoney"] = _CM;
                                                dr["Money"] = lastMoney - _CM;
                                            }
                                            dr["TableID"] = _MainID;
                                            dr["TypeID"] = 3;
                                            dr["Remark"] = dtVI.Rows[0]["Summary"];
                                            dtMI.Rows.Add(dr);
                                            BasicClass.GetDataSet.Add(bllMI, dtMI);
                                        }
                                    }
                                    else //科目余额需在贷方方的
                                    {
                                        if (_CM != 0)//有贷方金额
                                        {
                                            dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + _CM;//原余额减少
                                        }
                                        if (_DM != 0)//有贷方金额
                                        {
                                            dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - _DM;//原余额增加
                                        }
                                    }
                                    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                                }
                            }
                            dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtVI.Rows[i]["AccountsOne"] + ")" }).Tables[0];
                            if (dtKM.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dtKM.Rows[0]["MoneyType"]) == 1)//科目余额需在借方的
                                {
                                    if (_CM != 0)//有贷方金额
                                    {
                                        dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - _CM;//原余额减少
                                    }
                                    if (_DM != 0)//有贷方金额
                                    {
                                        dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + _DM;//原余额增加
                                    }
                                    if (Convert.ToInt32(dtKM.Rows[0]["ID"]) == 130)//属于现金的
                                    {
                                        decimal lastMoney = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(bllMI, "GetLastMoney", new object[] { Convert.ToInt32(dtVI.Rows[i]["AccountsOne"]) }));
                                        DataTable dtMI = BasicClass.GetDataSet.GetDS(bllMI, "GetList", new object[] { "1=2" }).Tables[0];
                                        DataRow dr = dtMI.NewRow();
                                        dr["KJKMID"] = Convert.ToInt32(dtVI.Rows[i]["AccountsOne"]);
                                        dr["DateTime"] = dtVM.Rows[0]["DateTime"];
                                        if (_DM != 0)
                                        {
                                            dr["InMoney"] = _DM;
                                            dr["Money"] = lastMoney + _DM;
                                        }
                                        if (_CM != 0)
                                        {
                                            dr["OutMoney"] = _CM;
                                            dr["Money"] = lastMoney - _CM;
                                        }
                                        dr["TableID"] = _MainID;
                                        dr["TypeID"] = (int)BasicClass.Enums.MoneyTableType.Vouchers;
                                        dr["Remark"] = dtVI.Rows[0]["Summary"];
                                        dtMI.Rows.Add(dr);
                                        BasicClass.GetDataSet.Add(bllMI, dtMI);
                                    }
                                }
                                else //科目余额需在贷方方的
                                {
                                    if (_CM != 0)//有贷方金额
                                    {
                                        dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + _CM;//原余额减少
                                    }
                                    if (_DM != 0)//有贷方金额
                                    {
                                        dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - _DM;//原余额增加
                                    }
                                }
                                BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                                if (Convert.ToInt32(dtVI.Rows[i]["AccountsTwo"]) > 0)//有二级级科目的，更新科目余额
                                {
                                    BasicClass.GetDataSet.ExecSql("Hownet.BLL.Bas_KJKM", "UpMoney", new object[] { Convert.ToInt32(dtVI.Rows[i]["AccountsOne"]) });
                                }
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                dtVM.Rows[0]["FuHe"] = BasicClass.UserInfo.UserID;
                dtVM.Rows[0]["FHDate"] = BasicClass.GetDataSet.GetDateTime().Date;
                BasicClass.GetDataSet.UpData(bllVM, dtVM);
                //DataTable dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + lookUpEdit1.EditValue + ")" }).Tables[0];
                //if (dtKM.Rows.Count > 0)
                //{
                //    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - money;
                //    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                //}
                //for (int i = 0; i < dtInfo.Rows.Count; i++)
                //{
                //    try
                //    {
                //        dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtInfo.Rows[i]["费用类别ID"] + ")" }).Tables[0];
                //        if (dtKM.Rows.Count > 0)
                //        {
                //            dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + Convert.ToDecimal(dtInfo.Rows[i]["金额"]) + Convert.ToDecimal(dtInfo.Rows[i]["手续费"]);
                //            BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                //        }
                //    }
                //    catch
                //    {
                //    }
                //}


                //
                //decimal lastMoney = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(bllMI, "GetLastMoney", new object[] { Convert.ToInt32(lookUpEdit1.EditValue) }));
                //DataTable dtMI = BasicClass.GetDataSet.GetDS(bllMI, "GetList", new object[] { "1=2" }).Tables[0];
                //DataRow dr = dtMI.NewRow();
                //dr["KJKMID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                //dr["DateTime"] = dtPZ.Rows[0]["时间"];
                //dr["InMoney"] = 0;
                //dr["OutMoney"] = money;
                //dr["Money"] = lastMoney - money;
                //dr["TableID"] = 0;
                //dr["TypeID"] = 0;
                //dr["Remark"] = gridView1.GetRowCellDisplayText(0, _co备注);
                //dtMI.Rows.Add(dr);
                //BasicClass.GetDataSet.Add(bllMI, dtMI);
                ////dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(CompanyID=" + _companyID + ")" }).Tables[0];
                ////if (dtKM.Rows.Count > 0)
                ////{
                ////    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - chang;
                ////    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                ////}

                ShowView(bs.Position);
            }
            catch (Exception ex)
            {
            }
        }


    }
}