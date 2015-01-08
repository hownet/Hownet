using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraNavBar;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.IO;
using BasicClass;

namespace Hownet
{
    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public partial class frNavigations : DevExpress.XtraEditors.XtraForm
    {
        string Liens = string.Empty;
        Cursor currentCursor;
        DataTable dtOT = new DataTable();
        public frNavigations()
        {
            InitializeComponent();
        }

        public static cResult r = new cResult();
        private bool _IsClose = false;
        public frNavigations(cResult cr)
            : this()
        {
            r = cr;
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            r.TextChanged += new TextChangedHandler(r_TextChanged);
            //try
            //{
            //    Liens = BasicClass.GetDataSet.GetOne("Hownet.BLL.GetHDID", "GetLiness", null).ToString();
            //    if (Liens != string.Empty)
            //    {
            //        int a = 0;
            //        for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            //        {
            //            a = Convert.ToInt32(Liens.Substring(i, 1));
            //            if (a == 0)
            //            {
            //                xtraTabControl1.TabPages[i].PageVisible = false;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //}
            timer1.Enabled = true;
            timer1.Start();
            ShowImage();
            timer2.Enabled = true;
            timer2.Start();
        }
        void r_TextChanged(string s)
        {
            if (s == "1")
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
            }
            else if (s == "2")
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
            }
            else if (s == "3")
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage3;
            }
            else if (s == "4")
            {
                xtraTabControl1.SelectedTabPage = _xtraStock;
            }
            else if (s == "5")
            {
                xtraTabControl1.SelectedTabPage = _xtrTask;
            }
            else if (s == "6")
            {
                xtraTabControl1.SelectedTabPage = _xtrSell;
            }
            else if (s == "C")
            {
                _IsClose = true;
                this.Close();
            }
        }
        private void ShowImage()
        {
            for (int c = 0; c < xtraTabControl1.TabPages.Count; c++)
            {
                for (int i = 0; i < xtraTabControl1.TabPages[c].Controls.Count; i++)
                {
                    if (xtraTabControl1.TabPages[c].Controls[i] is SimpleButton)
                    {
                        SimpleButton ss = xtraTabControl1.TabPages[c].Controls[i] as SimpleButton;
                        if (File.Exists(BasicClass.BasicFile.AppDir + @"\Image\" + ss.Text + ".bmp"))
                        {
                            try
                            {
                                ss.Image = Image.FromFile(BasicClass.BasicFile.AppDir + @"\Image\" + ss.Text + ".bmp");
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        private bool CheckIsOpen(string formName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
            {
                //如果所点的窗口被打开则重新激活
                if (this.MdiParent.MdiChildren[i].Tag.ToString() == formName.ToLower())
                {
                    this.MdiParent.MdiChildren[i].Activate();
                    this.MdiParent.MdiChildren[i].Show();
                    this.MdiParent.MdiChildren[i].WindowState = FormWindowState.Normal;
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        private void ShowFrom(Form fr, string FormName, string Texts, int ParentID)
        {
            BaseForm.FrmWaitDialog fw = new BaseForm.FrmWaitDialog("请稍候...", "正在打开........");
            try
            {
                if (BasicClass.BasicFile.GetPermissions(FormName, Texts, ParentID) != string.Empty)
                {
                    if (!CheckIsOpen(FormName))
                    {
                        fr.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        fr.MdiParent = this.MdiParent;
                        fr.Tag = FormName;
                        fr.Show();
                    }
                }
                else
                {
                    MessageBox.Show("没有权限 ");
                }
            }
            finally
            {
                fw.Close();
            }
        }
        /// <summary>
        /// 生产制单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label9_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            Form fr = new Task.frTaskForm();
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //出工票
        private void label15_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            Form fr = new Pay.frToTicket();
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //生产制单列表
        private void label10_Click(object sender, EventArgs e)
        {
            Form fr = new Task.frTaskList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //颜色 
        private void label1_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frColor();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //尺码
        private void label2_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frSize();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //款号
        private void label3_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frFinished();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //工序
        private void label7_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frWorking();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //部门 
        private void label4_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frDeparment();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //工种
        private void label5_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frWorkType();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //员工
        private void label6_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.BarEmployeeForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //工艺
        private void label8_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.ProductWorkingForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //员工费用记录
        private void label11_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.BarPayCosts();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //数量查看
        private void label12_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.PayLab();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //条码回收
        private void label14_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.WorkticketBack();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //工资统计
        private void label13_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.PaySum();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //手动回收
        private void label16_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.HandBackForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_IsClose)
                {
                    e.Cancel = true;
                }
                else
                {
                    // BasicClass.GetDataSet.CloseClient();
                    //Application.Exit();
                }
            }
            catch
            {
                BasicClass.BasicFile.DelDir();
                BasicClass.GetDataSet.CloseClient();
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.YellowGreen, 5);//设置笔的粗细为,颜色为蓝色
            Graphics g = _xtrTask.CreateGraphics();
            int a, b, c, d;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            p.DashStyle = DashStyle.Solid;//恢复实线
            p.EndCap = LineCap.ArrowAnchor;//定义线尾的样式为箭头

            a = label18.Location.X + label18.Width + 10;
            b = label18.Location.Y + label18.Height / 2;

            c = label3.Location.X - 10;
            d = label3.Location.Y + label3.Height / 2;
            g.DrawLine(p, a, b, c, d);


            a = label3.Location.X + label3.Width + 10;
            b = label3.Location.Y + label3.Height / 2;

            c = label1.Location.X - 10;
            d = label1.Location.Y + label1.Height / 2;
            g.DrawLine(p, a, b, c, d);


            a = label1.Location.X + label1.Width + 10;
            b = label1.Location.Y + label1.Height / 2;

            c = label2.Location.X - 10;
            d = label2.Location.Y + label2.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label2.Location.X + label2.Width + 10;
            b = label2.Location.Y + label2.Height / 2;

            c = label17.Location.X - 10;
            d = label17.Location.Y + label17.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label4.Location.X + label4.Width + 10;
            b = label4.Location.Y + label4.Height / 2;

            c = label5.Location.X - 10;
            d = label5.Location.Y + label5.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label5.Location.X + label5.Width + 10;
            b = label5.Location.Y + label5.Height / 2;

            c = label6.Location.X - 10;
            d = label6.Location.Y + label6.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label6.Location.X + label6.Width + 10;
            b = label6.Location.Y + label6.Height / 2;

            c = label7.Location.X - 10;
            d = label7.Location.Y + label7.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label7.Location.X + label7.Width + 10;
            b = label7.Location.Y + label7.Height / 2;

            c = label8.Location.X - 10;
            d = label8.Location.Y + label8.Height / 2;
            g.DrawLine(p, a, b, c, d);

            p = new Pen(Color.SkyBlue, 5);
            p.DashStyle = DashStyle.Solid;//恢复实线
            p.EndCap = LineCap.ArrowAnchor;//定义线尾的样式为箭头

            a = label19.Location.X + label19.Width + 10;
            b = label19.Location.Y + label19.Height / 2;

            c = label9.Location.X - 10;
            d = label9.Location.Y + label9.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label9.Location.X + label9.Width + 10;
            b = label9.Location.Y + label9.Height / 2;

            c = label15.Location.X - 10;
            d = label15.Location.Y + label15.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label15.Location.X + label15.Width + 10;
            b = label15.Location.Y + label15.Height / 2;

            c = label10.Location.X - 10;
            d = label10.Location.Y + label10.Height / 2;
            g.DrawLine(p, a, b, c, d);

            a = label10.Location.X + label10.Width + 10;
            b = label10.Location.Y + label10.Height / 2;

            c = label12.Location.X - 10;
            d = label12.Location.Y + label12.Height / 2;
            g.DrawLine(p, a, b, c, d);


            //p = new Pen(Color.Bisque, 5);
            //p.DashStyle = DashStyle.Solid;//恢复实线
            //p.EndCap = LineCap.ArrowAnchor;//定义线尾的样式为箭头
            //a = label16.Location.X + label16.Width + 10;
            //b = label16.Location.Y + label16.Height / 2;

            //c = label14.Location.X - 10;
            //d = label14.Location.Y + label14.Height / 2;
            //g.DrawLine(p, a, b, c, d);

            //a = label14.Location.X + label14.Width + 10;
            //b = label14.Location.Y + label14.Height / 2;

            //c = label11.Location.X - 10;
            //d = label11.Location.Y + label11.Height / 2;
            //g.DrawLine(p, a, b, c, d);

            //a = label11.Location.X + label11.Width + 10;
            //b = label11.Location.Y + label11.Height / 2;

            //c = label13.Location.X - 10;
            //d = label13.Location.Y + label13.Height / 2;
            //g.DrawLine(p, a, b, c, d);
        }

        //商标

        private void label17_Click(object sender, EventArgs e)
        {

            Form fr = new BaseForm.frBrandList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.K)
            {
                Form fr = new frEditCardAmount();
                fr.ShowDialog();

            }
        }
        /// <summary>
        /// 物料类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label18_Click(object sender, EventArgs e)
        {

            Form fr = new BaseForm.frMaterielType();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 生产计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label19_Click(object sender, EventArgs e)
        {

            Form fr = new WMS.frTaskBOM();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 成品入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label20_Click(object sender, EventArgs e)
        {

            Form fr = new WMS.frP2D();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 客户订单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label22_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSalesFormList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 成品库存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label21_Click(object sender, EventArgs e)
        {

            Form fr = new WMS.frProductDepotList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 订单分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label23_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSalesList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 客户订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label26_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.SalesForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 市场订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label27_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSalesForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 只扣库存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label28_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSell1();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 需扣订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label29_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSell();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 销售退货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label25_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSellBack();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 销售分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label24_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSellAnalysis();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 物料计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label32_Click(object sender, EventArgs e)
        {

            Form fr = new Stock.frMaterielStructure();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 物料列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label30_Click(object sender, EventArgs e)
        {

            Form fr = new BaseForm.frMateriel();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 单用量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label31_Click(object sender, EventArgs e)
        {

            Form fr = new Materiel.MaterielBom();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 库存列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label36_Click(object sender, EventArgs e)
        {

            Form fr = new WMS.frMaterielList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 申购单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label33_Click(object sender, EventArgs e)
        {

            Form fr = new Stock.frNeedStock();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 采购订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label34_Click(object sender, EventArgs e)
        {

            Form fr = new Stock.frStock();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 采购收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label37_Click(object sender, EventArgs e)
        {

            Form fr = new Stock.frSBack();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 采购退货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label35_Click(object sender, EventArgs e)
        {

            Form fr = new Stock.frStockBackSupp();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 物料进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label38_Click(object sender, EventArgs e)
        {

            Form fr = new Stock.frStockList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label43_Click(object sender, EventArgs e)
        {

            Form fr = new BaseForm.frSupplier();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label44_Click(object sender, EventArgs e)
        {

            Form fr = new BaseForm.frCompany();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 收客户货款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label39_Click(object sender, EventArgs e)
        {

            Form fr = new Finance.BsInMoneyForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 付供应商货款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label40_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.BsOutMoneyForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 对帐单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label41_Click(object sender, EventArgs e)
        {

            Form fr = new Finance.CompanyLogForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 收付款查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label42_Click(object sender, EventArgs e)
        {

            Form fr = new Finance.frMoneyList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        //ID卡使用查询
        private void label45_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frCardIDTest();
            fr.ShowDialog();
        }
        /// <summary>
        /// 伙食费设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label46_Click(object sender, EventArgs e)
        {

            Form fr = new Pay.frCatering();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 员工订、就餐情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label47_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frOrdering();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 生产计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void label49_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frTaskBOM();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        private void label50_Click(object sender, EventArgs e)
        {
            Form fr = new Clothing.frTaskForm();
            fr.ShowDialog();
        }
        /// <summary>
        /// 会计科目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label51_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frKJKM();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 费用项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label52_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frZaiYao();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 费用记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label53_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frKJFL();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        //库存盘点
        private void label55_Click(object sender, EventArgs e)
        {

            Form fr = new WMS.frInventory();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 裁片列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label56_Click(object sender, EventArgs e)
        {

            Form fr = new BaseForm.frCaiPian();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 加工厂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label57_Click(object sender, EventArgs e)
        {

            Form fr = new WeiWai.frCompany();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 车间领料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label58_Click(object sender, EventArgs e)
        {
            Form fr = new Task.frTaskLinLiao();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 生产领料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label59_Click(object sender, EventArgs e)
        {
            //Form fr = new Sell.frSellAllList();
            // fr.Show();
            Form fr = new Task.frLinLiao();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        /// <summary>
        /// 考勤时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.AttendanceSet();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 考勤记录查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frAttendanceRecords();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 成品收货明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Form fr = new Task.frP2PackList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frMeasure();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 采购收货明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frStockInfoList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            //Form fr = new BaseForm.frMesg();
            //fr.ShowDialog();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            //Form fr = new BaseForm.frMesgList();
            //fr.ShowDialog();
        }
        /// <summary>
        /// 成品入包装部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frP2Pack();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 物料加工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frProcessingTask();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BasicClass.GetDataSet.AddLog(BasicClass.UserInfo.TrueName);
        }
        /// <summary>
        /// 生产计划列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frPlanList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 包装部数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frPackAmount();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 包装部入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton15_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frPack2Depot();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 计时工序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton16_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frDayWorking();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 计时工资录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton17_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frAddDayWorking();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 成品采购订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton18_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frFinishedStock();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));

        }
        /// <summary>
        /// 成品采购收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton19_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frFinishedSBack();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 领料明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton20_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frLinLiaoList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 物料加工领料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton21_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frPTaskOut();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 物料加工收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton22_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frPTaskIn();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 只填明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton23_Click(object sender, EventArgs e)
        {

            Form fr = new Sell.frSellOnlyInfo();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 收款记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton24_Click(object sender, EventArgs e)
        {

            Form fr = new Finance.frKJFLInMoney();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 费用列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton25_Click(object sender, EventArgs e)
        {

            Form fr = new Finance.frKJFLList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 流水帐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton26_Click(object sender, EventArgs e)
        {

            Form fr = new Finance.frMoneyListByKJKM();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton27_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frSum();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 记帐凭证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton28_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frVouchers();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 转帐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton29_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frZhuanZhang();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 生产单设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton30_Click(object sender, EventArgs e)
        {
            Form fr = new Task.frTaskSet();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 规格列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton31_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frSpec();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 缝制要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton32_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.FinishedWorkingForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 考勤规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton33_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.AttendanceRulesForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 成品尺寸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton34_Click(object sender, EventArgs e)
        {
            Form fr = new Materiel.MaterielSize();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 工资提成设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Form fr = new Pay.frPayTiChengSet();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton35_Click(object sender, EventArgs e)
        {
            Form fr = new SystemSet.BarUserInfoForm();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton36_Click(object sender, EventArgs e)
        {
            Form fr = new SystemSet.frPermissions();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 仓库调拨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton37_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frStorageAllocation();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }

        /// <summary>
        /// 销售汇总分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton38_Click(object sender, EventArgs e)
        {
            Form fr = new Sell.frSellAllList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 采购查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton39_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frStockAllList();
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ShowFrom(fr, ll.Text, ll.Tag.ToString(), Convert.ToInt32(ll.Parent.Tag));
        }
        /// <summary>
        /// 样版单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton40_Click(object sender, EventArgs e)
        {
            Form fr = new Task.frSample();
            fr.ShowDialog();
        }

    }
}
