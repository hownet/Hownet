using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ServiceModel;


namespace BasicClass
{
    public class FileUpDown
    {
        //private static Client.ChatCallBack callBack = new Client.ChatCallBack();
        //private static InstanceContext instance = new InstanceContext(callBack);
        /// <summary>
        /// 将服务器上的某个文件下载到本地
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="oldName">文件名，該文件保存到Tem文件夾下</param>
        /// <returns></returns>
        public static string DownLoad(string ServerName, string oldName)
        {
            string strDir = "";
            try
            {
              
                Client.ServiceReference1.FileTransportServiceClient ftc = new Client.ServiceReference1.FileTransportServiceClient();
                Stream fileStream = ftc.GetFile(ServerName);
                ftc.Close();
                //fileTransportServiceClient.Close();
                if (fileStream.CanRead)
                {
                    Int32 count = 0;
                    byte[] buffer = new byte[4096];
                    FileStream targetStream = new FileStream(BasicClass.BasicFile.Dir + oldName, FileMode.Create, FileAccess.Write, FileShare.None);
                    while ((count = fileStream.Read(buffer, 0, 4096)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                    }
                    fileStream.Close();
                    targetStream.Close();
                    targetStream = null;
                    targetStream = null;
                    strDir = oldName;
                }
            }
            catch (Exception ex)
            {

            }
            return strDir;
        }
        /// <summary>
        /// 将本地文件以当前时间重命名上传到服务器
        /// </summary>
        /// <param name="OldName"></param>
        /// <param name="SevName"></param>
        /// <returns></returns>
        public static string UpFile(string OldName,string SevName)
        {
            string fileType = "";
            string sortName = "";
            string newfilename = "";
            Client.ServiceReference1.FileTransportServiceClient ftc = new Client.ServiceReference1.FileTransportServiceClient();
            try
            {
                FileStream stream = new FileStream(OldName, FileMode.Open, FileAccess.Read);
                sortName = OldName.Substring(OldName.LastIndexOf(@"\") + 1);
                if (SevName == "")
                {
                    if (OldName.IndexOf(".") != -1)
                        fileType = OldName.Substring(OldName.LastIndexOf(".")).ToLower();
                    newfilename = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + fileType;
                }
                else
                {
                    newfilename = SevName;
                }
                ftc.UploadFile(newfilename, stream);
                //fileTransportServiceClient.Close();
                stream.Close();
                stream.Dispose();
                stream = null;
                return (sortName + "," + newfilename + "," + fileType);
            }
            catch 
            {
                
                return ("Error");
            }
            finally
            {
                ftc.Close();
            }
        }
        /// <summary>
        /// 删除服务器上的某个文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string DelFile(string FileName)
        {
            Client.ServiceReference1.FileTransportServiceClient ftc = new Client.ServiceReference1.FileTransportServiceClient();
            try
            {
                //FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
                ftc.DelFile(FileName);
                //fileTransportServiceClient.Close();
                return "OK";
            }
            catch 
            {
               
                return ("Error");
            }
            finally
            {
                ftc.Close();
            }
        }
        /// <summary>
        /// 将byte[]形式的图片，保存为当前时间命名的jpg文件并上传到服务器
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="SevName"></param>
        /// <returns></returns>
        public static string UpFile(byte[] bb,string SevName)
        {
            string newfilename = "";
            Client.ServiceReference1.FileTransportServiceClient ftc = new Client.ServiceReference1.FileTransportServiceClient();
            try
            {
                if (!Directory.Exists(BasicFile.Dir))
                    Directory.CreateDirectory(BasicFile.Dir);
                //FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
                if (SevName == "")
                    newfilename = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + ".jpg";
                else
                    newfilename = SevName;
                string dir = BasicFile.Dir + newfilename;
                FileStream stream = new FileStream(dir, FileMode.OpenOrCreate);
                stream.Write(bb, 0, bb.Length);
                stream.Close();
                stream.Dispose();
                FileStream filestream = new FileStream(dir, FileMode.Open, FileAccess.Read);
                ftc.UploadFile(newfilename, filestream);
                //fileTransportServiceClient.Close();
                filestream.Close();
                filestream.Dispose();
                stream = null;
                return newfilename;
            }
            catch 
            {
                return "Error";
            }
            finally
            {
                ftc.Close();
            }
        }
        /// <summary>
        /// 将byte[]形式的图片，保存为以当前时间命名的jpg文件,和"Mini"开头的缩略头
        /// </summary>
        /// <param name="bb"></param>
        /// <returns></returns>
        public static string SavePic(byte[] bb)
        {
            string newfilename = "";
            try
            {
                if (!Directory.Exists(BasicFile.Dir))
                    Directory.CreateDirectory(BasicFile.Dir);
                //FileTransportServiceClient fileTransportServiceClient = new FileTransportServiceClient();
                Client.ServiceReference1.FileTransportServiceClient ftc = new Client.ServiceReference1.FileTransportServiceClient();
                newfilename = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + ".jpg";
                string dir = BasicFile.Dir + newfilename;
                FileStream stream = new FileStream(dir, FileMode.OpenOrCreate);
                stream.Write(bb, 0, bb.Length);
                stream.Close();
                stream.Dispose();
                FileStream filestream = new FileStream(dir, FileMode.Open, FileAccess.Read);
                ftc.UploadFile(newfilename, filestream);
                //fileTransportServiceClient.Close();
              //  GenThumbnail(filestream, BasicFile.Dir + "Mini" + newfilename);
                filestream.Close();
                filestream.Dispose();
                stream = null;
                return newfilename;
            }
            catch 
            {
                return "Error";
            }
        }
        public static byte[] ImageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }

        /// <summary> 
        /// 生成缩略图 静态方法 
        /// </summary> 
        /// <param name="pathImageFrom"> 源图的路径(含文件名及扩展名) </param> 
        /// <param name="pathImageTo"> 生成的缩略图所保存的路径(含文件名及扩展名) 
        /// 注意：扩展名一定要与生成的缩略图格式相对应 </param> 
        /// <param name="width"> 欲生成的缩略图 "画布" 的宽度(像素值) </param> 
        /// <param name="height"> 欲生成的缩略图 "画布" 的高度(像素值) </param> 
        //public static void GenThumbnail(FileStream filestream , string pathImageTo)
        //{
        //     int width=128;
        //     int height = 170;
        //    Image imageFrom = null;
        //    try
        //    {
        //        //imageFrom = Image.FromFile(pathImageFrom);
        //        imageFrom = Image.FromStream(filestream);
        //    }
        //    catch
        //    {
        //        //throw; 
        //    }
        //    if (imageFrom == null)
        //    {
        //        return;
        //    }
        //    // 源图宽度及高度 
        //    int imageFromWidth = imageFrom.Width;
        //    int imageFromHeight = imageFrom.Height;
        //    // 生成的缩略图实际宽度及高度 
        //    int bitmapWidth = width;
        //    int bitmapHeight = height;
        //    // 生成的缩略图在上述"画布"上的位置 
        //    int X = 0;
        //    int Y = 0;
        //    // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置 
        //    if (bitmapHeight * imageFromWidth > bitmapWidth * imageFromHeight)
        //    {
        //        bitmapHeight = imageFromHeight * width / imageFromWidth;
        //        Y = (height - bitmapHeight) / 2;
        //    }
        //    else
        //    {
        //        bitmapWidth = imageFromWidth * height / imageFromHeight;
        //        X = (width - bitmapWidth) / 2;
        //    }
        //    // 创建画布 
        //    Bitmap bmp = new Bitmap(width, height);
        //    Graphics g = Graphics.FromImage(bmp);
        //    // 用白色清空 
        //    g.Clear(Color.White);
        //    // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    // 指定高质量、低速度呈现。 
        //    g.SmoothingMode = SmoothingMode.HighQuality;
        //    // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
        //    g.DrawImage(imageFrom, new Rectangle(X, Y, bitmapWidth, bitmapHeight), new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);
        //    try
        //    {
        //        //经测试 .jpg 格式缩略图大小与质量等最优 
        //        bmp.Save(pathImageTo, ImageFormat.Jpeg);
            
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        //显示释放资源 
        //        imageFrom.Dispose();
        //        bmp.Dispose();
        //        g.Dispose();
        //    }
        //}
        public static bool ExistsFile(string filePath)
        {
            return File.Exists(BasicFile.Dir + filePath);
        }
        /// <summary>
        /// 将本地某个图片以byte[]模式返回
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] getPicEditValue(string filePath)
        {
            byte[] data=new byte[0];
            try
            {
                FileStream ff = new FileStream(BasicFile.Dir + filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                data = new byte[ff.Length];
                ff.Read(data, 0, data.Length);
                ff.Close();
                ff.Dispose();
            }
            catch { }
            return data;
        }
        /// <summary>
        /// 将服务器端某个图片以byte[]模式返回
        /// </summary>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public static byte[] getServerPic(string ServerName)
        {
            if (File.Exists(BasicFile.Dir + ServerName))
            {
                return getPicEditValue(ServerName);
            }
            else
            {
                DownLoad(ServerName, ServerName);
                return getPicEditValue(ServerName);
            }
        }
    }
}
