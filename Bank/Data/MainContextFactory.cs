using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Bank.Data
{
    /// <summary>
    /// Provides Entity Framework migrations with a means of connecting to the database outside of runtime.
    /// </summary>
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
