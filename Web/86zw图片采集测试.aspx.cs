using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _86zw图片采集测试 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = "http://www.86zw.com/Html/Book/29/29532/3834166.shtml";
        System.Net.WebClient wc = new System.Net.WebClient();
        wc.Headers["Referer"] = url;
       Response.BinaryWrite( wc.DownloadData("http://c.f776.com/attachment/Mon_1208/23/1166861_0.gif"));
        wc.Dispose();
       
    }
}