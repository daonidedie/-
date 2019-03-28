using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserHaveProp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();

        if (!IsPostBack)
        {
            int pageIndex = 1;
            int recordCount = 0;
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string url = Request.Url.AbsolutePath + "?page=";

            if (Request.QueryString["page"] != null)
            {
                pageIndex = Convert.ToInt32(Request.QueryString["page"]);
            }
            repHaveProp.DataSource = iu.getUserHaveProp(userId, pageIndex, 6, out recordCount);
            repHaveProp.DataBind();

            
            List<Model.UsersInfo> list = iu.getBookMoney(userId);

            int pageNumber = (int)Math.Ceiling((double)recordCount / 6);

            if (recordCount == 0)
            {
                lblUserProp.Text = "&nbsp&nbsp&nbsp&nbsp您目前没有任何道具！" + "书币剩余：" + Convert.ToInt32(list[0].BookMoney) + "&nbsp;&nbsp;&nbsp;&nbsp;";
                LastPage.Visible = false;
                NewxtPage.Visible = false;
            }
            else
            {
                lblUserProp.Text = "&nbsp&nbsp&nbsp&nbsp您共有" + recordCount + "种道具，书币剩余：" + Convert.ToInt32(list[0].BookMoney) + "&nbsp;&nbsp;&nbsp;&nbsp;当前第 " + pageIndex + "/" + pageNumber + " 页";
            }

            FirstPage.NavigateUrl = url + 1;
            NewxtPage.NavigateUrl = url + (pageIndex + 1);
            PrvPage.NavigateUrl = url + (pageIndex - 1);
            LastPage.NavigateUrl = url + pageNumber;

            if (pageIndex == 1)
            {
                FirstPage.Visible = false;
                PrvPage.Visible = false;
            }

            if (pageIndex == pageNumber)
            {
                NewxtPage.Visible = false;
                LastPage.Visible = false;
            }
        }

    }
    protected void repHaveProp_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        //获取用户类型
        List<Model.UsersInfo> userList = (List<Model.UsersInfo>)Session["NewUser"];

        if (e.CommandName == "useProp")
        {
            int propId = Convert.ToInt32(e.CommandArgument);
            IDAL.IUsers iu = BllFactory.BllAccess.CreateIUsersBLL();
            IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();
            int userID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int isAuCheck = iu.IsCheckAuthor(userID);
            switch (propId)
            {
                //VIP卡使用
                case 1:
                    if (userList[0].UserType >= 2 && userList[0].UserType < 5)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('您已经具备VIP资格了，不需要使用该道具！');window.location.href=window.location.href;</script>", false);
                    }
                    else
                    {
                        int count = novel.userVIPCard(userID);
                        if (count > 0)
                        {
                            Common.UserPanel us = new Common.UserPanel();
                            us.userExit();
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>var dialog = Dialog.getInstance('Diag');dialog.close();dialog.TopWindow.location.href=dialog.TopWindow.location.href;alert('恭喜您成为本站VIP,重新登陆后生效！');</script>", false);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('道具使用出错，请联系管理员。');window.location.href=window.location.href;</script>", false);
                        }
                    }
                    break;
                case 2:
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('该道具不能直接使用，请在书本封面页点击<订阅连载>！');window.location.href=window.location.href;</script>", false);
                    break;
                case 3:
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('该道具不能直接使用，请在书本封面页点击<投书票>！');window.location.href=window.location.href;</script>", false);
                    break;
                case 4:
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('该道具不能直接使用，请在书本封面页点击<送鲜花>！');window.location.href=window.location.href;</script>", false);
                    break;
                case 5:
                    if (userList[0].UserType == 3 && userList[0].UserType != 5)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('您已经是本站作者了，不需要使用该道具！');window.location.href=window.location.href;</script>", false);
                    }
                    else if (isAuCheck > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('已经提交过，不需要重复使用道具！');window.location.href=window.location.href;</script>", false);
                    }
                    else
                    {
                        int count = iu.AuthorCard(userID);
                        if (count > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('道具使用成功,请等待管理员审核！');window.location.href=window.location.href;</script>", false);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('道具使用失败，请联系管理员！');window.location.href=window.location.href;</script>", false);
                        }
                    }
                    break;
                default:
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "isvip", "<script type='text/javascript'>alert('该道具正在开发中！');window.location.href=window.location.href;</script>", false);
                    break;
            }
               
        }
    }
}