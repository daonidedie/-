using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 书信息
    /// </summary>
    public class BookInfoYankuaikan_com : Skybot.Collections.Sites.AbstractBookInfo
    {

        public string 字数 { get; set; }
        public string 更新 { get; set; }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <returns></returns>
        public override TygModel.书名表 Convert()
        {
            DateTime updateTime;
            DateTime.TryParse(更新, out updateTime);
            TygModel.书名表 book = null;
            //看看分类表里有没有这个分类如果没有则进行分类添加

            using (TygModel.Entities tygdb = new TygModel.Entities())
            {
                //分类表
                var classItems = tygdb.分类表.Where(p => p.分类名称.Trim() == 类别.Trim());
                //当前分类
                TygModel.分类表 classItem = null;
                //如果分类不存在
                if (classItems.Count() == 0)
                {
                    classItem = new TygModel.分类表()
                    {
                        分类标识 = 类别.Trim(),
                        分类名称 = 类别.Trim(),
                        分类说明 = 类别.Trim(),
                        备注 = "来自 yankuaikan.com",
                        通用分类 = 类别.Trim()

                    };
                    tygdb.AddTo分类表(classItem);
                    //保存分类
                    tygdb.SaveChanges();
                }
                else
                {
                    classItem = classItems.FirstOrDefault();
                }


                 book = new TygModel.书名表()
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
                    完本 = 状态.Trim() == "完成" ? true : false,
                    配图 = new Func<string>(() =>
                    {
                        if (小说简介URL != null)
                        {
                            //初始化一个DOM
                            HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
                            dom.LoadHtml(小说简介URL.GetWeb());

                            //内容
                            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("content");

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
                                                  where img.CurrnetHtmlElement.Name == "a" && img.CurrnetHtmlElement.HasChildNodes && img.CurrnetHtmlElement.ChildNodes.Where(p => p.Name == "img").Count() > 0
                                                  select img;

                            try
                            {
                                if (PageimgElements.Count() > 0)
                                {
                                    //img.CurrnetHtmlElement.Attributes["src"].Value.Contains("http://tu.yankuai.com") && img.CurrnetHtmlElement.Attributes["src"] != null 

                                    string imgurl = PageimgElements.First().CurrnetHtmlElement.ChildNodes[0].Attributes["src"].Value;
                                    return imgurl.Trim().Contains("http://tu.yankuai.com") ? imgurl : "/images/noimg.jpg";
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(DateTime.Now + ex.Message + "|||||" + ex.StackTrace);
                            }

                        }
                        //找到文章目录
                        return "/images/noimg.jpg";
                    }).Invoke(),
                };
            }
            return book;
        }
    }
}
