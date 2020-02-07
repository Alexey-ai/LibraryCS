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
        public int BookID { get; set; }
        [Required]
        public string NameBook { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-' ''.']*$")]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Edition { get; set; }
        public string Genre { get; set; }

        public bool Aviability { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookAddDate { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<FileModel> FileModels { get; set; }

        public string FullName
        {
            get
            {
                return BookID + "--" + Author + "--" + NameBook;
            }
        }
    }
}
