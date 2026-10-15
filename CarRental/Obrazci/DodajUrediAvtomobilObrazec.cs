using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public partial class DodajUrediAvtomobilObrazec : OsnovniObrazec
    {
        private Avtomobil avtomobilZaUrejanje;
        private List<Kategorija> kategorije;
        private string novaSlikaPot = null; // pot do nove izbrane slike (še ni kopirana)
        private bool jeUrejanje => avtomobilZaUrejanje != null;

        // Konstruktor za DODAJANJE
        public DodajUrediAvtomobilObrazec()
        {
            InitializeComponent();
            lblNaslovDialoga.Text = "➕  Dodaj nov avtomobil";
            NaloziKategorije();
            NaloziStatuse();
        }

        // Konstruktor za UREJANJE
        public DodajUrediAvtomobilObrazec(Avtomobil avto) : this()
        {
            avtomobilZaUrejanje = avto;
            lblNaslovDialoga.Text = "✏️  Uredi avtomobil";
            NapolniPolja();
        }

        private void NaloziKategorije()
        {
            kategorije = new List<Kategorija>();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string sql = "SELECT * FROM kategorije ORDER BY naziv";
                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    while (bralec.Read())
                    {
                        kategorije.Add(new Kategorija
                        {
                            Id = Convert.ToInt32(bralec["id"]),
                            Naziv = bralec["naziv"].ToString()
                        });
                    }
                }
            }

            cmbKategorija.Items.Clear();
            foreach (var k in kategorije)
                cmbKategorija.Items.Add(k.Naziv);
            if (cmbKategorija.Items.Count > 0)
                cmbKategorija.SelectedIndex = 0;
        }

        private void NaloziStatuse()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("prosto");
            cmbStatus.Items.Add("rezervirano");
            cmbStatus.Items.Add("servis");
            cmbStatus.SelectedIndex = 0;
        }

        private void NapolniPolja()
        {
            txtZnamka.Text = avtomobilZaUrejanje.Znamka;
            txtModel.Text = avtomobilZaUrejanje.Model;
            numLetnik.Value = avtomobilZaUrejanje.Letnik;
            txtRegistrska.Text = avtomobilZaUrejanje.RegistrskaSt;
            txtBarva.Text = avtomobilZaUrejanje.Barva;
            numSedezev.Value = avtomobilZaUrejanje.StSedezev;
            numCena.Value = avtomobilZaUrejanje.CenaNaDan;
            cmbStatus.SelectedItem = avtomobilZaUrejanje.Status;

            // Najdi kategorijo
            var kategorija = kategorije.Find(k => k.Id == avtomobilZaUrejanje.KategorijaId);
            if (kategorija != null)
                cmbKategorija.SelectedItem = kategorija.Naziv;

            // Naloži obstoječo sliko
            if (!string.IsNullOrEmpty(avtomobilZaUrejanje.PotSlike))
            {
                string polnaPot = Path.Combine(Application.StartupPath, avtomobilZaUrejanje.PotSlike);
                if (File.Exists(polnaPot))
                    picSlika.Image = SlikePomocnik.Nalozi(polnaPot);
            }
        }

        private void btnIzberiSliko_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Slike|*.jpg;*.jpeg;*.png;*.bmp";
                dialog.Title = "Izberite sliko avtomobila";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Naloži v PictureBox, ampak NE kopiraj še v Slike mapo
                        // (kopiramo šele ko klikneš Shrani)
                        novaSlikaPot = dialog.FileName;
                        picSlika.Image = SlikePomocnik.Nalozi(novaSlikaPot);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Napaka pri nalaganju slike: " + ex.Message,
                            "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnOdstraniSliko_Click(object sender, EventArgs e)
        {
            picSlika.Image = null;
            novaSlikaPot = null;
            if (jeUrejanje)
                avtomobilZaUrejanje.PotSlike = null;
        }

        private bool PreveriPolja()
        {
            if (string.IsNullOrWhiteSpace(txtZnamka.Text))
            {
                MessageBox.Show("Vnesite znamko avtomobila!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZnamka.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Vnesite model avtomobila!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtModel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRegistrska.Text))
            {
                MessageBox.Show("Vnesite registrsko številko!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegistrska.Focus();
                return false;
            }

            if (numCena.Value <= 0)
            {
                MessageBox.Show("Cena na dan mora biti večja od 0!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCena.Focus();
                return false;
            }

            if (cmbKategorija.SelectedIndex < 0)
            {
                MessageBox.Show("Izberite kategorijo!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private string ShraniSlikoVMapo()
        {
            if (string.IsNullOrEmpty(novaSlikaPot))
                return jeUrejanje ? avtomobilZaUrejanje.PotSlike : null;

            try
            {
                // Ustvari mapo Slike/avtomobili če ne obstaja
                string mapaSlik = Path.Combine(Application.StartupPath, "Slike", "avtomobili");
                if (!Directory.Exists(mapaSlik))
                    Directory.CreateDirectory(mapaSlik);

                // Generiraj unikatno ime datoteke
                string razsiritev = Path.GetExtension(novaSlikaPot);
                string novoIme = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}{razsiritev}";
                string polnaPot = Path.Combine(mapaSlik, novoIme);

                // Kopiraj datoteko
                File.Copy(novaSlikaPot, polnaPot, true);

                // Vrni relativno pot (Slike\avtomobili\xxx.jpg)
                return Path.Combine("Slike", "avtomobili", novoIme);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri shranjevanju slike: " + ex.Message,
                    "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnShrani_Click(object sender, EventArgs e)
        {
            if (!PreveriPolja())
                return;

            try
            {
                // Najprej shrani sliko (če je izbrana)
                string potSlike = ShraniSlikoVMapo();

                // Najdi kategorijo ID
                int kategorijaId = kategorije[cmbKategorija.SelectedIndex].Id;

                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();

                    string sql;
                    if (jeUrejanje)
                    {
                        sql = @"UPDATE avtomobili SET 
                                znamka = @znamka, model = @model, letnik = @letnik,
                                registrska_st = @registrska, barva = @barva, 
                                st_sedezev = @sedezev, cena_na_dan = @cena,
                                status = @status, kategorija_id = @kategorija,
                                pot_slike = @slika
                                WHERE id = @id";
                    }
                    else
                    {
                        sql = @"INSERT INTO avtomobili 
                                (znamka, model, letnik, registrska_st, barva, 
                                 st_sedezev, cena_na_dan, status, kategorija_id, pot_slike)
                                VALUES 
                                (@znamka, @model, @letnik, @registrska, @barva,
                                 @sedezev, @cena, @status, @kategorija, @slika)";
                    }

                    using (var ukaz = new SQLiteCommand(sql, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@znamka", txtZnamka.Text.Trim());
                        ukaz.Parameters.AddWithValue("@model", txtModel.Text.Trim());
                        ukaz.Parameters.AddWithValue("@letnik", (int)numLetnik.Value);
                        ukaz.Parameters.AddWithValue("@registrska", txtRegistrska.Text.Trim());
                        ukaz.Parameters.AddWithValue("@barva", txtBarva.Text.Trim());
                        ukaz.Parameters.AddWithValue("@sedezev", (int)numSedezev.Value);
                        ukaz.Parameters.AddWithValue("@cena", numCena.Value);
                        ukaz.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                        ukaz.Parameters.AddWithValue("@kategorija", kategorijaId);
                        ukaz.Parameters.AddWithValue("@slika", (object)potSlike ?? DBNull.Value);

                        if (jeUrejanje)
                            ukaz.Parameters.AddWithValue("@id", avtomobilZaUrejanje.Id);

                        ukaz.ExecuteNonQuery();
                    }
                }

                // Nova glavna slika se doda tudi v galerijo
                PovezavaBaze.SinhronizirajGlavneSlike();

                MessageBox.Show(
                    jeUrejanje ? "Avtomobil je bil uspešno posodobljen." : "Avtomobil je bil uspešno dodan.",
                    "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
            {
                MessageBox.Show("Registrska številka že obstaja v bazi!", "Napaka",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri shranjevanju: " + ex.Message, "Napaka",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreklici_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}