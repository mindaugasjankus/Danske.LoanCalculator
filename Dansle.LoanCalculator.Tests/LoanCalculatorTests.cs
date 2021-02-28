using System;
using NUnit.Framework;

namespace Danske.LoanCalculator.Tests
{
    public class LoanCalculatorTests
    {
        [TestCase(0, 1, 1, 1, 1, 1)]
        [TestCase(-1, 1, 1, 1, 1, 1)]
        [TestCase(1, 0, 1, 1, 1, 1)]
        [TestCase(1, -1, 1, 1, 1, 1)]
        [TestCase(1, 1, 0, 1, 1, 1)]
        [TestCase(1, 1, -1, 1, 1, 1)]
        [TestCase(1, 1, -1, -1, 1, 1)]
        [TestCase(1, 1, -1, 1, 0, 1)]
        [TestCase(1, 1, -1, 1, 101, 1)]
        [TestCase(1, 1, -1, 1, 1, 0)]
        [TestCase(1, 1, -1, 1, 1, -1)]
        public void Cant_Create_Loan_Calculator_With_Invalid_Parameters(decimal loanAmount, double durationInMonths, double annualInterestRate, decimal administrationFeeMaxValue, double administrationFeePercentage, int compound)
        {
            Assert.Throws<ArgumentException>(() => new Services.LoanCalculator(loanAmount, durationInMonths, annualInterestRate, administrationFeeMaxValue, administrationFeePercentage, compound));
        }

        [TestCase(500000, 120, 5, 5000, 5, 12, 5303.28)]
        [TestCase(100000, 120, 5, 5000, 5, 12, 1060.66)]
        [TestCase(100000, 24, 5, 5000, 5, 12, 4387.14)]
        [TestCase(987456, 36, 13, 5000, 5, 12, 33271.30)]
        public void Can_Calculate_Monthly_Payment(decimal loanAmount, double durationInMonths, double annualInterestRate, decimal administrationFeeMaxValue, double administrationFeePercentage, int compound, decimal result)
        {
            var loanService = new Services.LoanCalculator(loanAmount, durationInMonths, annualInterestRate, administrationFeeMaxValue, administrationFeePercentage, compound);

            Assert.That(Math.Round(loanService.MonthlyPayment(), 2, MidpointRounding.AwayFromZero), Is.EqualTo(result));
        }

        [TestCase(500000, 120, 5, 5000, 99, 12, 5000)]
        [TestCase(30000, 120, 5, 5000, 1, 12, 300)]
        [TestCase(30000, 120, 5, 5000, 99, 12, 5000)]
        [TestCase(30000, 120, 5, 5000, 13, 12, 3900)]
        public void Can_Calculate_AdministrationFee(decimal loanAmount, double durationInMonths, double annualInterestRate, decimal administrationFeeMaxValue, double administrationFeePercentage, int compound, decimal result)
        {
            var loanService = new Services.LoanCalculator(loanAmount, durationInMonths, annualInterestRate, administrationFeeMaxValue, administrationFeePercentage, compound);

            Assert.That(Math.Round(loanService.AdministrationFee(), 2, MidpointRounding.AwayFromZero), Is.EqualTo(result));
        }

        [TestCase(500000, 120, 5, 5000, 99, 12, 136393.09)]
        [TestCase(100000, 120, 5, 5000, 99, 12, 27278.62)]
        [TestCase(1235897, 12, 13, 5000, 99, 12, 88746.44)]
        public void Can_Calculate_TotalInterestRateAmount(decimal loanAmount, double durationInMonths, double annualInterestRate, decimal administrationFeeMaxValue, double administrationFeePercentage, int compound, decimal result)
        {
            var loanService = new Services.LoanCalculator(loanAmount, durationInMonths, annualInterestRate, administrationFeeMaxValue, administrationFeePercentage, compound);

            Assert.That(Math.Round(loanService.TotalInterestRateAmount(), 2, MidpointRounding.AwayFromZero), Is.EqualTo(result));
        }

        [TestCase(500000, 120, 5, 5000, 99, 12, 641393.09)]
        [TestCase(100000, 120, 5, 5000, 99, 12, 132278.62)]
        [TestCase(1235897, 12, 13, 5000, 99, 12, 1329643.44)]
        public void Can_Calculate_TotalCost(decimal loanAmount, double durationInMonths, double annualInterestRate, decimal administrationFeeMaxValue, double administrationFeePercentage, int compound, decimal result)
        {
            var loanService = new Services.LoanCalculator(loanAmount, durationInMonths, annualInterestRate, administrationFeeMaxValue, administrationFeePercentage, compound);

            Assert.That(Math.Round(loanService.TotalCost(), 2, MidpointRounding.AwayFromZero), Is.EqualTo(result));
        }
    }
}
