<%@ WebHandler Language="C#" Class="PicProxy" %>

using System;
using System.Web;
using System.Linq;
using Skybot.Cache;
public class PicProxy : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        string url = string.Empty;
        string u = url;
        url = context.Request["url"];
        u = context.Request["u"];
        if (!string.IsNullOrEmpty(url))
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            //获得访问的URL
            wc.Headers["Referer"] = "http://www.86zw.com/Html/Book/29/29532/3834166.shtml";
            try
            {
                context.Response.BinaryWrite(wc.DownloadData(url));
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Message.Contains("404"))
                {
                    if (u.Contains("86zw.com"))
                    {
                        Guid guid;
                        if (Guid.TryParse(context.Request["guid"],out guid))
                        {
                            string html = wc.DownloadString(u);
                            HtmlAgilityPack.HtmlDocument hdom = new HtmlAgilityPack.HtmlDocument();
                            hdom.LoadHtml(html);
                            //原素
                            HtmlAgilityPack.HtmlNode node = hdom.GetElementbyId("BookText");
                            //如果有有效数据 
                            if (node != null)
                            {
                                using (TygModel.Entities entity = new TygModel.Entities())
                                {
                                    string tempid=guid.ToString();
                                    var query = entity.文章表.Where(p => p.本记录GUID == Guid.Parse(tempid));
                                    if (query.Count() > 0)
                                    {
                                        var record = query.FirstOrDefault();
                                      //如果内容无效
                                        if (record.内容.Length <= 200 ||
                                            //中文小于60
                                          string.Join("", System.Text.RegularExpressions.Regex.Matches(record.内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                                            .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                                            ).Length < 60
                                            )
                                        {
                                            //更新内容
                                            record.内容 = node.InnerHtml;
                                            context.Response.Write("更新内容");
                                        }
                                        //生成HTML
                                        wc.Encoding = System.Text.Encoding.UTF8;
                                        record.CreateStaticHTMLFile(
                                            wc.DownloadString(string.Format("http://localhost/Site/ShowDoc.aspx?guid={0}&html=true", tempid))
                                           );
                                        context.Response.Write("生成HTML");
                                        
                                    }
                                    entity.SaveChanges();

                                }
                            }
                        }
                    }
                }
                context.Response.Write(ex.Message);
            }
            wc.Dispose();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}