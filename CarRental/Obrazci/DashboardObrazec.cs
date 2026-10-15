using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public partial class DashboardObrazec : OsnovniObrazec
    {
        public DashboardObrazec()
        {
            InitializeComponent();
            NastaviDataGridView(dgvZadnjeRezervacije);
            NastaviDataGridView(dgvPopularni);

            NaloziStatistike();
            NaloziZadnjeRezervacije();
            NaloziPopularneAvtomobile();
            DodajOpozorilaInGrafe();
        }

        // V3: vrstica z opozorili in dva grafa med karticami in tabelami
        private void DodajOpozorilaInGrafe()
        {
            var opozorila = new List<string>();
            int servisi = ServisPomocnik.PrihajajociServisi(Nastavitve.ServisOpozoriloDni).Rows.Count;
            if (servisi > 0)
                opozorila.Add(Jezik.T("🔧 Servis potreben: {0}", servisi));

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("SELECT COUNT(*) FROM poskodbe WHERE status = 'odprta'", povezava))
                {
                    int poskodbe = Convert.ToInt32(ukaz.ExecuteScalar());
                    if (poskodbe > 0)
                        opozorila.Add(Jezik.T("⚠ Odprte poškodbe: {0}", poskodbe));
                }
                using (var ukaz = new SQLiteCommand(
                    "SELECT COUNT(*) FROM rezervacije WHERE status = 'aktivna' AND datum_do = @danes", povezava))
                {
                    ukaz.Parameters.AddWithValue("@danes", DateTime.Today.ToString("yyyy-MM-dd"));
                    int vrnitve = Convert.ToInt32(ukaz.ExecuteScalar());
                    if (vrnitve > 0)
                        opozorila.Add(Jezik.T("🚗 Danes se vrne vozil: {0}", vrnitve));
                }
                using (var ukaz = new SQLiteCommand(
                    "SELECT COUNT(*) FROM rezervacije WHERE status = 'aktivna' AND datum_do < @danes", povezava))
                {
                    ukaz.Parameters.AddWithValue("@danes", DateTime.Today.ToString("yyyy-MM-dd"));
                    int zamujene = Convert.ToInt32(ukaz.ExecuteScalar());
                    if (zamujene > 0)
                        opozorila.Add(Jezik.T("⏰ Zamujene vrnitve: {0}", zamujene));
                }
            }

            var panelOpozorila = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(15, 0, 15, 8),
                Visible = opozorila.Count > 0
            };
            var lblOpozorila = new Label
            {
                Dock = DockStyle.Fill,
                Text = string.Join("     ", opozorila),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(130, 80, 0),
                BackColor = Color.FromArgb(255, 243, 205),
                Padding = new Padding(12, 0, 0, 0)
            };
            panelOpozorila.Controls.Add(lblOpozorila);

            // Grafa: prihodki in število rezervacij v zadnjih 12 mesecih
            var meseci = GrafPomocnik.ZadnjiMeseci(12);
            var prihodki = new Dictionary<string, double>();
            var rezervacije = new Dictionary<string, double>();
            foreach (var m in meseci)
            {
                prihodki[m.ToString("yyyy-MM")] = 0;
                rezervacije[m.ToString("yyyy-MM")] = 0;
            }

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string od = meseci[0].ToString("yyyy-MM-dd");
                using (var ukaz = new SQLiteCommand(@"SELECT substr(datum_placila, 1, 7) AS mesec, SUM(znesek) AS vsota
                        FROM placila WHERE status = 'placano' AND datum_placila >= @od GROUP BY mesec", povezava))
                {
                    ukaz.Parameters.AddWithValue("@od", od);
                    using (var b = ukaz.ExecuteReader())
                        while (b.Read())
                            if (prihodki.ContainsKey(b["mesec"].ToString()))
                                prihodki[b["mesec"].ToString()] = Convert.ToDouble(b["vsota"]);
                }
                using (var ukaz = new SQLiteCommand(@"SELECT substr(datum_od, 1, 7) AS mesec, COUNT(*) AS st
                        FROM rezervacije WHERE status <> 'preklicana' AND datum_od >= @od GROUP BY mesec", povezava))
                {
                    ukaz.Parameters.AddWithValue("@od", od);
                    using (var b = ukaz.ExecuteReader())
                        while (b.Read())
                            if (rezervacije.ContainsKey(b["mesec"].ToString()))
                                rezervacije[b["mesec"].ToString()] = Convert.ToDouble(b["st"]);
                }
            }

            var tockePrihodki = new List<KeyValuePair<string, double>>();
            var tockeRezervacije = new List<KeyValuePair<string, double>>();
            foreach (var m in meseci)
            {
                tockePrihodki.Add(new KeyValuePair<string, double>(GrafPomocnik.OznakaMeseca(m), prihodki[m.ToString("yyyy-MM")]));
                tockeRezervacije.Add(new KeyValuePair<string, double>(GrafPomocnik.OznakaMeseca(m), rezervacije[m.ToString("yyyy-MM")]));
            }

            var grafPrihodki = GrafPomocnik.Ustvari("Prihodki po mesecih (€)");
            GrafPomocnik.Stolpci(grafPrihodki, "Prihodki", tockePrihodki, Tema.Modra, "N0");
            var grafRezervacije = GrafPomocnik.Ustvari("Rezervacije po mesecih");
            GrafPomocnik.Crta(grafRezervacije, "Rezervacije", tockeRezervacije, Tema.Zelena);

            var mreza = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            mreza.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mreza.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grafPrihodki.Margin = new Padding(0, 0, 8, 0);
            grafRezervacije.Margin = new Padding(8, 0, 0, 0);
            mreza.Controls.Add(grafPrihodki, 0, 0);
            mreza.Controls.Add(grafRezervacije, 1, 0);

            var panelGrafi = new Panel { Dock = DockStyle.Top, Height = 270, Padding = new Padding(15, 0, 15, 15) };
            panelGrafi.Controls.Add(mreza);

            // Vrstni red sidranja: kartice, opozorila, grafi, tabele
            Controls.Add(panelGrafi);
            Controls.Add(panelOpozorila);
            Controls.SetChildIndex(panelGlavno, 0);
            Controls.SetChildIndex(panelGrafi, 1);
            Controls.SetChildIndex(panelOpozorila, 2);
            Controls.SetChildIndex(panelKartice, 3);
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