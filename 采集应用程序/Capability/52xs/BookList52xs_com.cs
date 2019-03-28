using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 大文学 http://www.xs52.com
    /// </summary>
    public class BookListxs52_com : AbstractBookList
    {

        /// <summary>
        /// 分类
        /// </summary>
        public string[] arrTypes = @"
玄幻奇幻,http://www.xs52.com/booksort1/
武侠仙侠,http://www.xs52.com/booksort2/
都市言情,http://www.xs52.com/booksort3/
历史军事,http://www.xs52.com/booksort4/
科幻小说,http://www.xs52.com/booksort5/
恐怖灵异,http://www.xs52.com/booksort6/
军事侦探,http://www.xs52.com/booksort7/
网游竞技,http://www.xs52.com/booksort8/
综合其他,http://www.xs52.com/booksort9/
全本小说,http://www.xs52.com/modules/article/index.php?fullflag=1&page=
".Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);



 
 
 
 
 

        public BookListxs52_com()
        {
            BaseUrl = "http://www.xs52.com/booksort1/{0}.html";
            WebAddress = "www.xs52.com";
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
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("centerm");
   
         
            //得到分页
            HtmlAgilityPack.HtmlNode pagesNode = dom.GetElementbyId("pagestats");


            //得到最大页码
            MaxPageNum = GetMaxPageNum(pagesNode);
            
            //最多只取50页
            if (MaxPageNum > 50)
            {
                MaxPageNum=50;
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
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("centerm");

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
                    var book = ParseResultFunction(els);
                    if (book.小说目录URL != null)
                    {
                        result.Add(book);
                    }
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
            BookInfoxs52_com bookinfo = new BookInfoxs52_com();
            els = els.Where(p => p.Name == "td");
            if (els.ElementAt(1).Name == "td" && els.Count() >= 5&&els.Count()<10)
            {

                //<tr>
                //  <td class="odd"><a href="http://www.dawenxue.net/html/73/73184/index.html">酷酷总裁</a></td>
                //  <td class="even"><a href="http://www.dawenxue.net/html/73/73184/index.html" target="_blank"> 第220章 你做了什么？1</a></td>
                //  <td class="odd">话梅糖</td>
                //  <td class="even">667K</td>
                //  <td class="odd" align="center">12-09-18</td>
                //  <td class="even" align="center">连载</td>
                //</tr>

                //
                //<td class="odd"><a href="http://www.xs52.com/xiaoshuo/17/17067/" target="_blank">无敌升级王</a></td>
                //<td class="even">[<a href="http://www.xs52.com/xiaoshuo/17/17067/" target="_blank">目录</a>] <a href="http://www.xs52.com/xiaoshuo/17/17067/7082771.html" target="_blank">正文 第401章 顽童一般的九指剑仙</a></td>
                //<td class="odd">可爱内内</td>
                //<td class="odd" align="center">13-06-02</td>
                //<td class="even" align="center">连载中</td>


                //当前数据实体的类型，用于反射
                Type bookInfoType = bookinfo.GetType();
                //填充  类别 小说名称 最新章节 作者 字数 更新 状态 
                bookinfo.类别 = GetBookType(url);
                bookinfo.最新章节 = els.ElementAt(1).InnerText.Replace("[目录]","");
                bookinfo.作者 = els.ElementAt(2).InnerText;
                bookinfo.更新 = "20" + els.ElementAt(3).InnerText;
                bookinfo.状态 = els.ElementAt(4).InnerText;
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
