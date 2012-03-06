using System.Diagnostics;
using NUnit.Framework;

namespace ShouldBe
{
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
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public static string ShouldStartWith(this string value, string startsWith)
        {
            return value.AssertAwesomely(Is.StringStarting(startsWith), value, startsWith);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endsWith"></param>
        /// <returns></returns>
        public static string ShouldEndWith(this string value, string endsWith)
        {
            return value.AssertAwesomely(Is.StringEnding(endsWith), value, endsWith);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static string ShouldContain(this string value, string contains)
        {
            return value.AssertAwesomely(Is.StringContaining(contains), value, contains);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="notContains"></param>
        /// <returns></returns>
        public static string ShouldNotContain(this string value, string notContains)
        {
            return value.AssertAwesomely(Is.Not.StringContaining(notContains), value, notContains);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string ShouldMatch(this string actual, string regexPattern)
        {
            return actual.AssertAwesomely(Is.StringMatching(regexPattern), actual, regexPattern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string ShouldNotMatch(this string actual, string regexPattern)
        {
            return actual.AssertAwesomely(Is.Not.StringMatching(regexPattern), actual, regexPattern);
        }
    }
}