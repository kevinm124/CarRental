using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Koledar rezervacij: vrstice so avtomobili, stolpci dnevi v mesecu, rezervacije so barvni trakovi
    public class KoledarObrazec : OsnovniObrazec
    {
        private readonly CasovnicaKontrola casovnica;
        private readonly Label lblMesec;
        private readonly CheckBox chkPreklicane;
        private DateTime mesec;

        public KoledarObrazec()
        {
            BackColor = Color.WhiteSmoke;
            Text = "Koledar rezervacij";
            mesec = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            var panelOrodja = UiPomocnik.PanelOrodja();
            var btnNazaj = UiPomocnik.SiviGumb("◀", 44);
            var btnNaprej = UiPomocnik.SiviGumb("▶", 44);
            var btnDanes = UiPomocnik.Gumb("DANES", Tema.Modra, 100);
            lblMesec = UiPomocnik.Oznaka("", 14F, FontStyle.Bold);
            lblMesec.AutoSize = false;
            lblMesec.Size = new Size(220, 38);
            lblMesec.TextAlign = ContentAlignment.MiddleCenter;
            chkPreklicane = new CheckBox { Text = "Prikaži preklicane", AutoSize = true, Font = new Font("Segoe UI", 9.75F) };
            var btnNova = UiPomocnik.Gumb("NOVA REZERVACIJA", Tema.Zelena, 190);

            btnNazaj.Location = new Point(15, 17);
            lblMesec.Location = new Point(65, 17);
            btnNaprej.Location = new Point(290, 17);
            btnDanes.Location = new Point(345, 17);
            chkPreklicane.Location = new Point(465, 26);
            btnNova.Location = new Point(620, 17);
            panelOrodja.Controls.AddRange(new Control[] { btnNazaj, lblMesec, btnNaprej, btnDanes, chkPreklicane, btnNova });

            // Legenda
            var panelLegenda = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 36, Padding = new Padding(15, 8, 0, 0), BackColor = Color.White };
            foreach (var (besedilo, barva) in new[]
            {
                ("aktivna", Tema.Modra), ("zakljucena", Tema.Zelena), ("preklicana", Tema.Rdeca), ("servis", Tema.Oranzna)
            })
            {
                panelLegenda.Controls.Add(new Label { BackColor = barva, Size = new Size(14, 14), Margin = new Padding(0, 3, 6, 0) });
                panelLegenda.Controls.Add(new Label { Text = besedilo, AutoSize = true, Margin = new Padding(0, 1, 22, 0), Font = new Font("Segoe UI", 9F) });
            }

            casovnica = new CasovnicaKontrola { Dock = DockStyle.Fill };
            var panelGlavno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            panelGlavno.Controls.Add(casovnica);

            Controls.Add(panelGlavno);
            Controls.Add(panelLegenda);
            Controls.Add(panelOrodja);

            btnNazaj.Click += (s, e) => { mesec = mesec.AddMonths(-1); NaloziPodatke(); };
            btnNaprej.Click += (s, e) => { mesec = mesec.AddMonths(1); NaloziPodatke(); };
            btnDanes.Click += (s, e) => { mesec = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); NaloziPodatke(); };
            chkPreklicane.CheckedChanged += (s, e) => NaloziPodatke();
            btnNova.Click += (s, e) =>
            {
                using (var dialog = new NovaRezervacijaObrazec())
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        NaloziPodatke();
            };

            NaloziPodatke();
        }

        private void NaloziPodatke()
        {
            var kultura = Jezik.JeAnglescina ? CultureInfo.GetCultureInfo("en-GB") : CultureInfo.GetCultureInfo("sl-SI");
            string naziv = mesec.ToString("MMMM yyyy", kultura);
            lblMesec.Text = char.ToUpper(naziv[0]) + naziv.Substring(1);

            DateTime konec = mesec.AddMonths(1).AddDays(-1);
            var avtomobili = new List<CasovnicaKontrola.Vrsta>();
            var trakovi = new List<CasovnicaKontrola.Trak>();

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(
                    "SELECT id, znamka, model, registrska_st, status FROM avtomobili ORDER BY znamka, model", povezava))
                using (var b = ukaz.ExecuteReader())
                {
                    while (b.Read())
                    {
                        avtomobili.Add(new CasovnicaKontrola.Vrsta
                        {
                            Id = Convert.ToInt32(b["id"]),
                            Naziv = $"{b["znamka"]} {b["model"]}",
                            Podnaslov = $"{b["registrska_st"]} · {Jezik.T(b["status"].ToString())}"
                        });
                    }
                }

                string sql = @"SELECT r.id, r.avtomobil_id, r.datum_od, r.datum_do, r.status, r.skupna_cena,
                                      (s.ime || ' ' || s.priimek) AS stranka
                               FROM rezervacije r LEFT JOIN stranke s ON r.stranka_id = s.id
                               WHERE r.datum_od <= @konec AND r.datum_do >= @zacetek"
                             + (chkPreklicane.Checked ? "" : " AND r.status <> 'preklicana'");
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@zacetek", mesec.ToString("yyyy-MM-dd"));
                    ukaz.Parameters.AddWithValue("@konec", konec.ToString("yyyy-MM-dd"));
                    using (var b = ukaz.ExecuteReader())
                    {
                        while (b.Read())
                        {
                            string status = b["status"].ToString();
                            string stranka = b["stranka"] == DBNull.Value ? "?" : b["stranka"].ToString();
                            DateTime od = DateTime.Parse(b["datum_od"].ToString());
                            DateTime @do = DateTime.Parse(b["datum_do"].ToString());
                            trakovi.Add(new CasovnicaKontrola.Trak
                            {
                                VrstaId = Convert.ToInt32(b["avtomobil_id"]),
                                Od = od,
                                Do = @do,
                                Besedilo = stranka,
                                Barva = status == "zakljucena" ? Tema.Zelena : status == "preklicana" ? Tema.Rdeca : Tema.Modra,
                                Opis = Jezik.T("Rezervacija #{0}", b["id"]) + "\n" +
                                       stranka + "\n" +
                                       $"{od:d.M.yyyy} – {@do:d.M.yyyy}\n" +
                                       $"{Jezik.T("Status")}: {Jezik.T(status)}\n" +
                                       $"{Convert.ToDecimal(b["skupna_cena"]):N2} €"
                            });
                        }
                    }
                }

                // Servisi so prikazani kot oranžni enodnevni trakovi
                using (var ukaz = new SQLiteCommand(
                    "SELECT avtomobil_id, datum, vrsta, cena FROM servisi WHERE datum BETWEEN @zacetek AND @konec", povezava))
                {
                    ukaz.Parameters.AddWithValue("@zacetek", mesec.ToString("yyyy-MM-dd"));
                    ukaz.Parameters.AddWithValue("@konec", konec.ToString("yyyy-MM-dd"));
                    using (var b = ukaz.ExecuteReader())
                    {
                        while (b.Read())
                        {
                            DateTime datum = DateTime.Parse(b["datum"].ToString());
                            string vrsta = Jezik.T(b["vrsta"].ToString());
                            trakovi.Add(new CasovnicaKontrola.Trak
                            {
                                VrstaId = Convert.ToInt32(b["avtomobil_id"]),
                                Od = datum,
                                Do = datum,
                                Besedilo = "🔧",
                                Barva = Tema.Oranzna,
                                Opis = $"{Jezik.T("Servis")}: {vrsta}\n{datum:d.M.yyyy}\n{Convert.ToDecimal(b["cena"]):N2} €"
                            });
                        }
                    }
                }
            }

            casovnica.Nastavi(mesec, avtomobili, trakovi);
        }
    }

    // Kontrola, ki nariše časovnico (dvojni medpomnilnik za gladko risanje)
    public class CasovnicaKontrola : Control
    {
        public class Vrsta
        {
            public int Id;
            public string Naziv;
            public string Podnaslov;
        }

        public class Trak
        {
            public int VrstaId;
            public DateTime Od;
            public DateTime Do;
            public string Besedilo;
            public string Opis;
            public Color Barva;
            public Rectangle Obmocje;
        }

        private const int SirinaImen = 210;
        private const int VisinaGlave = 46;
        private const int VisinaVrstice = 40;

        private DateTime mesec;
        private List<Vrsta> vrste = new List<Vrsta>();
        private List<Trak> trakovi = new List<Trak>();
        private readonly ToolTip namig = new ToolTip { InitialDelay = 200, ReshowDelay = 100 };
        private readonly VScrollBar drsnik = new VScrollBar { Dock = DockStyle.Right };
        private Trak zadnjiNamig;

        public CasovnicaKontrola()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F);
            Controls.Add(drsnik);
            drsnik.ValueChanged += (s, e) => Invalidate();
        }

        public void Nastavi(DateTime mesec, List<Vrsta> vrste, List<Trak> trakovi)
        {
            this.mesec = mesec;
            this.vrste = vrste;
            this.trakovi = trakovi;
            drsnik.Value = 0;
            PosodobiDrsnik();
            Invalidate();
        }

        private void PosodobiDrsnik()
        {
            int vidno = Math.Max(1, Height - VisinaGlave);
            int vsebina = vrste.Count * VisinaVrstice;
            drsnik.Visible = vsebina > vidno;
            drsnik.Maximum = Math.Max(0, vsebina);
            drsnik.LargeChange = vidno;
            drsnik.SmallChange = VisinaVrstice;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PosodobiDrsnik();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (drsnik.Visible)
            {
                int nova = drsnik.Value - Math.Sign(e.Delta) * VisinaVrstice;
                drsnik.Value = Math.Max(0, Math.Min(drsnik.Maximum - drsnik.LargeChange + 1, nova));
            }
            base.OnMouseWheel(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            bool temna = Tema.JeTemna;
            Color ozadje = temna ? Color.FromArgb(45, 45, 48) : Color.White;
            Color vikend = temna ? Color.FromArgb(55, 55, 58) : Color.FromArgb(248, 248, 248);
            Color crta = temna ? Color.FromArgb(70, 70, 73) : Color.FromArgb(230, 230, 230);
            Color besedilo = temna ? Color.FromArgb(235, 235, 235) : Color.FromArgb(33, 33, 33);
            Color sivo = temna ? Color.FromArgb(165, 165, 165) : Color.FromArgb(120, 120, 120);
            g.Clear(ozadje);

            if (mesec == DateTime.MinValue)
                return;

            int steviloDni = DateTime.DaysInMonth(mesec.Year, mesec.Month);
            int sirinaVsebine = Width - SirinaImen - (drsnik.Visible ? drsnik.Width : 0);
            float sirinaDneva = Math.Max(18f, sirinaVsebine / (float)steviloDni);
            int zamik = drsnik.Visible ? drsnik.Value : 0;
            var kultura = Jezik.JeAnglescina ? CultureInfo.GetCultureInfo("en-GB") : CultureInfo.GetCultureInfo("sl-SI");

            using (var copicCrta = new Pen(crta))
            using (var copicBesedilo = new SolidBrush(besedilo))
            using (var copicSivo = new SolidBrush(sivo))
            using (var copicVikend = new SolidBrush(vikend))
            using (var krepko = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var majhno = new Font("Segoe UI", 7.5F))
            {
                // Ozadje dni (vikendi, danes)
                for (int d = 0; d < steviloDni; d++)
                {
                    DateTime dan = mesec.AddDays(d);
                    float x = SirinaImen + d * sirinaDneva;
                    if (dan.DayOfWeek == DayOfWeek.Saturday || dan.DayOfWeek == DayOfWeek.Sunday)
                        g.FillRectangle(copicVikend, x, 0, sirinaDneva, Height);
                    if (dan == DateTime.Today)
                        using (var danes = new SolidBrush(Color.FromArgb(40, 33, 150, 243)))
                            g.FillRectangle(danes, x, 0, sirinaDneva, Height);
                    g.DrawLine(copicCrta, x, 0, x, Height);
                }

                // Vrstice z avtomobili
                var obrezovanje = g.Clip;
                g.SetClip(new Rectangle(0, VisinaGlave, Width, Height - VisinaGlave));
                for (int i = 0; i < vrste.Count; i++)
                {
                    int y = VisinaGlave + i * VisinaVrstice - zamik;
                    g.DrawLine(copicCrta, 0, y + VisinaVrstice, Width, y + VisinaVrstice);
                    g.DrawString(vrste[i].Naziv, krepko, copicBesedilo, new RectangleF(12, y + 4, SirinaImen - 16, 18));
                    g.DrawString(vrste[i].Podnaslov, majhno, copicSivo, new RectangleF(12, y + 21, SirinaImen - 16, 16));
                }

                // Trakovi
                foreach (var trak in trakovi)
                    trak.Obmocje = Rectangle.Empty;

                using (var formatTraku = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
                {
                    foreach (var trak in trakovi)
                    {
                        int indeks = vrste.FindIndex(v => v.Id == trak.VrstaId);
                        if (indeks < 0)
                            continue;

                        DateTime od = trak.Od < mesec ? mesec : trak.Od.Date;
                        DateTime @do = trak.Do > mesec.AddDays(steviloDni - 1) ? mesec.AddDays(steviloDni - 1) : trak.Do.Date;
                        if (@do < od)
                            continue;

                        bool servis = trak.Barva == Tema.Oranzna;
                        float x = SirinaImen + (od - mesec).Days * sirinaDneva + 2;
                        float sirina = ((@do - od).Days + 1) * sirinaDneva - 4;
                        int y = VisinaGlave + indeks * VisinaVrstice - zamik + (servis ? 22 : 6);
                        var pravokotnik = new RectangleF(x, y, sirina, servis ? 14 : 22);

                        using (var pot = ZaobljenPravokotnik(pravokotnik, 5))
                        using (var polnilo = new SolidBrush(trak.Barva))
                            g.FillPath(polnilo, pot);

                        if (!servis)
                        {
                            var notranji = new RectangleF(pravokotnik.X + 5, pravokotnik.Y, pravokotnik.Width - 8, pravokotnik.Height);
                            g.DrawString(trak.Besedilo, Font, Brushes.White, notranji, formatTraku);
                        }
                        trak.Obmocje = Rectangle.Round(pravokotnik);
                    }
                }
                g.Clip = obrezovanje;

                // Glava z dnevi (narisana nazadnje, ostane na vrhu ob drsenju)
                using (var glava = new SolidBrush(Color.FromArgb(33, 33, 33)))
                    g.FillRectangle(glava, 0, 0, Width, VisinaGlave);
                g.DrawString(Jezik.T("Avtomobil"), krepko, Brushes.White, 12, 14);
                using (var format = new StringFormat { Alignment = StringAlignment.Center })
                {
                    for (int d = 0; d < steviloDni; d++)
                    {
                        DateTime dan = mesec.AddDays(d);
                        float x = SirinaImen + d * sirinaDneva;
                        var barva = dan == DateTime.Today ? new SolidBrush(Tema.Modra) : new SolidBrush(Color.White);
                        g.DrawString((d + 1).ToString(), krepko, barva, new RectangleF(x, 6, sirinaDneva, 18), format);
                        g.DrawString(dan.ToString("ddd", kultura).Substring(0, Math.Min(2, dan.ToString("ddd", kultura).Length)),
                            majhno, Brushes.Silver, new RectangleF(x, 25, sirinaDneva, 14), format);
                        barva.Dispose();
                    }
                }

                if (vrste.Count == 0)
                    g.DrawString(Jezik.T("Ni avtomobilov."), Font, copicSivo, 12, VisinaGlave + 12);
            }
        }

        private static GraphicsPath ZaobljenPravokotnik(RectangleF r, float polmer)
        {
            var pot = new GraphicsPath();
            float d = Math.Min(polmer * 2, Math.Min(r.Width, r.Height));
            if (d <= 0)
            {
                pot.AddRectangle(r);
                return pot;
            }
            pot.AddArc(r.X, r.Y, d, d, 180, 90);
            pot.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            pot.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            pot.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            pot.CloseFigure();
            return pot;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var trak = trakovi.LastOrDefault(t => t.Obmocje.Contains(e.Location));
            if (trak == zadnjiNamig)
                return;

            zadnjiNamig = trak;
            if (trak == null)
                namig.Hide(this);
            else
                namig.Show(trak.Opis, this, e.X + 14, e.Y + 14);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            zadnjiNamig = null;
            namig.Hide(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                namig.Dispose();
            base.Dispose(disposing);
        }
    }
}
