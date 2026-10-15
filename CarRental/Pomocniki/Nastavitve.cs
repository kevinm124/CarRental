using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using CarRental.Baza;

namespace CarRental.Pomocniki
{
    // Nastavitve aplikacije so shranjene v tabeli "nastavitve" (ključ -> vrednost)
    public static class Nastavitve
    {
        private static Dictionary<string, string> predpomnilnik;

        private static void NaloziVse()
        {
            predpomnilnik = new Dictionary<string, string>();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("SELECT kljuc, vrednost FROM nastavitve", povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    while (bralec.Read())
                        predpomnilnik[bralec["kljuc"].ToString()] = bralec["vrednost"]?.ToString();
                }
            }
        }

        public static string Pridobi(string kljuc, string privzeto = "")
        {
            if (predpomnilnik == null)
                NaloziVse();

            return predpomnilnik.TryGetValue(kljuc, out string vrednost) && vrednost != null ? vrednost : privzeto;
        }

        public static void Shrani(string kljuc, string vrednost)
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string sql = "INSERT OR REPLACE INTO nastavitve (kljuc, vrednost) VALUES (@kljuc, @vrednost)";
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@kljuc", kljuc);
                    ukaz.Parameters.AddWithValue("@vrednost", (object)vrednost ?? DBNull.Value);
                    ukaz.ExecuteNonQuery();
                }
            }

            if (predpomnilnik == null)
                NaloziVse();
            predpomnilnik[kljuc] = vrednost;
        }

        // Po obnovi varnostne kopije moramo nastavitve prebrati znova
        public static void Ponastavi()
        {
            predpomnilnik = null;
        }

        public static int PridobiInt(string kljuc, int privzeto)
        {
            return int.TryParse(Pridobi(kljuc), out int v) ? v : privzeto;
        }

        public static decimal PridobiDecimal(string kljuc, decimal privzeto)
        {
            return decimal.TryParse(Pridobi(kljuc), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal v) ? v : privzeto;
        }

        public static bool PridobiBool(string kljuc, bool privzeto)
        {
            string v = Pridobi(kljuc);
            return string.IsNullOrEmpty(v) ? privzeto : v == "1";
        }

        public static void ShraniInt(string kljuc, int vrednost) => Shrani(kljuc, vrednost.ToString(CultureInfo.InvariantCulture));
        public static void ShraniDecimal(string kljuc, decimal vrednost) => Shrani(kljuc, vrednost.ToString(CultureInfo.InvariantCulture));
        public static void ShraniBool(string kljuc, bool vrednost) => Shrani(kljuc, vrednost ? "1" : "0");

        // Gesla (npr. SMTP) shranimo šifrirana z Windows DPAPI - dešifrira jih lahko samo isti Windows uporabnik
        public static string PridobiSkrito(string kljuc)
        {
            string sifrirano = Pridobi(kljuc);
            if (string.IsNullOrEmpty(sifrirano))
                return "";

            try
            {
                byte[] podatki = ProtectedData.Unprotect(Convert.FromBase64String(sifrirano), null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(podatki);
            }
            catch
            {
                return "";
            }
        }

        public static void ShraniSkrito(string kljuc, string vrednost)
        {
            if (string.IsNullOrEmpty(vrednost))
            {
                Shrani(kljuc, "");
                return;
            }

            byte[] sifrirano = ProtectedData.Protect(Encoding.UTF8.GetBytes(vrednost), null, DataProtectionScope.CurrentUser);
            Shrani(kljuc, Convert.ToBase64String(sifrirano));
        }

        // ===== Videz =====
        public static bool TemnaTema
        {
            get => PridobiBool("tema_temna", false);
            set => ShraniBool("tema_temna", value);
        }

        public static string JezikKoda
        {
            get => Pridobi("jezik", "sl");
            set => Shrani("jezik", value);
        }

        // ===== Podatki podjetja (za račune in pogodbe) =====
        public static string PodjetjeIme
        {
            get => Pridobi("podjetje_ime", "CarRental d.o.o.");
            set => Shrani("podjetje_ime", value);
        }

        public static string PodjetjeNaslov
        {
            get => Pridobi("podjetje_naslov", "Slovenska cesta 1, 1000 Ljubljana");
            set => Shrani("podjetje_naslov", value);
        }

        public static string PodjetjeKraj
        {
            get => Pridobi("podjetje_kraj", "Ljubljana");
            set => Shrani("podjetje_kraj", value);
        }

        public static string PodjetjeDavcna
        {
            get => Pridobi("podjetje_davcna", "SI12345678");
            set => Shrani("podjetje_davcna", value);
        }

        public static string PodjetjeTelefon
        {
            get => Pridobi("podjetje_telefon", "+386 1 234 56 78");
            set => Shrani("podjetje_telefon", value);
        }

        public static string PodjetjeEmail
        {
            get => Pridobi("podjetje_email", "info@izposoja.si");
            set => Shrani("podjetje_email", value);
        }

        public static string PodjetjeTrr
        {
            get => Pridobi("podjetje_trr", "SI56 0000 0000 0000 000");
            set => Shrani("podjetje_trr", value);
        }

        public static decimal StopnjaDdv
        {
            get => PridobiDecimal("ddv_stopnja", 22m);
            set => ShraniDecimal("ddv_stopnja", value);
        }

        // ===== Email (SMTP) =====
        public static bool EmailOmogocen
        {
            get => PridobiBool("email_omogocen", false);
            set => ShraniBool("email_omogocen", value);
        }

        public static string SmtpStreznik
        {
            get => Pridobi("smtp_streznik", "smtp.gmail.com");
            set => Shrani("smtp_streznik", value);
        }

        public static int SmtpVrata
        {
            get => PridobiInt("smtp_vrata", 587);
            set => ShraniInt("smtp_vrata", value);
        }

        public static bool SmtpSsl
        {
            get => PridobiBool("smtp_ssl", true);
            set => ShraniBool("smtp_ssl", value);
        }

        public static string SmtpUporabnik
        {
            get => Pridobi("smtp_uporabnik", "");
            set => Shrani("smtp_uporabnik", value);
        }

        public static string SmtpGeslo
        {
            get => PridobiSkrito("smtp_geslo");
            set => ShraniSkrito("smtp_geslo", value);
        }

        public static string EmailPosiljatelj
        {
            get => Pridobi("email_posiljatelj", "");
            set => Shrani("email_posiljatelj", value);
        }

        public static bool EmailPripniPogodbo
        {
            get => PridobiBool("email_pripni_pogodbo", true);
            set => ShraniBool("email_pripni_pogodbo", value);
        }

        // ===== Program zvestobe =====
        public static bool ZvestobaOmogocena
        {
            get => PridobiBool("zvestoba_omogocena", true);
            set => ShraniBool("zvestoba_omogocena", value);
        }

        // ===== Servisi =====
        public static int ServisOpozoriloDni
        {
            get => PridobiInt("servis_opozorilo_dni", 14);
            set => ShraniInt("servis_opozorilo_dni", value);
        }
    }
}
