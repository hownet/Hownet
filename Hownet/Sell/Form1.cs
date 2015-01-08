using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet.Sell
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DataTable dt = new DataTable();
        private int _ID = 0;
        public Form1(DataTable dtt)
            : this()
        {
            dt = dtt;
        }
        public Form1(int ID)
            : this()
        {
            _ID = ID;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (_ID > 0)
            {

            }
            else
            {
                amountList1.Open(false, dt);
            }
        }
    }
}
