using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Collections
{
    /// <summary>
    /// 异步开始执行搜索操作 完成事件
    /// </summary>
    public class GetDocuemntWorkAnsycCompleteEventArgs : EventArgs
    {
        /// <summary>
        /// 用户自定义数据
        /// </summary>
        public Object ToKen
        {
            get;

            set;
        }

        /// <summary>
        /// 搜索到的结果
        /// </summary>
        public System.Collections.Generic.IEnumerable<Skybot.Collections.Content.IDocumnetContent> Result
        {
            get;
            set;
        }
    }
}
