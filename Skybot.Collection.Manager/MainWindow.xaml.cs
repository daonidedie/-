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
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace BaiduMapConvertManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// 每天自动生成定时器
        /// </summary>
        public System.Windows.Threading.DispatcherTimer dpTimer = new System.Windows.Threading.DispatcherTimer();

        private System.Collections.ObjectModel.ObservableCollection<ItemStatus> states = new System.Collections.ObjectModel.ObservableCollection<ItemStatus>();

        public System.Collections.ObjectModel.ObservableCollection<ItemStatus> States
        {
            get { return states; }
        }

        /// <summary>
        /// 数据管理字典
        /// </summary>
        public System.Collections.Generic.Dictionary<string, ItemStatus> StateDic = new Dictionary<string, ItemStatus>();

        /// <summary>
        /// 获取总共需要采集的范围
        /// </summary>
        //public System.Collections.Generic.Dictionary<string, CollectionStates> Strs = System.IO.File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "limits0.1.txt").Select(p => new CollectionStates()
        //{
        //    IsCollectioned = false,
        //    Key = p
        //}).ToDictionary(p => p.Key);

        /// <summary>
        /// 获取总共需要采集的范围
        /// </summary>
        public System.Collections.Generic.Dictionary<string, CollectionStates> Strs = new Entities().书名表.OrderByDescending(p=>p.最后更新时间).Select(p => p.GUID).ToList().Select(p => new CollectionStates()
        {
            IsCollectioned = false,
            Key = p.ToString()
        }).ToDictionary(p => p.Key);

        /// <summary>
        /// 20个进程同时进行采集
        /// </summary>
        public int MaxPorcess = 20;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = States;
            //初始化子进程的数量
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["子进程"], out MaxPorcess);
            //初始化定时器 默认时间为 晚上3点
            dpTimer.Interval = TimeSpan.FromHours(7);
            dpTimer.Tick += dpTimer_Tick;
        }



        public void AsyncCallback(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp = (System.Net.Sockets.UdpClient)ar.AsyncState;
            //如果已经释放的资源
            if (udp.Client == null)
            {
                //直接返回
                return;
            }
            IPEndPoint remoteHost = null;
            if (ar.IsCompleted)
            {
                            //当前客户端的UDP不能为null
                if (udp.Client != null)
                {
                    //接收数据
                    byte[] bytearr = new byte[0];
                    try
                    {
                        //接收数据
                      bytearr = udp.EndReceive(ar, ref remoteHost);
                    }
                    catch (ObjectDisposedException)
                    {
                        return;
                    }
                    String States = System.Text.Encoding.GetEncoding("GB2312").GetString(bytearr);
                    ///如果带|则表示当前是状态 x,y|状态说明
                    ////if (States.IndexOf("|") != -1)
                    //{
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        UpdateStates(States);
                    }));
                    //}
                    ///采集完成{0},{1}
                    if (States.IndexOf("状态:采集完成:") != -1)
                    {
                        System.IO.File.AppendAllLines(ProgressFileName, new string[] { States.Replace("采集完成", string.Empty) });
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            RemoveStates(States.Replace("状态:采集完成:", string.Empty));
                        }));

                    }
                }
            }
            //当前客户端的UDP不能为null
            if (udp.Client != null)
            {
                //重新开始收数据
                udp.BeginReceive(AsyncCallback, udp);
            }
        }

        //初始化文件说明
        string ProgressFileName = AppDomain.CurrentDomain.BaseDirectory + "states.txt";



        /// <summary>
        /// 删除指定的标识
        /// </summary>
        /// <param name="Token"></param>
        public void RemoveStates(string Token)
        {
            lock (states)
            {
                var query = states.Where(p => p.范围 == Token);
                if (query.Count() >= 1)
                {
                    if (states.Count > 0)
                    {
                        var item = query.FirstOrDefault();
                        states.Remove(item);
                        ((Timer)item.UserToKen).Dispose();
                        if (!item.CurrentProcess.HasExited)
                        {
                            item.CurrentProcess.Kill();
                        }
                    }
                }

            }
            //添加新项
            AddStates();
        }

        public void UpdateStates(string Token)
        {
            if (StateDic.ContainsKey(Token.Split('|')[0]))
            {
                StateDic[Token.Split('|')[0]].状态 = Token;
            }
            else
            {
                if (StateDic.Count > 0)
                {
                    StateDic.Last().Value.状态 = Token;
                }
            }
        }

        /// <summary>
        /// 在当前进度上添加一个新的状态对像
        /// </summary>
        /// <param name="appName">
        /// 应用程序名 默认为 采集应用程序.exe
        /// </param>
        /// <param name="parameStr">参数默认null</param>
        public void AddStates(string parameStr = null, string appName = "采集应用程序.exe")
        {
            //当前需要添加的参数
            string parame = "";
            //如果没有参数
            if (string.IsNullOrEmpty(parameStr))
            {
                foreach (var item in Strs)
                {
                    if (!item.Value.IsCollectioned)
                    {
                        //得到索引
                        parame = item.Key;
                        //表示当前正在处理或者已经处理完成了
                        item.Value.IsCollectioned = true;
                        break;
                    }
                }
            }
            else {
                //指定參數
                parame = parameStr;
            }

            #region 进程处理
            Process process = new Process();
            //创建进程对象                 
            ProcessStartInfo startinfo = new ProcessStartInfo();
            //创建进程时使用的一组值，如下面的属性             
            startinfo.FileName = AppDomain.CurrentDomain.BaseDirectory + appName;
            //设定需要执行的命令程序                 
            //以下是隐藏cmd窗口的方法              
            startinfo.Arguments = parame;
            //设定参数，要输入到命令程序的字符            
            //startinfo.UseShellExecute = false;
            //不使用系统外壳程序启动             
            //startinfo.RedirectStandardInput = false;
            //不重定向输入            
            //startinfo.RedirectStandardOutput = true;
            //重定向输出，而不是默认的显示在dos控制台上 
            startinfo.CreateNoWindow = true;
 
            //不创建窗口            
            process.StartInfo = startinfo;
            process.Start();
            System.Diagnostics.Process proc = process;
            //检查进程是不是已经结束
            System.Threading.Timer timer = new System.Threading.Timer((o) =>
            {
                

                //关闭进程后从面板中移除项
                if (proc.HasExited)
                {
                    ((Timer)o).Dispose();
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        RemoveStates(parame);

                    }));
                }
                else { 
                
                }
            });
            timer.Change(2000, 2000);
            proc.StartInfo.CreateNoWindow = true;



            //状态项
            ItemStatus itemstatus = new ItemStatus()
            {
                CurrentProcess = proc,
                范围 = parame,
                状态 = "正在启动...",
                ButtonCommand = new Command()
                {
                    Action = () =>
                    {
                        proc.Kill();
                        RemoveStates(parame);
                    }
                },
                UserToKen = timer
            };
            #endregion
            if (StateDic.ContainsKey(parame))
            {
                StateDic.Remove(parame);

            }
            StateDic.Add(parame, itemstatus);
            states.Add(itemstatus);
        }

     


        System.Net.Sockets.UdpClient udp = null;
        System.Threading.Tasks.Task task = null;
        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (button1.Content.ToString() == "停止运行")
            {

                foreach (var item in states)
                {
                    //释放内存
                    ((Timer)item.UserToKen).Dispose();
                    if (!item.CurrentProcess.HasExited)
                    {
                        item.CurrentProcess.Kill();
                    }
                }
                try
                {
                   
                    //移除组播
                    udp.DropMulticastGroup(System.Diagnostics.UDPGroup.group.ep.Address);
                    udp.Close();
   
                    states.Clear();
                    task.Dispose();
                }
                catch (Exception ex)
                { 
                    
                }
                button1.Content = "开始运行";
            }
            else
            {
                //当前已经采集的索引
                if (System.IO.File.Exists(ProgressFileName))
                {
                    List<string> arr = System.IO.File.ReadLines(ProgressFileName).ToList();
                    foreach (var item in Strs)
                    {
                        //得到当前项的状态如果包含则将当前项设置为 true 表示已经处理过了
                        if (arr.Contains(item.Key))
                        {
                            item.Value.IsCollectioned = true;
                        }
                    }
                }
                //初始化采集进程
                System.Linq.Enumerable.Range(0, MaxPorcess).ToList().ForEach((p) =>
                {
                    System.Threading.Thread.Sleep(40);
                    AddStates();
                });


                //开始监听
                task = System.Threading.Tasks.Task.Factory.StartNew(() =>
                      {
                          udp = new System.Net.Sockets.UdpClient(System.Diagnostics.UDPGroup.group.ep.Port);
                          udp.JoinMulticastGroup(System.Diagnostics.UDPGroup.group.ep.Address);
                          udp.BeginReceive(AsyncCallback, udp);
                      });
                button1.Content = "停止运行";
            }
        }

        /// <summary>
        ///  更新前50
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTop50_Click(object sender, RoutedEventArgs e)
        {
            //命令 Top50
            AddStates("Top50");

        }
        /// <summary>
        /// 創建瓣的HTML 並且創建站點地圖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateHtmlAndSiteMap_Click(object sender, RoutedEventArgs e)
        {
            AddStates("createSitemap_Click", "生成应用程序.exe");
        }

        /// <summary>
        /// 每天自動更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDataEveryday_Click(object sender, RoutedEventArgs e)
        {
            dpTimer.Stop();
            dpTimer.Start();
            EveryDayRun();
        }

        /// <summary>
        /// 每天自動更新
        /// </summary>
        /// 1.关闭所有相关的程序 
        /// 2.开始执行各程序
        /// 3.执行 button1_Click
        /// 4.执行 UpdateTop50_Click
        /// 5.执行 CreateHtmlAndSiteMap_Click
        /// 6.定时器开始工作
        protected void EveryDayRun()
        {

            foreach (var item in states)
            {
                //释放内存
                ((Timer)item.UserToKen).Dispose();
                if (!item.CurrentProcess.HasExited)
                {
                    item.CurrentProcess.Kill();
                }
            }
            try
            {

                //移除组播
                udp.DropMulticastGroup(System.Diagnostics.UDPGroup.group.ep.Address);
                udp.Close();

                states.Clear();
                task.Dispose();
            }
            catch (Exception ex)
            {

            }

            foreach (var item in StateDic.ToArray())
            {
                StateDic.Remove(item.Key);
                states.Remove(item.Value);
            }

            button1.Content = "开始运行";
            App.CheckProcess("采集应用程序");
            App.CheckProcess("生成应用程序");
            button1_Click(this, null);
            UpdateTop50_Click(this, null);
            CreateHtmlAndSiteMap_Click(this, null);
        }

        /// <summary>
        /// 每天执行的定时器 间隔格1小时检查1次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dpTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            //检查数据
            //..if ((DateTime.Now - currentDate).Hours <= 7)
            //{
                EveryDayRun();
            //}
        }


    }

    /// <summary>
    /// 状态实体对像
    /// </summary>
    public class CollectionStates
    {
        /// <summary>
        /// 表示当前正在处理或者已经处理完成了
        /// </summary>
        public bool IsCollectioned { get; set; }
        public string Key { get; set; }
    }
}
