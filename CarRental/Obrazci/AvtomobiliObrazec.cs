using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;

namespace CarRental.Obrazci
{
    public partial class AvtomobiliObrazec : Form
    {
        private List<Kategorija> kategorije;
        private Avtomobil izbranAvtomobil;

        public AvtomobiliObrazec()
        {
            InitializeComponent();
            NastaviDataGridView();
            NaloziKategorije();
            NaloziStatusFilter();
            NaloziAvtomobile();

            // Event ko se izbere vrstica
            dgvAvtomobili.SelectionChanged += DgvAvtomobili_SelectionChanged;

            // Event ko se spremeni iskanje ali filter
            txtIskanje.TextChanged += (s, e) => NaloziAvtomobile();
            cmbFilterKategorija.SelectedIndexChanged += (s, e) => NaloziAvtomobile();
            cmbFilterStatus.SelectedIndexChanged += (s, e) => NaloziAvtomobile();
        }

        private void NastaviDataGridView()
        {
            // Style za DataGridView
            dgvAvtomobili.EnableHeadersVisualStyles = false;
            dgvAvtomobili.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgvAvtomobili.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAvtomobili.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvAvtomobili.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 33, 33);
            dgvAvtomobili.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAvtomobili.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            dgvAvtomobili.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 253);
            dgvAvtomobili.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvAvtomobili.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvAvtomobili.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvAvtomobili.GridColor = Color.FromArgb(230, 230, 230);
            dgvAvtomobili.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
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
                            Naziv = bralec["naziv"].ToString(),
                            Opis = bralec["opis"]?.ToString()
                        });
                    }
                }
            }

            cmbFilterKategorija.Items.Clear();
            cmbFilterKategorija.Items.Add("Vse kategorije");
            foreach (var k in kategorije)
                cmbFilterKategorija.Items.Add(k.Naziv);
            cmbFilterKategorija.SelectedIndex = 0;
        }

        private void NaloziStatusFilter()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.Add("Vsi statusi");
            cmbFilterStatus.Items.Add("prosto");
            cmbFilterStatus.Items.Add("rezervirano");
            cmbFilterStatus.Items.Add("servis");
            cmbFilterStatus.SelectedIndex = 0;
        }

        private void NaloziAvtomobile()
        {
            string sql = @"
                SELECT a.id, a.znamka, a.model, a.letnik, a.registrska_st, 
                       a.barva, a.st_sedezev, a.cena_na_dan, a.status,
                       k.naziv AS kategorija, a.pot_slike
                FROM avtomobili a
                LEFT JOIN kategorije k ON a.kategorija_id = k.id
                WHERE 1=1";

            string iskanje = txtIskanje.Text.Trim();
            if (!string.IsNullOrEmpty(iskanje))
                sql += " AND (a.znamka LIKE @iskanje OR a.model LIKE @iskanje OR a.registrska_st LIKE @iskanje)";

            if (cmbFilterKategorija.SelectedIndex > 0)
                sql += " AND k.naziv = @kategorija";

            if (cmbFilterStatus.SelectedIndex > 0)
                sql += " AND a.status = @status";

            sql += " ORDER BY a.znamka, a.model";

            DataTable tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    if (!string.IsNullOrEmpty(iskanje))
                        ukaz.Parameters.AddWithValue("@iskanje", "%" + iskanje + "%");
                    if (cmbFilterKategorija.SelectedIndex > 0)
                        ukaz.Parameters.AddWithValue("@kategorija", cmbFilterKategorija.SelectedItem.ToString());
                    if (cmbFilterStatus.SelectedIndex > 0)
                        ukaz.Parameters.AddWithValue("@status", cmbFilterStatus.SelectedItem.ToString());

                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }

            dgvAvtomobili.DataSource = tabela;

            // Skrij stolpce id in pot_slike
            if (dgvAvtomobili.Columns["id"] != null)
                dgvAvtomobili.Columns["id"].Visible = false;
            if (dgvAvtomobili.Columns["pot_slike"] != null)
                dgvAvtomobili.Columns["pot_slike"].Visible = false;

            // Lepša imena stolpcev
            if (dgvAvtomobili.Columns["znamka"] != null) dgvAvtomobili.Columns["znamka"].HeaderText = "Znamka";
            if (dgvAvtomobili.Columns["model"] != null) dgvAvtomobili.Columns["model"].HeaderText = "Model";
            if (dgvAvtomobili.Columns["letnik"] != null) dgvAvtomobili.Columns["letnik"].HeaderText = "Letnik";
            if (dgvAvtomobili.Columns["registrska_st"] != null) dgvAvtomobili.Columns["registrska_st"].HeaderText = "Registrska";
            if (dgvAvtomobili.Columns["barva"] != null) dgvAvtomobili.Columns["barva"].HeaderText = "Barva";
            if (dgvAvtomobili.Columns["st_sedezev"] != null) dgvAvtomobili.Columns["st_sedezev"].HeaderText = "Sedeži";
            if (dgvAvtomobili.Columns["cena_na_dan"] != null) dgvAvtomobili.Columns["cena_na_dan"].HeaderText = "Cena/dan (€)";
            if (dgvAvtomobili.Columns["status"] != null) dgvAvtomobili.Columns["status"].HeaderText = "Status";
            if (dgvAvtomobili.Columns["kategorija"] != null) dgvAvtomobili.Columns["kategorija"].HeaderText = "Kategorija";
        }

        private void DgvAvtomobili_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAvtomobili.SelectedRows.Count == 0)
            {
                PocistiDetajle();
                return;
            }

            var vrstica = dgvAvtomobili.SelectedRows[0];

            izbranAvtomobil = new Avtomobil
            {
                Id = Convert.ToInt32(vrstica.Cells["id"].Value),
                Znamka = vrstica.Cells["znamka"].Value?.ToString(),
                Model = vrstica.Cells["model"].Value?.ToString(),
                Letnik = Convert.ToInt32(vrstica.Cells["letnik"].Value),
                RegistrskaSt = vrstica.Cells["registrska_st"].Value?.ToString(),
                Barva = vrstica.Cells["barva"].Value?.ToString(),
                StSedezev = Convert.ToInt32(vrstica.Cells["st_sedezev"].Value),
                CenaNaDan = Convert.ToDecimal(vrstica.Cells["cena_na_dan"].Value),
                Status = vrstica.Cells["status"].Value?.ToString(),
                PotSlike = vrstica.Cells["pot_slike"].Value?.ToString()
            };

            string kategorija = vrstica.Cells["kategorija"].Value?.ToString();

            lblImeAvta.Text = $"{izbranAvtomobil.Znamka} {izbranAvtomobil.Model}";
            lblRegistrska.Text = $"Registrska: {izbranAvtomobil.RegistrskaSt}";
            lblCena.Text = $"Cena na dan: {izbranAvtomobil.CenaNaDan:F2} €";
            lblKategorija.Text = $"Kategorija: {kategorija}";
            lblLetnik.Text = $"Letnik: {izbranAvtomobil.Letnik}";
            lblSedezev.Text = $"Št. sedežev: {izbranAvtomobil.StSedezev}";
            lblBarva.Text = $"Barva: {izbranAvtomobil.Barva}";

            // Status z barvo
            lblStatus.Text = izbranAvtomobil.Status.ToUpper();
            switch (izbranAvtomobil.Status)
            {
                case "prosto":
                    lblStatus.BackColor = Color.FromArgb(76, 175, 80);
                    break;
                case "rezervirano":
                    lblStatus.BackColor = Color.FromArgb(33, 150, 243);
                    break;
                case "servis":
                    lblStatus.BackColor = Color.FromArgb(255, 152, 0);
                    break;
                default:
                    lblStatus.BackColor = Color.Gray;
                    break;
            }

            // Naloži sliko
            NaloziSliko(izbranAvtomobil.PotSlike);
        }

        private void NaloziSliko(string potSlike)
        {
            if (!string.IsNullOrEmpty(potSlike))
            {
                string polnaPot = Path.Combine(Application.StartupPath, potSlike);
                if (File.Exists(polnaPot))
                {
                    try
                    {
                        picSlika.Image = Image.FromFile(polnaPot);
                        return;
                    }
                    catch { }
                }
            }
            picSlika.Image = null;
        }

        private void PocistiDetajle()
        {
            izbranAvtomobil = null;
            lblImeAvta.Text = "Izberite avtomobil";
            lblRegistrska.Text = "-";
            lblCena.Text = "Cena na dan: -";
            lblKategorija.Text = "Kategorija: -";
            lblLetnik.Text = "Letnik: -";
            lblSedezev.Text = "Št. sedežev: -";
            lblBarva.Text = "Barva: -";
            lblStatus.Text = "";
            lblStatus.BackColor = Color.Gray;
            picSlika.Image = null;
        }
        
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            DodajUrediAvtomobilObrazec dodaj = new DodajUrediAvtomobilObrazec();
            if (dodaj.ShowDialog() == DialogResult.OK)
            {
                NaloziAvtomobile();
            }
        }
        
        private void btnUredi_Click(object sender, EventArgs e)
        {
            if (izbranAvtomobil == null)
            {
                MessageBox.Show("Najprej izberite avtomobil!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DodajUrediAvtomobilObrazec uredi = new DodajUrediAvtomobilObrazec(izbranAvtomobil);
            if (uredi.ShowDialog() == DialogResult.OK)
            {
                NaloziAvtomobile();
            }
        }
        
        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            if (izbranAvtomobil == null)
            {
                MessageBox.Show("Najprej izberite avtomobil!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rezultat = MessageBox.Show(
                $"Ali res želite izbrisati avtomobil {izbranAvtomobil.Znamka} {izbranAvtomobil.Model}?",
                "Potrditev brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rezultat == DialogResult.Yes)
            {
                try
                {
                    using (var povezava = PovezavaBaze.PridobiPovezavo())
                    {
                        povezava.Open();
                        string sql = "DELETE FROM avtomobili WHERE id = @id";
                        using (var ukaz = new SQLiteCommand(sql, povezava))
                        {
                            ukaz.Parameters.AddWithValue("@id", izbranAvtomobil.Id);
                            ukaz.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Avtomobil je bil uspešno izbrisan.", "Uspeh",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NaloziAvtomobile();
                    PocistiDetajle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Napaka pri brisanju: " + ex.Message, "Napaka",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            NaloziAvtomobile();
            PocistiDetajle();
        }
    }
}