using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;

using MyExtension;
public partial class Admin_Process_DelBookReplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        string ReplayIDs = Request["id"];
        string[] replayIds = ReplayIDs.Substring(0, ReplayIDs.Length - 1).Split(',');
        try
        {
            int i;
            for (i = 0; i < replayIds.Length; i++)
            {
                novel.DelBookReplay(Convert.ToInt32(replayIds[i]));
            }
            Response.Write("{success:true,msg:'操作成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message.ToStr();
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}