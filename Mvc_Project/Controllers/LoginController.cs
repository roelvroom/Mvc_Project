using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using Mvc_Project.Data;
using Mvc_Project.Models; 

namespace Mvc_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly Mvc_ProjectContext _context;

        public LoginController(Mvc_ProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string gebruikersNaam, string wachtWoord)
        {
            var gebruiker = _context.Gebruikers
                .FirstOrDefault(u => u.GebruikersNaam == gebruikersNaam && u.Wachtwoord == wachtWoord);

            if (gebruiker != null)
            {
                HttpContext.Session.SetString("GebruikerId", gebruiker.GebruikerId.ToString());
                HttpContext.Session.SetString("GebruikersNaam", gebruiker.GebruikersNaam);
                HttpContext.Session.SetString("Rol", gebruiker.Rol);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Ongeldige gebruikersnaam of wachtwoord.");
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
