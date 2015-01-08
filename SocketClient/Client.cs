using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SanNiuSignal;
using System.Threading;
using System.Net;
using SanNiuSignal.FileCenter;
namespace SocketClient
{
    public partial class Client : Form ,IFileSendMust
    {
        #region TCP客户端区
        private ITxClient TxClient = null;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                TxClient = TxStart.startClient(textBox2.Text, int.Parse(textBox3.Text));
                TxClient.AcceptString += new TxDelegate<IPEndPoint, string>(accptString);//当收到文本数据的时候
                TxClient.dateSuccess += new TxDelegate<IPEndPoint>(sendSuccess);//当对方已经成功收到我数据的时候
                TxClient.EngineClose += new TxDelegate(engineClose);//当客户端引擎完全关闭释放资源的时候
                TxClient.EngineLost += new TxDelegate<string>(engineLost);//当客户端非正常原因断开的时候
                TxClient.ReconnectionStart += new TxDelegate(reconnectionStart);//当自动重连开始的时候
                TxClient.StartResult += new TxDelegate<bool, string>(startResult);//当登录完成的时候
                //TxClient.BufferSize = 12048;//这里大小自己设置，默认为1KB，也就是1024个字节
                TxClient.StartEngine();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        /// <summary>
        /// 接收到文本数据的时候
        /// </summary>
        /// <param name="str"></param>
        private void accptString(IPEndPoint end, string str)
        {
            textBox1.Text = str;
        }
        /// <summary>
        /// 当数据发送成功的时候
        /// </summary>
        private void sendSuccess(IPEndPoint end)
        {
            textBox1.Text = "数据发送成功";
        }
        /// <summary>
        /// 当客户端引擎完全关闭的时候
        /// </summary>
        private void engineClose()
        {
            textBox1.Text = "客户端已经关闭";
            this.button1.Enabled = false;
            this.button2.Enabled = true;
        }
        /// <summary>
        /// 当客户端突然断开的时候
        /// </summary>
        /// <param name="str">断开原因</param>
        private void engineLost(string str)
        {
            MessageBox.Show(str);
        }
        /// <summary>
        /// 当自动重连开始的时候
        /// </summary>
        private void reconnectionStart()
        {
            textBox1.Text = "10秒后自动重连开始";
        }
        /// <summary>
        /// 当登录有结果的时候
        /// </summary>
        /// <param name="b">是否成功</param>
        /// <param name="str">失败或成功原因</param>
        private void startResult(bool b, string str)
        {
            textBox1.Text = str;
            if (b)
            {
                this.button1.Enabled = true;
                this.button2.Enabled = false;
            }
            else
            {
                this.button1.Enabled = false;
                this.button2.Enabled = true;
            }
        }
        /// <summary>
        /// 发送文件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
            TxClient.sendMessage(textBox1.Text);
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Image im = pictureBox1.Image;
            byte[] bytes = objectByte.ConvertImage(im);
            TxClient.sendMessage(bytes);
        }
        #endregion

        #region UDP引擎区
        private IUdpTx udptx = null;
        private IPEndPoint _udpIPEndPoint = null;

        public IPEndPoint UdpIPEndPoint
        {
            get
            {
                if (_udpIPEndPoint == null)
                {
                    IPAddress ipAddress = IPAddress.Parse(textBox6.Text);
                    _udpIPEndPoint = new IPEndPoint(ipAddress, int.Parse(textBox7.Text));
                }
                return _udpIPEndPoint; 
            }
        }
        /// <summary>
        /// UDP启动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            udptx = TxStart.startUdp();
            udptx.Port = 13091;//如果要持续的进行监听,请在这里设置好非0的监听端口，一次性的话可以不用设置
            udptx.AcceptString += new TxDelegate<IPEndPoint, string>(UdpAcceptString);
            udptx.dateSuccess += new TxDelegate<IPEndPoint>(UdpDateSuccess);
            udptx.EngineClose += new TxDelegate(engineClose);
            udptx.EngineLost += new TxDelegate<string>(engineLost);
            udptx.StartEngine();
            button6.Enabled = false;
        }
        /// <summary>
        /// 当接收到来之客户端的信息的时候
        /// </summary>
        /// <param name="state"></param>
        /// <param name="str"></param>
        private void UdpAcceptString(IPEndPoint ipEndPoint, string str)
        {
            _udpIPEndPoint = ipEndPoint;//udp用用的
            textBox5.Text = "收到来之" + ipEndPoint.ToString() + "的信息:" + str;
        }
        /// <summary>
        /// 对方收到我的信息
        /// </summary>
        /// <param name="ipEndPoint"></param>
        private void UdpDateSuccess(IPEndPoint ipEndPoint)
        {
            textBox5.Text = "已向" + ipEndPoint.ToString() + "发送成功";
        }
        /// <summary>
        /// 发送UDP信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
                udptx.sendMessage(UdpIPEndPoint, textBox5.Text);
        }
        #endregion

        #region 文件发送区
        private IFileSend FileSend = null;
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            switch (button3.Text)
            {
                case "发送文件":
                    try
                    {
                        int fileLabel = 0;
                        if (checkBox1.Checked)
                          fileLabel = udptx.SendFile(UdpIPEndPoint, this.textBox4.Text);
                        else
                          fileLabel=  TxClient.SendFile(this.textBox4.Text);
                        label6.Text = fileLabel.ToString();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    break;
                case "暂停发送":
                    try
                    {
                        FileSend.FileStop(int.Parse(label6.Text));
                        button3.Text = "文件续传";
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    break;
                case "文件续传":
                    try
                    {
                        if (checkBox1.Checked)
                            udptx.ContinueFile(UdpIPEndPoint, int.Parse(label6.Text));
                        else
                        TxClient.ContinueFile(int.Parse(label6.Text));
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    break;
            }

        }
        #region IFileSendMust 成员

        /// <summary>
        /// 接收方拒绝接收文件
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileRefuse(int FileLabel)
        {
            MessageBox.Show("对方拒绝接收这个文件");
        }
        /// <summary>
        /// 开始传输
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileStartOn(int FileLabel)
        {
            button3.Text = "暂停发送";
        }
        /// <summary>
        /// 发送完成
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void SendSuccess(int FileLabel)
        {
            button3.Text = "发送文件";
        }

        #endregion

        #region IFileMustBase 成员

        /// <summary>
        /// 文件已中断；状态已自动改为暂停状态；等待对方上线的时候；进行续传；
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        /// <param name="Reason">中断原因</param>
        public void FileBreak(int FileLabel, string Reason)
        {
            MessageBox.Show(Reason);
            button3.Text = "文件续传";
        }
        /// <summary>
        /// 对方已经取消这个文件的传输；我方已经关掉这个传输
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileCancel(int FileLabel)
        {
            button3.Text = "发送文件";
        }
        /// <summary>
        /// 文件开始续传；这时不会触发开始传输的方法
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileContinue(int FileLabel)
        {
            button3.Text = "暂停发送";
        }
        /// <summary>
        /// 文件传输失败
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileFailure(int FileLabel)
        {
            button3.Text = "发送文件";
        }
        /// <summary>
        /// 对方拒绝续传;文件又处于暂停状态；
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileNoContinue(int FileLabel)
        {
            button3.Text = "文件续传";
        }
        /// <summary>
        /// 对方发过来的续传确认信息；你是否同意续传；
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        /// <returns>同意或不同意</returns>
        public bool FileOrNotContingue(int FileLabel)
        {
            MessageBox.Show("是否续传这个文件");
            button3.Text = "暂停发送";
            return true;
        }
        /// <summary>
        /// 对方暂停；我方也已经暂停；等待着对方的再一次请求传输；会在FileOrNotContingue这里触发
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        public void FileStop(int FileLabel)
        {
            button3.Text = "文件续传";
        }
        /// <summary>
        /// 得到文件的进度;每次缓冲区为单位折算成百分比输出进度；这样可以提高效率；
        /// </summary>
        /// <param name="FileLabel">文件标签</param>
        /// <param name="Progress">进度</param>
        public void FileProgress(int FileLabel, int Progress)
        {
            label7.Text = Progress.ToString()+"%";
            progressBar1.Value = Progress;
        }
        #endregion
        /// <summary>
        /// 取消发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FileSend.FileCancel(int.Parse(label6.Text));
                label7.Text = "0%";
                progressBar1.Value = 0;
                button3.Text = "发送文件";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// 启动文件发送系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                FileSend = FileStart.StartFileSend(this);
            }
            catch (Exception Ex) { MessageBox.Show(Ex.Message); return; }
            FileSend.BufferSize = 191230;
            button5.Enabled = false;
        }
        #endregion
        public Client()
        { InitializeComponent(); }

        
    }

}
