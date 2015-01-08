using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace Hownet.Pay
{
    public partial class BarTaskCar : DevExpress.XtraEditors.XtraForm
    {
        public BarTaskCar()
        {
            InitializeComponent();
        }
        int TaskID = 0;
        int PWMainID = 0;
        string _matName = string.Empty;
        string _brandName = string.Empty;
        string _num = string.Empty;
        int _materielID = 0;
        prints pri;
        Thread thd1;
        bool _IsEdit = false;
        bool _IsCheckNoEnd = false;
        public BarTaskCar(int TID, int PID, string MatName, string BrandName, string Num,int MaterielID)
            : this()
        {
            TaskID = TID;
            PWMainID = PID;
            _matName = MatName;
            _brandName = BrandName;
            _num = Num;
            _materielID = MaterielID;
         }
        DataTable dtMain = new DataTable();
        DataTable dtPWI = new DataTable();
        DataTable dtWTC = new DataTable();
        DataTable dtOneAmount = new DataTable();
        string blPWI = "Hownet.BLL.ProductWorkingInfo";
        string blWTDC = "Hownet.BLL.WorkTicketIDCard";
        string bllSF = "Hownet.BLL.SysFormula";
        bool t = false;
        int _Count = 0;
        int _UseCount = 0;
        private void StockBackForm_Load(object sender, EventArgs e)
        {
            dtWTC = BasicClass.GetDataSet.GetDS(blWTDC, "GetList", new object[] { "(TaskID=" + TaskID + ")" }).Tables[0];

            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            InData();
            _IsCheckNoEnd = BasicClass.BasicFile.liST[0].IsCheckNoWork;
            DataTable dtTem = BasicClass.GetDataSet.GetDS(bllSF, "GetList", new object[] {"(TypeID=-1)" }).Tables[0];
            if (dtTem.Rows.Count == 0)
            {
                DataRow drr = dtTem.NewRow();
                drr["A"] = 3;
                drr["ID"] = 0;
                drr["TypeID"] = -1;
                for (int i = 0; i < dtTem.Columns.Count; i++)
                {
                    if (dtTem.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        drr[i] = string.Empty;
                    }
                }
                drr["Operator1"] = 0;
                dtTem.Rows.Add(drr);
                BasicClass.GetDataSet.Add(bllSF, dtTem);
            }
            else
            {
                _Count = Convert.ToInt32(dtTem.Rows[0]["Operator1"]);
            }
            _UseCount = Convert.ToInt32(BasicClass.GetDataSet.GetOne(blWTDC, "GetCountRows", null));
            ShowOneAmount(); 
        }

        private void InData()
        {
            dtOneAmount = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicket, "GetOneAmount", new object[] {TaskID }).Tables[0];
            gridControl2.DataSource = dtOneAmount;
        }
        private void ShowOneAmount()
        {
            dtPWI = BasicClass.GetDataSet.GetDS(blPWI, "GetGroup", new object[] { TaskID, Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coOneAmount)) }).Tables[0];
          //  gridView2.FocusedRowHandle
            for (int i = 8; i <gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Visible = false;
            }
            DataTable dtTem = BasicClass.GetDataSet.GetBySql("Select Value From  OtherType Where  (Name='分组显示为部位')");

            string bll = "Hownet.BLL.CaiPian";
            DataTable dtCaiPian = new DataTable();
            bool _IsUserCaiPian = false;
            if (dtTem.Rows.Count > 0)
            {
                _IsUserCaiPian = Convert.ToBoolean(dtTem.Rows[0]["Value"]);
                dtCaiPian = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];  
            }
            if (dtPWI.Rows.Count > 0)
            {
                for (int i = 0; i < dtPWI.Rows.Count; i++)
                {
                    gridView1.Columns.Add();
                    if (!_IsUserCaiPian)
                    {
                        gridView1.Columns[7 + i].Caption = "第" + dtPWI.Rows[i]["GroupBy"].ToString() + "道";
                    }
                    else
                    {
                        try
                        {
                            gridView1.Columns[7 + i].Caption = dtCaiPian.Select("(ID=" + dtPWI.Rows[i]["GroupBy"] + ")")[0]["Name"].ToString();
                        }
                        catch
                        {

                        }
                    }
                    gridView1.Columns[7 + i].FieldName = dtPWI.Rows[i]["GroupBy"].ToString();
                    gridView1.Columns[7 + i].Visible = true;
                    gridView1.Columns[7 + i].VisibleIndex = 7 + i;
                    gridView1.Columns[7 + i].Width = 120;
                }
            }
            
            dtMain = BasicClass.GetDataSet.GetDS(blWTDC, "GetView", new object[] { TaskID, Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coOneAmount)) }).Tables[0];
            DataRow dr = dtMain.NewRow();
            dr["ColorID"] = dr["ColorOneID"] = dr["ColorTwoID"] = dr["SizeID"] = 0;
            dtMain.Rows.Add(dr);
            gridControl1.DataSource = dtMain;
        }
        #region 记录移动
        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null || e.Value.ToString() == "")
                return;
            pri = new prints();
            pri.MaterielName = _matName;
            pri.Brand = _brandName;
            pri.ColorName = gridView1.GetFocusedRowCellDisplayText(_coColorID);
            pri.BoxNum = gridView1.GetFocusedRowCellDisplayText(_coBoxNum);
            pri.BoxAmount = gridView1.GetFocusedRowCellDisplayText(_coAmount);
            pri.SizeName = gridView1.GetFocusedRowCellDisplayText(_coSizeID);
            pri.Num = _num;
            pri.Groups = gridView1.FocusedColumn.Caption;
            pri.CardID = e.Value.ToString();
            DataRow[] drs = dtWTC.Select("(IDCardNo=" + e.Value + ")");//查询该卡是否正在本单使用，主要为误发卡
            if (drs.Length > 0)
            {
                XtraMessageBox.Show("该卡正在本生产单中使用！");
                gridView1.SetFocusedValue(DBNull.Value);
                return;
            }
            //查询是否有非本单正在使用该ID卡
            DataTable dtt = BasicClass.GetDataSet.GetDS(blWTDC, "CheckCard", new object[] { int.Parse(e.Value.ToString()), TaskID }).Tables[0];
            if (dtt.Rows.Count == 0)//没有记录
            {
                if (_UseCount > _Count)
                {
                    gridView1.SetFocusedValue(DBNull.Value);
                    XtraMessageBox.Show("使用ID卡数量已超过购买数量，请咨询软件客服！");
                    return;
                }
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                DataTable dwww = dtWTC.Clone();
                DataRow dr = dwww.NewRow();
                dr["ID"] = 0;
                dr["TicketID"] = gridView1.GetFocusedRowCellValue(_coWorkTicketID);
                dr["IDCardNo"] = e.Value;
                dr["IsEnd"] = false;
                dr["GroupBy"] = dtPWI.Rows[e.Column.ColumnHandle - 8]["GroupBy"];
                dr["TaskID"] = TaskID;
                dr["FishWork"] = false;
                dr["Num"] = _num;
                dr["MaterielName"] = _matName;
                dr["ColorName"] = gridView1.GetFocusedRowCellDisplayText(_coColorID);
                dr["SizeName"] = gridView1.GetFocusedRowCellDisplayText(_coSizeID);
                dr["BoxNum"] = gridView1.GetFocusedRowCellValue(_coBoxNum);
                dr["ColorID"] = gridView1.GetFocusedRowCellValue(_coColorID);
                dr["SizeID"] = gridView1.GetFocusedRowCellValue(_coSizeID);
                dr["MaterielID"] = _materielID;
                dr["ColorOneName"] = gridView1.GetFocusedRowCellDisplayText(_coColorOneID);
                dr["ColorTwoName"] = gridView1.GetFocusedRowCellDisplayText(_coColorTwoID);
                dr["ColorOneID"] = gridView1.GetFocusedRowCellValue(_coColorOneID);
                dr["ColorTwoID"] = gridView1.GetFocusedRowCellValue(_coColorTwoID);
                dwww.Rows.Add(dr);
                dtWTC.Rows.Add(dwww.Rows[0].ItemArray);
                BasicClass.GetDataSet.Add(blWTDC, dwww);
                _UseCount+=1;
                if (!checkBox1.Checked)
                {
                    int a = dtPWI.Rows.Count;
                    if (e.Column.ColumnHandle < (7 + a))
                    {
                        SendKeys.Send("{RIGHT}");
                    }
                    else
                    {
                        if (e.RowHandle < gridView1.RowCount - 2)
                        {
                            SendKeys.Send("{DOWN}");
                            for (int i = 0; i < a - 1; i++)
                            {
                                SendKeys.Send("{LEFT}");
                            }
                        }
                        else
                        {
                            gridView1.OptionsBehavior.Editable = false;
                        }
                    }
                }
                else
                {
                    if (e.RowHandle < gridView1.RowCount - 2)
                    {
                        SendKeys.Send("{DOWN}");
                    }
                    else
                    {
                        gridView1.OptionsBehavior.Editable = false;
                    }
                }
                _IsEdit = true;
            }
            else
            {//有使用，已完工
                if (dtt.Rows.Count > 1)
                {
                    for (int i = 0; i < dtt.Rows.Count - 1; i++)
                    {
                        BasicClass.GetDataSet.ExecSql(blWTDC, "Delete", new object[] { Convert.ToInt32(dtt.Rows[i]["ID"]) });
                    }
                    dtt = BasicClass.GetDataSet.GetDS(blWTDC, "CheckCard", new object[] { int.Parse(e.Value.ToString()), TaskID }).Tables[0];
                }
                if ((bool)(dtt.Rows[0]["FishWork"]) || (bool)(dtt.Rows[0]["IsEnd"]))
                {
                    gridView1.CloseEditor();
                    dtt.Rows[0]["TicketID"] = gridView1.GetFocusedRowCellValue(_coWorkTicketID);
                    dtt.Rows[0]["GroupBy"] = dtPWI.Rows[e.Column.ColumnHandle - 8]["GroupBy"];
                    dtt.Rows[0]["TaskID"] = TaskID;
                    dtt.Rows[0]["FishWork"] = dtt.Rows[0]["IsEnd"] = false;
                    dtt.Rows[0]["Num"] = _num;
                    dtt.Rows[0]["MaterielName"] = _matName;
                    dtt.Rows[0]["ColorName"] = gridView1.GetFocusedRowCellDisplayText(_coColorID);
                    dtt.Rows[0]["SizeName"] = gridView1.GetFocusedRowCellDisplayText(_coSizeID);
                    dtt.Rows[0]["BoxNum"] = gridView1.GetFocusedRowCellValue(_coBoxNum);
                    dtt.Rows[0]["ColorID"] = gridView1.GetFocusedRowCellValue(_coColorID);
                    dtt.Rows[0]["SizeID"] = gridView1.GetFocusedRowCellValue(_coSizeID);
                    dtt.Rows[0]["MaterielID"] = _materielID;
                    dtt.Rows[0]["ColorOneName"] = gridView1.GetFocusedRowCellDisplayText(_coColorOneID);
                    dtt.Rows[0]["ColorTwoName"] = gridView1.GetFocusedRowCellDisplayText(_coColorTwoID);
                    dtt.Rows[0]["ColorOneID"] = gridView1.GetFocusedRowCellValue(_coColorOneID);
                    dtt.Rows[0]["ColorTwoID"] = gridView1.GetFocusedRowCellValue(_coColorTwoID);
                    BasicClass.GetDataSet.UpData(blWTDC, dtt);
                    dtWTC.Rows.Add(dtt.Rows[0].ItemArray);
                    if (!checkBox1.Checked)
                    {
                        int a = dtPWI.Rows.Count;
                        if (e.Column.ColumnHandle < (7 + a))
                        {
                            SendKeys.Send("{RIGHT}");
                        }
                        else
                        {
                            if (e.RowHandle < gridView1.RowCount - 2)
                            {
                                SendKeys.Send("{DOWN}");
                                for (int i = 0; i < a - 1; i++)
                                {
                                    SendKeys.Send("{LEFT}");
                                }
                            }
                            else
                            {
                                gridView1.OptionsBehavior.Editable = false;
                            }
                        }
                    }
                    else
                    {
                        if (e.RowHandle < gridView1.RowCount - 2)
                        {
                            SendKeys.Send("{DOWN}");
                        }
                        else
                        {
                            gridView1.OptionsBehavior.Editable = false;
                        }
                    }
                    _IsEdit = true;
                    return;
                }
                if (!_IsCheckNoEnd)
                {
                    gridView1.CloseEditor();
                    dtt.Rows[0]["TicketID"] = gridView1.GetFocusedRowCellValue(_coWorkTicketID);
                    dtt.Rows[0]["GroupBy"] = dtPWI.Rows[e.Column.ColumnHandle - 8]["GroupBy"];
                    dtt.Rows[0]["TaskID"] = TaskID;
                    dtt.Rows[0]["FishWork"] = dtt.Rows[0]["IsEnd"] = false;
                    dtt.Rows[0]["Num"] = _num;
                    dtt.Rows[0]["MaterielName"] = _matName;
                    dtt.Rows[0]["ColorName"] = gridView1.GetFocusedRowCellDisplayText(_coColorID);
                    dtt.Rows[0]["SizeName"] = gridView1.GetFocusedRowCellDisplayText(_coSizeID);
                    dtt.Rows[0]["BoxNum"] = gridView1.GetFocusedRowCellValue(_coBoxNum);
                    dtt.Rows[0]["ColorID"] = gridView1.GetFocusedRowCellValue(_coColorID);
                    dtt.Rows[0]["SizeID"] = gridView1.GetFocusedRowCellValue(_coSizeID);
                    dtt.Rows[0]["MaterielID"] = _materielID;
                    dtt.Rows[0]["ColorOneName"] = gridView1.GetFocusedRowCellDisplayText(_coColorOneID);
                    dtt.Rows[0]["ColorTwoName"] = gridView1.GetFocusedRowCellDisplayText(_coColorTwoID);
                    dtt.Rows[0]["ColorOneID"] = gridView1.GetFocusedRowCellValue(_coColorOneID);
                    dtt.Rows[0]["ColorTwoID"] = gridView1.GetFocusedRowCellValue(_coColorTwoID);
                    BasicClass.GetDataSet.UpData(blWTDC, dtt);
                    dtWTC.Rows.Add(dtt.Rows[0].ItemArray);
                    _IsEdit = true;
                    if (!checkBox1.Checked)
                    {
                        int a = dtPWI.Rows.Count;
                        if (e.Column.ColumnHandle < (7 + a))
                        {
                            SendKeys.Send("{RIGHT}");
                        }
                        else
                        {
                            if (e.RowHandle < gridView1.RowCount - 2)
                            {
                                SendKeys.Send("{DOWN}");
                                for (int i = 0; i < a - 1; i++)
                                {
                                    SendKeys.Send("{LEFT}");
                                }
                            }
                            else
                            {
                                gridView1.OptionsBehavior.Editable = (gridView1.GetFocusedValue().ToString() == "");
                            }
                        }
                    }
                    else
                    {
                        if (e.RowHandle < gridView1.RowCount - 2)
                        {
                            SendKeys.Send("{DOWN}");
                        }
                        else
                        {
                            gridView1.OptionsBehavior.Editable = false;
                        }
                    }
                    return;
                }
                //有使用，未完工
                if (DialogResult.Yes == XtraMessageBox.Show("该卡号在使用中,有工序未完成，是否回收并使用该卡号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否回收并使用该卡号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        gridView1.CloseEditor();
                        dtt.Rows[0]["TicketID"] = gridView1.GetFocusedRowCellValue(_coWorkTicketID);
                        dtt.Rows[0]["GroupBy"] = dtPWI.Rows[e.Column.ColumnHandle - 8]["GroupBy"];
                        dtt.Rows[0]["TaskID"] = TaskID;
                        dtt.Rows[0]["FishWork"] = dtt.Rows[0]["IsEnd"] = false;
                        dtt.Rows[0]["Num"] = _num;
                        dtt.Rows[0]["MaterielName"] = _matName;
                        dtt.Rows[0]["ColorName"] = gridView1.GetFocusedRowCellDisplayText(_coColorID);
                        dtt.Rows[0]["SizeName"] = gridView1.GetFocusedRowCellDisplayText(_coSizeID);
                        dtt.Rows[0]["BoxNum"] = gridView1.GetFocusedRowCellValue(_coBoxNum);
                        dtt.Rows[0]["ColorID"] = gridView1.GetFocusedRowCellValue(_coColorID);
                        dtt.Rows[0]["SizeID"] = gridView1.GetFocusedRowCellValue(_coSizeID);
                        dtt.Rows[0]["MaterielID"] = _materielID;
                        dtt.Rows[0]["ColorOneName"] = gridView1.GetFocusedRowCellDisplayText(_coColorOneID);
                        dtt.Rows[0]["ColorTwoName"] = gridView1.GetFocusedRowCellDisplayText(_coColorTwoID);
                        dtt.Rows[0]["ColorOneID"] = gridView1.GetFocusedRowCellValue(_coColorOneID);
                        dtt.Rows[0]["ColorTwoID"] = gridView1.GetFocusedRowCellValue(_coColorTwoID);
                        BasicClass.GetDataSet.UpData(blWTDC, dtt);
                        dtWTC.Rows.Add(dtt.Rows[0].ItemArray);
                        _IsEdit = true;
                        if (!checkBox1.Checked)
                        {
                            int a = dtPWI.Rows.Count;
                            if (e.Column.ColumnHandle < (7 + a))
                            {
                                SendKeys.Send("{RIGHT}");
                            }
                            else
                            {
                                if (e.RowHandle < gridView1.RowCount - 2)
                                {
                                    SendKeys.Send("{DOWN}");
                                    for (int i = 0; i < a - 1; i++)
                                    {
                                        SendKeys.Send("{LEFT}");
                                    }
                                }
                                else
                                {
                                    gridView1.OptionsBehavior.Editable = (gridView1.GetFocusedValue().ToString() == "");
                                }
                            }
                        }
                        else
                        {
                            if (e.RowHandle < gridView1.RowCount - 2)
                            {
                                SendKeys.Send("{DOWN}");
                            }
                            else
                            {
                                gridView1.OptionsBehavior.Editable = false;
                            }
                        }
                        return;
                    }
                }
                gridView1.SetFocusedValue(DBNull.Value);
                return;
            }
        }
        private bool CheckUse(int CardID, int r, int c)
        {
            t = false;
            for (int i = 0; i < gridView1.RowCount - 1; i++)
            {
                for (int j = 7; j < 7 + dtPWI.Rows.Count; j++)
                {
                    bool f = (i != r && j != c);
                    if ((!(i == r || j == c)) && gridView1.GetRowCellValue(i, gridView1.Columns[j]).ToString() != "")
                    {
                        object obj = gridView1.GetRowCellValue(i, gridView1.Columns[j]);
                        if (int.Parse(gridView1.GetRowCellValue(i, gridView1.Columns[j]).ToString()) == CardID)
                        {
                            t = true;
                            break;
                        }
                    }
                }
                if (t)
                    break;
            }
            return t;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedValue() != null)
                gridView1.OptionsBehavior.Editable = (gridView1.GetFocusedValue().ToString() == "") && e.FocusedRowHandle < gridView1.RowCount - 1;
        }

        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (gridView1.GetFocusedValue() != null)
                gridView1.OptionsBehavior.Editable = (gridView1.GetFocusedValue().ToString() == "");
        }
        private void PrintLabes()
        {
            try
            {
                //MyPrintDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
                //this.MyPrintDocument.Print();//直接打印
            }
            catch { }
        }
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //文字右对齐
            StringFormat sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            //划虚线
            Pen ftqGoal = new Pen(Color.Black, 1);
            ftqGoal.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //StringAlignment.Center;或者：StringAlignment.Far;或者：StringAlignment.Near; 
            Brush brush = new SolidBrush(Color.Black);//画刷 
            Brush brred = new SolidBrush(Color.Red);//
            Font titleFont = new Font("黑体", 24, FontStyle.Bold);//标题字体 
            Font font = new Font("Consolas", 12, FontStyle.Regular);//数字0字有斜线的字体:WST_Ital,  01 DigitGraphics , 00 Starmap Truetype,Consolas,
            //Font font = new Font("WST_Engl", 8);//正文字体 
            Font headerFont = new Font("黑体", 12, FontStyle.Bold);//列名标题 
            Font footerFont = new Font("Arial", 8);//页脚显示页数的字体 
            Font upLineFont = new Font("Arial", 9, FontStyle.Bold);//当header分两行显示的时候，上行显示的字体。 
            Font underLineFont = new Font("Arial", 8);//当header分两行显示的时候，下行显示的字

            e.Graphics.DrawString(pri.Num, font, brush, 5, 10);
            e.Graphics.DrawString(pri.MaterielName + "  " + pri.ColorName, font, brush, 5, 30);
            e.Graphics.DrawString(pri.SizeName + "  " + pri.BoxAmount + "件", font, brush, 5, 50);
            e.Graphics.DrawString("第" + pri.BoxNum + "箱" + "  " + pri.Groups, font, brush, 5, 70);
        }
        private class prints
        {
            public prints()
            { }
            public string MaterielName
            {
                set;
                get;
            }
            public string ColorName
            {
                set;
                get;
            }
            public string SizeName
            {
                set;
                get;
            }
            public string BoxNum
            {
                set;
                get;
            }
            public string BoxAmount
            {
                set;
                get;
            }
            public string Groups
            {
                set;
                get;
            }
            public string Num
            {
                set;
                get;
            }
            public string CardID
            {
                set;
                get;
            }
            public string Brand
            {
                set;
                get;
            }
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }
        //修改
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedColumn.ColumnHandle > 7 && gridView1.GetFocusedValue() != null && gridView1.GetFocusedValue().ToString().Trim() != string.Empty)
            {
                BasicClass.cResult r = new BasicClass.cResult();
                r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                //int TicketID, int IDCardNo, int GroupBy, int TaskID
                Form fr = new frEditCard(r, Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coWorkTicketID)), Convert.ToInt32(gridView1.GetFocusedValue()), Convert.ToInt32(dtPWI.Rows[gridView1.FocusedColumn.ColumnHandle - 8]["GroupBy"]), TaskID, dtWTC);
                fr.ShowDialog();
            }
        }

        void r_TextChanged(string s)
        {
            this.gridView1.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            gridView1.SetFocusedValue(s);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            _IsEdit = true;
        }
        //打印
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _colorid = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coColorID));
            int _sizeid = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coSizeID));
            int _boxid = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coWorkTicketID));
            int _groupby = Convert.ToInt32(dtPWI.Rows[gridView1.FocusedColumn.ColumnHandle - 8]["GroupBy"]);
            int _ticketid = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coWorkTicketID));
            int _oneAmount = Convert.ToInt32(gridView2.GetFocusedRowCellValue(_coOneAmount));
            Form fr = new frPrintCard(_colorid, _sizeid, _boxid, _groupby, TaskID, _ticketid, _num,_oneAmount);
            fr.ShowDialog();
        }

        private void BarTaskCar_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if (_IsEdit)
            BasicClass.GetDataSet.SetDataTable();
            if (_IsEdit)
            {
                BasicClass.GetDataSet.ExecSql(blWTDC, "DelMore", null);
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowOneAmount();
        }
    }
}