using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Xml.Linq;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Analyse
{

    /// <summary>
    /// XML文档分析器
    /// </summary>
    public class XMLDocuentAnalyse : IAnalyse
    {

        //正文正则表达式有效长度
        protected int TextValidLength = 25;

        /// <summary>
        /// 用户操作常用类
        /// </summary>
        Skybot.Tong.TongUse tong = new Tong.TongUse();

        /// <summary>
        /// 索引页面
        /// </summary>
        public ListPageContentUrl IndexPageUrl
        {
            get;
            set;

        }


        #region IAnalyse 成员

        public string PathExpression
        {
            get;
            set;
        }

        public object UserToKen
        {

            get;
            set;
        }

        /// <summary>
        /// 返回XML表达式
        /// </summary>
        /// <returns></returns>
        public object GetResult()
        {
            return (PathExpression == null ? "" : PathExpression);
        }

        /// <summary>
        /// 查找到指定的表达式 没有则返回空
        /// </summary>
        /// <param name="IndexPageUrl">索引页面</param>
        /// <param name="ContentPageUrls">分析用的一个页面</param>
        /// <returns>查找到指定的表达式 没有则返回空</returns>
        public string GetPathExpression(ListPageContentUrl IndexPageUrl, System.Collections.Generic.IEnumerable<ListPageContentUrl> ContentPageUrls)
        {

            //得到当前所有的 XML分析结果
            List<XMLDocuentAnalyseEntity> xdoms = ParsPageUrlsToXDocuments(IndexPageUrl, ContentPageUrls);

            //对得到的XML内容进行分胡 取出有效的表达式
            var groups = xdoms.GroupBy(p => p.Expression);
            if (groups.Count() > 0)
            {
                //按表达式排序后的结果
                var groupOrder = groups.OrderBy(p => p.Key.Length);

                string express = groupOrder.Last().Key;

                if (express.Length > 0)
                {

                    //表达式 
                    var xpathArr = express.Split('/').ToList();
                    //如果最后一个是文字节点则直接删除
                    if (xpathArr.Last().StartsWith("#"))
                    {
                        xpathArr.Remove(xpathArr.Last());
                        express = string.Join("/", xpathArr.ToArray());
                    }
                }

                if (IndexPageUrl.Url.ToString() == this.IndexPageUrl.Url.ToString())
                {


                    //返回表达式
                    return PathExpression = express;
                }
                else
                {
                    return express;
                }

            }



            return "";


        }
        /// <summary>
        /// 得到当前文档的 主要 内容
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string GetContent(string Content)
        {
            //如果为 null 直接返回
            if (Content == null)
            {
                return "";
            }
            //如果没有内容
            if (Content.Length < 300)
            {
                return "";
            }

            //原素标签
            var elementTags = new string[] { "p", "span", "strong", "font", "h1", "tbody", "o:p", "dd", "tr", "table" };


            HtmlDocument htmlDom = new HtmlDocument();
            //格式化为html
            htmlDom.LoadHtml(Content);
            //格式化为html 
            Content = htmlDom.DocumentNode.OuterHtml;
            //重新加载HTML字符串 对一些标签进行闭合
            htmlDom.LoadHtml(Content.FiltrateHTML(elementTags));


            //如果有表达式则直接使用表达式
            if (PathExpression != "" && PathExpression!=null)
            {
                HtmlDocument dom = new HtmlDocument();
                dom.LoadHtml(Content);
                return dom.DocumentNode.SelectSingleNode(PathExpression).InnerHtml;
            }



            return GetContentX(Content);
        }

        /// <summary>
        /// 没有表达式
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="NoExpress"></param>
        /// <returns></returns>
        public string GetContentX(string Content)
        {            //如果为 null 直接返回
            if (Content == null)
            {
                return "";
            }
            //如果没有内容
            if (Content.Length < 300)
            {
                return "";
            }

            //原素标签
            var elementTags = new string[] { "p", "span", "strong", "font", "h1", "tbody", "o:p", "dd", "tr", "table" };

            HtmlDocument htmlDom = new HtmlDocument();
            //格式化为html
            htmlDom.LoadHtml(Content);
            //格式化为html 
            Content = htmlDom.DocumentNode.OuterHtml;
            //重新加载HTML字符串 对一些标签进行闭合
            htmlDom.LoadHtml(Content.FiltrateHTML(elementTags));


            //使用内容自动分析


            XMLDocuentAnalyseEntity entity = new XMLDocuentAnalyseEntity()
            {
                BaseUrlObject = null,
                XDocument = null,
                OriginContent = Content,
                PageTitle = "没有指定",
                Content = "",
                IsGetContentSuccess = false,
                IndexPageUrl = this.IndexPageUrl,
                XmlParseConten = "<data>" + Content + "</data>",
                Expression = "",
            };

            #region 得到文档内容
            // <summary>
            // 得到主要内容 的表达式 如果没有则返回空
            // 同时填充  Content ，Expression 属性
            // </summary>
            GetMainContentExpression(entity);
            #endregion
            return entity.Content;



        }

        #endregion

        #region 分析得到文章内容



        /// <summary>
        /// 将指定的url转换成为
        /// </summary>
        /// <param name="indexPageUrl">列表页面</param>
        /// <param name="ContentPageUrls">内容页面列表</param>
        /// <returns></returns>
        List<XMLDocuentAnalyseEntity> ParsPageUrlsToXDocuments(ListPageContentUrl indexPageUrl, System.Collections.Generic.IEnumerable<ListPageContentUrl> ContentPageUrls)
        {
            List<XMLDocuentAnalyseEntity> result = new List<XMLDocuentAnalyseEntity>();

            //需要分析的url
            System.Collections.Generic.List<ListPageContentUrl> analyseUrls = new List<ListPageContentUrl>();

            //如果转url大于10则只取10个
            analyseUrls = ContentPageUrls.Take(10).ToList();

            //开始创建多个并行任务数组
            System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task<XMLDocuentAnalyseEntity>[analyseUrls.Count];
            //实例化数组
            for (int k = 0; k < tasks.Length; k++)
            {
                tasks[k] = System.Threading.Tasks.Task.Factory.StartNew<XMLDocuentAnalyseEntity>((url) =>
                {
                    return FillContentUrl(indexPageUrl, (ListPageContentUrl)url);

                }, analyseUrls[k]);
            }

            //等待并行计算完成
            System.Threading.Tasks.Task.WaitAll(tasks);

            //填充结果
            tasks.ToList().ForEach((o) =>
            {
                if (o.IsCompleted)
                {
                    result.Add(((System.Threading.Tasks.Task<XMLDocuentAnalyseEntity>)o).Result);
                }
                //表示异步已经处理
                if (o.Exception != null)
                {
                    o.Exception.Handle((ex) =>
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message + "|||||" + ex.StackTrace);
                        return true;
                    });



                }
            });

            return result;
        }


        /// <summary>
        /// 找到当前文章内容并填充为xdom原素
        /// </summary>
        /// <param name="indexPageUrl"></param>
        /// <param name="ContentPageUrl"></param>
        /// <returns></returns>
        XMLDocuentAnalyseEntity FillContentUrl(ListPageContentUrl indexPageUrl, ListPageContentUrl ContentPageUrl)
        {

            //主要内容 
            string MainContent = "";
            //用于保存原始内容 
            string originContent = "";
            //得到要读取的页面
            string pageurl = ContentPageUrl.Url.ToString();

            //结果文档
            XMLDocuentAnalyseEntity entity = null;

            //得到当前页面的HTML内容 
            string htmlContent = "";
            //原素标签
            var elementTags = new string[] { "p", "span", "strong", "font", "h1", "tbody", "o:p", "dd", "tr", "table" };
            // 处理Xml的dom
            XDocument xdom = null;
            //基url
            Uri baseUri = new Uri(pageurl);
            #region 把文档转换成为 XDocument


            //得到当前页面的HTML内容 
            //并将HTML转换成为xml
            originContent = htmlContent = pageurl.GetWeb();
            HtmlDocument htmlDom = new HtmlDocument();
            //格式化为html
            htmlDom.LoadHtml(htmlContent);
            //格式化为html 
            //htmlContent = Tong.HtmlToXML.HTMLConvert(htmlDom.DocumentNode.OuterHtml);
            //重新加载HTML字符串 对一些标签进行闭合
            htmlDom.LoadHtml(htmlContent.FiltrateHTML(elementTags));
            //重新转换字符串
            //htmlContent = Tong.HtmlToXML.HTMLConvert(htmlDom.DocumentNode.OuterHtml);
            //可能无法转换成为XML结构    
            try
            {
                xdom = XDocument.Parse("<data>" + htmlContent + "</data>");
            }
            catch
            {

            }



            #endregion


            entity = new XMLDocuentAnalyseEntity()
             {
                 BaseUrlObject = ContentPageUrl,
                 XDocument = xdom,
                 OriginContent = htmlContent,
                 PageTitle = ContentPageUrl.Title,
                 Content = MainContent,
                 IsGetContentSuccess = MainContent.Length > 50 ? true : false,
                 IndexPageUrl = indexPageUrl,
                 XmlParseConten = "<data>" + htmlContent + "</data>",
                 Expression = "",
             };

            #region 得到文档内容
            GetMainContentExpression(entity);
            #endregion

            //重新设置加载状态
            entity.IsGetContentSuccess = MainContent.Length > 50 ? true : false;

            //返回结果
            return entity;
        }


        /// <summary>
        /// 得到主要内容 的表达式 如果没有则返回空
        /// 同时填充  Content ，Expression 属性
        /// </summary>
        /// 算法说明
        /// 使用XMLDocument 
        /// 1. 使用正则表达式得到所有中文集合 , 取得所有的中文一段一段的集合
        /// 2. 转换成为 正则表达式 Match的集合,方便使用Linq 计算
        string GetMainContentExpression(XMLDocuentAnalyseEntity domEntity)
        {


            //主要内容 
            string mainContont = "";

            //使用正则表达式得到所有中文集合
            System.Text.RegularExpressions.MatchCollection MC = System.Text.RegularExpressions.Regex.Matches(domEntity.XmlParseConten, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]");

            //取得所有的中文一段一段的集合
            //转换成为 正则表达式 Match的集合
            int index = 0;
            var matchResluts = from x in MC.Cast<System.Text.RegularExpressions.Match>()
                               select new MatchReslut
                               {
                                   Match = x,
                                   Index = index++,
                                   StrinLength = x.Value.Length,
                                   //与上一个表达式相隔的位置，一个 mc 一个位置记数
                                   space = 0
                               }
                                  ;



            //得到有有效内容的集合
            List<MatchReslut> ContentTempTextarr = new List<MatchReslut>();


            try
            {

                //计算出 上一个表达式相隔的位置，一个 mc 一个位置记数
                matchResluts.Where(p => p.StrinLength >= TextValidLength).Aggregate((curr, next) =>
                {
                    ContentTempTextarr.Add(new MatchReslut
                                     {
                                         Match = next.Match,
                                         Index = next.Index,
                                         StrinLength = next.StrinLength,
                                         //与上一个表达式相隔的位置，一个 mc 一个位置记数
                                         space = Math.Abs(next.Index - curr.Index)
                                     });
                    return next;
                });
            }
                //序列不包含任何异常
            catch { 
                
            }

            //如果没有找到有效的内容 可能是图片/
            //需要对文字内容处理后得到的表达式对这个URL进行处理
            if (ContentTempTextarr.Count() == 0)
            {
                return "";
            }


            //填充距离 位置记数后使用Xdocument来取得文章内容
            //遍历有效内容集合，对 space  距离 位置记数 进行分析 如果两个 相近的原素间 space 值过大则可能是两个不同的原素了
            //对结果进行用 Space分组
            List<List<MatchReslut>> groupMathResult = new List<List<MatchReslut>>();

            //如果超过10 则可能需要分成不同的组了
            int MaxSpace = 10;

            //用于添加的组
            List<MatchReslut> group = new List<MatchReslut>();
            //添加默认组
            groupMathResult.Add(group);
            //将可能的文字编写方式，进行分组
            ContentTempTextarr.ForEach((o) =>
            {

                //需要分胡重新初始化
                if (o.space > MaxSpace)
                {
                    group = new List<MatchReslut>();
                    groupMathResult.Add(group);
                }
                group.Add(o);

            });

            //取出结果最多的那个组做为有效内容的  集合进行提取
            //找到有效的原素
            var ValidMathes = groupMathResult.OrderByDescending(p => p.Count).ElementAt(0);

            //通过有效值生成动态数表达式
            List<string> regStr = new List<string>();
            ValidMathes.ForEach((o) =>
            {
                if (o != ValidMathes.Last())
                {
                    regStr.Add(o.Match.Value + @"[\w\W]+");
                }
                else
                {
                    regStr.Add(o.Match.Value);
                }
            });


            //提取数据
            mainContont = System.Text.RegularExpressions.Regex.Match(domEntity.XmlParseConten, string.Join("", regStr.ToArray())).Value;


            #region 开始查找包含这些内容的最内层的原素
            HtmlDocument htmldom = new HtmlDocument();

            htmldom.LoadHtml(domEntity.OriginContent);

            List<HtmlNode> htmlNodes = new List<HtmlNode>();


            // 遍历所有HTML原素
            EachAllHTMLElelents(htmldom.DocumentNode, htmlNodes, tong.ForMatTextReplaceHTMLTags(mainContont).Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", ""));

            if (htmlNodes.Count > 0)
            {
                var Mainel = htmlNodes.Last();
                //设置内容
                domEntity.Content = Mainel.InnerHtml;

                //指表达式
                domEntity.Expression = Mainel.XPath;

                //返回path表达式
                return Mainel.XPath;

            }
            return "";



            #endregion

        }
        /// <summary>
        /// 遍历所有HTML原素
        /// </summary>
        /// <param name="htmlNode">html 原素</param>
        /// <param name="ContainsContent">包含的字符串内容 </param>
        /// <param name="htmlNodes">添加到的包含字符串内容的集合 </param>
        void EachAllHTMLElelents(HtmlNode htmlNode, List<HtmlNode> htmlNodes, string ContainsContent)
        {


            foreach (HtmlNode node in htmlNode.ChildNodes)
            {


                if (tong.ForMatTextReplaceHTMLTags(node.InnerHtml).Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Contains(ContainsContent))
                {
                    htmlNodes.Add(node);
                    EachAllHTMLElelents(node, htmlNodes, ContainsContent);
                    break;
                }
            }
        }

        #endregion


    }

    /// <summary>
    /// 正则表达式扩展结果 
    /// </summary>
    public class MatchReslut
    {
        /// <summary>
        ///  表示单个正则表达式匹配的结果。
        /// </summary>
        public System.Text.RegularExpressions.Match Match { get; set; }
        /// <summary>
        ///  表示单个正则表达式匹配的结果。 在总结果集合中的索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        ///  表示单个正则表达式匹配的结果。 长度
        /// </summary>
        public int StrinLength { get; set; }
        /// <summary>
        /// 与上一个表达式相隔的位置，一个 mc 一个位置记数
        /// </summary>
        public int space { get; set; }


    }

    /// <summary>
    /// xml文档页面内容分析器 分析后所得到的结果
    /// </summary>
    public class XMLDocuentAnalyseEntity
    {

        internal XDocument XDocument { get; set; }

        /// <summary>
        /// 基URL
        /// </summary>
        public ListPageContentUrl BaseUrlObject
        {
            get;
            set;
        }

        /// <summary>
        /// 页面标题
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 原始内容 
        /// </summary>
        public string OriginContent { get; set; }

        /// <summary>
        /// 分析后得到 的  文章内容 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 当前得到文章内容 是不是成功
        /// </summary>
        public bool IsGetContentSuccess { get; set; }


        /// <summary>
        /// 用于转换成为 XDocument 文档内容 
        /// </summary>
        public string XmlParseConten { get; set; }

        /// <summary>
        /// 表达式 如果没有则为空
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 索引页面
        /// </summary>
        public ListPageContentUrl IndexPageUrl
        {
            get;
            set;

        }

    }


}