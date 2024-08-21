namespace Mvc_Project.Models
{
    public class Aankoop
    {
        public int AankoopId { get; set; }
        public int VakId { get; set; }
        public int NaamAanvragerId { get; set; }
        public DateTime Datum { get; set; }
        public string Reden { get; set; }
        public bool GoedGekeurd { get; set; }
        public bool Verwijderd { get; set; }
    }
}
