using System;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Nastavitve aplikacije (samo administrator)
    public class NastavitveObrazec : OsnovniObrazec
    {
        // Podjetje
        private TextBox txtPodjetjeIme, txtPodjetjeNaslov, txtPodjetjeKraj, txtPodjetjeDavcna,
                        txtPodjetjeTelefon, txtPodjetjeEmail, txtPodjetjeTrr;
        private NumericUpDown numDdv;

        // Email
        private CheckBox chkEmailOmogocen, chkSsl, chkPripniPogodbo;
        private TextBox txtStreznik, txtUporabnik, txtGeslo, txtPosiljatelj, txtTestniNaslov;
        private NumericUpDown numVrata;

        // Zvestoba
        private CheckBox chkZvestoba;
        private readonly NumericUpDown[] numPragovi = new NumericUpDown[3];
        private readonly NumericUpDown[] numPopusti = new NumericUpDown[3];

        // Splošno
        private NumericUpDown numServisDni;

        public NastavitveObrazec()
        {
            BackColor = Color.WhiteSmoke;
            Text = "Nastavitve";

            var zavihki = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), Padding = new Point(16, 6) };
            zavihki.TabPages.Add(ZavihekPodjetje());
            zavihki.TabPages.Add(ZavihekEmail());
            zavihki.TabPages.Add(ZavihekZvestoba());
            zavihki.TabPages.Add(ZavihekKopija());
            zavihki.TabPages.Add(ZavihekSplosno());

            var panelGumbi = new Panel { Dock = DockStyle.Bottom, Height = 64, BackColor = Color.White };
            var btnShrani = UiPomocnik.Gumb("SHRANI NASTAVITVE", Tema.Modra, 220, 40);
            btnShrani.Location = new Point(15, 12);
            btnShrani.Click += (s, e) => Shrani();
            panelGumbi.Controls.Add(btnShrani);

            var panelGlavno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            panelGlavno.Controls.Add(zavihki);

            Controls.Add(panelGlavno);
            Controls.Add(panelGumbi);

            NaloziVrednosti();
        }

        private static TabPage NovZavihek(string naslov)
        {
            return new TabPage(naslov) { BackColor = Color.White, Padding = new Padding(25), AutoScroll = true };
        }

        private static T Polje<T>(TabPage zavihek, string naslov, T kontrola, int x, int y, int sirina = 300) where T : Control
        {
            var lbl = new Label
            {
                Text = naslov,
                Location = new Point(x, y),
                Size = new Size(sirina, 18),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(80, 80, 80)
            };
            kontrola.Location = new Point(x, y + 21);
            kontrola.Width = sirina;
            kontrola.Font = new Font("Segoe UI", 9.75F);
            if (kontrola is TextBox txt)
                txt.BorderStyle = BorderStyle.FixedSingle;
            zavihek.Controls.Add(lbl);
            zavihek.Controls.Add(kontrola);
            return kontrola;
        }

        private static Label Opis(TabPage zavihek, string besedilo, int x, int y, int sirina = 640)
        {
            var lbl = new Label
            {
                Text = besedilo,
                Location = new Point(x, y),
                Size = new Size(sirina, 40),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120)
            };
            zavihek.Controls.Add(lbl);
            return lbl;
        }

        private TabPage ZavihekPodjetje()
        {
            var z = NovZavihek("Podjetje");
            Opis(z, "Ti podatki se izpišejo na računih, pogodbah in v emailih strankam.", 25, 15);
            txtPodjetjeIme = Polje(z, "Naziv podjetja", new TextBox(), 25, 60);
            txtPodjetjeDavcna = Polje(z, "Davčna številka / ID za DDV", new TextBox(), 345, 60);
            txtPodjetjeNaslov = Polje(z, "Naslov", new TextBox(), 25, 120);
            txtPodjetjeKraj = Polje(z, "Kraj (za pogodbe)", new TextBox(), 345, 120);
            txtPodjetjeTelefon = Polje(z, "Telefon", new TextBox(), 25, 180);
            txtPodjetjeEmail = Polje(z, "Email", new TextBox(), 345, 180);
            txtPodjetjeTrr = Polje(z, "TRR (IBAN)", new TextBox(), 25, 240);
            numDdv = Polje(z, "Stopnja DDV (%)", new NumericUpDown { Minimum = 0, Maximum = 50, DecimalPlaces = 1 }, 345, 240, 120);
            return z;
        }

        private TabPage ZavihekEmail()
        {
            var z = NovZavihek("Email");
            chkEmailOmogocen = new CheckBox { Text = "Omogoči pošiljanje emailov strankam", AutoSize = true, Location = new Point(25, 18), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            z.Controls.Add(chkEmailOmogocen);
            Opis(z, "Za Gmail uporabite strežnik smtp.gmail.com, vrata 587, SSL in \"geslo za aplikacije\" (App Password), ne svojega običajnega gesla.", 25, 48);

            txtStreznik = Polje(z, "SMTP strežnik", new TextBox(), 25, 95);
            numVrata = Polje(z, "Vrata", new NumericUpDown { Minimum = 1, Maximum = 65535 }, 345, 95, 100);
            chkSsl = new CheckBox { Text = "SSL / TLS", AutoSize = true, Location = new Point(465, 118) };
            z.Controls.Add(chkSsl);
            txtUporabnik = Polje(z, "Uporabniško ime", new TextBox(), 25, 155);
            txtGeslo = Polje(z, "Geslo (shranjeno šifrirano)", new TextBox { UseSystemPasswordChar = true }, 345, 155);
            txtPosiljatelj = Polje(z, "Email pošiljatelja", new TextBox(), 25, 215);
            chkPripniPogodbo = new CheckBox { Text = "Potrditvi rezervacije pripni pogodbo (PDF)", AutoSize = true, Location = new Point(345, 238) };
            z.Controls.Add(chkPripniPogodbo);

            txtTestniNaslov = Polje(z, "Pošlji testni email na naslov", new TextBox(), 25, 290);
            var btnTest = UiPomocnik.Gumb("POŠLJI TEST", Tema.Zelena, 150, 30);
            btnTest.Location = new Point(345, 309);
            btnTest.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTestniNaslov.Text))
                {
                    Sporocilo.Opozorilo("Vnesite email naslov za test!");
                    return;
                }
                Shrani(false);
                btnTest.Enabled = false;
                Cursor = Cursors.WaitCursor;
                try
                {
                    string naslov = txtTestniNaslov.Text.Trim();
                    await System.Threading.Tasks.Task.Run(() => EmailPomocnik.Poslji(naslov,
                        Jezik.T("CarRental – testni email"),
                        "<p>" + Jezik.T("Nastavitve emaila delujejo pravilno.") + "</p>"));
                    Sporocilo.Info("Testni email je bil uspešno poslan.");
                }
                catch (Exception ex)
                {
                    Sporocilo.Napaka("Pošiljanje ni uspelo: {0}", ex.GetBaseException().Message);
                }
                finally
                {
                    btnTest.Enabled = true;
                    Cursor = Cursors.Default;
                }
            };
            z.Controls.Add(btnTest);
            return z;
        }

        private TabPage ZavihekZvestoba()
        {
            var z = NovZavihek("Zvestoba");
            chkZvestoba = new CheckBox { Text = "Omogoči program zvestobe", AutoSize = true, Location = new Point(25, 18), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            z.Controls.Add(chkZvestoba);
            Opis(z, "Stranka doseže stopnjo, ko ima dovolj zaključenih rezervacij. Popust se samodejno upošteva pri novi rezervaciji.", 25, 48);

            var barve = new[] { Color.FromArgb(205, 127, 50), Color.FromArgb(158, 158, 158), Color.FromArgb(255, 193, 7) };
            for (int i = 0; i < 3; i++)
            {
                int y = 100 + i * 65;
                var znacka = new Label
                {
                    Text = Zvestoba.Nazivi[i],
                    Location = new Point(25, y + 20),
                    Size = new Size(110, 28),
                    BackColor = barve[i],
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
                };
                z.Controls.Add(znacka);
                numPragovi[i] = Polje(z, "Zaključenih rezervacij", new NumericUpDown { Minimum = 0, Maximum = 1000 }, 160, y, 150);
                numPopusti[i] = Polje(z, "Popust (%)", new NumericUpDown { Minimum = 0, Maximum = 90, DecimalPlaces = 1 }, 340, y, 120);
            }
            return z;
        }

        private TabPage ZavihekKopija()
        {
            var z = NovZavihek("Varnostna kopija");
            Opis(z, "Varnostna kopija vsebuje celotno bazo in vse slike (ZIP datoteka). Pred obnovo se trenutno stanje samodejno shrani v mapo VarnostneKopije.", 25, 15, 680);

            var btnUstvari = UiPomocnik.Gumb("💾  USTVARI KOPIJO", Tema.Modra, 240, 44);
            btnUstvari.Location = new Point(25, 75);
            btnUstvari.Click += (s, e) => UstvariKopijo();
            z.Controls.Add(btnUstvari);

            var btnObnovi = UiPomocnik.Gumb("♻  OBNOVI IZ KOPIJE", Tema.Oranzna, 240, 44);
            btnObnovi.Location = new Point(285, 75);
            btnObnovi.Click += (s, e) => ObnoviKopijo();
            z.Controls.Add(btnObnovi);

            var btnMapa = UiPomocnik.SiviGumb("📂  ODPRI MAPO S KOPIJAMI", 240, 36);
            btnMapa.Location = new Point(25, 135);
            btnMapa.Click += (s, e) =>
            {
                System.IO.Directory.CreateDirectory(VarnostnaKopija.MapaSamodejnihKopij);
                System.Diagnostics.Process.Start(VarnostnaKopija.MapaSamodejnihKopij);
            };
            z.Controls.Add(btnMapa);
            return z;
        }

        private TabPage ZavihekSplosno()
        {
            var z = NovZavihek("Splošno");
            numServisDni = Polje(z, "Opozorilo za servis (dni vnaprej)", new NumericUpDown { Minimum = 1, Maximum = 365 }, 25, 20, 200);
            Opis(z, "Tema (svetla/temna) in jezik se preklapljata z gumboma zgoraj desno in veljata za vse uporabnike.", 25, 85);
            return z;
        }

        private void NaloziVrednosti()
        {
            txtPodjetjeIme.Text = Nastavitve.PodjetjeIme;
            txtPodjetjeNaslov.Text = Nastavitve.PodjetjeNaslov;
            txtPodjetjeKraj.Text = Nastavitve.PodjetjeKraj;
            txtPodjetjeDavcna.Text = Nastavitve.PodjetjeDavcna;
            txtPodjetjeTelefon.Text = Nastavitve.PodjetjeTelefon;
            txtPodjetjeEmail.Text = Nastavitve.PodjetjeEmail;
            txtPodjetjeTrr.Text = Nastavitve.PodjetjeTrr;
            numDdv.Value = Math.Min(numDdv.Maximum, Nastavitve.StopnjaDdv);

            chkEmailOmogocen.Checked = Nastavitve.EmailOmogocen;
            txtStreznik.Text = Nastavitve.SmtpStreznik;
            numVrata.Value = Nastavitve.SmtpVrata;
            chkSsl.Checked = Nastavitve.SmtpSsl;
            txtUporabnik.Text = Nastavitve.SmtpUporabnik;
            txtGeslo.Text = Nastavitve.SmtpGeslo;
            txtPosiljatelj.Text = Nastavitve.EmailPosiljatelj;
            chkPripniPogodbo.Checked = Nastavitve.EmailPripniPogodbo;
            txtTestniNaslov.Text = PrijavaObrazec.TrenutniUporabnik?.Email ?? "";

            chkZvestoba.Checked = Nastavitve.ZvestobaOmogocena;
            var stopnje = Zvestoba.Stopnje();
            for (int i = 0; i < 3; i++)
            {
                numPragovi[i].Value = Math.Min(numPragovi[i].Maximum, stopnje[i].Prag);
                numPopusti[i].Value = Math.Min(numPopusti[i].Maximum, stopnje[i].Popust);
            }

            numServisDni.Value = Math.Max(1, Math.Min(365, Nastavitve.ServisOpozoriloDni));
        }

        private void Shrani(bool sporocilo = true)
        {
            for (int i = 1; i < 3; i++)
            {
                if (numPragovi[i].Value > 0 && numPragovi[i].Value <= numPragovi[i - 1].Value)
                {
                    Sporocilo.Opozorilo("Pragovi zvestobe morajo naraščati (bronasta < srebrna < zlata)!");
                    return;
                }
            }

            try
            {
                Nastavitve.PodjetjeIme = txtPodjetjeIme.Text.Trim();
                Nastavitve.PodjetjeNaslov = txtPodjetjeNaslov.Text.Trim();
                Nastavitve.PodjetjeKraj = txtPodjetjeKraj.Text.Trim();
                Nastavitve.PodjetjeDavcna = txtPodjetjeDavcna.Text.Trim();
                Nastavitve.PodjetjeTelefon = txtPodjetjeTelefon.Text.Trim();
                Nastavitve.PodjetjeEmail = txtPodjetjeEmail.Text.Trim();
                Nastavitve.PodjetjeTrr = txtPodjetjeTrr.Text.Trim();
                Nastavitve.StopnjaDdv = numDdv.Value;

                Nastavitve.EmailOmogocen = chkEmailOmogocen.Checked;
                Nastavitve.SmtpStreznik = txtStreznik.Text.Trim();
                Nastavitve.SmtpVrata = (int)numVrata.Value;
                Nastavitve.SmtpSsl = chkSsl.Checked;
                Nastavitve.SmtpUporabnik = txtUporabnik.Text.Trim();
                Nastavitve.SmtpGeslo = txtGeslo.Text;
                Nastavitve.EmailPosiljatelj = txtPosiljatelj.Text.Trim();
                Nastavitve.EmailPripniPogodbo = chkPripniPogodbo.Checked;

                Nastavitve.ZvestobaOmogocena = chkZvestoba.Checked;
                for (int i = 0; i < 3; i++)
                    Zvestoba.ShraniStopnjo(i, (int)numPragovi[i].Value, numPopusti[i].Value);

                Nastavitve.ServisOpozoriloDni = (int)numServisDni.Value;

                if (sporocilo)
                    Sporocilo.Info("Nastavitve so bile shranjene.");
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri shranjevanju: {0}", ex.Message);
            }
        }

        private void UstvariKopijo()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "ZIP (*.zip)|*.zip";
                dialog.FileName = VarnostnaKopija.PrivzetoIme;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    VarnostnaKopija.Ustvari(dialog.FileName);
                    Cursor = Cursors.Default;
                    Sporocilo.Info("Varnostna kopija je bila ustvarjena:\n{0}", dialog.FileName);
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    Sporocilo.Napaka("Napaka pri ustvarjanju kopije: {0}", ex.Message);
                }
            }
        }

        private void ObnoviKopijo()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "ZIP (*.zip)|*.zip";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                if (!Sporocilo.Vprasaj("Vsi trenutni podatki bodo zamenjani s podatki iz kopije.\nTrenutno stanje se bo prej samodejno shranilo.\n\nAli želite nadaljevati?"))
                    return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    string samodejna = VarnostnaKopija.Obnovi(dialog.FileName);
                    Cursor = Cursors.Default;
                    Sporocilo.Info("Podatki so bili obnovljeni. Prejšnje stanje je shranjeno v:\n{0}\n\nProgram se bo zdaj ponovno zagnal.", samodejna);
                    Application.Restart();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    Sporocilo.Napaka("Obnova ni uspela: {0}", ex.Message);
                }
            }
        }
    }
}
