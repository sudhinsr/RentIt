using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentIt.Models;
using RentIt.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace RentIt.Controllers
{
    public class ProductItemController : Controller
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductRepository _productRepository;

        public ProductItemController(IProductItemRepository productItemRepository,
            IProductRepository productRepository)
        {
            _productItemRepository = productItemRepository;
            _productRepository = productRepository;
        }

        // GET: ProductItem
        public async Task<IActionResult> Index(int productId)
        {
            var productItems = await _productItemRepository.GetProductItemsAsync(productId);
            return View(productItems);
        }

        // GET: ProductItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _productItemRepository.Query()
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductItemId == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // GET: ProductItem/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_productItemRepository.Query(), "ProductId", "Code");
            return View();
        }

        // POST: ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Remarks,Code,Amount,Status")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                await _productItemRepository.InsertAsync(productItem);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_productRepository.Query(), "ProductId", "Code", productItem.ProductId);
            return View(productItem);
        }

        // GET: ProductItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _productItemRepository.GetAsync(id.Value);
            if (productItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_productItemRepository.Query(), "ProductId", "Code", productItem.ProductId);
            return View(productItem);
        }

        // POST: ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductItemId,ProductId,Remarks,Code,Amount,Status")] ProductItem productItem)
        {
            if (id != productItem.ProductItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productItemRepository.UpdateAsync(productItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemExists(productItem.ProductItemId))
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
            ViewData["ProductId"] = new SelectList(_productItemRepository.Query(), "ProductId", "ProductId", productItem.ProductId);
            return View(productItem);
        }

        // GET: ProductItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _productItemRepository.Query()
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductItemId == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // POST: ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productItemRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemExists(int id)
        {
            return _productItemRepository.Query().Any(e => e.ProductItemId == id);
        }
    }
}
