using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Skybot.Collections.Analyse;

/// <summary>
/// 搜索比较完成事件
/// </summary>
public class SearchCompareEventArgs:EventArgs
{
    /// <summary>
    /// 用户自定义数据
    /// </summary>
    public object ToKen
    {
        get;
        set;
    }

    /// <summary>
    /// 返回列表的结果
    /// </summary>
    public System.Collections.Generic.List<ListPageContentUrl> Reslut
    {
        get;
        set;
    }
}
