using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Data;
using Mvc_Project.Models;

namespace Mvc_Project.Controllers
{
    public class ProductenController : Controller
    {
        private readonly Mvc_ProjectContext _context;

        public ProductenController(Mvc_ProjectContext context)
        {
            _context = context;
        }

        // GET: Producten
        public async Task<IActionResult> Index()
        {
              return _context.Producten != null ? 
                          View(await _context.Producten.ToListAsync()) :
                          Problem("Entity set 'Mvc_ProjectContext.Product'  is null.");
        }

        // GET: Producten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producten == null)
            {
                return NotFound();
            }

            var product = await _context.Producten
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Producten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,AankoopId,Naam,Prijs,Hoevelheid")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Producten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producten == null)
            {
                return NotFound();
            }

            var product = await _context.Producten.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Producten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,AankoopId,Naam,Prijs,Hoevelheid")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Producten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producten == null)
            {
                return NotFound();
            }

            var product = await _context.Producten
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Producten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producten == null)
            {
                return Problem("Entity set 'Mvc_ProjectContext.Product'  is null.");
            }
            var product = await _context.Producten.FindAsync(id);
            if (product != null)
            {
                _context.Producten.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Producten?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
