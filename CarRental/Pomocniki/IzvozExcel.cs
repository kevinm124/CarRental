using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace CarRental.Pomocniki
{
    // Izvoz tabel v Excel (.xlsx) s knjižnico ClosedXML
    public static class IzvozExcel
    {
        private static readonly Regex datumVzorec = new Regex(@"^\d{4}-\d{2}-\d{2}( \d{2}:\d{2}(:\d{2})?)?$");

        // Izvozi tisto, kar uporabnik vidi v tabeli (vidni stolpci, trenutni filtri)
        public static void IzvoziTabelo(DataGridView dgv, string imeLista)
        {
            var tabela = IzTabele(dgv, imeLista);
            IzvoziZDialogom(new List<DataTable> { tabela }, imeLista);
        }

        public static void IzvoziZDialogom(List<DataTable> tabele, string privzetoIme)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel (*.xlsx)|*.xlsx";
                dialog.FileName = $"{Jezik.T(privzetoIme)}_{DateTime.Now:yyyy-MM-dd}.xlsx";
                dialog.Title = Jezik.T("Izvoz v Excel");

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    Shrani(tabele, dialog.FileName);
                    UiPomocnik.PonudiOdpiranje(dialog.FileName);
                }
                catch (Exception ex)
                {
                    Sporocilo.Napaka("Napaka pri izvozu: {0}", ex.Message);
                }
            }
        }

        // DataGridView -> DataTable z vidnimi stolpci in (prevedenimi) naslovi
        public static DataTable IzTabele(DataGridView dgv, string ime)
        {
            var stolpci = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            var tabela = new DataTable(Jezik.T(ime));
            var imena = new HashSet<string>();
            foreach (var stolpec in stolpci)
            {
                string naslov = Jezik.T(stolpec.HeaderText);
                while (!imena.Add(naslov))
                    naslov += " ";
                tabela.Columns.Add(naslov, typeof(object));
            }

            foreach (DataGridViewRow vrstica in dgv.Rows)
            {
                if (vrstica.IsNewRow)
                    continue;

                var vrednosti = stolpci.Select(c =>
                {
                    object v = vrstica.Cells[c.Index].Value;
                    // statusi (aktivna, prosto ...) se izvozijo v izbranem jeziku
                    return v is string niz ? (object)Jezik.T(niz) : v;
                }).ToArray();
                tabela.Rows.Add(vrednosti);
            }
            return tabela;
        }

        public static void Shrani(List<DataTable> tabele, string pot)
        {
            using (var delovniZvezek = new XLWorkbook())
            {
                var uporabljenaImena = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var tabela in tabele)
                {
                    string imeLista = VarnoImeLista(tabela.TableName, uporabljenaImena);
                    var list = delovniZvezek.Worksheets.Add(imeLista);

                    for (int c = 0; c < tabela.Columns.Count; c++)
                        list.Cell(1, c + 1).Value = tabela.Columns[c].ColumnName.Trim();

                    for (int r = 0; r < tabela.Rows.Count; r++)
                    {
                        for (int c = 0; c < tabela.Columns.Count; c++)
                            NastaviCelico(list.Cell(r + 2, c + 1), tabela.Rows[r][c]);
                    }

                    var glava = list.Range(1, 1, 1, Math.Max(1, tabela.Columns.Count));
                    glava.Style.Font.Bold = true;
                    glava.Style.Font.FontColor = XLColor.White;
                    glava.Style.Fill.BackgroundColor = XLColor.FromArgb(33, 33, 33);

                    if (tabela.Columns.Count > 0)
                    {
                        list.Range(1, 1, Math.Max(1, tabela.Rows.Count + 1), tabela.Columns.Count).SetAutoFilter();
                        list.SheetView.FreezeRows(1);
                        list.Columns().AdjustToContents(8.0, 60.0);
                    }
                }

                if (tabele.Count == 0)
                    delovniZvezek.Worksheets.Add("List1");

                delovniZvezek.SaveAs(pot);
            }
        }

        private static void NastaviCelico(IXLCell celica, object vrednost)
        {
            switch (vrednost)
            {
                case null:
                case DBNull _:
                    return;
                case DateTime datum:
                    celica.Value = datum;
                    celica.Style.DateFormat.Format = datum.TimeOfDay == TimeSpan.Zero ? "d.m.yyyy" : "d.m.yyyy hh:mm";
                    return;
                case decimal d:
                    celica.Value = (double)d;
                    celica.Style.NumberFormat.Format = "#,##0.00";
                    return;
                case double d:
                    celica.Value = d;
                    celica.Style.NumberFormat.Format = "#,##0.00";
                    return;
                case float f:
                    celica.Value = f;
                    celica.Style.NumberFormat.Format = "#,##0.00";
                    return;
                case int i:
                    celica.Value = i;
                    return;
                case long l:
                    celica.Value = l;
                    return;
                case bool b:
                    celica.Value = Jezik.T(b ? "Da" : "Ne");
                    return;
            }

            string niz = vrednost.ToString();
            // SQLite hrani datume kot besedilo (yyyy-MM-dd)
            if (datumVzorec.IsMatch(niz) &&
                DateTime.TryParse(niz, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                NastaviCelico(celica, dt);
                return;
            }
            celica.Value = niz;
        }

        private static string VarnoImeLista(string ime, HashSet<string> uporabljena)
        {
            if (string.IsNullOrWhiteSpace(ime))
                ime = "List";
            ime = Regex.Replace(ime, @"[\[\]\*\?/\\:]", "");
            if (ime.Length > 28)
                ime = ime.Substring(0, 28);

            string rezultat = ime;
            int i = 2;
            while (!uporabljena.Add(rezultat))
                rezultat = $"{ime} {i++}";
            return rezultat;
        }
    }
}
