using CarRental.Baza;
using CarRental.Obrazci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            PovezavaBaze.InicializirajBazo();

            Application.Run(new PrijavaObrazec());
        }
    }
}
