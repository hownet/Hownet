using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace BasicClass
{
    /// <summary>
    /// 关于文件、文件夹操作的基础类
    /// </summary>
    public class BasicFile
    {
        private static string _dir = System.Windows.Forms.Application.StartupPath + @"\Tem\";
        private static string _appDir = System.Windows.Forms.Application.StartupPath;
        
        /// <summary>
        /// 获取当前程序所在文件夹下的TEM文件夹
        /// </summary>
        public static string Dir
        {
            set
            {
                _dir = value;
            }
            get
            {
                return _dir;
            }
        }
        public static string AppDir
        {
            get
            {
                return _appDir;
            }
        }
        public static Guid GuidName
        {
            set;
            get;
        }
        public static string strIP
        {
            set;
            get;
        }
        /// <summary>
        /// 如果当前程序所在文件夹下的TEM文件夹不存在，则创建一个
        /// </summary>
        public static void CheckDir()
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
        }
        /// <summary>
        /// 删除当前程序所在文件夹下的TEM文件夹及其下所有文件
        /// </summary>
        public static void DelDir()
        {
            try
            {
                Directory.Delete(Dir, true);
                Directory.Delete(Dir, true);
            }
            catch { }
        }
        /// <summary>
        /// 判断当前程序所在文件夹下的TEM文件夹中某个文件是否存在，不用带路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool FileExists(string fileName)
        {
            bool t=File.Exists(Dir + fileName);
            return t;
        }
        public static  List<Model.SysTem> liST
        {
            get
            {
                Model.SysTemDTT STDTT = new Model.SysTemDTT();
               // DataTable dtt = GetDataSet.GetST().Tables[0];
                return STDTT.DataTableToList(GetDataSet.GetST().Tables[0]);
            }
        }
        public static DataTable dtPU
        {
            get
            {
                return GetDataSet.GetPU().Tables[0];
            }
        }
        public static bool IsHavePermissions(int Operation, string FormName)
        {
            DataRow[] drs = dtPU.Select("(ItemsName='" + FormName + "') And (UserID=" + UserInfo.UserID + ")");
            if (drs.Length == 0)
                return false;
            else
            {
                string _per = drs[0]["PermissionsPropertyID"].ToString();
                string[] per = _per.Split(',');
                if (per.Length == 0)
                    return false;
                else
                {
                    bool t = false;
                    for (int i = 0; i < per.Length; i++)
                    {
                        if (per[i] == Operation.ToString())
                        {
                            t = true;
                            break;
                        }
                    }
                    return t;
                }
            }
        }
        public static string GetPermissions(string FormName,string Texts,int ParentID)
        {
            DataRow[] drs = dtPU.Select("(ItemsName='" + FormName + "') And (UserID=" + UserInfo.UserID + ")");
            if (drs.Length > 0)
            {
                return drs[0]["PermissionsPropertyID"].ToString();
            }
            else
            {
                if(_dtItems==null)
                {
                    _dtItems = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllItems, "GetAllList", null).Tables[0];
                }
                if (_dtItems.Select("(Text='"+FormName+"')").Length == 0)
                {
                    DataTable dtTem = _dtItems.Clone();
                    DataRow dr = dtTem.NewRow();
                    dr["ID"] = dr["A"] = 0;
                    dr["ParentID"] = ParentID;
                    dr["Text"] = FormName;
                    dr["FormName"] = Texts;
                    dr["Parameter"] = string.Empty;
                    dtTem.Rows.Add(dr);
                    dr["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllItems, dtTem);
                    _dtItems.Rows.Add(dr.ItemArray);
                }
                return string.Empty;
            }
        }
        public static string GetPermissions(string FormName)
        {
            DataRow[] drs = dtPU.Select("(ItemsName='" + FormName + "') And (UserID=" + UserInfo.UserID + ")");
            if (drs.Length > 0)
            {
                return drs[0]["PermissionsPropertyID"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        private static DataTable _dtItems;

        public static DataTable dtItems
        {
            set
            {
                _dtItems = value;
            }        
        }
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /**/
        /// <summary> 
        /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num) 
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
    }
}
