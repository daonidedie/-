using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;

public partial class UserControls_VisitAuthor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            IUsers users = BllFactory.BllAccess.CreateIUsersBLL();
            repVisitAuthor.DataSource = users.getVisitAuthor();
            repVisitAuthor.DataBind();
            

    }
}