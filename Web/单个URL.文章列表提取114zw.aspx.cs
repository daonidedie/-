using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skybot.Collections.Analyse;

public partial class 单个URL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Skybot.Collections.Analyse.SingleListPageAnalyse ListPageAnalyse = new Skybot.Collections.Analyse.SingleListPageAnalyse("https://www.biquge5200.cc/76_76271/");

        //处理url
        Filter114Zw(ListPageAnalyse);

        XMLDocuentAnalyse documentAnalyse = new Skybot.Collections.Analyse.XMLDocuentAnalyse() { IndexPageUrl = new Skybot.Collections.Analyse.ListPageContentUrl() { index = 0, Title = "书名", Url = new Uri(ListPageAnalyse.IndexPageUrl) } };


        //初始化内容分析器
      var regex=  documentAnalyse.GetPathExpression(documentAnalyse.IndexPageUrl, ListPageAnalyse.ListPageContentUrls);

    }

    /// <summary>
    /// 得到114zw网的索引
    /// </summary>
    /// <param name="url">内容页面url</param>
    /// <param name="IndexUrl">列表页面url</param>
    /// <returns></returns>
    private string Get114Index(Uri url, string IndexUrl)
    {
        string filename = System.IO.Path.GetFileName(url.LocalPath);

        //得到扩展名
        string exname = System.IO.Path.GetExtension(url.LocalPath);

        return filename.Replace(exname, "");

    }

    /// <summary>
    /// 114 中文网过滤.
    /// 1.去重
    ///     如果开始出现了 后面再出现就删除后面的.
    ///     
    /// 2.url ID 排序
    /// 3.包含索引页面的url 才能正确排序  不包含索引URL的直接删除
    /// </summary>
    public void Filter114Zw(Skybot.Collections.Analyse.SingleListPageAnalyse analyse)
    {

        List<ListPageContentUrl> urls = new List<ListPageContentUrl>();

        //string x= string.Join("/", new Uri(analyse.IndexPageUrl).Segments);
 
        //包含索引页面的url
        var bookurls = analyse.ListPageContentUrls.Where(p => p.Url.ToString().Contains(analyse.IndexPageUrl.Replace("index.html","")));
        var urlsu = from p in bookurls
                    select new
                    {
                        Index = Get114Index(p.Url, analyse.IndexPageUrl),
                        page = p,

                    };
        int index = 0;
        var urlsx = (from p in urlsu
                     where int.TryParse(p.Index, out  index)
                     select new
                     {
                         Index = int.Parse(p.Index),
                         Page = p.page
                     }).ToList();

        int[] order = urlsx.Select(p => p.Index).Take(3).ToArray();
        if (order.Length > 2)
        {
            //如果第一个的索引ID大于第二个的索引ID,并且第二个索引小于第三个索引则移除第一个索引
            if (order[0] > order[1] && order[2] > order[1])
            {
                urlsx.RemoveAt(0);
            }
        }
        //处理url 内容页面列表完成
      //  urlsx.Take(3).ToList();

        analyse.ListPageContentUrls = urlsx.Select(p => p.Page).ToList();


    }




}