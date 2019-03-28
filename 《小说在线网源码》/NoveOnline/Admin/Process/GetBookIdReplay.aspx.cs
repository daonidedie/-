using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using BllFactory;
using System.Text;
public partial class Admin_Process_GetBookIdReplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        int recordCount;
        int bookId = Convert.ToInt32(Request.QueryString["bookId"]);
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
        List<Model.BookReplayInfo> list = new List<Model.BookReplayInfo>();
        list = novel.getBookIdReplay(out recordCount, pageSize, recordIndex, bookId);
        StringBuilder sb = new StringBuilder("{recordCount:" + recordCount + ",result:[");

        foreach (Model.BookReplayInfo item in list)
        {
            sb.Append("{ReplayId:")
                .Append(item.ReplayId)
                .Append(",ReplayContext:'")
                .Append(item.ReplayContext)
                .Append("',ReplayTime:'")
                .Append(Convert.ToDateTime(item.ReplayTime).ToShortDateString())
                .Append("',UserName:'")
                .Append(item.UserName)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]}");

        Response.Write(sb.ToString());
    }
}