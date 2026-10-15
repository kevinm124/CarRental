using System;
using System.Data.SQLite;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Vsak uporabnik lahko spremeni svoje geslo
    public class SpremeniGesloObrazec : DialogObrazec
    {
        private readonly TextBox txtTrenutno;
        private readonly TextBox txtNovo;
        private readonly TextBox txtPotrdi;

        public SpremeniGesloObrazec() : base("🔑  Spremeni geslo", 420, 330)
        {
            txtTrenutno = DodajPolje("Trenutno geslo *", new TextBox { UseSystemPasswordChar = true }, 30, 20, 340);
            txtNovo = DodajPolje("Novo geslo * (vsaj 6 znakov)", new TextBox { UseSystemPasswordChar = true }, 30, 80, 340);
            txtPotrdi = DodajPolje("Ponovi novo geslo *", new TextBox { UseSystemPasswordChar = true }, 30, 140, 340);
            AcceptButton = btnShrani;
        }

        protected override void Shrani()
        {
            var uporabnik = PrijavaObrazec.TrenutniUporabnik;
            if (uporabnik == null)
                return;

            if (txtNovo.Text.Length < 6)
            {
                Sporocilo.Opozorilo("Novo geslo mora imeti vsaj 6 znakov!");
                return;
            }
            if (txtNovo.Text != txtPotrdi.Text)
            {
                Sporocilo.Opozorilo("Gesli se ne ujemata!");
                return;
            }

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();

                    string hash;
                    using (var ukaz = new SQLiteCommand("SELECT geslo_hash FROM zaposleni WHERE id = @id", povezava))
                    {
                        ukaz.Parameters.AddWithValue("@id", uporabnik.Id);
                        hash = ukaz.ExecuteScalar()?.ToString();
                    }

                    if (hash == null || !BCrypt.Net.BCrypt.Verify(txtTrenutno.Text, hash))
                    {
                        Sporocilo.Napaka("Trenutno geslo ni pravilno!");
                        return;
                    }

                    using (var ukaz = new SQLiteCommand("UPDATE zaposleni SET geslo_hash = @hash WHERE id = @id", povezava))
                    {
                        ukaz.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword(txtNovo.Text));
                        ukaz.Parameters.AddWithValue("@id", uporabnik.Id);
                        ukaz.ExecuteNonQuery();
                    }
                }

                Sporocilo.Info("Geslo je bilo uspešno spremenjeno.");
                base.Shrani();
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri shranjevanju: {0}", ex.Message);
            }
        }
    }
}
