using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropControls_PropList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IProp IP = BllFactory.BllAccess.CreateIPropBLL();
        int RecordCount = 0;
        int pageIndex = 1;
        string pageurl = "?page=";
        if (Request.QueryString["page"] != null)
        {
            pageIndex = Convert.ToInt32(Request.QueryString["page"]);
        }

        repProp.DataSource = IP.getProps(6, pageIndex, out RecordCount);
        repProp.DataBind();

        int pagenumber = (int)Math.Ceiling((double)RecordCount / 6);


        lblPropNumber.Text = "商品总数：" + RecordCount +" 共" + pagenumber + "页";
        lblpageNumber.Text = "当前：第" + pageIndex + "页";
        FirstPage.NavigateUrl = Request.Url.AbsolutePath + pageurl + 1;
        NewxtPage.NavigateUrl = Request.Url.AbsolutePath + pageurl + (pageIndex + 1);
        PrvPage.NavigateUrl = Request.Url.AbsolutePath + pageurl + (pageIndex - 1);
        LastPage.NavigateUrl = Request.Url.AbsolutePath + pageurl + pagenumber;

        if (pageIndex == 1)
        {
            FirstPage.Visible = false;
            PrvPage.Visible = false;
        }

        if (pageIndex == pagenumber)
        {
            NewxtPage.Visible = false;
            LastPage.Visible = false;
        }

        
    }

    protected void repProp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HyperLink hyp = (HyperLink)e.Item.FindControl("hypls");
       
        
         string pid = hyp.NavigateUrl;
         string cartID;

         if (HttpContext.Current.User.Identity.Name != "")
         {
             cartID = BLL.ShopCartAccess.getCartID;
         }
         else
         {
             cartID = "nologin";
         }
        
         string test = "return Islogin(" + pid + ",'" + cartID + "');";
         hyp.Attributes.Add("onclick", "return Islogin(" + pid + ",'" + cartID + "');");
         
    }
}