using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skybot.Tong;
using TygModel;

public partial class file_SiteMap : System.Web.UI.Page
{
    /// <summary>
    /// 是不是创建HTML静态页面
    /// </summary>
    public bool IsCresteHTMLPage = true;

    /// <summary>
    /// 网站通用方法 
    /// </summary>
    public TongUse textData = new TongUse();
    /// <summary>
    /// 数据库访问对像
    /// </summary>
    TygModel.Entities Tygdb = new Entities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["html"]))
        {
            bool.TryParse(Request.QueryString["html"], out IsCresteHTMLPage);
        }
        DataBind();

    }


    /// <summary>
    /// 数据邦定
    /// </summary>
    void DataBind()
    {
     ShowDocs.DataSource=  Tygdb.文章表.OrderByDescending(p => p.ID).Take(2000).Where(p => p.创建时间.Day == DateTime.Now.Day);
     ShowDocs.DataBind();
    }
    /// <summary>
    /// 输出HTML
    /// </summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer)
    {

        //将当前产生的文件写到HTML里
        System.IO.StringWriter sw = new System.IO.StringWriter();
        Html32TextWriter hw = new Html32TextWriter(sw);
        base.Render(hw);
        if (IsCresteHTMLPage)
        {
            System.IO.File.WriteAllText(Server.MapPath("./SiteMap.htm"), sw.ToString());
        }

        //输出页面
        writer.Write(sw.ToString());

        sw.Dispose();
        hw.Dispose();
    }
}