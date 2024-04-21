using NUnit.Framework;
using Shouldly;
using StringParser.Services;

namespace StringParser.UnitTests;

[TestFixture]
public class StringParserTests
{
    // Test cases to ensure the output string is truncated to 15 chars
    [TestCase("12345678901234567890", "123567890123567")]
    [TestCase("Short", "Short")]
    [TestCase("Exactly15qwerty", "Exactly15qwerty")]
    [TestCase("long Write C# code to process a collection of string values which are passed to a method which returns a collection of processed strings.", "long Write C# c")] 
    
    public void ShouldTruncateTo15Chars(string input, string expected)
    {
        // Arrange
        var stringParser = new StringProcessor(); 

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected, $"Input '{input}' should be truncated to '{expected}'.");
    }

      // Test case to ensure the duplicate characters are reduced
      [Test]
    public void ReduceContiguousDuplicateCharacters()
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

    // Test cases to ensure the replacement of $ sign with £ sign
    [TestCase("mid: e$sential", "mid: e£sential")]
    [TestCase("end: pqr$", "end: pqr£")]
    [TestCase("start: $tart", "start: £tart")]   
    [TestCase("$$$$$ £££ ££ $$", "£ £ £ £")]
    [TestCase("No change here", "No change here")]
    [TestCase("$$$ becomes £££", "£ becomes £")] 
    [TestCase("$1 is < than £2", "£1 is < than £2")] 
    public void ShouldReplaceDollarSignsWithPoundSigns(string input, string expected)
    {
        // Arrange
        var stringParser = new StringProcessor();

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldBe(expected, $"Input '{input ?? "null"}' should be transformed to '{expected}'.");
    }

    // Test cases to ensure the removal of underscores and number 4
    [TestCase("un_der_sco_res", "underscores")]
    [TestCase("one4andtwo4and", "oneandtwoand")]
    [TestCase("Com4bi_ne", "Combine")]

    public void ShouldRemoveUnderscoresAndNumber4(string input, string expected)
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
    public void ShouldNotReturnNullOrEmptyStrings(string input)
    {
        // Arrange
        var stringParser = new StringProcessor(); 

        // Act
        var result = stringParser.Parse(input);

        // Assert
        result.ShouldNotBeNullOrEmpty();
    }

    // Test case to ensure the output collection is never null
     [Test]
    public void CollectionShouldNotBeNull()
    {
        // Arrange
        var stringCollectionParser = new StringCollectionParser(new StringProcessor()); 
        
        var testCases = new List<IEnumerable<string>>
        {
            new List<string> { "test", "another test" }, 
            new List<string>(), 
            new List<string> { null, "" }, 
            null 
        };

        foreach (var testCase in testCases)
        {
            // Act
            var result = stringCollectionParser.Parse(testCase);

            // Assert
            result.ShouldNotBeNull("The returned collection should not be null regardless of input.");
        }
    }
}
