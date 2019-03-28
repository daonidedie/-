using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Tong.Tyg.Creates
{
    using Skybot.Cache;
    using TygModel;
    /// <summary>
    /// 创建网站地图
    /// </summary>
    public class SiteMap
    {
        public string url = "http://localhost";
        public string Path = @"E:\網站項目\网站文件\听语阁文学\Web\";
        /// <summary>
        /// 网站地图
        /// </summary>
        public List<string> list = new List<string>();

        /// <summary>
        /// 创建网站地图 10000个写一次
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="index"></param>
        public void CreateMaps(string[] strs, int index)
        {
            System.IO.File.WriteAllLines(Path + "SiteMap" + index + ".txt", strs);
        }
        int SiteIndex = 1;
        /// <summary>
        /// 
        /// </summary>
        int currentIndex = 1;
        /// <summary>
        /// 写入文件 
        /// </summary>
        public void Foreach()
        {
            TygModel.Entities tity = new TygModel.Entities();
            var items = tity.文章表.OrderBy(p => p.ID);
            int count = items.Count();
            tity.Dispose();
            for (int k = 0; k < count / 10000; k++)
            {
                using (TygModel.Entities ent = new TygModel.Entities())
                {
                    var records = ent.文章表.OrderBy(p => p.ID).Skip(k * 10000).Take(10000).ToList();

                    foreach (var sp in records)
                    {
                        if (list.Count >= 10000)
                        {
                            CreateMaps(list.ToArray(), SiteIndex++);
                            list.Clear();
                        }
                        //添加到路径
                        list.Add(url + sp.GetHTMLFilePath());
                        currentIndex++;
                        System.Console.WriteLine(string.Format("进度{0}/{1},{2}", currentIndex, count, sp.章节名));
                    }

                    records.Clear();
                    records = null;
                }
            }

            //全部写入
            CreateMaps(list.ToArray(), SiteIndex++);






        }

        /// <summary>
        /// 更新最新的文章
        /// </summary>
        public void ForUpdateDocs(IEnumerable<文章表> docs)
        {
           

            if (docs.Count() > 0)
            { 
                CreateBookHTMLFTP ftpBook = new CreateBookHTMLFTP(docs.ElementAt(0).书名表);

                CreateBookHTML bookHTML = new CreateBookHTML(docs.ElementAt(0).书名表);
                //创建地图
                bookHTML.GetWebUrl(url + "/file/SiteMap.aspx");

                //得到所在书的目录
                var books = docs.ToLookup(p => p.GUID);

                int index = 0;
                int bookCount = books.Count();
                //生成书目录
                foreach (var book in books)
                {
                    string urlx = string.Format(CreateBookHTML.UrlIndex, book.ElementAt(0).GUID);
                    bookHTML.GetWebUrl(urlx);
                    //上传数据
                    ftpBook.UploadIndexFile(book.ElementAt(0).书名表);

                    index = index + 1;
                    //输出当前进度
                    System.Console.WriteLine(string.Format("生成目录,进度{0}/{1},url:{2}",  index, bookCount, urlx));

                }


                 index = 0;
                int count = docs.Count();
                foreach (var item in docs)
                {
                    string urlx = string.Format(CreateBookHTML.UrlDoc, item.ID);
                    bookHTML.GetWebUrl(urlx);
                    ftpBook.UploadDocFile(item);
                    index = index + 1;
                    //输出当前进度
                    System.Console.WriteLine(string.Format("{0},进度{1}/{2},名称：{3},url:{4}", item.书名, index, count, item.章节名, urlx));
                }

                System.Console.WriteLine("創建站點地圖完成");
            }
        }
    }
}
