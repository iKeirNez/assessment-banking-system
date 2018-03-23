using System;
using Bank.Exceptions;
using BankServices.Repository;

namespace Bank.Service
{
    public class LoginService : ILoginService
    {
        private IAccountRepository accountRepository;

        public LoginService(IAccountRepository accountRepository)
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

            return new Session(account.Id, account.AccountNumber, DateTime.Now);
        }
    }
}
