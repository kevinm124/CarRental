using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public class DodajUrediUporabnikaObrazec : DialogObrazec
    {
        private readonly Zaposleni uporabnikZaUrejanje;
        private bool jeUrejanje => uporabnikZaUrejanje != null;

        private readonly TextBox txtIme;
        private readonly TextBox txtPriimek;
        private readonly TextBox txtEmail;
        private readonly ComboBox cmbVloga;
        private readonly CheckBox chkAktiven;
        private readonly TextBox txtGeslo;
        private readonly TextBox txtPotrdi;

        // Konstruktor za DODAJANJE
        public DodajUrediUporabnikaObrazec() : this(null) { }

        // Konstruktor za UREJANJE
        public DodajUrediUporabnikaObrazec(Zaposleni uporabnik)
            : base(uporabnik == null ? "➕  Dodaj uporabnika" : "✏️  Uredi uporabnika", 540, 470)
        {
            uporabnikZaUrejanje = uporabnik;

            txtIme = DodajPolje("Ime *", new TextBox(), 30, 20);
            txtPriimek = DodajPolje("Priimek *", new TextBox(), 280, 20);
            txtEmail = DodajPolje("Email (uporabniško ime) *", new TextBox(), 30, 80, 470);

            cmbVloga = DodajPolje("Vloga", new ComboBox(), 30, 140);
            cmbVloga.Items.Add(Pravice.VlogaAgent);
            cmbVloga.Items.Add(Pravice.VlogaAdmin);
            cmbVloga.SelectedIndex = 0;

            chkAktiven = new CheckBox { Text = "Račun je aktiven", Checked = true, AutoSize = true, Location = new System.Drawing.Point(280, 165) };
            panelGlavno.Controls.Add(chkAktiven);

            string opomba = jeUrejanje ? "Novo geslo (pustite prazno, če ga ne spreminjate)" : "Geslo * (vsaj 6 znakov)";
            txtGeslo = DodajPolje(opomba, new TextBox { UseSystemPasswordChar = true }, 30, 200, 470);
            txtPotrdi = DodajPolje("Ponovi geslo", new TextBox { UseSystemPasswordChar = true }, 30, 260, 470);

            var lblVloge = UiPomocnik.Oznaka("admin – vse pravice  ·  agent – brez brisanja, uporabnikov in nastavitev",
                8.25F, System.Drawing.FontStyle.Italic, System.Drawing.Color.FromArgb(120, 120, 120));
            lblVloge.Location = new System.Drawing.Point(30, 320);
            panelGlavno.Controls.Add(lblVloge);

            if (jeUrejanje)
            {
                txtIme.Text = uporabnik.Ime;
                txtPriimek.Text = uporabnik.Priimek;
                txtEmail.Text = uporabnik.Email;
                cmbVloga.SelectedItem = uporabnik.Vloga == Pravice.VlogaAdmin ? Pravice.VlogaAdmin : Pravice.VlogaAgent;
                chkAktiven.Checked = uporabnik.Aktiven;

                // Sebe ne moreš deaktivirati ali si odvzeti admin pravic
                if (uporabnik.Id == PrijavaObrazec.TrenutniUporabnik?.Id)
                {
                    chkAktiven.Enabled = false;
                    cmbVloga.Enabled = false;
                }
            }
        }

        private bool PreveriPolja()
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text) || string.IsNullOrWhiteSpace(txtPriimek.Text))
            {
                Sporocilo.Opozorilo("Vnesite ime in priimek!");
                return false;
            }

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Sporocilo.Opozorilo("Email naslov ni veljaven!");
                txtEmail.Focus();
                return false;
            }

            bool spreminjaGeslo = !jeUrejanje || txtGeslo.Text.Length > 0;
            if (spreminjaGeslo)
            {
                if (txtGeslo.Text.Length < 6)
                {
                    Sporocilo.Opozorilo("Geslo mora imeti vsaj 6 znakov!");
                    return false;
                }
                if (txtGeslo.Text != txtPotrdi.Text)
                {
                    Sporocilo.Opozorilo("Gesli se ne ujemata!");
                    return false;
                }
            }

            return true;
        }

        protected override void Shrani()
        {
            if (!PreveriPolja())
                return;

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();
                    string vloga = cmbVloga.SelectedItem.ToString();
                    bool aktiven = chkAktiven.Checked;

                    // Vedno mora ostati vsaj en aktiven administrator
                    if (jeUrejanje && uporabnikZaUrejanje.Vloga == Pravice.VlogaAdmin && (vloga != Pravice.VlogaAdmin || !aktiven))
                    {
                        using (var ukaz = new SQLiteCommand(
                            "SELECT COUNT(*) FROM zaposleni WHERE vloga = 'admin' AND COALESCE(aktiven, 1) = 1 AND id <> @id", povezava))
                        {
                            ukaz.Parameters.AddWithValue("@id", uporabnikZaUrejanje.Id);
                            if (Convert.ToInt32(ukaz.ExecuteScalar()) == 0)
                            {
                                Sporocilo.Opozorilo("V sistemu mora ostati vsaj en aktiven administrator!");
                                return;
                            }
                        }
                    }

                    string sql;
                    bool spreminjaGeslo = !jeUrejanje || txtGeslo.Text.Length > 0;
                    if (jeUrejanje)
                    {
                        sql = @"UPDATE zaposleni SET ime = @ime, priimek = @priimek, email = @email,
                                vloga = @vloga, aktiven = @aktiven"
                              + (spreminjaGeslo ? ", geslo_hash = @geslo" : "") +
                              " WHERE id = @id";
                    }
                    else
                    {
                        sql = @"INSERT INTO zaposleni (ime, priimek, email, geslo_hash, vloga, aktiven)
                                VALUES (@ime, @priimek, @email, @geslo, @vloga, @aktiven)";
                    }

                    using (var ukaz = new SQLiteCommand(sql, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@ime", txtIme.Text.Trim());
                        ukaz.Parameters.AddWithValue("@priimek", txtPriimek.Text.Trim());
                        ukaz.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        ukaz.Parameters.AddWithValue("@vloga", vloga);
                        ukaz.Parameters.AddWithValue("@aktiven", aktiven ? 1 : 0);
                        if (spreminjaGeslo)
                            ukaz.Parameters.AddWithValue("@geslo", BCrypt.Net.BCrypt.HashPassword(txtGeslo.Text));
                        if (jeUrejanje)
                            ukaz.Parameters.AddWithValue("@id", uporabnikZaUrejanje.Id);
                        ukaz.ExecuteNonQuery();
                    }
                }

                Sporocilo.Info(jeUrejanje ? "Uporabnik je bil uspešno posodobljen." : "Uporabnik je bil uspešno dodan.");
                base.Shrani();
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
            {
                Sporocilo.Napaka("Uporabnik s tem emailom že obstaja!");
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri shranjevanju: {0}", ex.Message);
            }
        }
    }
}
