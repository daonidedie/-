using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using Skybot.Collections.Analyse;

/// <summary>
/// 搜索比较接口&#13;
/// 用于比较关键字列表,找到最合适的关键字列表,
/// 子类可以提供导出
/// </summary>
[System.ComponentModel.Composition.InheritedExport(typeof(ISearchCompare))]
public interface ISearchCompare
{
    /// <summary>
    /// 搜索关键字,并通过内部算法取到了有效列表集合事件
    /// </summary>
    event EventHandler<SearchCompareEventArgs> DoWorkAnsycComplete;
    /// <summary>
    /// 当前搜索的关键字
    /// </summary>
    string KeyWord
    {
        get;
    }

    /// <summary>
    /// 当前关键字搜索的搜索引擎页面列表
    /// </summary>
    List<string> SearchUrls
    {
        get;
    }

    /// <summary>
    /// 索引页面列表
    /// </summary>
    List<ListPageContentUrl> IndexPageUrls
    {
        get;
    }

    /// <summary>
    /// 默认在搜索引擎中搜索的分页页数
    /// </summary>
    /// <value>默认5</value>
    int SearchPageCount
    {
        get;
    }

    /// <summary>
    /// 异步开始执行搜索操作
    /// </summary>
    /// <param name="keyWord">关键字</param>
    /// <param name="Token">数据标识</param>
    void DoWorkAsync(string keyWord, object Token);

    /// <summary>
    /// 开始执行搜索操作
    /// </summary>
    /// <param name="keyWord">关键字</param>
    /// <returns>返回搜索到的列表集合</returns>
    List<ListPageContentUrl> DoWork(string keyWord);
}
