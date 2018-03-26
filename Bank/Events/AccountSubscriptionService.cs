using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Events
{
    /// <summary>
    /// Handles subscribing and firing of <see cref="Account"/> related events.
    /// </summary>
    public class AccountSubscriptionService
    {
        public delegate void AccountChangedHandler(Account account);
        public event AccountChangedHandler AccountChangedEvent;

        /// <summary>
        /// Fires an <see cref="AccountChangedEvent"/> to all subscribers.
        /// </summary>
        /// <param name="account">the changed account</param>
        public void OnAccountChangedEvent(Account account)
        {
            AccountChangedEvent(account);
        }
    }
}
