using System;

namespace CarRental.Modeli
{
    public class Placilo
    {
        public int Id { get; set; }
        public int RezervacijaId { get; set; }
        public decimal Znesek { get; set; }
        public DateTime DatumPlacila { get; set; }
        public string NacinPlacila { get; set; }
        public string Status { get; set; }
    }
}