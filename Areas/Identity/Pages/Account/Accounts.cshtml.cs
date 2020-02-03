using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LibraryCS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryCS.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryCS.Areas.Identity.Pages.Account
{
    public class IndexViewModel : PageModel
    {
        private readonly LibraryAuthContext _authContext;

        public IndexViewModel(LibraryAuthContext authContext)
        {
            _authContext = authContext;
        }

        public IList<LibraryUser> LibraryUsers { get; set; }
        public async Task OnGetAsync()
        {
            LibraryUsers = await _authContext.Users.ToListAsync();           
        }

    }
}
