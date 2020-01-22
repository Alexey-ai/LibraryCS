using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryCS.Data;
using LibraryCS.Models;

namespace LibraryCS.Controllers
{
    public class ReadersController : Controller
    {
        private readonly LibraryContext _context;

        public ReadersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int? pageSize)
        {
            
            ViewData["LNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["AgeSortParm"] = sortOrder == "Age" ? "age_desc" : "Age";
            ViewData["AdressSortParm"] = sortOrder == "Adress" ? "adress_desc" : "Adress";
            ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewData["PassportSortParm"] = sortOrder == "Passport" ? "passport_desc" : "Passport";
            ViewData["CurrentSort"] = sortOrder;


            if (searchString!=null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["PageSize"] = pageSize;
            ViewData["CurrentFilter"] = searchString;

            var readers = from s in _context.Readers
                           select s;
            if(!String.IsNullOrEmpty(searchString))
            {
                readers = readers.Where(s => s.ReaderName.Contains(searchString) 
                || s.ReaderLastName.Contains(searchString)
                || s.Adress.Contains(searchString)
                || s.Phone.Contains(searchString)
                || s.Passport.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "lname_desc":
                    readers = readers.OrderByDescending(s => s.ReaderLastName);
                    break;
                case "Name":
                    readers = readers.OrderBy(s => s.ReaderName);
                    break;
                case "name_desc":
                    readers = readers.OrderByDescending(s => s.ReaderName);
                    break;
                case "Date":
                    readers = readers.OrderBy(s => s.AddDate);
                    break;
                case "date_desc":
                    readers = readers.OrderByDescending(s => s.AddDate);
                    break;
                case "Age":
                    readers = readers.OrderBy(s => s.Age);
                    break;
                case "age_desc":
                    readers = readers.OrderByDescending(s => s.Age);
                    break;
                case "Adress":
                    readers = readers.OrderBy(s => s.Adress);
                    break;
                case "adress_desc":
                    readers = readers.OrderByDescending(s => s.Adress);
                    break;
                case "Phone":
                    readers = readers.OrderBy(s => s.Phone);
                    break;
                case "phone_desc":
                    readers = readers.OrderByDescending(s => s.Phone);
                    break;
                case "Passport":
                    readers = readers.OrderBy(s => s.Passport);
                    break;
                case "passport_desc":
                    readers = readers.OrderByDescending(s => s.Passport);
                    break;
                default:
                    readers = readers.OrderBy(s => s.ReaderLastName);
                    break;
            }

            //int pageSize = 5;
            return View(await PaginatedList<Reader>.CreateAsync(readers.AsNoTracking(),pageNumber ?? 1,pageSize ?? 5));
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .Include(s=>s.Orders)
                .ThenInclude(e=>e.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
     [Bind("ReaderName,ReaderLastName,Age,Adress,Phone,Passport,AddDate")] Reader reader)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(reader);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.FindAsync(id);
           // var reader = await _context.Readers.SingleOrDefaultAsync(m => m.ID == id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ReaderName,ReaderLastName,Age,Adress,Phone,Passport,AddDate")] Reader reader)
        {
            if (id != reader.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaderExists(reader.ID))
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
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id,bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reader == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists" +
                    "see your system administrator.";
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if(reader==null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException)
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists" +
                    "see your system administrator.";
                return RedirectToAction(nameof(Delete), new {id, saveChangesError = true });

            }
        }

        private bool ReaderExists(int id)
        {
            return _context.Readers.Any(e => e.ID == id);
        }
    }
}
