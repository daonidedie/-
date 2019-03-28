using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace Skybot.Collections
{
    using Skybot.Collections.Content;

    using Skybot.Collections.Analyse;
    using Skybot.Collections.Search;

    /// <summary>
    /// 得到页面内容的数据实体,
    /// 用于初始化类型
    /// </summary>
    public class StoryGetDocument : IGetDocuemnt
    {

        BaiduSearchCompare BaiduSearch = new BaiduSearchCompare();

        /// <summary>
        /// 通用操作方法
        /// </summary>
        public Skybot.Tong.TongUse tong = new Skybot.Tong.TongUse();

        #region 构造函数

        public StoryGetDocument()
        {
            //初始化搜索
            Search = BaiduSearch;

        }



        #endregion


        #region IGetDocuemnt 成员

        /// <summary>
        /// 搜索关键字,并通过内部算法取到了有效列表集合事件
        /// </summary>
        public event EventHandler<GetDocuemntWorkAnsycCompleteEventArgs> DoWorkAnsycComplete;

        /// <summary>
        /// 指定关键字
        /// </summary>
        public string KeyWord
        {
            get;
            protected set;
        }

        /// <summary>
        /// 搜索实例
        /// </summary>
        public ISearchCompare Search
        {
            get;
            protected set;
        }

        /// <summary>
        /// 索引集合,PageListContent
        /// </summary>
        public System.Collections.Generic.IEnumerable<IDocumnetContent> IndexPages
        {
            get;
            protected set;
        }

        /// <summary>
        /// 写入记录的委托
        /// </summary>
        public Action<IDocumnetContent> FuncWriteRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 内容数据页面
        /// </summary>
        public System.Collections.Generic.IEnumerable<IDocumnetContent> ContentPages
        {
            get;
            protected set;
        }

        /// <summary>
        /// 调用FuncWriteRecord中的委托,
        /// 循环调用所有ContentPages中的数据
        /// </summary>
        public void WirteRecord()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 开始获取文档数据,返回得到的文档数据
        /// </summary>
        /// <param name="keyWord">指定的关键字</param>
        /// <param name="Token">用户状态数据</param>
        /// <returns>返回搜索到的列表集合</returns>
        public System.Collections.Generic.IEnumerable<IDocumnetContent> DoWork(string keyWord)
        {
            return DoWork(keyWord, null);
        }

        /// <summary>
        /// 开始获取文档数据,返回得到的文档数据
        /// </summary>
        /// <param name="keyWord">指定的关键字</param>
        /// <param name="Token">用户状态数据</param>
        /// <returns>返回搜索到的列表集合</returns>
        public System.Collections.Generic.IEnumerable<IDocumnetContent> DoWork(string keyWord, object Token)
        {

            KeyWord = keyWord;
            BaiduSearch.KeyWord = keyWord;
            //得到内容页面
            List<ListPageContentUrl> ListPageUrlStrings = BaiduSearch.DoWork(keyWord);
            //用于返回的结果
            List<IDocumnetContent> indexpages = new List<IDocumnetContent>();
            IndexPages = indexpages;
            //将列表页面url转换成为 PageListContent 
            ListPageUrlStrings.ForEach((o) =>
            {
                //列表分析实例对象
                SingleListPageAnalyse Analyse = new SingleListPageAnalyse(o.Url.ToString())
                     {
                         UserToKen = Token,
                     };

                indexpages.Add(new PageListContent()
                {
                    Analyse = Analyse,
                    KeyWord = keyWord,
                    PageUrl = o.Url.ToString(),
                    UserToKen = Token,
                    StartTime = DateTime.Now,
                    SearchCompare = Search,
                    Content = "",
                    OriginContent = o.Url.GetWeb(),
                    Title = o.Title,
                    CompleteTime = DateTime.MinValue,
                });

                #region 开始采集文章内容
                //new TextContent()
                //{
                //    Analyse = Analyse,
                //    CompleteTime = DateTime.MaxValue
                //};

                #endregion
            });

            


            return indexpages;
        }


        /// <summary>
        /// 异步开始执行搜索操作
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="Token">数据标识</param>
        public void DoWorkAsync(string keyWord, object Token)
        {
            //使用 Task 异步调用事件
            System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<IDocumnetContent>>.Factory.StartNew(
                () =>
                {
                    System.Collections.Generic.IEnumerable<IDocumnetContent> result = DoWork(keyWord, Token);
                    //返回结果
                    if (DoWorkAnsycComplete != null)
                    {
                        //调用事件
                        DoWorkAnsycComplete(this, new GetDocuemntWorkAnsycCompleteEventArgs()
                        {
                            Result = result,
                            ToKen = Token
                        });
                    }
                    return result;
                });
        }

        #endregion


    }
}