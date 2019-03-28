using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DalFactory;
using IDAL;

public partial class UserControls_NovelNewSections : System.Web.UI.UserControl
{
    INovel novel = BllFactory.BllAccess.CreateINovelBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int pageSize = BLL.Config.PageSize;
        int pageCount;
        gv1.DataSource = novel.getNewSections(1, 15,out pageCount);
        gv1.DataBind();
        
    }
    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DDD';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        }
    }
}