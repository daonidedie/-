using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
public partial class BooksAllInfo : System.Web.UI.Page
{
    INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        string BookPicPath = @"Images/Books/";

        int typeId = Convert.ToInt32(Request.QueryString["bookTypeId"]);


        appointTypeId2.Value = novel.getAppointTypeId(typeId).TypeName + "小说排行榜";
        appointTypeId2.Text = novel.getAppointTypeId(typeId).TypeId.ToString();

        leTypeName.Value = novel.getAppointTypeId(typeId).TypeName + " - 新书精品推荐";

        List<Model.BooksInfo> booklist = novel.getTypeIdUpFoure(typeId);

        bookTextRepeater.DataSource = booklist;
        bookTextRepeater.DataBind();

        //动态给图片路径
        LargeImg1.ImageUrl = BookPicPath + booklist[0].Images;
        LargeImg2.ImageUrl = BookPicPath + booklist[1].Images;
        LargeImg3.ImageUrl = BookPicPath + booklist[2].Images;
        LargeImg4.ImageUrl = BookPicPath + booklist[3].Images;

        l1.HRef = "~/BookCover.aspx?bookId=" + booklist[0].BookId;
        l2.HRef = "~/BookCover.aspx?bookId=" + booklist[1].BookId;
        l3.HRef = "~/BookCover.aspx?bookId=" + booklist[2].BookId;
        l4.HRef = "~/BookCover.aspx?bookId=" + booklist[3].BookId;

        SmallImg1.ImageUrl = BookPicPath + booklist[0].Images;
        SmallImg2.ImageUrl = BookPicPath + booklist[1].Images;
        SmallImg3.ImageUrl = BookPicPath + booklist[2].Images;
        SmallImg4.ImageUrl = BookPicPath + booklist[3].Images;

        s1.HRef = "~/BookCover.aspx?bookId=" + booklist[0].BookId;
        s2.HRef = "~/BookCover.aspx?bookId=" + booklist[0].BookId;
        s3.HRef = "~/BookCover.aspx?bookId=" + booklist[0].BookId;
        s4.HRef = "~/BookCover.aspx?bookId=" + booklist[0].BookId;

        booksInfo2.DataSource = novel.getTicketOrFlower(typeId);
        booksInfo2.DataBind();

        repeaterGetNewBooksCommend.DataSource = novel.getNewBooksCommend(typeId);
        repeaterGetNewBooksCommend.DataBind();


        lbMemory.Value = novel.getAppointTypeId(typeId).TypeName + " - 经典怀旧";

        rpMemoryName.DataSource = novel.getMemory(typeId);
        rpMemoryName.DataBind();

    }
    protected void repeaterAll_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        TextBox tbTypeId = e.Item.FindControl("tbTypeId") as TextBox;
        HyperLink hlTypeName = e.Item.FindControl("hlType") as HyperLink;
        if (Convert.ToInt32(Request.QueryString["bookTypeId"]) == Convert.ToInt32(tbTypeId.Text))
        {
            hlTypeName.Style.Add("font-weight", "bold");
        }
    }
}