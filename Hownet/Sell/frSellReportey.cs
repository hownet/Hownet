using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Sell
{
    public partial class frSellReportey : DevExpress.XtraEditors.XtraForm
    {
        public frSellReportey()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtRL = new DataTable();
        DataTable dtInfo = new DataTable();
        string bll = "Hownet.BLL.RepertoryList";
        int _MaterielID = 0;
        int _BrandID = 0;
        int _DepotID = 0;
        int _RowID = 0;
        bool _IsVerify = false;
        public frSellReportey(BasicClass.cResult cr,DataTable dt, int MaterielID,int BrandID,int DepotID,bool IsVerify,int RowID):this()
        {
            r = cr;
            dtInfo = dt;
            _MaterielID = MaterielID;
            _BrandID = BrandID;
            _DepotID = DepotID;
            _IsVerify = IsVerify;
            _RowID = RowID;
        }

        private void frSellReportey_Load(object sender, EventArgs e)
        {
            if (!_IsVerify)
            {
                dtRL = BasicClass.GetDataSet.GetDS(bll, "GetListByMB", new object[] { _MaterielID, _BrandID, _DepotID }).Tables[0];
                dtRL.Columns.Add("NowAmount", typeof(decimal));
                if (dtRL.Rows.Count > 0 && dtInfo.Rows.Count > 0)
                {
                    DataRow[] drs;
                    for (int i = 0; i < dtRL.Rows.Count; i++)
                    {
                        if (dtRL.Rows[i]["RepertoryListID"] != DBNull.Value)
                        {
                            drs = dtInfo.Select("(RepertoryListID=" + Convert.ToInt32(dtRL.Rows[i]["RepertoryListID"]) + ")");
                            if (drs.Length > 0)
                            {
                                dtRL.Rows[i]["NowAmount"] = drs[0]["Amount"];
                            }
                        }
                        else
                        {
                            drs = dtInfo.Select("(RepertoryID=" + Convert.ToInt32(dtRL.Rows[i]["RepertoryID"]) + ")");
                            if (drs.Length > 0)
                            {
                                dtRL.Rows[i]["NowAmount"] = drs[0]["Amount"];
                            }
                        }
                    }
                }
                gridControl1.DataSource = dtRL;
            }
            else
            {
                DataTable dtTem = dtInfo.Copy();

                dtTem.Columns.Add("NowAmount", typeof(decimal));
                _coNowAmount.FieldName = "Amount";
                gridControl1.DataSource = dtTem;
                gridView1.OptionsBehavior.Editable=false;
            }
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coDepotInfoID.ColumnEdit = BaseForm.RepositoryItem._reDepotInfo(_DepotID);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int a = 0;
            DataRow[] drs;
            for(int i=0;i<dtRL.Rows.Count;i++)
            {
                a = Convert.ToInt32(dtRL.Rows[i]["A"]);
                if(a==2)
                {
                    if (dtRL.Rows[i]["RepertoryListID"] != DBNull.Value)
                    {
                        drs = dtInfo.Select("(RepertoryListID=" + Convert.ToInt32(dtRL.Rows[i]["RepertoryListID"]) + ")");
                        if (drs.Length > 0)
                        {
                           drs[0]["Amount"]  =dtRL.Rows[i]["NowAmount"] ;
                           if (Convert.ToInt32(drs[0]["A"]) == 1)
                               drs[0]["A"] = 2;
                        }
                        else
                        {
                            DataRow dr = dtInfo.NewRow();
                            for(int j=0;j<dtInfo.Columns.Count;j++)
                            {
                                dr[j] = 0;
                            }
                            dr["A"] = 3;
                            dr["MListID"] = dtRL.Rows[i]["MListID"];
                            dr["MaterielID"] = dtRL.Rows[i]["MaterielID"];
                            dr["BrandID"] = dtRL.Rows[i]["BrandID"];
                            dr["ColorID"] = dtRL.Rows[i]["ColorID"];
                            dr["ColorOneID"] = dtRL.Rows[i]["ColorOneID"];
                            dr["ColorTwoID"] = dtRL.Rows[i]["ColorTwoID"];
                            dr["SizeID"] = dtRL.Rows[i]["SizeID"];
                            dr["MeasureID"] = dtRL.Rows[i]["MeasureID"];
                            dr["RepertoryListID"] = dtRL.Rows[i]["RepertoryListID"];
                            dr["RepertoryID"] = dtRL.Rows[i]["RepertoryID"];
                            dr["Amount"] = dtRL.Rows[i]["NowAmount"];
                            dr["RowID"] = _RowID;
                            dtInfo.Rows.Add(dr);

                        }
                    }
                    else
                    {
                        drs = dtInfo.Select("(RepertoryID=" + Convert.ToInt32(dtRL.Rows[i]["RepertoryID"]) + ")");
                        if (drs.Length > 0)
                        {
                            drs[0]["Amount"] = dtRL.Rows[i]["NowAmount"];
                            if (Convert.ToInt32(drs[0]["A"]) == 1)
                                drs[0]["A"] = 2;
                        }
                        else
                        {
                            DataRow dr = dtInfo.NewRow();
                            for (int j = 0; j < dtInfo.Columns.Count; j++)
                            {
                                dr[j] = 0;
                            }
                            dr["A"] = 3;
                            dr["MListID"] = dtRL.Rows[i]["MListID"];
                            dr["MaterielID"] = dtRL.Rows[i]["MaterielID"];
                            dr["BrandID"] = dtRL.Rows[i]["BrandID"];
                            dr["ColorID"] = dtRL.Rows[i]["ColorID"];
                            dr["ColorOneID"] = dtRL.Rows[i]["ColorOneID"];
                            dr["ColorTwoID"] = dtRL.Rows[i]["ColorTwoID"];
                            dr["SizeID"] = dtRL.Rows[i]["SizeID"];
                            dr["MeasureID"] = dtRL.Rows[i]["MeasureID"];
                            dr["RepertoryListID"] = dtRL.Rows[i]["RepertoryListID"];
                            dr["RepertoryID"] = dtRL.Rows[i]["RepertoryID"];
                            dr["Amount"] = dtRL.Rows[i]["NowAmount"];
                            dr["RowID"] = _RowID;
                            dtInfo.Rows.Add(dr);
                        }
                    }
                }
            }
            r.RowChang(dtInfo);
            this.Close();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == DBNull.Value)
                return;
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
            if(e.Column==_coNowAmount)
            {
                if(Convert.ToInt32( BasicClass.BaseTableClass.dtAllMateriel.Select("(ID="+_MaterielID+")")[0]["AttributeID"])==4)
                {
                    if(e.Value.ToString().IndexOf('.')>-1)
                    {
                        XtraMessageBox.Show("成品只能填写整数!");
                        gridView1.SetFocusedValue(DBNull.Value);
                    }
                }
                decimal amount = Convert.ToDecimal(e.Value);
                if(gridView1.GetFocusedRowCellValue(_coRepertoryListID)!=DBNull.Value)
                {
                    if(amount>Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coAmount)))
                    {
                        XtraMessageBox.Show("出库数量大于库存数量");
                        gridView1.SetFocusedRowCellValue(_coNowAmount, gridView1.GetFocusedRowCellValue(_coAmount));
                    }
                }
                else
                {
                    if (amount > Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coRepertoryAmount)))
                    {
                        XtraMessageBox.Show("出库数量大于库存数量");
                        gridView1.SetFocusedRowCellValue(_coNowAmount, gridView1.GetFocusedRowCellValue(_coRepertoryAmount));
                    }
                }
            }
        }
    }
}