using Bank.Service;
using BankGUI.Services;
using Bank.Model;
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
    /// <summary>
    /// The view shown after the user successfully authenticates via the <see cref="LoginView"/>.
    /// </summary>
    public partial class MainMenuView : Form
    {
        private ViewService viewService;
        private Session session;

        private string welcomeLabelFormat;
        private string balanceLabelFormat;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="viewService">the view service</param>
        /// <param name="accountService">the account service</param>
        /// <param name="session">the user session</param>
        public MainMenuView(ViewService viewService, IAccountService accountService, Session session)
        {
            this.viewService = viewService;
            this.session = session;

            InitializeComponent();

            // the welcome and balance labels are pre-populated with a format string
            // store these in a variable so we can use them in UpdateAccountDisplay()
            welcomeLabelFormat = welcomeLabel.Text;
            balanceLabelFormat = balanceLabel.Text;

            // register events for account objects updating
            // this allows the balance label to update in realtime
            accountService.SubscriptionService.AccountChangedEvent += onAccountChanged;
        }

        /// <summary>
        /// Updates display elements backed by the logged-in <see cref="Account"/>
        /// </summary>
        public void UpdateAccountDisplay()
        {
            welcomeLabel.Text = string.Format(welcomeLabelFormat, session.Account.FirstName);
            balanceLabel.Text = string.Format(balanceLabelFormat, session.Account.Balance);
        }

        /// <summary>
        /// An event fired by <see cref="Bank.Events.AccountSubscriptionService"/> when a change to an account object is made.
        /// </summary>
        /// <param name="account">the account</param>
        private void onAccountChanged(Account account)
        {
            if (account.Id == session.Account.Id)
            {
                UpdateAccountDisplay();
            }
        }

        /// <summary>
        /// Shows the withdrawal view.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void withdrawButton_Click(object sender, EventArgs e)
        {
            viewService.ShowWithdrawView(session);
        }

        /// <summary>
        /// Exits the application when the view is closed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Logs the user out of the currently logged in account.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void logoutButton_Click(object sender, EventArgs e)
        {
            // hide and dispose of the form since we won't be using it again (we'll instantiate a new one if the user logs in again)
            Hide();
            Dispose();

            viewService.ShowLoginView();
        }
    }
}
