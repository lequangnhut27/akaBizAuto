using akaBizAuto.Service.Interfaces;
using akaBizAuto.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace akaBizAuto.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IAccountFacebookService accFbService = new AccountFacebookService();
            Application.Run(new MainForm(accFbService));
        }
    }
}
