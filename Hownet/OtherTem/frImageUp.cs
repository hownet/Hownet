using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.OtherTem
{
    public partial class frImageUp : DevExpress.XtraEditors.XtraForm
    {
        public frImageUp()
        {
            InitializeComponent();
        }
        Image SourceImg;
        string HashValue = "";
        string bllMA = "Hownet.BLL.MesgAnnex";
        private void frImageUp_Load(object sender, EventArgs e)
        {
            lookUpEdit1.EditValue = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
                HashValue = GetHash();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue == null)
                return;

            byte[] bb = BasicClass.FileUpDown.ImageToByteArray((Image)pictureEdit1.EditValue);
               // pictureEdit1.Image
            string    fileName = BasicClass.FileUpDown.SavePic(bb);
             
            DataTable dt = BasicClass.GetDataSet.GetDS(bllMA, "GetList", new object[] { "1=2" }).Tables[0];
            DataRow dr = dt.NewRow();
            dr["A"] = dr["ID"] = 0;
            dr["MainID"] = dr["InfoID"] = -99;
            dr["FileName"] = fileName;
            dr["Remark"] = memoEdit1.Text.Trim();
            dr["ToID"] = lookUpEdit1.EditValue;
            dr["Remakr1"] = textEdit1.Text.Trim();
            dr["Remark2"] = textEdit2.Text.Trim();
            dr["Remark3"] = textEdit3.Text.Trim();
            dr["Remark4"] = textEdit4.Text.Trim();
            dr["Remark5"] = textEdit5.Text.Trim();
            dr["HashValue"] = HashValue;
            dr["DateTime"] = BasicClass.GetDataSet.GetDateTime();
            dt.Rows.Add(dr);
            BasicClass.GetDataSet.Add(bllMA, dt);
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
    }
}