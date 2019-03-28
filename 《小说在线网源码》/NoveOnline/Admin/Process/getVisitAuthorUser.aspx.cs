using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using System.Text;

public partial class Admin_Process_getVisitAuthorUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IUsers user = BllFactory.BllAccess.CreateIUsersBLL();
            List<Model.UsersInfo> list = user.getVisitAuthorUser();
            StringBuilder sb = new StringBuilder("[");

            foreach (Model.UsersInfo item in list)
            {
                sb.Append("{UserId:'")
                    .Append(item.UserId)
                    .Append("',UserName:'")
                    .Append(item.UserName)
                    .Append("'},");
            }
            sb.Remove(sb.Length - 1, 1).Append("]");
            Response.Write(sb.ToString());
        }
    }
}