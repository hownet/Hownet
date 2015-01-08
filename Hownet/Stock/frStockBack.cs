using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Threading;
using System.Drawing.Printing;

namespace Hownet.Stock
{

    public partial class frStockBack : DevExpress.XtraEditors.XtraForm
    {
        public frStockBack()
        {
            InitializeComponent();
        }
        int _companyID = 0;
        BasicClass.cResult r = new BasicClass.cResult();
        int _TypeID = 0;
        Point p = new Point();
        public frStockBack(BasicClass.cResult cr, int CompanyID,int TypeID)
            : this()
        {
            r = cr;
            _companyID = CompanyID;
            _TypeID = TypeID;
        }
        private string bllSTI = "Hownet.BLL.StockBackInfo";
        DataTable dtST = new DataTable();
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ShowData();
            dtST = BasicClass.GetDataSet.GetDS(bllSTI, "GetNeedInfoList", new object[] {0, _TypeID, _companyID }).Tables[0];
            dtST.Columns.Add("NowAmount", typeof(decimal));
            gridControl1.DataSource = dtST;
        }

        void ShowData()
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reAllMateriel;
            //_coColorID.ColumnEdit = _coColorOneID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coDepotMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coPrice.ColumnEdit = _coMoney.ColumnEdit = BaseForm.RepositoryItem._re2Money;
            _coColorID.ColumnEdit = _coColorOneID.ColumnEdit = _coColorTwoID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coMoney)
            {
                try
                {
                    decimal amount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coNowAmount).ToString());
                    decimal price = decimal.Parse(gridView1.GetFocusedRowCellValue(_coPrice).ToString());
                    gridView1.SetFocusedRowCellValue(_coMoney, amount * price);
                    if (e.Column == _coNowAmount)
                    {
                        decimal notamount = decimal.Parse(gridView1.GetFocusedRowCellValue(_coNotAmount).ToString());
                        if (decimal.Parse(e.Value.ToString()) > notamount)
                        {
                            if (DialogResult.No == XtraMessageBox.Show("来货数量超过采购数量，是否收货？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                            {
                                gridView1.SetFocusedRowCellValue(_coNowAmount, 0);
                            }
                        }
                    }
                    else if (e.Column == _coPrice)
                    {
                        decimal stockPrice = decimal.Parse(gridView1.GetFocusedRowCellValue(_coStockPrice).ToString());
                        if (decimal.Parse(e.Value.ToString()) > stockPrice)
                        {
                            if (DialogResult.No == XtraMessageBox.Show("收货价格大于订货价格，是否收货？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                            {
                                gridView1.SetFocusedRowCellValue(_coNowAmount, 0);
                                gridView1.SetFocusedRowCellValue(_coPrice, stockPrice);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtST.AcceptChanges();
            DataTable dt = dtST.Clone();
            for (int i = 0; i < dtST.Rows.Count; i++)
            {
                if (dtST.Rows[i]["NowAmount"]!=DBNull.Value&&Convert.ToDecimal(dtST.Rows[i]["NowAmount"])!=0)// decimal.Parse(dtST.Rows[i]["Money"].ToString()) > 0)
                {
                    dt.Rows.Add(dtST.Rows[i].ItemArray);
                }
            }
            r.RowChang(dt);
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            dtST = BasicClass.GetDataSet.GetDS(bllSTI, "GetNeedInfoList", new object[] { -1, _TypeID, _companyID }).Tables[0];
            dtST.Columns.Add("NowAmount", typeof(decimal));
            gridControl1.DataSource = dtST;

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                BasicClass.cResult r = new BasicClass.cResult();
                p.X += this.Location.X;
                p.Y += this.Location.Y;
                DataTable dt = new DataTable();
                //Form fr = new frStockInfoList(r, dt, p,Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)));
                //fr.ShowDialog();
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }
    }
}