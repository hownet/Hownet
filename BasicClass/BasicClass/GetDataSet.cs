using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BasicClass
{
    public class GetDataSet
    {
        //public static DataSet GetDS(string Bll, string Exc, object[] par)
        //{
        //    DataSet ds=Client.GetSerializeDS.GetDT(Bll, Exc, par);
        //    return ds;
        //}
        public static DataTable GetBySql(string sql)
        {
            return Client.GetSerializeDS.GetBySql(sql);
        }
        public static string GetStringList(string Bll, string Exc, object[] par)
        {
            return Client.GetSerializeDS.GetStringList(Bll, Exc, par);
        }
        public static DataSet GetDS(string Bll, string Exc, object[] par)
        {
            DataSet ds = Client.GetSerializeDS.GetDS(Bll, Exc, par);
            return ds;
        }
        public static int UpOrAdd(string bll, DataTable dt)
        {
            return Client.GetSerializeDS.UpOrAdd(bll, dt);
        }
        public static int Add(string bll, DataTable dt)
        {
            return Client.GetSerializeDS.Add(bll, dt);
        }
        public static void UpData(string bll, DataTable dt)
        {
            Client.GetSerializeDS.Updata(bll, dt);
        }
        public static void ExecSql(string bll, string Exc, object[] par)
        {
            Client.GetSerializeDS.ExecSql(bll, Exc, par);
        }
        public static object GetOne(string bll, string Exc, object[] par)
        {
            return Client.GetSerializeDS.GetOne(bll, Exc, par);
        }
        public static void CloseClient()
        {
            Client.GetSerializeDS.CloseClient();
        }
        public static DataSet GetST()
        {
            return Client.GetSerializeDS.GetST();
        }
        public static string AddLog(string UserName)
        {
            return Client.GetSerializeDS.AddLog(UserName);
        }
        public static DataSet GetPU()
        {
            return Client.GetSerializeDS.GetPU();
        }
        public static void SetDataTable()
        {
            Client.GetSerializeDS.SetDataTable();
        }
        public static string CarID(string ss)
        {
            return Client.GetSerializeDS.CarID(ss);
        }
        public static DateTime GetDateTime()
        {
            return Client.GetSerializeDS.GetDateTime();
        }
        public static DateTime GetLastBackupTime()
        {
            return Client.GetSerializeDS.GetLastBackupTime();
        }

        public static DataSet GetCardMain(int CardID)
        {
            return Client.GetSerializeDS.GetCardMain(CardID);
        }
        public static DataSet GetDSForPrcoce(string storedProcName, object[] parameters, string tableName)
        {
            return Client.GetSerializeDS.GetDSForPrcoce(storedProcName, parameters, tableName);
        }
        public static object GetSingle(string SQLString)
        {
            return Client.GetSerializeDS.GetSingle(SQLString);
        }
    }
}
