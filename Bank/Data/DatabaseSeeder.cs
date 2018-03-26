using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bank.Data
{
    public class DatabaseSeeder
    {
        private MainContext context;
        private ILogger logger;

        public DatabaseSeeder(MainContext context, ILogger<DatabaseSeeder> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Seed()
        {
            if (context.Accounts.Count() == 0)
            {
                logger.LogInformation("Seeding accounts.");
                var accounts = SeedAccounts();
                logger.LogInformation("Seeded {0} accounts.", accounts.Count());
            }
        }

        public List<Account> SeedAccounts()
        {
            var accounts = new List<Account>();

            var account1 = new Account("example-1", "1470");
            account1.FirstName = "Graham";
            account1.LastName = "Hastings";
            account1.Balance = 1500;
            accounts.Add(account1);

            var account2 = new Account("example-2", "1590");
            account2.FirstName = "Fiona";
            account2.LastName = "Lavender";
            account2.Balance = 9800;
            accounts.Add(account2);

            var account3 = new Account("example-3", "1287");
            account3.FirstName = "Felix";
            account3.LastName = "Huberg";
            account3.Balance = 200;
            accounts.Add(account3);

            context.Accounts.AddRange(accounts);
            context.SaveChanges();

            return accounts;
        }
    }
}
