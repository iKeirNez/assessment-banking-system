using Bank.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    /// <summary>
    /// Represents a transaction on an account.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The account id.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The date/time of the transaction.
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// The amount.
        /// </summary>
        /// <remarks>Positive for deposits, negative for withdrawals</remarks>
        public double Amount { get; set; }

        #region Foreign Entities
        public Account Account { get; set; }
        #endregion

        /// <summary>
        /// No-args constructor for Entity Framework.
        /// </summary>
        private Transaction()
        {
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="accountId">the account id</param>
        /// <param name="amount">the amount</param>
        /// <param name="time">the date/time of the transaction</param>
        public Transaction(int accountId, double amount, DateTimeOffset time)
        {
            AccountId = accountId;
            Amount = amount;
            Time = time;
        }
    }
}
