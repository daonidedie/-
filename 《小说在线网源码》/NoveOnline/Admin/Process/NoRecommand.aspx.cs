using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Process_NoRecommand : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        int bookid = Convert.ToInt32(Request["id"]);

        int count = novel.noRecommand(bookid);
        if (count > 0)
        {
            Response.Write("{success:true,msg:'取消推荐成功!'}");
        }
        else
        {
            Response.Write("{success:false,msg:'操作失败!'}");
        }


    }
}