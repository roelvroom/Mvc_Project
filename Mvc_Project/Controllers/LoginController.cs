using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Voor toegang tot de sessie
using Mvc_Project.Data;
using Mvc_Project.Models; // Zorg ervoor dat je model is geïmporteerd

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
            // Toon de loginpagina
            return View();
        }

        [HttpPost]
        public IActionResult Index(string gebruikersNaam, string wachtWoord)
        {
            var gebruiker = _context.Gebruikers
                .FirstOrDefault(u => u.GebruikersNaam == gebruikersNaam && u.Wachtwoord == wachtWoord);

            if (gebruiker != null)
            {
                // Sla gebruikers-ID op in de sessie
                HttpContext.Session.SetString("GebruikerId", gebruiker.GebruikerId.ToString());
                HttpContext.Session.SetString("GebruikersNaam", gebruiker.GebruikersNaam);
                HttpContext.Session.SetString("Rol", gebruiker.Rol);

                // Redirect naar de homepagina of een andere beveiligde pagina
                return RedirectToAction("Index", "Home");
            }

            // Foutmelding als inloggen niet lukt
            ModelState.AddModelError("", "Ongeldige gebruikersnaam of wachtwoord.");
            return View();
        }

        public IActionResult LogOut()
        {
            // Verwijder alle sessiegegevens
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
