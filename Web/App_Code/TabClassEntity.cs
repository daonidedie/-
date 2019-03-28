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
    /// 数据实体三 分类
    /// </summary>
    public class TabClassEntity :  ITabEnity
    {
        public bool Modifyd { get; set; }

        private ConcurrentDictionary<string,TabBookEntity> _Books = new ConcurrentDictionary<string, TabBookEntity>();
        /// <summary>
        /// 包括的当前分类下所有书的索引,默认为 章节 GUID , Key 表示 GUID, 或者ID
        /// </summary>
        public ConcurrentDictionary<string, TabBookEntity> Books
        {
            get { return _Books; }
        }

        public TygModel.分类表 Record { get; set; }
    }

}