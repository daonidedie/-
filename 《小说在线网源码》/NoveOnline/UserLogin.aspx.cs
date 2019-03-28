using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void enterLogin_Click(object sender, ImageClickEventArgs e)
    {
        string username = loginUserName.Text;
        string password = loginPassword.Text;
        IUsers us = BllFactory.BllAccess.CreateIUsersBLL();
        List<Model.UsersInfo> listUser = us.UserLogin(username,password);
        
        if (listUser.Count > 0)
        {
            if (listUser[0].UserType == 5)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "close",
            "<script type='text/javascript'>var dialog = Dialog.getInstance('Diag');dialog.close();dialog.TopWindow.location.href=dialog.TopWindow.location.href;alert('你的帐号已被禁用，请联系管理员！');</script>", false);
            }
            else
            {
                listUser[0].IpAddress = Request.UserHostAddress;
                Session["NewUser"] = listUser;
                FormsAuthentication.SetAuthCookie(listUser[0].UserId.ToString(), false);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "close",
                 "<script type='text/javascript'>var dialog = Dialog.getInstance('Diag');dialog.close();dialog.TopWindow.location.href=dialog.TopWindow.location.href;</script>", false);
            }
        }
        else
        {
            if (loginPassword.Text == "" && loginUserName.Text == "")
            {
                errlb.Text = "提示：帐号，密码不能为空！";
                loginUserName.Focus();
            }
            else if (loginUserName.Text == "")
            {
                errlb.Text = "提示：帐号不能为空！"; loginUserName.Focus();
            }
            else if (loginPassword.Text == "")
            {
                errlb.Text = "提示：密码不能为空！"; loginPassword.Focus();
            }
            else 
            {
                errlb.Text = "提示：帐号密码错误！"; loginUserName.Focus();
            }
                
            errlb.Visible = true;
        }
    }
}