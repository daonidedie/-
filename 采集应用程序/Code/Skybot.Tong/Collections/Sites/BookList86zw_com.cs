using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 86中文网 http://www.86zw.com/
    /// </summary>
    public class BookList86zw_com : AbstractBookList
    {

        public BookList86zw_com()
        {
            BaseUrl = "http://www.86zw.com/Book/ShowBookList.aspx?tclassid=1&page={0}";
            WebAddress = "www.86zw.com";
        }

        public override IEnumerable<AbstractBookInfo> DoWork()
        {
            string url = string.Format(BaseUrl, "1");
            //得到最大的页码
            int MaxPageNum = 1;
            //初始化一个DOM
            HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(url.GetWeb());

            //内容
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("CrListTitle");

            //得到分页
            HtmlAgilityPack.HtmlNode pagesNode = dom.GetElementbyId("CrListPage");


            //得到最大页码
            MaxPageNum = GetMaxPageNum(pagesNode);

            IEnumerable<AbstractBookInfo> AllBooks = GetBooks(MaxPageNum, GetPageBooks);

            return AllBooks;
        }

        public override int GetMaxPageNum(HtmlAgilityPack.HtmlNode pagesNode)
        {
            // &amp;page=83
            //得到最大的页码
            int _MaxPageNum = 1;

            if (pagesNode == null)
            {
                return 0;
            }

            //可能的原素
            List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();


            //开始循环子原素
            SingleListPageAnalyse.AnalyseMaxATagNearest(pagesNode, possiblyResultElements, 0, new PossiblyResultElement()
            {
                ParentPossiblyResult = null,
                CurrnetHtmlElement = pagesNode,
                LayerIndex = -1,
                ContainTagNum = 0
            });

            int x = 0;
            //计算当前所有HTML原素中的A原素
            var PageAElements = from a in possiblyResultElements
                                where a.CurrnetHtmlElement.Name == "a" && a.CurrnetHtmlElement.Attributes["href"] != null
                                select new Func<int>(() =>
                                {
                                    var mc = System.Text.RegularExpressions.Regex.Match(a.CurrnetHtmlElement.Attributes["href"].Value.ToString(), @"&amp;page=\d{1,}");
                                    if (mc != null)
                                    {
                                        var pageNum = mc.Value.Replace("&amp;page=", "");


                                        int.TryParse(

                                            pageNum
                                        , out x);
                                    }
                                    return x;
                                }).Invoke();
            if (PageAElements.Count() > 0)
            {

                //得到最大的页码
                _MaxPageNum = PageAElements.OrderByDescending(p => p).FirstOrDefault();
            }
            return _MaxPageNum;


            return base.GetMaxPageNum(pagesNode);
        }

        /// <summary>
        /// 得到指定页面的Page代码
        /// </summary>
        /// <param name="url">页面完整的URL</param>
        /// <returns></returns>
        public IEnumerable<AbstractBookInfo> GetPageBooks(string url)
        {
            //初始化一个DOM
            HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(url.GetWeb());

            //内容
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("CrListText");

            Uri baseUri = new Uri(url);
            //找到 相当于 tr 的原素用于分开记录的
            return GetBookInfos(listContent, url, li => li.CurrnetHtmlElement.Name == "ul",
                (o) => { return GetBookInfoByHtmlNode(o, url, baseUri); }
                ).Where(p => p.小说名称 != null && p.小说目录URL != null);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="els">原素</param>
        /// <param name="url">当前采集路径</param>
        /// <param name="baseUri">基url</param>
        /// <returns></returns>
        public AbstractBookInfo GetBookInfoByHtmlNode(IEnumerable<HtmlAgilityPack.HtmlNode> els, string url, Uri baseUri)
        {
            BookInfo86zw_com bookinfo = new BookInfo86zw_com();
            if (els.ElementAt(1).Name == "li" && els.Count() >= 5)
            {

                //<li class="li1"><a href="/Book/LN/20.aspx">东方玄幻</a></li>
                //<li class="li2"><a href="/Html/Book/0/252/Index.shtml"><font color="#006699">[目录]</font></a>&nbsp;<a href="/Book/252/Index.aspx"><font color="#006699">蜀山乱</font></a></li>
                //<li class="li3"><a href="/Html/Book/0/252/49702.shtml">正文 第五卷 第十九章 金光擒矮子 符咒定金石</a></li>
                //<li class="li4">5月3日</li>
                //<li class="li5"><a href="/Author/WB/252.aspx">云飞洛晚</a></li>
                //<li class="li6"><font color=blue>连载</font></li>

                //当前数据实体的类型，用于反射
                Type bookInfoType = bookinfo.GetType();
                //填充  类别 小说名称 最新章节 作者 字数 更新 状态 
                bookinfo.类别 = els.ElementAt(0).InnerText;
                bookinfo.最新章节 = els.ElementAt(2).InnerText;
                bookinfo.作者 = els.ElementAt(4).InnerText;
                //bookinfo.更新 = els.ElementAt(5).InnerText;
                bookinfo.状态 = els.ElementAt(5).InnerText;
                bookinfo.采集URL = url;


                try
                {
                    bookinfo.小说简介URL =  els.ElementAt(1).ChildNodes.Where(p => p.Name == "a").ElementAt(1).Attributes["href"].Value.ToString();
                    bookinfo.小说简介URL = new Uri(baseUri, bookinfo.小说简介URL).ToString();
                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }

                if (els.ElementAt(1).ChildNodes.Count >= 2)
                {
                    try
                    {

                        bookinfo.小说目录URL = els.ElementAt(1).ChildNodes.Where(p => p.Name == "a").ElementAt(0).Attributes["href"].Value.ToString();
                        bookinfo.小说目录URL = new Uri(baseUri, bookinfo.小说目录URL).ToString();

                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }
                    try
                    {
                        bookinfo.小说名称 = els.ElementAt(1).ChildNodes.Where(p => p.Name == "a").ElementAt(1).InnerText;
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }


                }

            }
            return bookinfo;
        }

    }
}
