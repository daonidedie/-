using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Skybot.Collections.Content;

namespace Skybot.Collections
{
    using System.Linq;
    /// <summary>
    /// 得到文档内容的接口,通过为每个关键字创建
    /// </summary>
    public interface IGetDocuemnt
    {

        /// <summary>
        /// 搜索关键字,并通过内部算法取到了有效列表集合事件
        /// </summary>
        event EventHandler<GetDocuemntWorkAnsycCompleteEventArgs> DoWorkAnsycComplete;

        /// <summary>
        /// 指定关键字
        /// </summary>
        string KeyWord
        {
            get;
        }

        /// <summary>
        /// 搜索实例
        /// </summary>
        ISearchCompare Search
        {
            get;
        }

        /// <summary>
        /// 索引集合,PageListContent
        /// </summary>
        System.Collections.Generic.IEnumerable<IDocumnetContent> IndexPages
        {
            get;
        }

        /// <summary>
        /// 写入记录的委托
        /// </summary>
        Action<IDocumnetContent> FuncWriteRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 内容数据页面
        /// </summary>
        System.Collections.Generic.IEnumerable<IDocumnetContent> ContentPages
        {
            get;
        }

        /// <summary>
        /// 调用FuncWriteRecord中的委托,
        /// 循环调用所有ContentPages中的数据
        /// </summary>
        void WirteRecord();


        /// <summary>
        /// 开始获取文档数据,返回得到的文档数据
        /// </summary>
        /// <param name="keyWord">指定的关键字</param>
        /// <returns>返回搜索到的列表集合</returns>
        System.Collections.Generic.IEnumerable<IDocumnetContent> DoWork(string keyWord);


        /// <summary>
        /// 异步开始执行搜索操作
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="Token">数据标识</param>
        void DoWorkAsync(string keyWord, object Token);




    }
}