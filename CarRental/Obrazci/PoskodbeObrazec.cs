using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Poškodbe vozil s fotografijami
    public class PoskodbeObrazec : OsnovniObrazec
    {
        private readonly ComboBox cmbAvto;
        private readonly ComboBox cmbStatus;
        private readonly DataGridView dgvPoskodbe;
        private readonly Label lblNaslov, lblPodrobnosti, lblOpis, lblResnost;
        private readonly FlowLayoutPanel panelSlike;
        private readonly Button btnPopravljeno;
        private Poskodba izbrana;

        public PoskodbeObrazec() : this(null) { }

        public PoskodbeObrazec(int? avtomobilId)
        {
            BackColor = Color.WhiteSmoke;
            Text = "Poškodbe";

            var panelOrodja = UiPomocnik.PanelOrodja();
            cmbAvto = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 280, Font = new Font("Segoe UI", 10F), Location = new Point(15, 22) };
            cmbStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150, Font = new Font("Segoe UI", 10F), Location = new Point(310, 22) };
            cmbStatus.Items.AddRange(new object[] { "Vsi statusi", "odprta", "popravljena" });
            var btnDodaj = UiPomocnik.Gumb("DODAJ POŠKODBO", Tema.Modra, 170);
            var btnExcel = UiPomocnik.Gumb("📊 EXCEL", Tema.Zelena, 110);
            var btnOsvezi = UiPomocnik.SiviGumb("🔄");
            btnDodaj.Location = new Point(480, 17);
            btnExcel.Location = new Point(660, 17);
            btnOsvezi.Location = new Point(780, 17);
            panelOrodja.Controls.AddRange(new Control[] { cmbAvto, cmbStatus, btnDodaj, btnExcel, btnOsvezi });

            dgvPoskodbe = new DataGridView { Dock = DockStyle.Fill };
            UiPomocnik.StilizirajTabelo(dgvPoskodbe);

            // Podrobnosti desno
            var panelDetajli = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20), AutoScroll = true };
            var lblDetajliNaslov = UiPomocnik.Oznaka("DETAJLI POŠKODBE", 11.25F, FontStyle.Bold, Color.FromArgb(120, 120, 120));
            lblDetajliNaslov.Location = new Point(20, 15);
            lblNaslov = UiPomocnik.Oznaka("Izberite poškodbo", 14F, FontStyle.Bold);
            lblNaslov.AutoSize = false;
            lblNaslov.Size = new Size(300, 28);
            lblNaslov.Location = new Point(20, 45);
            lblResnost = new Label { Location = new Point(20, 80), AutoSize = true, ForeColor = Color.White, Padding = new Padding(8, 4, 8, 4), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            lblPodrobnosti = UiPomocnik.Oznaka("", 9.75F);
            lblPodrobnosti.AutoSize = false;
            lblPodrobnosti.Size = new Size(300, 90);
            lblPodrobnosti.Location = new Point(20, 115);
            lblOpis = UiPomocnik.Oznaka("", 9.75F, FontStyle.Italic, Color.FromArgb(80, 80, 80));
            lblOpis.AutoSize = false;
            lblOpis.Size = new Size(300, 80);
            lblOpis.Location = new Point(20, 210);
            panelSlike = new FlowLayoutPanel { Location = new Point(20, 295), Size = new Size(300, 180), AutoScroll = true };

            var btnUredi = UiPomocnik.Gumb("UREDI", Tema.Oranzna, 95, 40);
            btnPopravljeno = UiPomocnik.Gumb("POPRAVLJENO", Tema.Zelena, 120, 40);
            var btnIzbrisi = UiPomocnik.Gumb("IZBRIŠI", Tema.Rdeca, 85, 40);
            btnUredi.Location = new Point(20, 490);
            btnPopravljeno.Location = new Point(120, 490);
            btnIzbrisi.Location = new Point(245, 490);
            btnIzbrisi.Visible = Pravice.LahkoBrise;
            panelDetajli.Controls.AddRange(new Control[] { lblDetajliNaslov, lblNaslov, lblResnost, lblPodrobnosti, lblOpis, panelSlike, btnUredi, btnPopravljeno, btnIzbrisi });

            var razdelitev = new SplitContainer { Dock = DockStyle.Fill, FixedPanel = FixedPanel.Panel2 };
            razdelitev.Panel1.Controls.Add(dgvPoskodbe);
            razdelitev.Panel2.Controls.Add(panelDetajli);

            var panelGlavno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            panelGlavno.Controls.Add(razdelitev);

            Controls.Add(panelGlavno);
            Controls.Add(panelOrodja);

            // Detajli imajo fiksno širino ~350 px (nastavimo, ko je znana velikost)
            bool razdeljeno = false;
            razdelitev.SizeChanged += (s, e) =>
            {
                if (!razdeljeno && razdelitev.Width > 700)
                {
                    razdelitev.SplitterDistance = razdelitev.Width - 360;
                    razdeljeno = true;
                }
            };

            IzbiraAvtomobila.NapolniComboBox(cmbAvto, true, avtomobilId);
            // Za izbran avto prikažemo vse poškodbe, sicer samo odprte
            cmbStatus.SelectedIndex = avtomobilId.HasValue ? 0 : 1;

            cmbAvto.SelectedIndexChanged += (s, e) => NaloziPoskodbe();
            cmbStatus.SelectedIndexChanged += (s, e) => NaloziPoskodbe();
            dgvPoskodbe.SelectionChanged += (s, e) => PrikaziIzbrano();
            dgvPoskodbe.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Uredi(); };
            btnDodaj.Click += (s, e) =>
            {
                var avto = cmbAvto.SelectedItem as IzbiraAvtomobila;
                using (var dialog = new DodajUrediPoskodboObrazec(avto?.Id))
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        NaloziPoskodbe();
            };
            btnExcel.Click += (s, e) => IzvozExcel.IzvoziTabelo(dgvPoskodbe, "Poskodbe");
            btnOsvezi.Click += (s, e) => NaloziPoskodbe();
            btnUredi.Click += (s, e) => Uredi();
            btnPopravljeno.Click += (s, e) => OznaciPopravljeno();
            btnIzbrisi.Click += (s, e) => Izbrisi();

            NaloziPoskodbe();
        }

        private void NaloziPoskodbe()
        {
            string sql = @"
                SELECT p.id, p.avtomobil_id, p.rezervacija_id, p.datum AS 'Datum',
                       (a.znamka || ' ' || a.model || ' (' || a.registrska_st || ')') AS 'Avtomobil',
                       p.resnost AS 'Resnost', p.ocenjeni_strosek AS 'Strošek (€)', p.status AS 'Status',
                       (s.ime || ' ' || s.priimek) AS 'Stranka', p.opis AS 'Opis',
                       (SELECT COUNT(*) FROM poskodbe_slike ps WHERE ps.poskodba_id = p.id) AS 'Slike'
                FROM poskodbe p
                JOIN avtomobili a ON p.avtomobil_id = a.id
                LEFT JOIN rezervacije r ON p.rezervacija_id = r.id
                LEFT JOIN stranke s ON r.stranka_id = s.id
                WHERE 1=1";

            var avto = cmbAvto.SelectedItem as IzbiraAvtomobila;
            if (avto != null)
                sql += " AND p.avtomobil_id = @avto";
            if (cmbStatus.SelectedIndex > 0)
                sql += " AND p.status = @status";
            sql += " ORDER BY p.datum DESC, p.id DESC";

            var tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    if (avto != null)
                        ukaz.Parameters.AddWithValue("@avto", avto.Id);
                    if (cmbStatus.SelectedIndex > 0)
                        ukaz.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }

            dgvPoskodbe.DataSource = tabela;
            foreach (string skrit in new[] { "id", "avtomobil_id", "rezervacija_id", "Opis" })
                if (dgvPoskodbe.Columns[skrit] != null)
                    dgvPoskodbe.Columns[skrit].Visible = false;
            if (dgvPoskodbe.Columns["Strošek (€)"] != null)
                dgvPoskodbe.Columns["Strošek (€)"].DefaultCellStyle.Format = "N2";
            if (dgvPoskodbe.Columns["Avtomobil"] != null)
                dgvPoskodbe.Columns["Avtomobil"].FillWeight = 180;

            PrikaziIzbrano();
        }

        private Poskodba PreberiIzbrano()
        {
            if (dgvPoskodbe.SelectedRows.Count == 0)
                return null;

            var v = dgvPoskodbe.SelectedRows[0];
            var p = new Poskodba
            {
                Id = Convert.ToInt32(v.Cells["id"].Value),
                AvtomobilId = Convert.ToInt32(v.Cells["avtomobil_id"].Value),
                RezervacijaId = v.Cells["rezervacija_id"].Value == DBNull.Value ? (int?)null : Convert.ToInt32(v.Cells["rezervacija_id"].Value),
                Datum = DateTime.Parse(v.Cells["Datum"].Value.ToString()),
                Opis = v.Cells["Opis"].Value?.ToString(),
                Resnost = v.Cells["Resnost"].Value?.ToString(),
                OcenjeniStrosek = Convert.ToDecimal(v.Cells["Strošek (€)"].Value),
                Status = v.Cells["Status"].Value?.ToString()
            };

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("SELECT pot_slike FROM poskodbe_slike WHERE poskodba_id = @id ORDER BY id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", p.Id);
                    using (var b = ukaz.ExecuteReader())
                        while (b.Read())
                            p.Slike.Add(b["pot_slike"].ToString());
                }
            }
            return p;
        }

        private void PrikaziIzbrano()
        {
            izbrana = PreberiIzbrano();

            foreach (Control c in panelSlike.Controls)
                (c as PictureBox)?.Image?.Dispose();
            panelSlike.Controls.Clear();

            if (izbrana == null)
            {
                lblNaslov.Text = "Izberite poškodbo";
                lblResnost.Visible = false;
                lblPodrobnosti.Text = "";
                lblOpis.Text = "";
                btnPopravljeno.Enabled = false;
                return;
            }

            var v = dgvPoskodbe.SelectedRows[0];
            lblNaslov.Text = $"#{izbrana.Id} · {izbrana.Datum:d.M.yyyy}";
            lblResnost.Visible = true;
            lblResnost.Text = Jezik.T(izbrana.Resnost).ToUpper();
            lblResnost.BackColor = izbrana.Resnost == "huda" ? Tema.Rdeca : izbrana.Resnost == "srednja" ? Tema.Oranzna : Tema.Modra;
            lblPodrobnosti.Text =
                $"{v.Cells["Avtomobil"].Value}\n" +
                $"{Jezik.T("Stranka")}: {(v.Cells["Stranka"].Value == DBNull.Value ? "-" : v.Cells["Stranka"].Value)}\n" +
                $"{Jezik.T("Ocenjeni strošek")}: {izbrana.OcenjeniStrosek:N2} €\n" +
                $"{Jezik.T("Status")}: {Jezik.T(izbrana.Status)}";
            lblOpis.Text = izbrana.Opis;
            btnPopravljeno.Enabled = izbrana.Status == "odprta";

            foreach (string pot in izbrana.Slike)
            {
                string p = pot;
                var slicica = new PictureBox
                {
                    Size = new Size(92, 68),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = SlikePomocnik.Slicica(p, 92, 68),
                    Margin = new Padding(0, 0, 6, 6),
                    Cursor = Cursors.Hand
                };
                slicica.Click += (s, e) =>
                {
                    using (var pregled = new PregledSlikeObrazec(p))
                        pregled.ShowDialog(this);
                };
                panelSlike.Controls.Add(slicica);
            }
        }

        private void Uredi()
        {
            if (izbrana == null)
            {
                Sporocilo.Opozorilo("Najprej izberite poškodbo!");
                return;
            }
            using (var dialog = new DodajUrediPoskodboObrazec(izbrana))
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    NaloziPoskodbe();
        }

        private void OznaciPopravljeno()
        {
            if (izbrana == null)
                return;
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("UPDATE poskodbe SET status = 'popravljena' WHERE id = @id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", izbrana.Id);
                    ukaz.ExecuteNonQuery();
                }
            }
            NaloziPoskodbe();
        }

        private void Izbrisi()
        {
            if (izbrana == null)
            {
                Sporocilo.Opozorilo("Najprej izberite poškodbo!");
                return;
            }
            if (!Sporocilo.Vprasaj("Ali res želite izbrisati to poškodbo in njene fotografije?"))
                return;

            var slike = new List<string>(izbrana.Slike);
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand("DELETE FROM poskodbe WHERE id = @id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@id", izbrana.Id);
                    ukaz.ExecuteNonQuery();
                }
            }

            foreach (Control c in panelSlike.Controls)
                (c as PictureBox)?.Image?.Dispose();
            panelSlike.Controls.Clear();
            foreach (string pot in slike)
                SlikePomocnik.IzbrisiDatoteko(pot);
            NaloziPoskodbe();
        }
    }
}
