using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_setFontColor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FontColor"] != null)
        {
            string FontColor = Request.QueryString["FontColor"].ToString();
            Session["FontColor"] = FontColor;
        }
        if (Request.QueryString["FontSize"] != null)
        {
            string FontSize = Request.QueryString["FontSize"];
            Session["FontSize"] = FontSize;
        }
    }
}