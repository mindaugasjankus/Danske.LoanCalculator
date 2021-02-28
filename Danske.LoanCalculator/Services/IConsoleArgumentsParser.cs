using Danske.LoanCalculator.Models;

namespace Danske.LoanCalculator.Services
{
    public interface IConsoleArgumentsParser
    {
        ConsoleArguments Parse(string[] args);
    }
}