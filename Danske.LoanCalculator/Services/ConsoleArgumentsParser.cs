using System;
using Danske.LoanCalculator.Models;
using Fclp;

namespace Danske.LoanCalculator.Services
{
    public class ConsoleArgumentsParser : IConsoleArgumentsParser
    {
        private readonly AppConfiguration _appConfiguration;

        public ConsoleArgumentsParser(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public ConsoleArguments Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<ConsoleArguments>();

            parser.Setup(arg => arg.LoanAmount)
                .As('l', "LoanAmount")
                .Required();

            parser.Setup(arg => arg.DurationInMonths)
                .As('d', "DurationInMonths")
                .Required();

            parser.Setup(arg => arg.AnnualInterestRate)
                .As('a', "AnnualInterestRate")
                .SetDefault(_appConfiguration.AnnualInterestRate);

            parser.Setup(arg => arg.AdministrationFeeMaxValue)
                .As('f', "AdministrationFeeMaxValue")
                .SetDefault(_appConfiguration.AdministrationFeeMaxValue);

            parser.Setup(arg => arg.AdministrationFeePercentage)
                .As('p', "AdministrationFeePercentage")
                .SetDefault(_appConfiguration.AdministrationFeePercentage);

            parser.Setup(arg => arg.Compound)
                .As('c', "Compound")
                .SetDefault(_appConfiguration.Compound);

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine(result.ErrorText);
                throw new ArgumentException(result.ErrorText);
            }
            
            return parser.Object;
        }
    }
}
