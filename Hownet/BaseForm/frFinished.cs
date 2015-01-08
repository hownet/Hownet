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
    public partial class frFinished : DevExpress.XtraEditors.XtraForm
    {
        public frFinished()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public frFinished(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _id = ID;
        }
        private BindingSource bs = new BindingSource();
        private DataTable dt = new DataTable();
        private int _typeID = 0;
        private int _attributeID = 0;
        private bool _isShowPopu = false;
        private string bll = "Hownet.BLL.Materiel";
        private string bllMT = "Hownet.BLL.MaterielType";
        private void frColor_Load(object sender, EventArgs e)
        {
           // this.Text = BasicClass.DefaultFormText.TEXT003;
            radioGroup1.SelectedIndex = -1;
            dt = BasicClass.BaseTableClass.dtFinished;// BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "AttributeID=4" }).Tables[0];
            radioGroup1.SelectedIndex = 0;
            ShowData();
            treeList1.ExpandAll();
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _barAdd.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                barButtonItem1.Enabled = _barEdit.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barDel.Enabled = false;
        }
        private void ShowData()
        {
            _reMT.DataSource = treeList1.DataSource = BasicClass.GetDataSet.GetDS(bllMT, "GetList", new object[] { "AttributeID=4 And IsEnd=0" }).Tables[0];
            _coMeasureID.ColumnEdit = RepositoryItem._reMeasure;
            _coDesigners.ColumnEdit = RepositoryItem._reMiniEmp;
        }

        private void _barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
            Form fr = new frFinishedOne(cr, 0, dt, _typeID, _attributeID);
            fr.ShowDialog();
        }

        void cr_RowChanged(DataTable dtt)
        {
            dt = dtt;
            gridControl1.DataSource = dt.DefaultView;
        }

        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                BasicClass.cResult cr = new BasicClass.cResult();
                cr.RowChanged += new BasicClass.RowChangedHandler(cr_RowChanged);
                Form fr = new frFinishedOne(cr, int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()), dt, _typeID, _attributeID);
                fr.ShowDialog();
            }
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
                        dttt.Rows[0]["IsEnd"] = 1;
                        BasicClass.GetDataSet.UpData(bll, dttt);
                    }
                    dt.DefaultView.RowFilter = "(TypeID=" + _typeID + ") And (IsEnd=0)";
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

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                _attributeID = int.Parse(treeList1.FocusedNode.GetValue(_trAttributeID).ToString());
                _typeID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                dt.DefaultView.RowFilter = "(TypeID=" + _typeID + ")";
                if (gridView1.FocusedRowHandle < 0)
                {
                    pictureEdit1.EditValue = null;
                }
                else
                {
                    string fileName = gridView1.GetFocusedRowCellDisplayText(_coImage);//检查并显示图片
                    ShowPic(fileName);
                }
            }
            else
            {
                _attributeID = _typeID = 0;
            }

        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            pictureEdit1.EditValue = null;
            if (e.FocusedRowHandle  > -1)
            {
                string fileName = gridView1.GetFocusedRowCellDisplayText(_coImage);//检查并显示图片
                ShowPic(fileName);
              }
        }
        private void ShowPic(string picName)
        {
            if (picName.Trim() != "")
            {
                if (!BasicClass.BasicFile.FileExists("Mini" + picName))
                    BasicClass.FileUpDown.DownLoad("Mini" + picName,  "Mini" + picName);
                pictureEdit1.EditValue = BasicClass.FileUpDown.getPicEditValue("Mini" + picName);
            }
            else
            {
                pictureEdit1.EditValue = null;
            }
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue != null)
            {
                string fileName = gridView1.GetFocusedRowCellDisplayText(_coImage);
                //try
                //{
                //    if (fileName != "" && !BasicClass.BasicFile.FileExists(fileName))
                //        BasicClass.FileUpDown.DownLoad(fileName, "234.jpg");
                //}
                //catch { }
                Form fr = new ShowPic(fileName);
                
                fr.ShowDialog();
            }
        }

        private void _barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowData();
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
                    XtraMessageBox.Show("只能選擇使用中的辦號！");
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

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIsEnd();
        }
        private void CheckIsEnd()
        {
            _barDel.Enabled = _isShowPopu = false;
            if (radioGroup1.SelectedIndex == 0)
            {
                dt.DefaultView.RowFilter = "(TypeID=" + _typeID + ") And (IsEnd=0)";
                _barDel.Enabled = true;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                dt.DefaultView.RowFilter = "(TypeID=" + _typeID + ") And (IsEnd>0)";
                _isShowPopu = true;
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                dt.DefaultView.RowFilter = "";
            }
            gridControl1.DataSource = dt.DefaultView;
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

    }
}