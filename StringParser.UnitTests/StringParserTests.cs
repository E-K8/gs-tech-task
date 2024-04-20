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

   
}
