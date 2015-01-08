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
    public partial class BarEmployeeForm : DevExpress.XtraEditors.XtraForm
    {
        public BarEmployeeForm()
        {
            InitializeComponent();
        }
        BindingSource bs = new BindingSource();
        DataTable dt = new DataTable();
        BasicClass.cResult r = new BasicClass.cResult();
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            InData();
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                barButtonItem1.Enabled = _brEdit.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _brDel.Enabled = false;
            if(!BasicClass.BasicFile.liST[0].IsShowOutEmp)
            {
            radioGroup1.Visible =false ;
            }
        }
        void InData()
        {
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetViewList", null).Tables[0];
            radioGroup1.SelectedIndex = -1;
            radioGroup1.SelectedIndex = 0;
        }
        void r_TextChanged(string s)
        {
            if (s != string.Empty)
            {
                InData();
            }
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView3.ExportToXls(fileName);
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

        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
            Form fr = new frEmployeeForms(r, 0, (int)BasicClass.Enums.Operation.Add);
            fr.ShowDialog();
        }

        private void _brEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.FocusedRowHandle > -1)
            {
                r.TextChanged += new BasicClass.TextChangedHandler(r_TextChanged);
                Form fr = new frEmployeeForms(r, int.Parse(gridView3.GetFocusedRowCellValue(_coID).ToString()), (int)BasicClass.Enums.Operation.Edit);
                fr.ShowDialog();
            }
        }

        private void _barView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.FocusedRowHandle > -1)
            {
                Form fr = new frEmployeeForms(r, int.Parse(gridView3.GetFocusedRowCellValue(_coID).ToString()), (int)BasicClass.Enums.Operation.View);
                fr.ShowDialog();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex > -1)
            {
                if (radioGroup1.SelectedIndex == 0)
                    dt.DefaultView.RowFilter = "IsEnd=0";
                else if (radioGroup1.SelectedIndex == 1)
                    dt.DefaultView.RowFilter = "IsEnd=1";
                else if (radioGroup1.SelectedIndex == 2)
                    dt.DefaultView.RowFilter = "";
                bs.DataSource = gridControl3.DataSource = dt.DefaultView;
            }
        }
    }
}