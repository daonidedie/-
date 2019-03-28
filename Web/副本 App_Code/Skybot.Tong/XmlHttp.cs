/******************************************************
* 文件名：XmlHttp.cs
* 文件功能描述：XMLHTTP 服務器操作 数据采集 XMLHTTP 類  命名空间 XmlHttp
* 
* 创建标识：周渊 2007-6-23
* 
* 修改标识：周渊 2008-4-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using MSXML2; //要在工程文件中引用 MicroSoft XML2.6

//下面　SHA-1加密用的類

namespace Skybot.Tong
{
    #region XMLHTTP 服務器操作 数据采集 XMLHTTP 類 XmlHttp

    //2007-6-23
    /// <summary>
    /// 服務器操作 XMLHTTP 類
    /// 用于选程请求数据
    /// </summary>
    public class XMLHTTPServer
    {
        /// <summary>
        /// 替换数据 使他符合ＸＭＬ
        /// </summary>
        /// <param name="strXdata">数据</param>
        protected String XmlTextData(string strXdata)
        {
            strXdata = strXdata.Replace("&", "&#38;");
            //替换 所属分类 
            strXdata = strXdata.Replace("/asklist.php?parentid=", TongUse.SysPath + "baike/ShowList.Aspx?Type=");
            //替换 标题
            strXdata = strXdata.Replace("/ask/", TongUse.SysPath + "baike/showdoc.aspx?url=/ask/");
            //替换 \"
            strXdata = strXdata.Replace("\\\"", "");
            //替换\'
            strXdata = strXdata.Replace("\\\'", "");
            //替换 '
            strXdata = strXdata.Replace("\'", "");
            //替换 17173 的bad数据
            //  xmlcont = xmlcont.Replace("<div class=\"noaskDiv\">","<a>");

            strXdata = strXdata.Replace("<td>提问者</td><td id=\"managetd\">管理</td>", "");
            //替换 <span class="font12">

            // xmlcont = xmlcont.Replace("<span class=\"font12\">", "");
            // xmlcont = xmlcont.Replace("<span class=\"question_table_icon\">", "");
            // xmlcont = xmlcont.Replace("</span>","");
            // xmlcont = xmlcont.Replace("<script>setAskTotal('55');</script>", "");
            //xmlcont = xmlcont.Replace("=", "&#61;"); 
            // res.Response.Write(xmlcont);

            return strXdata;
        }

        /// <summary>
        /// 使用XMLHTTP 来得到数据 返回经过处理的 responseText的数据
        /// </summary>
        /// <param name="xmlgetdataurl">请求URL</param>
        public String XmlHttpCont(string xmlgetdataurl)
        {
            string strXdata = "";
            if (xmlgetdataurl != "")
            {
                // xmlhttp類
                XMLHTTPClass xmlhttp = new XMLHTTPClass();
                xmlhttp.open("get", xmlgetdataurl, false, "", "");
                //xmlhttp.open("get", "http://bk.17173.com/ajax/getajaxinfo.php?Start=getAskList&currpage=" + (page + 1) + "&pagesize=20&status=" + status + "&parent=" + url + "&type=3", false, "", "");
                //xmlhttp.open("get", "http://bk.17173.com/ajax/getajaxinfo.php?Start=getAskList&currpage=1&pagesize=20&status=r&parent=20&type=3", false, "", "");
                xmlhttp.send(null);
                if (xmlhttp.readyState == 4)
                {
                    //替换数据 使他符合ＸＭＬ
                    strXdata = XmlTextData(xmlhttp.responseText);
                }
            }

            return strXdata;
        }

        /// <summary>
        /// 使用XMLHTTP 来得到数据 返回 responseText的数据
        /// </summary>
        /// <param name="getxmlurl">请求URL</param>
        public String XMLContent(string getxmlurl)
        {
            string strXdata = "";
            if (getxmlurl != "")
            {
                // xmlhttp類
                XMLHTTPClass xmlhttp = new XMLHTTPClass();
                xmlhttp.open("get", getxmlurl, false, "", "");
                xmlhttp.send(null);
                if (xmlhttp.readyState == 4)
                {
                    //替换数据 使他符合ＸＭＬ
                    strXdata = xmlhttp.responseText;
                }
            }

            return strXdata;
        }
    }

    /// <summary>
    /// web 采集 类
    /// </summary>
    public class GetWebData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetWebData() { }

        /// <summary>
        /// HttpWebRequest读取网页 
        /// </summary>
        /// <param name="website">url 可以为 相对于根目录,与绝对路径</param>
        /// <param name="codeEncod">文件编码 GB2312 UTF-8 </param>
        /// <returns></returns>
        public string GetWebDate(string website, string codeEncod)
        {
            string strHtmlContent = "";
            try
            {
                if (website.IndexOf("http://") == -1)//如果米有HTTP
                {
                    website = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + website;
                }
                System.Net.HttpWebRequest myrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(website);
                myrequest.Timeout = 90000;//指定超时时间为90秒
                // myrequest.Headers.Set("Pragma", "no-cache");
                System.Net.HttpWebResponse myresponse = (System.Net.HttpWebResponse)myrequest.GetResponse();
                System.IO.Stream mystream = myresponse.GetResponseStream();
                System.Text.Encoding myencoding = System.Text.Encoding.GetEncoding(codeEncod);
                System.IO.StreamReader mystreamreader = new System.IO.StreamReader(mystream, myencoding);
                strHtmlContent = mystreamreader.ReadToEnd();
                mystream.Close();
                mystreamreader.Dispose();
                mystream.Close();
                mystream.Dispose();

            }
            catch (Exception ex)
            {
                strHtmlContent = "读取错误" + ex.Message;
            }

            return strHtmlContent;
        }
    }

    #endregion
}
