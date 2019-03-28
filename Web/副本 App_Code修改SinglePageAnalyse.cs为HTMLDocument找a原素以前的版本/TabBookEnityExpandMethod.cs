using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TygModel;

namespace Skybot.Cache
{
    using Skybot.Tong.CodeCharSet;
    using System.IO;
    using Skybot.Tong.ZIP;
    /// <summary>
    /// 缓存扩展方法
    /// </summary>
    public static class TabBookEnityExpandMethod
    {
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
            var records = tygDb.文章表.Where(p => p.GUID == key);
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