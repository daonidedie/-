using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;
using Skybot.Cache;
public partial class Site_BookList : System.Web.UI.Page
{

    /// <summary>
    /// 是不是创建HTML静态页面
    /// </summary>
    public bool IsCresteHTMLPage = true;

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
        ShowNews.EnableViewState = false;
       
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request["keyword"]))
            {
                BindData();
            }
            else
            {
                BindData(Request["keyword"]);
            }
        }
       

    }

    ///<summary>
    /// 帮定分页数据
    ///</summary>
    ///
    void BindData(string key = null)
    {
        //    try
        //    {

        IEnumerable<书名表> ListSource = new List<书名表>();

        if (key == null)
        {
            if (Request.QueryString["lx"] != null)
            {
                decimal lx = 0;
                if (decimal.TryParse(Request.QueryString["lx"], out lx))
                {
                    //如果是分类
                    if (lx > 0)
                    {
                        ListSource = Tygdb.书名表.Where(p => p.分类表ID == lx && p.最新章节 != null && p.包含有效章节 != null && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);
                    }
                    //完本
                    if (lx == -1)
                    {
                        ListSource = Tygdb.书名表.Where(p => p.完本 && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);
                    }
                    //连载
                    if (lx == -2)
                    {
                        ListSource = Tygdb.书名表.Where(p => !p.完本 && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);
                    }
                }
            }
            else
            {

                ListSource = Tygdb.书名表.Where(p => p.最新章节 != null && p.包含有效章节 != null && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);
            }
        }
        else
        {

            ListSource = Tygdb.书名表.Where(p => p.书名.Contains(key) || p.作者名称.Contains(key)).OrderByDescending(p => p.最后更新时间).ToList();
            AspNetPager1.CurrentPageIndex = 1;

        }
        list = ListSource.Skip(AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1)).Take(AspNetPager1.PageSize).ToList();

        //foreach (var book in list)
        //{
        //    string url = book.GetHTMLFilePath();
        //    book.首发地址 = System.IO.File.Exists(System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + url)) ? url : "BookIndex.aspx?guid=" + book.GUID;
        //}
        ShowNews.DataSource = list;
        ShowNews.DataBind();
        AspNetPager1.RecordCount = ListSource.Count();
        //  AspNetPager1.UrlRewritePattern = "/aspnet/testPargrameDir/site/NewsList.aspx?CloumnID=" + CloumnID + "&SendentID=" + SendentID + "&page={0}";
        //AspNetPager1.UrlRewritePattern = "tow/List" + CloumnID + "-" + SendentID + "-{0}.aspx";
        #region  AspNetPager1 实现静态页面分页
        //AspNetPager1.UrlRewritePattern = TextThisSite.SiteGetColumnDirLink(SendentID) + "{0}.html";
        ///以下代码开始给 AspNetPager1 实现静态页面分页
        //Tong.ThisSiteUse.CreateSitePageList TextPageList = new Tong.ThisSiteUse.CreateSitePageList(SendentID, AspNetPager1.PageCount);
        //TextPageList.Start();
        #endregion

        //}
        //catch
        //{

        //}

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
        BindData(Request["keyword"]);
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        BindData(TextBox1.Text.Trim());
    }


}