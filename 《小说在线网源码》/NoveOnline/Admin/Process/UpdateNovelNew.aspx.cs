using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using MyExtension;
public partial class Admin_Process_UpdateNovelNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Model.NovelNewsInfo item = new Model.NovelNewsInfo();
        item.NewsId = Convert.ToInt32(Request["NewsId"]);
        item.AddTime = DateTime.Now;
        item.NewsTitle = Request["NewsTitle"];
        item.NewsImages = Request["NewsImages"];
        item.FromWhere = Request["FromWhere"];
        item.NewsContens = Request["NewsContens"];

        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        try
        {
            novel.updateNovelNews(item);
            Response.Write("{success:true,msg:'修改成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}