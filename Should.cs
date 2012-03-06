using System;
using System.Diagnostics;
using System.Linq.Expressions;
using NUnit.Framework;

namespace ShouldBe
{
    /// <summary>
    /// Extension class for testing exceptions
    /// </summary>
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class Should
    {
         /// <summary>
        /// Test that an action throws an exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">Expected exception type</typeparam>
        /// <param name="action">Action that should throw exception</param>
        /// <returns>Exception thrown</returns>
        public static TException ShouldThrowException<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException e)
            {
                return e;
            }
            catch (Exception e)
            {
                ShouldBeMessage.Fail(e.GetType(), typeof(TException));
            }

            Assert.Fail("Should throw " + typeof(TException) + " but failed to throw an exception.");

            throw new AssertionException("Never thrown but keeps C# compiler happy");
        }

        /// <summary>
        /// Test that an action throws an exception of 
        /// type <typeparamref name="TException"/> that 
        /// contains a message substring.
        /// </summary>
        /// <typeparam name="TException">Expected exception type</typeparam>
        /// <param name="action">Action that should throw exception</param>
        /// <param name="expected">Expected exception</param>
        /// <returns>Exception thrown</returns>
        public static TException ShouldThrowExceptionContaining<TException>(Action action, string containsMessage)
            where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException e)
            {
                if (e.Message.Contains(containsMessage)) 
                {
                    return e;
                }

                ShouldBeMessage.Fail(e.Message, containsMessage);
            }
            catch (Exception e)
            {
                ShouldBeMessage.Fail(e.GetType(), typeof(TException));
            }

            Assert.Fail("Should throw " + typeof(TException) + " but failed to throw an exception.");

            throw new AssertionException("Never thrown but keeps C# compiler happy");
        }
    }
}