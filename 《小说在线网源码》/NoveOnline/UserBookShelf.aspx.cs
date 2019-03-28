using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;

public partial class UserBookShelf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

            INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        
            List<Model.BookTypeInfo> typeList = novel.getNovelType();

            RebookType.DataSource = typeList;
            RebookType.DataBind();

            int userID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int booktype = 0;
            int pageIndex = 1;
            int recordCount;
            int pageNumber = 0;
            string typeName = "全部分类，";
            string url = Request.Url.AbsolutePath + "?NS=2";
            if (Request.QueryString["type"] != null)
            {
                booktype = Convert.ToInt32(Request.QueryString["type"]);
                if (booktype != 0)
                {
                    typeName = typeList[booktype - 1].TypeName + "类，";
                }

            }
 
            if (Request.QueryString["page"] != null)
            {
                pageIndex = Convert.ToInt32(Request.QueryString["page"]);
            }

            

            List<Model.UserBookShelfInfo> list = novel.getUserBookShelf(userID, pageIndex, 4, booktype, out recordCount);
            repBooksSelf.DataSource = list;
            repBooksSelf.DataBind();

            pageNumber = (int)Math.Ceiling((double)recordCount / 4);

            lblUserShelf.Text = typeName + "共" + pageNumber + "页，当前第" + pageIndex + "页";
            url = url + "&type=" + booktype;
            url = url + "&page=";
            

            FirstPage.NavigateUrl = url + 1;
            PrvPage.NavigateUrl = url + (pageIndex - 1);
            NewxtPage.NavigateUrl = url + (pageIndex + 1);
            LastPage.NavigateUrl = url + pageNumber;

            if (pageIndex == 1)
            {
                FirstPage.Visible = false;
                PrvPage.Visible = false;
            }

            if (pageIndex == pageNumber)
            {
                NewxtPage.Visible = false;
                LastPage.Visible = false;
            }

            if (recordCount == 0)
            {
                nobooks.Visible = true;
                hasbooks.Visible = false;
            }
        

    }


    protected void repBooksSelf_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
        //删除用户书架书本
        if (e.CommandName == "delbook")
        {
            LinkButton delbutton =  (LinkButton)e.Item.FindControl("delshelfbook");
            int bookid = Convert.ToInt32(delbutton.CommandArgument);
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            IDAL.INovel  novel = BllFactory.BllAccess.CreateINovelBLL();
            novel.delUserbookShelf(userId,bookid);
            Response.Redirect("UserBookShelf.aspx");
        }
    }
}