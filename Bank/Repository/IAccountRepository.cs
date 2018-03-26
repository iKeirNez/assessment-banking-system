using System;
using System.Collections.Generic;
using Bank.Model;
using Bank.Model;

namespace Bank.Repository
{
    /// <summary>
    /// Handles creating/reading/updating/deleting of <see cref="Account"/> and <see cref="Transaction"/> entities.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Gets an account by it's account number.
        /// </summary>
        /// <param name="accountNumber">the account number</param>
        /// <returns>the account, or null if not found</returns>
        Account GetByAccountNumber(string accountNumber);

        /// <summary>
        /// Updates an existing account.
        /// </summary>
        /// <param name="account">the account</param>
        void UpdateAccount(Account account);

        /// <summary>
        /// Gets transactions which occurred for the supplied account id on a given date.
        /// </summary>
        /// <param name="accountId">the account id</param>
        /// <param name="date">the date, the time part is ignored</param>
        /// <returns>the list of transactions</returns>
        List<Transaction> GetTransactionsForAccountOnDate(int accountId, DateTimeOffset date);

        /// <summary>
        /// Adds a new transaction.
        /// </summary>
        /// <param name="transaction">the transaction</param>
        void AddTransaction(Transaction transaction);
    }
}
