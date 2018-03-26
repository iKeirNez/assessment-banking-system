using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    /// <summary>
    /// Represents an account entity in the database.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// The id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The pin (password).
        /// </summary>
        public string Pin { get; set; }

        /// <summary>
        /// The current balance.
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// A no-args constructor for Entity Framework.
        /// </summary>
        private Account()
        {
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="accountNumber">the account number</param>
        /// <param name="pin">the pin</param>
        public Account(string accountNumber, string pin)
        {
            AccountNumber = accountNumber;
            Pin = pin;
            Balance = 0;
        }
    }
}
