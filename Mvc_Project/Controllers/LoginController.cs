using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Data;

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
            var gebruiker = _context.Gebruiker.FirstOrDefault(u => u.GebruikersNaam == gebruikersNaam && u.Wachtwoord == wachtWoord);

            if(gebruiker != null)
            {
                HttpContext.Session.SetString("GebruikersNaam", gebruiker.GebruikersNaam);
                HttpContext.Session.SetString("Rol", gebruiker.Rol);
                return RedirectToAction("Index", "Home");
            }

            

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
