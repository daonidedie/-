using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybot.Cache
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// 数据实体  页面
    /// </summary>
    public class TabPageEntity :  ITabEnity
    {



        private bool _Modifyd = false;
        /// <summary>
        /// 修改状态
        /// </summary>
        public bool Modifyd
        {
            get { return _Modifyd; }
            set
            {
                _Modifyd = value;
                Book.Modifyd = true;
            }
        }

        /// <summary>
        /// 记录数据
        /// </summary>
        public TygModel.文章表 Record { get; set; }

        /// <summary>
        /// 当前页面所属于的书
        /// </summary>
        public TabBookEntity Book { get; set; }

        /// <summary>
        /// 数据库与文件同步状态,如果同步则为 true
        /// </summary>
        private bool _Issync = true;

        /// <summary>
        /// 数据库与文件同步状态,如果同步则为 true
        /// </summary>
        public bool Issync
        {
            get { return _Issync; }
            set { _Issync = value; }
        }


    }
}