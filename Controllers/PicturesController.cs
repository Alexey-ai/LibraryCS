using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryCS.Data;
using LibraryCS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryCS.Controllers
{
    public class PicturesController : Controller
    {
        private readonly LibraryContext _context;
        private IHostingEnvironment _appEnvironment;

        public PicturesController(LibraryContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            return View(_context.Pictures.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                PictureModel picture = new PictureModel { Name = uploadedFile.FileName, Path = path };
                _context.Pictures.Add(picture);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddFileToBook(int? id, IFormFile uploadedFile)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                PictureModel picture = new PictureModel { Name = uploadedFile.FileName, Path = path };
                _context.Pictures.Add(picture);
                _context.SaveChanges();
                var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookID == id);
                book.BookPicturesPath = picture.Path;
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            return Redirect(("/Books/Details/"+id.ToString()));
        }
        public async Task<IActionResult> AddFileToReader(int? id, IFormFile uploadedFile)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                PictureModel picture = new PictureModel { Name = uploadedFile.FileName, Path = path };
                _context.Pictures.Add(picture);
                _context.SaveChanges();
                var reader = await _context.Readers
                .FirstOrDefaultAsync(r => r.ID == id);
                reader.ReadersPicsPath = picture.Path;
                _context.Readers.Update(reader);
                _context.SaveChanges();
            }
            return Redirect(("/Readers/Details/" + id.ToString()));
        }

        public async Task<IActionResult> AddPictureToBook(int? id, IFormFile uploadedFile)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                PictureModel picture = new PictureModel { Name = uploadedFile.FileName, Path = path };
                var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookID == id);
                book.Pictures.Add(picture);
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            return Redirect(("/Books/Details/" + id.ToString()));
        }
        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Pictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }
            return View(file);
        }

        // GET: File/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Pictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var file = await _context.Pictures.FindAsync(id);
            _context.Pictures.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}