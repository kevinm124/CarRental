using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using CarRental.Obrazci;

namespace CarRental.Obrazci
{
    public partial class GlavniObrazec : MaterialForm
    {
        private Button trenutniAktivenGumb;

        public GlavniObrazec()
        {
            InitializeComponent();

            // Onemogoči Material drawer da nam ne pokvari layouta
            this.Sizable = true;

            // Prikaži uporabnika
            if (PrijavaObrazec.TrenutniUporabnik != null)
            {
                lblUporabnik.Text = $"👤 {PrijavaObrazec.TrenutniUporabnik.Ime} {PrijavaObrazec.TrenutniUporabnik.Priimek}";
            }

            // Prikaži datum
            lblStatusDatum.Text = DateTime.Now.ToString("d.M.yyyy HH:mm");

            // Posodavljanje datuma vsako sekundo (timer)
            Timer timerCas = new Timer();
            timerCas.Interval = 1000;
            timerCas.Tick += (s, e) => lblStatusDatum.Text = DateTime.Now.ToString("d.M.yyyy HH:mm");
            timerCas.Start();

            // Privzeto je aktiven Dashboard
            trenutniAktivenGumb = btnDashboard;
            OznaciAktivenGumb(btnDashboard, "Nadzorna plošča");
            OdpriObrazec(new DashboardObrazec());
        }

        // Pomocna metoda za označevanje aktivnega gumba
        private void OznaciAktivenGumb(Button gumb, string naslov)
        {
            if (trenutniAktivenGumb != null)
            {
                trenutniAktivenGumb.BackColor = Color.FromArgb(33, 33, 33);
                trenutniAktivenGumb.ForeColor = Color.FromArgb(200, 200, 200);
            }

            gumb.BackColor = Color.FromArgb(33, 150, 243);
            gumb.ForeColor = Color.White;
            trenutniAktivenGumb = gumb;

            lblNaslovStrani.Text = naslov;
        }

        // Pomocna metoda za odpiranje pod-obrazcev v panelVsebina
        private void OdpriObrazec(Form obrazec)
        {
            panelVsebina.Controls.Clear();
            obrazec.TopLevel = false;
            obrazec.FormBorderStyle = FormBorderStyle.None;
            obrazec.Dock = DockStyle.Fill;
            panelVsebina.Controls.Add(obrazec);
            obrazec.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OznaciAktivenGumb(btnDashboard, "Nadzorna plošča");
            OdpriObrazec(new DashboardObrazec());
        }

        private void btnAvtomobili_Click(object sender, EventArgs e)
        {
            OznaciAktivenGumb(btnAvtomobili, "Avtomobili");
            OdpriObrazec(new AvtomobiliObrazec());
        }

        private void btnStranke_Click(object sender, EventArgs e)
        {
            OznaciAktivenGumb(btnStranke, "Stranke");
            OdpriObrazec(new StrankeObrazec());
        }

        private void btnRezervacije_Click(object sender, EventArgs e)
        {
            OznaciAktivenGumb(btnRezervacije, "Rezervacije");
            OdpriObrazec(new RezervacijeObrazec());
        }

        private void btnPlacila_Click(object sender, EventArgs e)
        {
            OznaciAktivenGumb(btnPlacila, "Plačila");
            OdpriObrazec(new PlacilaObrazec());
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ali se res želite odjaviti?",
                "Odjava",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                PrijavaObrazec prijava = new PrijavaObrazec();
                prijava.FormClosed += (s, args) => Application.Exit();
                prijava.Show();
            }
        }

       
    }
}