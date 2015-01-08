using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;

namespace Hownet
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Login());
        //}
        [STAThread]
        static void Main(string[] args)
        {
            //int[] KeyHandle = new int[8];
            //int[] KeyNumber = new int[1];


            //long APPID = 0x19800327;//应用程序标识由工具生)成

            ////查找加密锁
            //int Rtn = DogAPI.NoxFind((int)APPID, KeyHandle, KeyNumber);
            //if (KeyNumber[0] == 0)
            //{
            //    //MessageBox.Show("未找到加密狗!");
            //    //return;
            //    BasicClass.GetHDID.IsReg = false;
            //}

            ////打开加密锁 用户密码由工具生成
            //string uPin = "008e86f2a3434757";
            //Rtn = DogAPI.NoxOpen(KeyHandle[0], uPin);
            //if (Rtn > 0)
            //{
            //    //MessageBox.Show("加密狗不正确!");
            //    //return;
            //    BasicClass.GetHDID.IsReg = false;
            //}

            if (args.Length == 0) //没有传送参数
            {
                Process p = BasicClass.SingleInstance.GetRunningInstance();
                if (p != null) //已经有应用程序副本执行
                {
                    BasicClass.SingleInstance.HandleRunningInstance(p);
                }
                else //启动第一个应用程序
                {
                    RunApplication();
                }
            }
            else //有多个参数
            {
                switch (args[0].ToLower())
                {
                    case "-api":
                        if (BasicClass.SingleInstance.HandleRunningInstance() == false)
                        {
                            RunApplication();
                        }
                        break;
                    case "-mutex":
                        if (args.Length >= 2) //参数中传入互斥体名称
                        {
                            if (BasicClass.SingleInstance.CreateMutex(args[1]))
                            {
                                RunApplication();
                                BasicClass.SingleInstance.ReleaseMutex();
                            }
                            else
                            {
                                //调用SingleInstance.HandleRunningInstance()方法显示到前台。
                                MessageBox.Show("程序已经运行！");
                            }
                        }
                        else
                        {
                            if (BasicClass.SingleInstance.CreateMutex())
                            {
                                RunApplication();
                                BasicClass.SingleInstance.ReleaseMutex();
                            }
                            else
                            {
                                //调用SingleInstance.HandleRunningInstance()方法显示到前台。
                                MessageBox.Show("程序已经运行！");
                            }
                        }
                        break;
                    case "-flag"://使用该方式需要在程序退出时调用
                        if (args.Length >= 2) //参数中传入运行标志文件名称
                        {
                            BasicClass.SingleInstance.RunFlag = args[1];
                        }
                        try
                        {
                            if (BasicClass.SingleInstance.InitRunFlag())
                            {
                                RunApplication();
                                BasicClass.SingleInstance.DisposeRunFlag();
                            }
                            else
                            {
                                //调用SingleInstance.HandleRunningInstance()方法显示到前台。
                                MessageBox.Show("程序已经运行！");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        break;
                    default:
                        MessageBox.Show("应用程序参数设置失败。");
                        break;
                }
            }
        }
        private static bool _isNew = false;
        public static bool IsNew
        {
            set
            {
                _isNew = value;
            }
            get
            {
                return _isNew;
            }
        }
        static void RunApplication()
        {

            Application.EnableVisualStyles();

           //  Application.Run(new frItems());
            NewLogin f = new NewLogin();
            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                int a=0;
                DataTable dt = BasicClass.GetDataSet.GetBySql("Select EmployeeID From Users Where ID=1");
                if(dt.Rows.Count==1)
                {
                    a = Convert.ToInt32(dt.Rows[0][0]);
                }
                if (a == 0)
                {
                    if (_isNew)
                    {
                        Application.Run(new frTabMain());// 
                    }
                    else
                    {
                        Application.Run(new frMain());// Tasks.
                    }
                }
                else
                {
                    Application.Run(new OtherTem.frTemMain());// 
                }

                //Application.Run(new  Hownet.Straps.Form1());
            }
        }
    }
}
