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
using Skybot.Tong.Tyg.Creates;

namespace 生成应用程序
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowFTP : Window
    {
        /// <summary>
        /// 已经创建的章节文件记录名
        /// </summary>
        protected string ProgressFileName = AppDomain.CurrentDomain.BaseDirectory + "Progress/";
        //进度
        List<string> progress = new List<string>();
        /// <summary>
        /// 听雨阁数据库对像
        /// </summary>
        static TygModel.Entities Tygdb = Skybot.Cache.RecordsCacheManager.Instance.Tygdb;

        /// <summary>
        /// 当前已经采集的进度
        /// </summary>
        public int CurrentIndex = 0;
        /// <summary>
        /// 需要全部采集的书名表
        /// </summary>
        List<TygModel.书名表> books = Skybot.Cache.RecordsCacheManager.Instance.Tygdb.书名表.OrderByDescending(p => p.最后更新时间).ToList().ToList();
        /// <summary>
        /// 书数据状态
        /// </summary>
        System.Collections.Generic.Dictionary<string, State> booksState = null;

        /// <summary>
        /// 用于锁的对像
        /// </summary>
        public object lockobj = new object();

        /// <summary>
        /// 状态用于数据邦定
        /// </summary>
        private System.Collections.ObjectModel.ObservableCollection<StateCont> states = new System.Collections.ObjectModel.ObservableCollection<StateCont>();

        /// <summary>
        /// 状态用于数据邦定
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<StateCont> States
        {
            get { return states; }
        }
        public MainWindowFTP()
        {
            InitializeComponent();

            //创建目录
            if (!System.IO.Directory.Exists(ProgressFileName))
            {
                System.IO.Directory.CreateDirectory(ProgressFileName);
            }
            //初始化进度文件
            ProgressFileName = ProgressFileName + "Index.txt";

            if (System.IO.File.Exists(ProgressFileName))
            {
                progress = System.IO.File.ReadAllLines(ProgressFileName).ToList();
                //移除最后4个采集的数据，不然程序会跳过上次采集的数据
                if (progress.Count > 4)
                {
                    progress.Remove(progress.Last());
                    progress.Remove(progress.Last());
                    progress.Remove(progress.Last());
                    progress.Remove(progress.Last());
                    CurrentIndex = progress.Count;
                }
            }
            //初始化数据状态
            booksState = books.Select(p => new State
            {
                str = p.GUID.ToString() + ((char)2) + p.书名
            }).ToDictionary(p => p.str);

            DataContext = States;

        }


        /// <summary>
        /// 添加状态数据
        /// </summary>
        /// <param name="item"></param>
        public void AddStates(StateCont item)
        {
            lock (States)
            {
                if (States.Count > 4)
                {
                    States.RemoveAt(0);
                }
                States.Add(item);
            }

        }

        /// <summary>
        /// 开始进行数据采集
        /// </summary>
        public void Run()
        {
            //四个线程同时执行
            System.Threading.Tasks.Task[] task = new System.Threading.Tasks.Task[]{
             System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
            {
                CallBakc();
            })),
                         System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
            {
                CallBakc();
            })),
                         System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
            {
                CallBakc();
            })),
                         System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
            {
                CallBakc();
            })),

            };

            System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
           {
               try
               {
                   System.Threading.Tasks.Task.WaitAll(task);
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }));



        }

        /// <summary>
        /// 生成最新50
        /// </summary>
        public void RunTo50()
        {

            //每一个分类取最新的一条记录
            var docs = Tygdb.ExecuteStoreQuery<NewDocItem>(@"
select d.ID,d.本记录GUID,d.GUID,d.书名 into #doc  from (
  select top 100 [GUID] from 书名表 where 包含有效章节 > 0 and 书名表.完本='false'  order by 最后更新时间 desc 
 ) as
 doctype
 left join 
 (
    SELECT *  from  文章表 
 )
 d
 on d.[GUID]=doctype.[GUID] 
select * from (select *,row=row_number()over(partition by [GUID] order by ID desc) from #doc)t where row=1 and t.id>0
drop table #doc ");

            //decimal[] ids = docs.Select(p => p.ID).ToArray();
            ////使用了in
            //var topDocs = Tygdb.文章表.Where(p => ids.Contains(p.ID)).OrderByDescending(p => p.ID).ToList().Where(
            //    //确保章节是有效的
            //    p => string.Join("", System.Text.RegularExpressions.Regex.Matches(p.内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
            //                .Cast<System.Text.RegularExpressions.Match>().Select(x => x.Value).ToArray()
            //                ).Length > 200
            //    );


            ////更新其它数据
            //foreach (var item in topDocs)
            //{
            //    CreateBookHTMLFTP createbook = new CreateBookHTMLFTP(item.书名表);
            //    createbook.Run();
            //}


            var topbook = books.Where(p => p.包含有效章节 > 0 && !p.完本).OrderByDescending(p => p.最后更新时间).Take(50);

            //生成明细项
            foreach (var book in topbook)
            {
                //开始生成最新
                using (TygModel.Entities tygdb = new TygModel.Entities())
                {
                    var itembook = tygdb.书名表.Where(b => b.GUID == book.GUID).FirstOrDefault();
                    CreateBookHTMLFTP createbook = new CreateBookHTMLFTP(itembook);
                    createbook.Run();
                }
            }

        }

        /// <summary>
        /// 得到可以采集的书
        /// </summary>
        /// <returns></returns>
        public TygModel.书名表 GetBook()
        {

            for (int x = 0; x < books.Count; x++)
            {

                //如果没有采集则继续采集
                if (!progress.Contains(books[x].GUID.ToString() + ((char)2) + books[x].书名))
                {
                    var p = books[x];
                    //数据功能 如果存在Key则改变状态
                    if (booksState.ContainsKey(p.GUID.ToString() + ((char)2) + p.书名))
                    {
                        //如果当前状态没有采集则这里指示采集了
                        if (!booksState[p.GUID.ToString() + ((char)2) + p.书名].IsCollected)
                        {
                            booksState[p.GUID.ToString() + ((char)2) + p.书名].IsCollected = true;
                            string guid = books[x].GUID.ToString();
                            //添加信息
                            Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    //添加状态数据
                                    AddStates(new StateCont()
                                    {
                                        书名 = p.书名,
                                        进度 = ""
                                    });

                                    stat.Text = CurrentIndex + "/" + books.Count;
                                }));


                            return books[x];
                        }

                    }


                }

            }
            return null;
        }

        public object callBakc = new object();
        public void CallBakc()
        {

            var book = GetBook();
            Run(CallBakc, book);


        }

        /// <summary>
        /// 开始运行数据
        /// </summary>
        public void Run(Action callBack, TygModel.书名表 book)
        {
            //数据无效直接返回
            if (book == null)
            {
                return;
            }


            //记时器
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
            {
                using (TygModel.Entities entity = new TygModel.Entities())
                {
                    var itembook = entity.书名表.Where(b => b.GUID == book.GUID).FirstOrDefault();
                    CreateBookHTMLFTP createbook = new CreateBookHTMLFTP(itembook);
                    createbook.Run();

                }
                //线程写文件时不锁会冲突 另一个文件正在使用的异常
                lock (lockobj)
                {
                    //采集完成一个记录一个
                    System.IO.File.AppendAllLines(ProgressFileName, new string[] { book.GUID.ToString() + ((char)2) + book.书名 });
                }
            }));
            try
            {
                task.Wait();
                task.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            //进度加1
            System.Threading.Interlocked.Increment(ref CurrentIndex);
            //输出当前进度
            System.Console.WriteLine(string.Format("生成完成{0}，用时:{1},进度:{2}/{3}", book.书名, Math.Round(watch.ElapsedMilliseconds / 1000.0 / 60, 2), CurrentIndex, books.Count));
            watch.Stop();

            //运行下一个数据
            callBack();
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            Run();
        }
        /// <summary>
        /// 生成最新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button2_Click(object sender, RoutedEventArgs e)
        {
            button2.IsEnabled = false;
            System.Threading.Tasks.Task.Factory.StartNew(delegate
            {
                RunTo50();
                Dispatcher.BeginInvoke(new Action(delegate { button2.IsEnabled = true; }));
            });


        }

        /// <summary>
        /// 生成站点地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button3_Click(object sender, RoutedEventArgs e)
        {
            button3.IsEnabled = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                SiteMap map = new SiteMap();
                map.Foreach();
                Dispatcher.BeginInvoke(new Action(delegate { button3.IsEnabled = true; }));
            });
        }

        /// <summary>
        /// 生成网站地图 及时更新当前更新的2000条数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void createSitemap_Click(object sender, RoutedEventArgs e)
        {
            createSitemap.IsEnabled = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
              {
                  SiteMap map = new SiteMap();

                  using (TygModel.Entities entity = new TygModel.Entities())
                  {

                      var query = entity.文章表.OrderByDescending(p => p.ID).Take(2000).Where(p => p.创建时间.Day == DateTime.Now.Day);
                      //更新数据
                      map.ForUpdateDocs(query);
                  }
                  Dispatcher.BeginInvoke(new Action(delegate { createSitemap.IsEnabled = true; }));

              });
        }




    }
    /// <summary>
    /// 用于保存的数据状态
    /// </summary>
    public class State
    {
        /// <summary>
        /// 当前是不是已经在开始或者完成了
        /// </summary>
        public bool IsCollected { get; set; }
        /// <summary>
        /// 数据包含的字符串
        /// </summary>
        public string str { get; set; }
    }
    /// <summary>
    /// 状态数据
    /// </summary>
    public class StateCont : System.ComponentModel.INotifyPropertyChanged
    {
        private string _进度 = string.Empty;

        /// <summary>
        /// 进度
        /// </summary>
        public string 进度
        {
            get { return _进度; }
            set
            {
                _进度 = value;
                OnPropertyChanged("进度");
            }
        }
        public string 书名 { get; set; }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="Name"></param>
        protected void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(Name));
            }

        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }


}
