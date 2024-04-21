# QA Automation Tester

## Notes:

My tests check the following:

- the output string is truncated to 15 chars
- the duplicate characters are reduced to 1 of the same case
- $ signs are replaced with £ signs
- underscores and 4s are removed
- output is never null or empty
- the output collection is never null

## Findings

The following tests fail:

- output is never null or empty
- the output collection is never null

## Possible solutions

For "output is never null or empty":

- One approach could be modify the "Parse" method to return a default string, such as 'default', when faced with null or empty inputs. Alternatively, we could consider a placeholder string that indicates the input was invalid.

For "the output collection is never null":

- `Parse` method throws `NullReferenceException` with null input. A potential solution could involve modifying the `Parse` method to check for null inputs and return an empty collection in these cases.

## The Brief

A developer was given the following task:

> Write C# code to process a collection of string values which are passed to a method which returns a collection of processed strings. The input strings may be any length and can contain any character. Output strings must not be null or empty string, should be truncated to max length of 15 chars, and contiguous duplicate characters in the same case should be reduced to a single character in the same case. Dollar sign ($) should be replaced with a pound sign (£), and underscores (\_) and number 4 should be removed. Code should be test-driven, efficient, re-usable and loosely coupled. The returned collection must not be null.
>
> Example input string: AAAc91%cWwWkLq$1ci3_848v3d\_\_K

Your task is to write a set of tests around the developers work to prove it has been suitably done to the given specifications.

## The Solution

The solution is a set of C# library projects. These projects contain the developers implementation and a placeholder project for unit tests.

The unit test project has the following NuGet packages already installed to facilitate writing tests:

- NUnit
- Moq
- Shouldly

The solution has no runnable component (e.g. cannot press F5) and is expected to be run through the Test Runner.

## Submission

Submission of your solution should be as a link to a GitHub, or other Git-based, repo that we are able to access and clone for review.
