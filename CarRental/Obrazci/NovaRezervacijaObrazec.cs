using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using CarRental.Baza;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public partial class NovaRezervacijaObrazec : OsnovniObrazec
    {
        // Razrede za hranjenje podatkov v ComboBox-ih
        private class StrankaItem
        {
            public int Id { get; set; }
            public string PrikaznoIme { get; set; }
            public string Email { get; set; }
            public override string ToString() => PrikaznoIme;
        }

        // V3: popust zvestobe za izbrano stranko
        private decimal popust = 0;
        private Label lblPopustNaslov;
        private Label lblPopust;

        private class AvtomobilItem
        {
            public int Id { get; set; }
            public string PrikaznoIme { get; set; }
            public decimal CenaNaDan { get; set; }
            public override string ToString() => PrikaznoIme;
        }

        public NovaRezervacijaObrazec()
        {
            InitializeComponent();
            DodajPrikazPopusta();
            NaloziStranke();
            NaloziProsteAvtomobile();
            NaloziStatuse();

            // Privzeti datumi
            dtpDatumOd.Value = DateTime.Today;
            dtpDatumDo.Value = DateTime.Today.AddDays(1);

            // Minimalni datum = danes (ne moreš rezervirati v preteklosti)
            dtpDatumOd.MinDate = DateTime.Today;
            dtpDatumDo.MinDate = DateTime.Today;

            // Eventi za avtomatski izračun cene
            cmbStranka.SelectedIndexChanged += (s, e) => PosodobiPopust();
            cmbAvto.SelectedIndexChanged += (s, e) => PreracunajCeno();
            dtpDatumOd.ValueChanged += (s, e) =>
            {
                if (dtpDatumDo.Value <= dtpDatumOd.Value)
                    dtpDatumDo.Value = dtpDatumOd.Value.AddDays(1);
                PreracunajCeno();
            };
            dtpDatumDo.ValueChanged += (s, e) => PreracunajCeno();
        }

        private void NaloziStranke()
        {
            cmbStranka.Items.Clear();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string sql = "SELECT id, ime, priimek, email FROM stranke ORDER BY priimek, ime";
                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    while (bralec.Read())
                    {
                        string email = bralec["email"]?.ToString();
                        string prikaz = $"{bralec["ime"]} {bralec["priimek"]}";
                        if (!string.IsNullOrEmpty(email))
                            prikaz += $" ({email})";

                        cmbStranka.Items.Add(new StrankaItem
                        {
                            Id = Convert.ToInt32(bralec["id"]),
                            PrikaznoIme = prikaz,
                            Email = email
                        });
                    }
                }
            }

            if (cmbStranka.Items.Count == 0)
            {
                MessageBox.Show("Najprej dodajte vsaj eno stranko!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NaloziProsteAvtomobile()
        {
            cmbAvto.Items.Clear();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                // Samo prosti avtomobili
                string sql = @"SELECT id, znamka, model, registrska_st, cena_na_dan 
                              FROM avtomobili 
                              WHERE status = 'prosto'
                              ORDER BY znamka, model";

                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    while (bralec.Read())
                    {
                        decimal cena = Convert.ToDecimal(bralec["cena_na_dan"]);
                        string prikaz = $"{bralec["znamka"]} {bralec["model"]} ({bralec["registrska_st"]}) - {cena:F2} €/dan";

                        cmbAvto.Items.Add(new AvtomobilItem
                        {
                            Id = Convert.ToInt32(bralec["id"]),
                            PrikaznoIme = prikaz,
                            CenaNaDan = cena
                        });
                    }
                }
            }

            if (cmbAvto.Items.Count == 0)
            {
                MessageBox.Show("Trenutno ni nobenega prostega avtomobila!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NaloziStatuse()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("aktivna");
            cmbStatus.SelectedIndex = 0;
        }

        private void DodajPrikazPopusta()
        {
            lblPopustNaslov = new Label
            {
                Text = "POPUST ZVESTOBE",
                Location = new Point(390, 45),
                Size = new Size(215, 18),
                Font = new Font("Segoe UI", 8.25F, FontStyle.Bold),
                ForeColor = Color.FromArgb(120, 120, 120),
                TextAlign = ContentAlignment.MiddleRight
            };
            lblPopust = new Label
            {
                Text = "-",
                Location = new Point(390, 68),
                Size = new Size(215, 22),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                TextAlign = ContentAlignment.MiddleRight
            };
            panelPovzetek.Controls.Add(lblPopustNaslov);
            panelPovzetek.Controls.Add(lblPopust);
        }

        private void PosodobiPopust()
        {
            popust = 0;
            lblPopust.Text = "-";
            if (cmbStranka.SelectedItem is StrankaItem stranka)
            {
                var stopnja = Zvestoba.StopnjaStranke(stranka.Id);
                if (stopnja != null)
                {
                    popust = stopnja.Popust;
                    lblPopust.Text = $"{Jezik.T(stopnja.Naziv)}: −{popust:0.#} %";
                }
            }
            PreracunajCeno();
        }

        private void PreracunajCeno()
        {
            if (cmbAvto.SelectedItem == null)
            {
                lblSteviloDni.Text = "0 dni";
                lblCenaDan.Text = "0,00 €";
                lblSkupaj.Text = "0,00 €";
                return;
            }

            AvtomobilItem izbran = (AvtomobilItem)cmbAvto.SelectedItem;
            int stDni = (dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1;

            if (stDni < 1) stDni = 1;

            decimal skupaj = Zvestoba.IzracunajCeno(stDni, izbran.CenaNaDan, popust);

            lblSteviloDni.Text = $"{stDni} {(stDni == 1 ? "dan" : "dni")}";
            lblCenaDan.Text = $"{izbran.CenaNaDan:F2} €";
            lblSkupaj.Text = $"{skupaj:F2} €";
        }

        private bool PreveriPolja()
        {
            if (cmbStranka.SelectedItem == null)
            {
                MessageBox.Show("Izberite stranko!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbAvto.SelectedItem == null)
            {
                MessageBox.Show("Izberite avtomobil!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpDatumDo.Value.Date < dtpDatumOd.Value.Date)
            {
                MessageBox.Show("Datum 'do' ne sme biti pred datumom 'od'!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void btnShrani_Click(object sender, EventArgs e)
        {
            if (!PreveriPolja())
                return;

            try
            {
                StrankaItem stranka = (StrankaItem)cmbStranka.SelectedItem;
                AvtomobilItem avto = (AvtomobilItem)cmbAvto.SelectedItem;

                int stDni = (dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1;
                decimal skupaj = Zvestoba.IzracunajCeno(stDni, avto.CenaNaDan, popust);
                long novaRezervacijaId;

                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();

                    // Vstavi rezervacijo
                    string sql1 = @"INSERT INTO rezervacije 
                                   (stranka_id, avtomobil_id, zaposleni_id, 
                                    datum_od, datum_do, skupna_cena, status, popust)
                                   VALUES 
                                   (@stranka, @avto, @zaposleni, 
                                    @od, @do, @cena, @status, @popust)";

                    using (var ukaz = new SQLiteCommand(sql1, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@stranka", stranka.Id);
                        ukaz.Parameters.AddWithValue("@avto", avto.Id);
                        ukaz.Parameters.AddWithValue("@zaposleni",
                            PrijavaObrazec.TrenutniUporabnik?.Id ?? 1);
                        ukaz.Parameters.AddWithValue("@od", dtpDatumOd.Value.ToString("yyyy-MM-dd"));
                        ukaz.Parameters.AddWithValue("@do", dtpDatumDo.Value.ToString("yyyy-MM-dd"));
                        ukaz.Parameters.AddWithValue("@cena", skupaj);
                        ukaz.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                        ukaz.Parameters.AddWithValue("@popust", popust);

                        ukaz.ExecuteNonQuery();
                        novaRezervacijaId = povezava.LastInsertRowId;
                    }

                    // Posodobi status avtomobila na "rezervirano"
                    string sql2 = "UPDATE avtomobili SET status = 'rezervirano' WHERE id = @id";
                    using (var ukaz = new SQLiteCommand(sql2, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@id", avto.Id);
                        ukaz.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Rezervacija je bila uspešno ustvarjena!\n\nSkupna cena: {skupaj:F2} €",
                    "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // V3: potrditev po emailu
                if (EmailPomocnik.JeNastavljen && !string.IsNullOrWhiteSpace(stranka.Email))
                {
                    var odgovor = MessageBox.Show($"Ali želite stranki poslati potrditev na {stranka.Email}?",
                        "Email", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (odgovor == DialogResult.Yes)
                    {
                        btnShrani.Enabled = false;
                        Cursor = Cursors.WaitCursor;
                        try
                        {
                            await EmailPomocnik.PosljiPotrditevAsync((int)novaRezervacijaId);
                            MessageBox.Show("Potrditev je bila poslana stranki.", "Uspeh",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Pošiljanje ni uspelo: " + ex.GetBaseException().Message, "Napaka",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Cursor = Cursors.Default;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
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