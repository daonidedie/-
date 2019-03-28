using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 采集应用程序.Capability
{

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
}
