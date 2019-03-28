using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 采集应用程序.Capability;


namespace Skybot.Collections
{
    using Skybot.Collections.Analyse;
    using Skybot.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Skybot.Collections.Search;
    using Skybot.Tong.Tyg.Creates;
    /// <summary>
    /// 更新小说百度搜索榜单中的前50
    /// </summary>
    public class UpdateTop50SourceFromBingCollectSourceByXs52 : System.ComponentModel.INotifyPropertyChanged
    {

        #region 实现 INotifyPropertyChanged
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 注册属性发生改变的事件
        /// </summary>
        /// <param name="PropertyName"></param>
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(PropertyName));
            }


        }
        #endregion


        string[] urlIndex黑名单 = new string[]{
        //黑名单有 内容  分多页
 "chinabhl.com",
 //有可能全部是收费的查看不鸟内容
 ///"qidian.com"
};
        string[] urlIndex白名单 = new string[]{
 
 //眼快看书
// "yankuai.com",
  //眼快看书
 "yankuai.org",
 //八路中文网
 "86zw.net"
 ,//燃文
 "ranwen.com",
 //可以得到章节总数
 //"qidian.com",
 "17k.com",

 
    

};

        /// <summary>
        /// 一些数据操作的封装方法 
        /// </summary>
        public Tong.TongUse tong = new Tong.TongUse();

        /// <summary>
        /// 是不是自动采集
        /// </summary>
        private bool _IsAutoCollection = false;

        /// <summary>
        /// 是不是启用自动采集
        /// </summary>
        public bool IsAutoCollection
        {
            get { return _IsAutoCollection; }
            set
            {
                _IsAutoCollection = value;
                if (value)
                {
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                }
                OnPropertyChanged("IsAutoCollection");
            }
        }

        /// <summary>
        /// 定时器
        /// </summary>
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        /// <summary>
        /// 创建一个新的更新搜索实例
        /// </summary>
        public UpdateTop50SourceFromBingCollectSourceByXs52()
        {
            timer.Interval = TimeSpan.FromHours(4);
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //每天晚上执行一次
            if (Math.Abs((DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) - DateTime.Now).TotalHours) <= 4)
            {
                Task.Factory.StartNew(delegate
                {
                    try
                    {
                        GetTopBaiduBookNames();
                        //更新数据
                        Update();
                    }
                    catch (Exception ex)
                    {
                        //打印状态
                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                    }
                });
            }
        }



        /// <summary>
        /// 更新百度小说排名
        /// </summary>
        public List<string> GetTopBaiduBookNames()
        {
            //书名集合
            List<string> BookNames = new List<string>();

            HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml("http://top.baidu.com/buzz.php?p=book".GetWeb());


            //可能的原素
            List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();


            //开始循环子原素
            SingleListPageAnalyse.AnalyseMaxATagNearest(dom.DocumentNode, possiblyResultElements, 0, new PossiblyResultElement()
            {
                ParentPossiblyResult = null,
                CurrnetHtmlElement = dom.DocumentNode,
                LayerIndex = -1,
                ContainTagNum = 0
            });


            //计算当前所有HTML原素中的tr原素
            var PageTrElements = from tr in possiblyResultElements
                                 where tr.CurrnetHtmlElement.Name == "tr"
                                 select tr;

            //填类
            foreach (var item in PageTrElements)
            {
                if (item.CurrnetHtmlElement.HasChildNodes)
                {
                    var els = item.CurrnetHtmlElement.ChildNodes.Where(p => p.HasChildNodes);

                    double x;
                    #region 2013-5-12以前的代码无法获取百度小说的排列
                    //if (els.ElementAt(0).Name == "th" && double.TryParse(els.ElementAt(0).InnerText, out x))
                    //{
                    //    BookNames.Add(els.ElementAt(1).InnerText);
                    //}
                    #endregion


                    if (els.ElementAt(0).Name == "td" && double.TryParse(els.ElementAt(0).InnerText, out x))
                    {
                        if (els.ElementAt(1).HasChildNodes)
                        {
                            //得到小说名
                            BookNames.Add(els.ElementAt(1).Elements("a").ElementAt(0).InnerText);
                        }
                    }
                }
            }
            if (BookNames.Count == 0)
            {
                System.Diagnostics.UDPGroup.SendStrGB2312("无法从百度获取小说排名 ");
            }

            using (TygModel.Entities tygdb = new TygModel.Entities())
            {
                //找到前50的小说并对数据库记录进行更新
                //更新所有小说的书名
                foreach (string bookName in BookNames)
                {
                    var query = tygdb.书名表.Where(p => p.书名.Trim() == bookName).ToList();
                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            item.最后更新时间 = DateTime.Now;
                        }
                    }
                }
                //提交更新
                tygdb.SaveChanges();
            }

            return BookNames;
        }


        public void Update()
        {
            try
            {
                //更新网页数据
                GetTopBaiduBookNames();
            }
            catch (Exception exd)
            {
                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "从百度更新50条网页数据" + exd.Message + (exd.StackTrace == null ? " " : " " + exd.StackTrace));
            }

            try
            {
                ///////////////////////////////////开始进行数采集
                //表示5个任务同时开始
                using (TygModel.Entities tygdb = new TygModel.Entities())
                {
                    tygdb.书名表.ToList()
                        .OrderByDescending(p => p.最后更新时间)
                        .Where(p => !p.完本)
                        //.Where(p => p.书名 == "吞噬星空" || p.书名 == "仙逆")
                        .Take(50).AsParallel()
                        // .WithDegreeOfParallelism(3
                        .ForAll((o) =>
                        {

                            using (TygModel.Entities entities = new TygModel.Entities())
                            {
                                var books = entities.书名表.Where(p => p.GUID == o.GUID);
                                if (books.Count() > 0)
                                {
                                    //保存记录数   不允许启动新事务，因为有其他线程正在该会话中运行。 
                                    try
                                    {
                                        GetContents(books.First(), entities);

                                    }
                                    catch (Exception ex)
                                    {
                                        //打印状态
                                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                    }
                                    finally
                                    {
                                        entities.SaveChanges();


                                    }
                                }
                            }
                        });
                    //打印状态
                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "开始采集 所有书本信息");
                    //更新书有效章节数量
                    TygModel.Entities enti = new TygModel.Entities();
                    var dd = enti.书名表.ToList();
                    dd = dd.Where(p => p.包含有效章节 == null || p.包含有效章节 == 0).ToList();
                    for (int k = 0; k < dd.Count(); k++)
                    {
                        dd.ElementAt(k).包含有效章节 = dd.ElementAt(k).文章表.Count;

                    }
                    enti.SaveChanges();
                    enti.Dispose();
                }
            }
            catch (Exception ex)
            {
                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
            }


        }

        /// <summary>
        /// 指定 GUID 標識採集數據
        /// </summary>
        /// <param name="BookGuid">書的GUID </param>
        /// <param name="byName">来自集合</param>
        public void Update(string BookGuid, string byName)
        {
            try
            {


                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(BookGuid + "|" + DateTime.Now + "开始采集 ");

                #region 更新所有书本的信息

                using (TygModel.Entities entities = new TygModel.Entities())
                {
                    Guid bookguid = Guid.Parse(BookGuid.Trim());
                    var booksx = entities.书名表
                        .Where(p => p.GUID == bookguid)
                    .Take(99999);

                    foreach (var o in booksx)
                    {
                        using (TygModel.Entities db = new TygModel.Entities())
                        {
                            #region 采集数据
                            var books = db.书名表.Where(p => p.GUID == o.GUID);
                            if (books.Count() > 0)
                            {
                                //保存记录数   不允许启动新事务，因为有其他线程正在该会话中运行。 
                                try
                                {
                                    string guid = GetContents(books.First(), db);


                                    System.Diagnostics.UDPGroup.SendStrGB2312(BookGuid + "|" + DateTime.Now + "采集书 " + o.书名 + "完成 ");
                                }
                                catch (AggregateException ex)
                                {

                                    ex.Handle((exx) =>
                                    {
                                        System.Diagnostics.UDPGroup.SendStrGB2312(BookGuid + "|" + DateTime.Now + exx.Message + "|||||" + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                        return true;
                                    });
                                }
                                catch (Exception ex)
                                {
                                    //打印状态
                                    System.Diagnostics.UDPGroup.SendStrGB2312(BookGuid + "|" + DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                }
                                finally
                                {
                                    o.包含有效章节 = o.文章表.Count();
                                    //保存更新
                                    db.SaveChanges();


                                }

                            }
                            #endregion
                        }
                    }
                    //保存更新
                }
                #endregion


            }
            catch (Exception ex)
            {
                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
            }
        }

        /// <summary>
        /// 更新指定的小说
        /// </summary>
        /// <param name="bookName"></param>
        public void Update(string bookName)
        {
            //出错的GUID
            List<string> ErrorBookGuids = new List<string>();
            string filename = AppDomain.CurrentDomain.BaseDirectory + "CurrentBookIndex1.txt";
            try
            {


                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "开始采集 " + bookName);

                #region 更新所有书本的信息

                using (TygModel.Entities entities = new TygModel.Entities())
                {
                    var booksx = entities.书名表.ToList()
                    .OrderByDescending(p => p.最后更新时间)
                        // .Where(p=>p.包含有效章节.Value<=0)
                        //  .Where(p => !p.完本)
                        .Where(p => p.书名.Trim() == bookName.Trim())//|| p.书名 == "仙逆"
                    .Take(99999);

                    foreach (var o in booksx)
                    {
                        using (TygModel.Entities db = new TygModel.Entities())
                        {
                            #region 采集数据
                            var books = db.书名表.Where(p => p.GUID == o.GUID);
                            if (books.Count() > 0)
                            {
                                //保存记录数   不允许启动新事务，因为有其他线程正在该会话中运行。 
                                try
                                {
                                    string guid = GetContents(books.First(), db);
                                    if (!string.IsNullOrEmpty(guid))
                                    {
                                        lock (ErrorBookGuids)
                                        {
                                            ErrorBookGuids.Add(guid);
                                        }
                                    }

                                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "采集书 " + o.书名 + "完成  ");
                                }
                                catch (AggregateException ex)
                                {

                                    ex.Handle((exx) =>
                                    {
                                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                        return true;
                                    });
                                }
                                catch (Exception ex)
                                {
                                    //打印状态
                                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                }
                                finally
                                {

                                    db.SaveChanges();


                                }

                            }
                            #endregion
                        }

                    }
                }
                #endregion

                //书采集完成后写入索引
                System.IO.File.WriteAllText(filename, 0 + "");


                //更新书有效章节数量
                TygModel.Entities enti = new TygModel.Entities();
                var dd = enti.书名表.ToList();
                dd = dd.Where(p => p.包含有效章节 == null || p.包含有效章节 == 0).ToList();
                for (int k = 0; k < dd.Count(); k++)
                {
                    dd.ElementAt(k).包含有效章节 = dd.ElementAt(k).文章表.Count;

                }
                enti.SaveChanges();
                enti.Dispose();

            }
            catch (Exception ex)
            {
                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
            }

            if (ErrorBookGuids.Count > 0)
            {
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "采集书本出现问题" + string.Join("\r\n", ErrorBookGuids.ToArray()));
            }
        }

        /// <summary>
        /// 更新所有章节
        /// </summary>
        public void UpdateAll(List<string> BookGuids = null)
        {
            //出错的GUID
            List<string> ErrorBookGuids = new List<string>();
            string filename = AppDomain.CurrentDomain.BaseDirectory + "CurrentBookIndex.txt";
            try
            {
                //当前采集的书
                int CurrentBookIndex = 1;

                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "开始采集 所有书本信息");

                #region 更新所有书本的信息

                using (TygModel.Entities entities = new TygModel.Entities())
                {
                    var booksx = entities.书名表.ToList()
                    .OrderByDescending(p => p.最后更新时间)
                        // .Where(p=>p.包含有效章节.Value<=0)
                        //  .Where(p => !p.完本)
                        /// .Where(p => p.书名 == "斗破苍穹")//|| p.书名 == "仙逆"
                    .Take(99999);

                    foreach (var o in booksx)
                    {
                        using (TygModel.Entities db = new TygModel.Entities())
                        {
                            #region 采集数据
                            //开始采集书的总状态
                            //打印状态
                            System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "开始采集书 " + o.书名 + "  进度：" + CurrentBookIndex++ + "/" + booksx.Count());

                            //从缓存中读取索引
                            if (System.IO.File.Exists(filename))
                            {
                                int index = 0;
                                int.TryParse(System.IO.File.ReadAllText(filename), out index);
                                if (index > CurrentBookIndex)
                                {
                                    continue;
                                }
                            }
                            //将索引写入文件
                            System.IO.File.WriteAllText(filename, CurrentBookIndex + "");

                            var books = db.书名表.Where(p => p.GUID == o.GUID);
                            if (books.Count() > 0)
                            {
                                //保存记录数   不允许启动新事务，因为有其他线程正在该会话中运行。 
                                try
                                {
                                    string guid = GetContents(books.First(), db);
                                    if (!string.IsNullOrEmpty(guid))
                                    {
                                        lock (ErrorBookGuids)
                                        {
                                            ErrorBookGuids.Add(guid);
                                        }
                                    }

                                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "采集书 " + o.书名 + "完成  ");
                                }
                                catch (AggregateException ex)
                                {

                                    ex.Handle((exx) =>
                                    {
                                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                        return true;
                                    });
                                }
                                catch (Exception ex)
                                {
                                    //打印状态
                                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                }
                                finally
                                {

                                    db.SaveChanges();


                                }

                            }
                            #endregion

                        }
                    }
                }
                #endregion

                //书采集完成后写入索引
                System.IO.File.WriteAllText(filename, 0 + "");


                //更新书有效章节数量
                TygModel.Entities enti = new TygModel.Entities();
                var dd = enti.书名表.ToList();
                dd = dd.Where(p => p.包含有效章节 == null || p.包含有效章节 == 0).ToList();
                for (int k = 0; k < dd.Count(); k++)
                {
                    dd.ElementAt(k).包含有效章节 = dd.ElementAt(k).文章表.Count;

                }
                enti.SaveChanges();
                enti.Dispose();

            }
            catch (Exception ex)
            {
                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
            }

            if (ErrorBookGuids.Count > 0)
            {
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "采集书本出现问题" + string.Join("\r\n", ErrorBookGuids.ToArray()));
            }
        }

        /// <summary>
        /// 得到文章的内容
        /// </summary>
        /// <param name="o">记录</param>
        /// <param name="entities">数据库操作实体</param>
        /// <returns>返回出错的GUID如果没有出错直接返回 ""</returns>
        private string GetContents(TygModel.书名表 o, TygModel.Entities entities)
        {
            string errGUID = "";

            //得到
            Skybot.Collections.Analyse.SingleListPageAnalyse ListPageAnalyse = null;
            Skybot.Collections.Analyse.XMLDocuentAnalyse documentAnalyse = null;





            //打印状态
            System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "开始采集:" + o.书名 + " 目录:" + o.采集用的URL1);


            //调用显示数据 得到分析结果
            BookAnalyse result = null;



            //得到转换后的对应分析器
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() =>
            {
                ListPageAnalyse = new Skybot.Collections.Analyse.SingleListPageAnalyse(o.采集用的URL1);
                documentAnalyse = new Skybot.Collections.Analyse.XMLDocuentAnalyse() { IndexPageUrl = new Skybot.Collections.Analyse.ListPageContentUrl() { index = 0, Title = o.书名, Url = new Uri(o.采集用的URL1) } };

                try
                {
                    //初始化内容分析器
                    documentAnalyse.GetPathExpression(documentAnalyse.IndexPageUrl, ListPageAnalyse.ListPageContentUrls);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
                }

                //处理url
                Filterxs52(ListPageAnalyse);

 

                //调用显示数据 得到分析结果
                result = GetBookAnalyse(new SearchCompareEventArgs()
               {
                   Reslut = new Skybot.Collections.Search.BingSearchCompare().DoWork(o.书名.Trim()),
                   ToKen = o.书名.Trim()
               });

            }));


            try
            {
                //等待添加页面列表分析完成 7 分钟没有完成则表示超时
                System.Threading.Tasks.Task.WaitAll(tasks.ToArray(), TimeSpan.FromMinutes(7));


                //暂时停止一会，释放CPU资源  3 秒
                //System.Threading.Thread.Sleep(1*30);
            }
            catch (AggregateException ex)
            {
                ex.Handle((exx) =>
                {
                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + exx.StackTrace);
                    return true;
                });

                ex.Flatten().Handle((exx) =>
                {
                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + exx.StackTrace);
                    return true;
                });
            }
            finally
            {
                //释放资源
                tasks.ForEach((t) =>
                {//表示异步已经处理
                    if (t.Exception != null)
                    {
                        t.Exception.Handle((ex) =>
                        {
                            System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + "|||||" + ex.StackTrace);
                            return true;
                        });
                    } if (t.Status == System.Threading.Tasks.TaskStatus.RanToCompletion
     || t.Status == System.Threading.Tasks.TaskStatus.Faulted
     || t.Status == System.Threading.Tasks.TaskStatus.Canceled)
                    {//释放资源
                        t.Dispose();
                    }

                });
            }


            //如果数据有效
            if (ListPageAnalyse != null && documentAnalyse != null && documentAnalyse.PathExpression != null)
            {
                //如果文章列表内容小于60 则返回
                if (ListPageAnalyse.ListPageContentUrls.Count <= 60)
                {
                    return "";
                }

                List<TygModel.文章表> docentitys = new List<TygModel.文章表>();

                try
                {
                    //得到已经存在的记录
                    docentitys = o.文章表.ToList();
                }
                catch (Exception ex)
                {
                    //打印状态
                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "o.文章表.ToList() 出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                    return o.GUID.ToString();
                }
                var docs = docentitys.Select(p => p.章节名.Trim());

                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + o.书名 + " 内容共:" + ListPageAnalyse.ListPageContentUrls.Count + "条记录");

                //将章节列表索引转换成为扩展实体数据
                List<UrlExtentEntity> entitys = ListPageAnalyse.ListPageContentUrls.Select(p => new UrlExtentEntity() { index = p.index, Indexs = p.Indexs, Title = p.Title, Url = p.Url }).ToList();

                //开始采集数据
                for (int k = 0; k < entitys.Count; k++)
                {

                    //暂时停止一会，释放CPU资源  3 秒
                    //System.Threading.Thread.Sleep(1 * 30);

                    //上一条记录
                    UrlExtentEntity PreviousItem = null; // entitys[k - 1];
                    //当前记录
                    UrlExtentEntity CurrentItem = null; //  entitys[k];
                    //下一条记录
                    UrlExtentEntity NextItem = null; //  entitys[k + 1];

                    //当前记录
                    CurrentItem = entitys[k];
                    //看看记录是不是已经存在了 如果存在则不进行更新
                    if (!docs.Contains(CurrentItem.Title.Trim()))
                    {
                        #region 传值

                        //上一条记录
                        if (k - 1 >= 0)
                        {
                            PreviousItem = entitys[k - 1];
                            //如果上一章节已经存在了则就用数据库中的记录
                            var Temprecords = docentitys.Where(p => p.章节名.Trim() == PreviousItem.Title.Trim());
                            if (Temprecords.Count() > 0)
                            {
                                PreviousItem.Token = Temprecords.ElementAt(0).本记录GUID.ToString();
                                //设置本记录ID
                                CurrentItem.Token = Temprecords.ElementAt(0).下一章.Value.ToString();
                            }
                        }


                        //下一条记录
                        if (k + 1 < entitys.Count)
                        {

                            NextItem = entitys[k + 1];

                        }
                        #endregion

                        //当前章节的GUID
                        if (CurrentItem.Token.Length < 10)
                        {
                            CurrentItem.Token = Guid.NewGuid().ToString();
                        }

                        //产生下一章节的GUID
                        if (NextItem != null && NextItem.Token.Length < 10)
                        {
                            NextItem.Token = Guid.NewGuid().ToString();

                            //如果上一章节已经存在了则就用数据库中的记录
                            var Temprecords = docentitys.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                            if (Temprecords.Count() > 0)
                            {
                                NextItem.Token = Temprecords.ElementAt(0).下一章.Value.ToString();
                            }
                        }
                        //得到内容
                        string content = "";

                        try
                        {
                            content = documentAnalyse.GetContent(CurrentItem.Url.GetWeb());
                            //如果内容无效 则在其它分析器中获取
                            //if (!ISContentHasJavaScript文章内容是不是有效(content))
                            //{
                            //    //取得文章内容
                            //    content = GetContent(content, result, CurrentItem, documentAnalyse);
                            //}
                        }
                        catch
                        {

                            try
                            {
                                //如果搜索图书名搜索器已经初始化
                                if (result != null)
                                {
                                    //去掉图
                                    if (CurrentItem.Title.Trim().EndsWith("图") || CurrentItem.Title.Trim().EndsWith("t") || CurrentItem.Title.Trim().EndsWith("T"))
                                    {
                                        CurrentItem.Title = CurrentItem.Title.Substring(0, CurrentItem.Title.Length - 2);
                                    }
                                    //取得文章内容
                                    content = GetContent(content, result, CurrentItem, documentAnalyse);
                                }
                            }
                            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }

                        }
                        //添加到数据库
                        entities.文章表.AddObject (new TygModel.文章表()
                        {
                            GUID = o.GUID,
                            书名 = o.书名,
                            分类标识 = o.分类标识,
                            本记录GUID =Guid.NewGuid(),
                            创建时间 = DateTime.Now,
                            分类名称 = o.分类表.分类名称,
                            章节名 = CurrentItem.Title,
                            上一章 = PreviousItem == null ? Guid.Empty : Guid.Parse(PreviousItem.Token),
                            下一章 = NextItem == null ? Guid.Empty: Guid.Parse(NextItem.Token),
                            最后访问时间 = DateTime.Now,
                            内容 = content,
                            采集用的URL1 = CurrentItem.Url.ToString()
                        });
                        //打印状态
                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + o.书名 + " 采集内容:" + CurrentItem.Title + "成功,正文长度:" + content.Length + CurrentItem.Url.ToString());
                    }
                    #region 如果内容存在则更新
#if 更新
                    else
                    {
                        //创建


                        //如果记录的内容比较少则更新内容
                        var Temprecords = docentitys.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                        if (Temprecords.Count() > 0)
                        {

                            //如果内容无效
                            if ( //中文小于6
                              string.Join("", System.Text.RegularExpressions.Regex.Matches(Temprecords.ElementAt(0).内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                                .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                                ).Length < 6
                                )
                            {
                                //更新记录
                                var reccords = o.文章表.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                                if (reccords.Count() > 0)
                                {
                                    var record = reccords.ElementAt(0);
                                    try
                                    {
                                        record.采集用的URL1 = CurrentItem.Url.ToString();
                                        record.内容 = documentAnalyse.GetContent(CurrentItem.Url.GetWeb());
                                    }
                                    catch { }
                                    //打印状态
                                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now+o.书名 + " 更新记录:" + CurrentItem.Title + "成功,正文长度:" + +record.内容.Length + CurrentItem.Url.ToString());
                                }


                            }

                        }
                    }
#endif
                    else
                    {
                        //如果搜索图书名搜索器已经初始化
                        if (result != null)
                        {

                            //如果记录的内容比较少则更新内容
                            var Temprecords = docentitys.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                            if (Temprecords.Count() > 0)
                            {

                                //如果内容无效
                                if ( //中文小于60
                                  string.Join("", System.Text.RegularExpressions.Regex.Matches(Temprecords.ElementAt(0).内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                                    .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                                    ).Length < 60
                                    ||
                                    !Temprecords.ElementAt(0).内容.Contains("img")
                                    )
                                {
                                    //更新记录
                                    var reccords = o.文章表.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                                    if (reccords.Count() > 0)
                                    {
                                        var record = reccords.ElementAt(0);
                                        try
                                        {
                                            record.采集用的URL1 = CurrentItem.Url.ToString();
                                            //去掉图
                                            if (CurrentItem.Title.Trim().EndsWith("图") || CurrentItem.Title.Trim().EndsWith("t") || CurrentItem.Title.Trim().EndsWith("T"))
                                            {
                                                CurrentItem.Title = CurrentItem.Title.Substring(0, CurrentItem.Title.Length - 2);
                                            }
                                            record.内容 = GetContent(record.内容, result, CurrentItem, documentAnalyse);
                                        }
                                        catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }
                                        //打印状态
                                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + o.书名 + " 更新记录:" + CurrentItem.Title + "成功,正文长度:" + +record.内容.Length + CurrentItem.Url.ToString());
                                    }


                                }

                            }


                        }
                    }

                    #endregion





                }



                docentitys.Clear();


            }


            return errGUID;
        }
        #region 分析书本的数据内容



        /// <summary>
        /// 过滤文件名中的特殊字符
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        protected string FilterSpecial(string fileName)
        {

            return GetHttpContentHepler.FilterSpecial(fileName);
        }


        /// <summary>
        /// 清空所有的在文章内容中没有用途的标签,返回过滤后的内容
        /// </summary>
        string ClearTags(string html)
        {
            return GetHttpContentHepler.ClearTags(html);

        }
        /// <summary>
        /// 获取当前指定书本的分析器
        /// </summary>
        protected BookAnalyse GetBookAnalyse(SearchCompareEventArgs e)
        {
            //k.du8.com 文章内容转好
            ListPageContentUrl url = null;

            //索引页面
            List<Skybot.Collections.Analyse.ListPageContentUrl> indexspageurl = new List<ListPageContentUrl>();
            foreach (var indexurl in e.Reslut)
            {
                bool IsBad = false;
                //查看黑名单
                foreach (var badurl in urlIndex黑名单)
                {
                    if (indexurl.ToString().Contains(badurl))
                    {
                        IsBad = true;
                        break;
                    }
                }

                if (!IsBad)
                {
                    indexspageurl.Add(indexurl);
                }

            }

            //分析内容列表的集合，的采集方式, 如果一个内容列表采集出现问题后将调用其它内容列表进行采集
            //对应列表的分析器
            //书本分析器
            List<BookAnalyseItem> BookAnalyses = new List<BookAnalyseItem>();

            //XML分析task
            List<System.Threading.Tasks.Task> xmlAnalyseTasks = new List<System.Threading.Tasks.Task>();

            foreach (var urlx in indexspageurl)
            {
                //添加到集合中
                xmlAnalyseTasks.Add(System.Threading.Tasks.Task.Factory.StartNew((o) =>
                {

                    //肿可能会在获取 列表时出现错误
                    try
                    {
                        //单个索引页面的分析器
                        BookAnalyseItem Analyseitem = new BookAnalyseItem();

                        //运行时计算
                        ListPageContentUrl x = (ListPageContentUrl)o;

                        //取得内容页面列表
                        SingleListPageAnalyse singlelistpageAnalyse = new SingleListPageAnalyse(x.Url.ToString());
                        //最少需要有100章节才进行采集
                        if (singlelistpageAnalyse.ListPageContentUrls.Count > 100)
                        {
                            if (url == null)
                            {
                                url = x;
                            }
                            //初始化文档采集对像
                            XMLDocuentAnalyse xmlanalyse = new XMLDocuentAnalyse() { IndexPageUrl = x };

                            //指内容页面分析器
                            Analyseitem.XMLDocuentAnalyse = xmlanalyse;
                            //指定索引页面分析器对像
                            Analyseitem.SingleListPageAnalyse = singlelistpageAnalyse;

                            //添加到分析器对像集合
                            BookAnalyses.Add(Analyseitem);

                            //初始化表达式对像
                            xmlanalyse.GetPathExpression(x, singlelistpageAnalyse.ListPageContentUrls);



                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                    }

                }, urlx));

            }
            try
            {
                //等待添加页面列表分析完成 5 分钟没有完成则表示超时
                System.Threading.Tasks.Task.WaitAll(xmlAnalyseTasks.ToArray(), TimeSpan.FromMinutes(5));

            }
            catch (AggregateException ex)
            {
                ex.Handle((exx) =>
                {
                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + exx.StackTrace);
                    return true;
                });
            }
            finally
            {
                //释放资源
                xmlAnalyseTasks.ForEach((t) =>
               {//表示异步已经处理
                   if (t.Exception != null)
                   {
                       t.Exception.Handle((ex) =>
                       {
                           System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + "|||||" + ex.StackTrace);
                           return true;
                       });
                   }

               });
            }
            #region 处理结果中的异常
            //合并结果
            xmlAnalyseTasks.ForEach((xo) =>
            {
                //表示异步已经处理
                if (xo.Exception != null)
                {
                    xo.Exception.Handle((ex) =>
                    {
                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                        return true;
                    });
                }
                if (xo.Status == System.Threading.Tasks.TaskStatus.RanToCompletion
    || xo.Status == System.Threading.Tasks.TaskStatus.Faulted
    || xo.Status == System.Threading.Tasks.TaskStatus.Canceled)
                {//释放资源
                    xo.Dispose();
                }

            });
            #endregion

            #region 取得分析列表页面分析实体集合与内容页面分析实体集合结果




            //取得内容页面列表
            BookAnalyseItem DefaultAnalyse = null;
            //当前可用的章节列表
            IEnumerable<BookItem> DocumentItems = AnalyseUrlTitles(BookAnalyses);

            //清除不要的一些记录
            Func<BookItem, int> fcuc = (x) =>
            {
                var xr = x.SortTile.Split(' ');
                int n = 0;
                if (xr.Length < 2)
                {
                    return 0;
                }
                int.TryParse(xr[0], out n);
                if (n <= 0)
                {
                    return 0;
                }

                if (xr[1].Trim() == "")
                {
                    return 0;
                }
                return n;
            };
            //取得章节最小值
            var rs = DocumentItems.ToLookup(p => fcuc(p));


            //输出结果
            //string resultstring = string.Join("\r\n", rs.Select(p => string.Join(" ", (from nx in p select nx.SortTile).ToArray()).ToArray()));

            #region 初始化内容列表 分析器


            //创建 一个计算 当前数据对像的集合找到
            var TempBookAnalyses = BookAnalyses.Select(p => new { count = rs.Count() - p.SingleListPageAnalyse.ListPageContentUrls.Count, item = p, exp = p.XMLDocuentAnalyse.PathExpression })
                //进行排序找到最近的一个分析器做为最合理的分析器
              .OrderBy(p => p.count);



            if (TempBookAnalyses.Count() > 0)
            {
                #region 这些代码已经不再使用了
                //取得内容页面列表 临时变量用于在出现找不到比它大的章节时使用
                //BookAnalyseItem TempAnalyse = null;
                ////得到当前最合理的一个采集查询分析器
                //for (int k = 0; k < TempBookAnalyses.Count(); k++)
                //{
                //    if (TempBookAnalyses.ElementAt(k).item.XMLDocuentAnalyse.PathExpression != null)
                //    {
                //        //如果当前的列表比清理过后的记录还少则先保存起来 如果后面有正数则使用正数了
                //        if (TempBookAnalyses.ElementAt(k).count <= 0)
                //        {
                //            TempAnalyse = TempBookAnalyses.ElementAt(k).item;
                //        }
                //        //如果比当前清理过的记录大
                //        else
                //        {
                //            //取值在可选范围内 不是大太多
                //            if (TempBookAnalyses.ElementAt(k).count < 60)
                //            {
                //                TempAnalyse = DefaultAnalyse = TempBookAnalyses.ElementAt(k).item;
                //                break;
                //            }
                //        }
                //    }
                //}
                ////如果实在找不到 合适的集合则使用较小的集合
                //if (DefaultAnalyse == null)
                //{
                //    DefaultAnalyse = TempAnalyse;
                //}
                #endregion

                //找到白名单中的章节列表
                for (int k = 0; k < TempBookAnalyses.Count(); k++)
                {
                    if (TempBookAnalyses.ElementAt(k).item.XMLDocuentAnalyse.PathExpression != null)
                    {
                        //如果当前的分析器为白名单网站  中的分析器则 提出出来做为默认分析器
                        foreach (var item in urlIndex白名单)
                        {
                            //如果当前分析器是白名单网站 则直接传值
                            if (TempBookAnalyses.ElementAt(k).item.SingleListPageAnalyse.IndexPageUrl.Contains(item))
                            {
                                DefaultAnalyse = TempBookAnalyses.ElementAt(k).item;
                            }
                        }
                    }
                }

                //如果实在找不到 合适的集合则使用较大的集合
                if (DefaultAnalyse == null)
                {
                    DefaultAnalyse = BookAnalyses.OrderByDescending(p => p.SingleListPageAnalyse.ListPageContentUrls.Count).FirstOrDefault();
                }



            }





            #endregion



            #endregion
            //返回分析实体结果
            return new BookAnalyse()
            {
                BookAnalyses = BookAnalyses,
                BookAnalyseItem = DefaultAnalyse,
                VlidBookItems = DocumentItems
            };


        }

        /// <summary>
        /// 遍历所有的章节列表找到出现机率最大的那几个章节
        /// </summary>
        /// <param name="bookAnalyses">所有 索引 页面 分析器</param>
        /// <returns>返回可用采集列表</returns>
        protected IEnumerable<BookItem> AnalyseUrlTitles(List<BookAnalyseItem> bookAnalyses)
        {
            //用于保存的实体数据 
            List<VlidContentUrlEntity> vlidEntities = new List<VlidContentUrlEntity>();


            //处理名称的方法 
            Func<string, string> fun = (str) =>
            {

                return string.Join("", System.Text.RegularExpressions.Regex.Matches(str, @"[\u4e00-\u9fa5\d|a-z]{1,}", System.Text.RegularExpressions.RegexOptions.Multiline)
                                   .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                                   );

            };




            //开始过滤清除重复项
            for (int k = 0; k < bookAnalyses.Count; k++)
            {
                var item = bookAnalyses[k];

                item.SingleListPageAnalyse.ListPageContentUrls = item.SingleListPageAnalyse.ListPageContentUrls.Distinct(new ListPageContentUrlComparer()).ToList();
                //添加到集合中
                vlidEntities.AddRange(from p in item.SingleListPageAnalyse.ListPageContentUrls
                                      select new VlidContentUrlEntity()
                                      {
                                          SortTile = Filter(fun(p.Title)),
                                          PageContentUrl = p,
                                          AnalyseEntity = item,
                                      });
            }




            //转换成为lookUp
            var Lookup = vlidEntities.ToLookup(p => p.SortTile);
            //去掉只重复一次的标记，
            var result = Lookup.Select(p => new BookItem { SortTile = p.Key, UrlsCount = p.Count(), Entity = p }).Where(p => p.UrlsCount > 1);

            return result;
        }
        #endregion


        /// <summary>
        /// 一些要过滤的表达式
        /// </summary>
        private string[] strings = new string[]{
        @"^第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}卷[\u4e00-\u9fa5\d|a-z]{1,}第",
        @"^第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}集[\u4e00-\u9fa5\d|a-z]{1,}第",
        @"第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}更",
        

    };

        /// <summary>
        /// 更新站点首页
        /// </summary>
        public void UpdateDefaultPage()
        {
            //更新首页
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadString("http://susucong.com/file/App.aspx?" + Guid.NewGuid());
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新首页成功...http://www.susucong.com/");
            }
            catch (Exception ex)
            {
                //打印状态
                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新首页出现问题" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));

            }
        }
        /// <summary>
        /// 生成最新50
        /// </summary>
        public void RunTo50()
        {
            using (TygModel.Entities Tygdb = new TygModel.Entities())
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

                decimal[] ids = docs.Select(p => p.ID).ToArray();
                //使用了in
                var topDocs = Tygdb.文章表.Where(p => ids.Contains(p.ID)).OrderByDescending(p => p.ID).ToList().Where(
                    //确保章节是有效的
                    p => string.Join("", System.Text.RegularExpressions.Regex.Matches(p.内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                                .Cast<System.Text.RegularExpressions.Match>().Select(x => x.Value).ToArray()
                                ).Length > 200
                    );


                //更新其它数据
                foreach (var item in topDocs)
                {
                    CreateBookHTML createbook = new CreateBookHTML(item.书名表);
                    createbook.Run();
                }


                var topbook = Tygdb.书名表.Where(p => p.包含有效章节 > 0 && !p.完本).OrderByDescending(p => p.最后更新时间).Take(50);

                //生成明细项
                foreach (var book in topbook)
                {
                    //开始生成最新
                    using (TygModel.Entities tygdb = new TygModel.Entities())
                    {
                        var itembook = tygdb.书名表.Where(b => b.GUID == book.GUID).FirstOrDefault();
                        CreateBookHTML createbook = new CreateBookHTML(itembook);
                        createbook.Run();
                    }
                }
            }

        }


    

    /// <summary>
    /// 得到114zw网的索引
    /// </summary>
    /// <param name="url">内容页面url</param>
    /// <param name="IndexUrl">列表页面url</param>
    /// <returns></returns>
    private string Get114Index(Uri url, string IndexUrl)
    {
        string filename = System.IO.Path.GetFileName(url.LocalPath);

        //得到扩展名
        string exname = System.IO.Path.GetExtension(url.LocalPath);

        return filename.Replace(exname, "");

    }

    /// <summary>
    /// 114 中文网过滤.
    /// 1.去重
    ///     如果开始出现了 后面再出现就删除后面的.
    ///     
    /// 2.url ID 排序
    /// 3.包含索引页面的url 才能正确排序  不包含索引URL的直接删除
    /// </summary>
    public void Filterxs52(Skybot.Collections.Analyse.SingleListPageAnalyse analyse)
    {

        List<ListPageContentUrl> urls = new List<ListPageContentUrl>();
        //包含索引页面的url
        var bookurls = analyse.ListPageContentUrls.Where(p => p.Url.ToString().Contains(analyse.IndexPageUrl.Replace("index.html", "")));
        var urlsu = from p in bookurls
                    select new
                    {
                        Index = Get114Index(p.Url, analyse.IndexPageUrl),
                        page = p,

                    };
        int index = 0;
        var urlsx = (from p in urlsu
                     where int.TryParse(p.Index, out  index)
                     select new
                     {
                         Index = int.Parse(p.Index),
                         Page = p.page
                     }).ToList();

        int[] order = urlsx.Select(p => p.Index).Take(3).ToArray();
        if (order.Length > 2)
        {
            //如果第一个的索引ID大于第二个的索引ID,并且第二个索引小于第三个索引则移除第一个索引
            if (order[0] > order[1] && order[2] > order[1])
            {
                urlsx.RemoveAt(0);
            }
        }
        //处理url 内容页面列表完成
      //  urlsx.Take(3).ToList();

        analyse.ListPageContentUrls = urlsx.OrderBy(p=>p.Index).Select(p => p.Page).ToList();


    }



        /// <summary>
        /// 过滤一些数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string Filter(string str)
        {
            str = str.WordTraditionalToSimple();

            //过滤 一些标题数据
            strings.ToList().ForEach((s) =>
            {
                str = System.Text.RegularExpressions.Regex.Replace(str, s, "第");
            });
            str =
                //取得最后的数据
             int.Parse(System.Text.RegularExpressions.Regex.Match(str, @"第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}章").Value.Replace("第", "").Replace("章", "").ConverToDigit().ToString()) + " "
                +
                System.Text.RegularExpressions.Regex.Replace(str, @"第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}章", "");

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="bookAnalyse"></param>
        /// <param name="o"></param>
        /// <param name="xmlAnalyse"></param>
        /// <returns></returns>
        protected string GetContent(string content, BookAnalyse bookAnalyse, ListPageContentUrl o, XMLDocuentAnalyse xmlAnalyse)
        {
            if (content == null)
            {
                content = "";
            }
            //处理名称的方法 
            Func<string, string> fun = (str) =>
            {

                return string.Join("", System.Text.RegularExpressions.Regex.Matches(str, @"[\u4e00-\u9fa5\d|a-z]{1,}", System.Text.RegularExpressions.RegexOptions.Multiline)
                                   .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                                   );

            };

            //如果内容无效
            if (content.Length <= 200 ||
                //中文小于60
              string.Join("", System.Text.RegularExpressions.Regex.Matches(content, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                ).Length < 60
                )
            {


                #region 使用从其它文章列表中提取数据
                //算法说明
                //1.找到当前要查看的章节名，
                //2.将当前所有列表中的这个章节都取出来合并成为一个新 的类 包含分析用的XMLdom分析器与当前需要采集的url
                //3.取出内容，
                //4.对内容进行分析.最合理的那个内容做为结果,合理为,字数与其它结果的字数比较相近.不是最大也不是最小的

                //其它索引页面分析器
                var otherListPageAnalyse = bookAnalyse.BookAnalyses;
                //在这些分析器中找到所有的这个章节的列表 老方法 已经不再使用了因为 名称不对
                var otherContentPages = from p in otherListPageAnalyse
                                        select new BookItemPage
                                        {
                                            ListPageAnalyse = p.SingleListPageAnalyse,
                                            url = GetHttpContentHepler.ContetPageUrl(p.SingleListPageAnalyse.ListPageContentUrls, o),
                                            xmlAnalyse = p.XMLDocuentAnalyse
                                        };

                // 得到这个章节的其它索引分析器的列表集合
                var bookItemEntity = bookAnalyse.VlidBookItems.Where(p => p.SortTile == Filter(fun(o.Title)));


                //如果存在集合
                if (bookItemEntity.Count() > 0)
                {
                    //重新获取章节列表
                    otherContentPages = from p in bookItemEntity.FirstOrDefault().Entity
                                        select new BookItemPage
                                        {
                                            ListPageAnalyse = p.AnalyseEntity.SingleListPageAnalyse,
                                            url = p.PageContentUrl,
                                            xmlAnalyse = p.AnalyseEntity.XMLDocuentAnalyse
                                        };
                }



                //取出文章内容
                var ContentPagesEntity = from p in otherContentPages
                                         where p.url != null
                                         select new
                                         {
                                             ListPageAnalyse = p.ListPageAnalyse,
                                             url = p.url,
                                             //有可能读取内容的时候出现问题
                                             PageContent = new Func<string>(() =>
                                             {
                                                 try { return p.url.Url.GetWeb(); }
                                                 catch (Exception exx)
                                                 {
                                                     System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + exx.StackTrace);
                                                     return "";
                                                 }
                                             }).Invoke(),
                                             xmlAnalyse = p.xmlAnalyse
                                         };

                var all = ContentPagesEntity.AsParallel();

                //制造垃圾，等系统回收 得到文章的内容
                var allContents = all.Select(p =>
                     new Func<string>(() =>
                     {
                         try
                         {
                             return p.xmlAnalyse != null ? p.xmlAnalyse.GetContent(p.PageContent).Replace("<a", "\r\n<a") : p.PageContent.Replace("<a", "\r\n<a");
                         }
                         catch (Exception ex)
                         {
                             System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                             return "";
                         }
                     }).Invoke()

                    ).ToList();

                //得到筛选的内容, 制造垃圾，等系统回收
                var resultContents = allContents.Select(p => ClearTags(p)).OrderBy(p => p.Length);

                if (resultContents.Count() > 0)
                {

                    content = resultContents.Last();

                    //替換掉一部分  a 标签
                    foreach (System.Text.RegularExpressions.Match item in System.Text.RegularExpressions.Regex.Matches(content, @"<a[^>\s\w=/]*>.*<\/a>"
                         ,
                        System.Text.RegularExpressions.RegexOptions.None | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
                    {
                        content = content.Replace(item.Value, "");
                    }
                }

                #endregion
                //如果内容无效的话还是用这个
                if (content.Length < 100)
                {
                    //启用百度搜索
                    BingSearchCompare baiduSearch = new BingSearchCompare();
                    baiduSearch.KeyWord = o.Title;
                    //搜索 内容页面 url
                    // e.ToKen = 永生
                    List<Skybot.Collections.Analyse.ListPageContentUrl> contentUrls = baiduSearch.AnalyseListUrls(
                         string.Format(BingSearchCompare.BaduUrl, System.Web.HttpUtility.UrlEncode(o.Title), "").GetWeb()
                        , false);
                    ////////搜索章节名称时需要注意 看看能不能优化
                    //可能是搜索引擎出错了，如果出现了过60秒再试一下
                    if (contentUrls.Count == 0 && baiduSearch.KeyWord.Contains(o.Title))
                    {
                        System.Threading.Thread.Sleep(60 * 1000);
                        contentUrls = baiduSearch.AnalyseListUrls(
                        string.Format(BingSearchCompare.BaduUrl, System.Web.HttpUtility.UrlEncode(o.Title), "").GetWeb()
                       , false);
                    }
                    #region 并行计算出当前页面的内容
                    //返回的列表结果
                    List<string> result = new List<string>();


                    //使用多线程调用搜索引擎
                    System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[contentUrls.Count()];

                    #region 开始调用搜索引擎

                    System.Threading.CancellationTokenSource toKenSource = new System.Threading.CancellationTokenSource();

                    for (int k = 0; k < tasks.Length; k++)
                    {
                        //初始化搜索线程并开始搜索 
                        tasks[k] = System.Threading.Tasks.Task.Factory.StartNew<string>((contentPageurl) =>
                        {



                            string PageHtml = contentPageurl.ToString().GetWeb();
                            if (toKenSource.Token.CanBeCanceled)
                            {
                                return "没有数据";
                            }
                            toKenSource.Token.ThrowIfCancellationRequested();

                            //替換掉一部分腳本
                            foreach (System.Text.RegularExpressions.Match item in System.Text.RegularExpressions.Regex.Matches(PageHtml, @"<script[^>\s\w=/]*>.*<\/script>"
                                 ,
                                System.Text.RegularExpressions.RegexOptions.None | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
                            {
                                PageHtml = PageHtml.Replace(item.Value, "");
                            }

                            return xmlAnalyse.GetContentX(PageHtml);

                        }, contentUrls[k].Url, toKenSource.Token);
                    }


                    try
                    {
                        //等待搜索全部完成
                        //2分钟内全部要搜索完成如果不完成则退出
                        System.Threading.Tasks.Task.WaitAll(tasks, TimeSpan.FromSeconds(10));
                        //放弃
                        toKenSource.Cancel();

                    }
                    #region 处理异常

                    catch (AggregateException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                        ex.Handle((exp) => { return true; });
                    }
                    #endregion

                    //合并结果
                    tasks.ToList().ForEach((xo) =>
                    {
                        if (xo.IsCompleted)
                        {
                            //添加到结果中
                            result.Add(ClearTags(
                                // tong.ForMatTextReplaceHTMLTags(
                                (xo as System.Threading.Tasks.Task<string>).Result.Replace("<br/>", "\r")
                                //)
                                .Replace("\r", "<br/>"))
                            )
                               ;
                        }
                        //表示异步已经处理
                        if (xo.Exception != null)
                        {
                            xo.Exception.Handle((ex) =>
                            {
                                System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                                return true;
                            });



                        }
                        if (xo.Status == System.Threading.Tasks.TaskStatus.RanToCompletion
    || xo.Status == System.Threading.Tasks.TaskStatus.Faulted
    || xo.Status == System.Threading.Tasks.TaskStatus.Canceled)
                        {//释放资源
                            xo.Dispose();
                        }
                    });



                    #endregion


                    //对搜索到的进行分析
                    //找出合适的内容数据
                    var reslutOrder = result.OrderBy(p => p.Length).Where(p => p.Length > 500).ToList();

                    var reqs = reslutOrder.ToList();
                    reqs.Clear();
                    //去掉nbsp 之类的空格 还有HTML标签等 如果开头是中文则表示有效的数据
                    reslutOrder.ForEach((p) =>
                    {

                        //替換掉一部分  a 标签
                        foreach (System.Text.RegularExpressions.Match item in System.Text.RegularExpressions.Regex.Matches(p, @"<a[^>\s\w=/]*>.*<\/a>"
                             ,
                            System.Text.RegularExpressions.RegexOptions.None | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
                        {
                            p = p.Replace(item.Value, "");
                        }
                        //看看第几个字母是中文，数字越小越好

                        int Charindex = 99999;
                        for (int n = 0; n < p.Length; n++)
                        {

                            var mc = System.Text.RegularExpressions.Regex.IsMatch(
                                "" + p[n],
                                @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]"
                                );
                            if (mc)
                            {
                                Charindex = n;
                                break;
                            }
                        }

                        reqs.Add("<字符" + Charindex + "开始位置/>" + p.Replace(" ", "")
                            .Replace(" ", "")
                            .Replace("<br/>", "br/")
                            .Replace("<br>", "br/")
                            .Replace("\t", "")
                            .Replace(" ", "")
                            .Replace(" ", "")
                            );
                    });


                    reslutOrder = reqs.OrderBy(p => int.Parse(System.Text.RegularExpressions.Regex.Match(p, @"^<字符\d{1,}开始位置/>").Value.Replace("<字符", "").Replace("开始位置/>", ""))).ToList();


                    //找到 字数最少的 的那个结果
                    //正文一般都会超过500个字符的
                    foreach (var str in reslutOrder)
                    {
                        if (!str.Contains("if"))
                        {
                            content = System.Text.RegularExpressions.Regex.Replace(str, @"^<字符\d{1,}开始位置/>", "").Replace("br/", "<br>");
                            //如果换行少了
                            if (System.Text.RegularExpressions.Regex.Matches(content, @"<br/>").Count <= 5)
                            {
                                content = content.Replace("”", "”<br/>");
                            }

                            break;
                        }
                    }
                    #endregion
                }

            }

            return content;
        }

        List<string> javascriptTags = new List<string>()
        {
"function",
"alert",
"if(",
"else{",
"return",
//"=\"",
//"='",
//"=''",
//"=\"\"",
"var" 
        };

        /// <summary>
        /// 看看传入的内容是不是有效
        /// 要求内容超过500个中文字
        /// </summary>
        /// <returns></returns>
        protected bool ISContentHasJavaScript文章内容是不是有效(string content)
        {
            try
            {
                bool HasJavascript = false;
                foreach (string item in javascriptTags)
                {
                    if (content.Contains(item))
                    {
                        HasJavascript = true;

                    }
                }

                //如果包含javascript就看看是不是有500个以上的中文字
                if (HasJavascript)
                {

                    return string.Join("", System.Text.RegularExpressions.Regex.Matches(content, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                                         .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                                       ).Length > 500;
                }
                else
                {
                    //不包含 javascript直接返回
                    return true;
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }
            finally
            {
                content = "";
            }
            return false;
        }


    }





}
