using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public partial class PrijavaObrazec : MaterialForm
    {
        public static Zaposleni TrenutniUporabnik { get; private set; }

        private LinkLabel lnkJezik;

        public PrijavaObrazec()
        {
            InitializeComponent();

            // Material Design nastavitve
            var skin = MaterialSkinManager.Instance;
            skin.AddFormToManage(this);
            Tema.NastaviMaterialSkin();
            skin.ColorScheme = new ColorScheme(
                Primary.Blue600,
                Primary.Blue700,
                Primary.Blue200,
                Accent.LightBlue200,
                TextShade.WHITE);

            lblVerzija.Text = "v3.0.0";

            // Preklop jezika že na prijavnem zaslonu
            lnkJezik = new LinkLabel
            {
                AutoSize = true,
                Location = new Point(305, 74),
                Font = new Font("Segoe UI", 9F),
                LinkColor = Tema.Modra,
                BackColor = Color.Transparent
            };
            lnkJezik.LinkClicked += (s, e) => PreklopiJezik();
            Controls.Add(lnkJezik);
            OsveziJezik();

            // Enter na geslu sproži prijavo
            txtGeslo.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnPrijava.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void PreklopiJezik()
        {
            Nastavitve.JezikKoda = Jezik.JeAnglescina ? Jezik.Slovenscina : Jezik.Anglescina;
            OsveziJezik();
        }

        private void OsveziJezik()
        {
            Jezik.PonovnoPrevedi(this);
            txtEmail.Hint = Jezik.T("Email");
            txtGeslo.Hint = Jezik.T("Geslo");
            lnkJezik.Text = Jezik.JeAnglescina ? "Slovenščina" : "English";
            lnkJezik.Left = ClientSize.Width - lnkJezik.PreferredWidth - 20;
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string geslo = txtGeslo.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(geslo))
            {
                MessageBox.Show("Vnesite email in geslo!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();
                    string sql = "SELECT * FROM zaposleni WHERE email = @email";

                    using (var ukaz = new SQLiteCommand(sql, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@email", email);

                        using (var bralec = ukaz.ExecuteReader())
                        {
                            if (bralec.Read())
                            {
                                string hashIzBaze = bralec["geslo_hash"].ToString();
                                bool aktiven = bralec["aktiven"] == DBNull.Value || Convert.ToInt32(bralec["aktiven"]) == 1;

                                if (BCrypt.Net.BCrypt.Verify(geslo, hashIzBaze))
                                {
                                    if (!aktiven)
                                    {
                                        MessageBox.Show("Vaš račun je deaktiviran. Obrnite se na administratorja.", "Napaka",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    TrenutniUporabnik = new Zaposleni
                                    {
                                        Id = Convert.ToInt32(bralec["id"]),
                                        Ime = bralec["ime"].ToString(),
                                        Priimek = bralec["priimek"].ToString(),
                                        Email = bralec["email"].ToString(),
                                        Vloga = bralec["vloga"].ToString(),
                                        Aktiven = aktiven
                                    };

                                    this.Hide();
                                    GlavniObrazec glavni = new GlavniObrazec();
                                    glavni.FormClosed += (s, args) => this.Close();
                                    glavni.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Napačno geslo!", "Napaka",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtGeslo.Text = "";
                                    txtGeslo.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Uporabnik s tem emailom ne obstaja!", "Napaka",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri povezavi z bazo: " + ex.Message, "Napaka",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreklici_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
