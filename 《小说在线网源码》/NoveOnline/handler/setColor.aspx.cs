using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_setColor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Color"] != null)
        {
            string Color = Request.QueryString["Color"].ToString();
            Session["setColor"] = Color;
        }

    }
}