using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.OtherTem
{
    public partial class frSellList : DevExpress.XtraEditors.XtraForm
    {
        public frSellList()
        {
            InitializeComponent();
        }
        private int _MaterielID = 0;
        DateTime dt1 = DateTime.Today;
        DateTime dt2 = DateTime.Today;
        private void frSellList_Load(object sender, EventArgs e)
        {
            DataTable dtMat = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0];
            DataRow drmat = dtMat.NewRow();
            drmat["ID"] = 0;
            drmat["Name"] = string.Empty;
            dtMat.Rows.Add(drmat);
            dtMat.DefaultView.Sort =  "ID";
            lookUpEdit1.Properties.DataSource = dtMat.DefaultView;
            dateEdit1.EditValue = Convert.ToDateTime(BasicClass.GetDataSet.GetDateTime().Year.ToString() + "-1-1");
            dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            lookUpEdit1.EditValue = 0;
            _coMaterielID.ColumnEdit = BaseForm.RepositoryItem._reProduce;
            _coBrandID.ColumnEdit = BaseForm.RepositoryItem._reBrand;
            _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dt1 = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMinutes(-1);
            dt2 = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
             string strWhere = " (ProduceSell.DateTime>'" + dt1 + "') And (ProduceSell.DateTime<'" + dt2 + "')";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT     ProduceSell.DateTime, ProduceSellInfo.MaterielID, ProduceSellInfo.BrandID, ProduceSellInfo.ColorID, ProduceSellInfo.SizeID,Sum( ProduceSellInfo.Amount) as Amount ");
            strSql.Append(" FROM         ProduceSellInfo INNER JOIN ProduceSell ON ProduceSellInfo.MainID = ProduceSell.ID WHERE    ");
            strSql.Append(strWhere);
            if(Convert.ToInt32(lookUpEdit1.EditValue)>0)
                strSql.Append(" And   (ProduceSellInfo.MaterielID = "+Convert.ToInt32(lookUpEdit1.EditValue)+")");
            strSql.Append(" GROUP BY ProduceSell.DateTime, ProduceSellInfo.MaterielID, ProduceSellInfo.BrandID, ProduceSellInfo.ColorID, ProduceSellInfo.SizeID, ProduceSellInfo.Amount ");
            strSql.Append(" ORDER BY ProduceSell.DateTime DESC");
            DataTable dtList = BasicClass.GetDataSet.GetBySql(strSql.ToString());
            gridControl1.DataSource=dtList;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string fileName = Hownet.BaseContranl.BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls", this.Text);
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
                Hownet.BaseContranl.BaseFormClass.OpenFile(fileName);
            }
        }
    }
}