namespace Danske.LoanCalculator.Services
{
    public interface ILoanCalculator
    {
        public decimal LoanAmount { get; }
        public double DurationInMonths { get; }
        public double AnnualInterestRate { get; }
        public decimal AdministrationFeeMaxValue { get; }
        public double AdministrationFeePercentage { get; }
        public int Compound { get; }

        decimal AdministrationFee();
        decimal TotalInterestRateAmount();
        decimal TotalCost();
        decimal Apr();
    }
}