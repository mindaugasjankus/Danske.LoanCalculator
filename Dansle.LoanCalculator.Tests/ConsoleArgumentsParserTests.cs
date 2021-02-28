using System;
using System.Collections.Generic;
using Danske.LoanCalculator.Models;
using Danske.LoanCalculator.Services;
using NUnit.Framework;

namespace Danske.LoanCalculator.Tests
{
    public class ConsoleArgumentsParserTests
    {
        private ConsoleArgumentsParser _consoleArgumentsParser;
        private AppConfiguration _appConfiguration;
        private List<string> _consoleArguments;

        [SetUp]
        public void SetUp()
        {
            _appConfiguration = new AppConfiguration
            {
                AdministrationFeeMaxValue = 5000,
                AdministrationFeePercentage = 5,
                AnnualInterestRate = 5,
                Compound = 12
            };

            _consoleArgumentsParser = new ConsoleArgumentsParser(_appConfiguration);

            _consoleArguments = new List<string>
            {
                "--LoanAmount",
                "1234567",
                "--DurationInMonths",
                "12",
                "--AnnualInterestRate",
                "15",
                "--AdministrationFeeMaxValue",
                "150",
                "--AdministrationFeePercentage",
                "12",
                "--Compound",
                "12",
            };
        }

        [Test]
        public void Can_Parse_All_Arguments()
        {
            var arguments = _consoleArgumentsParser.Parse(_consoleArguments.ToArray());

            Assert.That(arguments.LoanAmount, Is.EqualTo(1234567));
            Assert.That(arguments.DurationInMonths, Is.EqualTo(12));
            Assert.That(arguments.AnnualInterestRate, Is.EqualTo(15));
            Assert.That(arguments.AdministrationFeeMaxValue, Is.EqualTo(150));
            Assert.That(arguments.AdministrationFeePercentage, Is.EqualTo(12));
            Assert.That(arguments.Compound, Is.EqualTo(12));
        }


        [Test]
        public void Throws_If_DurationInMonths_Not_Provided()
        {
            var consoleArguments = new List<string>
            {
                "--LoanAmount",
                "1234567",
                "--AnnualInterestRate",
                "15",
                "--AdministrationFeeMaxValue",
                "150",
                "--AdministrationFeePercentage",
                "12",
                "--Compound",
                "12",
            };

            Assert.Throws<ArgumentException>(() => _consoleArgumentsParser.Parse(consoleArguments.ToArray()));
        }

        [Test]
        public void Throws_If_LoanAmount_Not_Provided()
        {
            var consoleArguments = new List<string>
            {
                "--DurationInMonths",
                "12",
                "--AnnualInterestRate",
                "15",
                "--AdministrationFeeMaxValue",
                "150",
                "--AdministrationFeePercentage",
                "12",
                "--Compound",
                "12",
            };

            Assert.Throws<ArgumentException>(() => _consoleArgumentsParser.Parse(consoleArguments.ToArray()));
        }

        [Test]
        public void Parses_Default_Values_If_Not_Provided()
        {
            var consoleArguments = new List<string>
            {
                "--LoanAmount",
                "1234567",
                "--DurationInMonths",
                "12"
            };

            var result = _consoleArgumentsParser.Parse(consoleArguments.ToArray());

            Assert.That(result.AdministrationFeeMaxValue, Is.EqualTo(_appConfiguration.AdministrationFeeMaxValue));
            Assert.That(result.AdministrationFeePercentage, Is.EqualTo(_appConfiguration.AdministrationFeePercentage));
            Assert.That(result.AnnualInterestRate, Is.EqualTo(_appConfiguration.AnnualInterestRate));
            Assert.That(result.Compound, Is.EqualTo(_appConfiguration.Compound));
        }
    }
}
