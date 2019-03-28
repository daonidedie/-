using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class payMoney : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["NewUser"] != null)
        {
            List<Model.UsersInfo> list = (List<Model.UsersInfo>)Session["NewUser"];
            uname.Text = list[0].UserName;
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();

        int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        int money = Convert.ToInt32(txMoney.Text);

        int count = iu.UserPayMoney(userId, money);
        if (count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('冲值成功！');</script>", false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "no", "<script type='text/javascript'>alert('冲值失败！');</script>", false);
        }
        
    }
}