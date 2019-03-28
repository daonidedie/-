using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using BllFactory;
using System.Text;
public partial class Admin_Process_GetRnchainUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IUsers novel = BllFactory.BllAccess.CreateIUsersBLL();
        List<Model.UsersInfo> list = novel.GetRnchainUser();
        StringBuilder sb = new StringBuilder("[");
        foreach (Model.UsersInfo item in list)
        {
            sb.Append("{UserId:")
                .Append(item.UserId)
                .Append(",UserName:'")
                .Append(item.UserName)
                .Append("',UserSex:'");
            if (item.UserSex == 0)
                sb.Append("男");
            else if (item.UserSex == 1)
                sb.Append("女");
            sb.Append("',UserBrithday:'")
                .Append(item.UserBrithday)
                .Append("',BookMoney:'")
                .Append(item.BookMoney)
                .Append("',CreateTime:'")
                .Append(item.CreateTime)
                .Append("',UserTypeName:'")
                .Append(item.UserTypeName)
                .Append("',province:'")
                .Append(item.province)
                .Append("',city:'")
                .Append(item.city)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]");

        Response.Write(sb.ToString());
    }
}