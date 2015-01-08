using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.OtherTem
{
    public partial class frOut : DevExpress.XtraEditors.XtraForm
    {
        public frOut()
        {
            InitializeComponent();
        }
        private string bllPS = "Hownet.BLL.ProduceSell";
        private string bllPSO = "Hownet.BLL.ProduceSellOne";
        private string bllPSI = "Hownet.BLL.ProduceSellInfo";

        DataTable dtPS = new DataTable();
        DataTable dtPSO = new DataTable();
        DataTable dtPSI = new DataTable();
        DataTable dtRep = new DataTable();
        int _MainID = 0;
        private void frOut_Load(object sender, EventArgs e)
        {
            DataTable dtDep = BasicClass.GetDataSet.GetBySql("Select ID,Name From Deparment Where TypeID=42");
            lookUpEdit1.Properties.DataSource = dtDep;
            if (dtDep.Rows.Count > 0)
                lookUpEdit1.EditValue = Convert.ToInt32(dtDep.Rows[0]["ID"]);
            else
                lookUpEdit1.EditValue = 0;
            _MainID = Convert.ToInt32(BasicClass.GetDataSet.GetOne(bllPS, "GetMaxId", null)) - 1;
            dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];
            if (dtPS.Rows.Count == 1)
            {
                if (Convert.ToDateTime(dtPS.Rows[0]["DateTime"]) < BasicClass.GetDataSet.GetDateTime().Date)
                {
                    dtPS.Rows.Clear();
                    dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                    DataRow dr = dtPS.NewRow();
                    dr["A"] = dr["CompanyID"] = dr["ID"] = dr["Depot"] = dr["UpData"] = dr["Money"] = _MainID = 0;
                    dr["VerifyDate"] = dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                    dr["VerifyMan"] = dr["FillMan"] = BasicClass.UserInfo.UserID;
                    dr["IsVerify"] = 3;
                    dr["LastDate"] = DateTime.Parse("1900-1-1");
                    dr["Remark"] = "";
                    dr["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(), 0 });
                    dr["LastMoney"] = 0;
                    dr["BackMoney"] = 0;
                    dr["BackDate"] = DateTime.Parse("1900-1-1");
                    dtPS.Rows.Add(dr);
                    _MainID = BasicClass.GetDataSet.Add(bllPS, dtPS);
                }
                else
                {
                    _MainID = Convert.ToInt32(dtPS.Rows[0]["ID"]);
                }
            }
            else
            {
                dtPS = BasicClass.GetDataSet.GetDS(bllPS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtPS.NewRow();
                dr["A"] = dr["CompanyID"] = dr["ID"] = dr["Depot"] = dr["UpData"] = dr["Money"] = _MainID = 0;
                dr["VerifyDate"] = dr["FillDate"] = dr["DateTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dr["VerifyMan"] = dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 3;
                dr["LastDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = BasicClass.GetDataSet.GetOne(bllPS, "NewNum", new object[] { BasicClass.GetDataSet.GetDateTime(), 0 });
                dr["LastMoney"] = 0;
                dr["BackMoney"] = 0;
                dr["BackDate"] = DateTime.Parse("1900-1-1");
                dtPS.Rows.Add(dr);
                _MainID = BasicClass.GetDataSet.Add(bllPS, dtPS);
            }
            dtPSI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            gridControl1.DataSource = dtPSI;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                //if(textEdit1.Text.Trim().Length<8)
                //{
                //    textEdit1.EditValue = string.Empty;
                //    return;
                //}
                if (Convert.ToInt32(lookUpEdit1.EditValue) == 0)
                {
                    XtraMessageBox.Show("请选择出货仓库！");
                    textEdit1.EditValue = string.Empty;
                    return;
                }
                if (!checkEdit1.Checked)
                {
                    if (DialogResult.No == XtraMessageBox.Show("是否真的出仓？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        textEdit1.EditValue = string.Empty;
                        return;
                    }
                }
                string lab = textEdit1.Text.Trim();
                //if(lab.Length>8)
                // int SizeID=Convert.ToInt32( lab.Substring(lab.Length-2,2));
                // int ColorID=Convert.ToInt32(lab.Substring(lab.Length-5,3));
                // int BrandID=Convert.ToInt32(lab.Substring(lab.Length-7,2));
                // int MaterielID = Convert.ToInt32(BasicClass.BaseTableClass.dtFinished.Select("(Name='" + lab.Substring(0, lab.Length - 7)+"')")[0]["ID"]);
                //string strWhere = "(SizeID=" + SizeID + ") And (ColorID=" + ColorID + ") And (BrandID=" + BrandID + ") And (MaterielID=" + MaterielID + ") And (DepartmentID=" + lookUpEdit1.EditValue + ")";
                string strWhere = "(MListID=" + Convert.ToInt32(lab) + ")";
                dtRep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetList", new object[] { strWhere }).Tables[0];
                if (dtRep.Rows.Count > 0)
                {
                    decimal amount = Convert.ToDecimal(dtRep.Rows[0]["Amount"]);
                    if (dtRep.Rows.Count > 1)
                    {
                        for (int i = 1; i < dtRep.Rows.Count; i++)
                        {
                            amount += Convert.ToDecimal(dtRep.Rows[i]["Amount"]);
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllRepertory, "Delete", new object[] { Convert.ToInt32(dtRep.Rows[i]["ID"]) });
                        }
                        dtRep.Rows[0]["Amount"] = amount;
                        DataTable dtTem = dtRep.Clone();
                        dtTem.Rows.Add(dtRep.Rows[0].ItemArray);
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtTem);
                    }
                    DataTable dtPSITem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetList", new object[] { "(ID=0)" }).Tables[0];
                    DataRow dr = dtPSITem.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["MainID"] = _MainID;
                    dr["MaterielID"] = dtRep.Rows[0]["MaterielID"];
                    dr["ColorID"] = dtRep.Rows[0]["ColorID"];
                    dr["ColorOneID"] = dr["ColorTwoID"] = 0;
                    dr["RepertoryID"] = dtRep.Rows[0]["ID"];
                    dr["Amount"] = 1;
                    dr["SalesInfoID"] = amount;
                    dr["SizeID"] = dtRep.Rows[0]["SizeID"];
                    dr["BrandID"] = dtRep.Rows[0]["BrandID"];
                    dr["MListID"] = dtRep.Rows[0]["MListID"];
                    dtPSITem.Rows.Add(dr);
                    dtPSITem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllPSI, dtPSITem);
                    dtPSI.Rows.Add(dtPSITem.Rows[0].ItemArray);
                    amount--;
                    dtRep.Rows[0]["Amount"] = amount;
                    DataTable dtTT = dtRep.Clone();
                    dtTT.Rows.Add(dtRep.Rows[0].ItemArray);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllRepertory, dtTT);
                }
                else
                {
                    XtraMessageBox.Show("没有库存！");
                }
                textEdit1.EditValue = string.Empty;
            }
        }

        private void frOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
                textEdit1.Focus();
        }

        private void gridControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
                textEdit1.Focus();
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
                textEdit1.Focus();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    int _id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                    int _RID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coRepertoryID));
                    BasicClass.GetDataSet.ExecSql(bllPSI, "Delete", new object[] { _id });
                    try
                    {
                        BasicClass.GetDataSet.GetBySql(" UPDATE    TOP (200) Repertory SET   Amount = Amount + 1 WHERE     (ID = " + _RID + ")");
                    }
                    catch
                    {

                    }
                    dtPSI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSellInfo, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
                    gridControl1.DataSource = dtPSI;
                }
            }
        }
    }
}