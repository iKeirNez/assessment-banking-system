using Bank.Exceptions;
using Bank.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankGUI
{
    public partial class Login : Form
    {
        private ILoginService loginService;

        public Login(ILoginService loginService)
        {
            this.loginService = loginService;
            InitializeComponent();
        }

        private void setLoading(bool loading)
        {
            usernameBox.Enabled = !loading;
            passwordBox.Enabled = !loading;
            submitButton.Enabled = !loading;
            Cursor.Current = loading ? Cursors.WaitCursor : Cursors.Default;
            Application.DoEvents();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            setLoading(true);

            Session session;
            try
            {
                session = loginService.Login(usernameBox.Text, passwordBox.Text);
            }
            catch (InvalidCredentialsException ex)
            {
                MessageBox.Show(ex.Message);
                setLoading(false);
                return;
            }

            MessageBox.Show("Authenticated!");
        }
    }
}
