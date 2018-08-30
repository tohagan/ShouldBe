using System.Diagnostics;
using NUnit.Framework;

namespace ShouldBe
{
    /// <summary/>
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static string ShouldBeCloseTo(this string actual, string expected)
        {
            var strippedActual = actual == null ? "null" : actual.Quotify().StripWhitespace();
            var strippedExpected = expected == null ? "null" : expected.Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(Is.EqualTo(strippedExpected), actual, expected);
            return actual;
        }

        /// <summary>
        /// Asserts that a string starts with the <paramref name="startsWith"/> substring.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public static string ShouldStartWith(this string value, string startsWith)
        {
            return value.AssertAwesomely(Does.StartWith(startsWith), value, startsWith);
        }

        /// <summary>
        /// Asserts that a string ends with the <paramref name="endsWith"/> substring.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endsWith"></param>
        /// <returns></returns>
        public static string ShouldEndWith(this string value, string endsWith)
        {
            return value.AssertAwesomely(Does.EndWith(endsWith), value, endsWith);
        }

        /// <summary>
        /// Asserts that a string contains a given substring.  Matching is case sensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static string ShouldContain(this string value, string contains)
        {
            return value.AssertAwesomely(Does.Contain(contains), value, contains);
        }

        /// <summary>
        /// Asserts that a string does not contains a given substring.  Matching is case sensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="notContains"></param>
        /// <returns></returns>
        public static string ShouldNotContain(this string value, string notContains)
        {
            return value.AssertAwesomely(Does.Not.Contain(notContains), value, notContains);
        }

        /// <summary>
        /// Asserts that a string matches a regular expression.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string ShouldMatch(this string actual, string regexPattern)
        {
            return actual.AssertAwesomely(Does.Match(regexPattern), actual, regexPattern);
        }

        /// <summary>
        /// Asserts that a string does not match a regular expression.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string ShouldNotMatch(this string actual, string regexPattern)
        {
            return actual.AssertAwesomely(Does.Not.Match(regexPattern), actual, regexPattern);
        }
    }
}