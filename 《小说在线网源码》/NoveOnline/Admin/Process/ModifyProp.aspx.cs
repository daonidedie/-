using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using MyExtension;
public partial class Admin_Process_ModifyProp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Model.PropInfo p = new Model.PropInfo();
            p.PropId = Convert.ToInt32(Request["PropId"]);
            p.PropName = Request["PropName"].ToString();
            p.PropIntroduction = Request["PropIntroduction"].ToString();
            p.PropPrice = Convert.ToDecimal(Request["PropPrice"]);
            p.PropImage = Request["PropImage"].ToString();
            p.createDate = DateTime.Now.ToString();
            p.outNumber = Convert.ToInt32(Request["outNumber"]);

            IProp ip = BllFactory.BllAccess.CreateIPropBLL();
            try
            {
                ip.updatePropInfo(p);
                Response.Write("{success:true,msg:'修改成功'}");
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToStr();
                Response.Write("{success:false,msg:'" + msg + "'}");
            }
            
        }
    }
}