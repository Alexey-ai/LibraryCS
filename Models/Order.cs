using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCS.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int ReaderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderReturnDate { get; set; }

        public Book Book { get; set; }
        public Reader Reader { get; set; }
    }
}
