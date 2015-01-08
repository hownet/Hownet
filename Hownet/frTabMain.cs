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
using DevExpress.XtraBars;

namespace Hownet
{
    public partial class frTabMain : DevExpress.XtraEditors.XtraForm
    {
        string skinMask = "风格: ";
        DataTable dtOT = new DataTable();
        public frTabMain()
        {
            InitializeComponent();
            InitSkins();
            dtOT = BasicClass.GetDataSet.GetBySql("Select 1 as A ,* From OtherType Where (Name=" + BasicClass.UserInfo.UserID + ") And (TypeID=" + (int)BasicClass.Enums.OtherType.风格 + ")");
            if (dtOT.Rows.Count > 0)
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(dtOT.Rows[0]["Value"].ToString());
            else
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Visual Studio 2013 Light");
        }
        cResult r = new cResult();
        DataTable dt = new DataTable();
        private void frMain_Load(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.Items", "GetPUList", new object[] { BasicClass.UserInfo.UserID }).Tables[0];

            timer1.Enabled = true;
            timer1.Start();

            timer2.Enabled = true;
            timer2.Start();
        }
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
            ShowUser();
        }
        private void ShowUser()
        {
            string ss = BasicClass.GetDataSet.AddLog(BasicClass.UserInfo.TrueName);
            string[] sss = ss.Split(',');

            if (sss.Length == 3)
            {
                ss = sss[1];
                DateTime dtt = Convert.ToDateTime(sss[2]);
                barStaticItem2.Caption = "登录用户：" + BasicClass.UserInfo.TrueName + "  本机IP：  " + ss + "  现在时间：" + dtt.ToLongDateString() + dtt.ToShortTimeString();
            }
            else
            {
                DateTime dtt = Convert.ToDateTime(sss[1]);
                barStaticItem2.Caption = "登录用户：" + BasicClass.UserInfo.TrueName + "  本机IP：  " + sss[0] + "  现在时间：" + dtt.ToLongDateString() + dtt.ToShortTimeString();
            }
            if(!timer3.Enabled)
            {
                timer3.Enabled = true;
                timer3.Start();
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
            Form fr = new frNavigations(r);
            fr.Tag = "nnn";
            fr.MdiParent = this;
            fr.Show();
            ShowUser();
            CheckBackup();
            timer3.Enabled = true;
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            CheckBackup();
        }
        private void CheckBackup()
        {
            label2.Visible = false;
            DateTime dtnow = BasicClass.GetDataSet.GetDateTime();
            DateTime dtbackup = BasicClass.GetDataSet.GetLastBackupTime();
            if(dtbackup.AddHours(4)<dtnow)
            {
                label2.Visible = true;
            }
        }
    }
}