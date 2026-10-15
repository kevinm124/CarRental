using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Galerija slik enega avtomobila: več slik, izbira glavne slike
    public class GalerijaObrazec : DialogObrazec
    {
        private class SlikaVGaleriji
        {
            public int Id;
            public string Pot;
        }

        private readonly int avtomobilId;
        private string glavnaSlika;
        private readonly PictureBox picVelika;
        private readonly FlowLayoutPanel panelSlicice;
        private readonly Label lblInfo;
        private readonly Button btnGlavna, btnIzbrisi;
        private readonly List<SlikaVGaleriji> slike = new List<SlikaVGaleriji>();
        private SlikaVGaleriji izbrana;
        private static readonly Font pisavaZvezdice = new Font("Segoe UI", 14F, FontStyle.Bold);

        public bool Spremenjeno { get; private set; }

        public GalerijaObrazec(int avtomobilId, string nazivAvtomobila)
            : base("📷  " + Jezik.T("Galerija") + " – " + nazivAvtomobila, 900, 680)
        {
            this.avtomobilId = avtomobilId;
            panelGlavno.Padding = new Padding(20, 15, 20, 10);
            panelGlavno.AutoScroll = false;

            picVelika = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(240, 240, 240),
                Cursor = Cursors.Hand
            };
            picVelika.DoubleClick += (s, e) =>
            {
                if (izbrana != null)
                    using (var pregled = new PregledSlikeObrazec(izbrana.Pot, nazivAvtomobila))
                        pregled.ShowDialog(this);
            };

            lblInfo = new Label { Dock = DockStyle.Top, Height = 26, Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(120, 120, 120) };

            panelSlicice = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 130,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0),
                BackColor = Color.White
            };

            panelGlavno.Controls.Add(picVelika);
            panelGlavno.Controls.Add(lblInfo);
            panelGlavno.Controls.Add(panelSlicice);

            // Gumbi spodaj
            btnShrani.Text = "ZAPRI";
            btnPreklici.Visible = false;
            var btnDodaj = UiPomocnik.Gumb("➕ DODAJ SLIKE", Tema.Modra, 160, 40);
            btnGlavna = UiPomocnik.Gumb("★ GLAVNA SLIKA", Tema.Oranzna, 170, 40);
            btnIzbrisi = UiPomocnik.Gumb("IZBRIŠI", Tema.Rdeca, 120, 40);
            btnDodaj.Location = new Point(20, 15);
            btnGlavna.Location = new Point(190, 15);
            btnIzbrisi.Location = new Point(370, 15);
            panelGumbi.Controls.AddRange(new Control[] { btnDodaj, btnGlavna, btnIzbrisi });

            btnDodaj.Click += (s, e) => DodajSlike();
            btnGlavna.Click += (s, e) => NastaviGlavno();
            btnIzbrisi.Click += (s, e) => IzbrisiSliko();

            NaloziSlike();
        }

        private void NaloziSlike()
        {
            slike.Clear();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("SELECT pot_slike FROM avtomobili WHERE id = @id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", avtomobilId);
                    glavnaSlika = ukaz.ExecuteScalar() as string;
                }
                using (var ukaz = new SQLiteCommand(
                    "SELECT id, pot_slike FROM avtomobili_slike WHERE avtomobil_id = @id ORDER BY id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", avtomobilId);
                    using (var b = ukaz.ExecuteReader())
                        while (b.Read())
                            slike.Add(new SlikaVGaleriji { Id = Convert.ToInt32(b["id"]), Pot = b["pot_slike"].ToString() });
                }
            }

            foreach (Control c in panelSlicice.Controls)
                (c as PictureBox)?.Image?.Dispose();
            panelSlicice.Controls.Clear();

            foreach (var slika in slike)
            {
                var sl = slika;
                var slicica = new PictureBox
                {
                    Size = new Size(150, 105),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = SlikePomocnik.Slicica(sl.Pot, 150, 105),
                    Margin = new Padding(0, 0, 8, 0),
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(3),
                    Tag = sl
                };
                slicica.Click += (s, e) => Izberi(sl);
                slicica.Paint += (s, e) =>
                {
                    if (sl.Pot == glavnaSlika)
                        e.Graphics.DrawString("★", pisavaZvezdice, Brushes.Orange, 4, 2);
                    if (sl == izbrana)
                        using (var pero = new Pen(Tema.Modra, 4))
                            e.Graphics.DrawRectangle(pero, 2, 2, slicica.Width - 4, slicica.Height - 4);
                };
                panelSlicice.Controls.Add(slicica);
            }

            Izberi(slike.Find(s => s.Pot == glavnaSlika) ?? (slike.Count > 0 ? slike[0] : null));
        }

        private void Izberi(SlikaVGaleriji slika)
        {
            izbrana = slika;
            picVelika.Image?.Dispose();
            picVelika.Image = slika == null ? null : SlikePomocnik.Nalozi(slika.Pot);

            btnGlavna.Enabled = slika != null && slika.Pot != glavnaSlika;
            btnIzbrisi.Enabled = slika != null;
            lblInfo.Text = slike.Count == 0
                ? Jezik.T("Galerija je prazna. Dodajte slike z gumbom spodaj.")
                : Jezik.T("Slik: {0}  ·  ★ = glavna slika  ·  dvojni klik za povečavo", slike.Count);

            foreach (Control c in panelSlicice.Controls)
                c.Invalidate();
        }

        private void DodajSlike()
        {
            using (var dialog = new OpenFileDialog { Filter = SlikePomocnik.FilterSlik, Multiselect = true, Title = Jezik.T("Izberite slike") })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    using (var povezava = PovezavaBaze.PridobiPovezavo())
                    {
                        povezava.Open();
                        foreach (string datoteka in dialog.FileNames)
                        {
                            string pot = SlikePomocnik.KopirajVMapo(datoteka, "avtomobili");
                            using (var ukaz = new SQLiteCommand(
                                "INSERT INTO avtomobili_slike (avtomobil_id, pot_slike) VALUES (@id, @pot)", povezava))
                            {
                                ukaz.Parameters.AddWithValue("@id", avtomobilId);
                                ukaz.Parameters.AddWithValue("@pot", pot);
                                ukaz.ExecuteNonQuery();
                            }

                            // Prva slika postane glavna
                            if (string.IsNullOrEmpty(glavnaSlika))
                            {
                                PosodobiGlavno(povezava, pot);
                                glavnaSlika = pot;
                            }
                        }
                    }
                    Spremenjeno = true;
                    NaloziSlike();
                }
                catch (Exception ex)
                {
                    Sporocilo.Napaka("Napaka pri dodajanju slik: {0}", ex.Message);
                }
            }
        }

        private void PosodobiGlavno(SQLiteConnection povezava, string pot)
        {
            using (var ukaz = new SQLiteCommand("UPDATE avtomobili SET pot_slike = @pot WHERE id = @id", povezava))
            {
                ukaz.Parameters.AddWithValue("@pot", (object)pot ?? DBNull.Value);
                ukaz.Parameters.AddWithValue("@id", avtomobilId);
                ukaz.ExecuteNonQuery();
            }
        }

        private void NastaviGlavno()
        {
            if (izbrana == null)
                return;

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                PosodobiGlavno(povezava, izbrana.Pot);
            }
            glavnaSlika = izbrana.Pot;
            Spremenjeno = true;
            Izberi(izbrana);
        }

        private void IzbrisiSliko()
        {
            if (izbrana == null || !Sporocilo.Vprasaj("Ali res želite izbrisati izbrano sliko?"))
                return;

            var brisana = izbrana;
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("DELETE FROM avtomobili_slike WHERE id = @id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", brisana.Id);
                    ukaz.ExecuteNonQuery();
                }

                // Če je bila glavna, postane glavna naslednja slika (ali nobena)
                if (brisana.Pot == glavnaSlika)
                {
                    var naslednja = slike.Find(s => s.Id != brisana.Id);
                    PosodobiGlavno(povezava, naslednja?.Pot);
                }
            }

            picVelika.Image?.Dispose();
            picVelika.Image = null;
            SlikePomocnik.IzbrisiDatoteko(brisana.Pot);
            Spremenjeno = true;
            NaloziSlike();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            picVelika.Image?.Dispose();
            foreach (Control c in panelSlicice.Controls)
                (c as PictureBox)?.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
