using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Process_delBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int bookid = Convert.ToInt32(Request["bookid"]);
        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();
        int count = ia.delbook(bookid);
        if (count > 0)
        {
            Response.Write("{success:true,msg:'删除成功'}");
        }
        else
        {
            Response.Write("{success:false,msg:'删除失败'}");
        }
    }
}