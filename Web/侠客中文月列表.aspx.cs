using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Skybot.Collections;
using Skybot.Collections.Analyse;
public partial class 侠客中文月列表 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        获取中文更新();
    }

    void 获取中文更新()
    {
        //书名集合
        List<string> BookNames = new List<string>();

        HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
       // dom.LoadHtml("http://www.xkzw.org/xkph_2.htm".GetWeb());
        dom.LoadHtml( listdiv.InnerHtml);

        //可能的原素
        List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();


        //开始循环子原素
        SingleListPageAnalyse.AnalyseMaxATagNearest(dom.DocumentNode, possiblyResultElements, 0, new PossiblyResultElement()
        {
            ParentPossiblyResult = null,
            CurrnetHtmlElement = dom.DocumentNode,
            LayerIndex = -1,
            ContainTagNum = 0
        });

        //移除第一个 ul 原素
        var removeitem = from title in possiblyResultElements
                         where title.CurrnetHtmlElement.Name == "li"
                         select title;
        //移除
        possiblyResultElements.Remove(removeitem.ElementAt(0));


        //计算当前所有HTML原素中的tr原素
        var PageTrElements = from tr in possiblyResultElements
                             where tr.CurrnetHtmlElement.Name == "li"
                             select tr;

        List<Skybot.Collections.Sites.BookInfo86zw_com> list = new List<Skybot.Collections.Sites.BookInfo86zw_com>();

        using (TygModel.Entities tygdb = new TygModel.Entities())
        {
            var books = tygdb.书名表.ToLookup(p => p.书名.Replace("》", "").Replace("《", "").Trim() + "|" + p.作者名称);
            //填类
            foreach (var item in PageTrElements)
            {
                if (item.CurrnetHtmlElement.HasChildNodes)
                {

                    //span class="fl">[东方玄幻]</span> 
                    //<span class="sm"><a href="/xkzw3226/" target="_blank">
                    //一等家丁</a></span>
                    //<span class="zj"><a href="/xkzw3226/5162956.html" title="第一五七三章 药水"
                    //target="_blank">第一五七三章 药水</a></span> 
                    //<span class="zz">纯情犀利哥</span> <span class="zs">
                    //1608193</span> 
                    //<span class="sj">2013-05-12</span> <span class="zt">连载</span>

                    var els = item.CurrnetHtmlElement.SelectNodes("span");

                    Skybot.Collections.Sites.BookInfo86zw_com bookITEM = new Skybot.Collections.Sites.BookInfo86zw_com();
                    bookITEM.类别 = els[0].InnerText.Replace("[", "").Replace("]", "").Trim();
                    bookITEM.小说名称 = els[1].Element("a").InnerText.Replace("\r\n", "").Trim();
                    bookITEM.小说目录URL = "http://www.xkzw.org/" + els[1].Element("a").Attributes["href"].Value;
                    bookITEM.最新章节 = els[2].InnerText;
                    bookITEM.作者 = els[3].InnerText;
                    bookITEM.更新 = DateTime.Now.ToString();
                    bookITEM.采集URL = bookITEM.小说目录URL;
                    bookITEM.状态 = els[6].InnerText;
                    bookITEM.小说简介URL = null;
                    list.Add(bookITEM);
                }
            }



            //更新或者是添加书
            foreach (var item in list)
            {
                string key = item.小说名称 + "|" + item.作者;
                var query = tygdb.书名表.Where(p => p.书名.Replace("》", "").Replace("《", "").Trim() + "|" + p.作者名称 == key);
                if (query.Count() > 0)
                {
                    foreach (var bookItem in query)
                    {
                        bookItem.最后更新时间 = DateTime.Now;
                    }
                }
                else
                {
                    var bok = item.Convert();

                    //添加记录
                     Skybot.Cache.RecordsCacheManager.Instance.Tygdb.AddTo书名表(bok);
                }

            }

            tygdb.SaveChanges();

            tygdb.Connection.Close();
            tygdb.Dispose();
            Skybot.Cache.RecordsCacheManager.Instance.Tygdb.SaveChanges();
        }

       




    }

}