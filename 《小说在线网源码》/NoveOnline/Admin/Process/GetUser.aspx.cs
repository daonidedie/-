using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using BllFactory;
using System.Text;
public partial class Admin_Process_GetUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        List<Model.UsersInfo> list = novel.getUser();
        StringBuilder sb = new StringBuilder("[");

        foreach (Model.UsersInfo item in list)
        {
            sb.Append("{UserName:'")
                .Append(item.UserName)
                .Append("',UserId:'")
                .Append(item.UserId)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]");
        Response.Write(sb.ToString());
    }
}