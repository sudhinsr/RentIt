using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentIt.Models;
using RentIt.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RentIt.Controllers
{
    public class RentController : Controller
    {
        private readonly IRentRepository _rentRepository;
        private readonly IProductItemRepository _productItemRepository;

        public RentController(IRentRepository rentRepository, IProductItemRepository productItemRepository)
        {
            _rentRepository = rentRepository;
            _productItemRepository = productItemRepository;
        }

        // GET: Rent
        public async Task<IActionResult> Index()
        {
            var rentItContext = _rentRepository.Query().Include(r => r.ProductItem);
            return View(await rentItContext.ToListAsync());
        }

        // GET: Rent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _rentRepository.Query()
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
            ViewData["ProductItemId"] = new SelectList(_productItemRepository.Query(), "ProductItemId", "Code");
            return View();
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductItemId,Amount,PhoneNo,AddressLine1,AddressLine2,Status")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                rent.CreatedDate = DateTime.UtcNow;
                await _rentRepository.InsertAsync(rent);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductItemId"] = new SelectList(_productItemRepository.Query(), "ProductItemId", "Code", rent.ProductItemId);
            return View(rent);
        }

        // GET: Rent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _rentRepository.GetAsync(id.Value);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["ProductItemId"] = new SelectList(_productItemRepository.Query(), "ProductItemId", "Code", rent.ProductItemId);
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
                    rent.CreatedDate = DateTime.UtcNow;
                    await _rentRepository.UpdateAsync(rent);
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
            ViewData["ProductItemId"] = new SelectList(_productItemRepository.Query(), "ProductItemId", "Code", rent.ProductItemId);
            return View(rent);
        }

        // GET: Rent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _rentRepository.Query()
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
            await _rentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _rentRepository.Query().Any(e => e.RentId == id);
        }
    }
}
