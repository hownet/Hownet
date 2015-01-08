using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;


namespace BasicClass
{
        /// <summary>
        /// 只启动一个应用程序实例控制类
        /// </summary>
        public static class SingleInstance
        {
            private const int WS_SHOWNORMAL = 1;
            [DllImport("User32.dll")]
            private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
            [DllImport("User32.dll")]
            private static extern bool SetForegroundWindow(IntPtr hWnd);
            //标志文件名称
            private static string runFlagFullname = null;
            //声明同步基元
            private static Mutex mutex = null;

            /// <summary>
            /// static Constructor
            /// </summary>
            static SingleInstance()
            {
            }

            #region api实现
            /// <summary>
            /// 获取应用程序进程实例,如果没有匹配进程，返回Null
            /// </summary>
            /// <returns>返回当前Process实例</returns>
            public static Process GetRunningInstance()
            {

                Process currentProcess = Process.GetCurrentProcess();//获取当前进程
                //获取当前运行程序完全限定名
                string currentFileName = currentProcess.MainModule.FileName;
                //获取进程名为ProcessName的Process数组。
                Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
                //遍历有相同进程名称正在运行的进程
                foreach (Process process in processes)
                {
                    if (process.MainModule.FileName == currentFileName)
                    {
                        if (process.Id != currentProcess.Id)//根据进程ID排除当前进程
                            return process;//返回已运行的进程实例
                    }
                }
                return null;
            }

            /// <summary>
            /// 获取应用程序句柄，设置应用程序前台运行，并返回bool值
            /// </summary>
            public static bool HandleRunningInstance(Process instance)
            {
                //确保窗口没有被最小化或最大化
                ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
                //设置真实例程为foreground window
                return SetForegroundWindow(instance.MainWindowHandle);
            }

            /// <summary>
            /// 获取窗口句柄，设置应用程序前台运行，并返回bool值，重载方法
            /// </summary>
            /// <returns></returns>
            public static bool HandleRunningInstance()
            {
                Process p = GetRunningInstance();
                if (p != null)
                {
                    HandleRunningInstance(p);
                    return true;
                }
                return false;
            }

            #endregion


            #region Mutex实现
            /// <summary>
            /// 创建应用程序进程Mutex
            /// </summary>
            /// <returns>返回创建结果，true表示创建成功，false创建失败。</returns>
            public static bool CreateMutex()
            {
                return CreateMutex(Assembly.GetEntryAssembly().FullName);
            }

            /// <summary>
            /// 创建应用程序进程Mutex
            /// </summary>
            /// <param name="name">Mutex名称</param>
            /// <returns>返回创建结果，true表示创建成功，false创建失败。</returns>
            public static bool CreateMutex(string name)
            {
                bool result = false;
                mutex = new Mutex(true, name, out result);
                return result;
            }

            /// <summary>
            /// 释放Mutex
            /// </summary>
            public static void ReleaseMutex()
            {
                if (mutex != null)
                {
                    mutex.Close();
                }
            }

            #endregion


            #region 设置标志实现
            /// <summary>
            /// 初始化程序运行标志，如果设置成功，返回true，已经设置返回false，设置失败将抛出异常
            /// </summary>
            /// <returns>返回设置结果</returns>
            public static bool InitRunFlag()
            {
                if (File.Exists(RunFlag))
                {
                    return false;
                }
                using (FileStream fs = new FileStream(RunFlag, FileMode.Create))
                {
                }
                return true;
            }

            /// <summary>
            /// 释放初始化程序运行标志，如果释放失败将抛出异常
            /// </summary>
            public static void DisposeRunFlag()
            {
                if (File.Exists(RunFlag))
                {
                    File.Delete(RunFlag);
                }
            }






            /// <summary>
            /// 获取或设置程序运行标志，必须符合Windows文件命名规范
            /// 这里实现生成临时文件为依据，如果修改成设置注册表，那就不需要符合文件命名规范。
            /// </summary>
            public static string RunFlag
            {

                get
                {
                    if (runFlagFullname == null)
                    {
                        string assemblyFullName = Assembly.GetEntryAssembly().FullName;
                        //CommonApplicationData：//"C:\\Documents and Settings\\All Users\\Application Data"
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                        //"C:\\Program Files\\Common Files"
                        //string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
                        runFlagFullname = Path.Combine(path, assemblyFullName);
                    }
                    return runFlagFullname;
                }
                set
                {
                    runFlagFullname = value;
                }
            }
            #endregion
        }


    }
