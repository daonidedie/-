using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BookNumberTable
    {
        public int BookId { get; set; }//小说Id
        public string BookName { get; set; }//小说名称
        public string Sections { get; set; }//最新章节
        public string UserName { get; set; }//小说作者
        public string UpdateTime { get;set; }//最后更新时间
        public string TypeName { get; set; }//小说类型
        public string State { get; set; }//小说状态
        public string SectiuonId { get; set; }
    }
}
