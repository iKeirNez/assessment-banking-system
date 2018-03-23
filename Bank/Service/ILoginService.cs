using System;

namespace Bank.Service
{
    public interface ILoginService
    {
        Session Login(string accountNumber, string password);
    }
}
