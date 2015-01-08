using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hownet.BaseContranl;

namespace Hownet.BaseForm
{
    public partial class frWorking : DevExpress.XtraEditors.XtraForm
    {
        public frWorking()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public frWorking(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _id = ID;
        }
        private DataTable dt = new DataTable();
        private string bll = "Hownet.BLL.Working";
        private bool _isShowPopu = false;
        private void frColor_Load(object sender, EventArgs e)
        {
            this.Text = BasicClass.DefaultFormText.TEXT006;
            _coWorkTypeID.ColumnEdit = BaseForm.RepositoryItem._reWorkType;
            radioGroup1.SelectedIndex = -1;
            dt = BasicClass.BaseTableClass.dtWorking;// BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(WorkTypeID>-1)" }).Tables[0];
            radioGroup1.SelectedIndex = 0;
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
            Form fr = new frWorkingOne(cr, 0, dt);
            
            fr.ShowDialog();
        }

        void cr_RowChanged(DataTable dt)
        {
            
        }


        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                BasicClass.cResult cr = new BasicClass.cResult();
                cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
                Form fr = new frWorkingOne(cr, int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()),dt);
                
                fr.ShowDialog();
            }
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //检测该记录是否被使用

            if (gridView1.FocusedRowHandle > -1)
            {
                int rID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                if (radioGroup1.SelectedIndex == 0)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
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
                else if (radioGroup1.SelectedIndex == 1)
                {
                    if (!Convert.ToBoolean(BasicClass.GetDataSet.GetOne(bll, "CheckCanDelete", new object[] { rID })))
                    {
                        XtraMessageBox.Show(" 该工序已被使用，不能彻底删除！");
                        return;
                    }
                    if (DialogResult.Yes == XtraMessageBox.Show("删除后将不能恢复，是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                    {
                        BasicClass.GetDataSet.ExecSql(bll, "Delete", new object[] { rID });
                        DataRow[] drs = dt.Select("(ID=" + rID + ")");
                        if (drs.Length > 0)
                        {
                            drs[0].Delete();
                        }
                        dt.AcceptChanges();
                    }
                }
            }
        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void _barExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
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
                    XtraMessageBox.Show("只能選擇使用中的顏色！");
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
            _barDel.Enabled = false;//_isShowPopu = 
            barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (radioGroup1.SelectedIndex == 0)
            {
                dt.DefaultView.RowFilter = "IsEnd=0";
                _barDel.Enabled = true;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                dt.DefaultView.RowFilter = "IsEnd>0";
                _barDel.Enabled = true;
//                _isShowPopu = true;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
            //if (_isShowPopu)
            //{
                if (e.Button == MouseButtons.Right)
                    DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
            //}
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                Form fr = new frNotWorkAmount(Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)), gridView1.GetFocusedRowCellDisplayText(_coName));
                fr.ShowDialog();
            }
        }
    }
}