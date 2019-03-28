using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Process_getCheckAuthorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();
        int recordCount = 0;
        int pageindex = Convert.ToInt32(Request["start"]);
        int pagesize = Convert.ToInt32(Request["limit"]);
        if (pagesize == 0)
        {
            pagesize = 22;
        }

        int pageNumber = pageindex / pagesize + 1;
        
        List<Model.UsersInfo> checkAuthorList = ia.getCheckAuthor(pageNumber,pagesize, out recordCount);

        StringBuilder sb = new StringBuilder("{recordCount:"+ recordCount + ",result:[");

        foreach (Model.UsersInfo item in checkAuthorList)
        {
            sb.Append("{UserId:")
                .Append(item.UserId)
                .Append(",UserName:'")
                .Append(item.UserName)
                .Append("',UserType:'")
                .Append(item.UserTypeName)
                .Append("',UserAddress:'")
                .Append(item.userAddress)
                .Append("',IdentityCardNumber:'")
                .Append(item.IdentityCardNumber)                
                .Append("',UserSex:'");

            if (item.UserSex == 1)
            {
                sb.Append("男'");
            }
            else
            {
                sb.Append("女'");
            }

            sb.Append("},");
        }

        sb.Remove(sb.Length - 1,1);
        sb.Append("]}");

        Response.Write(sb.ToString());
    }
}