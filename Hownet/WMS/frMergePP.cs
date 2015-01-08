using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.WMS
{
    public partial class frMergePP : DevExpress.XtraEditors.XtraForm
    {
        public frMergePP()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int MaterielID = 0;
        public frMergePP(BasicClass.cResult cr, int MatID)
            : this()
        {
            r = cr;
            MaterielID = MatID;
        }
        DataTable dt = new DataTable();
        string bllPP = "Hownet.BLL.ProductionPlan";
        private void frMergePP_Load(object sender, EventArgs e)
        {
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coCompanyID.ColumnEdit = BaseForm.RepositoryItem._reCompanyID;
            dt = BasicClass.GetDataSet.GetDS(bllPP, "GetMerge", new object[] {MaterielID,(int)BasicClass.Enums.TableType.ProductionPlan }).Tables[0];
            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dt.AcceptChanges();
            if (DialogResult.Yes == XtraMessageBox.Show("新的生产计划中，商标将使用所选择的第一个商标为准，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                string ssID = string.Empty;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (Convert.ToBoolean(gridView1.GetRowCellValue(i, _coIsSelect)))
                    {
                        ssID = ssID + gridView1.GetRowCellDisplayText(i, _coID) + ",";
                    }
                }
                if (ssID.Length > 0)
                {
                    ssID = ssID.Remove(ssID.Length - 1);
                    string[] ss = ssID.Split(',');
                    if (ss.Length < 2)
                    {
                        XtraMessageBox.Show("未选择生产计划或只选择了一单，不能合并！");
                        return;
                    }
                    DataTable ddt = new DataTable();
                    ddt.Columns.Add("ID", typeof(int));
                    ddt.Rows.Add(Convert.ToInt32(BasicClass.GetDataSet.GetOne(bllPP, "MergePP", new object[] { ssID })));
                    Form fr = new frTaskBOM(ddt);
                    fr.ShowDialog();
                    this.Close();
                }
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == _coIsSelect)
                gridView1.SetFocusedRowCellValue(_coIsSelect, e.Value);
        }


    }
}