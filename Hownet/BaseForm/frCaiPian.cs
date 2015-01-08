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
    public partial class frCaiPian : DevExpress.XtraEditors.XtraForm
    {
        public frCaiPian()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public frCaiPian(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _id = ID;
        }
        private BindingSource bs = new BindingSource();
        private DataTable dt = new DataTable();
        private bool _isEdit = false;
        private int _rowCount = 0;
        private object _oldValue = null;
        private string bll = "Hownet.BLL.CaiPian";
        private void frColor_Load(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            gridControl1.DataSource = bs.DataSource = dt;
            _isEdit = false;
            _rowCount = gridView1.RowCount;
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barAdd.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _barEdit.Enabled = _barEdit.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
        }
        private bool Save()
        {
            bool t = true;
            try
            {
                gridView1.CloseEditor();
                DataTable dtt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int a = int.Parse(dt.Rows[i]["A"].ToString());
                    if (a > 1)
                    {
                        dtt.Clear();
                        DataRow dr = dtt.NewRow();
                        dr.ItemArray = dt.Rows[i].ItemArray;
                        dtt.Rows.Add(dr);
                        if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData(bll, dtt);
                        }
                        else if (a == 3)
                        {
                            dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bll,  dtt);
                        }
                        dt.Rows[i]["A"] = 1;
                    }
                }
                _isEdit = false;
            }
            catch
            {
                t = false;
            }

            return t;
        }
        private void _barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
            Form fr = new frCaiPianOne(cr, 0, dt);

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
                Form fr = new frCaiPianOne(cr, int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()), dt);

                fr.ShowDialog();
            }
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                int rID=int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                //if (rID > 0)
                //{
                //    if (bool.Parse(gridView1.GetFocusedRowCellValue(_coIsUse).ToString()))
                //    {
                //        XtraMessageBox.Show("该明细记录已被使用，不能删除！");
                //        return;
                //    }
                //}
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (rID > 0)
                    {
                        if (rID > 0)
                        {
                            DataRow[] drs = dt.Select("(ID=" + rID + ")");
                            if (drs.Length > 0)
                            {
                                DataTable dttt = dt.Clone();
                                dttt.Rows.Add(drs[0]);
                                dttt.Rows[0]["IsEnd"] = 1;
                                BasicClass.GetDataSet.UpData(bll, dttt);
                            }
                        }
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                }
            }
        }

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            if(  Save())
              XtraMessageBox.Show("保存成功！");
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.RowHandle > -1)
            {
                if (e.Column == _coName)
                {
                    if (e.Value.ToString().Trim() == "")
                    {
                        XtraMessageBox.Show("裁片名不能为空！");
                        gridView1.SetFocusedValue(_oldValue);
                        return;
                    }
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (i != e.RowHandle && e.Value.ToString().Trim() == gridView1.GetRowCellDisplayText(i, _coName))
                        {
                            XtraMessageBox.Show("裁片名重复！");
                            gridView1.SetFocusedValue(_oldValue);
                            return;
                        }
                    }
                }
                if (e.Column != _coA && int.Parse(gridView1.GetFocusedRowCellValue(_coA).ToString()) == 1)
                    gridView1.SetFocusedRowCellValue(_coA, 2);
                _isEdit = true;
            }
            else
            {
                if(e.Column!=_coSn&& gridView1.GetFocusedRowCellValue(_coSn)==null||gridView1.GetFocusedRowCellValue(_coSn).ToString()=="")
                {
                    try
                    {
                        int sn = int.Parse(dt.Rows[dt.Rows.Count-1]["Sn"].ToString()) + 1;
                        gridView1.SetFocusedRowCellValue(_coSn, sn.ToString().PadLeft(3, '0'));
                    }
                    catch
                    {
                        gridView1.SetFocusedRowCellValue(_coSn,"001");
                    }
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < 0);
            if (e.FocusedRowHandle < 0 && gridView1.RowCount > 0)
                gridView1.AddNewRow();
            else
                _oldValue = gridView1.GetFocusedValue();

        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (_rowCount < gridView1.RowCount)
            {
                int r = gridView1.RowCount - 1;
                string _newValue = dt.Rows[r]["Name"].ToString().Trim();
                if (_newValue == "")
                {
                    gridView1.DeleteRow(r);
                    dt.AcceptChanges();
                    return;
                }
                for (int i = 0; i < r; i++)
                {
                    if (_newValue == dt.Rows[i]["Name"].ToString().Trim())
                    {
                        XtraMessageBox.Show("裁片名重复！");
                        gridView1.DeleteRow(r);
                        dt.AcceptChanges();
                        return;
                    }
                }
                _isEdit = true;
                _rowCount = gridView1.RowCount;
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(_coA, 3);
            gridView1.SetFocusedRowCellValue(_coID, 0);
            gridView1.SetFocusedRowCellValue(_coName, "");
            gridView1.SetFocusedRowCellValue(_coSn, "");
            gridView1.SetFocusedRowCellValue(_coRemark, "");
            gridView1.SetFocusedRowCellValue(_coIsEnd, 0);
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
                Save();
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
    }
}