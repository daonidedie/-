using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class NewBooksIntroduce
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; }
        public string Images { get; set; }
        public string BookIntroduction { get; set; }
        public DateTime AddTime { get; set; }
        public string StateName { get; set; }
        public string TypeName { get; set; }
    }
}
