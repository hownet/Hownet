using BasicClass;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Hownet
{
    public partial class frNavigation : DevExpress.XtraEditors.XtraForm
    {
        public frNavigation()
        {
            InitializeComponent();
        }
        public static cResult r = new cResult();
        private bool _IsClose = false;
        public frNavigation(cResult cr)
            : this()
        {
            r = cr;
        }
        private void frNavigation_Load(object sender, EventArgs e)
        {
            r.TextChanged += new TextChangedHandler(r_TextChanged);
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

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
                xtraTabControl1.SelectedTabPage = xtraTabPage4;
            }
                else if(s=="5")
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage5;
            }
            else if (s == "6")
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage6;
            }
            else if (s == "C")
            {
                _IsClose = true;
                this.Close();
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

        private void frNavigation_FormClosing(object sender, FormClosingEventArgs e)
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
                BasicClass.GetDataSet.CloseClient();
                Application.Exit();
            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            LabelControl lc = sender as LabelControl;
            if (lc.Tag.ToString() != string.Empty)
            {
                string[] ss = lc.Tag.ToString().Split(',');
                if(!CheckIsOpen(ss[0]))
                {
                    BaseForm.FrmWaitDialog fw = new BaseForm.FrmWaitDialog("请稍候...", "正在打开........");
                    try
                    {

                        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                        // BasicClass.UserInfo.itemsID = int.Parse(tree.FocusedNode.GetValue(tree.Columns[2]).ToString());
                        string formName = ss[1];
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
                            frms.MdiParent = this.MdiParent;
                            frms.Tag = ss[0];
                            frms.Show();
                        }
                    }
                        catch(Exception ex)
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

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            LabelControl la = sender as LabelControl;
            la.BackColor = Color.LightBlue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            LabelControl la = sender as LabelControl;
            la.BackColor = Color.FromName("0");
        }

        private void labelControl86_Click(object sender, EventArgs e)
        {
            DataTable dtUser = BasicClass.GetDataSet.GetDS("Hownet.BLL.Users", "GetViewList", null).Tables[0];
            dtUser.DefaultView.Sort = "ID";
            if(BasicClass.UserInfo.UserID==Convert.ToInt32( dtUser.DefaultView[0]["ID"]))
            {
                Form fr = new SystemSet.frPermissions();
                fr.ShowDialog();
            }
        }
    }
}
