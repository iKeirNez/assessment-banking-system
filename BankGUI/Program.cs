using Bank;
using Bank.Service;
using BankServices.Data;
using BankServices.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankGUI
{
    static class Program
    {
        private static BankApplication application;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            application = new BankApplication();
            application.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login(application.ServiceProvider.GetService<ILoginService>()));
        }
    }
}
