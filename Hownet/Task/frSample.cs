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
using Hownet.BaseContranl;
using System.Reflection;

namespace Hownet.Task
{
    public partial class frSample : DevExpress.XtraEditors.XtraForm
    {

        public frSample()
        {

            InitializeComponent();
        }
        int _MainID = 0;
        int _ParentID = -1;
        DataTable dtMain = new DataTable();
        public frSample(DataTable dt)
            : this()
        {
            dtMain = dt;
        }
        BasicClass.cResult cRR = new BasicClass.cResult();
        public frSample(BasicClass.cResult rr, int ID)
            : this()
        {
            cRR = rr;
            _ParentID = ID;
        }
        public frSample(BasicClass.cResult rr, int MainID, int a)
            : this()
        {
            cRR = rr;
            _MainID = MainID;
        }
        BindingSource bs = new BindingSource();
        private string bllS = "Hownet.BLL.Sample";
        private string bllCA = "Hownet.BLL.SampleColorAmount";
        private string bllSML = "Hownet.BLL.SampleMaterielList";
        bool _isVerify = false;
        string per = string.Empty;
        DataTable dtS = new DataTable();
        DataTable dtCA = new DataTable();
        DataTable dtSML = new DataTable();
        DataTable dtCom = new DataTable();
        int _companyID = 0, _materielID = 0;
        private void XtraForm1_Load(object sender, EventArgs e)
        {

            per = BasicClass.BasicFile.GetPermissions(this.Text);
            ShowData();
            if (_MainID == 0)
            {
                InData();
                bs.PositionChanged += new EventHandler(bs_PositionChanged);
                if (dtMain.Rows.Count > 0)
                    bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_MainID);
                bs.DataSource = dtMain;
                if (dtMain.Rows.Count > 0)
                    bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
                _brFrist.Enabled = _brPrv.Enabled = _brNext.Enabled = _brLast.Enabled = _brAddNew.Enabled = false;
            }
            if (_ParentID > 0)
            {
                _brSave.Enabled = false;
            }
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _barVerify.Enabled = _barUnVerify.Enabled = _brAddNew.Enabled = _brDel.Enabled = _brSave.Enabled = false;
            //}

            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _brDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barUnVerify.Visibility = _barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        void ShowData()
        {
            dtCom = BasicClass.BaseTableClass.dtCompany.Copy();
            DataRow dr = dtCom.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtCom.Rows.InsertAt(dr, 0);
            _leCompanyID.Properties.DataSource = dtCom;

            DataTable dtMateriel = BasicClass.BaseTableClass.dtFinished.Copy();
            DataRow drr = dtMateriel.NewRow();
            drr["ID"] = 0;
            drr["Name"] = string.Empty;
            dtMateriel.Rows.InsertAt(drr, 0);
            _leMaterielID.Properties.DataSource = dtMateriel;
        }
        /// <summary>
        /// 读取dtMain，
        /// </summary>
        void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(bllS, "GetIDList", null).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;
        }
        /// <summary>
        /// 显示详细记录
        /// </summary>
        /// <param name="p"></param>
        void ShowView(int p)
        {
            #region 移动按钮
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                _brSave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _brDel.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
                _barVerify.Enabled = false;
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;
            _brNext.Enabled = true;
            _brLast.Enabled = true;
            if (bs.Position == 0)
            {
                _brFrist.Enabled = false;
                _brPrv.Enabled = false;
            }
            if (bs.Position == dtMain.Rows.Count - 1)
            {
                _brNext.Enabled = false;
                _brLast.Enabled = false;
            }
            #endregion
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtS = BasicClass.GetDataSet.GetDS(bllS, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];

            }
            else
            {
                _MainID = 0;
                dtS = BasicClass.GetDataSet.GetDS(bllS, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtS.NewRow();
                DateTime dtNow = BasicClass.GetDataSet.GetDateTime();
                for (int i = 0; i < dtS.Columns.Count; i++)
                {
                    if (dtS.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = string.Empty;
                    }
                    else if (dtS.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }
                    else if (dtS.Columns[i].DataType == System.Type.GetType("System.DateTime"))
                    {
                        dr[i] = dtNow.AddDays(2);
                    }
                }
                dtS.Rows.Add(dr);
                _brAddNew.Enabled = false;
            }
            _teStrNum.EditValue = dtS.Rows[0]["StrNum"];
            _teNum.EditValue = dtS.Rows[0]["Num"];
            _teTitlle.EditValue = dtS.Rows[0]["Titlle"];
            _teSeriesName.EditValue = dtS.Rows[0]["SeriesName"];
            _teProductName.EditValue = dtS.Rows[0]["ProductName"];
            _teTypeName.EditValue = dtS.Rows[0]["TypeName"];
            _teFillManName.EditValue = dtS.Rows[0]["FillManName"];
            _teVerifyName.EditValue = dtS.Rows[0]["VerifyName"];
            _meRemark.EditValue = dtS.Rows[0]["Remark"];
            _leCompanyID.EditValue = _companyID = Convert.ToInt32(dtS.Rows[0]["CompanyID"]);
            _leMaterielID.EditValue = _materielID = Convert.ToInt32(dtS.Rows[0]["MaterielID"]);
            _deFillDate.EditValue = dtS.Rows[0]["FillDate"];
            _deLastDate.EditValue = dtS.Rows[0]["LastDate"];
            _deOpenDate.EditValue = dtS.Rows[0]["OpenDate"];
            _dePlanDate.EditValue = dtS.Rows[0]["PlanDate"];
            _deStockBackDate.EditValue = dtS.Rows[0]["StockBackDate"];
            _deTechDate.EditValue = dtS.Rows[0]["TechDate"];
            _deVerifyDate.EditValue = dtS.Rows[0]["VerifyDate"];
            _isVerify = (Convert.ToInt32(dtS.Rows[0]["VerifyID"]) == 3);
            dtCA = BasicClass.GetDataSet.GetDS(bllCA, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            if (dtCA.Rows.Count == 0)
            {
                DataRow dr = dtCA.NewRow();
                for (int i = 0; i < dtCA.Columns.Count; i++)
                {
                    if (dtCA.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = string.Empty;
                    }
                    else if (dtCA.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }

                }
                dr["ID"] = 0;
                dr["A"] = 3;
                dr["MainID"] = _MainID;
                dtCA.Rows.Add(dr);
                dtCA.Rows.Add(dr.ItemArray);
                dtCA.Rows.Add(dr.ItemArray);
                dtCA.Rows.Add(dr.ItemArray);
                dtCA.Rows.Add(dr.ItemArray);
                dtCA.Rows.Add(dr.ItemArray);
                dtCA.Rows[0]["ColorName"] = dtCA.Rows[2]["ColorName"] = dtCA.Rows[4]["ColorName"] = "颜色名";
                dtCA.Rows[0]["PantoneName"] = dtCA.Rows[2]["PantoneName"] = dtCA.Rows[4]["PantoneName"] = "潘通色号";
                dtCA.Rows[0]["CupName"] = dtCA.Rows[2]["CupName"] = dtCA.Rows[4]["CupName"] = "杯型";
                dtCA.Rows[0]["Size8Name"] = dtCA.Rows[2]["Size8Name"] = dtCA.Rows[4]["Size8Name"] = @"数量\尺码";
            }
            gridControl2.DataSource = dtCA;

            dtSML = BasicClass.GetDataSet.GetDS(bllSML, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            if (!_isVerify)
            {
                DataRow dr = dtSML.NewRow();
                for (int i = 0; i < dtSML.Columns.Count; i++)
                {
                    if (dtSML.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = string.Empty;
                    }
                    else if (dtSML.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }
                }
                dr["A"] = 3;
                dr["MainID"] = _MainID;
                dtSML.Rows.Add(dr.ItemArray);
                dtSML.Rows.Add(dr.ItemArray);
            }

            gridControl1.DataSource = dtSML;
            gridView2.OptionsBehavior.Editable = gridView1.OptionsBehavior.Editable = !_isVerify;
            _barVerify.Enabled = _brSave.Enabled = _brDel.Enabled = !_isVerify;
            _barUnVerify.Enabled = _isVerify;
            _brAddNew.Enabled = (_MainID > 0);
        }

        #region 记录移动
        /// <summary>
        /// 首记录
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MoveFirst();
        }
        /// <summary>
        /// 上一条
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MovePrevious();
        }
        /// <summary>
        /// 下一条
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MoveNext();
        }
        /// <summary>
        /// 尾记录
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            bs.MoveLast();
        }
        /// <summary>
        /// 新单
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ParentID = 0;
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Save(false))
            {
                _barUnVerify.Enabled = _isVerify;

                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = !_isVerify;
                _brAddNew.Enabled = _MainID > 0;
                XtraMessageBox.Show("保存成功！");
            }
        }
        private bool Save(bool IsVerify)
        {

            dtS.Rows[0]["StrNum"] = _teStrNum.EditValue;
            dtS.Rows[0]["Num"] = _teNum.EditValue;
            dtS.Rows[0]["Titlle"] = _teTitlle.EditValue;
            dtS.Rows[0]["SeriesName"] = _teSeriesName.EditValue;
            dtS.Rows[0]["ProductName"] = _teProductName.EditValue;
            dtS.Rows[0]["TypeName"] = _teTypeName.EditValue;
            dtS.Rows[0]["FillManName"] = _teFillManName.EditValue;
            dtS.Rows[0]["VerifyName"] = _teVerifyName.EditValue;
            dtS.Rows[0]["Remark"] = _meRemark.EditValue;
            dtS.Rows[0]["CompanyID"] = _leCompanyID.EditValue;
            dtS.Rows[0]["MaterielID"] = _leMaterielID.EditValue;
            dtS.Rows[0]["FillDate"] = _deFillDate.EditValue;
            dtS.Rows[0]["LastDate"] = _deLastDate.EditValue;
            dtS.Rows[0]["OpenDate"] = _deOpenDate.EditValue;
            dtS.Rows[0]["PlanDate"] = _dePlanDate.EditValue;
            dtS.Rows[0]["StockBackDate"] = _deStockBackDate.EditValue;
            dtS.Rows[0]["TechDate"] = _deTechDate.EditValue;
            dtS.Rows[0]["VerifyDate"] = _deVerifyDate.EditValue;
            dtS.Rows[0]["CompanyName"] = _leCompanyID.Text;
            dtS.Rows[0]["MaterielName"] = _leMaterielID.Text;
            if (IsVerify)
                dtS.Rows[0]["VerifyID"] = 3;
            else
                dtS.Rows[0]["VerifyID"] = 0;
            if (_MainID == 0)
            {
                dtS.Rows[0]["ID"] = _MainID = BasicClass.GetDataSet.Add(bllS, dtS);
            }
            //ucSampleMC1.Save(_MainID);
            //ucSampleMC2.Save(_MainID);
            //ucSampleMC3.Save(_MainID);
            gridView1.CloseEditForm();
            gridView1.UpdateCurrentRow();
            dtSML.AcceptChanges();
            int a = 0;
            DataTable dtTem = dtSML.Clone();
            for (int i = 0; i < dtSML.Rows.Count - 2; i++)
            {
                a = Convert.ToInt32(dtSML.Rows[i]["A"]);
                if (a > 1)
                {
                    dtTem.Rows.Clear();
                    dtSML.Rows[i]["MainID"] = _MainID;
                    dtTem.Rows.Add(dtSML.Rows[i].ItemArray);
                    if (a == 2)
                    {
                        BasicClass.GetDataSet.UpData(bllSML, dtTem);
                    }
                    else if (a == 3)
                    {
                        dtSML.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllSML, dtTem);
                    }
                    dtSML.Rows[i]["A"] = 1;
                }
            }
            dtTem = dtCA.Clone();
            for (int i = 0; i < dtCA.Rows.Count; i++)
            {
                a = Convert.ToInt32(dtCA.Rows[i]["A"]);
                if (a > 1)
                {
                    dtTem.Rows.Clear();
                    dtCA.Rows[i]["MainID"] = _MainID;
                    dtTem.Rows.Add(dtCA.Rows[i].ItemArray);
                    if (a == 2)
                    {
                        BasicClass.GetDataSet.UpData(bllCA, dtTem);
                    }
                    else if (a == 3)
                    {
                        dtCA.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bllCA, dtTem);
                    }
                    dtCA.Rows[i]["A"] = 1;
                }
            }
            _isVerify = IsVerify;
            return true;
        }

        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.No == XtraMessageBox.Show("确认审核本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            if (Save(true))
            {
                _barUnVerify.Enabled = _isVerify;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = !_isVerify;
                _brAddNew.Enabled = _MainID > 0;
                XtraMessageBox.Show("审核成功！");
            }

        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.No == XtraMessageBox.Show("确认弃审本单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            if (Save(false))
            {
                _barUnVerify.Enabled = _isVerify;
                _brSave.Enabled = _barVerify.Enabled = _brDel.Enabled = !_isVerify;
                _brAddNew.Enabled = _MainID > 0;
                XtraMessageBox.Show("审核成功！");
            }
        }

        private void _brDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (_MainID > 0)
                {
                    BasicClass.GetDataSet.ExecSql(bllCA, "DeleteByMainID", new object[] { _MainID });
                    BasicClass.GetDataSet.ExecSql(bllSML, "DeleteByMainID", new object[] { _MainID });
                    BasicClass.GetDataSet.ExecSql(bllS, "Delete", new object[] { _MainID });
                }
                InData();
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowView(0);
            }
        }



        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print(false);
        }

        private void _leCompanyID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                Form fr = new BaseForm.frCompany();
                fr.ShowDialog();
                dtCom = BasicClass.BaseTableClass.dtCompany.Copy();
                DataRow dr = dtCom.NewRow();
                dr["ID"] = 0;
                dr["Name"] = string.Empty;
                dtCom.Rows.InsertAt(dr, 0);
                _leCompanyID.Properties.DataSource = dtCom;
                _leCompanyID.EditValue = _companyID;
            }
        }

        private void _leMaterielID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                Form fr = new BaseForm.frFinished();
                fr.ShowDialog();
                DataTable dtMateriel = BasicClass.BaseTableClass.dtFinished.Copy();
                DataRow drr = dtMateriel.NewRow();
                drr["ID"] = 0;
                drr["Name"] = string.Empty;
                dtMateriel.Rows.InsertAt(drr, 0);
                _leMaterielID.Properties.DataSource = dtMateriel;
                _leMaterielID.EditValue = _materielID;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1))
            {
                gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow dr = dtSML.NewRow();
                for (int i = 0; i < dtSML.Columns.Count; i++)
                {
                    if (dtSML.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        dr[i] = string.Empty;
                    }
                    else if (dtSML.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        dr[i] = 0;
                    }
                }
                dr["A"] = 3;
                dr["MainID"] = _MainID;
                dtSML.Rows.Add(dr.ItemArray);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = e.FocusedRowHandle < gridView1.RowCount - 1 && !_isVerify;
        }

        private void _leCompanyID_EditValueChanged(object sender, EventArgs e)
        {
            _companyID = Convert.ToInt32(_leCompanyID.EditValue);
            dtS.Rows[0]["CompanyName"] = _leCompanyID.Text;
            if (_companyID > 0)
            {
                DataRow[] drs = dtCom.Select("(ID=" + _companyID + ")");

                if (drs.Length > 0)
                    _teStrNum.EditValue = drs[0]["SN"];
            }
            else
            {
                _teStrNum.EditValue = string.Empty;
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!_isVerify)
            {
                if ((e.FocusedRowHandle % 2 == 0 && gridView2.FocusedColumn.VisibleIndex < 4) || (e.FocusedRowHandle % 2 == 1 && gridView2.FocusedColumn.VisibleIndex == 3))
                    gridView2.OptionsBehavior.Editable = false;
                else
                    gridView2.OptionsBehavior.Editable = true;
            }
        }

        private void gridView2_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!_isVerify)
            {
                if ((gridView2.FocusedRowHandle % 2 == 0 && e.FocusedColumn.VisibleIndex < 4) || (gridView2.FocusedRowHandle % 2 == 1 && e.FocusedColumn.VisibleIndex == 3))
                    gridView2.OptionsBehavior.Editable = false;
                else
                    gridView2.OptionsBehavior.Editable = true;
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1))
            {
                gridView1.SetFocusedRowCellValue(_coA, 2);
            }
            if (e.Value == DBNull.Value)
                return;
            int aa = 0;
            if (e.Column.VisibleIndex > 3 && e.RowHandle % 2 == 1)
            {
                try
                {
                    aa = Convert.ToInt32(e.Value);
                }
                catch
                {
                    XtraMessageBox.Show("数量只能填整数");
                    gridView2.SetFocusedValue(DBNull.Value);
                    return;
                }
            }
            aa = 0;
            if (e.Column.VisibleIndex > 3 && e.RowHandle % 2 == 1)
            {
                for (int i = 4; i < 9; i++)
                {
                    try
                    {
                        aa += Convert.ToInt32(gridView2.GetFocusedRowCellValue(gridView2.VisibleColumns[i]));
                    }
                    catch
                    {

                    }
                    gridView2.SetFocusedRowCellValue(_coSize8Name, aa);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print(true);
        }
        private void Print(bool IsDesign)
        {
            gridView2.CloseEditor();
            gridView2.UpdateCurrentRow();
            dtCA.AcceptChanges();

            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtSML.AcceptChanges();

            DataSet ds = new DataSet();
            ds.DataSetName = "ds";
            ds.Tables.Add(dtS.Copy());
            ds.Tables[0].TableName = "Main";
            ds.Tables.Add(dtCA.Copy());
            ds.Tables[1].TableName = "CA";
            ds.Tables.Add(dtSML.Copy());
            ds.Tables[2].TableName = "SML";
            BaseForm.PrintClass.PrintSamply(ds, IsDesign);
        }

        private void _leMaterielID_EditValueChanged(object sender, EventArgs e)
        {
            _materielID = Convert.ToInt32(_leMaterielID.EditValue);
            dtS.Rows[0]["MaterielName"] = _leMaterielID.Text;
        }
    }
}