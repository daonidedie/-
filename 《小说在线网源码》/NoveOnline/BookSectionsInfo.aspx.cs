using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using Model;
public partial class BookSectionsInfo : System.Web.UI.Page
{
    INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    Model.BooksInfo booksInfo = new Model.BooksInfo();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断用户是否有阅读权限
        IDAL.INovel vn = BllFactory.BllAccess.CreateINovelBLL();
        List<Model.SectionsInfo> volumeNumber = vn.sectionsVolumeNumber(Convert.ToInt32(Request.QueryString["SectiuonId"]));

        //处理URL
        string parentURl = Request.UrlReferrer.ToString();
        int temp = parentURl.LastIndexOf("/");
        parentURl = parentURl.Substring(0, temp);
        parentURl = parentURl + "/BooksInfo.aspx?bookId=" + Request.QueryString["bookId"];
        if (volumeNumber[0].VolumeId > 1)
        {
            if (User.Identity.Name != "" && Session["NewUser"] != null)
            {
                List<UsersInfo> list = (List<UsersInfo>)Session["NewUser"];
                if (list[0].UserType < 2 || list[0].UserType > 4)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "noread", "<script type='text/javascript'>alert('对不起，您没有阅读权限，请成为本站VIP！');this.location.href='" + parentURl + "';</script>", false);
                    return;
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "noread", "<script type='text/javascript'>alert('对不起，您没有阅读权限，请成为本站VIP！');this.location.href='" + parentURl + "';</script>", false);
                return;
            }
        }


        if (!IsPostBack)
        {           

            //增加点击量
            novel.sectionClickCount(Convert.ToInt32(Request.QueryString["SectiuonId"]));

            if (Request.QueryString["Next"] == null)
                booksInfo = novel.getSectionsInfo(Convert.ToInt32(Request.QueryString["SectiuonId"]));
            else if (Request.QueryString["Next"] == "0")
            {
                booksInfo = novel.getAnteSections(Convert.ToInt32(Request.QueryString["SectiuonId"]), Convert.ToInt32(Request.QueryString["bookId"]));
                if (booksInfo == null)
                {
                    string url = "~/BooksInfo.aspx?bookId=" + Request.QueryString["bookId"];
                    Response.Redirect(url);
                }
            }
            else if (Request.QueryString["Next"] == "1")
            {
                booksInfo = novel.getNextSections(Convert.ToInt32(Request.QueryString["SectiuonId"]), Convert.ToInt32(Request.QueryString["bookId"]));
                if (booksInfo == null)
                {
                    string url = "~/BooksInfo.aspx?bookId=" + Request.QueryString["bookId"];
                    Response.Redirect(url);
                }
            }

            booksInfo.ValumeName = Common.Myweeks.changeNumber(booksInfo.ValumeName);
            lbBookName.Text = booksInfo.SectionTitle;
            lbUserName.Text =  "小说名称：" + booksInfo.BookName + "    " + "作者:" + booksInfo.UserName;
            AddTime.Text = "更新时间:" + booksInfo.AddTime.ToShortDateString();

            sectionsinfo.InnerHtml = booksInfo.Contents;

            cataLog.NavigateUrl = "~/BooksInfo.aspx?bookId=" + booksInfo.BookId;
            hlUpLeaf.NavigateUrl = "~/BookSectionsInfo.aspx?bookId=" + booksInfo.BookId + "&&SectiuonId=" + booksInfo.SectiuonId + "&&Next=0";
            lbNextLeaf.NavigateUrl = "~/BookSectionsInfo.aspx?bookId=" + booksInfo.BookId + "&&SectiuonId=" + booksInfo.SectiuonId + "&&Next=1";
        }
    }

    //获取页面风格
    protected string getColor()
    {
        if (Session["setColor"] == null)
        {
            return "#F4F4F4";
        }
        else
        {
            string Color = Session["setColor"].ToString();
            return Color;
        }
    }

    //获取字体颜色
    protected string getFont()
    {
        if (Session["FontColor"] == null)
        {
            return "black";
        }
        else
        {
            string Color = Session["FontColor"].ToString();
            return Color;
        }
    }

    //获取字体大小
    protected string getFontSize()
    {
        if (Session["FontSize"] == null)
        {
            return "14px";
        }
        else
        {
            string FontSize = Session["FontSize"].ToString();
            return FontSize;
        }
    }
}