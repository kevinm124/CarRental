using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;

namespace CarRental.Obrazci
{
    public partial class RezervacijeObrazec : Form
    {
        private int izbranaRezervacijaId = -1;
        private string trenutniStatus = "";

        public RezervacijeObrazec()
        {
            InitializeComponent();
            NastaviDataGridView();
            NaloziStatusFilter();
            NaloziRezervacije();

            dgvRezervacije.SelectionChanged += DgvRezervacije_SelectionChanged;
            txtIskanje.TextChanged += (s, e) => NaloziRezervacije();
            cmbFilterStatus.SelectedIndexChanged += (s, e) => NaloziRezervacije();
        }

        private void NastaviDataGridView()
        {
            dgvRezervacije.EnableHeadersVisualStyles = false;
            dgvRezervacije.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgvRezervacije.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRezervacije.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvRezervacije.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 33, 33);
            dgvRezervacije.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRezervacije.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            dgvRezervacije.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 253);
            dgvRezervacije.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvRezervacije.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvRezervacije.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvRezervacije.GridColor = Color.FromArgb(230, 230, 230);
            dgvRezervacije.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void NaloziStatusFilter()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.Add("Vsi statusi");
            cmbFilterStatus.Items.Add("aktivna");
            cmbFilterStatus.Items.Add("zakljucena");
            cmbFilterStatus.Items.Add("preklicana");
            cmbFilterStatus.SelectedIndex = 0;
        }

        private void NaloziRezervacije()
        {
            string sql = @"
                SELECT r.id, 
                       (s.ime || ' ' || s.priimek) AS stranka,
                       (a.znamka || ' ' || a.model) AS avto,
                       r.datum_od, r.datum_do, r.skupna_cena, r.status
                FROM rezervacije r
                LEFT JOIN stranke s ON r.stranka_id = s.id
                LEFT JOIN avtomobili a ON r.avtomobil_id = a.id
                WHERE 1=1";

            string iskanje = txtIskanje.Text.Trim();
            if (!string.IsNullOrEmpty(iskanje))
            {
                sql += @" AND (s.ime LIKE @iskanje OR s.priimek LIKE @iskanje 
                              OR a.znamka LIKE @iskanje OR a.model LIKE @iskanje)";
            }

            if (cmbFilterStatus.SelectedIndex > 0)
                sql += " AND r.status = @status";

            sql += " ORDER BY r.datum_od DESC";

            DataTable tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    if (!string.IsNullOrEmpty(iskanje))
                        ukaz.Parameters.AddWithValue("@iskanje", "%" + iskanje + "%");
                    if (cmbFilterStatus.SelectedIndex > 0)
                        ukaz.Parameters.AddWithValue("@status", cmbFilterStatus.SelectedItem.ToString());

                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }

            dgvRezervacije.DataSource = tabela;

            if (dgvRezervacije.Columns["id"] != null)
                dgvRezervacije.Columns["id"].HeaderText = "ID";
            if (dgvRezervacije.Columns["stranka"] != null)
                dgvRezervacije.Columns["stranka"].HeaderText = "Stranka";
            if (dgvRezervacije.Columns["avto"] != null)
                dgvRezervacije.Columns["avto"].HeaderText = "Avtomobil";
            if (dgvRezervacije.Columns["datum_od"] != null)
                dgvRezervacije.Columns["datum_od"].HeaderText = "Od";
            if (dgvRezervacije.Columns["datum_do"] != null)
                dgvRezervacije.Columns["datum_do"].HeaderText = "Do";
            if (dgvRezervacije.Columns["skupna_cena"] != null)
                dgvRezervacije.Columns["skupna_cena"].HeaderText = "Cena (€)";
            if (dgvRezervacije.Columns["status"] != null)
                dgvRezervacije.Columns["status"].HeaderText = "Status";

            if (dgvRezervacije.Columns["id"] != null)
                dgvRezervacije.Columns["id"].Width = 50;

            if (dgvRezervacije.Columns["datum_od"] != null)
                dgvRezervacije.Columns["datum_od"].DefaultCellStyle.Format = "d.M.yyyy";
            if (dgvRezervacije.Columns["datum_do"] != null)
                dgvRezervacije.Columns["datum_do"].DefaultCellStyle.Format = "d.M.yyyy";
        }

        private void DgvRezervacije_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRezervacije.SelectedRows.Count == 0)
            {
                PocistiDetajle();
                return;
            }

            var vrstica = dgvRezervacije.SelectedRows[0];
            izbranaRezervacijaId = Convert.ToInt32(vrstica.Cells["id"].Value);
            trenutniStatus = vrstica.Cells["status"].Value?.ToString();

            lblIdRezervacije.Text = $"Rezervacija #{izbranaRezervacijaId}";
            lblStranka.Text = vrstica.Cells["stranka"].Value?.ToString();
            lblAvto.Text = vrstica.Cells["avto"].Value?.ToString();

            DateTime datumOd = DateTime.Parse(vrstica.Cells["datum_od"].Value.ToString());
            DateTime datumDo = DateTime.Parse(vrstica.Cells["datum_do"].Value.ToString());
            int stDni = (datumDo - datumOd).Days + 1;

            lblObdobje.Text = $"{datumOd:d.M.yyyy} → {datumDo:d.M.yyyy}";
            lblSteviloDni.Text = $"{stDni} {(stDni == 1 ? "dan" : "dni")}";
            lblCena.Text = $"{Convert.ToDecimal(vrstica.Cells["skupna_cena"].Value):F2} €";

            // Barva statusa
            lblStatus.Text = trenutniStatus.ToUpper();
            switch (trenutniStatus)
            {
                case "aktivna":
                    lblStatus.BackColor = Color.FromArgb(33, 150, 243);
                    break;
                case "zakljucena":
                    lblStatus.BackColor = Color.FromArgb(76, 175, 80);
                    break;
                case "preklicana":
                    lblStatus.BackColor = Color.FromArgb(211, 47, 47);
                    break;
                default:
                    lblStatus.BackColor = Color.Gray;
                    break;
            }

            // Onemogoči gumbe za že zaključene/preklicane rezervacije
            btnZakljuci.Enabled = trenutniStatus == "aktivna";
            btnPrekliciRez.Enabled = trenutniStatus == "aktivna";
        }

        private void PocistiDetajle()
        {
            izbranaRezervacijaId = -1;
            trenutniStatus = "";
            lblIdRezervacije.Text = "Rezervacija #-";
            lblStranka.Text = "-";
            lblAvto.Text = "-";
            lblObdobje.Text = "-";
            lblSteviloDni.Text = "-";
            lblCena.Text = "- €";
            lblStatus.Text = "-";
            lblStatus.BackColor = Color.Gray;
            btnZakljuci.Enabled = false;
            btnPrekliciRez.Enabled = false;
        }
        
        private void btnNovaRezervacija_Click(object sender, EventArgs e)
        {
            NovaRezervacijaObrazec dialog = new NovaRezervacijaObrazec();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                NaloziRezervacije();
            }
        }
        
        private void btnZakljuci_Click(object sender, EventArgs e)
        {
            if (izbranaRezervacijaId < 0) return;

            var rezultat = MessageBox.Show(
                "Ali res želite zaključiti to rezervacijo?\n\nAvtomobil bo ponovno na voljo.",
                "Potrditev",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rezultat == DialogResult.Yes)
            {
                SpremeniStatusRezervacije("zakljucena");
            }
        }

        private void btnPrekliciRez_Click(object sender, EventArgs e)
        {
            if (izbranaRezervacijaId < 0) return;

            var rezultat = MessageBox.Show(
                "Ali res želite preklicati to rezervacijo?",
                "Potrditev",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rezultat == DialogResult.Yes)
            {
                SpremeniStatusRezervacije("preklicana");
            }
        }

        private void SpremeniStatusRezervacije(string novStatus)
        {
            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();

                    // Posodobi rezervacijo
                    string sql1 = "UPDATE rezervacije SET status = @status WHERE id = @id";
                    using (var ukaz = new SQLiteCommand(sql1, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@status", novStatus);
                        ukaz.Parameters.AddWithValue("@id", izbranaRezervacijaId);
                        ukaz.ExecuteNonQuery();
                    }

                    // Sprosti avtomobil (postavi nazaj na "prosto")
                    string sql2 = @"UPDATE avtomobili SET status = 'prosto' 
                                   WHERE id = (SELECT avtomobil_id FROM rezervacije WHERE id = @id)";
                    using (var ukaz = new SQLiteCommand(sql2, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@id", izbranaRezervacijaId);
                        ukaz.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Status rezervacije je bil posodobljen.", "Uspeh",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                NaloziRezervacije();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka: " + ex.Message, "Napaka",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            NaloziRezervacije();
            PocistiDetajle();
        }
    }
}