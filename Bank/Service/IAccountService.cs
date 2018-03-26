using System;

namespace Bank.Service
{
    public interface IAccountService
    {
        Session Login(string accountNumber, string password);
    }
}
