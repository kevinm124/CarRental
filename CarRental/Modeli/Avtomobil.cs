namespace CarRental.Modeli
{
    public class Avtomobil
    {
        public int Id { get; set; }
        public string Znamka { get; set; }
        public string Model { get; set; }
        public int Letnik { get; set; }
        public string RegistrskaSt { get; set; }
        public string Barva { get; set; }
        public int StSedezev { get; set; }
        public decimal CenaNaDan { get; set; }
        public string Status { get; set; }
        public int KategorijaId { get; set; }
        public string PotSlike { get; set; }
        public int PrevozeniKm { get; set; }
    }
}