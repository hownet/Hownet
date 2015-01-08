using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Sell
{
    public partial class frSalesList : DevExpress.XtraEditors.XtraForm
    {
        public frSalesList()
        {
            InitializeComponent();
        }
        private int _id = 0;
        private int _materielID, _brandID, _typeID;
        private string IDS = string.Empty;
        DataTable dt = new DataTable();
        bool _isMateriel = false;
        string fileName = string.Empty;
        private void frSalesList_Load(object sender, EventArgs e)
        {
            DataTable dtCom = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=1)" }).Tables[0];
            dtCom.Rows.Add(0, 0, "", "");
            dtCom.DefaultView.Sort = "ID";
            _reCom.DataSource = dtCom.DefaultView;

            DataTable dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
            dtMat.Rows.Add(0, 0, "", 0);
            dtMat.DefaultView.Sort = "ID";
            _reMateriel.DataSource = dtMat.DefaultView;
            DataTable dtBrand = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=5)" }).Tables[0];
            dtBrand.Rows.Add(0, 0, "", 0);
            dtBrand.DefaultView.Sort = "ID";
            _reBrand.DataSource = dtBrand;
            _coMeasureID.ColumnEdit = BaseForm.RepositoryItem._reMeasure;
            _coPackingMethodID.ColumnEdit = BaseForm.RepositoryItem._rePackingMethod;
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                _id = int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString());
                radioGroup1.SelectedIndex = -1;
                radioGroup1.SelectedIndex = 0;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex > -1)
            {
                fileName = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].ToString();
            }
            if (gridView1.RowCount == 0)
            {
                amountList1.ClearData();
                return;
            }
            _id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
            if (radioGroup1.SelectedIndex > -1 && radioGroup1.SelectedIndex < 3)
                amountList1.Open(_id, (int)BasicClass.Enums.TableType.SalesOne, true, radioGroup1.SelectedIndex + 1);
            _brandID = _materielID = 0;
            if (gridView1.FocusedRowHandle > -1)
            {
                _materielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID));
                _brandID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coBrandID));
            }
            if (_materielID == 0)
            {
                amountList1.ClearData();
                return;
            }
            //该款订单总数
            if (radioGroup1.SelectedIndex == 3)
            {
                if (IDS.Length == 0)
                {
                    amountList1.ClearData();
                    return;
                }
                DataTable dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllAmountByMat", new object[] { IDS, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                amountList1.Open(false, dtt);
            }
            //该款未发货总数
            else if (radioGroup1.SelectedIndex == 4)
            {
                if (IDS.Length == 0)
                {
                    amountList1.ClearData();
                    return;
                }
                DataTable dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllNotAmount", new object[] { IDS, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                amountList1.Open(false, dtt);
            }
            //该款库存
            else if (radioGroup1.SelectedIndex == 5)
            {
                DataTable dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetReAmount", new object[] { _materielID, _brandID }).Tables[0];
                amountList1.Open(false, dtt);

            }
            //该款在线数量
            else if (radioGroup1.SelectedIndex == 6)
            {
                DataTable dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetWorkAmount", new object[] { _materielID, _brandID, (int)BasicClass.Enums.TableType.Task }).Tables[0];
                amountList1.Open(false, dtt);
            }
            //本单欠数
            else if (radioGroup1.SelectedIndex == 7)
            {
                DataTable dtSales = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetNotAmount", new object[] { _id, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                DataTable dtAll = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllAmount", new object[] { _materielID, _brandID, (int)BasicClass.Enums.TableType.Task }).Tables[0];
                amountList1.Open(false, CaicAmount(dtSales, dtAll));
            }
            //本款欠数
            else if (radioGroup1.SelectedIndex == 8)
            {
                if (IDS.Length == 0)
                {
                    amountList1.ClearData();
                    return;
                }
                DataTable dtSales = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllNotAmount", new object[] { IDS, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                DataTable dtAll = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllAmount", new object[] { _materielID, _brandID, (int)BasicClass.Enums.TableType.Task }).Tables[0];

                amountList1.Open(false, CaicAmount(dtSales, dtAll));
            }
            //本单库存欠数
            else if (radioGroup1.SelectedIndex == 9)
            {
                DataTable dtSales = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetNotAmount", new object[] { _id, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                DataTable dtAll = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetReAmount", new object[] { _materielID, _brandID }).Tables[0];
                amountList1.Open(false, CaicAmount(dtSales, dtAll));
            }
            //本款库存欠数
            else if (radioGroup1.SelectedIndex == 10)
            {
                if (IDS.Length == 0)
                {
                    amountList1.ClearData();
                    return;
                }
                DataTable dtSales = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllNotAmount", new object[] { IDS, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                DataTable dtAll = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetReAmount", new object[] { _materielID, _brandID }).Tables[0];
                amountList1.Open(false, CaicAmount(dtSales, dtAll));
            }
        }
        private DataTable CaicAmount(DataTable dtSales, DataTable dtAll)
        {
            bool t = false;
            for (int i = 0; i < dtSales.Rows.Count; i++)
            {
                t = false;
                for (int j = 0; j < dtAll.Rows.Count; j++)
                {
                    if (dtSales.Rows[i]["ColorID"].Equals(dtAll.Rows[j]["ColorID"]) &&
                        dtSales.Rows[i]["ColorOneID"].Equals(dtAll.Rows[j]["ColorOneID"]) &&
                        dtSales.Rows[i]["ColorTwoID"].Equals(dtAll.Rows[j]["ColorTwoID"]) &&
                        dtSales.Rows[i]["SizeID"].Equals(dtAll.Rows[j]["SizeID"]))
                    {
                        dtSales.Rows[i]["Amount"] = Convert.ToInt32(dtAll.Rows[j]["Amount"]) - Convert.ToInt32(dtSales.Rows[i]["Amount"]);
                        t = true;
                        break;
                    }
                }
                if (!t)
                {
                    dtSales.Rows[i]["Amount"] = Convert.ToInt32(dtSales.Rows[i]["Amount"]) * -1;
                }
            }
            for (int j = 0; j < dtAll.Rows.Count; j++)
            {
                t = false;
                for (int i = 0; i < dtSales.Rows.Count; i++)
                {
                    if (dtSales.Rows[i]["ColorID"].Equals(dtAll.Rows[j]["ColorID"]) &&
                        dtSales.Rows[i]["ColorOneID"].Equals(dtAll.Rows[j]["ColorOneID"]) &&
                        dtSales.Rows[i]["ColorTwoID"].Equals(dtAll.Rows[j]["ColorTwoID"]) &&
                        dtSales.Rows[i]["SizeID"].Equals(dtAll.Rows[j]["SizeID"]))
                    {
                        t = true;
                        break;
                    }
                }
                if (!t)
                {
                    dtSales.Rows.Add(dtAll.Rows[j].ItemArray);
                }
            }
            return dtSales;
        }
        private void _sbFind_Click(object sender, EventArgs e)
        {
            gridView2.Columns.Clear();
            _typeID = radioGroup2.SelectedIndex + 1;
            gridControl2.DataSource = null;
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetSalesList", new object[] { _ldOneDate.val, _ldTwoDate.val, _chHaveIsEnd.Checked, radioGroup2.SelectedIndex + 1, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
            gridControl2.DataSource = dt;
            if (radioGroup2.SelectedIndex == 0)
            {
                gridView2.Columns["款号"].ColumnEdit = _reMateriel;
                gridView2.Columns["商标"].ColumnEdit = _reBrand;
                _isMateriel = true;
            }
            else
            {
                gridView2.Columns["客户"].ColumnEdit = _reCom;
                _isMateriel = false;
            }
            if (gridView2.RowCount > 0)
            {
                ShowSalesList(0);
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle > -1)
            {
                ShowSalesList(e.FocusedRowHandle);
                IDS = string.Empty;
            }
        }
        private void ShowSalesList(int RowID)
        {
            if (_typeID == 1)
            {
                int MaterielID = Convert.ToInt32(gridView2.GetRowCellValue(RowID, "款号"));
                int BrandID = Convert.ToInt32(gridView2.GetRowCellValue(RowID, "商标"));
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetSalesListByMC", new object[] { MaterielID, BrandID, 0, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
            }
            else if (_typeID == 2)
            {
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetSalesListByMC", new object[] { 0, 0, Convert.ToInt32(gridView2.GetRowCellValue(RowID, "客户")), (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
            }
            radioGroup1.SelectedIndex = -1;
            radioGroup1.SelectedIndex = 0;
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (IDS.Length == 0)
                {
                    amountList1.ClearData();
                    return;
                }
                DataTable dtSales = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSalesOrderInfoList, "GetAllNotAmount", new object[] { IDS, (int)BasicClass.Enums.TableType.SalesOne }).Tables[0];
                DataTable dtAll = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllRepertory, "GetReAmount", new object[] { _materielID, _brandID }).Tables[0];
                DataTable dttt = CaicAmount(dtSales, dtAll);
                Form fr = new frWorkList(_materielID, _brandID, gridView1.GetFocusedRowCellDisplayText(_coMaterielID), gridView1.GetFocusedRowCellDisplayText(_coBrandID), dttt);
                fr.ShowDialog();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column == _coIsSelect)
            {
                gridView1.SetFocusedRowCellValue(_coIsSelect, e.Value);
                IDS = string.Empty;
                int MaterielID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coMaterielID));
                int BrandID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coBrandID));
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (Convert.ToInt32(gridView1.GetRowCellValue(i, _coMaterielID)) == MaterielID && Convert.ToInt32(gridView1.GetRowCellValue(i, _coBrandID)) == BrandID && Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsSelect)))
                    {
                        IDS += gridView1.GetRowCellDisplayText(i, _coID) + ",";
                    }
                    if (Convert.ToInt32(gridView1.GetRowCellValue(i, _coMaterielID)) != MaterielID || Convert.ToInt32(gridView1.GetRowCellValue(i, _coBrandID)) != BrandID)
                    {
                        gridView1.SetRowCellValue(i, _coIsSelect, false);
                    }
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView2.ExportToXls(fileName);
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
        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_isMateriel)
            {
                XtraMessageBox.Show("只有按款号查询，才可以全选");
                return;
            }
            IDS = string.Empty;
            if (gridView1.RowCount > 0)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.SetRowCellValue(i, _coIsSelect, true);
                    IDS += gridView1.GetRowCellDisplayText(i, _coID) + ",";
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            amountList1.Ex2Excel(fileName);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex > 2)
                return;
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";

            DataTable dtOne = new DataTable();
            dtOne.TableName = "Main";
            dtOne.Columns.Add("编号", typeof(string));
            dtOne.Columns.Add("客户", typeof(string));
            dtOne.Columns.Add("说明", typeof(string));
            dtOne.Columns.Add("制单", typeof(string));
            dtOne.Columns.Add("审核", typeof(string));
            dtOne.Columns.Add("日期", typeof(string));
            DataRow drOne = dtOne.NewRow();
            drOne[0] = string.Empty;
            drOne[1] = string.Empty;
            drOne[2] = string.Empty;
                drOne[3] = string.Empty;
                drOne[4] = string.Empty;
                drOne[5] = string.Empty;
            dtOne.Rows.Add(drOne);
            ds.Tables.Add(dtOne);

            DataTable dtTwo = new DataTable();
            dtTwo.TableName = "List";
            dtTwo.Columns.Add("编号", typeof(string));
            dtTwo.Columns.Add("款号", typeof(string));
            dtTwo.Columns.Add("商标", typeof(string));
            dtTwo.Columns.Add("数量", typeof(decimal));
            dtTwo.Columns.Add("单价", typeof(decimal));
            dtTwo.Columns.Add("金额", typeof(decimal));
            dtTwo.Columns.Add("货期", typeof(string));
            dtTwo.Columns.Add("单位", typeof(string));
            dtTwo.Columns.Add("ID", typeof(int));
            dtTwo.Columns.Add("客户", typeof(string));
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow drTwo = dtTwo.NewRow();
                drTwo[0] = gridView1.GetRowCellDisplayText(i, _coNum);
                drTwo[1] = gridView1.GetRowCellDisplayText(i, _coMaterielID);
                drTwo[2] = gridView1.GetRowCellDisplayText(i, _coBrandID);
                if (radioGroup1.SelectedIndex == 0)
                    drTwo[3] = gridView1.GetRowCellValue(i, _coAmount);
                else if (radioGroup1.SelectedIndex == 1)
                    drTwo[3] = gridView1.GetRowCellValue(i, _coNotAmount);
                drTwo[4] = gridView1.GetRowCellValue(i, _coPrice);
                if( gridView1.GetRowCellValue(i, _coMoney)!=null)
                    drTwo[5] = gridView1.GetRowCellValue(i, _coMoney);
                else
                drTwo[5] = DBNull.Value;//
                drTwo[6] = gridView1.GetRowCellDisplayText(i, _coLastDate);
                drTwo[7] = gridView1.GetRowCellDisplayText(i, _coMeasureID);
                drTwo[8] = gridView1.GetRowCellValue(i, _coID);
                dtTwo.Rows.Add(drTwo);
            }
            ds.Tables.Add(dtTwo);

            DataTable dtThree = new DataTable();
            if (dtTwo.Rows.Count > 0)
                dtThree = BasicClass.OrderTask.GetSalesList(Convert.ToInt32(dtTwo.Rows[0]["ID"]), (int)BasicClass.Enums.TableType.SalesOne,radioGroup1.SelectedIndex+1);
            if (dtTwo.Rows.Count > 1)
            {
                for (int i = 1; i < dtTwo.Rows.Count; i++)
                {
                    DataTable dtTem = BasicClass.OrderTask.GetSalesList(Convert.ToInt32(dtTwo.Rows[i]["ID"]), (int)BasicClass.Enums.TableType.SalesOne, radioGroup1.SelectedIndex + 1);
                    for (int j = 0; j < dtTem.Rows.Count; j++)
                    {
                        try
                        {
                            dtThree.Rows.Add(dtTem.Rows[j].ItemArray);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            dtThree.TableName = "Info";
            ds.Tables.Add(dtThree);
            BaseForm.PrintClass.PrintSalesAlllist(ds, true);
        }
    }
}