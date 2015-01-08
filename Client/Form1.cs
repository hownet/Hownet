using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Client.ServiceReference1;
using System.IO;

namespace Client
{
    public partial class Form1 : Form
    {
        #region -- 构造函数 --

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region -- 按钮事件 --

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxPath.Text = this.openFileDialog1.FileName;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxDownLoadPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
            //    Stream stream = this.TransferDocument(this.textBoxPath.Text);
            //    String fileName = this.textBoxPath.Text.Substring(this.textBoxPath.Text.LastIndexOf(@"\") + 1);
            //    fileTransportServiceClient.UploadFile(fileName, stream);

            //    fileTransportServiceClient.Close();
            //    stream.Close();
            //    stream.Dispose();
            //    stream = null;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //FileUploadMessage request = new FileUploadMessage();
            //request.FileName = "矮人DOS_4.2.rar";
            //request.SavePath = @"F:\Temp";
            //request.FileData = stream;

            //FileService.IFileTransportServiceChannel channel = fileTransportServiceClient.ChannelFactory.CreateChannel();
            //channel.UploadFile(request);
        }
        private void buttonGetFileList_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
            //    String[] fileNameList = fileTransportServiceClient.GetFileList();
            //    fileTransportServiceClient.Close();

            //    this.listBox1.Items.Clear();
            //    for (int i = 0; i < fileNameList.Length; i++)
            //    {
            //        this.listBox1.Items.Add(fileNameList[i]);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void buttonDownLad_Click(object sender, EventArgs e)
        {
            //if (this.listBox1.SelectedItems.Count > 0)
            //{
            //    FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
            //    Stream fileStream = fileTransportServiceClient.GetFile(this.listBox1.SelectedItems[0].ToString());
            //    fileTransportServiceClient.Close();

            //    if (fileStream.CanRead)
            //    {
            //        try
            //        {
            //            //如果文件目录不存在,创建文件所存放的目录.
            //            if (!Directory.Exists(this.textBoxDownLoadPath.Text))
            //            {
            //                Directory.CreateDirectory(this.textBoxDownLoadPath.Text);
            //            }

            //            Int32 count = 0;
            //            byte[] buffer = new byte[4096];
            //            String saveFilePath = this.textBoxDownLoadPath.Text + @"\" + this.listBox1.SelectedItems[0];
            //            FileStream targetStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write, FileShare.None);

            //            while ((count = fileStream.Read(buffer, 0, 4096)) > 0)
            //            {
            //                targetStream.Write(buffer, 0, count);
            //            }

            //            fileStream.Close();
            //            targetStream.Close();
            //            targetStream = null;
            //            targetStream = null;

            //            MessageBox.Show("文件下载成功!!");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("文件不可读!");
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("请选择您要下载的文件");
            //}
        }

        #endregion

        #region -- 私有方法 -- 

        private Stream TransferDocument(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return stream;
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            //try
            //{

                //FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
                //////DataTable dt = fileTransportServiceClient.GetTable();
                ////BindingList<Hownet.Model.Materiel> li = new BindingList<Hownet.Model.Materiel>(fileTransportServiceClient.GetMateriel());

                ////dataGridView1.DataSource = li;
                //fileTransportServiceClient.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //ServiceClient sc = new ServiceClient();
            //string ss = string.Empty;
            //sc.GetEmpDayAsync("123456", ss);
            //MessageBox.Show(ss);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Hownet.Model.Materiel mod = new Hownet.Model.Materiel();
            //mod.DefaultMeasureID = 3;
            //mod.MaterielName = "asdfasd";
            //List<Hownet.Model.Materiel> li = new List<Hownet.Model.Materiel>();
            //li.Add(mod);
            //FileTransportServiceClient ftc = new FileTransportServiceClient();
            ////DataTable ds = ftc.GetMateriel();
            ////dataGridView1.DataSource = ds;
            //DataSet ds = ftc.GetList("Hownet.BLL.Materiel", "GetAllList",null);
            //dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
