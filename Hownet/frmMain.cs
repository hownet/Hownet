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

namespace Hownet
{
    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {

        DataTable dt = new DataTable();
        string skinMask = "风格: ";
        string Liens = string.Empty;

        const string imageFormName = "image";
        const string textFormName = "text";
        const string textRTFFormName = "rtf";
        Cursor currentCursor;
        DataTable dtOT = new DataTable();
        public frmMain()
        {
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");
            InitializeComponent();
            InitSkins();
            dtOT = BasicClass.GetDataSet.GetBySql("Select 1 as A ,* From OtherType Where (Name=" + BasicClass.UserInfo.UserID + ") And (TypeID=" + (int)BasicClass.Enums.OtherType.风格 + ")");
            if (dtOT.Rows.Count > 0)
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(dtOT.Rows[0]["Value"].ToString());
            else
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Visual Studio 2013 Light");
        }


        #region Skins

        void InitSkins()
        {
            barManager1.ForceInitialize();
            foreach (DevExpress.Skins.SkinContainer cnt in DevExpress.Skins.SkinManager.Default.Skins)
            {
                BarButtonItem item = new BarButtonItem(barManager1, skinMask + cnt.SkinName);
                iPaintStyle.AddItem(item);
                item.ItemClick += new ItemClickEventHandler(OnSkinClick);
            }
        }
        void OnSkinClick(object sender, ItemClickEventArgs e)
        {
            string skinName = e.Item.Caption.Replace(skinMask, "");
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            barManager1.GetController().PaintStyleName = "Skin";
            iPaintStyle.Caption = e.Item.Caption;
            iPaintStyle.Hint = iPaintStyle.Caption;
            iPaintStyle.ImageIndex = -1;
            if (dtOT.Rows.Count > 0)
            {
                if (skinName != dtOT.Rows[0]["Value"].ToString())
                {
                    dtOT.Rows[0]["Value"] = skinName;
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtOT);
                }
            }
            else
            {
                DataRow dr = dtOT.NewRow();
                dr["A"] = 1;
                dr["ID"] = 0;
                dr["Name"] = BasicClass.UserInfo.UserID;
                dr["TypeID"] = (int)BasicClass.Enums.OtherType.风格;
                dr["Value"] = skinName;
                dtOT.Rows.Add(dr);
                BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dtOT);
            }
        }
        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Liens = BasicClass.GetDataSet.GetOne("Hownet.BLL.GetHDID", "GetLiness", null).ToString();
                if (Liens != string.Empty)
                {
                    int a = 0;
                    for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
                    {
                        a = Convert.ToInt32(Liens.Substring(i, 1));
                        if (a == 0)
                        {
                            xtraTabControl1.TabPages[i].PageVisible = false;
                        }
                    }
                }
            }
            catch
            {
            }
            timer1.Enabled = true;
            timer1.Start();
            ShowImage();
            timer2.Enabled = true;
            timer2.Start();
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
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ll.BackColor = Color.Pink;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            ll.BackColor = Color.Transparent;
        }
        /// <summary>
        /// 生产制单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label9_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Task.frTaskForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
           
        }
        //出工票
        private void label15_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.frToTicket();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //生产制单列表
        private void label10_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("生产制单列表") != string.Empty)
            {
                Form fr = new Task.frTaskList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //颜色 
        private void label1_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.frColor();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //尺码
        private void label2_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.frSize();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //款号
        private void label3_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.frFinished();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //工序
        private void label7_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.frWorking();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //部门 
        private void label4_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.frDeparment();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //工种
        private void label5_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.frWorkType();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //员工
        private void label6_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new BaseForm.BarEmployeeForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //工艺
        private void label8_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.ProductWorkingForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //费用记录
        private void label11_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.BarPayCosts();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //数量查看
        private void label12_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.PayLab();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //条码回收
        private void label14_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.WorkticketBack();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //工资统计
        private void label13_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.PaySum();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //手动回收
        private void label16_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton ll = sender as DevExpress.XtraEditors.SimpleButton;
            if (BasicClass.BasicFile.GetPermissions(ll.Text) != string.Empty)
            {
                Form fr = new Pay.HandBackForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                BasicClass.BasicFile.DelDir();
                BasicClass.GetDataSet.CloseClient();
            }
            catch { }
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
            if (BasicClass.BasicFile.GetPermissions("商标") != string.Empty)
            {
                Form fr = new BaseForm.frBrandList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.K)
            {
                Form fr = new frEditCardAmount();
                fr.ShowDialog();

            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("物料类别") != string.Empty)
            {
                Form fr = new BaseForm.frMaterielType();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("生产计划") != string.Empty)
            {
                Form fr = new WMS.frTaskBOM();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("成品入库") != string.Empty)
            {
                Form fr = new WMS.frP2D();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("客户订单列表") != string.Empty)
            {
                Form fr = new Sell.frSalesFormList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("成品库存") != string.Empty)
            {
                Form fr = new WMS.frProductDepotList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("订单分析") != string.Empty)
            {
                Form fr = new Sell.frSalesList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("客户订单") != string.Empty)
            {
                Form fr = new Sell.SalesForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("客户订单") != string.Empty)
            {
                Form fr = new Sell.frSalesForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("销售发货") != string.Empty)
            {
                Form fr = new Sell.frSell2();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label29_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("销售发货") != string.Empty)
            {
                Form fr = new Sell.frSell();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("销售退货") != string.Empty)
            {
                Form fr = new Sell.frSellBack();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("销售分析") != string.Empty)
            {
                Form fr = new Sell.frSellAnalysis();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label32_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("物料计划") != string.Empty)
            {
                Form fr = new Stock.frMaterielStructure();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("物料列表") != string.Empty)
            {
                Form fr = new BaseForm.frMateriel();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("单用量") != string.Empty)
            {
                Form fr = new Materiel.MaterielBom();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label36_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("库存列表") != string.Empty)
            {
                Form fr = new WMS.frMaterielList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label33_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("申购单") != string.Empty)
            {
                Form fr = new Stock.frNeedStock();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label34_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("采购订单") != string.Empty)
            {
                Form fr = new Stock.frStock();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label37_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("采购收货") != string.Empty)
            {
                Form fr = new Stock.frSBack();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label35_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("采购退货") != string.Empty)
            {
                Form fr = new Stock.frStockBackSupp();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label38_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("物料进度") != string.Empty)
            {
                Form fr = new Stock.frStockList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label43_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("供应商") != string.Empty)
            {
                Form fr = new BaseForm.frSupplier();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label44_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("客户列表") != string.Empty)
            {
                Form fr = new BaseForm.frCompany();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label39_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("收客户货款") != string.Empty)
            {
                Form fr = new Finance.BsInMoneyForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("付供应商货款") != string.Empty)
            {
                Form fr = new Finance.BsOutMoneyForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }

        }

        private void label41_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("对帐单") != string.Empty)
            {
                Form fr = new Finance.CompanyLogForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label42_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("收付款查询") != string.Empty)
            {
                Form fr = new Finance.frMoneyList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }
        //ID卡使用查询
        private void label45_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frCardIDTest();
            fr.ShowDialog();
        }

        private void label46_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("伙食费设置") != string.Empty)
            {
                Form fr = new Pay.frCatering();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label47_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("员工订、就餐情况") != string.Empty)
            {
                Form fr = new Pay.frOrdering();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label48_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("客户订单") != string.Empty)
            {
                Form fr = new Sell.SalesForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label49_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("生产计划") != string.Empty)
            {
                Form fr = new WMS.frTaskBOM();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label50_Click(object sender, EventArgs e)
        {
            Form fr = new Clothing.frTaskForm();
            fr.ShowDialog();
        }

        private void label51_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frKJKM();
            fr.ShowDialog();
        }

        private void label52_Click(object sender, EventArgs e)
        {
            Form fr = new Finance.frZaiYao();
            fr.ShowDialog();
        }

        private void label53_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("费用记录") != string.Empty)
            {
                Form fr = new Finance.frKJFL();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Form fr = new Sell.frSample();
            //fr.Show();
        }
        //库存盘点
        private void label55_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("库存盘点") != string.Empty)
            {
                Form fr = new WMS.frInventory();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label56_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("裁片列表") != string.Empty)
            {
                Form fr = new BaseForm.frCaiPian();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label57_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("加工厂") != string.Empty)
            {
                Form fr = new WeiWai.frCompany();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label58_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("车间领料") != string.Empty)
            {
                Form fr = new Task.frTaskLinLiao();

                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void label59_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("生产领料") != string.Empty)
            {
                Form fr = new Task.frLinLiao();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("考勤时间") != string.Empty)
            {
                Form fr = new Pay.AttendanceSet();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (BasicClass.BasicFile.GetPermissions("考勤记录查询") != string.Empty)
            {
                Form fr = new Pay.frAttendanceRecords();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限");
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //Form fr = new frP2DList();
            //fr.ShowDialog();
            Form fr = new Task.frP2PackList();
            fr.ShowDialog();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frMeasure();
            fr.ShowDialog();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frStockInfoList();
            fr.ShowDialog();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            //Form fr = new BaseForm.frMesg();
            //fr.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("用户列表") != string.Empty)
            {
                Form fr = new SystemSet.BarUserInfoForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            //Form fr = new BaseForm.frMesgList();
            //fr.ShowDialog();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            //  Form fr = new WMS.frStorageAllocation();
            // Form fr = new XtraForm1();
            Form fr = new WMS.frP2Pack();
            fr.ShowDialog();
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frProcessingTask();
            fr.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BasicClass.GetDataSet.AddLog(BasicClass.UserInfo.TrueName);
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frPlanList();
            fr.ShowDialog();
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frPackAmount();
            fr.ShowDialog();
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frPack2Depot();
            fr.ShowDialog();
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frDayWorking();
            fr.ShowDialog();
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frAddDayWorking();
            fr.ShowDialog();
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("成品采购订单") != string.Empty)
            {
                Form fr = new Stock.frFinishedStock();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }

        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("成品采购收货") != string.Empty)
            {
                Form fr = new Stock.frFinishedSBack();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frLinLiaoList();
            fr.ShowDialog();
        }

        private void simpleButton21_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frPTaskOut();
            fr.ShowDialog();
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            Form fr = new Stock.frPTaskIn();
            fr.ShowDialog();
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("销售发货") != string.Empty)
            {
                Form fr = new Sell.frSellOnlyInfo();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("收款记录") != string.Empty)
            {
                Form fr = new Finance.frKJFLInMoney();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("费用列表") != string.Empty)
            {
                Form fr = new Finance.frKJFLList();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {

            if (BasicClass.BasicFile.GetPermissions("流水帐") != string.Empty)
            {
                Form fr = new Finance.frMoneyListByKJKM();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton27_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("汇总") != string.Empty)
            {
                Form fr = new Finance.frSum();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton28_Click(object sender, EventArgs e)
        {
if (BasicClass.BasicFile.GetPermissions("记帐凭证") != string.Empty)
            {
                Form fr = new Finance.frVouchers();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton29_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("转帐") != string.Empty)
            {
                Form fr = new Finance.frZhuanZhang();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限 ");
            }
        }

        private void simpleButton30_Click(object sender, EventArgs e)
        {
            Form fr=new Task.frTaskSet();
            fr.ShowDialog();
        }

        private void simpleButton31_Click(object sender, EventArgs e)
        {
            Form fr = new BaseForm.frSpec();
            fr.ShowDialog();
        }

        private void simpleButton32_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.FinishedWorkingForm();
            fr.ShowDialog();
        }

        private void simpleButton33_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.GetPermissions("考勤规则") != string.Empty)
            {
                Form fr = new Pay.AttendanceRulesForm();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限");
            }
        }

        private void simpleButton34_Click(object sender, EventArgs e)
        {
            Form fr = new Materiel.MaterielSize();
            fr.ShowDialog();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Form fr = new Pay.frPayTiChengSet();
            fr.ShowDialog();
        }

    }



}
