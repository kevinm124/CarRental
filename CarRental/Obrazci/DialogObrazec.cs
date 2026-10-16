using System;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Osnova za nove dialoge (enak videz kot DodajUrediStrankoObrazec):
    // moder naslov zgoraj, polja na sredini, gumba Shrani / Prekliči spodaj.
    public class DialogObrazec : OsnovniObrazec
    {
        protected Panel panelNaslov;
        protected Label lblNaslovDialoga;
        protected Panel panelGlavno;
        protected Panel panelGumbi;
        protected Button btnShrani;
        protected Button btnPreklici;

        public DialogObrazec() : this("", 540, 500) { }

        public DialogObrazec(string naslov, int sirina, int visina)
        {
            Text = naslov;
            ClientSize = new Size(sirina, visina);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F);

            panelNaslov = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Tema.Modra };
            lblNaslovDialoga = new Label
            {
                Text = naslov,
                AutoSize = true,
                Font = new Font("Segoe UI", 14.25F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 15)
            };
            panelNaslov.Controls.Add(lblNaslovDialoga);

            panelGumbi = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.WhiteSmoke };
            btnShrani = UiPomocnik.Gumb("SHRANI", Tema.Modra, 150, 40);
            btnPreklici = UiPomocnik.SiviGumb("PREKLIČI", 130, 40);
            btnShrani.Top = 15;
            btnPreklici.Top = 15;
            // Gumba postavimo ob vsaki spremembi velikosti - širina panela ob ustvarjanju
            // še ni končna (sidranje desno bi ju sicer potisnilo izven okna)
            panelGumbi.Layout += (s, e) => PostaviGumba();
            btnShrani.Click += (s, e) => Shrani();
            btnPreklici.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            panelGumbi.Controls.Add(btnShrani);
            panelGumbi.Controls.Add(btnPreklici);

            panelGlavno = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 20, 30, 20),
                AutoScroll = true,
                BackColor = Color.White
            };

            Controls.Add(panelGlavno);
            Controls.Add(panelGumbi);
            Controls.Add(panelNaslov);

            AcceptButton = null;
            CancelButton = btnPreklici;
        }

        private void PostaviGumba()
        {
            btnShrani.Left = panelGumbi.ClientSize.Width - btnShrani.Width - 20;
            btnPreklici.Left = btnShrani.Left - btnPreklici.Width - 10;
        }

        // Podrazredi tu preverijo polja in shranijo podatke
        protected virtual void Shrani()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        protected Label DodajNaslovPolja(string besedilo, int x, int y, int sirina = 220)
        {
            var lbl = new Label
            {
                Text = besedilo,
                Location = new Point(x, y),
                Size = new Size(sirina, 18),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(80, 80, 80),
                BackColor = Color.Transparent
            };
            panelGlavno.Controls.Add(lbl);
            return lbl;
        }

        // Doda naslov in kontrolo pod njim, vrne kontrolo
        protected T DodajPolje<T>(string naslov, T kontrola, int x, int y, int sirina = 220) where T : Control
        {
            DodajNaslovPolja(naslov, x, y, sirina);
            kontrola.Location = new Point(x, y + 22);
            kontrola.Width = sirina;
            kontrola.Font = new Font("Segoe UI", 9.75F);
            if (kontrola is TextBox txt)
                txt.BorderStyle = BorderStyle.FixedSingle;
            if (kontrola is ComboBox cmb && cmb.DropDownStyle != ComboBoxStyle.DropDown)
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            panelGlavno.Controls.Add(kontrola);
            return kontrola;
        }

        protected static NumericUpDown Stevilo(decimal min, decimal max, int decimalke = 0, decimal korak = 1)
        {
            return new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                DecimalPlaces = decimalke,
                Increment = korak,
                ThousandsSeparator = true
            };
        }

        protected static DateTimePicker Datum()
        {
            return new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "d.M.yyyy" };
        }
    }
}
