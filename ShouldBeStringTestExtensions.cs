using System.Diagnostics;
using System.Text.RegularExpressions;
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

            if (strippedActual != strippedExpected)
            {
                ShouldBeMessage.Fail(actual, expected);
            }

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
            if (!value.StartsWith(startsWith))
            {
                ShouldBeMessage.Fail(value, startsWith);
            }

            return value;
        }

        /// <summary>
        /// Asserts that a string ends with the <paramref name="endsWith"/> substring.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endsWith"></param>
        /// <returns></returns>
        public static string ShouldEndWith(this string value, string endsWith)
        {
            if (!value.EndsWith(endsWith))
            {
                ShouldBeMessage.Fail(value, endsWith);
            }

            return value;
        }

        /// <summary>
        /// Asserts that a string contains a given substring.  Matching is case sensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static string ShouldContain(this string value, string contains)
        {
            if (!value.Contains(contains))
            {
                ShouldBeMessage.Fail(value, contains);
            }

            return value;
        }

        /// <summary>
        /// Asserts that a string does not contains a given substring.  Matching is case sensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="notContains"></param>
        /// <returns></returns>
        public static string ShouldNotContain(this string value, string notContains)
        {
            if (value.Contains(notContains))
            {
                ShouldBeMessage.Fail(value, notContains);
            }

            return value;
        }

        /// <summary>
        /// Asserts that a string matches a regular expression.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string ShouldMatch(this string value, string regexPattern)
        {
            if (!Regex.IsMatch(value, regexPattern))
            {
                ShouldBeMessage.Fail(value, regexPattern);
            }

            return value;
        }

        /// <summary>
        /// Asserts that a string does not match a regular expression.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string ShouldNotMatch(this string value, string regexPattern)
        {
            if (Regex.IsMatch(value, regexPattern))
            {
                ShouldBeMessage.Fail(value, regexPattern);
            }

            return value;
        }
    }
}