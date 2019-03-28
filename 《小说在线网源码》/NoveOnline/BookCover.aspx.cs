using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

using IDAL;
public partial class BookCover : System.Web.UI.Page,IRequiresSessionState
{
    private int booId;
    private int userId = 0;
    private INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    private Model.BooksInfo item = new Model.BooksInfo();
    private Model.BooksInfo itemNumber = new Model.BooksInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        booId = Convert.ToInt32(Request.QueryString["bookId"]);

        string userId2 = HttpContext.Current.User.Identity.Name;
        
        if (userId2 != null && userId2 != "")
            userId = Convert.ToInt32(Convert.ToInt32(userId2));
        item = BllFactory.BllAccess.CreateINovelBLL().getBookCoverInfo(booId);
        if (!IsPostBack)
        {

            ibookImg.ImageUrl = @"~/Images/books/" + item.Images;
            hlBookName.Text = item.BookName;
            lbAuthor.Text = item.UserName;
            bookType.InnerHtml = item.TypeName;
            chickAll.InnerHtml = item.AllSumClick;
            updateTime.InnerHtml = item.AddTime.ToShortDateString();
            stateName.InnerHtml = item.StateName;
            contents.InnerHtml = item.BookIntroduction.Length > 300 ? item.BookIntroduction.Substring(0, 300) + "……" : item.BookIntroduction;
            keyName.InnerHtml = item.BookName + "　" + item.UserName + "　" + item.TypeName;

            replayTitle.InnerHtml = item.BookName + " - 评论列表";

            Model.BooksInfo BookFinallySection = novel.getBookFinallySection(booId);
            SectionTitle.Text = BookFinallySection.SectionTitle.Length > 15 ? BookFinallySection.SectionTitle.Substring(0, 14) + "……" : BookFinallySection.SectionTitle;
            SectionTitle.NavigateUrl = "~/BookSectionsInfo.aspx?SectiuonId=" + BookFinallySection.SectiuonId + "&&BookId=" + item.BookId;

            ibBookRead.PostBackUrl = "~/BooksInfo.aspx?BookId=" + item.BookId;

            BookReplayInfo.DataSource = novel.getBookReplayInfo(booId);
            BookReplayInfo.DataBind();

            //rpPropInfo.DataSource = novel.getUserPropInfo(booId);
            //rpPropInfo.DataBind();

            itemNumber = novel.getTicketNumber(booId);
            lbTicketPlace.Text = itemNumber.ticketNumber.ToString();
            lbTicketAmount.Text = "排名：" + itemNumber.flowerNumber.ToString();

            itemNumber = novel.getFlowerNumber(booId);
            lbFlowerPlace.Text = itemNumber.flowerNumber.ToString();
            lbFlowerAmout.Text = "排名：" + itemNumber.ticketNumber.ToString();

            itemNumber = novel.getAuthorIdBooksOne(booId);

        }

        
    }
    protected void rpPropInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label propName = e.Item.FindControl("lbPropName") as Label;
        Image propImg = e.Item.FindControl("imProp") as Image;
        if (propName.Text.Contains("鲜花"))
            propImg.ImageUrl = "~/Images/prop/xianhua.jpg";
        else if(propName.Text.Contains("书票"))
            propImg.ImageUrl = "~/Images/prop/shupiao.jpg";

    }
    protected void BookReplayInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image userImg = e.Item.FindControl("userImg") as Image;
            userImg.ImageUrl = @"~/Images/noavatar_middle.gif";
    }




    protected void readingbook_Click(object sender, ImageClickEventArgs e)
    {
        //订阅书
        int bookID = Convert.ToInt32(Request.QueryString["bookId"]);
        IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        if (HttpContext.Current.User.Identity.Name == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ok",
                "<script type='text/javascript'>alert('您还未登陆，请先登陆！');</script>", false
                );
        }
        else
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int count = novel.userAfterBook(userId, bookID);
            if (count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok",
                    "<script type='text/javascript'>alert('成功订阅该书，章节更新时将站内短消息通知您！');</script>", false
                    );
            }
            else
            {
                int userid = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                int counterr = novel.isReaddingBook(userid, bookID);
                if (counterr == -1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "buok",
                     "<script type='text/javascript'>alert('您已经订阅过该书。');</script>", false
                     );
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "buok", "<script type='text/javascript'>alert('你没有订阅连载卡，请到商城购买再订阅连载！');</script>", false);
                }
            }

        }
    }
    protected void bookshelf_Click(object sender, ImageClickEventArgs e)
    {
        //加入书架
        int bookId = Convert.ToInt32(Request.QueryString["bookId"]);
        IDAL.INovel iu = BllFactory.BllAccess.CreateINovelBLL();

        if (HttpContext.Current.User.Identity.Name == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ok",
                "<script type='text/javascript'>alert('您还未登陆，请先登陆！');</script>", false
                );
        }
        else
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int count = iu.adduserBookShelf(userId, bookId);
            if (count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok",
                    "<script type='text/javascript'>alert('已将书本加入您的书架！');</script>", false
                    );
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "buok",
                     "<script type='text/javascript'>alert('您的书架里已有该书！');</script>", false
                     );
            }
        }
    }
    protected void hlTicket_Click(object sender, ImageClickEventArgs e)
    {
        //送书票
        int bookId = Convert.ToInt32(Request.QueryString["bookId"]);
        IDAL.IUsers IU = BllFactory.BllAccess.CreateIUsersBLL();

        if (HttpContext.Current.User.Identity.Name == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ok",
                "<script type='text/javascript'>alert('您还未登陆，请先登陆！');</script>", false
                );
        }
        else
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int count = IU.userBookTickedCard(userId, bookId);
            if (count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('我们代表作者对您的支持表示感谢！');this.location.href=this.location.href;</script>", false);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('对不起，您道具中没有书票，请去商城购买！');</script>", false);
            }

        }
    }
    protected void hlFlower_Click(object sender, ImageClickEventArgs e)
    {
        //送鲜花
        int bookId = Convert.ToInt32(Request.QueryString["bookId"]);
        IDAL.IUsers IU = BllFactory.BllAccess.CreateIUsersBLL();

        if (HttpContext.Current.User.Identity.Name == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ok",
                "<script type='text/javascript'>alert('您还未登陆，请先登陆！');</script>", false
                );
        }
        else
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int count = IU.UserFlowerCard(userId, bookId);
            if (count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('我们代表作者对您的支持表示感谢！');this.location.href=this.location.href;</script>", false);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('对不起，您道具中没有鲜花，请去商城购买！');</script>", false);
            }

        }
    }

    protected void issuBmit_Click(object sender, ImageClickEventArgs e)
    {
        IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();
        string context = fckPL.Value;
        int userid = 100;
        string picString = "";
        if (HttpContext.Current.Session["pic"] != null)
        {
            picString = HttpContext.Current.Session["pic"].ToString();
        }

        if (HttpContext.Current.User.Identity.Name != "")
        {
            userid = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        }

        if (picString != txValid.Text)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "s", "<script type='text/javascript'>alert('验证码错误，评论失败！');</script>");
            return;
        }
        else
        {
            iu.addReplayBook(userid, booId, context);
            Response.Redirect("BookCover.aspx?bookId=" + booId);
        }
    }
}