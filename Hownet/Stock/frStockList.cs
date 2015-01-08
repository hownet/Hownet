using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Stock
{
    public partial class frStockList : DevExpress.XtraEditors.XtraForm
    {
        public frStockList()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dtIsVerify = new DataTable();
        string bllSB = BasicClass.Bllstr.bllStockBack;
        int _TypeID = 0;
        private void frStockList_Load(object sender, EventArgs e)
        {
            DataTable dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "4=4" }).Tables[0];
            DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=2" }).Tables[0];
            DataRow drmat = dtMat.NewRow();
            drmat["ID"] = 0;
            drmat["Name"] = string.Empty;
            dtMat.Rows.Add(drmat);
            DataRow drCom = dtCom.NewRow();
            drCom["ID"] = 0;
            drCom["Name"] = string.Empty;
            dtCom.Rows.Add(drCom);
            dtMat.DefaultView.Sort = dtCom.DefaultView.Sort = "ID";
            lookUpEdit1.Properties.DataSource = dtMat.DefaultView;
            lookUpEdit3.Properties.DataSource = dtCom.DefaultView;
            dateEdit1.EditValue = Convert.ToDateTime("2010-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            lookUpEdit1.EditValue = lookUpEdit3.EditValue =  0;
            _coCompanyID.ColumnEdit = BaseForm.RepositoryItem._reSupplier;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coColorOneID.ColumnEdit=_coColorTwoID.ColumnEdit= _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coDepotMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coFillMan.ColumnEdit = BaseForm.RepositoryItem._reUser;
            checkEdit1.Checked = false;
            dtIsVerify.Columns.Add("Name", typeof(string));
            dtIsVerify.Columns.Add("ID", typeof(int));
            dtIsVerify.Rows.Add("", 0);
            dtIsVerify.Rows.Add("未审核", 1);
            dtIsVerify.Rows.Add("审核中", 2);
            dtIsVerify.Rows.Add("已审核", 3);
            dtIsVerify.Rows.Add("开始备料", 4);
            dtIsVerify.Rows.Add("已完成", 9);
            dtIsVerify.Rows.Add("客户取消", 21);
            dtIsVerify.Rows.Add("公司取消", 22);
            repositoryItemLookUpEdit1.DataSource = dtIsVerify;
            _coIsVerify.ColumnEdit = repositoryItemLookUpEdit1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }
        private void GetList()
        {
            if (textEdit1.Text.Trim() == string.Empty)
            {
                DateTime dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
                DateTime dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
                int Mat = Convert.ToInt32(lookUpEdit1.EditValue);
                int CompanyID = Convert.ToInt32(lookUpEdit3.EditValue);
                _TypeID = Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
                dt = BasicClass.GetDataSet.GetDS(bllSB, "GetStockList", new object[] { dt1, dt2, CompanyID, Mat, _TypeID, checkEdit1.Checked }).Tables[0];
                gridControl1.DataSource = dt;
            }
            else
            {
                try
                {
                    _TypeID = Convert.ToInt32(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
                    dt = BasicClass.GetDataSet.GetDS(bllSB, "GetStockListByNum", new object[] { Convert.ToInt32(textEdit1.Text.Trim()), _TypeID }).Tables[0];
                    gridControl1.DataSource = dt;
                }
                catch
                {
                    dt.Rows.Clear();
                }
            }
            textEdit1.Text = string.Empty;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            int a = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a = Convert.ToInt16(dt.Rows[i]["A"]);
                if (a == 2)
                {
                    BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllStockBackInfo, "UpIsEnd", new object[] { Convert.ToInt32(dt.Rows[i]["ID"]), Convert.ToInt32(dt.Rows[i]["IsEnd"]) });
                    dt.Rows[i]["A"] = 1;
                }
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string fileName =BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coNeedIsEnd)
            {
                gridView1.SetFocusedValue(e.Value);
                gridView1.SetFocusedRowCellValue(_coA, 2);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的标记为已完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(bllSB, "UpIsEnd", new object[] { _TypeID });
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                int TypeID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coState));
                int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMainID));
                if (TypeID == (int)BasicClass.Enums.TableType.Stock)
                {
                    Form fr = new frStock(ID);
                    fr.ShowDialog();
                }
                else if (TypeID == (int)BasicClass.Enums.TableType.NeedStock)
                {
                    Form fr = new frNeedStock(ID);
                    fr.ShowDialog();
                }
            }
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = (int)e.KeyChar;
            if (k == 13 )
            {
                if (textEdit2.Text.Trim().Length > 0)
                {
                    string s = "%" + textEdit2.Text.Trim() + "%";
                    dt.DefaultView.RowFilter = "(StockRemark like '" + s + "')";
                    gridControl1.DataSource = dt.DefaultView;
                }
                else
                {
                    
                    gridControl1.DataSource = dt;
                    dt.DefaultView.RowFilter = "";
                }

            }
        }
    }
}