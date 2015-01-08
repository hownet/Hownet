using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace BasicClass
{
    public class Linescess
    {
        public string md5One()
        {
            ComputerSn CS = ComputerSn.Instance();
            string cn = FormsAuthentication.HashPasswordForStoringInConfigFile(CS.ComputerName, "Md5");
           // string di = FormsAuthentication.HashPasswordForStoringInConfigFile(CS.DiskID, "Md5");
            string mac= FormsAuthentication.HashPasswordForStoringInConfigFile(CS.MacAddress, "Md5");
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < cn.Length; i++)
            {
                str.Append(cn[i].ToString());
               // str.Append(di[i].ToString());
                str.Append(mac[i].ToString());
            }
            return str.ToString();
        }

    }
}
