using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 获取站点书本列列的抽象类
    /// </summary>
    public abstract class AbstractBookList
    {

        private int _MaxPageNum = 1;

        /// <summary>
        /// 最大页码
        /// </summary>
        public int MaxPageNum
        {
            get { return _MaxPageNum; }
            set { _MaxPageNum = value; }
        }

        /// <summary>
        /// 如 yankuankan.com 
        /// </summary>
        private string _WebAddress = "";

        /// <summary>
        /// 网站 如 yankuankan.com 
        /// </summary>
        public string WebAddress
        {
            get { return _WebAddress; }
            set { _WebAddress = value; }
        }

        /// <summary>
        /// 如 http://www.yankuai.com/book/toplastupdate/0/{0}.htm
        /// </summary>
        private string _BaseUrl = "";//1-175

        /// <summary>
        /// 页面基路径 如http://www.yankuai.com/book/toplastupdate/0/{0}.htm
        /// </summary>
        public string BaseUrl
        {
            get { return _BaseUrl; }
            set
            {
                if (!value.Contains("{0}"))
                {
                    throw new Exception("基URL中不包括{0}页面分页的页码");
                }
                _BaseUrl = value;

            }
        }



        /// <summary>
        /// 开始工作
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<AbstractBookInfo> DoWork();


        /// <summary>
        /// 得到所有页面书集合
        /// </summary>
        /// <param name="MaxPageNum">全部页面列表</param>
        /// <param name="AnalyseMethod">分析方法 用于分析提取页面中的书本信息</param>
        public IEnumerable<AbstractBookInfo> GetBooks(int MaxPageNum, Func<string, IEnumerable<AbstractBookInfo>> AnalyseMethod)
        {

            List<AbstractBookInfo> books = new List<AbstractBookInfo>();

            foreach (var pageurlIndex in System.Linq.Enumerable.Range(1, MaxPageNum))
            {
                string url = string.Format(BaseUrl, "" + pageurlIndex);
                IEnumerable<AbstractBookInfo> Pagebooks = AnalyseMethod(url);
                books.AddRange(Pagebooks);

                System.Diagnostics.Debug.WriteLine("已经完成页面" + pageurlIndex + "/" + MaxPageNum);
            }

            return books;
        }


        /// <summary>
        /// 提取单个页面的图书索引
        /// </summary>
        /// <param name="listContent">列表内容HTML容器原素</param>
        /// <param name="url">所排定的分析URL路径</param>
        /// <param name="exp">如 tr =&gt; tr.CurrnetHtmlElement.Name == "tr"; </param>
        /// <returns></returns>
        public virtual List<AbstractBookInfo> GetBookInfos(HtmlAgilityPack.HtmlNode listContent, string url, System.Linq.Expressions.Expression<Func<PossiblyResultElement, bool>> exp, Func<IEnumerable<HtmlAgilityPack.HtmlNode>, AbstractBookInfo> ParseResultFunction)
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

            //计算当前所有HTML原素中的tr原素
            var PageTrElements = from tr in possiblyResultElements
                                 where func(tr) //
                                 select tr;
            //填类
            foreach (var item in PageTrElements)
            {
                if (item.CurrnetHtmlElement.HasChildNodes)
                {
                    var els = item.CurrnetHtmlElement.ChildNodes.Where(p => p.HasChildNodes);
                    result.Add(ParseResultFunction(els));
                }
            }

            return result;

        }

        /// <summary>
        /// 传入分页 基容器 得到最大的页码
        /// </summary>
        /// <param name="pagesNode">分页页码  基容器</param>
        /// <returns>最大的页码</returns>
        public virtual int GetMaxPageNum(HtmlAgilityPack.HtmlNode pagesNode)
        {
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
                                select new Func<int>(() => { int.TryParse(a.CurrnetHtmlElement.InnerText, out x); return x; }).Invoke();
            if (PageAElements.Count() > 0)
            {

                //得到最大的页码
                _MaxPageNum = PageAElements.OrderByDescending(p => p).FirstOrDefault();
            }
            return _MaxPageNum;

        }





    }

}
