using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
      Server.Transfer("/file/index.htm");
        //Server.Transfer("/Site/BookList.aspx");
      // Response.Redirect("/Site/BookList.aspx");

       // TygModel.Entities etni = new TygModel.Entities();
        //Response.Write(etni.分类表.Count());


    }


}