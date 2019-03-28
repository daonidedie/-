using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using BllFactory;
using Model;
public partial class BooksInfo : System.Web.UI.Page
{
    private IDAL.INovel dal = BllAccess.CreateINovelBLL();
    private int BooksId;
    private string SectiuonId;
    int VolumeId;
    int VolumeId2;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        BooksId = Convert.ToInt32(Request.QueryString["bookId"]);

        VolumeId = dal.getNovelClassInfo(BooksId)[0].VolumeId;
        List<Model.VolumeInfo> List = dal.getNovelClassInfo(BooksId);
        List<Model.VolumeInfo> changeList = new List<VolumeInfo>();
        foreach (Model.VolumeInfo item in List)
        {
            item.ValumeName = Common.Myweeks.changeNumber(item.ValumeName);
            changeList.Add(item);
        }
        
        repeater.DataSource = changeList;
        

        repeater.DataBind();

        Model.BooksInfo bookInfo = dal.getBookIdBooksInfo(BooksId);
        lbBookName.Text = bookInfo.BookName;
        lbUserName.Text = "作者:" + bookInfo.UserName + "&nbsp;&nbsp;&nbsp;&nbsp;" + "类型：" + bookInfo.TypeName;
        lbUpdateTime.Text = "更新时间:" + bookInfo.AddTime.ToShortDateString();

    }
    protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Repeater repeaterNoelSections = e.Item.FindControl("repeaterNovelSectionsInfo") as Repeater;
            WebControlExtension.LabelEx lbe = e.Item.FindControl("lbeNovelClassId") as WebControlExtension.LabelEx;
            VolumeId2 = Convert.ToInt32(lbe.Text);

            repeaterNoelSections.DataSource = dal.getNovelSectionsInfo(VolumeId2);
            repeaterNoelSections.DataBind();
            
        }
    }
    protected void repeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void Bcd(object sender, RepeaterItemEventArgs e)
    {
        TextBox b = e.Item.FindControl("TextBox1") as TextBox;
        if (b != null)
            SectiuonId = b.Text;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink a = e.Item.FindControl("hyperLink") as HyperLink;

            if (a != null && VolumeId == VolumeId2)
            {
                a.NavigateUrl = "~/BookSectionsInfo.aspx?BookId=" + BooksId + "&&SectiuonId=" + SectiuonId + "&&inspect=0";
            }
            else
            {
                a.NavigateUrl = "~/BookSectionsInfo.aspx?BookId=" + BooksId + "&&SectiuonId=" + SectiuonId + "&&inspect=1";
            }
        }
    }

    protected void Abc(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    HyperLink a = e.Item.FindControl("hyperLink") as HyperLink;

        //    if (a != null && VolumeId == VolumeId2)
        //    {
        //        a.NavigateUrl = "~/BookSectionsInfo.aspx?BooksId=" + BooksId + "&&SectiuonId=" + SectiuonId + "&&inspect=0";
        //    }
        //    else 
        //    {
        //        a.NavigateUrl = "~/BookSectionsInfo.aspx?BooksId=" + BooksId + "&&SectiuonId=" + SectiuonId + "&&inspect=1";
        //    }
        //}
    }
    protected void hlBookcase_Click(object sender, EventArgs e)
    {
        //加入书架
        int bookId =  Convert.ToInt32(Request.QueryString["bookId"]);
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
    protected void linkreadingbook_Click(object sender, EventArgs e)
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
    protected void hlFlower_Click(object sender, EventArgs e)
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok","<script type='text/javascript'>alert('我们代表作者对您的支持表示感谢！');</script>", false);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('对不起，您道具中没有鲜花，请去商城购买！');</script>", false);
            }
            
        }
    }
    protected void hlTicket_Click(object sender, EventArgs e)
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('我们代表作者对您的支持表示感谢！');</script>", false);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('对不起，您道具中没有书票，请去商城购买！');</script>", false);
            }

        }
    }
}