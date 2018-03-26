using Bank.Service;
using BankGUI.Services;
using BankServices.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankGUI.Interface
{
    public partial class MainMenuView : Form
    {
        private ViewService viewService;
        private Session session;

        private string welcomeLabelFormat;
        private string balanceLabelFormat;

        public MainMenuView(ViewService viewService, IAccountService accountService, Session session)
        {
            this.viewService = viewService;
            this.session = session;

            InitializeComponent();

            welcomeLabelFormat = welcomeLabel.Text;
            balanceLabelFormat = balanceLabel.Text;

            // register events for account objects updating
            accountService.SubscriptionService.AccountChangedEvent += onAccountChanged;
        }

        public void UpdateDisplay()
        {
            welcomeLabel.Text = string.Format(welcomeLabelFormat, session.Account.FirstName);
            balanceLabel.Text = string.Format(balanceLabelFormat, session.Account.Balance);
        }

        private void onAccountChanged(Account account)
        {
            if (account.Id == session.Account.Id)
            {
                UpdateDisplay();
            }
        }

        private void withdrawButton_Click(object sender, EventArgs e)
        {
            viewService.ShowWithdrawView(session);
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
