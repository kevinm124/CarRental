using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;

namespace CarRental.Obrazci
{
    public partial class StrankeObrazec : Form
    {
        private Stranka izbranaStranka;

        public StrankeObrazec()
        {
            InitializeComponent();
            NastaviDataGridView();
            NaloziStranke();

            dgvStranke.SelectionChanged += DgvStranke_SelectionChanged;
            txtIskanje.TextChanged += (s, e) => NaloziStranke();
        }

        private void NastaviDataGridView()
        {
            dgvStranke.EnableHeadersVisualStyles = false;
            dgvStranke.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgvStranke.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStranke.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvStranke.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 33, 33);
            dgvStranke.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStranke.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            dgvStranke.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 253);
            dgvStranke.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvStranke.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvStranke.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvStranke.GridColor = Color.FromArgb(230, 230, 230);
            dgvStranke.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void NaloziStranke()
        {
            string sql = @"SELECT id, ime, priimek, email, telefon, naslov, st_vozniske 
                          FROM stranke WHERE 1=1";

            string iskanje = txtIskanje.Text.Trim();
            if (!string.IsNullOrEmpty(iskanje))
            {
                sql += @" AND (ime LIKE @iskanje OR priimek LIKE @iskanje 
                              OR email LIKE @iskanje OR telefon LIKE @iskanje)";
            }

            sql += " ORDER BY priimek, ime";

            DataTable tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    if (!string.IsNullOrEmpty(iskanje))
                        ukaz.Parameters.AddWithValue("@iskanje", "%" + iskanje + "%");

                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }

            dgvStranke.DataSource = tabela;

            // Skrij id stolpec
            if (dgvStranke.Columns["id"] != null)
                dgvStranke.Columns["id"].Visible = false;

            // Lepša imena stolpcev
            if (dgvStranke.Columns["ime"] != null) dgvStranke.Columns["ime"].HeaderText = "Ime";
            if (dgvStranke.Columns["priimek"] != null) dgvStranke.Columns["priimek"].HeaderText = "Priimek";
            if (dgvStranke.Columns["email"] != null) dgvStranke.Columns["email"].HeaderText = "Email";
            if (dgvStranke.Columns["telefon"] != null) dgvStranke.Columns["telefon"].HeaderText = "Telefon";
            if (dgvStranke.Columns["naslov"] != null) dgvStranke.Columns["naslov"].HeaderText = "Naslov";
            if (dgvStranke.Columns["st_vozniske"] != null) dgvStranke.Columns["st_vozniske"].HeaderText = "Št. vozniške";
        }

        private void DgvStranke_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStranke.SelectedRows.Count == 0)
            {
                PocistiDetajle();
                return;
            }

            var vrstica = dgvStranke.SelectedRows[0];

            izbranaStranka = new Stranka
            {
                Id = Convert.ToInt32(vrstica.Cells["id"].Value),
                Ime = vrstica.Cells["ime"].Value?.ToString(),
                Priimek = vrstica.Cells["priimek"].Value?.ToString(),
                Email = vrstica.Cells["email"].Value?.ToString(),
                Telefon = vrstica.Cells["telefon"].Value?.ToString(),
                Naslov = vrstica.Cells["naslov"].Value?.ToString(),
                StVozniske = vrstica.Cells["st_vozniske"].Value?.ToString()
            };

            lblPolnoIme.Text = $"{izbranaStranka.Ime} {izbranaStranka.Priimek}";
            lblEmail.Text = string.IsNullOrEmpty(izbranaStranka.Email) ? "-" : izbranaStranka.Email;
            lblTelefon.Text = string.IsNullOrEmpty(izbranaStranka.Telefon) ? "-" : izbranaStranka.Telefon;
            lblNaslov.Text = string.IsNullOrEmpty(izbranaStranka.Naslov) ? "-" : izbranaStranka.Naslov;
            lblVozniska.Text = string.IsNullOrEmpty(izbranaStranka.StVozniske) ? "-" : izbranaStranka.StVozniske;

            // Inicialke (prva črka imena + prva črka priimka)
            string inicialke = "";
            if (!string.IsNullOrEmpty(izbranaStranka.Ime)) inicialke += izbranaStranka.Ime[0];
            if (!string.IsNullOrEmpty(izbranaStranka.Priimek)) inicialke += izbranaStranka.Priimek[0];
            lblInicialke.Text = inicialke.ToUpper();
        }

        private void PocistiDetajle()
        {
            izbranaStranka = null;
            lblInicialke.Text = "?";
            lblPolnoIme.Text = "Izberite stranko";
            lblEmail.Text = "-";
            lblTelefon.Text = "-";
            lblNaslov.Text = "-";
            lblVozniska.Text = "-";
        }
        
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            DodajUrediStrankoObrazec dodaj = new DodajUrediStrankoObrazec();
            if (dodaj.ShowDialog() == DialogResult.OK)
            {
                NaloziStranke();
            }
        }

        private void btnUredi_Click(object sender, EventArgs e)
        {
            if (izbranaStranka == null)
            {
                MessageBox.Show("Najprej izberite stranko!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DodajUrediStrankoObrazec uredi = new DodajUrediStrankoObrazec(izbranaStranka);
            if (uredi.ShowDialog() == DialogResult.OK)
            {
                NaloziStranke();
            }
        }

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            if (izbranaStranka == null)
            {
                MessageBox.Show("Najprej izberite stranko!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rezultat = MessageBox.Show(
                $"Ali res želite izbrisati stranko {izbranaStranka.Ime} {izbranaStranka.Priimek}?",
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
                        string sql = "DELETE FROM stranke WHERE id = @id";
                        using (var ukaz = new SQLiteCommand(sql, povezava))
                        {
                            ukaz.Parameters.AddWithValue("@id", izbranaStranka.Id);
                            ukaz.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Stranka je bila uspešno izbrisana.", "Uspeh",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NaloziStranke();
                    PocistiDetajle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Napaka pri brisanju: " + ex.Message +
                        "\n\nMorda ima stranka aktivne rezervacije.", "Napaka",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            NaloziStranke();
            PocistiDetajle();
        }
    }
}