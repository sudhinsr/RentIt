using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentIt.Models;
using RentIt.Repository.Interface;

namespace RentIt.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRentRepository _rentRepository;

        public PaymentController(IPaymentRepository paymentRepository, IRentRepository rentRepository)
        {
            _paymentRepository = paymentRepository;
            _rentRepository = rentRepository;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var rentItContext = _paymentRepository.Query().Include(p => p.Rent);
            return View(await rentItContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentRepository.Query()
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
            ViewData["RentId"] = new SelectList(_rentRepository.Query(), "RentId", "RentId");
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
                await _paymentRepository.InsertAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RentId"] = new SelectList(_rentRepository.Query(), "RentId", "RentId", payment.RentId);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentRepository.GetAsync(id.Value);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["RentId"] = new SelectList(_rentRepository.Query(), "RentId", "RentId", payment.RentId);
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
                    await _paymentRepository.UpdateAsync(payment);
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
            ViewData["RentId"] = new SelectList(_rentRepository.Query(), "RentId", "RentId", payment.RentId);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentRepository.Query()
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
            await _paymentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _paymentRepository.Query().Any(e => e.PaymentId == id);
        }
    }
}
