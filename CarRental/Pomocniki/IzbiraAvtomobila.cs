using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using CarRental.Baza;

namespace CarRental.Pomocniki
{
    // Element za ComboBox z avtomobili (servisi, poškodbe, filtri)
    public class IzbiraAvtomobila
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Status { get; set; }
        public override string ToString() => Naziv;

        public static List<IzbiraAvtomobila> NaloziVse()
        {
            var seznam = new List<IzbiraAvtomobila>();
            using (var povezava = PovezavaBaze.PridobiPovezavo())
            {
                povezava.Open();
                using (var ukaz = new SQLiteCommand(
                    "SELECT id, znamka, model, registrska_st, status FROM avtomobili ORDER BY znamka, model", povezava))
                using (var b = ukaz.ExecuteReader())
                {
                    while (b.Read())
                    {
                        seznam.Add(new IzbiraAvtomobila
                        {
                            Id = Convert.ToInt32(b["id"]),
                            Naziv = $"{b["znamka"]} {b["model"]} ({b["registrska_st"]})",
                            Status = b["status"]?.ToString()
                        });
                    }
                }
            }
            return seznam;
        }

        // Napolni ComboBox; z "vsi" doda prvo izbiro "Vsi avtomobili"
        public static void NapolniComboBox(ComboBox cmb, bool vsi, int? izbraniId = null)
        {
            cmb.Items.Clear();
            if (vsi)
                cmb.Items.Add("Vsi avtomobili");
            foreach (var a in NaloziVse())
            {
                cmb.Items.Add(a);
                if (a.Id == izbraniId)
                    cmb.SelectedItem = a;
            }
            if (cmb.SelectedIndex < 0 && cmb.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }
    }
}
