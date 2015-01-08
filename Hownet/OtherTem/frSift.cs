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
    public partial class frSift : DevExpress.XtraEditors.XtraForm
    {
        public frSift()
        {
            InitializeComponent();
        }

        private void frSift_Load(object sender, EventArgs e)
        {
            SimpleMatrix A = new SimpleMatrix(4, 4);
            A[0, 0] = 5; A[0, 1] = 3; A[0, 2] = -1; A[0, 3] = 0;
            A[1, 0] = 2; A[1, 1] = 0; A[1, 2] = 4; A[1, 3] = 1;
            A[2, 0] = -3; A[2, 1] = 3; A[2, 2] = -3; A[2, 3] = 5;
            A[3, 0] = 0; A[3, 1] = 6; A[3, 2] = -2; A[3, 3] = 3;

            SimpleMatrix b = new SimpleMatrix(4, 1);
            b[0, 0] = 11; b[1, 0] = 1; b[2, 0] = -2; b[3, 0] = 9;

            Console.WriteLine("Correct results should be: (1.0, 2.0, 0.0, -1.0)\n");
            Console.WriteLine("CALCULATING");
            A.SolveLinear(b);
            Console.WriteLine("Results:");
            for (int n = 0; n < 4; ++n)
                Console.WriteLine("b[{0}] = {1}", n, b[n, 0]);
        }
    }
}
	

