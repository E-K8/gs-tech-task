using NUnit.Framework;
using Shouldly;
using StringParser.Services;

namespace StringParser.UnitTests;

[TestFixture]
public class StringParserTests
{
    [Test]
    public void Test()
    {
        1.ShouldBe(1);
    }

      [Test]
    public void StringIsTruncatedTo15Chars_WhenLongerThan15Chars()
    {
        // Arrange
        var stringParser = new StringProcessor();
        var input = "WhetherTheWeatherIsGood"; 
        var expected = "WhetherTheWeath"; 

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected);
    }

      [Test]
    public void Parse_ReduceContiguousDuplicateCharacters()
    {
        // Arrange
        var stringParser = new StringProcessor(); 
        var input = "AAAbbCCCCddDDD";
        var expected = "AbCdD";

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected, "Contiguous duplicate characters should be reduced to a single character in the same case.");
    }
 
    [TestCase("mid: e$sential", "mid: e£sential")]
    [TestCase("end: pqr$", "end: pqr£")]
    [TestCase("start: $tart", "start: £tart")]   
    [TestCase("$$$$$ £££ ££ $$", "£ £ £ £")]
    [TestCase("No change here", "No change here")]
    [TestCase("$$$ becomes £££", "£ becomes £")] 
    [TestCase("$1 is < than £2", "£1 is < than £2")] 
    public void Parse_ShouldReplaceDollarSignsWithPoundSigns(string input, string expected)
    {
        // Arrange
        var stringParser = new StringProcessor();

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected, $"Input '{input ?? "null"}' should be transformed to '{expected}'.");
    }
 
    [TestCase("un_der_sco_res", "underscores")]
    [TestCase("one4andtwo4and", "oneandtwoand")]
    [TestCase("Com4bi_ne", "Combine")]
    [TestCase("____4___4___", "")] 
    [TestCase("4444", "")] 
    [TestCase("____", "")]

    public void Parse_ShouldRemoveUnderscoresAndNumber4(string input, string expected)
    {
        // Arrange
        var stringParser = new StringProcessor(); 

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected, $"Input '{input ?? "null"}' should transform to '{expected}' with underscores and number 4 removed.");
    }

     // Test cases to ensure the output is never null or empty
    [TestCase("____")] 
    [TestCase("4444")] 
    [TestCase("")]
    [TestCase(null)]    
    public void Parse_ShouldNotReturnNullOrEmptyStrings(string input)
    {
        // Arrange
        var stringParser = new StringProcessor(); 

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldNotBeNullOrEmpty();
    }
}
