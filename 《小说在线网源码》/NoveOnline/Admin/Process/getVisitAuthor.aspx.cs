using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using System.Text;
public partial class Admin_Process_App_Code_getVisitAuthor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IUsers user = BllFactory.BllAccess.CreateIUsersBLL();
        int recordCount;
        int recordIndex = Convert.ToInt32(Request["start"]);
        int pageSize = Convert.ToInt32(Request["limit"]);
        if (recordIndex == 0)
        {
            recordIndex = 0;
        }
        if (pageSize == 0)
        {
            pageSize = 22;
        }
        int pageIndex=recordIndex/pageSize+1;
        List<Model.VisitAuthorInfo> list = new List<Model.VisitAuthorInfo>();
        list = user.getVisitAuthor(pageIndex, pageSize,out recordCount);

        StringBuilder sb = new StringBuilder("{recordCount:" + recordCount + ",result:[");

        foreach (Model.VisitAuthorInfo item in list)
        {
            sb.Append("{VisitId:")
                .Append(item.VisitId)
                .Append(",UserName:'")
                .Append(item.UserName)
                .Append("',Contents:'")
                .Append(item.Contents)
                .Append("',VisitTitle:'")
                .Append(item.VisitTitle)
                .Append("',VisitDate:'")
                .Append(item.VisitDate)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]}");

        Response.Write(sb.ToString());
    }
}