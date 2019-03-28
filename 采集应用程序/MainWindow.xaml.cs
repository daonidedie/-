using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 采集应用程序
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        /// <summary>
        /// 当前页面的处理实体
        /// </summary>
        MainWindowModel model = null;
        public MainWindow()
        {


           
            InitializeComponent();
      DataContext =   model    = new MainWindowModel();
        }

        /// <summary>
        /// 从86zw更新书本列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>Admin
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                model.BookIndexUpdate.Update();
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                button2.IsEnabled = false;
                System.Threading.Tasks.Task.Factory.StartNew(delegate
                {
                    model.UpdateTop50.UpdateAll();
                    Dispatcher.BeginInvoke(new Action(delegate{ button2.IsEnabled = true;}));

                });
               
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                button3.IsEnabled = false;
                System.Threading.Tasks.Task.Factory.StartNew(delegate
                {
                    model.UpdateTop50.Update();
                    //生成首页
                    model.UpdateTop50.UpdateDefaultPage();
                    //生成目录
                    model.UpdateTop50.RunTo50();

                    Dispatcher.BeginInvoke(new Action(delegate { button3.IsEnabled = true; }));

                });
                
            }
        }

        /// <summary>
        /// 单本书采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                string bookName = textBox1.Text;
                if (!string.IsNullOrEmpty(bookName))
                {
                    button4.IsEnabled = false;
                    System.Threading.Tasks.Task.Factory.StartNew(delegate
                    {
                        model.UpdateTop50.Update(bookName);
                        Dispatcher.BeginInvoke(new Action(delegate { button4.IsEnabled = true; }));

                    });
                }
            }
        }

        /// <summary>
        /// 加入了更新首页的功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDefaultBtn_Click(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                //更新首页
                model.UpdateTop50.UpdateDefaultPage();
            }
        }


    }
}
