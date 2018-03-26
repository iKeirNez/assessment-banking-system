using Bank.Service;
using BankGUI.Interface;
using BankGUI.Views;
using BankServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankGUI.Services
{
    public class ViewService
    {
        private IAccountService accountService;

        public ViewService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public LoginView ShowLoginView()
        {
            var view = new LoginView(this, accountService);
            view.Show();
            return view;
        }

        public MainMenuView ShowMainMenuView(Session session)
        {
            var view = new MainMenuView(this, accountService, session);
            view.UpdateDisplay();
            view.Show();
            return view;
        }

        public WithdrawView ShowWithdrawView(Session session)
        {
            var view = new WithdrawView(this, accountService, session);
            view.Show();
            return view;
        }
    }
}
