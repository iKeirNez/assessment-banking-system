using System;
using System.Linq;
using BankServices.Data;
using BankServices.Model;
using Microsoft.Extensions.DependencyInjection;

namespace BankServices.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private IServiceProvider serviceProvider;

        public AccountRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Account GetByAccountNumber(string accountNumber)
        {
            var cnt = serviceProvider.GetService<AccountContext>();
            return cnt.Accounts.Where(a => a.AccountNumber == accountNumber).SingleOrDefault();
        }
    }
}
