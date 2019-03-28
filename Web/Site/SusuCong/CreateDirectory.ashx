<%@ WebHandler Language="C#" Class="CreateDirectory" %>

using System;
using System.Web;

/// <summary>
/// 用于创建文件夹的东东
/// </summary>
public class CreateDirectory : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        
        if (context.Request.QueryString.AllKeys.Length > 0)
        {
            string dic = AppDomain.CurrentDomain.BaseDirectory + context.Request.QueryString[0];
            if (!System.IO.Directory.Exists(dic))
            {
                System.IO.Directory.CreateDirectory(dic);
                context.Response.Write("创建");
            }
            else
            {
                context.Response.Write("存在");
            }
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}