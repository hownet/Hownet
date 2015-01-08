using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet.Finance
{
    public partial class frKJFL :  DevExpress.XtraEditors.XtraForm
    {
        public frKJFL()
        {
            InitializeComponent();
        }
        BindingSource bs = new BindingSource();
        int _MainID = 0;
        DataTable dtMain = new DataTable();
        DataTable dtPZ = new DataTable();
        DataTable dtInfo = new DataTable();
        string bllMain = "Hownet.BLL.CW_KJFL";
        string bllInfo = "Hownet.BLL.CW_KJFLInfo";
        public frKJFL(int MainID)
            : this()
        {
            _MainID = MainID;
        }
        private void frKJFL_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetMoneyKJKM", null).Tables[0];
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
                bs.DataSource = dtMain;
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                bs.Position = dtMain.Rows.Count - 1;
                ShowView(0);
               // bar1.Visible = false;
            }
            _co报销部门ID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        void bs_PositionChanged(object sender, EventArgs e)
        {
            if (bs.Position > -1)
                ShowView(bs.Position);
        }

        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllMain, "GetIDList",new object[]{0}).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(0);
            bs.DataSource = dtMain;
        }
        void AddRow()
        {
            try
            {
                dtPZ.Rows.Clear();
                DataRow dr = dtPZ.NewRow();
                dr["A"] = 3;
                dr["ID"] = 0;
                DateTime dtNow = BasicClass.GetDataSet.GetDateTime();

                dr["编号"] = BasicClass.GetDataSet.GetOne(bllMain, "NewNum", new object[] { dtNow,0 });
                dr["年"] = dtNow.Year;
                dr["月"] = dtNow.Month.ToString().PadLeft(2, '0');
                dr["日"] = dtNow.Day.ToString().PadLeft(2, '0');
                dr["时间"] = dtNow.ToString("yyyy-MM-dd");
                dr["附件"] = string.Empty;
                dr["制单人"] = BasicClass.UserInfo.TrueName;
                dr["审核"] = string.Empty;
                dr["记帐"] = string.Empty;
                dr["财务主管"] = string.Empty;
                dr["制单人ID"] = BasicClass.UserInfo.UserID;
                dr["审核ID"] = 0;
                dr["记帐ID"] = 0;
                dr["财务主管ID"] =0;
                dr["制单日期"] = dtNow;
                dr["审核日期"] = string.Empty;
                dr["记账日期"] = string.Empty;
                dr["帐户"] = string.Empty;
                dr["帐户ID"] = 130;
                dr["TypeID"] = 0;
                dr["TableID"] = 0;
                dtPZ.Rows.Add(dr);
            }
            catch (Exception ex)
            {
            }
        }

        private void ShowView(int p)
        {
            try
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
                _MainID =Convert.ToInt32(dtMain.DefaultView[p]["ID"]);
                dtPZ = BasicClass.GetDataSet.GetDS(bllMain, "GetList", new object[] { "(ID='" + _MainID + "')" }).Tables[0];
                if (_MainID ==0)
                {
                    AddRow();
                }
                _te编号.Text = dtPZ.Rows[0]["年"].ToString() + dtPZ.Rows[0]["月"].ToString() + dtPZ.Rows[0]["日"].ToString() + dtPZ.Rows[0]["编号"].ToString().PadLeft(3, '0');
                dateEdit1.EditValue = Convert.ToDateTime(dtPZ.Rows[0]["年"].ToString() + "-" + dtPZ.Rows[0]["月"].ToString() + "-" + dtPZ.Rows[0]["日"].ToString());
                _lb记账.Text = "记账：" + dtPZ.Rows[0]["记帐"].ToString();
                _lb审核.Text = "审核：" + dtPZ.Rows[0]["审核"].ToString();
                _lb制单人.Text = "制单人：" + dtPZ.Rows[0]["制单人"].ToString();
             lookUpEdit1.EditValue =Convert.ToInt32( dtPZ.Rows[0]["帐户ID"]);
             if (_MainID == 0)
             {
                 dtPZ.Rows[0]["帐户"] = lookUpEdit1.Text;
             }
                dtInfo = BasicClass.GetDataSet.GetDS(bllInfo, "GetList", new object[] { "(MainID='" + _MainID + "')" }).Tables[0];
                if (dtInfo.Rows.Count < 5)
                {
                    DataRow dr = dtInfo.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["MainID"] = _MainID;
                    dr["费用类别"] = string.Empty;
                    dr["金额"] = DBNull.Value;
                    dr["手续费"] = DBNull.Value;
                    dr["费用类别ID"] = 0;
                    dr["项目名称"] = string.Empty;
                    dr["项目名称ID"] = 0;
                    dr["客户订单编号"] = string.Empty;
                    dr["客户"] = string.Empty;
                    dr["款号"] = string.Empty;
                    dr["报销人"] = string.Empty;
                    dr["备注"] = string.Empty;
                    dr["客户订单ID"] = 0;
                    dr["客户ID"] = 0;
                    dr["款号ID"] = 0;
                    dr["报销人ID"] = 0;
                    dr["二级科目"] = string.Empty;
                    dr["二级科目ID"] = 0;
                    dr["报销部门"] = string.Empty;
                    dr["报销部门ID"] = 0;

                    for (int i = dtInfo.Rows.Count; i < 6; i++)
                    {
                        dtInfo.Rows.Add(dr.ItemArray);
                    }
                }
                gridControl1.DataSource = dtInfo;
                _barVerify.Enabled = dtPZ.Rows[0]["审核"].ToString() == string.Empty;
                _barUnVierfy.Enabled = !_barVerify.Enabled;
                _barGZ.Enabled = dtPZ.Rows[0]["记帐"].ToString() == string.Empty;
                _barTuiZhang.Enabled = !_barGZ.Enabled;
              //  _brAddNew.Enabled = dtPZ.Rows[0]["审核"].ToString() != string.Empty;
            }
            catch (Exception ex)
            {
            }
        }

        private void _reZY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                BasicClass.cResult crZY = new BasicClass.cResult();
                crZY.RowChanged += new BasicClass.RowChangedHandler(crZY_RowChanged);
                Form fr = new frZaiYao(crZY,-1);
                fr.ShowDialog();
            }
            
        }

        void crZY_RowChanged(DataTable dtZY)
        {
            if (dtZY.Rows.Count > 0)
            {
                gridView1.SetFocusedValue(dtZY.Rows[0][1]);
                gridView1.SetFocusedRowCellValue(_co项目名称ID, dtZY.Rows[0][0]);

            }
        }

        private void _brFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }

        private void _brPrv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }

        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }

        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(0);
            bs.Position = dtMain.Rows.Count - 1;
        }

        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(0);
            ShowView(bs.Position);
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(1);
            ShowView(bs.Position);
        }

        private void _barUnVierfy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtPZ.Rows[0]["记帐"].ToString() != string.Empty)
            {
                MessageBox.Show("已记帐，不能弃审~");
                return;
            }
            Save(-1);
            ShowView(bs.Position);
        }

        private void _barGZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (dtPZ.Rows[0]["审核"].ToString() == string.Empty)
                {
                    MessageBox.Show("请先审核~");
                    return;
                }
                DataTable dtKM = new DataTable();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {

                    dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtInfo.Rows[i]["费用类别ID"] + ")" }).Tables[0];
                    if (dtKM.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtKM.Rows[0]["MoneyType"]) == 0)
                        {
                            MessageBox.Show("科目" + dtKM.Rows[0]["Name"].ToString() + "没有指定余额方向 ！");
                            return;
                        }
                    }
                }
                if (DialogResult.No == MessageBox.Show("是否确认过帐", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    return;
                Save(2);
                decimal money = 0;
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    try
                    {
                        money += Convert.ToDecimal(dtInfo.Rows[i]["金额"]);
                        money += Convert.ToDecimal(dtInfo.Rows[i]["手续费"]);
                    }
                    catch
                    {
                    }
                }
                 dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + lookUpEdit1.EditValue + ")" }).Tables[0];
                if (dtKM.Rows.Count > 0)
                {
                    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - money;
                    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                }
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    try
                    {
                        dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtInfo.Rows[i]["费用类别ID"] + ")" }).Tables[0];
                        if (dtKM.Rows.Count > 0)
                        {
                            //dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + Convert.ToDecimal(dtInfo.Rows[i]["金额"]) + Convert.ToDecimal(dtInfo.Rows[i]["手续费"]);
                            //BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);

                            if (Convert.ToInt32(dtKM.Rows[0]["MoneyType"]) == 1)//科目余额需在借方的
                            {
                                dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + Convert.ToDecimal(dtInfo.Rows[i]["金额"]);//原余额增加
                            }
                            else //科目余额需在贷方方的
                            {
                                    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - Convert.ToDecimal(dtInfo.Rows[i]["金额"]);//原余额增加
                            }
                            BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);


                        }
                        dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtInfo.Rows[i]["二级科目ID"] + ")" }).Tables[0];
                        if (dtKM.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtKM.Rows[0]["MoneyType"]) == 1)//科目余额需在借方的
                            {
                                dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + Convert.ToDecimal(dtInfo.Rows[i]["金额"]);//原余额增加
                            }
                            else //科目余额需在贷方方的
                            {
                                dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - Convert.ToDecimal(dtInfo.Rows[i]["金额"]);//原余额增加
                            }
                            BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                        }
                    }
                    catch
                    {
                    }
                }
                string bllMI = "Hownet.BLL.MoneyList";
                decimal lastMoney = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(bllMI, "GetLastMoney", new object[] { Convert.ToInt32(lookUpEdit1.EditValue) }));
                DataTable dtMI = BasicClass.GetDataSet.GetDS(bllMI, "GetList", new object[] { "1=2" }).Tables[0];
                DataRow dr = dtMI.NewRow();
                dr["KJKMID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                dr["DateTime"] = dtPZ.Rows[0]["时间"];
                dr["InMoney"] = 0;
                dr["OutMoney"] = money;
                dr["Money"] = lastMoney - money;
                dr["TableID"] = _MainID;
                dr["TypeID"] = (int)BasicClass.Enums.MoneyTableType.KJFL;
                dr["Remark"] = gridView1.GetRowCellDisplayText(0, _co备注);
                dtMI.Rows.Add(dr);
                BasicClass.GetDataSet.Add(bllMI, dtMI);
                //dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(CompanyID=" + _companyID + ")" }).Tables[0];
                //if (dtKM.Rows.Count > 0)
                //{
                //    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - chang;
                //    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                //}
            
            ShowView(bs.Position);
            }
            catch (Exception ex)
            {
            }
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            string IDList = string.Empty;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtInfo.Rows[i]["ID"]) > 0)
                {
                    IDList += dtInfo.Rows[i]["ID"].ToString() + ",";
                }
            }
            if (IDList.Length > 0)
            {
              IDList=  IDList.Remove(IDList.Length - 1);
                BasicClass.GetDataSet.ExecSql(bllInfo, "DeleteList", new object[] { IDList });
            }
            BasicClass.GetDataSet.ExecSql(bllMain, "Delete", new object[] { _MainID });
            if (dtMain.Rows.Count > 1)
            {
                dtMain.Rows.RemoveAt(bs.Position);
                _brAddNew.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            }
            else
            {
                InData();
                ShowView(0);
            }
        }

        private void _barTuiZhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToInt32(dtPZ.Rows[0]["TableID"]) > 0)
            {
                MessageBox.Show("此单据不能退帐，请到原始单据中弃审！");
                return;
            }


              if (DialogResult.No == MessageBox.Show("是否确认退帐", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            Save(-2);
            decimal money = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                try
                {
                    money += Convert.ToDecimal(dtInfo.Rows[i]["金额"]);
                    money += Convert.ToDecimal(dtInfo.Rows[i]["手续费"]);
                }
                catch
                {
                }
            }
          DataTable    dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + lookUpEdit1.EditValue + ")" }).Tables[0];
            if (dtKM.Rows.Count > 0)
            {
                dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + money;
                BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
            }
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                try
                {
                    dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + dtInfo.Rows[i]["费用类别ID"] + ")" }).Tables[0];
                    if (dtKM.Rows.Count > 0)
                    {
                        dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - Convert.ToDecimal(dtInfo.Rows[i]["金额"]) - Convert.ToDecimal(dtInfo.Rows[i]["手续费"]);
                        BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
                    }
                }
                catch
                {
                }
            }
            ShowView(bs.Position);
        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _reKM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                BasicClass.cResult crKM = new BasicClass.cResult();
                crKM.RowChanged += new BasicClass.RowChangedHandler(crKM_RowChanged);
                Form fr = new frKJKM(crKM, -1);
                fr.ShowDialog();
            }
        }

        void crKM_RowChanged(DataTable dtKM)
        {
            if (dtKM.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtKM.Rows[0][0]) > 0)
                {
                    gridView1.SetFocusedValue(dtKM.Rows[0][1]);
                    gridView1.SetFocusedRowCellValue(_co费用类别ID, dtKM.Rows[0][0]);
                    gridView1.SetFocusedRowCellValue(_co二级科目, dtKM.Rows[1][1]);
                    gridView1.SetFocusedRowCellValue(_co二级科目ID, dtKM.Rows[1][0]);
                }
                else
                {
                    gridView1.SetFocusedValue(dtKM.Rows[1][1]);
                    gridView1.SetFocusedRowCellValue(_co费用类别ID, dtKM.Rows[1][0]);
                    gridView1.SetFocusedRowCellValue(_co二级科目, string.Empty);
                    gridView1.SetFocusedRowCellValue(_co二级科目ID, 0);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeID">0为制单保存，1为审核，2为记帐，3为财务主管</param>
        private void Save(int TypeID)
        {
            try
            {
                DateTime dtNow = Convert.ToDateTime(dateEdit1.EditValue);
                dtPZ.Rows[0]["时间"] = dtNow.ToString("yyyy-MM-dd");
                dtPZ.Rows[0]["年"] = dtNow.Year;
                dtPZ.Rows[0]["月"] = dtNow.Month.ToString().PadLeft(2, '0');
                dtPZ.Rows[0]["日"] = dtNow.Day.ToString().PadLeft(2, '0');

                if (TypeID == 1)
                {
                    dtPZ.Rows[0]["审核"] = BasicClass.UserInfo.TrueName;
                    dtPZ.Rows[0]["审核ID"] = BasicClass.UserInfo.UserID;
                    dtPZ.Rows[0]["审核日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                else if (TypeID == -1)
                {
                    dtPZ.Rows[0]["审核"] =string.Empty;
                    dtPZ.Rows[0]["审核ID"] = 0;
                    dtPZ.Rows[0]["审核日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                else if (TypeID == 2)
                {
                    dtPZ.Rows[0]["记帐"] = BasicClass.UserInfo.TrueName;
                    dtPZ.Rows[0]["记帐ID"] = BasicClass.UserInfo.UserID;
                    dtPZ.Rows[0]["记账日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                     else if (TypeID == -2)
                {
                    dtPZ.Rows[0]["记帐"] = string.Empty;
                    dtPZ.Rows[0]["记帐ID"] = 0;
                    dtPZ.Rows[0]["记账日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                else if (TypeID == 3)
                {
                    dtPZ.Rows[0]["财务主管"] = BasicClass.UserInfo.TrueName;
                    dtPZ.Rows[0]["财务主管ID"] = BasicClass.UserInfo.UserID;
                    dtPZ.Rows[0]["主管审核日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                else if (TypeID == -3)
                {
                    dtPZ.Rows[0]["财务主管"] = string.Empty;
                    dtPZ.Rows[0]["财务主管ID"] = 0;
                    dtPZ.Rows[0]["主管审核日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                if (_MainID == 0)
                {
                    dtPZ.Rows[0]["编号"] = BasicClass.GetDataSet.GetOne(bllMain, "NewNum", new object[] { dtNow, 0 });
                    dtMain.Rows[bs.Position]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllMain, dtPZ);
                }
                else
                {
                    BasicClass.GetDataSet.UpData(bllMain, dtPZ);
                }
                int a = 0;
                DataTable dtTem = dtInfo.Clone();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    a = Convert.ToInt32(dtInfo.Rows[i]["A"]);
                    if (dtInfo.Rows[i]["费用类别"].ToString() != string.Empty)
                    {
                        try
                        {
                            if ((dtInfo.Rows[i]["金额"].ToString() != string.Empty && Convert.ToDecimal(dtInfo.Rows[i]["金额"]) != 0) || (dtInfo.Rows[i]["手续费"].ToString() != string.Empty && Convert.ToDecimal(dtInfo.Rows[i]["手续费"]) != 0))
                            {
                                dtTem.Rows.Clear();
                                dtTem.Rows.Add(dtInfo.Rows[i].ItemArray);
                                if (a == 3)
                                {
                                    dtTem.Rows[0]["MainID"] = _MainID;
                                    dtInfo.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllInfo, dtTem);
                                }
                                else if (a == 2)
                                {
                                    BasicClass.GetDataSet.UpData(bllInfo, dtTem);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        if (a == 2)
                        {
                            BasicClass.GetDataSet.ExecSql(bllInfo, "Delete", new object[] { new Guid(dtInfo.Rows[i]["ID"].ToString()) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                if (gridView1.FocusedColumn == _co项目名称)
                {
                    BasicClass.cResult crZY = new BasicClass.cResult();
                    crZY.RowChanged += new BasicClass.RowChangedHandler(crZY_RowChanged);
                    Form fr = new frZaiYao(crZY, -1);
                    fr.ShowDialog();
                }
                else if (gridView1.FocusedColumn == _co费用类别)
                {
                    BasicClass.cResult crKM = new BasicClass.cResult();
                    crKM.RowChanged += new BasicClass.RowChangedHandler(crKM_RowChanged);
                    Form fr = new frKJKM(crKM, -1);
                    fr.ShowDialog();
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Column != _coA&&Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA))==1)
                {
                    gridView1.SetFocusedRowCellValue(_coA, 2);
                }
            }
        }

        private void _reKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                BasicClass.cResult crKH = new BasicClass.cResult();
                crKH.RowChanged += new BasicClass.RowChangedHandler(crKH_RowChanged);
                Form fr = new BaseForm.frSelectTask(crKH);
                fr.ShowDialog();
            }
        }

        void crKH_RowChanged(DataTable dtKM)
        {
            if (dtKM.Rows.Count > 0)
            {
                gridView1.SetFocusedValue(dtKM.Rows[0][1]);
                gridView1.SetFocusedRowCellValue(_co款号ID, dtKM.Rows[0][0]);
                gridView1.SetFocusedRowCellValue(_co客户订单编号, dtKM.Rows[0][3]);
                gridView1.SetFocusedRowCellValue(_co客户订单ID, dtKM.Rows[0][2]);
                gridView1.SetFocusedRowCellValue(_co客户, dtKM.Rows[0][5]);
                gridView1.SetFocusedRowCellValue(_co客户ID, dtKM.Rows[0][4]);

            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (dtPZ.Rows[0]["审核"].ToString().Trim().Length == 0)
            {

                dtPZ.Rows[0]["帐户"] = lookUpEdit1.Text;

                dtPZ.Rows[0]["帐户ID"] = Convert.ToInt32(lookUpEdit1.EditValue);

            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtMain = new DataTable();
            dtMain.TableName = "Main";
            dtMain.Columns.Add("支付帐号", typeof(string));
            dtMain.Columns.Add("编号");
            dtMain.Columns.Add("日期");
            dtMain.Columns.Add("制单");
            dtMain.Columns.Add("审核");
            dtMain.Columns.Add("记帐");

            DataTable dtInfo = new DataTable();
            dtInfo.TableName = "Info";
            dtInfo.Columns.Add("描述");
            dtInfo.Columns.Add("款号");
            dtInfo.Columns.Add("客户订单编号");
            dtInfo.Columns.Add("客户");
            dtInfo.Columns.Add("一级科目");
            dtInfo.Columns.Add("二级科目");
            dtInfo.Columns.Add("金额");
            dtInfo.Columns.Add("项目名称");
            dtInfo.Columns.Add("报销人");
            dtInfo.Columns.Add("报销部门");


            dtMain.Rows.Add(lookUpEdit1.Text, _te编号.Text, dateEdit1.Text, _lb制单人.Text, _lb审核.Text, _lb记账.Text);
            for(int i=0;i<gridView1.RowCount;i++)
            {
                DataRow dr = dtInfo.NewRow();
                dr[0] = gridView1.GetRowCellDisplayText(i, _co备注);
                dr[1] = gridView1.GetRowCellDisplayText(i, _co款号);
                dr[2] = gridView1.GetRowCellDisplayText(i, _co客户订单编号);
                dr[3] = gridView1.GetRowCellDisplayText(i, _co客户);
                dr[4] = gridView1.GetRowCellDisplayText(i, _co费用类别);
                dr[5] = gridView1.GetRowCellDisplayText(i, _co二级科目);
                dr[6] = gridView1.GetRowCellDisplayText(i, _co金额);
                dr[7] = gridView1.GetRowCellDisplayText(i, _co项目名称);
                dr[8] = gridView1.GetRowCellDisplayText(i, _co报销人);
                dr[9] = gridView1.GetRowCellDisplayText(i, _co报销部门);
                dtInfo.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            ds.Tables.Add(dtMain);
            ds.Tables.Add(dtInfo);

            BaseForm.PrintClass.PrintKJFL(ds);
        }

    }
}
