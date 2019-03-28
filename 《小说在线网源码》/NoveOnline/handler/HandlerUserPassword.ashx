<%@ WebHandler Language="C#" Class="HandlerUserPassword" %>

using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;


public class HandlerUserPassword : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        string acc = context.Request.QueryString["acc"];
        string pwd = context.Request.QueryString["PWD"];
        IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();
        List<Model.UsersInfo> list = iu.UserLogin(acc,pwd);
        if (list.Count > 0)
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