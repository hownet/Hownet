using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace Client
{
    public class ChatCallBack :ServiceReference1.IFileTransportServiceCallback
    {
        public ListBox LstbShowMsg;
        public static ListBox LstbChaters = new ListBox();
        public Label lblCount = new Label();

        private delegate void enterDelegate(string name);
        private delegate void receiveDelegate(string name, string msg);


        public void Receive(string name, string msg)
        {
            if (LstbShowMsg.InvokeRequired)
            {
                LstbShowMsg.Invoke(new receiveDelegate(Receive), name, msg);
            }
            else
            {
                LstbShowMsg.Items.Add(string.Format("{0}说：{1}\r\n", name, msg));
            }

        }

        public void ReceiveWhisper(string name, string msg)
        {
            LstbShowMsg.Items.Add(string.Format("{0}说：{1}\r\n", name, msg));
        }
        /// <summary>
        /// Chater加入时显示信息
        /// </summary>
        /// <param name="name"></param>
        public void UserEnter(string name)
        {

            if (LstbShowMsg.InvokeRequired)
            {
                LstbShowMsg.Invoke(new enterDelegate(UserEnter), name);
            }
            else
            {
                LstbShowMsg.Items.Add(string.Format("欢迎{0}进入聊天室\r\n", name));
                LstbChaters.Items.Add(name);
                lblCount.Text = LstbChaters.Items.Count.ToString();
            }
        }
        /// <summary>
        /// Chater离开时发送信息
        /// </summary>
        /// <param name="name"></param>
        public void UserLeave(string name)
        {
            if (LstbShowMsg.InvokeRequired)
            {
                LstbShowMsg.Invoke(new enterDelegate(UserLeave), name);
            }
            {
                LstbShowMsg.Items.Add(string.Format("{0}离开了聊天室\r\n", name));
                LstbChaters.Items.Remove(name);
                lblCount.Text = LstbChaters.Items.Count.ToString();

            }
        }
        /// <summary>
        /// 加载聊天者
        /// </summary>
        /// <param name="chaters"></param>
        public void LoadUsers(string[] chaters)
        {
            foreach (string chater in chaters)
            {
                if (LstbChaters.Items.Contains(chater))
                {
                    continue;
                }
                LstbChaters.Items.Add(chater);
                lblCount.Text = LstbChaters.Items.Count.ToString();
            }

        }

        #region IFileTransportServiceCallback 成员


        public IAsyncResult BeginReceive(string name, string msg, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceiveWhisper(string name, string msg, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceiveWhisper(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginUserEnter(string name, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndUserEnter(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginUserLeave(string name, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndUserLeave(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginLoadUsers(string[] chaters, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndLoadUsers(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
