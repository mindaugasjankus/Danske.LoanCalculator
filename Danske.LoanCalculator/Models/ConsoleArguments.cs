namespace Danske.LoanCalculator.Models
{
    public class ConsoleArguments
    {
        public double LoanAmount { get; set; }
        public double DurationInMonths { get; set; }
        public double? AnnualInterestRate { get; set; }
        public double? AdministrationFeeMaxValue { get; set; }
        public double? AdministrationFeePercentage { get; set; }
        public int? Compound { get; set; }
    }
}
