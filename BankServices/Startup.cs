using System;
using BankServices.Data;
using BankServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankServices
{
    public class Startup
    {
        public static void Main()
        {
            var serviceProvider = SetupDependencyInjection();
            Console.WriteLine("BankServices started successfully.");
            var accountRepo = serviceProvider.GetService<IAccountRepository>();
            var account = accountRepo.GetByAccountNumber("knellyer0010");
            Console.WriteLine(account);
        }

        public static IServiceProvider SetupDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            // TODO move connect string to external file
            serviceCollection.AddDbContext<AccountContext>(options =>
                                                           options.UseSqlServer("Server=127.0.0.1;Database=qaDotNetAdvanced;User Id=app;Password=app;"));
            serviceCollection.AddSingleton<IAccountRepository, AccountRepository>();
            serviceCollection.AddLogging();
            return serviceCollection.BuildServiceProvider();
        }
    }
}
