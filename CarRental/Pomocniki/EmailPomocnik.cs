using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Pomocniki
{
    // Pošiljanje emailov strankam prek SMTP strežnika (nastavitve v Nastavitve > Email)
    public static class EmailPomocnik
    {
        public static bool JeNastavljen =>
            Nastavitve.EmailOmogocen &&
            !string.IsNullOrWhiteSpace(Nastavitve.SmtpStreznik) &&
            !string.IsNullOrWhiteSpace(Nastavitve.EmailPosiljatelj);

        public static void Poslji(string prejemnik, string zadeva, string htmlTelo, string priloga = null)
        {
            // .NET Framework privzeto morda ne uporablja TLS 1.2
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            using (var sporocilo = new MailMessage())
            using (var odjemalec = new SmtpClient(Nastavitve.SmtpStreznik, Nastavitve.SmtpVrata))
            {
                sporocilo.From = new MailAddress(Nastavitve.EmailPosiljatelj, Nastavitve.PodjetjeIme);
                sporocilo.To.Add(prejemnik);
                sporocilo.Subject = zadeva;
                sporocilo.Body = htmlTelo;
                sporocilo.IsBodyHtml = true;
                sporocilo.BodyEncoding = Encoding.UTF8;
                sporocilo.SubjectEncoding = Encoding.UTF8;

                if (!string.IsNullOrEmpty(priloga) && File.Exists(priloga))
                    sporocilo.Attachments.Add(new Attachment(priloga));

                odjemalec.EnableSsl = Nastavitve.SmtpSsl;
                odjemalec.DeliveryMethod = SmtpDeliveryMethod.Network;
                odjemalec.Timeout = 20000;
                if (!string.IsNullOrEmpty(Nastavitve.SmtpUporabnik))
                {
                    odjemalec.UseDefaultCredentials = false;
                    odjemalec.Credentials = new NetworkCredential(Nastavitve.SmtpUporabnik, Nastavitve.SmtpGeslo);
                }

                odjemalec.Send(sporocilo);
            }
        }

        // Potrditev rezervacije (po želji s pogodbo v PDF prilogi). Teče v ozadju, da UI ne zmrzne.
        public static Task PosljiPotrditevAsync(int rezervacijaId)
        {
            var podatki = PodatkiRezervacije.Nalozi(rezervacijaId);
            if (podatki == null)
                throw new InvalidOperationException(Jezik.T("Rezervacija ne obstaja."));
            if (string.IsNullOrWhiteSpace(podatki.Stranka.Email))
                throw new InvalidOperationException(Jezik.T("Stranka nima vpisanega email naslova."));

            string zadeva = Jezik.T("Potrditev rezervacije #{0} – {1}", podatki.Rezervacija.Id, Nastavitve.PodjetjeIme);
            string telo = TeloPotrditve(podatki);
            bool pripni = Nastavitve.EmailPripniPogodbo;

            return Task.Run(() =>
            {
                string priloga = null;
                try
                {
                    if (pripni)
                    {
                        priloga = Path.Combine(Path.GetTempPath(), $"{Jezik.T("Pogodba")}_{podatki.Rezervacija.Id}.pdf");
                        PdfDokumenti.UstvariPogodbo(podatki, priloga);
                    }
                    Poslji(podatki.Stranka.Email, zadeva, telo, priloga);
                }
                finally
                {
                    if (priloga != null)
                        try { File.Delete(priloga); } catch { }
                }
            });
        }

        private static string TeloPotrditve(PodatkiRezervacije p)
        {
            string Vrstica(string oznaka, string vrednost) =>
                $"<tr><td style='padding:6px 12px;color:#777'>{WebUtility.HtmlEncode(oznaka)}</td>" +
                $"<td style='padding:6px 12px'><b>{WebUtility.HtmlEncode(vrednost)}</b></td></tr>";

            var sb = new StringBuilder();
            sb.Append("<div style='font-family:Segoe UI,Arial,sans-serif;color:#212121;max-width:560px'>");
            sb.Append($"<div style='background:#2196F3;color:white;padding:18px 22px;font-size:20px'><b>{WebUtility.HtmlEncode(Nastavitve.PodjetjeIme)}</b></div>");
            sb.Append("<div style='padding:18px 22px'>");
            sb.Append($"<p>{WebUtility.HtmlEncode(Jezik.T("Pozdravljeni, {0}!", p.ImeStranke))}</p>");
            sb.Append($"<p>{WebUtility.HtmlEncode(Jezik.T("Vaša rezervacija je potrjena. Podrobnosti:"))}</p>");
            sb.Append("<table style='border-collapse:collapse;background:#f7f7f7;width:100%'>");
            sb.Append(Vrstica(Jezik.T("Rezervacija"), "#" + p.Rezervacija.Id));
            sb.Append(Vrstica(Jezik.T("Vozilo"), $"{p.ImeAvtomobila} ({p.Avtomobil.RegistrskaSt})"));
            sb.Append(Vrstica(Jezik.T("Prevzem"), p.Rezervacija.DatumOd.ToString("d.M.yyyy")));
            sb.Append(Vrstica(Jezik.T("Vrnitev"), p.Rezervacija.DatumDo.ToString("d.M.yyyy")));
            sb.Append(Vrstica(Jezik.T("Število dni"), p.SteviloDni.ToString()));
            if (p.Rezervacija.Popust > 0)
                sb.Append(Vrstica(Jezik.T("Popust zvestobe"), p.Rezervacija.Popust.ToString("0.#") + " %"));
            sb.Append(Vrstica(Jezik.T("Skupna cena"), p.Rezervacija.SkupnaCena.ToString("N2") + " €"));
            sb.Append("</table>");
            if (Nastavitve.EmailPripniPogodbo)
                sb.Append($"<p>{WebUtility.HtmlEncode(Jezik.T("V prilogi vam pošiljamo pogodbo o najemu."))}</p>");
            sb.Append($"<p>{WebUtility.HtmlEncode(Jezik.T("Lep pozdrav,"))}<br>{WebUtility.HtmlEncode(Nastavitve.PodjetjeIme)}<br>" +
                      $"<span style='color:#777'>{WebUtility.HtmlEncode(Nastavitve.PodjetjeTelefon)} · {WebUtility.HtmlEncode(Nastavitve.PodjetjeEmail)}</span></p>");
            sb.Append("</div></div>");
            return sb.ToString();
        }
    }
}
