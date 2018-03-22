using System;
using System.ComponentModel.DataAnnotations;

namespace BankServices.Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string AccountNumber { get; set; }

        public string Pin { get; set; }

        public double Balance { get; set; }

        public Account()
        {
        }
    }
}
