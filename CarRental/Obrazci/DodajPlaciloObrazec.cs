using System;
using System.Data.SQLite;
using System.Windows.Forms;
using CarRental.Baza;

namespace CarRental.Obrazci
{
    public partial class DodajPlaciloObrazec : OsnovniObrazec
    {
        // Razred za hranjenje rezervacije v ComboBox-u
        private class RezervacijaItem
        {
            public int Id { get; set; }
            public string Stranka { get; set; }
            public string Avto { get; set; }
            public decimal SkupnaCena { get; set; }
            public string PrikaznoIme { get; set; }
            public override string ToString() => PrikaznoIme;
        }

        public DodajPlaciloObrazec()
        {
            InitializeComponent();
            NaloziRezervacije();
            NaloziNacine();
            NaloziStatuse();

            cmbRezervacija.SelectedIndexChanged += CmbRezervacija_SelectedIndexChanged;
        }

        private void NaloziRezervacije()
        {
            cmbRezervacija.Items.Clear();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                // Naloži aktivne rezervacije ki še nimajo plačila
                string sql = @"
                    SELECT r.id, r.skupna_cena,
                           (s.ime || ' ' || s.priimek) AS stranka,
                           (a.znamka || ' ' || a.model) AS avto
                    FROM rezervacije r
                    LEFT JOIN stranke s ON r.stranka_id = s.id
                    LEFT JOIN avtomobili a ON r.avtomobil_id = a.id
                    WHERE r.id NOT IN (SELECT rezervacija_id FROM placila)
                    ORDER BY r.id DESC";

                using (var ukaz = new SQLiteCommand(sql, povezava))
                using (var bralec = ukaz.ExecuteReader())
                {
                    while (bralec.Read())
                    {
                        int id = Convert.ToInt32(bralec["id"]);
                        string stranka = bralec["stranka"].ToString();
                        string avto = bralec["avto"].ToString();
                        decimal cena = Convert.ToDecimal(bralec["skupna_cena"]);

                        cmbRezervacija.Items.Add(new RezervacijaItem
                        {
                            Id = id,
                            Stranka = stranka,
                            Avto = avto,
                            SkupnaCena = cena,
                            PrikaznoIme = $"#{id} - {stranka} - {avto} ({cena:F2} €)"
                        });
                    }
                }
            }

            if (cmbRezervacija.Items.Count == 0)
            {
                MessageBox.Show("Vse rezervacije so že plačane ali ni nobene rezervacije!",
                    "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NaloziNacine()
        {
            cmbNacin.Items.Clear();
            cmbNacin.Items.Add("gotovina");
            cmbNacin.Items.Add("kartica");
            cmbNacin.Items.Add("nakazilo");
            cmbNacin.SelectedIndex = 0;
        }

        private void NaloziStatuse()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("placano");
            cmbStatus.Items.Add("v obdelavi");
            cmbStatus.Items.Add("neplacano");
            cmbStatus.SelectedIndex = 0;
        }

        private void CmbRezervacija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRezervacija.SelectedItem == null)
            {
                lblInfoStranka.Text = "-";
                lblInfoAvto.Text = "-";
                lblInfoCena.Text = "0,00 €";
                numZnesek.Value = 0;
                return;
            }

            RezervacijaItem rez = (RezervacijaItem)cmbRezervacija.SelectedItem;
            lblInfoStranka.Text = rez.Stranka;
            lblInfoAvto.Text = rez.Avto;
            lblInfoCena.Text = $"{rez.SkupnaCena:F2} €";

            // Avtomatsko predlaga znesek = skupna cena rezervacije
            numZnesek.Value = rez.SkupnaCena;
        }

        private bool PreveriPolja()
        {
            if (cmbRezervacija.SelectedItem == null)
            {
                MessageBox.Show("Izberite rezervacijo!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numZnesek.Value <= 0)
            {
                MessageBox.Show("Znesek mora biti večji od 0!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numZnesek.Focus();
                return false;
            }

            return true;
        }

        private void btnShrani_Click(object sender, EventArgs e)
        {
            if (!PreveriPolja())
                return;

            try
            {
                RezervacijaItem rez = (RezervacijaItem)cmbRezervacija.SelectedItem;

                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();

                    string sql = @"INSERT INTO placila 
                                  (rezervacija_id, znesek, nacin_placila, status)
                                  VALUES 
                                  (@rezervacija, @znesek, @nacin, @status)";

                    using (var ukaz = new SQLiteCommand(sql, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@rezervacija", rez.Id);
                        ukaz.Parameters.AddWithValue("@znesek", numZnesek.Value);
                        ukaz.Parameters.AddWithValue("@nacin", cmbNacin.SelectedItem.ToString());
                        ukaz.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());

                        ukaz.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Plačilo je bilo uspešno zabeleženo!\n\nZnesek: {numZnesek.Value:F2} €",
                    "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri shranjevanju: " + ex.Message, "Napaka",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreklici_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}