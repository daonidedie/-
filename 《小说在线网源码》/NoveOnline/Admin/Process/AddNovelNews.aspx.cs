using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using MyExtension;
public partial class Admin_Process_AddNovelNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Model.NovelNewsInfo item = new Model.NovelNewsInfo();
        item.NewsTitle = Request["NewsTitle"];
        item.NewsImages = Request.QueryString["img"].ToString();
        item.FromWhere = Request["FromWhere"];
        item.NewsContens = Request["NewsContens"];
        item.NewsContens = Server.HtmlEncode(item.NewsContens);

        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        try
        {
            novel.addNovelNews(item);
            Response.Write("{success:true,msg:'添加成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}