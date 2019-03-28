using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 大文学 http://www.dawenxue.org
    /// </summary>
    public class BookListdawenxue_org : AbstractBookList
    {

        /// <summary>
        /// 分类
        /// </summary>
        public string[] arrTypes = @"
玄幻魔法,http://www.dawenxue.org/book/sort1/0/
奇幻修真,http://www.dawenxue.org/book/sort2/0/
都市言情,http://www.dawenxue.org/book/sort3/0/
历史军事,http://www.dawenxue.org/book/sort4/0/
推理侦探,http://www.dawenxue.org/book/sort5/0/
网游动漫,http://www.dawenxue.org/book/sort6/0/
科幻小说,http://www.dawenxue.org/book/sort7/0/
恐怖灵异,http://www.dawenxue.org/book/sort8/0/
散文诗词,http://www.dawenxue.org/book/sort9/0/
其他类型,http://www.dawenxue.org/book/sort10/0/
".Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);



        public BookListdawenxue_org()
        {
            BaseUrl = "http://www.dawenxue.org/book/sort1/0/{0}.html";
            WebAddress = "www.dawenxue.org";
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
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("content");

            //得到分页
            HtmlAgilityPack.HtmlNode pagesNode = dom.GetElementbyId("pagestats");


            //得到最大页码
            MaxPageNum = GetMaxPageNum(pagesNode);
            
            //最多只取100页
            if (MaxPageNum > 100)
            {
                MaxPageNum=100;
            }
           

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

            //1/760
            var arr = pagesNode.InnerText.Split('/');
            if (arr.Length > 1)
            {
                _MaxPageNum = int.Parse(arr[1]);
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
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("content");

            //得到表格清单
            //		listContent.ChildNodes[3].XPath	"/html[1]/body[1]/div[1]/div[3]/div[7]/div[2]/div[1]/table[2]"	string
            if (listContent == null)
            {

                return new List<AbstractBookInfo>();
            }

            Uri baseUri = new Uri(url);
            //找到 相当于 tr 的原素用于分开记录的
            return GetBookInfos(listContent, url, li => li.CurrnetHtmlElement.Name == "tr" && li.CurrnetHtmlElement.InnerHtml.Contains("td"),
                (o) => { return GetBookInfoByHtmlNode(o, url, baseUri); }
                ).Where(p => p.小说名称 != null && p.小说目录URL != null);
        }


        /// <summary>
        /// 提取单个页面的图书索引
        /// </summary>
        /// <param name="listContent">列表内容HTML容器原素</param>
        /// <param name="url">所排定的分析URL路径</param>
        /// <param name="exp">如 tr =&gt; tr.CurrnetHtmlElement.Name == "tr"; </param>
        /// <returns></returns>
        public override List<AbstractBookInfo> GetBookInfos(HtmlAgilityPack.HtmlNode listContent, string url, System.Linq.Expressions.Expression<Func<PossiblyResultElement, bool>> exp, Func<IEnumerable<HtmlAgilityPack.HtmlNode>, AbstractBookInfo> ParseResultFunction)
        {
            //用于返回的结果
            List<AbstractBookInfo> result = new List<AbstractBookInfo>();

            //可能的原素
            List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();


            //开始循环子原素
            SingleListPageAnalyse.AnalyseMaxATagNearest(listContent, possiblyResultElements, 0, new PossiblyResultElement()
            {
                ParentPossiblyResult = null,
                CurrnetHtmlElement = listContent,
                LayerIndex = -1,
                ContainTagNum = 0
            });

            //得到动态表达式
            Func<PossiblyResultElement, bool> func = exp.Compile();

            //处理大文学的文章列表
            var query=  possiblyResultElements.Where(p => p.CurrnetHtmlElement.Id == "centerm");


            //计算当前所有HTML原素中的tr原素
            var PageTrElements = from tr in possiblyResultElements
                                 where func(tr) //
                                 select tr;
            //填类
            foreach (var item in PageTrElements)
            {
                if (item.CurrnetHtmlElement.HasChildNodes)
                {
                    var els = item.CurrnetHtmlElement.ChildNodes;
                    result.Add(ParseResultFunction(els));
                }
            }

            return result;

        }


        /// <summary>
        /// 传入URL返回类型
        /// </summary>
        /// <param name="url">采集列表的URL</param>
        /// <returns></returns>
        public string GetBookType(string url)
        {
            try
            {
                foreach (string typeurl in arrTypes)
                {
                    if (url.Contains(typeurl.Split(',')[1]))
                    {
                        return typeurl.Split(',')[0];
                    }
                }
            }
            catch { } return "其他类型";
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
            BookInfodawenxue_org bookinfo = new BookInfodawenxue_org();
            els = els.Where(p => p.Name == "td");
            if (els.ElementAt(1).Name == "td" && els.Count() >= 5)
            {

                //<tr>
                //  <td class="odd"><a href="http://www.dawenxue.net/html/73/73184/index.html">酷酷总裁</a></td>
                //  <td class="even"><a href="http://www.dawenxue.net/html/73/73184/index.html" target="_blank"> 第220章 你做了什么？1</a></td>
                //  <td class="odd">话梅糖</td>
                //  <td class="even">667K</td>
                //  <td class="odd" align="center">12-09-18</td>
                //  <td class="even" align="center">连载</td>
                //</tr>


                //当前数据实体的类型，用于反射
                Type bookInfoType = bookinfo.GetType();
                //填充  类别 小说名称 最新章节 作者 字数 更新 状态 
                bookinfo.类别 = GetBookType(url);
                bookinfo.最新章节 = els.ElementAt(1).InnerText;
                bookinfo.作者 = els.ElementAt(2).InnerText;
                bookinfo.更新 = "20" + els.ElementAt(4).InnerText;
                bookinfo.状态 = els.ElementAt(5).InnerText;
                bookinfo.采集URL = url;



                try
                {
                    bookinfo.小说简介URL = els.ElementAt(1).ChildNodes.Where(p => p.Name == "a").ElementAt(0).Attributes["href"].Value.ToString();
                    bookinfo.小说简介URL = new Uri(baseUri, bookinfo.小说简介URL).ToString();
                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }


                try
                {

                    bookinfo.小说目录URL = els.ElementAt(1).ChildNodes.Where(p => p.Name == "a").ElementAt(0).Attributes["href"].Value.ToString();
                    bookinfo.小说目录URL = new Uri(baseUri, bookinfo.小说目录URL).ToString();

                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }
                try
                {
                    bookinfo.小说名称 = els.ElementAt(0).InnerText;
                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace); }




            }


            return bookinfo;
        }

    }
}
