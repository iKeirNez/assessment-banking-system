using BankServices.Model;
using System;
namespace Bank.Service
{
    public class Session
    {
        public Guid Guid { get; private set; }
        public Account Account { get; private set; }
        public DateTime StartTime { get; private set; }

        public Session(Guid guid, Account account, DateTime startTime)
        {
            Guid = guid;
            Account = account;
            StartTime = startTime;
        }
    }
}
