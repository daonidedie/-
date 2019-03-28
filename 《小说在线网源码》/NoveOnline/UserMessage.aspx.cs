using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.Security;
public partial class UserMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["userId"] != HttpContext.Current.User.Identity.Name)
        {
            Response.Redirect("");
        }
    }
    protected void repMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delmsg")
        {
            string msgId = e.CommandArgument.ToString();
            IUsers us = BllFactory.BllAccess.CreateIUsersBLL();
            int ok = us.delMsg(Convert.ToInt32(msgId));
        }
    }
}