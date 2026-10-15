using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace CarRental.Pomocniki
{
    // Večjezičnost (slovenščina / angleščina).
    // Vsa besedila v programu so napisana v slovenščini. Ko je izbrana angleščina,
    // jih Jezik.T() poišče v slovarju (JezikSlovar.cs) in vrne angleški prevod.
    // Besedila z vrednostmi (npr. "Cena na dan: 45,00 €") prevedemo z vzorci "{0}".
    public static partial class Jezik
    {
        public const string Slovenscina = "sl";
        public const string Anglescina = "en";

        public static bool JeAnglescina => Nastavitve.JezikKoda == Anglescina;

        private static Dictionary<string, string> tocni;
        private static Dictionary<string, string> brezVelikosti;
        private static List<Vzorec> vzorci;

        private class Vzorec
        {
            public Regex Izraz;
            public string Prevod;
        }

        private class Izvirnik
        {
            public string Besedilo;
        }

        private static readonly ConditionalWeakTable<Control, Izvirnik> izvirniki = new ConditionalWeakTable<Control, Izvirnik>();
        private static readonly HashSet<Control> vTeku = new HashSet<Control>();

        private static void PripraviSlovar()
        {
            if (tocni != null)
                return;

            tocni = new Dictionary<string, string>(StringComparer.Ordinal);
            brezVelikosti = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            vzorci = new List<Vzorec>();

            foreach (var par in Slovar())
            {
                tocni[par.Key] = par.Value;
                if (!brezVelikosti.ContainsKey(par.Key))
                    brezVelikosti[par.Key] = par.Value;

                if (par.Key.Contains("{0}"))
                {
                    // "Cena na dan: {0}" -> ^Cena\ na\ dan:\ (?<p0>.*?)$
                    string izraz = Regex.Replace(Regex.Escape(par.Key), @"\\\{(\d)}", "(?<p$1>.*?)");
                    vzorci.Add(new Vzorec
                    {
                        Izraz = new Regex("^" + izraz + "$", RegexOptions.Singleline | RegexOptions.Compiled),
                        Prevod = par.Value
                    });
                }
            }

            // Daljši vzorci najprej (bolj specifični)
            vzorci = vzorci.OrderByDescending(v => v.Izraz.ToString().Length).ToList();
        }

        public static string T(string besedilo)
        {
            if (!JeAnglescina || string.IsNullOrWhiteSpace(besedilo))
                return besedilo;

            PripraviSlovar();

            if (tocni.TryGetValue(besedilo, out string prevod))
                return prevod;

            // "AKTIVNA" -> "ACTIVE"
            if (besedilo == besedilo.ToUpper() && brezVelikosti.TryGetValue(besedilo, out prevod))
                return prevod.ToUpper();

            foreach (var vzorec in vzorci)
            {
                var ujemanje = vzorec.Izraz.Match(besedilo);
                if (!ujemanje.Success)
                    continue;

                string rezultat = vzorec.Prevod;
                for (int i = 0; i < 10; i++)
                {
                    var skupina = ujemanje.Groups["p" + i];
                    if (!skupina.Success)
                        continue;

                    // Vstavljena vrednost je lahko tudi sama prevedljiva (npr. status)
                    string vrednost = skupina.Value;
                    if (tocni.TryGetValue(vrednost, out string prevodVrednosti))
                        vrednost = prevodVrednosti;
                    rezultat = rezultat.Replace("{" + i + "}", vrednost);
                }
                return rezultat;
            }

            return besedilo;
        }

        public static string T(string format, params object[] argumenti)
        {
            return string.Format(T(format), argumenti);
        }

        // Prevede obrazec in vse njegove kontrole ter poskrbi, da se prevedejo
        // tudi besedila, ki jih koda nastavi kasneje (TextChanged).
        public static void Prevedi(Control koren)
        {
            PrevediKontrolo(koren);
        }

        private static void PrevediKontrolo(Control c)
        {
            if (c is MaterialTextBox mtb)
            {
                mtb.Hint = T(mtb.Hint);
            }
            else if (c is MaterialTextBox2 mtb2)
            {
                mtb2.Hint = T(mtb2.Hint);
            }
            else if (c is ComboBox cmb)
            {
                // Vrednosti v ComboBox-u ostanejo slovenske (uporabljajo se v SQL),
                // prevede se samo prikaz
                if (!izvirniki.TryGetValue(c, out _))
                {
                    izvirniki.Add(c, new Izvirnik());
                    cmb.FormattingEnabled = true;
                    cmb.Format += (s, e) =>
                    {
                        if (e.ListItem is string niz)
                            e.Value = T(niz);
                    };
                }
            }
            else if (c is DataGridView dgv)
            {
                PripraviTabelo(dgv);
            }
            else if (!(c is TextBoxBase) && !(c is NumericUpDown) && !(c is DateTimePicker))
            {
                PrevediBesedilo(c);
            }

            if (c is ToolStrip orodna)
                PrevediElemente(orodna.Items);
            if (c.ContextMenuStrip != null)
                PrevediElemente(c.ContextMenuStrip.Items);

            foreach (Control otrok in c.Controls)
                PrevediKontrolo(otrok);
        }

        private static void PrevediBesedilo(Control c)
        {
            if (!izvirniki.TryGetValue(c, out Izvirnik izvirnik))
            {
                izvirnik = new Izvirnik { Besedilo = c.Text };
                izvirniki.Add(c, izvirnik);

                c.TextChanged += (s, e) =>
                {
                    if (vTeku.Contains(c))
                        return;
                    izvirnik.Besedilo = c.Text;
                    NastaviBesedilo(c, T(c.Text));
                };

                c.ControlAdded += (s, e) => PrevediKontrolo(e.Control);
            }

            NastaviBesedilo(c, T(izvirnik.Besedilo));
        }

        private static void NastaviBesedilo(Control c, string besedilo)
        {
            if (c.Text == besedilo)
                return;

            vTeku.Add(c);
            try
            {
                c.Text = besedilo;
            }
            finally
            {
                vTeku.Remove(c);
            }
        }

        private static void PrevediElemente(ToolStripItemCollection elementi)
        {
            foreach (ToolStripItem element in elementi)
            {
                if (element.Tag == null)
                    element.Tag = element.Text;
                element.Text = T(element.Tag as string ?? element.Text);

                if (element is ToolStripDropDownItem spustni)
                    PrevediElemente(spustni.DropDownItems);
            }
        }

        // Statusi in podobne vrednosti v tabelah (prosto, aktivna, kartica ...)
        private static readonly HashSet<string> vrednostiVTabelah = new HashSet<string>
        {
            "prosto", "rezervirano", "servis",
            "aktivna", "zakljucena", "preklicana",
            "gotovina", "kartica", "nakazilo",
            "placano", "v obdelavi", "neplacano",
            "admin", "agent",
            "odprta", "popravljena", "manjsa", "srednja", "huda",
            "Da", "Ne", "Zamujen", "Kmalu", "V redu",
            "Brez", "Bronasta", "Srebrna", "Zlata"
        };

        private static void PripraviTabelo(DataGridView dgv)
        {
            if (izvirniki.TryGetValue(dgv, out _))
                return;
            izvirniki.Add(dgv, new Izvirnik());

            dgv.CellFormatting += (s, e) =>
            {
                if (JeAnglescina && e.Value is string niz && vrednostiVTabelah.Contains(niz))
                {
                    e.Value = T(niz);
                    e.FormattingApplied = true;
                }
            };

            // Glave stolpcev nastavi koda šele po nalaganju podatkov,
            // zato jih prevedemo tik pred izrisom
            dgv.CellPainting += (s, e) =>
            {
                if (!JeAnglescina || e.RowIndex != -1 || e.ColumnIndex < 0)
                    return;

                var stolpec = dgv.Columns[e.ColumnIndex];
                string prevod = T(stolpec.HeaderText);
                if (prevod != stolpec.HeaderText && dgv.IsHandleCreated && !dgv.IsDisposed)
                {
                    dgv.BeginInvoke(new Action(() =>
                    {
                        if (!stolpec.DataGridView?.IsDisposed ?? false)
                            stolpec.HeaderText = prevod;
                    }));
                }
            };
        }

        // Ob preklopu jezika ponovno prevedemo kontrole, ki ostanejo odprte (glavno okno)
        public static void PonovnoPrevedi(Control koren)
        {
            if (izvirniki.TryGetValue(koren, out Izvirnik izvirnik) && izvirnik.Besedilo != null)
                NastaviBesedilo(koren, T(izvirnik.Besedilo));
            else if (!(koren is TextBoxBase) && !(koren is ComboBox) && !(koren is DataGridView))
                PrevediKontrolo(koren);

            foreach (Control otrok in koren.Controls)
                PonovnoPrevedi(otrok);
        }
    }
}
