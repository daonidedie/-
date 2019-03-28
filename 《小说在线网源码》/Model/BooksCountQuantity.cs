using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BooksCountQuantity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Times { get; set; }
        public int Weeks { get; set; }
        public int Months { get; set; }
        public DateTime Times2 { get; set; }
        public string BookName { get; set; }
        public string TypeName { get; set; }
    }
}
