using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skybot.Collections;
using Skybot.Collections.Analyse;
using Skybot.Collections.Sites;
public partial class 网站图书列表采集_yankuai_com : System.Web.UI.Page
{

    //基url
    public const string BaseUrl = "http://www.yankuai.com/book/toplastupdate/0/{0}.htm";//1-175

    /// <summary>
    /// 序列化文件路径
    /// </summary>
    public string yankuankanPath = System.Web.HttpContext.Current.Server.MapPath("/数据暂存/") + "yankuankan.com.xml";


    protected void Page_Load(object sender, EventArgs e)
    {

        string url = string.Format(BaseUrl, "1");
        //得到最大的页码
        int MaxPageNum = 1;
        //初始化一个DOM
        HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
        dom.LoadHtml(url.GetWeb());

        //内容
        HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("content");

        //得到分页
        HtmlAgilityPack.HtmlNode pagesNode = dom.GetElementbyId("pagelink");


        #region 得到页面的所有tr原素
        List<BookInfoYankuaikan_com> books = GetBookInfos(listContent, url);
        #endregion

        #region 提取 pages 中 所有的a原素

        MaxPageNum = GetMaxPageNum(pagesNode);
        #endregion

        List<BookInfoYankuaikan_com> AllBooks = new List<BookInfoYankuaikan_com>();
        //序列化保存为文件
        System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(AllBooks.GetType());
        //如果文件不存在则从缓存读取
        if (!System.IO.File.Exists(yankuankanPath))
        {
            AllBooks = GetBooks(MaxPageNum);//MaxPageNum
            System.IO.StringWriter sw = new System.IO.StringWriter();
            ser.Serialize(sw, AllBooks);
            System.IO.File.WriteAllText(yankuankanPath, sw.ToString());
        }
        else
        {
            AllBooks = (List<BookInfoYankuaikan_com>)ser.Deserialize(new System.IO.StringReader(System.IO.File.ReadAllText(yankuankanPath)));
        }





        //System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
        //{
        //添加到Book中
        for (int k = 0; k < AllBooks.Count; k++)
        {
            try
            {
                TygModel.书名表 book = AllBooks[k].Convert();
                //当前记录
                var records = Skybot.Cache.RecordsCacheManager.Instance.Tygdb.书名表.Where(p => p.书名.Trim() == book.书名.Trim() && p.作者名称.Trim() == book.作者名称.Trim());

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

                    //保存更改
                    Skybot.Cache.RecordsCacheManager.Instance.Tygdb.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
            }
            System.Diagnostics.Debug.WriteLine("已经完成书" + k + "/" + AllBooks.Count);
        }


        //}));

        Response.Write("输入完成");


    }

    /// <summary>
    /// 得到页面书集合
    /// </summary>
    /// <param name="MaxPageNum"></param>
    public List<BookInfoYankuaikan_com> GetBooks(int MaxPageNum)
    {

        List<BookInfoYankuaikan_com> books = new List<BookInfoYankuaikan_com>();

        foreach (var pageurlIndex in System.Linq.Enumerable.Range(1, MaxPageNum))
        {

            string url = string.Format(BaseUrl, "" + pageurlIndex);

            //初始化一个DOM
            HtmlAgilityPack.HtmlDocument dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(url.GetWeb());

            //内容
            HtmlAgilityPack.HtmlNode listContent = dom.GetElementbyId("content");

            //得到分页
            HtmlAgilityPack.HtmlNode pagesNode = dom.GetElementbyId("pagelink");


            #region 得到页面的所有tr原素
            List<BookInfoYankuaikan_com> Pagebooks = GetBookInfos(listContent, url);



            books.AddRange(Pagebooks);
            #endregion

            System.Diagnostics.Debug.WriteLine("已经完成页面" + pageurlIndex + "/" + MaxPageNum);
        }

        return books;
    }


    public List<BookInfoYankuaikan_com> GetBookInfos(HtmlAgilityPack.HtmlNode listContent, string url)
    {
        //用于返回的结果
        List<BookInfoYankuaikan_com> result = new List<BookInfoYankuaikan_com>();

        //可能的原素
        List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();


        //开始循环子原素
        SingleListPageAnalyse.AnalyseMaxATagNearest(listContent, possiblyResultElements, 0, new PossiblyResultElement()
        {
            ParentPossiblyResult = null,
            CurrnetHtmlElement = listContent,
            LayerIndex = -1,
            ContainTagNum = 0
        });
        //计算当前所有HTML原素中的tr原素
        var PageTrElements = from tr in possiblyResultElements
                             where tr.CurrnetHtmlElement.Name == "tr"
                             select tr;


        //填类
        foreach (var item in PageTrElements)
        {
            if (item.CurrnetHtmlElement.HasChildNodes)
            {
                var els = item.CurrnetHtmlElement.ChildNodes.Where(p => p.HasChildNodes);

                if (els.ElementAt(1).Name == "td" && els.Count() >= 7)
                {
                    BookInfoYankuaikan_com bookinfo = new BookInfoYankuaikan_com();


                    //当前数据实体的类型，用于反射
                    Type bookInfoType = bookinfo.GetType();
                    //填充  类别 小说名称 最新章节 作者 字数 更新 状态 
                    bookinfo.类别 = els.ElementAt(0).InnerText;
                    bookinfo.小说名称 = els.ElementAt(1).InnerText;

                    bookinfo.最新章节 = els.ElementAt(2).InnerText;
                    bookinfo.作者 = els.ElementAt(3).InnerText;
                    bookinfo.字数 = els.ElementAt(4).InnerText;
                    bookinfo.更新 = els.ElementAt(5).InnerText;
                    bookinfo.状态 = els.ElementAt(6).InnerText;
                    bookinfo.采集URL = url;
                    try
                    {
                        bookinfo.小说简介URL = els.ElementAt(1).ChildNodes.Where(p => p.Name == "a").ElementAt(0).Attributes["href"].Value.ToString();
                    }
                    catch { }

                    if (els.ElementAt(2).ChildNodes.Count >= 2)
                    {
                        try
                        {
                            bookinfo.最新章节 = els.ElementAt(2).ChildNodes.Where(p => p.Name == "a").ElementAt(1).InnerText;
                        }
                        catch { }
                        try
                        {
                            bookinfo.小说目录URL = els.ElementAt(2).ChildNodes.Where(p => p.Name == "a").ElementAt(0).Attributes["href"].Value.ToString();
                        }
                        catch { }

                        result.Add(bookinfo);
                    }

                }
            }
        }

        return result;

    }

    /// <summary>
    /// 得到最大的页码
    /// </summary>
    /// <returns>最大的页码</returns>
    public int GetMaxPageNum(HtmlAgilityPack.HtmlNode pagesNode)
    {
        //得到最大的页码
        int MaxPageNum = 1;

        if (pagesNode == null)
        {
            return 0;
        }

        //可能的原素
        List<PossiblyResultElement> possiblyResultElements = new List<PossiblyResultElement>();


        //开始循环子原素
        SingleListPageAnalyse.AnalyseMaxATagNearest(pagesNode, possiblyResultElements, 0, new PossiblyResultElement()
        {
            ParentPossiblyResult = null,
            CurrnetHtmlElement = pagesNode,
            LayerIndex = -1,
            ContainTagNum = 0
        });

        int x = 0;
        //计算当前所有HTML原素中的A原素
        var PageAElements = from a in possiblyResultElements
                            where a.CurrnetHtmlElement.Name == "a" && a.CurrnetHtmlElement.Attributes["href"] != null
                            select new Func<int>(() => { int.TryParse(a.CurrnetHtmlElement.InnerText, out x); return x; }).Invoke();
        if (PageAElements.Count() > 0)
        {

            //得到最大的页码
            MaxPageNum = PageAElements.OrderByDescending(p => p).FirstOrDefault();
        }
        return MaxPageNum;

    }



}




