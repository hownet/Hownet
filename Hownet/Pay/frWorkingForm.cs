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
    public partial class frWorkingForm : DevExpress.XtraEditors.XtraForm
    {
        public frWorkingForm()
        {
            InitializeComponent();
        }
        BasicClass.cResult c = new BasicClass.cResult();
        public frWorkingForm(BasicClass.cResult cr)
            : this()
        {
            c = cr;
        }
        string Permissions = "";
        BindingSource bs = new BindingSource();
        //Hownet.BLL.ProductWorkingInfo bllPWI = new Hownet.BLL.ProductWorkingInfo();
        //Hownet.BLL.ProductWorkingMain bllPWM = new Hownet.BLL.ProductWorkingMain();
        //Hownet.BLL.UserInfo bllUI = new Hownet.BLL.UserInfo();
        //Hownet.BLL.Working bllWork = new Hownet.BLL.Working();
        //Hownet.Model.ProductWorkingMain modPWM = new Hownet.Model.ProductWorkingMain();
        //Hownet.Model.ProductWorkingInfo modPWI = new Hownet.Model.ProductWorkingInfo();
        string bllMa = "Hownet.BLL.Materiel";
        string bllPWI = "Hownet.BLL.ProductWorkingInfo";
        string bllPWM = "Hownet.BLL.ProductWorkingMain";
        string bllW = "Hownet.BLL.Working";
        int _mainID = 0;
        int _materielID = 0;
        ArrayList liDel = new ArrayList();
        DataTable dtID = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtWork = new DataTable();
        DataTable dtM = new DataTable();
        bool t = false;
        private void PrlductWorkingForm_Load(object sender, EventArgs e)
        {
            this.barManager1.Form = this;
            //Hownet.BLL.Company bllCom = new Hownet.BLL.Company();
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            ShowData();
            if (!BasicClass.BasicFile.liST[0].CustOder)
                _coCustOder.Visible = false;
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //   simpleButton1.Enabled= _barNew.Enabled = barSubItem1.Enabled = _barSave.Enabled = _barEdit.Enabled = false;
            //}
        }
        private void ShowData()
        {
            dtWork = BasicClass.GetDataSet.GetDS(bllW, "GetPWList", new object[]{false}).Tables[0];
            dtM = BasicClass.GetDataSet.GetDS(bllMa, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
        _reWork.DataSource =  dtWork;
            gridControl1.DataSource = dtM;
        }
        void bs_PositionChanged(object sender, EventArgs e)
        {
            showInfo(bs.Position);
        }
        void InMain(int MaterielID)
        {
            dtID = BasicClass.GetDataSet.GetDS(bllPWM, "GetIDList", new object[] { MaterielID,0 }).Tables[0];
            if (dtID.Rows.Count == 0)
                dtID.Rows.Add(dtID.NewRow());
            bs.DataSource = dtID;
            showInfo(0);
        }
        void showInfo(int p)
        {
            t = false;
            _barFrist.Enabled = _barPve.Enabled = _barNext.Enabled = _barLast.Enabled = true;
            if (p == 0)
                _barFrist.Enabled = _barPve.Enabled = false;
            if (p == dtID.Rows.Count - 1)
                _barNext.Enabled = _barLast.Enabled = false;
            if (dtID.DefaultView[p]["ID"].ToString() != "")
            {
                _mainID = int.Parse(dtID.DefaultView[p]["ID"].ToString());
                dtMain = BasicClass.GetDataSet.GetDS(bllPWM, "GetList", new object[] { "(ID=" +_mainID+ ")" }).Tables[0];
                _ltRemark.val = dtMain.Rows[0]["Remark"].ToString();
                checkEdit1.Checked = (bool)(dtMain.Rows[0]["IsDefault"]);
            }
            else
            {
                dtMain = BasicClass.GetDataSet.GetDS(bllPWM, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtMain.NewRow();
                if (p == 0)
                {
                    dr["IsDefault"] = checkEdit1.Checked = true;
                    dr["Remark"] = _ltRemark.val = gridView1.GetFocusedRowCellValue(_coName).ToString() + "的默认工艺单";
                }
                else
                {
                    dr["IsDefault"] = checkEdit1.Checked = false;
                    dr["Remark"] = _ltRemark.val = gridView1.GetFocusedRowCellValue(_coName).ToString() + "第" + (p + 1).ToString() + "份工艺单";
                }
                dr["MaterielID"] = _materielID;
                //dr["IsVerify"] = 1;
                 dr["TaskID"] = 0;
                 dr["ID"] = _mainID = (p + 1) * -1;
                dr["CompanyID"] = 0;
                dr["FillDate"] = dr["DateTime"] = DateTime.Today;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["VerifyMan"] = 0;
                dr["Ver"] = "";
                dr["A"] = 3;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dtMain.Rows.Add(dr);
            }

            gridControl2.DataSource = null;
            dtInfo =BasicClass.GetDataSet.GetDS( bllPWI,"GetList",new object[]{"(MainID="+dtMain.Rows[0]["ID"]+")"}).Tables[0];
            gridControl2.DataSource = dtInfo;
            liDel.Clear();
            t = true;
        }
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle>-1&&e.Column != _coA && gridView2.GetFocusedRowCellValue(_coA).ToString() == "1")
                gridView2.SetFocusedRowCellValue(_coA, 2);

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


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                _materielID = int.Parse(gridView1.GetFocusedRowCellValue(_coMaterielID).ToString());
                splitContainerControl1.Panel1.Text = "款号：" + gridView1.GetFocusedRowCellValue(_coName).ToString();
                InMain(_materielID);
            }
        }
        #region 记录移动
        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }

        private void _barPve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _barNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }

        private void _barLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }

        private void _barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XtraMessageBox.Show(dtMain.GetChanges().Rows.Count.ToString());
            //XtraMessageBox.Show(dtInfo.GetChanges().Rows.Count.ToString());
            bs.AddNew();
        }
        #endregion


        private void _barToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView2.ExportToXls(fileName);
                OpenFile(fileName);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void _barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowData();
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtt = new DataTable();
            dtt.TableName = "Work";
            dtt.Columns.Add("Materiel", typeof(string));
            dtt.Columns.Add("Work", typeof(string));
            dtt.Columns.Add("Orders", typeof(string));
            dtt.Columns.Add("CustOrders", typeof(string));
            dtt.Columns.Add("GroupBy", typeof(string));
            dtt.Columns.Add("Price", typeof(decimal));
            dtt.Columns.Add("Remark", typeof(string));
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                dtt.Rows.Add(_ltRemark.val, gridView2.GetRowCellDisplayText(i, _coWorkingID),
                    gridView2.GetRowCellDisplayText(i, _coOrders), gridView2.GetRowCellDisplayText(i, _coCustOder),
                    gridView2.GetRowCellDisplayText(i, _coGroupBy), Convert.ToDecimal(gridView2.GetRowCellValue(i, _coPrice)),
                    gridView2.GetRowCellDisplayText(i, _coRemark));
            }
            BaseForm.PrintClass.ProductWorking(dtt);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount > 0)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("确认选择此工艺单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    c.RowChang(dtInfo);
                    this.Close();
                }
            }
            else
            {
                XtraMessageBox.Show("当前工艺单没有明细记录！");
                return;
            }
        }
    }
}