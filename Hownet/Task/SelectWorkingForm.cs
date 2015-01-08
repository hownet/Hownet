using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Hownet.Task
{
    public partial class SelectWorkingForm : DevExpress.XtraEditors.XtraForm
    {
        public SelectWorkingForm()
        {
            InitializeComponent();
        }
        int _materielID = 0;
        BasicClass.cResult r = new BasicClass.cResult();
        private string bllPW = "Hownet.BLL.ProductWorkingMain";
        public SelectWorkingForm(BasicClass.cResult cr, int MaterielID)
            : this()
        {
            r = cr;
            _materielID = MaterielID;
        }
        private void PrlductWorkingForm_Load(object sender, EventArgs e)
        {
            _lePW.Par = new object[] { "(MaterielID=" + _materielID + ")" };
            _lePW.FormName = (int)BasicClass.Enums.TableType.PW;
        }

        private void _lePW_EditValueChanged(object val, string text)
        {
            _gcGYD.DataSource = BasicClass.GetDataSet.GetDS(bllPW, "GetInfoList", new object[] { int.Parse(val.ToString()) }).Tables[0];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            r.ChangeText(_lePW.editVal.ToString());
            this.Close();
        }

    }
}