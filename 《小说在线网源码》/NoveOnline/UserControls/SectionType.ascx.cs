using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using DalFactory;

public partial class UserControls_SectionType : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();

        rep1.DataSource = novel.getNovelType();
        rep1.DataBind();
        
    }
}