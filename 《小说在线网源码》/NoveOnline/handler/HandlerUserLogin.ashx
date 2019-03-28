<%@ WebHandler Language="C#" Class="HandlerUserLogin" %>

using System;
using System.Web;

public class HandlerUserLogin : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/XML";
        
        if (HttpContext.Current.User.Identity.Name != "")
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