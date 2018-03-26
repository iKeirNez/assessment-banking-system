using Bank.Service;
using BankGUI.Interface;
using BankGUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankGUI.Services
{
    /// <summary>
    /// A service which handles creation, initialization and displaying of different views.
    /// </summary>
    public class ViewService
    {
        private IAccountService accountService;

        public ViewService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Shows the login view.
        /// </summary>
        /// <returns>the login view</returns>
        public LoginView ShowLoginView()
        {
            var view = new LoginView(this, accountService);
            view.Show();
            return view;
        }

        /// <summary>
        /// Shows the main menu view.
        /// </summary>
        /// <param name="session">the user session</param>
        /// <returns>the main menu view</returns>
        public MainMenuView ShowMainMenuView(Session session)
        {
            var view = new MainMenuView(this, accountService, session);
            view.UpdateDisplay();
            view.Show();
            return view;
        }

        /// <summary>
        /// Shows the withdrawal view.
        /// </summary>
        /// <param name="session">the user session</param>
        /// <returns>the withdrawal view</returns>
        public WithdrawView ShowWithdrawView(Session session)
        {
            var view = new WithdrawView(this, accountService, session);
            view.Show();
            return view;
        }
    }
}
