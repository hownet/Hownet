using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Hownet.Pay
{
    public partial class WorkticketBack : DevExpress.XtraEditors.XtraForm
    {
        public WorkticketBack()
        {
            InitializeComponent();
        }
        private int EmployeeID = 0;
        DataSet ds = new DataSet();
        string bllE = "Hownet.BLL.MiniEmp";
        string bllWInfo = "Hownet.BLL.WorkTicketInfo";
        string bllPI = "Hownet.BLL.PayInfo";
        BindingSource bs = new BindingSource();
        //Hownet.Model.PayInfo modPI = new Hownet.Model.PayInfo();
        DataTable dtMain = new DataTable();
        DataTable dtEmp = new DataTable();
        DataTable dtPayInfo = new DataTable();
        DateTime dt = DateTime.Today;
        int _TaskID = 0;
        int _BoxNum = 0;
        int _WorkingID = 0;
        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = (int)e.KeyChar;

            if (k == 13)
            {
                dt = DateTime.Parse(dateEdit1.EditValue.ToString());
                RepSn();
                //textEdit2.Focus();
            }
        }
        void showView(int p)
        {
            if (dtMain.DefaultView[p]["DateTime"].ToString() != "")
                dateEdit1.EditValue = dt = (DateTime)(dtMain.DefaultView[p]["DateTime"]);
            else
                dateEdit1.EditValue = dt = DateTime.Today;
            textEdit1.Focus();
            dtPayInfo = BasicClass.GetDataSet.GetDS(bllPI, "GetList", new object[] { "(ID=0)" }).Tables[0];
        }
        private void WorkticketBack_Load(object sender, EventArgs e)
        {
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            ShowData();
            _coMaterielName.ColumnEdit =BaseForm. RepositoryItem._reProduce;
            _coColorName.ColumnEdit =BaseForm. RepositoryItem._reColor;
            _coSizeName.ColumnEdit =BaseForm. RepositoryItem._reSize;
            _coWorkName.ColumnEdit =BaseForm. RepositoryItem._reWorking;
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _brAddNew.Enabled = simpleButton2.Enabled = false;
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //}
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            showView(bs.Position);
        }
        private void ShowData()
        {
            dtEmp = BasicClass.GetDataSet.GetDS(bllE, "GetWorkList", null).Tables[0];
            lookUpEdit1.Properties.DataSource = dtEmp.DefaultView;
            lookUpEdit1.EditValue = 0;
            dtMain = BasicClass.GetDataSet.GetDS(bllPI, "GetDayList", null).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;
            bs.Position = dtMain.Rows.Count - 1;
            showView(bs.Position);
        }
        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (EmployeeID == 0)
            {
                XtraMessageBox.Show("请先选择员工！");
                return;
            }
            int k = (int)e.KeyChar;
            string barNum = textEdit2.Text.Trim();

            if (k == 13)
            {
                //try
                //{
                    //bar = Convert.ToInt32(barNum.ToUpper(), 16);//十六进制
                    //SixtyTwoScale sts = barNum;
                    //bar = (int)(sts.ToInt64());
                    ////bar = int.Parse(barNum);//十进制
                    //DataSet dsTem = bllWInfo.GetBoolAdd(bar);
                    //if (dsTem.Tables[0].Rows.Count == 0)
                    //{
                _TaskID = int.Parse(barNum.Substring(0, 5));
                _BoxNum = int.Parse(barNum.Substring(5, 4));
                _WorkingID = int.Parse(barNum.Substring(9));
                object[] o = new object[] {_TaskID,_BoxNum,_WorkingID };
                DataTable dtInfo = BasicClass.GetDataSet.GetDS(bllWInfo, "GetBoolAdd", o).Tables[0];
                if (dtInfo.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("错误的条码号！");
                        textEdit2.Text = null;
                        return;
                    }
                    //}

                if (dtInfo.DefaultView[0]["BackID"].ToString() == "1")
                    {
                        XtraMessageBox.Show("该条码已于：" + ((DateTime)(dtInfo.DefaultView[0]["DateTime"])).ToShortDateString()
                            + "记录给：" + BasicClass.GetDataSet.GetOne(bllE, "GetName", new object[] { (int.Parse(dtInfo.DefaultView[0]["EmployeeID"].ToString())) }).ToString());
                        textEdit2.Text = null;
                        return;
                    }


                    //if (checkEdit1.Checked)
                    //{
                    //    if (dsTem.Tables[0].DefaultView[0]["BackID"].ToString() == "2")
                    //    {
                    //        XtraMessageBox.Show("该条码已有分拆后录入");
                    //        return;
                    //    }
                        dt = (DateTime)(dateEdit1.EditValue);
                        dtInfo.Rows[0]["EmployeeID"] = EmployeeID;
                        dtInfo.Rows[0]["BackID"] = 1;
                        dtInfo.Rows[0]["DateTime"] = dt;
                        BasicClass.GetDataSet.UpData(bllWInfo, dtInfo);
                        dtPayInfo.Rows.Clear();

                        dtPayInfo = BasicClass.GetDataSet.GetDS(bllPI, "GetAddPayInfo", o).Tables[0];
                        dtPayInfo.Rows[0]["OderNum"] = BasicClass.GetDataSet.GetOne("Hownet.BLL.ProductTaskMain", "GetNum", new object[] { _TaskID });
                        dtPayInfo.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllPI, dtPayInfo);
                        ds.Tables[0].Rows.Add(dtPayInfo.Rows[0].ItemArray);
                        ds.AcceptChanges();
                        //bllWInfo.GetBarBack(bar, EmployeeID, dt, 1);
                        //DataSet dsBack = bllWInfo.GetInfoIDListByPW(bar);
                        //if (dsBack.Tables[0].Rows.Count > 0)
                        //{
                        //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        //    int i = ds.Tables[0].Rows.Count - 1;
                        //    for (int r = 0; r < 8; r++)
                        //    {
                        //        ds.Tables[0].Rows[i][r] = dsBack.Tables[0].Rows[0][r];
                        //    }
                        //    ds.Tables[0].Rows[i]["OderNum"] = ((DateTime)(dsBack.Tables[0].Rows[0]["Date"])).ToString("yyyyMMdd") + Num.GetNum(dsBack.Tables[0].Rows[0]["Num"].ToString());
                        //    decimal amount = 0;
                        //    decimal price = 0;
                        //    decimal money = 0;

                        //    amount = decimal.Parse(dsBack.Tables[0].DefaultView[0]["Amount"].ToString());
                        //    string obj = dsBack.Tables[0].DefaultView[0]["Price"].ToString();
                        //    if (obj != "")
                        //        price = decimal.Parse(obj);
                        //    money = amount * price;
                        //    ds.Tables[0].Rows[i]["Money"] = money;
                        //    ds.Tables[0].Rows[i]["BreakID"] = 0;
                        //    ds.Tables[0].Rows[i]["Price"] = price;
                        //    modPI.Amount = int.Parse(dsBack.Tables[0].DefaultView[0]["Amount"].ToString());
                        //    modPI.DateTime = dt;
                        //    modPI.EmployeeID = EmployeeID;
                        //    modPI.WorkticketInfoID = bar;
                        //    modPI.MaterielID = int.Parse(dsBack.Tables[0].DefaultView[0]["MaterielID"].ToString());
                        //    modPI.Price = price;
                        //    modPI.ProductWorkingID = int.Parse(dsBack.Tables[0].DefaultView[0]["ProductWorkingInfoID"].ToString());
                        //    modPI.WorkingID = int.Parse(dsBack.Tables[0].DefaultView[0]["WorkingID"].ToString());
                        //    modPI.IsSum = false;
                        //    ds.Tables[0].Rows[i]["BreakID"] = modPI.BreakID = 0;
                        //    modPI.BoxNum = int.Parse(dsBack.Tables[0].DefaultView[0]["BoxNum"].ToString());
                        //    modPI.ColorID = int.Parse(dsBack.Tables[0].DefaultView[0]["ColorID"].ToString());
                        //    modPI.SizeID = int.Parse(dsBack.Tables[0].DefaultView[0]["SizeID"].ToString());
                        //    modPI.OderNum = ds.Tables[0].Rows[i]["OderNum"].ToString();
                        //    ds.Tables[0].Rows[i]["ID"] = bllPI.Add(modPI);
                        //}
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                        textEdit2.Text = null;
                        textEdit2.Focus();
                    //}
                    //else
                    //{
                    //    textEdit3.Focus();
                    //}

                //}
                //catch
                //{
                //    bar = 0;
                //    MessageBox.Show("错误的条码号！");
                //    textEdit2.Text = null;
                //}

            }
        }
        /// <summary>
        /// 打印日报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dt = (DateTime)(dateEdit1.EditValue);

            DataSet  ds = BasicClass.GetDataSet.GetDS(bllPI, "GetDayReport", new object[] { dt });
            ds.Tables[0].TableName = "DayReport";
            ds.Tables[0].Columns.Add("Date", typeof(string));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["Date"] = dt.ToString("yyyy年MM月dd日");
                }
            }
            BaseForm.PrintClass.PrintDay(ds);
            GC.Collect();
        }
        /// <summary>
        /// 更改日期后，刷新数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dt = (DateTime)(dateEdit1.EditValue);
            DataTable dtPM = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayMain", "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];
            if (dtPM.Rows.Count > 0)
            {
                if(dt< ((DateTime)(dtPM.Rows[0]["EndDate"])).AddDays(1))
                {   //textEdit1.Enabled = false;
                    textEdit2.Enabled = false;
                    lookUpEdit1.Enabled = false;
                }
                else
                {
                    //textEdit1.Enabled = true;
                    textEdit2.Enabled = true;
                    lookUpEdit1.Enabled = true;
                }
            }
            if (Convert.ToInt32( lookUpEdit1.EditValue)>0)
            {
                dt = DateTime.Parse(dateEdit1.EditValue.ToString());
                RepSn();
            }

        }
        /// <summary>
        /// 更改员工后，刷新数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEdit1.EditValue) == 0)
                return;
            textEdit1.Text = BasicClass.GetDataSet.GetOne(bllE, "GetSn", new object[] { int.Parse(lookUpEdit1.EditValue.ToString()) }).ToString();
            dt = DateTime.Parse(dateEdit1.EditValue.ToString());
            RepSn();
        }

        /// <summary>
        /// 判断员工编号是否正确
        /// </summary>
        void RepSn()
        {
            string emlName;
            string inde = textEdit1.Text.Trim();
            //try
            //{
            EmployeeID = int.Parse(BasicClass.GetDataSet.GetOne(bllE, "GetID", new object[] { inde }).ToString());
            if (EmployeeID == 0)
            {
                XtraMessageBox.Show("错误的员工编号！");
                return;
            }
            lookUpEdit1.EditValue = EmployeeID;
            //emlName = bllE.GetName(EmployeeID);
            emlName = lookUpEdit1.Text;
            ds.Clear();
            //ds = bllWInfo.GetWorkListByPW(EmployeeID, DateTime.Parse(dateEdit1.EditValue.ToString()));
            ds = BasicClass.GetDataSet.GetDS(bllPI, "GetBackList", new object[] { dt, EmployeeID });
            gridControl1.DataSource = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            textEdit2.Focus();
            //}
            //catch
            //{
            //    MessageBox.Show("没有该编号的员工！");
            //    textEdit1.Text = "";
            //    //textEdit1.Focus();
            //}
        }

        /// <summary>
        /// 删除某条明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (e.Button.Tag.ToString() == "Remove")
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        dt = (DateTime)(dateEdit1.EditValue);
                        DataTable dtPM = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayMain", "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];
                        if (dtPM.Rows.Count > 0)
                        {
                            if (dt < ((DateTime)(dtPM.Rows[0]["EndDate"])).AddDays(1))
                            {
                                XtraMessageBox.Show("已汇总，不能删除！");
                                return;
                            }
                        }
                        int id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                        int InfoID = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, _coInfoID).ToString());
                        //int BreakID = int.Parse(gridView1.GetFocusedRowCellValue(_coBreakID).ToString());
                        BasicClass.GetDataSet.ExecSql(bllWInfo, "DelBar", new object[] { InfoID });
                        BasicClass.GetDataSet.ExecSql(bllPI, "Delete", new object[] { id });
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                        BasicClass.GetDataSet.SetDataTable();
                    }
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            bool t = checkEdit1.Checked;
            labelControl4.Visible = !t;
            textEdit3.Visible = !t;
            simpleButton2.Visible = !t;
        }

        private void textEdit3_Enter(object sender, EventArgs e)
        {
            textEdit3.SelectAll();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {

            AddBreakBar();
        }
        private void AddBreakBar()
        {
            //if (EmployeeID == 0)
            //{
            //    XtraMessageBox.Show("请先选择员工！");
            //    return;
            //}
            //string barNum = textEdit2.Text.Trim();
            //try
            //{
            //    bar = int.Parse(barNum);
            //}
            //catch
            //{
            //    XtraMessageBox.Show("输入条码有误！");
            //    return;
            //}
            //if (bar == 0)
            //    return;
            //try
            //{
            //    modWTB.Amount = int.Parse(textEdit3.Text.Trim());
            //}
            //catch
            //{
            //    XtraMessageBox.Show("输入数量有误！");
            //    return;
            //}
            //dt = (DateTime)(dateEdit1.EditValue);
            //modWTB.DateTime = dt;
            //modWTB.EmployeeID = EmployeeID;
            //modWTB.WorkTicketInfoID = bar;
            //DataSet dsBack = bllWInfo.GetInfoIDListByPW(bar);
            //if (dsBack.Tables[0].Rows.Count > 0)
            //{
            //    modWTB.WorkTicketBreakID = bllWTB.Add(modWTB);
            //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            //    int i = ds.Tables[0].Rows.Count - 1;
            //    for (int r = 0; r < 8; r++)
            //    {
            //        ds.Tables[0].Rows[i][r] = dsBack.Tables[0].Rows[0][r];
            //    }
            //    decimal amount = 0;
            //    decimal price = 0;
            //    decimal money = 0;
            //    if (dsBack.Tables[0].DefaultView[0]["Price"] == null || dsBack.Tables[0].DefaultView[0]["Price"].ToString() == string.Empty)
            //    {
            //        ds.Tables[0].Rows[i]["Price"] = "0";
            //    }
            //    amount = decimal.Parse(modWTB.Amount.ToString());
            //    price = decimal.Parse(dsBack.Tables[0].DefaultView[0]["Price"].ToString());
            //    money = amount * price;
            //    ds.Tables[0].Rows[i]["Money"] = money;
            //    ds.Tables[0].Rows[i]["Amount"] = amount;
            //    ds.Tables[0].Rows[i]["BreakID"] = modWTB.WorkTicketBreakID;
            //    ds.Tables[0].Rows[i]["OderNum"] = ((DateTime)(dsBack.Tables[0].Rows[0]["Date"])).ToString("yyyyMMdd") + Num.GetNum(dsBack.Tables[0].Rows[0]["Num"].ToString());
            //    modPI.Amount = modWTB.Amount;
            //    modPI.DateTime = dt;
            //    modPI.EmployeeID = EmployeeID;
            //    modPI.WorkticketInfoID = bar;
            //    modPI.MaterielID = int.Parse(dsBack.Tables[0].DefaultView[0]["MaterielID"].ToString());
            //    modPI.Price = price;
            //    modPI.ProductWorkingID = int.Parse(dsBack.Tables[0].DefaultView[0]["ProductWorkingInfoID"].ToString());
            //    modPI.WorkingID = int.Parse(dsBack.Tables[0].DefaultView[0]["WorkingID"].ToString());
            //    modPI.IsSum = false;
            //    ds.Tables[0].Rows[i]["BreakID"] = modPI.BreakID = modWTB.WorkTicketBreakID;
            //    modPI.IsSum = false;
            //    modPI.BoxNum = int.Parse(dsBack.Tables[0].DefaultView[0]["BoxNum"].ToString());
            //    modPI.ColorID = int.Parse(dsBack.Tables[0].DefaultView[0]["ColorID"].ToString());
            //    modPI.SizeID = int.Parse(dsBack.Tables[0].DefaultView[0]["SizeID"].ToString());
            //    modPI.OderNum = ds.Tables[0].Rows[i]["OderNum"].ToString();
            //    ds.Tables[0].Rows[i]["ID"] = bllPI.Add(modPI);
            //    bllWInfo.UpWorkTicketBreak(bar, 2);
            //}
            //else
            //{
            //    XtraMessageBox.Show("错误的条码号码");
            //}
            //gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            //textEdit2.Text = null;
            //textEdit2.Focus();
            //textEdit3.Text = null;

        }
        private void textEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = (int)e.KeyChar;
            string barNum = textEdit2.Text.Trim();

            if (k == 13)
            {
                AddBreakBar();
            }
        }
        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position = 0;
            _brFrist.Enabled = false;
            _brPrv.Enabled = false;
            _brNext.Enabled = true;
            _brLast.Enabled = true;

        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void _barPrv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position -= 1;
            if (bs.Position == 0)
            {
                _brFrist.Enabled = false;
                _brPrv.Enabled = false;
            }
            _brNext.Enabled = true;
            _brLast.Enabled = true;

        }
        /// <summary>
        /// 下一条
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position += 1;
            if (bs.Position == dtMain.Rows.Count - 1)
            {
                _brNext.Enabled = false;
                _brLast.Enabled = false;
            }
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;

        }
        /// <summary>
        /// 尾记录
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.Position = dtMain.Rows.Count - 1;
            _brNext.Enabled = false;
            _brLast.Enabled = false;
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;

        }
        /// <summary>
        /// 新单
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
            _brNext.Enabled = false;
            _brLast.Enabled = false;
        }
        #endregion

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _dataNavigator_PositionChanged(object sender, EventArgs e)
        {
            showView(bs.Position);
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "Fil")
            {
                //cResult r = new cResult();
                //r.TextChanged += new TextChangedHandler(r_TextChanged);
                //Form fr = new ERP.BaseFile.BarEmployeeForm(r, "qwe");
                //fr.ShowDialog();
            }
        }

        void r_TextChanged(string s)
        {
            EmployeeID = int.Parse(s);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            lookUpEdit1.EditValue = EmployeeID;
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int InfoID = 0;
            //string barNum = barEditItem1.EditValue.ToString();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ProduceTaskID", typeof(int));
            //dt.Columns.Add("MainID", typeof(int));
            //dt.Columns.Add("InfoID", typeof(int));
            ////bar = Convert.ToInt32(barNum.ToUpper(), 16);//十六进制
            //SixtyTwoScale sts = barNum;
            //InfoID = (int)(sts.ToInt64());
            ////bar = int.Parse(barNum);//十进制
            //DataSet dsTem = bllWInfo.GetBoolAdd(InfoID);
            //if (dsTem.Tables[0].Rows.Count > 0)
            //{
            //    dt.Rows.Add(bllWInfo.GetTaskID(InfoID));
            //}
            //try
            //{
            //    InfoID = int.Parse(barNum);
            //    dsTem = bllWInfo.GetBoolAdd(InfoID);
            //    if (dsTem.Tables[0].Rows.Count > 0)
            //    {
            //        dt.Rows.Add(bllWInfo.GetTaskID(InfoID),dsTem.Tables[0].Rows[0]["MainID"],InfoID);
            //    }
            //}
            //catch
            //{
            //}
            
            //if (dt.Rows.Count>0)
            //{
            //    Form fr = new Task.TaskForm(dt);
            //    fr.ShowDialog();
            //}
            //else
            //{
            //    XtraMessageBox.Show("未找到相关记录！");
            //}
        }

        private void repositoryItemLookUpEdit1_Popup(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_QueryCloseUp(object sender, CancelEventArgs e)
        {
            dtEmp.DefaultView.RowFilter = "";
        }

        private void lookUpEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
            try
            {
                dtEmp.DefaultView.RowFilter = "IsEnd=0";
            }
            catch { }
        }
    }
}