using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["userId"] != null)
        {
            if (User.Identity.Name != Request.QueryString["userId"].ToString())
            {
                string url = Request.Url.AbsolutePath;
                Response.Redirect(url);
            }
        }  
    }
}