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
public partial class 测试 : System.Web.UI.Page
{

    TygModel.Entities tygdb = new TygModel.Entities();

    string[] urlIndex黑名单 = new string[]{
        //黑名单有 内容  分多页
 "chinabhl.com",
 //有可能全部是收费的查看不鸟内容
 "qidian.com"
};


    /// <summary>
    /// 用户操作常用类
    /// </summary>
    Skybot.Tong.TongUse tong = new Skybot.Tong.TongUse();
    bool isCompleted = false;


    string TypeStr = @"";
    protected void Page_Load(object sender, EventArgs e)
    {
        TypeStr.Split('\r').ToList().ForEach((o) =>
        {

            o.Trim().Replace("\n", "").Split('\t').ToList().ForEach((p) =>
            {
                Response.Write(p + "<br/>");
            });
            Response.Write("<hr/>");
        });


        Response.Write("1".ToPingYing());




        //搜索工作线程
        BaiduSearchCompare searchCompare = null;



        searchCompare = new BaiduSearchCompare();

        //searchCompare.DoWorkAsync("仙逆", "仙逆");
        //searchCompare.DoWorkAsync("天珠变", "天珠变");
        //searchCompare.DoWorkAsync("遮天", "遮天");
        //searchCompare.DoWorkAsync("吞噬星空", "吞噬星空");
        //searchCompare.DoWorkAnsycComplete += new EventHandler<SearchCompareEventArgs>(searchCompare_DoWorkAnsycComplete);

        System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(() =>
        {

            string key = "仙逆";
            var result = searchCompare.DoWork(key);
            searchCompare_DoWorkAnsycComplete(searchCompare, new SearchCompareEventArgs()
            {
                Reslut = result,
                ToKen = key
            });


        }
             );






        task.ContinueWith((xo) =>
         {
             #region 处理异常
             try { xo.Wait(); }
             catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
             #endregion

             string key = "凡人修仙传";
             var result = searchCompare.DoWork(key);
             searchCompare_DoWorkAnsycComplete(searchCompare, new SearchCompareEventArgs()
             {
                 Reslut = result,
                 ToKen = key
             });

             //表示异步已经处理
             if (xo.Exception != null)
             {
                 xo.Exception.Handle((ex) =>
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                     return true;
                 });
             }

         })

         .ContinueWith((xo) =>
         {
             #region 处理异常
             try { xo.Wait(); }
             catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
             #endregion
             string key = "仙逆";
             var result = searchCompare.DoWork(key);
             searchCompare_DoWorkAnsycComplete(searchCompare, new SearchCompareEventArgs()
             {
                 Reslut = result,
                 ToKen = key
             });

             //表示异步已经处理
             if (xo.Exception != null)
             {
                 xo.Exception.Handle((ex) =>
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                     return true;
                 });
             }

         })

         .ContinueWith((xo) =>
         {
             #region 处理异常
             try { xo.Wait(); }
             catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
             #endregion
             string key = "天珠变";
             var result = searchCompare.DoWork(key);
             searchCompare_DoWorkAnsycComplete(searchCompare, new SearchCompareEventArgs()
             {
                 Reslut = result,
                 ToKen = key
             });

             //表示异步已经处理
             if (xo.Exception != null)
             {
                 xo.Exception.Handle((ex) =>
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                     return true;
                 });
             }

         })

         .ContinueWith((xo) =>
         {
             #region 处理异常
             try { xo.Wait(); }
             catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
             #endregion
             string key = "遮天";
             var result = searchCompare.DoWork(key);
             searchCompare_DoWorkAnsycComplete(searchCompare, new SearchCompareEventArgs()
             {
                 Reslut = result,
                 ToKen = key
             });

             //表示异步已经处理
             if (xo.Exception != null)
             {
                 xo.Exception.Handle((ex) =>
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                     return true;
                 });
             }

         })


         .ContinueWith((xo) =>
         {
             #region 处理异常
             try { xo.Wait(); }
             catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
             #endregion
             //表示异步已经处理
             if (xo.Exception != null)
             {
                 xo.Exception.Handle((ex) =>
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                     return true;
                 });
             }

             System.Diagnostics.Debug.Write("任务完成" + tong.EStr(50, "/"));

         });




        try
        {
            //输出章节结果
            Response.Write(
                    string.Join("<br/>\r\n", (from p in searchCompare.DoWork("仙逆")
                                              // orderby p.index
                                              select p.ToString()

                                                 ).ToArray()
                ));

        }
        catch
        {

        }

        //列表页面分析
        SingleListPageAnalyse analyse = null;
        analyse = new SingleListPageAnalyse("http://www.78xs.com/article/7/5319/Default.shtml");//.GetResult<List<string>>();
        //analyse = new SingleListPageAnalyse("http://www.yankuai.com/files/article/html/1/1845/");
        // analyse = new SingleListPageAnalyse("http://www.qidian.com/BookReader/1264634.aspx");
        while (true)
        {
            System.Threading.Thread.Sleep(2);
            if (analyse.ListPageContentUrls.Count > 200)
            {
                //输出章节结果
                Response.Write(
                        string.Join("<br/>\r\n", (from p in analyse.ListPageContentUrls
                                                  // orderby p.index
                                                  select "" + p.index + "\t " + p.Title + "\t\t " + p.Url

                                                     ).ToArray()
                    ));

                break;
            }

        }
        //analyse = new SingleListPageAnalyse("http://www.baidu.com/s?bs=xml+linq+GetElementsByTagName&f=8&rsv_bp=1&wd=xml+linq++TagName&inputT=718");


        //输出Web数据
        // Response.Write("http://www.qlili.com".GetWeb());


        Skybot.Cache.RecordsCacheManager.Instance.UpdateRecord(RecordType.Page, "" + 6);


        // Response.Write();
        Response.Write(tygdb.分类表.Count());
        // 序列化测试();

        #region 处理异常
        try { task.Wait(); }
        catch (AggregateException ex) { ex.Handle((exp) => { return true; }); }
        #endregion


        return;
        Skybot.Cache.TabPageEntity page = new TabPageEntity()
        {
            Record = tygdb.文章表.First()
        };


    }

    /// <summary>
    /// 搜索完成时输出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void searchCompare_DoWorkAnsycComplete(object sender, SearchCompareEventArgs e)
    {


        try
        {

            //输出章节结果
            Response.Write(
                    string.Join("<br/>\r\n", (from p in e.Reslut
                                              // orderby p.index
                                              select p.ToString()

                                                 ).ToArray()
                ));
            Response.Write("<br/>永生<br/>");
            //加载完成
            isCompleted = true;
        }

            //响应在这里不能用
        catch { }
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
        List<XMLDocuentAnalyse> xmlDocuentAnalyses = new List<XMLDocuentAnalyse>();
        //对应列表的分析器
        List<SingleListPageAnalyse> ingleListPageAnalyses = new List<SingleListPageAnalyse>();
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

                        //添加到集合中
                        xmlDocuentAnalyses.Add(xmlanalyse);
                        //初始化表达式对像
                        xmlanalyse.GetPathExpression(x, singlelistpageAnalyse.ListPageContentUrls);
                        ingleListPageAnalyses.Add(singlelistpageAnalyse);

                    }
                }
                catch
                {

                }

            }, urlx));

        }
        //等待添加页面列表分析完成 3 分钟没有完成则表示超时
        System.Threading.Tasks.Task.WaitAll(xmlAnalyseTasks.ToArray(), TimeSpan.FromMinutes(5));
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

        //取得内容页面列表
        SingleListPageAnalyse singleListPageAnalyse = null;
        //XML分析页面
        XMLDocuentAnalyse xmlAnalyse = null;
        #region 初始化内容列表 分析器
        //得到当前最合理的一个采集查询分析器
        singleListPageAnalyse = new Func<SingleListPageAnalyse>(() =>
        {

            //用于返回的结果
            SingleListPageAnalyse resultAnalyse = null;

            //div
            var div = xmlDocuentAnalyses.Where(p => p.PathExpression.Length > 0).Where(p => p.PathExpression.Split('/').Last().Contains("div"));


            if (div.Count() > 0)
            {
                //XML分析页面
                xmlAnalyse = div.First();
                //得到查询idv的表达式的结果
                var divresults = ingleListPageAnalyses.Where(p => p.IndexPageUrl.Trim() == div.First().IndexPageUrl.Url.ToString().Trim());
                if (divresults.Count() > 0)
                {
                    resultAnalyse = divresults.First();
                }
            }
            else
            {

                //td
                var td = xmlDocuentAnalyses.Where(p => p.PathExpression.Length > 0).Where(p => p.PathExpression.Split('/').Last().Contains("td"));


                if (td.Count() > 0)
                {
                    //XML分析页面
                    xmlAnalyse = td.First();
                    //得到查询idv的表达式的结果
                    var tdresults = ingleListPageAnalyses.Where(p => p.IndexPageUrl.Trim() == div.First().IndexPageUrl.Url.ToString().Trim());
                    if (tdresults.Count() > 0)
                    {
                        resultAnalyse = tdresults.First();
                    }
                }
                else
                {

                    //li
                    var li = xmlDocuentAnalyses.Where(p => p.PathExpression.Length > 0).Where(p => p.PathExpression.Split('/').Last().Contains("li"));

                    if (li.Count() > 0)
                    {
                        //XML分析页面
                        xmlAnalyse = li.First();

                        //得到查询idv的表达式的结果
                        var liresults = ingleListPageAnalyses.Where(p => p.IndexPageUrl.Trim() == div.First().IndexPageUrl.Url.ToString().Trim());
                        if (liresults.Count() > 0)
                        {
                            resultAnalyse = liresults.First();
                        }
                    }
                }

            }




            return resultAnalyse;

        }).Invoke();
        #endregion



        if (singleListPageAnalyse != null && xmlAnalyse != null)
        {
            //XML分析页面
            //XMLDocuentAnalyse xmlAnalyse = xmlDocuentAnalyses.Where(p => p.IndexPageUrl.ToString().Trim() == singleListPageAnalyse.IndexPageUrl.Trim()).First();
            //得到表达式
            string express = xmlAnalyse.GetPathExpression(url, singleListPageAnalyse.ListPageContentUrls);

            #region 开始写入文件
            //出错的url
            List<Skybot.Collections.Analyse.ListPageContentUrl> errurls = new List<ListPageContentUrl>();
            //开始写入文件
            //注意 写入通过正则表达式获取内容的时候可能会 获取空内容的请况，只要发现内容少于100 就可能是无效的内容需要重新获取
            singleListPageAnalyse.ListPageContentUrls.AsParallel().WithDegreeOfParallelism(20).ForAll((o) =>
            {
                try
                {
                    //处理 a标签
                    string content = ClearTags(xmlAnalyse.GetContent(o.Url.GetWeb()).Replace("<a", "\r\n<a"));

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
                        var otherListPageAnalyse = ingleListPageAnalyses.Where(p => p != singleListPageAnalyse);
                        //在这些分析器中找到所有的这个章节的列表
                        var otherContentPages = from p in otherListPageAnalyse
                                                select new
                                                {
                                                    ListPageAnalyse = p,
                                                    url = ContetPageUrl(p.ListPageContentUrls, o),
                                                    xmlAnalyse = xmlDocuentAnalyses.Where(xp => xp.IndexPageUrl.Url.ToString().Trim() == p.IndexPageUrl.Trim())
                                                };

                        //取出文章内容
                        var ContentPagesEntity = from p in otherContentPages
                                                 where p.url != null
                                                 select new { ListPageAnalyse = p.ListPageAnalyse, url = p.url, PageContent = p.url.Url.GetWeb(), xmlAnalyse = p.xmlAnalyse.Count() > 0 ? p.xmlAnalyse.First() : null };

                        var all = ContentPagesEntity.AsParallel().ToList();

                        //制造垃圾，等系统回收
                        var allContents = all.Select(p => p.xmlAnalyse != null ? p.xmlAnalyse.GetContent(p.PageContent).Replace("<a", "\r\n<a") : p.PageContent.Replace("<a", "\r\n<a")).ToList();

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

                            //可能是搜索引擎出错了，如果出现了过60秒再试一下
                            if (contentUrls.Count == 0 && baiduSearch.KeyWord.Contains("章"))
                            {
                                System.Threading.Thread.Sleep(60 * 2000);
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
                        }

                            #endregion
                    }
                    //创建目录
                    var dir = @"C:\temp\" + e.ToKen.ToString();
                    if (!System.IO.Directory.Exists(dir))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                    }

                    System.IO.File.WriteAllText(dir + "\\" + FilterSpecial(o.Title).Replace("(", "").Replace(")", "").Replace("?", "").Replace("*", "").Replace(".", "").Replace("/", "").Replace("\"", "").Replace("'", "").Replace("\\", "") + ".html", content);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    errurls.Add(o);
                }

            });


            #endregion


            System.Diagnostics.Debug.WriteLine("/////" + express);

        }
    }

    void 序列化测试()
    {
        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TygModel.文章表));

        System.IO.StringWriter sw = new System.IO.StringWriter();
        xmlSerializer.Serialize(sw, tygdb.文章表.First());

        Response.Write(sw.ToString());

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