using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace Hownet.BaseContranl
{
    public partial class AmountList : DevExpress.XtraEditors.XtraUserControl
    {
        public AmountList()
        {
            InitializeComponent();
        }
        public delegate void EditValueChangedHandler(object val, string text);
        public event EditValueChangedHandler EditValueChanged;

        //Hownet.Model.Color modC = new Hownet.Model.Color();
        //Hownet.Model.Size modS = new Hownet.Model.Size();
        ArrayList _liS = new ArrayList();
        private DataTable _dtSize = new DataTable();
        private DataTable _dtColor = new DataTable();
        /// <summary>
        /// 颜色是否有重复
        /// </summary>
        private bool _checkColor = false;
        /// <summary>
        /// 是否被修改
        /// </summary>
        private bool _IsEdit = false;
        private int _mainID;
        private int _tableTypeID;
        private int _materielID;
        
        private DataTable dt = new DataTable();
        /// <summary>
        /// 是否允许修改
        /// </summary>
        private bool _isCanEdit = false;
        private bool _isShowPopupMenu = false;
        private bool _isShowToolTip = false;
        private bool _isCanEditCS = true;
        string bllOT = "Hownet.BLL.BaseFile.OrderTask";
        string bllC = "Hownet.BLL.Color";
        string bllS = "Hownet.BLL.Size";
        /// <summary>
        /// 是否显示右键菜单
        /// </summary>
        public bool IsShowPopupMenu
        {
            set
            {
                _isShowPopupMenu = value;
            }
        }
        /// <summary>
        /// 是否显示ToolTip
        /// </summary>
        public bool IsShowToolTip
        {
            set
            {
                _isShowToolTip = value;
            }
        }
        public bool CheckColor
        {
            get
            {
                _checkColor = checkColor();
                return _checkColor;
            }
        }
        public int SumAmount
        {
            get
            {
                int amount = 0;
                try
                {
                    gridView1.CloseEditor();
                    amount = int.Parse(gridView1.GetRowCellValue(gridView1.RowCount - 1, gridView1.VisibleColumns[gridView1.Columns.Count - 3]).ToString());
                }
                catch (Exception ex)
                {
                }
                return amount;
            }
        }
        public bool IsEdit
        {
            set { _IsEdit = value; }
            get { return _IsEdit; }
        }
        public bool IsCanEdit
        {
            set
            {
                _isCanEdit = value;
                gridView1.OptionsBehavior.Editable = _isCanEdit;
            }
            get { return _isCanEdit; }
        }
        public bool IsCanEditCS
        {
            set
            {
                _isCanEditCS = value;
            }
            get
            {
                return _isCanEditCS;
            }
        }
        public int MaterielID
        {
            set { _materielID = value; }
            get { return _materielID; }
        }
        public void Open(int mainID, int TableTypeID, bool t,int AmountTypeID)
        {
            gridView1.Columns.Clear();
            _mainID = mainID;
            _tableTypeID = TableTypeID;
            this.gridView1.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            dt = BasicClass.GetDataSet.GetDS(bllOT, "ShowTemInfo", new object[] { _mainID, _tableTypeID, t ,AmountTypeID}).Tables[0];
            dt.TableName = "tableName";
            gridControl1.DataSource = dt;
            FormOpen.SunRow(dt);
            FormOpen.ReSumTable(dt);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            _isCanEdit = false;
            GetColorSize();
            SetWidth();
        }
        public DataTable dtDataSource
        {
            get
            {
                return (DataTable)(gridControl1.DataSource);
            }
        }
        public void Open(int mainID, int TableTypeID, bool t,DataTable dtt)
        {
            gridView1.Columns.Clear();
            GetColorSize();
            _mainID = mainID;
            dt = dtt;
            _tableTypeID = TableTypeID;
            this.gridView1.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            dtt.TableName = "tableName";
            gridControl1.DataSource = dt;
            //FormOpen.SunRow(dt);
            //FormOpen.ReSumTable(dt);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            _isCanEdit = false;
            _IsEdit = true;
            gridView1.OptionsBehavior.Editable = false;
            SetWidth();
        }
        public void ClearData()
        {
            gridView1.Columns.Clear();
           //gridControl1.DataSource = null;
        }
        public void Open( bool t, DataTable dtt)
        {
            gridView1.Columns.Clear();
            GetColorSize();
            dt = AmountListClass.GetList(dtt);
            this.gridView1.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            dtt.TableName = "tableName";
            gridControl1.DataSource = dt;
            FormOpen.SunRow(dt);
            FormOpen.ReSumTable(dt);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            _isCanEdit = t;
            _IsEdit = true;
            gridView1.OptionsBehavior.Editable = t;
            SetWidth();
        }
        private void SetWidth()
        {
            for (int i = 1; i < gridView1.VisibleColumns.Count - 3; i++)
            {
                gridView1.VisibleColumns[i].Width = 50;
            }
        }
        public void EndEdit()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
        }
        public void ShowInfo(int MaterielID, int BrandID, bool t, DataTable dtt)
        {
            gridView1.Columns.Clear();
            GetColorSize();
            _mainID = MaterielID;
            dt = dtt;
            _tableTypeID = BrandID;
            this.gridView1.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            dtt.TableName = "tableName";
            gridControl1.DataSource = dt;
            FormOpen.SunRow(dt);
            FormOpen.ReSumTable(dt);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            _isCanEdit = false;
            _IsEdit = true;
            gridView1.OptionsBehavior.Editable = t;
        }
        public void SetValue(string ColorName, string ColorOneName, string ColorTwoName, string SizeName, int Vale)
        {
            int c = gridView1.VisibleColumns.Count;
            int r = 0;
            for (int i = 1; i < gridView1.RowCount - 1; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, gridView1.VisibleColumns[0]).Trim() == ColorName &&
                    gridView1.GetRowCellDisplayText(i, gridView1.VisibleColumns[c - 2]).Trim() == ColorOneName &&
                    gridView1.GetRowCellDisplayText(i, gridView1.VisibleColumns[c - 1]).Trim() == ColorTwoName)
                {
                    r = i;
                    break;
                }
            }
            for (int i = 1; i < c - 3; i++)
            {
                if (gridView1.GetRowCellDisplayText(0, gridView1.VisibleColumns[i]).Trim() == SizeName)
                {
                    c = i;
                    break;
                }
            }
            if (r > 0)
            {
                object o = gridView1.GetRowCellValue(r, gridView1.VisibleColumns[c]);
                if (o != DBNull.Value)
                    Vale += Convert.ToInt32(o);
                gridView1.SetRowCellValue(r, gridView1.VisibleColumns[c], Vale);
            }
        }
        public void Save(int MainID, int materielID, int BrandID)
        {
            _mainID = MainID;
            _materielID = materielID;
            gridView1.UpdateCurrentRow();
            gridView1.CloseEditor();
            dt.AcceptChanges();
            if (_IsEdit)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt.Copy());
                byte[] bb = ZipJpg.Ds2Byte(ds);
                object[] o = new object[] {bb,_mainID,_materielID,_tableTypeID,BrandID };
                BasicClass.GetDataSet.ExecSql(bllOT, "SaveTemInfo", o);

                //DataTable dtt = dt.Copy();
                //BasicClass.GetDataSet.ftc.ExecSql(bllOT, "SaveTemInfo", new object[] { dtt });
                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt.Copy());
                //BasicClass.GetDataSet.ftc.ExecSql(bllOT, "SaveTemInfo", new object[] { ds });
            }
            _IsEdit = false;
        }
        public void Save(int MainID, int materielID, int BrandID,int TableTypeID)
        {
            _tableTypeID = TableTypeID;
            _mainID = MainID;
            _materielID = materielID;
            gridView1.UpdateCurrentRow();
            gridView1.CloseEditor();
            dt.AcceptChanges();
            if (_IsEdit)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt.Copy());
                byte[] bb = ZipJpg.Ds2Byte(ds);
                object[] o = new object[] { bb, _mainID, _materielID, _tableTypeID, BrandID };
                BasicClass.GetDataSet.ExecSql(bllOT, "SaveTemInfo", o);

                //DataTable dtt = dt.Copy();
                //BasicClass.GetDataSet.ftc.ExecSql(bllOT, "SaveTemInfo", new object[] { dtt });
                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt.Copy());
                //BasicClass.GetDataSet.ftc.ExecSql(bllOT, "SaveTemInfo", new object[] { ds });
            }
            _IsEdit = false;
        }
        public DataTable GetDTList()
        {
            gridView1.UpdateCurrentRow();
            gridView1.CloseEditor();
            dt.AcceptChanges();
            DataTable dttt = new DataTable();
            dttt.TableName = "dtAmount";
            dttt.Columns.Add("ColorID", typeof(int));
            dttt.Columns.Add("ColorOneID", typeof(int));
            dttt.Columns.Add("ColorTwoID", typeof(int));
            dttt.Columns.Add("SizeID", typeof(int));
            dttt.Columns.Add("Amount", typeof(int));
            DataRow dr;
            for (int rr = 1; rr < dt.Rows.Count - 1; rr++)
            {
                if (dt.DefaultView[rr]["Color"].ToString() != string.Empty)
                {

                    for (int c = 1; c < dt.Columns.Count - 3; c++)
                    {
                        if (dt.DefaultView[0][c].ToString() != string.Empty)
                        {
                            if (dt.DefaultView[rr][c].ToString()!=string.Empty&& dt.DefaultView[rr][c] != DBNull.Value && Convert.ToInt32(dt.DefaultView[rr][c]) > 0)
                            {
                                dr = dttt.NewRow();
                                dr["ColorID"] = _dtColor.Select("(Name='" + dt.Rows[rr]["Color"] + "')")[0]["ID"];
                                if (dt.Rows[rr]["ColorOne"].ToString() != string.Empty)
                                    dr["ColorOneID"] = _dtColor.Select("(Name='" + dt.Rows[rr]["ColorOne"] + "')")[0]["ID"];
                                else
                                    dr["ColorOneID"] = 0;
                                if (dt.Rows[rr]["ColorTwo"].ToString() != string.Empty)
                                    dr["ColorTwoID"] = _dtColor.Select("(Name='" + dt.Rows[rr]["ColorTwo"] + "')")[0]["ID"];
                                else
                                    dr["ColorTwoID"] = 0;
                                dr["Amount"] = dt.DefaultView[rr][c];
                                dr["SizeID"] = _dtSize.Select("(Name='" + dt.DefaultView[0][c] + "')")[0]["ID"];
                                dttt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            return dttt;
        }
        public DataTable GetSizeList()
        {
            DataTable dttttt = new DataTable();
            dttttt.Columns.Add("ID", typeof(int));
            for (int c = 1; c < dt.Columns.Count - 3; c++)
            {
                if (dt.DefaultView[0][c].ToString() != string.Empty)
                {
                    dttttt.Rows.Add(_dtSize.Select("(Name='" + dt.DefaultView[0][c] + "')")[0]["ID"]);
                }
            }
            return dttttt;
        }
        public void GetColorSize()
        {
            _dtColor = BasicClass.BaseTableClass.dtColor;// BasicClass.GetDataSet.GetDS(bllC, "GetAllList", null).Tables[0];
            _dtSize = BasicClass.BaseTableClass.dtSize;// BasicClass.GetDataSet.GetDS(bllS, "GetAllList", null).Tables[0];
            AddReSizeItems();
            AddColor();
        }
        void AddColor()
        {
            _reColor.Items.Clear();
            for (int r = 0; r < _dtColor.Rows.Count; r++)
            {
                _reColor.Items.Add(_dtColor.Rows[r]["Name"]);
            }
        }
       public int GetRows()
        {
            return gridView1.RowCount;
        }
        void AddReSizeItems()
        {
            _reSize.Items.Clear();
            _liS.Clear();
            for (int r = 0; r < _dtSize.Rows.Count; r++)
            {
                _reSize.Items.Add(_dtSize.Rows[r]["Name"]);
                _liS.Add(_dtSize.Rows[r]["Name"].ToString());
            }
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                try
                {
                    string tem = gridView1.GetFocusedRowCellValue(gridView1.Columns[i]).ToString();

                    if (i != gridView1.FocusedColumn.ColumnHandle && tem != "")
                    {

                        _reSize.Items.Remove(gridView1.GetFocusedRowCellValue(gridView1.Columns[i]));
                    }
                }
                catch { }
            }
        }
        private bool checkColor()
        {
            bool aa = false;
            ArrayList liTem = new ArrayList();
            for (int i = 1; i < gridView1.RowCount - 2; i++)
            {
                if (gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString() != "")
                {
                    liTem.Add(gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString() + gridView1.GetRowCellValue(i, gridView1.Columns[gridView1.Columns.Count - 2]).ToString()
                        + gridView1.GetRowCellValue(i, gridView1.Columns[gridView1.Columns.Count - 1]).ToString());
                }
            }
            for (int i = 0; i < liTem.Count; i++)
            {
                int n = i + 1;
                for (int j = n; j < liTem.Count; j++)
                {
                    if (liTem[i].ToString() == liTem[j].ToString())
                    {
                        aa = true;
                        return aa;
                    }
                }
            }
            return aa;
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.RowHandle != 0 && e.RowHandle != gridView1.RowCount - 1)
                {
                    if (e.Column.ColumnHandle == 0 || e.Column.ColumnHandle == gridView1.Columns.Count - 2 || e.Column.ColumnHandle == gridView1.Columns.Count - 1)
                    {
                        if (e.Value.ToString().Trim() != "" && _reColor.Items.IndexOf(e.Value.ToString().Trim()) == -1)
                        {
                            if (DialogResult.Yes == XtraMessageBox.Show("所填內容不在已有列表中，是否新增？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                DataTable dtTem = _dtColor.Clone();
                                DataRow dr = dtTem.NewRow();
                                dr["ID"] = 0;
                                dr["Name"] = e.Value.ToString().Trim();
                                dr["Value"] = 0;
                                dr["IsEnd"] = 0;
                                dr["IsUse"] = true;
                                dr["ColorTypeID"] = 0;
                                dr["Sn"] = (int.Parse(_dtColor.Rows[_dtColor.Rows.Count - 1]["Sn"].ToString()) + 1).ToString().PadLeft(3, '0');
                                dr["A"] = 1;
                                dtTem.Rows.Add(dr);
                                dtTem.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bllC, dtTem);
                                _dtColor.Rows.Add(dtTem.Rows[0].ItemArray);
                                _reColor.Items.Add(e.Value.ToString().Trim());
                                return;
                            }
                            else
                            {
                                gridView1.SetFocusedValue("");
                            }
                        }
                    }
                }
                if (e.RowHandle == 0)
                {
                    if (e.Value.ToString().Trim() != "" && _reSize.Items.IndexOf(e.Value.ToString().Trim()) == -1)
                    {
                        if (_liS.IndexOf(e.Value.ToString().Trim()) == -1)
                        {
                            if (DialogResult.Yes == XtraMessageBox.Show("所填內容不在已有列表中，是否新增？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                DataTable dtTem = _dtSize.Clone();
                                DataRow dr = dtTem.NewRow();
                                dr["ID"] = 0;
                                dr["Name"] = e.Value.ToString().Trim();
                                dr["IsEnd"] = 0;
                                dr["IsUse"] = true;
                                dr["SizeTypeID"] = 0;
                                dr["Sn"] = string.Empty;// (int.Parse(_dtSize.Rows[_dtSize.Rows.Count - 1]["Sn"].ToString()) + 1).ToString().PadLeft(3, '0');
                                dr["A"] = 1;
                                dtTem.Rows.Add(dr);
                                BasicClass.GetDataSet.Add(bllS, dtTem);
                                _dtSize.Rows.Add(dtTem.Rows[0].ItemArray);
                                _reSize.Items.Add(e.Value.ToString().Trim());
                                _liS.Add(e.Value.ToString().Trim());
                            }
                            else
                            {
                                gridView1.SetFocusedValue("");
                            }
                        }
                        else
                        {
                            gridView1.SetFocusedValue("");
                            return;
                        }
                    }
                }
                if (e.RowHandle == gridView1.RowCount - 2)
                {
                    DataTable dttem = (DataTable)(gridControl1.DataSource);
                    dttem.Rows.RemoveAt(dttem.Rows.Count - 1);
                    dttem.Rows.Add(dttem.NewRow());
                    dttem.Rows.Add(dttem.NewRow());
                    FormOpen.ReSumTable(dttem);
                    gridView1.SetRowCellValue(gridView1.RowCount - 1, gridView1.Columns[0], "合计");
                    return;
                }
                if (e.Column.ColumnHandle == gridView1.Columns.Count - 4)
                {
                    int c = gridView1.Columns.Count;

                    DataTable dttem = (DataTable)(gridControl1.DataSource);
                    dttem.Columns.Add("Columns" + c.ToString(), typeof(string));
                    dttem.Rows[0]["Columns" + c.ToString()] = "";
                    dttem.Columns["Columns" + c.ToString()].SetOrdinal(dttem.Columns.Count - 4);
                    gridView1.Columns.Add();

                    gridView1.Columns[c].FieldName = "Columns" + c.ToString();
                    gridView1.Columns[c].VisibleIndex = c - 3;
                    gridView1.Columns[c].Visible = true;
                    gridView1.Columns[c].Width = gridView1.Columns[1].Width;
                    return;
                }
                if (e.RowHandle != 0 && e.RowHandle != gridView1.RowCount - 1 && e.Column.ColumnHandle > 0 && e.Column.ColumnHandle < gridView1.Columns.Count - 3)
                {
                    DataTable dttem = (DataTable)(gridControl1.DataSource);
                    FormOpen.ReSumTable(dttem);
                }
                _IsEdit = true;
                _isCanEdit = true;
                int sAmount = 0;
                try
                {
                    sAmount = int.Parse(gridView1.GetRowCellValue(gridView1.RowCount - 1, gridView1.Columns[gridView1.Columns.Count - 3]).ToString());
                }
                catch { }
                ChangeVal(sAmount, "总数");
            }
        }
        private void ChangeVal(object s, string text)
        {
            if (EditValueChanged != null)
                EditValueChanged(s, text);
        }
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                if (e.Column.ColumnHandle > 1 && e.Column.ColumnHandle < gridView1.Columns.Count - 3)
                {
                    if (e.RowHandle > 0 && e.RowHandle < gridView1.RowCount - 1)
                    {
                        try
                        {
                            int.Parse(e.Value.ToString());
                        }
                        catch
                        {
                            SendKeys.Send("{Esc}");
                        }
                    }
                }
            }
            GC.Collect();
        }

        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!_isCanEdit)
                return;
            if (e.FocusedColumn == null)
                return;
            gridView1.OptionsBehavior.Editable = SetCanEdit(gridView1.FocusedRowHandle, e.FocusedColumn.ColumnHandle);
            //if (gridView1.FocusedColumn != null)
            //{
            //    int c = e.FocusedColumn.ColumnHandle;
            //    if (gridView1.FocusedRowHandle == 0)
            //    {
            //        if (c > 0 && c < gridView1.Columns.Count - 3)
            //        {
            //            if (gridView1.GetRowCellValue(0, gridView1.Columns[c - 1]).ToString() != "")
            //            {
            //                gridView1.OptionsBehavior.Editable = true;
            //                AddReSizeItems();
            //            }
            //            else
            //                gridView1.OptionsBehavior.Editable = false;
            //        }
            //        else
            //        {
            //            gridView1.OptionsBehavior.Editable = false;
            //        }
            //    }
            //    if (gridView1.FocusedRowHandle > 0 && gridView1.FocusedRowHandle < gridView1.RowCount - 1)
            //    {
            //        if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString() != "" &&
            //            gridView1.GetRowCellValue(0, e.FocusedColumn).ToString() != "")
            //            gridView1.OptionsBehavior.Editable = true;
            //        else
            //            gridView1.OptionsBehavior.Editable = false;
            //        if (gridView1.FocusedRowHandle > 0 && c == 0 && gridView1.GetRowCellValue(gridView1.FocusedRowHandle - 1, e.FocusedColumn).ToString() != "")
            //            gridView1.OptionsBehavior.Editable = true;
            //    }
            //}
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!_isCanEdit)
                return;
            if (e.FocusedRowHandle < 0)
                return;
            gridView1.OptionsBehavior.Editable = SetCanEdit(e.FocusedRowHandle, gridView1.FocusedColumn.VisibleIndex);
            //if (e.FocusedRowHandle == gridView1.RowCount - 1)
            //{
            //    gridView1.OptionsBehavior.Editable = false;
            //    return;
            //}
            //if (gridView1.FocusedRowHandle > -1 && gridView1.FocusedRowHandle < gridView1.RowCount - 1 && gridView1.FocusedColumn != null)
            //{
            //    int c = gridView1.FocusedColumn.ColumnHandle;
            //    if (c == 0)//颜色列
            //    {
            //        //如果上一行不为空，才可以选择新颜色
            //        if (e.FocusedRowHandle > 0 && gridView1.GetRowCellValue(e.FocusedRowHandle - 1, gridView1.FocusedColumn).ToString() != "")
            //            gridView1.OptionsBehavior.Editable = true;
            //        else
            //            gridView1.OptionsBehavior.Editable = false;
            //    }
            //    else
            //    {
            //        //中间数据区，如果有颜色和尺码，才可填写
            //        if (gridView1.GetRowCellValue(e.FocusedRowHandle, gridView1.Columns[0]).ToString() != "" &&
            //            gridView1.GetRowCellValue(0, gridView1.Columns[c]).ToString() != "")
            //            gridView1.OptionsBehavior.Editable = true;
            //        else
            //            gridView1.OptionsBehavior.Editable = false;
            //        if (e.FocusedRowHandle == 0 && gridView1.GetRowCellValue(0, gridView1.Columns[c]).ToString() != "" &&
            //            c < gridView1.Columns.Count - 3)
            //        {
            //            gridView1.OptionsBehavior.Editable = true;
            //            AddReSizeItems();
            //        }
            //    }
            //}
        }
        private bool SetCanEdit(int _RowID, int _ColID)
        {
            try
            {
                if (_ColID == gridView1.VisibleColumns.Count - 3)//顏色名、合計列不能修改
                    return false;
                if (_RowID == gridView1.RowCount - 1) //最后一行不能修改
                    return false;
                if (!_isCanEditCS && (_RowID == 0 || _ColID == 0))
                    return false;
                if (_RowID > 0)
                {
                    object o = gridView1.GetRowCellValue(_RowID - 1, gridView1.VisibleColumns[_ColID]).ToString();
                    if (_ColID == 0)//顏色ID列
                        return (gridView1.GetRowCellValue(_RowID - 1, "Color").ToString() != string.Empty);//需上一行不為空，才能添加新顏色
                    if (_ColID > gridView1.VisibleColumns.Count - 3)//兩個配色
                        return (gridView1.GetFocusedRowCellValue("Color").ToString() != string.Empty);
                    // return ((!gridView1.GetFocusedRowCellValue("ColorID").ToString().Equals("0")) && gridView1.GetRowCellValue(0, gridView1.GetVisibleColumn(_ColID)).ToString() != string.Empty);

                    o = gridView1.GetFocusedRowCellValue("Color");
                    o=!(gridView1.GetFocusedRowCellValue("Color") == null );
                    return ((!(gridView1.GetFocusedRowCellValue("Color").ToString() == string.Empty || gridView1.GetFocusedRowCellValue("Color").ToString().Equals("0"))) && gridView1.GetRowCellValue(0, gridView1.GetVisibleColumn(_ColID)).ToString() != string.Empty);
                }
                else
                {
                    if (_ColID == 0 || _ColID > gridView1.VisibleColumns.Count - 3)
                        return false;
                    if (gridView1.GetFocusedRowCellValue(gridView1.GetVisibleColumn(_ColID - 1)).ToString() != string.Empty)
                    {
                        AddReSizeItems();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (_isCanEdit)
            {
                if (e.RowHandle == 0 && 0 < e.Column.ColumnHandle && e.Column.ColumnHandle < gridView1.Columns.Count - 3)
                {
                    e.RepositoryItem = _reSize;
                }
                if ((e.RowHandle > 0 && e.RowHandle < gridView1.RowCount - 1) && (e.Column.ColumnHandle == 0 || e.Column.ColumnHandle > gridView1.Columns.Count - 3))
                {
                    e.RepositoryItem = _reColor;
                }
            }
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isShowPopupMenu)
            {
                if (e.Button == MouseButtons.Right)
                    DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
            }
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void _barFillRightCell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int a = 0;
            if (gridView1.GetFocusedRowCellValue(gridView1.Columns[1]).ToString() != "")
            {
                a = int.Parse(gridView1.GetFocusedRowCellValue(gridView1.Columns[1]).ToString());
            }
            else
            {
                XtraMessageBox.Show("第一个尺码没有数量!");
                return;
            }
            for (int i = 2; i < gridView1.Columns.Count - 3; i++)
            {
                if (gridView1.GetRowCellValue(0, gridView1.Columns[i]).ToString() != "")
                {
                    gridView1.SetFocusedRowCellValue(gridView1.Columns[i], a);
                }
                else
                    return;
            }
        }

        private void _barFillRightColumns_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int j = 1; j < gridView1.RowCount - 2; j++)
            {
                int a = 0;
                if (gridView1.GetRowCellValue(j, gridView1.VisibleColumns[1]).ToString() != "")
                {
                    a = int.Parse(gridView1.GetRowCellValue(j, gridView1.VisibleColumns[1]).ToString());
                }
                if (a != 0)
                {
                    for (int i = 2; i < gridView1.VisibleColumns.Count - 3; i++)
                    {
                        if (gridView1.GetRowCellValue(0, gridView1.VisibleColumns[i]).ToString() != "")
                        {
                            gridView1.SetRowCellValue(j, gridView1.VisibleColumns[i], a);
                        }
                    }
                }
            }
        }

        private void _barFillBottCell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int a = 0;
            if (gridView1.GetRowCellValue(1, gridView1.FocusedColumn).ToString() != "")
            {
                a = int.Parse(gridView1.GetRowCellValue(1, gridView1.FocusedColumn).ToString());
            }
            if (a != 0)
            {
                for (int i = 2; i < gridView1.RowCount - 2; i++)
                {
                    gridView1.SetRowCellValue(i, gridView1.FocusedColumn, a);
                }
            }
        }

        private void _barFillRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int j = 1; j < gridView1.VisibleColumns.Count - 3; j++)
            {
                int a = 0;
                if (gridView1.GetRowCellValue(1, gridView1.VisibleColumns[j]).ToString() != "")
                {
                    a = int.Parse(gridView1.GetRowCellValue(1, gridView1.VisibleColumns[j]).ToString());
                }
                if (a != 0)
                {
                    for (int i = 2; i < gridView1.RowCount - 2; i++)
                    {
                        gridView1.SetRowCellValue(i, gridView1.VisibleColumns[j], a);
                    }
                }
            }
        }

        private void _barFillOneRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetRowCellValue(1, gridView1.VisibleColumns[1]).ToString() != "")
            {
                //第一行第一列
                int o = int.Parse(gridView1.GetRowCellValue(1, gridView1.VisibleColumns[1]).ToString());
                int a;
                for (int j = 2; j < gridView1.RowCount - 2; j++)
                {
                    a = 0;
                    if (gridView1.GetRowCellValue(j, gridView1.VisibleColumns[1]).ToString() != "")
                    {
                        //第j行第一列
                        a = int.Parse(gridView1.GetRowCellValue(j, gridView1.VisibleColumns[1]).ToString());
                        for (int i = 2; i < gridView1.VisibleColumns.Count - 3; i++)
                        {
                            if (gridView1.GetRowCellValue(1, gridView1.VisibleColumns[i]).ToString() != "")
                            {
                                //第一行第i列
                                int c = int.Parse(gridView1.GetRowCellValue(1, gridView1.VisibleColumns[i]).ToString());
                                decimal d = (decimal)(a) / (decimal)(o);
                                //c = int.Parse(Math.Truncate(d * c).ToString());
                                c = int.Parse(Math.Round(d * c, 0).ToString());
                                gridView1.SetRowCellValue(j, gridView1.VisibleColumns[i], c);
                            }
                        }
                    }
                }
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControl1) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), GetCellHintText(view, hi.RowHandle, hi.Column));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }
        private string GetCellHintText(GridView view, int rowHandle, DevExpress.XtraGrid.Columns.GridColumn column)
        {
            string ret = view.GetRowCellDisplayText(rowHandle, column);
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                if (col.VisibleIndex < 0)
                    ret += string.Format("\r\n {0}: {1}", col.GetTextCaption(), view.GetRowCellDisplayText(rowHandle, col));
            return ret;
        }
        public void Ex2Excel(string filesName)
        {
            string fileName = BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", filesName);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                BaseFormClass.OpenFile(fileName);
            }
        }
    }
}