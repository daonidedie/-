/******************************************************
* 文件名：UrlReWrite.cs
* 文件功能描述：URL重写类
* 
* 创建标识：周渊 2007-10-4
* 
* 修改标识：周渊 2008-4-16
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using System.Configuration;
using System.Web;

//2007-10-4 加入
namespace Skybot.Tong
{
    #region URL从写类 命名空间 Bocom.Project.Library

    /// <summary>
    /// UrlRewriter URL重写类
    /// </summary>
    /// <![CDATA[
    ///  from blog:http://yangmingsheng.cn
    /// 试例
    /// 要在 web.Config 中 配置 
    /// <configuration xmlns="http://schemas.microsoft.com/.NetConfiguration/v2.0">
    /// <appSettings>
    ///     <!-- 开始设置 URL 重写 配置文件-->
    /// 
    ///     <add key="urlRewriter" value="~/App_Data/URLRewriterSettings.xml"/>
    /// 
    ///     用于指定URL配置文件路径
    /// 
    ///     <!-- 设置 URL 重写配置文件 结束-->
    ///    <appSettings/> 
    ///   </configuration>
    /// 
    /// 
    /// 	<system.web>
    ///    <httpHandlers>
    ///  <!-- 开始设置 URL 重写-->
    ///  <add verb="*" path="AspNetPaperList/List*.aspx" type="UrlRewriter"  ></add>
    ///   用于指定URL从写 请求的 文件表达式, 即 哪些文件要用到 url重写
    ///  <!-- 设置 URL 重写结束-->
    ///     </httpHandlers>
    ///</system.web>
    /// 
    /// 
    ///  URLRewriterSettings.xml 内容 
    /// <?xml version="1.0" encoding="utf-8" ?>
    ///<!--如有多个匹配，则优先级向下递减-->
    ///<!--http://localhost:6679/httphandleUrlRewrite/22.html-->
    ///<!--http://localhost:6679/httphandleUrlRewrite/a/b/c.html-->
    ///<!--如有多个匹配，则优先级向下递减-->
    ///<URLRewriters>
    ///  <URLRewriters Path="~/AspNetPaperList/List(\d+).aspx" RealPath="~/AspNetPaperList/PageList.aspx?page=$1" />
    ///  <URLRewriters Path="~/([^/]*)/([^/]*)/([^/]*)/([^/]*).aspx" RealPath="~/aspx/show.aspx?par1=$1&amp;par2=$2&amp;par3=$3"/>
    ///</URLRewriters>
    /// 如果是 站点根目录 要用 Path="/......"
    /// 如果不是用 Path="~/......"
    /// 
    /// ]]>
    /// 
    /// 
    /// 
    /// 
    /// 
    ///
    public class UrlRewriter : IHttpHandler, System.Web.SessionState.IRequiresSessionState    //这个东东用于访问 session 状态
    {

        #region 构造函数
        /// <summary>
        /// 默认的函数
        /// </summary>
        public UrlRewriter()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 声明变量
        String Rh;

        #endregion






        #region IHttpHandler 成员


        /// <summary>
        ///  通过实现 System.Web.IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
        /// </summary>
        /// <param name="context">System.Web.HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session  和 Server）的引用。</param>
        public void ProcessRequest(HttpContext context)
        {
            //取得原始URL屏蔽掉参数
            string Url = context.Request.RawUrl;

            System.Xml.XmlDocument Dom = new System.Xml.XmlDocument();
            Dom.Load(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["urlRewriter"]));//从web.config读取设置文件URLRewriterSettings.xml的路径
            System.Xml.XmlNodeList ItemList = Dom.SelectSingleNode(@"//URLRewriters").ChildNodes;
            foreach (System.Xml.XmlNode Node in ItemList)
            {
                //if(!Node.HasChildNodes) continue;
                String Ph = "";
                Ph = Node.Attributes.GetNamedItem(@"Path").InnerText;
                Rh = Node.Attributes.GetNamedItem(@"RealPath").InnerText;
                if (Ph.StartsWith(@"~")) Ph = HttpContext.Current.Request.ApplicationPath + Ph.Substring(1);
                if (Rh.StartsWith(@"~")) Rh = HttpContext.Current.Request.ApplicationPath + Rh.Substring(1);
                //建立正则表达式
                System.Text.RegularExpressions.Regex Reg = new System.Text.RegularExpressions.Regex(Ph, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Match m = Reg.Match(Url);//匹配
                if (m.Success)//成功
                {
                    for (int i = 1; i <= m.Groups.Count; i++)
                    {
                        Rh = Rh.Replace("$" + i.ToString(), m.Groups[i].Value);
                    }

                    //Context.Response.Write(Context.Request.Url.PathAndQuery);
                    //Context.RewritePath(RealPath);//(RewritePath 用在无 Cookie 会话状态中。)

                    context.Server.Execute(Rh);
                    //Context.Response.Write(Rh);
                    context.Response.End();
                    break;
                }

            }
            try
            {
                context.Server.Execute(context.Request.Url.PathAndQuery);

            }
            catch
            {
                TongUse TextData = new TongUse();
                // TextData.Err("在获取文件时出现错误,文件没有找到"+ex.Message);
                TextData.Res.Response.Write(context.Request.Url.PathAndQuery);
                context.Response.End();
            }
        }
        /// <summary>
        /// 获取一个值，该值指示其他请求是否可以使用 System.Web.IHttpHandler 实例。
        /// </summary>
        /// <value>返回结果:如果 System.Web.IHttpHandler 实例可再次使用，则为 true；否则为 false。</value>
        /// 
        ///
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion
    }
    #endregion
}
