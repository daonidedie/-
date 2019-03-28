using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class UserChangePWD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Model.UsersInfo> list = (List<Model.UsersInfo>)Session["NewUser"];

        string UserName = list[0].UserName;
        string acc = list[0].AccountNumber;
        lbusername.Text = UserName;
        lbacc.Text = acc;

    }
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        int userid = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        string newpwd = newpwd2.Text;
        IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();
        int count = iu.changeUserPassword(userid,newpwd);
        if (count == 1)
        {
            Session["NewUser"] = null;
            Session["cartId_shopping_" + HttpContext.Current.User.Identity.Name] = null;
            FormsAuthentication.SignOut();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "close",
                "<script type='text/javascript'>var dialog = Dialog.getInstance('Diag');dialog.close();alert('修改成功，请重新登陆！');dialog.TopWindow.location.href=dialog.TopWindow.location.href;</script>", false);
        }
        else
        { 
        }
    }
}