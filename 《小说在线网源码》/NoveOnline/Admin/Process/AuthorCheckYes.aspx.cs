using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Process_AuthorCheckYes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();
       
        string userID = Request["ids"].ToString();
        userID = userID.Remove(userID.Length - 1,1); //移掉最后一个逗号

        string[] uidstring = userID.Split(',');
        int[] UserSID = new int[uidstring.Length];
        
        for(int i = 0;i<UserSID.Length;i++)
        {
            UserSID[i] = Convert.ToInt32(uidstring[i]);
        }

        int count = ia.AuthorCheckYes(UserSID);

        if(count > 0)
        {
            Response.Write("{success:true,msg:'操作成功'}");
        }
        else {
            Response.Write("{success:false,msg:'操作失败'}");
        }
    }
}