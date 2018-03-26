using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Tests.Utilities
{
    public class ObjectBuilder
    {
        private static int nextAccountId = 1;
        private static int nextTransactionId = 1;

        public static Account BuildAccount(string accountNumber, string pin)
        {
            if (accountNumber == null)
            {
                accountNumber = "test-" + nextAccountId;
            }

            var account = new Account(accountNumber, pin);
            account.Id = nextAccountId++;
            account.FirstName = string.Format("Test Account #{0}", account.Id);
            return account;
        }

        public static Transaction BuildTransaction(int accountId, double amount, DateTimeOffset time)
        {
            var transaction = new Transaction(accountId, amount, time);
            transaction.Id = nextTransactionId++;
            return transaction;
        }

        private ObjectBuilder()
        {
        }
    }
}
