using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Skybot.Cache;
using Skybot.Tong.CodeCharSet;

using Skybot.Collections;
using Skybot.Collections.Analyse;
using Skybot.Collections.Search;

public partial class 文章阅读器 : System.Web.UI.Page
{


    private static System.Collections.Concurrent.ConcurrentBag<BookCollectState> _BookCollectStates = new System.Collections.Concurrent.ConcurrentBag<BookCollectState>();
    /// <summary>
    /// 所有的处理文章状态管理
    /// </summary>
    public static System.Collections.Concurrent.ConcurrentBag<BookCollectState> BookCollectStates
    {
        get { return 文章阅读器._BookCollectStates; }
    }



    BaiduSearchCompare baiduSearch = new BaiduSearchCompare();
    BingSearchCompare bingsearch = new BingSearchCompare();

    TygModel.Entities tygdb = new TygModel.Entities();

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
 "86zw.com"
 ,//燃文
 "ranwen.com",
 //可以得到章节总数
 //"qidian.com",
 "17k.com",
 
};


    /// <summary>
    /// 用户操作常用类
    /// </summary>
    Skybot.Tong.TongUse tong = new Skybot.Tong.TongUse();
    


    protected void Page_Load(object sender, EventArgs e)
    {

        //眼快看书  http://www.yankuai.com/files/article/html/10/10672/


        if (IsPostBack)
        {
            //字符串
            var result = from p in BookCollectStates
                         where p.BookName == BookName.Text
                         select p;
            //如果包含这个书
            if (result.Count() > 0)
            {

                stateText.Text = string.Join("<br/>", result.Select(p => p.ToString()).ToArray());

            }
        }
    }


    /// <summary>
    /// 状态刷新定时器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void timer_Tick(object sender, EventArgs e)
    {
        if (!BookName.Enabled)
        {

            var result = from p in BookCollectStates
                         where p.BookName == BookName.Text
                         select p;

            //如果有这本书的记录
            if (result.Count() > 0)
            {
                string str = string.Join("<br/>", result.Select(p => p.ToString()));
                stateText.Text = str;
               
                //设置数据
                DataList1.DataSource = result.ElementAt(0).UrlList.Select(p=>p.Value);
                DataList1.DataBind();
            }

          

        }


    }

    /// <summary>
    /// 运行查看文章内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Unnamed1_Click(object sender, EventArgs e)
    {



        if (BookName.Text == "")
        {
            return;
        }

        //添加一个新的文章状态管理
        BookCollectState state = null;
        var resultxd = from p in BookCollectStates
                       where p.BookName == BookName.Text
                       select p;

        //如果有这本书的记录
        if (resultxd.Count() > 0)
        {
            stateText.Text = "《" + BookName.Text + "》的任务正在进行 ...";
            return;
        }

        BookName.Enabled = false;

        ///初始化状态
        state = new BookCollectState()
        {
            BookName = BookName.Text,

        };
        //添加到记录中
        BookCollectStates.Add(state);

        stateText.Text = "正在获取" + BookName.Text + "的信息...";

        System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(() =>
        {


           
            //得到所在页面的列表
          //  List<ListPageContentUrl> listPages = baiduSearch.DoWork(BookName.Text);
            List<ListPageContentUrl> listPages = bingsearch.DoWork(BookName.Text);
            //调用显示数据 得到分析结果
            BookAnalyse result = GetBookAnalyse(new SearchCompareEventArgs()
                 {
                     Reslut = listPages,
                     ToKen = BookName.Text
                 });




            state.BookAnalyse = result;
            state.Count = result.BookAnalyseItem.SingleListPageAnalyse.ListPageContentUrls.Count;
            state.CurrentNumeric = 0;
            state.Completed += new System.ComponentModel.RunWorkerCompletedEventHandler(state_Completed);

            var urls=(from p in result.BookAnalyseItem.SingleListPageAnalyse.ListPageContentUrls
                             select new HrefEntity()
                             {
                                 OriginUrl = p,
                             });

            Dictionary<string, HrefEntity> hrefs = new Dictionary<string, HrefEntity>();
            foreach (var ur in urls)
            {
                hrefs[ur.OriginUrl.ToString()] = ur;
                 
            }

            //初始化目录URL
            state.UrlList = hrefs;

            //将获取的书写入内容
            GetContents(result, state);

        });
        //这样就不会出现页面阻塞了
        System.Threading.Tasks.Task.Factory.StartNew(() =>
           {
               try
               {
                   //等待添任务执行完成
                   task.Wait();
               }
               catch (AggregateException ex)
               {
                   ex.Handle((exx) =>
                   {
                       System.Diagnostics.Debug.WriteLine(exx.Message + "|||||" + exx.StackTrace);
                       return true;
                   });

                   ex.Flatten().Handle((exx) =>
                   {
                       System.Diagnostics.Debug.WriteLine(exx.Message + "|||||" + exx.StackTrace);
                       return true;
                   });

               }
               BookName.Enabled = true;
           });

    }

    void state_Completed(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        BookName.Enabled = true;
    }

    /// <summary>
    /// 获取内容
    /// </summary>
    /// <param name="bookAnalyse">书分析器</param>
    /// <param name="state">状态对像</param>
    void GetContents(BookAnalyse bookAnalyse, BookCollectState state)
    {
        //处理名称的方法 
        Func<string, string> fun = (str) =>
        {

            return string.Join("", System.Text.RegularExpressions.Regex.Matches(str, @"[\u4e00-\u9fa5\d|a-z]{1,}", System.Text.RegularExpressions.RegexOptions.Multiline)
                               .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                               );

        };

        //取得内容页面列表
        SingleListPageAnalyse singleListPageAnalyse = bookAnalyse.BookAnalyseItem.SingleListPageAnalyse;
        //XML分析页面
        XMLDocuentAnalyse xmlAnalyse = bookAnalyse.BookAnalyseItem.XMLDocuentAnalyse;

        if (singleListPageAnalyse != null && xmlAnalyse != null)
        {

            //得到表达式
            string express = xmlAnalyse.PathExpression;

            #region 开始得到单个的页面内容

            //开始写入文件
            //注意 写入通过正则表达式获取内容的时候可能会 获取空内容的请况，只要发现内容少于100 就可能是无效的内容需要重新获取
            singleListPageAnalyse.ListPageContentUrls.AsParallel().WithDegreeOfParallelism(20).ForAll((o) =>
            {
                // var o = oxurlEntity.Entity.ElementAt(0).PageContentUrl;

                //处理 内容
                string content = "";

                try
                {
                    //处理 a标签
                    content = ClearTags(xmlAnalyse.GetContent(o.Url.GetWeb()).Replace("<a", "\r\n<a"));
                }
                catch (Exception ex)
                {
                    //添加出错的url
                    state.ErrUrls.Add(o);
                }

                try
                {
                    //目录
                    string dir = @"C:\temp\" + BookName.Text.ToString();
                    //写入文件
                    //WirtePageFile(dir, content, o);

                    try
                    {
                        //如果内容无效
                        if (content.Length <= 200 ||
                            //中文小于6
                          string.Join("", System.Text.RegularExpressions.Regex.Matches(content, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                            .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                            ).Length < 6
                            )
                        {


                            #region 使用从其它文章列表中提取数据
                            //算法说明
                            //1.找到当前要查看的章节名，
                            //2.将当前所有列表中的这个章节都取出来合并成为一个新 的类 包含分析用的XMLdom分析器与当前需要采集的url
                            //3.取出内容，
                            //4.对内容进行分析.最合理的那个内容做为结果,合理为,字数与其它结果的字数比较相近.不是最大也不是最小的

                            //其它索引页面分析器
                            var otherListPageAnalyse = bookAnalyse.BookAnalyses.Where(p => p.SingleListPageAnalyse != singleListPageAnalyse);
                            //在这些分析器中找到所有的这个章节的列表 老方法 已经不再使用了因为 名称不对
                            var otherContentPages = from p in otherListPageAnalyse
                                                    select new BookItemPage
                                                    {
                                                        ListPageAnalyse = p.SingleListPageAnalyse,
                                                        url = ContetPageUrl(p.SingleListPageAnalyse.ListPageContentUrls, o),
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
                                                         PageContent = new Func<string>(() => { try { return p.url.Url.GetWeb(); } catch { return ""; } }).Invoke(),
                                                         xmlAnalyse = p.xmlAnalyse
                                                     };

                            var all = ContentPagesEntity.AsParallel().ToList();

                            //制造垃圾，等系统回收 得到文章的内容
                            var allContents = all.Select(p =>
                                 new Func<string>(() =>
                                 {
                                     try
                                     {
                                         return p.xmlAnalyse != null ? p.xmlAnalyse.GetContent(p.PageContent).Replace("<a", "\r\n<a") : p.PageContent.Replace("<a", "\r\n<a");
                                     }
                                     catch
                                     {
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
                                BaiduSearchCompare baiduSearch = new BaiduSearchCompare();
                                baiduSearch.KeyWord = o.Title;
                                //搜索 内容页面 url
                                // e.ToKen = 永生
                                List<Skybot.Collections.Analyse.ListPageContentUrl> contentUrls = baiduSearch.AnalyseListUrls(
                                     string.Format(BaiduSearchCompare.BaduUrl, System.Web.HttpUtility.UrlEncode(o.Title), "").GetWeb()
                                    , false);
                                ////////搜索章节名称时需要注意 看看能不能优化
                                //可能是搜索引擎出错了，如果出现了过60秒再试一下
                                if (contentUrls.Count == 0 && baiduSearch.KeyWord.Contains("章"))
                                {
                                    System.Threading.Thread.Sleep(60 * 1000);
                                    contentUrls = baiduSearch.AnalyseListUrls(
                                    string.Format(BaiduSearchCompare.BaduUrl, System.Web.HttpUtility.UrlEncode(o.Title), "").GetWeb()
                                   , false);
                                }
                                #region 并行计算出当前页面的内容
                                //返回的列表结果
                                List<string> result = new List<string>();


                                //使用多线程调用搜索引擎
                                System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[contentUrls.Count()];

                                #region 开始调用搜索引擎

                                for (int k = 0; k < tasks.Length; k++)
                                {
                                    //初始化搜索线程并开始搜索 
                                    tasks[k] = System.Threading.Tasks.Task.Factory.StartNew<string>((contentPageurl) =>
                                    {
                                        string PageHtml = contentPageurl.ToString().GetWeb();

                                        //替換掉一部分腳本
                                        foreach (System.Text.RegularExpressions.Match item in System.Text.RegularExpressions.Regex.Matches(PageHtml, @"<script[^>\s\w=/]*>.*<\/script>"
                                             ,
                                            System.Text.RegularExpressions.RegexOptions.None | System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
                                        {
                                            PageHtml = PageHtml.Replace(item.Value, "");
                                        }

                                        return xmlAnalyse.GetContentX(PageHtml);

                                    }, contentUrls[k].Url);
                                }


                                try
                                {
                                    //等待搜索全部完成
                                    //2分钟内全部要搜索完成如果不完成则退出
                                    System.Threading.Tasks.Task.WaitAll(tasks, TimeSpan.FromMinutes(2));
                                }
                                #region 处理异常

                                catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
                                #endregion

                                //合并结果
                                tasks.ToList().ForEach((xo) =>
                                {
                                    if (xo.IsCompleted)
                                    {
                                        //添加到结果中
                                        result.Add(ClearTags(tong.ForMatTextReplaceHTMLTags((xo as System.Threading.Tasks.Task<string>).Result.Replace("<br/>", "\r")).Replace("\r", "<br/>")));
                                    }
                                    //表示异步已经处理
                                    if (xo.Exception != null)
                                    {
                                        xo.Exception.Handle((ex) =>
                                        {
                                            System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                                            return true;
                                        });



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
                    }
                    catch (Exception ex)
                    {
                        //添加出错的url
                        state.ErrUrls.Add(o);
                    }
                    //写入文件 如果文件不对则重新写入
                    WirtePageFile(dir, content, o);
                    state.CurrentNumeric = state.CurrentNumeric + 1;
                    //更新目录完成状态列表
                    //这里不改变目录集合不需要用到锁
                    if (state.UrlList.ContainsKey(o.Url.ToString()))
                    {
                        state.UrlList[o.Url.ToString()].ResultUrl = new Uri(dir + "\\" + FilterSpecial(o.Title).Replace("(", "").Replace(")", "").Replace("?", "").Replace("*", "").Replace(".", "").Replace("/", "").Replace("\"", "").Replace("'", "").Replace("\\", "") + ".html");
                    }

                }
                catch (Exception ex)
                {
                    //添加出错的url
                    state.ErrUrls.Add(o);
                }




            });


            #endregion

            //指定完成
            state.Count = singleListPageAnalyse.ListPageContentUrls.Count;

            System.Diagnostics.Debug.WriteLine("/////" + express);

        }
    }
    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    /// <param name="currentPageContentUrl"></param>
    void WirtePageFile(string path, string content, ListPageContentUrl currentPageContentUrl)
    {
        //创建目录
        var dir = path;
        if (!System.IO.Directory.Exists(dir))
        {
            System.IO.Directory.CreateDirectory(dir);
        }

        System.IO.File.WriteAllText(dir + "\\" + FilterSpecial(currentPageContentUrl.Title).Replace("(", "").Replace(")", "").Replace("?", "").Replace("*", "").Replace(".", "").Replace("/", "").Replace("\"", "").Replace("'", "").Replace("\\", "") + ".html", content);
    }


    /// <summary>
    /// 获取当前指定书本的分析器
    /// </summary>
    BookAnalyse GetBookAnalyse(SearchCompareEventArgs e)
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
                catch
                {

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
                System.Diagnostics.Debug.WriteLine(exx.Message + "|||||" + exx.StackTrace);
                return true;
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
                    System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                    return true;
                });
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
    private IEnumerable<BookItem> AnalyseUrlTitles(List<BookAnalyseItem> bookAnalyses)
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
        foreach (var item in bookAnalyses)
        {
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




    /// <summary>
    /// 一些要过滤的表达式
    /// </summary>
    public string[] strings = new string[]{
        @"^第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}卷[\u4e00-\u9fa5\d|a-z]{1,}第",
        @"^第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}集[\u4e00-\u9fa5\d|a-z]{1,}第",
        @"第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}更",
        

    };

    /// <summary>
    /// 过滤一些数据
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string Filter(string str)
    {
        str = str.WordTraditionalToSimple();
        //替换掉书名
        str = str.Replace(BookName.Text, "");
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
    /// 看看集合是不是包含指定的url 条件
    /// 如果集合中包含则返回
    /// </summary>
    /// <param name="urls">搜索的集合</param>
    /// <param name="pointCondition">条件</param>
    /// <returns></returns>
    ListPageContentUrl ContetPageUrl(IEnumerable<ListPageContentUrl> urls, ListPageContentUrl pointCondition)
    {


        return GetHttpContentHepler.ContetPageUrl(urls, pointCondition);
    }

    /// <summary>
    /// 过滤文件名中的特殊字符
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public string FilterSpecial(string fileName)
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



}





/// <summary>
/// 去掉重复项的实现
/// </summary>
public class ListPageContentUrlComparer : IEqualityComparer<ListPageContentUrl>
{




    //处理名称的方法 
    Func<string, string> fun = (str) =>
    {

        return string.Join("", System.Text.RegularExpressions.Regex.Matches(str, @"[\u4e00-\u9fa5\d|a-z]{1,}", System.Text.RegularExpressions.RegexOptions.Multiline)
                           .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()

                           );

    };

    public bool Equals(ListPageContentUrl x, ListPageContentUrl y)
    {
        if (fun(x.Title.Trim()) == fun(y.Title.Trim()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetHashCode(ListPageContentUrl obj)
    {
        if (obj == null) return 0;
        int nameCode = obj.Title == null ? 0 : obj.Title.GetHashCode();
        return obj.Title.GetHashCode() * 32 + nameCode;

    }
}


/// <summary>
/// 单个章节的url查询用实体
/// </summary>
public class BookItemPage
{

    /// <summary>
    /// url实体
    /// </summary>
    public ListPageContentUrl url { get; set; }

    /// <summary>
    ///分析器实体
    /// </summary>
    public SingleListPageAnalyse ListPageAnalyse { get; set; }
    /// <summary>
    /// 页面内容分析器实体
    /// </summary>
    public XMLDocuentAnalyse xmlAnalyse { get; set; }
}


/// <summary>
/// 书章节
/// </summary>
public class BookItem
{

    /// <summary>
    /// 缩写 标题
    /// </summary>
    public string SortTile { get; set; }

    /// <summary>
    /// 当前记录包含的总章节明细页面记数和
    /// </summary>
    public int UrlsCount { get; set; }

    /// <summary>
    /// 数据实体
    /// </summary>
    public IGrouping<string, VlidContentUrlEntity> Entity { get; set; }



}

/// <summary>
/// 有效内容实体
/// </summary>
public class VlidContentUrlEntity
{
    /// <summary>
    /// url实体
    /// </summary>
    public ListPageContentUrl PageContentUrl { get; set; }

    /// <summary>
    ///分析器实体
    /// </summary>
    public BookAnalyseItem AnalyseEntity { get; set; }

    /// <summary>
    /// 过滤后的标题
    /// </summary>
    public string SortTile { get; set; }
}


/// <summary>
/// 分析结果集合类
/// </summary>
public class BookAnalyse
{
    //分析内容列表的集合，的采集方式, 如果一个内容列表采集出现问题后将调用其它内容列表进行采集
    List<BookAnalyseItem> _BookAnalyses = new List<BookAnalyseItem>();
    /// <summary>
    /// 所有有效的分析器对像集合
    /// </summary>
    public List<BookAnalyseItem> BookAnalyses
    {
        get { return _BookAnalyses; }
        set { _BookAnalyses = value; }
    }


    /// <summary>
    /// 当前可用采集的章节列表
    /// </summary>
    public IEnumerable<BookItem> VlidBookItems { get; set; }

    /// <summary>
    /// 最为合理的
    /// 单个站点的图书分析实例，包括 SingleListPageAnalyse  与
    /// XMLDocuentAnalyse 页面 
    /// </summary>
    public BookAnalyseItem BookAnalyseItem { get; set; }

}
/// <summary>
/// 一个书的索引分析器与内容分析器 封装对像
/// </summary>
public class BookAnalyseItem
{



    /// <summary>
    /// 索引分析器
    /// </summary>
    public SingleListPageAnalyse SingleListPageAnalyse { get; set; }
    /// <summary>
    /// 内容分析器
    /// </summary>
    public XMLDocuentAnalyse XMLDocuentAnalyse { get; set; }
}

/// <summary>
/// 单个书的内容获取状态 
/// </summary>
public class BookCollectState
{
    /// <summary>
    /// 书名
    /// </summary>
    private string _BookName = "";
    /// <summary>
    /// 书名
    /// </summary>
    public string BookName
    {
        get { return _BookName; }
        set { _BookName = value; }
    }

    /// <summary>
    /// 书分析器
    /// </summary>
    public BookAnalyse BookAnalyse { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 当前记录数
    /// </summary>
    private int _CurrentNumeric = 0;
    /// <summary>
    /// 当前记录数
    /// </summary>
    public int CurrentNumeric
    {
        get { return _CurrentNumeric; }
        set
        {
            _CurrentNumeric = value;
            //如果采集完成了
            if (value >= Count)
            {
                //开始完成对像
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(1);
                    if (Completed != null)
                    {
                        Completed(this, new System.ComponentModel.RunWorkerCompletedEventArgs(ErrUrls, null, false));
                    }

                });
            }
            //进程变化
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new System.ComponentModel.ProgressChangedEventArgs(value, null));
            }


        }
    }

    //出错的url
    List<Skybot.Collections.Analyse.ListPageContentUrl> errUrls = new List<ListPageContentUrl>();
    /// <summary>
    /// 出错的url
    /// </summary>
    public List<Skybot.Collections.Analyse.ListPageContentUrl> ErrUrls
    {
        get { return errUrls; }
    }

    /// <summary>
    /// 目录URL
    /// </summary>
    Dictionary<string, HrefEntity> _UrlList = new Dictionary<string, HrefEntity>();

    /// <summary>
    /// 目录URL
    /// </summary>
    public Dictionary<string, HrefEntity> UrlList
    {
        get { return _UrlList; }
        set { _UrlList = value; }
    }



    /// <summary>
    ///ProgressChanged 
    /// </summary>
    public event System.ComponentModel.ProgressChangedEventHandler ProgressChanged;

    /// <summary>
    /// 运行时完成
    /// </summary>
    public event System.ComponentModel.RunWorkerCompletedEventHandler Completed;

    public override string ToString()
    {
        string result = string.Format(
            "《{0}》,总记录数:{1},当前进度:{2},<br/>出错的页面:<br/>{3}",
            BookName,
             Count,
              CurrentNumeric,
             string.Join("<br/>", (from x in errUrls select x.ToString()).ToArray())
            );

        return result;
    }

}

/// <summary>
/// 联接实体
/// </summary>
public class HrefEntity
{
    /// <summary>
    /// 原始URL
    /// </summary>
    public ListPageContentUrl OriginUrl { get; set; }

    /// <summary>
    /// 结果URL
    /// </summary>
    public Uri ResultUrl { get; set; }
}