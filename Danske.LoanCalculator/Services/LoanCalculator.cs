using System;

namespace Danske.LoanCalculator.Services
{
    public class LoanCalculator : ILoanCalculator
    {
        public decimal LoanAmount { get; }
        public double DurationInMonths { get; }
        public double AnnualInterestRate { get; }
        public decimal AdministrationFeeMaxValue { get; }
        public double AdministrationFeePercentage { get; }
        public int Compound { get; }

        public LoanCalculator(decimal loanAmount, double durationInMonths, double annualInterestRate, decimal administrationFeeMaxValue, double administrationFeePercentage, int compound)
        {
            if (loanAmount <= 0)
                throw new ArgumentException($"{nameof(loanAmount)} - should be more then 0");
            if(durationInMonths <= 0)
                throw new ArgumentException($"{nameof(durationInMonths)} - should be more then 0");
            if(administrationFeeMaxValue < 0)
                throw new ArgumentException($"{nameof(administrationFeeMaxValue)} - should be more or equal to 0");
            if (administrationFeePercentage < 0)
                throw new ArgumentException($"{nameof(administrationFeePercentage)} - should be more or equal to 0");
            if(administrationFeePercentage > 100)
                throw new ArgumentException($"{nameof(administrationFeePercentage)} - can't be more then 100%");
            if(compound <= 0)
                throw new ArgumentException($"{nameof(compound)} - should be more then 0");
            if(annualInterestRate <= 0)
                throw new ArgumentException($"{nameof(annualInterestRate)} - should be more then 0");

            LoanAmount = loanAmount;
            DurationInMonths = durationInMonths;
            AnnualInterestRate = annualInterestRate;
            AdministrationFeeMaxValue = administrationFeeMaxValue;
            AdministrationFeePercentage = administrationFeePercentage;
            Compound = compound;
        }

        public decimal MonthlyPayment()
        {
            var monthlyRate = AnnualInterestRate / 100 / Compound;
            var pow = Math.Pow(1 + monthlyRate, DurationInMonths);
            var discountFactor = (pow - 1) / (monthlyRate * pow);
            var monthlyPayment = LoanAmount / (decimal)discountFactor;

            return monthlyPayment;
        }

        public decimal AdministrationFee()
        {
            var percentageFee = LoanAmount / 100 * (decimal)AdministrationFeePercentage;
            return percentageFee < AdministrationFeeMaxValue ? percentageFee : AdministrationFeeMaxValue;
        }

        public decimal TotalInterestRateAmount()
        {
            return MonthlyPayment() * (decimal)DurationInMonths - LoanAmount;
        }

        public decimal TotalCost()
        {
            return MonthlyPayment() * (decimal) DurationInMonths + AdministrationFee();
        }

        public decimal Apr()
        {
            return ((TotalInterestRateAmount() + AdministrationFee()) / LoanAmount) / 3650 * 365 * 100;
        }
    }
}