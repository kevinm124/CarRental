using System;
using System.Data.SQLite;
using System.IO;

namespace CarRental.Baza
{
    public static class PovezavaBaze
    {
        // Baza je vedno poleg .exe datoteke (ne glede na to, iz katere mape zaženemo program)
        private static readonly string potBaze = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "izposoja.db");

        private static readonly string povezavaString = new SQLiteConnectionStringBuilder
        {
            DataSource = potBaze,
            Version = 3,
            ForeignKeys = true
        }.ToString();

        public static string PotBaze => potBaze;

        public static SQLiteConnection PridobiPovezavo()
        {
            return new SQLiteConnection(povezavaString);
        }

        // To se kliče ob prvem zagonu aplikacije
        public static void InicializirajBazo()
        {
            if (!File.Exists(potBaze))
            {
                SQLiteConnection.CreateFile(potBaze);
            }

            using (var povezava = PridobiPovezavo())
            {
                povezava.Open();

                string ustvariTabele = @"
                    CREATE TABLE IF NOT EXISTS kategorije (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        naziv TEXT NOT NULL,
                        opis TEXT
                    );

                    CREATE TABLE IF NOT EXISTS avtomobili (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        znamka TEXT NOT NULL,
                        model TEXT NOT NULL,
                        letnik INTEGER NOT NULL,
                        registrska_st TEXT UNIQUE,
                        barva TEXT,
                        st_sedezev INTEGER,
                        cena_na_dan REAL NOT NULL,
                        status TEXT DEFAULT 'prosto',
                        kategorija_id INTEGER,
                        pot_slike TEXT,
                        FOREIGN KEY (kategorija_id) REFERENCES kategorije(id)
                    );

                    CREATE TABLE IF NOT EXISTS stranke (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ime TEXT NOT NULL,
                        priimek TEXT NOT NULL,
                        email TEXT UNIQUE,
                        telefon TEXT,
                        naslov TEXT,
                        st_vozniske TEXT
                    );

                    CREATE TABLE IF NOT EXISTS zaposleni (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ime TEXT NOT NULL,
                        priimek TEXT NOT NULL,
                        email TEXT UNIQUE NOT NULL,
                        geslo_hash TEXT NOT NULL,
                        vloga TEXT DEFAULT 'agent'
                    );

                    CREATE TABLE IF NOT EXISTS rezervacije (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        stranka_id INTEGER NOT NULL,
                        avtomobil_id INTEGER NOT NULL,
                        zaposleni_id INTEGER,
                        datum_od TEXT NOT NULL,
                        datum_do TEXT NOT NULL,
                        skupna_cena REAL,
                        status TEXT DEFAULT 'aktivna',
                        datum_kreiranja TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (stranka_id) REFERENCES stranke(id),
                        FOREIGN KEY (avtomobil_id) REFERENCES avtomobili(id),
                        FOREIGN KEY (zaposleni_id) REFERENCES zaposleni(id)
                    );

                    CREATE TABLE IF NOT EXISTS placila (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        rezervacija_id INTEGER NOT NULL,
                        znesek REAL NOT NULL,
                        datum_placila TEXT DEFAULT CURRENT_TIMESTAMP,
                        nacin_placila TEXT,
                        status TEXT DEFAULT 'placano',
                        FOREIGN KEY (rezervacija_id) REFERENCES rezervacije(id)
                    );

                    -- V3: nastavitve aplikacije (tema, jezik, podjetje, email ...)
                    CREATE TABLE IF NOT EXISTS nastavitve (
                        kljuc TEXT PRIMARY KEY,
                        vrednost TEXT
                    );

                    -- V3: galerija slik za vsak avtomobil
                    CREATE TABLE IF NOT EXISTS avtomobili_slike (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        avtomobil_id INTEGER NOT NULL,
                        pot_slike TEXT NOT NULL,
                        datum_dodajanja TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (avtomobil_id) REFERENCES avtomobili(id) ON DELETE CASCADE
                    );

                    -- V3: zgodovina servisov
                    CREATE TABLE IF NOT EXISTS servisi (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        avtomobil_id INTEGER NOT NULL,
                        datum TEXT NOT NULL,
                        vrsta TEXT NOT NULL,
                        opis TEXT,
                        km INTEGER,
                        cena REAL DEFAULT 0,
                        naslednji_servis TEXT,
                        FOREIGN KEY (avtomobil_id) REFERENCES avtomobili(id) ON DELETE CASCADE
                    );

                    -- V3: poškodbe vozil
                    CREATE TABLE IF NOT EXISTS poskodbe (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        avtomobil_id INTEGER NOT NULL,
                        rezervacija_id INTEGER,
                        datum TEXT NOT NULL,
                        opis TEXT NOT NULL,
                        resnost TEXT DEFAULT 'manjsa',
                        ocenjeni_strosek REAL DEFAULT 0,
                        status TEXT DEFAULT 'odprta',
                        FOREIGN KEY (avtomobil_id) REFERENCES avtomobili(id) ON DELETE CASCADE,
                        FOREIGN KEY (rezervacija_id) REFERENCES rezervacije(id) ON DELETE SET NULL
                    );

                    CREATE TABLE IF NOT EXISTS poskodbe_slike (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        poskodba_id INTEGER NOT NULL,
                        pot_slike TEXT NOT NULL,
                        FOREIGN KEY (poskodba_id) REFERENCES poskodbe(id) ON DELETE CASCADE
                    );
                ";

                using (var ukaz = new SQLiteCommand(ustvariTabele, povezava))
                {
                    ukaz.ExecuteNonQuery();
                }

                // V3: novi stolpci v obstoječih tabelah (stara baza ostane veljavna)
                DodajStolpecCeNeObstaja(povezava, "rezervacije", "popust", "REAL DEFAULT 0");
                DodajStolpecCeNeObstaja(povezava, "avtomobili", "prevozeni_km", "INTEGER DEFAULT 0");
                DodajStolpecCeNeObstaja(povezava, "zaposleni", "aktiven", "INTEGER DEFAULT 1");

                // Dodaj privzete kategorije in admin uporabnika
                DodajPrivzetePodatke(povezava);
            }

            SinhronizirajGlavneSlike();
        }

        private static void DodajStolpecCeNeObstaja(SQLiteConnection povezava, string tabela, string stolpec, string definicija)
        {
            using (var ukaz = new SQLiteCommand($"PRAGMA table_info({tabela})", povezava))
            using (var bralec = ukaz.ExecuteReader())
            {
                while (bralec.Read())
                {
                    if (string.Equals(bralec["name"].ToString(), stolpec, StringComparison.OrdinalIgnoreCase))
                        return;
                }
            }

            using (var ukaz = new SQLiteCommand($"ALTER TABLE {tabela} ADD COLUMN {stolpec} {definicija}", povezava))
            {
                ukaz.ExecuteNonQuery();
            }
        }

        // Glavna slika avtomobila je vedno tudi v galeriji
        public static void SinhronizirajGlavneSlike()
        {
            using (var povezava = PridobiPovezavo())
            {
                povezava.Open();
                string sql = @"
                    INSERT INTO avtomobili_slike (avtomobil_id, pot_slike)
                    SELECT a.id, a.pot_slike FROM avtomobili a
                    WHERE a.pot_slike IS NOT NULL AND a.pot_slike <> ''
                      AND NOT EXISTS (SELECT 1 FROM avtomobili_slike s
                                      WHERE s.avtomobil_id = a.id AND s.pot_slike = a.pot_slike)";
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.ExecuteNonQuery();
                }
            }
        }

        private static void DodajPrivzetePodatke(SQLiteConnection povezava)
        {
            // Preveri če že obstajajo kategorije
            string preveriQuery = "SELECT COUNT(*) FROM kategorije";
            using (var ukaz = new SQLiteCommand(preveriQuery, povezava))
            {
                long stevilo = (long)ukaz.ExecuteScalar();
                if (stevilo > 0) return;
            }

            string vstaviPodatke = @"
                INSERT INTO kategorije (naziv, opis) VALUES
                ('Ekonomski', 'Mali in varčni avtomobili'),
                ('SUV', 'Veliki družinski avtomobili'),
                ('Luksuzni', 'Luksuzni avtomobili');
            ";

            using (var ukaz = new SQLiteCommand(vstaviPodatke, povezava))
            {
                ukaz.ExecuteNonQuery();
            }

            // Dodaj admin uporabnika (geslo: admin123)
            string hashGesla = BCrypt.Net.BCrypt.HashPassword("admin123");
            string vstaviAdmina = @"
                INSERT INTO zaposleni (ime, priimek, email, geslo_hash, vloga)
                VALUES ('Admin', 'User', 'admin@izposoja.si', @geslo, 'admin');
            ";

            using (var ukaz = new SQLiteCommand(vstaviAdmina, povezava))
            {
                ukaz.Parameters.AddWithValue("@geslo", hashGesla);
                ukaz.ExecuteNonQuery();
            }
        }
    }
}
