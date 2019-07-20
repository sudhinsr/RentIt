using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentIt.Models;

namespace RentIt.Controllers
{
    public class RentController : Controller
    {
        private readonly RentItContext _context;

        public RentController(RentItContext context)
        {
            _context = context;
        }

        // GET: Rent
        public async Task<IActionResult> Index()
        {
            var rentItContext = _context.Rent.Include(r => r.ProductItem);
            return View(await rentItContext.ToListAsync());
        }

        // GET: Rent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .Include(r => r.ProductItem)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rent/Create
        public IActionResult Create()
        {
            ViewData["ProductItemId"] = new SelectList(_context.ProductItem, "ProductItemId", "ProductItemId");
            return View();
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,ProductItemId,Amount,PhoneNo,AddressLine1,AddressLine2,Status,CreatedDate")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductItemId"] = new SelectList(_context.ProductItem, "ProductItemId", "ProductItemId", rent.ProductItemId);
            return View(rent);
        }

        // GET: Rent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["ProductItemId"] = new SelectList(_context.ProductItem, "ProductItemId", "ProductItemId", rent.ProductItemId);
            return View(rent);
        }

        // POST: Rent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentId,ProductItemId,Amount,PhoneNo,AddressLine1,AddressLine2,Status,CreatedDate")] Rent rent)
        {
            if (id != rent.RentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.RentId))
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
            ViewData["ProductItemId"] = new SelectList(_context.ProductItem, "ProductItemId", "ProductItemId", rent.ProductItemId);
            return View(rent);
        }

        // GET: Rent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .Include(r => r.ProductItem)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rent = await _context.Rent.FindAsync(id);
            _context.Rent.Remove(rent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _context.Rent.Any(e => e.RentId == id);
        }
    }
}
