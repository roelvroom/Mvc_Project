using System.Runtime.CompilerServices;

namespace Mvc_Project.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Initialen { get; set; }
        public string GebruikersNaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public bool Verwijder { get; set; }
    }
}
