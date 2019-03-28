using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 86zw 书信息
    /// </summary>
    public class BookInfo86zw_com : Skybot.Collections.Sites.AbstractBookInfo
    {

        public string 更新 { get; set; }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <returns></returns>
        public override TygModel.书名表 Convert()
        {
            DateTime updateTime = DateTime.Parse("2000-01-01");


            //看看分类表里有没有这个分类如果没有则进行分类添加


            //分类表
            var classItems = Skybot.Cache.RecordsCacheManager.Instance.Tygdb.分类表.Where(p => p.分类名称.Trim() == 类别.Trim());
            //当前分类
            TygModel.分类表 classItem = null;
            //如果分类不存在
            if (classItems.Count() == 0)
            {
                try
                {
                    classItem = new TygModel.分类表()
                    {
                        分类标识 = 类别.Trim(),
                        分类名称 = 类别.Trim(),
                        分类说明 = 类别.Trim(),
                        备注 = "来自 86zw.com",
                        通用分类 = 类别.Trim()

                    };
                    Skybot.Cache.RecordsCacheManager.Instance.Tygdb.AddTo分类表(classItem);
                    //保存分类
                    Skybot.Cache.RecordsCacheManager.Instance.Tygdb.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
                }
            }
            else
            {
                classItem = classItems.FirstOrDefault();
            }





            TygModel.书名表 book = new TygModel.书名表()
            {
                分类表 = classItem,
                分类标识 = classItem.分类标识,
                分类表ID = classItem.ID,
                GUID = Guid.NewGuid(),
                采集用的URL1 = 小说目录URL,
                采集用的URL2 = 小说简介URL,
                创建时间 = DateTime.Now,
                最新章节 = 最新章节,
                作者名称 = 作者,
                说明 = "",
                书名 = 小说名称.Replace("》", "").Replace("《", ""),
                最后更新时间 = updateTime,
                完本 = 状态.Trim() == "完结" ? true : false,
                配图 = "/images/noimg.gif",
            };
            //修改配图或者说明

            if (小说简介URL != null)
            {

                System.Diagnostics.Debug.WriteLine("获取配图：" + 小说简介URL);
                //初始化一个DOM
                HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
                dom.LoadHtml(小说简介URL.GetWeb());

                //信息说明字段
                HtmlAgilityPack.HtmlNode DesriptionContent = dom.GetElementbyId("CrbtrTop");
                //采集时间的xpath表达式 2011-11-18
                HtmlAgilityPack.HtmlNode node = dom.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[2]/div[3]/div[2]/div[1]/ul[1]/li[6]");
                if (node != null)
                {
                    if (DateTime.TryParse(node.InnerText, out updateTime))
                    {
                        book.最后更新时间 = updateTime;
                    }
                }
                //说明
                HtmlAgilityPack.HtmlNode summary = dom.GetElementbyId("CrbsSum");

                if (summary != null)
                {


                    book.说明 = summary.InnerHtml.Length > 4000 ?new Tong.TongUse().ForMatText(summary.InnerText,0,3800) : summary.InnerHtml;

                }

                //图片
                HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("CrbtlBookImg");

                if (listContent != null)
                {
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
                    //计算当前所有HTML原素中的img原素
                    var PageimgElements = from img in possiblyResultElements
                                          where img.CurrnetHtmlElement.Name == "img"
                                          select img;

                    try
                    {
                        if (PageimgElements.Count() > 0)
                        {
                            string imgurl = PageimgElements.First().CurrnetHtmlElement.Attributes["src"].Value;
                            if (!imgurl.ToLower().Contains("images/noimg.gif"))

                                try
                                {
                                    imgurl = new Uri(new Uri(小说简介URL), imgurl).ToString();
                                }
                                catch(Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                                    imgurl = "/images/noimg.gif";
                                }

                            System.Diagnostics.Debug.WriteLine("获取配图：" + 小说简介URL + "  完成" + imgurl);

                            book.配图 = imgurl.Trim().Contains("/images/noimg.gif") ? "/images/noimg.gif" : imgurl;
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                    }

                }
            }



            return book;
        }
    }
}
