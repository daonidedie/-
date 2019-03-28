using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using MyExtension;
public partial class Admin_Process_AddVolume : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Model.VolumeInfo item = new Model.VolumeInfo();
        item.BookId = Convert.ToInt32(Request["BookId"]);
        item.ValumeName = Request["VolumeName"];

        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        try
        {
            novel.addVolume(item);
            Response.Write("{success:true,msg:'添加成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}