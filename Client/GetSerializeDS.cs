using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.ServiceModel;

namespace Client
{
    public class GetSerializeDS
    {
        //private static ChatCallBack callBack = new ChatCallBack();
        //private static InstanceContext instance = new InstanceContext(callBack);
        //public static ServiceReference1.FileTransportServiceClient ftc = new ServiceReference1.FileTransportServiceClient(instance);
        public static string AppDir = Application.StartupPath;

        public static DataSet GetDT(string Bll, string Exc, object[] par)
        {
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            //try
            //{
            //    byte[] da = ft.getZipData(Bll, Exc, par);
            //    ft.Close();
            //    return Byte2DS(da);
            //}
            //catch (Exception ex)
            //{
            //    DataSet ds = new DataSet();
            //    ds.Tables.Add();
            //    return ds;
            //}
            //finally
            //{
            //    ft.Close();
            //}

            try
            {
                byte[] da = ft.GetJson(Bll, Exc, par);
                DataTable dt = ToDataTable(da);
                DataSet ds = new DataSet();
                ds.DataSetName = "ds";
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add();
                return ds;
            }
            finally
            {
                ft.Close();
            }

        }
        public static DataTable GetBySql(string sql)
        {
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                byte[] da = ft.GetBySql(sql);
                DataTable dt = ToDataTable(da);
                dt.TableName = "dt";
                return dt;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                ft.Close();
            }
        }
        public static string GetStringList(string Bll, string Exc, object[] par)
        {
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            string strDS =string.Empty;
            try
            {
                byte[] da = ft.GetStringList(Bll, Exc, par);
                MemoryStream ms = new MemoryStream();
                ms.Write(da, 0, da.Length);
                ms.Position = 0;
                DeflateStream ZipStream = new DeflateStream(ms, CompressionMode.Decompress);
                MemoryStream UnzipStream = new MemoryStream();
                byte[] sDecompressed = new byte[128];
                while (true)
                {
                    int bytesRead = ZipStream.Read(sDecompressed, 0, 128);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    UnzipStream.Write(sDecompressed, 0, bytesRead);
                }
                ZipStream.Close();
                ms.Close();
                UnzipStream.Position = 0;// 解压起始位置设置为头
                StreamReader sr = new StreamReader(UnzipStream);
                strDS = sr.ReadToEnd();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ft.Close();
            }
            return strDS;
        }
        public static DataSet GetDS(string Bll, string Exc, object[] par)
        {
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                byte[] da = ft.getZipData(Bll, Exc, par);
                ft.Close();
                return Byte2DS(da);
            }
            catch (Exception ex)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add();
                return ds;
            }
            finally
            {
                ft.Close();
            }

       }
        /// <summary>
        /// 获取解压缩后的字符串
        /// </summary>
        public static DataTable ToDataTable(byte[] arrbts)
        {
            DataTable dt = new DataTable();
            dt.TableName = "dt";
            try
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(arrbts, 0, arrbts.Length);
                ms.Position = 0;
                DeflateStream ZipStream = new DeflateStream(ms, CompressionMode.Decompress);
                MemoryStream UnzipStream = new MemoryStream();
                byte[] sDecompressed = new byte[128];
                while (true)
                {
                    int bytesRead = ZipStream.Read(sDecompressed, 0, 128);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    UnzipStream.Write(sDecompressed, 0, bytesRead);
                }
                ZipStream.Close();
                ms.Close();
                UnzipStream.Position = 0;// 解压起始位置设置为头
                StreamReader sr = new StreamReader(UnzipStream);
                string strDS = sr.ReadToEnd();


                string[] ss = strDS.Split('й');
                string[] ssColumns = ss[0].Split('ж');
                string[] sc;
                for (int i = 0; i < ssColumns.Length; i++)
                {
                    sc = ssColumns[i].Split('ю');
                    if (sc[0] != string.Empty)
                        dt.Columns.Add(sc[0], System.Type.GetType(sc[1]));
                }
                if (ss.Length > 1)
                {
                    for (int i = 1; i < ss.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        sc = ss[i].Split('ж');
                        if (sc[0] != string.Empty)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                try
                                {
                                    dr[j] = sc[j];
                                }
                                catch (Exception ex)
                                {
                                    dr[j] = DBNull.Value;
                                }
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               
            }
            return dt;
        }
        public static DataSet GetDSForPrcoce(string storedProcName, object[] parameters, string tableName)
        {
            // System.ServiceModel.Dispatcher.DispatchRuntime.AutomaticInputSessionShutdown = false;

            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                byte[] da = ft.GetDSForPrcoce(storedProcName, parameters, tableName);
                ft.Close();
                return Byte2DS(da);
            }
            catch (Exception ex)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add();
                return ds;
            }
            finally
            {
                ft.Close();
            }
        }
        public static int UpOrAdd(string bll, DataTable dt)
        {
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                DataRow[] drs = dt.Select("(A>1)");
                StringBuilder ss = new StringBuilder();

                for (int j = 0; j < drs.Length; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //ss.Append(i);
                        //ss.Append("ю");
                        ss.Append(drs[j][i].ToString());
                        ss.Append("ж");
                    }
                    ss.Append("й");
                }

                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms);
                sw.Write(ss.ToString());
                sw.Close();
                byte[] bsrc = ms.ToArray();
                ms.Close();
                ms.Dispose();
                ms = new MemoryStream();
                ms.Position = 0;
                // 压缩数组序列并返回压缩后的数组
                DeflateStream zipStream = new DeflateStream(ms, CompressionMode.Compress);
                zipStream.Write(bsrc, 0, bsrc.Length);
                zipStream.Close();
                zipStream.Dispose();
                return Convert.ToInt32(ft.GetOne(bll, "UpOrAdd", new object[] { ms.ToArray() }));
            }
            finally
            {
                ft.Close();
            }
           

        }
        public static object GetSingle(string SQLString)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.GetSingle(SQLString);
            }
            finally
            {
                ft.Close();
            }

        }
        public static DataSet GetST()
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                byte[] da = ft.GetSysTem();
                ft.Close();
                return Byte2DS(da);
            }
            finally
            {
                ft.Close();
            }
        }
        public static DataSet GetPU()
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                byte[] da = ft.GetPU();
                ft.Close();
                return Byte2DS(da);
            }
            finally
            {
                ft.Close();
            }
        }
        private static DataSet Byte2DS(byte[] da)
        {
            MemoryStream input = new MemoryStream();
            input.Write(da, 0, da.Length);
            input.Position = 0;
            GZipStream gzip = new GZipStream(input, CompressionMode.Decompress, true);
            MemoryStream output = new MemoryStream();
            byte[] buff = new byte[4096];
            int read = -1;
            read = gzip.Read(buff, 0, buff.Length);
            while (read > 0)
            {
                output.Write(buff, 0, read);
                read = gzip.Read(buff, 0, buff.Length);
            }
            gzip.Close();
            byte[] rebytes = output.ToArray();
            output.Close(); input.Close();
            MemoryStream ms = new MemoryStream(rebytes);
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(ms);
            DataSet ds = (DataSet)obj;
            return ds;
        }
        public static void SetDataTable()
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                ft.SetDataTable();
            }
            finally
            {
                ft.Close();
            }
        }
        public static DateTime GetDateTime()
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.GetDateTime();
            }
            finally
            {
                ft.Close();
            }
        }
        public static DateTime GetLastBackupTime()
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.GetLastBackupTime();
            }
                catch
            {
                return Convert.ToDateTime("1900-1-1");
            }
            finally
            {
                ft.Close();
            }
        }
        public static string CarID(string aa)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.CarID(aa);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                ft.Close();
            }
        }
        public static int Add(string bll, DataTable dt)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.Add(bll, "AddByDt", dt);
            }
                catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                ft.Close();
            }
        }
        public static void Updata(string bll, DataTable dt)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                ft.Updata(bll, "UpdateByDt", dt);
            }
            finally
            {
                ft.Close();
            }
        }
        public static void ExecSql(string bll, string Exc, object[] par)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                ft.ExecSql(bll, Exc, par);
            }
                catch
            {

            }
            finally
            {
                ft.Close();
            }
        }
        public static object GetOne(string bll, string Exc, object[] par)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.GetOne(bll, Exc, par);
            }
            finally
            {
                ft.Close();
            }
        }
        public static void CloseClient()
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                ft.CloseClient();
            }
            finally
            {
                ft.Close();
            }
        }
        public static string AddLog(string UserName)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                return ft.AddLog(UserName);
            }
            finally
            {
                ft.Close();
            }
        }
        public static DataSet GetCardMain(int CardID)
        {
            //ChatCallBack callBack = new ChatCallBack();
            //InstanceContext instance = new InstanceContext(callBack);
            ServiceReference1.FileTransportServiceClient ft = new ServiceReference1.FileTransportServiceClient();
            try
            {
                byte[] da = ft.GetCardMain(CardID);
                ft.Close();
                return Byte2DS(da);
            }
            finally
            {
                ft.Close();
            }
        }
    }
}