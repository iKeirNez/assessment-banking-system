using System;
using System.Collections.Generic;
using Bank.Model;
using BankServices.Model;

namespace BankServices.Repository
{
    public interface IAccountRepository
    {
        Account GetByAccountNumber(string accountNumber);
        void UpdateAccount(Account account);
        List<Transaction> GetTransactionsForAccountOnDate(int accountId, DateTimeOffset date);
        void AddTransaction(Transaction transaction);
    }
}
