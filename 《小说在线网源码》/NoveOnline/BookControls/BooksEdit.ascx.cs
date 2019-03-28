using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_BooksEdit : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IAuthor IA = BllFactory.BllAccess.CreateIAuthorBLL();
        int userId = -1;

        Control c = null;

        userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

        if (Request.QueryString["ct"] == null)
        {
            c = this.LoadControl(@"~/BookControls/noselect.ascx");
            divedit.Controls.Add(c);
        }
        else
        {
            int ct = Convert.ToInt32(Request.QueryString["ct"]);
            switch (ct)
            {
                case 1:
                    c = this.LoadControl(@"~/BookControls/bookface.ascx");
                    divedit.Controls.Add(c);
                    break;
                case 2:
                    c = this.LoadControl(@"~/BookControls/bookvolume.ascx");
                    divedit.Controls.Add(c);
                    break;
                case 3:
                    c = this.LoadControl(@"~/BookControls/booksections.ascx");
                    divedit.Controls.Add(c);
                    break;
                default:
                    c = this.LoadControl(@"~/BookControls/noselect.ascx");
                    divedit.Controls.Add(c);
                    break;
            }
        }

        repbooks.DataSource = IA.getAuthorBooks(userId);
        repbooks.DataBind();

        
    }


}