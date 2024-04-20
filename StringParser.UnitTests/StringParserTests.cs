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
    [TestCase("start: $tart", "start: £tart")] // fails  
    [TestCase("$$$$$ £££ ££ $$", "£ £ £ £")]
    [TestCase("No change here", "No change here")]
    [TestCase("$$$ becomes £££", "£ becomes £")] 
    [TestCase("$1 is < than £2", "£1 is < than £2")] // fails
    public void Parse_ShouldReplaceDollarSignsWithPoundSigns(string input, string expected)
    {
        // Arrange
        var stringParser = new StringProcessor();

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected, $"Input '{input ?? "null"}' should be transformed to '{expected}'.");
    }
}
