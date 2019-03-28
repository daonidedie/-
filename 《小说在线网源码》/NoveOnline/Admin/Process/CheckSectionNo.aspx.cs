using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Process_CheckSectionNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int SeciontId = Convert.ToInt32(Request.QueryString["sectionsId"]);
        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();
        int count = ia.SectionCheckNo(SeciontId);
        if (count > 0)
        {
            Response.Write("{success:true,msg:'删除成功！'}");
        }
        else
        {
            Response.Write("{success:false,msg:'操作失败'}");
        }

    }
}