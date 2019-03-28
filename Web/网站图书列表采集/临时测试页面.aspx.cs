using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Skybot.Collections;

public partial class 网站图书列表采集_临时测试页面 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //初始化一个DOM
       // HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
       // dom.LoadHtml("http://www.86zw.com/Book/33839/Index.aspx".GetWeb());
        //更新所有文章的子章节状态 0 表示没有有效的章节
        TygModel.Entities enti = new TygModel.Entities();
        var dd = enti.书名表.ToList();
        dd = dd.Where(p => p.包含有效章节 == null || p.包含有效章节 == 0).ToList();
        for (int k = 0; k < dd.Count(); k++)
        {
           dd.ElementAt(k).包含有效章节 =dd.ElementAt(k).文章表.Count;
            enti.SaveChanges();
        }

        
    }
}