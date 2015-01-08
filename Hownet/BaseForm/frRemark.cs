using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frRemark : DevExpress.XtraEditors.XtraForm
    {
        public frRemark()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _TableTypeID = 0;
        public frRemark(BasicClass.cResult cr ,int TableTypeID):this()
        {
            r = cr;
            _TableTypeID = TableTypeID;
        }
        DataTable dtRemark = new DataTable();
        string bll = "Hownet.BLL.Remark";
        private void frRemark_Load(object sender, EventArgs e)
        {
            if(_TableTypeID==(int)BasicClass.Enums.TableType._缝制要求)
            {
                this.Text = "缝制要求";
                _coRemarks.Caption = "缝制要求";
            }
            else if(_TableTypeID==(int)BasicClass.Enums.TableType._针步)
            {
                this.Text = "针步";
                _coRemarks.Caption = "针步";
            }
            else if (_TableTypeID == (int)BasicClass.Enums.TableType._针距)
            {
                this.Text = "针距";
                _coRemarks.Caption = "针距";
            }
            else if (_TableTypeID == (int)BasicClass.Enums.TableType._针宽)
            {
                this.Text = "止口";
                _coRemarks.Caption = "止口";
            }
            dtRemark = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(TableTypeID=" + _TableTypeID + ")" }).Tables[0];
            gridControl1.DataSource = dtRemark;

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = false;
        }
        //增加
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DataRow dr = dtRemark.NewRow();
            dr["A"] = 3;
            dr["ID"] = 0;
            dr["TableTypeID"] = _TableTypeID;
            dr["Remarks"] = string.Empty;
            dtRemark.Rows.Add(dr);
            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            gridView1.OptionsBehavior.Editable = true;
        }
        //修改
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
        }
        //保存
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            DataTable dtTem = dtRemark.Clone();
            int a = 0;
            for(int i=0;i<dtRemark.Rows.Count;i++)
            {
                a = Convert.ToInt32(dtRemark.Rows[i]["A"]);
                if(a>1)
                {
                    dtTem.Rows.Clear();
                    dtTem.Rows.Add(dtRemark.Rows[i].ItemArray);
                    if(a==2)
                    {
                        BasicClass.GetDataSet.UpData(bll, dtTem);
                    }
                    else if(a==3)
                    {
                        dtRemark.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bll, dtTem);
                    }
                    dtRemark.Rows[i]["A"] = 1;
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(gridView1.FocusedRowHandle<0)
                return;
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                int _id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                if (_id > 0)
                    BasicClass.GetDataSet.ExecSql(bll, "Delete", new object[] {_id });
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (_TableTypeID == (int)BasicClass.Enums.TableType._缝制要求)
            {
                r.ChangeText(gridView1.GetFocusedRowCellDisplayText(_coRemarks));
            }
            else
            {
                r.ChangeText(gridView1.GetFocusedRowCellValue(_coID).ToString());
            }
            this.Close();
        }
    }
}