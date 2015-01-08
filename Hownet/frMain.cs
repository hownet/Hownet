using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BasicClass;
using System.Reflection;

namespace Hownet
{
    public partial class frMain : DevExpress.XtraEditors.XtraForm
    {
        public frMain()
        {
            InitializeComponent();
        }
        cResult r = new cResult();
        DataTable dt = new DataTable();
        private void frMain_Load(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.Items", "GetPUList", new object[] { BasicClass.UserInfo.UserID }).Tables[0];
           
            timer1.Enabled = true;
            timer1.Start();
            this.MouseWheel += FormSample_MouseWheel;
            timer2.Enabled = true;
            timer2.Start();
        }
        void FormSample_MouseWheel(object sender, MouseEventArgs e)
        {
            //获取光标位置
            Point mousePoint = new Point(e.X, e.Y);
            //换算成相对本窗体的位置
            mousePoint.Offset(this.Location.X, this.Location.Y);
            //判断是否在panel内
            if (panel4.RectangleToScreen(panel4.DisplayRectangle).Contains(mousePoint))
            {
                //滚动
                panel4.AutoScrollPosition = new Point(0, panel4.VerticalScroll.Value - e.Delta);
            }
        }

        private void ActivateNavigation()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                //如果所点的窗口被打开则重新激活
                if (this.MdiChildren[i].Tag.ToString().ToLower() == "nnn")
                {
                    this.MdiChildren[i].Activate();
                    this.MdiChildren[i].Show();
                    this.MdiChildren[i].WindowState = FormWindowState.Normal;
                    break;
                }
            }
        }

        private void frMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                r.ChangeText("C");
                BasicClass.GetDataSet.CloseClient();
                Application.Exit();
            }
            catch
            {
                BasicClass.GetDataSet.CloseClient();
                Application.Exit();
                
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SimpleButton sb=sender as SimpleButton;
            //r.ChangeText(sb.Tag.ToString());
            //ActivateNavigation();
            panel4.Controls.Clear();
            label2.Text = sb.Text;
            dt.DefaultView.RowFilter = "(ParentID=" + sb.Tag + ") ";
            dt.DefaultView.Sort = "Orders";
            if (dt.DefaultView.Count > 0)
            {
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    panel4.Controls.Add(lb);
                    lb.Location = new Point(1, (i * 20) + 5);
                    lb.Text = dt.DefaultView[i]["Text"].ToString();
                    lb.Tag = dt.DefaultView[i]["FormName"].ToString();
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    lb.Click += new System.EventHandler(this.labelControl1_Click);

                }
            }
        }
        private void labelControl1_Click(object sender, EventArgs e)
        {
            Label lc = sender as Label;
            if (lc.Tag.ToString() != string.Empty)
            {
              //  string[] ss = lc.Tag.ToString().Split(',');
                if (!CheckIsOpen(lc.Text))
                {
                    BaseForm.FrmWaitDialog fw = new BaseForm.FrmWaitDialog("请稍候...", "正在打开........");
                    try
                    {

                        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                        // BasicClass.UserInfo.itemsID = int.Parse(tree.FocusedNode.GetValue(tree.Columns[2]).ToString());
                        string formName = lc.Tag.ToString();
                        //if (!BasicClass.BasicFile.IsHavePermissions((int)BasicClass.Enums.Operation.View, ss[0]))
                        //{
                        //    XtraMessageBox.Show("没有权限！");
                        //    return;
                        //}
                        if (OpenFormClass.CheckFormIsOpen(formName.Substring(formName.LastIndexOf('.') + 1)))
                            return;
                        Assembly asm = Assembly.Load("Hownet");//程序集名
                        object frmObj = asm.CreateInstance("Hownet." + formName);//程序集+form的类名。
                        Form frms = (Form)frmObj;
                        if (frms != null)
                        {
                            frms.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                            frms.MdiParent = this;
                            frms.Tag = formName;
                            frms.Show();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        fw.Close();
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                    }
                }
            }
        }
        private bool CheckIsOpen(string formName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                //如果所点的窗口被打开则重新激活
                if (this.MdiChildren[i].Tag.ToString() == formName.ToLower())
                {
                    this.MdiChildren[i].Activate();
                    this.MdiChildren[i].Show();
                    this.MdiChildren[i].WindowState = FormWindowState.Normal;
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string ss = BasicClass.GetDataSet.AddLog(BasicClass.UserInfo.TrueName);
            string[] sss = ss.Split(',');

            if (sss.Length == 3)
            {
                ss = sss[1];
                DateTime dtt = Convert.ToDateTime(sss[2]);
                label1.Text = "本机IP：  " + ss + "  现在时间：" + dtt.ToLongDateString() + dtt.ToShortTimeString();
            }
            else
            {
                DateTime dtt = Convert.ToDateTime(sss[1]);
                label1.Text = "本机IP：  " + sss[0] + "  现在时间：" + dtt.ToLongDateString() + dtt.ToShortTimeString();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BasicClass.GetDataSet.CloseClient();
            }
            catch
            {

            }
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer2.Stop();
            //Form fr = new frNavigation(r);
            //fr.Tag = "nnn";
            //fr.MdiParent = this;
            //fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form fr = new Task.frP2PackList();
            fr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form fr = new WMS.frLinLiaoList();
            fr.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form fr = new Pay.frCardIDTest();
            fr.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form fr = new XtraForm1();
            fr.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form fr = new Sell.frSell1();
            fr.ShowDialog();
        }

 
    }
}