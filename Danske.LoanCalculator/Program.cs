using System;
using Danske.LoanCalculator.Models;
using Danske.LoanCalculator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Danske.LoanCalculator
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args) 
        {
            RegisterServices();
            
            var arguments = _serviceProvider.GetService<IConsoleArgumentsParser>().Parse(args);

            var loan = new Services.LoanCalculator((decimal)arguments.LoanAmount, 
                arguments.DurationInMonths, 
                arguments.AnnualInterestRate.Value, 
                (decimal)arguments.AdministrationFeeMaxValue.Value, 
                arguments.AdministrationFeePercentage.Value, 
                arguments.Compound.Value);

            Console.WriteLine($"Monthly payment: {Math.Round(loan.MonthlyPayment(), 2, MidpointRounding.AwayFromZero)} kr.");
            Console.WriteLine($"Total amount payed in interests: {Math.Round(loan.TotalInterestRateAmount(), 2, MidpointRounding.AwayFromZero)} kr.");
            Console.WriteLine($"Total administration fee: {Math.Round(loan.AdministrationFee(), 2, MidpointRounding.AwayFromZero)} kr.");
            Console.WriteLine($"Total loan cost: {Math.Round(loan.TotalCost(), 2, MidpointRounding.AwayFromZero)} kr.");
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddLogging();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton(configuration.GetSection("AppSettings").Get<AppConfiguration>());
            services.AddSingleton<IConsoleArgumentsParser, ConsoleArgumentsParser>();

            _serviceProvider = services.BuildServiceProvider(true);
        }
    }
}