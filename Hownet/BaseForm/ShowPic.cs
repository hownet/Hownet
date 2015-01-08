using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace Hownet.BaseForm
{
    public partial class ShowPic : DevExpress.XtraEditors.XtraForm
    {
        public ShowPic()
        {
            InitializeComponent();
        }
        string _fileName="";
        /// <summary>
        /// 显示大图片
        /// </summary>
        /// <param name="FileName">图片文件名，不用带路径</param>
        public ShowPic(string FileName)
            : this()
        {
            _fileName = FileName;
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void ShowPic_Load(object sender, EventArgs e)
        {
            if (_fileName != "")
            {
                if (!BasicClass.FileUpDown.ExistsFile(_fileName))
                    BasicClass.FileUpDown.DownLoad(_fileName, _fileName);
                if (BasicClass.FileUpDown.ExistsFile(_fileName))
                    File.Copy(BasicClass.BasicFile.Dir + _fileName, BasicClass.BasicFile.Dir + "234.jpg", true);
            }
            webBrowser1.Url = new System.Uri(Application.StartupPath + "\\ShowPic.html");
        }
    }
}