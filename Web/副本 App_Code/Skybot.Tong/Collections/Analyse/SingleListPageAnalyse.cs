using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace Skybot.Collections.Analyse
{
    /// <summary>
    /// 单列表无分页索引页面分析
    /// </summary>
    public class SingleListPageAnalyse : IAnalyse
    {
        #region 构造函数
        /// <summary>
        /// 单列表无分页索引页面分析
        /// </summary>
        /// <param name="indexPageUrl">需要初始化的索引页面路径</param>
        public SingleListPageAnalyse(string indexPageUrl)
        {
            IndexPageUrl = indexPageUrl;

            //开始分析表达式
            Analyse();

        }
        #endregion




        /// <summary>
        /// 索引页面路径 
        /// </summary>
        public string IndexPageUrl { get; protected set; }

        /// <summary>
        /// 内容 
        /// </summary>
        private string content;

        private List<ListPageContentUrl> _ListPageContentUrls = new List<ListPageContentUrl>();

        /// <summary>
        /// 内容页面集合对像
        /// </summary>
        public List<ListPageContentUrl> ListPageContentUrls
        {
            get { return _ListPageContentUrls; }
            set { _ListPageContentUrls = value; }
        }

        #region IAnalyse 成员

        /// <summary>
        /// 用户数据表达式,如果不能生成表达式则返回 null
        /// </summary>
        public string PathExpression
        {
            get;
            set;
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public object UserToKen
        {

            get;
            set;
        }

        /// <summary>
        /// 列表页面不支持此方法
        /// </summary>
        /// <param name="IndexPageUrl"></param>
        /// <param name="ContentPageUrls"></param>
        /// <returns></returns>
        public string GetPathExpression(ListPageContentUrl IndexPageUrl, System.Collections.Generic.IEnumerable<ListPageContentUrl> ContentPageUrls)
        {
            throw new NotImplementedException("列表页面不支持此方法");
        }

        /// <summary>
        /// 得到内容
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string GetContent(string Content)
        {
            return content;
        }

        #endregion

        #region 分析列表页面内容表达式




        /// <summary>
        /// 得到一个标题多个章节内容合并时的索引
        /// </summary>
        /// <returns></returns>
        List<int> GetTitleIndexs(ListPageContentUrl Item)
        {

            return GetTitleIndexs(Item.index);

        }
        /// <summary>
        /// 得到一个标题多个章节内容合并时的索引
        /// </summary>
        /// <returns></returns>
        List<int> GetTitleIndexs(int Item)
        {

            // 拆分 章节编号
            //          算法说明
            //如果章节 的长度是奇数，则很有可能是两个章节， 
            //如果是5，则前面取2个，后面取三个，因为同样的章节后面的数会比前面的大
            //如果长度是7 则前面取3个后面取4个

            //如果长度是偶数则对半取
            //如果长度是 6 则前面取3 个后面取3个
            //如果长度为8 则前面取4 个后面取4个
            //得到
            string indexstr = Item.ToString();
            //索引结果集合
            List<int> Indexs = new List<int>();
            //奇数
            if (indexstr.Length % 2 == 1)
            {
                switch (indexstr.Length)
                {
                    case 5:
                        Indexs.Add(int.Parse(new string(indexstr.Take(2).ToArray())));
                        Indexs.Add(int.Parse(new string(indexstr.Skip(2).Take(3).ToArray())));
                        break;
                    case 7:
                        Indexs.Add(int.Parse(new string(indexstr.Take(3).ToArray())));
                        Indexs.Add(int.Parse(new string(indexstr.Skip(3).Take(4).ToArray())));
                        break;
                }
            }
            //如果长度是偶数
            else
            {
                switch (indexstr.Length)
                {

                    case 6:
                        Indexs.Add(int.Parse(new string(indexstr.Take(3).ToArray())));
                        Indexs.Add(int.Parse(new string(indexstr.Skip(3).Take(3).ToArray())));

                        break;
                    case 8:
                        Indexs.Add(int.Parse(new string(indexstr.Take(4).ToArray())));
                        Indexs.Add(int.Parse(new string(indexstr.Skip(4).Take(4).ToArray())));
                        break;
                }
            }
            return Indexs;
        }

        /// <summary>
        /// 分析表达式
        /// </summary>
        void Analyse()
        {


            //得到当前页面的HTML内容 
            string htmlContent = "";
            //原素标签
            var elementTags = new string[] { "p", "span", "strong", "font", "h1", "tbody", "o:p", "dd", "tr", "table" };

            //用于HTML处理
            HtmlDocument htmlDom = new HtmlDocument();

            //基url
            Uri baseUri = new Uri(IndexPageUrl);
            #region

            #endregion
            try
            {
                //得到当前页面的HTML内容 
                //并将HTML转换成为xml
                content = htmlContent = IndexPageUrl.GetWeb();

                //格式化为html
                htmlDom.LoadHtml(htmlContent);
                //格式化为html 
                htmlContent = Tong.HtmlToXML.HTMLConvert(htmlDom.DocumentNode.OuterHtml);
                //重新加载HTML字符串 对一些标签进行闭合
                htmlDom.LoadHtml(htmlContent.FiltrateHTML(elementTags));
               


            }
            catch
            {
                return;
            }

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

            #region 尝试分析原始，找到包括 a href 最多的原素 目前 还不能完成
            ////得到最合适的原素
            //var els = from p in possiblyResultElements
            //          where p.ContainTagNum > 0
            //          orderby p.LayerIndex ascending
            //          orderby p.ContainTagNum , p.ContainTagNum descending
            //          select p;

            ////得到结果
            //var relsutels = els.ToList().Take(3);
            #endregion

            #endregion



            //得到所页面中所有的A标题原素
            //并将标题转换成为简体中文
            var allAElements = from a in possiblyResultElements
                               where a.CurrnetHtmlElement.Name == "a" && a.CurrnetHtmlElement.Attributes["href"] != null
                               select new ListPageContentUrl()
                               {
                                   Url = new Uri(baseUri, a.CurrnetHtmlElement.Attributes["href"].Value),
                                   Title = a.CurrnetHtmlElement.InnerText.WordTraditionalToSimple(),
                                   index = System.Text.RegularExpressions.Regex.Match(a.CurrnetHtmlElement.InnerText.WordTraditionalToSimple(), @"第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}章").Value.Replace("第", "").Replace("章", "").ConverToDigit(),
                                   Indexs = GetTitleIndexs(System.Text.RegularExpressions.Regex.Match(a.CurrnetHtmlElement.InnerText.WordTraditionalToSimple(), @"第[壹一两贰二叁三肆四伍五陆六柒七捌八玖九拾十佰百仟千萬万零|(0-9)]{1,}章").Value.Replace("第", "").Replace("章", "").ConverToDigit())
                               };

            //对联接的路径深度进行排序
            allAElements = allAElements.OrderBy(p => p.Url.Segments.Length);

            var groupAElements = (from p in allAElements
                                  group p by
                                    new
                                    {
                                        路径深度 = p.Url.Segments.Length,
                                        基本路径字符串 = string.Join("", new Func<string[]>(() =>
                                        {
                                            var templist = p.Url.Segments.ToList();
                                            templist.RemoveAt(templist.Count - 1);
                                            return templist.ToArray();
                                        }).Invoke()),

                                    }).OrderByDescending(p => p.Count())

                                 ;

            //当前分组对像的复制版本
            var tempElements = groupAElements.ToList();
            //清空对像 
            tempElements.Clear();

            ///如果有数据出现
            if (groupAElements.Count() > 2)
            {
                //如果第一重类型的数据是第二重类型数据的数倍之多则取第一种类型的数据
                if (groupAElements.ElementAt(0).Count() / groupAElements.ElementAt(1).Count() >= 3)
                {
                    //只取第一次的数据
                    tempElements.Add((groupAElements.ElementAt(0)));
                }
                else
                {
                    for (int n = 1; n < groupAElements.Count(); n++)
                    {
                        //这里可能传奇引发n+1超出范围的索引异常
                        if (groupAElements.Count() > n + 1)
                        {

                            //如果第一重类型的数据是第二重类型数据的数倍之多则取第一种类型的数据
                            if (groupAElements.ElementAt(n).Count() / groupAElements.ElementAt(n + 1).Count() >= 3)
                            {
                                //取得数据后直接返回
                                tempElements.Add((groupAElements.ElementAt(n)));
                                break;
                            }

                        }
                    }
                }
            }

            //如果找到了a href 分类列表集合
            if (tempElements.Count > 0)
            {



                //给内容页面集合传值
                var tempListPageContentUrls = tempElements.ElementAt(0).ToList();

                #region 过滤无用的数据只留下列表
                int kcount = tempListPageContentUrls.Count;
                //去掉列面前面的一些不用的数据
                for (int k = 0; k < kcount; k++)
                {
                    //第一章以前的联接全部删除
                    if (tempListPageContentUrls[0].Title.Trim().Contains("第一章") || tempListPageContentUrls[0].Title.Trim().Contains("第1章"))
                    {
                        k = 9999999;
                        break;
                    }
                    else
                    {
                        tempListPageContentUrls.RemoveAt(0);
                    }
                }
                //
                for (int k = 0; k < kcount; k++)
                {
                    //第一章以前的联接全部删除
                    if (tempListPageContentUrls.Last().index > 0)
                    {
                        k = 9999999;
                        break;
                    }
                    else
                    {
                        //删除最后的数据
                        tempListPageContentUrls.Remove(tempListPageContentUrls.Last());
                    }
                }

                #endregion

                ListPageContentUrls = tempListPageContentUrls;



                #region 各种排序方法  这个方法不完善无法实现得到正确的文章顺序
                ////如果可以作文章的ID来排序则用ID来排序
                //var noIdsurl = ListPageContentUrls.Where(p => p.index == 0);
                //int pagesCount = noIdsurl.Count();

                //if (pagesCount <= 1)
                //{
                //    #region 章节编号排序，一般不会出现问题只有当为0的index 大于2以上才进行以下排序
                //    ListPageContentUrls = ListPageContentUrls.OrderBy(p => p.index).ToList();
                //    #endregion



                //}
                //else
                //{

                //    #region 如果索引为0的数据过大则 开始查找最第一章 和最后一章节 既索引最大的那个章节 中间差的用数字填起来

                //    //初始化一个类集合
                //    var results = (from p in noIdsurl select new { page = p, index = 0, IsIndex = false }).ToList();

                //    //选进行排序
                //    var descReoceds = ListPageContentUrls.OrderByDescending(p => p.index);
                //    //得到最大章节与最小章节
                //    int min = ListPageContentUrls.Min(p => p.index);
                //    //取值
                //    ListPageContentUrl maxItem = descReoceds.ElementAt(0);

                //    //清空变量用于下面的添加匿名类型
                //    results.Clear();

                //    //同一个索引可能出现多个
                //    System.Collections.Generic.Dictionary<int, List<ListPageContentUrl>> dic = new Dictionary<int, List<ListPageContentUrl>>();
                //    //得到索引产生的字典
                //    ListPageContentUrls.Where(p => p.index != 0).ToList().ForEach(
                //            (x) =>
                //            {
                //                //如果不包含就直接添加
                //                if (!dic.ContainsKey(x.index))
                //                {
                //                    dic.Add(x.index, new List<ListPageContentUrl>() { x });
                //                }
                //                else
                //                {
                //                    //添加到集合中
                //                    dic[x.index].Add(x);
                //                }
                //            }

                //        );

                //    //对章节进行对号入坐
                //    for (int i = min; i <= maxItem.index; i++)
                //    {
                //        if (dic.ContainsKey(i))
                //        {

                //            results.Add(new { page = dic[i][0], index = i, IsIndex = true });
                //        }
                //        else
                //        {
                //            results.Add(new { page = new ListPageContentUrl(), index = i, IsIndex = false });
                //        }
                //    }



                //    #endregion

                //    #region 章节与目录列的项对号入坐后对所有索引0的项进行处理



                //    #endregion




                //    #region 页面ID排序 有可能会出现问题
                //    Func<ListPageContentUrl, bool> func = p =>
                //    {

                //        string last = p.Url.Segments.Last();
                //        decimal o;
                //        if (last.IndexOf(".") != -1)
                //        {
                //            last = last.Substring(0, last.Length - last.IndexOf("."));

                //            return decimal.TryParse(last, out o);
                //        }

                //        return true;
                //    };

                //    Func<ListPageContentUrl, double> funx = p =>
                //    {

                //        string last = p.Url.Segments.Last();
                //        double o;
                //        if (last.IndexOf(".") != -1)
                //        {
                //            last = last.Substring(0, last.Length - last.IndexOf("."));

                //            double.TryParse(last, out o);
                //            return o;
                //        }

                //        return -1;
                //    };


                //    #endregion

                //    //如果可以作文章的ID来排序则用ID来排序
                //    pagesCount = ListPageContentUrls.Where(func).Count();
                //    //如果可以用ID排序
                //    if (pagesCount <= ListPageContentUrls.Count - 1)
                //    {
                //        ListPageContentUrls = ListPageContentUrls.OrderBy(p => funx(p)).ToList();
                //    }
                //    //默认排序
                //}
                #endregion



            }
            #endregion


        }

        /// <summary>
        /// 找到最近的一个 包括 最多 a 的
        /// </summary>
        /// <param name="currentXElement"></param>
        /// <param name="PossiblyResultElements">可以的结果，找到级别最数最大并且包括a原素最多的Href</param>
        /// <param name="LayerIndex">层次 数字越大，层次越深</param>
        /// <param name="CurrentPossiblyResultElement">当前结果对像用于向下传递</param>
        public static void AnalyseMaxATagNearest(HtmlNode currentXElement, List<PossiblyResultElement> PossiblyResultElements, int LayerIndex, PossiblyResultElement CurrentPossiblyResultElement)
        {
            //要查找的原素名
            string tagname = "a";

            //对子原素进行循环
            //如果有子原素将一直循环到最后一层
            foreach (HtmlNode el in currentXElement.ChildNodes)
            {
                PossiblyResultElement reslut = new PossiblyResultElement()
                {
                    LayerIndex = LayerIndex,
                    // CurrentElement = el,
                    CurrnetHtmlElement = el,
                    ParentPossiblyResult = CurrentPossiblyResultElement,
                };
                //添加到集合中
                PossiblyResultElements.Add(reslut);
                #region 开始查找 指定标签的 a 原素
                if (el.Name == tagname)
                {
                    //如果找到了 指定标识则+1
                    reslut.ContainTagNum++;
                    continue;
                }
                #endregion
                LayerIndex = LayerIndex + 1;
                AnalyseMaxATagNearest(el, PossiblyResultElements, LayerIndex, reslut);
            }
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        public object GetResult()
        {
            return _ListPageContentUrls;
        }
        #endregion



    }



    /// <summary>
    /// 可能的结果类
    /// </summary>
    public class PossiblyResultElement
    {

        /// <summary>
        /// XElemnt 原素
        /// </summary>
        //public XElement CurrentElement { get; set; }

        /// <summary>
        /// HTML原素
        /// </summary>
        public HtmlNode CurrnetHtmlElement { get; set; }

        /// <summary>
        /// 层次 数字越大，层次越深
        /// </summary>
        public int LayerIndex { get; set; }

        /// <summary>
        /// 此原素包含原素的个数
        /// </summary>
        private int _ContainTagNum = 0;
        /// <summary>
        /// 此原素包含原素的个数 设置时将在上父对像上+指定的值
        /// </summary>
        public int ContainTagNum
        {
            get { return _ContainTagNum; }
            set
            {
                _ContainTagNum = value;
                if (ParentPossiblyResult != null)
                {
                    ParentPossiblyResult.ContainTagNum += value;
                }
            }
        }

        /// <summary>
        /// 父结果对像
        /// </summary>
        public PossiblyResultElement ParentPossiblyResult
        {
            get;
            set;
        }


    }

    /// <summary>
    /// 索引页面包括的内容页面url说明对像
    /// </summary>
    public class ListPageContentUrl
    {
        public Uri Url { get; set; }

        /// <summary>
        /// 说明用的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 当前章节转换成的索引
        /// </summary>
        public int index { get; set; }

        public override string ToString()
        {
            return "" + index + "  " + Title + "  " + Url;
        }

        /// <summary>
        /// 有可能是多个章节合并起来的
        /// </summary>
        private List<int> _Indexs = new List<int>();

        public List<int> Indexs
        {
            get { return _Indexs; }
            set
            {
                _Indexs = value;
                //重新指定索引
                if (_Indexs.Count >= 2)
                {
                    //如果是正常的章节
                    if (Math.Abs(_Indexs[0] - _Indexs[1]) == 1)
                    {
                        //指定索引
                        index = _Indexs[0];
                    }
                }
            }
        }

    }
}