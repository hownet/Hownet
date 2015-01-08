using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Hownet.BaseContranl
{
    public class FormOpen
    {
        public static void showChildTwo(Form fr, Form frm, string tex)
        {
            Form _de = fr;
            if (!(SetOpen(frm, tex)))
            {
                _de.MdiParent = frm;
                _de.Show();
                // MessageBox.Show(_de.Text);
            }
        }
        public static void ShowDialogChild(Form fr, Form frm, string tex)
        {
            Form _de = fr;
            if (!(SetOpen(frm, tex)))
            {
                _de.MdiParent = frm;
                _de.Show();
                // MessageBox.Show(_de.Text);
            }
        }
        public static bool SetOpen(Form _frm, string _frmName)
        {
            bool IsOpen = false;
            for (int i = 0; i < _frm.MdiChildren.Length; i++)
            {
                if (_frm.MdiChildren[i].Text.Equals(_frmName))
                {
                    IsOpen = true;
                    /*如果窗体存在，则激活*/
                    _frm.MdiChildren[i].Activate();
                    break;
                }
                else
                {
                    IsOpen = false;
                }
            }
            return IsOpen;
        }
        public static bool checkNotNull(DevExpress.XtraGrid.Views.Grid.GridView gv, ArrayList li)
        {
            bool t = false;
            string name = string.Empty;
            if (li.Count > 0)
            {
                for (int r = 0; r < li.Count; r++)
                {
                    for (int i = 0; i < gv.RowCount; i++)
                    {
                        if (gv.GetRowCellValue(i, li[r].ToString()).ToString().Length < 1)
                        {
                            t = true;
                            MessageBox.Show(li[r].ToString() + "：列第 " + i.ToString() + " 行不能为空！");
                            break;
                        }
                    }
                }
            }
            return t;
        }

        public static bool checkEdit(ArrayList del, DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            bool t = false;
            if (del.Count > 0)
            {
                t = true;
                return t;
            }
            else
            {
                for (int r = 0; r < gv.RowCount; r++)
                {

                    if ((gv.GetRowCellValue(r, "a")).ToString() == "2" || (gv.GetRowCellValue(r, "a")).ToString() == "3")
                    {
                        t = true;
                        break;
                    }
                }
            }
            return t;
        }
        public static void GridVierReadOnly(DevExpress.XtraGrid.Views.Grid.GridView gv, bool t)
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                gv.Columns[i].OptionsColumn.AllowEdit = (!t);

            }
        }
        public static void GridVierReadOnly(DevExpress.XtraGrid.Views.Grid.GridView gv, bool t,string LiWai)
        {
            
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    gv.Columns[i].OptionsColumn.AllowEdit = (!t);
                    
                }
                gv.Columns[LiWai].OptionsColumn.AllowEdit = t;
           
        }

        public static void ReSum(DevExpress.XtraGrid.Views.Grid.GridView gv, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < gv.RowCount - 1 && e.Column.ColumnHandle < gv.Columns.Count - 3&&e.Column.ColumnHandle!=0&&e.RowHandle!=0)
            {
                int amount = 0;
                for (int r = 1; r < gv.RowCount - 1; r++)
                {
                    if (gv.GetRowCellValue(r, e.Column).ToString() != string.Empty)
                    {
                        amount = amount + int.Parse(gv.GetRowCellValue(r, e.Column).ToString());
                    }
                }
                gv.SetRowCellValue(gv.RowCount - 1, e.Column, amount);
                amount = 0;
                for (int c = 1; c < gv.Columns.Count - 3; c++)
                {
                    if (gv.GetRowCellValue(e.RowHandle, gv.Columns[c]).ToString() != string.Empty)
                    {
                        amount = amount + int.Parse(gv.GetRowCellValue(e.RowHandle, gv.Columns[c]).ToString());
                    }
                }
                gv.SetRowCellValue(e.RowHandle, "SumNum", amount);
                amount = 0;
                for (int c = 1; c < gv.Columns.Count - 3; c++)
                {
                    if (gv.GetRowCellValue(gv.RowCount - 1, gv.Columns[c]).ToString() != string.Empty)
                    {
                        amount = amount + int.Parse(gv.GetRowCellValue(gv.RowCount - 1, gv.Columns[c]).ToString());
                    }
                }
                gv.SetRowCellValue(gv.RowCount - 1, "SumNum", amount);
            }
        }

        public static void ReSumTable(DataTable dt)
        {
            int sumAmount = 0;
            for (int r = 1; r < dt.Rows.Count - 1; r++)
            {
                int amount = 0;
                for (int c = 1; c < dt.Columns.Count - 3; c++)
                {
                    if (dt.DefaultView[r][c].ToString() != string.Empty)
                    {

                        object a = dt.DefaultView[r][c];
                        if (Convert.ToDecimal(a) > 0)
                            amount = amount + Convert.ToInt32(Convert.ToDecimal(a));// (int)(dt.DefaultView[r][c]);
                    }
                }
                sumAmount = sumAmount + amount;
                if (amount != 0)
                {
                    dt.Rows[r]["SumNum"] = amount;
                }
            }
            dt.Rows[dt.Rows.Count - 1]["SumNum"] = sumAmount;
            for (int r = 1; r < dt.Columns.Count - 3; r++)
            {
                int amount = 0;
                for (int c = 1; c < dt.Rows.Count - 1; c++)
                {
                    if (dt.DefaultView[c][r].ToString() != string.Empty)
                    {
                        if (Convert.ToDecimal(dt.DefaultView[c][r]) > 0)
                            amount = amount + Convert.ToInt32(Convert.ToDecimal(dt.DefaultView[c][r]));
                    }
                }
                if (amount != 0)
                {
                    dt.Rows[dt.Rows.Count - 1][r] = amount;
                }
            }
        }

        public static void SunRow(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "合计";
        }
        public static void SumCol(DataTable dt)
        {
            dt.Columns.Add("SumNum", typeof(string));
            dt.Rows[0]["SumNum"] = "合计";
            ReSumTable(dt);
        }
        public static void SaveTable(string TableName, ArrayList li,DataTable dt)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("");
        }
        public static bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        

    }
}
