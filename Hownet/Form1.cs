using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Hownet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        Hownet.BLL.MaterielCompany bllMC = new BLL.MaterielCompany();
        private void Form1_Load(object sender, EventArgs e)
        {
            //gridView1.OptionsView.ColumnAutoWidth = false;
            //gridControl1.DataSource = bllMC.GetModelList("");
            //for (int i = 0; i < gridView1.Columns.Count; i++)
            //{
            //    gridView1.Columns[i].GetBestWidth();
            //}
            //listView1.Columns.Clear();//清空列记录
            //ColumnHeader cZh = new ColumnHeader();//创建一个列
            //cZh.Text = "英文";//列名
            //ColumnHeader cCh = new ColumnHeader();
            //cCh.Text = "中文";
            //listView1.Columns.AddRange(new ColumnHeader[] { cZh, cCh });//将这两列加入listView1
            //listView1.View = View.Details;//列的显示模式
            //ListViewItem lvi = new ListViewItem(new string[] { "Gog", "狗1" }, -1);//创建列表项
            //ListViewItem lvi1 = new ListViewItem(new string[] { "Gog", "狗2" }, -1);//创建列表项
            //ListViewItem lvi2 = new ListViewItem(new string[] { "Gog", "狗3" }, -1);//创建列表项
            //ListViewItem lvi3 = new ListViewItem(new string[] { "Gog", "狗4" }, -1);//创建列表项
            //listView1.Items.Add(lvi);//将项加入listView1列表中
            //listView1.Items.Add(lvi1);
            //listView1.Items.Add(lvi2);
            //listView1.Items.Add(lvi3);
            this.MouseWheel += FormSample_MouseWheel;
        }
         void FormSample_MouseWheel(object sender, MouseEventArgs e)
         {
             //获取光标位置
             Point mousePoint = new Point(e.X, e.Y);
             //换算成相对本窗体的位置
             mousePoint.Offset(this.Location.X, this.Location.Y);
             //判断是否在panel内
             if (panel1.RectangleToScreen(panel1.DisplayRectangle).Contains(mousePoint))
             {
                 //滚动
                 panel1.AutoScrollPosition = new Point(0, panel1.VerticalScroll.Value - e.Delta);
             }
         }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    MessageBox.Show(listView1.SelectedItems[0].Index.ToString());
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Height = 200;
            Point p = new Point(64, 250);
            panel2.Location=p;
            label1.Text = label2.Text;
            panel1.Controls.Clear();
            for(int i=0;i<10;i++)
            {
                Label lb = new Label();
                lb.AutoSize = false;
                lb.BorderStyle = BorderStyle.FixedSingle;
                panel1.Controls.Add(lb);
                lb.Location = new Point(1, (i * 20) + 2);
                lb.Text = "aaaa" + i.ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel1.Height = 300;
            Point p = new Point(64, 350);
            panel2.Location = p;
            label1.Text = label3.Text;
            panel1.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                Label lb = new Label();
                lb.AutoSize = false;
                lb.BorderStyle = BorderStyle.FixedSingle;
                panel1.Controls.Add(lb);
                lb.Location = new Point(1, (i * 20) + 2);
                lb.Text = "bbbb" + i.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel1.Height = 100;
            Point p = new Point(64, 150);
            panel2.Location = p;
            label1.Text = label4.Text;
            panel1.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                Label lb = new Label();
                lb.AutoSize = false;
                lb.BorderStyle = BorderStyle.FixedSingle;
                panel1.Controls.Add(lb);
                lb.Location = new Point(1, (i * 20) + 2);
                lb.Text = "cccc" + i.ToString();
            }
        }
    }
}
