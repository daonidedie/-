using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace BaiduMapConvertManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
            Exit += new ExitEventHandler(App_Exit);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            CheckProcess("采集应用程序");
            CheckProcess("生成应用程序");
        }
        #region 检查是不是存在主进程,如果存在关闭
        /// <summary>
        /// 检查 进程
        /// </summary>
        /// <param name="ProcessName">进程字符串</param>
      public static  void CheckProcess(string ProcessName)
        {
            //得到进程
            var proceses = System.Diagnostics.Process.GetProcessesByName(ProcessName);
            if (proceses.Length > 0)
            {
                //关闭程序
                proceses.ToList().ForEach(new Action<System.Diagnostics.Process>((pr) =>
                {
                    if (pr.HasExited)
                    {
                        return;
                    }
                    //关闭
                    pr.Kill();
                }));

                return;
            }

        }
        #endregion




        void App_Startup(object sender, StartupEventArgs e)
        {
            CheckProcess("采集应用程序");
            CheckProcess("生成应用程序");
        }
    }
}
