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
    public class AankoopController : Controller
    {
        private readonly Mvc_ProjectContext _context;

        public AankoopController(Mvc_ProjectContext context)
        {
            _context = context;
        }

        // GET: Aankoop
        public async Task<IActionResult> Index()
        {
            return View();

            /*_context.Aankoop != null ?
                          View(await _context.Aankoop.ToListAsync()) :
                          Problem("Entity set 'Mvc_ProjectContext.Aankoop'  is null.");*/
        }

        // GET: Aankoop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aankoop == null)
            {
                return NotFound();
            }

            var aankoop = await _context.Aankoop
                .FirstOrDefaultAsync(m => m.AankoopId == id);
            if (aankoop == null)
            {
                return NotFound();
            }

            return View(aankoop);
        }

        // GET: Aankoop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aankoop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AankoopId,VakId,NaamAanvragerId,Datum,Reden,GoedGekeurd,Verwijderd")] Aankoop aankoop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aankoop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aankoop);
        }

        // GET: Aankoop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aankoop == null)
            {
                return NotFound();
            }

            var aankoop = await _context.Aankoop.FindAsync(id);
            if (aankoop == null)
            {
                return NotFound();
            }
            return View(aankoop);
        }

        // POST: Aankoop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AankoopId,VakId,NaamAanvragerId,Datum,Reden,GoedGekeurd,Verwijderd")] Aankoop aankoop)
        {
            if (id != aankoop.AankoopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aankoop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AankoopExists(aankoop.AankoopId))
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
            return View(aankoop);
        }

        // GET: Aankoop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aankoop == null)
            {
                return NotFound();
            }

            var aankoop = await _context.Aankoop
                .FirstOrDefaultAsync(m => m.AankoopId == id);
            if (aankoop == null)
            {
                return NotFound();
            }

            return View(aankoop);
        }

        // POST: Aankoop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aankoop == null)
            {
                return Problem("Entity set 'Mvc_ProjectContext.Aankoop'  is null.");
            }
            var aankoop = await _context.Aankoop.FindAsync(id);
            if (aankoop != null)
            {
                _context.Aankoop.Remove(aankoop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AankoopExists(int id)
        {
          return (_context.Aankoop?.Any(e => e.AankoopId == id)).GetValueOrDefault();
        }
    }
}
