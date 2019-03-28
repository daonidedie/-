using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;

using Skybot.Cache;
using Skybot.Tong;
public partial class Site_BookIndex : System.Web.UI.Page
{
    /// <summary>
    /// 是不是创建HTML静态页面
    /// </summary>
    public bool IsCresteHTMLPage;

    /*
     第01步、内容页的 Page_PreInit
第02步、母版页的 Page_Init
第03步、内容页的 Page_Init
第04步、内容页的 Page_InitComplete
第05步、内容页的 Page_PreLoad
第06步、内容页的 Page_Load
第07步、母版页的 Page_Load
第08步、母版页或内容页的 按钮点击等回发事件（Master或Content的Button事件不会同时触发）
第09步、内容页的 Page_LoadComplete
第10步、内容页的 Page_PreRender
第11步、母版页的 Page_PreRender
第12步、内容页的 Page_PreRenderComplete
第13步、内容页的 Page_SaveStateComplete
第14步、母版页的 Page_Unload
第15步、内容页的 Page_Unload
     */
    /// <summary>
    /// 图书名称
    /// </summary>
    public string BookName = "";

    /// <summary>
    /// 创建者
    /// </summary>
    public string Creater = "";


    /// <summary>
    /// 最后更新时间
    /// </summary>
    public string LastTime = "";


    /// <summary>
    /// 数据库访问对像
    /// </summary>
    TygModel.Entities Tygdb = new Entities();

    /// <summary>
    /// 当前书名表
    /// </summary>
    public 书名表 currentBook = null;

    protected void Page_Load(object sender, EventArgs e)
    {




        System.Collections.Generic.IEnumerable<文章表> list = new List<文章表>();
        string guid = Request.QueryString["guid"];
        bool.TryParse(Request.QueryString["html"], out IsCresteHTMLPage);








        if (!string.IsNullOrEmpty(guid))
        {
            //susucong 生成静态页面
            if (QliliHelper.BaseSite.Contains("qlili.com") && System.Configuration.ConfigurationManager.AppSettings["启用Qlili获取数据"] != "0")
            {
                //使用url
                string result = QliliHelper.IndexBook();
                if (!string.IsNullOrEmpty(result))
                {
                    currentBook = Tygdb.书名表.Where(p => p.GUID ==Guid.Parse( guid)).FirstOrDefault();
                    Response.Write(result);
                    currentBook.CreateStaticHTMLFile(result);
                    Response.End();
                    return;
                }

            }

            list = Tygdb.文章表.Where(p => p.GUID == Guid.Parse(guid)).OrderBy(p => p.创建时间);
            if (list.Count() > 0)
            {
                var item = list.First();
                BookName = item.书名;
                Creater = item.书名表.作者名称;
                LastTime = item.书名表.最后更新时间.ToString();
                currentBook = item.书名表;
            }
            else
            {
                currentBook = Tygdb.书名表.Where(p => p.GUID == Guid.Parse(guid)).FirstOrDefault();
            }
            if (list.Count() > 2)
            {
                //循环数据
                list.Aggregate((c, n) =>
                {
                    n.上一章 = c.本记录GUID;
                    c.下一章 = n.本记录GUID;
                    return n;

                });
                currentBook.包含有效章节 = list.Count();
            }


        }

        docList.EnableViewState = false;

        //System.Threading.Tasks.Task.Factory.StartNew(delegate
        //{
        //    System.Net.WebClient wc = new System.Net.WebClient();

        //    foreach (var item in list)
        //    {
        //        string url = item.GetHTMLFilePath();
        //        if (!System.IO.File.Exists(System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + url)))
        //        {
        //            //生成静态页面
        //            string str = wc.DownloadString(new Uri(Request.Url, "BookIndex.aspx?guid=" + item.本记录GUID));

        //            item.CreateStaticHTMLFile(str);

        //        }
        //        // item.采集用的URL9 = url ? url : "BookIndex.aspx?guid=" + book.GUID;
        //    }
        //});
        docList.DataSource = list;
        docList.DataBind();



    }

    /// <summary>
    /// 内容状态保存时发生
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Page_SaveStateComplete(object sender, EventArgs e)
    {
        try
        {

            //保存更新
            Tygdb.SaveChanges();
        }
        catch { }
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
            if (currentBook != null)
            {
                currentBook.CreateStaticHTMLFile(sw.ToString());
            }
        }

        //输出页面
        writer.Write(sw.ToString());

        sw.Dispose();
        hw.Dispose();
    }


}