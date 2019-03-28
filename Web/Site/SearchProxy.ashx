<%@ WebHandler Language="C#" Class="SearchProxy" %>

using System;
using System.Web;
using Skybot.Tong.CodeCharSet;
/// <summary>
/// 搜索代理页面
/// </summary>
public class SearchProxy : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string key= context.Request["keyword"];
        if (string.IsNullOrEmpty(key))
        {
             context.Response.Redirect("~/Site/BookList.aspx");
            context.Response.End();
        }
        
        key=string.IsNullOrEmpty(key.Trim())?string.Empty:key.Trim();
        context.Response.Redirect("/Search/" + key.ToPingYing() + "/" + context.Server.UrlEncode(key) + "/index.aspx");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}