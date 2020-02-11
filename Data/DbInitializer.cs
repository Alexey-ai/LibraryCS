using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryCS.Models;

namespace LibraryCS.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
            {
            }
        }
    }
}
