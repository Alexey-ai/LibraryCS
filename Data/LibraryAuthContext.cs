using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryCS.Data
{
    public class LibraryAuthContext : IdentityDbContext
    {
        public LibraryAuthContext(DbContextOptions<LibraryAuthContext> options)
            : base(options)
        {
        }
    }
}
