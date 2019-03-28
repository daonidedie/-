using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class err : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["err"] != null)
        {
            int type = Convert.ToInt32(Request.QueryString["err"]);

            switch(type)
            {
                case 1:
                    Response.Write("请不要乱搞！");
                    break;
                case 2:
                    Response.Write("你再这样，我就要查你咯！");
                    break;
            }
                
        }

    }
}