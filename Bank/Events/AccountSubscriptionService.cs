using BankServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Events
{
    public class AccountSubscriptionService
    {
        public delegate void AccountChangedHandler(Account account);
        public event AccountChangedHandler AccountChangedEvent;

        public void OnAccountChangedEvent(Account account)
        {
            AccountChangedEvent(account);
        }
    }
}
