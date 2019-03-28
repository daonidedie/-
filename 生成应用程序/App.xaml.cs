using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace 生成应用程序
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 命令行
        /// </summary>
        public static string command = string.Empty;
        /// <summary>
        /// 出错次数
        /// </summary>
        public static int FTPError = 0;

        //当前窗口
        MainWindowFTP window = new MainWindowFTP();

        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
              command = "";
            ///命令行参数 
            foreach (string argument in e.Args)
            {
                command = argument;
            }

            //如果没有命令
            if (string.IsNullOrEmpty(command))
            {
                window.Show();
            }
            else
            {
                //window.Show();
                window.Hide();
                Guid guid;
                //用于处理数据的实例
                //guid 
                if (!command.Contains("-"))
                {
                    if (command == "Run")
                    {
                        //采集全部
                        window.Run();
                    }
                }
                //更新文件
                if (command == "createSitemap_Click")
                {
                    System.Diagnostics.UDPGroup.SendStrGB2312(command + "|" + DateTime.Now + "更新文件");
                    window.createSitemap_Click(window,null);
                    //System.Diagnostics.UDPGroup.SendStrGB2312(string.Format("状态:采集完成:{0}", command));
                }

            }
        }
    }
}
