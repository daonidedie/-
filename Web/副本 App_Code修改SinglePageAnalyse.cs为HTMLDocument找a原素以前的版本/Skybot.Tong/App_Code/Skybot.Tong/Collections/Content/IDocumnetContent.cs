using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Content
{
    /// <summary>
    /// 内容接口
    /// </summary>
    public interface IDocumnetContent
    {
        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// 原始内容
        /// </summary>
        string OriginContent
        {
            get;
            set;
        }

        /// <summary>
        /// 分析对象实例
        /// </summary>
        IAnalyse Analyse
        {
            get;
            set;
        }

        /// <summary>
        /// 用户数据
        /// </summary>
        object UserToKen
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页面URL
        /// </summary>
        string PageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 页面标题
        /// </summary>
        string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 开始获取时间
        /// </summary>
        DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 分析完成后产生结果的时间
        /// </summary>
        DateTime CompleteTime
        {
            get;
            set;
        }

        /// <summary>
        /// 关键字
        /// </summary>
        string KeyWord
        {
            get;
            set;
        }

        /// <summary>
        /// 用户搜索实例
        /// </summary>
        ISearchCompare SearchCompare
        {
            get;
            set;
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        void ReLoad();
    }
}