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
}
