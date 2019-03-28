using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybot.Cache
{
    /// <summary>
    /// 缓存数据实体接口
    /// </summary>
    public interface ITabEnity
    {
        /// <summary>
        /// 是不民已经修改过了,默认为 false
        /// </summary>
        bool Modifyd { get; set; }
    }
}