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
    public class GebruikerController : Controller
    {
        private readonly Mvc_ProjectContext _context;

        public GebruikerController(Mvc_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Gebruikers != null ? 
                          View(await _context.Gebruikers.ToListAsync()) :
                          Problem("Entity set 'Mvc_ProjectContext.Gebruiker'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gebruikers == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruikers
                .FirstOrDefaultAsync(m => m.GebruikerId == id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return View(gebruiker);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GebruikerId,Naam,Voornaam,Initialen,GebruikersNaam,Wachtwoord,Email,Rol")] Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                _context.Gebruikers.Add(gebruiker);
                _context.Add(gebruiker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gebruiker);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gebruikers == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }
            return View(gebruiker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GebruikerId,Naam,Voornaam,Initialen,GebruikersNaam,Wachtwoord,Email,Rol")] Gebruiker gebruiker)
        {
            if (id != gebruiker.GebruikerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Gebruikers.Update(gebruiker);
                    _context.Update(gebruiker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GebruikerExists(gebruiker.GebruikerId))
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
            return View(gebruiker);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gebruikers == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruikers
                .FirstOrDefaultAsync(m => m.GebruikerId == id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return View(gebruiker);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gebruikers == null)
            {
                return Problem("Entity set 'Mvc_ProjectContext.Gebruiker'  is null.");
            }
            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker != null)
            {
                _context.Gebruikers.Remove(gebruiker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GebruikerExists(int id)
        {
          return (_context.Gebruikers?.Any(e => e.GebruikerId == id)).GetValueOrDefault();
        }
    }
}
