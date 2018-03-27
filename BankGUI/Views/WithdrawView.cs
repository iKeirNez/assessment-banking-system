using Bank.Exceptions;
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
    /// <summary>
    /// The view shown when the user clicks the withdraw button from <see cref="MainMenuView"/>.
    /// </summary>
    public partial class WithdrawView : Form
    {
        private ViewService viewService;
        private IAccountService accountService;
        private Session session;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="viewService">the view service</param>
        /// <param name="accountService">the account service</param>
        /// <param name="session">the user session</param>
        public WithdrawView(ViewService viewService, IAccountService accountService, Session session)
        {
            this.viewService = viewService;
            this.accountService = accountService;
            this.session = session;
            InitializeComponent();
        }

        /// <summary>
        /// Performs a withdrawal from the currently logged in account.
        /// </summary>
        /// <param name="amount">the amount</param>
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

        /// <summary>
        /// Withdraws £10 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw10_Click(object sender, EventArgs e)
        {
            doWithdraw(10);
        }

        /// <summary>
        /// Withdraws £20 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw20_Click(object sender, EventArgs e)
        {
            doWithdraw(20);
        }

        /// <summary>
        /// Withdraws £30 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw30_Click(object sender, EventArgs e)
        {
            doWithdraw(30);
        }

        /// <summary>
        /// Withdraws £40 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw40_Click(object sender, EventArgs e)
        {
            doWithdraw(40);
        }

        /// <summary>
        /// Withdraws £50 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw50_Click(object sender, EventArgs e)
        {
            doWithdraw(50);
        }

        /// <summary>
        /// Withdraws £150 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw150_Click(object sender, EventArgs e)
        {
            doWithdraw(150);
        }

        /// <summary>
        /// Withdraws £200 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw200_Click(object sender, EventArgs e)
        {
            doWithdraw(200);
        }

        /// <summary>
        /// Withdraws £250 from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdraw250_Click(object sender, EventArgs e)
        {
            doWithdraw(250);
        }

        /// <summary>
        /// Withdraws a custom amount from the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void buttonWithdrawCustom_Click(object sender, EventArgs e)
        {
            doWithdraw((double)inputCustomAmount.Value);
        }
    }
}
