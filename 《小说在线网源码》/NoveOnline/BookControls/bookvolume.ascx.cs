using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_bookvolume : System.Web.UI.UserControl
{
    IDAL.IAuthor IA = BllFactory.BllAccess.CreateIAuthorBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
        int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        face.NavigateUrl = Request.Url.AbsolutePath + "?type=2&ct=1&bookId=" + bookId;
        vs.NavigateUrl = Request.Url.AbsolutePath + "?type=2&ct=2&bookId=" + bookId;
        List<Model.VolumeInfo> list = IA.getBookVolumes(userId, bookId);
        if (list.Count > 0)
        {
            lbname.Text = list[0].BookName;
        }
        gr1.DataSource = IA.getBookVolumes(userId, bookId);
        gr1.DataBind();

        
    }

    protected void gr1_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        
       
    }
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
        int count = IA.AuaddVolume(bookId, txnewVO.Text, Convert.ToInt32(txnum.Text));


        if (count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('添加成功！');this.location.href=this.location.href;</script>", false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "err", "<script type='text/javascript'>alert('添加失败！');</script>", false);
        }
    }


    protected void gr1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delvo")
        {
            try
            {
                int volumeId = Convert.ToInt32(e.CommandArgument);
                int count = IA.AudelVolume(volumeId);
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('删除成功！');this.location.href=this.location.href;</script>", false);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "err", "<script type='text/javascript'>alert('请先删除卷下章节！');</script>", false);
            }
        }
    }
}