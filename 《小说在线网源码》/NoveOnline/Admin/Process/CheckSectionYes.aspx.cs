using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Process_CheckSectionYes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int bookid = Convert.ToInt32(Request.QueryString["BookId"]);
        int sectionId = Convert.ToInt32(Request.QueryString["sectionId"]);
        string BookNmae = Request.QueryString["BookName"].ToString();


        IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        Model.BooksInfo item = novel.getBookCoverInfo(bookid);


        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();

        int count  = ia.SectionCheckYES(sectionId, bookid, item.BookName);
        if (count > 0)
        {
            Response.Write("{success:true,msg:'操作成功'}");
        }
        else
        {
            Response.Write("{success:false,msg:'操作失败'}");
        }
    }
}