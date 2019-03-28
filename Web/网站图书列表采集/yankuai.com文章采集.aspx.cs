using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Skybot.Collections;
public partial class 网站图书列表采集_yankuai : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


// -- 去除重复记录
//--delete from [书名表] where ID not in(select max(ID) from [书名表] group by [书名] having count([书名])>=1);

        //更新排序
        new Skybot.Tong.TongUse().GetWeb("http://localhost/百度风云榜/小说前50.aspx");


        try
        {
            ///////////////////////////////////开始进行数采集
            //表示5个任务同时开始
            Skybot.Cache.RecordsCacheManager.Instance.Tygdb.书名表
                .OrderByDescending(p=>p.最后更新时间)
                //.Where(p => p.书名 == "吞噬星空" || p.书名 == "仙逆")
                .Take(50).AsParallel().WithDegreeOfParallelism(15).ForAll((o) =>
                {

                    using (TygModel.Entities entities = new TygModel.Entities())
                    {
                        var books = entities.书名表.Where(p => p.GUID == o.GUID);
                        if (books.Count() > 0)
                        {
                            GetContents(books.First(), entities);
                            o.最后更新时间 = DateTime.Now;
                            //保存记录数   不允许启动新事务，因为有其他线程正在该会话中运行。 
                            entities.SaveChanges();
                        }
                    }
                });
            //打印状态
            System.Diagnostics.Debug.WriteLine("开始采集 所有书本信息");
        }
        catch (Exception) { }


        return;
        try
        {
            ///////////////////////////////////开始进行数采集
            //表示5个任务同时开始
            Skybot.Cache.RecordsCacheManager.Instance.Tygdb.书名表
                .Where(p => p.文章表.Count < 0)
                //.Where(p => p.书名 == "吞噬星空" || p.书名 == "仙逆")
                .Take(9999).AsParallel().WithDegreeOfParallelism(15).ForAll((o) =>
            {

                using (TygModel.Entities entities = new TygModel.Entities())
                {
                    var books = entities.书名表.Where(p => p.GUID == o.GUID);
                    if (books.Count() > 0)
                    {
                        GetContents(books.First(), entities);
                        //保存记录数   不允许启动新事务，因为有其他线程正在该会话中运行。 
                        entities.SaveChanges();
                    }
                }
            });

        }
        catch(Exception) { }




        //保存数
        Skybot.Cache.RecordsCacheManager.Instance.Tygdb.SaveChanges();

    }

    /// <summary>
    /// 得到文章的内容
    /// </summary>
    /// <param name="o">记录</param>
    /// <param name="entities">数据库操作实体</param>
    private void GetContents(TygModel.书名表 o, TygModel.Entities entities)
    {
        //得到
        Skybot.Collections.Analyse.SingleListPageAnalyse ListPageAnalyse = null;
        Skybot.Collections.Analyse.XMLDocuentAnalyse documentAnalyse = null;

        //打印状态
        System.Diagnostics.Debug.WriteLine("开始采集:" + o.书名 + " 目录:" + o.采集用的URL1);

        //得到转换后的对应分析器
        List<Task> tasks = new List<Task>();
        tasks.Add(Task.Factory.StartNew(() =>
        {
            ListPageAnalyse = new Skybot.Collections.Analyse.SingleListPageAnalyse(o.采集用的URL1);
            documentAnalyse = new Skybot.Collections.Analyse.XMLDocuentAnalyse() { IndexPageUrl = new Skybot.Collections.Analyse.ListPageContentUrl() { index = 0, Title = o.书名, Url = new Uri(o.采集用的URL1) } };

            try
            {
                //初始化内容分析器
                documentAnalyse.GetPathExpression(documentAnalyse.IndexPageUrl, ListPageAnalyse.ListPageContentUrls);
            }
            catch (Exception)
            {

            }
        }));


        try
        {
            //等待添加页面列表分析完成 5 分钟没有完成则表示超时
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray(), TimeSpan.FromMinutes(5));
        }
        catch (AggregateException ex)
        {
            ex.Handle((exx) =>
            {
                System.Diagnostics.Debug.WriteLine(exx.Message + "|||||" + exx.StackTrace);
                return true;
            });
        }


        //如果数据有效
        if (ListPageAnalyse != null && documentAnalyse != null && documentAnalyse.PathExpression != null)
        {

            //得到已经存在的记录
            var docentitys = o.文章表.ToList();
            var docs = docentitys.Select(p => p.章节名.Trim());

            //打印状态
            System.Diagnostics.Debug.WriteLine("内容共:" + ListPageAnalyse.ListPageContentUrls.Count + "条记录");

            //将章节列表索引转换成为扩展实体数据
            List<UrlExtentEntity> entitys = ListPageAnalyse.ListPageContentUrls.Select(p => new UrlExtentEntity() { index = p.index, Indexs = p.Indexs, Title = p.Title, Url = p.Url }).ToList();

            //开始采集数据
            for (int k = 0; k < entitys.Count; k++)
            {


                //上一条记录
                UrlExtentEntity PreviousItem = null; // entitys[k - 1];
                //当前记录
                UrlExtentEntity CurrentItem = null; //  entitys[k];
                //下一条记录
                UrlExtentEntity NextItem = null; //  entitys[k + 1];

                //当前记录
                CurrentItem = entitys[k];
                //看看记录是不是已经存在了 如果存在则不进行更新
                if (!docs.Contains(CurrentItem.Title.Trim()))
                {
                    #region 传值

                    //上一条记录
                    if (k - 1 >= 0)
                    {
                        PreviousItem = entitys[k - 1];
                        //如果上一章节已经存在了则就用数据库中的记录
                        var Temprecords = docentitys.Where(p => p.章节名.Trim() == PreviousItem.Title.Trim());
                        if (Temprecords.Count() > 0)
                        {
                            PreviousItem.Token = Temprecords.ElementAt(0).本记录GUID.ToString();
                            //设置本记录ID
                            CurrentItem.Token = Temprecords.ElementAt(0).下一章.Value.ToString();
                        }
                    }


                    //下一条记录
                    if (k + 1 < entitys.Count)
                    {

                        NextItem = entitys[k + 1];

                    }
                    #endregion

                    //当前章节的GUID
                    if (CurrentItem.Token.Length < 10)
                    {
                        CurrentItem.Token = Guid.NewGuid().ToString();
                    }

                    //产生下一章节的GUID
                    if (NextItem != null && NextItem.Token.Length < 10)
                    {
                        NextItem.Token = Guid.NewGuid().ToString();

                        //如果上一章节已经存在了则就用数据库中的记录
                        var Temprecords = docentitys.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                        if (Temprecords.Count() > 0)
                        {
                            NextItem.Token = Temprecords.ElementAt(0).下一章.Value.ToString();
                        }
                    }
                    //得到内容
                    string content = "";

                    try
                    {
                        content = documentAnalyse.GetContent(CurrentItem.Url.GetWeb());
                    }
                    catch
                    {



                    }
                    //添加到数据库
                    entities.AddTo文章表(new TygModel.文章表()
                    {
                        GUID = o.GUID,
                        书名 = o.书名,
                        分类标识 = o.分类标识,
                        本记录GUID =Guid.Parse( CurrentItem.Token),
                        创建时间 = DateTime.Now,
                        分类名称 = o.分类表.分类名称,
                        章节名 = CurrentItem.Title,
                        上一章 = PreviousItem == null ? Guid.Empty :Guid.Parse( PreviousItem.Token),
                        下一章 = NextItem == null ? Guid.Empty : Guid.Parse( NextItem.Token),
                        最后访问时间 = DateTime.Now,
                        内容 = content,
                        采集用的URL1 = CurrentItem.Url.ToString()
                    });
                    //打印状态
                    System.Diagnostics.Debug.WriteLine("采集内容:" + CurrentItem.Title + "成功,正文长度:" + content.Length);
                }
                else
                {
                    //如果记录的内容比较少则更新内容
                    var Temprecords = docentitys.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                    if (Temprecords.Count() > 0)
                    {

                        //如果内容无效
                        if ( //中文小于6
                          string.Join("", System.Text.RegularExpressions.Regex.Matches(Temprecords.ElementAt(0).内容, @"[\u4e00-\u9fa5\d\w１２３４５６７８９～！!·＃￥％……—＊（）——＋／”》“‘’，；。、？，：…《]+[\u4e00-\u9fa5１２３４５６７８９～！!·＃￥％……—＊（!）——＋／”》“‘，’\r\n；。、？，：…《]", System.Text.RegularExpressions.RegexOptions.Multiline)
                            .Cast<System.Text.RegularExpressions.Match>().Select(p => p.Value).ToArray()
                            ).Length < 6
                            )
                        {
                            //更新记录
                            var reccords = o.文章表.Where(p => p.章节名.Trim() == CurrentItem.Title.Trim());
                            if (reccords.Count() > 0)
                            {
                                var record = reccords.ElementAt(0);
                                try
                                {
                                    record.采集用的URL1 = CurrentItem.Url.ToString();
                                    record.内容 = documentAnalyse.GetContent(CurrentItem.Url.GetWeb());
                                }
                                catch { }
                                //打印状态
                                System.Diagnostics.Debug.WriteLine("更新记录:" + CurrentItem.Title + "成功,正文长度:" + record.内容.Length);
                            }


                        }

                    }
                }



            }





        }

    }


}

/// <summary>
/// url扩展
/// </summary>
public class UrlExtentEntity : Skybot.Collections.Analyse.ListPageContentUrl
{
    private string _Token = "";

    /// <summary>
    /// 用户标识
    /// </summary>
    public string Token
    {
        get { return _Token; }
        set { _Token = value; }
    }
}