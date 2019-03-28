using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;

public partial class UserControls_BookType : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            INovel novel = BllFactory.BllAccess.CreateINovelBLL();
            RebookType.DataSource = novel.getNovelType();
            RebookType.DataBind();
            
    }
}