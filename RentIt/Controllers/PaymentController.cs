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
    public class PaymentController : Controller
    {
        private readonly RentItContext _context;

        public PaymentController(RentItContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var rentItContext = _context.Payment.Include(p => p.Rent);
            return View(await rentItContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Rent)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            ViewData["RentId"] = new SelectList(_context.Rent, "RentId", "RentId");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,RentId,PhoneNo")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.CreatedDate = DateTime.UtcNow;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RentId"] = new SelectList(_context.Rent, "RentId", "RentId", payment.RentId);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["RentId"] = new SelectList(_context.Rent, "RentId", "RentId", payment.RentId);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Amount,RentId,PhoneNo")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    payment.CreatedDate = DateTime.UtcNow;
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["RentId"] = new SelectList(_context.Rent, "RentId", "RentId", payment.RentId);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Rent)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
