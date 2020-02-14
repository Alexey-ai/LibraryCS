using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryCS.Models
{
    public class Reader
    {
        public int ID { get; set; }

        [Required][StringLength(30,MinimumLength =2)][RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string ReaderName { get; set; }

        [Required][StringLength(30, MinimumLength = 3)][RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string ReaderLastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }
        public string ReadersPicsPath { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<FileModel> FileModels { get; set; }

        [Display(Name ="Full Name")]
        public string FullName
        {
            get
            {
                return ID + "--" + ReaderName + "--" + ReaderLastName;
            }
        }
        public int Age 
        {
            get
            {
                return (DateTime.Now - BirthdayDate).Days / 365; 
            }
        }
    }
}
