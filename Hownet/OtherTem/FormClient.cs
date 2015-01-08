using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Hownet
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        //客户端通信套接字
        Socket sokMsg = null;
        //客户端通信线程
        Thread thrMsg = null;

        #region 1.0 发送连接服务端请求 
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            try
            {
                //1.0创建连接套接字，使用ip4协议，流式传输，tcp链接
                sokMsg = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.0获取要链接的服务端节点
                //2.1获取网络节点对象
                IPAddress address = IPAddress.Parse(txtIp.Text);
                IPEndPoint endPoint = new IPEndPoint(address, int.Parse(txtPort.Text));
                //3 向服务端发送链接请求。
                sokMsg.Connect(endPoint);
                ShowMsg("连接服务器成功！");

                //4 开启通信线程
                thrMsg = new Thread(ReceiveMsg);
                thrMsg.IsBackground = true;
                //win7， win8 需要设置客户端通信线程同步设置，才能在接收文件时打开文件选择框
                thrMsg.SetApartmentState(ApartmentState.STA); 
                thrMsg.Start();
            }
            catch (Exception ex)
            {
                ShowMsg("连接服务器失败！" + ex.Message);
            }
        } 
        #endregion

        bool isReceive = true;
        #region 2.0 接收服务端消息
        void ReceiveMsg()
        {
            //准备一个消息缓冲区域
            byte[] arrMsg = new byte[1024 * 1024 * 1];
            try
            {
                while (isReceive)
                {
                    //接收 服务器发来的数据，因为包含了一个标示符，所以内容的真实长度应该-1
                    int realLength = sokMsg.Receive(arrMsg)-1;
                    switch (arrMsg[0])
                    {
                        //文本
                        case 0:
                            GetMsg(arrMsg,realLength);
                            break;
                        //文件
                        case 1:
                            GetFile(arrMsg,realLength);
                            break;
                        default:
                            ShakeWindow();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                sokMsg.Close();
                sokMsg = null;
                ShowMsg("服务器断开连接！");
            }
        } 
        #endregion

        #region 2.1  接收服务端文本消息 + GetMsg(byte[] arrContent, int realLength)
        /// <summary>
        /// 2.1  接收服务端文本消息
        /// </summary>
        /// <param name="arrContent"></param>
        /// <param name="realLength"></param>
        public void GetMsg(byte[] arrContent, int realLength)
        {
            //获取接收的内容，去掉第一个标示符。
            string strMsg = System.Text.Encoding.UTF8.GetString(arrContent, 1, realLength);
            ShowMsg("服务器说：" + strMsg);
        } 
        #endregion

        #region 2.2 保存文件 + GetFile(byte[] arrContent, int realLength)
        /// <summary>
        /// 2.2 保存文件
        /// </summary>
        /// <param name="arrContent"></param>
        /// <param name="realLength"></param>
        public void GetFile(byte[] arrContent, int realLength)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string savaPath = sfd.FileName;
                //使用文件流，保存文件
                using (System.IO.FileStream fs = new System.IO.FileStream(savaPath, System.IO.FileMode.OpenOrCreate))
                {
                    //将收到的文件数据数组，写入硬盘。
                    fs.Write(arrContent, 1, realLength);
                }
                ShowMsg("保存文件到 【" + savaPath + "】成功！");
            }
        } 
        #endregion

        #region 2.3 抖屏 + void ShakeWindow()
        Random ran = new Random();
        /// <summary>
        /// 抖屏
        /// </summary>
        public void ShakeWindow()
        {
            // 保存当前窗体位置
            Point oldPoint = this.Location;
            for (int i = 0; i < 15; i++)
            {
                //随机生成新位置
                Point newPoint = new Point(oldPoint.X + ran.Next(8), oldPoint.Y + ran.Next(8));
                //将新位置设置给窗体
                this.Location = newPoint;
                System.Threading.Thread.Sleep(25);
                this.Location = oldPoint;
                //休息15毫秒
                System.Threading.Thread.Sleep(25);
            }
        } 
        #endregion

        #region 3.0 客户端发送消息到服务端
        /// <summary>
        /// 客户端发送消息到服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            string strMsg = txtInput.Text.Trim();
            if (strMsg != "")
            {
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
                try
                {
                    sokMsg.Send(arrMsg);
                }
                catch (Exception ex)
                {
                    ShowMsg("发送消息失败！" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("未输入任何信息！");
            }
        } 
        #endregion

        #region 4.0 展示消息方法 + ShowMsg(string strmsg)
        void ShowMsg(string strmsg)
        {
            try
            {
                this.txtShow.AppendText(strmsg + "\r\n");
            }
            catch (Exception ex)
            {

            }
        } 
        #endregion

        private void txtSendFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("参考服务端发送!");
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("参考服务端发送!");
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            sokMsg.Close();
            
        }
    }
}
