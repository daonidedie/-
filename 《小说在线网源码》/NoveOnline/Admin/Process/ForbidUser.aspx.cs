using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;

using MyExtension;
public partial class Admin_Process_ForbidUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IUsers users = BllFactory.BllAccess.CreateIUsersBLL();
        string userID = Request["id"];
        string[] userId = userID.Substring(0, userID.Length - 1).Split(',');
        try
        {
            int i;
            for (i = 0; i < userId.Length; i++)
            {
                users.ForbidUser(Convert.ToInt32(userId[i]));
            }
            Response.Write("{success:true,msg:'操作成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message.ToStr();
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}