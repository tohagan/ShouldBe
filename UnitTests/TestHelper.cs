using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShouldBe.UnitTests
{
    /// <summary>
    /// Used internally to assist in testing assertions
    /// </summary>
    [ShouldBeMethods]
    public static class TestHelper
    {
        /// <summary>
        /// Asserts that an action will fail with a specific error.
        /// Ignores whitespace (including newlines) when comparing errors.
        /// </summary>
        /// <param name="action">Action that is expected to fail.</param>
        /// <param name="expectedError">Expected error message</param>
        /// <example>
        /// 
        /// </example>
        public static void ShouldFailWithError(Action action, string expectedError)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var actualError = ex.Message;
                var strippedActual = actualError.StripWhitespace();
                var strippedExpected = expectedError.StripWhitespace();

                if (strippedActual.Contains(strippedExpected)) return;

                throw new AssertionException(
                    string.Format("Should fail with error\n'{0}:{1}'\n    but error was\n'{2}:{3}'\n",
                    strippedExpected.Length, strippedExpected, strippedActual.Length, strippedActual));
                    //strippedExpected.Length, strippedExpected, strippedActual.Length, strippedActual));
            }

            throw new AssertionException(
                string.Format("Should fail with error\n{0}\n    but it succeeded.",  expectedError));
        }

        /// <summary>
        /// Asserts that an action will fail with a specific error.
        /// Ignores whitespace (including newlines) when comparing errors.
        /// </summary>
        /// <param name="action">Action that is expected to fail.</param>
        /// <param name="expectedError">Expected error message</param>
        /// <example>
        /// 
        /// </example>
        public static async Task ShouldFailWithErrorAsync(Func<Task> action, string expectedError)
        {
            try
            {
                await action();
            }
            catch (AssertionException ex)
            {
                var actualError = ex.Message;
                var strippedActual = actualError.StripWhitespace();
                var strippedExpected = expectedError.StripWhitespace();

                if (strippedActual.Contains(strippedExpected)) return;

                throw new AssertionException(
                    string.Format("Should fail with error:\n{0}\n    but error was\n'{1}'\n", 
                        expectedError, actualError));
            }

            throw new AssertionException(
                string.Format("Should fail with error:\n{0}\n   but succeeded.", expectedError));
        }
    }
}