﻿using Bank.Exceptions;
using Bank.Service;
using BankGUI.Services;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankGUI.Views
{
    public partial class WithdrawView : Form
    {
        private ViewService viewService;
        private IAccountService accountService;
        private Session session;

        public WithdrawView(ViewService viewService, IAccountService accountService, Session session)
        {
            this.viewService = viewService;
            this.accountService = accountService;
            this.session = session;
            InitializeComponent();
        }

        private void doWithdraw(double amount)
        {
            var account = session.Account;

            try
            {
                var transaction = accountService.Withdraw(account, amount);
                MessageBox.Show(string.Format("Successfully withdrew £{0:0.00}.", -transaction.Amount), "Success");
            }
            catch (InsufficientFundsException ex)
            {
                MessageBox.Show(ex.Message, "Insufficient Funds");
                return;
            }
            catch (AmountException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Amount");
                return;
            }
            catch (WithdrawLimitException ex)
            {
                MessageBox.Show(ex.Message, "Withdrawal Limit");
                return;
            }
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

        private void buttonWithdrawCustom_Click(object sender, EventArgs e)
        {
            doWithdraw((double)inputCustomAmount.Value);
        }
    }
}
