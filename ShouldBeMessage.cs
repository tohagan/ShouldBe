using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

using ShouldBe.DifferenceHighlighting;

namespace ShouldBe
{
    /// <summary/>
    public class TestEnvironment
    {
        /// <summary/>
        public bool CanReadSourceCode { get; set; }
        /// <summary/>
        public string ShouldMethod { get; set; }
        /// <summary/>
        public string FileName { get; set; }
        /// <summary/>
        public int LineNumber { get; set; }
    }

    /// <summary>
    /// Helper class for creating assertion failure messages.
    /// </summary>
    /// <remarks>
    /// Fails using NUnit's <see cref="Assert.Fail(string)"/> method 
    /// to ensure that ShouldBe works with tools like ReSharper 
    /// that are designed to integrate with NUnit.
    /// </remarks>
    internal class ShouldBeMessage
    {
        public static void FailActualIfNull<T>(T actual)
        {
            if (actual == null)
            {
                Assert.Fail(GetMessageActualIsNull());
            }
        }

        public static void FailActual<T>(T actual)
        {
            Assert.Fail(GetMessageActual(actual));
        }

        public static void FailExpecting<T>(T expected)
        {
            Assert.Fail(GetMessageExpecting(expected));
        }

        public static void FailExpectingElement<T>(T expectedElement)
        {
            Assert.Fail(GetMessageExpectingElement(expectedElement));
        }

        public static void FailExpectingFormatted(string expected)
        {
            Assert.Fail(GetMessageExpectingFormatted(expected));
        }

        public static void Fail<T>(T actual, T expected)
        {
            Assert.Fail(GetMessage(actual, expected));
        }

        public static void Fail<T>(IEnumerable<T> actual, T expected, T tolerance)
        {
            Assert.Fail(GetMessageWithTolerance(actual, expected, tolerance));
        }

        public static void Fail<T>(IEnumerable<T> actual, T expected)
        {
            Assert.Fail(GetMessage(actual, expected));
        }

        public static string GetMessage<T1, T2>(T1 actual, T2 expected)
        {
            string message = string.Format("{0}\n    {1}\n        but was\n    {2}",
                GetContext(), expected.Inspect(), actual.Inspect());

            if (actual.CanGenerateDifferencesBetween(expected))
            {
                message += string.Format("\n        difference\n    {0}",
                actual.HighlightDifferencesBetween(expected));
            }

            return message;
        }

        public static string GetMessageWithTolerance<T>(IEnumerable<T> actual, T expected, T tolerance)
        {
            return string.Format("{0}\n    {1} (+/- {2})\n        but was\n    {3}",
                GetContext(), expected, tolerance, actual.Inspect());
        }
        
        public static string GetMessageActual<T>(T actual)
        {
            string context = GetContext();
            return string.Format("{0} but was\n    {1}", context, actual.Inspect());
        }

        public static string GetMessageActualIsNull()
        {
            string context = GetContext(true);
            return string.Format("{0} should not be null", context);
        }

        public static string GetMessageExpecting<T>(T expected)
        {
            string context = GetContext();
            return string.Format("{0}\n    {1}", context, expected.Inspect());
        }

        public static string GetMessageExpectingFormatted(string expected)
        {
            string context = GetContext();
            return string.Format("{0}\n    {1}", context, expected);
        }

        public static string GetMessageExpectingElement<T>(T expectedElement)
        {
            string context = GetContext();
            return string.Format("{0} an element satisfying the condition\n    {1}",
                context, expectedElement.Inspect());
        }

        private static string GetContext()
        {
            return GetContext(false);
        }

        private static string GetContext(bool codeOnly)
        {
            var environment = GetStackFrameForOriginatingTestMethod();
            var codePart = "The provided expression";

            if (environment.CanReadSourceCode)
            {
                var possibleCodeLines = File.ReadAllLines(environment.FileName)
                                            .Skip(environment.LineNumber).ToArray();
                var codeLines = possibleCodeLines.DelimitWith("\n");

                var shouldMethodIndex = codeLines.IndexOf(environment.ShouldMethod);
                codePart = shouldMethodIndex > -1 ?
                    codeLines.Substring(0, shouldMethodIndex - 1).Trim() :
                    possibleCodeLines[0];

                if (codePart.StartsWith("() => ")) codePart = codePart.Substring(6);
            }

            if (codeOnly)
            {
                return codePart;
            }
            else
            {
                return string.Format("{0}\n  {1}", codePart, environment.ShouldMethod.CamelCasedToSpaced());
            }
        }

        private static TestEnvironment GetStackFrameForOriginatingTestMethod()
        {
            var stackTrace = new StackTrace(true);
            var i = 0;
            var shouldbeFrame = stackTrace.GetFrame(i);
            if (shouldbeFrame == null) throw new Exception("Unable to find test method");

            while (!shouldbeFrame.GetMethod().DeclaringType.GetCustomAttributes(typeof(ShouldBeMethodsAttribute), true).Any())
            {
                shouldbeFrame = stackTrace.GetFrame(++i);
            }

            var originatingFrame = stackTrace.GetFrame(i+1);

            string sourceFile = originatingFrame.GetFileName();

            return new TestEnvironment
                {
                    CanReadSourceCode = sourceFile != null && File.Exists(sourceFile),
                    ShouldMethod = shouldbeFrame.GetMethod().Name,
                    FileName = sourceFile,
                    LineNumber = originatingFrame.GetFileLineNumber() - 1
                };
        }
    }
}