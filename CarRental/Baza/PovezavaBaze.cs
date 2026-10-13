using System;
using System.Data.SQLite;
using System.IO;

namespace CarRental.Baza
{
    public static class PovezavaBaze
    {
        private static string potBaze = "izposoja.db";
        private static string povezavaString = $"Data Source={potBaze};Version=3;";

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
                ";

                using (var ukaz = new SQLiteCommand(ustvariTabele, povezava))
                {
                    ukaz.ExecuteNonQuery();
                }

                // Dodaj privzete kategorije in admin uporabnika
                DodajPrivzetePodatke(povezava);
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