using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using IDAL;

public partial class UserControls_BookTicket : System.Web.UI.UserControl
{
    LinkButton lbticket = null;

    INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

            List<Model.BooksInfo> listTicket = novel.getTicketCompositor();

            girdBookTicket.DataSource = listTicket;
            girdBookTicket.DataBind();
            

    }
    protected void weeks_Click(object sender, EventArgs e)
    {
        lbticket = (LinkButton)this.FindControl("flower");
        lbticket.ForeColor = Color.Black;
        LinkButton lb = (LinkButton)sender;
        lb.ForeColor = Color.Red;
        List<Model.BooksInfo> listTicket = novel.getTicketCompositor();
        girdBookTicket.DataSource = listTicket;
        girdBookTicket.DataBind();

    }
    protected void flower_Click(object sender, EventArgs e)
    {
        lbticket = (LinkButton)this.FindControl("ticket");
        lbticket.ForeColor = Color.Black;

        LinkButton lb = (LinkButton)sender;
        lb.ForeColor = Color.Red;
        List<Model.BooksInfo> listFlower = novel.getFlowerCompositor();
        girdBookTicket.DataSource = listFlower;
        girdBookTicket.DataBind();

    }
    protected void girdBookTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover","this.style.backgroundColor='#D6D6D6';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        }
    }
}