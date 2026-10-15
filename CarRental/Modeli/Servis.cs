using System;

namespace CarRental.Modeli
{
    public class Servis
    {
        public int Id { get; set; }
        public int AvtomobilId { get; set; }
        public DateTime Datum { get; set; }
        public string Vrsta { get; set; }
        public string Opis { get; set; }
        public int? Km { get; set; }
        public decimal Cena { get; set; }
        public DateTime? NaslednjiServis { get; set; }
    }
}
