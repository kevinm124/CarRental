namespace CarRental.Modeli
{
    public class Zaposleni
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Email { get; set; }
        public string GesloHash { get; set; }
        public string Vloga { get; set; }
    }
}