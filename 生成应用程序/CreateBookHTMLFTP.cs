using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Tong.Tyg.Creates
{
    using Skybot.Cache;
    using Skybot.Tong.CodeCharSet;
    /// <summary>
    /// 生成书的book,并且上传到FTP
    /// </summary>
    public class CreateBookHTMLFTP : CreateBookHTML
    {

        /// <summary>
        /// 创建一个新的书本采集实例
        /// </summary>
        /// <param name="book">书</param>
        public CreateBookHTMLFTP(TygModel.书名表 book)
            : base(book)
        {

        }

        /// <summary>
        /// ftp对像
        /// </summary>
        FtpWeb ftp = new FtpWeb(
            System.Configuration.ConfigurationManager.AppSettings["FtpUri"]
            ,
             System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]
             ,
              System.Configuration.ConfigurationManager.AppSettings["Uid"]
              ,
               System.Configuration.ConfigurationManager.AppSettings["pwd"]);

      public  string createDirUrl = System.Configuration.ConfigurationManager.AppSettings["创建文件夹"];
        /// <summary>
        /// FTP基目录
        /// </summary>
       public string BaseFtpDic = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];

      public  string BaseWebSite = System.Configuration.ConfigurationManager.AppSettings["本地文件路径"];

        public override void Run()
        {
      

            //记时器
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            System.Data.Objects.DataClasses.EntityCollection<TygModel.文章表> docs = Book.文章表;
            int count = docs.Count;
            int index = 0;
            List<string> progress = new List<string>();

            if (System.IO.File.Exists(ProgressFileName))
            {

                progress = System.IO.File.ReadAllLines(ProgressFileName).ToList();
            }
            //创建目录
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(ProgressFileName)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(ProgressFileName));
            }
            ///全部重新创建 
            if (IsCreateNew)
            {
                System.IO.File.WriteAllText(ProgressFileName, string.Empty);
            }

            watch.Reset();
            System.Console.WriteLine(string.Format("{0},生成目录", Book.书名));
            //重新生成目录
            string urlIndex = string.Format(UrlIndex, Book.GUID);
            GetWebUrl(urlIndex);

            string dirserver = string.Format(createDirUrl, System.IO.Path.GetDirectoryName("/" + Book.GetHTMLFilePath()));

            //创建目录 
            string dic = GetWebDic(dirserver);
            if (!string.IsNullOrEmpty(dic))
            {
                //生成没有成功
                //移动当前工作目录
                ftp.GotoDirectory(BaseFtpDic + Book.GetHTMLFilePath().Replace("/index.html", string.Empty), true);

                string dirindex = BaseWebSite + "HTML/" + Book.分类标识.Trim().ToPingYing() + "/" + Book.书名.Trim().ToPingYing() + "/index.html";
                //ftp上传文件
                if (Upload(dirindex))
                {
                    System.Console.WriteLine(string.Format("FTP {0},生成目录---------完成,用时{1}秒", Book.书名, Math.Round(watch.ElapsedMilliseconds / 1000.0, 2)));

                }
            }

            System.Console.WriteLine(string.Format("{0},生成目录---------完成,用时{1}秒", Book.书名, Math.Round(watch.ElapsedMilliseconds / 1000.0, 2)));

            //生成明细项
            foreach (var item in docs)
            {
                //这里处理有点问题,他不能在上次关闭的时候接着生成
                if (!IsCreateNew)
                {
                    //如果有则不进行重新生成
                    if (progress.Contains(item.本记录GUID.ToString() + ((char)2) + item.章节名))
                    {
                        index++;
                        continue;
                    }
                }
                //计时
                watch.Reset();
                string url = string.Format(UrlDoc, item.ID);
                GetWebUrl(url);

                //生成目录成功
                if (!string.IsNullOrEmpty(dic))
                {
                    string dir = BaseWebSite + "HTML/" + Book.分类标识.Trim().ToPingYing() + "/" + Book.书名.Trim().ToPingYing() + "/" + item.ID + ".html";
                    //ftp上传文件
                    if (Upload(dir))
                    {
                        //采集完成一个记录一个
                        System.IO.File.AppendAllLines(ProgressFileName, new string[] { item.本记录GUID.ToString() + ((char)2) + item.章节名 });
                        //输出当前进度
                        System.Console.WriteLine(string.Format("FTP {0},进度{1}/{2},名称：{3},url:{4},用时:{5}秒", Book.书名, index, count, item.章节名, url, Math.Round(watch.ElapsedMilliseconds / 1000.0, 0)));

                    }

                }
                index++;
                watch.Stop();
                //输出当前进度
                System.Console.WriteLine(string.Format("{0},进度{1}/{2},名称：{3},url:{4},用时:{5}秒", Book.书名, index, count, item.章节名, url, Math.Round(watch.ElapsedMilliseconds / 1000.0, 0)));

            }
            ftp = null;
        }

        /// <summary>
        /// 生成目录
        /// </summary>
        /// <param name="book"></param>
        public string UploadIndexFile(TygModel.书名表 book)
        {
            string dirserver = string.Format(createDirUrl, System.IO.Path.GetDirectoryName("/" + book.GetHTMLFilePath()));

            //创建目录 
            string dic = GetWebDic(dirserver);


            //生成目录成功
            if (!string.IsNullOrEmpty(dic))
            {
                //生成没有成功
                //移动当前工作目录
                ftp.GotoDirectory(BaseFtpDic + Book.GetHTMLFilePath().Replace("/index.html", string.Empty), true);

                string dirindex = BaseWebSite + "HTML/" + Book.分类标识.Trim().ToPingYing() + "/" + Book.书名.Trim().ToPingYing() + "/index.html";
                //ftp上传文件
                if (Upload(dirindex))
                {
                    System.Console.WriteLine(string.Format("FTP {0},生成目录---------完成", Book.书名));

                }
            }


            return dic;
        }

        /// <summary>
        /// 上传DOC文件
        /// </summary>
        /// <param name="item"></param>
        public void UploadDocFile(TygModel.文章表 item)
        { 
            string dir = BaseWebSite + "HTML/" + Book.分类标识.Trim().ToPingYing() + "/" + Book.书名.Trim().ToPingYing() + "/" + item.ID + ".html";
                    //ftp上传文件
            Upload(dir);
        }

        /// <summary>
        /// 请求url生成HTML
        /// </summary>
        /// <param name="url"></param>
        public string GetWebDic(string url)
        {
            if (!string.IsNullOrEmpty(Skybot.Collections.GetHttpContentHepler.GetWeb(url)))
            {
                return "完成";
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                System.Console.WriteLine(DateTime.Now + "获取URL出现问题" + url);
                return GetWebDic(url);
            }
            return string.Empty;
        }

        /// <summary>
        /// 上传文件 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected bool Upload(string url)
        {
            if (ftp.Upload(url))
            {
                return true;
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                System.Console.WriteLine(DateTime.Now + "FTP 上传文件出现问题" + url);
                return Upload(url);
            }

        }

    }
}
