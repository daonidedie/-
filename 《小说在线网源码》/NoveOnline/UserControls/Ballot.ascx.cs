using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class UserControls_Ballot : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IProp ip = BllFactory.BllAccess.CreateIPropBLL();
        gridHotPropss.DataSource = ip.getHotProps();
        gridHotPropss.DataBind();
        
    }
}