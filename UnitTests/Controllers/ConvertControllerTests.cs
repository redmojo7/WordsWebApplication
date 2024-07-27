using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WordsWebApplication.Controllers;
using WordsWebApplication.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ConvertControllerTests
    {
        private ConvertController _controller;
        private Mock<INumberToWordsConvertService> _numberToWordsConvertService;
        private Mock<ILogger<ConvertController>> _logger;

        [SetUp]
        public void Setup()
        {
            _numberToWordsConvertService = new Mock<INumberToWordsConvertService>();
            _logger = new Mock<ILogger<ConvertController>>();
            _controller = new ConvertController(_numberToWordsConvertService.Object, _logger.Object);
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
        public void TestConvertController_Convert_ValidInput(string input, string expectedOutput)
        {
            // Arrange
            SetUpMockService(input, expectedOutput);

            // Act
            var result = GetJsonResultFromController(input);

            // Assert
            AssertJsonResult(result, expectedOutput);
        }

        [TestCase("", "Input is empty")]
        [TestCase("abc", "Input is not a valid number")]
        [TestCase("1be2n", "Input is not a valid number")]
        [TestCase("-123", "Input is a negative number")]
        [TestCase("123.456", "Input has more than two decimal places")]
        public void TestConvertController_Convert_InvalidInput(string input, string expectedOutput)
        {
            // Arrange
            SetUpMockService(input, expectedOutput);

            // Act
            var result = GetJsonResultFromController(input);

            // Assert
            AssertJsonResult(result, expectedOutput);
        }

        [TestCase("12345678901", "Amount is too large")]
        [TestCase("11111111111.12", "Amount is too large")]
        public void TestConvertController_Convert_LargeNumbers(string input, string expectedOutput)
        {
            // Arrange
            SetUpMockService(input, expectedOutput);

            // Act
            var result = GetJsonResultFromController(input);

            // Assert
            AssertJsonResult(result, expectedOutput);
        }

        private void SetUpMockService(string input, string expectedOutput)
        {
            _numberToWordsConvertService.Setup(s => s.Convert(input)).Returns(expectedOutput);
        }

        private JsonResult GetJsonResultFromController(string input)
        {
            var result = _controller.Convert(input) as JsonResult;
            if (result == null)
            {
                throw new InvalidOperationException("Expected JsonResult, but got null.");
            }
            return result;
        }

        private void AssertJsonResult(JsonResult result, string expectedOutput)
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.Not.Null);

            var resultValue = JObject.FromObject(result.Value);
            Assert.That(resultValue, Is.Not.Null);
            Assert.That(resultValue["result"], Is.Not.Null);
            Assert.That(resultValue["result"].ToString(), Is.EqualTo(expectedOutput));
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }
    }
}
