using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
    public partial class ucGridLookup : DevExpress.XtraEditors.XtraUserControl
    {
        public ucGridLookup()
        {
            InitializeComponent();
        }
        public delegate void EditValueChangedHandler(object val, string text);
        public event EditValueChangedHandler EditValueChanged;

        private int _TypeID;
        private DataTable dt = new DataTable();
        private DataTable dtFC = new DataTable();
        private DataTable dtTem = new DataTable();
        private int _value;
        private string _DisplayMember = "Name";
        private string _ValueMember = "ID";
        private int MainID = 0;

        public bool IsReadOnly
        {
            set
            {
                popupContainerEdit1.Properties.ReadOnly = value;
            }
        }
        public string DisplayMember
        {
            set
            {
                _DisplayMember = value;
            }
        }
        public string ValueMember
        {
            set
            {
                _ValueMember = value;
            }            
        }

        public string StringValues
        {
            get
            {
                try
                {
                    return popupContainerEdit1.EditValue.ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public int Values
        {
            set
            {
                try
                {
                    _value = value;
                    DataRow[] drs = dt.Select("(" + _ValueMember + "=" + _value + ")");
                    if (drs.Length > 0)
                        popupContainerEdit1.EditValue = drs[0][_DisplayMember].ToString();
                    else
                        popupContainerEdit1.EditValue = string.Empty;

                }
                catch
                {

                }
            }
            get
            {
                if (gridView1.FocusedRowHandle > -1)
                {
                    return Convert.ToInt32(gridView1.GetFocusedRowCellValue(_ValueMember));
                }
                else
                    return 0;
            }
        }
        public int TypeID
        {
            set
            {
                _TypeID = value;

                    dt.Rows.Clear();
                string Ename = Enum.GetName(typeof(BasicClass.Enums.TableType), _TypeID);
                DataTable dtFCM = BasicClass.GetDataSet.GetDS("Hownet.BLL.FromCloumnsMain", "GetList", new object[] { "(EFormName='" + Enum.GetName(typeof(BasicClass.Enums.TableType), _TypeID) + "')" }).Tables[0];
                if (dtFCM.Rows.Count > 0)
                {
                    MainID = Convert.ToInt32(dtFCM.Rows[0]["ID"]);
                    SetColumns();
                    switch (_TypeID)
                    {
                        case ((int)BasicClass.Enums.TableType.Company):
                            {
                                dtTem = BasicClass.BaseTableClass.dtCompany;
                                break;
                            }
                        case ((int)BasicClass.Enums.TableType.Supplier):
                            {
                                dtTem = BasicClass.BaseTableClass.dtSupplier;
                                break;
                            }
                        case ((int)BasicClass.Enums.TableType.MiniEmp):
                            {
                                dtTem = BasicClass.BaseTableClass.dtMiniEmp;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    SetData();
                 
                }
            }
        }
        private void SetData()
        {
            for (int i = 0; i < dtTem.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count-1; j++)
                {
                    try
                    {
                        dr[j] = dtTem.Rows[i][dt.Columns[j].ColumnName];
                    }
                    catch
                    { }
                }
                try
                {
                    dr["PY"] = BasicClass.GetChinese.GetChineseSpell(dr["Name"].ToString());
                }
                catch
                { }
                dt.Rows.Add(dr);
            }
            DataRow drr = dt.NewRow();
            drr[_ValueMember] = 0;
            drr[_DisplayMember] = string.Empty;
            dt.Rows.InsertAt(drr, 0);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            for (int i = 0; i < dtFC.Rows.Count; i++)
            {
                gridView1.Columns[i + 1].Caption = dtFC.Rows[i]["CName"].ToString();
                gridView1.Columns[i + 1].Visible = Convert.ToBoolean(dtFC.Rows[i]["IsShow"]);
                gridView1.Columns[i + 1].Width = Convert.ToInt32(dtFC.Rows[i]["Width"]);
            }
            gridView1.Columns["PY"].Visible = false;
            SetColumnsEdit();
        }
        private void SetColumnsEdit()
        {
            switch (_TypeID)
            {
                case (int)BasicClass.Enums.TableType.MiniEmp:
                    {
                        gridView1.Columns["PayID"].ColumnEdit = BaseForm.RepositoryItem._rePayID;
                        gridView1.Columns["DepartmentID"].ColumnEdit = BaseForm.RepositoryItem._reDeparment;
                        break;
                    }
                 default:
                    {
                        break;
                    }
            }

        }
        private void SetColumns()
        {
            dtFC = BasicClass.GetDataSet.GetDS("Hownet.BLL.FromCloumns", "GetFC", new object[] { MainID , BasicClass.UserInfo.UserID  }).Tables[0];
            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("ID", typeof(int));
            for(int i=0;i<dtFC.Rows.Count;i++)
            {
                dt.Columns.Add(dtFC.Rows[i]["Fileds"].ToString());
            }
            dt.Columns.Add("PY");
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.EmptyRow||hi.HitTest==DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Row)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if(buttonEdit1.EditValue.ToString().Trim()==string.Empty)
            {
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                string s = "'%" + buttonEdit1.Text.Trim() + "%'";
               StringBuilder sFilter=new StringBuilder();
               
                for (int i = 0; i < dtFC.Rows.Count;i++ )
                {
                    if (Convert.ToBoolean(dtFC.Rows[i]["IsShow"]))
                    {
                        sFilter.Append("(");
                        sFilter.Append(dtFC.Rows[i]["Fileds"].ToString());
                        sFilter.Append(" LIKE ");
                        sFilter.Append(s);
                        sFilter.Append(" ) ");
                        sFilter.Append(" OR ");
                    }
                }
                sFilter.Append(" (PY LIKE ");
                sFilter.Append(s);
                sFilter.Append(")");
                //sFilter.Remove(sFilter.Length - 4,3);
                dt.DefaultView.RowFilter = sFilter.ToString();
            }
            gridControl1.DataSource = dt.DefaultView;

        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            buttonEdit1.EditValue = string.Empty;
        }
        private void ChangeVal(object s, string text)
        {
            if (EditValueChanged != null)
                EditValueChanged(s, text);
        }
        private void popupContainerEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (popupContainerEdit1.EditValue != null)
            {
                ChangeVal(_value, popupContainerEdit1.Text);
            }
        }

        private void popupContainerEdit1_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                e.Value = gridView1.GetFocusedRowCellValue(_DisplayMember);
                _value = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_ValueMember));
            }
            else
            {
                e.Value = string.Empty;
                _value = 0;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
           
            if (gridView1.FocusedRowHandle > -1)
            {
                _value = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_ValueMember));
                popupContainerEdit1.EditValue = gridView1.GetFocusedRowCellValue(_DisplayMember);
             
            }
            else
                popupContainerEdit1.EditValue = string.Empty;
            popupContainerEdit1.ClosePopup();
        }

        private void _barSetShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicClass.cResult r = new BasicClass.cResult();
            r.TextChanged += r_TextChanged;
            Form fr = new frSetShowFileds(dtFC,r);
            fr.ShowDialog();
        }

        void r_TextChanged(string s)
        {
            SetColumns();
            SetData();
        }

        private void popupContainerEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
            for(int i=0;i<gridView1.RowCount;i++)
            {
                if(Convert.ToInt32(gridView1.GetRowCellValue(i,_ValueMember))==_value)
                {
                    gridView1.FocusedRowHandle = i;
                    break;
                }
            }
        }
    }
}
