using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using DalFactory;

public partial class UserControls_SectionRepDisplay : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();

        int count;
        rep1.DataSource = novel.getNewSections(Common.PMethod.GetRand(1, 50), 4, out count);
        rep1.DataBind();
        
    }
}