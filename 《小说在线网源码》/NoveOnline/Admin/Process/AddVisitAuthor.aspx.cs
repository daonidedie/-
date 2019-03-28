﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using MyExtension;

public partial class Admin_Process_AddVisitAuthor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Model.VisitAuthorInfo item = new Model.VisitAuthorInfo();
            item.UserId = Convert.ToInt32(Request["UserId"]);
            item.VisitTitle = Request["VisitTitle"].ToString();
            item.Contents = Request["Contents"].ToString();
            item.Contents = Server.HtmlEncode(item.Contents);

            item.VisitDate = DateTime.Now.ToShortDateString();

            IUsers user = BllFactory.BllAccess.CreateIUsersBLL();

            try
            {
                user.ModifyVisitAuthor(item, false);
                Response.Write("{success:true,msg:'添加成功'}");
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToStr();
                Response.Write("{success:false,msg:'" + msg + "'}");
            }
        }
    }
}