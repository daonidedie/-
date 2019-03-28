using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using BllFactory;
using System.Text;

public partial class Admin_Process_GetNewsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
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
        List<Model.NovelNewsInfo> list = new List<Model.NovelNewsInfo>();
       list = novel.getNovelNewss(out recordCount, pageSize, recordIndex);

        StringBuilder sb = new StringBuilder("{recordCount:" + recordCount + ",result:[");

        foreach (Model.NovelNewsInfo item in list)
        {
            sb.Append("{NewsId:")
                .Append(item.NewsId)
                .Append(",NewsTitle:'")
                .Append(item.NewsTitle)
                .Append("',NewsContens:'")
                .Append(item.NewsContens)
                .Append("',NewsImages:'")
                .Append(item.NewsImages)
                .Append("',FromWhere:'")
                .Append(item.FromWhere)
                .Append("',AddTime:'")
                .Append(item.AddTimeString)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]}");

        Response.Write(sb.ToString());
    }
}