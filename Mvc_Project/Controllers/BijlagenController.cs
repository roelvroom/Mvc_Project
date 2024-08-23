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
    public class BijlagenController : Controller
    {
        private readonly Mvc_ProjectContext _context;

        public BijlagenController(Mvc_ProjectContext context)
        {
            _context = context;
        }

        // GET: Bijlagen
        public async Task<IActionResult> Index()
        {
              return _context.Bijlagen != null ? 
                          View(await _context.Bijlagen.ToListAsync()) :
                          Problem("Entity set 'Mvc_ProjectContext.Bijlagen'  is null.");
        }

        // GET: Bijlagen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bijlagen == null)
            {
                return NotFound();
            }

            var bijlagen = await _context.Bijlagen
                .FirstOrDefaultAsync(m => m.BijlagenId == id);
            if (bijlagen == null)
            {
                return NotFound();
            }

            return View(bijlagen);
        }

        // GET: Bijlagen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bijlagen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BijlagenId,AankoopId,Naam")] Bijlagen bijlagen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bijlagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bijlagen);
        }

        // GET: Bijlagen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bijlagen == null)
            {
                return NotFound();
            }

            var bijlagen = await _context.Bijlagen.FindAsync(id);
            if (bijlagen == null)
            {
                return NotFound();
            }
            return View(bijlagen);
        }

        // POST: Bijlagen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BijlagenId,AankoopId,Naam")] Bijlagen bijlagen)
        {
            if (id != bijlagen.BijlagenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bijlagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BijlagenExists(bijlagen.BijlagenId))
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
            return View(bijlagen);
        }

        // GET: Bijlagen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bijlagen == null)
            {
                return NotFound();
            }

            var bijlagen = await _context.Bijlagen
                .FirstOrDefaultAsync(m => m.BijlagenId == id);
            if (bijlagen == null)
            {
                return NotFound();
            }

            return View(bijlagen);
        }

        // POST: Bijlagen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bijlagen == null)
            {
                return Problem("Entity set 'Mvc_ProjectContext.Bijlagen'  is null.");
            }
            var bijlagen = await _context.Bijlagen.FindAsync(id);
            if (bijlagen != null)
            {
                _context.Bijlagen.Remove(bijlagen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BijlagenExists(int id)
        {
          return (_context.Bijlagen?.Any(e => e.BijlagenId == id)).GetValueOrDefault();
        }
    }
}
