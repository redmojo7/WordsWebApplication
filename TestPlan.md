# Number to Words Converter: Test Plan

## 1. Unit Tests

### NumberToWordsConverter Class
- **Integer Numbers**: Test conversion of numbers like 0, 1, 10, 100, 1000.
- **Decimal Numbers**: Test conversion of numbers like 0.01, 1.23, 999.99.
- **Large Numbers**: Test conversion of numbers in millions and billions.
- **Negative Numbers**: Test handling of negative inputs.
- **Invalid Inputs**: Test handling of non-numeric strings, null, and empty strings.

## 2. Integration Tests

### ConvertController
- **Convert Action**: Test with various valid and invalid inputs.
- **JSON Responses**: Verify that responses are correctly formatted in JSON.

## 3. UI Tests

### Web Interface
- **Input Validation**: Test validation for empty inputs and non-numeric values.
- **Conversion Button**: Test functionality of the conversion button.
- **Result Display**: Verify the correct display of conversion results.
- **Responsiveness**: Test layout and functionality across different screen sizes.

## 4. Edge Cases and Error Handling

- **Very Large Numbers**: Test with numbers close to `decimal.MaxValue`.
- **Very Small Numbers**: Test with numbers close to `decimal.MinValue`.
- **Many Decimal Places**: Test with numbers that have more than two decimal places.
- **Invalid Inputs**: Verify error messages for invalid inputs.


## 5. Test Data

| Input    | Type        | Expected Output                                              |
|----------|-------------|--------------------------------------------------------------|
| 0        | Integer     | ZERO DOLLARS                                                 |
| 1        | Integer     | ONE DOLLAR                                                   |
| 25       | Integer     | TWENTY-FIVE DOLLARS                                          |
| 100      | Integer     | ONE HUNDRED DOLLARS                                          |
| 1000     | Integer     | ONE THOUSAND DOLLARS                                         |
| 1000000  | Integer     | ONE MILLION DOLLARS                                          |
| 1234567  | Integer     | ONE MILLION TWO HUNDRED AND THIRTY-FOUR THOUSAND FIVE HUNDRED AND SIXTY-SEVEN DOLLARS |
| 0.01     | Decimal    | ZERO DOLLARS AND ONE CENT                                    |
| 1.23     | Decimal    | ONE DOLLAR AND TWENTY-THREE CENTS                            |
| 123.45   | Decimal    | ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS    |
| 1000.01  | Decimal    | ONE THOUSAND DOLLARS AND ONE CENT                            |
| 999999.99| Decimal     | NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS |
| 1234567890.12 | Decimal | ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR MILLION FIVE HUNDRED AND SIXTY-SEVEN THOUSAND EIGHT HUNDRED AND NINETY DOLLARS AND TWELVE CENTS |
| ""       | Invalid      | Input is empty                                               |
| abc      | Invalid    | Input is not a valid number                                  |
| 1be2n    | Invalid    | Input is not a valid number                                  |
| -123     | Negative   | Input is a negative number                                   |
| 123.456  | Invalid     | Input has more than two decimal places                       |
| 12345678901 | Large  | Amount is too large                                       |
| 11111111111.12 | Large | Amount is too large                                    |

## 6. Running Tests

To run tests from the command line:

```
dotnet test UnitTests/UnitTests.csproj
```