using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.Utils;
using Hownet.BaseContranl;


namespace Hownet.Pay
{
    public partial class frAttendanceRecords : Form
    {
        public frAttendanceRecords()
        {
            InitializeComponent();
        }
        DataTable dtAR;
        string[] TimeOne, TimeTwo, TimeThree, TimeFour, TimeFive, TimeSix,vals;
        int iTimeOne, iTimeTwo, iTimeThree, iTimeFour, iTimeFive, iTimeSix, ivals;
        string val;
        DataTable dt = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            dtAR = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "考勤设置" }).Tables[0];
            DataTable dtEmp = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetView", new object[] { }).Tables[0];
            dtEmp.Rows.Add(0, "", "", 0, 0);
            dtEmp.DefaultView.Sort = "ID";
            lookUpEdit1.Properties.DataSource = dtEmp.DefaultView;
           // _ldDateOne.val = _ldDateTwo.val = DateTime.Today;
            _coEmployeeID.ColumnEdit = _cosEmployeeID.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
            _cosDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            lookUpEdit1.EditValue = 0;
            TimeOne = dtAR.Select("(Name='One')")[0]["Value"].ToString().Split(':');
            TimeTwo = dtAR.Select("(Name='lOne')")[0]["Value"].ToString().Split(':');
            TimeThree = dtAR.Select("(Name='Two')")[0]["Value"].ToString().Split(':');
            TimeFour = dtAR.Select("(Name='lTwo')")[0]["Value"].ToString().Split(':');
            TimeFive = dtAR.Select("(Name='Three')")[0]["Value"].ToString().Split(':');
            TimeSix = dtAR.Select("(Name='lThree')")[0]["Value"].ToString().Split(':');
            //iTimeOne = Array.ConvertAll<string, int>(TimeOne, delegate(string s) { return int.Parse(s); });
            iTimeOne = int.Parse(TimeOne[0] + TimeOne[1]);
            iTimeTwo = int.Parse(TimeTwo[0] + TimeTwo[1]);// Array.ConvertAll<string, int>(TimeTwo, delegate(string s) { return int.Parse(s); });
            iTimeThree = int.Parse(TimeThree[0] + TimeThree[1]);// Array.ConvertAll<string, int>(TimeThree, delegate(string s) { return int.Parse(s); });
            iTimeFour = int.Parse(TimeFour[0] + TimeFour[1]);// Array.ConvertAll<string, int>(TimeFour, delegate(string s) { return int.Parse(s); });
            iTimeFive = int.Parse(TimeFive[0] + TimeFive[1]);// Array.ConvertAll<string, int>(TimeFive, delegate(string s) { return int.Parse(s); });
            iTimeSix = int.Parse(TimeSix[0] + TimeSix[1]);// Array.ConvertAll<string, int>(TimeSix, delegate(string s) { return int.Parse(s); });
            _ldDateTwo.val = BasicClass.GetDataSet.GetDateTime().Date;
            _ldDateOne.val = BasicClass.GetDataSet.GetDateTime().Date.AddDays(-30);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }
        private void GetList()
        {
            DateTime dt1 = ((DateTime)(_ldDateOne.val)).AddSeconds(-1);
            DateTime dt2 = ((DateTime)(_ldDateTwo.val)).AddDays(1);
            string strWhere = "(DateDay >'" + dt1 + "') And (DateDay <'" + dt2 + "')";
            if (lookUpEdit1.EditValue != null && int.Parse(lookUpEdit1.EditValue.ToString()) > 0)
                strWhere = strWhere + " And (EmployeeID=" + int.Parse(lookUpEdit1.EditValue.ToString()) + ")";
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAttendanceRecords, "GetList", new object[] { strWhere }).Tables[0];
            gridControl1.DataSource = dt;
            gridControl2.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAttendanceRecords, "GetSumRecords", new object[] { dt1, dt2 }).Tables[0];

        }
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column == _coTimeOne)
            //{
            //    val=gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString().Trim();
            //    if (val == string.Empty)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        vals = val.Split(':');
            //        ivals = int.Parse(vals[0] + vals[1]);// Array.ConvertAll<string, int>(vals, delegate(string s) { return int.Parse(s); });
            //        if (ivals>iTimeOne)
            //            e.Appearance.BackColor = Color.Red;
            //    }
            //}
            //else if (e.Column == _coTimeTwo)
            //{
            //    val = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString().Trim();
            //    if (val == string.Empty)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        vals = val.Split(':');
            //        ivals = int.Parse(vals[0] + vals[1]);// Array.ConvertAll<string, int>(vals, delegate(string s) { return int.Parse(s); });
            //        if (ivals < iTimeTwo)
            //            e.Appearance.BackColor = Color.Red;
            //    }
            //}
            //if (e.Column == _coTimeThree)
            //{
            //    val = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString().Trim();
            //    if (val == string.Empty)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        vals = val.Split(':');
            //        ivals = int.Parse(vals[0] + vals[1]);// Array.ConvertAll<string, int>(vals, delegate(string s) { return int.Parse(s); });
            //        if (ivals > iTimeThree)
            //            e.Appearance.BackColor = Color.Red;
            //    }
            //}
            //else if (e.Column == _coTimeFour)
            //{
            //    val = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString().Trim();
            //    if (val == string.Empty)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        vals = val.Split(':');
            //        ivals = int.Parse(vals[0] + vals[1]);// Array.ConvertAll<string, int>(vals, delegate(string s) { return int.Parse(s); });
            //        if (ivals < iTimeFour)
            //            e.Appearance.BackColor = Color.Red;
            //    }
            //}
            //if (e.Column == _coTimeFive)
            //{
            //    val = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString().Trim();
            //    if (val == string.Empty)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        vals = val.Split(':');
            //        ivals = int.Parse(vals[0] + vals[1]);// Array.ConvertAll<string, int>(vals, delegate(string s) { return int.Parse(s); });
            //        if (ivals > iTimeFive)
            //            e.Appearance.BackColor = Color.Red;
            //    }
            //}
            //else if (e.Column == _coTimeSix)
            //{
            //    val = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString().Trim();
            //    if (val == string.Empty)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        vals = val.Split(':');
            //        ivals = int.Parse(vals[0] + vals[1]);// Array.ConvertAll<string, int>(vals, delegate(string s) { return int.Parse(s); });
            //        if (ivals < iTimeSix)
            //            e.Appearance.BackColor = Color.Red;
            //    }
            //}
        }
        /// <summary>
        /// 设置通宵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToDateTime(gridView1.GetFocusedRowCellValue(_coDateDay)).Date == BasicClass.GetDataSet.GetDateTime().Date)
            {
                if (!Convert.ToBoolean(gridView1.GetFocusedRowCellValue(_coIsTongXiao)))
                {
                    gridView1.SetFocusedRowCellValue(_coIsTongXiao, true);
                    gridView1.SetFocusedRowCellValue(_coA, 2);
                }
            }
        }
        //保存
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            int a = 0;
            DataTable dtt = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a = Convert.ToInt32(dt.Rows[i]["A"]);
                if (a > 1)
                {
                    dtt = dt.Clone();
                    dtt.Rows.Add(dt.Rows[i].ItemArray);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllAttendanceRecords, dtt);
                    dt.Rows[i]["A"] = 1;
                }
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

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToDateTime(gridView1.GetFocusedRowCellValue(_coDateDay)).Date == BasicClass.GetDataSet.GetDateTime().Date)
            {
                if (!Convert.ToBoolean(gridView1.GetFocusedRowCellValue(_coIsTongXiao)))
                {
                    gridView1.SetFocusedRowCellValue(_coIsTongXiao, false);
                    gridView1.SetFocusedRowCellValue(_coA, 2);
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                advBandedGridView1.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
        }

        private void advBandedGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu2(advBandedGridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu2(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult r = new BasicClass.cResult();
            r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
            DateTime ddt = Convert.ToDateTime(gridView1.GetFocusedRowCellValue(_coDateDay));
            if (gridView1.GetFocusedValue() != null&&gridView1.GetFocusedValue()!=DBNull.Value)
                ddt = Convert.ToDateTime(gridView1.GetFocusedValue());
            Form fr = new frEditTime(r, ddt);
            fr.ShowDialog();
        }

        void r_TextChanged(string s)
        {
            DataTable dddt = dt.Clone();
            dddt.Rows.Add(dt.Select("(ID=" + gridView1.GetFocusedRowCellValue(_coID) + ")")[0].ItemArray);
            if (s == string.Empty)
            {
                gridView1.SetFocusedValue(DBNull.Value);
                dddt.Rows[0][gridView1.FocusedColumn.FieldName] = DBNull.Value;
            }
            else
            {
                gridView1.SetFocusedValue(Convert.ToDateTime(s));
                DateTime time=Convert.ToDateTime(s);
                dddt.Rows[0][gridView1.FocusedColumn.FieldName] = time;
            }
            BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllAttendanceRecords, dddt);
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllAttendanceRecords, "CaicMinute", new object[] { Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) });
            GetList();
        }
    }
}
