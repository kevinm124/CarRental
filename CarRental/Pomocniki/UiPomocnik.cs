using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarRental.Pomocniki
{
    // Pomožne metode za obrazce, ki jih zgradimo v kodi (enak videz kot obstoječi obrazci)
    public static class UiPomocnik
    {
        public static void StilizirajTabelo(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.RowTemplate.Height = 34;
            dgv.Font = new Font("Segoe UI", 9.75F);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 33, 33);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 253);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private static readonly Regex isoDatum = new Regex(@"^\d{4}-\d{2}-\d{2}( \d{2}:\d{2}(:\d{2})?)?$");
        private static readonly ConditionalWeakTable<DataGridView, object> pripravljene = new ConditionalWeakTable<DataGridView, object>();

        // SQLite hrani datume kot besedilo "2026-05-14" - v tabelah jih prikažemo kot "14.5.2026"
        public static void PripraviTabele(Control koren)
        {
            if (koren is DataGridView dgv && !pripravljene.TryGetValue(dgv, out _))
            {
                pripravljene.Add(dgv, null);
                dgv.CellFormatting += (s, e) =>
                {
                    if (e.Value is string niz && isoDatum.IsMatch(niz) &&
                        DateTime.TryParse(niz, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datum))
                    {
                        e.Value = niz.Length > 10 ? datum.ToString("d.M.yyyy HH:mm") : datum.ToString("d.M.yyyy");
                        e.FormattingApplied = true;
                    }
                };
            }

            foreach (Control otrok in koren.Controls)
                PripraviTabele(otrok);
        }

        public static Button Gumb(string besedilo, Color barva, int sirina = 150, int visina = 38)
        {
            var gumb = new Button
            {
                Text = besedilo,
                BackColor = barva,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold),
                Size = new Size(sirina, visina),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            gumb.FlatAppearance.BorderSize = 0;
            return gumb;
        }

        // Sivi gumb (npr. osveži, prekliči)
        public static Button SiviGumb(string besedilo, int sirina = 60, int visina = 38)
        {
            var gumb = Gumb(besedilo, Color.WhiteSmoke, sirina, visina);
            gumb.ForeColor = Color.FromArgb(33, 33, 33);
            gumb.FlatAppearance.BorderSize = 1;
            gumb.FlatAppearance.BorderColor = Color.Gainsboro;
            return gumb;
        }

        public static Label Oznaka(string besedilo, float velikost = 9.75F, FontStyle slog = FontStyle.Regular, Color? barva = null)
        {
            return new Label
            {
                Text = besedilo,
                AutoSize = true,
                Font = new Font("Segoe UI", velikost, slog),
                ForeColor = barva ?? Color.FromArgb(33, 33, 33),
                BackColor = Color.Transparent
            };
        }

        // Majhen siv naslov nad poljem (enako kot "STRANKA", "EMAIL" ...)
        public static Label NaslovPolja(string besedilo)
        {
            return Oznaka(besedilo, 8.25F, FontStyle.Bold, Color.FromArgb(120, 120, 120));
        }

        public static Panel PanelOrodja(int visina = 70)
        {
            return new Panel
            {
                Dock = DockStyle.Top,
                Height = visina,
                BackColor = Color.White,
                Padding = new Padding(15, 0, 15, 0)
            };
        }

        // Kartica s statistiko (enak videz kot na nadzorni plošči)
        public static Panel Kartica(string naslov, out Label lblVrednost, Color barva, int sirina = 220, int visina = 90)
        {
            var kartica = new Panel { Size = new Size(sirina, visina), BackColor = Color.White, Margin = new Padding(0, 0, 12, 0) };
            var crta = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = barva };
            var lblNaslov = NaslovPolja(naslov);
            lblNaslov.Location = new Point(18, 14);
            lblVrednost = Oznaka("0", 18F, FontStyle.Bold);
            lblVrednost.Location = new Point(15, 36);

            kartica.Controls.Add(lblVrednost);
            kartica.Controls.Add(lblNaslov);
            kartica.Controls.Add(crta);
            return kartica;
        }

        public static void PonudiOdpiranje(string pot)
        {
            var odgovor = Sporocilo.Vprasaj(
                "Datoteka je bila shranjena:\n{0}\n\nAli jo želite odpreti?", pot);
            if (odgovor)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(pot) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Sporocilo.Napaka("Datoteke ni bilo mogoče odpreti: {0}", ex.Message);
                }
            }
        }
    }
}
