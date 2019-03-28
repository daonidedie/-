using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using Skybot.Collections.Analyse;

using System.Linq;

namespace Skybot.Collections.Content
{
    /// <summary>
    /// 页面列表
    /// </summary>
    public class PageListContent : Skybot.Collections.Content.IDocumnetContent
    {
        #region 构造函数
        /// <summary>
        /// 页面列表
        /// </summary>
        public PageListContent()
        {

        }
        #endregion


        /// <summary>
        /// 页面列表
        /// </summary>
        private List<ListPageContentUrl> _LinkList = new List<ListPageContentUrl>();
        /// <summary>
        /// 页面列表
        /// </summary>
        public List<ListPageContentUrl> LinkList
        {
            get { return _LinkList; }
            protected set { _LinkList = value; }
        }



        #region IDocumnetContent 成员



        private string _Content = "";
        /// <summary>
        /// 内容 
        /// </summary>
        public string Content
        {
            get
            {
                if (_Content.Length == 0)
                {
                    return string.Join("\r", _LinkList.Select(p => p.ToString()));
                }
                return _Content;
            }
            set { _Content = value; }
        }

        private string _OriginContent = "";
        /// <summary>
        /// 原始内容 
        /// </summary>
        public string OriginContent
        {
            get
            {
                if (_OriginContent.Length == 0)
                {
                    return string.Join("\r", Analyse.GetContent(null));
                }

                return _OriginContent;
            }
            set { _OriginContent = value; }
        }

        private IAnalyse _Analyse = null;

        /// <summary>
        /// 分析实例 实例化时将会阻止当前线程
        /// </summary>
        public IAnalyse Analyse
        {
            get { return _Analyse; }
            set
            {
                _Analyse = value;
                if (value != null)
                {
                    //指定数据
                    LinkList = (List<ListPageContentUrl>)value.GetResult();
                }
            }
        }


        public object UserToKen
        {
            get;
            set;
        }

        public string PageUrl
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime CompleteTime
        {
            get;
            set;
        }

        public string KeyWord
        {
            get;
            set;
        }

        public ISearchCompare SearchCompare
        {
            get;
            set;
        }

        public void ReLoad()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}