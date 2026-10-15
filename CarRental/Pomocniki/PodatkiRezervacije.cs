using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CarRental.Baza;
using CarRental.Modeli;

namespace CarRental.Pomocniki
{
    // Vsi podatki o eni rezervaciji (za račun, pogodbo in email)
    public class PodatkiRezervacije
    {
        public Rezervacija Rezervacija { get; set; }
        public Stranka Stranka { get; set; }
        public Avtomobil Avtomobil { get; set; }
        public string Kategorija { get; set; }
        public string Zaposleni { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public List<Placilo> Placila { get; } = new List<Placilo>();

        public int SteviloDni => Math.Max(1, (Rezervacija.DatumDo.Date - Rezervacija.DatumOd.Date).Days + 1);
        public decimal CenaBrezPopusta => SteviloDni * Avtomobil.CenaNaDan;
        public decimal ZnesekPopusta => Math.Max(0, CenaBrezPopusta - Rezervacija.SkupnaCena);

        public decimal Placano
        {
            get
            {
                decimal vsota = 0;
                foreach (var p in Placila)
                    if (p.Status == "placano")
                        vsota += p.Znesek;
                return vsota;
            }
        }

        public string ImeStranke => $"{Stranka.Ime} {Stranka.Priimek}";
        public string ImeAvtomobila => $"{Avtomobil.Znamka} {Avtomobil.Model}";

        public static PodatkiRezervacije Nalozi(int rezervacijaId)
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string sql = @"
                    SELECT r.*, s.ime, s.priimek, s.email, s.telefon, s.naslov, s.st_vozniske,
                           a.znamka, a.model, a.letnik, a.registrska_st, a.barva, a.st_sedezev,
                           a.cena_na_dan, a.prevozeni_km, a.kategorija_id, k.naziv AS kategorija,
                           (z.ime || ' ' || z.priimek) AS zaposleni
                    FROM rezervacije r
                    LEFT JOIN stranke s ON r.stranka_id = s.id
                    LEFT JOIN avtomobili a ON r.avtomobil_id = a.id
                    LEFT JOIN kategorije k ON a.kategorija_id = k.id
                    LEFT JOIN zaposleni z ON r.zaposleni_id = z.id
                    WHERE r.id = @id";

                var podatki = new PodatkiRezervacije();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", rezervacijaId);
                    using (var b = ukaz.ExecuteReader())
                    {
                        if (!b.Read())
                            return null;

                        podatki.Rezervacija = new Rezervacija
                        {
                            Id = Convert.ToInt32(b["id"]),
                            StrankaId = Convert.ToInt32(b["stranka_id"]),
                            AvtomobilId = Convert.ToInt32(b["avtomobil_id"]),
                            DatumOd = DateTime.Parse(b["datum_od"].ToString()),
                            DatumDo = DateTime.Parse(b["datum_do"].ToString()),
                            SkupnaCena = b["skupna_cena"] == DBNull.Value ? 0 : Convert.ToDecimal(b["skupna_cena"]),
                            Popust = b["popust"] == DBNull.Value ? 0 : Convert.ToDecimal(b["popust"]),
                            Status = b["status"].ToString()
                        };
                        podatki.Stranka = new Stranka
                        {
                            Id = podatki.Rezervacija.StrankaId,
                            // stranka je lahko izbrisana (stari podatki brez tujih ključev)
                            Ime = b["ime"] == DBNull.Value ? "?" : b["ime"].ToString(),
                            Priimek = b["priimek"]?.ToString(),
                            Email = b["email"]?.ToString(),
                            Telefon = b["telefon"]?.ToString(),
                            Naslov = b["naslov"]?.ToString(),
                            StVozniske = b["st_vozniske"]?.ToString()
                        };
                        podatki.Avtomobil = new Avtomobil
                        {
                            Id = podatki.Rezervacija.AvtomobilId,
                            Znamka = b["znamka"] == DBNull.Value ? "?" : b["znamka"].ToString(),
                            Model = b["model"]?.ToString(),
                            Letnik = b["letnik"] == DBNull.Value ? 0 : Convert.ToInt32(b["letnik"]),
                            RegistrskaSt = b["registrska_st"]?.ToString(),
                            Barva = b["barva"]?.ToString(),
                            StSedezev = b["st_sedezev"] == DBNull.Value ? 0 : Convert.ToInt32(b["st_sedezev"]),
                            CenaNaDan = b["cena_na_dan"] == DBNull.Value ? 0 : Convert.ToDecimal(b["cena_na_dan"]),
                            PrevozeniKm = b["prevozeni_km"] == DBNull.Value ? 0 : Convert.ToInt32(b["prevozeni_km"])
                        };
                        podatki.Kategorija = b["kategorija"]?.ToString();
                        podatki.Zaposleni = b["zaposleni"]?.ToString();
                        podatki.DatumKreiranja = DateTime.TryParse(b["datum_kreiranja"]?.ToString(), out DateTime dk) ? dk : DateTime.Now;
                    }
                }

                using (var ukaz = new SQLiteCommand(
                    "SELECT * FROM placila WHERE rezervacija_id = @id ORDER BY datum_placila", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", rezervacijaId);
                    using (var b = ukaz.ExecuteReader())
                    {
                        while (b.Read())
                        {
                            podatki.Placila.Add(new Placilo
                            {
                                Id = Convert.ToInt32(b["id"]),
                                RezervacijaId = rezervacijaId,
                                Znesek = Convert.ToDecimal(b["znesek"]),
                                DatumPlacila = DateTime.TryParse(b["datum_placila"]?.ToString(), out DateTime dp) ? dp : DateTime.Now,
                                NacinPlacila = b["nacin_placila"]?.ToString(),
                                Status = b["status"]?.ToString()
                            });
                        }
                    }
                }

                return podatki;
            }
        }
    }
}
