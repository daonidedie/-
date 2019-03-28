using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class UserControls_LoginInUserPanel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["NewUser"] != null && HttpContext.Current.User.Identity.Name !="")
        {
            List<Model.UsersInfo> list = (List<Model.UsersInfo>)Session["NewUser"];

            int UsertypeId = list[0].UserType;
            switch (UsertypeId)
            { 
                case 1:
                    list[0].UserTypeName = "普通会员";
                    break;
                case 2:
                    list[0].UserTypeName = "VIP用户";
                    break;
                case 3:
                    list[0].UserTypeName = "签约作者";
                    break;
                case 4:
                    list[0].UserTypeName = "管理员";
                    break;
            }
            g1.DataSource = list;
            g1.DataBind();
        }
        
    }
    protected void exit_Click(object sender, EventArgs e)
    {
        Session["NewUser"] = null;
        Session["cartId_shopping_" + HttpContext.Current.User.Identity.Name] = null;
        FormsAuthentication.SignOut();
        Response.Redirect("Default.aspx");
    }
}