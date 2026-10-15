using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public partial class GlavniObrazec : MaterialForm
    {
        private Button trenutniAktivenGumb;
        private Func<Form> tvorecTrenutneStrani;
        private string naslovTrenutneStrani;
        private Form trenutnaStran;
        private List<Button> navigacijskiGumbi;

        public GlavniObrazec()
        {
            InitializeComponent();

            // Onemogoči Material drawer da nam ne pokvari layouta
            this.Sizable = true;

            // Prikaži uporabnika
            if (PrijavaObrazec.TrenutniUporabnik != null)
            {
                lblUporabnik.Text = $"👤 {PrijavaObrazec.TrenutniUporabnik.Ime} {PrijavaObrazec.TrenutniUporabnik.Priimek} ({PrijavaObrazec.TrenutniUporabnik.Vloga}) ▾";
            }

            // Prikaži datum
            lblStatusDatum.Text = DateTime.Now.ToString("d.M.yyyy HH:mm");

            // Posodavljanje datuma vsako sekundo (timer)
            Timer timerCas = new Timer();
            timerCas.Interval = 1000;
            timerCas.Tick += (s, e) => lblStatusDatum.Text = DateTime.Now.ToString("d.M.yyyy HH:mm");
            timerCas.Start();

            UrediNavigacijo();
            PostaviGumbeVrstice();
            lblUporabnik.SizeChanged += (s, e) => PostaviGumbeVrstice();
            panelVrh.Resize += (s, e) => PostaviGumbeVrstice();

            // Tema in jezik
            Tema.Uporabi(this);
            Jezik.Prevedi(this);
            OsveziGumbaTemeInJezika();

            // Privzeto je aktiven Dashboard
            trenutniAktivenGumb = btnDashboard;
            OdpriStran(btnDashboard, "Nadzorna plošča", () => new DashboardObrazec());
        }

        // Gumbi v stranski vrstici - vrstni red in pravice
        private void UrediNavigacijo()
        {
            navigacijskiGumbi = new List<Button>
            {
                btnDashboard, btnAvtomobili, btnStranke, btnRezervacije, btnKoledar,
                btnPlacila, btnServisi, btnPoskodbe, btnPorocila, btnUporabniki, btnNastavitve
            };

            // Samo administrator vidi uporabnike in nastavitve
            // (Visible pred prikazom okna vedno vrne false, zato preverimo pravice neposredno)
            var skriti = new HashSet<Button>();
            if (!Pravice.LahkoUrejaUporabnike) skriti.Add(btnUporabniki);
            if (!Pravice.LahkoUrejaNastavitve) skriti.Add(btnNastavitve);

            int y = 80;
            foreach (var gumb in navigacijskiGumbi)
            {
                gumb.TextAlign = ContentAlignment.MiddleLeft;
                gumb.Height = 44;
                gumb.Visible = !skriti.Contains(gumb);
                if (skriti.Contains(gumb))
                    continue;
                gumb.Top = y;
                y += 44;
            }
        }

        private void PostaviGumbeVrstice()
        {
            lblUporabnik.Left = panelVrh.Width - lblUporabnik.Width - 20;
            btnJezik.Left = lblUporabnik.Left - btnJezik.Width - 15;
            btnTema.Left = btnJezik.Left - btnTema.Width - 6;
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
            // Prejšnjo stran zapremo, da ne ostaja v pomnilniku
            if (trenutnaStran != null)
            {
                panelVsebina.Controls.Remove(trenutnaStran);
                trenutnaStran.Dispose();
            }
            panelVsebina.Controls.Clear();

            obrazec.TopLevel = false;
            obrazec.FormBorderStyle = FormBorderStyle.None;
            obrazec.Dock = DockStyle.Fill;
            panelVsebina.Controls.Add(obrazec);
            obrazec.Show();
            trenutnaStran = obrazec;
        }

        private void OdpriStran(Button gumb, string naslov, Func<Form> tvorec)
        {
            tvorecTrenutneStrani = tvorec;
            naslovTrenutneStrani = naslov;
            OznaciAktivenGumb(gumb, naslov);
            OdpriObrazec(tvorec());
        }

        // Po spremembi teme ali jezika trenutno stran ustvarimo na novo
        private void OsveziTrenutnoStran()
        {
            if (tvorecTrenutneStrani != null)
                OdpriStran(trenutniAktivenGumb, naslovTrenutneStrani, tvorecTrenutneStrani);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OdpriStran(btnDashboard, "Nadzorna plošča", () => new DashboardObrazec());
        }

        private void btnAvtomobili_Click(object sender, EventArgs e)
        {
            OdpriStran(btnAvtomobili, "Avtomobili", () => new AvtomobiliObrazec());
        }

        private void btnStranke_Click(object sender, EventArgs e)
        {
            OdpriStran(btnStranke, "Stranke", () => new StrankeObrazec());
        }

        private void btnRezervacije_Click(object sender, EventArgs e)
        {
            OdpriStran(btnRezervacije, "Rezervacije", () => new RezervacijeObrazec());
        }

        private void btnKoledar_Click(object sender, EventArgs e)
        {
            OdpriStran(btnKoledar, "Koledar rezervacij", () => new KoledarObrazec());
        }

        private void btnPlacila_Click(object sender, EventArgs e)
        {
            OdpriStran(btnPlacila, "Plačila", () => new PlacilaObrazec());
        }

        private void btnServisi_Click(object sender, EventArgs e)
        {
            OdpriStran(btnServisi, "Servisi in vzdrževanje", () => new ServisiObrazec());
        }

        private void btnPoskodbe_Click(object sender, EventArgs e)
        {
            OdpriStran(btnPoskodbe, "Poškodbe vozil", () => new PoskodbeObrazec());
        }

        private void btnPorocila_Click(object sender, EventArgs e)
        {
            OdpriStran(btnPorocila, "Poročila", () => new PorocilaObrazec());
        }

        private void btnUporabniki_Click(object sender, EventArgs e)
        {
            OdpriStran(btnUporabniki, "Uporabniki", () => new UporabnikiObrazec());
        }

        private void btnNastavitve_Click(object sender, EventArgs e)
        {
            OdpriStran(btnNastavitve, "Nastavitve", () => new NastavitveObrazec());
        }

        private void btnTema_Click(object sender, EventArgs e)
        {
            Nastavitve.TemnaTema = !Nastavitve.TemnaTema;
            Tema.NastaviMaterialSkin();
            Tema.Uporabi(this);
            OsveziGumbaTemeInJezika();
            OsveziTrenutnoStran();
        }

        private void btnJezik_Click(object sender, EventArgs e)
        {
            Nastavitve.JezikKoda = Jezik.JeAnglescina ? Jezik.Slovenscina : Jezik.Anglescina;
            Jezik.PonovnoPrevedi(this);
            OsveziGumbaTemeInJezika();
            OsveziTrenutnoStran();
        }

        private void OsveziGumbaTemeInJezika()
        {
            btnTema.Text = Tema.JeTemna ? "☀" : "🌙";
            btnJezik.Text = Jezik.JeAnglescina ? "SL" : "EN";
        }

        private void lblUporabnik_Click(object sender, EventArgs e)
        {
            var meni = new ContextMenuStrip();
            meni.Items.Add(Jezik.T("Spremeni geslo"), null, (s, a) =>
            {
                using (var dialog = new SpremeniGesloObrazec())
                    dialog.ShowDialog(this);
            });
            meni.Items.Add(new ToolStripSeparator());
            meni.Items.Add(Jezik.T("Odjava"), null, (s, a) => btnOdjava.PerformClick());
            meni.Show(lblUporabnik, new Point(0, lblUporabnik.Height));
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
