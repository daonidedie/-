using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;

using MyExtension;

public partial class Admin_Process_DelNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request["id"]);

        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        try
        {
            novel.deleteNovelNews(id);
            Response.Write("{success:true,msg:'删除成功'}");
        }
        catch(Exception ex)
        {
            string msg = ex.Message.ToStr();
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
        
    }
}