using Bank.Events;
using Bank.Model;
using BankServices.Model;
using System;

namespace Bank.Service
{
    public interface IAccountService
    {
        AccountSubscriptionService SubscriptionService { get; }
        Session Login(string accountNumber, string password);
        Transaction Withdraw(Account account, double amount);
        void UpdateAccount(Account account);
    }
}
