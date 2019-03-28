using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Skybot.Collections.Content;

using System.Linq;
using Mozilla.NUniversalCharDet;
using System.Text.RegularExpressions;
using System.Text;
using Skybot.Collections.Analyse;
using System.Collections.Generic;



namespace Skybot.Collections
{
    /// <summary>
    /// 得到HTTP内容的帮肋
    /// </summary>
    public static class GetHttpContentHepler
    {

        /// <summary>
        /// HttpWebRequest读取网页 字符集将自动匹配如果找不倒指定字符集，则使用utf-8
        /// </summary>
        /// <param name="url">url</param>
        public static string GetWeb(this Uri url)
        {
            return GetWeb(url.ToString());
        }
        /// <summary>
        /// HttpWebRequest读取网页 字符集将自动匹配如果找不倒指定字符集，则使用utf-8
        /// </summary>
        /// <param name="url">url</param>
        public static string GetWeb(this string url)
        {
            string cont = GetWeb(url, "UTF-8");
            if (System.Text.RegularExpressions.Regex.Match(cont, @"�����").Value.Length == 0)
            {
                return cont;

            }
            else
            {

                cont = GetWeb(url, "GB2312");
                if (System.Text.RegularExpressions.Regex.Match(cont, @"�����").Value.Length == 0)
                {
                    return cont;
                }
                else
                {
                    return GetWeb(url, "");
                }
            }

        }





        /// <summary>
        /// HttpWebRequest读取网页 字符集将自动匹配如果找不倒指定字符集，则使用utf-8
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="parament">一个用于区分的参数 </param>
        private static string GetWeb(string url, string encoding)
        {
            string strHtmlContent = "";
            System.IO.Stream mystream = new System.IO.MemoryStream();
            System.Net.HttpWebRequest myrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            try
            {
                //字符集编码

                if (url.IndexOf("http") == -1)//如果米有HTTP
                {
                    throw new Exception("请提供完整的HTTP地址");
                }
                
                myrequest.Timeout = 20 * 1000;//超时时间 20秒
                //设置没有缓存
                myrequest.Headers.Set("Pragma", "no-cache");
                System.Net.HttpWebResponse myresponse = null;
                if (myrequest.KeepAlive)
                {
                    try
                    {
                        myresponse = (System.Net.HttpWebResponse)myrequest.GetResponse();
                        mystream = myresponse.GetResponseStream();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(DateTime.Now + "获取网页内容出错：" + ex.Message + " " + (ex.StackTrace == null ? " " : " " + ex.StackTrace));

                        return strHtmlContent;
                    }

                    
                }
                //用于读取数据的内存流
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();

                #region 自动判断编码字符集

                //查看流长时是不是有效数据 
                int len = 0;
                byte[] buff = new byte[512];
                while ((len = mystream.Read(buff, 0, buff.Length)) > 0)
                {
                    memoryStream.Write(buff, 0, len);
                }

                if (memoryStream.Length > 0)
                {
                    //设置流指向头
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    int DetLen = 0;
                    //编码字符体的buffer 默认需要4KB的数据
                    byte[] DetectBuff = new byte[4096];
                    //开始取得编码
                    UniversalDetector Det = new UniversalDetector(null);
                    //从当前流中读取块并写入到buff中
                    while ((DetLen = memoryStream.Read(DetectBuff, 0, DetectBuff.Length)) > 0 && !Det.IsDone())
                    {
                        Det.HandleData(DetectBuff, 0, DetectBuff.Length);
                    }
                    Det.DataEnd();
                    //得到字符集合
                    if (Det.GetDetectedCharset() != null)
                    {
                        if (encoding == "")
                        {
                            //得到字符集
                            encoding = Det.GetDetectedCharset();
                        }
                    }
                    //设置流指向头
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                }

                #endregion
                System.Text.Encoding myencoding = System.Text.Encoding.GetEncoding(encoding);
                System.IO.StreamReader mystreamreader = new System.IO.StreamReader(memoryStream, myencoding);
                strHtmlContent = mystreamreader.ReadToEnd();
                mystreamreader.Dispose();
                if (myresponse != null)
                {
               
                    myresponse.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + "获取网页内容出错：" + ex.Message + " " + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
            }
            finally {
                mystream.Close();
                mystream.Dispose();
                // HttpWebRequest 不会自己销毁对象
                //销毁关闭连接
                myrequest.Abort();
            }
            return strHtmlContent;
        }

        /// <summary>
        /// 清理HTML标签的多余样式；如<div style="color:#454353">示例</div>;换成<div>示例</div>
        /// </summary>
        /// <param name="htmlText">原始文本</param>
        /// <param name="element">要清除的标签</param>
        /// <returns></returns>
        public static string FiltrateHTML(this string htmlText, System.Collections.Generic.IEnumerable<string> elements)
        {
            foreach (var el in elements)
            {
                string old = @"<" + el + "[^>]+>";
                string rep = "<" + el + ">";
                htmlText = Regex.Replace(htmlText, old, rep, RegexOptions.IgnoreCase);
            }
            return htmlText;
        }



        /// <summary>
        /// 中国大陆简体字转化为繁体字 返回汉字串的繁体
        /// </summary>
        /// <param name="Str">输入汉字简体字符串</param>
        /// <returns>返回汉字串的繁体</returns>
        public static string WordSimpleToTraditional(this string Str)
        {
            string S = Microsoft.VisualBasic.Strings.StrConv(Str, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, System.Globalization.CultureInfo.CurrentCulture.LCID);
            return S;
        }

        /// <summary>
        ///   繁体字转化为 中国大陆简体字返回汉字串的简体
        /// </summary>
        /// <param name="Str">输入汉字繁体字符串</param>
        /// <returns>返回汉字串的简体</returns>
        public static string WordTraditionalToSimple(this string Str)
        {
            string S = Microsoft.VisualBasic.Strings.StrConv(Str, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
            return S;
        }

        /// <summary>  
        /// 将中文数字转换成阿拉伯数字  
        /// </summary>  
        /// <param name="cnNumber"></param>  
        /// <returns></returns>  
        public static int ConverToDigit(this string cnNumber)
        {

            int result = 0;

            int temp = 0;


            int.TryParse(cnNumber, out result);

            foreach (char c in cnNumber)
            {

                int temp1 = ToDigit(c);

                if (temp1 == 10000)
                {

                    result += temp;

                    result *= 10000;

                    temp = 0;

                }

                else if (temp1 > 9)
                {

                    if (temp1 == 10 && temp == 0) temp = 1;

                    result += temp * temp1;

                    temp = 0;

                }

                else temp = temp1;

            }

            result += temp;

            return result;

        }



        /// <summary>  
        /// 将中文数字转换成阿拉伯数字  
        /// </summary>  
        /// <param name="cn"></param>  
        /// <returns></returns>  
        static int ToDigit(char cn)
        {

            int number = 0;

            switch (cn)
            {

                case '壹':

                case '一':

                    number = 1;

                    break;

                case '两':

                case '贰':

                case '二':

                    number = 2;

                    break;

                case '叁':

                case '三':

                    number = 3;

                    break;

                case '肆':

                case '四':

                    number = 4;

                    break;

                case '伍':

                case '五':

                    number = 5;

                    break;

                case '陆':

                case '六':

                    number = 6;

                    break;

                case '柒':

                case '七':

                    number = 7;

                    break;

                case '捌':

                case '八':

                    number = 8;

                    break;

                case '玖':

                case '九':

                    number = 9;

                    break;

                case '拾':

                case '十':

                    number = 10;

                    break;

                case '佰':

                case '百':

                    number = 100;

                    break;

                case '仟':

                case '千':

                    number = 1000;

                    break;

                case '萬':

                case '万':

                    number = 10000;

                    break;

                case '零':

                default:

                    number = 0;

                    break;

            }

            return number;

        }



        /// <summary>
        /// 看看集合是不是包含指定的url 条件
        /// 如果集合中包含则返回
        /// </summary>
        /// <param name="urls">搜索的集合</param>
        /// <param name="pointCondition">条件</param>
        /// <returns></returns>
        public static ListPageContentUrl ContetPageUrl(IEnumerable<ListPageContentUrl> urls, ListPageContentUrl pointCondition)
        {
            //结果url
            ListPageContentUrl url = null;
            //结果urls
            var resulturls = from p in urls
                             where p.index == pointCondition.index
                             select p;
            //如果有结果
            if (resulturls.Count() > 0)
            {
                foreach (var item in resulturls)
                {
                    //如果有相等的记录
                    if (item.Title.Trim() == pointCondition.Title.Trim())
                    {
                        url = item;
                        break;
                    }
                }

                if (url == null)
                {
                    url = resulturls.FirstOrDefault();
                }
            }

            return url;

        }

        /// <summary>
        /// 过滤文件名中的特殊字符
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string FilterSpecial(string fileName)
        {
            if (string.Empty == fileName)
            {

                return fileName;
            }
            string[] aryReg = { "'", "'delete", "?", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "_", ";", "||", "[", "]", "&", "/", "-", "|", " ", "''" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                fileName = fileName.Replace(aryReg[i], string.Empty);
            }
            return fileName;
        }


        /// <summary>
        /// 清空所有的在文章内容中没有用途的标签,返回过滤后的内容
        /// </summary>
        public static string ClearTags(string html)
        {
            HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(html);
            //开始移除标签
            RemovClildTags(dom.DocumentNode, new List<string>() { "a", "div", "script", "ul", "td", "table" });

            return dom.DocumentNode.InnerHtml;

        }
        /// <summary>
        /// 移除子标签
        /// </summary>
        /// <param name="parnode">原素</param>
        /// <param name="List">要移除的子标签名</param>
        public static void RemovClildTags(HtmlAgilityPack.HtmlNode parnode, List<string> TagNames)
        {
            //需要创建一个复本，然后才能移除，不然会出现集合已经修改的错误
            foreach (HtmlAgilityPack.HtmlNode node in parnode.ChildNodes.ToArray())
            {
                //移除标签
                if (TagNames.Contains(node.Name.Trim().ToLower()))
                {
                    //如果可能是内容div也不要删除
                    if (node.Attributes["id"] != null && (node.Name.Trim().ToLower() == "div" || node.Name.Trim().ToLower() == "td"))
                    {
                        if (!node.Attributes["id"].Value.ToString().ToLower().Contains("content"))
                        {
                            node.Remove();
                        }
                    }
                    else
                    {
                        node.Remove();
                    }
                }
                else
                {

                    RemovClildTags(node, TagNames);
                }
            }
        }





    }



}
