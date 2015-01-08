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

namespace Hownet.Clothing
{
    public partial class Ticket2Task : DevExpress.XtraEditors.XtraForm
    {
        public Ticket2Task()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _MainID = 0;
        public Ticket2Task(BasicClass.cResult r,int MainID)
            : this()
        {
            this.r = r;
            _MainID = MainID;
        }
        DataTable dt = new DataTable();
        //DataTable dtPriv = new DataTable();
        DataTable dtSize = new DataTable();
        DataTable dtUseSize = new DataTable();
        DataTable dtBrand = new DataTable();
        DataTable dtCA = new DataTable();
        DataTable dtColor = new DataTable();
        bool _IsEditSize = false;
        int _Max = 0;
        private void Ticket2Task_Load(object sender, EventArgs e)
        {
            dtColor = BasicClass.GetDataSet.GetDS("Hownet.BLL.Color", "GetAllList", null).Tables[0];
            _reColor.DataSource = dtColor;
            _coSizeID.ColumnEdit = BaseForm. RepositoryItem._reSize;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            lookUpEdit1.Properties.DataSource = dtColor;
            lookUpEdit2.Properties.DataSource = (DataView)(BaseForm.RepositoryItem._reSize.DataSource);
            lookUpEdit1.EditValue = lookUpEdit2.EditValue =textEdit3.EditValue= 0;
            dtSize = BasicClass.GetDataSet.GetDS("Hownet.BLL.Size", "GetAllList", null).Tables[0];
            listBoxControl1.DataSource = dtSize;
            dtUseSize = dtSize.Clone();
            dtUseSize.DefaultView.Sort = "Orders";
            listBoxControl2.DataSource = dtUseSize.DefaultView;

            dtBrand = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetLookupList", new object[] { "(AttributeID=5)" }).Tables[0];
            DataRow drB = dtBrand.NewRow();
            drB["ID"] = 0;
            drB["Name"] = string.Empty;
            dtBrand.Rows.InsertAt(drB, 0);
            lookUpEdit3.Properties.DataSource = dtBrand;
            for (int i = 0; i < dtBrand.Rows.Count; i++)
            {
                _reBrand.Items.Add(dtBrand.Rows[i]["Name"].ToString());
            }
            dtCA = BasicClass.GetDataSet.GetDS("Hownet.BLL.ClothAmount", "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            gridControl1.DataSource = dtCA;
            gridView1.Columns["A"].Visible = false;
            gridView1.Columns["ID"].Visible = false;
            gridView1.Columns["MainID"].Visible = false;
            gridView1.Columns["ColorID"].Visible = false;
            gridView1.Columns["BrandID"].Visible = false;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].FieldName != "BrandName")
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            if (dtCA.Rows.Count > 0)
                SumAmount();
            dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.WorkTicket", "GetList", new object[] { "(TaskID=" + _MainID + ")" }).Tables[0];
            gridControl2.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                //dt.DefaultView.Sort = "BoxNum Desc";
                _Max = Convert.ToInt32(dt.Rows[dt.Rows.Count-1]["BoxNum"]);
                //dt.DefaultView.Sort = "";
            }
        }

        private void gridView3_RowCountChanged(object sender, EventArgs e)
        {
            //if (gridView3.RowCount > 0)
            //{
            //    int r=gridView3.RowCount-1;
            //    if (int.Parse(gridView3.GetRowCellValue(r, _coBoxAmount).ToString()) < 1)
            //    {
            //        XtraMessageBox.Show("数量不能小于1！");
            //        gridView3.DeleteRow(r);
            //        return;
            //    }
            //    if (gridView3.GetRowCellValue(r, _coSizeID).ToString() == "" || gridView3.GetRowCellValue(r, _coSizeID).ToString() == ""||
            //        gridView3.GetRowCellValue(r, _coSizeID).ToString() == "0" || gridView3.GetRowCellValue(r, _coSizeID).ToString() == "0")
            //    {
            //        XtraMessageBox.Show("颜色和尺码不能为空！");
            //        gridView3.DeleteRow(r);
            //        return;
            //    }
            //}
            //gridControl1.DataSource = GetPivoData(dt);
            //gridControl3.DataSource = GetBox(dt);
        }
  

 
        public void AddColthAmount()
        {
            try
            {
                if (dtCA.Rows.Count == 0)
                {
                    DataRow dr = dtCA.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["MainID"] = _MainID;
                    dr["BoxNum"] = "扎号";
                    dr["ColorID"] = 0;
                    dr["ColorName"] = "颜色";
                    dr["BrandID"] = 0;
                    dr["BrandName"] = "商标";
                    dr["SumAmount"] = "合计";
                    for (int i = 0; i < dtUseSize.Rows.Count; i++)
                    {
                        dr["Size" + (i + 1).ToString()] = dtUseSize.Rows[i]["Name"].ToString();
                    }
                    dtCA.Rows.Add(dr);
                    DataRow drr = dtCA.NewRow();
                    drr["A"] = 3;
                    drr["ID"] = 0;
                    drr["MainID"] = _MainID;
                    drr["ColorID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                    drr["BoxNum"] = _Max;// Convert.ToInt32(textEdit2.EditValue);
                    drr["ColorName"] = lookUpEdit1.Text;
                    drr["BrandID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                    drr["BrandName"] = lookUpEdit3.Text;
                    for (int i = 0; i < dtUseSize.Rows.Count; i++)
                    {
                        drr["Size" + (i + 1).ToString()] = textEdit1.EditValue;
                    }
                    drr["SumAmount"] = Convert.ToInt32(textEdit1.EditValue) * dtUseSize.Rows.Count;
                    dtCA.Rows.Add(drr);
                }
                else
                {
                    dtCA.Rows[dtCA.Rows.Count - 1].Delete();
                    dtCA.AcceptChanges();
                    DataRow dr = dtCA.NewRow();
                    for (int i = dtCA.Rows.Count-1; i > -1; i--)
                    {
                        if (dtCA.Rows[i]["BoxNum"].ToString() == "扎号")
                        {
                            dr = dtCA.Rows[i];
                            break;
                        }
                    }
                    bool _Is = true;
                    for (int i = 0; i < dtUseSize.Rows.Count; i++)
                    {
                        if (dr["Size" + (i + 1).ToString()].ToString() != dtUseSize.Rows[i]["Name"].ToString())
                        {
                            _Is = false;
                            break;
                        }
                    }
                    if (!_Is)
                    {
                        DataRow drr = dtCA.NewRow();
                        drr["A"] = 3;
                        drr["ID"] = 0;
                        drr["MainID"] = _MainID;
                        drr["BoxNum"] = "扎号";
                        drr["ColorID"] = 0;
                        drr["ColorName"] = "颜色";
                        drr["BrandID"] = 0;
                        drr["BrandName"] = "商标";
                        drr["SumAmount"] = "合计";
                        for (int i = 0; i < dtUseSize.Rows.Count; i++)
                        {
                            drr["Size" + (i + 1).ToString()] = dtUseSize.Rows[i]["Name"].ToString();
                        }
                        dtCA.Rows.Add(drr);
                    }
                    DataRow ddrr = dtCA.NewRow();
                    ddrr["A"] = 3;
                    ddrr["ID"] = 0;
                    ddrr["MainID"] = _MainID;
                    ddrr["ColorID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                    ddrr["BoxNum"] = _Max;// Convert.ToInt32(textEdit2.EditValue);
                    ddrr["ColorName"] = lookUpEdit1.Text;
                    ddrr["BrandID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                    ddrr["BrandName"] = lookUpEdit3.Text;
                    for (int i = 0; i < dtUseSize.Rows.Count; i++)
                    {
                        ddrr["Size" + (i + 1).ToString()] = textEdit1.EditValue;
                    }
                    ddrr["SumAmount"] = Convert.ToInt32(textEdit1.EditValue) * dtUseSize.Rows.Count;
                    dtCA.Rows.Add(ddrr);
                }
                SumAmount();
            }
            catch (Exception ex)
            {
            }
        }
        private void SumAmount()
        {
            DataRow dr = dtCA.NewRow();
            dr["A"] = 4;
            dr["ID"] = 0;
            dr["BoxNum"] = "合计";
            int amount = 0;
            for (int i = 1; i < 16; i++)
            {
                amount = 0;
                for (int j = 1; j < dtCA.Rows.Count; j++)
                {
                    try
                    {
                        amount += Convert.ToInt32(dtCA.Rows[j]["Size" + i.ToString()]);
                    }
                    catch
                    {
                    }
                }
                if (amount > 0)
                    dr["Size" + i.ToString()] = amount;
            }
            for (int j = 1; j < dtCA.Rows.Count; j++)
            {
                try
                {
                    amount += Convert.ToInt32(dtCA.Rows[j]["SumAmount"]);
                }
                catch
                {
                }
            }
            if (amount > 0)
                dr["SumAmount"] = amount;
            dtCA.Rows.Add(dr);
        }
        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && Convert.ToInt32(gridView3.GetFocusedRowCellValue("A")) == 1)
                gridView3.SetFocusedRowCellValue("A", 2);
            if (e.Column == _coAmount )
            {
                //if (gridView3.GetFocusedRowCellValue(_coBoxNum).ToString() != "" && gridView3.GetFocusedRowCellValue(_coBoxAmount).ToString() != "")
                //{
                //    gridView3.SetFocusedRowCellValue(_coSumAmount, int.Parse(gridView3.GetFocusedRowCellValue(_coBoxNum).ToString()) * int.Parse(gridView3.GetFocusedRowCellValue(_coBoxAmount).ToString()));
                //}
                //else
                //{
                //    gridView3.SetFocusedRowCellValue(_coSumAmount, 0);
                //}
                if (Convert.ToInt32(e.Value) > 0)
                {
                    DataRow[] drs = dt.Select("(BoxNum=" + gridView3.GetFocusedRowCellValue(_coBoxNum) + ")");
                    for (int i = 0; i < drs.Length; i++)
                    {
                        drs[i]["Amount"] = e.Value;
                    }
                    dt.AcceptChanges();
                    DataRow[] drss = dtCA.Select("(BoxNum='" + gridView3.GetFocusedRowCellValue(_coBoxNum).ToString() + "')");
                    if (drss.Length > 0)
                    {
                        for (int i = 8; i < 23; i++)
                        {
                            if (drss[0][i] != DBNull.Value && drss[0][i].ToString() != string.Empty)
                                drss[0][i] = e.Value;
                        }
                        drss[0]["SumAmount"] = Convert.ToInt32(e.Value) * drs.Length;
                        dtCA.Rows[dtCA.Rows.Count - 1].Delete();
                        SumAmount();
                        dtCA.AcceptChanges();
                    }
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

        }

        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("请检查填写是否正确", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                //if (_MainID == 0)
                //{
                    gridView3.CloseEditor();
                    gridView3.UpdateCurrentRow();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtCA.Copy());

                    dt.TableName = "Ticket";
                    ds.Tables.Add(dt.Copy());
                    r.TableChang(ds);
                    this.Close();
                //}
                //else
                //{
                //}
            }
            else
            {
                return;
            }
            
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Convert.ToInt32(lookUpEdit1.EditValue) > 0 && Convert.ToInt32(lookUpEdit2.EditValue) > 0 && Convert.ToInt32(textEdit3.EditValue) > 0)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["ColorID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                //    dr["SizeID"] = Convert.ToInt32(lookUpEdit2.EditValue);
                //    dr["Amount"] = Convert.ToInt32(textEdit1.EditValue);
                //    dr["BoxNum"] = Convert.ToInt32(textEdit2.EditValue);
                //    dr["SumAmount"] = Convert.ToInt32(textEdit3.EditValue);
                //    dr["ColorName"] = lookUpEdit1.Text;
                //    dr["SizeName"] = lookUpEdit2.Text;
                //    dr["ColorOneID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                //    dr["ColorTwoID"] = Convert.ToInt32(lookUpEdit4.EditValue);
                //    dr["ColorOneName"] = lookUpEdit3.Text;
                //    dr["colorTwoName"] = lookUpEdit4.Text;
                //    dr["BrandID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                //    dr["BrandName"] = lookUpEdit3.Text;
                //    dt.Rows.Add(dr);
                //    _Max += Convert.ToInt32(textEdit2.EditValue);
                //}
                //else
                //{
                //    XtraMessageBox.Show("颜色、尺码不能为空，数量不能为0或负数！");
                //}

                if (Convert.ToInt32(lookUpEdit1.EditValue) > 0 && dtUseSize.Rows.Count > 0 && Convert.ToInt32(textEdit3.EditValue) > 0)
                {
                    _Max += Convert.ToInt32(textEdit2.EditValue);
                    for (int i = 0; i < dtUseSize.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["A"] = 3;
                        dr["ID"] = 0;
                        dr["TaskID"] = _MainID;
                        dr["DepartmentID"] = 0;
                        dr["P2DInfoID"] = 0;
                        dr["EligibleAmount"] = 0;
                        dr["InferiorAmount"] = 0;
                        dr["P2DDepartmentID"] = 0;
                        dr["MListID"] = 0;
                        dr["OneAmount"] = 0;
                        dr["ColorID"] = Convert.ToInt32(lookUpEdit1.EditValue);
                        dr["SizeID"] = Convert.ToInt32(dtUseSize.Rows[i]["ID"]);
                        dr["Amount"] = Convert.ToInt32(textEdit1.EditValue);
                        dr["BoxNum"] = _Max;// Convert.ToInt32(textEdit2.EditValue);
                       // dr["SumAmount"] = Convert.ToInt32(textEdit3.EditValue);
                       // dr["ColorName"] = lookUpEdit1.Text;
                      //  dr["SizeName"] = dtUseSize.Rows[i]["Name"].ToString();
                        dr["ColorOneID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                        dr["ColorTwoID"] = Convert.ToInt32(lookUpEdit4.EditValue);
                     //   dr["ColorOneName"] = lookUpEdit3.Text;
                    //    dr["colorTwoName"] = lookUpEdit4.Text;
                        dr["BrandID"] = Convert.ToInt32(lookUpEdit3.EditValue);
                     //   dr["BrandName"] = lookUpEdit3.Text;
                        dt.Rows.Add(dr);
                    }
                    //gridView1.Columns.Clear();
                    AddColthAmount();
                   // gridControl1.DataSource = ZhaAmount(dt);
                    //for (int i = 0; i < gridView1.Columns.Count; i++)
                    //{
                    //    if (gridView1.Columns[i].FieldName != "BrandName")
                    //        gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                    //}
                }
                else
                {
                    XtraMessageBox.Show("颜色、尺码不能为空，数量不能为0或负数！");
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("颜色、尺码不能为空，数量不能为0或负数！");
            }
            finally
            {
                textEdit1.EditValue =  textEdit3.EditValue = 0;//textEdit2.EditValue =
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

        private void listBoxControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxControl1.SelectedIndex > -1)
            {
                if (dtUseSize.Select("(ID=" + listBoxControl1.SelectedValue + ")").Length > 0&&!checkEdit1.Checked)
                {
                    return;
                }
                dtUseSize.Rows.Add(dtSize.Select("(ID=" + listBoxControl1.SelectedValue + ")")[0].ItemArray);
                dtUseSize.AcceptChanges();
            }
        }

        private void listBoxControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxControl2.SelectedIndex > -1)
            {
                DataRow[] drs = dtUseSize.Select("(ID=" + listBoxControl2.SelectedValue + ")");
                if (drs.Length > 0)
                {
                    drs[0].Delete();
                }
                dtUseSize.AcceptChanges();
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                simpleButton3_Click(this, EventArgs.Empty);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            

            try
            {
                int a = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BoxNum"));
                gridView1.Columns["BrandName"].ColumnEdit = _reBrand;
                gridView1.Columns["BrandName"].OptionsColumn.AllowEdit = true;
            }
            catch
            {
                gridView1.Columns["BrandName"].ColumnEdit = null;
                gridView1.Columns["BrandName"].OptionsColumn.AllowEdit = false;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && Convert.ToInt32(gridView1.GetFocusedRowCellValue("A")) == 1)
                gridView1.SetFocusedRowCellValue("A", 2);
            if (e.Column.FieldName == "BrandName")
            {
                if (_reBrand.Items.IndexOf(e.Value.ToString().Trim()) == -1)
                {
                    gridView1.SetFocusedValue(string.Empty);
                }
                DataRow[] drs = dt.Select("(BoxNum=" + gridView1.GetFocusedRowCellValue("BoxNum") + ")");
                DataRow[] drsb = dtBrand.Select("(Name='" + e.Value + "')");
                int brandID = 0;
                if (drsb.Length > 0)
                    brandID = Convert.ToInt32(drsb[0]["ID"]);
                for (int i = 0; i < drs.Length; i++)
                {
                   drs[i]["BrandID"] = brandID;
                }
                dt.AcceptChanges();
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void lookUpEdit1_GetNotInListValue(object sender, DevExpress.XtraEditors.Controls.GetNotInListValueEventArgs e)
        {
            
        }

        private void lookUpEdit1_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (lookUpEdit1.Text.Trim().Length > 0)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("刚才所填是否为新色号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    string bll = "Hownet.BLL.Color";
                    DataTable dtTem = dtColor.Clone();
                    DataRow dr = dtTem.NewRow();
                    dr["A"] = 3;
                    dr["ID"] = 0;
                    dr["Sn"] = string.Empty;
                    dr["Name"] = lookUpEdit1.Text;
                    dr["Value"] = 0;
                    dr["IsEnd"] = 0;
                    dr["IsUse"] = 1;
                    dr["ColorTypeID"] = 0;
                    dr["Remark"] = string.Empty;
                    dtTem.Rows.Add(dr);
                    int _temID = 0;
                    dr["ID"]=_temID= BasicClass.GetDataSet.Add(bll, dtTem);
                    dtColor.Rows.Add(dr.ItemArray);
                    lookUpEdit1.EditValue = _temID;
                }
                else
                {
                    lookUpEdit1.EditValue = 0;
                    lookUpEdit1.Text = string.Empty;
                }
            }
        }
    }
}