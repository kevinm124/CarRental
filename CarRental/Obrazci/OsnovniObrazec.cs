using System;
using System.Windows.Forms;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Skupni osnovni razred za vse obrazce (razen MaterialForm):
    // ob nalaganju uporabi izbrano temo in jezik.
    public class OsnovniObrazec : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                Tema.Uporabi(this);
                Jezik.Prevedi(this);
                UiPomocnik.PripraviTabele(this);
            }
            base.OnLoad(e);
        }
    }
}
