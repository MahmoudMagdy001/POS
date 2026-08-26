using System;
using System.Windows.Forms;

namespace POS
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

            // Set Arabic Culture as default across all threads and forms
            var arabicCulture = new System.Globalization.CultureInfo("ar-EG");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = arabicCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = arabicCulture;

            FontManager.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
