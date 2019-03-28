<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLServerDAL;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        //在这里加方法内容 request接参数

        string userId = HttpContext.Current.User.Identity.Name;
        string PROC_ADDPROP = "addprop";
        string cartId = context.Request.QueryString["cartId"];
        string porpId = context.Request.QueryString["propId"];
        
        SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@cartId",cartId),
            new SqlParameter("@userId",userId),
            new SqlParameter("@propId",porpId)
        };

        context.Response.Write(SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDPROP, parm));
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    

}