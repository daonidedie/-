using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class author_EditVolume : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();

        //添加
        //int count = ia.EidtVolume(TextBox1.Text);
    }
}