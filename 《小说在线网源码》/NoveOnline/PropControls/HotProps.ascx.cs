using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropControls_HotProps : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IProp ip = BllFactory.BllAccess.CreateIPropBLL();
        gridHotProps.DataSource = ip.getHotProps();
        gridHotProps.DataBind();
        
    }
    protected void gridHotProps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hyp = (HyperLink)e.Row.FindControl("hypls1");
            string pid = hyp.NavigateUrl;
            string cartID;

            if (HttpContext.Current.User.Identity.Name != "")
            {
                cartID = BLL.ShopCartAccess.getCartID;
            }
            else
            {
                cartID = "nologin";
            }

            string test = "return Islogin(" + pid + ",'" + cartID + "');";
            hyp.Attributes.Add("onclick", "return Islogin(" + pid + ",'" + cartID + "');");
        }
    }
}