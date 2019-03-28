using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 网站图书列表采集_86zw_com : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(
            Server.UrlDecode("http://www.trimedgas.com/error/error.aspx?Error='s'+%e9%99%84%e8%bf%91%e6%9c%89%e8%af%ad%e6%b3%95%e9%94%99%e8%af%af%e3%80%82%0d%0a%e5%85%b3%e9%94%ae%e5%ad%97+'with'+%e9%99%84%e8%bf%91%e6%9c%89%e8%af%ad%e6%b3%95%e9%94%99%e8%af%af%e3%80%82%e5%a6%82%e6%9e%9c%e6%ad%a4%e8%af%ad%e5%8f%a5%e6%98%af%e5%85%ac%e7%94%a8%e8%a1%a8%e8%a1%a8%e8%be%be%e5%bc%8f%e6%88%96+xmlnamespaces+%e5%ad%90%e5%8f%a5%ef%bc%8c%e9%82%a3%e4%b9%88%e5%89%8d%e4%b8%80%e4%b8%aa%e8%af%ad%e5%8f%a5%e5%bf%85%e9%a1%bb%e4%bb%a5%e5%88%86%e5%8f%b7%e7%bb%93%e5%b0%be%e3%80%82%0d%0a%e5%85%b3%e9%94%ae%e5%ad%97+'with'+%e9%99%84%e8%bf%91%e6%9c%89%e8%af%ad%e6%b3%95%e9%94%99%e8%af%af%e3%80%82%e5%a6%82%e6%9e%9c%e6%ad%a4%e8%af%ad%e5%8f%a5%e6%98%af%e5%85%ac%e7%94%a8%e8%a1%a8%e8%a1%a8%e8%be%be%e5%bc%8f%e6%88%96+xmlnamespaces+%e5%ad%90%e5%8f%a5%ef%bc%8c%e9%82%a3%e4%b9%88%e5%89%8d%e4%b8%80%e4%b8%aa%e8%af%ad%e5%8f%a5%e5%bf%85%e9%a1%bb%e4%bb%a5%e5%88%86%e5%8f%b7%e7%bb%93%e5%b0%be%e3%80%82%0d%0a%e5%85%b3%e9%94%ae%e5%ad%97+'with'+%e9%99%84%e8%bf%91%e6%9c%89%e8%af%ad%e6%b3%95%e9%94%99%e8%af%af%e3%80%82%e5%a6%82%e6%9e%9c%e6%ad%a4%e8%af%ad%e5%8f%a5%e6%98%af%e5%85%ac%e7%94%a8%e8%a1%a8%e8%a1%a8%e8%be%be%e5%bc%8f%e6%88%96+xmlnamespaces+%e5%ad%90%e5%8f%a5%ef%bc%8c%e9%82%a3%e4%b9%88%e5%89%8d%e4%b8%80%e4%b8%aa%e8%af%ad%e5%8f%a5%e5%bf%85%e9%a1%bb%e4%bb%a5%e5%88%86%e5%8f%b7%e7%bb%93%e5%b0%be%e3%80%82"
           )
            );

        var books = Skybot.Cache.RecordsCacheManager.Instance.Tygdb.书名表.ToList();


        for (int classid = 1; classid <= 8; classid++)
        {

            System.Diagnostics.Debug.WriteLine("开始采集图书列表" + "http://www.86zw.com/Book/ShowBookList.aspx?tclassid=" + classid + "&page={0}");
            var al = new Skybot.Collections.Sites.BookList86zw_com() { BaseUrl = "http://www.86zw.com/Book/ShowBookList.aspx?tclassid=" + classid + "&page={0}" }.DoWork();

            //开始写入数据库
            var AllBooks = from doc in al select doc;

            //添加到Book中
            for (int k = 0; k < AllBooks.Count(); k++)
            {
                try
                {
                    TygModel.书名表 book = AllBooks.ElementAt(k).Convert();
                    //当前记录
                    var records = books.Where(p => p.书名.Replace("》", "").Replace("《", "").Trim() == book.书名.Replace("》", "").Replace("《", "").Trim() && p.作者名称.Trim() == book.作者名称.Trim());

                    if (records.Count() > 0)
                    {
                        //得到当前书更新记录
                        TygModel.书名表 currentBook = records.FirstOrDefault();
                        // currentBook.分类表 = book.分类表;
                        //currentBook.分类表ID = book.分类表ID;
                        //currentBook.GUID = book.GUID;
                        currentBook.采集用的URL1 = book.采集用的URL1;
                        currentBook.采集用的URL2 = book.采集用的URL2;
                        // currentBook.创建时间 = book.创建时间;
                        currentBook.最新章节 = book.最新章节;
                        //currentBook.作者名称 = book.作者名称;
                        currentBook.说明 = book.说明;
                        // currentBook.书名 = book.书名;
                        currentBook.最后更新时间 = book.最后更新时间;
                        currentBook.完本 = book.完本;
                        currentBook.配图 = book.配图;
                        //保存更改
                        Skybot.Cache.RecordsCacheManager.Instance.Tygdb.SaveChanges();
                    }
                    else
                    {

                        Skybot.Cache.RecordsCacheManager.Instance.Tygdb.AddTo书名表(book);
                        Skybot.Cache.RecordsCacheManager.Instance.Tygdb.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
                }
                System.Diagnostics.Debug.WriteLine("已经完成书" + k + "/" + AllBooks.Count());
            }

        }
        System.Diagnostics.Debug.WriteLine("86zw 所有图书状态更新完成");

        //更新书有效章节数量
        TygModel.Entities enti = new TygModel.Entities();
        var dd = enti.书名表.ToList();
        dd = dd.Where(p => p.包含有效章节 == null || p.包含有效章节 == 0).ToList();
        for (int k = 0; k < dd.Count(); k++)
        {
            dd.ElementAt(k).包含有效章节 = dd.ElementAt(k).文章表.Count;
            enti.SaveChanges();
        }


    }
}