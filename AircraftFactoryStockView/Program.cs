using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AircraftFactoryStockView
{
    static class Program
    {
        public static bool LoggedIn;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            APIClient.Connect();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new FormEnter();

            form.ShowDialog();

            if (LoggedIn)
            {
                Application.Run(new FormMain());
            }
        }
    }
}
