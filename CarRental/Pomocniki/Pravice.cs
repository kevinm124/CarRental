using CarRental.Obrazci;

namespace CarRental.Pomocniki
{
    // Vloge uporabnikov:
    //  - admin: vse pravice (brisanje, uporabniki, nastavitve, varnostne kopije)
    //  - agent: vsakodnevno delo (dodajanje, urejanje, rezervacije, plačila), brez brisanja
    public static class Pravice
    {
        public const string VlogaAdmin = "admin";
        public const string VlogaAgent = "agent";

        public static bool JeAdmin =>
            PrijavaObrazec.TrenutniUporabnik != null &&
            PrijavaObrazec.TrenutniUporabnik.Vloga == VlogaAdmin;

        public static bool LahkoBrise => JeAdmin;
        public static bool LahkoUrejaUporabnike => JeAdmin;
        public static bool LahkoUrejaNastavitve => JeAdmin;
    }
}
