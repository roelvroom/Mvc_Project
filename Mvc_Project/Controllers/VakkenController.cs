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
    public class VakkenController : Controller
    {
        private readonly Mvc_ProjectContext _context;

        public VakkenController(Mvc_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Vakken != null ? 
                          View(await _context.Vakken.ToListAsync()) :
                          Problem("Entity set 'Mvc_ProjectContext.Vak'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakId,Naam,Verwijderd")] Vak vak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vak);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }
            return View(vak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakId,Naam,Verwijderd")] Vak vak)
        {
            if (id != vak.VakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            return View(vak);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vakken == null)
            {
                return Problem("Entity set 'Mvc_ProjectContext.Vak'  is null.");
            }
            var vak = await _context.Vakken.FindAsync(id);
            if (vak != null)
            {
                _context.Vakken.Remove(vak);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakExists(int id)
        {
          return (_context.Vakken?.Any(e => e.VakId == id)).GetValueOrDefault();
        }
    }
}
