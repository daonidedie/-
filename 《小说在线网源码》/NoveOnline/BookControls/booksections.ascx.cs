using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_booksections : System.Web.UI.UserControl
{

    IDAL.IAuthor Ia = BllFactory.BllAccess.CreateIAuthorBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        int VolumeID = Convert.ToInt32(Request.QueryString["volumeId"]);
        int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        int bookId = Convert.ToInt32(Request.QueryString["bookId"]);
        face.NavigateUrl = Request.Url.AbsolutePath + "?type=2&ct=1&bookId=" + bookId;
        vs.NavigateUrl = Request.Url.AbsolutePath + "?type=2&ct=2&bookId=" + bookId;

        int recordCount= 0;
        int pageindex = 1;
        int pageSize = 11;
        int pageNumber = 0;
        if (Request.QueryString["page"] != null)
        {
            pageindex = Convert.ToInt32(Request.QueryString["page"]);
        }

        gr2.DataSource = Ia.getVolumeSecionts(pageindex, pageSize, VolumeID, out recordCount);
        gr2.DataBind();

        pageNumber = (int)Math.Ceiling((double)recordCount / pageSize);

        string url = Request.Url.AbsolutePath + "?type=2&ct=3&bookId=" + bookId  + "&volumeId=" + VolumeID + "&page=";

        lblPropNumber.Text = "该卷共有 " + recordCount + " 章，第" + pageindex + "页/共" + pageNumber + "页";

        FirstPage.NavigateUrl = url + 1;
        PrvPage.NavigateUrl = url + (pageindex - 1);
        NewxtPage.NavigateUrl = url + (pageindex + 1);
        LastPage.NavigateUrl = url + pageNumber;

        if (pageindex == 1)
        {
            FirstPage.Visible = false;
            PrvPage.Visible = false;
        }

        if (pageindex == pageNumber)
        {
            NewxtPage.Visible = false;
            LastPage.Visible = false;
        }

        


    }
    protected void gr2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delsec")
        {
            int secid = Convert.ToInt32(e.CommandArgument);
            int count = Ia.Audelsection(secid);

            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('删除成功！');this.location.href=this.location.href;</script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "err", "<script type='text/javascript'>alert('删除失败！');</script>", false);
            }
        }
    }
}