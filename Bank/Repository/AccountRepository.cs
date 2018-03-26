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
        private IServiceProvider serviceProvider;

        public AccountRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Account GetByAccountNumber(string accountNumber)
        {
            var cnt = serviceProvider.GetService<MainContext>();
            return cnt.Accounts.Where(a => a.AccountNumber == accountNumber)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public void UpdateAccount(Account account)
        {
            var cnt = serviceProvider.GetService<MainContext>();
            cnt.Accounts.Attach(account);
            cnt.Entry(account).State = EntityState.Modified;
            cnt.SaveChanges();
        }

        public List<Transaction> GetTransactionsForAccountOnDate(int accountId, DateTimeOffset date)
        {
            var cnt = serviceProvider.GetService<MainContext>();
            return cnt.Transactions.Where(t => t.AccountId == accountId && t.Time.Date == date.Date)
                .AsNoTracking()
                .ToList();
        }

        public void AddTransaction(Transaction transaction)
        {
            var cnt = serviceProvider.GetService<MainContext>();
            cnt.Transactions.Add(transaction);
        }
    }
}
