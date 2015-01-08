
#region C#图片处理功能 -- BY DREAMDLM
/*
{*******************************************************************}
{                                                                  }
{                    C#图片处理功能-DREAMDLM                      }
{                                                                  }
{*******************************************************************}
*/
#endregion

using System;using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace BasicClass
{
  public  class ZipJpg
    {
        public ZipJpg()
        {
            //构造函数
        }

        /// <summary>
        /// Bitmap转换byte[]数组
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public byte[] Bmptobyte(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            ms.Flush();
            byte[] buffer = ms.GetBuffer();
            ms.Close();
            return buffer;
        }

        /// <summary>
        /// byte[]数组转换Bitmap
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Bitmap bytetobmp(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            Bitmap bmp = new Bitmap(ms);
            ms.Close();
            return bmp;
        }

        /// <summary>
        /// 返回默认图片
        /// </summary>
        /// <returns></returns>
        public Bitmap getInstance()
        {
            Bitmap bmp = DefaultPic();
            return bmp;
        }

        /// <summary>
        /// 选取本地图片
        /// </summary>
        /// <param name="IMG"></param>
        /// <returns></returns>
        public Bitmap LocalIMG(string IMG)
        {
            FileStream fs = new FileStream(IMG, FileMode.Open);
            Bitmap bmp = new Bitmap(fs);
            fs.Close();
            return bmp;
        }

        /// <summary>
        /// 返回流状态图片
        /// </summary>
        /// <param name="Img"></param>
        /// <returns></returns>
        public Bitmap ImgFromBase64(string Img)
        {
            Bitmap bmp;
            byte[] buffer = Convert.FromBase64String(Img);
            if (buffer.Length > 0)
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(buffer, 0, buffer.Length);
                bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            else
            {
                bmp = DefaultPic() ;
                return bmp;
            }
        }

        /// <summary>
        /// 默认图片
        /// </summary>
        /// <returns></returns>
        private Bitmap DefaultPic()
        {
            FileStream fs = new FileStream( Directory.GetCurrentDirectory()  + @"\Goodr.jpg", FileMode.Open);
            Bitmap bmp = new Bitmap(fs);
            fs.Close();
            return bmp;
        }

        /// <summary>
        /// GDI压缩图片
        /// </summary>
        /// <param name="bmp">传入参数Bitmap</param>
        /// <returns></returns>
        public byte[] ImageGdi(Bitmap bmp)
        {
            Bitmap xbmp = new Bitmap(bmp);
            MemoryStream ms = new MemoryStream();
            xbmp.Save(ms, ImageFormat.Jpeg);
            byte[] buffer;
            ms.Flush();
            if (ms.Length > 95000)
            {
                //buffer = ms.GetBuffer();
                double new_width = 0;
                double new_height = 0;

                Image m_src_image = Image.FromStream(ms);
                if (m_src_image.Width >= m_src_image.Height)
                {
                    new_width = 1024;
                    new_height = new_width * m_src_image.Height / (double)m_src_image.Width;
                }
                else if (m_src_image.Height >= m_src_image.Width)
                {
                    new_height = 768;
                    new_width = new_height * m_src_image.Width / (double)m_src_image.Height;
                }

                Bitmap bbmp = new Bitmap((int)new_width, (int)new_height, m_src_image.PixelFormat);
                Graphics m_graphics = Graphics.FromImage(bbmp);
                m_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                m_graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                m_graphics.DrawImage(m_src_image, 0, 0, bbmp.Width, bbmp.Height);

                ms = new MemoryStream();

                bbmp.Save(ms, ImageFormat.Jpeg);
                buffer = ms.GetBuffer();
                ms.Close();

                return buffer;
            }
            else
            {
                buffer = ms.GetBuffer();
                ms.Close();
                return buffer;
            }
        }
        public static byte[] Ds2Byte(DataSet ds)
        {
            ds.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream unMS = new MemoryStream();
            ser.Serialize(unMS, ds);
            byte[] bytes = unMS.ToArray();
            int lenbyte = bytes.Length;
            MemoryStream compMs = new MemoryStream();
            GZipStream compStream = new GZipStream(compMs, CompressionMode.Compress, true);
            compStream.Write(bytes, 0, lenbyte);
            compStream.Close();
            unMS.Close();
            compMs.Close();
            byte[] zipData = compMs.ToArray();
            return zipData;
        }
        public static object ShowPic(string picName)
        {
            if (picName.Trim() != "")
            {
                if (!BasicFile.FileExists("Mini" + picName))
                {
                    FileUpDown.DownLoad("Mini" + picName, BasicFile.Dir + "Mini" + picName);
                    return FileUpDown.getPicEditValue("Mini" + picName);
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}
