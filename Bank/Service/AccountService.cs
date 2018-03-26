using System;
using Bank.Exceptions;
using BankServices.Repository;

namespace Bank.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Session Login(string accountNumber, string password)
        {
            var account = accountRepository.GetByAccountNumber(accountNumber);

            if (account == null || account.Pin != password)
            {
                throw new InvalidCredentialsException("An account wasn't found with that username/password combination.");
            }

            return new Session(Guid.NewGuid(), account, DateTime.Now);
        }
    }
}
