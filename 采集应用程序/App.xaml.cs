using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace 采集应用程序
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //当前窗口
        MainWindow window = new MainWindow();

        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            string command = "";
            ///命令行参数 78.1,22.3
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
                Skybot.Collections.CommandCollection collEntity = new Skybot.Collections.CommandCollection();
                //guid 
                if (Guid.TryParse(command, out guid))
                {
                   
                    collEntity.Run(command);
                }
                //update top 50 books  and CreateHtml
                if (command == "Top50")
                {
                    collEntity.UpdateTop50(command);
                }

            }
        }
    }
}
