using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CarRental.Baza;

namespace CarRental.Pomocniki
{
    // Program zvestobe: stranke z več zaključenimi najemi dobijo popust
    public class StopnjaZvestobe
    {
        public string Naziv { get; set; }
        public int Prag { get; set; }        // potrebno število zaključenih rezervacij
        public decimal Popust { get; set; }  // v odstotkih
    }

    public static class Zvestoba
    {
        public static readonly string[] Nazivi = { "Bronasta", "Srebrna", "Zlata" };
        private static readonly int[] privzetiPragovi = { 2, 5, 10 };
        private static readonly decimal[] privzetiPopusti = { 5m, 10m, 15m };

        public static List<StopnjaZvestobe> Stopnje()
        {
            var stopnje = new List<StopnjaZvestobe>();
            for (int i = 0; i < Nazivi.Length; i++)
            {
                stopnje.Add(new StopnjaZvestobe
                {
                    Naziv = Nazivi[i],
                    Prag = Nastavitve.PridobiInt("zvestoba_prag_" + i, privzetiPragovi[i]),
                    Popust = Nastavitve.PridobiDecimal("zvestoba_popust_" + i, privzetiPopusti[i])
                });
            }
            return stopnje;
        }

        public static void ShraniStopnjo(int indeks, int prag, decimal popust)
        {
            Nastavitve.ShraniInt("zvestoba_prag_" + indeks, prag);
            Nastavitve.ShraniDecimal("zvestoba_popust_" + indeks, popust);
        }

        public static int SteviloZakljucenih(int strankaId)
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(
                    "SELECT COUNT(*) FROM rezervacije WHERE stranka_id = @id AND status = 'zakljucena'", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", strankaId);
                    return Convert.ToInt32(ukaz.ExecuteScalar());
                }
            }
        }

        // Vrne najvišjo doseženo stopnjo ali null
        public static StopnjaZvestobe StopnjaZaStevilo(int zakljucenih)
        {
            if (!Nastavitve.ZvestobaOmogocena)
                return null;

            StopnjaZvestobe najboljsa = null;
            foreach (var s in Stopnje())
            {
                if (s.Prag > 0 && zakljucenih >= s.Prag && (najboljsa == null || s.Popust > najboljsa.Popust))
                    najboljsa = s;
            }
            return najboljsa;
        }

        public static StopnjaZvestobe StopnjaStranke(int strankaId)
        {
            return StopnjaZaStevilo(SteviloZakljucenih(strankaId));
        }

        // Koliko najemov manjka do naslednje stopnje (null, če je že najvišja)
        public static string OpisNapredka(int zakljucenih)
        {
            if (!Nastavitve.ZvestobaOmogocena)
                return "";

            foreach (var s in Stopnje())
            {
                if (s.Prag > zakljucenih)
                    return Jezik.T("Še {0} najem(ov) do stopnje {1}", s.Prag - zakljucenih, Jezik.T(s.Naziv));
            }
            return Jezik.T("Najvišja stopnja dosežena");
        }

        public static decimal IzracunajCeno(int dni, decimal cenaNaDan, decimal popust)
        {
            return Math.Round(dni * cenaNaDan * (1 - popust / 100m), 2);
        }
    }
}
