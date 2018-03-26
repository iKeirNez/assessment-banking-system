using System;
using BankServices.Model;

namespace BankServices.Repository
{
    public interface IAccountRepository
    {
        Account GetByAccountNumber(string accountNumber);
        void UpdateAccount(Account account);
    }
}
