<%@ WebHandler Language="C#" Class="HandlerHasUser" %>

using System;
using System.Web;

public class HandlerHasUser : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";

        IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();

        string username = context.Request.QueryString["username"].ToString();

        int flag = iu.havaUser(username);

        if (flag == 1)
        {
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write("0");
        }

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}