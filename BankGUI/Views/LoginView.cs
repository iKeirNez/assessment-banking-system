using Bank.Exceptions;
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
    /// <summary>
    /// The first view shown when launching the application. It allows users to enter their credentials in order to authenticate.
    /// If authentication is successful, then they will be transitioned to the main menu view.
    /// </summary>
    public partial class LoginView : Form
    {
        private ViewService viewService;
        private IAccountService accountService;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="viewService">the view service</param>
        /// <param name="accountService">the account service</param>
        public LoginView(ViewService viewService, IAccountService accountService)
        {
            this.viewService = viewService;
            this.accountService = accountService;
            InitializeComponent();
        }

        /// <summary>
        /// Updates all input elements according to the <paramref name="loading"/> value to indicate an operation is taking place.
        /// Disables the username and password input boxes as well as the login button.
        /// A spinning cursor is also displayed if <paramref name="loading"/> is true.
        /// </summary>
        /// <param name="loading">true if an operation is taking place, false otherwise</param>
        private void setLoading(bool loading)
        {
            usernameBox.Enabled = !loading;
            passwordBox.Enabled = !loading;
            submitButton.Enabled = !loading;
            Application.UseWaitCursor = loading;
            Application.DoEvents();
        }

        /// <summary>
        /// Attempts to authenticate using the user input credentials.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void submitButton_Click(object sender, EventArgs e)
        {
            setLoading(true);

            Session session;
            try
            {
                session = accountService.Login(usernameBox.Text, passwordBox.Text);
            }
            catch (InvalidCredentialsException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Credentials");
                setLoading(false);
                return;
            }

            setLoading(false);
            viewService.ShowMainMenuView(session);

            // since we will no longer be using this view, hide and dispose of it
            Hide();
            Dispose();
        }

        /// <summary>
        /// Exits the application when the form is closed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void LoginView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
