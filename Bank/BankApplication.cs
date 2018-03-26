using System;
using Bank.Service;
using Bank.Data;
using Bank.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank
{
    /// <summary>
    /// The entrypoint for the backend of the application.
    /// </summary>
    public class BankApplication
    {
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        public BankApplication()
        {
        }

        /// <summary>
        /// Initializes the backend of the application (ie dependency injection and logging).
        /// </summary>
        public void Init()
        {
            var serviceCollection = new ServiceCollection();
            // TODO move connect string to external file
            serviceCollection.AddDbContext<MainContext>(options =>
                                                           options.UseSqlServer("Server=127.0.0.1;Database=qaDotNetAdvanced;User Id=app;Password=app;"));
            serviceCollection.AddSingleton<IAccountRepository, AccountRepository>();
            serviceCollection.AddSingleton<IAccountService, AccountService>();
            serviceCollection.AddLogging();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
