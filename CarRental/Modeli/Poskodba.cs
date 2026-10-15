using System;
using System.Collections.Generic;

namespace CarRental.Modeli
{
    public class Poskodba
    {
        public int Id { get; set; }
        public int AvtomobilId { get; set; }
        public int? RezervacijaId { get; set; }
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
        public string Resnost { get; set; }       // manjsa, srednja, huda
        public decimal OcenjeniStrosek { get; set; }
        public string Status { get; set; }        // odprta, popravljena
        public List<string> Slike { get; set; } = new List<string>();
    }
}
