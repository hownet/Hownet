using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Finance
{
    public partial class frSum : DevExpress.XtraEditors.XtraForm
    {
        public frSum()
        {
            InitializeComponent();
        }
        DateTime dtOne;
        DateTime dtTwo;
        private void frSum_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date;
            gridView1.OptionsBehavior.Editable = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dtOne = Convert.ToDateTime(dateEdit1.EditValue).Date.AddMilliseconds(-1);
            dtTwo = Convert.ToDateTime(dateEdit2.EditValue).Date.AddDays(1);
            gridView1.Columns.Clear();
            if (radioGroup1.SelectedIndex < 0)
                return;
            else if (radioGroup1.SelectedIndex == 0)
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBack, "GetSum", new object[] { dtOne, dtTwo, (int)BasicClass.Enums.TableType.StockBack }).Tables[0];
            else if(radioGroup1.SelectedIndex==1)
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllStockBack, "GetSum", new object[] { dtOne, dtTwo, (int)BasicClass.Enums.TableType.StockBackSupp }).Tables[0];
            else if (radioGroup1.SelectedIndex == 2)
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSell, "GetSum", new object[] { dtOne, dtTwo, (int)BasicClass.Enums.TableType.Sell }).Tables[0];
            else if (radioGroup1.SelectedIndex == 3)
                gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProduceSell, "GetSum", new object[] { dtOne, dtTwo, (int)BasicClass.Enums.TableType.SellBack }).Tables[0];

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string fileName =BaseContranl. BaseFormClass.ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                gridView1.ExportToXls(fileName);
              BaseContranl.  BaseFormClass.OpenFile(fileName);
            }
        }



    }
}