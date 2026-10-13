using System;
using System.Data.SQLite;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using CarRental.Baza;
using CarRental.Modeli;

namespace CarRental.Obrazci
{
    public partial class PrijavaObrazec : MaterialForm
    {
        public static Zaposleni TrenutniUporabnik { get; private set; }

        public PrijavaObrazec()
        {
            InitializeComponent();

            // Material Design nastavitve
            var skin = MaterialSkinManager.Instance;
            skin.AddFormToManage(this);
            skin.Theme = MaterialSkinManager.Themes.LIGHT;
            skin.ColorScheme = new ColorScheme(
                Primary.Blue600,
                Primary.Blue700,
                Primary.Blue200,
                Accent.LightBlue200,
                TextShade.WHITE);

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

                                if (BCrypt.Net.BCrypt.Verify(geslo, hashIzBaze))
                                {
                                    TrenutniUporabnik = new Zaposleni
                                    {
                                        Id = Convert.ToInt32(bralec["id"]),
                                        Ime = bralec["ime"].ToString(),
                                        Priimek = bralec["priimek"].ToString(),
                                        Email = bralec["email"].ToString(),
                                        Vloga = bralec["vloga"].ToString()
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