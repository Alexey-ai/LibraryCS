using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCS.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookID { get; set; }
        public string NameBook { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Edition { get; set; }
        public string Genre { get; set; }
        public bool Aviability { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookAddDate { get; set; }
        public Order Order { get; set; }
    }
}
