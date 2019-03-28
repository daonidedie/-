using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 中转页面 用于生成静态页面
/// </summary>
public partial class Site_MulticastTransformers : System.Web.UI.Page
{

    /// <summary>
    /// 转到指定路径
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string url = "";
            url = Request.QueryString["url"].Trim();

            Server.Transfer(url);
        }
        catch { }
    }
}