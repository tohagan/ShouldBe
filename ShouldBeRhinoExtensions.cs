using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;
using NUnit.Framework;

namespace ShouldBe
{
    /// <summary>
    /// Rhino Moch text extensions
    /// </summary>
    public static class ShouldBeRhinoExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock"></param>
        /// <param name="action"></param>
        public static void ShouldHaveBeenCalled<T>(this T mock, Expression<Action<T>> action)
        {
            try
            {
                mock.AssertWasCalled(action.Compile());
            }
            catch (ExpectationViolationException)
            {
                var methodCalls = mock.GetArgumentsForCallsMadeOn(action.Compile());

                var body = action.Body.As<MethodCallExpression>();

                var expectedCall = MethodCall(body.Method.Name, body.Arguments.Cast<object>());
                var recordedCalls = methodCalls.Select(args => MethodCall(body.Method.Name, args));

                Assert.Fail("*Expecting*\n    {0}\n*Recorded*\n{1}", 
                    expectedCall, 
                    recordedCalls.Select((c, i) => string.Format("{0: 0}: {1}", i, c)).DelimitWith("\n"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static string MethodCall(string name, IEnumerable<object> arguments)
        {
            return string.Format("{0}({1})", 
                                 name,
                                 arguments.Select(a => a.Inspect()).DelimitWith(", "));
        }
   }
}