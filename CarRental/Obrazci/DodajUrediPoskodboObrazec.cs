using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public class DodajUrediPoskodboObrazec : DialogObrazec
    {
        private class RezervacijaItem
        {
            public int? Id;
            public string Naziv;
            public override string ToString() => Naziv;
        }

        private readonly Poskodba poskodbaZaUrejanje;
        private bool jeUrejanje => poskodbaZaUrejanje != null;

        private readonly ComboBox cmbAvto;
        private readonly ComboBox cmbRezervacija;
        private readonly DateTimePicker dtpDatum;
        private readonly ComboBox cmbResnost;
        private readonly NumericUpDown numStrosek;
        private readonly ComboBox cmbStatus;
        private readonly TextBox txtOpis;
        private readonly FlowLayoutPanel panelSlike;

        // Slike: obstoječe (relativne poti) in nove (polne poti, kopirajo se ob shranjevanju)
        private readonly List<string> slike = new List<string>();
        private readonly List<string> izbrisaneSlike = new List<string>();
        private readonly int? zacetnaRezervacija;

        public DodajUrediPoskodboObrazec(int? avtomobilId = null, int? rezervacijaId = null) : this(null, avtomobilId, rezervacijaId) { }

        public DodajUrediPoskodboObrazec(Poskodba poskodba, int? avtomobilId = null, int? rezervacijaId = null)
            : base(poskodba == null ? "⚠  Nova poškodba" : "✏️  Uredi poškodbo", 600, 680)
        {
            poskodbaZaUrejanje = poskodba;
            zacetnaRezervacija = poskodba?.RezervacijaId ?? rezervacijaId;

            cmbAvto = DodajPolje("Avtomobil *", new ComboBox(), 30, 15, 530);
            cmbRezervacija = DodajPolje("Rezervacija (med katero je nastala poškodba)", new ComboBox(), 30, 75, 530);
            dtpDatum = DodajPolje("Datum *", Datum(), 30, 135, 160);
            cmbResnost = DodajPolje("Resnost", new ComboBox(), 210, 135, 160);
            cmbResnost.Items.AddRange(new object[] { "manjsa", "srednja", "huda" });
            cmbResnost.SelectedIndex = 0;
            numStrosek = DodajPolje("Ocenjeni strošek (€)", Stevilo(0, 100000, 2, 10), 390, 135, 170);
            cmbStatus = DodajPolje("Status", new ComboBox(), 30, 195, 160);
            cmbStatus.Items.AddRange(new object[] { "odprta", "popravljena" });
            cmbStatus.SelectedIndex = 0;
            txtOpis = DodajPolje("Opis poškodbe *", new TextBox { Multiline = true, Height = 90, ScrollBars = ScrollBars.Vertical }, 30, 255, 530);

            DodajNaslovPolja("Fotografije poškodbe (klik = povečava, desni klik = odstrani)", 30, 380, 530);
            panelSlike = new FlowLayoutPanel
            {
                Location = new Point(30, 402),
                Size = new Size(530, 100),
                AutoScroll = true,
                WrapContents = false,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelGlavno.Controls.Add(panelSlike);
            var btnDodajSliko = UiPomocnik.Gumb("📷 DODAJ FOTOGRAFIJE", Tema.Modra, 200, 32);
            btnDodajSliko.Location = new Point(30, 510);
            btnDodajSliko.Click += (s, e) => DodajSlike();
            panelGlavno.Controls.Add(btnDodajSliko);

            IzbiraAvtomobila.NapolniComboBox(cmbAvto, false, poskodba?.AvtomobilId ?? avtomobilId);
            cmbAvto.SelectedIndexChanged += (s, e) => NaloziRezervacije();
            NaloziRezervacije();

            if (jeUrejanje)
            {
                dtpDatum.Value = poskodba.Datum;
                cmbResnost.SelectedItem = poskodba.Resnost;
                numStrosek.Value = Math.Min(numStrosek.Maximum, poskodba.OcenjeniStrosek);
                cmbStatus.SelectedItem = poskodba.Status;
                txtOpis.Text = poskodba.Opis;
                slike.AddRange(poskodba.Slike);
            }
            PrikaziSlike();
        }

        private void NaloziRezervacije()
        {
            cmbRezervacija.Items.Clear();
            cmbRezervacija.Items.Add(new RezervacijaItem { Id = null, Naziv = Jezik.T("— brez —") });
            cmbRezervacija.SelectedIndex = 0;

            if (!(cmbAvto.SelectedItem is IzbiraAvtomobila avto))
                return;

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string sql = @"SELECT r.id, r.datum_od, r.datum_do, (s.ime || ' ' || s.priimek) AS stranka
                               FROM rezervacije r LEFT JOIN stranke s ON r.stranka_id = s.id
                               WHERE r.avtomobil_id = @id ORDER BY r.datum_od DESC LIMIT 50";
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", avto.Id);
                    using (var b = ukaz.ExecuteReader())
                    {
                        while (b.Read())
                        {
                            var item = new RezervacijaItem
                            {
                                Id = Convert.ToInt32(b["id"]),
                                Naziv = $"#{b["id"]} – {b["stranka"]} ({DateTime.Parse(b["datum_od"].ToString()):d.M.yyyy} – {DateTime.Parse(b["datum_do"].ToString()):d.M.yyyy})"
                            };
                            cmbRezervacija.Items.Add(item);
                            if (item.Id == zacetnaRezervacija)
                                cmbRezervacija.SelectedItem = item;
                        }
                    }
                }
            }
        }

        private void DodajSlike()
        {
            using (var dialog = new OpenFileDialog { Filter = SlikePomocnik.FilterSlik, Multiselect = true })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    slike.AddRange(dialog.FileNames);
                    PrikaziSlike();
                }
            }
        }

        private void PrikaziSlike()
        {
            foreach (Control c in panelSlike.Controls)
                (c as PictureBox)?.Image?.Dispose();
            panelSlike.Controls.Clear();

            foreach (string pot in slike)
            {
                string p = pot;
                var slicica = new PictureBox
                {
                    Size = new Size(110, 78),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = SlikePomocnik.Slicica(p, 110, 78),
                    Margin = new Padding(4),
                    Cursor = Cursors.Hand
                };
                slicica.MouseUp += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (Sporocilo.Vprasaj("Odstranim to fotografijo?"))
                        {
                            slike.Remove(p);
                            if (!System.IO.Path.IsPathRooted(p))
                                izbrisaneSlike.Add(p);
                            PrikaziSlike();
                        }
                    }
                    else
                    {
                        using (var pregled = new PregledSlikeObrazec(p))
                            pregled.ShowDialog(this);
                    }
                };
                panelSlike.Controls.Add(slicica);
            }
        }

        protected override void Shrani()
        {
            if (!(cmbAvto.SelectedItem is IzbiraAvtomobila avto))
            {
                Sporocilo.Opozorilo("Izberite avtomobil!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOpis.Text))
            {
                Sporocilo.Opozorilo("Vnesite opis poškodbe!");
                txtOpis.Focus();
                return;
            }

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();
                    using (var transakcija = povezava.BeginTransaction())
                    {
                        string sql = jeUrejanje
                            ? @"UPDATE poskodbe SET avtomobil_id = @avto, rezervacija_id = @rez, datum = @datum, opis = @opis,
                                    resnost = @resnost, ocenjeni_strosek = @strosek, status = @status WHERE id = @id"
                            : @"INSERT INTO poskodbe (avtomobil_id, rezervacija_id, datum, opis, resnost, ocenjeni_strosek, status)
                                    VALUES (@avto, @rez, @datum, @opis, @resnost, @strosek, @status)";

                        long poskodbaId;
                        using (var ukaz = new SQLiteCommand(sql, povezava, transakcija))
                        {
                            var rez = cmbRezervacija.SelectedItem as RezervacijaItem;
                            ukaz.Parameters.AddWithValue("@avto", avto.Id);
                            ukaz.Parameters.AddWithValue("@rez", (object)rez?.Id ?? DBNull.Value);
                            ukaz.Parameters.AddWithValue("@datum", dtpDatum.Value.ToString("yyyy-MM-dd"));
                            ukaz.Parameters.AddWithValue("@opis", txtOpis.Text.Trim());
                            ukaz.Parameters.AddWithValue("@resnost", cmbResnost.SelectedItem.ToString());
                            ukaz.Parameters.AddWithValue("@strosek", numStrosek.Value);
                            ukaz.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                            if (jeUrejanje)
                                ukaz.Parameters.AddWithValue("@id", poskodbaZaUrejanje.Id);
                            ukaz.ExecuteNonQuery();
                            poskodbaId = jeUrejanje ? poskodbaZaUrejanje.Id : povezava.LastInsertRowId;
                        }

                        foreach (string pot in izbrisaneSlike)
                        {
                            using (var ukaz = new SQLiteCommand("DELETE FROM poskodbe_slike WHERE poskodba_id = @id AND pot_slike = @pot", povezava, transakcija))
                            {
                                ukaz.Parameters.AddWithValue("@id", poskodbaId);
                                ukaz.Parameters.AddWithValue("@pot", pot);
                                ukaz.ExecuteNonQuery();
                            }
                        }

                        // Nove slike (polne poti) kopiramo v Slike\poskodbe
                        foreach (string pot in slike)
                        {
                            if (!System.IO.Path.IsPathRooted(pot))
                                continue;
                            string relativna = SlikePomocnik.KopirajVMapo(pot, "poskodbe");
                            using (var ukaz = new SQLiteCommand("INSERT INTO poskodbe_slike (poskodba_id, pot_slike) VALUES (@id, @pot)", povezava, transakcija))
                            {
                                ukaz.Parameters.AddWithValue("@id", poskodbaId);
                                ukaz.Parameters.AddWithValue("@pot", relativna);
                                ukaz.ExecuteNonQuery();
                            }
                        }

                        transakcija.Commit();
                    }
                }

                foreach (string pot in izbrisaneSlike)
                    SlikePomocnik.IzbrisiDatoteko(pot);

                base.Shrani();
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri shranjevanju: {0}", ex.Message);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            foreach (Control c in panelSlike.Controls)
                (c as PictureBox)?.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
