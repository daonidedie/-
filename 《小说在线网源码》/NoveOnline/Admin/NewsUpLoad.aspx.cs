using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
public partial class NewsUpLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filename = String.Empty;
        string strpath = String.Empty;
        try
        {

            HttpFileCollection files = Request.Files; //获取客户端表单中的所有上传文件

            for (int i = 0; i < files.Count; i++)
            {

                HttpPostedFile hpf = files[i];

                filename = Path.GetFileName(hpf.FileName);//获取文件名称

                strpath = System.Web.HttpContext.Current.Server.MapPath("~/Images/Novelnews/" + filename);

                hpf.SaveAs(strpath); //保存文件

            }

        }

        catch
        {

            Response.Write("{\"success\":\"false\",\"message\":\"文件上传失败！\"}");


        }

        Response.Write("{\"success\":\"true\",\"message\":\"" + filename + "\"}");
    }
}