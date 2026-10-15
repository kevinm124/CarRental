using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Zgodovina servisov in prihajajoči servisi
    public class ServisiObrazec : OsnovniObrazec
    {
        private readonly ComboBox cmbAvto;
        private readonly CheckBox chkSamoPrihajajoci;
        private readonly DataGridView dgvServisi;
        private readonly Label lblStroski, lblStevilo, lblPrihajajoci;
        private readonly int? zacetniAvtomobil;

        public ServisiObrazec() : this(null) { }

        public ServisiObrazec(int? avtomobilId)
        {
            zacetniAvtomobil = avtomobilId;
            BackColor = Color.WhiteSmoke;
            Text = "Servisi";

            var panelOrodja = UiPomocnik.PanelOrodja();
            cmbAvto = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 280, Font = new Font("Segoe UI", 10F), Location = new Point(15, 22) };
            chkSamoPrihajajoci = new CheckBox { Text = "Samo zamujeni / prihajajoči", AutoSize = true, Font = new Font("Segoe UI", 9.75F), Location = new Point(310, 25) };
            var btnDodaj = UiPomocnik.Gumb("DODAJ SERVIS", Tema.Modra, 150);
            var btnUredi = UiPomocnik.Gumb("UREDI", Tema.Oranzna, 100);
            var btnIzbrisi = UiPomocnik.Gumb("IZBRIŠI", Tema.Rdeca, 100);
            var btnExcel = UiPomocnik.Gumb("📊 EXCEL", Tema.Zelena, 110);
            var btnOsvezi = UiPomocnik.SiviGumb("🔄");
            btnIzbrisi.Visible = Pravice.LahkoBrise;

            int x = 540;
            foreach (var gumb in new[] { btnDodaj, btnUredi, btnIzbrisi, btnExcel, btnOsvezi })
            {
                if (!gumb.Visible) continue;
                gumb.Location = new Point(x, 17);
                x += gumb.Width + 10;
            }
            panelOrodja.Controls.AddRange(new Control[] { cmbAvto, chkSamoPrihajajoci, btnDodaj, btnUredi, btnIzbrisi, btnExcel, btnOsvezi });

            var panelKartice = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 112, Padding = new Padding(15, 12, 15, 0), WrapContents = false };
            panelKartice.Controls.Add(UiPomocnik.Kartica("STROŠKI SERVISOV", out lblStroski, Tema.Rdeca, 220));
            panelKartice.Controls.Add(UiPomocnik.Kartica("ŠTEVILO SERVISOV", out lblStevilo, Tema.Modra, 200));
            panelKartice.Controls.Add(UiPomocnik.Kartica("POTREBUJE SERVIS", out lblPrihajajoci, Tema.Oranzna, 200));

            dgvServisi = new DataGridView { Dock = DockStyle.Fill };
            UiPomocnik.StilizirajTabelo(dgvServisi);
            var panelGlavno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            panelGlavno.Controls.Add(dgvServisi);

            Controls.Add(panelGlavno);
            Controls.Add(panelKartice);
            Controls.Add(panelOrodja);

            IzbiraAvtomobila.NapolniComboBox(cmbAvto, true, avtomobilId);

            cmbAvto.SelectedIndexChanged += (s, e) => NaloziServise();
            chkSamoPrihajajoci.CheckedChanged += (s, e) => NaloziServise();
            btnDodaj.Click += (s, e) =>
            {
                var avto = cmbAvto.SelectedItem as IzbiraAvtomobila;
                using (var dialog = new DodajUrediServisObrazec(avto?.Id))
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        NaloziServise();
            };
            btnUredi.Click += (s, e) => Uredi();
            dgvServisi.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Uredi(); };
            btnIzbrisi.Click += (s, e) => Izbrisi();
            btnExcel.Click += (s, e) => IzvozExcel.IzvoziTabelo(dgvServisi, "Servisi");
            btnOsvezi.Click += (s, e) => NaloziServise();
            dgvServisi.CellFormatting += DgvServisi_CellFormatting;

            NaloziServise();
        }

        private void NaloziServise()
        {
            string sql = @"
                SELECT s.id, s.avtomobil_id, s.datum AS 'Datum', (a.znamka || ' ' || a.model) AS 'Avtomobil',
                       a.registrska_st AS 'Registrska', s.vrsta AS 'Vrsta', s.km AS 'Km', s.cena AS 'Cena (€)',
                       s.naslednji_servis AS 'Naslednji servis', '' AS 'Stanje', s.opis AS 'Opis'
                FROM servisi s JOIN avtomobili a ON s.avtomobil_id = a.id
                WHERE 1=1";

            var avto = cmbAvto.SelectedItem as IzbiraAvtomobila;
            if (avto != null)
                sql += " AND s.avtomobil_id = @avto";
            if (chkSamoPrihajajoci.Checked)
            {
                // samo zadnji servis vsakega avta, ki ima naslednji servis v roku
                sql += @" AND s.id = (SELECT s2.id FROM servisi s2 WHERE s2.avtomobil_id = s.avtomobil_id ORDER BY s2.datum DESC, s2.id DESC LIMIT 1)
                          AND s.naslednji_servis IS NOT NULL AND s.naslednji_servis <= @meja";
            }
            sql += " ORDER BY s.datum DESC, s.id DESC";

            var tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    if (avto != null)
                        ukaz.Parameters.AddWithValue("@avto", avto.Id);
                    ukaz.Parameters.AddWithValue("@meja", DateTime.Today.AddDays(Nastavitve.ServisOpozoriloDni).ToString("yyyy-MM-dd"));
                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }

            // Stanje računamo samo za zadnji servis posameznega avtomobila
            var zeVideni = new System.Collections.Generic.HashSet<long>();
            foreach (DataRow vrstica in tabela.Rows)
            {
                long avtoId = Convert.ToInt64(vrstica["avtomobil_id"]);
                if (zeVideni.Add(avtoId))
                    vrstica["Stanje"] = ServisPomocnik.StatusDatuma(vrstica["Naslednji servis"]?.ToString());
            }

            dgvServisi.DataSource = tabela;
            foreach (string skrit in new[] { "id", "avtomobil_id" })
                if (dgvServisi.Columns[skrit] != null)
                    dgvServisi.Columns[skrit].Visible = false;
            if (dgvServisi.Columns["Cena (€)"] != null)
                dgvServisi.Columns["Cena (€)"].DefaultCellStyle.Format = "N2";
            if (dgvServisi.Columns["Opis"] != null)
                dgvServisi.Columns["Opis"].FillWeight = 180;

            PosodobiKartice(avto);
        }

        private void PosodobiKartice(IzbiraAvtomobila avto)
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                string pogoj = avto != null ? " WHERE avtomobil_id = @avto" : "";
                using (var ukaz = new SQLiteCommand("SELECT COALESCE(SUM(cena), 0), COUNT(*) FROM servisi" + pogoj, povezava))
                {
                    if (avto != null)
                        ukaz.Parameters.AddWithValue("@avto", avto.Id);
                    using (var b = ukaz.ExecuteReader())
                    {
                        b.Read();
                        lblStroski.Text = $"{Convert.ToDecimal(b[0]):N2} €";
                        lblStevilo.Text = b[1].ToString();
                    }
                }
            }
            lblPrihajajoci.Text = ServisPomocnik.PrihajajociServisi(Nastavitve.ServisOpozoriloDni).Rows.Count.ToString();
        }

        private void DgvServisi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvServisi.Columns[e.ColumnIndex].Name != "Stanje")
                return;

            string stanje = dgvServisi.Rows[e.RowIndex].Cells["Stanje"].Value?.ToString();
            if (stanje == "Zamujen")
            {
                e.CellStyle.ForeColor = Tema.Rdeca;
                e.CellStyle.Font = new Font(dgvServisi.Font, FontStyle.Bold);
            }
            else if (stanje == "Kmalu")
            {
                e.CellStyle.ForeColor = Tema.Oranzna;
                e.CellStyle.Font = new Font(dgvServisi.Font, FontStyle.Bold);
            }
            else if (stanje == "V redu")
            {
                e.CellStyle.ForeColor = Tema.Zelena;
            }
        }

        private Servis IzbraniServis()
        {
            if (dgvServisi.SelectedRows.Count == 0)
                return null;

            var v = dgvServisi.SelectedRows[0];
            return new Servis
            {
                Id = Convert.ToInt32(v.Cells["id"].Value),
                AvtomobilId = Convert.ToInt32(v.Cells["avtomobil_id"].Value),
                Datum = DateTime.Parse(v.Cells["Datum"].Value.ToString()),
                Vrsta = v.Cells["Vrsta"].Value?.ToString(),
                Opis = v.Cells["Opis"].Value?.ToString(),
                Km = v.Cells["Km"].Value == DBNull.Value ? (int?)null : Convert.ToInt32(v.Cells["Km"].Value),
                Cena = v.Cells["Cena (€)"].Value == DBNull.Value ? 0 : Convert.ToDecimal(v.Cells["Cena (€)"].Value),
                NaslednjiServis = DateTime.TryParse(v.Cells["Naslednji servis"].Value?.ToString(), out DateTime n) ? n : (DateTime?)null
            };
        }

        private void Uredi()
        {
            var servis = IzbraniServis();
            if (servis == null)
            {
                Sporocilo.Opozorilo("Najprej izberite servis!");
                return;
            }
            using (var dialog = new DodajUrediServisObrazec(servis))
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    NaloziServise();
        }

        private void Izbrisi()
        {
            var servis = IzbraniServis();
            if (servis == null)
            {
                Sporocilo.Opozorilo("Najprej izberite servis!");
                return;
            }
            if (!Sporocilo.Vprasaj("Ali res želite izbrisati ta zapis o servisu?"))
                return;

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("DELETE FROM servisi WHERE id = @id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", servis.Id);
                    ukaz.ExecuteNonQuery();
                }
            }
            NaloziServise();
        }
    }
}
