using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

///2013-6-22 修改了  修建静态文件的路径.
namespace Skybot.Cache
{
    using Skybot.Tong.CodeCharSet;
    using System.IO;
    using Skybot.Tong.ZIP;
    using TygModel;
    /// <summary>
    /// 缓存扩展方法
    /// </summary>
    public static class TabBookEnityExpandMethod
    {

        /// <summary>
        /// 虚拟路径
        /// </summary>
        public static string virtualPath = "/docs";

        /// <summary>
        /// 返回书本文件的文件名
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static string GetHTMLFilePath(this 书名表 book)
        {
            string path = virtualPath + "/HTML/" + book.分类标识.Trim().ToPingYing() + "/" + book.书名.Trim().ToPingYing() + "/index.html";

            return path;
        }

        /// <summary>
        /// 返回文章文件的文件名
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetHTMLFilePath(this 文章表 doc)
        {

            string path = virtualPath + "/HTML/" + doc.书名表.分类标识.Trim().ToPingYing() + "/" + doc.书名表.书名.Trim().ToPingYing() + "/";

            path = path + +doc.ID + ".html";


            return path;
        }
        /// <summary>
        /// 返回文章文件的文件名
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetHTMLFilePath(BookDoc doc, string 分类标识, string 书名)
        {
            string path = virtualPath + "/HTML/" + 分类标识.Trim().ToPingYing() + "/" + 书名.Trim().ToPingYing() + "/";

            path = path + +doc.ID + ".html";


            return path;
        }
        /// <summary>
        /// 对书名表创建静态的HTML页面
        /// </summary>
        /// <param name="book">书对像</param>
        /// <param name="html">要生成页面的HTML</param>
        /// <returns>页面路径</returns>
        public static string CreateStaticHTMLFile(this 书名表 book, string html)
        {

            //susucong
            //string path = AppDomain.CurrentDomain.BaseDirectory + virtualPath + "/" + "HTML/" + book.分类标识.Trim().ToPingYing() + "/" + book.书名.Trim().ToPingYing() + "/";

            string path = AppDomain.CurrentDomain.BaseDirectory + "../" + "HTML/" + book.分类标识.Trim().ToPingYing() + "/" + book.书名.Trim().ToPingYing() + "/";
            //创建目录
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path = path + "index.html";

            //写入文件
            try
            {
                System.IO.File.WriteAllText(path, html, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + (ex.StackTrace == null ? " " : ex.StackTrace));
            }

            return path;
        }


        /// <summary>
        /// 为文章表创建静态HTML页面
        /// </summary>
        /// <param name="book">书对像</param>
        /// <param name="html">要生成页面的HTML</param>
        /// <returns>页面路径</returns>
        public static string CreateStaticHTMLFile(this 文章表 doc, string html)
        {
            //susucong
            //string path = AppDomain.CurrentDomain.BaseDirectory + virtualPath + "/" + "HTML/" + doc.书名表.分类标识.Trim().ToPingYing() + "/" + doc.书名表.书名.Trim().ToPingYing() + "/";
           string path = AppDomain.CurrentDomain.BaseDirectory +  "../" + "HTML/" + doc.书名表.分类标识.Trim().ToPingYing() + "/" + doc.书名表.书名.Trim().ToPingYing() + "/";

            //创建目录
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path = path + +doc.ID + ".html";

            //写入文件
            try
            {
                System.IO.File.WriteAllText(path, html, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + (ex.StackTrace == null ? " " : ex.StackTrace));
            }

            return path;
        }

        /// <summary>
        /// 数据库访问对像
        /// </summary>
        private static TygModel.Entities tygDb = new TygModel.Entities();





        /// <summary>
        /// 传入一个页索引　返回对应的页对象，并对对象的同步状态进行比较 如果不同步，则同步记录状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="book"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TabPageEntity GetPage(this System.Collections.Concurrent.ConcurrentBag<string> keys, TabBookEntity book, string key)
        {
            //得到指定key 的记录
            var records = tygDb.文章表.Where(p => p.GUID == Guid.Parse(key));
            if (records.Count() > 0)
            {
                文章表 record = records.FirstOrDefault();





                return new TabPageEntity()
                {
                    Record = record,
                    Book = book,
                    Issync = true,
                    Modifyd = false
                };



            }
            else
            {
                throw new IndexOutOfRangeException("没有找到指定索引的记录");
            }
        }

        /// <summary>
        /// 比较当前记录是不是与硬盘上的记录相等
        /// </summary>
        /// <param name="page">页对像实体</param>
        /// <param name="path">文件所在的路径</param>
        /// <returns></returns>
        public static bool IsTabPageEntitySync(this TabPageEntity page)
        {
            //如果文件不存在直接返回 false
            if (!System.IO.File.Exists(page.GetRecordPath(true)))
            {
                return false;
            }
            //实始化buff
            byte[] bytearr = new byte[10];

            //锁住读写文件时使用
            lock (page)
            {
                //读取缓存文件内容
                //如果多线程操作可能会引发文件访问异常,这里需要对,访问对象进行控制
                bytearr = System.IO.File.ReadAllBytes(page.GetRecordPath(true));
            }

            //得到Zip文件解压缩后的数据
            bytearr = Compression.DeCompress(bytearr);
            //得到需要序列化的字符串
            string Text = System.Text.UnicodeEncoding.Unicode.GetString(bytearr);
            //创建一个序列化对像
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TabPageEntity));

            //得到文中得到的Page 对象
            TabPageEntity diskPage = (TabPageEntity)xmlSerializer.Deserialize(new System.IO.StringReader(Text));

            if (
                diskPage.Book.Record.GUID == page.Book.Record.GUID
                &&
                diskPage.Book.Record.ID == page.Book.Record.ID
                &&
                diskPage.Book.Record.创建时间 == page.Book.Record.创建时间
                &&
                diskPage.Book.Record.分类标识 == page.Book.Record.分类标识
                &&
                diskPage.Book.Record.分类表ID == page.Book.Record.分类表ID
                &&
                diskPage.Book.Record.书名 == page.Book.Record.书名
                &&
                diskPage.Book.Record.说明 == page.Book.Record.说明
                &&
                diskPage.Book.Record.完本 == page.Book.Record.完本
                &&
                diskPage.Book.Record.周点击 == page.Book.Record.周鲜花
                &&
                diskPage.Book.Record.周鲜花 == page.Book.Record.周鲜花
                &&
                diskPage.Book.Record.总点击 == page.Book.Record.总点击
                &&
                diskPage.Book.Record.总鲜花 == page.Book.Record.总鲜花
                &&
                diskPage.Book.Record.最后更新时间 == page.Book.Record.最后更新时间
                //分类比较结束 开始内容比较
                &&
                diskPage.Record.ID == page.Record.ID
                &&
                diskPage.Record.本记录GUID == page.Record.本记录GUID
                &&
                diskPage.Record.最后访问时间 == page.Record.最后访问时间
                )
            {

                return true;
            }
            else
            {
                return false;
            }


        }

        #region 路径扩展方法
        /// <summary>
        /// 得到生成页面文件的路径
        /// </summary>
        /// <param name="page">页面文件代码</param>
        /// <param name="IsCacheFile">是不是返回页面缓存文件的路径 默认为false</param>
        /// <returns>返回生成页面文件的路径</returns>
        public static string GetRecordPath(this TabPageEntity page, bool IsCacheFile = false)
        {
            //得到生成文件的路径
            //基本路径
            string basePath = AppDomain.CurrentDomain.BaseDirectory + page.Record.分类标识.ToPingYing() + "/" + page.Record.书名.ToPingYing() + "/" + page.Record.ID;
            string htmlPage = System.IO.Path.GetFullPath(basePath + ".html");
            string cachePage = System.IO.Path.GetFullPath(basePath + ".zip");
            if (IsCacheFile)
            {
                return cachePage;
            }
            return htmlPage;
        }
        /// <summary>
        /// 得到生成项文件的路径
        /// </summary>
        /// <param name="page">项文件代码</param>
        /// <param name="IsCacheFile">是不是返回项缓存文件的路径 默认为false</param>
        /// <returns>返回生成项文件的路径</returns>
        public static string GetRecordPath(this TabBookEntity book, bool IsCacheFile = false)
        {
            //得到生成文件的路径
            //基本路径
            string basePath = AppDomain.CurrentDomain.BaseDirectory + book.Record.分类标识.ToPingYing() + "/" + book.Record.书名.ToPingYing();
            string htmlPage = System.IO.Path.GetFullPath(basePath + ".html");
            string cachePage = System.IO.Path.GetFullPath(basePath + ".zip");
            if (IsCacheFile)
            {
                return cachePage;
            }
            return htmlPage;

        }

        /// <summary>
        /// 得到生成分类文件的路径
        /// </summary>
        /// <param name="page">分类文件代码</param>
        /// <param name="IsCacheFile">是不是返回分类缓存文件的路径 默认为false</param>
        /// <returns>返回生成分类文件的路径</returns>
        public static string GetRecordPath(this TabClassEntity classEntity, bool IsCacheFile = false)
        {
            //得到生成文件的路径
            //基本路径
            string basePath = AppDomain.CurrentDomain.BaseDirectory + classEntity.Record.分类标识.ToPingYing();
            string htmlPage = System.IO.Path.GetFullPath(basePath + ".html");
            string cachePage = System.IO.Path.GetFullPath(basePath + ".zip");
            if (IsCacheFile)
            {
                return cachePage;
            }
            return htmlPage;

        }

        #endregion

        #region 写入缓存文件
        public static void WriteCache(this TabBookEntity book)
        {

            string path = book.GetRecordPath(true);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            //创建一个序列化对像
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TabBookEntity));
            //写入缓存文件
            System.IO.File.WriteAllBytes(path, Compression.Compress(System.Text.UnicodeEncoding.Unicode.GetBytes(sw.ToString())));
            book.Modifyd = false;
        }

        public static void WriteCache(this TabPageEntity page)
        {

            string path = page.GetRecordPath(true);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            //创建一个序列化对像
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TabPageEntity));
            //写入缓存文件
            System.IO.File.WriteAllBytes(path, Compression.Compress(System.Text.UnicodeEncoding.Unicode.GetBytes(sw.ToString())));

            page.Modifyd = false;
            page.Issync = true;
        }


        public static void WriteCache(this TabClassEntity classEntity)
        {
            string path = classEntity.GetRecordPath(true);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            //创建一个序列化对像
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TabClassEntity));
            //写入缓存文件
            System.IO.File.WriteAllBytes(path, Compression.Compress(System.Text.UnicodeEncoding.Unicode.GetBytes(sw.ToString())));
        }

        #endregion
    }
}