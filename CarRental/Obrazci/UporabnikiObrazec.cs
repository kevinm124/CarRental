using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Upravljanje zaposlenih (samo administrator)
    public class UporabnikiObrazec : OsnovniObrazec
    {
        private readonly DataGridView dgvUporabniki;
        private readonly Button btnAktiviraj;

        public UporabnikiObrazec()
        {
            BackColor = Color.WhiteSmoke;
            Text = "Uporabniki";

            var panelOrodja = UiPomocnik.PanelOrodja();
            var btnDodaj = UiPomocnik.Gumb("DODAJ UPORABNIKA", Tema.Modra, 200);
            var btnUredi = UiPomocnik.Gumb("UREDI", Tema.Oranzna, 110);
            btnAktiviraj = UiPomocnik.Gumb("DEAKTIVIRAJ", Tema.Vijolicna, 140);
            var btnIzbrisi = UiPomocnik.Gumb("IZBRIŠI", Tema.Rdeca, 110);
            var btnIzvoz = UiPomocnik.Gumb("📊 EXCEL", Tema.Zelena, 110);
            var btnOsvezi = UiPomocnik.SiviGumb("🔄");

            int x = 15;
            foreach (var gumb in new Control[] { btnDodaj, btnUredi, btnAktiviraj, btnIzbrisi, btnIzvoz, btnOsvezi })
            {
                gumb.Location = new Point(x, 17);
                x += gumb.Width + 10;
                panelOrodja.Controls.Add(gumb);
            }

            dgvUporabniki = new DataGridView { Dock = DockStyle.Fill };
            UiPomocnik.StilizirajTabelo(dgvUporabniki);
            var panelGlavno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            panelGlavno.Controls.Add(dgvUporabniki);

            Controls.Add(panelGlavno);
            Controls.Add(panelOrodja);

            btnDodaj.Click += (s, e) =>
            {
                using (var dialog = new DodajUrediUporabnikaObrazec())
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        NaloziUporabnike();
            };
            btnUredi.Click += (s, e) => Uredi();
            dgvUporabniki.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Uredi(); };
            btnAktiviraj.Click += (s, e) => PreklopiAktivnost();
            btnIzbrisi.Click += (s, e) => Izbrisi();
            btnIzvoz.Click += (s, e) => IzvozExcel.IzvoziTabelo(dgvUporabniki, "Uporabniki");
            btnOsvezi.Click += (s, e) => NaloziUporabnike();
            dgvUporabniki.SelectionChanged += (s, e) => PosodobiGumbAktivnosti();

            NaloziUporabnike();
        }

        private void NaloziUporabnike()
        {
            string sql = @"SELECT id, ime AS 'Ime', priimek AS 'Priimek', email AS 'Email', vloga AS 'Vloga',
                                  CASE WHEN COALESCE(aktiven, 1) = 1 THEN 'Da' ELSE 'Ne' END AS 'Aktiven',
                                  (SELECT COUNT(*) FROM rezervacije r WHERE r.zaposleni_id = z.id) AS 'Rezervacije'
                           FROM zaposleni z ORDER BY priimek, ime";

            var tabela = new DataTable();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var adapter = new SQLiteDataAdapter(sql, povezava))
                    adapter.Fill(tabela);
            }

            dgvUporabniki.DataSource = tabela;
            if (dgvUporabniki.Columns["id"] != null)
                dgvUporabniki.Columns["id"].Visible = false;
            PosodobiGumbAktivnosti();
        }

        private Zaposleni IzbraniUporabnik()
        {
            if (dgvUporabniki.SelectedRows.Count == 0)
                return null;

            var vrstica = dgvUporabniki.SelectedRows[0];
            return new Zaposleni
            {
                Id = Convert.ToInt32(vrstica.Cells["id"].Value),
                Ime = vrstica.Cells["Ime"].Value?.ToString(),
                Priimek = vrstica.Cells["Priimek"].Value?.ToString(),
                Email = vrstica.Cells["Email"].Value?.ToString(),
                Vloga = vrstica.Cells["Vloga"].Value?.ToString(),
                Aktiven = vrstica.Cells["Aktiven"].Value?.ToString() == "Da"
            };
        }

        private void PosodobiGumbAktivnosti()
        {
            var uporabnik = IzbraniUporabnik();
            btnAktiviraj.Text = Jezik.T(uporabnik != null && !uporabnik.Aktiven ? "AKTIVIRAJ" : "DEAKTIVIRAJ");
        }

        private void Uredi()
        {
            var uporabnik = IzbraniUporabnik();
            if (uporabnik == null)
            {
                Sporocilo.Opozorilo("Najprej izberite uporabnika!");
                return;
            }

            using (var dialog = new DodajUrediUporabnikaObrazec(uporabnik))
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    NaloziUporabnike();
        }

        private void PreklopiAktivnost()
        {
            var uporabnik = IzbraniUporabnik();
            if (uporabnik == null)
            {
                Sporocilo.Opozorilo("Najprej izberite uporabnika!");
                return;
            }
            if (uporabnik.Id == PrijavaObrazec.TrenutniUporabnik?.Id)
            {
                Sporocilo.Opozorilo("Svojega računa ne morete deaktivirati!");
                return;
            }

            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();

                if (uporabnik.Aktiven && uporabnik.Vloga == Pravice.VlogaAdmin)
                {
                    using (var ukaz = new SQLiteCommand(
                        "SELECT COUNT(*) FROM zaposleni WHERE vloga = 'admin' AND COALESCE(aktiven, 1) = 1 AND id <> @id", povezava))
                    {
                        ukaz.Parameters.AddWithValue("@id", uporabnik.Id);
                        if (Convert.ToInt32(ukaz.ExecuteScalar()) == 0)
                        {
                            Sporocilo.Opozorilo("V sistemu mora ostati vsaj en aktiven administrator!");
                            return;
                        }
                    }
                }

                using (var ukaz = new SQLiteCommand("UPDATE zaposleni SET aktiven = @aktiven WHERE id = @id", povezava))
                {
                    ukaz.Parameters.AddWithValue("@aktiven", uporabnik.Aktiven ? 0 : 1);
                    ukaz.Parameters.AddWithValue("@id", uporabnik.Id);
                    ukaz.ExecuteNonQuery();
                }
            }

            NaloziUporabnike();
        }

        private void Izbrisi()
        {
            var uporabnik = IzbraniUporabnik();
            if (uporabnik == null)
            {
                Sporocilo.Opozorilo("Najprej izberite uporabnika!");
                return;
            }
            if (uporabnik.Id == PrijavaObrazec.TrenutniUporabnik?.Id)
            {
                Sporocilo.Opozorilo("Svojega računa ne morete izbrisati!");
                return;
            }
            if (!Sporocilo.Vprasaj("Ali res želite izbrisati uporabnika {0}?", $"{uporabnik.Ime} {uporabnik.Priimek}"))
                return;

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();
                    using (var ukaz = new SQLiteCommand("DELETE FROM zaposleni WHERE id = @id", povezava))
                    {
                        ukaz.Parameters.AddWithValue("@id", uporabnik.Id);
                        ukaz.ExecuteNonQuery();
                    }
                }
                NaloziUporabnike();
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
            {
                Sporocilo.Opozorilo("Uporabnik ima zabeležene rezervacije, zato ga ni mogoče izbrisati. Namesto tega ga deaktivirajte.");
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri brisanju: {0}", ex.Message);
            }
        }
    }
}
