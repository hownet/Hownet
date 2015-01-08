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
    public partial class ucSizeList : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSizeList()
        {
            InitializeComponent();
        }
        public delegate void EditValueChangedHandler(object val, string text);
        public event EditValueChangedHandler EditValueChanged;

        //Hownet.Model.Color modC = new Hownet.Model.Color();
        //Hownet.Model.Size modS = new Hownet.Model.Size();
        ArrayList _liS = new ArrayList();
        private DataTable _dtSize = new DataTable();
        private DataTable _dtSP = new DataTable();
        /// <summary>
        /// 是否被修改
        /// </summary>
        private bool _IsEdit = false;
        private int _taskID;
        private int _materielID;
        
        private DataTable dt = new DataTable();
        /// <summary>
        /// 是否允许修改
        /// </summary>
        private bool _isCanEdit = false;
        private bool _isShowPopupMenu = false;
        private bool _isShowToolTip = false;
        private bool _isCanEditCS = true;
        string bllSTT = "Hownet.BLL.SizeTableTask";
        string bllOT = "Hownet.BLL.BaseFile.OrderTask";
        string bllSP = "Hownet.BLL.SizePart";
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
        public int TaskID
        {
            set
            {
                _taskID = value;
            }
            get
            {
                return _taskID;
            }
        }
        public void Open(int MaterielID,int _TaskID, bool t)
        {
            gridView1.Columns.Clear();
            _materielID = MaterielID;
            _taskID = _TaskID;
            this.gridView1.CustomRowCellEdit -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            DataSet ds = BasicClass.GetDataSet.GetDS(bllOT, "ShowSizeTable", new object[] { t, _materielID,_taskID });
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                dt.TableName = "tableName";
                if ( t)
                {
                    dt.Rows.Add(dt.NewRow());
                    dt.Rows.Add(dt.NewRow());
                }
                gridControl1.DataSource = dt;
                //   FormOpen.SunRow(dt);
                //    FormOpen.ReSumTable(dt);
                this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
                // _isCanEdit = false;
              //  gridView1.OptionsView.ShowColumnHeaders = true;
                GetColorSize();
                SetWidth();
            }
            else
            {
                dt = new DataTable();
                gridControl1.DataSource = dt;
            }
        }
        public DataTable dtDataSource
        {
            get
            {
                return (DataTable)(gridControl1.DataSource);
            }
        }
        public void ToExcel()
        {
            string fileName =Hownet.BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }
        public DataTable dtPrint
        {
            get
            {
                DataTable dtSizePart = new DataTable();// BasicClass.GetDataSet.GetDS("Hownet.BLL.SizeTableTask", "GetReport", new object[] { _MainID }).Tables[0];
                dtSizePart.TableName = "SizePart";
                dtSizePart.Columns.Add("SizePartName", typeof(string));
                dtSizePart.Columns.Add("Columns1", typeof(string));
                dtSizePart.Columns.Add("Columns2", typeof(string));
                dtSizePart.Columns.Add("Columns3", typeof(string));
                dtSizePart.Columns.Add("Columns4", typeof(string));
                dtSizePart.Columns.Add("Columns5", typeof(string));
                dtSizePart.Columns.Add("Columns6", typeof(string));
                dtSizePart.Columns.Add("Columns7", typeof(string));
                dtSizePart.Columns.Add("Columns8", typeof(string));
                dtSizePart.Columns.Add("Columns9", typeof(string));
                dtSizePart.Columns.Add("Columns10", typeof(string));
                dtSizePart.Columns.Add("Columns11", typeof(string));
                dtSizePart.Columns.Add("Columns12", typeof(string));
                dtSizePart.Columns.Add("Tolerance", typeof(string));
                for (int i = 0; i <gridView1 .RowCount; i++)
                {
                    DataRow dr = dtSizePart.NewRow();
                    for (int j = 0; j < gridView1.VisibleColumns.Count; j++)
                    {
                        if (gridView1.VisibleColumns[j].FieldName != "Tolerance" && j < 13)
                            dr[j] = gridView1.GetRowCellDisplayText(i, gridView1.VisibleColumns[j]);
                    }
                    dr["Tolerance"] = gridView1.GetRowCellDisplayText(i, "Tolerance");
                    //if (gridView1.GetRowCellDisplayText(i, gridView1.VisibleColumns[0]).Trim() != string.Empty)
                    //{
                    //    try
                    //    {
                    //        dr["Tolerance"] = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSizePart, "GetList", new object[] { "(Name='" + gridView1.GetRowCellDisplayText(i, gridView1.VisibleColumns[0]) + "')" }).Tables[0].Rows[0]["Tolerance"];
                    //    }
                    //    catch
                    //    {
                    //        dr["Tolerance"] = string.Empty;
                    //    }
                    //}
                    dtSizePart.Rows.Add(dr);
                }
                return dtSizePart;
            }
        }
        public void ClearData()
        {
            gridView1.Columns.Clear();
        }
        private void SetWidth()
        {
            gridView1.VisibleColumns[0].Width = 150;
            for (int i = 1; i < gridView1.VisibleColumns.Count - 3; i++)
            {
                gridView1.VisibleColumns[i].Width = 45;
            }
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
        public void Save(int MaterielID, int _TaskID)
        {
            _taskID = _TaskID;
            _materielID = MaterielID;
            gridView1.UpdateCurrentRow();
            gridView1.CloseEditor();
            dt.AcceptChanges();
            if (_IsEdit)
            {
                DataSet ds = new DataSet();
                ds.DataSetName = "ds";
                ds.Tables.Add(dt.Copy());
                ds.Tables[0].TableName = "dtt";
                byte[] bb = ZipJpg.Ds2Byte(ds);
                BasicClass.GetDataSet.ExecSql(bllOT, "SaveSizePartList", new object[] { bb,MaterielID,_TaskID });
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
                                dr["ColorID"] = _dtSP.Select("(Name='" + dt.Rows[rr]["Color"] + "')")[0]["ID"];
                                if (dt.Rows[rr]["ColorOne"].ToString() != string.Empty)
                                    dr["ColorOneID"] = _dtSP.Select("(Name='" + dt.Rows[rr]["ColorOne"] + "')")[0]["ID"];
                                else
                                    dr["ColorOneID"] = 0;
                                if (dt.Rows[rr]["ColorTwo"].ToString() != string.Empty)
                                    dr["ColorTwoID"] = _dtSP.Select("(Name='" + dt.Rows[rr]["ColorTwo"] + "')")[0]["ID"];
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
            _dtSP = BasicClass.GetDataSet.GetDS(bllSP, "GetAllList", null).Tables[0];
            _dtSP.Rows.Add(0,0,"","","",0,"");
            _dtSP.DefaultView.Sort = "ID";
            _dtSize = BasicClass.GetDataSet.GetDS(bllS, "GetAllList", null).Tables[0];
            AddReSizeItems();
            AddColor();
        }
        void AddColor()
        {
            //_reColor.Items.Clear();
            //for (int r = 0; r < _dtSP.Rows.Count; r++)
            //{
            //    _reColor.Items.Add(_dtSP.Rows[r]["Name"]);
            //}
            _reSizePart.DataSource = _dtSP.DefaultView;
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
                catch (Exception ex)
                {
                }
            }
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null)
            {
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
                    DataRow dr = dt.NewRow();
                    dr["SizePart"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
                if (e.Column.ColumnHandle == gridView1.Columns.Count - 2)
                {
                    int c = gridView1.Columns.Count;

                    DataTable dttem = (DataTable)(gridControl1.DataSource);
                    dttem.Columns.Add("Columns" + c.ToString(), typeof(string));
                    dttem.Rows[0]["Columns" + c.ToString()] = "";
                    dttem.Columns["Columns" + c.ToString()].SetOrdinal(dttem.Columns.Count - 2);
                    gridView1.Columns.Add();

                    gridView1.Columns[c].FieldName = "Columns" + c.ToString();
                    gridView1.Columns[c].VisibleIndex = c - 1;
                    gridView1.Columns[c].Visible = true;
                    gridView1.Columns[c].Width = gridView1.Columns[1].Width;
                    return;
                }
                _IsEdit = true;
                _isCanEdit = true;
                if (e.Column.FieldName == "SizePart")
                {
                    DataRow[] drs = _dtSP.Select("(ID=" + e.Value + ")");
                    if (drs.Length > 0)
                    {
                        gridView1.SetFocusedRowCellValue("Tolerance", drs[0]["Tolerance"]);
                    }
                    else
                    {
                        gridView1.SetFocusedRowCellValue("Tolerance","");
                    }
                }
            }
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
                           decimal.Parse(e.Value.ToString());
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
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!_isCanEdit)
                return;
            if (e.FocusedRowHandle < 0)
                return;
            gridView1.OptionsBehavior.Editable = SetCanEdit(e.FocusedRowHandle, gridView1.FocusedColumn.VisibleIndex);
        }
        private bool SetCanEdit(int _RowID, int _ColID)
        {
            if (_RowID < 0)
                return false;
            if (_ColID == gridView1.VisibleColumns.Count - 1)//顏色名、合計列不能修改
                return false;
            if (_RowID == gridView1.RowCount - 1) //最后一行不能修改
                return false;
            if (!_isCanEditCS && (_RowID == 0 || _ColID == 0))
                return false;
            if (_RowID > 0)
            {
                object o = gridView1.GetRowCellValue(_RowID - 1, gridView1.VisibleColumns[_ColID]).ToString();
                if (_ColID == 0)//顏色ID列
                    return (gridView1.GetRowCellValue(_RowID - 1, "SizePart").ToString() != string.Empty);//需上一行不為空，才能添加新顏色
                
                o = gridView1.GetRowCellValue(0, gridView1.GetVisibleColumn(_ColID));
                return ((!(gridView1.GetFocusedRowCellValue("SizePart") == null || gridView1.GetFocusedRowCellValue("SizePart").ToString().Equals("0"))) && gridView1.GetRowCellValue(0, gridView1.GetVisibleColumn(_ColID)).ToString() != string.Empty);
            }
            else
            {
                if (_ColID == 0 || _ColID > gridView1.VisibleColumns.Count - 1)
                    return false;
                if (gridView1.GetFocusedRowCellValue(gridView1.GetVisibleColumn(_ColID - 1)).ToString() != string.Empty)
                {
                    AddReSizeItems();
                    return true;
                }
                return false;
            }
        }
        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //if (_isCanEdit)
            //{
                if (e.RowHandle == 0 && 0 < e.Column.ColumnHandle && e.Column.ColumnHandle < gridView1.Columns.Count - 1)
                {
                    e.RepositoryItem = _reSize;
                }
                if ((e.RowHandle > 0 && e.RowHandle < gridView1.RowCount - 1) && (e.Column.ColumnHandle == 0))
                {
                    e.RepositoryItem = _reSizePart;// _reColor;
                }
            //}
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(_materielID, _taskID);
        }
    }
}