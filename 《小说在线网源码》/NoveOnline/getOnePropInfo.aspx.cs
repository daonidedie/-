using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class getOnePropInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IProp ip = BllFactory.BllAccess.CreateIPropBLL();

        int pid = Convert.ToInt32(Request.QueryString["PID"]);
        List<Model.PropInfo> list = ip.getOnePropInfo(pid);

        repaProp.DataSource = list;
        repaProp.DataBind();
       

    }
}