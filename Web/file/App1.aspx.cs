using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;
using Skybot.Cache;
using Skybot.Tong;
public partial class file_App1 : System.Web.UI.Page
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

    public List<书名表> list = new List<书名表>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["html"]))
        {
            bool.TryParse(Request.QueryString["html"], out IsCresteHTMLPage);
        }
        if (!IsPostBack)
        {
            BindData();
        }

    }

    ///<summary>
    /// 帮定分页数据
    ///</summary>
    ///
    void BindData(string key = null)
    {

        IEnumerable<书名表> ListSource = new List<书名表>();




        ListSource = Tygdb.书名表.Where(p => p.包含有效章节 != null && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);
        //ShowNews.DataSource = ListSource;
        //ShowNews.DataBind();
        //每一个分类取最新的一条记录
        var docs = Tygdb.ExecuteStoreQuery<NewDocItem>(@"
select d.ID,d.本记录GUID,d.GUID,d.书名 into #doc  from (
  select top 100 [GUID] from 书名表 where 包含有效章节 > 0 and 书名表.完本='false'  order by 最后更新时间 desc 
 ) as
 doctype
 left join 
 (
    SELECT *  from  文章表 
 )
 d
 on d.[GUID]=doctype.[GUID] 
select * from (select *,row=row_number()over(partition by [GUID] order by ID desc) from #doc)t where row=1 and t.id>0
drop table #doc ");

        decimal[] ids = docs.Select(p => p.ID).ToArray();
        //使用了in
        var topDocs = Tygdb.文章表.Where(p => ids.Contains(p.ID)).OrderByDescending(p => p.ID).ToList().Where(
            //确保章节是有效的
            p => string.Join("", System.Text.RegularExpressions.Regex.Matches(p.内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                        .Cast<System.Text.RegularExpressions.Match>().Select(x => x.Value).ToArray()
                        ).Length > 200
            );

        Docs1.DataSource = topDocs.Take(6);
        Docs1.DataBind();
        Repeater1.DataSource = topDocs.Skip(6 * 1).Take(6);
        Repeater1.DataBind();

        Repeater2.DataSource = topDocs.Skip(6 * 2).Take(6);
        Repeater2.DataBind();

        Repeater3.DataSource = topDocs.Skip(6 * 3).Take(6);
        Repeater3.DataBind();

        Repeater4.DataSource = topDocs.Skip(6 * 4).Take(6);
        Repeater4.DataBind();

        Repeater5.DataSource = topDocs.Skip(6 * 5).Take(6);
        Repeater5.DataBind();


        BindNav();
        BindTopBook();
    }

    /// <summary>
    /// 导航
    /// </summary>
    void BindNav()
    {
        List<string> lxs = new List<string>(){


"东方玄幻",

"武侠仙侠",

"都市言情",

"历史军事",

"科幻灵异",

"网游竞技",



        };
        Nav.DataSource = Tygdb.分类表.ToList().Take(7)
            //.Where(p => lxs.Contains(p.分类标识))
            ;
        Nav.DataBind();


    }
    /// <summary>
    /// 推荐的四本书
    /// </summary>
    void BindTopBook()
    {
        Random dom = new Random(DateTime.Now.Millisecond);
        int num = dom.Next(0, 50 - 4);
        List<书名表> ListSource = new List<书名表>();
        ListSource = Tygdb.书名表.Where(p => p.包含有效章节 != null && !p.完本 && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间)
           .Skip(num)
            .Take(4).ToList();

        TopBooks.DataSource = list = ListSource;
        TopBooks.DataBind();
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

    ///<summary>
    ///分页代码 
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="e"></param>
    protected void dfsd(object sender, EventArgs e)
    {
        BindData();
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
            System.IO.File.WriteAllText(Server.MapPath("./index.htm"), sw.ToString());
        }

        //输出页面
        writer.Write(sw.ToString());

        sw.Dispose();
        hw.Dispose();
    }


}

