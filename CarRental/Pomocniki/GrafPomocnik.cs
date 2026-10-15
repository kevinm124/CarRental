using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CarRental.Pomocniki
{
    // Chart vrže "Height must be greater than 0px", če se nariše, ko še nima prostora
    // (npr. v zavihku, ki še ni bil prikazan, ali v premajhnem oknu). Takrat risanje preskočimo.
    public class VarenGraf : Chart
    {
        public VarenGraf()
        {
            MinimumSize = new Size(120, 120);
        }

        // Chart.OnResize vrže izjemo, če je višina ali širina med postavitvijo 0
        protected override void OnResize(EventArgs e)
        {
            if (Width <= 0 || Height <= 0)
                return;
            try
            {
                base.OnResize(e);
            }
            catch (ArgumentException)
            {
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ClientSize.Width < 80 || ClientSize.Height < 80)
            {
                e.Graphics.Clear(BackColor);
                return;
            }
            try
            {
                base.OnPaint(e);
            }
            catch (ArgumentException)
            {
                e.Graphics.Clear(BackColor);
            }
        }
    }

    // Grafi z vgrajeno kontrolo Chart (System.Windows.Forms.DataVisualization)
    public static class GrafPomocnik
    {
        public static readonly Color[] Paleta =
        {
            Color.FromArgb(33, 150, 243), Color.FromArgb(76, 175, 80), Color.FromArgb(255, 152, 0),
            Color.FromArgb(211, 47, 47), Color.FromArgb(126, 87, 194), Color.FromArgb(0, 150, 136),
            Color.FromArgb(233, 30, 99), Color.FromArgb(121, 85, 72)
        };

        public static Chart Ustvari(string naslov)
        {
            var graf = new VarenGraf { Dock = DockStyle.Fill, BackColor = Color.White };
            var obmocje = new ChartArea("glavno") { BackColor = Color.White };
            obmocje.AxisX.MajorGrid.Enabled = false;
            obmocje.AxisX.Interval = 1;
            obmocje.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            obmocje.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            obmocje.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            obmocje.AxisX.IsMarginVisible = true;
            obmocje.AxisY.Minimum = 0;
            graf.ChartAreas.Add(obmocje);

            if (!string.IsNullOrEmpty(naslov))
            {
                graf.Titles.Add(new Title(Jezik.T(naslov))
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Alignment = ContentAlignment.TopLeft
                });
            }

            Tema.PreslikajGraf(graf);
            return graf;
        }

        public static Series Stolpci(Chart graf, string ime, IList<KeyValuePair<string, double>> tocke, Color barva, string oblika = "N2")
        {
            var serija = new Series(Jezik.T(ime))
            {
                ChartType = SeriesChartType.Column,
                Color = barva,
                IsValueShownAsLabel = tocke.Count <= 14,
                LabelFormat = oblika,
                Font = new Font("Segoe UI", 7.5F),
                LabelForeColor = Tema.Besedilo,
                ToolTip = "#VALX: #VAL{" + oblika + "}"
            };
            serija["PointWidth"] = "0.6";
            foreach (var t in tocke)
                serija.Points.AddXY(t.Key, t.Value);
            graf.Series.Add(serija);
            return serija;
        }

        public static Series Crta(Chart graf, string ime, IList<KeyValuePair<string, double>> tocke, Color barva)
        {
            var serija = new Series(Jezik.T(ime))
            {
                ChartType = SeriesChartType.Line,
                Color = barva,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                ToolTip = "#VALX: #VAL"
            };
            foreach (var t in tocke)
                serija.Points.AddXY(t.Key, t.Value);
            graf.Series.Add(serija);
            return serija;
        }

        public static Series Torta(Chart graf, string ime, IList<KeyValuePair<string, double>> tocke)
        {
            var serija = new Series(Jezik.T(ime))
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                Label = "#PERCENT{P0}",
                LegendText = "#VALX",
                LabelForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ToolTip = "#VALX: #VAL{N2} (#PERCENT{P1})"
            };
            for (int i = 0; i < tocke.Count; i++)
            {
                int indeks = serija.Points.AddXY(Jezik.T(tocke[i].Key), tocke[i].Value);
                serija.Points[indeks].Color = Paleta[i % Paleta.Length];
            }
            graf.Series.Add(serija);

            var legenda = new Legend { Docking = Docking.Right, Font = new Font("Segoe UI", 9F) };
            graf.Legends.Add(legenda);
            Tema.PreslikajGraf(graf);
            return serija;
        }

        // Zadnjih N mesecev kot ključi "yyyy-MM" (vključno s trenutnim)
        public static List<DateTime> ZadnjiMeseci(int stevilo)
        {
            var meseci = new List<DateTime>();
            var zacetek = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-(stevilo - 1));
            for (int i = 0; i < stevilo; i++)
                meseci.Add(zacetek.AddMonths(i));
            return meseci;
        }

        public static string OznakaMeseca(DateTime mesec)
        {
            var kultura = Jezik.JeAnglescina ? CultureInfo.GetCultureInfo("en-GB") : CultureInfo.GetCultureInfo("sl-SI");
            return mesec.ToString("MMM yy", kultura);
        }
    }
}
