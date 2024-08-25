namespace Mvc_Project.Models
{
    public class Aankoop
    {
        public int AankoopId { get; set; }
        public int VakId { get; set; } = default!;
        public int ProductId { get; set; } = default!;
        public int NaamAanvragerId { get; set; } = default!;
        public DateTime Datum { get; set; } = DateTime.Now;
        public string Reden { get; set; } = default!;
        public bool GoedGekeurd { get; set; } = false;
        public bool Verwijderd { get; set; } = false;

        public Gebruiker NaamAanvrager { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public ICollection<Bijlagen> bijlagen { get; set; }
    }
}
