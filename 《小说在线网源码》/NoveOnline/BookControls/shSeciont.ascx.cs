using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_shSeciont : System.Web.UI.UserControl
{
    IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        int userid = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        gvseex.DataSource = ia.getExsetions(userid);
        gvseex.DataBind();
        
    }

    protected void gvseex_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow) 
        {
            Label lb = (Label)e.Row.FindControl("lbnumber");
            lb.Text = (e.Row.RowIndex + 1).ToString();
        }
    }
}