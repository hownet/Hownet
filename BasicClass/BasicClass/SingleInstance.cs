using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;


namespace BasicClass
{
        /// <summary>
        /// ֻ����һ��Ӧ�ó���ʵ��������
        /// </summary>
        public static class SingleInstance
        {
            private const int WS_SHOWNORMAL = 1;
            [DllImport("User32.dll")]
            private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
            [DllImport("User32.dll")]
            private static extern bool SetForegroundWindow(IntPtr hWnd);
            //��־�ļ�����
            private static string runFlagFullname = null;
            //����ͬ����Ԫ
            private static Mutex mutex = null;

            /// <summary>
            /// static Constructor
            /// </summary>
            static SingleInstance()
            {
            }

            #region apiʵ��
            /// <summary>
            /// ��ȡӦ�ó������ʵ��,���û��ƥ����̣�����Null
            /// </summary>
            /// <returns>���ص�ǰProcessʵ��</returns>
            public static Process GetRunningInstance()
            {

                Process currentProcess = Process.GetCurrentProcess();//��ȡ��ǰ����
                //��ȡ��ǰ���г�����ȫ�޶���
                string currentFileName = currentProcess.MainModule.FileName;
                //��ȡ������ΪProcessName��Process���顣
                Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
                //��������ͬ���������������еĽ���
                foreach (Process process in processes)
                {
                    if (process.MainModule.FileName == currentFileName)
                    {
                        if (process.Id != currentProcess.Id)//���ݽ���ID�ų���ǰ����
                            return process;//���������еĽ���ʵ��
                    }
                }
                return null;
            }

            /// <summary>
            /// ��ȡӦ�ó�����������Ӧ�ó���ǰ̨���У�������boolֵ
            /// </summary>
            public static bool HandleRunningInstance(Process instance)
            {
                //ȷ������û�б���С�������
                ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
                //������ʵ����Ϊforeground window
                return SetForegroundWindow(instance.MainWindowHandle);
            }

            /// <summary>
            /// ��ȡ���ھ��������Ӧ�ó���ǰ̨���У�������boolֵ�����ط���
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


            #region Mutexʵ��
            /// <summary>
            /// ����Ӧ�ó������Mutex
            /// </summary>
            /// <returns>���ش��������true��ʾ�����ɹ���false����ʧ�ܡ�</returns>
            public static bool CreateMutex()
            {
                return CreateMutex(Assembly.GetEntryAssembly().FullName);
            }

            /// <summary>
            /// ����Ӧ�ó������Mutex
            /// </summary>
            /// <param name="name">Mutex����</param>
            /// <returns>���ش��������true��ʾ�����ɹ���false����ʧ�ܡ�</returns>
            public static bool CreateMutex(string name)
            {
                bool result = false;
                mutex = new Mutex(true, name, out result);
                return result;
            }

            /// <summary>
            /// �ͷ�Mutex
            /// </summary>
            public static void ReleaseMutex()
            {
                if (mutex != null)
                {
                    mutex.Close();
                }
            }

            #endregion


            #region ���ñ�־ʵ��
            /// <summary>
            /// ��ʼ���������б�־��������óɹ�������true���Ѿ����÷���false������ʧ�ܽ��׳��쳣
            /// </summary>
            /// <returns>�������ý��</returns>
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
            /// �ͷų�ʼ���������б�־������ͷ�ʧ�ܽ��׳��쳣
            /// </summary>
            public static void DisposeRunFlag()
            {
                if (File.Exists(RunFlag))
                {
                    File.Delete(RunFlag);
                }
            }






            /// <summary>
            /// ��ȡ�����ó������б�־���������Windows�ļ������淶
            /// ����ʵ��������ʱ�ļ�Ϊ���ݣ�����޸ĳ�����ע����ǾͲ���Ҫ�����ļ������淶��
            /// </summary>
            public static string RunFlag
            {

                get
                {
                    if (runFlagFullname == null)
                    {
                        string assemblyFullName = Assembly.GetEntryAssembly().FullName;
                        //CommonApplicationData��//"C:\\Documents and Settings\\All Users\\Application Data"
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
