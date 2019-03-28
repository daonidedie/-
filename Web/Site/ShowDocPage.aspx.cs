using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;

using Skybot.Cache;
public partial class Site_ShowDocPage : System.Web.UI.Page
{
    /// <summary>
    /// 是不是创建HTML静态页面
    /// </summary>
    public bool IsCresteHTMLPage;
    /// <summary>
    /// 图书名称
    /// </summary>
    public string BookName = "";
    /// <summary>
    /// 通用的数据
    /// </summary>
    public Skybot.Tong.TongUse textData = new Skybot.Tong.TongUse();
    /// <summary>
    /// 创建者
    /// </summary>
    public string Creater = "";

    /// <summary>
    /// 章节名
    /// </summary>
    public string DocName = "";
    /// <summary>
    /// 内容 
    /// </summary>
    public string Content = "";

    public string 上一页 = "";

    public string 下一页 = "";

    public string 上一章 = "";
    public string 下一章 = "";

    public string 书id = "";

    public string href = "<a href='?guid={0}'>{1}</a>";



    /// <summary>
    /// 数据库访问对像
    /// </summary>
    TygModel.Entities Tygdb = new Entities();
    /// <summary>
    /// 当前文档
    /// </summary>
    public 文章表 currentDoc = null;
    /// <summary>
    /// 书名表
    /// </summary>
    public 书名表 currentBook = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        List<文章表> list = new List<文章表>();
        string guid = Request.QueryString["guid"];
        string idstr = guid;
        bool.TryParse(Request.QueryString["html"], out IsCresteHTMLPage);
        if (!string.IsNullOrEmpty(guid))
        {
            decimal id = 0;
            if (decimal.TryParse(idstr, out id))
            {
                list = Tygdb.文章表.Where(p => p.ID == id).ToList();
            }
            else
            {
                Guid guidx = Guid.Parse(guid);

                list = Tygdb.文章表.Where(p => p.本记录GUID == guidx).ToList();
            }
            if (list.Count > 0)
            {
                BookName = list[0].书名;
                Creater = list[0].书名表.作者名称;
                DocName = list[0].章节名;
                Content = list[0].内容;

                书id = list[0].GUID.ToString();

                //替换86zw url 
                Content = Content.Replace("<img src=\"http://", "<img src=\"/Site/PicProxy.ashx?guid=" + list[0].本记录GUID + "&u=" + Server.UrlEncode(list[0].采集用的URL1) + "&url=http://");

                //上一章节
                var perRecords = GetDocByGuid(list[0].上一章.Value);
                上一章 = list[0].上一章.Value.ToString();
                //下一章节
                var nextRecords = GetDocByGuid(list[0].下一章.Value);
                下一章 = list[0].下一章.Value.ToString();
                上一页 = perRecords.Count() == 0 ? "" : string.Format(href, list[0].上一章, "上一章 " + perRecords.ElementAt(0).章节名);

                下一页 = nextRecords.Count() == 0 ? "" : string.Format(href, list[0].下一章, "下一章 " + nextRecords.ElementAt(0).章节名);
                list[0].最后访问时间 = DateTime.Now;
                list[0].总访问次数++;
                currentDoc = list[0];
                currentBook = currentDoc.书名表;

                //生成静态页面
                if (IsCresteHTMLPage)
                {
                    href = "<a href='{0}'>{1}</a>";
                    if (perRecords.Count() > 0)
                    {
                        var per = perRecords.ElementAt(0);
                        上一页 = perRecords.Count() == 0 ? "" : string.Format(href, per.GetHTMLFilePath(), "上一章 " + per.章节名);
                        上一章 = per.ID + ".html";
                    }
                    else
                    {
                        上一章 = "?";
                        上一页 = string.Empty;

                    }
                    if (nextRecords.Count() > 0)
                    {

                        var next = nextRecords.ElementAt(0);
                        下一页 = nextRecords.Count() == 0 ? "" : string.Format(href, next.GetHTMLFilePath(), "下一章 " + next.章节名);
                        下一章 = next.ID + ".html";
                    }
                    else
                    {
                        下一页 = "?";
                        下一章 = string.Empty;
                    }
                }

            }
        }






    }

    /// <summary>
    /// 内容状态保存时发生
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Page_SaveStateComplete(object sender, EventArgs e)
    {
        //释放资源
        Tygdb.Dispose();
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
            if (currentDoc != null)
            {
                currentDoc.CreateStaticHTMLFile(sw.ToString());
            }
        }
        //输出页面
        writer.Write(sw.ToString());

        sw.Dispose();
        hw.Dispose();

    }

    /// <summary>
    /// 传入GUID得到文章表
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public IEnumerable<文章表> GetDocByGuid(Guid guid)
    {
        if (guid == null)
        {
            return new List<文章表>();
        }
        return Tygdb.文章表.Where(p => p.本记录GUID == guid);
    }

}