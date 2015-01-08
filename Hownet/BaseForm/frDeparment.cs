using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frDeparment : DevExpress.XtraEditors.XtraForm
    {
        public frDeparment()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public frDeparment(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _id = ID;
        }
        private DataTable dt = new DataTable();
        private DataTable dtJobs = new DataTable();
        private string bll = "Hownet.BLL.Deparment";
        private string bllJobs = "Hownet.BLL.DepartmentJobs";
        private bool _isShowPopu = false;
        private void frColor_Load(object sender, EventArgs e)
        {
            this.Text = "部门列表";
            _coTypeID.ColumnEdit = RepositoryItem._reDepartmentType;
            dt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ParentID=0)" }).Tables[0];
            dtJobs = BasicClass.GetDataSet.GetDS(bllJobs, "GetAllList", null).Tables[0];
            gridControl1.DataSource = dt;

            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barAdd.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                barButtonItem1.Enabled = _barEdit.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
        }
        private void _barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
            Form fr = new frDeparmentOne(cr, 0, dt);
            fr.ShowDialog();
        }

        void cr_RowChanged(DataTable dtt)
        {
            dt = dtt;
            gridControl1.DataSource = dt.DefaultView;
        }

        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
                return;
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
            Form fr = new frDeparmentOne(cr, int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()), dt);
            fr.ShowDialog();
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool t = gridControl1.IsFocused;
            bool f = gridControl2.IsFocused;
            if (t&& gridView1.FocusedRowHandle > -1)
            {
                
                int rID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除該部門信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    DataRow[] drs = dt.Select("(ID=" + rID + ")");
                    if (drs.Length > 0)
                    {
                        DataTable dttt = dt.Clone();
                        dttt.Rows.Add(drs[0].ItemArray);
                        dttt.Rows[0]["IsEnd"] = drs[0]["IsEnd"] = 1;
                        BasicClass.GetDataSet.UpData(bll, dttt);
                    }
                    dt.DefaultView.RowFilter = "IsEnd=0";
                }
            }
            else if (f && gridView2.FocusedRowHandle > -1)
            {

                int rID = int.Parse(gridView2.GetFocusedRowCellValue(_coJobsID).ToString());
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除該職位信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    DataRow[] drs = dtJobs.Select("(ID=" + rID + ")");
                    if (drs.Length > 0)
                    {
                        DataTable dttt = dtJobs.Clone();
                        dttt.Rows.Add(drs[0].ItemArray);
                        dttt.Rows[0]["IsEnd"] = drs[0]["IsEnd"] = 1;
                        BasicClass.GetDataSet.UpData(bllJobs, dttt);
                    }
                    dtJobs.DefaultView.RowFilter = "IsEnd=0";
                }
            }
        }

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frColor_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetID();
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                if (radioGroup1.SelectedIndex == 0)
                {
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("只能選擇使用中的部門名稱！");
                    return;
                }
            }
        }
        private void GetID()
        {
            if (_id != 0)
            {
                if (gridView1.FocusedRowHandle > -1)
                {
                    int _mID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                    if (_mID > 0)
                    {
                        r.ChangeText(_mID.ToString());
                    }
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIsEnd();
        }
        private void CheckIsEnd()
        {
            _barDel.Enabled = _isShowPopu = false;
            if (radioGroup1.SelectedIndex == 0)
            {
                dt.DefaultView.RowFilter = "IsEnd=0";
                _barDel.Enabled = true;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                dt.DefaultView.RowFilter = "IsEnd>0";
                _isShowPopu = true;
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                dt.DefaultView.RowFilter = "";
            }
            gridControl1.DataSource = dt.DefaultView;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                int _TemID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                DataRow[] drs = dt.Select("(ID=" + _TemID + ")");
                DataTable dttt = dt.Clone();
                dttt.Rows.Add(drs[0].ItemArray);
                dttt.Rows[0]["IsEnd"] = 0;
                BasicClass.GetDataSet.UpData(bll, dttt);
                drs[0]["IsEnd"] = 0;
                dt.AcceptChanges();
            }
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isShowPopu)
            {
                if (e.Button == MouseButtons.Right)
                    DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
            }
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }
        //添加
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                BasicClass.cResult crr = new BasicClass.cResult();
                crr.RowChanged += new BasicClass.RowChangedHandler(crr_RowChanged);
                Form fr = new frDeparmentJobOne(crr, 0, dtJobs, gridView1.GetFocusedRowCellDisplayText(_coName), int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()));
                fr.ShowDialog();
            }
        }

        void crr_RowChanged(DataTable dt)
        {
           
        }
        //編輯
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                BasicClass.cResult crr = new BasicClass.cResult();
                crr.RowChanged += new BasicClass.RowChangedHandler(crr_RowChanged);
                Form fr = new frDeparmentJobOne(crr, int.Parse(gridView2.GetFocusedRowCellValue(_coJobsID).ToString()), dtJobs, gridView1.GetFocusedRowCellDisplayText(_coName), int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()));
                fr.ShowDialog();
            }
        }
        private void gridView2_MouseUp(object sender, MouseEventArgs e)
        {
            //if (_isShowPopu)
            //{
                if (e.Button == MouseButtons.Right)
                    DoShowMenu2(gridView2.CalcHitInfo(new Point(e.X, e.Y)));
            //}
        }
        void DoShowMenu2(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow|| hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                dtJobs.DefaultView.RowFilter = "(DepartmentID=" + int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()) + ") And (IsEnd=0)";
                gridControl2.DataSource = dtJobs.DefaultView;
                gridControl3.DataSource = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ParentID=" + gridView1.GetFocusedRowCellValue(_coID) + ")" }).Tables[0];
            }
            else
            {
                gridControl2.DataSource = null;
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtEm = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(BedID=" + gridView3.GetFocusedRowCellValue(gridColumn5) + ")" }).Tables[0];
            if(dtEm.Rows.Count==0)
                dtEm = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(TableID=" + gridView3.GetFocusedRowCellValue(gridColumn5) + ")" }).Tables[0];
            StringBuilder strEm = new StringBuilder();
            if (dtEm.Rows.Count > 0)
            {
                strEm.Append("共有 " + dtEm.Rows.Count.ToString() + " 位员工：");
                strEm.Append("\r\n");
                for (int i = 0; i < dtEm.Rows.Count; i++)
                {
                    strEm.Append((i + 1).ToString());
                    strEm.Append("、");
                    strEm.Append(dtEm.Rows[i]["Name"].ToString());
                    strEm.Append("\r\n");
                }
            }
            else
            {
                strEm.Append("没有分配员工！");
            }
            XtraMessageBox.Show(strEm.ToString());
        }

        private void gridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu3(gridView3.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu3(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu3.ShowPopup(Control.MousePosition);
            }
        }
    }
}