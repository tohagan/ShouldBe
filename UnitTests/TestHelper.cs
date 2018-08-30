using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

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
                ShouldBeMessage.TestHelper = true;
                action();
            }
            catch (AssertionException ex)
            {
                var actualError = ex.Message;
                var strippedActual = actualError.StripWhitespace();
                var strippedExpected = expectedError.StripWhitespace();

                if (strippedActual.Contains(strippedExpected))
                {
                    // TestContext.CurrentContext.Result. = new TestContext.ResultAdapter(new TestResult());

                    return;
                }

                ;

                Assert.Fail("Should fail with error\n'{0}:{1}'\n    but error was\n'{2}:{3}'\n",
                    expectedError.Length, expectedError, actualError.Length, actualError);
                //strippedExpected.Length, strippedExpected, strippedActual.Length, strippedActual));
            }
            finally
            {
                ShouldBeMessage.TestHelper = false;
            }

            Assert.Fail("Should fail with error\n{0}\n    but it succeeded.",  expectedError);
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

                Assert.Fail("Should fail with error:\n{0}\n    but error was\n'{1}'\n", expectedError, actualError);
            }

            Assert.Fail("Should fail with error:\n{0}\n   but succeeded.", expectedError);
        }
    }
}