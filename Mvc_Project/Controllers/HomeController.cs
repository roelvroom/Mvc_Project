using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Data;
using Mvc_Project.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Mvc_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Mvc_ProjectContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, Mvc_ProjectContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var aankopen = await _context.Aankopen
                .Include(a => a.NaamAanvrager)  
                .ToListAsync();

            var gebruikerNaam = _httpContextAccessor.HttpContext.Session.GetString("GebuikersNaam");
            ViewBag.GebruikerNaam = gebruikerNaam;

            return View(aankopen);
        }

        [HttpPost]
        public async Task<IActionResult> Goedkeuren(int id)
        {
            var aankoop = await _context.Aankopen.FindAsync(id);
            if (aankoop == null)
            {
                return NotFound();
            }

            aankoop.GoedGekeurd = true;
            _context.Update(aankoop);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Afwijzen(int id)
        {
            var aankoop = await _context.Aankopen.FindAsync(id);
            if (aankoop == null)
            {
                return NotFound();
            }

            _context.Aankopen.Remove(aankoop);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
