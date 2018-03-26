using System;
using Bank.Events;
using Bank.Exceptions;
using BankServices.Model;
using BankServices.Repository;

namespace Bank.Service
{
    public class AccountService : IAccountService
    {
        public AccountSubscriptionService SubscriptionService { get; private set; }

        private IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
            SubscriptionService = new AccountSubscriptionService();
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

        public void UpdateAccount(Account account)
        {
            accountRepository.UpdateAccount(account);
            SubscriptionService.OnAccountChangedEvent(account);
        }
    }
}
