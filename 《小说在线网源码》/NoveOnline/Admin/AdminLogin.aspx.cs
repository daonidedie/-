using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using IDAL;

public partial class Admin_AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void enterLogin_Click(object sender, ImageClickEventArgs e)
    {
        string username = loginUserName.Text;
        string password = loginPassword.Text;
        IUsers us = BllFactory.BllAccess.CreateIUsersBLL();
        List<Model.UsersInfo> listUser = us.UserLogin(username, password);
        
        if (listUser.Count > 0)
        {
            if (listUser[0].UserType == 4) //4是管理员
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "close",
                 "<script type='text/javascript'>var dialog = Dialog.getInstance('Diag');dialog.close();window.open('Admin_Index.aspx','main','width=1000px,height=700px');</script>", false);
            }
            else
            {
                errlb.Text = "您不是管理员！";
                errlb.Visible = true;
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