using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;
using Skybot.Cache;
using Skybot.Tong;
public partial class file_MasterPage : System.Web.UI.MasterPage
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

        BindNav();

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
            // System.IO.File.WriteAllText(Server.MapPath("./index.htm"), sw.ToString());
        }

        //输出页面
        writer.Write(sw.ToString());

        sw.Dispose();
        hw.Dispose();
    }


}

