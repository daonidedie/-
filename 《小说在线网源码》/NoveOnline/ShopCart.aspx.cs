using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ShopCart : System.Web.UI.Page
{

    decimal allprice;

    IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        string userID = string.Empty;

        if (Request.QueryString["UID"] == null)
        {
            Response.Redirect("err.aspx?err=1");
        }


        if (Request.QueryString["UID"] != null)
        {

            if (HttpContext.Current.User.Identity.Name != "")
            {
                userID = HttpContext.Current.User.Identity.Name;
                List<Model.UsersInfo> list = iu.getBookMoney(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                repallmoney.DataSource = list;
                repallmoney.DataBind();
            }

            if (Request.QueryString["UID"].ToString() != userID)
            {
                Response.Redirect("err.aspx?err=2");
            }
        }





        if (!IsPostBack)
        {
            int recordCount = 0;
            repshopcart.DataSource = iu.getUserShopCart(BLL.ShopCartAccess.getCartID, 1, 6, out recordCount);
            repshopcart.DataBind();

            if (recordCount == 0)
            {
                this.form1.Visible = false;
                this.FindControl("noprop").Visible = true;
            }

            

            Label lballprice = (Label)this.FindControl("lballprice");
            lballprice.Text = "需支付书币：" + allprice.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;";
        }


    }
   

    protected void repshopcart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delProp")
        {
            int proid = Convert.ToInt32(e.CommandArgument);
            string CartId = BLL.ShopCartAccess.getCartID;

            int count = iu.deleteProp(CartId, proid);

            Response.Redirect(Request.RawUrl);

        }

        
    }


    //修改数量
    protected void txPropNumber_TextChanged(object sender, EventArgs e)
    {
        WebControlExtension.TextBoxEx tx = (WebControlExtension.TextBoxEx)sender;
        int propid = Convert.ToInt32(tx.IDValue);
        int propNumber = Convert.ToInt32(tx.Text);
        string cartId = BLL.ShopCartAccess.getCartID;

        int count = iu.updateProp(cartId, propid, propNumber);
        
        Response.Redirect(Request.RawUrl);
    }

    protected void repshopcart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {


        Label lk = (Label)e.Item.FindControl("lblPrice");
        string pricetext = lk.Text.ToString().Replace("书币", "");
        decimal aprice = Convert.ToDecimal(pricetext);

        allprice += aprice;
    }


    protected void enterPrice_Click(object sender, EventArgs e)
    {
        //支付

        //获取用户购物车
        int recordCount;
        string cartID = BLL.ShopCartAccess.getCartID;
        int userid = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        List<Model.ShoppingCartInfo> shopcart = iu.getUserShopCart(cartID, 1, 6, out recordCount);

        string parentURl = Request.UrlReferrer.ToString() + "?isok=1&userid=" + userid;
        parentURl = parentURl.Replace("ShopCart.aspx", "PropIndex.aspx");

        foreach (Model.ShoppingCartInfo item in shopcart)
        {
            iu.payProps(cartID, userid, item.PropId, item.Propnumber, Convert.ToInt32(item.Propnumber * item.PropPrice));
        }

        ClientScript.RegisterClientScriptBlock(this.GetType(), "close",
            "<script type='text/javascript'>alert('支付成功，确认查看道具！');var diag = Dialog.getInstance('Diag');diag.close();diag.TopWindow.location.href='"+ parentURl +"';</script>", false);

        
    }
}