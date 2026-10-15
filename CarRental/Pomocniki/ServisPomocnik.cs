using System;
using System.Data;
using System.Data.SQLite;
using CarRental.Baza;

namespace CarRental.Pomocniki
{
    public static class ServisPomocnik
    {
        public static readonly string[] VrsteServisov =
        {
            "Redni servis", "Menjava olja", "Menjava pnevmatik", "Zavore",
            "Tehnični pregled", "Klimatska naprava", "Popravilo", "Drugo"
        };

        // Avtomobili, pri katerih je naslednji servis (iz zadnjega zapisa) zamujen ali v naslednjih N dneh
        public static DataTable PrihajajociServisi(int dni)
        {
            string sql = @"
                SELECT a.id AS avtomobil_id, (a.znamka || ' ' || a.model) AS avto, a.registrska_st,
                       s.naslednji_servis, s.vrsta
                FROM avtomobili a
                JOIN servisi s ON s.id = (SELECT s2.id FROM servisi s2 WHERE s2.avtomobil_id = a.id
                                          ORDER BY s2.datum DESC, s2.id DESC LIMIT 1)
                WHERE s.naslednji_servis IS NOT NULL AND s.naslednji_servis <> ''
                  AND s.naslednji_servis <= @meja
                ORDER BY s.naslednji_servis";

            var tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@meja", DateTime.Today.AddDays(dni).ToString("yyyy-MM-dd"));
                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }
            return tabela;
        }

        public static string StatusDatuma(string naslednjiServis)
        {
            if (!DateTime.TryParse(naslednjiServis, out DateTime datum))
                return "";
            if (datum.Date < DateTime.Today)
                return "Zamujen";
            if (datum.Date <= DateTime.Today.AddDays(Nastavitve.ServisOpozoriloDni))
                return "Kmalu";
            return "V redu";
        }
    }
}
