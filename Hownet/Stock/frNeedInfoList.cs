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
    public partial class frNeedInfoList : DevExpress.XtraEditors.XtraForm
    {
        public frNeedInfoList()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        DataTable dtSBI = new DataTable();
        public frNeedInfoList(BasicClass.cResult crr, DataTable dtSotck)
            : this()
        {
            r = crr;
            dtSBI = dtSotck;
        }
        string bllSBI = BasicClass.Bllstr.bllStockBackInfo;
        string bllPR = "Hownet.BLL.PlanUseRep";
        int _TypeID = (int)BasicClass.Enums.TableType.NeedStock;
        int _PRTypeID = (int)BasicClass.Enums.PlanUseRep.已申购数量;
        DataTable dt = new DataTable();
        private void frNeedInfoList_Load(object sender, EventArgs e)
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reMateriel;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coDepotMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS(bllSBI, "GetNeedInfoList", new object[] { Convert.ToInt32(checkEdit1.Checked), _TypeID,0 }).Tables[0];
            dt.Columns.Add("NowAmount", typeof(decimal));
            gridControl1.DataSource = dt;
            int _stockInfoID = 0;
            if (dt.Rows.Count > 0 && dtSBI.Rows.Count > 0)
            {
                for (int i = 0; i < dtSBI.Rows.Count; i++)
                {
                    _stockInfoID = Convert.ToInt32(dtSBI.Rows[i]["StockInfoID"]);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (_stockInfoID == Convert.ToInt32(dt.Rows[j]["StockInfoID"]))
                        {
                            dt.Rows[j]["NowAmount"] = dtSBI.Rows[i]["Amount"];
                            break;
                        }
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int _stockInfoID = 0;
            dtSBI.Rows.Clear();
            DataRow[] drs;
            DateTime lt = BasicClass.GetDataSet.GetDateTime().Date.AddDays(5);
            string sss=string.Empty;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                try
                {
                    _stockInfoID = Convert.ToInt32(gridView1.GetRowCellValue(i, _coStockInfoID));
                    if (gridView1.GetRowCellDisplayText(i, _coNowAmount).Length > 0&&Convert.ToDecimal(gridView1.GetRowCellValue(i,_coNowAmount))>0)
                    {
                        drs = dtSBI.Select("(StockInfoID=" + _stockInfoID + ")");
                        if (drs.Length > 0)
                        {
                            drs[0]["Amount"] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coNowAmount));
                        }
                        else
                        {
                            DataRow dr = dtSBI.NewRow();
                            dr["ID"]=0;
                            dr["MainID"]=0;
                            dr["A"] = 3;
                            dr["StringTaskID"] = gridView1.GetRowCellValue(i, _coStringTaskID);
                            dr["Remark"] = string.Empty;
                            dr["StockInfoID"] = _stockInfoID;
                            dr["MaterielID"] = gridView1.GetRowCellValue(i, _coMaterielID);
                            dr["ColorID"] = gridView1.GetRowCellValue(i, _coColorID);
                            dr["ColorOneID"] = gridView1.GetRowCellValue(i, _coColorOneID);
                            dr["ColorTwoID"] = gridView1.GetRowCellValue(i, _coColorTwoID);
                            dr["SizeID"] = gridView1.GetRowCellValue(i, _coSizeID);
                            dr["Price"] = gridView1.GetRowCellValue(i, _coPrice);
                            dr["DepotMeasureID"] = dr["CompanyMeasureID"] = gridView1.GetRowCellValue(i, _coDepotMeasureID);
                            dr["Amount"] = dr["NotAmount"] = dr["PriceAmount"] = dr["NotPriceAmount"] = gridView1.GetRowCellValue(i, _coNowAmount);
                            dr["MListID"] = gridView1.GetRowCellValue(i, _coMListID);
                            dr["CompanyID"] = dr["BrandID"] = dr["IsEnd"] = 0;
                            dr["DemandID"] = 0;
                            dr["NeedAMount"] = dr["ExcessAmount"] = 0;
                            dr["Money"] = Convert.ToDecimal(gridView1.GetRowCellValue(i, _coNowAmount)) * Convert.ToDecimal(gridView1.GetRowCellValue(i, _coPrice));
                            dr["Conversion"] = 1;
                            dr["NeedIsEnd"] = Convert.ToInt32(gridView1.GetRowCellValue(i, _coNeedIsEnd));
                            dr["MaterielRemark"] = gridView1.GetRowCellDisplayText(i, _coMaterielRemark);
                            dr["LastTime"] = lt;
                            sss+=gridView1.GetRowCellDisplayText(i,_coStockRemark);
                            dtSBI.Rows.Add(dr);
                        }
                        dtSBI.AcceptChanges();
                    }
                }
                catch
                {
                }
            }
            r.RowChang(dtSBI);
            r.ChangeText(sss);
            this.Close();
        }
    }
}