using System;
using System.Collections.Generic;
using System.Text;
using LibraryCS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryCS.Data
{
    public class LibraryAuthContext : IdentityDbContext<LibraryUser>
    {
        public LibraryAuthContext(DbContextOptions<LibraryAuthContext> options)
            : base(options)
        {
        }
    }
}
