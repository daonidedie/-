using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;
using Skybot.Cache;
using Skybot.Tong;
using System.Net;
using Skybot.Tong.CodeCharSet;
public partial class file_BookList : System.Web.UI.Page
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
    ///需要更新
    /// </summary>
    protected bool NeedUpdate = false;
    /// <summary>
    /// 类型名
    /// </summary>
    public string TypeName = string.Empty;
    /// <summary>
    /// 数据库访问对像
    /// </summary>
    TygModel.Entities Tygdb = new Entities();

    /// <summary>
    /// 书数据列表
    /// </summary>
    public List<BookListItem> bookList = new List<BookListItem>();
    /// <summary>
    /// 当前操作的书数据
    /// </summary>
    public IEnumerable<书名表> list = new List<书名表>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["html"]))
        {
            bool.TryParse(Request.QueryString["html"], out IsCresteHTMLPage);
        }

        ShowNews.EnableViewState = false;
        TypeName = "玄幻,修真,都市,穿越,网游,科幻";
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
    /// <summary>
    /// 检查文件是不是存在
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string ImgIsOK(string url, 书名表 Currentbook)
    {
        //  return url;
        if (url.Contains("86zw"))
        {
            string filename = "/images/BookImgs/" + Guid.NewGuid() + System.IO.Path.GetExtension(url);
            WebClient wc = new WebClient();
            try
            {

                wc.DownloadFile(url, Server.MapPath(filename));
                Currentbook.配图 = filename;
                NeedUpdate = true;
            }
            catch
            {
                //修改配图
                Currentbook.配图 = "/Images/Noimg.jpg";
                NeedUpdate = true;
                return Currentbook.配图;

            }
            wc.Dispose();
        }
        return url;
    }


    ///<summary>
    /// 帮定分页数据
    ///</summary>
    ///
    void BindData(string key = null)
    {
        //    try
        //    {

        IQueryable<书名表> ListSource = null;
        string 分类标识 = string.Empty;
        key = GetKeyWord();
        if (key == null)
        {
            if (GetBooksType() != "-3")
            {
                decimal lx = 0;
                if (decimal.TryParse(GetBooksType(), out lx))
                {

                    //如果是分类
                    if (lx > 0)
                    {
                        ListSource = Tygdb.书名表.Where(p => p.分类表ID == lx && p.最新章节 != null && p.包含有效章节 != null && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);

                        TypeName = ListSource.Count() > 0 ? ListSource.FirstOrDefault().分类标识 : string.Empty;
                    }
                    //完本
                    if (lx == -1)
                    {
                        TypeName = "完本";
                        ListSource = Tygdb.书名表.Where(p => p.完本 && p.包含有效章节 > 0).OrderByDescending(p => p.最后更新时间);
                    }
                    //连载
                    if (lx == -2)
                    {
                        TypeName = "连载";
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

            ListSource = Tygdb.书名表.Where(p => p.书名.Contains(key) || p.作者名称.Contains(key)).OrderByDescending(p => p.最后更新时间);
            //AspNetPager1.CurrentPageIndex = 1;

        }
        list = ListSource.Skip(AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1)).Take(AspNetPager1.PageSize);
        bookList = (from p in list
                    select new BookListItem()
                    {
                        书名 = p.书名
                    }).ToList();
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
        //如果没有搜索的时候
        if (string.IsNullOrEmpty(GetKeyWord()))
        {
            AspNetPager1.UrlRewritePattern = "/Book/" + TypeName.ToPingYing() + "/" + GetBooksType() + "/{0}.aspx";
        }
        else
        {
            //有搜索关键字的时候
            AspNetPager1.UrlRewritePattern = "/Search/" + key.ToPingYing() + "/" + Server.UrlEncode(key) + "/{0}.aspx";

        }
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
    /// 得到当前分类
    /// </summary>
    /// <returns>得到当前分类 -3 所有类型</returns>
    public string GetBooksType()
    {
        string lx = Request.QueryString["lx"];
        //url重写类型
        string urlRewriteLx = RouteData.Values["type"] as string;
        string temp = lx == null && urlRewriteLx != null ? urlRewriteLx : null;
        temp = string.IsNullOrEmpty(lx) ? temp : lx;
        temp = temp == null ? "-3" : temp;
        return temp;
    }

    public string GetPage()
    {
        string page = Request.QueryString["page"];
        //url重写类型
        string urlRewritePage = RouteData.Values["page"] as string;

        string temp = page == null && urlRewritePage != null ? urlRewritePage : null;
        temp = string.IsNullOrEmpty(page) ? temp : page;
        temp = temp == null ? "1" : temp;
        temp = temp == "index" ? "1" : temp;
        return temp;
    }

    /// <summary>
    /// 获得搜索关键字
    /// </summary>
    /// <returns>返回数据如果没有则返回1</returns>
    public string GetKeyWord()
    {
        string keyword = Request["keyword"];
        //url重写类型
        string urlRewriteKeyword = RouteData.Values["keyword"] as string;
        string temp = keyword == null && urlRewriteKeyword != null ? urlRewriteKeyword : null;
        temp = string.IsNullOrEmpty(keyword) ? temp : keyword;
        temp = temp == null ? null : temp;
        return temp;
    }



    /// <summary>
    /// 内容状态保存时发生
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Page_SaveStateComplete(object sender, EventArgs e)
    {
        if (NeedUpdate)
        {
            Tygdb.SaveChanges();
        }
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
        //url重写时指定URL
        //获得page
        AspNetPager1.CurrentPageIndex = int.Parse(GetPage());

        BindData(GetKeyWord());
    }



}