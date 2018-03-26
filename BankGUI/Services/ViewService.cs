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

        public Login ShowLoginView()
        {
            var view = new Login(this, accountService);
            view.Show();
            return view;
        }

        public MainMenu ShowMainMenuView(Session session)
        {
            var view = new MainMenu(this, session);
            view.UpdateDisplay();
            view.Show();
            return view;
        }

        public Withdraw ShowWithdrawView(Session session)
        {
            var view = new Withdraw(this, accountService, session);
            view.Show();
            return view;
        }
    }
}
