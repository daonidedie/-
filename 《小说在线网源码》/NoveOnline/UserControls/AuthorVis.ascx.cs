using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using DalFactory;

public partial class UserControls_AuthorVis : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IUsers user = BllFactory.BllAccess.CreateIUsersBLL();
        rep1.DataSource = user.getVisitAuthor(1);
        rep1.DataBind();
        

    }
}