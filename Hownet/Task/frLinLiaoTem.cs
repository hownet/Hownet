using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Task
{
    public partial class frLinLiaoTem : DevExpress.XtraEditors.XtraForm
    {
        public frLinLiaoTem()
        {
            InitializeComponent();
        }

        BasicClass.cResult r = new BasicClass.cResult();
        decimal _SumAmount = 0;
        public frLinLiaoTem(BasicClass.cResult cr, decimal SumAmount)
            : this()
        {
            r = cr;
            _SumAmount = SumAmount;
        }
        private void frLinLiaoTem_Load(object sender, EventArgs e)
        {
            textEdit1.EditValue = _SumAmount;
            textEdit2.EditValue = DBNull.Value;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit2.EditValue==DBNull.Value)
            {
                this.Close();

            }
            else
            {
                decimal amount = 0;
                try
                {
                    amount = Convert.ToDecimal(textEdit2.EditValue);
                    if (amount > _SumAmount)
                    {
                        XtraMessageBox.Show("不能超过总数量");
                        textEdit2.EditValue = _SumAmount;
                    }
                    else
                    {
                        r.ChangeText(amount.ToString());
                        this.Close();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("只能填写数字");
                    textEdit2.EditValue = DBNull.Value;
                }
            }
        }
    }
}