using Bank.Service;
using BankGUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankGUI.Views
{
    public partial class Withdraw : Form
    {
        private ViewService viewService;
        private IAccountService accountService;
        private Session session;

        public Withdraw(ViewService viewService, IAccountService accountService, Session session)
        {
            this.viewService = viewService;
            this.accountService = accountService;
            this.session = session;
            InitializeComponent();
        }

        private void doWithdraw(double amount)
        {
            var account = session.Account;

            if (account.Balance < amount)
            {
                MessageBox.Show(string.Format("You do not have enough funds to withdraw: {0}", amount), "You broke boi");
                return;
            }

            account.Balance -= amount;
            accountService.UpdateAccount(account);
        }

        private void buttonWithdraw10_Click(object sender, EventArgs e)
        {
            doWithdraw(10);
        }

        private void buttonWithdraw20_Click(object sender, EventArgs e)
        {
            doWithdraw(20);
        }

        private void buttonWithdraw30_Click(object sender, EventArgs e)
        {
            doWithdraw(30);
        }

        private void buttonWithdraw40_Click(object sender, EventArgs e)
        {
            doWithdraw(40);
        }

        private void buttonWithdraw50_Click(object sender, EventArgs e)
        {
            doWithdraw(50);
        }

        private void buttonWithdraw150_Click(object sender, EventArgs e)
        {
            doWithdraw(150);
        }

        private void buttonWithdraw200_Click(object sender, EventArgs e)
        {
            doWithdraw(200);
        }

        private void buttonWithdraw250_Click(object sender, EventArgs e)
        {
            doWithdraw(250);
        }
    }
}
