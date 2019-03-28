using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using BllFactory;
using System.Text;
public partial class Admin_Process_GetBookType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        List<Model.BookTypeInfo> list = novel.getBookType();
        StringBuilder sb = new StringBuilder("[");

        foreach (Model.BookTypeInfo item in list)
        {
            sb.Append("{TypeId:'")
                .Append(item.TypeId)
                .Append("',TypeName:'")
                .Append(item.TypeName)
                .Append("'},");
        }
        sb.Remove(sb.Length - 1, 1).Append("]");
        Response.Write(sb.ToString());
    }
}