using WordsWebApplication.Services;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

namespace UnitTests.Services
{
    [TestFixture]
    public class NumberToWordsConvertServiceTest
    {
        private INumberToWordsConvertService _numberToWordsConvertService;
        private ILogger<NumberToWordsConvertService> _logger;

        [SetUp]
        public void Setup()
        {
            // Create a LoggerFactory and Logger
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Logs to console; configure as needed
            });

            _logger = loggerFactory.CreateLogger<NumberToWordsConvertService>();

            // Initialize the service with the logger
            _numberToWordsConvertService = new NumberToWordsConvertService(_logger);
        }

        [TestCase("0", "ZERO DOLLARS")]
        [TestCase("1", "ONE DOLLAR")]
        [TestCase("25", "TWENTY-FIVE DOLLARS")]
        [TestCase("100", "ONE HUNDRED DOLLARS")]
        [TestCase("1000", "ONE THOUSAND DOLLARS")]
        [TestCase("1000000", "ONE MILLION DOLLARS")]
        [TestCase("1234567", "ONE MILLION TWO HUNDRED AND THIRTY-FOUR THOUSAND FIVE HUNDRED AND SIXTY-SEVEN DOLLARS")]
        [TestCase("0.01", "ZERO DOLLARS AND ONE CENT")]
        [TestCase("1.23", "ONE DOLLAR AND TWENTY-THREE CENTS")]
        [TestCase("123.45", "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]
        [TestCase("1000.01", "ONE THOUSAND DOLLARS AND ONE CENT")]
        [TestCase("999999.99", "NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS")]
        [TestCase("1234567890.12", "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR MILLION FIVE HUNDRED AND SIXTY-SEVEN THOUSAND EIGHT HUNDRED AND NINETY DOLLARS AND TWELVE CENTS")]
        public void Convert_ValidNumbers_ReturnsExpectedResults(string input, string expectedOutput)
        {
            // Act
            var result = _numberToWordsConvertService.Convert(input);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("", "Input is empty")]
        [TestCase("abc", "Input is not a valid number")]
        [TestCase("1be2n", "Input is not a valid number")]
        [TestCase("-123", "Input is a negative number")]
        [TestCase("123.456", "Input has more than two decimal places")]
        public void Convert_InvalidInput(string input, string expectedOutput)
        {
            // Act
            var result = _numberToWordsConvertService.Convert(input);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("12345678901")]
        [TestCase("11111111111.12")]
        public void Convert_LargeNumbers(string input)
        {
            // Act
            var result = _numberToWordsConvertService.Convert(input);

            // Assert
            Assert.That(result, Is.EqualTo("Amount is too large"));
        }
    }
}
