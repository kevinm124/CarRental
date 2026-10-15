using System.Drawing;
using System.Windows.Forms;
using CarRental.Pomocniki;

namespace CarRental.Obrazci
{
    // Prikaz slike v velikem oknu (klik ali Esc zapre)
    public class PregledSlikeObrazec : Form
    {
        public PregledSlikeObrazec(string potSlike, string naslov = "")
        {
            Text = string.IsNullOrEmpty(naslov) ? Jezik.T("Slika") : naslov;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1000, 720);
            BackColor = Color.Black;
            KeyPreview = true;
            ShowInTaskbar = false;

            var slika = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = SlikePomocnik.Nalozi(potSlike),
                Cursor = Cursors.Hand
            };
            slika.Click += (s, e) => Close();
            Controls.Add(slika);
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
            FormClosed += (s, e) => slika.Image?.Dispose();
        }
    }
}
