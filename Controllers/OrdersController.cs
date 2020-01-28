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
    public class OrdersController : Controller
    {
        private readonly LibraryContext _context;

        public OrdersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders.Include(o => o.Book).Include(o => o.Reader);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Book)
                .Include(o => o.Reader)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            BookAviableDropDownList();
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "FullName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,BookID,ReaderID,OrderDate,OrderReturnDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                var book = await _context.Books.FindAsync(order.BookID);
                book.Aviability = false;
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", order.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "ID", order.ReaderID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            BookAviableDropDownList();
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "FullName", order.ReaderID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,BookID,ReaderID,OrderDate,OrderReturnDate")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", order.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "ID", order.ReaderID);
            return View(order);
        }

        public IActionResult CloseOrderList()
        {

                var orderQuery = from o in _context.Orders.Include(o => o.Book).Include(o => o.Reader)
                                 orderby o.OrderID
                                 where o.OrderReturnDate == null
                                 select o;
                return View(orderQuery.ToList());
        }
        public async Task<IActionResult> CloseOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(o => o.Book).Include(o => o.Reader).FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Close
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseOrder([Bind("OrderID,BookID,ReaderID,OrderDate,OrderReturnDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Update(order);
               await _context.SaveChangesAsync();
                var book = await _context.Books.FindAsync(order.BookID);
                book.Aviability = true;
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Book)
                .Include(o => o.Reader)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            
            // возвращаем книгу только если удаляем активный заказ 
            if (order.OrderReturnDate==null)
            {
                var book = await _context.Books.FindAsync(order.BookID);
                book.Aviability = true;
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
        private void BookAviableDropDownList()
        {
            var booksQuery = from b in _context.Books
                             orderby b.BookID
                             where b.Aviability != false
                             select b;
            ViewData["BookID"] = new SelectList(booksQuery.AsNoTracking(), "BookID", "FullName");
        }
        private void BookNotAviableDropDownList()
        {
            var booksQuery = from b in _context.Books
                             orderby b.BookID
                             where b.Aviability == false
                             select b;
            ViewData["BookID"] = new SelectList(booksQuery.AsNoTracking(), "BookID", "FullName");
        }
    }
}
