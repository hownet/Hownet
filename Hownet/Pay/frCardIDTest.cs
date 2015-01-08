using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Hownet.Pay
{
    public partial class frCardIDTest : Form
    {
        public frCardIDTest()
        {
            InitializeComponent();
        }
        string CardID = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            int car = 0;
            try
            {
                car = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("卡号错误！");
                return;
            }
            //DataTable dt = BasicClass.GetDataSet.GetCardMain(car).Tables[0];
            DataTable dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketIDCard, "GetAllListByCardID", new object[] {car }).Tables[0];
            dataGridView1.DataSource = dt;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BasicClass.GetDataSet.SetDataTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] ss = BasicClass.BasicFile.strIP.Split('.');
            textBox2.Text = BasicClass.GetDataSet.CarID("1" + ss[3] + "+" +textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] ss = BasicClass.BasicFile.strIP.Split('.');
            textBox2.Text = BasicClass.GetDataSet.CarID("1" + ss[3] + "+" +"*0#");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] ss = BasicClass.BasicFile.strIP.Split('.');
            textBox2.Text = BasicClass.GetDataSet.CarID("1" + ss[3] + "+" + "*1#");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            int EMID =Convert.ToInt32( dataGridView1.CurrentRow.Cells["EmployeeID"].Value);
            if (EMID > 0)
            {
                 if (DialogResult.Yes == MessageBox.Show("是否真的删除所选择的记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                if (DialogResult.Yes == MessageBox.Show("请再次确认是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {

                    int _ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["InfoID"].Value);
                     BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllPayInfo, "DelByWTInfoID", new object[] { _ID });
                     BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllWorkTicketInfo, "DelBar", new object[] { _ID });
                     button1_Click(this, EventArgs.Empty);
                     BasicClass.GetDataSet.SetDataTable();
                }
                 }
            }

        }

        private void frCardIDTest_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (13 == (int)e.KeyChar)
            {
                if (textBox1.Text.Trim().Length > 0)
                {
                    CardID = textBox1.Text.Trim();
                    button1_Click(this, EventArgs.Empty);
                    string[] ss = BasicClass.BasicFile.strIP.Split('.');
                    string sssss = BasicClass.GetDataSet.CarID("1" + ss[3] + "+" + textBox1.Text);
                    textBox2.Text = sssss.Split(',')[2];// BasicClass.GetDataSet.CarID("1255+" + textBox1.Text);
                    textBox1.Text = string.Empty;
                }
            }
        }

        private void frCardIDTest_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.MiniEmp", "GetSumAmount", new object[] { Convert.ToInt32(CardID), 0 }).Tables[0];
                dataGridView1.DataSource = dt;
            }
            catch
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.MiniEmp", "GetSumAmount", new object[] { Convert.ToInt32(CardID), 1 }).Tables[0];
                dataGridView1.DataSource = dt;
            }
            catch
            {
            }
        }
    }
}
