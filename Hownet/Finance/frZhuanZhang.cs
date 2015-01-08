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
    public partial class frZhuanZhang :  DevExpress.XtraEditors.XtraForm
    {
        public frZhuanZhang()
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
        private void frKJFL_Load(object sender, EventArgs e)
        {
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
            dtMain = BasicClass.GetDataSet.GetDS(bllMain, "GetIDList",new object[]{2}).Tables[0];
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

                dr["编号"] = BasicClass.GetDataSet.GetOne(bllMain, "NewNum", new object[] { dtNow,2 });
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
                dr["TypeID"] = 2;
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
                textEdit1.Text = dtPZ.Rows[0]["帐户"].ToString();
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


                    for (int i = dtInfo.Rows.Count; i < 6; i++)
                    {
                        dtInfo.Rows.Add(dr.ItemArray);
                    }
                }
                gridControl1.DataSource = dtInfo;
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
            if (dtPZ.Rows[0]["审核"].ToString() != string.Empty)
            {
                MessageBox.Show("请先审核~");
                return;
            }
            Save(2);
            ShowView(bs.Position);
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barTuiZhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
                gridView1.SetFocusedValue(dtKM.Rows[0][1]);
                gridView1.SetFocusedRowCellValue(_co费用类别ID, dtKM.Rows[0][0]);
                
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
                    dtPZ.Rows[0]["记帐日期"] = BasicClass.GetDataSet.GetDateTime();
                }
                     else if (TypeID == -2)
                {
                    dtPZ.Rows[0]["记帐"] = string.Empty;
                    dtPZ.Rows[0]["记帐ID"] = 0;
                    dtPZ.Rows[0]["记帐日期"] = BasicClass.GetDataSet.GetDateTime();
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
                    dtPZ.Rows[0]["编号"] = BasicClass.GetDataSet.GetOne(bllMain, "NewNum", new object[] { dtNow,2 });
                    dtMain.Rows[bs.Position]["ID"] =_MainID = BasicClass.GetDataSet.Add(bllMain, dtPZ);
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

        private void textEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (dtPZ.Rows[0]["审核"].ToString().Trim().Length == 0)
            {
                BasicClass.cResult crKM2 = new BasicClass.cResult();
                crKM2.RowChanged += new BasicClass.RowChangedHandler(crKM2_RowChanged);
                Form fr = new frKJKM(crKM2, -1);
                fr.ShowDialog();
            }
        }
        void crKM2_RowChanged(DataTable dtKM)
        {
            if (dtKM.Rows.Count > 0)
            {
                dtPZ.Rows[0]["帐户"] = textEdit1.EditValue = dtKM.Rows[0][1];
                
              dtPZ.Rows[0]["帐户ID"] = dtKM.Rows[0][0];

            }
        }

    }
}
