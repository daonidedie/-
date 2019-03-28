using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Skybot.Tong.Tyg.Creates
{
    /// <summary>
    /// 生成书的book
    /// </summary>
    public class CreateBookHTML
    {
        /// <summary>
        /// 生成目录页面url表达式
        /// </summary>
        public static string  UrlIndex =System.Configuration.ConfigurationManager.AppSettings["目录"]
            //"http://localhost/Site/BookIndex.aspx?guid={0}&html=true"
            ;
        /// <summary>
        /// 生成文章页面url表达式
        /// </summary>
        public static string UrlDoc = System.Configuration.ConfigurationManager.AppSettings["文章"]
           // "http://localhost/Site/ShowDoc.aspx?guid={0}&html=true"
            ;

        /// <summary>
        /// 已经创建的章节文件记录名
        /// </summary>
        protected string ProgressFileName = AppDomain.CurrentDomain.BaseDirectory + "Progress/";

        /// <summary>
        /// 用于请求数据的webClient
        /// </summary>
        protected System.Net.WebClient wc = new System.Net.WebClient();
        #region 属性
        /// <summary>
        /// 当前正在处理的书对像
        /// </summary>
        public TygModel.书名表 Book { get; protected set; }

        /// <summary>
        /// 是不是重新创建所有页面
        /// </summary>
        public bool IsCreateNew { get; set; }
        #endregion

      

        /// <summary>
        /// 创建一个新的书本采集实例
        /// </summary>
        /// <param name="book">书</param>
        public CreateBookHTML(TygModel.书名表 book)
        {
            Book = book;
            //初始化进度文件
            ProgressFileName = ProgressFileName + book.GUID + ".txt";

        }

        /// <summary>
        /// 请求url生成HTML
        /// </summary>
        /// <param name="url"></param>
        public void GetWebUrl(string url)
        {
            try
            {
                wc.DownloadData(url);

            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(1000);
                System.Console.WriteLine(DateTime.Now + "获取URL出现问题" + url + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                GetWebUrl(url);
            }
        }


        /// <summary>
        /// 开始生成
        /// </summary>
        public virtual void Run()
        {
            //记时器
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            EntityCollection<TygModel.文章表> docs = Book.文章表;
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
                string url = string.Format(UrlDoc, item.本记录GUID);
                GetWebUrl(url);
                index++;
                //采集完成一个记录一个
                System.IO.File.AppendAllLines(ProgressFileName, new string[] { item.本记录GUID.ToString() + ((char)2) + item.章节名 });
                watch.Stop();
                //输出当前进度
                System.Console.WriteLine(string.Format("{0},进度{1}/{2},名称：{3},url:{4},用时:{5}秒", Book.书名, index, count, item.章节名, url, Math.Round(watch.ElapsedMilliseconds / 1000.0, 0)));

            }

        }

       

    }
}
