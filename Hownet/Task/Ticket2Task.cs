using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Hownet.BaseContranl;

namespace Hownet.Task
{
    public partial class Ticket2Task : DevExpress.XtraEditors.XtraForm
    {
        public Ticket2Task()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        public Ticket2Task(BasicClass.cResult r)
            : this()
        {
            this.r = r;
        }
        DataTable dt = new DataTable();
        DataTable dtPriv = new DataTable();
        private void Ticket2Task_Load(object sender, EventArgs e)
        {
            _coColorID.ColumnEdit = _ccColorID.ColumnEdit =BaseForm. RepositoryItem._reColor;
            _coSizeID.ColumnEdit = _ccSizeID.ColumnEdit =BaseForm. RepositoryItem._reSize;
            lookUpEdit1.Properties.DataSource = lookUpEdit3.Properties.DataSource = lookUpEdit4.Properties.DataSource = (DataView)(BaseForm.RepositoryItem._reColor.DataSource);
            lookUpEdit2.Properties.DataSource = (DataView)(BaseForm.RepositoryItem._reSize.DataSource);
            lookUpEdit1.EditValue = lookUpEdit2.EditValue =textEdit3.EditValue= 0;
            dt.Columns.Add("ColorID", typeof(int));
            dt.Columns.Add("SizeID", typeof(int));
            dt.Columns.Add("BoxAmount", typeof(int));
            dt.Columns.Add("BoxNum", typeof(int));
            dt.Columns.Add("SumAmount", typeof(int));
            dt.Columns.Add("ColorName", typeof(string));
            dt.Columns.Add("SizeName", typeof(string));
            dt.Columns.Add("ColorOneID", typeof(int));
            dt.Columns.Add("ColorTwoID", typeof(int));
            dt.Columns.Add("ColorOneName", typeof(string));
            dt.Columns.Add("colorTwoName", typeof(string));
            gridControl2.DataSource = dt;
        }

        private void gridView3_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView3.RowCount > 0)
            {
                int r=gridView3.RowCount-1;
                if (int.Parse(gridView3.GetRowCellValue(r, _coSumAmount).ToString()) < 1)
                {
                    XtraMessageBox.Show("数量不能小于1！");
                    gridView3.DeleteRow(r);
                    return;
                }
                if (gridView3.GetRowCellValue(r, _coSizeID).ToString() == "" || gridView3.GetRowCellValue(r, _coSizeID).ToString() == ""||
                    gridView3.GetRowCellValue(r, _coSizeID).ToString() == "0" || gridView3.GetRowCellValue(r, _coSizeID).ToString() == "0")
                {
                    XtraMessageBox.Show("颜色和尺码不能为空！");
                    gridView3.DeleteRow(r);
                    return;
                }
            }
            gridControl1.DataSource = GetPivoData(dt);
            gridControl3.DataSource = GetBox(dt);
        }
        private DataTable GetPivoData(DataTable dt)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dtPivo = new DataTable();
            ArrayList liColor = new ArrayList();
            ArrayList liSize = new ArrayList();
            ArrayList liColorOne = new ArrayList();
            ArrayList liColorTwo = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].RowState != DataRowState.Detached)
                {
                    bool t = false;
                    if (liColor.Count > 0)
                    {
                        for (int c = 0; c < liColor.Count; c++)
                        {
                            t = false;
                            if (liColor[c].ToString() == dt.Rows[i]["ColorName"].ToString() &&
                                liColorOne[c].ToString() == dt.Rows[i]["ColorOneName"].ToString() &&
                                liColorTwo[c].ToString() == dt.Rows[i]["ColorTwoName"].ToString())
                            {
                                t = true;
                                break;
                            }
                        }
                        if (!t)
                        {
                            liColor.Add(dt.Rows[i]["ColorName"]);
                            liColorOne.Add(dt.Rows[i]["ColorOneName"]);
                            liColorTwo.Add(dt.Rows[i]["ColorTwoName"]);
                        }
                    }
                    else
                    {
                        liColor.Add(dt.Rows[i]["ColorName"]);
                        liColorOne.Add(dt.Rows[i]["ColorOneName"]);
                        liColorTwo.Add(dt.Rows[i]["ColorTwoName"]);
                    }
                    t = false;
                    if (liSize.Count > 0)
                    {
                        for (int s = 0; s < liSize.Count; s++)
                        {
                            t = false;
                            if (liSize[s].ToString() == dt.Rows[i]["SizeName"].ToString())
                            {
                                t = true;
                                break;
                            }
                        }
                        if (!t)
                        {
                            liSize.Add(dt.Rows[i]["SizeName"]);
                        }
                    }
                    else
                        liSize.Add(dt.Rows[i]["SizeName"]);
                }
            }
            dtPivo.Columns.Add("Color", typeof(string));

            for (int i = 0; i < liSize.Count; i++)
            {
                dtPivo.Columns.Add("Columns" + i.ToString(), typeof(string));
            }
            for (int i = dtPivo.Columns.Count; i < 12; i++)
            {
                dtPivo.Columns.Add("Columns" + i);
            }
            dtPivo.Columns.Add("SumNum", typeof(string));
            dtPivo.Columns.Add("ColorOne", typeof(string));
            dtPivo.Columns.Add("ColorTwo", typeof(string));
            dtPivo.Rows.Add(dtPivo.NewRow());
            dtPivo.Rows[0]["ColorOne"] = "插色一";
            dtPivo.Rows[0]["ColorTwo"] = "插色二";

            dtPivo.Rows[0][0] = "颜色\\尺码";
            dtPivo.Rows[0]["SumNum"] = "合计";
            for (int i = 0; i < liSize.Count; i++)
            {
                dtPivo.Rows[0][i + 1] = liSize[i].ToString();
            }
            for (int i = 0; i < liColor.Count; i++)
            {
                dtPivo.Rows.Add(dtPivo.NewRow());
                dtPivo.Rows[i + 1][0] = liColor[i].ToString();
                dtPivo.Rows[i + 1][dtPivo.Columns.Count - 2] = liColorOne[i].ToString();
                dtPivo.Rows[i + 1][dtPivo.Columns.Count - 1] = liColorTwo[i].ToString();
                for (int c = 0; c < liSize.Count; c++)
                {
                    string sql = " SizeName= '" + liSize[c].ToString() + "' And ColorName = '" + liColor[i].ToString() + "' And (ColorOneName='" + liColorOne[i].ToString() + "') And (ColorTwoName='" + liColorTwo[i].ToString() + "') ";
                    DataRow[] drs = dt.Select(sql);
                    if (drs.Length > 0)
                    {
                        int amount = 0;
                        for (int n = 0; n < drs.Length; n++)
                        {
                            amount += int.Parse(drs[n]["SumAmount"].ToString());
                        }
                        dtPivo.Rows[i + 1][c + 1] = amount;
                    }
                    else
                        dtPivo.Rows[i + 1][c + 1] = string.Empty;
                }
            }



                dtPivo.Rows.Add(dtPivo.NewRow());
            
            dtPivo.Rows.Add(dtPivo.NewRow());
            dtPivo.Rows[dtPivo.Rows.Count - 1][0] = "合计";
            FormOpen.ReSumTable(dtPivo);
            return dtPivo;
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coBoxAmount || e.Column == _coBoxNum)
            {
                if (gridView3.GetFocusedRowCellValue(_coBoxNum).ToString() != "" && gridView3.GetFocusedRowCellValue(_coBoxAmount).ToString() != "")
                {
                    gridView3.SetFocusedRowCellValue(_coSumAmount, int.Parse(gridView3.GetFocusedRowCellValue(_coBoxNum).ToString()) * int.Parse(gridView3.GetFocusedRowCellValue(_coBoxAmount).ToString()));
                }
                else
                {
                    gridView3.SetFocusedRowCellValue(_coSumAmount, 0);
                }
            }
            if (e.Column == _coColorID && e.Value != null && e.Value.ToString() != "0")
            {
                gridView3.SetFocusedRowCellValue(_coColorName, gridView3.GetFocusedRowCellDisplayText(_coColorID));
            }
            if (e.Column == _coSizeID && e.Value != null && e.Value.ToString() != "0")
            {
                gridView3.SetFocusedRowCellValue(_coSizeName, gridView3.GetFocusedRowCellDisplayText(_coSizeID));
            }
            if (e.Column == _coColorOneID && e.Value != null && e.Value.ToString() != "0")
            {
                gridView3.SetFocusedRowCellValue(_coColorOneName, gridView3.GetFocusedRowCellDisplayText(_coColorOneID));
            }
            if (e.Column == _coColorTwoID && e.Value != null && e.Value.ToString() != "0")
            {
                gridView3.SetFocusedRowCellValue(_coColorTwoName, gridView3.GetFocusedRowCellDisplayText(_coColorTwoID));
            }
            if (e.RowHandle > -1)
            {
                gridControl1.DataSource = GetPivoData(dt);
                gridControl3.DataSource = GetBox(dt);
            }
        }

        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView3.CloseEditor();
            gridView3.UpdateCurrentRow();
            DataSet ds = new DataSet();
            ds.Tables.Add(((DataTable)(gridControl1.DataSource)).Copy());
            ds.Tables.Add(((DataTable)(gridControl3.DataSource)).Copy());
            r.TableChang(ds);
            this.Close();
        }

        private void _reColor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                gridView3.SetFocusedValue(0);
            }
        }

        private void gridView3_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //gridView3.SetFocusedRowCellValue(_coColorOneID, 0);
            //gridView3.SetFocusedRowCellValue(_coColorTwoID, 0);
            //gridView3.SetFocusedRowCellValue(_coBoxNum, 1);
        }
        private DataTable GetBox(DataTable dtt)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColorID", typeof(int));
            dt.Columns.Add("SizeID", typeof(int));
            dt.Columns.Add("ColorName", typeof(string));
            dt.Columns.Add("SizeName", typeof(string));
            dt.Columns.Add("Amount", typeof(int));
            dt.Columns.Add("BoxNum", typeof(int));
            dt.Columns.Add("ColorOneID", typeof(int));
            dt.Columns.Add("ColorTwoID", typeof(int));
            dt.Columns.Add("ColorOneName", typeof(string));
            dt.Columns.Add("ColorTwoName", typeof(string));
            int j = 1;
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                if (dtt.Rows[i].RowState != DataRowState.Detached)
                {
                    DataRow dr = dt.NewRow();
                    dr["ColorID"] = dtt.Rows[i]["ColorID"];
                    dr["SizeID"] = dtt.Rows[i]["SizeID"];
                    dr["ColorName"] = dtt.Rows[i]["ColorName"];
                    dr["SizeName"] = dtt.Rows[i]["SizeName"];
                    dr["ColorOneID"] = dtt.Rows[i]["ColorOneID"];
                    dr["ColorTwoID"] = dtt.Rows[i]["ColorTwoID"];
                    dr["ColorOneName"] = dtt.Rows[i]["ColorOneName"];
                    dr["ColorTwoName"] = dtt.Rows[i]["ColorTwoName"];
                    dr["Amount"] = dtt.Rows[i]["BoxAmount"];
                    if (dtt.Rows[i]["BoxNum"].ToString() == "1")
                    {
                        dr["BoxNum"] = j;
                        dt.Rows.Add(dr);
                        j += 1;
                    }
                    else
                    {
                        int b = int.Parse(dtt.Rows[i]["BoxNum"].ToString());
                        for (int n = 0; n < b; n++)
                        {
                            DataRow drr = dt.NewRow();
                            drr["ColorID"] = dtt.Rows[i]["ColorID"];
                            drr["SizeID"] = dtt.Rows[i]["SizeID"];
                            drr["ColorName"] = dtt.Rows[i]["ColorName"];
                            drr["SizeName"] = dtt.Rows[i]["SizeName"];
                            drr["Amount"] = dtt.Rows[i]["BoxAmount"];
                            drr["ColorOneID"] = dtt.Rows[i]["ColorOneID"];
                            drr["ColorTwoID"] = dtt.Rows[i]["ColorTwoID"];
                            drr["ColorOneName"] = dtt.Rows[i]["ColorOneName"];
                            drr["ColorTwoName"] = dtt.Rows[i]["ColorTwoName"];
                            drr["BoxNum"] = j;
                            dt.Rows.Add(drr);
                            j += 1;
                        }
                    }
                }
            }
            return dt;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lookUpEdit1.EditValue) > 0 && Convert.ToInt32(lookUpEdit2.EditValue) > 0 && Convert.ToInt32(textEdit3.EditValue) > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ColorID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                    dr["SizeID"] = Convert.ToInt32(lookUpEdit2.EditValue);
                    dr["BoxAmount"] = Convert.ToInt32(textEdit1.EditValue);
                    dr["BoxNum"] = Convert.ToInt32(textEdit2.EditValue);
                    dr["SumAmount"] = Convert.ToInt32(textEdit3.EditValue);
                    dr["ColorName"] = lookUpEdit1.Text;
                    dr["SizeName"] = lookUpEdit2.Text;
                    dr["ColorOneID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                    dr["ColorTwoID"] = Convert.ToInt32(lookUpEdit4.EditValue);
                    dr["ColorOneName"] = lookUpEdit3.Text;
                    dr["colorTwoName"] = lookUpEdit4.Text;
                    dt.Rows.Add(dr);
                }
                else
                {
                    XtraMessageBox.Show("颜色、尺码不能为空，数量不能为0或负数！");
                }
            }
            catch
            {
                XtraMessageBox.Show("颜色、尺码不能为空，数量不能为0或负数！");
            }
            finally
            {
                textEdit1.EditValue = textEdit2.EditValue = textEdit3.EditValue = 0;
            }
            lookUpEdit1.Focus();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            CaicSumAmount();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            CaicSumAmount();
        }
        private void CaicSumAmount()
        {
            int amount = 0;
            int Boxs = 0;
            try
            {
                amount = Convert.ToInt32(textEdit1.EditValue);
                Boxs = Convert.ToInt32(textEdit2.EditValue);
                textEdit3.EditValue = amount * Boxs;
            }
            catch
            {
                textEdit3.EditValue = 0;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            lookUpEdit1.EditValue = lookUpEdit2.EditValue = lookUpEdit3.EditValue = lookUpEdit4.EditValue = 0;
        }
    }
}