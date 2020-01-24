using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryCS.Models;
using LibraryCS.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            LibraryViewModel data =
            new LibraryViewModel()
            {
                BooksCount = _context.Books.Count(),
                ReadersCount = _context.Readers.Count(),
                OrderCount = _context.Orders.Count()
            };

            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
