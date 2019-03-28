using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;

public partial class UserControls_UserRemmendNewsReplay : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int recordCount;
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        IUsers users = BllFactory.BllAccess.CreateIUsersBLL();

        List<Model.BooksInfo> bkList = new List<Model.BooksInfo>();
        bkList.Add(novel.getRemmendNovels(1, 1, out recordCount)[0]);

        repRecommand.DataSource = bkList;
        repRecommand.DataBind();

        List<Model.BooksInfo> RecommandList =  novel.getRemmendNovels(1, 10, out recordCount);
        RecommandList.RemoveAt(0);
        gridRecommand.DataSource = RecommandList;
        gridRecommand.DataBind();

        List<Model.NovelNewsInfo> templist = novel.getNovelNews();
        List<Model.NovelNewsInfo> newlist = new List<Model.NovelNewsInfo>();
        for (int i = 0; i < 2; i++)
        {
            newlist.Add(templist[i]);
        }
        repNews.DataSource = newlist;
        repNews.DataBind();
        
    }

    protected void gridRecommand_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DDD';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        }
    }
}