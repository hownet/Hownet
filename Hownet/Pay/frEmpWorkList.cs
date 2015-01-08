using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Pay
{
    public partial class frEmpWorkList : DevExpress.XtraEditors.XtraForm
    {
        public frEmpWorkList()
        {
            InitializeComponent();
        }
        DateTime dtOne;
        DateTime dtTwo;
        int EmployeeID;
        DataTable dt=new DataTable();
        private void frEmpWorkList_Load(object sender, EventArgs e)
        {
            _leEmp.FormName = (int)BasicClass.Enums.TableType.MiniEmp;
            _ldDtTwo.val = DateTime.Today;
            _ldDtOne.val = DateTime.Today.AddMonths(-1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            dt.Clear();
            EmployeeID = int.Parse(_leEmp.editVal.ToString());
            dtOne = DateTime.Parse(_ldDtOne.val.ToString()).AddSeconds(-1);
            dtTwo = DateTime.Parse(_ldDtTwo.val.ToString()).AddDays(1);
            dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPayInfo, "GetEmpWorkList", new object[] {EmployeeID,dtOne,dtTwo }).Tables[0];
            gridControl1.DataSource = dt;
            ShoChart();
        }
        private void ShoChart()
        {
            chartControl1.Series.Clear();

                    chartControl1.Series.Add(_leEmp.valStr, DevExpress.XtraCharts.ViewType.Line);
                    
                        for (int c = 0; c <dt.Rows.Count; c++)
                        {
                            chartControl1.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint((DateTime.Parse(dt.Rows[c]["DateTime"].ToString())).ToLongDateString(), new object[] { (dt.Rows[c]["Amount"].ToString()) }));
                        }
                  
                    chartControl1.Series[0].Visible = true;

        }
    }
}