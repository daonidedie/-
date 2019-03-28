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
    /// 数据实体二 书名
    /// </summary>
    public class TabBookEntity :  ITabEnity
    {

        #region 初始化数据的方法
        
        #endregion


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
                Typeclass.Modifyd = true;
            }
        }

        private System.Collections.Concurrent.ConcurrentDictionary<string, TabPageEntity> _Pages = new ConcurrentDictionary<string, TabPageEntity>();

        /// <summary>
        /// 包括的所有章节集合 ,默认为 章节 GUID , Key 表示 GUID, 或者ID
        /// 不参加序列化
        /// 需要时可以使用 扩展方法获得
        /// </summary>
        internal System.Collections.Concurrent.ConcurrentDictionary<string,TabPageEntity> Pages
        {
            get { return _Pages; }
        }

        private TygModel.书名表 _Record = null;
        /// <summary>
        /// 记录 此属性需要从数据库查询
        /// </summary>
        public TygModel.书名表 Record
        {
            get { return _Record; }
            set { _Record = value; }
        }

        /// <summary>
        /// 分类
        /// </summary>
        public TabClassEntity Typeclass { get; set; }

    }
}