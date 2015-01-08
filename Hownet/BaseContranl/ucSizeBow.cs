using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
    public partial class ucSizeBow : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSizeBow()
        {
            InitializeComponent();
        }
        DataTable dt;
        public int TaskID
        {
            set;
            get;
        }
        public delegate void EditValueChangedHandler();
        public event EditValueChangedHandler EditValueChanged;
        private int _tableTypeID = 0;
        public void Open(int _taskID,int TableTypeID)
        {
            _coBowID.ColumnEdit = BaseForm.RepositoryItem._reBow("钢弓");
            _coCottonID.ColumnEdit = BaseForm.RepositoryItem._reBow("棉碗");
            _coSash.ColumnEdit = BaseForm.RepositoryItem._reBow("成品肩带");
            _coOpenSash.ColumnEdit = BaseForm.RepositoryItem._reBow("透明背带");
            _coPlasticBone.ColumnEdit = BaseForm.RepositoryItem._reBow("胶骨");
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            gridControl1.DataSource = null;
            TaskID = _taskID;
            _tableTypeID = TableTypeID;
            gridView1.OptionsBehavior.Editable = (TaskID >= 0);
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSizeBow, "GetList", new object[] { "(TaskID=" + TaskID + ")" }).Tables[0];
            if (dt.Rows.Count == 0 && TaskID > 0)
            {
                DataTable dtSize = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetSize", new object[] { TaskID, _tableTypeID }).Tables[0];
                if (dtSize.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSize.Rows.Count; i++)
                    {
                        dt.Rows.Add(3, 0, dtSize.Rows[i]["SizeID"], 0, 0, TaskID, 0, 0, 0, "");
                    }
                }
            }
            gridControl1.DataSource = dt;
        }
        public DataTable dtDataSource
        {
            get
            {
                try
                {
                    gridView1.CloseEditor();
                    gridView1.UpdateCurrentRow();
                    dt.AcceptChanges();
                    return (DataTable)(gridControl1.DataSource);
                }
                catch
                {
                    return new DataTable();
                }
            }
        }
        public void Save()
        {
            if (TaskID == 0)
                return;
            DataTable dtSize = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetSize", new object[] { TaskID, _tableTypeID }).Tables[0];
            if (dtSize.Rows.Count == 0)
                return;
            DataTable dtt = dt.Clone();
            if (gridView1.RowCount == 0)
            {
                for (int i = 0; i < dtSize.Rows.Count; i++)
                {
                    dt.Rows.Add(1, 0, dtSize.Rows[i]["SizeID"], 0, 0, TaskID, 0, 0, 0, "");
                    dtt.Rows.Clear();
                    dtt.Rows.Add(dt.Rows[i].ItemArray);
                    dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllSizeBow, dtt);
                }
                return;
            }
            else
            {
                bool t = false;
                int a = 0;
                for (int i = 0; i < dtSize.Rows.Count; i++)
                {
                    t = false;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["SizeID"].Equals(dtSize.Rows[i]["SizeID"]))
                        {
                            t = true;
                            a = int.Parse(dt.Rows[j]["A"].ToString());
                            if (a > 1)
                            {
                                dtt.Rows.Clear();
                                dtt.Rows.Add(dt.Rows[j].ItemArray);
                                if (a == 3)
                                    dt.Rows[j]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllSizeBow, dtt);
                                else if (a == 2)
                                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllSizeBow, dtt);
                                dt.Rows[j]["A"] = 1;
                            }
                            break;
                        }
                    }
                    if (!t)
                    {
                        dt.Rows.Add(1, 0, dtSize.Rows[i]["SizeID"], 0, 0, TaskID, 0, 0, 0, "");
                        dtt.Rows.Clear();
                        dtt.Rows.Add(dt.Rows[i].ItemArray);
                        dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllSizeBow, dtt);
                    }
                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    t = false;
                    for (int i = 0; i < dtSize.Rows.Count; i++)
                    {
                        if (dt.Rows[j]["SizeID"].Equals(dtSize.Rows[i]["SizeID"]))
                        {
                            t = true;
                            break;
                        }
                    }
                    if (!t)
                    {
                        a = int.Parse(dt.Rows[j]["ID"].ToString());
                        if (a > 0)
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllSizeBow, "Delete", new object[] { a });
                    }
                }
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSizeBow, "GetList", new object[] { "(TaskID=" + TaskID + ")" }).Tables[0];
                gridControl1.DataSource = dt;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column != _coA && int.Parse(gridView1.GetFocusedRowCellValue(_coA).ToString()) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
            ChangeVal();
        }
        private void ChangeVal()
        {
            if (EditValueChanged != null)
                EditValueChanged();
        }
    }
}
