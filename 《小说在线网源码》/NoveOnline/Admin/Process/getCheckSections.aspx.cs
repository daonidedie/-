using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Process_getCheckSections : System.Web.UI.Page
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
        List<Model.SectionsInfo> list = ia.getCheckSections(pageNumber,pagesize,out recordCount);
        StringBuilder sb = new StringBuilder("{recordCount:" + recordCount + ",result:[");

        foreach (Model.SectionsInfo item in list)
        {
            sb.Append("{SectiuonId:")
                .Append(Convert.ToInt32(item.SectiuonId))
                .Append(",BookName:'")
                .Append(item.BookName)
                .Append("',CharNum:")
                .Append(Convert.ToInt32(item.CharNum))
                .Append(",ShortAddTime:'")
                .Append(item.ShortAddTime)
                .Append("',StateName:'")
                .Append(item.StateName)
                .Append("',BookId:'")
                .Append(item.BookId)
                .Append("',SectionTitle:'")
                .Append(item.SectionTitle)
                .Append("'},");
        }

        sb.Remove(sb.Length - 1, 1);
        sb.Append("]}");

        Response.Write(sb.ToString());
    }
}