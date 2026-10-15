using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MaterialSkin;

namespace CarRental.Pomocniki
{
    // Svetla / temna tema.
    // Obrazci so narisani v svetlih barvah, zato pri temni temi vsako "svetlo" barvo
    // zamenjamo z ustrezno temno (in obratno, ko preklopimo nazaj na svetlo).
    // Barvni poudarki (modra, zelena, rdeča, oranžna) ostanejo enaki.
    public static class Tema
    {
        public static bool JeTemna => Nastavitve.TemnaTema;

        // svetla barva ozadja -> temna barva ozadja
        private static readonly Dictionary<int, Color> ozadjeVTemno = new Dictionary<int, Color>
        {
            { Color.White.ToArgb(),                 Color.FromArgb(45, 45, 48) },
            { Color.WhiteSmoke.ToArgb(),            Color.FromArgb(30, 30, 30) },
            { Color.FromArgb(250, 250, 250).ToArgb(), Color.FromArgb(40, 40, 43) },
            { Color.FromArgb(240, 240, 240).ToArgb(), Color.FromArgb(37, 37, 40) },
            { Color.FromArgb(245, 250, 255).ToArgb(), Color.FromArgb(38, 44, 52) },
            { Color.Gainsboro.ToArgb(),             Color.FromArgb(70, 70, 73) },
            { Color.FromArgb(232, 244, 253).ToArgb(), Color.FromArgb(40, 70, 100) },
            { Color.FromArgb(230, 230, 230).ToArgb(), Color.FromArgb(60, 60, 63) },
            { Color.FromArgb(200, 200, 200).ToArgb(), Color.FromArgb(90, 90, 93) },
        };

        // temna barva besedila -> svetla barva besedila
        private static readonly Dictionary<int, Color> besediloVTemno = new Dictionary<int, Color>
        {
            { Color.FromArgb(33, 33, 33).ToArgb(),   Color.FromArgb(235, 235, 235) },
            { Color.FromArgb(80, 80, 80).ToArgb(),   Color.FromArgb(205, 205, 205) },
            { Color.FromArgb(120, 120, 120).ToArgb(), Color.FromArgb(165, 165, 165) },
            { Color.Black.ToArgb(),                  Color.FromArgb(230, 230, 230) },
        };

        private static readonly Dictionary<int, Color> ozadjeVSvetlo =
            ozadjeVTemno.ToDictionary(p => p.Value.ToArgb(), p => Color.FromArgb(p.Key));
        private static readonly Dictionary<int, Color> besediloVSvetlo =
            besediloVTemno.ToDictionary(p => p.Value.ToArgb(), p => Color.FromArgb(p.Key));

        private static readonly ConditionalWeakTable<Control, object> prijavljeni = new ConditionalWeakTable<Control, object>();

        // Barve za kontrole, ki jih ustvarimo v kodi
        public static Color Ozadje => JeTemna ? Color.FromArgb(30, 30, 30) : Color.WhiteSmoke;
        public static Color Povrsina => JeTemna ? Color.FromArgb(45, 45, 48) : Color.White;
        public static Color Besedilo => JeTemna ? Color.FromArgb(235, 235, 235) : Color.FromArgb(33, 33, 33);
        public static Color BesediloSivo => JeTemna ? Color.FromArgb(165, 165, 165) : Color.FromArgb(120, 120, 120);
        public static Color Crta => JeTemna ? Color.FromArgb(70, 70, 73) : Color.Gainsboro;

        public static readonly Color Modra = Color.FromArgb(33, 150, 243);
        public static readonly Color Zelena = Color.FromArgb(76, 175, 80);
        public static readonly Color Rdeca = Color.FromArgb(211, 47, 47);
        public static readonly Color Oranzna = Color.FromArgb(255, 152, 0);
        public static readonly Color Vijolicna = Color.FromArgb(126, 87, 194);
        public static readonly Color Temna = Color.FromArgb(33, 33, 33);

        public static void NastaviMaterialSkin()
        {
            MaterialSkinManager.Instance.Theme = JeTemna
                ? MaterialSkinManager.Themes.DARK
                : MaterialSkinManager.Themes.LIGHT;
        }

        public static void Uporabi(Control koren)
        {
            Preslikaj(koren, JeTemna);
        }

        private static void Preslikaj(Control c, bool temna)
        {
            var ozadje = temna ? ozadjeVTemno : ozadjeVSvetlo;
            var besedilo = temna ? besediloVTemno : besediloVSvetlo;

            if (c.BackColor != Color.Transparent && ozadje.TryGetValue(c.BackColor.ToArgb(), out Color novoOzadje))
                c.BackColor = novoOzadje;
            if (besedilo.TryGetValue(c.ForeColor.ToArgb(), out Color novoBesedilo))
                c.ForeColor = novoBesedilo;

            if (c is Button gumb && gumb.FlatStyle == FlatStyle.Flat &&
                ozadje.TryGetValue(gumb.FlatAppearance.BorderColor.ToArgb(), out Color novaObroba))
                gumb.FlatAppearance.BorderColor = novaObroba;

            if (c is DataGridView dgv)
                PreslikajTabelo(dgv, ozadje, besedilo);

            if (c is Chart graf)
                PreslikajGraf(graf);

            foreach (Control otrok in c.Controls)
                Preslikaj(otrok, temna);

            // Kontrole, ki se dodajo kasneje (npr. sličice v galeriji), dobijo isto temo
            if (!prijavljeni.TryGetValue(c, out _))
            {
                prijavljeni.Add(c, null);
                c.ControlAdded += (s, e) => Preslikaj(e.Control, JeTemna);
            }
        }

        private static void PreslikajTabelo(DataGridView dgv, Dictionary<int, Color> ozadje, Dictionary<int, Color> besedilo)
        {
            if (ozadje.TryGetValue(dgv.BackgroundColor.ToArgb(), out Color b))
                dgv.BackgroundColor = b;
            if (ozadje.TryGetValue(dgv.GridColor.ToArgb(), out Color g))
                dgv.GridColor = g;

            PreslikajStil(dgv.DefaultCellStyle, ozadje, besedilo);
            PreslikajStil(dgv.RowsDefaultCellStyle, ozadje, besedilo);
            PreslikajStil(dgv.AlternatingRowsDefaultCellStyle, ozadje, besedilo);

            // Tabela brez nastavljenih barv uporablja sistemske (bela/črna)
            if (dgv.DefaultCellStyle.BackColor.IsEmpty)
                dgv.DefaultCellStyle.BackColor = Povrsina;
            if (dgv.DefaultCellStyle.ForeColor.IsEmpty)
                dgv.DefaultCellStyle.ForeColor = Besedilo;
        }

        private static void PreslikajStil(DataGridViewCellStyle stil, Dictionary<int, Color> ozadje, Dictionary<int, Color> besedilo)
        {
            if (!stil.BackColor.IsEmpty && ozadje.TryGetValue(stil.BackColor.ToArgb(), out Color b))
                stil.BackColor = b;
            if (!stil.ForeColor.IsEmpty && besedilo.TryGetValue(stil.ForeColor.ToArgb(), out Color f))
                stil.ForeColor = f;
            if (!stil.SelectionBackColor.IsEmpty && ozadje.TryGetValue(stil.SelectionBackColor.ToArgb(), out Color sb))
                stil.SelectionBackColor = sb;
            if (!stil.SelectionForeColor.IsEmpty && besedilo.TryGetValue(stil.SelectionForeColor.ToArgb(), out Color sf))
                stil.SelectionForeColor = sf;
        }

        public static void PreslikajGraf(Chart graf)
        {
            graf.BackColor = Povrsina;
            foreach (var obmocje in graf.ChartAreas)
            {
                obmocje.BackColor = Povrsina;
                foreach (var os in new[] { obmocje.AxisX, obmocje.AxisY })
                {
                    os.LabelStyle.ForeColor = Besedilo;
                    os.LineColor = Crta;
                    os.MajorGrid.LineColor = Crta;
                    os.TitleForeColor = Besedilo;
                }
            }
            foreach (var legenda in graf.Legends)
            {
                legenda.BackColor = Povrsina;
                legenda.ForeColor = Besedilo;
            }
            foreach (var naslov in graf.Titles)
                naslov.ForeColor = Besedilo;
        }
    }
}
