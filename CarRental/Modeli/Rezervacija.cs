using System;

namespace CarRental.Modeli
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public int StrankaId { get; set; }
        public int AvtomobilId { get; set; }
        public int ZaposleniId { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public decimal SkupnaCena { get; set; }
        public string Status { get; set; }
        public decimal Popust { get; set; }
    }
}