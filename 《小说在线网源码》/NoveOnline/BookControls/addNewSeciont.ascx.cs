using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_addNewSeciont : System.Web.UI.UserControl
{
    IDAL.IAuthor Ia = BllFactory.BllAccess.CreateIAuthorBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            DropDownList2.DataSource = Ia.getAuthorBooks(userId);
            DropDownList2.DataTextField = "BookName";
            DropDownList2.DataValueField = "BookId";
            DropDownList2.DataBind();

            ListItem li = new ListItem("====请选择书本===", "-1");
            DropDownList2.Items.Insert(0, li);
            DropDownList2.SelectedIndex = 0;
        }
        
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            DropDownList1.DataSource = Ia.getBookVolumes(userId, Convert.ToInt32(DropDownList2.SelectedValue));
            DropDownList1.DataTextField = "ValumeName";
            DropDownList1.DataValueField = "VolumeId";
            DropDownList1.DataBind();
       
    }


    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        int volumeId = Convert.ToInt32(DropDownList1.SelectedValue);
        string context = FCKeditor1.Value;
        string title = sectionname.Text;
        int cl = context.Length;
        int count = Ia.Auaddsection(volumeId, title, cl, context);

        if (count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('章节添加成功，请等待管理员审核！');</script>", false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "err", "<script type='text/javascript'>alert('章节添加失败！');</script>", false);
        }
    }

}