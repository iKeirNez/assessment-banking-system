using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Model;
using Bank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Repository
{
    /// <summary>
    /// An Entity Framework implementation of <see cref="IAccountRepository"/>.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private MainContext context;

        public AccountRepository(MainContext context)
        {
            this.context = context;
        }

        public Account GetByAccountNumber(string accountNumber)
        {
            return context.Accounts.Where(a => a.AccountNumber == accountNumber)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public void UpdateAccount(Account account)
        {
            context.Accounts.Attach(account);
            context.Entry(account).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<Transaction> GetTransactionsForAccountOnDate(int accountId, DateTimeOffset date)
        {
            return context.Transactions.Where(t => t.AccountId == accountId && t.Time.Date == date.Date)
                .AsNoTracking()
                .ToList();
        }

        public void AddTransaction(Transaction transaction)
        {
            context.Transactions.Add(transaction);
        }
    }
}
