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
    public class TestEnvironment
    {
        public bool CanReadSourceCode { get; set; }
        public string ShouldMethod { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
    }

    public class ShouldBeMessage
    {
        public static void Fail<T>(T expected)
        {
            Assert.Fail(GetMessage(expected));
        }

        public static void Fail<T>(T actual, T expected)
        {
            Assert.Fail(GetMessage(actual, expected));
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

        public static string GetMessage<T>(T expected)
        {
            return string.Format("{0} {1}\n    {2}",
                GetContext(),
                expected is BinaryExpression ? "an element satisfying the condition" : "", 
                expected);
        }

        private static string GetContext()
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

            return string.Format("{0}\n  {1}", codePart, environment.ShouldMethod.PascalToSpaced());
        }

        private static TestEnvironment GetStackFrameForOriginatingTestMethod()
        {
            var stackTrace = new StackTrace(true);
            var i = 0;
            var shouldlyFrame = stackTrace.GetFrame(i);
            if (shouldlyFrame == null) throw new Exception("Unable to find test method");

            while (!shouldlyFrame.GetMethod().DeclaringType.GetCustomAttributes(typeof(ShouldBeMethodsAttribute), true).Any())
            {
                shouldlyFrame = stackTrace.GetFrame(++i);
            }
            var originatingFrame = stackTrace.GetFrame(i+1);

            string sourceFile = originatingFrame.GetFileName();

            return new TestEnvironment
                       {
                           CanReadSourceCode = sourceFile != null && File.Exists(sourceFile),
                           ShouldMethod = shouldlyFrame.GetMethod().Name,
                           FileName = sourceFile,
                           LineNumber = originatingFrame.GetFileLineNumber() - 1
                       };
        }
    }
}