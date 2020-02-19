using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCS.Models
{
    public class PictureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

    }
}
