using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

using System.Data;

namespace Common
{
    public class UserPanel : System.Web.UI.Page
    {

        /// <summary>
        /// 获取当前登陆用户名
        /// </summary>
        /// <returns></returns>
        public string getUserName()
        {
            return User.Identity.Name;
        }

        //退出登陆
        public void userExit()
        {
            Session["NewUser"] = null;
            Session["cartId_shopping_" + HttpContext.Current.User.Identity.Name] = null;
            FormsAuthentication.SignOut();
        }

    }
}
