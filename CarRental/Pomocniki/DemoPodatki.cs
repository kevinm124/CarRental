using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using CarRental.Baza;

namespace CarRental.Pomocniki
{
    // Napolni bazo z realističnimi demo podatki (za predstavitev in posnetke zaslona):
    // avtomobili, stranke, rezervacije zadnjih 12 mesecev, plačila, servisi, poškodbe in agent.
    // Obstoječi podatki ostanejo - avtomobili/stranke z enako registrsko/emailom se preskočijo.
    public static class DemoPodatki
    {
        public const string AgentEmail = "agent@izposoja.si";
        public const string AgentGeslo = "agent123";

        private class Interval
        {
            public int AvtoId;
            public decimal Cena;
            public DateTime Od;
            public DateTime Do;
            public string Status;
        }

        public static bool SoZeDodani => Nastavitve.PridobiBool("demo_podatki", false);

        public static string Dodaj()
        {
            var nakljucno = new Random(2026);
            DateTime danes = DateTime.Today;
            int stRezervacij = 0;

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var transakcija = povezava.BeginTransaction())
                {
                    int Izvedi(string sql, params (string, object)[] parametri)
                    {
                        using (var ukaz = new SQLiteCommand(sql, povezava, transakcija))
                        {
                            foreach (var (ime, vrednost) in parametri)
                                ukaz.Parameters.AddWithValue(ime, vrednost ?? DBNull.Value);
                            return ukaz.ExecuteNonQuery();
                        }
                    }

                    object Skalar(string sql, params (string, object)[] parametri)
                    {
                        using (var ukaz = new SQLiteCommand(sql, povezava, transakcija))
                        {
                            foreach (var (ime, vrednost) in parametri)
                                ukaz.Parameters.AddWithValue(ime, vrednost ?? DBNull.Value);
                            return ukaz.ExecuteScalar();
                        }
                    }

                    int Kategorija(string naziv)
                    {
                        var id = Skalar("SELECT id FROM kategorije WHERE naziv = @n", ("@n", naziv))
                                 ?? Skalar("SELECT MIN(id) FROM kategorije");
                        return Convert.ToInt32(id);
                    }

                    // ===== Avtomobili =====
                    var avtomobili = new (string znamka, string model, int letnik, string reg, string barva, int sedezi, decimal cena, string kat, int km)[]
                    {
                        ("Volkswagen", "Polo", 2022, "LJ-PO-221", "Bela", 5, 32, "Ekonomski", 28000),
                        ("Renault", "Clio", 2021, "KR-CL-104", "Rdeča", 5, 29, "Ekonomski", 41000),
                        ("Škoda", "Fabia", 2023, "MB-FA-330", "Modra", 5, 31, "Ekonomski", 15000),
                        ("Toyota", "Yaris Hybrid", 2023, "CE-YH-512", "Siva", 5, 36, "Ekonomski", 19000),
                        ("Škoda", "Kodiaq", 2022, "LJ-KO-777", "Črna", 7, 72, "SUV", 52000),
                        ("Volvo", "XC60", 2023, "NM-XC-060", "Siva", 5, 89, "SUV", 23000),
                        ("Hyundai", "Tucson", 2021, "KP-TU-418", "Bela", 5, 65, "SUV", 61000),
                        ("BMW", "Serija 5", 2024, "LJ-BM-505", "Črna", 5, 135, "Luksuzni", 9000),
                        ("Mercedes-Benz", "E-razred", 2023, "MB-ME-220", "Srebrna", 5, 145, "Luksuzni", 17000),
                        ("Audi", "Q7", 2023, "LJ-AQ-007", "Temno modra", 7, 165, "Luksuzni", 21000),
                    };

                    var idAvtov = new List<(int id, decimal cena)>();
                    foreach (var a in avtomobili)
                    {
                        Izvedi(@"INSERT OR IGNORE INTO avtomobili
                                 (znamka, model, letnik, registrska_st, barva, st_sedezev, cena_na_dan, status, kategorija_id, prevozeni_km)
                                 VALUES (@z, @m, @l, @r, @b, @s, @c, 'prosto', @k, @km)",
                            ("@z", a.znamka), ("@m", a.model), ("@l", a.letnik), ("@r", a.reg), ("@b", a.barva),
                            ("@s", a.sedezi), ("@c", a.cena), ("@k", Kategorija(a.kat)), ("@km", a.km));
                        int id = Convert.ToInt32(Skalar("SELECT id FROM avtomobili WHERE registrska_st = @r", ("@r", a.reg)));
                        decimal cena = Convert.ToDecimal(Skalar("SELECT cena_na_dan FROM avtomobili WHERE id = @id", ("@id", id)));
                        idAvtov.Add((id, cena));
                    }

                    // ===== Stranke =====
                    var stranke = new (string ime, string priimek, string email, string tel, string naslov, string vozniska)[]
                    {
                        ("Luka", "Horvat", "luka.horvat@gmail.com", "041 123 456", "Trubarjeva 12, Ljubljana", "LJ123456"),
                        ("Nina", "Kovačič", "nina.kovacic@gmail.com", "031 234 567", "Partizanska 5, Maribor", "MB234567"),
                        ("Matej", "Krajnc", "matej.krajnc@siol.net", "040 345 678", "Prešernova 8, Celje", "CE345678"),
                        ("Sara", "Zupančič", "sara.zupancic@gmail.com", "051 456 789", "Glavni trg 3, Kranj", "KR456789"),
                        ("Jure", "Potočnik", "jure.potocnik@outlook.com", "041 567 890", "Bevkova 14, Koper", "KP567890"),
                        ("Eva", "Mlakar", "eva.mlakar@gmail.com", "031 678 901", "Ulica heroja Staneta 2, Novo mesto", "NM678901"),
                        ("Rok", "Vidmar", "rok.vidmar@gmail.com", "040 789 012", "Cankarjeva 21, Nova Gorica", "GO789012"),
                        ("Tjaša", "Golob", "tjasa.golob@gmail.com", "051 890 123", "Slovenska 40, Murska Sobota", "MS890123"),
                        ("Anže", "Kos", "anze.kos@siol.net", "041 901 234", "Ljubljanska 9, Domžale", "LJ901234"),
                        ("Maja", "Turk", "maja.turk@gmail.com", "031 012 345", "Kidričeva 17, Velenje", "SG012345"),
                        ("David", "Božič", "david.bozic@gmail.com", "040 135 790", "Mestni trg 6, Ptuj", "PT135790"),
                        ("Ana", "Rozman", "ana.rozman@outlook.com", "051 246 801", "Tržaška 30, Ljubljana", "LJ246801"),
                    };

                    var idStrank = new List<int>();
                    foreach (var s in stranke)
                    {
                        Izvedi(@"INSERT OR IGNORE INTO stranke (ime, priimek, email, telefon, naslov, st_vozniske)
                                 VALUES (@i, @p, @e, @t, @n, @v)",
                            ("@i", s.ime), ("@p", s.priimek), ("@e", s.email), ("@t", s.tel), ("@n", s.naslov), ("@v", s.vozniska));
                        idStrank.Add(Convert.ToInt32(Skalar("SELECT id FROM stranke WHERE email = @e", ("@e", s.email))));
                    }

                    // ===== Agent =====
                    Izvedi(@"INSERT OR IGNORE INTO zaposleni (ime, priimek, email, geslo_hash, vloga, aktiven)
                             VALUES ('Miha', 'Novak', @e, @g, 'agent', 1)",
                        ("@e", AgentEmail), ("@g", BCrypt.Net.BCrypt.HashPassword(AgentGeslo)));
                    int agentId = Convert.ToInt32(Skalar("SELECT id FROM zaposleni WHERE email = @e", ("@e", AgentEmail)));
                    int adminId = Convert.ToInt32(Skalar("SELECT MIN(id) FROM zaposleni WHERE vloga = 'admin'") ?? 1);

                    // ===== Rezervacije (zadnjih 12 mesecev, brez prekrivanja na istem avtu) =====
                    var intervali = new List<Interval>();
                    var aktivniAvti = new HashSet<int> { idAvtov[0].id, idAvtov[4].id, idAvtov[7].id };
                    foreach (var (id, cena) in idAvtov)
                    {
                        // dražji avti se izposojajo redkeje
                        int najvecPremora = cena > 100 ? 60 : cena > 60 ? 45 : 35;
                        DateTime dan = danes.AddDays(-365 + nakljucno.Next(0, 10));
                        DateTime meja = aktivniAvti.Contains(id) ? danes.AddDays(-6) : danes.AddDays(-2);
                        while (true)
                        {
                            int dni = nakljucno.Next(1, 8);
                            if (dan.AddDays(dni) > meja)
                                break;
                            intervali.Add(new Interval
                            {
                                AvtoId = id,
                                Cena = cena,
                                Od = dan,
                                Do = dan.AddDays(dni - 1),
                                Status = nakljucno.Next(100) < 9 ? "preklicana" : "zakljucena"
                            });
                            dan = dan.AddDays(dni + nakljucno.Next(5, najvecPremora));
                        }

                        if (aktivniAvti.Contains(id))
                        {
                            intervali.Add(new Interval
                            {
                                AvtoId = id,
                                Cena = cena,
                                Od = danes.AddDays(-nakljucno.Next(0, 3)),
                                Do = danes.AddDays(nakljucno.Next(2, 7)),
                                Status = "aktivna"
                            });
                        }
                    }

                    // stalne stranke dobijo več rezervacij (prikaz stopenj zvestobe)
                    var utezi = new[] { 14, 9, 6, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
                    var zakljucenih = idStrank.ToDictionary(s => s, s => Convert.ToInt32(Skalar(
                        "SELECT COUNT(*) FROM rezervacije WHERE stranka_id = @s AND status = 'zakljucena'", ("@s", s))));
                    string[] nacini = { "kartica", "kartica", "gotovina", "nakazilo" };
                    var zakljuceneRez = new List<(long id, int avto)>();

                    foreach (var iv in intervali.OrderBy(i => i.Od))
                    {
                        int stranka = IzberiUtezeno(idStrank, utezi, nakljucno);
                        var stopnja = Zvestoba.StopnjaZaStevilo(zakljucenih[stranka]);
                        decimal popust = stopnja?.Popust ?? 0;
                        int dni = (iv.Do - iv.Od).Days + 1;
                        decimal cena = Zvestoba.IzracunajCeno(dni, iv.Cena, popust);

                        Izvedi(@"INSERT INTO rezervacije (stranka_id, avtomobil_id, zaposleni_id, datum_od, datum_do,
                                     skupna_cena, status, popust, datum_kreiranja)
                                 VALUES (@s, @a, @z, @od, @do, @c, @st, @p, @dk)",
                            ("@s", stranka), ("@a", iv.AvtoId), ("@z", nakljucno.Next(3) == 0 ? agentId : adminId),
                            ("@od", iv.Od.ToString("yyyy-MM-dd")), ("@do", iv.Do.ToString("yyyy-MM-dd")),
                            ("@c", cena), ("@st", iv.Status), ("@p", popust),
                            ("@dk", iv.Od.AddDays(-nakljucno.Next(1, 15)).ToString("yyyy-MM-dd HH:mm:ss")));
                        long rezId = povezava.LastInsertRowId;
                        stRezervacij++;

                        if (iv.Status == "zakljucena")
                        {
                            zakljucenih[stranka]++;
                            zakljuceneRez.Add((rezId, iv.AvtoId));
                            Izvedi(@"INSERT INTO placila (rezervacija_id, znesek, datum_placila, nacin_placila, status)
                                     VALUES (@r, @z, @d, @n, 'placano')",
                                ("@r", rezId), ("@z", cena),
                                ("@d", iv.Do.AddHours(9 + nakljucno.Next(0, 8)).ToString("yyyy-MM-dd HH:mm:ss")),
                                ("@n", nacini[nakljucno.Next(nacini.Length)]));
                        }
                        else if (iv.Status == "aktivna")
                        {
                            Izvedi("UPDATE avtomobili SET status = 'rezervirano' WHERE id = @id", ("@id", iv.AvtoId));
                        }
                    }

                    // ===== Servisi =====
                    string[] vrste = { "Redni servis", "Menjava olja", "Menjava pnevmatik", "Tehnični pregled", "Zavore" };
                    for (int i = 0; i < idAvtov.Count; i++)
                    {
                        int id = idAvtov[i].id;
                        int km = avtomobili[i].km;
                        DateTime prvi = danes.AddDays(-nakljucno.Next(200, 330));
                        Izvedi(@"INSERT INTO servisi (avtomobil_id, datum, vrsta, opis, km, cena, naslednji_servis)
                                 VALUES (@a, @d, @v, @o, @km, @c, @n)",
                            ("@a", id), ("@d", prvi.ToString("yyyy-MM-dd")), ("@v", vrste[nakljucno.Next(vrste.Length)]),
                            ("@o", "Pregled in menjava filtrov"), ("@km", km - 8000), ("@c", 60 + nakljucno.Next(0, 200)),
                            ("@n", prvi.AddYears(1).ToString("yyyy-MM-dd")));

                        // zadnji servis: en zamujen, en kmalu, ostali v redu
                        DateTime zadnji = danes.AddDays(-nakljucno.Next(20, 120));
                        DateTime naslednji = i == 1 ? danes.AddDays(-5) : i == 5 ? danes.AddDays(7) : zadnji.AddDays(nakljucno.Next(150, 300));
                        Izvedi(@"INSERT INTO servisi (avtomobil_id, datum, vrsta, opis, km, cena, naslednji_servis)
                                 VALUES (@a, @d, @v, @o, @km, @c, @n)",
                            ("@a", id), ("@d", zadnji.ToString("yyyy-MM-dd")), ("@v", vrste[nakljucno.Next(vrste.Length)]),
                            ("@o", "Redno vzdrževanje po navodilih proizvajalca"), ("@km", km), ("@c", 80 + nakljucno.Next(0, 300)),
                            ("@n", naslednji.ToString("yyyy-MM-dd")));
                    }

                    // en avto je trenutno na servisu
                    int naServisu = idAvtov[6].id;
                    Izvedi("UPDATE avtomobili SET status = 'servis' WHERE id = @id AND status = 'prosto'", ("@id", naServisu));
                    Izvedi(@"INSERT INTO servisi (avtomobil_id, datum, vrsta, opis, km, cena, naslednji_servis)
                             VALUES (@a, @d, 'Popravilo', 'Menjava sklopke', @km, 640, @n)",
                        ("@a", naServisu), ("@d", danes.ToString("yyyy-MM-dd")), ("@km", avtomobili[6].km + 500),
                        ("@n", danes.AddYears(1).ToString("yyyy-MM-dd")));

                    // ===== Poškodbe =====
                    var poskodbe = new (string opis, string resnost, decimal strosek, string status)[]
                    {
                        ("Praska na zadnjem odbijaču", "manjsa", 150, "odprta"),
                        ("Razpoka na vetrobranskem steklu", "srednja", 420, "popravljena"),
                        ("Udrtina na desnih vratih", "srednja", 380, "odprta"),
                    };
                    for (int i = 0; i < poskodbe.Length && i < zakljuceneRez.Count; i++)
                    {
                        var (rezId, avtoId) = zakljuceneRez[zakljuceneRez.Count - 1 - i * 4];
                        string datum = Skalar("SELECT datum_do FROM rezervacije WHERE id = @id", ("@id", rezId)).ToString();
                        Izvedi(@"INSERT INTO poskodbe (avtomobil_id, rezervacija_id, datum, opis, resnost, ocenjeni_strosek, status)
                                 VALUES (@a, @r, @d, @o, @re, @s, @st)",
                            ("@a", avtoId), ("@r", rezId), ("@d", datum), ("@o", poskodbe[i].opis),
                            ("@re", poskodbe[i].resnost), ("@s", poskodbe[i].strosek), ("@st", poskodbe[i].status));
                    }

                    transakcija.Commit();
                }
            }

            Nastavitve.ShraniBool("demo_podatki", true);
            return Jezik.T("Dodano: {0} avtomobilov, {1} strank, {2} rezervacij, servisi, poškodbe in uporabnik agent ({3} / {4}).",
                10, 12, stRezervacij, AgentEmail, AgentGeslo);
        }

        private static int IzberiUtezeno(List<int> elementi, int[] utezi, Random nakljucno)
        {
            int vsota = utezi.Take(elementi.Count).Sum();
            int r = nakljucno.Next(vsota);
            for (int i = 0; i < elementi.Count; i++)
            {
                r -= utezi[i];
                if (r < 0)
                    return elementi[i];
            }
            return elementi[elementi.Count - 1];
        }
    }
}
