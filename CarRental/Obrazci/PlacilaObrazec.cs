using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;

namespace CarRental.Obrazci
{
    public partial class PlacilaObrazec : Form
    {
        public PlacilaObrazec()
        {
            InitializeComponent();
            NastaviDataGridView();
            NaloziFilterNacinov();
            NaloziPlacila();
            PosodobiStatistike();

            txtIskanje.TextChanged += (s, e) => NaloziPlacila();
            cmbFilterNacin.SelectedIndexChanged += (s, e) => NaloziPlacila();
        }

        private void NastaviDataGridView()
        {
            dgvPlacila.EnableHeadersVisualStyles = false;
            dgvPlacila.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgvPlacila.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPlacila.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPlacila.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 33, 33);
            dgvPlacila.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPlacila.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            dgvPlacila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 253);
            dgvPlacila.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPlacila.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvPlacila.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvPlacila.GridColor = Color.FromArgb(230, 230, 230);
            dgvPlacila.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void NaloziFilterNacinov()
        {
            cmbFilterNacin.Items.Clear();
            cmbFilterNacin.Items.Add("Vsi načini");
            cmbFilterNacin.Items.Add("gotovina");
            cmbFilterNacin.Items.Add("kartica");
            cmbFilterNacin.Items.Add("nakazilo");
            cmbFilterNacin.SelectedIndex = 0;
        }

        private void NaloziPlacila()
        {
            string sql = @"
                SELECT p.id AS 'ID',
                       (s.ime || ' ' || s.priimek) AS 'Stranka',
                       (a.znamka || ' ' || a.model) AS 'Avtomobil',
                       p.znesek AS 'Znesek (€)',
                       p.nacin_placila AS 'Način plačila',
                       p.datum_placila AS 'Datum',
                       p.status AS 'Status'
                FROM placila p
                LEFT JOIN rezervacije r ON p.rezervacija_id = r.id
                LEFT JOIN stranke s ON r.stranka_id = s.id
                LEFT JOIN avtomobili a ON r.avtomobil_id = a.id
                WHERE 1=1";

            string iskanje = txtIskanje.Text.Trim();
            if (!string.IsNullOrEmpty(iskanje))
            {
                sql += @" AND (s.ime LIKE @iskanje OR s.priimek LIKE @iskanje 
                              OR a.znamka LIKE @iskanje OR a.model LIKE @iskanje)";
            }

            if (cmbFilterNacin.SelectedIndex > 0)
                sql += " AND p.nacin_placila = @nacin";

            sql += " ORDER BY p.datum_placila DESC";

            DataTable tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    if (!string.IsNullOrEmpty(iskanje))
                        ukaz.Parameters.AddWithValue("@iskanje", "%" + iskanje + "%");
                    if (cmbFilterNacin.SelectedIndex > 0)
                        ukaz.Parameters.AddWithValue("@nacin", cmbFilterNacin.SelectedItem.ToString());

                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }

            dgvPlacila.DataSource = tabela;

            // Formatiranje stolpcev
            if (dgvPlacila.Columns["ID"] != null)
                dgvPlacila.Columns["ID"].Width = 50;
            if (dgvPlacila.Columns["Znesek (€)"] != null)
                dgvPlacila.Columns["Znesek (€)"].DefaultCellStyle.Format = "N2";
            if (dgvPlacila.Columns["Datum"] != null)
                dgvPlacila.Columns["Datum"].DefaultCellStyle.Format = "d.M.yyyy HH:mm";
        }

        private void PosodobiStatistike()
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string sql = @"SELECT 
                              COALESCE(SUM(znesek), 0) AS skupaj,
                              COUNT(*) AS stevilo,
                              COALESCE(AVG(znesek), 0) AS povprecje
                              FROM placila
                              WHERE status = 'placano'";

                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    if (bralec.Read())
                    {
                        decimal skupaj = Convert.ToDecimal(bralec["skupaj"]);
                        int stevilo = Convert.ToInt32(bralec["stevilo"]);
                        decimal povprecje = Convert.ToDecimal(bralec["povprecje"]);

                        lblVrednost1.Text = $"{skupaj:F2} €";
                        lblVrednost2.Text = stevilo.ToString();
                        lblVrednost3.Text = $"{povprecje:F2} €";
                    }
                }
            }
        }
        
        private void btnNovoPlacilo_Click(object sender, EventArgs e)
        {
            DodajPlaciloObrazec dialog = new DodajPlaciloObrazec();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                NaloziPlacila();
                PosodobiStatistike();
            }
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            NaloziPlacila();
            PosodobiStatistike();
        }
    }
}