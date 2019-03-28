using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Collections.Analyse
{
    /// <summary>
    /// 一个表达式
    /// </summary>
    public class ExpressionEntity
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Initalize()
        {
            NodeArr = Expression.Split('/');
            NodeLayIndex = NodeArr.Count() - 1;
        }

        private string _Expression = string.Empty;
        /// <summary>
        /// 表达式
        /// </summary>
        public string Expression
        {
            get { return _Expression; }
            set
            {
                _Expression = value;
                Initalize();
            }
        }
        /// <summary>
        /// 包括/的节点数量即，深度 从0开始
        /// </summary>
        public int NodeLayIndex { get; set; }

        /// <summary>
        /// 节点集合  
        /// </summary>
        public System.Collections.Generic.IEnumerable<string> NodeArr { get; set; }

       /// <summary>
       /// 重写返回字符串
       /// </summary>
        public override string ToString()
        {
            return Expression;
        }
    }
}
