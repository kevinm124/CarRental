using System.Windows.Forms;
using CarRental.Pomocniki;
using WinMessageBox = System.Windows.Forms.MessageBox;

namespace CarRental.Obrazci
{
    // Ta razred "prekrije" System.Windows.Forms.MessageBox za vse obrazce v CarRental.Obrazci.
    // Obstoječi klici MessageBox.Show(...) ostanejo enaki, besedilo pa se samodejno prevede
    // v izbrani jezik (glej Pomocniki/Jezik.cs).
    internal static class MessageBox
    {
        public static DialogResult Show(string besedilo)
        {
            return WinMessageBox.Show(Jezik.T(besedilo));
        }

        public static DialogResult Show(string besedilo, string naslov)
        {
            return WinMessageBox.Show(Jezik.T(besedilo), Jezik.T(naslov));
        }

        public static DialogResult Show(string besedilo, string naslov, MessageBoxButtons gumbi)
        {
            return WinMessageBox.Show(Jezik.T(besedilo), Jezik.T(naslov), gumbi);
        }

        public static DialogResult Show(string besedilo, string naslov, MessageBoxButtons gumbi, MessageBoxIcon ikona)
        {
            return WinMessageBox.Show(Jezik.T(besedilo), Jezik.T(naslov), gumbi, ikona);
        }

        public static DialogResult Show(string besedilo, string naslov, MessageBoxButtons gumbi, MessageBoxIcon ikona,
            MessageBoxDefaultButton privzetiGumb)
        {
            return WinMessageBox.Show(Jezik.T(besedilo), Jezik.T(naslov), gumbi, ikona, privzetiGumb);
        }

        public static DialogResult Show(IWin32Window lastnik, string besedilo, string naslov, MessageBoxButtons gumbi, MessageBoxIcon ikona)
        {
            return WinMessageBox.Show(lastnik, Jezik.T(besedilo), Jezik.T(naslov), gumbi, ikona);
        }
    }
}
