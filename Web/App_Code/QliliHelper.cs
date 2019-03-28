using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybot.Cache
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;



    /// <summary>
    /// QliliHelper 用于请求Qlili的功能
    /// </summary>
    public class QliliHelper
    {
        private static string baseSite = System.Configuration.ConfigurationManager.AppSettings["baseSite"];
        /// <summary>
        /// 基本网站
        /// </summary>
        public static string BaseSite
        {
            get { return QliliHelper.baseSite; }
        }
        /// <summary>
        /// 生成索引页面
        /// </summary>
        /// <returns></returns>
        public static string IndexBook()
        {
            string url = baseSite + System.Web.HttpContext.Current.Request.Url.LocalPath
                + (System.Web.HttpContext.Current.Request.Url.Query.Length > 0 ? System.Web.HttpContext.Current.Request.Url.Query : string.Empty);

            return Skybot.Collections.GetHttpContentHepler.GetWeb(url);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string BookList()
        {
            string url = baseSite + System.Web.HttpContext.Current.Request.Url.LocalPath
                + (System.Web.HttpContext.Current.Request.Url.Query.Length > 0 ? System.Web.HttpContext.Current.Request.Url.Query : string.Empty);

            return Skybot.Collections.GetHttpContentHepler.GetWeb(url);
        }

        /// <summary>
        /// 写入文件 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cont"></param>
        public static void WirteFile(string path, string cont)
        {
            string fullpath = AppDomain.CurrentDomain.BaseDirectory + path;
            string dir = System.IO.Path.GetDirectoryName(fullpath);
            if (!System.IO.Directory.Exists(dir))
            {
                //不存在则创建目录
                System.IO.Directory.CreateDirectory(dir);
            }

            System.IO.File.WriteAllText(fullpath, cont);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SearchPage()
        {
            return string.Empty;
        }

        private static List<TygModel.书名表> _Books = new List<TygModel.书名表>();

        /// <summary>
        /// 所用到的书
        /// </summary>
        public static List<TygModel.书名表> Books
        {
            get { return _Books; }
            set { _Books = value; }
        }
    }
}