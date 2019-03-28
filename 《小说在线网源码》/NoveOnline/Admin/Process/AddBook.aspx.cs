using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using MyExtension;
public partial class Admin_Process_AddBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Model.BooksInfo item = new Model.BooksInfo();
        item.BookName = Request["BookName"];
        item.AuthorId = Convert.ToInt32(Request["userId"]);
        item.Images = Request.QueryString["imgname"].ToString();
        item.BookIntroduction = Request["BookIntroduction"];
        item.BookType = Convert.ToInt32(Request["typeId"]);

        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        try
        {
            novel.addBook(item);
            Response.Write("{success:true,msg:'添加成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}