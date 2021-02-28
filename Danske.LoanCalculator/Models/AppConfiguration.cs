namespace Danske.LoanCalculator.Models
{
    public class AppConfiguration
    {
        public double AnnualInterestRate { get; set; }
        public double AdministrationFeeMaxValue { get; set; }
        public double AdministrationFeePercentage { get; set; }
        public int Compound { get; set; }
    }
}
