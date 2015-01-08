using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
    public partial class frSetShowFileds : DevExpress.XtraEditors.XtraForm
    {
        public frSetShowFileds()
        {
            InitializeComponent();
        }
        DataTable dtFC;
        BasicClass.cResult r = new BasicClass.cResult();
        public frSetShowFileds(DataTable _dtFC,BasicClass.cResult cr)
            : this()
        {
            dtFC = _dtFC;
            r = cr;
        }
        private void frSetShowFileds_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dtFC;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtFC.AcceptChanges();
            if(dtFC.Rows.Count>0)
            {
                DataTable dtTem = dtFC.Clone();
                if(Convert.ToInt32( dtFC.Rows[0]["UserID"])>-1)//用户第一次设置
                {
                    for(int i=0;i<dtFC.Rows.Count;i++)
                    {
                        dtTem.Rows.Clear();
                        dtTem.Rows.Add(dtFC.Rows[i].ItemArray);
                        dtTem.Rows[0]["UserID"] = BasicClass.UserInfo.UserID * -1;
                        BasicClass.GetDataSet.Add("Hownet.BLL.FromCloumns", dtTem);
                    }
                }
                else
                {
                    for(int i=0;i<dtFC.Rows.Count;i++)
                    {
                        if(Convert.ToInt32(dtFC.Rows[i]["A"])==2)
                        {
                            dtTem.Rows.Clear();
                            dtTem.Rows.Add(dtFC.Rows[i].ItemArray);
                            BasicClass.GetDataSet.UpData("Hownet.BLL.FromCloumns", dtTem);
                        }
                    }
                }
                r.ChangeText("1");
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coA && Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                gridView1.SetFocusedRowCellValue(_coA, 2);
        }
    }
}