using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;

namespace CarRental.Obrazci
{
    public partial class DashboardObrazec : Form
    {
        public DashboardObrazec()
        {
            InitializeComponent();
            NastaviDataGridView(dgvZadnjeRezervacije);
            NastaviDataGridView(dgvPopularni);

            NaloziStatistike();
            NaloziZadnjeRezervacije();
            NaloziPopularneAvtomobile();
        }

        private void NastaviDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 33, 33);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 253);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void NaloziStatistike()
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();

                // Statistika 1: Avtomobili
                using (var ukaz = new SQLiteCommand(
                    "SELECT COUNT(*) AS skupaj, SUM(CASE WHEN status='prosto' THEN 1 ELSE 0 END) AS prosti FROM avtomobili",
                    povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    if (bralec.Read())
                    {
                        int skupaj = Convert.ToInt32(bralec["skupaj"]);
                        int prosti = bralec["prosti"] == DBNull.Value ? 0 : Convert.ToInt32(bralec["prosti"]);
                        lblVrednostKartica1.Text = skupaj.ToString();
                        lblPodnaslovKartica1.Text = $"prostih: {prosti}";
                    }
                }

                // Statistika 2: Stranke
                using (var ukaz = new SQLiteCommand("SELECT COUNT(*) FROM stranke", povezava))
                {
                    int stevilo = Convert.ToInt32(ukaz.ExecuteScalar());
                    lblVrednostKartica2.Text = stevilo.ToString();
                }

                // Statistika 3: Aktivne rezervacije
                using (var ukaz = new SQLiteCommand(
                    "SELECT COUNT(*) AS aktivne, (SELECT COUNT(*) FROM rezervacije) AS skupaj FROM rezervacije WHERE status='aktivna'",
                    povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    if (bralec.Read())
                    {
                        int aktivne = Convert.ToInt32(bralec["aktivne"]);
                        int skupaj = Convert.ToInt32(bralec["skupaj"]);
                        lblVrednostKartica3.Text = aktivne.ToString();
                        lblPodnaslovKartica3.Text = $"od skupaj {skupaj}";
                    }
                }

                // Statistika 4: Skupni prihodki
                using (var ukaz = new SQLiteCommand(
                    "SELECT COALESCE(SUM(znesek), 0) FROM placila WHERE status='placano'",
                    povezava))
                {
                    decimal prihodki = Convert.ToDecimal(ukaz.ExecuteScalar());
                    lblVrednostKartica4.Text = $"{prihodki:F2} €";
                }
            }
        }

        private void NaloziZadnjeRezervacije()
        {
            string sql = @"
                SELECT r.id AS 'ID',
                       (s.ime || ' ' || s.priimek) AS 'Stranka',
                       (a.znamka || ' ' || a.model) AS 'Avto',
                       r.datum_od AS 'Od',
                       r.skupna_cena AS 'Cena (€)',
                       r.status AS 'Status'
                FROM rezervacije r
                LEFT JOIN stranke s ON r.stranka_id = s.id
                LEFT JOIN avtomobili a ON r.avtomobil_id = a.id
                ORDER BY r.id DESC
                LIMIT 10";

            DataTable tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var adapter = new SQLiteDataAdapter(ukaz))
                    adapter.Fill(tabela);
            }

            dgvZadnjeRezervacije.DataSource = tabela;

            if (dgvZadnjeRezervacije.Columns["ID"] != null)
                dgvZadnjeRezervacije.Columns["ID"].Width = 50;
            if (dgvZadnjeRezervacije.Columns["Cena (€)"] != null)
                dgvZadnjeRezervacije.Columns["Cena (€)"].DefaultCellStyle.Format = "N2";
            if (dgvZadnjeRezervacije.Columns["Od"] != null)
                dgvZadnjeRezervacije.Columns["Od"].DefaultCellStyle.Format = "d.M.yyyy";
        }

        private void NaloziPopularneAvtomobile()
        {
            string sql = @"
                SELECT (a.znamka || ' ' || a.model) AS 'Avtomobil',
                       COUNT(r.id) AS 'Rezervacije',
                       COALESCE(SUM(r.skupna_cena), 0) AS 'Prihodki (€)'
                FROM avtomobili a
                LEFT JOIN rezervacije r ON a.id = r.avtomobil_id
                GROUP BY a.id
                ORDER BY COUNT(r.id) DESC, SUM(r.skupna_cena) DESC
                LIMIT 10";

            DataTable tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var adapter = new SQLiteDataAdapter(ukaz))
                    adapter.Fill(tabela);
            }

            dgvPopularni.DataSource = tabela;

            if (dgvPopularni.Columns["Prihodki (€)"] != null)
                dgvPopularni.Columns["Prihodki (€)"].DefaultCellStyle.Format = "N2";
        }
    }
}