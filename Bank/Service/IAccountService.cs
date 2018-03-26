using Bank.Events;
using BankServices.Model;
using System;

namespace Bank.Service
{
    public interface IAccountService
    {
        AccountSubscriptionService SubscriptionService { get; }
        Session Login(string accountNumber, string password);
        void UpdateAccount(Account account);
    }
}
