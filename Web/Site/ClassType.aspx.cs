using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygModel;
using Skybot.Cache;
using Skybot.Tong;
public partial class Site_ClassType : System.Web.UI.Page
{
    /// <summary>
    /// 是不是创建HTML静态页面
    /// </summary>
    public bool IsCresteHTMLPage=true;

    /*
     第01步、内容页的 Page_PreInit
第02步、母版页的 Page_Init
第03步、内容页的 Page_Init
第04步、内容页的 Page_InitComplete
第05步、内容页的 Page_PreLoad
第06步、内容页的 Page_Load
第07步、母版页的 Page_Load
第08步、母版页或内容页的 按钮点击等回发事件（Master或Content的Button事件不会同时触发）
第09步、内容页的 Page_LoadComplete
第10步、内容页的 Page_PreRender
第11步、母版页的 Page_PreRender
第12步、内容页的 Page_PreRenderComplete
第13步、内容页的 Page_SaveStateComplete
第14步、母版页的 Page_Unload
第15步、内容页的 Page_Unload
     */
    /// <summary>
    /// 图书名称
    /// </summary>
    public string BookName = "";

    /// <summary>
    /// 创建者
    /// </summary>
    public string Creater = "";


    /// <summary>
    /// 最后更新时间
    /// </summary>
    public string LastTime = "";


    /// <summary>
    /// 数据库访问对像
    /// </summary>
    TygModel.Entities Tygdb = new Entities();

    /// <summary>
    /// 当前书名表
    /// </summary>
    public 书名表 currentBook = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        docList.DataSource = Tygdb.分类表;
        docList.DataBind();
    }

    /// <summary>
    /// 内容状态保存时发生
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Page_SaveStateComplete(object sender, EventArgs e)
    {
        //释放资源
        Tygdb.Dispose();
    }



}