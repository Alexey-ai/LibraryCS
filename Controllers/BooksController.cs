using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryCS.Data;
using LibraryCS.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;



namespace LibraryCS.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        private IHostingEnvironment _appEnvironment;
        //private readonly IWebHostEnviroment _webHostEnviroment;

        public BooksController(LibraryContext context,IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int? pageSize)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BookIDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "BookID_desc" : "";
            ViewData["NameBookSortParm"] = sortOrder == "NameBook" ? "NameBook_desc" : "NameBook";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
            ViewData["EditionSortParm"] = sortOrder == "Edition" ? "Edition_desc" : "Edition";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "Genre_desc" : "Genre";
            ViewData["AviabilitySortParm"] = sortOrder == "Aviability" ? "Aviability_desc" : "Aviability";
            ViewData["DateSortParm"] = sortOrder == "BookAddDate" ? "BookAddDate_desc" : "BookAddDate";

            if (searchString!=null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["PageSize"] = pageSize;

            var books = from b in _context.Books
                       select b;

            if(!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.NameBook.Contains(searchString)
                ||b.Author.Contains(searchString)
                || b.Description.Contains(searchString)
                || b.Edition.Contains(searchString)
                || b.Genre.Contains(searchString)
                || b.BookID.ToString().Contains(searchString)
                || b.BookAddDate.ToString().Contains(searchString)
                );
            }

            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "BookID";
            }
            bool descending = false;

            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                books = books.OrderByDescending(b => EF.Property<object>(b, sortOrder));
            }
            else
            {
                books = books.OrderBy(b => EF.Property<object>(b, sortOrder));
            }

            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(),pageNumber ?? 1,pageSize ?? 5));
        }
       

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(p=>p.Pictures)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameBook,Author,Description,Edition,Genre,BookAddDate")] Book book)
        {
            
            if (ModelState.IsValid)
            {
                book.Aviability = true;
                book.BookPicturesPath = "/Files/noimage.png";
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,NameBook,Author,Description,Edition,Genre,Aviability,BookAddDate,BookPicturesPath")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books
                .Include(p => p.Pictures)
                .FirstOrDefaultAsync(m => m.BookID == id);

            foreach (PictureModel picture in book.Pictures)
            {
                _context.Pictures.Remove(picture);
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
