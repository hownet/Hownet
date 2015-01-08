using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;


namespace Hownet.Pay
{
    public partial class PayLab : DevExpress.XtraEditors.XtraForm
    {
        public PayLab()
        {
            InitializeComponent();
        }
        private DateTime bigDate;
        private DateTime endDate;
        private string blPS = "Hownet.BLL.PaySum";
        DataSet ds = new DataSet();
        DataTable dtEmp = new DataTable();
        int SelectIndexs = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _coEmpName.ColumnEdit = null;
            simpleButton3.Enabled = false;
            bigDate = (DateTime)(dateEdit1.EditValue);
            endDate = (DateTime)(dateEdit2.EditValue);
            string date1 = bigDate.ToString("yyyy-MM-dd");
            string date2 = endDate.ToString("yyyy-MM-dd");
            if (Convert.ToInt32(_loSelect.EditValue) == 1)//数量查看
            {
                
                SelectIndexs = 1;
                if (!checkEdit3.Checked)
                {
                    ds = BasicClass.GetDataSet.GetDS(blPS, "SumAmountByPW", new object[] { bigDate, endDate, Convert.ToInt32(ucGridLookup1.Values), checkEdit1.Checked, Convert.ToInt32(_loMateriel.EditValue), Convert.ToInt32(_loWorking.EditValue), checkEdit4.Checked, Convert.ToInt32(lookUpEdit2.EditValue) });
                    ds.Tables[0].TableName = "DayReport";
                    gridControl1.DataSource = ds.Tables[0];
                    simpleButton2.Enabled = true;
                    _loMateriel.Enabled = true;
                    _loWorking.Enabled = true;
                    checkEdit2.Enabled = !checkEdit1.Checked;
                    simpleButton3.Enabled = checkEdit1.Checked;
                    checkEdit2.Checked = false;
                }
                else
                {
                    ds = BasicClass.GetDataSet.GetDS(blPS, "GetSumAmountByPW", new object[] { bigDate, endDate,checkEdit4.Checked });
                    ds.Tables[0].TableName = "DayReport";
                    gridControl1.DataSource = ds.Tables[0];
                    simpleButton2.Enabled = true;
                    _loMateriel.Enabled = true;
                    _loWorking.Enabled = true;
                    checkEdit2.Enabled = false;
                    simpleButton3.Enabled = false;
                    checkEdit2.Checked = false;
                }
            }
            else if (Convert.ToInt32(_loSelect.EditValue) == 2)//计时工资
            {
                SelectIndexs = 2;
                ds = BasicClass.GetDataSet.GetDS("Hownet.BLL.DayWorkingInfo", "GetViews", new object[] { bigDate, endDate, Convert.ToInt32(ucGridLookup1.Values), checkEdit1.Checked, Convert.ToInt32(_loWorking.EditValue) });
                ds.Tables[0].TableName = "DayReport";
                gridControl1.DataSource = ds.Tables[0];
                simpleButton2.Enabled = true;
                _loMateriel.Enabled = false;
                _loWorking.Enabled = true;
            }
            else if (Convert.ToInt32(_loSelect.EditValue) == 3)//手工回收
            {
                SelectIndexs = 3;
                ds = BasicClass.GetDataSet.GetDS("Hownet.BLL.HandBackInfo", "GetViews", new object[] { bigDate, endDate, Convert.ToInt32(ucGridLookup1.Values), checkEdit1.Checked, Convert.ToInt32(_loMateriel.EditValue), Convert.ToInt32(_loWorking.EditValue) });
                ds.Tables[0].TableName = "DayReport";
                gridControl1.DataSource = ds.Tables[0];
                simpleButton2.Enabled = true;
                _loMateriel.Enabled = true;
                _loWorking.Enabled = true;
            }
            else if (Convert.ToInt32(_loSelect.EditValue) == 4)//费用记录
            {
                SelectIndexs = 4;
                ds = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayCostsInfo", "GetViews", new object[] { bigDate, endDate, Convert.ToInt32(ucGridLookup1.Values), checkEdit1.Checked });
                ds.Tables[0].TableName = "DayReport";
                gridControl1.DataSource = ds.Tables[0];
                simpleButton2.Enabled = true;
                _loMateriel.Enabled = false;
                _loWorking.Enabled = false;
            }
            else if (Convert.ToInt32(_loSelect.EditValue) == 5)
            {
                SelectIndexs = 5;
                _coEmpName.ColumnEdit = BaseForm.RepositoryItem._reMiniEmp;
                DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPayMain, "GetTemPayByPWAll", new object[] { bigDate, endDate }).Tables[0];
                gridControl1.DataSource = dttt;
                simpleButton2.Enabled = true;
            }

        }

        private void PayLab_Load(object sender, EventArgs e)
        {
            DataTable dtSelect = new DataTable();
            dtSelect.Columns.Add("ID", typeof(int));
            dtSelect.Columns.Add("Name", typeof(string));
            dtSelect.Rows.Add(1, "数量查看");
            dtSelect.Rows.Add(2, "计时工资");
            dtSelect.Rows.Add(3, "手工回收");
            dtSelect.Rows.Add(4, "费用记录");
            dtSelect.Rows.Add(5, "工资汇总");
            _loSelect.Properties.DataSource = dtSelect;
            DataTable dtMateriel = new DataTable();
            dtMateriel = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID = 4)" }).Tables[0];
            DataRow dr = dtMateriel.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtMateriel.Rows.Add(dr);
            dtMateriel.DefaultView.Sort = "ID";
            _loMateriel.Properties.DataSource = dtMateriel.DefaultView;
            DataTable dtWork = new DataTable();
            dtWork = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorking, "GetAllList", null).Tables[0];
            DataRow drs = dtWork.NewRow();
            drs["ID"] = 0;
            drs["Name"] = string.Empty;
            dtWork.Rows.Add(drs);
            dtWork.DefaultView.Sort = "ID";
            _loWorking.Properties.DataSource = dtWork.DefaultView;
            dateEdit1.EditValue = DateTime.Today;
            dateEdit2.EditValue = DateTime.Today;
            simpleButton2.Enabled = false;
            //dtEmp = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetViewList", null).Tables[0];
            //DataRow drr = dtEmp.NewRow();
            //drr["ID"] = 0;
            //drr["Name"] = string.Empty;
            //drr["DimDate"] = Convert.ToDateTime("1900-1-1");
            //drr["DepartmentID"] = 0;
            //dtEmp.Rows.Add(drr);

            //dtEmp.DefaultView.Sort = "ID";
            //dtEmp.DefaultView.RowFilter = "(DimDate ='" + Convert.ToDateTime("1900-01-01") + "')";
            //lookUpEdit1.Properties.DataSource = dtEmp.DefaultView;
            //lookUpEdit1.EditValue = 0;
            _coDepartmentID.ColumnEdit = BaseForm.RepositoryItem._reDeparment;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coWorkingID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
            _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coCosts.ColumnEdit = BaseForm.RepositoryItem._reCosts;
            _loSelect.EditValue = SelectIndexs = 1;
            _loMateriel.EditValue = _loWorking.EditValue = 0;
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
               simpleButton3.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
            {
                _coPrice.FieldName = _coMoney.FieldName = "";
                checkEdit2.Visible = false;
            }
            else
            {
                _coMoney.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

            }
            DataTable dtDep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "缝制" }).Tables[0];
            DataRow drD = dtDep.NewRow();
            drD["ID"] = 0;
            drD["Name"] = string.Empty;
            dtDep.Rows.InsertAt(drD, 0);
            lookUpEdit2.Properties.DataSource = dtDep;
            ucGridLookup1.DisplayMember = "Name";
            ucGridLookup1.ValueMember = "ID";
            ucGridLookup1.TypeID = (int)BasicClass.Enums.TableType.MiniEmp;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (gridView1.RowCount == 0)
                return;
            if (SelectIndexs == 1)
            {
                if (!checkEdit3.Checked&&!checkEdit4.Checked)
                {
                    DataTable dtttt = new DataTable();
                    dtttt.TableName = "dtSum";
                    dtttt.Columns.Add("dtOne", typeof(string));
                    dtttt.Columns.Add("dtTwo", typeof(string));
                    dtttt.Columns.Add("DateTime", typeof(string));
                    dtttt.Columns.Add("DeparmentName", typeof(string));
                    dtttt.Columns.Add("Sn", typeof(string));
                    dtttt.Columns.Add("EmpName", typeof(string));
                    dtttt.Columns.Add("MaterielName", typeof(string));
                    dtttt.Columns.Add("WorkingName", typeof(string));
                    dtttt.Columns.Add("Amount", typeof(int));
                    dtttt.Columns.Add("BoxNum", typeof(string));
                    dtttt.Columns.Add("EmployeeID", typeof(int));
                    dtttt.Columns.Add("Price", typeof(decimal));
                    dtttt.Columns.Add("Money", typeof(decimal));
                    dtttt.Columns.Add("ColorName", typeof(string));
                    dtttt.Columns.Add("SizeName", typeof(string));
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        DataRow dr = dtttt.NewRow();
                        dr["dtOne"] = bigDate.ToString("yyyy年MM月dd日");
                        dr["dtTwo"] = endDate.ToString("yyyy年MM月dd日");
                        dr["DateTime"] = gridView1.GetRowCellDisplayText(i, _coDateTime);
                        dr["DeparmentName"] = gridView1.GetRowCellDisplayText(i, _coDepartmentID);
                        dr["Sn"] = gridView1.GetRowCellDisplayText(i, _coSn);
                        dr["EmpName"] = gridView1.GetRowCellDisplayText(i, _coEmpName);
                        dr["MaterielName"] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                        dr["WorkingName"] = gridView1.GetRowCellDisplayText(i, _coWorkingID);
                        dr["Amount"] = gridView1.GetRowCellValue(i, _coAmount);
                        dr["BoxNum"] = gridView1.GetRowCellDisplayText(i, _coBoxNum);
                        dr["EmployeeID"] = gridView1.GetRowCellValue(i, _coEmployeeID);
                        if (gridView1.GetRowCellDisplayText(i, _coPrice) != string.Empty)
                            dr["Price"] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice));
                        if (gridView1.GetRowCellDisplayText(i, _coMoney) != string.Empty)
                            dr["Money"] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coMoney));
                        dr["ColorName"] = gridView1.GetRowCellDisplayText(i, _coColorID);
                        dr["SizeName"] = gridView1.GetRowCellDisplayText(i, _coSizeID);
                        dtttt.Rows.Add(dr);
                    }
                    BaseForm.PrintClass.PrintDaySum(dtttt, gridView1.GetRowCellDisplayText(0, _coDateTime).Trim() == string.Empty, checkEdit2.Checked);
                }
                else
                {
                    string aa=checkEdit3.Text;
                    if(checkEdit4.Checked)
                        aa=checkEdit4.Text;
                    string fileName =BaseContranl .BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", bigDate.ToString("yyyy年MM月dd日") + "-" + endDate.ToString("yyyy年MM月dd日")+aa);
                    if (fileName != "")
                    {
                        gridView1.ExportToXls(fileName);
                        BaseContranl.BaseFormClass.OpenFile(fileName);
                    }
                }
            }
            else
            {
                string fileName = BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
                if (fileName != "")
                {
                    gridView1.ExportToXls(fileName);
                    BaseContranl.BaseFormClass.OpenFile(fileName);
                }
            }
        }

        private void _loSelect_EditValueChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(_loSelect.EditValue);
            if (a == 1)
            {
                _loMateriel.Enabled = true;
                _loWorking.Enabled = true;
            }
            else if (a == 2)
            {
                _loMateriel.Enabled = false;
                _loWorking.Enabled = true;
            }
            else if (a == 3)
            {
                _loMateriel.Enabled = true;
                _loWorking.Enabled = true;
            }
            else if (a == 4)
            {
                _loMateriel.Enabled = false;
                _loWorking.Enabled = false;
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DateTime dt = (DateTime)(dateEdit2.EditValue);
            DataTable dtPM = BasicClass.GetDataSet.GetDS("Hownet.BLL.PayMain", "GetTopList", new object[] { 1, "", "ID DESC" }).Tables[0];
            if (dtPM.Rows.Count > 0)
            {
                if (dt < ((DateTime)(dtPM.Rows[0]["EndDate"])).AddDays(1))
                {
                    XtraMessageBox.Show("已汇总，不能删除！");
                    return;
                }
            }
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除所选择的记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == XtraMessageBox.Show("请再次确认是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    string bllPI = BasicClass.Bllstr.bllPayInfo;
                    string bllWTI = BasicClass.Bllstr.bllWorkTicketInfo;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsSelect)))
                        {
                            BasicClass.GetDataSet.ExecSql(bllPI, "Delete", new object[] { Convert.ToInt32(gridView1.GetRowCellValue(i, _coID)) });
                            BasicClass.GetDataSet.ExecSql(bllWTI, "DelBar", new object[] { Convert.ToInt32(gridView1.GetRowCellValue(i, _coInfoID)) });
                        }
                    }
                    SelectIndexs = 1;
                    ds = BasicClass.GetDataSet.GetDS(blPS, "SumAmountByPW", new object[] { bigDate, endDate, Convert.ToInt32(ucGridLookup1.Values), checkEdit1.Checked, Convert.ToInt32(_loMateriel.EditValue), Convert.ToInt32(_loWorking.EditValue), checkEdit4.Checked, Convert.ToInt32(lookUpEdit2.EditValue) });
                    ds.Tables[0].TableName = "DayReport";
                    gridControl1.DataSource = ds.Tables[0];
                    simpleButton2.Enabled = true;
                    _loMateriel.Enabled = true;
                    _loWorking.Enabled = true;
                    BasicClass.GetDataSet.SetDataTable();
                }
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != string.Empty && e.Column == _coIsSelect)
                gridView1.SetFocusedValue(e.Value);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
                checkEdit3.Checked = false;
        }

        private void checkEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
                checkEdit1.Checked = false;
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit5.Checked)
            {
                dtEmp.DefaultView.RowFilter = "1=1";
            }
            else
            {
                dtEmp.DefaultView.RowFilter = "(DimDate ='" + Convert.ToDateTime("1900-01-01") + "')";
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                try
                {
                    int cardID = Convert.ToInt32(textEdit1.EditValue);
                    if (cardID < 1)
                        return;
                    DataRow[] drs = dtEmp.Select("(IDCardID=" + cardID + ")");
                    if (drs.Length > 0)
                    {
                        cardID = Convert.ToInt32(drs[0]["ID"]);
                        ucGridLookup1.Values = cardID;
                    }
                }
                catch
                {
                }
                finally
                {
                    textEdit1.Text = string.Empty;
                }
            }

        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            int _depID = Convert.ToInt32(lookUpEdit2.EditValue);
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            string fileName = BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", bigDate.ToString("yyyy年MM月dd日") + "-" + endDate.ToString("yyyy年MM月dd日") + "数量查看");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }
    }
}