using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;

public partial class UserControls_sectionTypeByNewBook : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();

        rep1.DataSource = novel.getNovelType();
        rep1.DataBind();
        
    }


    protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HyperLink type2 = e.Item.FindControl("hlTypeId") as HyperLink;
        HiddenField type = e.Item.FindControl("hfTypeId") as HiddenField;
        string a =Request.QueryString["bookTypeId"].ToString();
        if (type != null && type.Value == a)
            type2.Style.Add("font-weight", "bold");
    }
}