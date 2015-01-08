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
    public partial class frSpec : DevExpress.XtraEditors.XtraForm
    {
        public frSpec()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public frSpec(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _id = ID;
        }
        private DataTable dt = new DataTable();
        private string bll = "Hownet.BLL.Specification";
        private bool _isShowPopu = false;
        private int _focuHand = 0;
        private void frColor_Load(object sender, EventArgs e)
        {
            this.Text = BasicClass.DefaultFormText.TEXT002;
            dt = BasicClass.BaseTableClass.dtSpec;// BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            dt.DefaultView.RowFilter = "IsEnd=0";
            gridControl1.DataSource = dt.DefaultView;
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barAdd.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                barButtonItem1.Enabled = _barEdit.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
            splitContainer1.Panel1Collapsed = true;
        }
        private void _barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //BasicClass.cResult cr = new BasicClass.cResult();
            //cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
            //Form fr = new frSpecOne(cr, 0, dt);

            //fr.ShowDialog();
            splitContainer1.Panel1Collapsed = false;
            _focuHand = -1;
            _teName.EditValue = _teRemark.EditValue = string.Empty;
            _teName.Focus();
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
            //BasicClass.cResult cr = new BasicClass.cResult();
            //cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
            //Form fr = new frSpecOne(cr, int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()), dt);

            //fr.ShowDialog();
            _focuHand = gridView1.FocusedRowHandle;
            _teName.EditValue = gridView1.GetFocusedRowCellValue(_coName);
            _teRemark.EditValue = gridView1.GetFocusedRowCellValue(_coRemark);
            splitContainer1.Panel1Collapsed = false;
            _teName.Focus();
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                int rID = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
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
                this.Close();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                _focuHand = e.FocusedRowHandle;
                _teName.EditValue = gridView1.GetFocusedRowCellValue(_coName);
                _teRemark.EditValue = gridView1.GetFocusedRowCellValue(_coRemark);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_focuHand < 0)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = 0;
                dr["A"] = 3;
                dr["Name"] = _teName.EditValue;
                dr["Remark"] = _teRemark.EditValue;
                dr["IsEnd"] = 0;
                DataTable dtTem = dt.Clone();
                dtTem.Rows.Add(dr.ItemArray);
                dr["ID"] = BasicClass.GetDataSet.Add(bll, dtTem);
                dt.Rows.Add(dr);
            }
            else
            {
                gridView1.SetRowCellValue(_focuHand, _coName, _teName.EditValue);
                gridView1.SetRowCellValue(_focuHand, _coRemark, _teRemark.EditValue);
                DataRow dr = dt.NewRow();
                dr["ID"] = Convert.ToInt32(gridView1.GetRowCellValue(_focuHand, _coID));
                dr["A"] = 3;
                dr["Name"] = _teName.EditValue;
                dr["Remark"] = _teRemark.EditValue;
                dr["IsEnd"] = 0;
                DataTable dtTem = dt.Clone();
                dtTem.Rows.Add(dr.ItemArray);
                BasicClass.GetDataSet.UpData(bll, dtTem);
            }
            _teName.EditValue = _teRemark.EditValue = string.Empty;
            _teName.Focus();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _teName.EditValue = _teRemark.EditValue = string.Empty;
            _teName.Focus();
            splitContainer1.Panel1Collapsed = true;
        }
    }
}