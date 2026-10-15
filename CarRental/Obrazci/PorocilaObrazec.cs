using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CarRental.Baza;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Napredna poročila za poljubno časovno obdobje
    public class PorocilaObrazec : OsnovniObrazec
    {
        private readonly ComboBox cmbObdobje;
        private readonly DateTimePicker dtpOd;
        private readonly DateTimePicker dtpDo;
        private readonly Label lblPrihodki, lblRezervacije, lblPovprecje, lblDni, lblServisi, lblNeto;
        private readonly TabControl zavihki;

        // Tabele poročila (za prikaz in izvoz)
        private readonly List<DataTable> tabele = new List<DataTable>();

        private static readonly string[] obdobja =
        {
            "Ta mesec", "Prejšnji mesec", "Zadnjih 30 dni", "Zadnje četrtletje", "Letos", "Lani", "Vse", "Po meri"
        };

        public PorocilaObrazec()
        {
            BackColor = Color.WhiteSmoke;
            Text = "Poročila";

            // Orodna vrstica
            var panelOrodja = UiPomocnik.PanelOrodja();
            cmbObdobje = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 170, Font = new Font("Segoe UI", 10F), Location = new Point(15, 22) };
            cmbObdobje.Items.AddRange(obdobja);
            dtpOd = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "d.M.yyyy", Width = 120, Font = new Font("Segoe UI", 10F), Location = new Point(200, 22) };
            var lblCrtica = UiPomocnik.Oznaka("–", 12F);
            lblCrtica.Location = new Point(325, 22);
            dtpDo = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "d.M.yyyy", Width = 120, Font = new Font("Segoe UI", 10F), Location = new Point(345, 22) };
            var btnPrikazi = UiPomocnik.Gumb("PRIKAŽI", Tema.Modra, 110);
            btnPrikazi.Location = new Point(480, 17);
            var btnExcel = UiPomocnik.Gumb("📊 EXCEL", Tema.Zelena, 110);
            btnExcel.Location = new Point(600, 17);
            var btnPdf = UiPomocnik.Gumb("📄 PDF", Tema.Rdeca, 100);
            btnPdf.Location = new Point(720, 17);
            panelOrodja.Controls.AddRange(new Control[] { cmbObdobje, dtpOd, lblCrtica, dtpDo, btnPrikazi, btnExcel, btnPdf });

            // Kartice s povzetkom
            var panelKartice = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 115, Padding = new Padding(15, 12, 15, 0), WrapContents = false, AutoScroll = true };
            panelKartice.Controls.Add(UiPomocnik.Kartica("PRIHODKI (PLAČILA)", out lblPrihodki, Tema.Modra, 190));
            panelKartice.Controls.Add(UiPomocnik.Kartica("REZERVACIJE", out lblRezervacije, Tema.Zelena, 160));
            panelKartice.Controls.Add(UiPomocnik.Kartica("POVPREČNA REZERVACIJA", out lblPovprecje, Tema.Oranzna, 190));
            panelKartice.Controls.Add(UiPomocnik.Kartica("DNI NAJEMA", out lblDni, Tema.Vijolicna, 150));
            panelKartice.Controls.Add(UiPomocnik.Kartica("STROŠKI SERVISOV", out lblServisi, Tema.Rdeca, 180));
            panelKartice.Controls.Add(UiPomocnik.Kartica("NETO (PRIHODKI − SERVISI)", out lblNeto, Tema.Temna, 200));

            zavihki = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), Padding = new Point(14, 5) };
            var panelGlavno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            panelGlavno.Controls.Add(zavihki);

            Controls.Add(panelGlavno);
            Controls.Add(panelKartice);
            Controls.Add(panelOrodja);

            cmbObdobje.SelectedIndexChanged += (s, e) =>
            {
                NastaviObdobje();
                if (cmbObdobje.SelectedIndex != obdobja.Length - 1)
                    Osvezi();
            };
            dtpOd.ValueChanged += (s, e) => { if (dtpOd.Focused) cmbObdobje.SelectedIndex = obdobja.Length - 1; };
            dtpDo.ValueChanged += (s, e) => { if (dtpDo.Focused) cmbObdobje.SelectedIndex = obdobja.Length - 1; };
            btnPrikazi.Click += (s, e) => Osvezi();
            btnExcel.Click += (s, e) => IzvozExcel.IzvoziZDialogom(tabele, "Porocilo");
            btnPdf.Click += (s, e) => IzvoziPdf();

            cmbObdobje.SelectedIndex = 0;
        }

        private void NastaviObdobje()
        {
            DateTime danes = DateTime.Today;
            DateTime zacetekMeseca = new DateTime(danes.Year, danes.Month, 1);
            switch (cmbObdobje.SelectedIndex)
            {
                case 0: dtpOd.Value = zacetekMeseca; dtpDo.Value = zacetekMeseca.AddMonths(1).AddDays(-1); break;
                case 1: dtpOd.Value = zacetekMeseca.AddMonths(-1); dtpDo.Value = zacetekMeseca.AddDays(-1); break;
                case 2: dtpOd.Value = danes.AddDays(-29); dtpDo.Value = danes; break;
                case 3: dtpOd.Value = zacetekMeseca.AddMonths(-2); dtpDo.Value = zacetekMeseca.AddMonths(1).AddDays(-1); break;
                case 4: dtpOd.Value = new DateTime(danes.Year, 1, 1); dtpDo.Value = new DateTime(danes.Year, 12, 31); break;
                case 5: dtpOd.Value = new DateTime(danes.Year - 1, 1, 1); dtpDo.Value = new DateTime(danes.Year - 1, 12, 31); break;
                case 6: dtpOd.Value = new DateTime(2000, 1, 1); dtpDo.Value = danes.AddYears(5); break;
            }
        }

        private string Od => dtpOd.Value.ToString("yyyy-MM-dd");
        private string Do => dtpDo.Value.ToString("yyyy-MM-dd");
        private string OpisObdobja => $"{dtpOd.Value:d.M.yyyy} – {dtpDo.Value:d.M.yyyy}";

        private DataTable Poizvedba(string sql, string ime)
        {
            var tabela = new DataTable(Jezik.T(ime));
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@od", Od);
                    ukaz.Parameters.AddWithValue("@do", Do);
                    using (var adapter = new SQLiteDataAdapter(ukaz))
                        adapter.Fill(tabela);
                }
            }
            // Prevedeni naslovi stolpcev
            foreach (DataColumn stolpec in tabela.Columns)
                stolpec.ColumnName = Jezik.T(stolpec.ColumnName);
            return tabela;
        }

        private object Skalar(string sql)
        {
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(sql, povezava))
                {
                    ukaz.Parameters.AddWithValue("@od", Od);
                    ukaz.Parameters.AddWithValue("@do", Do);
                    return ukaz.ExecuteScalar();
                }
            }
        }

        private void Osvezi()
        {
            if (dtpDo.Value.Date < dtpOd.Value.Date)
            {
                Sporocilo.Opozorilo("Datum 'do' ne sme biti pred datumom 'od'!");
                return;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                OsveziPovzetek();
                OsveziTabele();
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri pripravi poročila: {0}", ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private decimal prihodki, servisi, povprecje;
        private int steviloRezervacij, dni;

        private void OsveziPovzetek()
        {
            prihodki = Convert.ToDecimal(Skalar(
                "SELECT COALESCE(SUM(znesek), 0) FROM placila WHERE status = 'placano' AND substr(datum_placila, 1, 10) BETWEEN @od AND @do"));
            steviloRezervacij = Convert.ToInt32(Skalar(
                "SELECT COUNT(*) FROM rezervacije WHERE status <> 'preklicana' AND datum_od BETWEEN @od AND @do"));
            povprecje = Convert.ToDecimal(Skalar(
                "SELECT COALESCE(AVG(skupna_cena), 0) FROM rezervacije WHERE status <> 'preklicana' AND datum_od BETWEEN @od AND @do"));
            dni = Convert.ToInt32(Skalar(
                "SELECT COALESCE(SUM(julianday(datum_do) - julianday(datum_od) + 1), 0) FROM rezervacije WHERE status <> 'preklicana' AND datum_od BETWEEN @od AND @do"));
            servisi = Convert.ToDecimal(Skalar(
                "SELECT COALESCE(SUM(cena), 0) FROM servisi WHERE datum BETWEEN @od AND @do"));

            lblPrihodki.Text = $"{prihodki:N2} €";
            lblRezervacije.Text = steviloRezervacij.ToString();
            lblPovprecje.Text = $"{povprecje:N2} €";
            lblDni.Text = dni.ToString();
            lblServisi.Text = $"{servisi:N2} €";
            lblNeto.Text = $"{prihodki - servisi:N2} €";
            lblNeto.ForeColor = prihodki - servisi < 0 ? Tema.Rdeca : Tema.Besedilo;
        }

        private void OsveziTabele()
        {
            tabele.Clear();
            zavihki.TabPages.Clear();

            // 1) Po mesecih
            var poMesecih = Poizvedba(@"
                SELECT m.mesec AS 'Mesec',
                       (SELECT COUNT(*) FROM rezervacije r WHERE r.status <> 'preklicana' AND substr(r.datum_od, 1, 7) = m.mesec
                            AND r.datum_od BETWEEN @od AND @do) AS 'Rezervacije',
                       (SELECT COALESCE(SUM(p.znesek), 0) FROM placila p WHERE p.status = 'placano' AND substr(p.datum_placila, 1, 7) = m.mesec
                            AND substr(p.datum_placila, 1, 10) BETWEEN @od AND @do) AS 'Prihodki (€)',
                       (SELECT COALESCE(SUM(s.cena), 0) FROM servisi s WHERE substr(s.datum, 1, 7) = m.mesec
                            AND s.datum BETWEEN @od AND @do) AS 'Servisi (€)'
                FROM (SELECT substr(datum_od, 1, 7) AS mesec FROM rezervacije WHERE datum_od BETWEEN @od AND @do
                      UNION SELECT substr(datum_placila, 1, 7) FROM placila WHERE substr(datum_placila, 1, 10) BETWEEN @od AND @do
                      UNION SELECT substr(datum, 1, 7) FROM servisi WHERE datum BETWEEN @od AND @do) m
                ORDER BY m.mesec", "Po mesecih");
            var grafMeseci = GrafPomocnik.Ustvari("");
            GrafPomocnik.Stolpci(grafMeseci, "Prihodki", Tocke(poMesecih, 0, 2), Tema.Modra, "N0");
            GrafPomocnik.Stolpci(grafMeseci, "Servisi", Tocke(poMesecih, 0, 3), Tema.Rdeca, "N0");
            grafMeseci.Legends.Add(new Legend { Docking = Docking.Top, Font = new Font("Segoe UI", 9F) });
            Tema.PreslikajGraf(grafMeseci);
            DodajZavihek("Po mesecih", poMesecih, grafMeseci);

            // 2) Po avtomobilih (z zasedenostjo v obdobju)
            var poAvtih = Poizvedba(@"
                SELECT (a.znamka || ' ' || a.model) AS 'Avtomobil', a.registrska_st AS 'Registrska',
                       COUNT(r.id) AS 'Rezervacije',
                       CAST(COALESCE(SUM(julianday(r.datum_do) - julianday(r.datum_od) + 1), 0) AS INTEGER) AS 'Dni',
                       COALESCE(SUM(r.skupna_cena), 0) AS 'Vrednost (€)'
                FROM avtomobili a
                LEFT JOIN rezervacije r ON r.avtomobil_id = a.id AND r.status <> 'preklicana' AND r.datum_od BETWEEN @od AND @do
                GROUP BY a.id ORDER BY 5 DESC", "Po avtomobilih");
            DodajZasedenost(poAvtih);
            var grafAvti = GrafPomocnik.Ustvari("");
            GrafPomocnik.Stolpci(grafAvti, "Vrednost (€)", Tocke(poAvtih, 0, 4, 10), Tema.Zelena, "N0");
            DodajZavihek("Po avtomobilih", poAvtih, grafAvti);

            // 3) Po strankah
            var poStrankah = Poizvedba(@"
                SELECT (s.ime || ' ' || s.priimek) AS 'Stranka', s.email AS 'Email',
                       COUNT(r.id) AS 'Rezervacije', COALESCE(SUM(r.skupna_cena), 0) AS 'Vrednost (€)',
                       (SELECT COUNT(*) FROM rezervacije z WHERE z.stranka_id = s.id AND z.status = 'zakljucena') AS 'Vseh zaključenih'
                FROM stranke s
                JOIN rezervacije r ON r.stranka_id = s.id AND r.status <> 'preklicana' AND r.datum_od BETWEEN @od AND @do
                GROUP BY s.id ORDER BY 4 DESC", "Po strankah");
            DodajStopnjoZvestobe(poStrankah);
            DodajZavihek("Po strankah", poStrankah, null);

            // 4) Po kategorijah
            var poKategorijah = Poizvedba(@"
                SELECT COALESCE(k.naziv, '-') AS 'Kategorija', COUNT(r.id) AS 'Rezervacije',
                       COALESCE(SUM(r.skupna_cena), 0) AS 'Vrednost (€)'
                FROM rezervacije r
                JOIN avtomobili a ON r.avtomobil_id = a.id
                LEFT JOIN kategorije k ON a.kategorija_id = k.id
                WHERE r.status <> 'preklicana' AND r.datum_od BETWEEN @od AND @do
                GROUP BY k.id ORDER BY 3 DESC", "Po kategorijah");
            var grafKategorije = GrafPomocnik.Ustvari("");
            GrafPomocnik.Torta(grafKategorije, "Vrednost (€)", Tocke(poKategorijah, 0, 2));
            DodajZavihek("Po kategorijah", poKategorijah, grafKategorije);

            // 5) Načini plačila
            var poNacinih = Poizvedba(@"
                SELECT COALESCE(nacin_placila, '-') AS 'Način plačila', COUNT(*) AS 'Plačila',
                       COALESCE(SUM(znesek), 0) AS 'Znesek (€)'
                FROM placila
                WHERE status = 'placano' AND substr(datum_placila, 1, 10) BETWEEN @od AND @do
                GROUP BY nacin_placila ORDER BY 3 DESC", "Načini plačila");
            var grafNacini = GrafPomocnik.Ustvari("");
            GrafPomocnik.Torta(grafNacini, "Znesek (€)", Tocke(poNacinih, 0, 2));
            DodajZavihek("Načini plačila", poNacinih, grafNacini);
        }

        // Zasedenost = dni najema, ki padejo v obdobje / dni v obdobju
        private void DodajZasedenost(DataTable tabela)
        {
            var stolpec = tabela.Columns.Add(Jezik.T("Zasedenost (%)"), typeof(double));
            double dniObdobja = Math.Max(1, (Math.Min(dtpDo.Value.Date.Ticks, DateTime.Today.AddYears(1).Ticks) - dtpOd.Value.Date.Ticks) / TimeSpan.TicksPerDay + 1);
            int indeksDni = 3;
            foreach (DataRow vrstica in tabela.Rows)
            {
                double dniNajema = vrstica[indeksDni] == DBNull.Value ? 0 : Convert.ToDouble(vrstica[indeksDni]);
                vrstica[stolpec] = Math.Round(Math.Min(100, dniNajema / dniObdobja * 100), 1);
            }
        }

        private void DodajStopnjoZvestobe(DataTable tabela)
        {
            var stolpec = tabela.Columns.Add(Jezik.T("Zvestoba"), typeof(string));
            foreach (DataRow vrstica in tabela.Rows)
            {
                var stopnja = Zvestoba.StopnjaZaStevilo(Convert.ToInt32(vrstica[4]));
                vrstica[stolpec] = Jezik.T(stopnja?.Naziv ?? "Brez");
            }
        }

        private static List<KeyValuePair<string, double>> Tocke(DataTable tabela, int stolpecOznake, int stolpecVrednosti, int najvec = 100)
        {
            return tabela.Rows.Cast<DataRow>()
                .Take(najvec)
                .Select(r => new KeyValuePair<string, double>(
                    Jezik.T(r[stolpecOznake]?.ToString() ?? ""),
                    r[stolpecVrednosti] == DBNull.Value ? 0 : Convert.ToDouble(r[stolpecVrednosti])))
                .ToList();
        }

        private void DodajZavihek(string naslov, DataTable tabela, Chart graf)
        {
            tabele.Add(tabela);

            var zavihek = new TabPage(Jezik.T(naslov)) { BackColor = Color.White, Padding = new Padding(10) };
            var dgv = new DataGridView { Dock = DockStyle.Fill };
            UiPomocnik.StilizirajTabelo(dgv);
            dgv.DataSource = tabela;
            foreach (DataGridViewColumn stolpec in dgv.Columns)
            {
                if (stolpec.ValueType == typeof(double) || stolpec.ValueType == typeof(decimal))
                {
                    stolpec.DefaultCellStyle.Format = "N2";
                    stolpec.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (graf != null)
            {
                var razdelitev = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical };
                razdelitev.Panel1.Controls.Add(dgv);
                razdelitev.Panel2.Controls.Add(graf);
                razdelitev.Panel2.Padding = new Padding(10, 0, 0, 0);
                zavihek.Controls.Add(razdelitev);
                bool razdeljeno = false;
                zavihek.Layout += (s, e) =>
                {
                    if (!razdeljeno && razdelitev.Width > 400 && razdelitev.Height > 150)
                    {
                        razdeljeno = true;
                        razdelitev.SplitterDistance = razdelitev.Width / 2;
                    }
                };
            }
            else
            {
                zavihek.Controls.Add(dgv);
            }

            zavihki.TabPages.Add(zavihek);
            Tema.Uporabi(zavihek);
            Jezik.Prevedi(zavihek);
            UiPomocnik.PripraviTabele(zavihek);
        }

        private void IzvoziPdf()
        {
            var povzetek = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Jezik.T("Prihodki (plačila)"), $"{prihodki:N2} €"),
                new KeyValuePair<string, string>(Jezik.T("Število rezervacij"), steviloRezervacij.ToString()),
                new KeyValuePair<string, string>(Jezik.T("Povprečna rezervacija"), $"{povprecje:N2} €"),
                new KeyValuePair<string, string>(Jezik.T("Dni najema"), dni.ToString()),
                new KeyValuePair<string, string>(Jezik.T("Stroški servisov"), $"{servisi:N2} €"),
                new KeyValuePair<string, string>(Jezik.T("Neto (prihodki − servisi)"), $"{prihodki - servisi:N2} €"),
            };

            PdfDokumenti.ShraniZDialogom($"{Jezik.T("Porocilo")}_{DateTime.Now:yyyy-MM-dd}",
                pot => PdfDokumenti.UstvariPorocilo(Jezik.T("Poslovno poročilo"), OpisObdobja, povzetek, tabele, pot));
        }
    }
}
