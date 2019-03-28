using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skybot.Cache
{
    /// <summary>
    /// 用户的数据类型
    /// </summary>
    public enum RecordType
    {
        /// <summary>
        /// 没有
        /// </summary>
        [System.ComponentModel.Description("没有")]
        None,
        /// <summary>
        /// 页面
        /// </summary>
        [System.ComponentModel.Description("页面")]
        Page,
        /// <summary>
        /// 书
        /// </summary>
        [System.ComponentModel.Description("书")]
        Book,
        /// <summary>
        /// 分类
        /// </summary>
        [System.ComponentModel.Description("分类")]
        ClassEntity,

    }
}