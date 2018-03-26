using System;
using System.Linq;
using Bank.Events;
using Bank.Exceptions;
using Bank.Model;
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

        public Transaction Withdraw(Account account, double amount)
        {
            if (amount <= 0)
            {
                throw new AmountException("Amount must be bigger than 0.");
            }

            if (account.Balance < amount)
            {
                throw new InsufficientFundsException(string.Format("You do not have enough funds to withdraw: £{0:0.00}", amount));
            }

            if (amount % 10 != 0)
            {
                throw new AmountException("Amount must be a multiple of 10.");
            }

            var transactions = accountRepository.GetTransactionsForAccountOnDate(account.Id, DateTimeOffset.UtcNow.Date);
            var withdrawn = transactions.Select(t => t.Amount).Where(a => a > 0).Sum();
            if (withdrawn + amount > 250)
            {
                throw new WithdrawLimitException(string.Format("Withdrawing £{0:0.00} would cause you to go over your withdrawal limit of £{1:0.00}, you have withdrawn £{2:0.00} already today.", amount, 250, withdrawn));
            }

            var transaction = new Transaction(account.Id, amount, DateTimeOffset.UtcNow);
            accountRepository.AddTransaction(transaction);

            account.Balance -= amount;
            accountRepository.UpdateAccount(account);
            SubscriptionService.OnAccountChangedEvent(account);

            return transaction;
        }

        public void UpdateAccount(Account account)
        {
            accountRepository.UpdateAccount(account);
            SubscriptionService.OnAccountChangedEvent(account);
        }
    }
}
