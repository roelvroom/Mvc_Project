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

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var userRoles = _httpContextAccessor.HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            var isBookhouder = userRoles.Contains("Boekhouder");

            var gebruikerNaam = _httpContextAccessor.HttpContext.Session.GetString("GebuikersNaam");

            var aankopen = isBookhouder
                ? await _context.Aankopen.Where(a => a.GoedGekeurd).ToListAsync()
                : await _context.Aankopen.Where(a => !a.GoedGekeurd).ToListAsync();

            ViewBag.GebruikerNaam = gebruikerNaam;
            return View(aankopen);
        }

        // POST: Home/Goedkeuren/5
        [HttpPost]
        public async Task<IActionResult> Goedkeuren(int id)
        {
            var aankoop = await _context.Aankopen.FindAsync(id);
            if (aankoop == null)
            {
                return NotFound();
            }

            // Zet de goedkeuring status op true
            aankoop.GoedGekeurd = true;
            _context.Update(aankoop);
            await _context.SaveChangesAsync();

            // Redirect naar de Index pagina zonder de goedgekeurde aankoop meer te tonen
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
