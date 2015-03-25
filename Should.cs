using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading.Tasks;
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
        static readonly Exception fakeException = new AssertionException("Never thrown but keeps C# compiler happy");
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

            throw fakeException;
        }

        /// <summary>
        /// Test that an action throws an exception of 
        /// type <typeparamref name="TException"/> that 
        /// contains a message substring.
        /// </summary>
        /// <typeparam name="TException">Expected exception type</typeparam>
        /// <param name="action">Action that should throw exception</param>
        /// <param name="containsMessage"></param>
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

            throw fakeException;
        }

#if NET_45_OR_GREATER
        /// <summary>
        /// Test that an action throws an exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">Expected exception type</typeparam>
        /// <param name="action">Action that should throw exception</param>
        /// <returns>Exception thrown</returns>
        public static async Task<TException> ShouldThrowExceptionAsync<TException>(Func<Task> action)
            where TException : Exception
        {
            try
            {
                await action();
            }
            catch (TException e)
            {
                return e;
            }
            catch (Exception e)
            {
                ShouldBeMessage.Fail(e.GetType(), typeof(TException), "Should throw exception");
            }

            Assert.Fail("Should throw " + typeof(TException) + " but failed to throw an exception.");

            throw fakeException;
        }

        /// <summary>
        /// Test that an action throws an exception of 
        /// type <typeparamref name="TException"/> that 
        /// contains a message substring.
        /// </summary>
        /// <typeparam name="TException">Expected exception type</typeparam>
        /// <param name="action">Action that should throw exception</param>
        /// <param name="containsMessage"></param>
        /// <returns>Exception thrown</returns>
        public static async Task<TException> ShouldThrowExceptionContainingAsync<TException>(Func<Task> action, string containsMessage)
            where TException : Exception
        {
            try
            {
                await action();
            }
            catch (TException e)
            {
                if (e.Message.Contains(containsMessage)) 
                {
                    return e;
                }

                ShouldBeMessage.Fail(e.Message, containsMessage, "Should throw " + typeof(TException) + " containing");
            }
            catch (Exception e)
            {
                ShouldBeMessage.Fail(e.GetType(), typeof(TException), "Should throw exception");
            }

            Assert.Fail("Should throw " + typeof(TException) + " but failed to throw an exception.");

            throw fakeException;
        }
#endif

    }
}