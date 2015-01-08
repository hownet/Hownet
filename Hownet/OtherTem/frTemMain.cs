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
    public partial class frTemMain : DevExpress.XtraEditors.XtraForm
    {
        public frTemMain()
        {
            InitializeComponent();
        }
        Image SourceImg;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Form fr = new OtherTem.frTaskForm();
            fr.ShowDialog();
     
        }

        private void frTemMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                BasicClass.BasicFile.DelDir();
                BasicClass.GetDataSet.CloseClient();
            }
            catch { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form fr = new OtherTem.frOut();
            fr.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
          Form fr=  new WMS.frMaterielList();
          fr.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Form fr = new OtherTem.frSellList();
            fr.ShowDialog();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

            Form fr = new frImageUp();
            fr.ShowDialog();
            
        }

        public String GetHash()
        {
            Image image = ReduceSize();
            Byte[] grayValues = ReduceColor(image);
            Byte average = CalcAverage(grayValues);
            String reslut = ComputeBits(grayValues, average);
            return reslut;
        }

        // Step 1 : Reduce size to 8*8
        private Image ReduceSize(int width = 8, int height = 8)
        {
            Image image = SourceImg.GetThumbnailImage(width, height, () => { return false; }, IntPtr.Zero);
            return image;
        }

        // Step 2 : Reduce Color
        private Byte[] ReduceColor(Image image)
        {
            Bitmap bitMap = new Bitmap(image);
            Byte[] grayValues = new Byte[image.Width * image.Height];

            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = bitMap.GetPixel(x, y);
                    byte grayValue = (byte)((color.R * 30 + color.G * 59 + color.B * 11) / 100);
                    grayValues[x * image.Width + y] = grayValue;
                }
            return grayValues;
        }

        // Step 3 : Average the colors
        private Byte CalcAverage(byte[] values)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
                sum += (int)values[i];
            return Convert.ToByte(sum / values.Length);
        }

        // Step 4 : Compute the bits
        private String ComputeBits(byte[] values, byte averageValue)
        {
            char[] result = new char[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < averageValue)
                    result[i] = '0';
                else
                    result[i] = '1';
            }
            return new String(result);
        }

        // Compare hash
        public static Int32 CalcSimilarDegree(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException();
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    count++;
            }
            return count;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            string resultFile = "";


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\Patch";
            openFileDialog1.Filter = "Image   Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All   files   (*.*)|*.* ";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                resultFile = openFileDialog1.FileName;
            textEdit1.Text = resultFile;
            if (resultFile != "")
            {
                pictureEdit1.Image = SourceImg = Image.FromFile(resultFile);
                textEdit2.EditValue = GetHash();
                MessageBox.Show(CalcSimilarDegree( textEdit2.Text.Trim(),textEdit3.Text.Trim()).ToString());
            }
        }

        private void frTemMain_Load(object sender, EventArgs e)
        {
            //DataTable dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.MesgAnnex", "GetAllList", null).Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    textEdit3.Text = dt.Rows[0]["HashValue"].ToString();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form fr = new FormClient();
            fr.Show();
        }
    }
}