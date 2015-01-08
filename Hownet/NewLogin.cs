using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet
{
    public partial class NewLogin : Form
    {
        public NewLogin()
        {
            InitializeComponent();
        }
        BasicClass.PassWord bllPW = new BasicClass.PassWord();
        DataSet ds = new DataSet();
        DataTable dtOT = new DataTable();
        int i = 0;
        string bll = "Hownet.BLL.Users";
        private void NewLogin_Load(object sender, EventArgs e)
        {
            ds = BasicClass.GetDataSet.GetDS(bll, "GetViewList", null);
            comboBox1.DataSource = ds.Tables[0];
            textBox1.Text = "";
            BasicClass.BasicFile.DelDir();
            BasicClass.BasicFile.CheckDir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckUser();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = (int)e.KeyChar;
            if (k == 13)
            {
                CheckUser();
            }
        }
        private void CheckUser()
        {

            if (comboBox1.SelectedValue == null || textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户名和密码不能为空");
                return;
            }
            else
            {
                string pass = bllPW.Encrypt(textBox1.Text.Trim(), "howneter");
                byte[] bb = Convert.FromBase64String(pass);
                Guid g = Guid.NewGuid();
                int userID = int.Parse(comboBox1.SelectedValue.ToString());
                object[] par = new object[] { userID, bb };
                bool ok = (bool)(BasicClass.GetDataSet.GetOne(bll, "CheckUser", par));// true;//
                if (ok)
                {
                    bool t = false;
                    if (!t)
                    {
                        BasicClass.UserInfo.UserID = userID;
                        BasicClass.UserInfo.UserName = comboBox1.Text;
                        DataRow[] drs = ds.Tables[0].Select("(ID=" + userID + ")");
                        BasicClass.UserInfo.UserJobsID = int.Parse(drs[0]["JobsID"].ToString());
                        BasicClass.UserInfo.TrueName = ds.Tables[0].Select("(ID=" + userID + ")")[0]["TrueName"].ToString();
                        BasicClass.UserInfo.UserDepartmentID = int.Parse(drs[0]["DepartmentID"].ToString());
                        string strIP = BasicClass.GetDataSet.AddLog(BasicClass.UserInfo.TrueName);

                        if (strIP.IndexOf('-') > -1)
                        {
                            Program.IsNew = checkBox1.Checked;
                            string[] sss = strIP.Split(',');
                            BasicClass.BasicFile.strIP = sss[1];
                           // BasicClass.BasicFile.GuidName = new Guid(sss[0]);
                            if(dtOT.Rows.Count>0)
                            {
                                if(checkBox1.Checked != Convert.ToBoolean(dtOT.Rows[0]["Value"]))
                                {
                                    dtOT.Rows[0]["Value"] = checkBox1.Checked;
                                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtOT);
                                }
                            }
                            else
                            {
                                DataRow dr = dtOT.NewRow();
                                dr["A"] = 1;
                                dr["ID"] = 0;
                                dr["Name"] = userID;
                                dr["TypeID"] = (int)BasicClass.Enums.OtherType.用户界面;
                                dr["Value"] = checkBox1.Checked;
                                dtOT.Rows.Add(dr);
                                BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllOtherType, dtOT);
                            }
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("该用户已在IP：" + strIP + "的电脑上登录！");
                        }
                    }
                }
                else
                {
                    i += 1;
                    int j = 3 - i;
                    MessageBox.Show("第 " + i.ToString() + " 次密码错误，还剩 " + j.ToString() + " 次重试机会！");
                    if (i == 3)
                    {
                        MessageBox.Show("密码连续三次错误，程序退出！");
                        GC.Collect();
                        Application.Exit();
                    }
                    textBox1.Text = "";
                }
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            BasicClass.cResult r = new BasicClass.cResult();
            int userID = int.Parse(comboBox1.SelectedValue.ToString());
            Form fr = new Hownet.SystemSet.UserOneForm(r, userID*-1);
            fr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userID = int.Parse(comboBox1.SelectedValue.ToString());
            if(userID>0)
            {
                 dtOT = BasicClass.GetDataSet.GetBySql("Select 1 as A ,* From OtherType Where (Name=" + userID + ") And (TypeID=" + (int)BasicClass.Enums.OtherType.用户界面 + ")");
                if(dtOT.Rows.Count>0)
                {
                    checkBox1.Checked = Convert.ToBoolean(dtOT.Rows[0]["Value"]);
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
        }
    }
}