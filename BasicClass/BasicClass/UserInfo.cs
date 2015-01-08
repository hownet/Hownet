using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BasicClass
{
    public class UserInfo
    {
        public static string UserName = "";
        public static int UserID = 0;
        public static int UserDepartmentID = 0;
        public static int UserJobsID = 0;
        public static bool IsReg = true;
        public static string TrueName = "";
        //public static List<Hownet.Model.SysTem> ListSt = new List<Hownet.Model.SysTem>();
        public static string conn = "";
        public static string strIP = "";
        /// <summary>
        /// х╗оч
        /// </summary>
        public static int itemsID = 0;
        public static string UserPU = string.Empty;
        public static string DepType = "";
        public static DataTable dtPer = new DataTable();
        public static string GetPer()
        {
            string per = "123";
            //DataRow[] drs = dtPer.Select("ItemsID='" + itemsID + "'");
            //if (drs.Length > 0)
            //    per = drs[0]["Permissions"].ToString();
            return per;
        }
    }
}
