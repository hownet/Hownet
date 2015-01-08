using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.SystemSet
{
    public partial class BarUserInfoForm : DevExpress.XtraEditors.XtraForm
    {
        public BarUserInfoForm()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public BarUserInfoForm(BasicClass.cResult cr, int id)
            : this()
        {
            _id = id;
            r = cr;
        }
        /// <summary>
        /// 用于导航的只有ID列的表
        /// </summary>
        DataTable dtMain = new DataTable();
        BindingSource bs = new BindingSource();
        string blUser = "Hownet.BLL.Users";
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            _coDepartmentName.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            _coJobsName.ColumnEdit = BaseForm.RepositoryItem._reJobs;
            InData();
            bs.Position = dtMain.Rows.Count - 1;
            _brNext.Enabled = false;
            _brLast.Enabled = false;
            if (_id != 0)
            {
                bar1.Visible = false;
            }
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //     _brAddNew.Enabled = _brEdit.Enabled =   false;
            //    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //}
        }
        /// <summary>
        /// 读取dtMain，并绑定到myBind
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(blUser, "GetViewList", null).Tables[0];
            bs.DataSource =gridControl1.DataSource= dtMain;
        }

        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
            _brFrist.Enabled = false;
            _brPrv.Enabled = false;
            _brNext.Enabled = true;
            _brLast.Enabled = true;
        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
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
            bs.MoveNext();
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
            bs.MoveLast();
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
            BasicClass.cResult r = new BasicClass.cResult();
            r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
            Form fr = new SystemSet.UserOneForm(r, 0);
            fr.ShowDialog();
        }
        #endregion

        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                OpenFile(fileName);
            }

        }
        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "导出" + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("是否打开刚才导出的文档？", "导出Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "未找到导出的文档", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _brPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XtraGridPrint xgp = new XtraGridPrint();
            //xgp.IsPrintDate = false;
            //xgp.IsPrintPage = false;
            //xgp.EnableEditPage = true;
            //xgp.PageHeaderName = this.Text;
            //xgp.ShowDevPreview(gridControl1);
        }

        private void _brEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult cr = new BasicClass.cResult();
            cr.TextChanged += new BasicClass. TextChangedHandler(r_TextChanged);
            Form fr = new SystemSet.UserOneForm(cr, int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()));
            fr.ShowDialog();
        }

        void r_TextChanged(string s)
        {
            InData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ChangTxt();
        }

        private void BarUserInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChangTxt();
        }
        private void ChangTxt()
        {
            if (gridView1.FocusedRowHandle > -1 && _id != 0)
            {
                if (gridView1.GetFocusedRowCellDisplayText(_coID) != "0")
                {
                    _id = int.Parse(gridView1.GetFocusedRowCellDisplayText(_coID));
                    r.ChangeText(_id.ToString());
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("請先保存！");
                    return;
                }
            }
        }
    }
}