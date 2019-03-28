<%@ WebHandler Language="C#" Class="HandlerPageStyle" %>

using System;
using System.Web;

public class HandlerPageStyle : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string Color = context.Request.QueryString["Color"].ToString();
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}