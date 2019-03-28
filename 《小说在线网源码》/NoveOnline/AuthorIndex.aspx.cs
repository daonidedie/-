using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AuthorIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (HttpContext.Current.User.Identity.Name == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "nouesr", "<script type='text/javascript'>alert('您还没有登陆');top.location='Default.aspx';</script>");
        }
        else
        {

            string url = Request.Url.AbsoluteUri;
            int index = url.LastIndexOf("/");
            url = url.Substring(0, index);
            url = url + "/Default.aspx";

            lbtime.Text = DateTime.Now.ToString("今天：yyyy年MM月dd日");
            List<Model.UsersInfo> list = (List<Model.UsersInfo>)Session["NewUser"];

            if (list[0].UserType < 3 || list[0].UserType > 4)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "nouesr", "<script type='text/javascript'>alert('您不是本站作者！');top.location='Default.aspx';</script>");
                return;
            }


            //生成用户控件
            Control c = null;
            if (Request.QueryString["type"] == null)
            {
                c = this.LoadControl(@"~/BookControls/addNewBook.ascx");
                AuthorContorls.Controls.Add(c);
            }
            else
            {
                int type = Convert.ToInt32(Request.QueryString["type"]);
                switch (type)
                {
                    case 1:
                        c = this.LoadControl(@"~/BookControls/addNewBook.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;
                    case 2:
                        c = this.LoadControl(@"~/BookControls/BooksEdit.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;

                    case 3:
                        c = this.LoadControl(@"~/BookControls/addNewSeciont.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;

                    case 4:
                        c = this.LoadControl(@"~/BookControls/shSeciont.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;

                    case 5:
                        c = this.LoadControl(@"~/BookControls/mybook.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;
                    case 6:
                        c = this.LoadControl(@"~/BookControls/getMoney.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;
                    default:
                        c = this.LoadControl(@"~/BookControls/addNewBook.ascx");
                        AuthorContorls.Controls.Add(c);
                        break;
                }

            }

            //在这里加代码


        }
    }
}