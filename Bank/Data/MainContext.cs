using System;
using Bank.Model;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    /// <summary>
    /// The main database context for Entity Framework.
    /// </summary>
    public class MainContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public MainContext(DbContextOptions<MainContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId);
        }
    }
}
