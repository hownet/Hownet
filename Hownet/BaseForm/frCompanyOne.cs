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
    public partial class frCompanyOne : DevExpress.XtraEditors.XtraForm
    {
        public frCompanyOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        int _TypeID = 0;
        DataTable dtCompany = new DataTable();
        DataTable dtOld = new DataTable();
        string _typeName = string.Empty;
        public frCompanyOne(BasicClass.cResult cr, int ID,int TypeID,DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            _TypeID = TypeID;
            dtOld = dt;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
            dtCompany = dtOld.Clone();
            if (_TypeID == 1)
            {
                _typeName = "客户";
                labelControl3.Text = "  客户名称";
            }
            else if (_TypeID == 2)
            {
                _typeName = "供应商";
            }
            else if (_TypeID == 3)
            {
                _typeName = "加工商";
                labelControl3.Text = "加工商名称";
            }
            if (_ID == 0)
            {
                this.Text = "添加" + _typeName;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is TextEdit)
                    {
                        TextEdit te = this.Controls[i] as TextEdit;
                        te.EditValue = string.Empty;
                    }
                }
                _teRemark.EditValue = string.Empty;
                _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
                _deLoanDate.EditValue = DateTime.Now.Date;
                _teLastMoney.EditValue = _teLoanMoney.EditValue = 0d;
            }
            else
            {
                this.Text = "编辑" + _typeName;
                dtCompany.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is TextEdit)
                    {
                        TextEdit te = this.Controls[i] as TextEdit;
                        object o = dtCompany.Rows[0][this.Controls[i].Name.Substring(3)];
                        if (o != null && o.ToString().Trim() != string.Empty)
                            te.EditValue = o.ToString();
                        else
                            te.EditValue = string.Empty;
                    }
                }
                _teLoanMoney.EditValue = Convert.ToDecimal(dtCompany.Rows[0]["LoanMoney"]);
                _deLoanDate.EditValue = Convert.ToDateTime(dtCompany.Rows[0]["LoanDate"]);
                _teRemark.EditValue = dtCompany.Rows[0]["Remark"].ToString();
               _teLoanMoney.Enabled=_deLoanDate.Enabled= (bool)(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllCompanyLog, "CheckCanEditLoan", new object[] {_ID }));
            }
            _teLastMoney.Enabled = false;
        }
        private bool Save()
        {
            if (_teName.Text.Trim().Length == 0 || _teSn.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(_typeName + "编号、" + _typeName + "名称不能为空！");
                return false;
            }
            string sqlWhere = " ((ID <> " + _ID + ") And (TypeID="+_TypeID+")) And ((Name = '" + _teName.Text.Trim() + "') OR (Sn = '" + _teSn.Text.Trim() + "')) ";
            dtCompany.Rows.Clear();
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString()) > 0 && drs[0]["Name"].Equals(_teName.Text.Trim()) && drs[0]["Sn"].Equals(_teSn.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtCompany.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllCompany, dtCompany);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show(_typeName + "编号、" + _typeName + "名称重复！");
                    return false;
                }
            }
            DataRow dr = dtCompany.NewRow();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is TextEdit)
                {
                    TextEdit te = this.Controls[i] as TextEdit;
                    try
                    {
                        dr[this.Controls[i].Name.Substring(3)] = te.EditValue;
                    }
                    catch { dr[this.Controls[i].Name.Substring(3)] = 0; }

                }
            }
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["TypeID"] = _TypeID;
            dr["IsEnd"] = 0;
            dr["LoanDate"] = _deLoanDate.EditValue;
            dr["Remark"] = _teRemark.EditValue;
            if (_TypeID == 1)
                dr["UserID"] = BasicClass.UserInfo.UserID;
            else
                dr["UserID"] = 0;
            dtCompany.Rows.Add(dr);
            if (_ID == 0)
            {
                dr["LastMoney"] = dr["LoanMoney"];
                dr["ID"] =_ID= BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllCompany, dtCompany);
                dtOld.Rows.Add(dr.ItemArray);
            }
            else
            {
                if (_teLoanMoney.Enabled)
                    dr["LastMoney"] = dr["LoanMoney"];
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllCompany, dtCompany);
                drs = dtOld.Select("(ID=" + _ID + ")");
                drs[0].ItemArray = dr.ItemArray;
                DataTable dddt = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(CompanyID=" + _ID + ")" }).Tables[0];
                if (dddt.Rows.Count > 0)
                {
                    if (dddt.Rows[0]["Name"].ToString() != _teName.EditValue.ToString())
                    {
                        dddt.Rows[0]["Name"] = _teName.EditValue;
                        BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dddt);
                    }
                }
            }
            if (_teLoanMoney.Enabled)
            {
                try
                {
                    decimal lm = Convert.ToDecimal(_teLoanMoney.EditValue);
                    DateTime ld = Convert.ToDateTime(_deLoanDate.EditValue);
                    string blCL = BasicClass.Bllstr.bllCompanyLog;
                    DataTable dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "(CompanyID=" + _ID + ")" }).Tables[0];

                    if (dtCL.Rows.Count == 1)
                    {
                        dtCL.Rows[0]["DateTime"] = Convert.ToDateTime(_deLoanDate.EditValue);
                        dtCL.Rows[0]["ChangMoney"] = dtCL.Rows[0]["Money"] = _teLoanMoney.EditValue;
                        BasicClass.GetDataSet.UpData(blCL, dtCL);
                    }
                    else
                    {
                        DataRow drr = dtCL.NewRow();
                        drr["ID"] = 0;
                        drr["CompanyID"] = _ID;
                        try
                        {
                            drr["ChangMoney"] = drr["Money"] = _teLoanMoney.EditValue;
                        }
                        catch
                        {
                            drr["ChangMoney"] = drr["Money"] = 0;
                        }
                        if (_deLoanDate.EditValue == null)
                            drr["DateTime"] = DateTime.Today;
                        else
                            drr["DateTime"] = Convert.ToDateTime(_deLoanDate.EditValue);
                        if (_TypeID == 1 || _TypeID == 3)
                            drr["TypeID"] = -1;
                        else if (_TypeID == 2)
                            drr["TypeID"] = -2;
                        drr["LastMoney"] = drr["TableID"] = drr["NowMoneyTypeID"] = 0;
                        drr["NowReta"] = 1;
                        drr["A"] = 1;
                        dtCL.Rows.Add(drr);
                        BasicClass.GetDataSet.Add(blCL, dtCL);
                    }
                }
                catch { }
            }
            dtOld.AcceptChanges();
            BasicClass.BaseTableClass.dtCompany = BasicClass.GetDataSet.GetBySql("Select 1 as A,* From Company where (TypeID=1)");
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllCompany, "InKJKM", null);
            return true;
        }
        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                _ID = 0;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is TextEdit)
                    {
                        TextEdit te = this.Controls[i] as TextEdit;
                        te.EditValue = string.Empty;
                    }
                }
                _teRemark.EditValue = string.Empty;
                _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
                _deLoanDate.EditValue = DateTime.Now.Date;
            }
        }

        private void _sbSaveAndExit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                this.Close();
            }
        }

        private void _sbCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否不保存當前處理直接退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                this.Close();
            }
        }

    }
}