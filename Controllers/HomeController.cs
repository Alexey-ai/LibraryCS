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

        public IActionResult InitialBase()
        {
            if (_context.Books.Any() || _context.Readers.Any())
            {
                return Redirect("/Home/Index/");
            }
            var Books = new List<Book>
            {
            new Book{NameBook="Sobranie sochineniy",Author="Lenin V.I.",Description="Full set of works Lenin",Edition="RED ARMY",Genre="Political",Aviability=true,BookAddDate=DateTime.Now,BookPicturesPath="/Files/noimage.png"},
            new Book{NameBook="Number the Stars",Author="Lois Lowry",Description="Book about second world war",Edition="RED ARMY",Genre="Drama",Aviability=true,BookAddDate=DateTime.Now,BookPicturesPath="/Files/noimage.png"},
            new Book{NameBook="Winnie-the-Pooh",Author="A. A. Milne",Description="Fairy tale about bear",Edition="RED ARMY",Genre="Kids book",Aviability=true,BookAddDate=DateTime.Now,BookPicturesPath="/Files/noimage.png"},
            new Book{NameBook="Northern Lights",Author="Philip Pullman",Description="Fantasy world",Edition="RED ARMY",Genre="Fantasy",Aviability=true,BookAddDate=DateTime.Now,BookPicturesPath="/Files/noimage.png"},
            new Book{NameBook="Prestuplenie i nakazanie",Author="F.M.Dostoevsky",Description="Roman",Edition="RED ARMY",Genre="Drama",Aviability=true,BookAddDate=DateTime.Now,BookPicturesPath="/Files/noimage.png"}
            };
            Books.ForEach(s => _context.Books.Add(s));
            _context.SaveChanges();

            var Readers = new List<Reader>
            {
            new Reader{ReaderName="Alex",ReaderLastName="Ivanov", BirthdayDate=DateTime.Parse("1989-05-05"), Adress="Kaluga Jukov st.", Phone="8-900-555-2020",Passport="2809-546237", AddDate=DateTime.Now,ReadersPicsPath="/Files/noimage.png"},
            new Reader{ReaderName="Olga",ReaderLastName="Petrova", BirthdayDate=DateTime.Parse("1993-10-05"), Adress="Moscow Neglinnaya st.", Phone="8-900-555-1000",Passport="2809-343337", AddDate=DateTime.Now,ReadersPicsPath="/Files/noimage.png"},
            new Reader{ReaderName="Fu",ReaderLastName="Kim", BirthdayDate=DateTime.Parse("1998-10-10"), Adress="New York Broadway st.", Phone="8-900-555-6767",Passport="2809-234322", AddDate=DateTime.Now,ReadersPicsPath="/Files/noimage.png"},
            new Reader{ReaderName="Vasil",ReaderLastName="Johnson", BirthdayDate=DateTime.Parse("2001-12-12"), Adress="Kaluga Pukhova st.", Phone="8-900-555-2666",Passport="2809-34333", AddDate=DateTime.Now,ReadersPicsPath="/Files/noimage.png"}
            };

            Readers.ForEach(s => _context.Readers.Add(s));
            _context.SaveChanges();

            return Redirect("/Home/Index/");
        }
    }
}
