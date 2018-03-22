using System;
using BankServices.Model;
using Microsoft.EntityFrameworkCore;

namespace BankServices.Data
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountContext(DbContextOptions<AccountContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {     
        }
    }
}
