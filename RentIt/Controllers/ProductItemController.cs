﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentIt.Models;

namespace RentIt.Controllers
{
    public class ProductItemController : Controller
    {
        private readonly RentItContext _context;

        public ProductItemController(RentItContext context)
        {
            _context = context;
        }

        // GET: ProductItem
        public async Task<IActionResult> Index()
        {
            var rentItContext = _context.ProductItem.Include(p => p.Product);
            return View(await rentItContext.ToListAsync());
        }

        // GET: ProductItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItem
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            return View();
        }

        // POST: ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductItemId,ProductId,Remarks,Code,Amount,Status")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productItem.ProductId);
            return View(productItem);
        }

        // GET: ProductItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItem.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productItem.ProductId);
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
                    _context.Update(productItem);
                    await _context.SaveChangesAsync();
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productItem.ProductId);
            return View(productItem);
        }

        // GET: ProductItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItem
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
            var productItem = await _context.ProductItem.FindAsync(id);
            _context.ProductItem.Remove(productItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemExists(int id)
        {
            return _context.ProductItem.Any(e => e.ProductItemId == id);
        }
    }
}