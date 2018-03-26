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
    public partial class MainMenu : Form
    {
        private ViewService viewService;
        private Session session;

        private string welcomeLabelFormat;
        private string balanceLabelFormat;

        public MainMenu(ViewService viewService, Session session)
        {
            this.viewService = viewService;
            this.session = session;

            InitializeComponent();

            welcomeLabelFormat = welcomeLabel.Text;
            balanceLabelFormat = balanceLabel.Text;
        }

        public void UpdateDisplay()
        {
            welcomeLabel.Text = string.Format(welcomeLabelFormat, session.Account.FirstName);
            balanceLabel.Text = string.Format(balanceLabelFormat, session.Account.Balance);
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
