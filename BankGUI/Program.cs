using Bank;
using BankGUI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BankGUI
{
    /// <summary>
    /// The main entrypoint for the frontend of the application.
    /// </summary>
    public static class Program
    {
        private static BankApplication application;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            application = new BankApplication();
            application.Init();

            var viewService = ActivatorUtilities.CreateInstance<ViewService>(application.ServiceProvider);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            viewService.ShowLoginView();
            Application.Run();
        }
    }
}
