namespace WordsWebApplication.Services
{
    public class NumberToWordsConvertService : INumberToWordsConvertService
    {
        private static string[] units = { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
        private static string[] teens = { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
        private static string[] tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        private readonly ILogger<NumberToWordsConvertService> _logger;

        public NumberToWordsConvertService(ILogger<NumberToWordsConvertService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Converts a string representation of a number to its English words representation.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string Convert(string number)
        {
            _logger.LogTrace("Converting number to words: {Number}", number);

            // Basic input validation
            if (string.IsNullOrEmpty(number))
            {
                _logger.LogWarning("Empty input detected");
                return "Input is empty";
            }

            if (!decimal.TryParse(number, out decimal amount))
            {
                _logger.LogWarning("Failed to parse number: {Number}", number);
                return "Input is not a valid number";
            }

            // Check if the number is negative
            if (amount < 0)
            {
                _logger.LogWarning("Negative number detected: {Number}", number);
                return "Input is a negative number";
            }

            // Check if the number has more than two decimal places
            if (decimal.Round(amount, 2) != amount)
            {
                _logger.LogWarning("Number has more than two decimal places: {Number}", number);
                return "Input has more than two decimal places";
            }

            try
            {
                _logger.LogDebug("Converting amount to dollars and cents: {Amount}", amount);

                int dollars = (int)Math.Floor(amount);
                int cents = (int)Math.Round((amount - dollars) * 100);

                _logger.LogDebug("Dollars: {Dollars}, Cents: {Cents}", dollars, cents);

                string result = ConvertToWords(dollars) + (dollars == 1 ? " DOLLAR" : " DOLLARS");
                if (cents > 0)
                {
                    result += " AND " + ConvertToWords(cents) + (cents == 1 ? " CENT" : " CENTS");
                }

                _logger.LogDebug("Result: {Result}", result);

                return result;
            }
            catch (OverflowException ex) 
            {
                _logger.LogError(ex, "Overflow exception occurred while converting number: {Number}", number);
                return "Amount is too large";
            }
        }

        /// <summary>
        /// Converts an integer number into its English words representation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string ConvertToWords(int number)
        {
            // Handle the case for zero
            if (number == 0)
                return "ZERO";

            string words = "";

            // Handle millions
            if ((number / 1000000) > 0)
            {
                words += ConvertToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            // Handle thousands
            if ((number / 1000) > 0)
            {
                words += ConvertToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            // Handle hundreds
            if ((number / 100) > 0)
            {
                words += ConvertToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            // Handle units, teens, and tens
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";

                if (number < 10)
                    // Handle units
                    words += units[number];
                else if (number < 20)
                    // Handle tens
                    words += teens[number - 10];
                else
                {
                    // Handle units of tens
                    words += tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + units[number % 10];
                }
            }

            return words.Trim();
        }
    }
}
