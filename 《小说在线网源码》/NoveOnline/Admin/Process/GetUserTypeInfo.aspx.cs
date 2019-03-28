using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using BllFactory;
using System.Text;
public partial class Admin_Process_GetUserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IUsers users = BllFactory.BllAccess.CreateIUsersBLL();
        List<Model.UserTypeInfo> list = users.getUserTypeInfo();
        StringBuilder sb = new StringBuilder("[");

        foreach (Model.UserTypeInfo item in list)
        {
            sb.Append("{UserTypeId:'")
                .Append(item.UserTypeId)
                .Append("',UserTypeName:'")
                .Append(item.UserTypeName)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]");
        Response.Write(sb.ToString());
    }
}