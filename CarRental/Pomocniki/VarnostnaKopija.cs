using System;
using System.Data.SQLite;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using CarRental.Baza;

namespace CarRental.Pomocniki
{
    // Varnostna kopija = ZIP datoteka z bazo (izposoja.db) in mapo Slike
    public static class VarnostnaKopija
    {
        private const string ImeBaze = "izposoja.db";
        private const string MapaSlik = "Slike";

        public static string PrivzetoIme => $"CarRental_kopija_{DateTime.Now:yyyy-MM-dd_HHmm}.zip";

        public static string MapaSamodejnihKopij => Path.Combine(Application.StartupPath, "VarnostneKopije");

        public static void Ustvari(string ciljnaPot)
        {
            string zacasnaBaza = Path.Combine(Path.GetTempPath(), $"izposoja_{Guid.NewGuid():N}.db");
            try
            {
                // SQLite "online backup" naredi dosledno kopijo tudi med delovanjem programa
                using (var vir = PovezavaBaze.PridobiPovezavo())
                using (var cilj = new SQLiteConnection($"Data Source={zacasnaBaza};Version=3;"))
                {
                    vir.Open();
                    cilj.Open();
                    vir.BackupDatabase(cilj, "main", "main", -1, null, 0);
                }
                SQLiteConnection.ClearAllPools();

                if (File.Exists(ciljnaPot))
                    File.Delete(ciljnaPot);

                using (var zip = ZipFile.Open(ciljnaPot, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(zacasnaBaza, ImeBaze);

                    string mapaSlik = Path.Combine(Application.StartupPath, MapaSlik);
                    if (Directory.Exists(mapaSlik))
                    {
                        foreach (string datoteka in Directory.GetFiles(mapaSlik, "*", SearchOption.AllDirectories))
                        {
                            string relativno = datoteka.Substring(Application.StartupPath.TrimEnd('\\').Length + 1).Replace('\\', '/');
                            zip.CreateEntryFromFile(datoteka, relativno);
                        }
                    }

                    var info = zip.CreateEntry("kopija.txt");
                    using (var pisalec = new StreamWriter(info.Open()))
                    {
                        pisalec.WriteLine("CarRental varnostna kopija");
                        pisalec.WriteLine("Ustvarjeno: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        pisalec.WriteLine("Uporabnik: " + CarRental.Obrazci.PrijavaObrazec.TrenutniUporabnik?.Email);
                    }
                }
            }
            finally
            {
                try { File.Delete(zacasnaBaza); } catch { }
            }
        }

        // Obnovi bazo in slike iz ZIP datoteke. Pred tem samodejno shrani kopijo trenutnega stanja.
        // Vrne pot do samodejne kopije.
        public static string Obnovi(string zipPot)
        {
            string zacasnaMapa = Path.Combine(Path.GetTempPath(), "CarRental_obnova_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(zacasnaMapa);

            try
            {
                using (var zip = ZipFile.OpenRead(zipPot))
                {
                    var vnosBaze = zip.GetEntry(ImeBaze);
                    if (vnosBaze == null)
                        throw new InvalidDataException(Jezik.T("Datoteka ni veljavna varnostna kopija programa CarRental."));

                    string novaBaza = Path.Combine(zacasnaMapa, ImeBaze);
                    vnosBaze.ExtractToFile(novaBaza);
                    PreveriBazo(novaBaza);

                    // 1) Samodejna kopija trenutnega stanja (za vsak slučaj)
                    Directory.CreateDirectory(MapaSamodejnihKopij);
                    string samodejna = Path.Combine(MapaSamodejnihKopij, $"pred_obnovo_{DateTime.Now:yyyy-MM-dd_HHmmss}.zip");
                    Ustvari(samodejna);

                    // 2) Zamenjaj bazo
                    SQLiteConnection.ClearAllPools();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.Copy(novaBaza, PovezavaBaze.PotBaze, true);

                    // 3) Slike (obstoječe datoteke z istim imenom so enake, zato jih preskočimo)
                    string koren = Path.GetFullPath(Application.StartupPath).TrimEnd('\\') + "\\";
                    foreach (var vnos in zip.Entries.Where(v => v.FullName.StartsWith(MapaSlik + "/") && v.Name.Length > 0))
                    {
                        string cilj = Path.GetFullPath(Path.Combine(koren, vnos.FullName.Replace('/', '\\')));
                        if (!cilj.StartsWith(koren, StringComparison.OrdinalIgnoreCase))
                            continue;   // zaščita pred "zip slip" (poti z ..\)

                        Directory.CreateDirectory(Path.GetDirectoryName(cilj));
                        if (!File.Exists(cilj))
                            vnos.ExtractToFile(cilj);
                    }

                    Nastavitve.Ponastavi();
                    return samodejna;
                }
            }
            finally
            {
                try { Directory.Delete(zacasnaMapa, true); } catch { }
            }
        }

        private static void PreveriBazo(string pot)
        {
            using (var povezava = new SQLiteConnection($"Data Source={pot};Version=3;Read Only=True;"))
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("PRAGMA integrity_check", povezava))
                {
                    if (ukaz.ExecuteScalar()?.ToString() != "ok")
                        throw new InvalidDataException(Jezik.T("Baza v varnostni kopiji je poškodovana."));
                }
                using (var ukaz = new SQLiteCommand(
                    "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name IN ('avtomobili','stranke','zaposleni','rezervacije')", povezava))
                {
                    if (Convert.ToInt32(ukaz.ExecuteScalar()) < 4)
                        throw new InvalidDataException(Jezik.T("Datoteka ni veljavna varnostna kopija programa CarRental."));
                }
            }
            SQLiteConnection.ClearAllPools();
        }
    }
}
