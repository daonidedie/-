using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Skybot.Collections;
using Skybot.Collections.Analyse;
/// <summary>
/// 获取小说明50排名书本并更新
/// </summary>
public partial class 百度风云榜_小说前50 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // --  --update  [Tyg].[dbo].[书名表] set 采集用的URL1='http://www.86zw.com/Html/Book/32/32600/Index.shtml'  where [书名]='永生'
        //书名集合
        List<string> BookNames = new List<string>();

        HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
        dom.LoadHtml("http://top.baidu.com/buzz.php?p=book".GetWeb());


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


        //计算当前所有HTML原素中的tr原素
        var PageTrElements = from tr in possiblyResultElements
                             where tr.CurrnetHtmlElement.Name == "tr"
                             select tr;

        //填类
        foreach (var item in PageTrElements)
        {
            if (item.CurrnetHtmlElement.HasChildNodes)
            {
                var els = item.CurrnetHtmlElement.ChildNodes.Where(p => p.HasChildNodes);

                double x;
                if (els.ElementAt(0).Name == "th" && double.TryParse(els.ElementAt(0).InnerText, out x))
                {
                    BookNames.Add(els.ElementAt(1).InnerText);
                }
            }
        }


        TygModel.Entities tntity = new TygModel.Entities();
        tntity.CommandTimeout = 60 * 100;
        //找到前50的小说并对数据库记录进行更新
        //更新所有小说的书名
        foreach (var k in tntity.书名表)
        {
            if (BookNames.Contains(k.书名.Trim()))
            {
                k.最后更新时间 = DateTime.Now;
            }
            //《时空玄仙》
           // k.书名 = k.书名.Replace("》", "").Replace("《", "");
        }

        //提交更新
        tntity.SaveChanges();
        tntity.Dispose();

    }

    
}