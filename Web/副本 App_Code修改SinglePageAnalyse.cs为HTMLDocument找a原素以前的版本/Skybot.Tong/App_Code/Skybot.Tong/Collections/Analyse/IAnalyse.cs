using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybot.Collections.Analyse
{

    /// <summary>
    /// 内容接口
    /// </summary>
    public interface IAnalyse
    {
        /// <summary>
        /// 内容路径表达式 ,用户数据表达式,如果不能生成表达式则返回 null
        /// </summary>
        string PathExpression { get; }

        /// <summary>
        /// 用户数据
        /// </summary>
        object UserToKen
        {
            get;
            set;
        }

        /// <summary>
        /// 分析器产生的结果
        /// 可以保存一些内容,如列表中所产生的所有可用的页面url集合
        /// 页面表中,分析出来的各种数据,如,作者,分类,等
        /// </summary>
        object GetResult();

        /// <summary>
        /// 传入索引页面url 与 索引页面所得到的 内容页面url集合
        /// </summary>
        /// <param name="IndexPageUrl">索引页面url</param>
        /// <param name="ContentPageUrls">内容页面url集合</param>
        /// <returns>返回表达式 类似到 xpath</returns>
        string GetPathExpression(ListPageContentUrl IndexPageUrl, IEnumerable<ListPageContentUrl> ContentPageUrls);

        /// <summary>
        /// 从原始内容获取当前指定表达式的内容
        /// </summary>
        /// <param name="Content">原始内容</param>
        /// <returns>返回结果内容</returns>
        string GetContent(string Content);
    }
}