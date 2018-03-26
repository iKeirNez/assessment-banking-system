using BankServices.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public DateTimeOffset Time { get; set; }

        public double Amount { get; set; }

        #region Foreign Entities
        public Account Account { get; set; }
        #endregion

        // no-args constructor for entity framework
        private Transaction()
        {
        }

        public Transaction(int accountId, double amount, DateTimeOffset time)
        {
            AccountId = accountId;
            Amount = amount;
            Time = time;
        }
    }
}
