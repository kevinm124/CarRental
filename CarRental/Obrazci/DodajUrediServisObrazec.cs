using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using CarRental.Baza;
using CarRental.Modeli;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    public class DodajUrediServisObrazec : DialogObrazec
    {
        private readonly Servis servisZaUrejanje;
        private bool jeUrejanje => servisZaUrejanje != null;

        private readonly ComboBox cmbAvto;
        private readonly DateTimePicker dtpDatum;
        private readonly ComboBox cmbVrsta;
        private readonly NumericUpDown numKm;
        private readonly NumericUpDown numCena;
        private readonly CheckBox chkNaslednji;
        private readonly DateTimePicker dtpNaslednji;
        private readonly TextBox txtOpis;
        private readonly CheckBox chkNaServisu;

        public DodajUrediServisObrazec(int? avtomobilId = null) : this(null, avtomobilId) { }

        public DodajUrediServisObrazec(Servis servis, int? avtomobilId = null)
            : base(servis == null ? "🔧  Nov servis" : "✏️  Uredi servis", 560, 560)
        {
            servisZaUrejanje = servis;

            cmbAvto = DodajPolje("Avtomobil *", new ComboBox(), 30, 15, 490);
            IzbiraAvtomobila.NapolniComboBox(cmbAvto, false, servis?.AvtomobilId ?? avtomobilId);

            dtpDatum = DodajPolje("Datum servisa *", Datum(), 30, 75);
            cmbVrsta = DodajPolje("Vrsta *", new ComboBox { DropDownStyle = ComboBoxStyle.DropDown }, 290, 75, 230);
            foreach (string v in ServisPomocnik.VrsteServisov)
                cmbVrsta.Items.Add(v);
            cmbVrsta.SelectedIndex = 0;

            numKm = DodajPolje("Stanje števca (km)", Stevilo(0, 2000000, 0, 1000), 30, 135);
            numCena = DodajPolje("Cena (€)", Stevilo(0, 100000, 2, 10), 290, 135, 230);

            chkNaslednji = new CheckBox { Text = "Naslednji servis", AutoSize = true, Location = new Point(30, 200), Checked = true };
            panelGlavno.Controls.Add(chkNaslednji);
            dtpNaslednji = Datum();
            dtpNaslednji.Location = new Point(30, 222);
            dtpNaslednji.Width = 220;
            dtpNaslednji.Font = new Font("Segoe UI", 9.75F);
            dtpNaslednji.Value = DateTime.Today.AddYears(1);
            panelGlavno.Controls.Add(dtpNaslednji);
            chkNaslednji.CheckedChanged += (s, e) => dtpNaslednji.Enabled = chkNaslednji.Checked;

            chkNaServisu = new CheckBox { Text = "Avtomobil je trenutno na servisu", AutoSize = true, Location = new Point(290, 224) };
            panelGlavno.Controls.Add(chkNaServisu);

            txtOpis = DodajPolje("Opis opravljenega dela", new TextBox { Multiline = true, Height = 110, ScrollBars = ScrollBars.Vertical }, 30, 265, 490);

            cmbAvto.SelectedIndexChanged += (s, e) => OsveziStatusAvta();
            OsveziStatusAvta();

            if (jeUrejanje)
            {
                dtpDatum.Value = servis.Datum;
                cmbVrsta.Text = servis.Vrsta;
                numKm.Value = Math.Min(numKm.Maximum, servis.Km ?? 0);
                numCena.Value = Math.Min(numCena.Maximum, servis.Cena);
                chkNaslednji.Checked = servis.NaslednjiServis.HasValue;
                if (servis.NaslednjiServis.HasValue)
                    dtpNaslednji.Value = servis.NaslednjiServis.Value;
                txtOpis.Text = servis.Opis;
            }
        }

        private void OsveziStatusAvta()
        {
            var avto = cmbAvto.SelectedItem as IzbiraAvtomobila;
            chkNaServisu.Checked = avto?.Status == "servis";
            // Rezerviranega avtomobila ne moremo poslati na servis
            chkNaServisu.Enabled = avto != null && avto.Status != "rezervirano";
        }

        protected override void Shrani()
        {
            var avto = cmbAvto.SelectedItem as IzbiraAvtomobila;
            if (avto == null)
            {
                Sporocilo.Opozorilo("Izberite avtomobil!");
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbVrsta.Text))
            {
                Sporocilo.Opozorilo("Vnesite vrsto servisa!");
                return;
            }
            if (chkNaslednji.Checked && dtpNaslednji.Value.Date <= dtpDatum.Value.Date)
            {
                Sporocilo.Opozorilo("Naslednji servis mora biti po datumu servisa!");
                return;
            }

            try
            {
                using (var povezava = PovezavaBaze.PridobiPovezavo())
                {
                    povezava.Open();
                    string sql = jeUrejanje
                        ? @"UPDATE servisi SET avtomobil_id = @avto, datum = @datum, vrsta = @vrsta, opis = @opis,
                                km = @km, cena = @cena, naslednji_servis = @naslednji WHERE id = @id"
                        : @"INSERT INTO servisi (avtomobil_id, datum, vrsta, opis, km, cena, naslednji_servis)
                                VALUES (@avto, @datum, @vrsta, @opis, @km, @cena, @naslednji)";

                    using (var ukaz = new SQLiteCommand(sql, povezava))
                    {
                        ukaz.Parameters.AddWithValue("@avto", avto.Id);
                        ukaz.Parameters.AddWithValue("@datum", dtpDatum.Value.ToString("yyyy-MM-dd"));
                        ukaz.Parameters.AddWithValue("@vrsta", cmbVrsta.Text.Trim());
                        ukaz.Parameters.AddWithValue("@opis", txtOpis.Text.Trim());
                        ukaz.Parameters.AddWithValue("@km", numKm.Value > 0 ? (object)(int)numKm.Value : DBNull.Value);
                        ukaz.Parameters.AddWithValue("@cena", numCena.Value);
                        ukaz.Parameters.AddWithValue("@naslednji",
                            chkNaslednji.Checked ? (object)dtpNaslednji.Value.ToString("yyyy-MM-dd") : DBNull.Value);
                        if (jeUrejanje)
                            ukaz.Parameters.AddWithValue("@id", servisZaUrejanje.Id);
                        ukaz.ExecuteNonQuery();
                    }

                    // Stanje števca na avtomobilu (samo navzgor)
                    if (numKm.Value > 0)
                    {
                        using (var ukaz = new SQLiteCommand(
                            "UPDATE avtomobili SET prevozeni_km = MAX(COALESCE(prevozeni_km, 0), @km) WHERE id = @id", povezava))
                        {
                            ukaz.Parameters.AddWithValue("@km", (int)numKm.Value);
                            ukaz.Parameters.AddWithValue("@id", avto.Id);
                            ukaz.ExecuteNonQuery();
                        }
                    }

                    // Status avtomobila (prosto <-> servis), rezerviranih ne spreminjamo
                    if (avto.Status != "rezervirano")
                    {
                        string novStatus = chkNaServisu.Checked ? "servis" : "prosto";
                        if (novStatus != avto.Status)
                        {
                            using (var ukaz = new SQLiteCommand("UPDATE avtomobili SET status = @status WHERE id = @id", povezava))
                            {
                                ukaz.Parameters.AddWithValue("@status", novStatus);
                                ukaz.Parameters.AddWithValue("@id", avto.Id);
                                ukaz.ExecuteNonQuery();
                            }
                        }
                    }
                }

                base.Shrani();
            }
            catch (Exception ex)
            {
                Sporocilo.Napaka("Napaka pri shranjevanju: {0}", ex.Message);
            }
        }
    }
}
