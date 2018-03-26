using Bank.Events;
using Bank.Model;
using System;

namespace Bank.Service
{
    /// <summary>
    /// The service layer which handles account related operations.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// The service used for subscribing to and firing events.
        /// </summary>
        AccountSubscriptionService SubscriptionService { get; }

        /// <summary>
        /// Attempts to log in using supplied credentials.
        /// </summary>
        /// <param name="accountNumber">the account number</param>
        /// <param name="pin">the account pin</param>
        /// <returns></returns>
        Session Login(string accountNumber, string pin);

        /// <summary>
        /// Attempts to withdraw an amount from an account.
        /// </summary>
        /// <param name="account">the account</param>
        /// <param name="amount">the amount</param>
        /// <returns>the resultant transaction</returns>
        Transaction Withdraw(Account account, double amount);
    }
}
