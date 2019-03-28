using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Collections.Search
{

    using Skybot.Collections;
    using System.Linq;
    using System.Xml.Linq;
    using Skybot.Collections.Analyse;
    using HtmlAgilityPack;
    /// <summary>
    /// bing搜索比较实例
    /// </summary>
    public class BingSearchCompare : ISearchCompare
    {


        /// <summary>
        /// 表示最多搜索10页 
        /// </summary>
        public const int MaxPages = 5;

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public const int PageSize = 10;

        /// <summary>
        /// 基页面
        /// </summary>
        public const string BaduUrl = "http://cn.bing.com/search?q={0}&&first={1}";


        #region SearchCompare 成员

        /// <summary>
        /// 搜索关键字,并通过内部算法取到了有效列表集合事件
        /// </summary>
        public event EventHandler<SearchCompareEventArgs> DoWorkAnsycComplete;
        /// <summary>
        /// 当前搜索的关键字
        /// </summary>
        public string KeyWord
        {
            get;
            set;
        }

        /// <summary>
        /// 当前关键字搜索的搜索引擎页面列表
        /// </summary>
        public List<string> SearchUrls
        {
            get;
            set;
        }

        /// <summary>
        /// 索引页面列表
        /// </summary>
        public List<ListPageContentUrl> IndexPageUrls
        {
            get;
            set;
        }

        /// <summary>
        /// 默认在搜索引擎中搜索的分页页数
        /// </summary>
        /// <value>默认10</value>
        public int SearchPageCount
        {
            get;
            set;
        }

        /// <summary>
        /// 异步开始执行搜索操作
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="Token">数据标识</param>
        public void DoWorkAsync(string keyWord, object Token)
        {

            KeyWord = keyWord;
            SearchPageCount = MaxPages;



            //异步执行
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {

                //返回的列表结果
                List<ListPageContentUrl> result = new List<ListPageContentUrl>();

                //使用多线程调用搜索引擎
                System.Threading.Tasks.Task[] tasks = Work();

                //等待搜索全部完成
                System.Threading.Tasks.Task.WaitAll(tasks);

                //合并结果
                tasks.ToList().ForEach((o) =>
                {
                    if (o.IsCompleted)
                    {
                        //添加到结果中
                        result.AddRange((o as System.Threading.Tasks.Task<List<ListPageContentUrl>>).Result);
                    }
                    //表示异步已经处理
                    if (o.Exception != null)
                    {
                        o.Exception.Handle((ex) =>
                        {
                            System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + "|||||" + ex.StackTrace);
                            return true;
                        });



                    }
                    if (o.Status == System.Threading.Tasks.TaskStatus.RanToCompletion
     || o.Status == System.Threading.Tasks.TaskStatus.Faulted
     || o.Status == System.Threading.Tasks.TaskStatus.Canceled)
                    {//释放资源
                        o.Dispose();
                    }
                });

                //指定索引列表
                IndexPageUrls = result;

                //事件完成调用方法 
                if (DoWorkAnsycComplete != null)
                {
                    DoWorkAnsycComplete(this, new SearchCompareEventArgs()
                    {
                        Reslut = result,
                        ToKen = Token
                    });
                }
                return result;
                
            });


        }

        /// <summary>
        /// 得到正文的一些路径
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<ListPageContentUrl> GetPageUrls(string keyWord)
        {
            KeyWord = keyWord;
            SearchPageCount = MaxPages;


            //返回的列表结果
            List<ListPageContentUrl> result = new List<ListPageContentUrl>();

            //使用多线程调用搜索引擎
            System.Threading.Tasks.Task[] tasks = Work(false);

            //等待搜索全部完成
            System.Threading.Tasks.Task.WaitAll(tasks);
            //合并结果
            tasks.ToList().ForEach((o) =>
            {
                if (o.IsCompleted)
                {
                    //添加到结果中
                    result.AddRange((o as System.Threading.Tasks.Task<List<ListPageContentUrl>>).Result);
                }
                //表示异步已经处理
                if (o.Exception != null)
                {
                    o.Exception.Handle((ex) =>
                    {
                        System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + "|||||" + ex.StackTrace);
                        return true;
                    });



                }
                if (o.Status == System.Threading.Tasks.TaskStatus.RanToCompletion
     || o.Status == System.Threading.Tasks.TaskStatus.Faulted
     || o.Status == System.Threading.Tasks.TaskStatus.Canceled)
                {//释放资源
                    o.Dispose();
                }
            });

            //指定索引列表
            IndexPageUrls = result;

            return result;
        }

        /// <summary>
        /// 开始执行搜索操作
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <returns>返回搜索到的列表集合</returns>
        public List<ListPageContentUrl> DoWork(string keyWord)
        {
            KeyWord = keyWord;
            SearchPageCount = MaxPages;


            //返回的列表结果
            List<ListPageContentUrl> result = new List<ListPageContentUrl>();

            //使用多线程调用搜索引擎
            System.Threading.Tasks.Task[] tasks = Work();

            //等待搜索全部完成
            System.Threading.Tasks.Task.WaitAll(tasks);
            //合并结果
            tasks.ToList().ForEach((o) =>
            {
                if (o.IsCompleted)
                {
                    //添加到结果中
                    result.AddRange((o as System.Threading.Tasks.Task<List<ListPageContentUrl>>).Result);
                }
                //表示异步已经处理
                if (o.Exception != null)
                {
                    o.Exception.Handle((ex) =>
                    {
                        System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + "|||||" + ex.StackTrace);
                        return true;
                    });



                }
            });

            //指定索引列表
            IndexPageUrls = result;

            return result;
        }

        #endregion


        #region 执行得到baidu搜索的一些数据处理方法

        /// <summary>
        /// 执行得到baidu搜索的全部页面url
        /// </summary>
        /// <returns>执行得到baidu搜索的全部页面url</returns>
        List<string> GetSearchUrls()
        {
            List<string> baiduUrls = new List<string>();
            for (int k = 1; k <= MaxPages; k++)
            {
                baiduUrls.Add(string.Format(BaduUrl, System.Web.HttpUtility.UrlEncode(KeyWord), k * PageSize));
            }
            return baiduUrls;
        }


        /// <summary>
        /// 作业的方法 封装
        /// </summary>
        /// <returns></returns>
        System.Threading.Tasks.Task[] Work(bool IsIndex = true)
        {
            //初始化要搜索的URL
            List<string> baiduUrls = GetSearchUrls();


            //使用多线程调用搜索引擎
            System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[MaxPages];
            #region 开始调用搜索引擎

            for (int k = 0; k < MaxPages; k++)
            {
                //初始化搜索线程并开始搜索 
                tasks[k] = System.Threading.Tasks.Task.Factory.StartNew<List<ListPageContentUrl>>((url) =>
                {
                    string BaiduPageHtml = url.ToString().GetWeb();
                    return AnalyseListUrls(BaiduPageHtml, IsIndex);

                }, baiduUrls[k]);
            }

            #endregion

            return tasks;

        }

        /// <summary>
        /// 传入搜索页面 的HTML内容
        /// 返回章节列表
        /// </summary>
        /// <param name="Html">搜索页面 的HTML内容</param>
        /// <returns>返回章节列表</returns>
        public List<ListPageContentUrl> AnalyseListUrls(string htmlContent, bool IsIndex = true)
        {
            List<ListPageContentUrl> pageListUrls = new List<ListPageContentUrl>();


            //原素标签
            var elementTags = new string[] { "p", "span", "strong", "font", "h1", "tbody", "o:p", "dd", "tr", "table" };
            //用于HTML处理
            HtmlDocument htmlDom = new HtmlDocument();
            #region 将HTML转换成 XDocuemnt


            try
            {

                //格式化为html
                htmlDom.LoadHtml(htmlContent);
                //格式化为html 
                htmlContent = Tong.HtmlToXML.HTMLConvert(htmlDom.DocumentNode.OuterHtml);
                //重新加载HTML字符串 对一些标签进行闭合
                htmlDom.LoadHtml(htmlContent.FiltrateHTML(elementTags));
                //重新转换字符串
                htmlContent = Tong.HtmlToXML.HTMLConvert(htmlDom.DocumentNode.OuterHtml);


            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                return pageListUrls;
            }
            #endregion


            #region 分析数据内容



            #region 处理所有原素数据 生成所有带a 的原素
            //可能的原素
            List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();

            //开始循环子原素
            SingleListPageAnalyse.AnalyseMaxATagNearest(htmlDom.DocumentNode, possiblyResultElements, 0, new PossiblyResultElement()
              {
                  ParentPossiblyResult = null,
                  CurrnetHtmlElement = htmlDom.DocumentNode,
                  LayerIndex = -1,
                  ContainTagNum = 0
              });
            #endregion

            //得到所页面中所有的A标题原素
            //并将标题转换成为简体中文
            var allAElements = from a in possiblyResultElements
                               where a.CurrnetHtmlElement.Name == "a" && a.CurrnetHtmlElement.Attributes["href"] != null
                               && a.CurrnetHtmlElement.Attributes["href"].Value.Length > 5 && a.CurrnetHtmlElement.Attributes["href"].Value.Contains("http")
                               && !a.CurrnetHtmlElement.Attributes["href"].Value.Contains("baidu")
                               && (
                                a.CurrnetHtmlElement.Attributes["href"].Value.EndsWith(".html", StringComparison.CurrentCultureIgnoreCase)
                               || a.CurrnetHtmlElement.Attributes["href"].Value.EndsWith(".aspx", StringComparison.CurrentCultureIgnoreCase)
                               || a.CurrnetHtmlElement.Attributes["href"].Value.EndsWith(".asp", StringComparison.CurrentCultureIgnoreCase)
                               || a.CurrnetHtmlElement.Attributes["href"].Value.EndsWith(".htm", StringComparison.CurrentCultureIgnoreCase)
                               || a.CurrnetHtmlElement.Attributes["href"].Value.EndsWith(".php", StringComparison.CurrentCultureIgnoreCase)
                               || a.CurrnetHtmlElement.Attributes["href"].Value.EndsWith(".shtml", StringComparison.CurrentCultureIgnoreCase)
                               )
                               select new ListPageContentUrl()
                               {
                                   Url = new Uri(a.CurrnetHtmlElement.Attributes["href"].Value),
                                   Title = a.CurrnetHtmlElement.InnerText.WordTraditionalToSimple(),
                               };
            //找到合适的章节列表
            //仙逆txt下载、仙逆全文阅读下载、仙逆免费章节列表 
            var Aels = allAElements.Where(p =>
                   (p.Title.Contains(KeyWord + "章节列表"))
                   ||
                   (p.Title.Contains(KeyWord + "txt"))
                    ||
                   (p.Title.Contains(KeyWord + "TXT"))
                                   ||
                   (p.Title.Contains(KeyWord + "全文阅读"))
                                   ||
                   (p.Title.Contains(KeyWord + "最新章节"))
                                   ||
                   (p.Title.Contains(KeyWord + "最新章节列表"))
                   ||
                   (p.Title.Contains(KeyWord + "目录"))
                   ||
                   (p.Title.Contains(KeyWord + "列表"))
                   ||
                   (p.Title.Contains(KeyWord))
                   );
            //有可能是正文
            if (!IsIndex)
            {
                Aels = allAElements;
            }
            allAElements = Aels;
            #endregion
            //转换为列表输出
            pageListUrls = allAElements.ToList();

            //清空资源
            possiblyResultElements.ForEach((x) => {
                //x.Dispose();
            });
            possiblyResultElements.Clear();

            return pageListUrls;
        }

        #endregion


    }
}
