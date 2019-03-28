using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using BllFactory;
using System.Text;

public partial class Admin_Process_GetBooks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        int recordCount;
        int bookType = Convert.ToInt32(Request["BookType"]) > 0 ? Convert.ToInt32(Request["BookType"]) : 0;
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
        List<Model.BooksInfo> list = new List<Model.BooksInfo>();
        list = novel.getBooks(out recordCount, pageSize, recordIndex, bookType);
        StringBuilder sb = new StringBuilder("{recordCount:" + recordCount + ",result:[");

        foreach (Model.BooksInfo item in list)
        {
            sb.Append("{BookId:")
                .Append(item.BookId)
                .Append(",BookName:'")
                .Append(item.BookName)
                .Append("',UserName:'")
                .Append(item.UserName)
                .Append("',Images:'")
                .Append(item.Images)
                .Append("',StateName:'")
                .Append(item.StateName)
                .Append("',TypeName:'")
                .Append(item.TypeName)
                .Append("',Recommand:'");
            if (item.Recommand == 0)
                sb.Append("是");
            else
                sb.Append("否");
                sb.Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]}");

        Response.Write(sb.ToString());
    }
}