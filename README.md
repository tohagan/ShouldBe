https://github.com/tohagan/ShouldBe

### About

* A cleaner *faster* way to code .NET / C# / VB.NET unit tests.
* Wrapper API for NUnit so it integrates well with existing NUnit tools like NUnit Runners and Resharper.
* **License**: BSD Open Source  (See LICENSE.txt)

### Similar Libraries

*http://shouldly.github.com*

ShouldBe is based on the [Shouldly](http://shouldly.github.com) library. Kudos to [Xerxes Battiwalla](https://github.com/xerxesb) for the original ideas and open source.

*http://should.codeplex.com/*

At first glance this API looks more fully featured but we suspect most of its features can be covered by LINQ or other .NET APIs to precompute values prior to assertion testing. So for us a smaller and simpler API was the preferred approach. Also last time I looked it lacked some of the enumeration methods we've added to ShouldBe. 

This lib inspired us to modify ShouldBe to be fluent!

### How to reference ShouldBe in your VS.NET project:

<Reference Include="ShouldBe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8cdd7c9ef40df8f9, processorArchitecture=MSIL" />


### Why use ShouldBe?

This is the old *Assert* way: 

    Assert.That(contestant.Points, Is.EqualTo(1337));

For your troubles, you get this message, when it fails:

    Expected 1337 but was 0

How it **Should** be:

    contestant.Points.ShouldBe(1337);

Which is just syntax, so far, but check out the message when it fails:

    contestant.Points should be 1337 but was 0

It might be easy to underestimate how useful this is. Another example, side by side:

    Assert.That(map.IndexOfValue("boo"), Is.EqualTo(2));    // -> Expected 2 but was 1
    map.IndexOfValue("boo").ShouldBe(2);                    // -> map.IndexOfValue("boo") should be 2 but was 1

It can also compare enumerations:

    new[] { 2, 7, 10, 9 }.ShouldBeTheSet(new[] { 2, 3, 10, 15, 9 }

Outputs:

    new[] { 2, 7, 10, 9 } 
      should be the set 
    [2, 3, 9, 10, 15] 
       but was 
    [2, 7, 9, 10] 
       difference 
    [2, *7*, 9, 10, *] 
       missing 
    [3, 15] 
      not expected 
    [7]

If you want to check that a particular call does/does not throw an exception, it's as simple as:

    Should.ShouldThrowException<ArgumentOutOfRangeException>(() => widget.Twist(-1));

### Key Features

* Uses type safe extension methods that wrapper NUnit assertions in order to write cleaner more readable Unit Tests
* Faster to code with Intellisense in VS.NET or other .NET IDE tools.
  * All methods all start with 'Should' to make them group together 
    in intellisense lists so you can quickly find the method you need.
* Integrates with existing NUnit runner tools and 3rd partly tools like Resharper.
  * NOTE: Removed support for additional unit test libs (found in Shouldly) 
    as this just complicated our build environment.
* Where feasible, wrapper methods are intentionally typesafe (in contrast to many NUnit methods).
   * In most cases, actual and expected argument types can be inferred and checked by the compiler. 
     The type does  not need to be declared.
   * Enforcing type safety typically avoids more unit test bugs and improves the fluent API by 
     knowing the return type. 
* Helper methods for creating Rhino mock instances (See MockMe class)
* In Debug build, uses the call stack to read unit test source code (if available) when reporting failure messages. See "contestant.Points" example above.
* Uses the name of the assertion method to construct the assertion failure message. 
  The method named "ShouldBeTheSet()" converts to "should be the set" in the final message.

### Changes to the original Shouldly library:

  * Renamed library to ShouldBe to distinguish it from original library.
  * Removed dependency on other unit test frameworks (We only needed NUnit).
    * Removed the copy of NUnit source code and other 3rd party libs.
  * Added many additional assertion methods (See complete list below).
    * e.g. Improved assertions on Sequences and Sets of values.
  * Refactored internal unit testing.
  * Added numerous new internal unit tests (both success and failure messages are now unit tested)
  * Refactored assertion reporting
    * Replaced all 'throw new ChuckAWobly(msg)' with NUnit's 'Assert.Fail(msg)' calls 
	  to support integratation with NUnit compatible tools like [Resharper](http://www.jetbrains.com/resharper)
    * Reporting works better when running unit tests with non-debug builds and when source is not available.
  * Fixed regex bug in StripWhiteSpace() extention method.
  * Fixed bug in the Should.FailWithError() that caused some unit tests in ShouldBe.UnitTest project to always succeed.
  * Improved internal unit test coverage.
  * Added missing InstanceOf, AssignableFrom, AssignableTo type constrain tests (See full list of methods below)
  * Added test that source file exists before using it to report a failure 
  * All ShouldBe methods return 'actual' so assertions can be chained as a Fluent API.
  * Renamed mock 'Create' class to 'MockMe' to reduce the risk of naming collisions.
    * Added a missing methods to support additional Rhino mock types.
  * Improved enumeration failure messages to show "missing" and "not expected" lists.
    * Very useful when debugging faults in larger lists.
	* Truncates large lists

*Breaking changes from Shouldly lib:*

  * Removed case insensitive matching in ShouldStartWith() and ShouldContain() methods.
    * A risky design in our view that could cause unit tests to unexpectedly succeed 
	  since it's not obvious from the method name that the match is case insensitive.
  * Renamed Should.Throw<T>() to be Should.ThrowException<T>()
    * Works better with the new method: Should.ThrowExceptionContaining()

### Why not just fork Shouldly?
We loved the great ideas in Shouldly but in it's current form 
we found it needed too much adaptation for use on our projects.

Main reasons included:

  * Too many external component dependencies for our needs.  (We only needed NUnit)
  * Our breaking changes that might not align with where Shoudly was heading or existing projects that use it.
  * Refactoring of the reporting system (we kept the key concept).
  * Refactoring of internal unit tests.
  * More freedom for us to adapt/extend the API (e.g. fluent API support)

### To Do
  * Additional unit test cases are needed to for missing unit tests (using a coverage analyser).
  * AssertAwesomely() extension method might make use of NUnit's constraint.WriteMessageTo() to compose better fail messages for some cases.
  * Add missing comments on all public methods.
  * Add missing NUnit.Framework.Is constraints:
    * e.g. Is.InRange(IComparable from, IComparable to)
  * Reporting for some conditions (ShouldBeTheSet() and ShouldHaveUniqueKeys() could be improved.  
    * AssertAwesomely().ToString() needs refactoring.  Replace with static methods?
  * Review [NUnit collection methods](http://www.nunit.org/index.php?p=collectionAssert&r=2.5.9): 
    * Implement: IEnumerable<T> ShouldHaveUniqueValues<T>(this IEnumerable<T> actual) - NUnit: AllItemsAreUnique
    * Implement: IEnumerable<T> ShouldHaveOrderedValues<T>(this IEnumerable<T> actual) - NUnit: IsOrdered

## Assertion methods

### Equality
    ShouldBe<T>
    ShouldBe<T> (with tolerance)
    ShouldNotBe<T>
    ShouldBeGreaterThan(OrEqualTo)<T>
    ShouldBeLessThan(OrEqualTo)<T>
    ShouldBeAtLeast<T> (new)
    ShouldBeAtMost<T> (new)
    ShouldBeSameAs<T>

### Types
    ShouldBeTypeOf<T>
    ShouldNotBeTypeOf<T>
    ShouldBeInstanceOf<T> (new)
    ShouldNotBeInstanceOf<T> (new)
    ShouldBeAssignableFrom<T> (new)
    ShouldNotAssignableFrom<T> (new)
    ShouldBeAssignableTo<T> (new)
    ShouldNotAssignableTo<T> (new)

### Enumerable
    ShouldBeEmpty
    ShouldNotBeEmpty
    ShouldBeTheSequence (new)
    ShouldBeTheSet (new)
    ShouldHaveUniqueKeys (new)
    ShouldBeASubsetOf (new)
    ShouldContainTheSubset (new)
    ShouldContain
    ShouldContain(predicate)
    ShouldContain(tolerance)  (new)
    ShouldNotContain
    ShouldNotContain(predicate)
    ShouldNotContain(tolerance)  (new) 
    ShouldBeAscending (new)       
    ShouldBeAscending(keySelector) (new)       
    ShouldBeDescending (new)       
    ShouldBeDescending(keySelector) (new)       

### String
    ShouldBeCloseTo
    ShouldStartWith
    ShouldEndWith
    ShouldContain
    ShouldNotContain
    ShouldMatch
    ShouldNotMatch (new)

### Dictionary
    ShouldContainKey
    ShouldContainKeyAndValue
    ShouldNotContainKey
    ShouldNotContainKeyAndValue
    ShouldNotContainValueForKey (new)

### Exceptions
    Should.ThrowException<T>(Action)
    Should.ThrowExceptionContaining<T>(Action)  (new)

### Failed  - Useful in writing your own custom assertions methods.
    Failed<T>(T expected)   (new)
    Failed<T>(T actual, T expected)   (new)
    Failed<T>(IEnumerable<T> actual, expected)   (new)
