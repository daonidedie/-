using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string search = Request.QueryString["search"];
        string[] words=search.Split(',');
        List<Model.SectionsInfo> list = new List<Model.SectionsInfo>();

        INovel novel=BllFactory.BllAccess.CreateINovelBLL();
        int count;
        int pageIndex;

        string url =  HttpContext.Current.Request.Url.AbsolutePath + "?search=" + Server.UrlEncode(search) + "&page=";

        pageIndex = Convert.ToInt32(Request.QueryString["page"]);

        if (Request.QueryString["page"] == null)
        {
            pageIndex = 1;
            list = novel.NovelSearch(1, 10,200,words, out count);

        }
        else
        {
            list = novel.NovelSearch(pageIndex, 10,200,words, out count);
        }


        
        int pageNumber = (int)Math.Ceiling((double)count / 10);


        lblPage.Text = "第"+ pageIndex + "页/" + "共" + pageNumber + "页";
        LinkFirst.NavigateUrl = url + 1;
        LinkNext.NavigateUrl = url + (pageIndex + 1);
        LinkPrece.NavigateUrl = url + (pageIndex - 1);
        LinkLast.NavigateUrl = url + pageNumber;
       

        if (pageIndex == 1)
        {
            LinkFirst.Visible = false;
            LinkPrece.Visible = false;
        }

        if (pageIndex == pageNumber)
        {
            LinkLast.Visible = false;
            LinkNext.Visible = false;
        }

        if (list.Count < 1)
        {
            lblnobook.Text = "抱歉，没有找到任何内容！";
            lblnobook.Visible = true;
            gv1.Visible = false;
            pagediv.Visible = false;
        } 



        gv1.DataSource = list;
        gv1.DataBind();
    }
}