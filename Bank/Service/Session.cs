using System;
namespace Bank.Service
{
    public class Session
    {
        public int AccountId { get; private set; }
        public string AccountNumber { get; private set; }
        public DateTime StartTime { get; private set; }

        public Session(int accountId, string accountNumber, DateTime startTime)
        {
            AccountId = accountId;
            AccountNumber = accountNumber;
            StartTime = startTime;
        }
    }
}
