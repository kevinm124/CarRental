using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CarRental.Pomocniki
{
    public static class SlikePomocnik
    {
        public static string PolnaPot(string relativnaPot)
        {
            return Path.Combine(Application.StartupPath, relativnaPot);
        }

        // Image.FromFile datoteko zaklene dokler slika obstaja - zato sliko preberemo v pomnilnik.
        // Tako lahko datoteko kasneje izbrišemo ali prepišemo (galerija, obnova kopije).
        public static Image Nalozi(string pot)
        {
            if (string.IsNullOrEmpty(pot))
                return null;

            string polnaPot = Path.IsPathRooted(pot) ? pot : PolnaPot(pot);
            if (!File.Exists(polnaPot))
                return null;

            try
            {
                using (var tok = new FileStream(polnaPot, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var slika = Image.FromStream(tok))
                {
                    return new Bitmap(slika);
                }
            }
            catch
            {
                return null;
            }
        }

        public static Image Slicica(string pot, int sirina, int visina)
        {
            using (var slika = Nalozi(pot))
            {
                if (slika == null)
                    return null;

                float razmerje = Math.Min((float)sirina / slika.Width, (float)visina / slika.Height);
                int s = Math.Max(1, (int)(slika.Width * razmerje));
                int v = Math.Max(1, (int)(slika.Height * razmerje));

                var rezultat = new Bitmap(sirina, visina);
                using (var g = Graphics.FromImage(rezultat))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(slika, (sirina - s) / 2, (visina - v) / 2, s, v);
                }
                return rezultat;
            }
        }

        // Kopira izbrano datoteko v Slike\<podmapa>\ z unikatnim imenom in vrne relativno pot
        public static string KopirajVMapo(string izvornaPot, string podmapa)
        {
            string mapa = Path.Combine(Application.StartupPath, "Slike", podmapa);
            if (!Directory.Exists(mapa))
                Directory.CreateDirectory(mapa);

            string razsiritev = Path.GetExtension(izvornaPot);
            string novoIme = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}{razsiritev}";
            File.Copy(izvornaPot, Path.Combine(mapa, novoIme), true);

            return Path.Combine("Slike", podmapa, novoIme);
        }

        public static void IzbrisiDatoteko(string relativnaPot)
        {
            if (string.IsNullOrEmpty(relativnaPot))
                return;

            try
            {
                string polnaPot = PolnaPot(relativnaPot);
                if (File.Exists(polnaPot))
                    File.Delete(polnaPot);
            }
            catch
            {
                // Če je datoteka še v uporabi, jo pustimo - zapis v bazi je že izbrisan
            }
        }

        public const string FilterSlik = "Slike|*.jpg;*.jpeg;*.png;*.bmp";
    }
}
