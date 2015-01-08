using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frNotWorkAmount : DevExpress.XtraEditors.XtraForm
    {
        public frNotWorkAmount()
        {
            InitializeComponent();
        }
        int _workingID = 0;
        string _workingName = string.Empty;
        public frNotWorkAmount(int WorkingID, string WorkingName)
            : this()
        {
            _workingID = WorkingID;
            _workingName = WorkingName;
        }
        private void frNotWorkAmount_Load(object sender, EventArgs e)
        {
            label1.Text = "工序：" + _workingName + " 在线数量";
            gridControl1.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorking, "GetWorkintNotAmount", new object[] { _workingID }).Tables[0];
        }
    }
}