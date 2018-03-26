using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace BankServices.Data
{
    public class MainContextFactory : IDesignTimeDbContextFactory<MainContext>
    {
        public MainContextFactory()
        {
        }

        public MainContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            // TODO move connect string to external file
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=qaDotNetAdvanced;User Id=app;Password=app;");
            return new MainContext(optionsBuilder.Options);
        }
    }
}
