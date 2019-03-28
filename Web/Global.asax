<%@ Application Language="C#" %>

<script RunAt="server">




    void Application_Start(object sender, EventArgs e)
    {
        //注册URL重写
        RegRoute(System.Web.Routing.RouteTable.Routes);


        System.Threading.Tasks.Task.Factory.StartNew(() =>
        {
             
            预计分页读取();
        });
    }




    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }


    public void 预计分页读取()
    {
        using (TygModel.Entities entiys = new TygModel.Entities())
        {
            Skybot.Tong.GetWebData getWeb = new Skybot.Tong.GetWebData();
            //string url="http://localhost/Book/ZhuaiShiChongSheng/{0}/1.html";
            string url = System.Configuration.ConfigurationManager.AppSettings["baseSite"] + "/Book/ZhuaiShiChongSheng/{0}/1.html";
            //循环ID
            entiys.分类表.Select(p => p.ID).Take(6).ToList().ForEach((o) =>
            {
                //分页1
                getWeb.GetWebDate(string.Format(url, o), "UTF-8");

            });

            //分页1
            getWeb.GetWebDate(string.Format(url, -1), "UTF-8");

            //分页1
            getWeb.GetWebDate(string.Format(url, -2), "UTF-8");

            //分页1
            getWeb.GetWebDate(string.Format(url, -3), "UTF-8");
        }

    }


    /// <summary>
    /// 注册URL重写 路由
    /// </summary>
    /// <param name="routes">路由集合</param>
    public void RegRoute(System.Web.Routing.RouteCollection routes)
    {
        //忽略路由
        //routes.Ignore("{resource}.axd/{*pathInfo}");
        if (System.Configuration.ConfigurationManager.AppSettings["启用Qlili获取数据"] == "0")
        {
            //url分页
            routes.MapPageRoute("BookList",
                "Book/{PingYing}/{type}/{page}.aspx",
                "~/Site/BookList.aspx",
                 false,//无访问权限控制
                //配置默认值
                 new System.Web.Routing.RouteValueDictionary() { 
                 {"type",string.Empty},
                 {"page",string.Empty}
                 },
                //参数的规则
                 new System.Web.Routing.RouteValueDictionary() {
                 {"type",@"[\d-]{1,}"},
                 {"page",@"\d{1,}|index"}
                 }
                 );
        }
        else
        {
            routes.MapPageRoute("BookList",
            "Book/{PingYing}/{type}/{page}.aspx",
            "~/Site/SusuCong/SuSuCongBookList.aspx",
             false,//无访问权限控制
                //配置默认值
             new System.Web.Routing.RouteValueDictionary() { 
             {"type",string.Empty},
             {"page",string.Empty}
             },
                //参数的规则
             new System.Web.Routing.RouteValueDictionary() {
             {"type",@"[\d-]{1,}"},
             {"page",@"\d{1,}|index"}
             }
             );
        }
        //带搜索功能的分页
        routes.MapPageRoute("SearchBook",
           "Search/{PingYing}/{keyword}/{page}.aspx",
          // "~/Site/BookList.aspx",
          "~/Site/SusuCong/SuSuCongBookList.aspx",
            false,//无访问权限控制
            //配置默认值
            new System.Web.Routing.RouteValueDictionary() { 
             {"PingYing","SearchBooks"},
             {"keyword",string.Empty},
             {"page",string.Empty}
             },
            //参数的规则
            new System.Web.Routing.RouteValueDictionary() {
             //{"keywork",@"(%[0-9a-zA-Z]{1}){2,600}"},
             {"page",@"\d{1,}|index"}
             }
            );



    }
    
</script>
