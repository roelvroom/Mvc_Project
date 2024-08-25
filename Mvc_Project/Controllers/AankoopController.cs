using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
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
        private readonly ILogger<AankoopController> _logger;


        public AankoopController(Mvc_ProjectContext context, ILogger<AankoopController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Aankoop
        public async Task<IActionResult> Index()
        {
            // Verkrijg de rol van de huidige gebruiker
            var userRoles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            var isDirectie = userRoles.Contains("Directie");

            var aankopen = isDirectie
                ? await _context.Aankopen.Where(a => !a.GoedGekeurd).ToListAsync()
                : await _context.Aankopen.ToListAsync();

            return View(aankopen);
        }

        /*[HttpPost]
        public async Task<IActionResult> Afwijzen(int id)
        {
            var aankoop = await _context.Aankopen.FindAsync(id);
            if (aankoop != null)
            {
                _context.Aankopen.Remove(aankoop);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Goedkeuren(int id)
        {
            var aankoop = await _context.Aankopen.FindAsync(id);
            if (aankoop != null)
            {
                aankoop.GoedGekeurd = true;
                _context.Update(aankoop);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }*/

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aankopen == null)
            {
                return NotFound();
            }

            var aankoop = await _context.Aankopen
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
            ViewBag.Gebruikers = new SelectList(_context.Gebruikers, "GebruikerId", "Naam");
            ViewBag.Vakken = new SelectList(_context.Vakken, "VakId", "Naam");
            var producten = _context.Producten
                .Select(p => new
                {
                    p.ProductId,
                    DisplayValue = p.Naam + " - €" + p.Prijs + " - " + p.Hoevelheid
                })
                .ToList();

            ViewBag.Producten = new SelectList(producten, "ProductId", "DisplayValue");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AankoopId,VakId,Datum,Reden")] Aankoop aankoop)
        {
            // Haal de ingelogde gebruiker ID uit de sessie
            var gebruikerIdString = HttpContext.Session.GetString("GebruikerId");

            var geselecteerdProduct = await _context.Producten
               .Where(p => p.ProductId == aankoop.ProductId)
               .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(gebruikerIdString))
            {
                ModelState.AddModelError("", "Je moet ingelogd zijn om een aankoop te maken.");
                ViewBag.Gebruikers = new SelectList(await _context.Gebruikers.ToListAsync(), "GebruikerId", "Naam");
                return View(aankoop);
            }

            if (int.TryParse(gebruikerIdString, out int gebruikerId))
            {
                aankoop.NaamAanvragerId = gebruikerId;

                _context.Add(aankoop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Fout bij het ophalen van gebruikersinformatie.");
            }

            ViewBag.Gebruikers = new SelectList(await _context.Gebruikers.ToListAsync(), "GebruikerId", "Naam");
            ViewBag.Vakken = new SelectList(await _context.Vakken.ToListAsync(), "VakId", "Naam");
            var producten = _context.Producten
                .Select(p => new
                {
                    p.ProductId,
                    DisplayValue = p.Naam + " - €" + p.Prijs + " - " + p.Hoevelheid
                })
                .ToList();


            ViewBag.Gebruikers = new SelectList(await _context.Gebruikers.ToListAsync(), "GebruikerId", "Naam");
            ViewBag.Producten = new SelectList(producten, "ProductId", "DisplayValue");
            return View(aankoop);
        }


        // GET: Aankoop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aankopen == null)
            {
                return NotFound();
            }

            var aankoop = await _context.Aankopen.FindAsync(id);
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
            if (id == null || _context.Aankopen == null)
            {
                return NotFound();
            }

            var aankoop = await _context.Aankopen
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
        public async Task<IActionResult> DeleteConfirmed(int id, string reden)
        {
            if (_context.Aankopen == null)
            {
                return Problem("Entity set 'Mvc_ProjectContext.Aankoop'  is null.");
            }
            var aankoop = await _context.Aankopen.FindAsync(id);
            if (aankoop != null)
            {
                _context.Aankopen.Remove(aankoop);
                await _context.SaveChangesAsync();

                var gebruiker = await _context.Gebruikers.FindAsync(aankoop.NaamAanvragerId);
                if(gebruiker != null)
                {
                    var mailVerzenden = new MailMessage
                    {
                        From = new MailAddress("r0950679@student.thomasmore.be"),
                        Subject = $"Aankoop afgewezen",
                        Body = $"Een aankoop met de ID {id} is verwijderd om de volgende reden: {reden}",
                        IsBodyHtml = true
                    };
                    //ontvanger email
                    mailVerzenden.To.Add(gebruiker.Email);

                    using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Credentials = new NetworkCredential("roelvroom93@gmail.com", "RO5879el!");
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mailVerzenden);
                    }
                }

                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AankoopExists(int id)
        {
          return (_context.Aankopen?.Any(e => e.AankoopId == id)).GetValueOrDefault();
        }
    }
}
