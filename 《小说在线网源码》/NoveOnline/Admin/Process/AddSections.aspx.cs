using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using IDAL;
using MyExtension;
public partial class Admin_Process_AddSections : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();

        int BookId = Convert.ToInt32(Request["BookId"]);
        int VolumeId = novel.AddBookIdSections(BookId);

        Model.SectionsInfo item = new Model.SectionsInfo();
        item.VolumeId = VolumeId;
        item.SectionTitle = Request["VolumeName"];
        item.CharNum = Request["Contents"].ToString().Length;
        item.Contents = Request["Contents"];
        item.Contents = Server.HtmlEncode(item.Contents);
        try
        {
            novel.addSections(item);
            Response.Write("{success:true,msg:'添加成功'}");
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            Response.Write("{success:false,msg:'" + msg + "'}");
        }
    }
}