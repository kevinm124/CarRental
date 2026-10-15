using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;

namespace CarRental.Obrazci
{
    public partial class DodajUrediStrankoObrazec : OsnovniObrazec
    {
        private Stranka strankaZaUrejanje;
        private bool jeUrejanje => strankaZaUrejanje != null;

        // Konstruktor za DODAJANJE
        public DodajUrediStrankoObrazec()
        {
            InitializeComponent();
            lblNaslovDialoga.Text = "➕  Dodaj novo stranko";
        }

        // Konstruktor za UREJANJE
        public DodajUrediStrankoObrazec(Stranka stranka) : this()
        {
            strankaZaUrejanje = stranka;
            lblNaslovDialoga.Text = "✏️  Uredi stranko";
            NapolniPolja();
        }

        private void NapolniPolja()
        {
            txtIme.Text = strankaZaUrejanje.Ime;
            txtPriimek.Text = strankaZaUrejanje.Priimek;
            txtEmail.Text = strankaZaUrejanje.Email;
            txtTelefon.Text = strankaZaUrejanje.Telefon;
            txtNaslov.Text = strankaZaUrejanje.Naslov;
            txtVozniska.Text = strankaZaUrejanje.StVozniske;
        }

        private bool PreveriPolja()
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                MessageBox.Show("Vnesite ime stranke!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIme.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPriimek.Text))
            {
                MessageBox.Show("Vnesite priimek stranke!", "Opozorilo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPriimek.Focus();
                return false;
            }

            // Preveri email format (če je vnesen)
            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                string emailVzorec = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailVzorec))
                {
                    MessageBox.Show("Email naslov ni veljaven!", "Opozorilo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnShrani_Click(object sender, EventArgs e)
        {
            if (!PreveriPolja())
                return;

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();

                    string sql;
                    if (jeUrejanje)
                    {
                        sql = @"UPDATE stranke SET 
                                ime = @ime, priimek = @priimek, email = @email,
                                telefon = @telefon, naslov = @naslov, st_vozniske = @vozniska
                                WHERE id = @id";
                    }
                    else
                    {
                        sql = @"INSERT INTO stranke 
                                (ime, priimek, email, telefon, naslov, st_vozniske)
                                VALUES 
                                (@ime, @priimek, @email, @telefon, @naslov, @vozniska)";
                    }

                    using (var ukaz = new SQLiteCommand(sql, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@ime", txtIme.Text.Trim());
                        ukaz.Parameters.AddWithValue("@priimek", txtPriimek.Text.Trim());
                        ukaz.Parameters.AddWithValue("@email",
                            string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        ukaz.Parameters.AddWithValue("@telefon",
                            string.IsNullOrWhiteSpace(txtTelefon.Text) ? (object)DBNull.Value : txtTelefon.Text.Trim());
                        ukaz.Parameters.AddWithValue("@naslov",
                            string.IsNullOrWhiteSpace(txtNaslov.Text) ? (object)DBNull.Value : txtNaslov.Text.Trim());
                        ukaz.Parameters.AddWithValue("@vozniska",
                            string.IsNullOrWhiteSpace(txtVozniska.Text) ? (object)DBNull.Value : txtVozniska.Text.Trim());

                        if (jeUrejanje)
                            ukaz.Parameters.AddWithValue("@id", strankaZaUrejanje.Id);

                        ukaz.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    jeUrejanje ? "Stranka je bila uspešno posodobljena." : "Stranka je bila uspešno dodana.",
                    "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
            {
                MessageBox.Show("Email naslov že obstaja v bazi!", "Napaka",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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