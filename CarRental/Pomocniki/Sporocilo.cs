using System.Windows.Forms;

namespace CarRental.Pomocniki
{
    // Kratka sporočila za nove obrazce. Besedilo je lahko vzorec z {0}, {1} ...
    // ki se najprej prevede in nato zapolni z vrednostmi.
    public static class Sporocilo
    {
        public static void Info(string besedilo, params object[] vrednosti)
        {
            MessageBox.Show(Jezik.T(besedilo, vrednosti), Jezik.T("Uspeh"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Opozorilo(string besedilo, params object[] vrednosti)
        {
            MessageBox.Show(Jezik.T(besedilo, vrednosti), Jezik.T("Opozorilo"),
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Napaka(string besedilo, params object[] vrednosti)
        {
            MessageBox.Show(Jezik.T(besedilo, vrednosti), Jezik.T("Napaka"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Vprasaj(string besedilo, params object[] vrednosti)
        {
            return MessageBox.Show(Jezik.T(besedilo, vrednosti), Jezik.T("Potrditev"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
