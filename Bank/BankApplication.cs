using System;
using Bank.Service;
using BankServices.Data;
using BankServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank
{
    public class BankApplication
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public BankApplication()
        {
        }

        public void Init()
        {
            var serviceCollection = new ServiceCollection();
            // TODO move connect string to external file
            serviceCollection.AddDbContext<AccountContext>(options =>
                                                           options.UseSqlServer("Server=127.0.0.1;Database=qaDotNetAdvanced;User Id=app;Password=app;"));
            serviceCollection.AddSingleton<IAccountRepository, AccountRepository>();
            serviceCollection.AddSingleton<ILoginService, LoginService>();
            serviceCollection.AddLogging();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
