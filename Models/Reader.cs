using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCS.Models
{
    public class Reader
    {
        public int ID { get; set; }
        public string ReaderName { get; set; }
        public string ReaderLastName { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }
        public DateTime AddDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
