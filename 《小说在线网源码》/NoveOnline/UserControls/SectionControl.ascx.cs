using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DalFactory;
using IDAL;

public partial class UserControls_SectionControl : System.Web.UI.UserControl
{
    INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    //int PageCount;
    int pageSize = BLL.Config.PageSize;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    if (Request.QueryString["booktypeId"] != null)
        //    {
        //        int typeId = Convert.ToInt32(Request.QueryString["booktypeId"]);

        //        gv1.DataSource = novel.getNewSectionsByType(1, 20, typeId, out PageCount);
        //        gv1.DataBind();
        //    }
        //    else
        //    {
        //        gv1.DataSource = novel.getNewSections(1, 20, out PageCount);
        //        gv1.DataBind();
        //    }
        //}
    }
    protected void gv1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex + 1;

            Label lbl = e.Row.FindControl("lbl") as Label;
            if (lbl != null)
            {
                lbl.Text = index.ToString();
            }

            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DDD';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
           

        }
    }

    
}